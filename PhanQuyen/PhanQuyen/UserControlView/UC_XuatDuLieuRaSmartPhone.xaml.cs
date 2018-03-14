using Model;
using System;
using System.Collections.Generic;
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
            }
            GetDanhMucFileTheoTo();

        }
        private void GetDanhMucFileTheoTo()
        {
            this.dtgridMain.ItemsSource = null;
            dtgridMain.ItemsSource = HandlingDataDBViewModel.Instance.GetDanhMucFileTheoTo(year, month, date, group);
        }
        private void GetFileCheck()
        {
            //for (int index = 0; index < this.dgv.Rows.Count; ++index)
            //{
            //    if (this.dgv.Rows[index].Cells[4].Value.ToString() == "")
            //        this.dgv.Rows[index].Cells[0].Value = (object)true;
            //}
        }

        private void GetDanhMucFileDaTao()
        {
            //foreach (FileSystemInfo file in new DirectoryInfo(GV.ToHH).GetFiles())
            //{
            //    object obj = (object)file.Name.Replace(".txt", "");
            //    for (int index = 0; index < this.dgv.Rows.Count; ++index)
            //    {
            //        if (this.dgv.Rows[index].Cells[2].Value.Equals(obj))
            //        {
            //            this.dgv.Rows[index].Cells[4].Value = obj;
            //            this.dgv.Rows[index].Cells[0].Value = (object)false;
            //            break;
            //        }
            //    }
            //}
        }

        //private string[] ChuanBiKH(object danhba, string nam, string ky)
        //{
        //    string str1 = nam + ky + danhba;
        //    string[] strArray = new string[16];
        //    int num1 = 0;
        //    string sqlStatement = "select top 13 Nam, Ky, Convert(varchar,DenNgay,103) DenNgay, CSMoi, CodeMoi, TieuThuMoi, TTDHNMOI,TBTT from DocSo where DanhBa = '" + danhba + "' and DocSoID < '" + str1 + "' order by DocSoID desc";
        //    DataTable dataTable = pc.GetDataTable(sqlStatement);
        //    int count = dataTable.Rows.Count;
        //    if (count == 0)
        //    {
        //        strArray[3] = "M0";
        //        strArray[0] = "0";
        //        strArray[4] = "0";
        //        strArray[2] = "0";
        //        strArray[1] = "GAN MOI";
        //        strArray[5] = "";
        //        strArray[14] = "0";
        //        strArray[15] = "";
        //    }
        //    else if (count >= 1)
        //    {
        //        string str2 = dataTable.Rows[0]["CodeMoi"].ToString();
        //        string str3 = this.ChuyenCode4(dataTable.Rows[0]["TTDHNMoi"].ToString());
        //        strArray[3] = !(str3 == "") ? str3 : dataTable.Rows[0]["CodeMoi"].ToString();
        //        switch (str2)
        //        {
        //            case "60":
        //            case "61":
        //            case "62":
        //            case "63":
        //            case "64":
        //            case "65":
        //            case "66":
        //            case "F1":
        //            case "F2":
        //            case "F3":
        //            case "F4":
        //            case "N":
        //                num1 = int.Parse(dataTable.Rows[0]["TBTT"].ToString());
        //                int num2 = int.Parse(dataTable.Rows[0]["TieuThuMoi"].ToString());
        //                for (int index = 1; index < dataTable.Rows.Count; ++index)
        //                {
        //                    string str4 = dataTable.Rows[index]["CodeMoi"].ToString().Substring(0, 1);
        //                    if (!(str4 == "4") && !(str4 == "5") && !(str4 == "8") && !(str4 == "M"))
        //                        num2 += int.Parse(dataTable.Rows[index]["TieuThuMoi"].ToString());
        //                    else
        //                        break;
        //                }
        //                strArray[14] = num2.ToString();
        //                strArray[0] = (int.Parse(dataTable.Rows[0]["CSMoi"].ToString()) + int.Parse(num2.ToString())).ToString();
        //                break;
        //            case "K":
        //                int num3 = int.Parse(dataTable.Rows[0]["TieuThuMoi"].ToString());
        //                for (int index = 1; index < dataTable.Rows.Count; ++index)
        //                {
        //                    string str4 = dataTable.Rows[index]["CodeMoi"].ToString().Substring(0, 1);
        //                    if (!(str4 == "4") && !(str4 == "5") && !(str4 == "8") && !(str4 == "M"))
        //                        num3 += int.Parse(dataTable.Rows[index]["TieuThuMoi"].ToString());
        //                    else
        //                        break;
        //                }
        //                strArray[14] = num3.ToString();
        //                strArray[0] = int.Parse(dataTable.Rows[0]["CSMoi"].ToString()).ToString();
        //                break;
        //            default:
        //                strArray[14] = "0";
        //                strArray[0] = dataTable.Rows[0]["CSMoi"].ToString();
        //                break;
        //        }
        //        strArray[0] = dataTable.Rows[0]["CSMoi"].ToString();
        //        strArray[4] = dataTable.Rows[0]["TieuThuMoi"].ToString();
        //        strArray[1] = dataTable.Rows[0]["TTDHNMoi"].ToString();
        //        double num4 = 0.0;
        //        if (str2 == "60" || str2 == "61" || (str2 == "62" || str2 == "63") || (str2 == "64" || str2 == "65" || (str2 == "66" || str2 == "F1")) || (str2 == "F2" || str2 == "F3" || (str2 == "F4" || str2 == "80")) || str2 == "N")
        //        {
        //            double num5 = (double)num1;
        //            strArray[2] = num5.ToString();
        //        }
        //        else
        //        {
        //            int index;
        //            for (index = 0; index < count && index < 3; ++index)
        //            {
        //                string s1 = dataTable.Rows[index]["TieuThuMoi"].ToString();
        //                if (s1 == null || s1 == "")
        //                {
        //                    string s2 = "0";
        //                    num4 += double.Parse(s2);
        //                }
        //                else
        //                    num4 += double.Parse(s1);
        //            }
        //            double num5 = Math.Round(num4 / (double)index);
        //            strArray[2] = num5.ToString();
        //        }
        //        strArray[5] = dataTable.Rows[0]["DenNgay"].ToString();
        //        strArray[15] = dataTable.Rows[0]["Ky"].ToString() + "/" + dataTable.Rows[0]["Nam"].ToString().Substring(2);
        //        if (count >= 2)
        //        {
        //            strArray[6] = dataTable.Rows[1]["Ky"].ToString() + "/" + dataTable.Rows[1]["Nam"].ToString().Substring(2);
        //            strArray[8] = dataTable.Rows[1]["CSMoi"].ToString();
        //            strArray[9] = dataTable.Rows[1]["TieuThuMoi"].ToString();
        //            string str4 = this.ChuyenCode4(dataTable.Rows[1]["TTDHNMoi"].ToString());
        //            strArray[7] = !(str4 == "") ? str4 : dataTable.Rows[1]["CodeMoi"].ToString();
        //            if (count >= 3)
        //            {
        //                strArray[10] = dataTable.Rows[2]["Ky"].ToString() + "/" + dataTable.Rows[2]["Nam"].ToString().Substring(2);
        //                strArray[11] = dataTable.Rows[2]["CodeMoi"].ToString();
        //                strArray[12] = dataTable.Rows[2]["CSMoi"].ToString();
        //                strArray[13] = dataTable.Rows[2]["TieuThuMoi"].ToString();
        //                string str5 = this.ChuyenCode4(dataTable.Rows[2]["TTDHNMoi"].ToString());
        //                strArray[11] = !(str5 == "") ? str5 : dataTable.Rows[2]["CodeMoi"].ToString();
        //            }
        //        }
        //    }
        //    UTIex.ReleasePCDATA(ref pc);
        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();
        //    return strArray;
        //}
        public static bool CheckDot(string text, ItemCollection items)
        {
            try
            {
                string str = int.Parse(text).ToString("00");
                if (items.Contains((object)str))
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void btnGetData_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckDot(cbbDate.Text, cbbDate.Items))
            {
                cbbDate.Text = int.Parse(cbbDate.Text).ToString("00");
                int num = (int)System.Windows.Forms.MessageBox.Show("Đợt không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                this.cbbDate.Focus();
            }
            else
            {
                //if (HandlingDataDBViewModel.Instance.CheckIzCB() != 1)
                //{
                //    int num1 = (int)System.Windows.Forms.MessageBox.Show("Vui lòng chuẩn bị dữ liệu trước khi tạo file đọc số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //}
                //else if (HandlingDataDBViewModel.Instance.CheckIzDS() != 1)
                //{
                //    for (int index1 = 0; index1 < this.dgv.Rows.Count; ++index1)
                //    {
                //        this.dtKH.Rows.Clear();
                //        if ((bool)this.dgv.Rows[index1].Cells[0].Value)
                //        {
                //            string str1 = this.dgv.Rows[index1].Cells[2].Value.ToString();
                //            string str2 = str1.Split('_')[3];
                //            string str3 = str1 + ".txt";
                //            pcData1 = (PCData)null;
                //            DataRow dataRow = (DataRow)null;
                //            try
                //            {
                //                PCData pcData2 = new PCData(GV.connString);
                //                string sqlStatement3 = "select *, Convert(varchar,NgayGan,103) NgayGanCV from KhachHang k inner join MayDS m on k.May = m.May  where k.May = '" + str2 + "' and Dot ='" + this.cbbDot.Text + "' and HieuLuc ='1' order by MLT2";
                //                DataTable dataTable = pcData2.GetDataTable(sqlStatement3);
                //                SqlParameter sqlParameter1 = new SqlParameter("@TuNgay", SqlDbType.Date);
                //                pcData2.PCCommand.Parameters.Add(sqlParameter1);
                //                SqlParameter sqlParameter2 = new SqlParameter("@DenNgay", (object)this.dtpDenNgay.Value);
                //                pcData2.PCCommand.Parameters.Add(sqlParameter2);
                //                SqlParameter sqlParameter3 = new SqlParameter("@EmptyValue", (object)string.Empty);
                //                pcData2.PCCommand.Parameters.Add(sqlParameter3);
                //                int count = dataTable.Rows.Count;
                //                this.toolStripProgressBar.Visible = true;
                //                this.toolStripProgressBar.Maximum = count;
                //                this.toolStripProgressBar.Minimum = 0;
                //                this.toolStripStatusLabel.Visible = true;
                //                this.Refresh();
                //                DateTime today = DateTime.Today;
                //                string str4 = today.ToString("dd/MM/yyyy");
                //                for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
                //                {
                //                    this.rowdtKH = this.dtKH.NewRow();
                //                    dataRow = dataTable.Rows[index2];
                //                    string[] strArray = this.ChuanBiKH(dataRow["DanhBa"], this.cbbNam.Text, this.cbbKy.Text);
                //                    this.rowdtKH["DanhBa"] = dataRow["DanhBa"];
                //                    this.rowdtKH["MLT1"] = dataRow["MLT1"];
                //                    this.rowdtKH["MLT2"] = dataRow["MLT2"];
                //                    this.rowdtKH["TenKH"] = dataRow["TenKH"];
                //                    this.rowdtKH["SoNhaCu"] = dataRow["So"];
                //                    this.rowdtKH["SoNhaMoi"] = dataRow["SoMoi"];
                //                    this.rowdtKH["Duong"] = dataRow["Duong"];
                //                    this.rowdtKH["SDT"] = dataRow["SDT"];
                //                    this.rowdtKH["GB"] = dataRow["GB"];
                //                    this.rowdtKH["DM"] = dataRow["DM"];
                //                    this.rowdtKH["SH"] = dataRow["SH"];
                //                    this.rowdtKH["SX"] = dataRow["SX"];
                //                    this.rowdtKH["DV"] = dataRow["DV"];
                //                    this.rowdtKH["HC"] = dataRow["HC"];
                //                    this.rowdtKH["Nam"] = (object)this.cbbNam.Text;
                //                    this.rowdtKH["Ky"] = (object)this.cbbKy.Text;
                //                    this.rowdtKH["Dot"] = (object)this.cbbDot.Text;
                //                    this.rowdtKH[GV.May] = (object)str2;
                //                    this.rowdtKH["TBTT"] = (object)strArray[2];
                //                    this.rowdtKH["TamTinh"] = (object)strArray[14];
                //                    this.rowdtKH["CSCu"] = (object)strArray[0];
                //                    this.rowdtKH["CSMoi"] = (object)"";
                //                    this.rowdtKH["CodeCu"] = (object)strArray[3];
                //                    this.rowdtKH["CodeMoi"] = (object)"";
                //                    this.rowdtKH["TTDHNCu"] = (object)strArray[1].Trim();
                //                    this.rowdtKH["TTDHNMoi"] = (object)"";
                //                    this.rowdtKH["TieuThuCu"] = (object)strArray[4];
                //                    this.rowdtKH["TieuThuMoi"] = (object)"";
                //                    if (!strArray[5].Equals(""))
                //                    {
                //                        this.rowdtKH["TuNgay"] = (object)strArray[5];
                //                        string str5 = this.rowdtKH["TuNgay"].ToString();
                //                        sqlParameter1.Value = (object)new DateTime(int.Parse(str5.Substring(6)), int.Parse(str5.Substring(3, 2)), int.Parse(str5.Substring(0, 2)));
                //                    }
                //                    else if (!dataRow["NgayGanCV"].ToString().Equals(""))
                //                    {
                //                        this.rowdtKH["TuNgay"] = dataRow["NgayGanCV"];
                //                        string str5 = this.rowdtKH["TuNgay"].ToString();
                //                        sqlParameter1.Value = (object)new DateTime(int.Parse(str5.Substring(6)), int.Parse(str5.Substring(3, 2)), int.Parse(str5.Substring(0, 2)));
                //                    }
                //                    else
                //                    {
                //                        DataRow rowdtKh = this.rowdtKH;
                //                        string index3 = "TuNgay";
                //                        today = this.dtpTuNgay.Value;
                //                        string str5 = today.ToString("dd/MM/yyyy");
                //                        rowdtKh[index3] = (object)str5;
                //                        sqlParameter1.Value = (object)this.dtpTuNgay.Value;
                //                    }
                //                    this.rowdtKH["DenNgay"] = (object)str4;
                //                    this.rowdtKH["TienNuoc"] = (object)"";
                //                    this.rowdtKH["BVMT"] = (object)"";
                //                    this.rowdtKH["Thue"] = (object)"";
                //                    this.rowdtKH["TongTien"] = (object)"";
                //                    this.rowdtKH["NgayGan"] = dataRow["NgayGanCV"];
                //                    this.rowdtKH["SoThanCu"] = dataRow["SoThan"];
                //                    this.rowdtKH["SoThanMoi"] = (object)"";
                //                    this.rowdtKH["HieuCu"] = dataRow["Hieu"];
                //                    this.rowdtKH["HieuMoi"] = (object)"";
                //                    this.rowdtKH["CoCu"] = dataRow["Co"];
                //                    this.rowdtKH["CoMoi"] = (object)"";
                //                    this.rowdtKH["GiengCu"] = dataRow["Gieng"];
                //                    this.rowdtKH["GiengMoi"] = (object)"";
                //                    this.rowdtKH["Van1Cu"] = dataRow["Van1"];
                //                    this.rowdtKH["Van1Moi"] = (object)"";
                //                    this.rowdtKH["MVCu"] = dataRow["MaVach"];
                //                    this.rowdtKH["MVMoi"] = (object)"";
                //                    this.rowdtKH["ViTriCu"] = (object)dataRow["ViTri"].ToString().Trim();
                //                    this.rowdtKH["ViTriMoi"] = (object)"";
                //                    this.rowdtKH["ChiThanCu"] = dataRow["ChiThan"];
                //                    this.rowdtKH["ChiThanMoi"] = (object)"";
                //                    this.rowdtKH["ChiCoCu"] = dataRow["ChiCo"];
                //                    this.rowdtKH["ChiCoMoi"] = (object)"";
                //                    this.rowdtKH["CapDoCu"] = dataRow["CapDo"];
                //                    this.rowdtKH["CapDoMoi"] = (object)"";
                //                    this.rowdtKH["CongDungCu"] = (object)dataRow["CongDung"].ToString().Trim();
                //                    this.rowdtKH["CongDungMoi"] = (object)"";
                //                    this.rowdtKH["DMACu"] = dataRow["DMA"];
                //                    this.rowdtKH["DMAMoi"] = (object)"";
                //                    this.rowdtKH["GhiChuKH"] = (object)dataRow["GhiChuKH"].ToString().Replace('|', ' ');
                //                    this.rowdtKH["GhiChuDS"] = (object)"";
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
                //                }
                //                GC.Collect();
                //                GC.WaitForPendingFinalizers();
                //            }
                //            catch (Exception ex)
                //            {
                //                int num2 = (int)MessageBox.Show("btnTaoFile_Click: " + ex.Message + dataRow.ItemArray[0].ToString());
                //                this.dtKH.Rows.Clear();
                //            }
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
                //        }
                //        this.Refresh();
                //        GC.Collect();
                //        GC.WaitForPendingFinalizers();
                //    }
                //}
                //else
                //{
                //    int num3 = (int)MessageBox.Show("Dữ liệu đã chuyển thương vụ, không thể tạo file.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}
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
        #region year,month,date
        private void cbbDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbDate.SelectedValue != null)
                date = cbbDate.SelectedValue.ToString();
        }

        private void cbbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbMonth.SelectedValue != null)
            {
                month = cbbMonth.SelectedValue.ToString();
                cbbDate.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctDateServer(year, month);
            }
        }



        private void cbbYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbYear.SelectedValue != null)
            {
                year = Int16.Parse(cbbYear.SelectedValue.ToString());
                cbbMonth.ItemsSource = HandlingDataDBViewModel.Instance.getDistinctMonthServer(year);
            }
        }
        #endregion
    }
}
