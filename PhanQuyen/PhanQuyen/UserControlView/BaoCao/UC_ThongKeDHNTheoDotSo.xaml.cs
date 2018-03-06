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
    public partial class UC_ThongKeDHNTheoDotSo : System.Windows.Controls.UserControl
    {
        public UC_ThongKeDHNTheoDotSo()
        {
            InitializeComponent();

            cbbGroup.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctGroupServer(Int16.Parse(MyUser.Instance.Year), MyUser.Instance.Month, MyUser.Instance.Date);

            System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
            ps.Landscape = true;
            //ps.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1170);
            Margins margins = new Margins(70, 50, 50, 50);
            ps.Margins = margins;

            PrinterSettings printerSetting = new PrinterSettings();

            IQueryable<PaperSize> paperSizes = printerSetting.PaperSizes.Cast<PaperSize>().AsQueryable();

            PaperSize a3 = paperSizes.Where(paperSize => paperSize.Kind == PaperKind.A3).FirstOrDefault();

            printerSetting.DefaultPageSettings.PaperSize = a3;
            //ps.PaperSize.RawKind = (int)System.Drawing.Printing.PaperKind.A4;
            _reportViewer.SetPageSettings(ps);
            _reportViewer.PrinterSettings = printerSetting;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = HandlingDataDBViewModel.Instance.GetListDate_Machine(cbbGroup.SelectedValue);
            _reportViewer.LocalReport.ReportPath = "../Report/rptInThongKeDHNTheoDotSo.rdlc";
            this._reportViewer.LocalReport.DataSources.Clear();
            this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dtsInThongKeDHNTheoSo", dt));
            this._reportViewer.RefreshReport();
        }
    }
}
