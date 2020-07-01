using MarathonSkills.WpfApp.DataModels;
using MarathonSkills.WpfApp.Extensions;
using MarathonSkills.WpfApp.Windows;
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
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        private MainWindow MW { get; }

        private ChosenType ChosenType { get; set; }

        private string ChosenTypeString { get; set; }

        public AuthorizationPage(MainWindow mw)
        {
            InitializeComponent();
            MW = mw;
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            MW.FrameGoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.CurrentUser != null)
            {
                MW.GoToMenu(App.CurrentUser.RoleId);
                return;
            }

            var win = new AuthTypeWindow(MW);
            win.ShowDialog();
            ChosenType = win.ChosenType;
            ChosenTypeString = Convert.ToString(ChosenType.ToString()[0]);

            //MessageBox.Show(ChosenTypeString);
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(EmailTB.Text.IsNotNull() && PasswordPB.Password.IsNotNull()))
            {
                ShowError("Не все поля заполнены!");
                return;
            }
            var user = App.DbContext.Users
	            .FirstOrDefault(x => x.Email == EmailTB.Text.Trim() &&
	                                 x.Password == PasswordPB.Password &&
	                                 x.RoleId == ChosenTypeString);
            if (user == default)
            {
                ShowError("Неверный логин или пароль!");
                return;
            }

            MW.Auth(user);

            MW.GoToMenu(ChosenTypeString);
        }

        private void ShowError(string msg) => MessageBox.Show(msg, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
