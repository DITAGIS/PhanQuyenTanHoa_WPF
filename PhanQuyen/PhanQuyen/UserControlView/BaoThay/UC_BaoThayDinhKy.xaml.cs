using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for UC_BaoThayDinhKy.xaml
    /// </summary>
    public partial class UC_BaoThayDinhKy : System.Windows.Controls.UserControl
    {
        public UC_BaoThayDinhKy()
        {
            InitializeComponent();
            cbbDKLoc.ItemsSource = new List<string> { "=", ">", ">=", "<", "<=" };
            cbbThang.ItemsSource = new List<string> { "", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };


            LoadCoDHN();
            AddItemCbbNam();
            LoadDanhMucBaoThay();
            this.btnBaoThay.IsEnabled = false;
            dtpNgayThay.SelectedDate = DateTime.Now;
        }
        private void AddItemCbbNam()
        {
            this.cbbNam.ItemsSource = HandlingDataDBViewModel.Instance.BaoThay_BaoThayDinhKy_LoadNam().DefaultView;
            this.cbbNam.DisplayMemberPath = "NamGan";
            this.cbbNam.SelectedValuePath = "NamGan";
        }

        private void LoadDanhMucBaoThay()
        {
            try
            {
                this.tabControl.TabIndex = 1;
                this.dtgDSBaoThay.ItemsSource = HandlingDataDBViewModel.Instance.BaoThay_BaoThayDinhKy_LoadDanhMucBaoThay().DefaultView;
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("Lỗi hàm LoadDanhMucBaoThay: " + ex.Message);
            }
        }
        private void LoadCoDHN()
        {
            this.cbbCo.ItemsSource = HandlingDataDBViewModel.Instance.BaoThay_NhapThongBao_LoadCoDHN().DefaultView;
            this.cbbCo.SelectedValuePath = "Code";
            this.cbbCo.DisplayMemberPath = "CodeDesc";
        }
        private void btnLoc_Click(object sender, RoutedEventArgs e)
        {
            this.tabControl.SelectedIndex = 0;
            if (!this.rbtLocTheoNgayGan.IsChecked.Value && !this.rbtLocTheoNgayKD.IsChecked.Value)
            {
                int num1 = (int)System.Windows.Forms.MessageBox.Show("Vui lòng chọn điều kiện lọc dữ liệu báo thay.", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);
            }
            else if (this.txtSoBK.Text == "")
            {
                int num2 = (int)System.Windows.Forms.MessageBox.Show("Vui lòng nhập số bảng kê báo thay.", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);
                this.txtSoBK.Focus();
            }
            else if (this.cbbDKLoc.Text == "" || this.cbbNam.Text == "")
            {
                int num3 = (int)System.Windows.Forms.MessageBox.Show("Vui lòng chọn đầy đủ thông tin lọc báo thay.", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);
            }
            else
            {
                try
                {

                    this.dtgDSLoc.ItemsSource = HandlingDataDBViewModel.Instance.BaoThay_BaoThayDinhKy_LoadDanhSachLoc(txtSoBK.Text.Trim(), cbbDKLoc.Text.ToString(), cbbNam.Text.ToString(), cbbThang.Text.ToString(), rbtLocTheoNgayGan.IsChecked.Value, cbbCo.Text.ToString()).DefaultView;
                    if (this.dtgDSLoc.Items.Count != 0)
                    {
                        this.lblSoDanhBa.Content = "Bao gồm: " + this.dtgDSLoc.Items.Count.ToString() + " danh bạ.";
                        this.btnBaoThay.IsEnabled = true;
                    }
                    else
                        this.lblSoDanhBa.Content = "Không tìm thấy danh bạ nào.";
                }
                catch (Exception ex)
                {
                    int num2 = (int)MessageBox.Show("Lỗi btnLocDL_Click: " + ex.Message);
                }
            }
        }

        private void btnBaoThay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //test
                if (this.txtCSGo.Text.Trim() == "" || this.txtCSGan.Text.Trim() == "")
                {
                    int num1 = (int)System.Windows.Forms.MessageBox.Show("Vui lòng nhập chỉ số để báo thay.", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);
                }
                else
                {
                    HandlingDataDBViewModel.Instance.BaoThay_BaoThayDinhKy_BaoThay(((DataView)dtgDSLoc.ItemsSource).ToTable(), txtCSGo.Text.Trim(), txtCSGan.Text.Trim(), dtpNgayThay.SelectedDate.Value.ToShortDateString());
                    this.LoadDanhMucBaoThay();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    int num2 = (int)System.Windows.Forms.MessageBox.Show("Báo thay thành công.", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);
                    this.tabControl.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                int num = (int)System.Windows.Forms.MessageBox.Show("Lỗi btnBaoThay_Click: " + ex.Message);
            }
        }
    }
}
