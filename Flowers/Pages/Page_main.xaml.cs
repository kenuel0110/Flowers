using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Flowers.Pages
{
    /// <summary>
    /// Interaction logic for Page_main.xaml
    /// </summary>
    public partial class Page_main : Page
    {
        string fullname;
        string role;

        List<Product> products = Classes.Manager.DBContext.Product.ToList();
        List<Mafactures> manufacturs = Classes.Manager.DBContext.Mafactures.ToList();
        List<Product> listview_list = new List<Product>();
        List<Product> filter_list = new List<Product>();
        public Page_main(string fullname, string role)
        {
            InitializeComponent();
            this.fullname = fullname;
            this.role = role;
            lbl_fullname.Text = fullname;
            init();
            List<String> list_combobox = new List<string>();
            list_combobox.Add("Все производители");
            foreach (var item in this.manufacturs) 
            {
                list_combobox.Add(item.Name);
            }
            cb_manufacture.ItemsSource = list_combobox;
            cb_manufacture.SelectedIndex = 0;
        }

        private void init()
        {
            this.listview_list.Clear();
            this.listview_list = this.products;
            this.filter_list = this.listview_list;
            lv_products.ItemsSource = listview_list;
        }

        private void lv_products_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (lv_products.SelectedIndex != -1 && this.role == "Администратор") 
            {
                DialogResult dialog = System.Windows.Forms.MessageBox.Show("После удаления продукт нельзя осстановить","Удаление", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    string artucule = this.filter_list[lv_products.SelectedIndex].ProductArticleNumber;
                    int i = 0;
                    foreach (var item in products)
                    {
                        if (item.ProductArticleNumber == artucule)
                        {
                            Classes.Manager.DBContext.Product.Remove(products[i]);
                            Classes.Manager.DBContext.SaveChanges();
                            init();
                            break;
                        }
                        i++;
                    }
                }
                
            }
        }

        private void filter_list(string filter, string _object) 
        {
            this.filter_list.Clear()
            if (_object == "cb") 
            {
                this.filter_list = listview_list.FindAll(x => x.ProductManufacturer == filter);
                lbl_filter.Text = $"{this.filter_list.Count} из {listview_list.Count}";
                lv_products.ItemsSource = listview_list.FindAll(x => x.ProductManufacturer == filter);
                
            }
            if (_object == "search")
            {
                this.filter_list = listview_list.FindAll(x => x.ProductName.ToLower().Contains(filter.ToLower()));
                lbl_filter.Text = $"{this.filter_list.Count} из {listview_list.Count}";
                lv_products.ItemsSource = listview_list.FindAll(x => x.ProductName.ToLower().Contains(filter.ToLower()));;
            }
        }

        private void cb_manufacture_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_manufacture.SelectedIndex == 0)
            {
                init();
                lbl_filter.Visibility = Visibility.Hidden;
            }
            else
            {
                filter_list(cb_manufacture.SelectedItem.ToString(), "cb");
                lbl_filter.Visibility = Visibility.Visible;
            }
        }

        private void tb_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_search.Text == "")
            {
                init();
                lbl_filter.Visibility = Visibility.Hidden;
            }
            else
            {
                filter_list(tb_search.Text.ToString(), "search");
                lbl_filter.Visibility = Visibility.Visible;
            }
        }

        private void btn_add_page_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate();
        }
    }
}
