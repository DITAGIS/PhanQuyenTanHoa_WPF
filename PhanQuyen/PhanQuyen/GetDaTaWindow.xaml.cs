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
        public GetDaTaWindow()
        {
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            InitializeComponent();
            cbbYear.Items.Add("2018");
            cbbMonth.Items.Add("01");
            cbbDate.Items.Add("18");
            cbbGroup.Items.Add("01");
            cbbMachine.Items.Add("01");



        }
        private void GetData_Click(object sender, RoutedEventArgs e)
        {
            List<String> danhBas = GetDataDBViewModel.getInstance.getDocsosByConditionCount(Int16.Parse(cbbYear.SelectedValue.ToString()), cbbMonth.SelectedValue.ToString(),
                cbbDate.SelectedValue.ToString(), Int16.Parse(cbbGroup.SelectedValue.ToString()), cbbMachine.SelectedValue.ToString());

            pbStatus.Maximum = danhBas.Count;
            pbStatus.Minimum = 0;
            pbStatus.Value = 0;
            foreach (String danhBa in danhBas)
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    
                    GetDataDBViewModel.getInstance.getDocSosByDanhBa(danhBa, Int16.Parse(cbbYear.SelectedValue.ToString()), cbbMonth.SelectedValue.ToString(),
               cbbDate.SelectedValue.ToString(), Int16.Parse(cbbGroup.SelectedValue.ToString()), cbbMachine.SelectedValue.ToString());
                    pbStatus.Value++;
                    txtbStatus.Text = String.Format("{0:0.0%}", (double)pbStatus.Value / pbStatus.Maximum);

                }), DispatcherPriority.Loaded);
            }



        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            typeof(Window).GetField("_isClosing", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(this, false);
            e.Cancel = true;
            this.Hide();
        }
    }
}
