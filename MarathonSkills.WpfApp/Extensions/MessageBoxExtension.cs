using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MarathonSkills.WpfApp.Extensions
{
	public static class MessageBoxExtension
	{
		public static void ShowError(string msg, string title = "Ошибка") =>
			MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Error);
	}
}
