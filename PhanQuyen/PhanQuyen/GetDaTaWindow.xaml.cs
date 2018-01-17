using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using ViewModel;

namespace PhanQuyen
{
    /// <summary>
    /// Interaction logic for GetDaTaWindow.xaml
    /// </summary>
    public partial class GetDaTaWindow : Window
    {
        private int year, group;
        private String month, date, machine;
        public GetDaTaWindow()
        {
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            InitializeComponent();
            cbbYear.ItemsSource = GetDataDBViewModel.getInstance.getDistinctYearServer();
            cbbYear.SelectedIndex = cbbYear.Items.Count - 1;

            //IconHelper.RemoveIcon(this);
        }
        private void GetData_Click(object sender, RoutedEventArgs e)
        {
            int year = Int16.Parse(cbbYear.SelectedValue.ToString());
            String month = cbbMonth.SelectedValue.ToString();
            String date = cbbDate.SelectedValue.ToString();
            int group = Int16.Parse(cbbGroup.SelectedValue.ToString());
            String groupString = group + "";
            if (group < 10)
                groupString = "0" + group;
            String machine = cbbMachine.SelectedValue.ToString();
            List<String> danhBas = GetDataDBViewModel.getInstance.getDocsosByConditionCount(year, month, date, group, machine);

            pbStatus.Maximum = danhBas.Count;
            pbStatus.Minimum = 0;
            pbStatus.Value = 0;
            foreach (String danhBa in danhBas)
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {

                    GetDataDBViewModel.getInstance.getDocSosByDanhBa(danhBa, year, month, date, group, machine);
                    pbStatus.Value++;
                    txtbStatus.Text = String.Format("{0:0.0%}", (double)pbStatus.Value / pbStatus.Maximum);
                    if (pbStatus.Value == pbStatus.Maximum)
                        GetDataDBViewModel.getInstance.WriteSoDaNhan(year, month, date, machine, (Int16)pbStatus.Maximum, group);

                }), DispatcherPriority.Loaded);
            }



        }



        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            typeof(Window).GetField("_isClosing", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(this, false);
            e.Cancel = true;
            this.Hide();
        }



        private void cbbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            year = Int16.Parse(cbbYear.SelectedValue.ToString());
            cbbMonth.ItemsSource = GetDataDBViewModel.getInstance.getDistinctMonthServer(year);
        }

        private void cbbMachine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            machine = cbbMachine.SelectedValue.ToString();
        }

        private void cbbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            month = cbbMonth.SelectedValue.ToString();
            cbbDate.ItemsSource = GetDataDBViewModel.getInstance.getDistinctDateServer(year, month);
        }
        private void cbbDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            date = cbbDate.SelectedValue.ToString();
            cbbGroup.ItemsSource = GetDataDBViewModel.getInstance.getDistinctGroupServer(year, month, date);
        }
        private void cbbGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            group = Int16.Parse(cbbGroup.SelectedValue.ToString());
            cbbMachine.ItemsSource = GetDataDBViewModel.getInstance.getDistinctMachineServer(year, month, date, group);
        }
    }
}