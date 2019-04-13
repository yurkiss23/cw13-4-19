using System;
using System.Collections.Generic;
using System.Data;
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
using WpfApp1.Entites;
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для CRUDWindow.xaml
    /// </summary>
    public partial class CRUDWindow : Window
    {
        private EFContext _context;

        private static List<User> _users = new List<User>
        {
            new User{Id=1,FirstName="q",LastName="w",Email="q@gmail.com",Password="123456"},
            new User{Id=2,FirstName="a",LastName="s",Email="a@gmail.com",Password="456789"}
        };
        private void updateDT()
        {
            List<UserModel> userList = _context
                .Users.Select(u => new UserModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Password = u.Password
                }).ToList();

            DataTable dt = new DataTable();

            DataColumn id = new DataColumn("id", typeof(int));
            //id.Caption = "hello";
            DataColumn firstname = new DataColumn("firstname", typeof(string));
            DataColumn lastname = new DataColumn("lastname", typeof(string));
            DataColumn email = new DataColumn("email", typeof(string));
            DataColumn password = new DataColumn("password", typeof(string));
            dt.Columns.Add(id);
            dt.Columns.Add(firstname);
            dt.Columns.Add(lastname);
            dt.Columns.Add(email);
            dt.Columns.Add(password);
            foreach (var user in userList)
            {
                DataRow row = dt.NewRow();
                row[0] = user.Id;
                row[1] = user.FirstName;
                row[2] = user.LastName;
                row[3] = user.Email;
                row[4] = user.Password;
                dt.Rows.Add(row);
            }

            myDT.ItemsSource = dt.DefaultView;

        }
        public CRUDWindow()
        {
            InitializeComponent();
            _context = new EFContext();
        }

        private void MyDT_Loaded(object sender, RoutedEventArgs e)
        {
            updateDT();
            //delete_btn.IsEnabled = false;
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            _context.Users.Add(new User
            {
                FirstName = firstname_txtbx.Text,
                LastName = lastname_txtbx.Text,
                Email = email_txtbx.Text,
                Password = password_txtbx.Text
            });
            _context.SaveChanges();
            //_users.Add(new User
            //{
            //    Id = int.Parse(user_id_txtbx.Text),
            //    FirstName = firstname_txtbx.Text,
            //    LastName = lastname_txtbx.Text,
            //    Email = email_txtbx.Text,
            //    Password = password_txtbx.Text
            //});
            updateDT();

            email_txtbx.Text = "";
            firstname_txtbx.Text = "";
            lastname_txtbx.Text = "";
            password_txtbx.Text = "";

            add_btn.IsEnabled = false;
            update_btn.IsEnabled = true;
        }

        private void MyDT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                user_id_txtbx.Text = dr["id"].ToString();
                firstname_txtbx.Text = dr["firstname"].ToString();
                lastname_txtbx.Text = dr["lastname"].ToString();

                add_btn.IsEnabled = false;
                update_btn.IsEnabled = true;
                delete_btn.IsEnabled = true;
            }
        }

        private void Update_btn_Click(object sender, RoutedEventArgs e)
        {
            

            updateDT();

            for (int i = 0; i < _users.Count; i++)
            {
                if (_users[i].Id == int.Parse(user_id_txtbx.Text))
                {
                    _users[i].FirstName = firstname_txtbx.Text;
                    _users[i].LastName = lastname_txtbx.Text;
                    _users[i].Email = email_txtbx.Text;
                    _users[i].Password = password_txtbx.Text;

                    updateDT();
                    break;
                }
            }
        }

        private void Resete_btn_Click(object sender, RoutedEventArgs e)
        {
            user_id_txtbx.Text = "";
            email_txtbx.Text = "";
            firstname_txtbx.Text = "";
            lastname_txtbx.Text = "";
            password_txtbx.Text = "";
            //updateDT();

            add_btn.IsEnabled = false;
            update_btn.IsEnabled = true;
            delete_btn.IsEnabled = true;
        }

        private void Lastname_txtbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            add_btn.IsEnabled = true;
        }

        private void Firstname_txtbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            add_btn.IsEnabled = true;
        }

        private void Email_txtbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            add_btn.IsEnabled = true;
        }

        private void Password_txtbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            add_btn.IsEnabled = true;
        }
    }
}
