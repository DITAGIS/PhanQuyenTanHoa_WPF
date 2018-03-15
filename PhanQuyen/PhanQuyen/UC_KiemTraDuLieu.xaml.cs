using Model;
using PhanQuyen.WindowView;
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
            this.btnUpdate.IsEnabled = false;
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
                txtbStatus.Text = "";

                if (!HandlingDataDBViewModel.Instance.CheckExistBienDong(year, month, date))
                {
                    int num3 = (int)System.Windows.Forms.MessageBox.Show("Không có biến động Kỳ " + year + "/" + month + " * Đợt " + date, "Thông báo");
                }
                else
                {
                    int capNhatKH = HandlingDataDBViewModel.Instance.CapNhatKH(year, month, date);
                    txtbStatus.Text = "Cập nhật " + capNhatKH + " khách hàng";
                    Thread.Sleep(500);
                    if (HandlingDataDBViewModel.Instance.CheckExistBienDong_KiemTraGanMoi(year, month, date))
                    {
                        new KHGanMoi_HuyWindow(year, month, date, true).ShowDialog();
                    }
                    int ganMoi = HandlingDataDBViewModel.Instance.InsertKhachHang(year, month, date);
                    txtbStatus.Text = "Gắn mới " + ganMoi + " khách hàng";
                    Thread.Sleep(500);

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
                    while (!sr.EndOfStream)
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            string str6 = sr.ReadLine().Replace("'", "");
                            if (str6 != null && str6 != "")
                            {
                                string[] strArray2 = str6.Split(new string[1]
                  {
                "\",\""
                  }, StringSplitOptions.None);
                                string[] strArray3 = strArray2[24].Split('/');
                                int year = int.Parse(strArray3[2]);
                                int month = int.Parse(strArray3[1]);
                                int day = int.Parse(strArray3[0]);
                                var ngayGan = new DateTime(year, month, day);
                                string str7 = strArray2[6];
                                string str8 = strArray2[0].Trim('"');
                                if (str8 == "")
                                    str8 = "0";
                                danhBa = strArray2[8];
                                if (danhBa.Equals("13122060119"))
                                    danhBa = "13122060119";
                                string str9 = strArray2[7];
                                string str10 = strArray2[5];
                                string str11 = strArray2[9].Trim(',', '\'', '\r');
                                string str12 = strArray2[10];
                                string str13 = strArray2[11];
                                string str14 = strArray2[12];
                                string str15 = strArray2[13];
                                string str16 = strArray2[23];
                                string str17 = strArray2[15];
                                if (str17 == "")
                                    str17 = "11";
                                string str18 = strArray2[22];
                                string str19 = strArray2[21];
                                if (str19 == "")
                                    str19 = "0";
                                string str20 = strArray2[17];
                                if (str20 == "")
                                    str20 = "0";
                                string str21 = strArray2[19];
                                if (str21 == "")
                                    str21 = "0";
                                string str22 = strArray2[20];
                                if (str22 == "")
                                    str22 = "0";
                                string str23 = strArray2[18];
                                if (str23 == "")
                                    str23 = "0";
                                string str24 = strArray2[16];
                                if (str24 == "")
                                    str24 = "0";
                                string str25 = strArray2[25];
                                if (str25 == "")
                                    str25 = "4";
                                string str26 = strArray2[26];
                                if (str26 == "")
                                    str26 = "0";
                                string str27 = strArray2[27].Trim('"');
                                if (str27 == "")
                                    str27 = "0";
                                string str28 = str4 + str10 + str7;
                                string str29 = str3 + str5 + danhBa;
                                var bienDong = new ViewModel.BienDong()
                                {
                                    BienDongID = str29,
                                    Nam = Int16.Parse(str3),
                                    Ky = str5,
                                    Dot = str4,
                                    DanhBa = danhBa,
                                    HopDong = str9,
                                    May = str10,
                                    TenKH = str11,
                                    So = str12,
                                    Duong = str13,
                                    Phuong = str15,
                                    Quan = str14,
                                    GB = short.Parse(str17),
                                    DM = Int16.Parse(str24),
                                    SH = short.Parse(str20),
                                    SX = short.Parse(str21),
                                    DV = short.Parse(str22),
                                    HC = short.Parse(str23),
                                    Hieu = str18,
                                    Co = short.Parse(str19),
                                    SoThan = str16,
                                    Code = str25,
                                    ChiSo = Int32.Parse(str26),
                                    TieuThu = Int32.Parse(str27),
                                    NgayGan = ngayGan,
                                    STT = Int32.Parse(str8),
                                    MLT1 = str28,
                                    NgayCapNhat = DateTime.Now,
                                    NVCapNhat = MyUser.Instance.UserID
                                };
                                bienDongList.Add(bienDong);
                                num2 += 1;
                            }
                            if (num2 < num1)
                            {
                                if (num2 % 1000 == 0)
                                {
                                    this.txtbStatus.Text = "Load biến động: " + num2.ToString() + "/" + (object)num1;
                                }
                            }
                            else
                            {
                                this.txtbStatus.Text = "Load biến động: " + num1.ToString() + "/" + (object)num1;
                            }

                        }), System.Windows.Threading.DispatcherPriority.Loaded);
                    }
                    num2 = 0;
                    num1 = bienDongList.Count;
                    foreach (BienDong bienDong in bienDongList)
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                        {

                            num2 += HandlingDataDBViewModel.Instance.InsertBienDong(bienDong);
                            if (num2 < num1)
                            {
                                if (num2 % 100 == 0)
                                {
                                    this.txtbStatus.Text = "Thêm biến động: " + num2.ToString() + "/" + (object)num1;
                                }
                            }
                            else
                            {
                                this.txtbStatus.Text = "Thêm biến động: " + num1.ToString() + "/" + (object)num1;
                            }

                        }), System.Windows.Threading.DispatcherPriority.Loaded);
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
