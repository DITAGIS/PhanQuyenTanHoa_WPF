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
using PhanQuyen.UserControl.BaoCao;
using PhanQuyen.UserControlView;
using PhanQuyen.UserControlView.BaoCao;
using PhanQuyen.UserControlView.HeThong;
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
        UC_InTieuThuBatThuong uc_InTieuThuBatThuong;
        UC_NhanDuLieu uc_NhanDuLieu;
        UC_CapNhatHoaDon uc_CapNhatHoaDon;
        UC_ChuyenMayDocSo uc_ChuyenMayDocSo;
        UC_ThongKeDHNSauDocSo uc_ThongKeDHNSauDocSo;
        UC_ThongKeDHNTrenMang uc_ThongKeDHNTrenMang;
        UC_BaoCaoTongHop uc_BaoCaoTongHop;
        UC_KiemTraDuLieu uc_KiemTraDuLieu;
        UC_ThongKeDHNTheoDotSo uc_ThongKeDHNTheoDotSo;
        UC_XuatDuLieuRaSmartPhone uc_XuatDuLieuRaSmartPhone;
        UC_DoiMatkhau uc_DoiMatKhau;
        UC_Config uc_Config;
        UC_QuanLyNhanVienDocSo uc_QuanLyNhanVienDocSo;
        private MyUser user;
        private String title = "Phần mềm kết nối ứng dụng đọc số trên Smartphone            Nhân viên: ";
        public MainWindow()
        {

            InitializeComponent();

        }

        public MainWindow(MyUser user)
        {
            this.user = user; InitializeComponent();
            this.title += user.UserName;
            this.Title = title;
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
            if (uc_DieuChinhThonTinDocSo != null)
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
            if (uc_InTieuThuBatThuong != null)
            {
                uc_InTieuThuBatThuong.Height = stkMain.ActualHeight;
                uc_InTieuThuBatThuong.Width = stkMain.ActualWidth;
            }
            if (uc_ThongKeDHNTrenMang != null)
            {
                uc_ThongKeDHNTrenMang.Height = stkMain.ActualHeight;
                uc_ThongKeDHNTrenMang.Width = stkMain.ActualWidth;
            }
            if (uc_XuatDuLieuRaSmartPhone != null)
            {
                uc_XuatDuLieuRaSmartPhone.Height = stkMain.ActualHeight;
                uc_XuatDuLieuRaSmartPhone.Width = stkMain.ActualWidth;
            }
            if (uc_DoiMatKhau != null)
            {
                uc_DoiMatKhau.Height = stkMain.ActualHeight;
                uc_DoiMatKhau.Width = stkMain.ActualWidth;
            }
            if (uc_Config != null)
            {
                uc_Config.Height = stkMain.ActualHeight;
                uc_Config.Width = stkMain.ActualWidth;
            }
            if (uc_QuanLyNhanVienDocSo != null)
            {
                uc_QuanLyNhanVienDocSo.Height = stkMain.ActualHeight;
                uc_QuanLyNhanVienDocSo.Width = stkMain.ActualWidth;
            }
        }


        private void ribBtnDieuChinhThongTinDocSo_Click(object sender, RoutedEventArgs e)
        {
            if (uc_DieuChinhThonTinDocSo == null)
                uc_DieuChinhThonTinDocSo = new UC_DieuChinhThongTinDocSo(user);
            resizeUC();
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_DieuChinhThonTinDocSo);
            this.Title = this.title + "            Điều chỉnh thông tin đọc số";
        }

        private void ribBtnInPhieuTieuThuKH_Click(object sender, RoutedEventArgs e)
        {
            String danhBa = null;
            if (uc_DieuChinhThonTinDocSo != null)
            {
                danhBa = uc_DieuChinhThonTinDocSo.DanhBa;
            }
            if (uc_InPhieuTieuThuKH == null)
                uc_InPhieuTieuThuKH = new UC_InPhieuTieuThuKH();
            if (danhBa != null)
            {
                uc_InPhieuTieuThuKH.setDanhBa(danhBa);
            }
            resizeUC();
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_InPhieuTieuThuKH);

            this.Title = this.title + "             In phiếu tiêu thụ khách hàng";
            uc_InPhieuTieuThuKH.Print();
        }


        private void ribBtnCapNhatHoaDon(object sender, RoutedEventArgs e)
        {
            if (uc_CapNhatHoaDon == null)
                uc_CapNhatHoaDon = new UC_CapNhatHoaDon();
            resizeUC();
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            this.Title = this.title + "             Cập nhật hóa đơn";
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
            this.Title = this.title + "             Nhận dữ liệu";
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
            this.Title = this.title + "             Thống kê ĐHN sau đọc số";
        }
        private void ribBtnBaoCaoTongHop_Click(object sender, RoutedEventArgs e)
        {
            if (uc_BaoCaoTongHop == null)
                uc_BaoCaoTongHop = new UC_BaoCaoTongHop();
            resizeUC();
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_BaoCaoTongHop);
            this.Title = this.title + "             Báo cáo tổng hợp";
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
            this.Title = this.title + "             Chuyển billing";
        }

        private void ribBtnInDanhSachDongCua_Click(object sender, RoutedEventArgs e)
        {
            if (uc_InDanhSachDongCua == null)
                uc_InDanhSachDongCua = new UC_InDanhSachDongCua();
            resizeUC();
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_InDanhSachDongCua);
            this.Title = this.title + "             In danh sách đóng cửa";
        }
        private void ribBtnXuatDuLieu_Click(object sender, RoutedEventArgs e)
        {
            if (uc_XuatDuLieuRaSmartPhone == null)
                uc_XuatDuLieuRaSmartPhone = new UC_XuatDuLieuRaSmartPhone();
            resizeUC();
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_XuatDuLieuRaSmartPhone);
            this.Title = this.title + "             Tạo dữ liệu đọc số, xuất dữ liệu ra Smartphone";
        }
        private void ribBtnKiemTraDuLieu_Click(object sender, RoutedEventArgs e)
        {
            if (uc_KiemTraDuLieu == null)
                uc_KiemTraDuLieu = new UC_KiemTraDuLieu();
            resizeUC();
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_KiemTraDuLieu);
            this.Title = this.title + "             Kiểm tra dữ liệu";
        }
        private void ribBtnThongKeDHNTheoDotSo_Click(object sender, RoutedEventArgs e)
        {
            if (uc_ThongKeDHNTheoDotSo == null)
                uc_ThongKeDHNTheoDotSo = new UC_ThongKeDHNTheoDotSo();
            resizeUC();
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_ThongKeDHNTheoDotSo);
            this.Title = this.title + "             Thống kê ĐHN theo đợt sổ";
        }
        private void ribBtnInTieuThuBatThuong_Click(object sender, RoutedEventArgs e)
        {
            if (uc_InTieuThuBatThuong == null)
                uc_InTieuThuBatThuong = new UC_InTieuThuBatThuong();
            resizeUC();
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_InTieuThuBatThuong);
            this.Title = this.title + "             In phiếu tiêu thụ bất thường";
        }
        private void ribBtnThongKeDHNTrenMang_Click(object sender, RoutedEventArgs e)
        {
            if (uc_ThongKeDHNTrenMang == null)
                uc_ThongKeDHNTrenMang = new UC_ThongKeDHNTrenMang();
            resizeUC();
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_ThongKeDHNTrenMang);
            this.Title = this.title + "             Thống kê ĐHN trên mạng";
        }
        private void ribBtnConfig_Click(object sender, RoutedEventArgs e)
        {
            if (uc_Config == null)
                uc_Config = new UC_Config();
            resizeUC();
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_Config);
            this.Title = this.title + "             Cấu hình hệ thống";
        }
        private void ribBtnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (uc_DoiMatKhau == null)
                uc_DoiMatKhau = new UC_DoiMatkhau();
            resizeUC();
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_DoiMatKhau);
            this.Title = this.title + "             Đổi mật khẩu";
        }
        private void ribBtnQuanLyNhanVienDocSo_Click(object sender, RoutedEventArgs e)
        {
            if (uc_QuanLyNhanVienDocSo == null)
                uc_QuanLyNhanVienDocSo = new UC_QuanLyNhanVienDocSo();
            resizeUC();
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_QuanLyNhanVienDocSo);
            this.Title = this.title + "             Quản lý nhân viên đọc số";
        }
        private void ribBtnHoanTatDocSo_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("Bạn có chắc chắn hoàn tất dữ liệu đọc số không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                return;
            try
            {
                bool result = HandlingDataDBViewModel.Instance.HoanTatDocSo();
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
                bool result = HandlingDataDBViewModel.Instance.HoanTatThuongVu();
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
