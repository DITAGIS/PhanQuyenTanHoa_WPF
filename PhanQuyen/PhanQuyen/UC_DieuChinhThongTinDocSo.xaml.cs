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
using ViewModel;
namespace PhanQuyen
{
    /// <summary>
    /// Interaction logic for UC_DieuChinhThongTinDocSo.xaml
    /// </summary>
    public partial class UC_DieuChinhThongTinDocSo : UserControl
    {


        public UC_DieuChinhThongTinDocSo()
        {
            InitializeComponent();
            //cbbYear.Items.Add("2017");
            //cbbMonth.Items.Add("12");
            //cbbDate.Items.Add("08");
            cbbGroup.Items.Add("' or '1'= '1");

            //cbbMachine.Items.Add("01");
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
