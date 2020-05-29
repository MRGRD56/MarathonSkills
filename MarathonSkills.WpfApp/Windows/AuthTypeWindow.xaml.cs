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
using System.Windows.Shapes;

namespace MarathonSkills.WpfApp.Windows
{
	/// <summary>
	/// Логика взаимодействия для AuthTypeWindow.xaml
	/// </summary>
	public partial class AuthTypeWindow : Window
	{
		private MainWindow MW { get; }

		public ChosenType ChosenType { get; set; } = ChosenType.None;

		public AuthTypeWindow(MainWindow mw)
		{
			InitializeComponent();
			MW = mw;
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (ChosenType == ChosenType.None)
				MW.MainFrame.GoBack();
		}

		private void SetAuthType(ChosenType type)
		{
			ChosenType = type;
			Close();
		}
		
		private void RunnerButton_Click(object sender, RoutedEventArgs e) => SetAuthType(ChosenType.Runner);

		private void CoordinatorButton_Click(object sender, RoutedEventArgs e) => SetAuthType(ChosenType.Coordinator);

		private void AdministratorButton_Click(object sender, RoutedEventArgs e) => SetAuthType(ChosenType.Administrator);
	}

	public enum ChosenType
	{
		Runner,
		Coordinator,
		Administrator,

		None
	}
}
