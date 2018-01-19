using Model;
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
using ViewModel;

namespace PhanQuyen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        private int year;
        private String month, date;
        private User user;
        public LogInWindow()
        {
            InitializeComponent();
            cbbYear.ItemsSource = GetDataDBViewModel.Instance.getDistinctYearServer();


        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (cbbMonth.SelectedIndex < 0 || cbbDate.SelectedIndex < 0)
                MessageBox.Show("Chưa chọn kỳ hoặc đợt!!!");
            else
                login();
        }
        private void login()
        {
            user = UserDBViewModel.getInstance.getUser(txtbUsername.Text, txtbPassword.Password);
            if (user.UserName != null)
                HandleLoginSuccess();
            else
                HandleLoginFail();
        }
        private void HandleLoginSuccess()
        {
            user.Year = cbbYear.SelectedValue.ToString();
            user.Month = cbbMonth.SelectedValue.ToString();
            user.Date = cbbDate.SelectedValue.ToString();
            MainWindow mainWindow = new MainWindow(user);
            mainWindow.Show();
            this.Close();
        }
        private void HandleLoginFail()
        {
            MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu");
        }

        private void cbbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            year = Int16.Parse(cbbYear.SelectedValue.ToString());
            cbbMonth.ItemsSource = GetDataDBViewModel.Instance.getDistinctMonthServer(year);
        }

        private void cbbDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            date = cbbDate.SelectedValue.ToString();
        }

        private void cbbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            month = cbbMonth.SelectedValue.ToString();
            cbbDate.ItemsSource = GetDataDBViewModel.Instance.getDistinctDateServer(year, month);
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
