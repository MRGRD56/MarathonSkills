using MarathonSkills.WpfApp.Extensions;
using MarathonSkills.WpfApp.ViewModel;
using Microsoft.Win32;
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
using static MarathonSkills.WpfApp.Extensions.MessageBoxExtension;

namespace MarathonSkills.WpfApp.Pages
{
	/// <summary>
	/// Логика взаимодействия для RunnerRegistrationPage.xaml
	/// </summary>
	public partial class RunnerRegistrationPage : Page
	{
		private MainWindow MW { get; }

		public RunnerRegistrationPageVM ThisContext { get; set; } = new RunnerRegistrationPageVM();

		private string selectedImage;
		private string SelectedImage
		{
			get => selectedImage;
			set
			{
				selectedImage = value;
				if (selectedImage.IsNotNull())
				{
					DefaultImage.Visibility = Visibility.Collapsed;
					RunnerPhoto.Visibility = Visibility.Visible;
				}
			}
		}
		private string SelectedImageName { get; set; }

		public RunnerRegistrationPage(MainWindow mw)
		{
			InitializeComponent();
			MW = mw;

			DataContext = ThisContext;
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			MW.MainFrame.GoBack();
		}

		private void LoadPhotoButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var fd = new OpenFileDialog();
				fd.Filter = "Image files (*.png;*.jpeg;*.jpg;*.bmp)|*.png;*.jpeg;*.jpg;*.bmp";//|All files (*.*)|*.*";
				fd.ShowDialog();
				RunnerPhoto.SetSource(fd.FileName);
				SelectedImage = fd.FileName;
				
				SelectedImageName = System.IO.Path.GetFileName(SelectedImage);
				FileNameTB.Text = SelectedImageName;
			}
			catch (NotSupportedException)
			{
				ShowError("Ошибка загрузки файла.");
			}			
		}

		private void RegistrationButton_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
