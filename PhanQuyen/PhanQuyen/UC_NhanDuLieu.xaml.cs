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
    /// Interaction logic for UC_NhanDuLieu.xaml
    /// </summary>
    public partial class UC_NhanDuLieu : UserControl
    {
        public UC_NhanDuLieu()
        {
            InitializeComponent();

            cbbYear.Items.Add(2018);

            cbbGroup.Items.Add(1);
        }

        private void GetData_Click(object sender, RoutedEventArgs e)
        {
            GetDataDBViewModel.getInstance.getDocSosByCondition(Int16.Parse(cbbYear.SelectedValue.ToString()), cbbMonth.SelectedValue.ToString(),
                cbbDate.SelectedValue.ToString(), Int16.Parse(cbbGroup.SelectedValue.ToString()));
        }
    }
}
