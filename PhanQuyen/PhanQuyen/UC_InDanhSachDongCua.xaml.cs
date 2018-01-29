﻿using Microsoft.Reporting.WinForms;
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
    /// Interaction logic for UC_InDanhSachDongCua.xaml
    /// </summary>
    public partial class UC_InDanhSachDongCua : UserControl
    {
        private int year;
        private string month, date, machine;
        public UC_InDanhSachDongCua()
        {
            InitializeComponent();
            cbbYear.ItemsSource = DataDBViewModel.Instance.getDistinctYearServer();
            cbbYear.SelectedValue = User.Instance.Year;
        }

        private void cbbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbYear.SelectedValue != null)
            {
                year = Int16.Parse(cbbYear.SelectedValue.ToString());
                cbbMonth.ItemsSource = DataDBViewModel.Instance.getDistinctMonthServer(year);
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

        private void cbbDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbDate.SelectedValue != null)
            {
                date = cbbDate.SelectedValue.ToString();
                cbbMachine.ItemsSource = DataDBViewModel.Instance.getDistinctMachineServer(year, month, date, 0);
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
            DataTable dt = DataDBViewModel.Instance.GetListCloseDoor(year, month, date, machine);
            _reportViewer.LocalReport.ReportPath = "../Debug/Report/rptInDongCua.rdlc";
            this._reportViewer.LocalReport.DataSources.Clear();
            this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dtsInDongCua", dt));
            this._reportViewer.RefreshReport();

        }
    }
}