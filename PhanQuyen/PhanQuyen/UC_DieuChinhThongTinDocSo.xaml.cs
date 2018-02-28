using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;
using ViewModel;
namespace PhanQuyen
{
    /// <summary>
    /// Interaction logic for UC_DieuChinhThongTinDocSo.xaml
    /// </summary>
    public partial class UC_DieuChinhThongTinDocSo : System.Windows.Controls.UserControl
    {
        private MyUser user;
        private int year, group;
        private String month, date, machine;
        private Point origin;  // Original Offset of image
        private Point start;   // Original Position of the mouse
        private int rotate;
        private double scaleX, scaleY;
        private double delta = 0.1;
        private DataGridRow row;
        private DataGridCell gridCell;
        private List<DocSo> docSoList = new List<DocSo>();
        private DocSo _selectedDocSo;
        private XemGhiChuWindow _xemGhiChuWindow;

        private const int COLUMN_MLT = 0;
        private const int COLUMN_DANHBA = 1;
        private const int COLUMN_TTDHNCu = 2;
        private const int COLUMN_TTDHNMoi = 3;
        private const int COLUMN_CodeCu = 4;
        private const int COLUMN_CodeMoi = 5;
        private const int COLUMN_CSCU = 6;
        private const int COLUMN_CSMOI = 7;
        private const int COLUMN_TIEUTHUMOI = 8;
        private const int COLUMN_TTTB = 9;
        private const int COLUMN_STACAPNHAT = 20;
        public UC_DieuChinhThongTinDocSo()
        {
            InitializeComponent();
            rotate = 0;
            scaleX = scaleY = 1.1;

            _xemGhiChuWindow = new XemGhiChuWindow();

            CheckIzDS();
        }

        private void CheckIzDS()
        {
            try
            {
                int executeScalar = DataDBViewModel.Instance.CheckIzDS();
                if (executeScalar == 1 && (MyUser.Instance.UserGroup == "DS" || MyUser.Instance.UserGroup == "Admin"))
                {
                    int num = (int)MessageBox.Show("Dữ liệu đã được chuyển lên thương vụ, bạn không thể điều chỉnh được thông tin.", "Thông báo");
                    this.cbbKHDS.IsEnabled = false;
                    this.txtbCSM.IsEnabled = false;
                    //this.chuyenMaHoa = true;
                }
                else
                {
                    this.cbbKHDS.IsEnabled = true;
                    this.txtbCSM.IsEnabled = true;
                    //this.chuyenMaHoa = false;
                }
                //if (executeScalar == 0 && GV.UserGroup == "TV")
                //{
                //    this.cbbNam.Enabled = false;
                //    this.cbbKy.Enabled = false;
                //    this.cbbDot.Enabled = false;
                //    this.cbbToDS.Enabled = false;
                //    this.cbbMay.Enabled = false;
                //    this.cbbCodeMoi.Enabled = false;
                //    this.btnCapNhat.Enabled = false;
                //}
                //if (executeScalar == 1 && GV.UserGroup == "TV")
                //    this.btnCapNhat.Enabled = true;
                //if (!(GV.UserGroup == "VP"))
                //    return;
                //this.btnCapNhat.Enabled = false;
            }
            catch (SqlException ex)
            {
                int num = (int)MessageBox.Show("Lỗi hàm CheckIzDS: " + ex.Message);
            }
        }

        public ScrollViewer GetScrollViewer
        {
            get { return scrollMain; }
        }
        private void btnRotate_Click(object sender, RoutedEventArgs e)
        {
            rotate += 90;
            RotateTransform rotateTransform = new RotateTransform(rotate);
            //image.LayoutTransform = rotateTransform;
        }
        private void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            ////scaleX += delta; scaleY += delta;
            //ScaleTransform scaleTransform = new ScaleTransform(scaleX, scaleY, 0.5, 0.5);
            //image.LayoutTransform = scaleTransform;
        }

        private void btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            //scaleX -= delta; scaleY -= delta;
            //ScaleTransform scaleTransform = new ScaleTransform(scaleX, scaleY);
            //image.LayoutTransform = scaleTransform;
        }
        private void cbbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (month == null)
                cbbMonth.SelectedValue = MyUser.Instance.Month;
            if (cbbMonth.SelectedValue != null)
                month = cbbMonth.SelectedValue.ToString();
            cbbDate.ItemsSource = DataDBViewModel.Instance.getDistinctDateServer(year, month);
            cbbDate.SelectedValue = MyUser.Instance.Date;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Update();
            }
            catch
            {

            }
        }
        private void Update()
        {
            if (this.txtbDanhBa.Text.Trim().Length < 11)
            {
                int num = (int)MessageBox.Show("Chọn danh bạ để cập nhật", "Thông báo");
                this.txtbDanhBa.Focus();
            }
            else if (this.txtbCSM.Text.Trim() == "")
            {
                int num = (int)MessageBox.Show("Nhập chỉ số mới, hoặc chọn bằng 0 nếu không có chỉ số", "Thông báo");
                this.txtbCSM.Focus();
            }
            else if (this.txtbTieuThu.Text.Trim() == "")
            {
                int num = (int)MessageBox.Show("Nhập tiêu thụ khách hàng", "Thông báo");
                this.txtbTieuThu.Focus();
            }
            else if (this.txtbCode.Text.Trim() == "")
            {
                int num = (int)MessageBox.Show("Vui lòng chọn KÝ HIỆU ĐỌC SỐ !", "Thông báo");
                this.cbbKHDS.Focus();
            }
            else
            {
                try
                {
                    bool isUpdate = DataDBViewModel.Instance.Update(txtbCode.Text.Trim(), txtbCSM.Text.Trim(), txtbTieuThu.Text.Trim(), txtbGCDS.Text.Trim(),
                         txtbGCMH.Text.Trim(), txtbGCKH.Text.Trim(), cbbKHDS.SelectedValue.ToString(), DateTime.Now, year, month, date, txtbDanhBa.Text.Trim());
                    if (isUpdate)
                        MessageBox.Show("Cập nhật thành công !", "Thông báo");
                    else
                        MessageBox.Show("Lỗi khi cập nhật !", "Thông báo");

                }
                catch (SqlException ex)
                {
                    int num = (int)MessageBox.Show("Lỗi khi lưu đọc số: " + ex.Message);
                }
            }
        }
        private void dtgridMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //foreach (DataGridRow row in dtgridMain.SelectedItems)
            //{
            //    System.Data.DataRow MyRow = (System.Data.DataRow)row.Item;
            //    string value = MyRow[1].ToString();
            //}
            _selectedDocSo = dtgridMain.SelectedValue as DocSo;
            cbbKHDS.SelectedValue = txtbCode.Text.ToString();
            foreach (var item in cbbKHDS.Items)
            {

            }
            if (_selectedDocSo != null)
            {
                btnViewNote.IsEnabled = true;
                btnPrint.IsEnabled = true;
            }
            else
            {
                btnViewNote.IsEnabled = false;
                btnPrint.IsEnabled = false;
            }
            row = getRow(dtgridMain.SelectedIndex);
        }
        private DataGridRow getRow(int index)
        {
            if (index < 0)
                return null;
            DataGridRow row = (DataGridRow)dtgridMain.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                // May be virtualized, bring into view and try again.
                dtgridMain.UpdateLayout();
                dtgridMain.ScrollIntoView(dtgridMain.Items[index]);
                row = (DataGridRow)dtgridMain.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;

        }
        private void cbbDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (date == null)
                cbbDate.SelectedValue = MyUser.Instance.Date;
            if (cbbDate.SelectedValue != null)
                date = cbbDate.SelectedValue.ToString();
        }

        private void Refresh()
        {
            //if (dtgridMain != null && dtgridMain.Items.Count > 0 && dtgridMain.SelectedValue != null)
            //    dtgridMain.SelectedIndex = -1;

        }

        private void cbbGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbGroup.SelectedValue != null)
            {
                short x;
                if (cbbGroup.SelectedValue == null)
                    group = -1;
                else if (Int16.TryParse(cbbGroup.SelectedValue.ToString(), out x))
                    group = Int16.Parse(cbbGroup.SelectedValue.ToString());
                else group = x;
                cbbMachine.ItemsSource = DataDBViewModel.Instance.getDistinctMachineServer(year, month, date, group);
            }
        }

        private void cbbCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //code mới

            gridCell = getCell(dtgridMain, row, 4);
            if (gridCell != null)
                gridCell.Content = cbbCode.SelectedValue.ToString();
            if (dtgridMain != null && cbbCode.SelectedValue != null && cbbCode.SelectedIndex > -1)
            {
                switch (cbbCode.SelectedIndex)
                {
                    case 0:
                        dtgridMain.ItemsSource = null;
                        dtgridMain.Items.Clear();
                        dtgridMain.ItemsSource = docSoList;
                        break;
                    case 1: //chưa ghi
                        dtgridMain.ItemsSource = null;
                        dtgridMain.Items.Clear();
                        foreach (DocSo docSo in docSoList)
                            if (docSo.GIOGHI == new DateTime(1900, 01, 01))
                                dtgridMain.Items.Add(docSo);
                        break;
                    default:
                        dtgridMain.ItemsSource = null;
                        dtgridMain.Items.Clear();
                        foreach (DocSo docSo in docSoList)
                            if (cbbCode.SelectedValue.ToString().StartsWith(docSo.CodeMoi))
                                dtgridMain.Items.Add(docSo);
                        break;

                }
            }
            Sum();

            CanhBaoBatThuong();
            Refresh();
        }
        private void Sum()
        {
            if (dtgridMain != null && dtgridMain.Items.Count >= 0)
            {
                int sanLuong = 0;
                foreach (DocSo docSo in dtgridMain.Items)
                    sanLuong += docSo.TieuThuMoi.GetValueOrDefault();

                txtbSanLuong.Text = String.Format("Sản lượng: {0} m3", sanLuong);
                txtbTongKH.Text = String.Format("Tổng KH: {0}", dtgridMain.Items.Count);
            }
        }
        private void txtbCSM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int csm = Int16.Parse(txtbCSM.Text.Trim().ToString());
                int tieuThuMoi = csm - Int16.Parse(txtbCSC.Text.ToString());
                if (dtgridMain.SelectedValue != null)
                {
                    (dtgridMain.SelectedValue as DocSo).CSMoi = csm;
                    (dtgridMain.SelectedValue as DocSo).TieuThuMoi = tieuThuMoi;
                }
            }

            if (e.Key >= Key.D0 && e.Key <= Key.D9 || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Back)
            //if (e.Key == Key.D0 || e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 || e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 ||
            //    e.Key == Key.NumPad0 || e.Key == Key.NumPad0 || e.Key == Key.NumPad0 || e.Key == Key.NumPad0 )
            {
                e.Handled = false;

                string tttb = "";
                if (dtgridMain.SelectedValue != null)
                    tttb = (dtgridMain.SelectedValue as DocSo).TBTT.Value + "";
                string str = this.txtbCode.Text.Trim();
                if (this.txtbCSM.Text == "")
                {
                    int num1 = (int)MessageBox.Show("Vui lòng nhập chỉ số mới.", "Thông báo");
                }
                else
                {
                    switch (str.Substring(0, 1))
                    {
                        case "4":
                        case "M":
                            this.txtbTieuThu.Text = (int.Parse(this.txtbCSM.Text.Trim()) - int.Parse(this.txtbCSC.Text.Trim())).ToString();
                            this.txtbTieuThu.Focus();
                            break;
                        case "5":
                            //int.Parse(this.lblTamTinh.Text);
                            this.txtbTieuThu.Text = (int.Parse(this.txtbCSM.Text.Trim()) - int.Parse(this.txtbCSC.Text.Trim())).ToString();
                            this.txtbTieuThu.Focus();
                            break;
                        case "X":
                            this.txtbCSM.Focus();
                            int num2 = int.Parse(this.txtbCSM.Text.Trim());
                            int num3 = int.Parse(this.txtbCSC.Text.Trim());
                            if (this.txtbCSC.Text.Trim().Length == 3)
                                num2 += 1000;
                            if (this.txtbCSC.Text.Trim().Length == 4)
                                num2 += 10000;
                            if (this.txtbCSC.Text.Trim().Length == 5)
                                num2 += 100000;
                            if (this.txtbCSC.Text.Trim().Length == 6)
                                num2 += 1000000;
                            if (this.txtbCSC.Text.Trim().Length == 7)
                                num2 += 10000000;
                            if (this.txtbCSC.Text.Trim().Length == 8)
                                num2 += 100000000;
                            this.txtbTieuThu.Text = (num2 - num3).ToString();
                            break;
                        case "8":
                            this.XuLyCode8(this.txtbDanhBa.Text.Trim(), this.txtbCSC.Text.Trim(), this.txtbCSM.Text.Trim(), tttb, this.txtbCode.Text.Trim());
                            break;
                        case "6":
                            if (str == "60" || str == "61" || str == "62")
                            {
                                this.txtbTieuThu.Text = tttb;
                                this.txtbCSM.IsEnabled = false;
                                break;
                            }
                            if (!(str == "63") && !(str == "64") && !(str == "66"))
                                break;
                            this.txtbTieuThu.Text = tttb;
                            this.txtbCSM.IsEnabled = false;
                            this.txtbCSM.Text = "0";
                            break;
                    }
                }
            }

            else
                e.Handled = true;
        }
        private void XuLyCode8(string danhba, string cscu, string csmoi, string tbttcu, string code)
        {
            try
            {
                string str = year + month + danhba;
                XuLyCode8 xuLyCode8 = DataDBViewModel.Instance.GetXuLyCode8(danhba);
                if (xuLyCode8 != null)
                {
                    int num1 = !(xuLyCode8.SoNgay.ToString() == "") &&
                        !(xuLyCode8.SoNgay.ToString() == "0") ? int.Parse(xuLyCode8.SoNgay.ToString()) : 1;
                    int num2 = int.Parse(xuLyCode8.CSGo.ToString());
                    int num3 = int.Parse(xuLyCode8.CSGan.ToString());
                    switch (code)
                    {
                        case "83":
                        case "82":
                            if (num1 > 32)
                            {
                                this.txtbTieuThu.Text = (int.Parse(csmoi) - num3 + (num2 - int.Parse(cscu))).ToString();
                                break;
                            }
                            this.txtbTieuThu.Text = (int.Parse(csmoi) - num3 + (num2 - int.Parse(cscu))).ToString();
                            break;
                        case "81":
                            if (num1 < 6)
                            {
                                int num4 = (int)MessageBox.Show("Chưa đủ ngày hoàn công để xử lý.", "Cảnh báo");
                                break;
                            }
                            if (num1 > 30)
                            {
                                int executeScalar = DataDBViewModel.Instance.GetTieuThuMoi(danhba, str);
                                this.txtbTieuThu.Text = Math.Round((double)((int.Parse(csmoi) - num3) / num1 * 60 - executeScalar), 0).ToString();
                            }
                            if (num1 > 6 && num1 <= 30)
                            {
                                this.txtbTieuThu.Text = Math.Round((double)((int.Parse(csmoi) - num3) / num1 * 30), 0).ToString();
                                break;
                            }
                            break;
                    }
                }
            }
            catch (SqlException ex)
            {
                int num = (int)MessageBox.Show("Lỗi XuLyCode8: " + ex.Message);
            }
        }
        private void txtbCSM_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

                //int csm = Int16.Parse(txtbCSM.Text.ToString());
                //int tieuThuMoi = csm - Int16.Parse(txtbCSC.Text.ToString());

                //txtbTieuThu.Text = tieuThuMoi + "";
                //todo 
                //if (txtbCSM.Text.Length > 0)
                //{
                //    //chỉ số mới
                //    gridCell = TryToFindGridCell(dtgridMain, row, COLUMN_CSMOI);
                //    int csmoi = Int16.Parse(txtbCSM.Text.ToString());
                //    if (gridCell != null) gridCell.Content = csmoi + "";

                //    //tiêu thụ mới
                //    gridCell = TryToFindGridCell(dtgridMain, row, COLUMN_TIEUTHUMOI);
                //    int tieuThuMoi = csmoi - Int16.Parse(txtbCSC.Text.ToString());
                //    txtbTieuThu.Text = tieuThuMoi + "";
                //    if (gridCell != null) gridCell.Content = tieuThuMoi + "";

                //    Refresh();
                //}
            }
            catch { }
        }

        private void btnGetData_Click(object sender, RoutedEventArgs e)
        {
            docSoList = DataDBViewModel.Instance.getAllDocSos(year, month, date, group, machine);
            dtgridMain.ItemsSource = null;
            dtgridMain.Items.Clear();
            dtgridMain.ItemsSource = docSoList;
            cbbCode.SelectedIndex = 0;
            Sum();
            CanhBaoBatThuong();

        }
        private String getValueCell(DataGridCell cell, DataGridRow row)
        {
            return (cell.Column.GetCellContent(row) as TextBlock).Text.ToString();
        }
        private void CanhBaoBatThuong()
        {
            if (dtgridMain == null)
                return;
            DataGridRow row;
            SolidColorBrush red = new SolidColorBrush(Colors.Red);
            SolidColorBrush blue = new SolidColorBrush(Colors.Blue);
            SolidColorBrush black = new SolidColorBrush(Colors.Black);
            Setter bold = new Setter(TextBlock.FontWeightProperty, FontWeights.Bold, null);
            Style newStyle;
            for (int index = 0; index < this.dtgridMain.Items.Count; ++index)
            {
                row = getRow(index);
                row.Foreground = black;
                //newStyle = new Style(row.GetType());
                //newStyle.Setters.Add(bold);
                //this.dtgridMain.Rows[index].Cells["Code Cũ"].Value.ToString();
                var cellCodeMoi = getCell(dtgridMain, row, COLUMN_CodeMoi);
                var cellStaCapNhat = getCell(dtgridMain, row, COLUMN_STACAPNHAT);
                string str1 = "";
                string str2 = "";
                double csc = 0.0;
                double csm = 0.0;
                double ttm = 0.0;
                double tttb = 0.0;
                try
                {

                    var cellCSC = getCell(dtgridMain, row, COLUMN_CSCU);
                    var cellTTM = getCell(dtgridMain, row, COLUMN_TIEUTHUMOI);
                    csc += Convert.ToDouble(getValueCell(cellCSC, row));
                    ttm += Convert.ToDouble(getValueCell(cellTTM, row));
                    str1 = getValueCell(cellCodeMoi, row);
                    str2 = getValueCell(cellStaCapNhat, row);
                }
                catch (Exception ex)
                {
                }
                try
                {
                    var cellCSM = getCell(dtgridMain, row, COLUMN_CSMOI);
                    csm += Convert.ToDouble(getValueCell(cellCSM, row));
                }
                catch
                {
                    row.Foreground = red;
                    //  row.Style = newStyle;
                }
                try
                {
                    var cellTTTB = getCell(dtgridMain, row, COLUMN_TTTB);
                    tttb += Convert.ToDouble(getValueCell(cellTTTB, row));
                }
                catch
                {
                }
                if (ttm >= 10.0 && ttm <= 49.0 && (ttm < tttb * 0.3 || ttm > tttb * 2.0))
                {
                    row.Foreground = red;
                    //  row.Style = newStyle;
                }
                if (ttm >= 50.0 && ttm <= 200.0 && (ttm < tttb * 0.7 || ttm > tttb * 1.5))
                {
                    row.Foreground = red;
                    //row.Style = newStyle;
                }
                if (ttm > 200.0 && ttm <= 2000.0 && (ttm < tttb * 0.8 || ttm > tttb * 1.5))
                {
                    row.Foreground = red;
                    //row.Style = newStyle;
                }
                if (ttm > 2000.0 && ttm <= 5000.0 && (ttm < tttb * 0.9 || ttm > tttb * 1.3))
                {
                    row.Foreground = red;
                    //row.Style = newStyle;
                }
                if (ttm > 5000.0 && (ttm < tttb * 0.9 || ttm > tttb * 1.2))
                {
                    row.Foreground = red;
                    //row.Style = newStyle;
                }
                if (ttm < 0.0)
                {
                    row.Foreground = red;
                    //row.Style = newStyle;
                }
                if (csm - csc != ttm || ttm == 0.0)
                {
                    row.Foreground = red;
                    //row.Style = newStyle;
                }
                if (str1.Contains("N") && ttm > 0.0 && str2 != "1")
                {
                    row.Foreground = red;
                    //row.Style = newStyle;
                }
                if (str1.Trim() != "" && str1.Trim().Substring(0, 1) == "6")
                {
                    row.Foreground = blue;
                    //row.Style = newStyle;
                }
            }
            SortDataGrid();
            SelectecTop();
        }
        private void SortDataGrid()
        {
            //ICollectionView dataView = CollectionViewSource.GetDefaultView(dtgridMain.ItemsSource);
            //dataView.SortDescriptions.Clear();
            //dataView.SortDescriptions.Add(new SortDescription("MLT", ListSortDirection.Ascending));
            //dataView.Refresh();

            System.Windows.Forms.ColumnClickEventArgs args = new System.Windows.Forms.ColumnClickEventArgs(0);

        }
        private void cbbMachine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbMachine.SelectedValue != null)
                machine = cbbMachine.SelectedValue.ToString();
        }

        private void btnViewNote_Click(object sender, RoutedEventArgs e)
        {
            if (_xemGhiChuWindow == null)
                _xemGhiChuWindow = new XemGhiChuWindow();
            _xemGhiChuWindow.GetNote(_selectedDocSo.DanhBa);
            _xemGhiChuWindow.ShowDialog();
        }
        private void SelectecTop()
        {
            try
            {
                if (dtgridMain.Items.Count > 0)
                    dtgridMain.SelectedIndex = 0;
            }
            catch { }
        }
        private void columnHeader_Click(object sender, RoutedEventArgs e)
        {
            CanhBaoBatThuong();

        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            InPhieuKiemTraWindow inPhieuKiemtRaWindow = new InPhieuKiemTraWindow(txtbDanhBa.Text.ToString());
            inPhieuKiemtRaWindow.ShowDialog();
        }

        private void cbbKHDS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgridMain.SelectedValue == null || cbbKHDS.SelectedValue == null)
                return;
            DocSo docSo = dtgridMain.SelectedValue as DocSo;
            this.txtbCode.Text = this.cbbKHDS.SelectedValue.ToString();
            docSo.CodeMoi = txtbCode.Text;
            string text = this.cbbKHDS.Text;
            string str1 = this.cbbKHDS.SelectedValue.ToString();
            string str2 = docSo.CodeCu.Length <= 1 ? docSo.CodeCu : docSo.CodeCu.Substring(0, 1);
            if ((text.Length >= 4 ? str1 = this.cbbKHDS.Text.Substring(0, 4) : str1 = this.cbbKHDS.Text) == "CSBT" && (str2 == "F" || str2 == "6" || (str2 == "K" || str2 == "N") || str2 == "Q"))
            {
                this.txtbCSM.Focus();
                this.txtbCode.Text = "5" + str2;
            }
            switch (str1)
            {
                case "40":
                case "41":
                case "42":
                case "43":
                case "44":
                case "45":
                case "81":
                case "82":
                case "83":
                    this.txtbCSM.IsEnabled = true;
                    this.txtbCSM.Focus();
                    break;
                case "F1":
                case "F2":
                case "F3":
                case "F4":
                case "60":
                case "61":
                case "62":
                case "80":
                    this.txtbTieuThu.Text = docSo.TBTT.Value + "";
                    this.txtbCSM.Focus();
                    break;
                case "63":
                case "64":
                case "66":
                    this.txtbTieuThu.Text = docSo.TBTT.Value + "";
                    this.txtbCSM.IsEnabled = false;
                    this.txtbCSM.Text = "0";
                    break;
                case "F5":
                case "N":
                case "K":
                    this.txtbTieuThu.Text = "0";
                    this.txtbCSM.Text = "0";
                    this.txtbTieuThu.Focus();
                    break;
            }
        }

        private void txtbDanhBa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string danhBa = txtbDanhBa.Text;
                if (txtbDanhBa.Text.Length == 0)
                {
                    dtgridMain.ItemsSource = null;
                    dtgridMain.Items.Clear();
                    dtgridMain.ItemsSource = docSoList;
                }
                else
                {
                    dtgridMain.ItemsSource = null;
                    dtgridMain.Items.Clear();
                    foreach (DocSo docSo in docSoList)
                        if (danhBa.Equals(docSo.DanhBa))
                            dtgridMain.Items.Add(docSo);
                }

                Sum();

                CanhBaoBatThuong();
                Refresh();
            }
        }

        private void cbbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbYear.SelectedValue != null)
                year = Int16.Parse(cbbYear.SelectedValue.ToString());
            cbbMonth.ItemsSource = DataDBViewModel.Instance.getDistinctMonthServer(year);
        }



        public UC_DieuChinhThongTinDocSo(MyUser user)
        {
            this.user = user;
            InitializeComponent();
            cbbYear.ItemsSource = DataDBViewModel.Instance.getDistinctYearServer();
            cbbYear.SelectedValue = MyUser.Instance.Year;

            cbbMonth.SelectedValue = MyUser.Instance.Month;
            if (MyUser.Instance.ToID == null)
                cbbGroup.ItemsSource = ToID.GetToID();
            else if (MyUser.Instance.ToID.Equals(""))
                cbbGroup.ItemsSource = ToID.GetToID();
            else
                cbbGroup.Items.Add(MyUser.Instance.ToID);

            cbbKHDS.ItemsSource = DataDBViewModel.Instance.getDistinctKHDS();
            cbbKHDS.DisplayMemberPath = "TTDHN1";
            cbbKHDS.SelectedValuePath = "CODE";
        }

        static DataGridCell getCell(DataGrid grid, DataGridRow row, int column)
        {
            if (row != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);

                if (presenter == null)
                {
                    grid.ScrollIntoView(row, grid.Columns[column]);
                    presenter = GetVisualChild<DataGridCellsPresenter>(row);
                }

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                return cell;
            }
            return null;
        }

        static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
    }
    public class ByteArrayImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is byte[])
            {
                byte[] bytes = value as byte[];

                MemoryStream stream = new MemoryStream(bytes);

                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.EndInit();

                return image;
            }

            return null;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
