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

        private void cbbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            month = cbbMonth.SelectedValue.ToString();
            cbbDate.ItemsSource = GetDataDBViewModel.getInstance.getDistinctDateServer(year, month);
            cbbDate.SelectedValue = User.getInstance.Date;
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
            cbbMachine.ItemsSource = GetDataDBViewModel.getInstance.getDistinctMachineServer(year, month, date, group);
        }

        private void cbbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            year = Int16.Parse(cbbYear.SelectedValue.ToString());
            cbbMonth.ItemsSource = GetDataDBViewModel.getInstance.getDistinctMonthServer(year);
        }

        public UC_DieuChinhThongTinDocSo()
        {
            InitializeComponent();

        }

        public UC_DieuChinhThongTinDocSo(User user)
        {
            this.user = user;
            InitializeComponent();
            cbbYear.ItemsSource = GetDataDBViewModel.getInstance.getDistinctYearServer();
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
