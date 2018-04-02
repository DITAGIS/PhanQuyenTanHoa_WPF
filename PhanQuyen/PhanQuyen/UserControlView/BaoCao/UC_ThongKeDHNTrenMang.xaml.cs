using Microsoft.Reporting.WinForms;
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

namespace PhanQuyen.UserControlView.BaoCao
{
    /// <summary>
    /// Interaction logic for UC_ThongKeDHNTrenMang.xaml
    /// </summary>
    public partial class UC_ThongKeDHNTrenMang : System.Windows.Controls.UserControl
    {
        public UC_ThongKeDHNTrenMang()
        {
            InitializeComponent();
            System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
            ps.Landscape = true;
            ps.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1170);
            Margins margins = new Margins(70, 50, 50, 50);
            ps.Margins = margins;
            //ps.PaperSize.RawKind = (int)System.Drawing.Printing.PaperKind.A4;
            _reportViewer.SetPageSettings(ps);

            cbbYear.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctYearServer();
            for (int i = 1; i <= 12; i++)
                cbbMonth.Items.Add(i.ToString("00"));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnViewReport_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = HandlingDataDBViewModel.Instance.GetDHNTrenMang(Int16.Parse(cbbYear.SelectedItem.ToString()), Int16.Parse(cbbMonth.SelectedItem.ToString()));
            _reportViewer.LocalReport.ReportPath = "../Report/rptInThongKeDHNTrenMang.rdlc";
            this._reportViewer.LocalReport.DataSources.Clear();
            this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dtsInThongKeDHNTrenMang", dt));
            this._reportViewer.RefreshReport();
        }
        #region year,month
    

        private void cbbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void cbbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (cbbYear.SelectedValue != null)
            //{
            //    cbbMonth.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctMonthServer(Int16.Parse(cbbYear.SelectedValue.ToString()));
            //}
        }
        #endregion
    }
}
