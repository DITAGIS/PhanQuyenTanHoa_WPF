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
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (checkInfo())
            {
                dtgridMain.Items.Clear();
                //MessageBox.Show(Connection.getInstance.getConnection.ToString());
                Connection.getInstance.disConnect();
                HoaDonDB hoaDonDB = new HoaDonDB();
                List<HoaDon> hoaDons = hoaDonDB.getAllHoaDon();
                foreach (HoaDon hoaDon in hoaDons)
                {
                    dtgridMain.Items.Add(hoaDon);
                }

            }

        }

        private bool checkInfo()
        {

            return true;
        }

        private void DataGridRow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Click");
           
        }

     
    }
}
