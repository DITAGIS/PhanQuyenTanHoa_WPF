using System;
using System.Collections.Generic;
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
    /// Interaction logic for UC_NhanDuLieu.xaml
    /// </summary>
    public partial class UC_NhanDuLieu : UserControl
    {
        private GetDaTaWindow _getDataWindow;
        public UC_NhanDuLieu()
        {
            InitializeComponent();
            _getDataWindow = new GetDaTaWindow();
            _getDataWindow.Height = 200;
            _getDataWindow.Width = 500;
        }
        public void ShowGetDataWindow()
        {
            if (_getDataWindow != null)
                _getDataWindow.ShowDialog();
        }
        private void GetData_Click(object sender, RoutedEventArgs e)
        {
            GetDataDBViewModel.getInstance.getDocSosByCondition(Int16.Parse(cbbYear.SelectedValue.ToString()), cbbMonth.SelectedValue.ToString(),
                cbbDate.SelectedValue.ToString(), Int16.Parse(cbbGroup.SelectedValue.ToString()));
        }

        private void dtGridSoDaNhan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SeletectedSoDaNhan = dtGridSoDaNhan.SelectedValue as SoDaNhan;
            dtGridDocSos.ItemsSource = GetDataDBViewModel.getInstance.getDistinctHoaDon(SeletectedSoDaNhan);
        }
    }
}
