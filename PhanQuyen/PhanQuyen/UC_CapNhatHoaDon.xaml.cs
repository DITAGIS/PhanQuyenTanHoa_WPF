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
            cbbYear.ItemsSource = DataDBViewModel.Instance.getDistinctYearServer();
            cbbYear.SelectedValue = User.Instance.Year;

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
                List<DocSo> result = new List<DocSo>();
                int count = 0;
                using (var sr = new StreamReader(new FileStream(openFileDialog.FileName, FileMode.Open)))
                {
                    while (!sr.EndOfStream)
                    {
                        sr.ReadLine();
                        count++;
                    }

                    txtbStatus.Text = "0/" + count;
                    sr.Close();
                    sr.Dispose();
                    var sr1 = (StreamReader)null;

                    int current = 0;
                    sr1 = new StreamReader(new FileStream(openFileDialog.FileName, FileMode.Open));
                    string s = String.Empty;
                    System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        while ((s = sr1.ReadLine()) != null)
                        {
                            string[] line = s.Split(new[] { "\",\"" }, StringSplitOptions.None);
                            var Khu = line[0].Replace("\"", "") != "" ? int.Parse(line[0].Replace("\"", "")) : 0;
                            var Dot = line[1] != "" ? int.Parse(line[1]) : 0;
                            var Culy = line[4] != "" ? int.Parse(line[4]) : 0;
                            var GB = line[12] != "" ? int.Parse(line[12]) : 0;
                            var SH = line[13].Replace(" ", "") != "" ? int.Parse(line[13].Replace(" ", "")) : 0;
                            var HCSN = line[14].Replace(" ", "") != "" ? int.Parse(line[14].Replace(" ", "")) : 0;
                            var SX = line[15].Replace(" ", "") != "" ? int.Parse(line[15].Replace(" ", "")) : 0;
                            var DV = line[16].Replace(" ", "") != "" ? int.Parse(line[16].Replace(" ", "")) : 0;
                            var TGDM = line[17].Replace(" ", "") != "" ? int.Parse(line[17].Replace(" ", "")) : 0;
                            var Ky = line[18] != "" ? int.Parse(line[18]) : 0;
                            var Nam = line[19] != "" ? int.Parse(line[19]) : 0;
                            var CSCu = line[22] != "" ? int.Parse(line[22]) : 0;
                            var CSMoi = line[23] != "" ? int.Parse(line[23]) : 0;
                            var ChuKyDS = line[27] != "" ? int.Parse(line[27]) : 0;
                            var LNCC = line[28] != "" ? int.Parse(line[28]) : 0;
                            var LNCT = line[29] != "" ? int.Parse(line[29]) : 0;
                            var LN_BU_TOITHIEU = line[30] != "" ? int.Parse(line[30]) : 0;
                            var LN_SH = line[31] != "" ? int.Parse(line[31]) : 0;
                            var LN_HCSN = line[32] != "" ? int.Parse(line[32]) : 0;
                            var LN_SX = line[33] != "" ? int.Parse(line[33]) : 0;
                            var LN_DV = line[34] != "" ? int.Parse(line[34]) : 0;
                            var GiaBan = line[37] != "" ? long.Parse(line[37]) : 0;
                            var ThueGTGT = line[38] != "" ? int.Parse(line[38]) : 0;
                            var BVMT = line[39] != "" ? int.Parse(line[39]) : 0;
                            var TongTien = line[40] != "" ? long.Parse(line[40]) : 0;
                            var GIABAN_BU_TOITHIEU = line[41] != "" ? int.Parse(line[41]) : 0;
                            var THUE_BU_TOITHIEU = line[42] != "" ? int.Parse(line[42]) : 0;
                            var PHIBVMT_BU_TOITHIEU = line[43] != "" ? int.Parse(line[43]) : 0;
                            var TONGCONG_BU_TOITHIEU = line[44] != "" ? int.Parse(line[44]) : 0;
                            var STT = line[59] != "" ? int.Parse(line[59]) : 0;
                            var DanhBo = line[2];
                            var CD = line[3];
                            var MSTLK = line[5];
                            var GiaoUoc = line[6];
                            var HoTen = line[7];
                            var DC1 = line[8];
                            var DC2 = line[9];
                            var MSKH = line[10];
                            var MSCQ = line[11];
                            var Code = line[20];
                            var CodePhu = line[21];
                            var RT = line[24];
                            var VUNG_DMA = line[54];
                            var TIEUVUNG_DMAZ = line[55];
                            var FAX = line[56];
                            var Email = line[57];
                            var CUST_ID = line[58];
                            var DCTruSo = line[60].Replace("\"", "");
                            var Cuon_GCS = line[35];
                            var Cuon_STT = line[36];
                            var SO_PHATHANH = line[45];
                            var SO_HOADON = line[46];
                            var QUAN = line[48];
                            var PHUONG = line[49];
                            var SODHN = line[50];
                            var MSTHUE = line[51];
                            var TILE_TIEUTHU = line[52];
                            var Ngay_DS_KT = this.formatStringToDate(line[25]);
                            var Ngay_DS_KN = this.formatStringToDate(line[26]);
                            var NGAY_PHATHANH = this.formatStringToDate(line[47]);
                            var NGAY_GANDH = this.formatStringToDate(line[53]);
                            var hdt = new DocSo()
                            {
                                //Khu = Khu,
                                Dot = Dot + "",
                                DanhBa = DanhBo,
                                TenKH = HoTen,
                                SoNhaCu = DC1,
                                Duong = DC2,
                                GB = GB + "",

                                DM = TGDM + "",
                                CodeCu = Code + CodePhu,
                                Ky = Ky + "",
                                Nam = Nam,
                                CSCu = CSCu,
                                CSMoi = CSMoi,
                                TieuThuCu = LNCC,


                            };
                            result.Add(hdt);
                            txtbStatus.Text = ++current + "/" + count;
                        }
                    }), DispatcherPriority.Loaded);
                }
            }
            catch { }

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
