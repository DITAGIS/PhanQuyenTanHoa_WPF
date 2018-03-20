using Model;
using PhanQuyen.WindowView;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Threading;
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
            cbbYear.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctYearServer();
            cbbYear.SelectedValue = MyUser.Instance.Year;
        }

        private void cbbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbYear.SelectedValue != null)
            {
                year = Int16.Parse(cbbYear.SelectedValue.ToString());
                cbbMonth.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctMonthServer(year);
            }
        }

        private void cbbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbMonth.SelectedValue != null)
            {
                month = cbbMonth.SelectedValue.ToString();
                cbbDate.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctDateServer(year, month);
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
            //this.btnUpdate.IsEnabled = false;
            DateTime dateTime = new DateTime(year, int.Parse(month), 1).AddMonths(-1);
            string str1 = dateTime.Year.ToString();
            string str2 = dateTime.Month.ToString();
            if (str2.Length == 1)
                str2 = str2.Insert(0, "0");

            if (!HandlingDataDBViewModel.Instance.CheckExistHoaDon(Int16.Parse(str1), str2, date))
            {
                int num1 = (int)System.Windows.Forms.MessageBox.Show("Không có thông tin hóa đơn Kỳ " + str2 + "/" + str1 + " * Đợt " + date, "Thông báo");
            }
            else
            {

                if (ckbLoadBienDong.IsChecked.Value)
                {

                    if (HandlingDataDBViewModel.Instance.CheckExistBienDong(year, month, date))
                    {
                        if (System.Windows.Forms.MessageBox.Show("Đã xử lý biến động, bạn muốn load lại Biến động?", "Câu hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            this.LoadFileBienDong(true);
                    }
                    else
                        this.LoadFileBienDong(false);
                }

                if (!HandlingDataDBViewModel.Instance.CheckExistBienDong(year, month, date))
                {
                    int num3 = (int)System.Windows.Forms.MessageBox.Show("Không có biến động Kỳ " + year + "/" + month + " * Đợt " + date, "Thông báo");
                }
                else
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        int capNhatKH = HandlingDataDBViewModel.Instance.CapNhatKH(year, month, date);
                        txtbStatus.Text = "Cập nhật " + capNhatKH + " khách hàng";
                        if (HandlingDataDBViewModel.Instance.CheckExistBienDong_KiemTraGanMoi(year, month, date))
                        {
                            new KHGanMoi_HuyWindow(year, month, date, true).ShowDialog();
                            int ganMoi = HandlingDataDBViewModel.Instance.InsertKhachHang(year, month, date);
                            txtbStatus.Text = "Gắn mới " + ganMoi + " khách hàng";
                        }
                        else
                            txtbStatus.Text = "Gắn mới 0 khách hàng";

                        //if (HandlingDataDBViewModel.Instance.CheckExistKHHuy(year, month, date))
                        if (HandlingDataDBViewModel.Instance.CheckExistBienDong_KiemTraHuy(year, month, date))
                        {
                            new KHGanMoi_HuyWindow(year, month, date, false).ShowDialog();
                        }
                        int huy = HandlingDataDBViewModel.Instance.HuyKhachHang(year, month, date);
                        this.txtbStatus.Text = "Hủy " + huy + " khách hàng cũ";
                        int num5 = dateTime.Year;
                        num5 = dateTime.Month;
                        string str4 = num5.ToString("00");


                        int capNhatCode = HandlingDataDBViewModel.Instance.Update_Docso_KiemTraDuLieu_Code4(dateTime.Year, str4, date);
                        this.txtbStatus.Text = "Cập nhật " + capNhatCode + " code 4";

                        capNhatCode = HandlingDataDBViewModel.Instance.Update_Docso_KiemTraDuLieu_Code4_Lan2(dateTime.Year, str4, date);
                        this.txtbStatus.Text = "Cập nhật " + capNhatCode + " code 4";

                        capNhatCode = HandlingDataDBViewModel.Instance.Update_Docso_KiemTraDuLieu_Code5_8_M(dateTime.Year, str4, date);
                        this.txtbStatus.Text = "Cập nhật " + capNhatCode + " code 5,8,M";

                        capNhatCode = HandlingDataDBViewModel.Instance.Update_Docso_KiemTraDuLieu_Code6_K_F_N(dateTime.Year, str4, date);
                        this.txtbStatus.Text = "Cập nhật " + capNhatCode + " code 6,K,F,N";
                        try
                        {
                            HandlingDataDBViewModel.Instance.ChuanBiDocSo(year, month, date);
                        }
                        catch (SqlException ex)
                        {
                            int num2 = (int)System.Windows.Forms.MessageBox.Show("Lỗi cập nhật trạng thái bill: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        this.txtbStatus.Text = "Chuẩn bị hoàn tất";
                    }), DispatcherPriority.Loaded);
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
            String danhBa = "";
            int num2 = 0;

            try
            {
                sr = new StreamReader(fileName);
                while (!sr.EndOfStream)
                {
                    str2 = sr.ReadLine().Replace("'", "");
                    ++num1;
                }

                string[] strArray1 = str2.Split(new string[1]
                {
          "\",\""
                }, StringSplitOptions.None);
                string str3 = strArray1[2];
                string str4 = strArray1[4];
                string str5 = int.Parse(strArray1[3]).ToString("00");
                if (!str4.Equals(date) || !str5.Equals(month) || !str3.Equals(year.ToString()))
                {
                    int num3 = (int)System.Windows.MessageBox.Show("File chứa thông tin không đúng với biến động được chọn", "Thông báo");
                }
                else
                {
                    List<BienDong> bienDongList = new List<BienDong>();
                    sr.Close();
                    sr.Dispose();
                    sr = (StreamReader)null;
                    sr = new StreamReader(fileName);
                    if (xoaBienDongCu)
                    {
                        HandlingDataDBViewModel.Instance.DeleteBienDong(Int16.Parse(cbbYear.SelectedIndex.ToString()), cbbMonth.SelectedIndex.ToString(), cbbDate.SelectedIndex.ToString());
                    }
                    DataTable tbl = new DataTable();
                    int maxColumn = 28;
                    for (int col = 0; col < maxColumn; col++)
                        tbl.Columns.Add(new DataColumn("Junk" + (col).ToString()));
                    string s = String.Empty;
                    while ((s = sr.ReadLine()) != null)
                    {
                        string str6 = s.Replace("'", "");
                        if (str6 != null && str6 != "")
                        {
                            string[] strArray2 = str6.Split(new string[1] { "\",\"" }, StringSplitOptions.None);
                            tbl.Rows.Add(s.Split(new[] { "\",\"" }, StringSplitOptions.None));
                        }
                    }

                    tbl.Columns.Add(new DataColumn("NgayCapNhat"));
                    tbl.Columns.Add(new DataColumn("BienDongID"));
                    tbl.Columns.Add(new DataColumn("NVCapNhat"));
                    tbl.Columns.Add(new DataColumn("MLT1"));

                    tbl.Columns["NgayCapNhat"].Expression = "'" + DateTime.Now + "'";
                    tbl.Columns["NVCapNhat"].Expression = "'" + MyUser.Instance.UserID + "'";

                    Dictionary<int, string> colIndexNameDict = new Dictionary<int, string>()
                    {

                        {8, "DanhBa" },
                        {7, "HopDong" },
                        {5 ,"May" },
                        {9, "TenKH" },
                        {10, "So" },
                        {11, "Duong" },
                        {13, "Phuong" },
                        {12, "Quan" },
                        {15, "GB" },
                        {16, "DM" },
                        {17,"SH" },
                        {18, "SX" },
                        {19 ,"DV" },
                        {20, "HC" },
                        {22, "Hieu" },
                        {21, "Co" },
                        {23, "SoThan" },
                        {25, "Code" },
                        {26, "ChiSo" },
                        {27, "TieuThu" },
                        {24, "NgayGan" },
                        {0, "STT" },
                        {6, "MLT" }
                    };
                    foreach (var item in colIndexNameDict.Keys)
                    {
                        tbl.Columns[item].ColumnName = colIndexNameDict[item];
                    }
                    tbl.Columns["BienDongID"].Expression = "'" + year + month + "'+DanhBa";
                    tbl.Columns["MLT1"].Expression = "'" + str4 + "'+ May + MLT";
                    for (int i = 0; i < maxColumn;)
                    {
                        if (tbl.Columns[i].ColumnName.StartsWith("Junk"))
                        {
                            tbl.Columns.RemoveAt(i);
                            maxColumn--;
                        }
                        else
                        {
                            i++;
                        }
                    }

                    //Create table biendong

                    DataTable table = new DataTable();
                    table.Columns.Add(new DataColumn("BienDongID"));
                    table.Columns.Add(new DataColumn("Nam", typeof(Int32)));
                    table.Columns.Add(new DataColumn("Ky"));
                    table.Columns.Add(new DataColumn("Dot"));
                    table.Columns.Add(new DataColumn("DanhBa"));
                    table.Columns.Add(new DataColumn("HopDong"));
                    table.Columns.Add(new DataColumn("May"));
                    table.Columns.Add(new DataColumn("TenKH"));
                    table.Columns.Add(new DataColumn("So"));
                    table.Columns.Add(new DataColumn("Duong"));
                    table.Columns.Add(new DataColumn("Phuong"));
                    table.Columns.Add(new DataColumn("Quan"));
                    table.Columns.Add(new DataColumn("GB", typeof(Int16)));
                    table.Columns.Add(new DataColumn("DM", typeof(Int32)));
                    table.Columns.Add(new DataColumn("SH", typeof(Int16)));
                    table.Columns.Add(new DataColumn("SX", typeof(Int16)));
                    table.Columns.Add(new DataColumn("DV", typeof(Int16)));
                    table.Columns.Add(new DataColumn("HC", typeof(Int16)));
                    table.Columns.Add(new DataColumn("Hieu"));
                    table.Columns.Add(new DataColumn("Co", typeof(Int16)));
                    table.Columns.Add(new DataColumn("SoThan"));
                    table.Columns.Add(new DataColumn("Code"));
                    table.Columns.Add(new DataColumn("ChiSo", typeof(Int32)));
                    table.Columns.Add(new DataColumn("TieuThu", typeof(Int32)));
                    table.Columns.Add(new DataColumn("NgayGan", typeof(DateTime)));
                    table.Columns.Add(new DataColumn("STT", typeof(Int32)));
                    table.Columns.Add(new DataColumn("MLT1"));
                    table.Columns.Add(new DataColumn("NgayCapNhat", typeof(DateTime)));
                    table.Columns.Add(new DataColumn("NVCapNhat"));


                    //Chuyen du lieu table biendong
                    foreach (DataRow row in tbl.Rows)
                    {
                        DataRow newRow = table.NewRow();
                        newRow["BienDongID"] = row["BienDongID"];
                        newRow["Nam"] = year;
                        newRow["Ky"] = month;
                        newRow["Dot"] = date;

                        newRow["DanhBa"] = row["DanhBa"];
                        newRow["HopDong"] = row["HopDong"];
                        newRow["May"] = row["May"];
                        newRow["TenKH"] = row["TenKH"].ToString().Trim(',', '\'', '\r');
                        newRow["So"] = row["So"];
                        newRow["Duong"] = row["Duong"];
                        newRow["Phuong"] = row["Phuong"];
                        newRow["Quan"] = row["Quan"];

                        if (row["GB"].ToString() != "")
                            newRow["GB"] = short.Parse(row["GB"].ToString());
                        else
                            newRow["GB"] = 0;

                        if (row["DM"].ToString().Replace(" ", "") != "")
                            newRow["DM"] = int.Parse(row["DM"].ToString().Replace(" ", ""));
                        else
                            newRow["DM"] = 0;

                        if (row["SH"].ToString() != "")
                            newRow["SH"] = short.Parse(row["SH"].ToString());
                        else
                            newRow["SH"] = 0;

                        if (row["SX"].ToString() != "")
                            newRow["SX"] = short.Parse(row["SX"].ToString());
                        else
                            newRow["SX"] = 0;

                        if (row["DV"].ToString() != "")
                            newRow["DV"] = short.Parse(row["DV"].ToString());
                        else
                            newRow["DV"] = 0;

                        if (row["HC"].ToString() != "")
                            newRow["HC"] = short.Parse(row["HC"].ToString());
                        else
                            newRow["HC"] = 0;

                        newRow["Hieu"] = row["Hieu"];

                        if (row["Co"].ToString() != "")
                            newRow["Co"] = short.Parse(row["Co"].ToString());
                        else
                            newRow["Co"] = 0;

                        newRow["SoThan"] = row["SoThan"];
                        newRow["Code"] = row["Code"].ToString().Equals("") ? "4" : row["Code"];

                        if (row["ChiSo"].ToString() != "")
                            newRow["ChiSo"] = int.Parse(row["ChiSo"].ToString());
                        else
                            newRow["ChiSo"] = 0;

                        if (row["TieuThu"].ToString().Replace("\"", "") != "")
                            newRow["TieuThu"] = int.Parse(row["TieuThu"].ToString().Replace("\"", ""));
                        else
                            newRow["TieuThu"] = 0;

                        newRow["NgayGan"] = new DateTime(year, int.Parse(month), int.Parse(date));

                        if (row["STT"].ToString().Trim('"') != "")
                            newRow["STT"] = int.Parse(row["STT"].ToString().Trim('"'));
                        else
                            newRow["STT"] = 0;

                        newRow["MLT1"] = row["MLT1"];
                        newRow["NgayCapNhat"] = row["NgayCapNhat"];
                        newRow["NVCapNhat"] = row["NVCapNhat"];
                        table.Rows.Add(newRow);

                    }

                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        HandlingDataDBViewModel.Instance.InsertBienDong(table);
                        txtbStatus.Text = "Cập nhật hoàn tất";
                    }), DispatcherPriority.Loaded);
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
