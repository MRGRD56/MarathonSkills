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

			RaceKitOption raceKit = null;
			if (OptionA.IsChecked == true) raceKit = App.DbContext.RaceKitOptions.ToList()[0];
			else
			if (OptionB.IsChecked == true) raceKit = App.DbContext.RaceKitOptions.ToList()[1];
			else
			if (OptionC.IsChecked == true) raceKit = App.DbContext.RaceKitOptions.ToList()[2];


			//Email, Password, FirstName, LastName, Gender, byte[] Photo, DateOfBirth, Country.CountryCode
			////TABLES: Registration, Runner, User
			var user = new User
			{
				Email = this.Email,
				Password = this.Password,
				FirstName = this.FirstName,
				LastName = this.LastName,
				Role = App.DbContext.Roles.Where(x => x.RoleId == "R").First()
			};
			App.DbContext.Users.Add(user);
			App.DbContext.SaveChanges(); //TODO: EXCEPTION
			/* SqlException: Нарушено "pk_User" ограничения PRIMARY KEY. Не удается вставить повторяющийся ключ в объект "dbo.User". 
			   Повторяющееся значение ключа: (login@gmail.com). Выполнение данной инструкции было прервано.*/

			var runner = new Runner
			{
				Email = App.DbContext.Users.ToList().Last().Email,
				Gender1 = this.Gender,
				DateOfBirth = this.BirthDate,
				CountryCode = this.Country.CountryCode,
				Photo = ImagePath.ImgToByteArray()
			};
			App.DbContext.Runners.Add(runner);
			App.DbContext.SaveChanges();

			var registration = new Registration
			{
				RunnerId = App.DbContext.Runners.ToList().Last().RunnerId,
				RegistrationDateTime = DateTime.Now,
				RaceKitOption = raceKit,
				RegistrationStatusId = App.DbContext.RegistrationStatus.ToList()[0].RegistrationStatusId,
				Cost = (decimal)AllSum,
				CharityId = ((Charity)CharityCB.SelectedItem).CharityId,
				SponsorshipTarget = 0
			};
			App.DbContext.Registrations.Add(registration);
			App.DbContext.SaveChanges();

			MW.Auth(App.DbContext.Users.Last());
			MW.MainFrame.Navigate(new Pages.RunnerRegistrationConfirmationPage(MW));
		}
	}
}
