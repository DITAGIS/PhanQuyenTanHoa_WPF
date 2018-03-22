using Model;
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
    /// Interaction logic for UC_ChuyenBilling.xaml
    /// </summary>
    public partial class UC_ChuyenBilling : System.Windows.Controls.UserControl
    {
        public UC_ChuyenBilling()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            dtgridMain.ItemsSource = HandlingDataDBViewModel.Instance.GetBilling();

            int countDHN = 0;
            int tieuThu = 0;
            foreach (ChuyenBilling item in dtgridMain.Items)
            {

                countDHN += item.DHN;
                tieuThu += item.TieuThu;
            }

            txtbSum.Text = String.Format("Tổng cộng: {0} khách hàng -------  {1} m3", countDHN, tieuThu);
        }

        private void btnGetData_Click(object sender, RoutedEventArgs e)
        {
            ConnectionViewModel.Instance.Connect();
            ConnectionViewModel.Instance.DisConnect();
            C_chuyenBilling chuyenBilling = new C_chuyenBilling(MyUser.Instance.Month,
                MyUser.Instance.Date, MyUser.Instance.Year,
                MyUser.Instance.UserName,
                MyUser.Instance.Password,
                "TH",
               ConnectionViewModel.Instance.getConnection);

            
            chuyenBilling.CapNhatDuLieuBilling(datePicker.SelectedDate.Value,new System.Windows.Forms.ToolStripProgressBar());
        }
    }
}
