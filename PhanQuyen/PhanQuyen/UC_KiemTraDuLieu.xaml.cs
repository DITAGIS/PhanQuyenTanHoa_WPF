using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModel;

namespace PhanQuyen
{
    /// <summary>
    /// Interaction logic for UC_KiemTraDuLieu.xaml
    /// </summary>
    public partial class UC_KiemTraDuLieu : System.Windows.Controls.UserControl
    {
        private int year;
        private string month, date;
        public UC_KiemTraDuLieu()
        {
            InitializeComponent();
            cbbYear.ItemsSource = DataDBViewModel.Instance.getDistinctYearServer();
            cbbYear.SelectedValue = MyUser.Instance.Year;
        }

        private void cbbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbYear.SelectedValue != null)
            {
                year = Int16.Parse(cbbYear.SelectedValue.ToString());
                cbbMonth.ItemsSource = DataDBViewModel.Instance.getDistinctMonthServer(year);
            }
        }

        private void cbbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbMonth.SelectedValue != null)
            {
                month =cbbMonth.SelectedValue.ToString();
                cbbDate.ItemsSource = DataDBViewModel.Instance.getDistinctDateServer(year,month);
            }
        }

        private void cbbDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbDate.SelectedValue != null)
            {
                date = cbbDate.SelectedValue.ToString();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string empty = string.Empty;
            this.btnUpdate.IsEnabled = false;
            DateTime dateTime = new DateTime(year, int.Parse(month), 1).AddMonths(-1);
            string str1 = dateTime.Year.ToString();
            string str2 = dateTime.Month.ToString();
            int value = DataDBViewModel.Instance.CountHoaDon(year, month, date);
           
            if (value== 0)
            {
                int num1 = (int)System.Windows.Forms.MessageBox.Show("Không có thông tin hóa đơn Kỳ " + str2 + "/" + str1 + " * Đợt " + date, "Thông báo");
            }
            else
            {
                if (ckbLoadBienDong.IsChecked.Value)
                {
                    int countBienDong = DataDBViewModel.Instance.CountBienDong(year, month, date);

                    if (countBienDong != 0)
                    {
                        if (System.Windows.Forms.MessageBox.Show("Đã xử lý biến động, bạn muốn load lại Biến động?", "Câu hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            this.LoadFileBienDong(true);
                    }
                    else
                        this.LoadFileBienDong(false);
                }
                txtbStatus.Text = "";

                int countBienDong1 = DataDBViewModel.Instance.CountBienDong(year, month, date);
                if (countBienDong1 == 0)
                {
                    int num3 = (int)System.Windows.Forms.MessageBox.Show("Không có biến động Kỳ " +year + "/" +month + " * Đợt " + date, "Thông báo");
                }
                else
                {
                    int capNhatKH = DataDBViewModel.Instance.CapNhatKH(year, month, date);
                    txtbStatus.Text = "Cập nhật " + capNhatKH + " khách hàng";
                    Thread.Sleep(500);
                    //string sqlStatement2 = "select count(*) from BienDong where DanhBa not in (select DanhBa from KhachHang) and Nam = '" + GV.Nam + "' and Ky = '" + this.cbbKy.Text + "' and Dot = '" + this.cbbDot.Text + "'";
                    //if (pc.GetExecuteScalar(sqlStatement2) > 0)
                    //{
                    //    GV.Ky = this.cbbKy.Text;
                    //    GV.Dot = this.cbbDot.Text;
                    //    int num2 = (int)new frmKHGanMoi().ShowDialog();
                    //}
                    //string sqlstatement2 = "insert into KhachHang(DanhBa,May,HieuLuc,HopDong,MLT1,MLT2,Dot,TenKH,So,Duong,SoMoi,Phuong,Quan,GB,DM,SX,DV,HC,NgayGan,Hieu,Co,SoThan,GanMoi,TTBaoThay) select DanhBa,May,'1'    ,HopDong,MLT1,MLT1,Dot,TenKH,So,Duong,So   ,Phuong,Quan,GB,DM,SX,DV,HC,NgayGan,Hieu,Co,SoThan,'1','0' from BienDong B where " + UTIex.StrBNamKyDot(this.cbbNam.Text, this.cbbKy.Text, this.cbbDot.Text) + " and DanhBa not in (select DanhBa from KhachHang)";
                    //this.toolStripStatusLabel.Text = "Gắn mới " + (object)pc.GetExecuteNonQuerry(sqlstatement2) + " khách hàng";
                    //this.Refresh();
                    //Thread.Sleep(500);
                    //"select count(*) from KhachHang where DanhBa not in (select DanhBa from BienDong where Nam = '" + GV.Nam + "' and Ky = '" + this.cbbKy.Text + "' and Dot = '" + this.cbbDot.Text + "'";
                    //if (pc.GetExecuteScalar(sqlStatement2) > 0)
                    //{
                    //    int num4 = (int)new frmKHHuy().ShowDialog();
                    //}
                    //string sqlstatement3 = "update KhachHang set HieuLuc = '0',TTBaoThay = '0',GanMoi = '0' where KhachHang.Dot = '" + this.cbbDot.Text + "' and DanhBa not in (select DanhBa from BienDong B where " + UTIex.StrBNamKyDot(this.cbbNam.Text, this.cbbKy.Text, this.cbbDot.Text) + ")";
                    //this.toolStripStatusLabel.Text = "Hủy " + (object)pc.GetExecuteNonQuerry(sqlstatement3) + " khách hàng cũ";
                    //this.Refresh();
                    //GC.Collect();
                    //GC.WaitForPendingFinalizers();
                    //Thread.Sleep(500);
                    //int num5 = dateTime.Year;
                    //string str3 = num5.ToString();
                    //num5 = dateTime.Month;
                    //string str4 = num5.ToString("00");
                    //string sqlstatement4 = "update DocSo set TieuThuMoi = H.TieuThu, CSMoi = H.CSMoi, TuNgay = H.TuNgay, DenNgay = H.DenNgay from HoaDon H inner join DocSo D on D.DocSoID = H.HoaDonID where Left(H.Code,1) = '4' and D.CodeMoi in ('40','41','42','43','44','45') and H.Nam = '" + str3 + "' and H.Ky = '" + str4 + "' and H.Dot = '" + this.cbbDot.Text + "'";
                    //this.toolStripStatusLabel.Text = "Cập nhật " + (object)pc.GetExecuteNonQuerry(sqlstatement4) + " code 4";
                    //this.Refresh();
                    //GC.Collect();
                    //GC.WaitForPendingFinalizers();
                    //Thread.Sleep(500);
                    //string sqlstatement5 = "update DocSo set TieuThuMoi = H.TieuThu, CSMoi = H.CSMoi, TTDHNMoi = 'CSBT', CodeMoi = '40', TuNgay = H.TuNgay, DenNgay = H.DenNgay from HoaDon H inner join DocSo D on D.DocSoID = H.HoaDonID where Left(H.Code,1) = '4' and D.CodeMoi <> '4' and H.Nam = '" + str3 + "' and H.Ky = '" + str4 + "' and H.Dot = '" + this.cbbDot.Text + "'";
                    //this.toolStripStatusLabel.Text = "Cập nhật " + (object)pc.GetExecuteNonQuerry(sqlstatement5) + " code 4";
                    //this.Refresh();
                    //GC.Collect();
                    //GC.WaitForPendingFinalizers();
                    //Thread.Sleep(500);
                    //string sqlstatement6 = "update DocSo set CodeMoi = H.Code, TieuThuMoi = H.TieuThu, CSMoi = H.CSMoi, TTDHNMoi = T.TTDHN, TuNgay = H.TuNgay, DenNgay = H.DenNgay  from HoaDon H inner join DocSo D on D.DocSoID = H.HoaDonID inner join TTDHN T on H.Code = T.Code where Left(H.Code,1) in ('5','8','M') and H.Nam = '" + str3 + "' and H.Ky = '" + str4 + "' and H.Dot = '" + this.cbbDot.Text + "'";
                    //this.toolStripStatusLabel.Text = "Cập nhật " + (object)pc.GetExecuteNonQuerry(sqlstatement6) + " code 5,8,M";
                    //this.Refresh();
                    //GC.Collect();
                    //GC.WaitForPendingFinalizers();
                    //Thread.Sleep(500);
                    //string sqlstatement7 = "update DocSo set CSMoi = D.CSMoi, CodeMoi = H.Code, TTDHNMoi = T.TTDHN, TieuThuMoi = H.TieuThu, TuNgay = H.TuNgay, DenNgay = H.DenNgay from HoaDon H inner join DocSo D on D.DocSoID = H.HoaDonID inner join TTDHN T on H.Code = T.Code where Left(H.Code,1) in ('6','K','F','N') and H.Nam = '" + str3 + "' and H.Ky = '" + str4 + "' and H.Dot = '" + this.cbbDot.Text + "'";
                    //this.toolStripStatusLabel.Text = "Cập nhật " + (object)pc.GetExecuteNonQuerry(sqlstatement7) + " code 6,K,F,N";
                    //this.Refresh();
                    //Thread.Sleep(500);
                    //GC.Collect();
                    //GC.WaitForPendingFinalizers();
                    //try
                    //{
                    //    string sqlstatement8 = "update BillState set izCB ='1' where BillID ='" + this.cbbNam.Text + this.cbbKy.Text + this.cbbDot.Text + "'";
                    //    pc.GetExecuteNonQuerry(sqlstatement8);
                    //}
                    //catch (SqlException ex)
                    //{
                    //    int num2 = (int)MessageBox.Show("Lỗi cập nhật trạng thái bill: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //}
                    //this.toolStripStatusLabel.Text = "Chuẩn bị hoàn tất";
                    //int num6 = (int)MessageBox.Show("Chuẩn bị hoàn tất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //GC.Collect();
                    //GC.WaitForPendingFinalizers();
                    //Thread.Sleep(300);
                }
            }
        }
        private void LoadFileBienDong(bool xoaBienDongCu)
        {
            int num1 = 0;
            string str2 = "";
            SqlTransaction trans = (SqlTransaction)null;
            StreamReader sr = (StreamReader)null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "dat";
            openFileDialog.Filter = "File biến động|*.dat";
            string empty2 = string.Empty;
            if (DialogResult.OK != openFileDialog.ShowDialog())
                return;
            string fileName = openFileDialog.FileName;
            String danhBa="";
            int num2 = 0;
            try
            {
                sr = new StreamReader(fileName);
                while (!sr.EndOfStream)
                {
                    str2 = sr.ReadLine().Replace("'", "");
                    ++num1;
                }
                txtbStatus.Text = "0";
                string[] strArray1 = str2.Split(new string[1]
                {
          "\",\""
                }, StringSplitOptions.None);
                string str3 = strArray1[2];
                string str4 = strArray1[4];
                string str5 = int.Parse(strArray1[3]).ToString("00");
                if (!str4.Equals(date) || !str5.Equals(month) || !str3.Equals(year))
                {
                    int num3 = (int)System.Windows.MessageBox.Show("File chứa thông tin không đúng với biến động được chọn", "Thông báo");
                }
                else
                {
                    //sr.Close();
                    //sr.Dispose();
                    //sr = (StreamReader)null;
                    //sr = new StreamReader(fileName);
                    //trans = pc.PCConn.BeginTransaction();
                    //pc.PCCommand.Transaction = trans;
                    //if (xoaBienDongCu)
                    //{
                    //    string sqlstatement = "delete from BienDong where " + UTIex.StrNamKyDot(this.cbbNam.Text, this.cbbKy.Text, this.cbbDot.Text);
                    //    pc.GetExecuteNonQuerry(sqlstatement);
                    //}
                    //SqlParameter sqlParameter1 = new SqlParameter("ngayGan", SqlDbType.Date);
                    //pc.PCCommand.Parameters.Add(sqlParameter1);
                    //SqlParameter sqlParameter2 = new SqlParameter("@ngayCapNhat", SqlDbType.DateTime);
                    //sqlParameter2.Value = (object)DateTime.Now;
                    //pc.PCCommand.Parameters.Add(sqlParameter2);
                    //string userId = GV.UserID;
                    while (!sr.EndOfStream)
                    {
                        string str6 = sr.ReadLine().Replace("'", "");
                        if (str6 != null && str6 != "")
                        {
                //            string[] strArray2 = str6.Split(new string[1]
                //            {
                //"\",\""
                //            }, StringSplitOptions.None);
                //            string[] strArray3 = strArray2[24].Split('/');
                //            int year = int.Parse(strArray3[2]);
                //            int month = int.Parse(strArray3[1]);
                //            int day = int.Parse(strArray3[0]);
                //            sqlParameter1.Value = (object)new DateTime(year, month, day);
                //            string str7 = strArray2[6];
                //            string str8 = UTIin.TrimNK(strArray2[0]);
                //           danhBa = strArray2[8];
                //            string str9 = strArray2[7];
                //            string str10 = strArray2[5];
                //            string str11 = strArray2[9].Trim(',', '\'', '\r');
                //            string str12 = strArray2[10];
                //            string str13 = strArray2[11];
                //            string str14 = strArray2[12];
                //            string str15 = strArray2[13];
                //            string str16 = strArray2[23];
                //            string str17 = strArray2[15];
                //            if (str17 == "")
                //                str17 = "11";
                //            string str18 = strArray2[22];
                //            string str19 = strArray2[21];
                //            string str20 = strArray2[17];
                //            string str21 = strArray2[19];
                //            string str22 = strArray2[20];
                //            string str23 = strArray2[18];
                //            string str24 = strArray2[16];
                //            string str25 = strArray2[25];
                //            if (str25 == "")
                //                str25 = "4";
                //            string str26 = strArray2[26];
                //            string str27 = UTIin.TrimNK(strArray2[27]);
                //            string str28 = str4 + str10 + str7;
                //            string str29 = str3 + str5 + danhBa;
                //            string sqlstatement = string.Format("insert into BienDong values(" + str1 + ")", (object)str29, (object)str3, (object)str5, (object)str4, (object)this.danhba, (object)str9, (object)str10, (object)str11, (object)str12, (object)str13, (object)str15, (object)str14, (object)str17, (object)str24, (object)str20, (object)str21, (object)str22, (object)str23, (object)str18, (object)str19, (object)str16, (object)str25, (object)str26, (object)str27, (object)"@ngayGan", (object)str8, (object)str28, (object)"@ngayCapNhat", (object)userId);
                //            num2 += pc.GetExecuteNonQuerry(sqlstatement);
                        }
                        if (num2 < num1)
                        {
                            if (num2 % 200 == 0)
                            {
                                this.txtbStatus.Text = num2.ToString() + "/" + (object)num1;
                            }
                        }
                        else
                        {
                            this.txtbStatus.Text = num1.ToString() + "/" + (object)num1;
                        }
                    }
                    if (trans != null)
                        trans.Commit();
                }
            }
            catch (Exception ex)
            {
                int num3 = (int)System.Windows.MessageBox.Show(this.Name + ".LoadFileBienDong(): danh ba: " + danhBa + "\n" + ex.Message);
                if (trans != null)
                    trans.Rollback();
            }
            this.txtbStatus.Text = "";
            
        }
    }
}
