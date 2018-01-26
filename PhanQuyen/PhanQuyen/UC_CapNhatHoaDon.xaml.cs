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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ViewModel;

namespace PhanQuyen
{
    /// <summary>
    /// Interaction logic for UC_CapNhatHoaDon.xaml
    /// </summary>
    public partial class UC_CapNhatHoaDon : System.Windows.Controls.UserControl
    {
        private int year;
        private String month;
        public UC_CapNhatHoaDon()
        {
            InitializeComponent();
            cbbYear.ItemsSource = DataDBViewModel.Instance.getDistinctYearServer();
            cbbYear.SelectedValue = User.Instance.Year;

        }

        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "dat";
            openFileDialog.Filter = "File hóa đơn|*.dat";
            if (DialogResult.OK != openFileDialog.ShowDialog())
                return;
            //      string str1 = UTIin.TaoChuoiStringFormat(18, new ArrayList()
            //{
            //  (object) "5",
            //  (object) "14",
            //  (object) "15",
            //  (object) "17",
            //  (object) "18"
            //});
            //      PCData pc = (PCData)null;
            //      string fileName = openFileDialog.FileName;
            //      int num1 = 0;
            //      StreamReader sr = (StreamReader)null;
            //      string str2 = "";
            //      SqlTransaction trans = (SqlTransaction)null;
            //      int num2 = 0;
            //      try
            //      {
            //          pc = new PCData(GV.connString);
            //          sr = new StreamReader(fileName);
            //          while (!sr.EndOfStream)
            //          {
            //              str2 = sr.ReadLine().Replace("'", "");
            //              ++num1;
            //          }
            //          this.toolStripProgressBar.Minimum = 0;
            //          this.toolStripProgressBar.Maximum = num1;
            //          this.toolStripProgressBar.Value = 0;
            //          this.toolStripProgressBar.Visible = true;
            //          this.toolStripStatusLabel.Visible = true;
            //          this.toolStripStatusLabel.Text = "0/" + (object)num1;
            //          string[] strArray1 = str2.Split(new string[1]
            //          {
            //    "\",\""
            //          }, StringSplitOptions.None);
            //          this.nam = "20" + strArray1[19];
            //          this.ky = strArray1[18];
            //          if (this.nam != this.cbbNamHD.Text || this.ky != this.cbbKyHD.Text)
            //          {
            //              int num3 = (int)MessageBox.Show("File không chứa đúng thông tin hóa đơn Kỳ :" + this.cbbKyHD.Text + "/" + this.cbbNamHD.Text, "Thông báo");
            //              goto label_29;
            //          }
            //          else
            //          {
            //              this.tungay = new SqlParameter("tungay", SqlDbType.Date);
            //              pc.PCCommand.Parameters.Add(this.tungay);
            //              this.denngay = new SqlParameter("denngay", SqlDbType.Date);
            //              pc.PCCommand.Parameters.Add(this.denngay);
            //              this.tenkh = new SqlParameter("tenkh", SqlDbType.NVarChar);
            //              pc.PCCommand.Parameters.Add(this.tenkh);
            //              string str3;
            //              if (strArray1[20] == "M")
            //                  str3 = strArray1[26].Trim('"');
            //              else
            //                  str3 = strArray1[25].Trim('"');
            //              string str4 = strArray1[26].Trim('"');
            //              this.tungay.Value = (object)new DateTime(int.Parse(str3.Substring(0, 4)), int.Parse(str3.Substring(4, 2)), int.Parse(str3.Substring(6)));
            //              this.denngay.Value = (object)new DateTime(int.Parse(str4.Substring(0, 4)), int.Parse(str4.Substring(4, 2)), int.Parse(str4.Substring(6)));
            //              this.ngayCapNhat = new SqlParameter("@ngayCapNhat", SqlDbType.DateTime);
            //              this.ngayCapNhat.Value = (object)DateTime.Now;
            //              pc.PCCommand.Parameters.Add(this.ngayCapNhat);
            //              this.nvCapNhat = new SqlParameter("@nvCapNhat", SqlDbType.VarChar);
            //              this.nvCapNhat.Value = (object)GV.UserID;
            //              pc.PCCommand.Parameters.Add(this.nvCapNhat);
            //              sr.Close();
            //              sr.Dispose();
            //              sr = (StreamReader)null;
            //              sr = new StreamReader(fileName);
            //              trans = pc.PCConn.BeginTransaction();
            //              pc.PCCommand.Transaction = trans;
            //              while (!sr.EndOfStream)
            //              {
            //                  string str5 = sr.ReadLine().Replace("'", "");
            //                  if (str5 != null && str5 != "")
            //                  {
            //                      string[] strArray2 = str5.Split(new string[1]
            //                      {
            //          "\",\""
            //                      }, StringSplitOptions.None);
            //                      this.dot = strArray2[1];
            //                      this.danhba = strArray2[2];
            //                      this.tenkh.Value = (object)strArray2[7];
            //                      this.so = strArray2[8];
            //                      this.duong = strArray2[9];
            //                      this.gb = strArray2[12];
            //                      this.dm = strArray2[17];
            //                      this.code = strArray2[20] + strArray2[21];
            //                      this.cscu = strArray2[22];
            //                      this.csmoi = strArray2[23];
            //                      this.tieuthu = strArray2[28];
            //                      this.sohoadon = strArray2[46];
            //                      this.hoadonID = this.cbbNamHD.Text + this.ky + this.danhba;
            //                      try
            //                      {
            //                          string sqlstatement = string.Format("insert into HoaDon values(" + str1 + ")", (object)this.hoadonID, (object)this.nam, (object)this.ky, (object)this.dot, (object)this.danhba, (object)"@tenkh", (object)this.so, (object)this.duong, (object)this.gb, (object)this.dm, (object)this.code, (object)this.cscu, (object)this.csmoi, (object)this.tieuthu, (object)"@tungay", (object)"@denngay", (object)this.sohoadon, (object)"@ngayCapNhat", (object)"@nvCapNhat");
            //                          num2 += pc.GetExecuteNonQuerry(sqlstatement);
            //                      }
            //                      catch (SqlException ex)
            //                      {
            //                          string sqlstatement = "Update HoaDon set Nam ='" + this.nam + "',Ky = '" + this.ky + "',Dot = '" + this.dot + "',DanhBa = '" + this.danhba + "',TenKH = @tenkh,So = '" + this.so + "',Duong = '" + this.duong + "',GB = '" + this.gb + "',DM = '" + this.dm + "',Code = '" + this.code + "',CSCu ='" + this.cscu + "',CSMoi ='" + this.csmoi + "',TieuThu ='" + this.tieuthu + "',TuNgay = @tungay,DenNgay = @denngay,NgayCapNhat = @ngayCapNhat,NVCapNhat = @nvCapNhat where HoaDonID = '" + this.hoadonID + "'";
            //                          num2 += pc.GetExecuteNonQuerry(sqlstatement);
            //                      }
            //                  }
            //                  if (num2 < num1)
            //                  {
            //                      if (num2 % 200 == 0)
            //                      {
            //                          this.toolStripProgressBar.Value = num2;
            //                          this.toolStripStatusLabel.Text = num2.ToString() + "/" + (object)num1;
            //                          GC.Collect();
            //                          GC.WaitForPendingFinalizers();
            //                          this.Refresh();
            //                      }
            //                  }
            //                  else
            //                  {
            //                      this.toolStripProgressBar.Value = num1;
            //                      this.toolStripStatusLabel.Text = num1.ToString() + "/" + (object)num1;
            //                      GC.Collect();
            //                      GC.WaitForPendingFinalizers();
            //                      this.Refresh();
            //                  }
            //              }
            //              if (trans != null)
            //                  trans.Commit();
            //              this.LoadGridView((object)this.cbbNamHD.Text, (object)this.cbbKyHD.Text);
            //          }
            //      }
            //      catch (Exception ex)
            //      {
            //          int num3 = (int)MessageBox.Show(this.Name + ".btnImport_Click() dừng tại hàng thứ :" + (object)(num2 + 1) + " \n tại danh bạ: " + this.danhba + "\n" + ex.Message);
            //          if (trans != null)
            //              trans.Rollback();
            //      }
            //      label_29:
            //      UTIex.ReleaseSR(ref sr);
            //      UTIex.ReleaseTRANS(ref trans);
            //      UTIex.ReleasePCDATA(ref pc);
            //      GC.Collect();
            //      GC.WaitForPendingFinalizers();
            //      this.toolStripProgressBar.Visible = false;
            //      this.toolStripStatusLabel.Visible = false;
        }

        private void cbbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbYear.SelectedValue == null) return;
            year = Int16.Parse(cbbYear.SelectedValue.ToString());
            cbbMonth.ItemsSource = DataDBViewModel.Instance.getDistinctMonthServer(year);
            cbbMonth.SelectedIndex = 0;

            LoadData();
        }

        private void cbbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbMonth.SelectedValue == null) return;
            month = cbbMonth.SelectedValue.ToString();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                if (year < 0 || month == null)
                    return;
                txtbStatus.Text = "Đang tải dữ liệu...";
                System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {

                    List<CapNhatHoaDon> listCNHD = DataDBViewModel.Instance.GetCapNhatHoaDon(month, year);

                    int danhBa = 0;
                    int tieuThu = 0;
                    for (int index = 0; index < listCNHD.Count; ++index)
                    {
                        danhBa += int.Parse(listCNHD.ElementAt(index).SoDanhBa.ToString());
                        tieuThu += int.Parse(listCNHD.ElementAt(index).TieuThu.ToString());
                    }
                    listCNHD.Add(new CapNhatHoaDon()
                    {
                        Dot = "Tổng cộng",
                        SoDanhBa = danhBa,
                        TieuThu = tieuThu
                    });
                    dtgridMain.ItemsSource = listCNHD;
                    dtgridMain.Items.Refresh();

                    txtbStatus.Text = "";
                }), DispatcherPriority.Loaded);
            }
            catch (Exception ex)
            {
                int num = (int)System.Windows.MessageBox.Show(this.Name + ".LoadGriView:\n" + ex.Message, "Thông báo");
            }

        }
    }
}
