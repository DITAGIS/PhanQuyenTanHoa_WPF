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
    /// Interaction logic for UC_ThongKeDHNSauDocSo.xaml
    /// </summary>
    public partial class UC_ThongKeDHNSauDocSo : System.Windows.Controls.UserControl
    {
        private int year, group;
        private String month, date, machine;
        public UC_ThongKeDHNSauDocSo()
        {
            InitializeComponent();

            cbbYear.ItemsSource = DataDBViewModel.Instance.getDistinctYearServer();
            cbbYear.SelectedValue = MyUser.Instance.Year;

        }

        private void cbbGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            group = Int16.Parse(cbbGroup.SelectedValue.ToString());
            cbbMachine.ItemsSource = DataDBViewModel.Instance.getDistinctMachineServer(year, month, date, group);
        }

        private void btnViewReport_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = DataDBViewModel.Instance.GetThongKeSauDocSo(year, month, date, machine);
            _reportViewer.LocalReport.ReportPath = "../Debug/Report/rptInThongKeSauDocSo.rdlc";
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
                cbbGroup.ItemsSource = DataDBViewModel.Instance.getDistinctGroupServer(year, month, date);
            }
        }

        private void cbbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbMonth.SelectedValue != null)
            {
                month = cbbMonth.SelectedValue.ToString();
                cbbDate.ItemsSource = DataDBViewModel.Instance.getDistinctDateServer(year, month);
            }
        }



        private void cbbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbYear.SelectedValue != null)
            {
                year = Int16.Parse(cbbYear.SelectedValue.ToString());
                cbbMonth.ItemsSource = DataDBViewModel.Instance.getDistinctMonthServer(year);
            }
        }
        #endregion
    }
}
