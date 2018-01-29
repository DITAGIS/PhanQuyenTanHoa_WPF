using Microsoft.Reporting.WinForms;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for UC_ThongKeDHNTheoDotSo.xaml
    /// </summary>
    public partial class UC_ThongKeDHNTheoDotSo : UserControl
    {
        public UC_ThongKeDHNTheoDotSo()
        {
            InitializeComponent();

            cbbGroup.ItemsSource = DataDBViewModel.Instance.getDistinctGroupServer(Int16.Parse(User.Instance.Year), User.Instance.Month, User.Instance.Date);
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = DataDBViewModel.Instance.GetGroup(cbbGroup.SelectedValue);
            _reportViewer.LocalReport.ReportPath = "../Debug/Report/rptInThongKeDHNTheoDotSo.rdlc";
            this._reportViewer.LocalReport.DataSources.Clear();
            this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dtsInThongKeDHNTheoDotSo", dt));
            this._reportViewer.RefreshReport();
        }
    }
}
