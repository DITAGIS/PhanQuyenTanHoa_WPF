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
using System.Windows.Shapes;
using ViewModel;

namespace PhanQuyen.WindowView.BaoThay
{
    /// <summary>
    /// Interaction logic for Window_BaoThay_NhapThongBao_ThemTB.xaml
    /// </summary>
    public partial class Window_BaoThay_NhapThongBao_ThemTB : Window
    {
        public Window_BaoThay_NhapThongBao_ThemTB()
        {
            InitializeComponent();
            txtbMaThongBao.Text = getSTT().ToString();
        }
        private int getSTT()
        {
            return HandlingDataDBViewModel.Instance.BaoThay_NhapThongBao_LaySoTB() + 1;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HandlingDataDBViewModel.Instance.BaoThay_NhapThongBao_ThemLoaiTB(txtbMaThongBao.Text.Trim(), txtbThongBao.Text.Trim());
            int num = (int)System.Windows.Forms.MessageBox.Show("Thêm THÔNG BÁO thành công.", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);
            txtbMaThongBao.Text = getSTT().ToString();

        }
    }
}
