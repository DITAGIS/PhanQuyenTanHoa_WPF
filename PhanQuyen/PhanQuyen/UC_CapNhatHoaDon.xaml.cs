using Model;
using System;
using System.Collections.Generic;
using System.Data;
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
            catch 
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
                    string[] strArray1 = strNamKy.Split(new string[1] { "\",\"" }, StringSplitOptions.None);
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
                        DataTable tbl = new DataTable();
                        for (int col = 0; col < 56; col++)
                            tbl.Columns.Add(new DataColumn("Junk" + (col).ToString()));
                        while ((s = sr1.ReadLine()) != null)
                        {
                            tbl.Rows.Add(s.Split(new[] { "\",\"" }, StringSplitOptions.None));
                        }
                        tbl.Columns.Add(new DataColumn("Nam"));
                        tbl.Columns.Add(new DataColumn("NgayCapNhat"));
                        tbl.Columns.Add(new DataColumn("NVCapNhat"));
                        tbl.Columns.Add(new DataColumn("HoaDonID"));
                        tbl.Columns.Add(new DataColumn("Code"));
                        tbl.Columns["Nam"].Expression = "'" + year + "'";
                        tbl.Columns["NgayCapNhat"].Expression = "'" + DateTime.Now + "'";
                        tbl.Columns["NVCapNhat"].Expression = "'" + MyUser.Instance.UserID + "'";


                        Dictionary<int, string> colIndexNameDict = new Dictionary<int, string>()
                    {
                        {1,"Dot" },
                        {2, "DanhBa" },
                        {7, "TenKH" },
                        {8 ,"So" },
                        {9, "Duong" },
                        {12, "GB" },
                        {17, "DM" },
                        {18, "Ky" },
                        {22, "CSCu" },
                        {23, "CSMoi" },
                        {25, "TuNgay" },
                        {26,"DenNgay" },
                        {28, "TieuThu" },
                        {40 ,"TienHD" },
                        {46, "SoHoaDon" }
                    };
                        foreach (DataRow row in tbl.Rows)
                        {
                            row["Code"] = row[20].ToString() + row[21].ToString();
                            row["HoaDonID"] = year + month + row[2].ToString();
                        }

                        int maxColumn = 56;

                        foreach (var item in colIndexNameDict.Keys)
                        {
                            tbl.Columns[item].ColumnName = colIndexNameDict[item];
                        }
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

                        DataTable table = new DataTable();
                        table.Columns.Add(new DataColumn("HoaDonID"));
                        table.Columns.Add(new DataColumn("Nam", typeof(Int32)));
                        table.Columns.Add(new DataColumn("Ky"));
                        table.Columns.Add(new DataColumn("Dot"));
                        table.Columns.Add(new DataColumn("DanhBa"));
                        table.Columns.Add(new DataColumn("TenKH"));
                        table.Columns.Add(new DataColumn("So"));
                        table.Columns.Add(new DataColumn("Duong"));
                        table.Columns.Add(new DataColumn("GB", typeof(Int32)));
                        table.Columns.Add(new DataColumn("DM", typeof(Int32)));
                        table.Columns.Add(new DataColumn("Code"));

                        table.Columns.Add(new DataColumn("CSCu", typeof(Int32)));
                        table.Columns.Add(new DataColumn("CSMoi", typeof(Int32)));
                        table.Columns.Add(new DataColumn("TieuThu", typeof(Int32)));
                        table.Columns.Add(new DataColumn("TuNgay", typeof(DateTime)));
                        table.Columns.Add(new DataColumn("DenNgay", typeof(DateTime)));


                        table.Columns.Add(new DataColumn("SoHoaDon"));
                        table.Columns.Add(new DataColumn("NgayCapNhat", typeof(DateTime)));
                        table.Columns.Add(new DataColumn("NVCapNhat"));

                        table.Columns.Add(new DataColumn("TienHD", typeof(Int32)));


                        foreach (DataRow row in tbl.Rows)
                        {
                            DataRow newRow = table.NewRow();
                            newRow["HoaDonID"] = row["HoaDonID"];
                            newRow["Nam"] = year;
                            newRow["Ky"] = month;

                            if (row["Dot"].ToString() != "")
                                newRow["Dot"] = int.Parse(row["Dot"].ToString());
                            else
                                newRow["Dot"] = 0;

                            newRow["DanhBa"] = row["DanhBa"];
                            newRow["TenKH"] = row["TenKH"];
                            newRow["So"] = row["So"];
                            newRow["Duong"] = row["Duong"];

                            if (row["GB"].ToString() != "")
                                newRow["GB"] = int.Parse(row["GB"].ToString());
                            else
                                newRow["GB"] = 0;

                            if (row["DM"].ToString().Replace(" ", "") != "")
                                newRow["DM"] = int.Parse(row["DM"].ToString().Replace(" ", ""));
                            else
                                newRow["DM"] = 0;

                            newRow["Code"] = row["Code"];

                            if (row["CSCu"].ToString() != "")
                                newRow["CSCu"] = int.Parse(row["CSCu"].ToString());
                            else
                                newRow["CSCu"] = 0;

                            if (row["CSMoi"].ToString() != "")
                                newRow["CSMoi"] = int.Parse(row["CSMoi"].ToString());
                            else
                                newRow["CSMoi"] = 0;

                            if (row["TieuThu"].ToString() != "")
                                newRow["TieuThu"] = int.Parse(row["TieuThu"].ToString());
                            else
                                newRow["TieuThu"] = 0;
                            try
                            {
                                newRow["TuNgay"] = new DateTime(int.Parse(row["TuNgay"].ToString().Substring(0, 4)), int.Parse(row["TuNgay"].ToString().Substring(4, 2)), int.Parse(row["TuNgay"].ToString().Substring(6)));
                            }
                            catch
                            {
                                newRow["TuNgay"] = table.Rows[table.Rows.Count - 1]["TuNgay"];
                            }

                            try
                            {
                                newRow["DenNgay"] = new DateTime(int.Parse(row["DenNgay"].ToString().Substring(0, 4)), int.Parse(row["DenNgay"].ToString().Substring(4, 2)), int.Parse(row["DenNgay"].ToString().Substring(6)));
                            }
                            catch
                            {
                                newRow["DenNgay"] = table.Rows[table.Rows.Count - 1]["DenNgay"];
                            }

                            newRow["SoHoaDon"] = row["SoHoaDon"];
                            newRow["NVCapNhat"] = row["NVCapNhat"];

                            newRow["NgayCapNhat"] = row["NgayCapNhat"];

                            if (row["TienHD"].ToString() != "")
                                newRow["TienHD"] = int.Parse(row["TienHD"].ToString());
                            else
                                newRow["TienHD"] = 0;


                            table.Rows.Add(newRow);

                        }
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            HandlingDataDBViewModel.Instance.InsertHoaDon(table);
                            txtbStatus.Text = "Cập nhật hoàn tất";
                        }), DispatcherPriority.Loaded);

                    }
                }
            }
            catch (Exception e1)
            {
                System.Windows.MessageBox.Show(e1.Message, "Lỗi");
            }

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

                System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                {

                    txtbStatus.Text = "Đang tải dữ liệu...";
                    DataTable table = HandlingDataDBViewModel.Instance.GetCapNhatHoaDon(month, year);

                    int danhBa = 0;
                    int tieuThu = 0;
                    foreach (DataRow row in table.Rows)
                    {
                        danhBa += int.Parse(row[1].ToString());
                        tieuThu += int.Parse(row[2].ToString());
                    }
                    DataRow newRow = table.NewRow();
                    newRow.ItemArray = new object[3]
                    {
                         "Tổng cộng",
                           danhBa,
                         tieuThu
                    };
                    table.Rows.InsertAt(newRow, table.Rows.Count);
                    dtgridMain.ItemsSource = table.DefaultView;
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
