using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PhanQuyen.WindowView
{
    /// <summary>
    /// Interaction logic for ViewImageWindow.xaml
    /// </summary>
    public partial class ViewImageWindow : Window
    {
        //private ImageSource _imageByteArray;
        //public ViewImageWindow(ImageSource imageByteArray)
        //{
        //    InitializeComponent();
        //    _imageByteArray = imageByteArray;
        //    image.Source = _imageByteArray;

        //}

        private Point origin;
        private Point start;
        private static ViewImageWindow _instance = null;
        public static ViewImageWindow Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ViewImageWindow();
                return _instance;
            }
        }
        private ViewImageWindow()
        {
            InitializeComponent();
            WPFWindow.MouseWheel += MainWindow_MouseWheel;

            image.MouseLeftButtonDown += image_MouseLeftButtonDown;
            image.MouseLeftButtonUp += image_MouseLeftButtonUp;
            image.MouseMove += image_MouseMove;
        }
        public void SetImage(ImageSource imageByteArray)
        {
            image.Source = imageByteArray;
        }

        private void image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            image.ReleaseMouseCapture();
        }

        private void image_MouseMove(object sender, MouseEventArgs e)
        {
            if (!image.IsMouseCaptured) return;
            Point p = e.MouseDevice.GetPosition(border);

            Matrix m = image.RenderTransform.Value;
            m.OffsetX = origin.X + (p.X - start.X);
            m.OffsetY = origin.Y + (p.Y - start.Y);

            image.RenderTransform = new MatrixTransform(m);
        }

        private void image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (image.IsMouseCaptured) return;
            image.CaptureMouse();

            start = e.GetPosition(border);
            origin.X = image.RenderTransform.Value.OffsetX;
            origin.Y = image.RenderTransform.Value.OffsetY;
        }

        private void MainWindow_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Point p = e.MouseDevice.GetPosition(image);

            Matrix m = image.RenderTransform.Value;
            if (e.Delta > 0)
                m.ScaleAtPrepend(1.1, 1.1, p.X, p.Y);
            else
                m.ScaleAtPrepend(1 / 1.1, 1 / 1.1, p.X, p.Y);

            image.RenderTransform = new MatrixTransform(m);
        }

        private void WPFWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            (typeof(Window)).GetField("_isClosing",  BindingFlags.Instance | BindingFlags.NonPublic).SetValue(sender, false);

            e.Cancel = true;

            (sender as Window).Hide();
        }
    }

}
