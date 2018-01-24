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
    public partial class UC_DieuChinhThongTinDocSo : UserControl
    {
        private User user;
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
        public UC_DieuChinhThongTinDocSo()
        {
            InitializeComponent();
            rotate = 0;
            scaleX = scaleY = 1.1;
        }

        public ScrollViewer GetScrollViewer
        {
            get { return scrollMain; }
        }
        private void btnRotate_Click(object sender, RoutedEventArgs e)
        {
            rotate += 90;
            RotateTransform rotateTransform = new RotateTransform(rotate);
            image.LayoutTransform = rotateTransform;
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
            if (cbbMonth.SelectedValue != null)
                month = cbbMonth.SelectedValue.ToString();
            cbbDate.ItemsSource = GetDataDBViewModel.Instance.getDistinctDateServer(year, month);
            cbbDate.SelectedValue = User.Instance.Date;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            GetDataDBViewModel.Instance.update(dtgridMain.SelectedValue as DocSo);
        }

        private void dtgridMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //foreach (DataGridRow row in dtgridMain.SelectedItems)
            //{
            //    System.Data.DataRow MyRow = (System.Data.DataRow)row.Item;
            //    string value = MyRow[1].ToString();
            //}

            row = getRow(dtgridMain.SelectedIndex);
        }
        private DataGridRow getRow(int index)
        {
            return (DataGridRow)dtgridMain.ItemContainerGenerator
                                              .ContainerFromIndex(index);
        }
        private void cbbDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
                cbbMachine.ItemsSource = GetDataDBViewModel.Instance.getDistinctMachineServer(year, month, date, group);
            }
        }

        private void cbbCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //code mới

            gridCell = TryToFindGridCell(dtgridMain, row, 4);
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
            Refresh();
        }
        private void txtbCSM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int csm = Int16.Parse(txtbCSM.Text.ToString());
                int tieuThuMoi = csm - Int16.Parse(txtbCSC.Text.ToString());
                if (dtgridMain.SelectedValue != null)
                {
                    (dtgridMain.SelectedValue as DocSo).CSMoi = csm;
                    (dtgridMain.SelectedValue as DocSo).TieuThuMoi = tieuThuMoi;
                }
            }
        }

        private void txtbCSM_TextChanged(object sender, TextChangedEventArgs e)
        {
            int csm =Int16.Parse(txtbCSM.Text.ToString());
            int tieuThuMoi=csm - Int16.Parse(txtbCSC.Text.ToString());
          
            txtbTieuThu.Text = tieuThuMoi + "";//todo 
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

        private void btnGetData_Click(object sender, RoutedEventArgs e)
        {
            docSoList = GetDataDBViewModel.Instance.getAllDocSos(year, month, date, group, machine);
            dtgridMain.ItemsSource = null;
            dtgridMain.Items.Clear();
            dtgridMain.ItemsSource = docSoList;

            int sanLuong = 0;
            foreach (DocSo docSo in dtgridMain.Items)
                sanLuong += docSo.TieuThuMoi.GetValueOrDefault();

            txtbSanLuong.Text = String.Format("Sản lượng: {0} m3", sanLuong);
            txtbTongKH.Text = String.Format("Tổng KH: {0}", dtgridMain.Items.Count);
            //CanhBaoBatThuong();
        }

        private void CanhBaoBatThuong()
        {
            DataGridRow row;
            SolidColorBrush red = new SolidColorBrush(Colors.Red);
            SolidColorBrush blue = new SolidColorBrush(Colors.Blue);
            Setter bold = new Setter(TextBlock.FontWeightProperty, FontWeights.Bold, null);
            Style newStyle;
            for (int index = 1; index <= this.dtgridMain.Items.Count; ++index)
            {
                row = getRow(index);
                newStyle = new Style(row.GetType());
                newStyle.Setters.Add(bold);
                //this.dtgridMain.Rows[index].Cells["Code Cũ"].Value.ToString();
                string str1 = TryToFindGridCell(dtgridMain, row, COLUMN_CodeMoi).Content.ToString();
                //string str2 = this.dtgridMain.Rows[index].Cells["StaCapNhat"].Value.ToString();
                double num1 = 0.0;
                double num2 = 0.0;
                double num3 = 0.0;
                double num4 = 0.0;
                try
                {
                    num1 += Convert.ToDouble(TryToFindGridCell(dtgridMain, row, COLUMN_CSCU).Content.ToString());
                    num3 += Convert.ToDouble(TryToFindGridCell(dtgridMain, row, COLUMN_TIEUTHUMOI).Content.ToString());
                }
                catch (Exception ex)
                {
                }
                try
                {
                    num2 += Convert.ToDouble(TryToFindGridCell(dtgridMain, row, COLUMN_CSMOI).Content.ToString());
                }
                catch
                {
                    row.Foreground = red;
                    //  row.Style = newStyle;
                }
                try
                {
                    num4 += Convert.ToDouble(TryToFindGridCell(dtgridMain, getRow(index), COLUMN_TTTB).Content.ToString());
                }
                catch
                {
                }
                if (num3 >= 10.0 && num3 <= 49.0 && (num3 < num4 * 0.3 || num3 > num4 * 2.0))
                {
                    row.Foreground = red;
                    //  row.Style = newStyle;
                }
                if (num3 >= 50.0 && num3 <= 200.0 && (num3 < num4 * 0.7 || num3 > num4 * 1.5))
                {
                    row.Foreground = red;
                    row.Style = newStyle;
                }
                if (num3 > 200.0 && num3 <= 2000.0 && (num3 < num4 * 0.8 || num3 > num4 * 1.5))
                {
                    row.Foreground = red;
                    row.Style = newStyle;
                }
                if (num3 > 2000.0 && num3 <= 5000.0 && (num3 < num4 * 0.9 || num3 > num4 * 1.3))
                {
                    row.Foreground = red;
                    row.Style = newStyle;
                }
                if (num3 > 5000.0 && (num3 < num4 * 0.9 || num3 > num4 * 1.2))
                {
                    row.Foreground = red;
                    row.Style = newStyle;
                }
                if (num3 < 0.0)
                {
                    row.Foreground = red;
                    row.Style = newStyle;
                }
                if (num2 - num1 != num3 || num3 == 0.0)
                {
                    row.Foreground = red;
                    row.Style = newStyle;
                }
                //if (str1.Contains("N") && num3 > 0.0 && str2 != "1")
                //{
                //    row.Foreground = red;
                //      row.Style = newStyle;
                //}
                if (str1.Trim() != "" && str1.Trim().Substring(0, 1) == "6")
                {
                    row.Foreground = blue;
                    row.Style = newStyle;
                }
            }
        }

        private void cbbMachine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbMachine.SelectedValue != null)
                machine = cbbMachine.SelectedValue.ToString();
        }

        
        private void cbbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbYear.SelectedValue != null)
                year = Int16.Parse(cbbYear.SelectedValue.ToString());
            cbbMonth.ItemsSource = GetDataDBViewModel.Instance.getDistinctMonthServer(year);
        }



        public UC_DieuChinhThongTinDocSo(User user)
        {
            this.user = user;
            InitializeComponent();
            cbbYear.ItemsSource = GetDataDBViewModel.Instance.getDistinctYearServer();
            cbbYear.SelectedValue = User.Instance.Year;
            cbbMonth.SelectedValue = User.Instance.Month;
            if (User.Instance.ToID == null)
            { }
            else if (User.Instance.ToID.Equals(""))
                cbbGroup.ItemsSource = ToID.GetToID();
            else
                cbbGroup.Items.Add(User.Instance.ToID);

            cbbKHDS.ItemsSource = GetDataDBViewModel.Instance.getDistinctKHDS();
        }

        static DataGridCell TryToFindGridCell(DataGrid grid, DataGridRow row, int columnIndex)
        {
            DataGridCell result = null;
            if (row != null)
            {
                if (columnIndex > -1)
                {
                    DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);
                    result = presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex) as DataGridCell;
                }
            }
            return result;
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
