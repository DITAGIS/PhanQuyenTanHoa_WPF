using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace PhanQuyen.UserControlView
{
    /// <summary>
    /// Interaction logic for UC_XuatDuLieuRaSmartPhone.xaml
    /// </summary>
    public partial class UC_XuatDuLieuRaSmartPhone : System.Windows.Controls.UserControl
    {
        private int year, group;
        private String month, date, machine;
        public UC_XuatDuLieuRaSmartPhone()
        {
            InitializeComponent();
            cbbYear.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctYearServer();
            dpkTuNgay.SelectedDate = DateTime.Now;
            dpkDenNgay.SelectedDate = DateTime.Now;
            if (MyUser.Instance.ToID == null || MyUser.Instance.ToID.Equals("") || MyUser.Instance.ToID.Trim().Equals(""))
                cbbGroup.ItemsSource = ToID.GetToID();
            //else if (MyUser.Instance.ToID.Equals(""))
            //    cbbGroup.ItemsSource = ToID.GetToID();
            else
                cbbGroup.Items.Add(MyUser.Instance.ToID);
        }


        private void cbbGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbGroup.SelectedValue != null)
            {
                short x;
                if (cbbGroup.SelectedValue == null)
                    group = -1;
                else if (Int16.TryParse(cbbGroup.SelectedValue.ToString(), out x))
                    group = Int16.Parse(cbbGroup.SelectedValue.ToString());
                else group = x;
            }
        }

        private void btnGetData_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUnSelect_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dtgridMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void columnHeader_Click(object sender, RoutedEventArgs e)
        {

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
