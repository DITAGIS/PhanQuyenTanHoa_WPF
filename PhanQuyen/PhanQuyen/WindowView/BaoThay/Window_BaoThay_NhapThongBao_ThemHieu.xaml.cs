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
    /// Interaction logic for Window_BaoThay_NhapThongBao_ThemHieu.xaml
    /// </summary>
    public partial class Window_BaoThay_NhapThongBao_ThemHieu : Window
    {
        public Window_BaoThay_NhapThongBao_ThemHieu()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (HandlingDataDBViewModel.Instance.BaoThay_NhapThongBao_ThemHieuDHN(txtbMaHieu.Text.Trim(), txtbHieu.Text.Trim()))
                {
                    System.Windows.Forms.MessageBox.Show("Thêm HIỆU thành công.", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);
                    this.txtbHieu.Clear();
                    this.txtbMaHieu.Clear();
                }
            }
            catch (Exception ex)
            {
                int num = (int)System.Windows.Forms.MessageBox.Show("Lỗi btnLuu_Click: " + ex.Message);
            }
        }
    }
}
