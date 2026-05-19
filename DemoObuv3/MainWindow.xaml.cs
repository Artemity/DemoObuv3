using DemoObuv3.ModelsDB;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Primitives;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoObuv3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User _currentUser;
        private DemoObuvContext _db = new DemoObuvContext();
        
        public MainWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;
            mainWindow.Title = "ООО Обувь" + " " + "-" + " " + _currentUser.Role;

            ConfigureRole();
            LoadDB();
        }

        void LoadDB()
        {
            var products = _db.Products.ToList();

            string search = tbSearch.Text.ToLower();

            if (!string.IsNullOrWhiteSpace(search))
            {
                products = products.Where(x => x.ProductName.ToLower().Contains(search) ||
                x.Description.ToLower().Contains(search) ||
                x.Manufacturer.ToLower().Contains(search)).ToList();
            }

            lvProducts.ItemsSource = products;
        }


        private void ConfigureRole()
        {
            if (_currentUser.Role == "Авторизированный клиент")
            {
                mAdd.Visibility = Visibility.Collapsed;
                mEdit.Visibility = Visibility.Collapsed;
                mDel.Visibility = Visibility.Collapsed;
                tbSearch.Visibility = Visibility.Collapsed;

                mainWindow.Title = "ООО 'Обувь'" + " " + _currentUser.Role;
            }

            else if (_currentUser.Role == "Менеджер")
            {
                mAdd.Visibility = Visibility.Collapsed;
                mEdit.Visibility = Visibility.Collapsed;
                mDel.Visibility = Visibility.Collapsed;

                mainWindow.Title = "ООО 'Обувь'" + " " + _currentUser.Role;
            }

            else if ( _currentUser.Role == "Гость")
            {
                mAdd.Visibility = Visibility.Collapsed;
                mEdit.Visibility = Visibility.Collapsed;
                mDel.Visibility = Visibility.Collapsed;
                tbSearch.Visibility = Visibility.Collapsed;

                mainWindow.Title = "ООО 'Обувь'" + " " + _currentUser.Role;
            }

            tbUser.Text = $"{_currentUser.Surname} {_currentUser.Name} {_currentUser.Patronymic}";
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private void mAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditWindow addEdit = new AddEditWindow();
            addEdit.ShowDialog();
            LoadDB();
        }

        private void mEdit_Click(object sender, RoutedEventArgs e)
        {
            Product product = lvProducts.SelectedItem as Product;

            if (product == null)
            {
                MessageBox.Show("Выберите запись для редактирования !", "Ошибка");
                return;
            }
           
            AddEditWindow addEdit = new AddEditWindow(product);
            addEdit.ShowDialog();
            LoadDB();

        }

        private void mDel_Click(object sender, RoutedEventArgs e)
        {
            Product product = lvProducts.SelectedItem as Product;

            if (product == null)
            {
                MessageBox.Show("Выберите товар!", "Ошибка");
                return;
            }

            var productDelete = _db.Products.First(x => x.Article == product.Article);
            _db.Products.Remove(productDelete);
            _db.SaveChanges();

            LoadDB();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadDB();
        }

        
    }
}