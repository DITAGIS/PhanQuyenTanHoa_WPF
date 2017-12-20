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
using System.Windows.Shapes;

namespace PhanQuyen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UC_DieuChinhThongTinDocSo uc_DieuChinhThonTinDocSo;
        UC_InPhieuTieuThuKH uc_InPhieuTieuThuKH;
        UC_NhanDuLieu uc_NhanDuLieu;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnExpandRibbon_Click(object sender, RoutedEventArgs e)
        {
            if (this.RibbonMain.IsMinimized)
            {
                this.btnExpandRibbon.SmallImageSource = new BitmapImage(new Uri("/PhanQuyen;component/Images/up.png", UriKind.Relative));
                this.RibbonMain.IsMinimized = false;
            }
            else
            {
                this.btnExpandRibbon.SmallImageSource = new BitmapImage(new Uri("/PhanQuyen;component/Images/down.png", UriKind.Relative));
                this.RibbonMain.IsMinimized = true;
            }
        }

        private void ribBtnDieuChinhThongTinDocSo_Click(object sender, RoutedEventArgs e)
        {
           if(uc_DieuChinhThonTinDocSo == null)
                uc_DieuChinhThonTinDocSo = new UC_DieuChinhThongTinDocSo();
            uc_DieuChinhThonTinDocSo.Height = stkMain.ActualHeight;
            uc_DieuChinhThonTinDocSo.Width = stkMain.ActualWidth;
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_DieuChinhThonTinDocSo);
        }

        private void ribBtnInPhieuTieuThuKH_Click(object sender, RoutedEventArgs e)
        {
           if(uc_InPhieuTieuThuKH == null)
                uc_InPhieuTieuThuKH = new UC_InPhieuTieuThuKH();
            uc_InPhieuTieuThuKH.Height = stkMain.ActualHeight;
            uc_InPhieuTieuThuKH.Width = stkMain.ActualWidth;
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_InPhieuTieuThuKH);
        }


        private void RibbonButton_Click(object sender, RoutedEventArgs e)
        {
            if (uc_NhanDuLieu == null)
                uc_NhanDuLieu = new UC_NhanDuLieu();
            uc_NhanDuLieu.Height = stkMain.ActualHeight;
            uc_NhanDuLieu.Width = stkMain.ActualWidth;
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(uc_NhanDuLieu);
        }
        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void ExitCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void LogoutCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void LogoutCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
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
