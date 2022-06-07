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

namespace Flowers.Pages
{
    /// <summary>
    /// Interaction logic for Page_login.xaml
    /// </summary>
    public partial class Page_login : Page
    {

        List<User> users = Classes.Manager.DBContext.User.ToList();
        List<Role> roles = Classes.Manager.DBContext.Role.ToList();
        public Page_login()
        {
            InitializeComponent();
        }

        private void btn_login_guest_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Pages.Page_main("Гость", "Гость"));
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            string login = tb_login.Text;
            string password = tb_password.Password;
            bool auth = false;
            string fullname = "";
            int idrole = 3;
            string role = "";

            if (login == "" || password == "")
            {
                MessageBox.Show("Заполните поля");
            }
            else
            {
                foreach (var item in this.users)
                {
                    if (item.UserLogin == login && item.UserPassword == password)
                    {
                        auth = true;
                        fullname = $"{item.UserSurname} {item.UserName} {item.UserPatronymic}";
                        idrole = item.UserRole;

                        
                        foreach (var item_role in this.roles) 
                        {
                            if (item_role.RoleID == idrole)
                                role = item_role.RoleName;
                        }
                        break;
                    }
                }
                if (auth == false)
                    MessageBox.Show("Неверный логин или пароль");
                else
                    NavigationService.Navigate(new Pages.Page_main(fullname, role));
            }
        }
    }
}
