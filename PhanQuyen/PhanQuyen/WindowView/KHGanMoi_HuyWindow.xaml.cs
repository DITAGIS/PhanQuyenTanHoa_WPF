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
using System.Windows.Shapes;
using ViewModel;

namespace PhanQuyen.WindowView
{
    /// <summary>
    /// Interaction logic for KHGanMoiWindow.xaml
    /// </summary>
    public partial class KHGanMoi_HuyWindow : Window
    {
        public KHGanMoi_HuyWindow()
        {
            InitializeComponent();
        }
        public KHGanMoi_HuyWindow(int nam, string ky, string dot, bool isGanMoi)
        {
            InitializeComponent();
            if (isGanMoi)
                LoadKHGanMoi(nam, ky, dot);
            else
                LoadKHHuy(nam, ky, dot);
        }
        private void LoadKHGanMoi(int nam, string ky, string dot)
        {
            dtgridMain.ItemsSource = HandlingDataDBViewModel.Instance.getKHGanMoi(nam, ky, dot);
            txtbSoLuong.Text = "Số lượng: " + dtgridMain.Items.Count;
        }
        private void LoadKHHuy(int nam, string ky, string dot)
        {
            dtgridMain.ItemsSource = HandlingDataDBViewModel.Instance.getKHHuy(nam, ky, dot);
            txtbSoLuong.Text = "Số lượng: " + dtgridMain.Items.Count;
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
