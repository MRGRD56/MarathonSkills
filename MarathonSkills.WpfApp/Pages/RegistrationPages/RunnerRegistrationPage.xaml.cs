using MarathonSkills.WpfApp.DataModels;
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
using static MarathonSkills.WpfApp.Extensions.MBoxExt;
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

			if (App.DbContext.Users.Where(x => x.Email.ToLower() == EmailTB.Text.Trim().ToLower()).Any())
			{
				ShowError("Пользователь с указаным email уже зарегистрирован!");
				return;
			}
			#endregion

			//EmailTB, Password1PB, FirstNameTB, LastNameTB, GenderCB, SelectedImage, BirthDateDP, CountryCB
			var email = EmailTB.Text.Trim().ToLower();
			var pswd = Password1PB.Password;
			var fName = FirstNameTB.Text.Trim();
			var lName = LastNameTB.Text.Trim();
			var gender = (Gender)GenderCB.SelectedItem;
			var imgPath = SelectedImage;
			var bDate = BirthDateDP.SelectedDate.Value;
			var country = (Country)CountryCB.SelectedItem;


			MW.MainFrame.Navigate(new EventRegisterPage(MW, email, pswd, fName, lName, gender, imgPath, bDate, country));
		}

		//private void DebugButton_Click(object sender, RoutedEventArgs e)
		//{
		//	var email = $"test{new Random().Next(1000, int.MaxValue)}@gmail.com";
		//	var pswd = "123qwe@";
		//	var fName = "Lorem";
		//	var lName = "Ipsum";
		//	var gender = App.DbContext.Genders.ToList().First();
		//	var imgPath = @"D:\1\Downloads\TestPhoto.png";
		//	var bDate = DateTime.Parse("2002-11-22");
		//	var country = App.DbContext.Countries.ToList().First();

		//	if (App.DbContext.Users.Where(x => x.Email.ToLower() == EmailTB.Text.Trim().ToLower()).Any())
		//	{
		//		ShowError("Пользователь с указаным email уже зарегистрирован!");
		//		return;
		//	}

		//	MW.MainFrame.Navigate(new EventRegisterPage(MW, email, pswd, fName, lName, gender, imgPath, bDate, country));
		//}
	}
}
