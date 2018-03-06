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
            cbbYear.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctYearServer();

            cbbBaoCao.ItemsSource = HandlingDataDBViewModel.Instance.getListBaoCaoTongHop();
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
                case 3:
                    PrintTKSoLuong_SanLuongDHN_Co_Hieu();
                    break;
                case 4:
                    PrintTKSoLuongCoDungGieng();
                    break;
            }
        }
        private void PrintTKSoLuongCoDungGieng()
        {
            SetPageSetting(true);
            DataTable dt = HandlingDataDBViewModel.Instance.GetTKSoLuongCoDungGieng(year, month, date);
            _reportViewer.LocalReport.ReportPath = "../Report/rptSanLuongNhanVien.rdlc";
            this._reportViewer.LocalReport.DataSources.Clear();
            this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dtsTableDocSo", dt));
            this._reportViewer.RefreshReport();
        }
        private void PrintTKSoLuong_SanLuongDHN_Co_Hieu()
        {
            SetPageSetting(true);
            DataTable dt = HandlingDataDBViewModel.Instance.GetTKSoLuong_SanLuongDHN_Co_Hieu(year, month, date);
            _reportViewer.LocalReport.ReportPath = "../Report/rptBaoCaoSoLuongSanLuongCoHieuDHN.rdlc";
            this._reportViewer.LocalReport.DataSources.Clear();
            this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dtsTableDocSoKH", dt));
            this._reportViewer.RefreshReport();
        }
        private void PrintTKSoLuong_SanLuongDHN_Phuong()
        {
            SetPageSetting(false);
            DataTable dt = HandlingDataDBViewModel.Instance.GetTKSoLuong_SanLuongDHN_Phuong(year, month, date);
            _reportViewer.LocalReport.ReportPath = "../Report/rptBaoCaoSoLuongVaSanLuongTungPhuongDC.rdlc";
            this._reportViewer.LocalReport.DataSources.Clear();
            this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dtsTableDocSoKH", dt));
            this._reportViewer.RefreshReport();
        }
        private void PrintTKSoLuong_SanLuongDHN_DMA()
        {
            SetPageSetting(false);
            DataTable dt = HandlingDataDBViewModel.Instance.GetTKSoLuong_SanLuongDHN_DMA(year, month, date);
            _reportViewer.LocalReport.ReportPath = "../Report/rptBaoCaoSoLuongVaSanLuongTungPhuongDMA.rdlc";
            this._reportViewer.LocalReport.DataSources.Clear();
            this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dtsTableDocSo", dt));
            this._reportViewer.RefreshReport();
        }

        private void PrintTKDHNDocSo()
        {
            SetPageSetting(false);
            DataTable dt = HandlingDataDBViewModel.Instance.GetThongKeDocSo(year, month, date);
            _reportViewer.LocalReport.ReportPath = "../Report/rptThongKeDHNDocSo.rdlc";
            this._reportViewer.LocalReport.DataSources.Clear();
            this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dtsTableDocSo", dt));
            this._reportViewer.RefreshReport();
        }

        private void SetPageSetting(Boolean isLandscape)
        {
            System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
            ps.Landscape = isLandscape;
            //ps.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1170);
            Margins margins = new Margins(70, 50, 50, 50);
            ps.Margins = margins;
            //ps.PaperSize.RawKind = (int)System.Drawing.Printing.PaperKind.A4;
            _reportViewer.SetPageSettings(ps);
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
