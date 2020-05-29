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
                .Where(x => x.Email == EmailTB.Text.Trim() &&
                    x.Password == PasswordPB.Password &&
                    x.RoleId == ChosenTypeString)
                .FirstOrDefault();
            if (user == default)
            {
                ShowError("Неверный логин или пароль!");
                return;
            }

            App.CurrentUser = user;

            switch (ChosenType)
            {
                case ChosenType.Runner:
                    MW.MainFrame.Navigate(new RunnerMenuPage(MW));
                    break;
                case ChosenType.Coordinator:
                    MW.MainFrame.Navigate(new CoordinatorMenuPage(MW));
                    break;
                case ChosenType.Administrator:
                    MW.MainFrame.Navigate(new AdministratorMenuPage(MW));
                    break;
                default:
                    ShowError("Неизвестная ошибка.");
                    break;
            }
        }

        private void ShowError(string msg) => MessageBox.Show(msg, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
