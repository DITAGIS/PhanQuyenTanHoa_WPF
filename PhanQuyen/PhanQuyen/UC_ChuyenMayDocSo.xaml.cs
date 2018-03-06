using System;
using System.Collections.Generic;
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
    /// Interaction logic for UC_ChuyenMayDocSo.xaml
    /// </summary>
    public partial class UC_ChuyenMayDocSo : System.Windows.Controls.UserControl
    {
        String date, machineLeft, machineRight;
        public UC_ChuyenMayDocSo()
        {
            InitializeComponent();
            //txtbGroup.Text = "Tổ: " + 
            cbbDate.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctDate();
            cbbMachineLeft.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctMachine();
            cbbMachineRight.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctMachine();
        }

        private void cbbDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            date = cbbDate.SelectedValue.ToString();
        }

        private void cbbMachineLeft_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            machineLeft = cbbMachineLeft.SelectedValue.ToString();
            dtGridLeft.ItemsSource = HandlingDataDBViewModel.Instance.getKH_ChuyenMayDS(date, machineLeft);
        }

        private void cbbMachineRight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            machineRight = cbbMachineRight.SelectedValue.ToString();
            dtGridRight.ItemsSource = HandlingDataDBViewModel.Instance.getKH_ChuyenMayDS(date, machineRight);
        }
    }
}
