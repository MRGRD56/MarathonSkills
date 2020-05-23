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
	/// Логика взаимодействия для CharityInfoWindow.xaml
	/// </summary>
	public partial class CharityInfoWindow : Window
	{
		public CharityInfoWindow(string orgName, string logoPath, string description)
		{
			InitializeComponent();
			HeaderLabel.Content = orgName;
			var bi = new BitmapImage();
			bi.BeginInit();
			bi.UriSource = new Uri(logoPath, UriKind.RelativeOrAbsolute);
			bi.EndInit();
			Logo.Source = bi;
			DercriptionTB.Text = description;
		}
	}
}
