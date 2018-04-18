using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewModel;

namespace PhanQuyen.WindowView
{
    /// <summary>
    /// Interaction logic for Window_BaoThay_NhapThongBao_ThemCoDHN.xaml
    /// </summary>
    public partial class Window_BaoThay_NhapThongBao_ThemCoDHN : Window
    {
        public Window_BaoThay_NhapThongBao_ThemCoDHN()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (HandlingDataDBViewModel.Instance.BaoThay_NhapThongBao_ThemCoDHN(txtbMaCo.Text.Trim(), txtbCo.Text.Trim()))
                {
                    System.Windows.Forms.MessageBox.Show("Thêm CỠ thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.txtbMaCo.Clear();
                    this.txtbCo.Clear();
                }
            }
            catch (Exception ex)
            {
                int num = (int)System.Windows.Forms.MessageBox.Show("Lỗi btnLuu_Click: " + ex.Message);
            }
        }

    }
}
