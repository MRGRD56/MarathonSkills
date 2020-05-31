using MarathonSkills.WpfApp;
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
	/// Логика взаимодействия для MainMenuPage.xaml
	/// </summary>
	public partial class MainMenuPage : Page
	{
		public MainWindow MW { get; }

		public MainMenuPage(MainWindow mw)
		{
			InitializeComponent();
			MW = mw;

		}

		private void RunnerRegister_Click(object sender, RoutedEventArgs e)
		{
			MW.MainFrame.Navigate(new Pages.BecomeRunnerPage(MW));
		}

		private void Authorize_Click(object sender, RoutedEventArgs e)
		{
			MW.MainFrame.Navigate(new Pages.AuthorizationPage(MW));
		}

		private void GetMoreInfo_Click(object sender, RoutedEventArgs e)
		{
			MW.MainFrame.Navigate(new Pages.EventInfoMenuPage(MW));
		}

		private void BeRunnerSponsor_Click(object sender, RoutedEventArgs e)
		{
			MW.MainFrame.Navigate(new Pages.RunnerSponsorPage(MW));
		}
	}
}
