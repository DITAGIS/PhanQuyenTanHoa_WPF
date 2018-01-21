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
            DocSoLocal selectedDocSo = dtgridMain.SelectedValue as DocSoLocal;

        }

        private void cbbDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            date = cbbDate.SelectedValue.ToString();
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
