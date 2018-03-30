using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
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
using ViewModel;

namespace PhanQuyen.WindowView
{
    /// <summary>
    /// Interaction logic for PrintImageWindow.xaml
    /// </summary>
    public partial class PrintImageWindow : Window
    {
        private static PrintImageWindow _instance;
        public static PrintImageWindow Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PrintImageWindow();
                return _instance;
            }
        }
        private PrintImageWindow()
        {
            InitializeComponent();
            PageSettings ps = new System.Drawing.Printing.PageSettings();
            ps.Landscape = false;
            Margins margins = new Margins(70, 50, 50, 50);
            ps.Margins = margins;
            _reportViewer.SetPageSettings(ps);
        }
        public void SetImage(DocSo selectedDocSo, ImageSource img)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DocSoID", typeof(byte[]));
            dt.Columns.Add("DanhBa");
            dt.Columns.Add("MLT1");
            dt.Columns.Add("SoThanCu");
            dt.Columns.Add("HieuCu");
            dt.Columns.Add("CoCu");
            dt.Columns.Add("Nam");
            dt.Columns.Add("Ky");
            dt.Columns.Add("Dot");
            dt.Columns.Add("TenKH");
            dt.Columns.Add("SoNhaMoi");
            dt.Columns.Add("Duong");
            dt.Columns.Add("GIOGHI");
            DataRow row = dt.NewRow();

            row["DocSoID"] = ImageSourceToBytes(new PngBitmapEncoder(), img);
            row["DanhBa"] = selectedDocSo.DanhBa;
            row["MLT1"] = selectedDocSo.MLT1;
            row["SoThanCu"] = selectedDocSo.SoThanCu;
            row["HieuCu"] = selectedDocSo.HieuCu;
            row["CoCu"] = selectedDocSo.CoCu;
            row["Nam"] = selectedDocSo.Nam;
            row["Ky"] = selectedDocSo.Ky;
            row["Dot"] = selectedDocSo.Dot;
            row["TenKH"] = selectedDocSo.TenKH;
            row["SoNhaMoi"] = selectedDocSo.SoNhaCu;
            row["Duong"] = selectedDocSo.Duong;
            row["GIOGHI"] = selectedDocSo.GIOGHI.Value.ToString("yyyy-MM-dd hh:mm:ss");

            dt.Rows.Add(row);
            _reportViewer.LocalReport.ReportPath = "../Report/rptInHinhAnh.rdlc";
            this._reportViewer.LocalReport.DataSources.Clear();
            this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dtsInHinhAnh", dt));
            this._reportViewer.RefreshReport();
        }

        public byte[] ImageSourceToBytes(BitmapEncoder encoder, ImageSource imageSource)
        {
            byte[] bytes = null;
            var bitmapSource = imageSource as BitmapSource;

            if (bitmapSource != null)
            {
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    bytes = stream.ToArray();
                }
            }

            return bytes;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            (typeof(Window)).GetField("_isClosing", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(sender, false);
            e.Cancel = true;
            (sender as Window).Hide();
        }
    }
}
