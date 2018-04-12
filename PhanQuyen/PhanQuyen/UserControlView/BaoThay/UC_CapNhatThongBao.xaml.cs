using Model;
using PhanQuyen.WindowView;
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
    /// Interaction logic for UC_NhapThongBao.xaml
    /// </summary>
    public partial class UC_CapNhatThongBao : System.Windows.Controls.UserControl
    {
        public UC_CapNhatThongBao()
        {
            InitializeComponent();
            dtpNgayKiem.SelectedDate = DateTime.Now;
            dtpNgayCapNhat.SelectedDate = DateTime.Now;
            LoadHieu();
            LoadCoDHN();
            LoadTieuDe();
            LoadDanhSach();
        }

        private void txtbDanhBa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TimKiem("k.DanhBa", txtbDanhBa.Text.ToString().Trim());
                txtbSoLenh.Focus();
            }
        }
        private void TimKiem(string loaiTK, string doituongTK)
        {
            string sqlStatment = "select k.DanhBa, k.TenKH, k.HopDong, k.MLT2, k.So, k.SoMoi, k.Duong, k.Hieu, k.Co, k.SoThan, k.ChiThan, k.ChiCo,m.ToID,k.ViTri from KhachHang k inner join MayDS m on k.May = m.May where " + loaiTK + " = '" + doituongTK + "'";
            DataTable table = HandlingDataDBViewModel.Instance.BaoThay_NhapThongBao_TimKiemKhachHang(loaiTK, doituongTK);

            if (table.Rows.Count > 0)
            {
                this.txtbDanhBa.Text = table.Rows[0]["DanhBa"].ToString();
                this.txtbHopDong.Text = table.Rows[0]["HopDong"].ToString();
                this.txtbMaLoTrinh.Text = table.Rows[0]["MLT2"].ToString();
                string str1 = table.Rows[0]["SoMoi"].ToString();
                this.txtbDiaChi.Text = table.Rows[0]["So"].ToString() + "(" + str1 + ")" + table.Rows[0]["Duong"].ToString();
                this.txtbTenKH.Text = table.Rows[0]["TenKH"].ToString();
                this.cbbHieu.Text = table.Rows[0]["Hieu"].ToString();
                string str2 = MyUser.Instance.Year + MyUser.Instance.Month + this.txtbDanhBa.Text.Trim();
                this.txtChiSo.Text = HandlingDataDBViewModel.Instance.BaoThay_NhapThongBao_LayChiSo(table.Rows[0]["DanhBa"].ToString(), str2).ToString();
            }
            else
            {
                int num = (int)MessageBox.Show("Không tồn tại danh bạ: " + this.txtbDanhBa.Text, "Thông báo");
                this.txtbDanhBa.Focus();
                this.txtbDanhBa.Select(this.txtbDanhBa.Text.Length, 0);
            }
        }
        private void Update()
        {
            if (this.txtbSoLenh.Text == "" || this.txtbDanhBa.Text == "")
            {
                int num = (int)System.Windows.Forms.MessageBox.Show("Vui lòng nhập số lệnh hoặc danh bạ để hoàn tất thông tin.", "Cảnh báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);
                this.txtbSoLenh.Focus();
            }
            else
            {
                try
                {
                    string id = "";
                    string danhBa = this.txtbDanhBa.Text.Trim();
                    string soLenh = this.txtbSoLenh.Text.Trim();
                    string tieuDe = this.cbbTieuDe.SelectedValue.ToString();
                    string hieu = this.cbbHieu.Text;
                    string co = this.cbbCo.Text;
                    int chiSo = int.Parse(this.txtChiSo.Text.Trim());
                    string noiDung = this.txtbNoiDung.Text.Trim();
                    string soThan = this.txtSoThan.Text.Trim();
                    string ngayKiem = dtpNgayKiem.SelectedDate.Value.ToString("yyyy-MM-dd");
                    string ngayCapNhat = dtpNgayCapNhat.SelectedDate.Value.ToString("yyyy-MM-dd");

                    if (HandlingDataDBViewModel.Instance.BaoThay_NhapThongBao_CapNhatThongBao(id, danhBa, soLenh, tieuDe, hieu, co, chiSo, noiDung, soThan, ngayKiem, ngayCapNhat))
                    {
                        System.Windows.Forms.MessageBox.Show("Lập phiếu thành công.", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);
                        this.LoadDanhSach();
                        this.Clear();
                    }
                }
                catch (Exception ex)
                {
                    int num = (int)System.Windows.Forms.MessageBox.Show("Lỗi btnLuu_Click: " + ex.Message);
                }
            }
        }

        private void Clear()
        {
            this.txtbDanhBa.Clear();
            this.txtbDanhBa.Focus();
            this.txtbMaLoTrinh.Clear();
            this.txtbDiaChi.Text = "";
            this.txtbHopDong.Text = "";
            this.txtbTenKH.Text = "";
            this.txtSoThan.Clear();
            this.txtChiSo.Clear();
            this.txtbNoiDung.Clear();
        }

        private void LoadDanhSach()
        {
            try
            {

                this.dtgridMain.ItemsSource = HandlingDataDBViewModel.Instance.BaoThay_NhapThongBao_LoadDanhSach(DateTime.Now.ToString("yyyy-MM-dd")).AsDataView();
                style_dgview();
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("Lỗi LoadDanhSach: " + ex.Message);
            }
        }
        public static void style_dgview()
        {
            //DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
            //gridViewCellStyle1.ForeColor = Color.Black;
            //gridViewCellStyle1.BackColor = Color.PowderBlue;
            //DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
            //gridViewCellStyle2.ForeColor = Color.Black;
            //gridViewCellStyle2.BackColor = Color.White;
            //for (int index = dgvV.RowCount - 1; index >= 0; --index)
            //{
            //    if (index % 2 == 0)
            //        dgvV.Rows[index].DefaultCellStyle = gridViewCellStyle1;
            //    else if (index % 2 != 0)
            //        dgvV.Rows[index].DefaultCellStyle = gridViewCellStyle2;
            //}
        }
        private void LoadCoDHN()
        {
            DataTable table = HandlingDataDBViewModel.Instance.BaoThay_NhapThongBao_LoadCoDHN();
            this.cbbCo.ItemsSource = table.DefaultView;
            this.cbbCo.SelectedValuePath = "Code";
            this.cbbCo.DisplayMemberPath = "CodeDesc";
        }
        private void btnReloadCo_Click(object sender, RoutedEventArgs e)
        {
            LoadCoDHN();
        }

        private void txtSoThan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtbNoiDung.Focus();
            }
        }

        private void btnAddCo_Click(object sender, RoutedEventArgs e)
        {
            Window_BaoThay_NhapThongBao_ThemCoDHN themCoDHN = new Window_BaoThay_NhapThongBao_ThemCoDHN();
            themCoDHN.ShowDialog();
        }

        private void cbbCo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                txtChiSo.Focus();
        }

        private void cbbHieu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                cbbCo.Focus();
        }

        private void cbbTieuDe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                cbbHieu.Focus();
        }

        private void txtbNoiDung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Update();
        }

        private void txtChiSo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                txtSoThan.Focus();
        }

        private void cbbCo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtChiSo.Focus();
        }

        private void cbbHieu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbbCo.Focus();
        }

        private void cbbTieuDe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbbHieu.Focus();
        }

        private void dtpNgayKiem_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            cbbTieuDe.Focus();
        }

        private void btnReloadTB_Click(object sender, RoutedEventArgs e)
        {
            LoadTieuDe();
        }

        private void LoadTieuDe()
        {
            this.cbbTieuDe.ItemsSource = HandlingDataDBViewModel.Instance.BaoThay_NhapThongBao_LoadTieuDe().AsDataView();
            this.cbbTieuDe.DisplayMemberPath = "CodeDesc";
            this.cbbTieuDe.SelectedValuePath = "Code";
        }

        private void btnReloadHieu_Click(object sender, RoutedEventArgs e)
        {
            LoadHieu();
        }

        private void LoadHieu()
        {
            this.cbbHieu.ItemsSource = HandlingDataDBViewModel.Instance.BaoThay_NhapThongBao_LoadTieuDe().AsDataView();
            this.cbbHieu.DisplayMemberPath = "CodeDesc";
            this.cbbHieu.SelectedValuePath = "Code";
        }

        private void txtbSoLenh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                dtpNgayKiem.Focus();
        }

        private void txtbMaLoTrinh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                TimKiem("k.MLT2", txtbMaLoTrinh.Text.Trim());
        }

        private void dtgridMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgridMain.SelectedIndex == -1 || dtgridMain.SelectedIndex >= dtgridMain.Items.Count)
                return;
            int rowIndex = dtgridMain.SelectedIndex;
            this.txtbSoLenh.Text = (dtgridMain.Items[rowIndex] as DataRowView)[1].ToString();
            this.txtbDanhBa.Text = (dtgridMain.Items[rowIndex] as DataRowView)[2].ToString();
            txtbTenKH.Text = (dtgridMain.Items[rowIndex] as DataRowView)[3].ToString();
            this.txtbDiaChi.Text = (dtgridMain.Items[rowIndex] as DataRowView)[4].ToString();
            this.txtbNoiDung.Text = (dtgridMain.Items[rowIndex] as DataRowView)[5].ToString();
            this.txtChiSo.Text = (dtgridMain.Items[rowIndex] as DataRowView)[6].ToString();
            this.txtbMaLoTrinh.Text = (dtgridMain.Items[rowIndex] as DataRowView)[7].ToString();
            this.txtbHopDong.Text = (dtgridMain.Items[rowIndex] as DataRowView)[8].ToString();
            this.cbbHieu.Text = (dtgridMain.Items[rowIndex] as DataRowView)[9].ToString();
            this.txtSoThan.Text = (dtgridMain.Items[rowIndex] as DataRowView)[10].ToString();
            this.cbbCo.Text = (dtgridMain.Items[rowIndex] as DataRowView)[11].ToString();
            cbbTieuDe.Text = (dtgridMain.Items[rowIndex] as DataRowView)[12].ToString();
            this.dtpNgayKiem.SelectedDate = DateTime.ParseExact((dtgridMain.Items[rowIndex] as DataRowView)[13].ToString(), "dd/MM/yyyy", (IFormatProvider)null);
            this.dtpNgayCapNhat.SelectedDate = DateTime.ParseExact((dtgridMain.Items[rowIndex] as DataRowView)[14].ToString(), "dd/MM/yyyy", (IFormatProvider)null);
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }
    }
}
