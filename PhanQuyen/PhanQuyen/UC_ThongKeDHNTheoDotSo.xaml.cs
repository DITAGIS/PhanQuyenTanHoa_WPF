using Microsoft.Reporting.WinForms;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
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

            System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
            ps.Landscape = true;
            //ps.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1170);
            Margins margins = new Margins(70, 50, 50, 50);
            ps.Margins = margins;
            //ps.PaperSize.RawKind = (int)System.Drawing.Printing.PaperKind.A4;
            _reportViewer.SetPageSettings(ps);
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = DataDBViewModel.Instance.GetGroup(cbbGroup.SelectedValue);
            _reportViewer.LocalReport.ReportPath = "../Debug/Report/rptInThongKeDHNTheoDotSo.rdlc";
            this._reportViewer.LocalReport.DataSources.Clear();
            this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dtsInThongKeDHNTheoSo", dt));
            this._reportViewer.RefreshReport();
        }
    }
}
