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
    /// Interaction logic for UC_LichSuDHN.xaml
    /// </summary>
    public partial class UC_LichSuDHN : System.Windows.Controls.UserControl
    {
        public UC_LichSuDHN()
        {
            InitializeComponent();
            txtDanhBa.Focus();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TimKiem(txtDanhBa.Text.Trim());
            }
        }
        private void TimKiem(string danhBa)
        {
            try
            {

                this.dtgridMain.ItemsSource = HandlingDataDBViewModel.Instance.BaoThay_LichSuDHN_TimKiemKhachHang(txtDanhBa.Text.Trim()).DefaultView;
                //UTIin.style_dgview(this.dgvView);
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("Lỗi LoadTTTimKiem: " + ex.Message);
            }
        }
    }
}
