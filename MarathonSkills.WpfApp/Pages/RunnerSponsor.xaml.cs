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

		public RunnerSponsor(MainWindow mw)
		{
			InitializeComponent();
			MW = mw;

			//TODO

			DataContext = new RunnerSporsorPageVM();

			//var col = App.DbContext.Users.ToObsCol();
			//var selCol = col.Select(x => $"{x.FirstName} {x.LastName}");
			//RunnersCB.ItemsSource = selCol;
			//RunnersCB.ItemsSource = new List<string> { "first", "second", "third" };
		}

		private void ShowCharityInfo(object sender, MouseButtonEventArgs e)
		{
			new Windows.CharityInfoWindow("Фонд кошек", "/Images/CharityLogos/arise-logo.png", "Some description").ShowDialog();
		}

		private void SumTB_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(SumTB.Text) || Convert.ToInt32(SumTB.Text) < 0)
			{
				SumTB.Text = "0";
			}

			SumLabel.Content = $"${SumTB.Text}";
		}

		private void SumTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
			#region ПРОВЕРКА ДАННЫХ КАРТЫ И ПЛАТЁЖ
			// coming soon...
			#endregion


		}
	}
}
