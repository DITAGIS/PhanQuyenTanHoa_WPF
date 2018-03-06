using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
            cbbYear.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctYearServer();
            cbbYear.SelectedValue = MyUser.Instance.Year;

        }
        private DateTime formatStringToDate(string s)
        {
            if (String.IsNullOrEmpty(s))
                return new DateTime();
            DateTime d = new DateTime();
            try
            {
                d = DateTime.ParseExact(s, "yyyyMMdd", CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(s);
            }

            return d;
        }
        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.DefaultExt = "dat";
                openFileDialog.Filter = "File hóa đơn|*.dat";
                if (DialogResult.OK != openFileDialog.ShowDialog())
                    return;
                List<ViewModel.HoaDon> result = new List<ViewModel.HoaDon>();
                int count = 0;
                using (var sr = new StreamReader(new FileStream(openFileDialog.FileName, FileMode.Open)))
                {
                    var strNamKy = "";
                    while (!sr.EndOfStream)
                    {
                        strNamKy = sr.ReadLine().Replace("'", "");
                        count++;
                    }


                    sr.Close();
                    sr.Dispose();
                    string[] strArray1 = strNamKy.Split(new string[1]
       {
          "\",\""
       }, StringSplitOptions.None);
                    var nam = "20" + strArray1[19];
                    var ky = strArray1[18];
                    if (nam != year.ToString() || ky != month)
                    {
                        int num3 = (int)System.Windows.MessageBox.Show("File không chứa đúng thông tin hóa đơn Kỳ :" + month + "/" + year, "Thông báo");
                    }
                    else
                    {
                        var sr1 = (StreamReader)null;

                        int current = 0;
                        txtbStatus.Text = current + "/" + count;
                        sr1 = new StreamReader(new FileStream(openFileDialog.FileName, FileMode.Open));
                        string s = String.Empty;
                        System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            while ((s = sr1.ReadLine()) != null)
                            {
                                if (s != String.Empty)
                                {
                                    string[] line = s.Split(new[] { "\",\"" }, StringSplitOptions.None);
                                    var Ky = line[18];
                                    var Nam = Int16.Parse(cbbYear.SelectedItem.ToString());
                                    var Dot = line[1] != "" ? int.Parse(line[1]) : 0;
                                    var DanhBo = line[2];
                                    var TenKH = line[7];
                                    var DC1 = line[8];
                                    var DC2 = line[9];
                                    var GB = line[12] != "" ? int.Parse(line[12]) : 0;
                                    var TGDM = line[17].Replace(" ", "") != "" ? int.Parse(line[17].Replace(" ", "")) : 0;
                                    var Code = line[20];
                                    var CodePhu = line[21];
                                    var CSCu = line[22] != "" ? int.Parse(line[22]) : 0;
                                    var CSMoi = line[23] != "" ? int.Parse(line[23]) : 0;
                                    var TieuThu = line[28] != "" ? int.Parse(line[28]) : 0;
                                    DateTime tuNgay = new DateTime(int.Parse(line[25].Substring(0, 4)), int.Parse(line[25].Substring(4, 2)), int.Parse(line[25].Substring(6)));
                                    DateTime denNgay = new DateTime(int.Parse(line[26].Substring(0, 4)), int.Parse(line[26].Substring(4, 2)), int.Parse(line[26].Substring(6)));
                                    var SO_HOADON = line[46];
                                    DateTime NgayCapNhat = DateTime.Now;
                                    string nvCapNhat = MyUser.Instance.UserID;
                                    var TongTien = line[40] != "" ? Int32.Parse(line[40]) : 0;


                                    var hoaDon = new ViewModel.HoaDon()
                                    {
                                        HoaDonID = Nam + Ky + DanhBo,
                                        Nam = Nam,
                                        Ky = Ky,
                                        Dot = Dot + "",
                                        DanhBa = DanhBo,
                                        TenKH = TenKH,
                                        So = DC1,
                                        Duong = DC2,
                                        GB = GB,
                                        DM = TGDM,
                                        Code = Code + CodePhu,
                                        CSCu = CSCu,
                                        CSMoi = CSMoi,
                                        TieuThu = TieuThu,
                                        TuNgay = tuNgay,
                                        DenNgay = denNgay,
                                        SoHoaDon = SO_HOADON,
                                        NgayCapNhat = NgayCapNhat,
                                        NVCapNhat = nvCapNhat,
                                        TienHD = TongTien

                                    };
                                    result.Add(hoaDon);
                                    HandlingDataDBViewModel.Instance.InsertHoaDon(hoaDon);
                                    txtbStatus.Text = ++current + "/" + count;
                                }
                            }
                        }), DispatcherPriority.Loaded);
                    }
                }
            }
            catch { }

        }

        private void cbbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbYear.SelectedValue == null) return;
            year = Int16.Parse(cbbYear.SelectedValue.ToString());
            cbbMonth.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctMonthServer(year);

            //LoadData();
        }

        private void cbbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbMonth.SelectedValue == null) return;
            month = cbbMonth.SelectedValue.ToString();
            //LoadData();
        }

        private void LoadData()
        {
            try
            {
                if (year < 0 || month == null)
                    return;

                System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {

                    txtbStatus.Text = "Đang tải dữ liệu...";
                    List<CapNhatHoaDon> listCNHD = HandlingDataDBViewModel.Instance.GetCapNhatHoaDon(month, year);

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

        private void btnGetData_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
