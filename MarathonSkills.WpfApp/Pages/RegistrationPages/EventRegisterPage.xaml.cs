using MarathonSkills.WpfApp.DataModels;
using MarathonSkills.WpfApp.Extensions;
using MarathonSkills.WpfApp.ViewModel;
using System;
using System.Collections.Generic;
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
	/// Логика взаимодействия для EventRegisterPage.xaml
	/// </summary>
	public partial class EventRegisterPage : Page
	{
		private MainWindow MW { get; }

		private string Email;
		private string Password;
		private string FirstName;
		private string LastName;
		private Gender Gender;
		private string ImagePath;
		private DateTime BirthDate;
		private Country Country;

		private double allSum;
		private double AllSum
		{
			get => allSum;
			set
			{
				allSum = value;
				if (AllSumLabel != null)
					AllSumLabel.Content = $"${allSum}";
			}
		}

		public EventRegisterPage(MainWindow mw, string email, string pswd, string fName, string lName,
			Gender gender, string imgPath, DateTime birthDate, Country country)
		{
			InitializeComponent();
			MW = mw;
			//EmailTB, Password1PB, FirstNameTB, LastNameTB, GenderCB, SelectedImage, BirthDateDP, CountryCB
			(Email, Password, FirstName, LastName, Gender, ImagePath, BirthDate, Country) =
				(email, pswd, fName, lName, gender, imgPath, birthDate, country);

			DataContext = new EventRegisterPageVM();
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			MW.MainFrame.GoBack();
		}

		private void ShowCharityInfo(object sender, MouseButtonEventArgs e)
		{
			var charity = (Charity)CharityCB.SelectedItem;

			if (charity != null)
			{
				new Windows.CharityInfoWindow(charity.CharityName, $"/Images/CharityLogos/{charity.CharityLogo}", charity.CharityDescription).ShowDialog();
			}
		}

		private void UpdateAllSum()
		{
			var sum = 0d;

			EventTypeCBs?.Children
				.OfType<CheckBox>()
				.ToList()
				.ForEach(x =>
				{
					if (x.IsChecked == true)
						sum += x.Tag.ToDouble();
				});

			ComplectsRBs?.Children
				.OfType<RadioButton>()
				.ToList()
				.ForEach(x =>
				{
					if (x.IsChecked == true)
						sum += x.Tag.ToDouble();
				});

			sum += SumTB.Text.ToDouble();

			AllSum = sum;
		}

		private void NumberTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			char inputSymbol = e.Text[0];
			e.Handled = !(char.IsDigit(inputSymbol) || inputSymbol == ',');
		}

		private void CheckBox_CheckedUnchecked(object sender, RoutedEventArgs e) => UpdateAllSum();

		private void SumTB_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(SumTB.Text) || SumTB.Text.ToDouble() < 0)
				{
					SumTB.Text = "0";
				}

				UpdateAllSum();
			}
			catch (FormatException) { }
		}

		private void RegistrationButton_Click(object sender, RoutedEventArgs e)
		{
			#region ПРОВЕРКА ДАННЫХ
			////НАЛИЧИЕ ДАННЫХ
			//EventTypeCBs
			var b1 = EventTypeCBs.Children.OfType<CheckBox>().ToList()
				.Where(x => x.IsChecked == true).Any();
			//CharityCB
			var b2 = CharityCB.SelectedItem != null;
			//ComplectsRBs
			var b3 = ComplectsRBs.Children.OfType<RadioButton>().ToList()
				.Where(x => x.IsChecked == true).Any();

			if (!(b1 && b2 && b3))
			{
				MBoxExt.ShowError("Не все поля заполнены!");
				return;
			}
			#endregion

			
		}
	}
}
