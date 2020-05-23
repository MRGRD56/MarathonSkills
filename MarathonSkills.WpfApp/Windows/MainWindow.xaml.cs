using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
//using Microsoft.Win32;

namespace MarathonSkills.WpfApp
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			//Изменение оставшегося времени каждую секунду.
			RemainingTimeSetting(); //async
			MainFrame.Navigate(new Pages.MainMenuPage(this));
		}

		/// <summary>
		/// Изменяет верхнюю панель на уменьшенную версию.
		/// </summary>
		public void ShowHeaderMini()
		{
			MainGrid.RowDefinitions[0].Height = new GridLength(45);
			HeaderFull.Visibility = Visibility.Collapsed;
			HeaderMini.Visibility = Visibility.Visible;
		}

		/// <summary>
		/// Изменяет верхнюю панель на широкую версию (для главного меню).
		/// </summary>
		public void ShowHeaderFull()
		{
			MainGrid.RowDefinitions[0].Height = new GridLength(140);
			HeaderMini.Visibility = Visibility.Collapsed;
			HeaderFull.Visibility = Visibility.Visible;
		}

		/// <summary>
		/// Возвращает кортеж: оставшееся время и логическое значение, начался ли марафон.
		/// </summary>
		/// <returns></returns>
		private Tuple<TimeSpan, bool> GetRemainingTime()
		{
			//берём время начала - 6:00:00 24.11.2020
			var startTime = new DateTime(2020, 11, 24, 6, 0, 0);
			//если марафон начался или прошёл
			if (DateTime.Now.CompareTo(startTime) >= 0)
				return new Tuple<TimeSpan, bool>(default, true);
			//иначе вычисляем оставшееся время и возвращаем
			var remainingTime = startTime.Subtract(DateTime.Now);
			return new Tuple<TimeSpan, bool>(remainingTime, false);
		}

		/// <summary>
		/// Возвращает слово, указываемое после количества дней.
		/// </summary>
		/// <param name="d">Количество дней.</param>
		private string GetDaysWord(int d)
		{
			var ds = d.ToString();
			if (ds.Length == 1) 
			{
				if (d == 0 || d > 4)
					return "дней";
				if (d == 1)
					return "день";
				return "дня";
			}
			if (ds[ds.Length - 2] != '1')
			{
				if (ds[ds.Length - 1] == '1')
					return "день";
				var dLast = Convert.ToInt32(ds[ds.Length - 1].ToString());
				if (dLast.IsBeth(2, 4))
					return "дня";
			}
			return "дней";
		}

		/// <summary>
		/// Возвращает слово, указываемое после количества часов.
		/// </summary>
		/// <param name="h">Количество часов.</param>
		private string GetHoursWord(int h)
		{
			if (h == 1 || h == 21)
				return "час";
			if (h.IsBeth(2, 4) || h.IsBeth(22, 24))
				return "часа";
			return "часов";
		}

		/// <summary>
		/// Возвращает слово, указываемое после количества минут.
		/// </summary>
		/// <param name="m">Количество минут.</param>
		private string GetMinutesWord(int m)
		{
			if (m == 1 || m == 21 || m == 31 || m == 41 || m == 51)
				return "минута";
			if (m.IsBeth(2, 4) || m.IsBeth(22, 24) || m.IsBeth(32, 34) || m.IsBeth(42, 44) || m.IsBeth(52, 54))
				return "минуты";
			return "минут";
		}

		/// <summary>
		/// Возвращает слово, указываемое после количества секунд.
		/// </summary>
		/// <param name="s">Количество секунд.</param>
		private string GetSecondsWord(int s)
		{
			if (s == 1 || s == 21 || s == 31 || s == 41 || s == 51)
				return "секунда";
			if (s.IsBeth(2, 4) || s.IsBeth(22, 24) || s.IsBeth(32, 34) || s.IsBeth(42, 44) || s.IsBeth(52, 54))
				return "секунды";
			return "секунд";
		}

		/// <summary>
		/// Изменяет отображаемое оставшееся время.
		/// </summary>
		/// <param name="time">Отображаемый промежуток времени.</param>
		private void ChangeRemainingTime(TimeSpan time)
		{
			var d = time.Days;
			var dd = GetDaysWord(d);
			var h = time.Hours;
			var hh = GetHoursWord(h);
			var m = time.Minutes;
			var mm = GetMinutesWord(m);
			var s = time.Seconds;
			var ss = GetSecondsWord(s);
			RemainingTimeLabel.Content = $"{d} {dd} {h} {hh} {m} {mm} {s} {ss} до старта марафона!";
			//строка по типу "23 дня 6 часов 1 минута 56 секунд до старта марафона!"
		}

		/// <summary>
		/// Цикличная установка оставшегося времени.
		/// </summary>
		private async void RemainingTimeSetting()
		{
			while (true)
			{
				var remTime = GetRemainingTime();
				//если марафон не начался, обновляем оставшееся время
				if (!remTime.Item2)
					ChangeRemainingTime(GetRemainingTime().Item1);
				//иначе, выводим, что марафон закончился, и выходим из цикла
				else
				{
					RemainingTimeLabel.Content = "Марафон уже начался!";
					break;
				}
				//остановка потока на 1 с.
				await Task.Delay(1000);
			}
		}

		//private async void AsyncRemainingTimeSetting()
		//{
		//    //await Task.Run(() => RemainingTimeSetting());
		//    await Task.Factory.StartNew(() => RemainingTimeSetting(), TaskCreationOptions.LongRunning);
		//    //await Dispatcher.BeginInvoke((Action)(() => RemainingTimeSetting()));
		//}

		private void FrameGoBack_Click(object sender, RoutedEventArgs e) => FrameGoBack();

		public void FrameGoBack()
		{
			if (MainFrame.CanGoBack)
				MainFrame.GoBack();
		}

		//Событие при каждой навигации: задаёт нужную верхнюю панель.
		private void MainFrame_Navigated(object sender, NavigationEventArgs e)
		{
			if (((Page)MainFrame.Content).Title == "MainMenuPage")
				ShowHeaderFull();
			else
				ShowHeaderMini();

			SetActualWindowTitle();
		}

		private void SetActualWindowTitle()
		{
			var curPage = (Page)MainFrame.Content;
			if (curPage.Title == "MainMenuPage")
				Title = "Marathon Skills 2020";
			else
				Title = $"Marathon Skills 2020 - {curPage.Title}";
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.Escape:
					FrameGoBack();
					break;
				default:
					break;
			}
		}
	}
}
