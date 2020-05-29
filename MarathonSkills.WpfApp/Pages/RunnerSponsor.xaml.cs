using MarathonSkills.WpfApp.DataModels;
using MarathonSkills.WpfApp.Extensions;
using MarathonSkills.WpfApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MarathonSkills.WpfApp.Pages
{
	/// <summary>
	/// Логика взаимодействия для RunnerSponsor.xaml
	/// </summary>
	public partial class RunnerSponsor : Page
	{
		private MainWindow MW { get; }

		private RunnerSporsorPageVM thisDataContext { get; } = new RunnerSporsorPageVM();

		public RunnerSponsor(MainWindow mw)
		{
			InitializeComponent();
			MW = mw;

			DataContext = thisDataContext;

			//var col = App.DbContext.Users.ToObsCol();
			//var selCol = col.Select(x => $"{x.FirstName} {x.LastName}");
			//RunnersCB.ItemsSource = selCol;
			//RunnersCB.ItemsSource = new List<string> { "first", "second", "third" };
		}

		private void ShowCharityInfo(object sender, MouseButtonEventArgs e)
		{
			var charity = thisDataContext.SelectedCharity;

			if (charity != null)
			{ 
				new Windows.CharityInfoWindow(charity.CharityName, $"/Images/CharityLogos/{charity.CharityLogo}", charity.CharityDescription).ShowDialog();
			}
		}

		private void SumTB_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(SumTB.Text) || Convert.ToInt32(SumTB.Text) < 0)
			{
				SumTB.Text = "0";
			}

			SumLabel.Content = $"${SumTB.Text}";
		}

		private void NumberTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			char inputSymbol = e.Text[0];
			e.Handled = !char.IsDigit(inputSymbol);
		}

		private void IncrementSum(int n) => SumTB.Text = (Convert.ToInt32(SumTB.Text) + n).ToString();
		
		private void MinusButton_Click(object sender, RoutedEventArgs e)
		{
			IncrementSum(-10);
		}

		private void PlusButton_Click(object sender, RoutedEventArgs e)
		{
			IncrementSum(10);
		}

		private void PayButton_Click(object sender, RoutedEventArgs e)
		{
			#region ПРОВЕРКА ДАННЫХ
			var isAllFilled = SponsorNameTB.Text.IsNotNull() && RunnersCB.SelectedItem != null && CardOwnerTB.Text.IsNotNull()
				&& CardNumberTB.Text.IsNotNull() && CardMonthTB.Text.IsNotNull() && CardYearTB.Text.IsNotNull() && CardCvcTB.Text.IsNotNull();
			if (!isAllFilled)
			{
				ShowInputError("Не все поля заполнены!");
				return;
			}

			if (SumTB.Text.ToInt() <= 0)
			{
				ShowInputError("Пожалуйста, выберите сумму пожертвования.");
				return;
			}

			var isCnValid = CardNumberTB.Text.Length == 16;
			var isCvcValid = CardCvcTB.Text.Length == 3;

			var m = CardMonthTB.Text.ToInt();
			var y = CardYearTB.Text.ToInt();
			// 05/2020 - дата истечения срока - 01.06.2020 00:00:00
			var dt = new DateTime(y, m, 1, 0, 0, 0).AddMonths(1); //дата и время истечения срока

			// cm - текущий месяц, cy = текущий год
			// карта действует, если m >= cm
			var isDateValid = DateTime.Now.CompareTo(dt) < 0;

			if (!(isCnValid && isCvcValid && isDateValid && isAllFilled))
			{
				ShowInputError("Данные введены неверно. Проверьте правильность ввода.");
				return;
			}
			#endregion

			#region ПЛАТЁЖ
			// coming soon...
			#endregion
			
			var runner = (RunnerExt)RunnersCB.SelectedItem;
			//string charityName = CharityNameLabel.Content.ToString();
			int sum = SumTB.Text.ToInt();
			var charity = thisDataContext.SelectedCharity;

			#region ДОБАВЛЕНИЕ СПОНСОРА В БД
			App.DbContext.Sponsorships.Add(new Sponsorship 
			{ 
				RegistrationId = runner.RegistrationId, 
				Amount = sum, 
				SponsorName = SponsorNameTB.Text.Trim() 
			});
			App.DbContext.SaveChanges();
			#endregion

			MW.MainFrame.Navigate(new SponsorshipConfirmationPage(MW, runner.ToString(), charity.CharityName, $"${sum}"));
		}

		private void ShowInputError(string msg) =>
			MessageBox.Show(msg, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			MW.MainFrame.GoBack();
		}

		private void RunnersCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var sel = (RunnerExt)RunnersCB.SelectedItem;
			//MessageBox.Show(sel.ToString());
			thisDataContext.SelectedCharity = App.DbContext.Charities.ToObsCol().Where(x => x.CharityId == sel.CharityId).FirstOrDefault();
			CharityNameLabel.Content = thisDataContext.SelectedCharity?.CharityName;
		}
	}
}
