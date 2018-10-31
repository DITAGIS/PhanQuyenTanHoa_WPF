using System;
using System.Collections.Generic;
using System.Linq;
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
using ViewModel;

namespace PhanQuyen.UserControlView.BaoThay
{
    /// <summary>
    /// Interaction logic for UC_NhapHoanCong.xaml
    /// </summary>
    public partial class UC_NhapHoanCong : System.Windows.Controls.UserControl
    {
        public UC_NhapHoanCong()
        {
            InitializeComponent();
        }

        private void LoadDanhMucBaoThay(DateTime ngayBao)
        {
            try
            {
                dtgDanhSachBao.ItemsSource = HandlingDataDBViewModel.Instance.BaoThay_HoanCong_LoadDanhSachBao(ngayBao).DefaultView;
                this.dtgDanhSachBao.Columns[0].Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("Lỗi LoadDanhMucBaoThay: " + ex.Message, "Thông báo");
            }
            finally
            {
            }
        }

        private void btnBaoThay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnHuyBaoThay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnInBangKe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLichSuThay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnHoanCong_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTroNgai_Click(object sender, RoutedEventArgs e)
        {

        }
        private void dtpNgayBao_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadDanhMucBaoThay(dtpNgayBao.SelectedDate.Value);
        }
    }
}
