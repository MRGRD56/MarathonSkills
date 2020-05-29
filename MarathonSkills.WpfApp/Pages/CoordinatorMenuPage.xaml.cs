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
	/// Логика взаимодействия для CoordinatorMenuPage.xaml
	/// </summary>
	public partial class CoordinatorMenuPage : Page
	{
		private MainWindow MW { get; }

		public CoordinatorMenuPage(MainWindow mw)
		{
			InitializeComponent();
			MW = mw;
		}
	}
}
