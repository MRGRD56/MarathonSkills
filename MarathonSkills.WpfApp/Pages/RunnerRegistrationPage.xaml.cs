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
using static MarathonSkills.WpfApp.Extensions.RegexUtilities;

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
			#region ПРОВЕРКА ДАННЫХ
			bool isAllFilled =
				EmailTB.Text.IsNotNull() &&
				Password1PB.Password.IsNotNull() &&
				Password2PB.Password.IsNotNull() &&
				FirstNameTB.Text.IsNotNull() &&
				LastNameTB.Text.IsNotNull() &&
				GenderCB.SelectedItem != null &&
				RunnerPhoto.Source != null &&
				BirthDateDP.SelectedDate != null &&
				CountryCB.SelectedItem != null;
			if (!isAllFilled)
			{
				ShowError("Не все поля заполнены!");
				return;
			}

			if (!IsValidEmail(EmailTB.Text))
			{
				ShowError("Email введён неверно!");
				return;
			}

			if (!IsValidPassword(Password1PB.Password))
			{
				ShowError(
					"Пароль должен отвечать следующим требованиям:\n" +
					"Минимум 6 символов\n" +
					"Минимум 1 прописная буква\n" +
					"Минимум 1 цифра\n" +
					"По крайней мере один из следующих символов: ! @ # $ % ^");
				return;
			}

			if (Password1PB.Password != Password2PB.Password)
			{
				ShowError("Введённые пароли не совпадают!");
				return;
			}

			//TODO: может неправильно определяться возраст, изменить
			if (!(DateTime.Now.Subtract(BirthDateDP.SelectedDate.Value).TotalDays > 365.25 * 10))
			{
				ShowError("Бегуну должно быть не менее 10 лет!");
				return;
			}
			#endregion


		}
	}
}
