using DemoObuv3.ModelsDB;
using Microsoft.EntityFrameworkCore.Diagnostics;
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

namespace DemoObuv3
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private DemoObuvContext _db = new DemoObuvContext();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text;
            string pass = tbPass.Password;

            var user = _db.Users.FirstOrDefault(u => u.Login == login && u.Password == pass);

            if (user != null)
            {
                MainWindow main = new MainWindow(user);
                main.Show();
                this.Close();
            }

            else
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка");
            }
        }

        private void btnLoginAsGuest_Click(object sender, RoutedEventArgs e)
        {
            User user = new User()
            {
                Name = "Гость",
                Surname = "",
                Patronymic = "",
                Role = "Гость"
            };

            MainWindow main = new MainWindow(user);
            main.Show();
            this.Close();
        }
    }
}
