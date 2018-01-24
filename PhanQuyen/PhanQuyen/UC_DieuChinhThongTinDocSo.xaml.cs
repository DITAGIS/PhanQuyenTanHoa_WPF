using System;
using System.Collections.Generic;
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
        public UC_DieuChinhThongTinDocSo()
        {
            InitializeComponent();
            rotate = 0;
            scaleX = scaleY = 1.1;
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
            month = cbbMonth.SelectedValue.ToString();
            cbbDate.ItemsSource = GetDataDBViewModel.Instance.getDistinctDateServer(year, month);
            cbbDate.SelectedValue = User.getInstance.Date;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            GetDataDBViewModel.Instance.update(dtgridMain.SelectedValue as DocSo);
        }

        private void dtgridMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            row = (DataGridRow)dtgridMain.ItemContainerGenerator
                                              .ContainerFromIndex(dtgridMain.SelectedIndex);
        }

        private void cbbDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            date = cbbDate.SelectedValue.ToString();
        }

        private void Refresh()
        {
            //if (dtgridMain != null && dtgridMain.Items.Count > 0 && dtgridMain.SelectedValue != null)
            //    dtgridMain.SelectedIndex = -1;

        }

        private void cbbGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            short x;
            if (cbbGroup.SelectedValue == null)
                group = -1;
            else if (Int16.TryParse(cbbGroup.SelectedValue.ToString(), out x))
                group = Int16.Parse(cbbGroup.SelectedValue.ToString());
            else group = x;
            cbbMachine.ItemsSource = GetDataDBViewModel.Instance.getDistinctMachineServer(year, month, date, group);
        }

        private void cbbCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //code mới
            gridCell = TryToFindGridCell(dtgridMain, row, 4);
            if (gridCell != null) gridCell.Content = cbbCode.SelectedValue.ToString();

            Refresh();
        }

        private void txtbCSM_TextChanged(object sender, TextChangedEventArgs e)
        {
            //chỉ số mới
            gridCell = TryToFindGridCell(dtgridMain, row, 6);
            int csmoi = Int16.Parse(txtbCSM.Text.ToString());
            if (gridCell != null) gridCell.Content = csmoi + "";

            //tiêu thụ mới
            gridCell = TryToFindGridCell(dtgridMain, row, 7);
            int tieuThuMoi = csmoi - Int16.Parse(txtbCSC.Text.ToString());
            txtbTieuThu.Text = tieuThuMoi + "";
            if (gridCell != null) gridCell.Content = tieuThuMoi + "";

            Refresh();
        }

        private void btnGetData_Click(object sender, RoutedEventArgs e)
        {
            dtgridMain.ItemsSource = GetDataDBViewModel.Instance.getAllDocSos(year, month, date, group, machine);
        }

        private void cbbMachine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            machine = cbbMachine.SelectedValue.ToString();
        }

        private void cbbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            year = Int16.Parse(cbbYear.SelectedValue.ToString());
            cbbMonth.ItemsSource = GetDataDBViewModel.Instance.getDistinctMonthServer(year);
        }



        public UC_DieuChinhThongTinDocSo(User user)
        {
            this.user = user;
            InitializeComponent();
            cbbYear.ItemsSource = GetDataDBViewModel.Instance.getDistinctYearServer();
            cbbYear.SelectedValue = User.getInstance.Year;

            if (User.getInstance.ToID == null)
            { }
            else if (User.getInstance.ToID.Equals(""))
                cbbGroup.ItemsSource = ToID.GetToID();
            else
                cbbGroup.Items.Add(User.getInstance.ToID);


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
