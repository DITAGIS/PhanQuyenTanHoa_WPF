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

namespace PhanQuyen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();
            //cbbYear.Items.Add("2017");
            //cbbMonth.Items.Add("10");
            //cbbMonth.Items.Add("11");
            //cbbMonth.Items.Add("12");
            //cbbDate.Items.Add("01");
            //cbbDate.Items.Add("02");
            //cbbDate.Items.Add("03");
            //cbbDate.Items.Add("04");
            //cbbDate.Items.Add("05");
            //cbbDate.Items.Add("06");
            //cbbDate.Items.Add("07");
            //cbbDate.Items.Add("08");

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            login();
        }
        private void login()
        {
            //if (txtbUsername.Text.Equals("tanhoa") && txtbPassword.Password.Equals("123"))
            //{
            //    //MessageBox.Show("Đăng nhập thành công");
            //    this.Hide();
            //    new MainWindow().Show();
            //}
        }
        //private void txtbPassword_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter)
        //    {
        //        login();
        //    }
        //}
    }
}
