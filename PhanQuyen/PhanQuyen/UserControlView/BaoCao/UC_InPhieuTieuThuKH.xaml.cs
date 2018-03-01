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
    /// Interaction logic for UC_InPhieuTieuThuKH.xaml
    /// </summary>
    public partial class UC_InPhieuTieuThuKH : System.Windows.Controls.UserControl
    {
        private int year;
        public UC_InPhieuTieuThuKH()
        {
            InitializeComponent();

            cbbYear.ItemsSource = DataDBViewModel.Instance.getDistinctYearServer();

            System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
            ps.Landscape = true;
            //ps.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1170);
            Margins margins = new Margins(70, 50, 50, 50);
            ps.Margins = margins;
            //ps.PaperSize.RawKind = (int)System.Drawing.Printing.PaperKind.A4;
            _reportViewer.SetPageSettings(ps);


        }
        public UC_InPhieuTieuThuKH(string danhBa)
        {
            InitializeComponent();

            cbbYear.ItemsSource = DataDBViewModel.Instance.getDistinctYearServer();

            System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
            ps.Landscape = true;
            //ps.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1170);
            Margins margins = new Margins(70, 50, 50, 50);
            ps.Margins = margins;
            //ps.PaperSize.RawKind = (int)System.Drawing.Printing.PaperKind.A4;
            _reportViewer.SetPageSettings(ps);

            this.txtbDanhBa.Text = danhBa;

        }
        public void setDanhBa(string danhBa)
        {
            this.txtbDanhBa.Text = danhBa;

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
            String nam = "";
            if (cbbYear.SelectedValue != null)
                nam = cbbYear.SelectedValue.ToString();
            DataTable dt = DataDBViewModel.Instance.GetInfoCheckCustomer1(str, danhBa);
            dt.Merge(DataDBViewModel.Instance.GetInfoCheckCustomer2(dt.Rows.Count, str, danhBa));

            _reportViewer.LocalReport.ReportPath = "../Debug/Report/rptInPhieuKiemTra.rdlc";
            this._reportViewer.LocalReport.DataSources.Clear();
            this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dtsInPhieuKiemTraKH", dt));
            this._reportViewer.RefreshReport();


            //_reportViewer.LocalReport.DataSources.Add(new ReportDataSource())
        }
    }
}
