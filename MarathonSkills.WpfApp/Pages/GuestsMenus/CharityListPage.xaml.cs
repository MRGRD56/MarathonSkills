﻿using MarathonSkills.WpfApp.ViewModel;
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
	/// Логика взаимодействия для CharityListPage.xaml
	/// </summary>
	public partial class CharityListPage : Page
	{
		private MainWindow MW { get; }

		public CharityListPage(MainWindow mw)
		{
			InitializeComponent();
			MW = mw;

			DataContext = new CharityListPageVM();
		}
	}
}
