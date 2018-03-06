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
     /// Interaction logic for UC_InDanhSachDongCua.xaml
     /// </summary>
     public partial class UC_InDanhSachDongCua : System.Windows.Controls.UserControl
    {
         private int year;
         private string month, date, machine;
         public UC_InDanhSachDongCua()
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
         }
 
         private void cbbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
         {
             if (cbbYear.SelectedValue != null)
             {
                 year = Int16.Parse(cbbYear.SelectedValue.ToString());
                 cbbMonth.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctMonthServer(year);
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
 
         private void cbbDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
         {
             if (cbbDate.SelectedValue != null)
             {
                 date = cbbDate.SelectedValue.ToString();
                 cbbMachine.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctMachineServer(year, month, date, 0);
             }
         }
 
         private void cbbMachine_SelectionChanged(object sender, SelectionChangedEventArgs e)
         {
             if (cbbMachine.SelectedValue != null)
                 machine = cbbMachine.SelectedValue.ToString();
         }
 
 
         private void btnPrint_Click(object sender, RoutedEventArgs e)
         {
             String danhBa = "";
             String str = "";
             DataTable dt = HandlingDataDBViewModel.Instance.GetListCloseDoor(year, month, date, machine);
             _reportViewer.LocalReport.ReportPath = "../Report/rptInDongCua.rdlc";
             this._reportViewer.LocalReport.DataSources.Clear();
             this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dtsInDongCua", dt));
             this._reportViewer.RefreshReport();
 
         }
     }
 }