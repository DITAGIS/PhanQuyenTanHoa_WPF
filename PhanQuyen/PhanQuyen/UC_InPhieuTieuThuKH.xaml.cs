using Microsoft.Reporting.WinForms;
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
    /// Interaction logic for UC_InPhieuTieuThuKH.xaml
    /// </summary>
    public partial class UC_InPhieuTieuThuKH : UserControl
    {
        private int year;
        public UC_InPhieuTieuThuKH()
        {
            InitializeComponent();

            cbbYear.ItemsSource = GetDataDBViewModel.Instance.getDistinctYearServer();
        }

        private void cbbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbYear.SelectedValue != null)
                year = Int16.Parse(cbbYear.SelectedValue.ToString());
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            String danhBa = txtbDanhBa.Text;
            String str = txtGhiChu.Text.Trim();
            DataTable dt = GetDataDBViewModel.Instance.GetInfoCheckCustomer1(str, danhBa);
            //dt.Merge(GetDataDBViewModel.Instance.GetInfoCheckCustomer2(dt.Rows.Count, str, danhBa));
            _reportViewer.LocalReport.ReportPath = "../Debug/Report/rptInPhieuKiemTra.rdlc";
            this._reportViewer.LocalReport.DataSources.Clear();
            this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dtsInPhieuKiemTraKH", dt));
            this._reportViewer.RefreshReport();


            //_reportViewer.LocalReport.DataSources.Add(new ReportDataSource())
        }
    }
}
