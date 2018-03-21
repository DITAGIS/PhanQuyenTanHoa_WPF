using Model;
using System;
using System.Collections.Generic;
using System.Data;
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
using ViewModel;

namespace PhanQuyen.UserControlView
{
    /// <summary>
    /// Interaction logic for UC_XuatDuLieuRaSmartPhone.xaml
    /// </summary>
    public partial class UC_XuatDuLieuRaSmartPhone : System.Windows.Controls.UserControl
    {
        private int year, group;
        private String month, date, machine;
        private ItemCollection DanhMucFile
        {
            get { return dtgridMain.Items; }

        }
        public UC_XuatDuLieuRaSmartPhone()
        {
            InitializeComponent();
            cbbYear.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctYearServer();
            dpkTuNgay.SelectedDate = DateTime.Now;
            dpkDenNgay.SelectedDate = DateTime.Now;
            if (MyUser.Instance.ToID == null || MyUser.Instance.ToID.Equals("") || MyUser.Instance.ToID.Trim().Equals(""))
                cbbGroup.ItemsSource = ToID.GetToID();
            //else if (MyUser.Instance.ToID.Equals(""))
            //    cbbGroup.ItemsSource = ToID.GetToID();
            else
                cbbGroup.Items.Add(MyUser.Instance.ToID);
            for (int i = 1; i <= 20; i++)
                cbbDate.Items.Add(i.ToString("00"));
            for (int i = 1; i <= 12; i++)
                cbbMonth.Items.Add(i.ToString("00"));
        }


        private void cbbGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbGroup.SelectedValue != null)
            {
                short x;
                if (cbbGroup.SelectedValue == null)
                    group = -1;
                else if (Int16.TryParse(cbbGroup.SelectedValue.ToString(), out x))
                    group = Int16.Parse(cbbGroup.SelectedValue.ToString());
                else group = x;
                GetDanhMucFileTheoTo();
                GetDanhMucFileDaTao();
                GetFileCheck();
            }
        }
        private void GetDanhMucFileTheoTo()
        {
            this.dtgridMain.ItemsSource = null;
            dtgridMain.ItemsSource = HandlingDataDBViewModel.Instance.GetDanhMucFileTheoTo(year, month, date, group);
        }
        private void GetFileCheck()
        {
            List<TruyenDuLieu> lst = this.dtgridMain.ItemsSource as List<TruyenDuLieu>;
            for (int index = 0; index < lst.Count; ++index)
            {
                if (lst.ElementAt(index).DaTao.Equals(""))
                    lst.ElementAt(index).X = true;
            }
            this.dtgridMain.ItemsSource = null;
            dtgridMain.ItemsSource = lst;
        }

        private void GetDanhMucFileDaTao()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            string empty2 = string.Empty;
            if (DialogResult.OK != folderBrowserDialog.ShowDialog())
                return;
            string path = folderBrowserDialog.SelectedPath;
            List<TruyenDuLieu> tmp = new List<TruyenDuLieu>();
            foreach (FileSystemInfo file in new DirectoryInfo(path).GetFiles())
            {
                object obj = (object)file.Name.Replace(".txt", "");
                foreach (var item in DanhMucFile)
                {
                    TruyenDuLieu t = item as TruyenDuLieu;
                    if (t.DanhMucFile.Equals(obj))
                    {
                        tmp.Add(new TruyenDuLieu()
                        {
                            X = false,
                            May = t.May,
                            DanhMucFile = t.DanhMucFile,
                            SoKH = t.SoKH,
                            DaTao = obj.ToString()
                        });
                        break;
                    }
                }
            }
            dtgridMain.ItemsSource = tmp;
        }
        private string ChuyenCode4(string TTDHN)
        {
            string str = "";
            if (TTDHN.Trim() == "CSBT(4.0)")
                str = "40";
            else if (TTDHN.Trim() == "CSBT(4.1)")
                str = "41";
            else if (TTDHN.Trim() == "CSBT(4.2)")
                str = "42";
            else if (TTDHN.Trim() == "BOT-IX")
                str = "43";
            else if (TTDHN.Trim() == "CSBT(4.4)")
                str = "44";
            return str;
        }
        private string[] ChuanBiKH(string danhba, string nam, string ky)
        {
            string str1 = nam + ky + danhba;
            string[] strArray = new string[16];
            int num1 = 0;
            List<ChuanBiKH> lst = HandlingDataDBViewModel.Instance.GetDocSo_ChuanBiKH(danhba, str1);
            int count = lst.Count;
            if (count == 0)
            {
                strArray[3] = "M0";
                strArray[0] = "0";
                strArray[4] = "0";
                strArray[2] = "0";
                strArray[1] = "GAN MOI";
                strArray[5] = "";
                strArray[14] = "0";
                strArray[15] = "";
            }
            else if (count >= 1)
            {
                string str2 = lst.ElementAt(0).CodeMoi.ToString();
                string str3 = this.ChuyenCode4(lst.ElementAt(0).TTDHNMoi.ToString());
                strArray[3] = !(str3 == "") ? str3 : lst.ElementAt(0).CodeMoi.ToString();
                switch (str2)
                {
                    case "60":
                    case "61":
                    case "62":
                    case "63":
                    case "64":
                    case "65":
                    case "66":
                    case "F1":
                    case "F2":
                    case "F3":
                    case "F4":
                    case "N":
                        num1 = int.Parse(lst.ElementAt(0).TBTT.ToString());
                        int num2 = int.Parse(lst.ElementAt(0).TieuThuMoi.ToString());
                        for (int index = 1; index < lst.Count; ++index)
                        {
                            string str4 = lst.ElementAt(index).CodeMoi.ToString().Substring(0, 1);
                            if (!(str4 == "4") && !(str4 == "5") && !(str4 == "8") && !(str4 == "M"))
                                num2 += int.Parse(lst.ElementAt(index).TieuThuMoi.ToString());
                            else
                                break;
                        }
                        strArray[14] = num2.ToString();
                        strArray[0] = (int.Parse(lst.ElementAt(0).CSMoi.ToString()) + int.Parse(num2.ToString())).ToString();
                        break;
                    case "K":
                        int num3 = int.Parse(lst.ElementAt(0).TieuThuMoi.ToString());
                        for (int index = 1; index < lst.Count; ++index)
                        {
                            string str4 = lst.ElementAt(index).CodeMoi.ToString().Substring(0, 1);
                            if (!(str4 == "4") && !(str4 == "5") && !(str4 == "8") && !(str4 == "M"))
                                num3 += int.Parse(lst.ElementAt(index).TieuThuMoi.ToString());
                            else
                                break;
                        }
                        strArray[14] = num3.ToString();
                        strArray[0] = int.Parse(lst.ElementAt(0).CSMoi.ToString()).ToString();
                        break;
                    default:
                        strArray[14] = "0";
                        strArray[0] = lst.ElementAt(0).CSMoi.ToString();
                        break;
                }
                strArray[0] = lst.ElementAt(0).CSMoi.ToString();
                strArray[4] = lst.ElementAt(0).TieuThuMoi.ToString();
                strArray[1] = lst.ElementAt(0).TTDHNMoi.ToString();
                double num4 = 0.0;
                if (str2 == "60" || str2 == "61" || (str2 == "62" || str2 == "63") || (str2 == "64" || str2 == "65" || (str2 == "66" || str2 == "F1")) || (str2 == "F2" || str2 == "F3" || (str2 == "F4" || str2 == "80")) || str2 == "N")
                {
                    double num5 = (double)num1;
                    strArray[2] = num5.ToString();
                }
                else
                {
                    int index;
                    for (index = 0; index < count && index < 3; ++index)
                    {
                        string s1 = lst.ElementAt(index).TieuThuMoi.ToString();
                        if (s1 == null || s1 == "")
                        {
                            string s2 = "0";
                            num4 += double.Parse(s2);
                        }
                        else
                            num4 += double.Parse(s1);
                    }
                    double num5 = Math.Round(num4 / (double)index);
                    strArray[2] = num5.ToString();
                }
                strArray[5] = lst.ElementAt(0).DenNgay.ToString();
                strArray[15] = lst.ElementAt(0).Ky.ToString() + "/" + lst.ElementAt(0).Nam.ToString().Substring(2);
                if (count >= 2)
                {
                    strArray[6] = lst.ElementAt(1).Ky.ToString() + "/" + lst.ElementAt(1).Nam.ToString().Substring(2);
                    strArray[8] = lst.ElementAt(1).CSMoi.ToString();
                    strArray[9] = lst.ElementAt(1).TieuThuMoi.ToString();
                    string str4 = this.ChuyenCode4(lst.ElementAt(1).TTDHNMoi.ToString());
                    strArray[7] = !(str4 == "") ? str4 : lst.ElementAt(1).CodeMoi.ToString();
                    if (count >= 3)
                    {
                        strArray[10] = lst.ElementAt(2).Ky.ToString() + "/" + lst.ElementAt(2).Nam.ToString().Substring(2);
                        strArray[11] = lst.ElementAt(2).CodeMoi.ToString();
                        strArray[12] = lst.ElementAt(2).CodeMoi.ToString();
                        strArray[13] = lst.ElementAt(2).TieuThuMoi.ToString();
                        string str5 = this.ChuyenCode4(lst.ElementAt(2).TTDHNMoi.ToString());
                        strArray[11] = !(str5 == "") ? str5 : lst.ElementAt(2).CodeMoi.ToString();
                    }
                }
            }
            return strArray;
        }
        public static bool CheckDot(string text, ItemCollection items)
        {
            try
            {
                string str = int.Parse(text).ToString("00");
                if (!items.Contains((object)str))
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void btnTaoFile_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckDot(cbbDate.Text, cbbDate.Items))
            {
                cbbDate.Text = int.Parse(cbbDate.Text).ToString("00");
                int num = (int)System.Windows.Forms.MessageBox.Show("Đợt không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                this.cbbDate.Focus();
            }
            else
            {
                if (HandlingDataDBViewModel.Instance.CheckIzCB() != 1)
                {
                    int num1 = (int)System.Windows.Forms.MessageBox.Show("Vui lòng chuẩn bị dữ liệu trước khi tạo file đọc số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (HandlingDataDBViewModel.Instance.CheckIzDS() != 1)
                {
                    //DataTable tbl = new DataTable();
                    //int maxColumn = 80;
                    //for (int col = 0; col < maxColumn; col++)
                    //    tbl.Columns.Add(new DataColumn("Junk" + (col).ToString()));
                    List<DocSo> docsoList = new List<DocSo>();
                    foreach (var item in DanhMucFile)
                    {
                        docsoList.Clear();
                        TruyenDuLieu t = item as TruyenDuLieu;
                        if (t.X)
                        {
                            string str1 = t.DanhMucFile;
                            string str2 = str1.Split('_')[3];
                            string str3 = str1 + ".txt";
                            try
                            {
                                List<MyKhachHang> lst = HandlingDataDBViewModel.Instance.GetKhachHang_TaoFile(str2, date);

                                for (int index2 = 0; index2 < lst.Count; ++index2)
                                {
                                    MyKhachHang khachHang = lst.ElementAt(index2);
                                    string[] strArray = this.ChuanBiKH(khachHang.DanhBa, year.ToString(), month);
                                    DocSo docSo = new DocSo()
                                    {
                                        DocSoID = year + month + khachHang.DanhBa,
                                        DanhBa = khachHang.DanhBa,
                                        MLT1 = khachHang.MLT1,
                                        MLT2 = khachHang.MLT2,
                                        TenKH = khachHang.TenKH.Replace("\r\n", "").Replace("'", "").Replace("\n", "").Replace("|", "/"),
                                        SoNhaCu = khachHang.So.Replace("|", ""),
                                        SoNhaMoi = khachHang.SoMoi == null ? "" : khachHang.SoMoi,
                                        Duong = khachHang.Duong,
                                        SDT = khachHang.SDT == null ? "" : khachHang.SDT,
                                        GB = khachHang.GB.ToString(),
                                        DM = khachHang.DM.ToString(),
                                        Nam = year,
                                        Ky = month,
                                        Dot = date,
                                        May = str2,
                                        TBTT = int.Parse(strArray[2]),
                                        TamTinh = int.Parse(strArray[14]),
                                        CSCu = int.Parse(strArray[0]),
                                        //CSMoi = "",
                                        CodeCu = strArray[3],
                                        CodeMoi = "",
                                        TTDHNCu = strArray[1].Trim(),
                                        TTDHNMoi = "",
                                        TieuThuCu = int.Parse(strArray[4]),
                                        TieuThuMoi = 0,
                                        DenNgay = dpkDenNgay.SelectedDate,
                                        TienNuoc = 0,
                                        BVMT = 0,
                                        Thue = 0,
                                        TongTien = 0,
                                        SoThanCu = khachHang.SoThan == null ? "" : khachHang.SoThan,
                                        SoThanMoi = "",
                                        HieuCu = khachHang.Hieu == null ? "" : khachHang.Hieu,
                                        HieuMoi = "",
                                        CoCu = khachHang.Co == null ? "" : khachHang.Co,
                                        CoMoi = "",
                                        GiengCu = khachHang.Gieng == null ? "" : khachHang.Gieng,
                                        GiengMoi = "",
                                        Van1Cu = khachHang.Van1 == null ? "" : khachHang.Van1,
                                        Van1Moi = "",
                                        MVCu = khachHang.MaVach == null ? "" : khachHang.MaVach,
                                        MVMoi = "",
                                        ViTriCu = khachHang.ViTri == null ? "" : khachHang.ViTri,
                                        ViTriMoi = "",
                                        ChiThanCu = khachHang.ChiThan == null ? "" : khachHang.ChiThan,
                                        ChiThanMoi = "",
                                        ChiCoCu = khachHang.ChiCo == null ? "" : khachHang.ChiCo,
                                        ChiCoMoi = "",
                                        CapDoCu = khachHang.CapDo == null ? "" : khachHang.CapDo,
                                        CapDoMoi = "",
                                        CongDungCu = khachHang.CongDung == null ? "" : khachHang.CongDung,
                                        CongDungMoi = "",
                                        DMACu = khachHang.DMA == null ? "" : khachHang.DMA,
                                        DMAMoi = "",
                                        GhiChuKH = khachHang.GhiChuKH == null ? "" : khachHang.GhiChuKH,
                                        GhiChuDS = "",
                                        TODS = int.Parse(khachHang.ToID)
                                    };
                                    if (!strArray[5].Equals(""))
                                        docSo.TuNgay = DateTime.ParseExact(strArray[5], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                    else if (!khachHang.NgayGanCV.Equals(""))
                                        docSo.TuNgay = DateTime.ParseExact(khachHang.NgayGanCV, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                    else
                                    {
                                        docSo.TuNgay = dpkTuNgay.SelectedDate;
                                    }
                                    int insert = HandlingDataDBViewModel.Instance.InsertDocSo(docSo);
                                    if(insert == 0)
                                    {

                                    }
                                    //                    this.rowdtKH["NgayGan"] = dataRow["NgayGanCV"];
                                    //                    this.rowdtKH["Ky0"] = (object)strArray[15];
                                    //                    this.rowdtKH["Ky1"] = (object)strArray[6];
                                    //                    this.rowdtKH["CodeCu1"] = (object)strArray[7];
                                    //                    this.rowdtKH["ChiSoCu1"] = (object)strArray[8];
                                    //                    this.rowdtKH["TieuThuCu1"] = (object)strArray[9];
                                    //                    this.rowdtKH["Ky2"] = (object)strArray[10];
                                    //                    this.rowdtKH["CodeCu2"] = (object)strArray[11];
                                    //                    this.rowdtKH["ChiSoCu2"] = (object)strArray[12];
                                    //                    this.rowdtKH["TieuThuCu2"] = (object)strArray[13];
                                    //                    string str6 = dataRow["ToID"].ToString();
                                    //                    this.dtKH.Rows.Add(this.rowdtKH);
                                    //                    try
                                    //                    {
                                    //                        string sqlstatement = "insert into DocSo values('" + this.rowdtKH["Nam"] + this.rowdtKH["Ky"] + this.rowdtKH["DanhBa"] + "','" + this.rowdtKH["DanhBa"] + "','" + this.rowdtKH["MLT1"] + "','" + this.rowdtKH["MLT2"] + "','" + this.rowdtKH["SoNhaCu"] + "','" + this.rowdtKH["SoNhaMoi"] + "','" + this.rowdtKH["Duong"] + "','" + this.rowdtKH["SDT"] + "','" + this.rowdtKH["GB"] + "','" + this.rowdtKH["DM"] + "','" + this.rowdtKH["Nam"] + "','" + this.rowdtKH["Ky"] + "','" + this.rowdtKH["Dot"] + "','" + this.rowdtKH[GV.May] + "','" + this.rowdtKH["TBTT"] + "','" + this.rowdtKH["TamTinh"] + "','" + this.rowdtKH["CSCu"] + "','" + this.rowdtKH["CSMoi"] + "','" + this.rowdtKH["CodeCu"] + "','" + this.rowdtKH["CodeMoi"] + "','" + this.rowdtKH["TTDHNCu"] + "','" + this.rowdtKH["TTDHNMoi"] + "','" + this.rowdtKH["TieuThuCu"] + "','" + this.rowdtKH["TieuThuMoi"] + "',@TuNgay,@DenNgay,'" + this.rowdtKH["TienNuoc"] + "','" + this.rowdtKH["BVMT"] + "','" + this.rowdtKH["Thue"] + "','" + this.rowdtKH["TongTien"] + "','" + this.rowdtKH["SoThanCu"] + "','" + this.rowdtKH["SoThanMoi"] + "','" + this.rowdtKH["HieuCu"] + "','" + this.rowdtKH["HieuMoi"] + "','" + this.rowdtKH["CoCu"] + "','" + this.rowdtKH["CoMoi"] + "','" + this.rowdtKH["GiengCu"] + "','" + this.rowdtKH["GiengMoi"] + "','" + this.rowdtKH["Van1Cu"] + "','" + this.rowdtKH["Van1Moi"] + "','" + this.rowdtKH["MVCu"] + "','" + this.rowdtKH["MVMoi"] + "','" + this.rowdtKH["ViTriCu"] + "','" + this.rowdtKH["ViTriMoi"] + "','" + this.rowdtKH["ChiThanCu"] + "','" + this.rowdtKH["ChiThanMoi"] + "','" + this.rowdtKH["ChiCoCu"] + "','" + this.rowdtKH["ChiCoMoi"] + "','" + this.rowdtKH["CapDoCu"] + "','" + this.rowdtKH["CapDoMoi"] + "','" + this.rowdtKH["CongDungCu"] + "','" + this.rowdtKH["CongDungMoi"] + "','" + this.rowdtKH["DMACu"] + "','" + this.rowdtKH["DMAMoi"] + "','" + this.rowdtKH["GhiChuKH"].ToString().Replace('|', ' ') + "','" + this.rowdtKH["GhiChuDS"].ToString().Replace('|', ' ') + "',@EmptyValue,@EmptyValue,@EmptyValue,@EmptyValue,@EmptyValue,@EmptyValue,@EmptyValue,@EmptyValue,@EmptyValue,'" + str6 + "')";
                                    //                        pcData2.GetExecuteNonQuerry(sqlstatement);
                                    //                    }
                                    //                    catch (SqlException ex)
                                    //                    {
                                    //                        string sqlstatement = "Update DocSo set DanhBa = '" + this.rowdtKH["DanhBa"] + "',MLT1 = '" + this.rowdtKH["MLT1"] + "',MLT2 = '" + this.rowdtKH["MLT2"] + "',SoNhaCu = '" + this.rowdtKH["SoNhaCu"] + "',SoNhaMoi = '" + this.rowdtKH["SoNhaMoi"] + "',Duong = '" + this.rowdtKH["Duong"] + "',SDT = '" + this.rowdtKH["SDT"] + "',GB = '" + this.rowdtKH["GB"] + "',DM = '" + this.rowdtKH["DM"] + "',Nam = '" + this.rowdtKH["Nam"] + "',Ky = '" + this.rowdtKH["Ky"] + "',Dot = '" + this.rowdtKH["Dot"] + "',May = '" + this.rowdtKH[GV.May] + "',ToDS = '" + str6 + "',CSCu = '" + this.rowdtKH["CSCu"] + "',CodeCu = '" + this.rowdtKH["CodeCu"] + "',TTDHNCu = '" + this.rowdtKH["TTDHNCu"] + "',TieuThuCu = '" + this.rowdtKH["TieuThuCu"] + "',SoThanCu = '" + this.rowdtKH["SoThanCu"] + "',SoThanMoi = '" + this.rowdtKH["SoThanMoi"] + "',HieuCu = '" + this.rowdtKH["HieuCu"] + "',HieuMoi = '" + this.rowdtKH["HieuMoi"] + "',CoCu = '" + this.rowdtKH["CoCu"] + "',CoMoi = '" + this.rowdtKH["CoMoi"] + "',GiengCu = '" + this.rowdtKH["GiengCu"] + "',GiengMoi = '" + this.rowdtKH["GiengMoi"] + "',Van1Cu = '" + this.rowdtKH["Van1Cu"] + "',Van1Moi = '" + this.rowdtKH["Van1Moi"] + "',MVCu = '" + this.rowdtKH["MVCu"] + "',MVMoi = '" + this.rowdtKH["MVMoi"] + "',ViTriCu = '" + this.rowdtKH["ViTriCu"] + "',ViTriMoi = '" + this.rowdtKH["ViTriMoi"] + "',ChiThanCu = '" + this.rowdtKH["ChiThanCu"] + "',ChiThanMoi = '" + this.rowdtKH["ChiThanMoi"] + "',ChiCoCu = '" + this.rowdtKH["ChiCoCu"] + "',ChiCoMoi = '" + this.rowdtKH["ChiCoMoi"] + "',CapDoCu = '" + this.rowdtKH["CapDoCu"] + "',CapDoMoi = '" + this.rowdtKH["CapDoMoi"] + "',CongDungCu = '" + this.rowdtKH["CongDungCu"] + "',CongDungMoi = '" + this.rowdtKH["CongDungMoi"] + "',DMACu = '" + this.rowdtKH["DMACu"] + "',  DMAMoi = '" + this.rowdtKH["DMAMoi"] + "', StaCapNhat = @EmptyValue where DocSoID = '" + this.rowdtKH["Nam"] + this.rowdtKH["Ky"] + this.rowdtKH["DanhBa"] + "'";
                                    //                        pcData2.GetExecuteNonQuerry(sqlstatement);
                                    //                    }
                                    //                    if (index2 < count - 1)
                                    //                    {
                                    //                        if (index2 % 10 == 0)
                                    //                        {
                                    //                            this.toolStripProgressBar.Value = index2;
                                    //                            this.toolStripStatusLabel.Text = str3 + ": " + (object)index2 + "/" + (object)count;
                                    //                            GC.Collect();
                                    //                            GC.WaitForPendingFinalizers();
                                    //                            this.Refresh();
                                    //                        }
                                    //                    }
                                    //                    else
                                    //                    {
                                    //                        this.toolStripProgressBar.Value = count;
                                    //                        this.toolStripStatusLabel.Text = str3 + ": " + (object)count + "/" + (object)count;
                                    //                        GC.Collect();
                                    //                        GC.WaitForPendingFinalizers();
                                    //                        this.Refresh();
                                    //                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                int num2 = (int)System.Windows.Forms.MessageBox.Show("btnTaoFile_Click: " + ex.Message + docsoList.ElementAt(0).ToString());
                                docsoList.Clear();
                            }
                            //            this.Refresh();
                            //            string path = GV.ToHH + "\\" + str3;
                            //            StreamWriter sw = new StreamWriter(path, false);
                            //            if (this.dtKH.Rows.Count == 0)
                            //            {
                            //                File.Delete(path);
                            //                break;
                            //            }
                            //            for (int index2 = 0; index2 < this.dtKH.Rows.Count; ++index2)
                            //            {
                            //                object[] itemArray = this.dtKH.Rows[index2].ItemArray;
                            //                string str4 = itemArray[0].ToString();
                            //                for (int index3 = 1; index3 < itemArray.Length; ++index3)
                            //                    str4 = str4 + (object)'|' + itemArray[index3].ToString();
                            //                sw.WriteLine(str4);
                            //            }
                            //            UTIex.ReleaseSW(ref sw);
                            //            GC.Collect();
                            //            GC.WaitForPendingFinalizers();
                            //            this.toolStripProgressBar.Visible = false;
                            //            this.toolStripProgressBar.Value = 0;
                            //            this.toolStripStatusLabel.Visible = false;
                            //            this.toolStripStatusLabel.Text = "";
                            //            this.GetDanhMucFileDaTao();
                            //            Thread.Sleep(300);
                        }
                        //        this.Refresh();
                        //        GC.Collect();
                        //        GC.WaitForPendingFinalizers();
                    }
                }
                else
                {
                    int num3 = (int)System.Windows.Forms.MessageBox.Show("Dữ liệu đã chuyển thương vụ, không thể tạo file.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnUnSelect_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dtgridMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void columnHeader_Click(object sender, RoutedEventArgs e)
        {

        }
        #region year, month, date
        private void cbbDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbDate.SelectedValue != null)
            {
                date = cbbDate.SelectedValue.ToString();
                //dpkTuNgay.SelectedDate = HandlingDataDBViewModel.Instance.GetNgayDocSo()
            }
        }

        private void cbbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbMonth.SelectedValue != null)
            {
                month = cbbMonth.SelectedValue.ToString();
                //cbbDate.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctDateServer(year, month);
            }
        }

        private void cbbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbYear.SelectedValue != null)
            {
                year = Int16.Parse(cbbYear.SelectedValue.ToString());
                //cbbMonth.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctMonthServer(year);
            }
        }
        #endregion
    }
}
