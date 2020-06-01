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
	/// Логика взаимодействия для SponsorshipConfirmationPage.xaml
	/// </summary>
	public partial class SponsorshipConfirmationPage : Page
	{
		private MainWindow MW { get; }

		public SponsorshipConfirmationPage(MainWindow mw, string runnerName, string charityName, string sum)
		{
			InitializeComponent();
			MW = mw;

			RunnerInfoLabel.Content = runnerName;
			CharityNameLabel.Content = charityName;
			SumLabel.Content = sum;
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			MW.MainFrame.Navigate(new MainMenuPage(MW));
		}
	}
}
