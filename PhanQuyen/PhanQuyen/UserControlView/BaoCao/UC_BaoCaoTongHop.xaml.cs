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
    /// Interaction logic for UC_BaoCaoTongHop.xaml
    /// </summary>
    public partial class UC_BaoCaoTongHop : System.Windows.Controls.UserControl
    {
        private int year, group;
        private String month, date, machine;
        public UC_BaoCaoTongHop()
        {
            InitializeComponent();
            cbbYear.ItemsSource = DataDBViewModel.Instance.getDistinctYearServer();

            cbbBaoCao.ItemsSource = DataDBViewModel.Instance.getListBaoCaoTongHop();
            cbbBaoCao.SelectedIndex = 0;
                
            //    new List<String>()
            //{
            //    "Thống Kê Đồng Hồ Nước Đọc Số",
            //    "Báo Cáo Số Lượng Và Sản Lượng DHN Theo DMA",
            //    "Báo Cáo Số Lượng Và Sản Lượng DHN Theo Phường",
            //    "Báo Cáo Số Lượng Và Sản Lượng Theo Cỡ Hiệu",
            //    "Báo cáo số lượng có dùng giếng"
            //};
        }

        private void cbbBaoCao_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnViewReport_Click(object sender, RoutedEventArgs e)
        {
            switch (cbbBaoCao.SelectedIndex)
            {
                case 0:
                    PrintTKDHNDocSo();
                    break;
                case 1:
                    PrintTKSoLuong_SanLuongDHN_DMA();
                    break;
                case 2:
                    PrintTKSoLuong_SanLuongDHN_Phuong();
                    break;
            }
        }

        private void PrintTKSoLuong_SanLuongDHN_Phuong()
        {
            DataTable dt = DataDBViewModel.Instance.GetTKSoLuong_SanLuongDHN_Phuong(year, month, date);
            _reportViewer.LocalReport.ReportPath = "../Report/rptBaoCaoSoLuongVaSanLuongTungPhuongDC.rdlc";
            this._reportViewer.LocalReport.DataSources.Clear();
            this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dtsTableDocSoKH", dt));
            this._reportViewer.RefreshReport();
        }
        private void PrintTKSoLuong_SanLuongDHN_DMA()
        {
            DataTable dt = DataDBViewModel.Instance.GetTKSoLuong_SanLuongDHN_DMA(year, month, date);
            _reportViewer.LocalReport.ReportPath = "../Report/rptBaoCaoSoLuongVaSanLuongTungPhuongDMA.rdlc";
            this._reportViewer.LocalReport.DataSources.Clear();
            this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dtsTableDocSo", dt));
            this._reportViewer.RefreshReport();
        }

        private void PrintTKDHNDocSo()
        {
            DataTable dt = DataDBViewModel.Instance.GetThongKeDocSo(year, month, date);
            _reportViewer.LocalReport.ReportPath = "../Report/rptThongKeDHNDocSo.rdlc";
            this._reportViewer.LocalReport.DataSources.Clear();
            this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dtsTableDocSo", dt));
            this._reportViewer.RefreshReport();
        }
        #region year,month,date
        private void cbbDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbDate.SelectedValue != null)
                date = cbbDate.SelectedValue.ToString();
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
