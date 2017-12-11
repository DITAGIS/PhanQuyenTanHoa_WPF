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
        public MainWindow()
        {
            InitializeComponent();
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
            }
            else
            {
                this.btnExpandRibbon.SmallImageSource = new BitmapImage(new Uri("/PhanQuyen;component/Images/down.png", UriKind.Relative));
                this.RibbonMain.IsMinimized = true;
            }
        }
    }
}
