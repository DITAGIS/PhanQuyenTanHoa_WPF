using Microsoft.Reporting.WinForms;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for UC_ThongKeDHNSauDocSo.xaml
    /// </summary>
    public partial class UC_ThongKeDHNSauDocSo : System.Windows.Controls.UserControl
    {
        private int year, group;
        private String month, date;
        public UC_ThongKeDHNSauDocSo()
        {
            InitializeComponent();
            System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
            ps.Landscape = false;
            //ps.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1170);
            Margins margins = new Margins(50, 0, 50, 50);
            ps.Margins = margins;
            //ps.PaperSize.RawKind = (int)System.Drawing.Printing.PaperKind.A4;
            _reportViewer.SetPageSettings(ps);
            cbbYear.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctYearServer();
            cbbYear.SelectedValue = MyUser.Instance.Year;

        }

        private void cbbGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            short x;
            if (cbbGroup.SelectedValue == null)
                group = -1;
            else if (Int16.TryParse(cbbGroup.SelectedValue.ToString(), out x))
                group = Int16.Parse(cbbGroup.SelectedValue.ToString());
            else group = x;
            cbbMachine.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctMachineServer(year, month, date, group);
        }

        private void btnViewReport_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = HandlingDataDBViewModel.Instance.GetThongKeSauDocSo(year, month, date, group, cbbMachine.SelectedItem.ToString());
            _reportViewer.LocalReport.ReportPath = "../Report/rptInThongKeSauDocSo.rdlc";
            this._reportViewer.LocalReport.DataSources.Clear();
            this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dtsInThongKeSauDocSo", dt));
            this._reportViewer.RefreshReport();
        }
        #region year,month,date
        private void cbbDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbDate.SelectedValue != null)
            {
                date = cbbDate.SelectedValue.ToString();
                cbbGroup.ItemsSource = ToID.GetToID();
            }
        }

        private void cbbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbMonth.SelectedValue != null)
            {
                month = cbbMonth.SelectedValue.ToString();
                cbbDate.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctDateServer(year, month);
            }
        }



        private void cbbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbYear.SelectedValue != null)
            {
                year = Int16.Parse(cbbYear.SelectedValue.ToString());
                cbbMonth.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctMonthServer(year);
            }
        }
        #endregion
    }
}
