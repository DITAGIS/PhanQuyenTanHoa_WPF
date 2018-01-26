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
    public partial class UC_ThongKeDHNSauDocSo : UserControl
    {
        private int year, group;
        private String month, date, machine;
        public UC_ThongKeDHNSauDocSo()
        {
            InitializeComponent();

            cbbYear.ItemsSource = DataDBViewModel.Instance.getDistinctYearServer();
            cbbYear.SelectedValue = User.Instance.Year;

        }

        private void cbbGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            group = Int16.Parse(cbbGroup.SelectedValue.ToString());
            cbbMachine.ItemsSource = DataDBViewModel.Instance.getDistinctMachineServer(year, month, date, group);
        }

        private void btnViewReport_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    string str = "Select ds.May,m.ToID,Count(ds.DanhBa) as DanhBa, Case when ds.CodeMoi is null or ds.CodeMoi = '' then N'Chưa đọc' else ds.CodeMoi end as CodeMoi, Sum(ds.TieuThuMoi) as TieuThuMoi,ds.Ky,ds.Dot from DocSo ds Inner Join MayDS m on ds.May = m.May where ds.Ky = '" + cbbMonth.Text + "' and ds.Dot = '" + cbbDate.Text + "' and ds.Nam = " + cbbYear.Text;

            //    if (this.cbbGroup.Text == "Tất cả" || this.cbbMachine.Text == "Chọn sổ đọc")
            //    {
            //        string sqlStatement = str + " Group by ds.May,m.ToID,ds.CodeMoi,ds.Ky,ds.Dot";
            //        GetDataDBViewModel.Instance.ThongKeDHNSauDocSo(sqlStatement);
            //        DataTable dataTable = pcData.GetDataTable(sqlStatement);
            //        this._reportViewer.LocalReport.DataSources.Clear();
            //        this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dtsInThongKeSauDocSo", dataTable));
            //        this._reportViewer.RefreshReport();
            //    }
            //    else
            //    {
            //        string text1 = this.cbbGroup.Text;
            //        string text2 = this.cbbMachine.Text;
            //        string sqlStatement = str + " and ToID = '" + text1 + "' and ds.May ='" + text2 + "' Group by ds.May,m.ToID,ds.CodeMoi,ds.Ky,ds.Dot";
            //        this._reportViewer.LocalReport.DataSources.Clear();
            //        this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dtsInThongKeSauDocSo", dataTable));
            //        this._reportViewer.RefreshReport();
            //    }
            //}
            //catch (SqlException ex)
            //{
            //    int num = (int)MessageBox.Show("Lỗi hàm LoadReport thống kê sau đọc số: " + ex.Message);
            //}
        }
        #region year,month,date
        private void cbbDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            date = cbbDate.SelectedValue.ToString();
            cbbGroup.ItemsSource = DataDBViewModel.Instance.getDistinctGroupServer(year, month, date);
        }

        private void cbbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            month = cbbMonth.SelectedValue.ToString();
            cbbDate.ItemsSource = DataDBViewModel.Instance.getDistinctDateServer(year, month);
        }



        private void cbbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            year = Int16.Parse(cbbYear.SelectedValue.ToString());
            cbbMonth.ItemsSource = DataDBViewModel.Instance.getDistinctMonthServer(year);
        }
        #endregion
    }
}
