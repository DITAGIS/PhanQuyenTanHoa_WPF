using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhanQuyen.UserControlView
{
    /// <summary>
    /// Interaction logic for UC_CapNhatSoThan.xaml
    /// </summary>
    public partial class UC_CapNhatSoThan : System.Windows.Controls.UserControl
    {
        public UC_CapNhatSoThan()
        {
            InitializeComponent();
        }

        private void btnLoadData_Click(object sender, RoutedEventArgs e)
        {
            StreamReader sr = (StreamReader)null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "dat";
            openFileDialog.Filter = "File số thân|*.dat";
            string empty2 = string.Empty;
            if (DialogResult.OK != openFileDialog.ShowDialog())
                return;

            string fileName = openFileDialog.FileName;
            String danhBa = "";
            int num2 = 0;

            try
            {
                sr = new StreamReader(fileName);
                while (!sr.EndOfStream)
                {
                }
            }
            catch
            {

            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
