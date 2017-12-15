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
    /// Interaction logic for UC_DieuChinhThongTinDocSo.xaml
    /// </summary>
    public partial class UC_DieuChinhThongTinDocSo : UserControl
    {


        public UC_DieuChinhThongTinDocSo()
        {
            InitializeComponent();
            //cbbYear.Items.Add("2017");
            //cbbMonth.Items.Add("12");
            //cbbDate.Items.Add("08");
            cbbGroup.Items.Add("' or '1'= '1");
            //cbbMachine.Items.Add("01");
        }


      
    }
}
