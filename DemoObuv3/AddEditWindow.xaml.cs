using DemoObuv3.ModelsDB;
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
    /// Логика взаимодействия для AddEditWindow.xaml
    /// </summary>
    public partial class AddEditWindow : Window
    {
        DemoObuvContext _db = new DemoObuvContext();

        Product _product;
        bool _isEdit;

        public AddEditWindow(Product product = null)
        {
            InitializeComponent();

            if (product == null)
            {
                _product = new Product();
                _isEdit = false;
            }
            else
            {
                _product = product;
                _isEdit = true;
                tbArticle.IsEnabled = false;

                tbArticle.Text = product.Article;
                tbProductName.Text = product.ProductName;
                tbUnit.Text = product.Unit;
                tbCost.Text = product.Cost.ToString();
                tbSupplier.Text = product.Supplier;
                tbManufacturer.Text = product.Manufacturer;
                tbCategory.Text = product.Category;
                tbSaleNow.Text = product.SaleNow.ToString();
                tbQuantity.Text = product.Quantity.ToString();
                tbDescription.Text = product.Description;
                tbPhoto.Text = product.Photo;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _product.Article = tbArticle.Text;
            _product.ProductName = tbProductName.Text;
            _product.Unit = tbUnit.Text;
            _product.Cost = Convert.ToInt32(tbCost.Text);
            _product.Supplier = tbSupplier.Text;
            _product.Manufacturer = tbManufacturer.Text;
            _product.Category = tbCategory.Text;
            _product.SaleNow = Convert.ToInt32(tbSaleNow.Text);
            _product.Quantity = Convert.ToInt32(tbQuantity.Text);
            _product.Description = tbDescription.Text;
            _product.Photo = tbPhoto.Text;

            if(_isEdit == false)
            {
                _db.Products.Add(_product);
            }
            else
            {
                _db.Products.Update(_product);
            }

            _db.SaveChanges();
            Close();
        }
    }
}
