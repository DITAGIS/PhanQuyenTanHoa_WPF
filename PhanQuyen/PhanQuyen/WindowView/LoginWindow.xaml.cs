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
        private MyUser user;
        public LogInWindow()
        {
            InitializeComponent();
            cbbYear.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctYearServer();
            for (int i = 1; i <= 20; i++)
                cbbDate.Items.Add(i.ToString("00"));
            for (int i = 1; i <= 12; i++)
                cbbMonth.Items.Add(i.ToString("00"));

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
                HandleLoginFail(user);
        }
        private void HandleLoginSuccess()
        {


            user.Year = cbbYear.SelectedValue.ToString();
            user.Month = cbbMonth.SelectedValue.ToString();
            user.Date = cbbDate.SelectedValue.ToString();
            string sqlstatement = "Insert into BillState(BillID) values('" + user.Year + user.Month + user.Date + "')";
            try
            {
                ConnectionViewModel.getInstance.Connect();
                ConnectionViewModel.getInstance.GetExecuteNonQuerry(sqlstatement);
                ConnectionViewModel.getInstance.DisConnect();
            }
            catch
            {
                ConnectionViewModel.getInstance.DisConnect();
            }

            MainWindow mainWindow = new MainWindow(user);
            mainWindow.Show();
            this.Close();
        }
        private void HandleLoginFail(MyUser user)
        {
            MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu: " + user.ToString());
        }

        private void cbbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                year = Int16.Parse(cbbYear.SelectedValue.ToString());
                //cbbMonth.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctMonthServer(year);
            }
            catch
            {

            }
        }

        private void cbbDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                date = cbbDate.SelectedValue.ToString();
            }
            catch { }
        }

        private void cbbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                month = cbbMonth.SelectedValue.ToString();
                //cbbDate.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctDateServer(year, month);
            }
            catch { }
        }

    }
}
