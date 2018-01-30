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
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = DataDBViewModel.Instance.GetDHNTrenMang();
            _reportViewer.LocalReport.ReportPath = "../Debug/Report/rptInThongKeDHNTrenMang.rdlc";
            this._reportViewer.LocalReport.DataSources.Clear();
            this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dtsInThongKeDHNTrenMang", dt));
            this._reportViewer.RefreshReport();
        }
    }
}
