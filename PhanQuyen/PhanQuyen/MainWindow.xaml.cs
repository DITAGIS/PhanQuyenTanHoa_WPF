using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Model;
using ViewModel;

namespace PhanQuyen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UC_DieuChinhThongTinDocSo uc_DieuChinhThonTinDocSo;
        UC_InPhieuTieuThuKH uc_InPhieuTieuThuKH;
        UC_InDanhSachDongCua uc_InDanhSachDongCua;
        UC_NhanDuLieu uc_NhanDuLieu;
        UC_CapNhatHoaDon uc_CapNhatHoaDon;
        UC_ChuyenMayDocSo uc_ChuyenMayDocSo;
        UC_ThongKeDHNSauDocSo uc_ThongKeDHNSauDocSo;
        UC_BaoCaoTongHop uc_BaoCaoTongHop;
        UC_ChuyenBilling uc_ChuyenBilling;
        UC_KiemTraDuLieu uc_KiemTraDuLieu;
        UC_ThongKeDHNTheoDotSo uc_ThongKeDHNTheoDotSo;
        private User user;

        public MainWindow()
        {

            InitializeComponent();

        }

        public MainWindow(User user)
        {
            this.user = user; InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void btnExpandRibbon_Click(object sender, RoutedEventArgs e)
        {
            if (this.RibbonMain.IsMinimized)
            {
                this.btnExpandRibbon.SmallImageSource = new BitmapImage(new Uri("/PhanQuyen;component/Images/up.png", UriKind.Relative));
                this.RibbonMain.IsMinimized = false;
                setScrollView(false);
            }
            else
            {
                this.btnExpandRibbon.SmallImageSource = new BitmapImage(new Uri("/PhanQuyen;component/Images/down.png", UriKind.Relative));
                this.RibbonMain.IsMinimized = true;
                setScrollView(true);
            }
            resizeUC();
        }

        private void setScrollView(bool v)
        {
            if (v)
            {
                uc_DieuChinhThonTinDocSo.GetScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            }
            else
            {
                uc_DieuChinhThonTinDocSo.GetScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
            uc_DieuChinhThonTinDocSo.GetScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
        }

        private void resizeUC()
        {
            if (uc_DieuChinhThonTinDocSo != null)
            {
                uc_DieuChinhThonTinDocSo.Height = stkMain.ActualHeight;
                uc_DieuChinhThonTinDocSo.Width = stkMain.ActualWidth;
            }
            if (uc_ChuyenMayDocSo != null)
            {
                uc_ChuyenMayDocSo.Height = stkMain.ActualHeight;
                uc_ChuyenMayDocSo.Width = stkMain.ActualWidth;
            }
            if (uc_InPhieuTieuThuKH != null)
            {
                uc_InPhieuTieuThuKH.Height = stkMain.ActualHeight;
                uc_InPhieuTieuThuKH.Width = stkMain.ActualWidth;
            }
            if (uc_CapNhatHoaDon != null)
            {
                uc_CapNhatHoaDon.Height = stkMain.ActualHeight;
                uc_CapNhatHoaDon.Width = stkMain.ActualWidth;
            }
            if (uc_NhanDuLieu != null)
            {
                uc_NhanDuLieu.Height = stkMain.ActualHeight;
                uc_NhanDuLieu.Width = stkMain.ActualWidth;
            }
            if (uc_ThongKeDHNSauDocSo != null)
            {
                uc_ThongKeDHNSauDocSo.Height = stkMain.ActualHeight;
                uc_ThongKeDHNSauDocSo.Width = stkMain.ActualWidth;
            }
            if (uc_BaoCaoTongHop != null)
            {
                uc_BaoCaoTongHop.Height = stkMain.ActualHeight;
                uc_BaoCaoTongHop.Width = stkMain.ActualWidth;
            }
            if (uc_ChuyenBilling != null)
            {
                uc_ChuyenBilling.Height = stkMain.ActualHeight;
                uc_ChuyenBilling.Width = stkMain.ActualWidth;
            }
            if (uc_InDanhSachDongCua != null)
            {
                uc_InDanhSachDongCua.Height = stkMain.ActualHeight;
                uc_InDanhSachDongCua.Width = stkMain.ActualWidth;
            }
            if (uc_KiemTraDuLieu != null)
            {
                uc_KiemTraDuLieu.Height = stkMain.ActualHeight;
                uc_KiemTraDuLieu.Width = stkMain.ActualWidth;
            }
            if (uc_ThongKeDHNTheoDotSo != null)
            {
                uc_ThongKeDHNTheoDotSo.Height = stkMain.ActualHeight;
                uc_ThongKeDHNTheoDotSo.Width = stkMain.ActualWidth;
            }
        }

        private void ribBtnChuyenMayDocSo_Click(object sender, RoutedEventArgs e)
        {
            if (uc_ChuyenMayDocSo == null)
                uc_ChuyenMayDocSo = new UC_ChuyenMayDocSo();
            resizeUC();
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_ChuyenMayDocSo);
        }
        private void ribBtnDieuChinhThongTinDocSo_Click(object sender, RoutedEventArgs e)
        {
            if (uc_DieuChinhThonTinDocSo == null)
                uc_DieuChinhThonTinDocSo = new UC_DieuChinhThongTinDocSo(user);
            resizeUC();
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_DieuChinhThonTinDocSo);
        }

        private void ribBtnInPhieuTieuThuKH_Click(object sender, RoutedEventArgs e)
        {
            if (uc_InPhieuTieuThuKH == null)
                uc_InPhieuTieuThuKH = new UC_InPhieuTieuThuKH();
            resizeUC();
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_InPhieuTieuThuKH);
        }


        private void ribBtnCapNhatHoaDon(object sender, RoutedEventArgs e)
        {
            if (uc_CapNhatHoaDon == null)
                uc_CapNhatHoaDon = new UC_CapNhatHoaDon();
            resizeUC();
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_CapNhatHoaDon);
        }


        private void ribBtnNhanDuLieu(object sender, RoutedEventArgs e)
        {
            if (uc_NhanDuLieu == null)
                uc_NhanDuLieu = new UC_NhanDuLieu();
            resizeUC();
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_NhanDuLieu);
            uc_NhanDuLieu.ShowGetDataWindow();
        }
        private void ribBtnThongKeSauDocSo_Click(object sender, RoutedEventArgs e)
        {
            if (uc_ThongKeDHNSauDocSo == null)
                uc_ThongKeDHNSauDocSo = new UC_ThongKeDHNSauDocSo();
            resizeUC();
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_ThongKeDHNSauDocSo);
        }
        private void ribBtnBaoCaoTongHop_Click(object sender, RoutedEventArgs e)
        {
            if (uc_BaoCaoTongHop == null)
                uc_BaoCaoTongHop = new UC_BaoCaoTongHop();
            resizeUC();
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_BaoCaoTongHop);
        }
        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void ExitCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void LogoutCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void LogoutCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(System.Windows.Application.ResourceAssembly.Location);
            System.Windows.Application.Current.Shutdown();
        }

        private void ribBtnChuyenBilling_Click(object sender, RoutedEventArgs e)
        {
            if (uc_ChuyenBilling == null)
                uc_ChuyenBilling = new UC_ChuyenBilling();
            resizeUC();
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_ChuyenBilling);
        }

        private void ribBtnInDanhSachDongCua_Click(object sender, RoutedEventArgs e)
        {
            if (uc_InDanhSachDongCua == null)
                uc_InDanhSachDongCua = new UC_InDanhSachDongCua();
            resizeUC();
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_InDanhSachDongCua);
        }

        private void ribBtnKiemTraDuLieu_Click(object sender, RoutedEventArgs e)
        {
            if (uc_KiemTraDuLieu == null)
                uc_KiemTraDuLieu = new UC_KiemTraDuLieu();
            resizeUC();
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_KiemTraDuLieu);
        }
        private void ribBtnThongKeDHNTheoDotSo_Click(object sender, RoutedEventArgs e)
        {
            if (uc_ThongKeDHNTheoDotSo == null)
                uc_ThongKeDHNTheoDotSo = new UC_ThongKeDHNTheoDotSo();
            resizeUC();
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_ThongKeDHNTheoDotSo);
        }
        private void ribBtnHoanTatDocSo_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("Bạn có chắc chắn hoàn tất dữ liệu đọc số không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                return;
            try
            {
                bool result = DataDBViewModel.Instance.HoanTatDocSo();
                if (result)
                    System.Windows.Forms.MessageBox.Show("Hoàn tất đọc số thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi khi hoàn tất đọc số: " + ex.Message);
            }
        }

        private void ribBtnHoanTatThuongVu_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("Bạn có chắc chắn hoàn tất dữ liệu đọc số không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                return;
            try
            {
                bool result = DataDBViewModel.Instance.HoanTatThuongVu();
                if (result)
                    System.Windows.Forms.MessageBox.Show("Hoàn tất đọc số thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            catch (SqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi khi hoàn tất đọc số: " + ex.Message);
            }
        }

     
    }

    public static class CustomCommands
    {
        public static readonly RoutedUICommand Exit = new RoutedUICommand(
            "Exit", "Exit", typeof(CustomCommands), new InputGestureCollection()
            {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
            });
        public static readonly RoutedUICommand Logout = new RoutedUICommand(
           "Logout", "Logout", typeof(CustomCommands), new InputGestureCollection()
           {
                    new KeyGesture(Key.F3, ModifierKeys.Alt)
           });

        //defind more command hear, just like the one above
    }

}
