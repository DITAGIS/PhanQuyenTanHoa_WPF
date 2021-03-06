﻿using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace ViewModel
{
    public class HandlingDataDBViewModel
    {


        private static HandlingDataDBViewModel _instance;
        private DataClassesLocalDataContext localContext;
        private DataClasses_thanleDataContext serverContext;

        private const String ALL = "Tất cả";

        private const String TTKH_COLUMN_TENKH = "TenKH";
        private const String TTKH_COLUMN_HIEU = "Hieu";
        private const String TTKH_COLUMN_CO = "Co";
        private const String TTKH_COLUMN_SDT = "SDT";
        private const String TTKH_COLUMN_DUONG = "Duong";



        private const String TTKH_COLUMN_SOTHAN = "SoThan";
        private const String TTKH_COLUMN_MLT1 = "MLT1";


        private const String TTKH_COLUMN_HOPDONG = "HopDong";
        private const String TTKH_COLUMN_DANHBA = "DanhBa";
        private const String TTKH_COLUMN_GB = "GB";
        private const String TTKH_COLUMN_DM = "DM";
        private const String TTKH_COLUMN_KY = "Ky";//todo
        private const String TTKH_COLUMN_NAM = "Nam";//todo
        private const String TTKH_COLUMN_NGAYDOC = "DenNgay";
        private const String TTKH_COLUMN_CODE = "Code";
        private const String TTKH_COLUMN_CSMOI = "CSMoi";
        private const String TTKH_COLUMN_TIEUTHU = "TieuThu";

        private const String TTKH_COLUMN_GHICHU = "GhiChuKH";//todo

        private const String DC_COLUMN_TODS = "TODS";
        private const String DC_COLUMN_KY = "Ky";
        private const String DC_COLUMN_DOT = "Dot";
        private const String DC_COLUMN_MAY = "May";
        private const String DC_COLUMN_MyUserNAME = "DM";
        private const String DC_COLUMN_STT = "DocSoID";


        private const String DC_COLUMN_MLT = "MLT2";
        private const String DC_COLUMN_SDT = "SDT";
        private const String DC_COLUMN_DANHBA = "DanhBa";
        private const String DC_COLUMN_TENKH = "TenKH";
        private const String DC_COLUMN_DUONG = "Duong";
        private const String DC_COLUMN_SOTHANCU = "SoThanCu";
        private const String DC_COLUMN_CODEMOI = "CodeMoi";
        private const String DC_COLUMN_CSCU = "CSCu";
        private const String DC_COLUMN_CSMOI = "CSMoi";
        private const String DC_COLUMN_TIEUTHUMOI = "TieuThuMoi";
        private const String DC_COLUMN_TBTT = "TBTT";

        private const String DHNTRENMANG_COLUMN_TOID = "ToID";
        private const String DHNTRENMANG_COLUMN_MAY = "May";
        private const String DHNTRENMANG_COLUMN_KY = "KY";
        private const String DHNTRENMANG_COLUMN_DOT = "Dot";
        private const String DHNTRENMANG_COLUMN_NAM = "NAM";
        private const String DHNTRENMANG_COLUMN_TITLE = "TITLE";
        private const String DHNTRENMANG_COLUMN_TITLE1 = "TITLE1";
        private const String DHNTRENMANG_COLUMN_HIEU = "Hieu";
        private const String DHNTRENMANG_COLUMN_CO = "Co";
        private const String DHNTRENMANG_COLUMN_DANHBA = "DANHBA";

        private const String TKSAUDOCSO_COLUMN_TOID = "TOID";
        private const String TKSAUDOCSO_COLUMN_KY = "Ky";
        private const String TKSAUDOCSO_COLUMN_DOT = "Dot";
        private const String TKSAUDOCSO_COLUMN_MAY = "May";
        private const String TKSAUDOCSO_COLUMN_CODEMOI = "CodeMoi";
        private const String TKSAUDOCSO_COLUMN_DANHBA = "DanhBa";
        private const String TKSAUDOCSO_COLUMN_TIEUTHUMOI = "TieuThuMoi";

        private const String TKDHNTHEODOTSO_COLUMN_TOID = "TODS";
        private const String TKDHNTHEODOTSO_COLUMN_DOT = "Dot";
        private const String TKDHNTHEODOTSO_COLUMN_MAY = "May";


        private const String TKDHNTHEODOTSO_COLUMN_DANHBA = "CSCu";

        private const String BCTONGHOP_COLUMN_NAM = "Nam";
        private const String BCTONGHOP_COLUMN_KY = "Ky";
        private const String BCTONGHOP_COLUMN_DOT = "Dot";
        private const String BCTONGHOP_COLUMN_TOID = "Toid";
        private const String BCTONGHOP_COLUMN_DANHBA = "DanhBa";
        private const String BCTONGHOP_COLUMN_TIEUTHUMOI = "TieuThuMoi";
        private const String BCTONGHOP_COLUMN_SOLANGHI = "SOLANGHI";
        private const String BCTONGHOP_COLUMN_TENPHUONG = "TenPhuong";
        private const String BCTONGHOP_COLUMN_CO = "Co";
        private const String BCTONGHOP_COLUMN_HIEU = "Hieu";
        private const String BCTONGHOP_COLUMN_NHANVIENID = "NhanVienID";
        string pattern = "dd/MM/yyyy";
        public static HandlingDataDBViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HandlingDataDBViewModel();
                return _instance;
            }
        }


        public DataTable GetCapNhatHoaDon(string month, int year)
        {

            try
            {
                ConnectionViewModel.Instance.Connect();

                string query = "SELECT Dot , COUNT(DANHBA) as SoDanhBa, SUM(TieuThu) as SoTieuThu FROM HoaDon WHERE hoadonid like '" + year + month + "%' GROUP BY DOT ORDER BY DOT";
                //data = serverContext.ExecuteQuery<CapNhatHoaDon>(query).ToList();
                DataTable table = ConnectionViewModel.Instance.GetDataTable(query);

                ConnectionViewModel.Instance.DisConnect();
                return table;
            }
            catch
            {
                return new DataTable();
            }

        }

        public int ChuanBiDocSo(int nam, string ky, string dot)
        {
            var query = "update BillState set izCB ='1' where BillID ='" + nam + ky + dot + "'";
            return serverContext.ExecuteQuery<int>(query).ToList().Count;
        }
        public int CheckIzCB()
        {
            var query = "select count(*) from BillState where izCB = '1' and BillID ='" + MyUser.Instance.Year + MyUser.Instance.Month + MyUser.Instance.Date + "'";
            return serverContext.ExecuteQuery<int>(query).First();
        }
        public int CheckIzDS(int nam, string ky, string dot)
        {
            var query = "select count(*) from BillState where izDS = '1' and BillID ='" + nam + ky + dot + "'";
            return serverContext.ExecuteQuery<int>(query).First();
        }

        private HandlingDataDBViewModel()
        {
            localContext = new DataClassesLocalDataContext();
            serverContext = new DataClasses_thanleDataContext(ConnectionViewModel.Instance.ConnectionString);
        }
        public void DeleteBienDong(int nam, string ky, string dot)
        {
            try
            {
                var BienDongs = (from x in serverContext.BienDongs
                                 where x.BienDongID.StartsWith(nam + ky + "%") && x.Dot == dot
                                 select x).ToList();
                foreach (var item in BienDongs)
                {
                    serverContext.BienDongs.DeleteOnSubmit(item);
                }
                serverContext.SubmitChanges();
            }
            catch
            {

            }
        }
        public int UpdateDocSo(DocSo docSo)
        {
            ConnectionViewModel.Instance.Connect();
            StringBuilder builder = new StringBuilder();
            builder.Append("Update DocSo set DanhBa = '" + docSo.DanhBa + "',MLT1 = '" + docSo.MLT1 + "',MLT2 = '" + docSo.MLT2);
            builder.Append("',SoNhaCu = '" + docSo.SoNhaCu + "',SoNhaMoi = '" + docSo.SoNhaMoi + "',Duong = '" + docSo.Duong + "',SDT = '" + docSo.SDT);
            builder.Append("',GB = '" + docSo.GB + "',DM = '" + docSo.DM + "',Nam = '" + docSo.Nam + "',Ky = '" + docSo.Ky + "',Dot = '" + docSo.Dot);
            builder.Append("',May = '" + docSo.May + "',ToDS = '" + docSo.TODS + "',CSCu = '" + docSo.CSCu + "',CodeCu = '" + docSo.CodeCu + "',TTDHNCu = '" + docSo.TTDHNCu);
            builder.Append("',TieuThuCu = '" + docSo.TieuThuCu + "',SoThanCu = '" + docSo.SoThanCu + "',SoThanMoi = '" + docSo.SoThanMoi + "',HieuCu = '" + docSo.HieuCu);
            builder.Append("',HieuMoi = '" + docSo.HieuMoi + "',CoCu = '" + docSo.CoCu + "',CoMoi = '" + docSo.CoMoi + "',GiengCu = '" + docSo.GiengCu);
            builder.Append("',GiengMoi = '" + docSo.GiengMoi + "',Van1Cu = '" + docSo.Van1Cu + "',Van1Moi = '" + docSo.Van1Moi + "',MVCu = '" + docSo.MVCu + "',MVMoi = '" + docSo.MVMoi);
            builder.Append("',ViTriCu = '" + docSo.ViTriCu + "',ViTriMoi = '" + docSo.ViTriMoi + "',ChiThanCu = '" + docSo.ChiThanCu + "',ChiThanMoi = '" + docSo.ChiThanMoi);
            builder.Append("',ChiCoCu = '" + docSo.ChiCoCu + "',ChiCoMoi = '" + docSo.ChiCoMoi + "',CapDoCu = '" + docSo.CapDoCu + "',CapDoMoi = '" + docSo.CapDoMoi);
            builder.Append("',CongDungCu = '" + docSo.CongDungCu + "',CongDungMoi = '" + docSo.CongDungMoi + "',DMACu = '" + docSo.DMACu + "',  DMAMoi = '" + docSo.DMAMoi);
            builder.Append("', StaCapNhat = '' where DocSoID = '" + docSo.DocSoID + "'");

            int value = ConnectionViewModel.Instance.GetExecuteNonQuerry(builder.ToString());
            ConnectionViewModel.Instance.DisConnect();
            //var value = serverContext.ExecuteQuery<int>(builder.ToString()).ToList();
            return value;
        }
        public int InsertDocSo(DocSo docSo)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into DocSo values('" + docSo.DocSoID + "','" + docSo.DanhBa + "','" + docSo.MLT1 + "','" + docSo.MLT2 + "','");
            builder.Append(docSo.SoNhaCu + "','" + docSo.SoNhaMoi + "','" + docSo.Duong + "','" + docSo.SDT + "','" + docSo.GB + "','" + docSo.DM + "','" + docSo.Nam + "','");
            builder.Append(docSo.Ky + "','" + docSo.Dot + "','" + docSo.May + "','" + docSo.TBTT + "','" + docSo.TamTinh + "','" + docSo.CSCu + "','" + /*docSo.CSMoi +*/ "','");
            builder.Append(docSo.CodeCu + "','" + /*docSo.CodeMoi +*/ "','" + docSo.TTDHNCu + "','" + /*docSo.TTDHNMoi +*/ "','" + docSo.TieuThuCu + "','" + /*docSo.TieuThuMoi +*/ "','");
            builder.Append(docSo.TuNgay + "','" + docSo.DenNgay + "','" + docSo.TienNuoc + "','" + docSo.BVMT + "','" + docSo.Thue + "','" + docSo.TongTien + "','");
            builder.Append(docSo.SoThanCu + "','" + docSo.SoThanMoi + "','" + docSo.HieuCu + "','" + docSo.HieuMoi + "','" + docSo.CoCu + "','" + docSo.CoMoi + "','");
            builder.Append(docSo.GiengCu + "','" + docSo.GiengMoi + "','" + docSo.Van1Cu + "','" + docSo.Van1Moi + "','" + docSo.MVCu + "','" + docSo.MVMoi + "','");
            builder.Append(docSo.ChiCoCu + "','" + docSo.ChiCoMoi + "','" + docSo.ChiThanCu + "','" + docSo.ChiThanMoi + "','" + docSo.ViTriCu + "','" + docSo.ViTriMoi + "','");
            builder.Append(docSo.CapDoCu + "','" + docSo.CapDoMoi + "','" + docSo.CongDungCu + "','" + docSo.CongDungMoi + "','" + docSo.DMACu + "','" + docSo.DMAMoi + "','");
            builder.Append(docSo.GhiChuKH.Replace('|', ' ') + "','" + docSo.GhiChuDS.Replace('|', ' ') + "','','','','','','','','','','");
            builder.Append(docSo.TODS + "', '','','','','','','','','','','','','','')");
            ConnectionViewModel.Instance.Connect();
            var query = "select docsoid from docso where docsoid ='" + docSo.DocSoID + "'";
            var find = ConnectionViewModel.Instance.GetExecuteReader(query);
            var insert = 0;
            if (!find.HasRows)
            {
                ConnectionViewModel.Instance.DisConnect();
                ConnectionViewModel.Instance.Connect();
                ConnectionViewModel.Instance.GetExecuteNonQuerry(builder.ToString());
                insert = 1;
            }
            ConnectionViewModel.Instance.DisConnect();
            return insert;
        }
        public int InsertBienDong(ViewModel.BienDong bienDong)
        {
            try
            {
                serverContext.BienDongs.InsertOnSubmit(bienDong);
                serverContext.SubmitChanges();
                return 1;
            }
            catch
            {
                try
                {
                    serverContext.SubmitChanges();
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }
        }
        public void InsertBienDong(DataTable table)
        {
            ConnectionViewModel.Instance.Connect();
            SqlBulkCopy bulkCopy = new SqlBulkCopy(ConnectionViewModel.Instance.getConnection);
            bulkCopy.DestinationTableName =
                   "BienDong";
            try
            {
                bulkCopy.WriteToServer(table);
            }
            catch (Exception e1)
            {
                System.Windows.MessageBox.Show(e1.Message, "Lỗi");
            }

            ConnectionViewModel.Instance.DisConnect();
        }
        public void InsertHoaDon(DataTable table)
        {
            ConnectionViewModel.Instance.Connect();
            SqlBulkCopy bulkCopy = new SqlBulkCopy(ConnectionViewModel.Instance.getConnection);
            bulkCopy.DestinationTableName =
                   "HoaDon";
            try
            {
                bulkCopy.WriteToServer(table);
            }
            catch (Exception e1)
            {
                System.Windows.MessageBox.Show(e1.Message, "Lỗi");
            }

            ConnectionViewModel.Instance.DisConnect();
        }
        public int InsertHoaDon(ViewModel.HoaDon hoaDon)
        {
            try
            {
                ConnectionViewModel.Instance.Connect();
                var querySearch = "select hoadonid from hoadon where hoadonid ='" + hoaDon.HoaDonID + "'";
                SqlDataReader dtr = ConnectionViewModel.Instance.GetExecuteReader(querySearch);
                if (dtr.Read())
                {

                    if (!dtr.IsClosed)
                    {
                        dtr.Close();
                        dtr.Dispose();
                    }
                    dtr = (SqlDataReader)null;
                    return UpdateHoaDon(hoaDon);
                }
                else

                //var search = serverContext.ExecuteQuery<string>(querySearch).FirstOrDefault();
                //if (search != null)
                //    return UpdateHoaDon(hoaDon);
                {
                    if (dtr != null)
                    {
                        if (!dtr.IsClosed)
                        {
                            dtr.Close();
                            dtr.Dispose();
                        }
                        dtr = (SqlDataReader)null;
                    }

                    //int before = serverContext.HoaDons.Count<HoaDon>();
                    StringBuilder builder = new StringBuilder();
                    builder.Append("insert into HoaDon values('");
                    builder.Append(hoaDon.HoaDonID + "'," + hoaDon.Nam + ",'" + hoaDon.Ky + "','");
                    builder.Append(int.Parse(hoaDon.Dot).ToString("00") + "','");
                    builder.Append(hoaDon.DanhBa + "','");
                    builder.Append(hoaDon.TenKH + "','");
                    builder.Append(hoaDon.So + "','");
                    builder.Append(hoaDon.Duong + "',");
                    builder.Append(hoaDon.GB + ",");
                    builder.Append(hoaDon.DM + ",'");
                    builder.Append(hoaDon.Code + "',");
                    builder.Append(hoaDon.CSCu + ",");
                    builder.Append(hoaDon.CSMoi + ",");
                    builder.Append(hoaDon.TieuThu + ",'");
                    builder.Append(hoaDon.TuNgay + "','");
                    builder.Append(hoaDon.DenNgay + "','");
                    builder.Append(hoaDon.SoHoaDon + "','");
                    builder.Append(hoaDon.NgayCapNhat + "','");
                    builder.Append(hoaDon.NVCapNhat + "',");
                    builder.Append(hoaDon.TienHD + ")");
                    var value = ConnectionViewModel.Instance.GetExecuteNonQuerry(builder.ToString());

                    ConnectionViewModel.Instance.DisConnect();
                    //serverContext.ExecuteQuery<object>(query);

                    //int after = serverContext.HoaDons.Count<HoaDon>();
                    if (value != 1)
                    {
                        return UpdateHoaDon(hoaDon);
                    }
                    return value;
                }
            }
            catch
            {

                return UpdateHoaDon(hoaDon);
            }
        }
        private int UpdateHoaDon(ViewModel.HoaDon hoaDon)
        {
            try
            {
                StringBuilder builder = new StringBuilder("Update HoaDon set Nam ='" + hoaDon.Nam + "',Ky = '" + hoaDon.Ky + "',Dot = '" + int.Parse(hoaDon.Dot).ToString("00") + "',DanhBa = '" + hoaDon.DanhBa + "',TenKH = '" + hoaDon.TenKH
                       + "',So = '" + hoaDon.So + "',Duong = '" + hoaDon.Duong + "',GB = '" + hoaDon.GB + "',DM = '" + hoaDon.DM + "',Code = '" + hoaDon.Code + "',CSCu ='" + hoaDon.CSCu +
                       "',CSMoi ='" + hoaDon.CSMoi + "',TieuThu ='" + hoaDon.TieuThu + "',TuNgay = '" + hoaDon.TuNgay + "',DenNgay = '" + hoaDon.DenNgay +
                       "',NgayCapNhat = '" + hoaDon.NgayCapNhat + "',NVCapNhat = '" + hoaDon.NVCapNhat + "', tienhd =" + hoaDon.TienHD + " where HoaDonID = '" + hoaDon.HoaDonID + "'");
                //serverContext.ExecuteQuery<object>(query);
                int value = ConnectionViewModel.Instance.GetExecuteNonQuerry(builder.ToString());
                ConnectionViewModel.Instance.DisConnect();

                return value;

            }
            catch
            {
                ConnectionViewModel.Instance.DisConnect();

                return 0;
            }
        }
        public List<QuanLyNVDocSo> GetNhanVienDocSo(string nam, string ky, string dot, string to)
        {
            List<QuanLyNVDocSo> items = new List<QuanLyNVDocSo>();
            try
            {
                string query = "";
                if (to.Equals(ALL))
                {
                    query = "select distinct m.ToID , m.May, m.NhanVienID as TenNV, m.DienThoai as SDT, count(d.docsoid) as SoKH,count(case when d.CodeMoi <> '' then 1 else null end) as DongBo, sum(d.TieuThuMoi) as SanLuong " +
                   "from MayDS1 m inner join DocSo d on m.May = d.May where m.NhanVienID is not null and d.DocSoID like '" + nam + ky + "%' and d.Dot = '" + dot +
                   "' group by m.ToID, m.May, m.NhanVienID, m.DienThoai";
                }
                else
                    query = "select distinct m.ToID , m.May, m.NhanVienID as TenNV, m.DienThoai as SDT, count(d.docsoid) as SoKH,count(case when d.CodeMoi <> '' then 1 else null end) as DongBo, sum(d.TieuThuMoi) as SanLuong " +
                    "from MayDS1 m inner join DocSo d on m.May = d.May where m.NhanVienID is not null and d.DocSoID like '" + nam + ky + "%' and d.Dot = '" + dot +
                    "' and m.ToID = " + to + " group by m.ToID, m.May, m.NhanVienID, m.DienThoai";
                var data = serverContext.ExecuteQuery<QuanLyNVDocSo>(query).ToList();
                return data;
            }
            catch
            {

            }
            return items;
        }
        public DateTime GetNgayDocSoKyTruoc(int year, string month, string date)
        {
            try
            {
                DateTime dateTime = new DateTime(year, int.Parse(month), int.Parse(date));
                dateTime = dateTime.AddMonths(-1);
                string query = "select distinct denngay from hoadon where hoadonid like '" + dateTime.Year + dateTime.Month.ToString("00") + "%' and dot = " + date + "";
                var data = serverContext.ExecuteQuery<DateTime>(query).ToList();
                return data.ElementAt(0);
            }
            catch
            {

            }
            return DateTime.Now;
        }

        public List<TruyenDuLieu> GetDanhMucFileTheoTo(int year, string month, string date, int xGroup)
        {

            List<TruyenDuLieu> items = new List<TruyenDuLieu>();
            try
            {
                string query = "";
                if (xGroup == 0)
                    query = "select (b.May) as May,Count(distinct b.DanhBa) as SoKH from khachhang b inner join MayDS1 m on b.May = m.May" +
                        " where  b.Dot = " + date + " and HieuLuc = '1' group by b.May  order by b.May";
                else
                    query = "select (b.May) as May,Count(distinct b.DanhBa) as SoKH from khachhang b inner join MayDS1 m on b.May = m.May" +
                        " where  b.Dot = " + date + " and m.ToID = " + xGroup + " and HieuLuc = '1' group by b.May  order by b.May";
                var data = serverContext.ExecuteQuery<TruyenDuLieu>(query).ToList();
                foreach (var item in data)
                {
                    items.Add(new TruyenDuLieu()
                    {
                        X = false,
                        May = item.May,
                        DanhMucFile = year + "_" + month + "_" + date + "_" + item.May,
                        SoKH = item.SoKH,
                        DaTao = ""

                    });
                }
            }
            catch
            {

            }
            return items;
        }

        public List<String> getListBaoCaoTongHop()
        {
            var query = serverContext.ExecuteQuery<String>("select codedesc from ThamSo where CodeType='BC' and Code <>4 order by code ").ToList();
            return query;
        }
        public int CapNhatKH(int year, string month, string date)
        {
            var queryStr = "update KhachHang set HopDong=B.HopDong,MLT1 = B.MLT1,MLT2 = B.MLT1,So = B.So,May = B.May, Duong = B.Duong,GB = B.GB,DM = B.DM" +
                ",SX = B.SX,SH = B.SH,DV = B.DV,HC = B.HC,HieuLuc = '1',Dot = B.Dot  from BienDong B inner join KhachHang K on K.DanhBa = B.DanhBa where biendongid like '" +
                year + month + "%' and b.dot ='" + date + "'";
            var update = serverContext.ExecuteQuery<object>(queryStr).ToList();
            return update.Count;
            //var query = (from B in serverContext.BienDongs
            //             where B.Nam == year && B.Ky == month && B.Dot == date
            //             select new { B.DanhBa, B.HopDong, B.MLT1, B.So, B.May, B.Duong, B.GB, B.DM, B.SX, B.SH, B.DV, B.HC, B.Dot })
            //             .ToList();
            //foreach (var item in query)
            //{
            //    var KH = serverContext.KhachHangs.SingleOrDefault(row => row.DanhBa == item.DanhBa);
            //    if (KH != null)
            //    {
            //        //
            //        serverContext.SubmitChanges();
            //    }
            //}
            //return query.Count;
        }
        public List<ChuanBiKH> GetDocSo_ChuanBiKH(string danhBa, string docSoID)
        {
            string sqlStatement3 = "select top 13 Nam, Ky, Convert(varchar,DenNgay,103) DenNgay, CSMoi, CodeMoi, TieuThuMoi, TTDHNMOI,TBTT from DocSo where DanhBa = '" + danhBa + "' and DocSoID < '" + docSoID + "' order by DocSoID desc";
            var data = serverContext.ExecuteQuery<ChuanBiKH>(sqlStatement3).ToList();
            return data;
        }
        public List<MyKhachHang> GetKhachHang_TaoFile(String may, String dot)
        {
            string sqlStatement3 = "select distinct *, Convert(varchar,NgayGan,103) NgayGanCV from KhachHang k inner join MayDS m on k.May = m.May  where k.May = '" + may + "' and Dot ='" + dot + "' and HieuLuc ='1' order by MLT2";
            var data = serverContext.ExecuteQuery<MyKhachHang>(sqlStatement3).ToList();
            return data;
        }


        public List<MyBienDong> getKHHuy(int nam, string ky, string dot)
        {
            var query = "Select ROW_NUMBER() OVER(ORDER BY DanhBa) STT, DanhBa , MLT1 , TenKH , So,  Duong  from KhachHang k where dot = '" + dot + "' and hieuluc ='1' and not exists ( Select DanhBa from BienDong b where k.DanhBa = b.DanhBa and b.BienDongID like '" + nam + ky + "%' and b.Dot ='" + dot + "' )";
            var data = serverContext.ExecuteQuery<KhachHang>(query).ToList();
            List<MyBienDong> list = new List<MyBienDong>();
            int stt = 0;
            foreach (var item in data)
            {
                stt++;
                list.Add(new MyBienDong()
                {
                    STT = stt,
                    DanhBa = item.DanhBa,
                    MLT1 = item.MLT1,
                    TenKH = item.TenKH,
                    So = item.So,
                    Duong = item.Duong
                });
            }
            //var data = (from x in serverContext.BienDongs
            //            where x.BienDongID.StartsWith(nam + ky + "%") && x.Dot == dot
            //            select new {x.DanhBa, x.MLT1, x.TenKH, x.So, x.Duong }).ToList();
            return list;
        }
        public List<MyBienDong> getKHGanMoi(int nam, string ky, string dot)
        {
            var query = "Select danhba, mlt1, tenkh, so, duong from BienDong where DanhBa not in ( Select DanhBa from KhachHang) and biendongid like '" + nam + ky + "%' and Dot ='" + dot + "'";
            var data = serverContext.ExecuteQuery<BienDong>(query).ToList();
            List<MyBienDong> list = new List<MyBienDong>();
            int stt = 0;
            foreach (var item in data)
            {
                stt++;
                list.Add(new MyBienDong()
                {
                    STT = stt,
                    DanhBa = item.DanhBa,
                    MLT1 = item.MLT1,
                    TenKH = item.TenKH,
                    So = item.So,
                    Duong = item.Duong
                });
            }
            //var data = (from x in serverContext.BienDongs
            //            where x.BienDongID.StartsWith(nam + ky + "%") && x.Dot == dot
            //            select new {x.DanhBa, x.MLT1, x.TenKH, x.So, x.Duong }).ToList();
            return list;
        }
        public IEnumerable getNote(string danhBa)
        {
            var data = (from x in serverContext.DocSos
                        where x.DanhBa == danhBa
                        orderby x.DocSoID descending
                        select new { x.Ky, x.Nam, danhBa, x.CodeMoi, x.TTDHNMoi, x.CSCu, x.CSMoi, x.TieuThuMoi, x.GhiChuDS, x.GhiChuKH, x.GhiChuTV }).ToList();
            return data;
        }
        public bool CheckExistHoaDon(int nam, string ky, string dot)
        {
            var value = serverContext.ExecuteQuery<string>("select top 1 hoadonid from hoadon where hoadonid like '" + nam + ky + "%' and dot = " + dot + "").ToList();
            if (value.Count > 0)
                return true;
            return false;
        }
        public bool CheckExistBienDong(int nam, string ky, string dot)
        {
            var value = serverContext.ExecuteQuery<string>("select top 1 biendongid from biendong where biendongid like '" + nam + ky + "%' and dot = " + dot + "").ToList();
            if (value.Count > 0)
                return true;
            return false;
        }
        public bool CheckExistBienDong_KiemTraHuy(int nam, string ky, string dot)
        {
            try
            {
                string query = "Select top 1 danhba from KhachHang k where dot = '" + dot + "' and hieuluc = '1' and not exists(Select DanhBa from BienDong b where k.DanhBa = b.DanhBa and b.BienDongID like '" + nam + ky + "%' and dot = '" + dot + "')";
                var value = serverContext.ExecuteQuery<object>(query).ToList();
                if (value.Count > 0)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool CheckExistBienDong_KiemTraGanMoi(int nam, string ky, string dot)
        {
            try
            {
                string query = "select top 1 biendongid from BienDong where biendongid like '" + nam + ky + "%' " +
                    "and dot = '" + dot + "' and not exists (" +
                    " Select DanhBa from KhachHang where BienDong.DanhBa = KhachHang.DanhBa and dot = '" + dot + "')";
                var value = serverContext.ExecuteQuery<object>(query).ToList();
                if (value.Count > 0)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public int InsertKhachHang(int nam, string ky, string dot)
        {
            string query = "insert into KhachHang(DanhBa,May,HieuLuc,HopDong,MLT1,MLT2,Dot,TenKH,So,Duong,SoMoi,Phuong,Quan,GB,DM,SX,DV,HC,NgayGan,Hieu,Co,SoThan,GanMoi,TTBaoThay) select DanhBa,May,'1'    ,HopDong,MLT1,MLT1,Dot,TenKH,So,Duong,So   ,Phuong,Quan,GB,DM,SX,DV,HC,NgayGan,Hieu,Co,SoThan,'1','0' from BienDong B where biendongid like '" + nam + ky + "%' and dot = '" + dot + "' and  not exists ( Select DanhBa from KhachHang where B.DanhBa = KhachHang.DanhBa and dot = '" + dot + "')";
            var value = serverContext.ExecuteQuery<object>(query).ToList();
            return value.Count;
        }
        public int HuyKhachHang(int nam, string ky, string dot)
        {
            string query = "update KhachHang set HieuLuc = '0',TTBaoThay = '0',GanMoi = '0' where KhachHang.Dot = '" + dot + "' and hieuluc = '1' and not exists (select DanhBa from BienDong B where b.DanhBa = KhachHang.DanhBa and b.BienDongID like'" + nam + ky + "%' and dot = '" + dot + "')";
            var value = serverContext.ExecuteQuery<object>(query).ToList();
            return value.Count;
        }
        public int Update_Docso_KiemTraDuLieu_Code4(int nam, string ky, string dot)
        {
            string query = "update DocSo set TieuThuMoi = H.TieuThu, CSMoi = H.CSMoi, TuNgay = H.TuNgay, DenNgay = H.DenNgay from HoaDon H inner join DocSo D on D.DocSoID = H.HoaDonID where Left(H.Code,1) = '4' and D.CodeMoi in ('40','41','42','43','44','45') and H.hoadonid like '" + nam + ky + "%' and H.Dot = '" + dot + "'";
            var value = serverContext.ExecuteQuery<object>(query).ToList();
            return value.Count;
        }
        public int Update_Docso_KiemTraDuLieu_Code4_Lan2(int nam, string ky, string dot)
        {
            string query = "update DocSo set TieuThuMoi = H.TieuThu, CSMoi = H.CSMoi, TTDHNMoi = 'CSBT', CodeMoi = '40', TuNgay = H.TuNgay, DenNgay = H.DenNgay from HoaDon H inner join DocSo D on D.DocSoID = H.HoaDonID where Left(H.Code,1) = '4' and D.CodeMoi <> '4' and H.hoadonid like '" + nam + ky + "%' and H.Dot = '" + dot + "'";
            var value = serverContext.ExecuteQuery<object>(query).ToList();
            return value.Count;
        }
        public int Update_Docso_KiemTraDuLieu_Code5_8_M(int nam, string ky, string dot)
        {
            string query = "update DocSo set CodeMoi = H.Code, TieuThuMoi = H.TieuThu, CSMoi = H.CSMoi, TTDHNMoi = T.TTDHN, TuNgay = H.TuNgay, DenNgay = H.DenNgay  from HoaDon H inner join DocSo D on D.DocSoID = H.HoaDonID inner join TTDHN T on H.Code = T.Code where Left(H.Code,1) in ('5','8','M') and H.hoadonid like '" + nam + ky + "%' and H.Dot = '" + dot + "'";
            var value = serverContext.ExecuteQuery<object>(query).ToList();
            return value.Count;

        }
        public int Update_Docso_KiemTraDuLieu_Code6_K_F_N(int nam, string ky, string dot)
        {
            string query = "update DocSo set CSMoi = D.CSMoi, CodeMoi = H.Code, TTDHNMoi = T.TTDHN, TieuThuMoi = H.TieuThu, TuNgay = H.TuNgay, DenNgay = H.DenNgay from HoaDon H inner join DocSo D on D.DocSoID = H.HoaDonID inner join TTDHN T on H.Code = T.Code where Left(H.Code,1) in ('6','K','F','N') and H.hoadonid like '" + nam + ky + "%' and H.Dot = '" + dot + "'";
            var value = serverContext.ExecuteQuery<object>(query).ToList();
            return value.Count;
        }
        public bool CheckExistKHHuy(int nam, string ky, string dot)
        {
            var query = "select top 1 danhba from KhachHang where DanhBa not in (select DanhBa from BienDong where biendongid like '" + nam + ky + "%' and Dot = '" + dot + "')";
            var value = serverContext.ExecuteQuery<object>(query).ToList();
            if (value.Count > 0)
                return true;
            return false;
        }
        public int CountHoaDon(int nam, string ky, string dot)
        {
            var value = (from x in serverContext.HoaDons
                         where x.Nam == nam && x.Ky == ky && x.Dot == dot
                         select x.HoaDonID).ToList();
            return value.Count;
        }
        public int CountBienDong(int nam, string ky, string dot)
        {
            var value = (from x in serverContext.BienDongs
                         where x.Nam == nam && x.Ky == ky && x.Dot == dot
                         select x.BienDongID).ToList();
            return value.Count;
        }
        public DataTable GetDHNTrenMang(int nam, int ky)
        {
            List<DHNTrenMang> query = new List<DHNTrenMang>();
            DataTable table = new DataTable();
            try
            {
                serverContext.ExecuteQuery<object>(String.Format(@"execute thongkedhn_trenmang {0},{1}", ky, nam));
                ConnectionViewModel.Instance.Connect();
                table = ConnectionViewModel.Instance.GetDataTable("select * from tb_thongkedhn");
                ConnectionViewModel.Instance.DisConnect();
                int count = 0;
                foreach (DataRow row in table.Rows)
                {
                    row["STT"] = ++count;
                }
            }
            catch
            {

            }
            return table;
        }
        public DataTable GetTKSoLuongCoDungGieng(int year, string month, string date)
        {
            List<BCTongHop> query;
            String queryStr = "";
            DataTable table = new DataTable();
            table.Columns.Add(BCTONGHOP_COLUMN_NAM, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_KY, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_DOT, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_TOID, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_TENPHUONG, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_SOLANGHI, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_DANHBA, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_TIEUTHUMOI, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_CO, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_HIEU, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_NHANVIENID, typeof(string));

            try
            {

                queryStr = " select Toid, d.May, Dot, KY, NAM,m.nhanvienid, count(danhba) as danhba, sum(TieuThuMoi) as tieuthumoi from DocSo d inner join MayDS m on d.May=m.May     where Ky='" + month + "' and Nam='" + year + "' and CodeMoi <> 'X'    group by toid,d.May,dot,ky,NAM,m.nhanvienid order by May";

                query = serverContext.ExecuteQuery<BCTongHop>(queryStr).ToList();

                foreach (var item in query)
                {
                    DataRow row = table.NewRow();
                    row[BCTONGHOP_COLUMN_NAM] = item.Nam;
                    row[BCTONGHOP_COLUMN_KY] = item.Ky;
                    row[BCTONGHOP_COLUMN_DOT] = item.Dot;
                    row[BCTONGHOP_COLUMN_TOID] = item.Toid;
                    row[BCTONGHOP_COLUMN_TENPHUONG] = item.TenPhuong;
                    row[BCTONGHOP_COLUMN_SOLANGHI] = item.SOLANGHI;
                    row[BCTONGHOP_COLUMN_DANHBA] = item.DanhBa;
                    row[BCTONGHOP_COLUMN_TIEUTHUMOI] = item.TieuThuMoi;
                    row[BCTONGHOP_COLUMN_CO] = item.Co;
                    row[BCTONGHOP_COLUMN_HIEU] = item.Hieu;
                    row[BCTONGHOP_COLUMN_NHANVIENID] = item.NhanVienID;

                    table.Rows.Add(row);
                }
            }
            catch
            {

            }

            return table;
        }
        public DataTable GetTKSoLuong_SanLuongDHN_Co_Hieu(int year, string month, string date)
        {
            List<BCTongHop> query;
            String queryStr = "";
            DataTable table = new DataTable();
            table.Columns.Add(BCTONGHOP_COLUMN_NAM, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_KY, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_DOT, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_TOID, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_TENPHUONG, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_SOLANGHI, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_DANHBA, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_TIEUTHUMOI, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_CO, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_HIEU, typeof(string));

            try
            {

                queryStr = " select Toid,k.Dot,ky,nam,Hieu ,cast(co as int) as co,  count(k.danhba) DanhBa, SUM(TieuThuMoi) TieuThuMoi  from DocSo d inner join MayDS m on d.May=m.May  \t\t\t  inner join KhachHang k on d.DanhBa=k.DanhBa  where k.dot='" + date + "' and Ky='" + month + "' and Nam='" + year + "'  and Hieu is not null and Co is not null  group by Toid,k.Dot,ky,nam,Hieu,co  order by Toid,k.Dot,ky,nam,Hieu, co asc ";

                query = serverContext.ExecuteQuery<BCTongHop>(queryStr).ToList();

                foreach (var item in query)
                {
                    DataRow row = table.NewRow();
                    row[BCTONGHOP_COLUMN_NAM] = item.Nam;
                    row[BCTONGHOP_COLUMN_KY] = item.Ky;
                    row[BCTONGHOP_COLUMN_DOT] = item.Dot;
                    row[BCTONGHOP_COLUMN_TOID] = item.Toid;
                    row[BCTONGHOP_COLUMN_TENPHUONG] = item.TenPhuong;
                    row[BCTONGHOP_COLUMN_SOLANGHI] = item.SOLANGHI;
                    row[BCTONGHOP_COLUMN_DANHBA] = item.DanhBa;
                    row[BCTONGHOP_COLUMN_TIEUTHUMOI] = item.TieuThuMoi;
                    row[BCTONGHOP_COLUMN_CO] = item.Co;
                    row[BCTONGHOP_COLUMN_HIEU] = item.Hieu;

                    table.Rows.Add(row);
                }
            }
            catch
            {

            }

            return table;
        }
        public DataTable GetTKSoLuong_SanLuongDHN_Phuong(int year, string month, string date)
        {
            List<BCTongHop> query;
            String queryStr = "";
            DataTable table = new DataTable();
            table.Columns.Add(BCTONGHOP_COLUMN_NAM, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_KY, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_DOT, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_TOID, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_TENPHUONG, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_SOLANGHI, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_DANHBA, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_TIEUTHUMOI, typeof(string));

            try
            {

                queryStr = " select Toid,tenphuong,ky,nam,COUNT(d.danhba) as DanhBa ,SUM(TieuThuMoi) as TieuThuMoi  from DocSo d inner join KhachHang k on d.DanhBa=k.DanhBa                 inner join MayDS m on d.May=m.May                 inner join Phuong p on k.Phuong=p.Phuong  where Ky ='" + month + "' and Nam='" + year + "' and HieuLuc=1   group by Toid,tenphuong,ky,nam    order by Toid,tenphuong,ky,nam ";

                query = serverContext.ExecuteQuery<BCTongHop>(queryStr).ToList();

                foreach (var item in query)
                {
                    DataRow row = table.NewRow();
                    row[BCTONGHOP_COLUMN_NAM] = item.Nam;
                    row[BCTONGHOP_COLUMN_KY] = item.Ky;
                    row[BCTONGHOP_COLUMN_DOT] = item.Dot;
                    row[BCTONGHOP_COLUMN_TOID] = item.Toid;
                    row[BCTONGHOP_COLUMN_TENPHUONG] = item.TenPhuong;
                    row[BCTONGHOP_COLUMN_SOLANGHI] = item.SOLANGHI;
                    row[BCTONGHOP_COLUMN_DANHBA] = item.DanhBa;
                    row[BCTONGHOP_COLUMN_TIEUTHUMOI] = item.TieuThuMoi;

                    table.Rows.Add(row);
                }
            }
            catch
            {

            }

            return table;
        }
        public DataTable GetTKSoLuong_SanLuongDHN_DMA(int year, string month, string date)
        {
            List<BCTongHop> query;
            String queryStr = "";
            DataTable table = new DataTable();
            table.Columns.Add(BCTONGHOP_COLUMN_NAM, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_KY, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_DOT, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_SOLANGHI, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_DANHBA, typeof(string));
            table.Columns.Add(BCTONGHOP_COLUMN_TIEUTHUMOI, typeof(string));

            try
            {
                if (date.Equals(ALL))
                {
                    queryStr = " select k.DMA as SOLANGHI,ky,nam,COUNT(d.danhba) as DanhBa ,SUM(d.TieuThuMoi) as TieuThuMoi  from DocSo d inner join KhachHang k on d.DanhBa = k.DanhBa  where d.Ky ='" + month + "' and d.Nam='" + year + "' group by DMA,ky,nam  order by DMA,ky,nam ";
                }
                else
                {
                    queryStr = " select k.DMA as SOLANGHI,ky,nam,COUNT(d.danhba) as DanhBa ,SUM(d.TieuThuMoi) as TieuThuMoi  from DocSo d inner join KhachHang k on d.DanhBa = k.DanhBa  where d.Ky ='" + month + "' and d.Nam='" + year + "' and d.Dot = '" + date + "'  group by DMA,ky,nam  order by DMA,ky,nam ";
                }
                query = serverContext.ExecuteQuery<BCTongHop>(queryStr).ToList();

                foreach (var item in query)
                {
                    DataRow row = table.NewRow();
                    row[BCTONGHOP_COLUMN_NAM] = item.Nam;
                    row[BCTONGHOP_COLUMN_KY] = item.Ky;
                    row[BCTONGHOP_COLUMN_DOT] = item.Dot;
                    row[BCTONGHOP_COLUMN_SOLANGHI] = item.SOLANGHI;
                    row[BCTONGHOP_COLUMN_DANHBA] = item.DanhBa;
                    row[BCTONGHOP_COLUMN_TIEUTHUMOI] = item.TieuThuMoi;

                    table.Rows.Add(row);
                }
            }
            catch
            {

            }

            return table;
        }
        public DataTable GetThongKeDocSo(int year, string month, string date)
        {
            List<TKSauDocSo> query;
            String queryStr = "";
            DataTable table = new DataTable();
            table.Columns.Add(TKSAUDOCSO_COLUMN_TOID, typeof(string));
            table.Columns.Add(TKSAUDOCSO_COLUMN_KY, typeof(string));
            table.Columns.Add(TKSAUDOCSO_COLUMN_DOT, typeof(string));
            table.Columns.Add(TKSAUDOCSO_COLUMN_MAY, typeof(string));
            table.Columns.Add(TKSAUDOCSO_COLUMN_CODEMOI, typeof(string));
            table.Columns.Add(TKSAUDOCSO_COLUMN_DANHBA, typeof(string));
            table.Columns.Add(TKSAUDOCSO_COLUMN_TIEUTHUMOI, typeof(string));

            try
            {
                queryStr = " select ToID,CodeMoi,dot,ky,nam,count(DanhBa) as DanhBa ,sum(TieuThuMoi) as TieuThuMoi  from DocSo d inner join MayDS m on d.May=m.May  where Nam='" + year + "' and Ky='" + month + "' and Dot='" + date + "'  and CodeMoi is not null and CodeMoi !=''  group by ToID,CodeMoi,dot,ky,nam  order by ToID,CodeMoi,dot,ky,nam";
                query = serverContext.ExecuteQuery<TKSauDocSo>(queryStr).ToList();

                foreach (var item in query)
                {
                    DataRow row = table.NewRow();
                    row[TKSAUDOCSO_COLUMN_TOID] = item.ToID;
                    row[TKSAUDOCSO_COLUMN_KY] = item.Ky;
                    row[TKSAUDOCSO_COLUMN_DOT] = item.Dot;
                    row[TKSAUDOCSO_COLUMN_MAY] = item.May;
                    row[TKSAUDOCSO_COLUMN_CODEMOI] = item.CodeMoi;
                    row[TKSAUDOCSO_COLUMN_DANHBA] = item.DanhBa;
                    row[TKSAUDOCSO_COLUMN_TIEUTHUMOI] = item.TieuThuMoi;

                    table.Rows.Add(row);
                }
            }
            catch
            {

            }

            return table;
        }

        public DataTable GetThongKeSauDocSo(int year, string month, string date, int group, string machine)
        {
            List<TKSauDocSo> query;
            String queryStr = "";
            DataTable table = new DataTable();
            table.Columns.Add(TKSAUDOCSO_COLUMN_TOID, typeof(string));
            table.Columns.Add(TKSAUDOCSO_COLUMN_KY, typeof(string));
            table.Columns.Add(TKSAUDOCSO_COLUMN_DOT, typeof(string));
            table.Columns.Add(TKSAUDOCSO_COLUMN_MAY, typeof(string));
            table.Columns.Add(TKSAUDOCSO_COLUMN_CODEMOI, typeof(string));
            table.Columns.Add(TKSAUDOCSO_COLUMN_DANHBA, typeof(string));
            table.Columns.Add(TKSAUDOCSO_COLUMN_TIEUTHUMOI, typeof(string));

            try
            {
                queryStr = "Select ds.May, m.ToID, Count(ds.DanhBa) as DanhBa, Case when ds.CodeMoi is null or ds.CodeMoi = ''" +
                   " then N'Chưa đọc' else ds.CodeMoi end as CodeMoi, Sum(ds.TieuThuMoi) as TieuThuMoi, ds.Ky, ds.Dot " +
                   "from DocSo ds Inner Join MayDS m on ds.May = m.may where ds.Ky = '" + month + "' and ds.Dot = '" + date + "' and ds.Nam = " + year;
                if (machine.Equals(ALL) && group != 0)
                    queryStr += " and ToID = " + group + " Group by ds.May,m.ToID,ds.CodeMoi,ds.Ky,ds.Dot";
                else if (group == 0)
                    queryStr += " Group by ds.May,m.ToID,ds.CodeMoi,ds.Ky,ds.Dot";
                else
                    queryStr += " and ToID = " + group + " and ds.May ='" + machine + "' Group by ds.May,m.ToID,ds.CodeMoi,ds.Ky,ds.Dot";
                query = serverContext.ExecuteQuery<TKSauDocSo>(queryStr).ToList();

                foreach (var item in query)
                {
                    DataRow row = table.NewRow();
                    row[TKSAUDOCSO_COLUMN_TOID] = item.ToID;
                    row[TKSAUDOCSO_COLUMN_KY] = item.Ky;
                    row[TKSAUDOCSO_COLUMN_DOT] = item.Dot;
                    row[TKSAUDOCSO_COLUMN_MAY] = item.May;
                    row[TKSAUDOCSO_COLUMN_CODEMOI] = item.CodeMoi;
                    row[TKSAUDOCSO_COLUMN_DANHBA] = item.DanhBa;
                    row[TKSAUDOCSO_COLUMN_TIEUTHUMOI] = item.TieuThuMoi;

                    table.Rows.Add(row);
                }
            }
            catch
            {

            }

            return table;
        }

        public DataTable GetListTieuThuBatThuong(int year, string month, string date, string machine)
        {
            List<KHTieuThuBatThuong> query;
            if (machine.Equals(ALL))
                if (MyUser.Instance.ToID != "")
                    query = serverContext.ExecuteQuery<KHTieuThuBatThuong>("SELECT KH.SDT, KH.DOT, KH.MLT2, KH.DANHBA, DS.TBTT," +
                        " KH.TENKH," +
                        " DS.MAY, DS.CODEMoi, DS.TIEUTHUMoi, DS.CSMOI, DS.KY,ds.Nam,m.NhanVienID as NVGHI," +
                        " CASE WHEN KH.SOMOI IS NOT NULL AND  KH.SOMOI != '' THEN KH.So + ' (' + KH.SOMOI + ') ' + KH.DUONG ELSE KH.SO + ' ' + KH.DUONG END AS SoMoi," +
                        "m.ToID FROM  DocSo DS LEFT OUTER JOIN KHACHHANG AS KH ON DS.DANHBA = KH.DANHBA " +
                        "Inner join MayDS as m on DS.May = m.May WHERE m.ToID = '" + MyUser.Instance.ToID + "' AND DS.KY = " + month + " AND DS.DOT = " + date + " AND DS.Nam = " + year + " AND LEFT(DS.CodeMoi,1) != 'M' AND LEFT(DS.CodeMoi,1) != 'F' AND DS.TieuThuMoi > 4 ORDER BY KH.MLT2 ").ToList();
                else
                    query = serverContext.ExecuteQuery<KHTieuThuBatThuong>("SELECT KH.SDT, KH.DOT, KH.MLT2, KH.DANHBA, DS.TBTT, KH.TENKH, DS.MAY, DS.CODEMoi, DS.TIEUTHUMoi, DS.CSMOI, DS.KY,ds.Nam,m.NhanVienID as NVGHI, CASE WHEN KH.SOMOI IS NOT NULL AND  KH.SOMOI != '' THEN KH.So + ' (' + KH.SOMOI + ') ' + KH.DUONG ELSE KH.SO + ' ' + KH.DUONG END AS SoMoi,m.ToID FROM  DocSo DS LEFT OUTER JOIN KHACHHANG AS KH ON DS.DANHBA = KH.DANHBA Inner join MayDS as m on DS.May = m.May WHERE DS.KY = " + month + " AND DS.DOT = " + date + " AND DS.Nam = " + year + " AND LEFT(DS.CodeMoi,1) != 'M' AND LEFT(DS.CodeMoi,1) != 'F' AND DS.TieuThuMoi > 4 ORDER BY KH.MLT2 ").ToList();
            else query = serverContext.ExecuteQuery<KHTieuThuBatThuong>("SELECT KH.SDT, KH.DOT, KH.MLT2, KH.DANHBA, DS.TBTT, KH.TENKH, DS.MAY, DS.CODEMoi, DS.TIEUTHUMoi, DS.CSMOI, DS.KY,ds.Nam,m.NhanVienID as NVGHI, CASE WHEN KH.SOMOI IS NOT NULL AND  KH.SOMOI != '' THEN KH.So + ' (' + KH.SOMOI + ') ' + KH.DUONG ELSE KH.SO + ' ' + KH.DUONG END AS SoMoi,m.ToID FROM  DocSo DS LEFT OUTER JOIN KHACHHANG AS KH ON DS.DANHBA = KH.DANHBA Inner join MayDS as m on DS.May = m.May WHERE DS.KY = " + month + " AND DS.DOT = " + date + " AND DS.May = '" + machine + "' AND DS.Nam = " + year + " AND LEFT(DS.CodeMoi,1) != 'M' AND LEFT(DS.CodeMoi,1) != 'F' AND DS.TieuThuMoi > 4 ORDER BY KH.MLT2 ").ToList();
            DataTable table = new DataTable();
            table.Columns.Add(DC_COLUMN_MyUserNAME, typeof(string));
            table.Columns.Add(DC_COLUMN_TODS, typeof(string));
            table.Columns.Add(DC_COLUMN_KY, typeof(string));
            table.Columns.Add(DC_COLUMN_DOT, typeof(string));
            table.Columns.Add(DC_COLUMN_MAY, typeof(string));
            table.Columns.Add(DC_COLUMN_STT, typeof(string));
            table.Columns.Add(DC_COLUMN_MLT, typeof(string));
            table.Columns.Add(DC_COLUMN_SDT, typeof(string));
            table.Columns.Add(DC_COLUMN_DANHBA, typeof(string));
            table.Columns.Add(DC_COLUMN_TENKH, typeof(string));
            table.Columns.Add(DC_COLUMN_DUONG, typeof(string));
            table.Columns.Add(DC_COLUMN_SOTHANCU, typeof(string));
            table.Columns.Add(DC_COLUMN_CODEMOI, typeof(string));
            table.Columns.Add(DC_COLUMN_CSCU, typeof(string));
            table.Columns.Add(DC_COLUMN_TBTT, typeof(string));
            table.Columns.Add(DC_COLUMN_TIEUTHUMOI, typeof(string));
            table.Columns.Add(DC_COLUMN_CSMOI, typeof(string));


            int stt = 0;
            foreach (var item in query)
            {
                double num1 = item.TieuThuMoi;
                double num2 = item.TBTT;
                if (num1 < 0.0 || (num1 == 0.0 && num2 > 3.0) ||
                    (num1 >= 1.0 && num1 <= 10.0 && (num1 < num2 * 0.4 || num1 > num2 * 4.0)) ||
                    (num1 > 10.0 && num1 <= 20.0 && (num1 < num2 * 0.55 || num1 > num2 * 4.0)) ||
                    (num1 > 20.0 && num1 <= 40.0 && (num1 < num2 * 0.65 || num1 > num2 * 2.0)) ||
                    (num1 > 40.0 && num1 <= 80.0 && (num1 < num2 * 0.7 || num1 > num2 * 1.8)) ||
                    (num1 > 80.0 && num1 <= 200.0 && (num1 < num2 * 0.75 || num1 > num2 * 1.8)) ||
                    (num1 > 200.0 && num1 <= 400.0 && (num1 < num2 * 0.8 || num1 > num2 * 1.7)) ||
                    (num1 > 400.0 && num1 <= 800.0 && (num1 < num2 * 0.8 || num1 > num2 * 1.6)) ||
                    (num1 > 800.0 && num1 <= 2000.0 && (num1 < num2 * 0.8 || num1 > num2 * 1.5)) ||
                    (num1 > 2000.0 && num1 <= 5000.0 && (num1 < num2 * 0.8 || num1 > num2 * 1.4)) ||
                    (num1 > 5000.0 && (num1 < num2 * 0.8 || num1 > num2 * 1.3)))
                {
                    DataRow row = table.NewRow();
                    if (!machine.Equals(ALL))
                        row[DC_COLUMN_MyUserNAME] = item.NVGHI;
                    row[DC_COLUMN_TODS] = item.ToID;
                    row[DC_COLUMN_KY] = month;
                    row[DC_COLUMN_DOT] = date;
                    row[DC_COLUMN_MAY] = machine;
                    row[DC_COLUMN_STT] = ++stt;
                    row[DC_COLUMN_MLT] = item.MLT2;
                    row[DC_COLUMN_SDT] = item.SDT;
                    row[DC_COLUMN_DANHBA] = item.DANHBA;
                    row[DC_COLUMN_TENKH] = item.TENKH;
                    row[DC_COLUMN_DUONG] = item.SoMoi;
                    row[DC_COLUMN_CODEMOI] = item.CodeMoi;
                    row[DC_COLUMN_CSMOI] = item.CSMoi;
                    row[DC_COLUMN_TIEUTHUMOI] = item.TieuThuMoi;
                    row[DC_COLUMN_TBTT] = item.TBTT;
                    table.Rows.Add(row);
                }
            }
            return table;
        }
        public DataTable GetListCloseDoor(int year, string month, string date, string machine)
        {
            List<KHDongCua> query;
            if (machine.Equals(ALL))
                query = serverContext.ExecuteQuery<KHDongCua>("SELECT  0 AS STT,ds.SDT,  K.MLT2, K.DANHBA" +
                    ", RTRIM(K.TENKH) AS TENKH,SoThanCu, CASE WHEN K.SOMOI IS NULL OR K.SOMOI = '' " +
                    "THEN K.SO + ' ' + K.DUONG ELSE K.SO + '('+ K.SOMOI +')' + K.DUONG END AS Duong" +
                    ",m.NhanVienID as MyUsername," +
                    "ds.CodeCu, DS.CodeMoi, DS.CSCu" +
                    ",DS.TieuThuCu" +
                    ",m.ToID" +
                    ",k.SoThan as TamTinh " +
                    " FROM DocSo as DS RIGHT OUTER JOIN KHACHHANG AS K ON DS.DANHBA = K.DANHBA Inner JOIN MayDS as m on DS.May = m.may " +
                    "WHERE ds.nam = " + year + " and DS.KY = " + month + " AND DS.DOT = " + date + " AND DS.CodeMoi LIKE'F%' ORDER BY K.MLT2").ToList();
            else query = serverContext.ExecuteQuery<KHDongCua>("SELECT  0 AS STT,ds.SDT,  K.MLT2, K.DANHBA, RTRIM(K.TENKH) AS TENKH, CASE WHEN K.SOMOI IS NULL OR K.SOMOI = '' THEN K.SO + ' ' + K.DUONG ELSE K.SO + '('+ K.SOMOI +')' + K.DUONG END AS Duong,m.NhanVienID as MyUsername,ds.nam, DS.KY, DS.DOT, DS.MAY,ds.CodeCu, DS.CodeMoi, DS.CSCu,DS.TieuThuCu,m.ToID,k.SoThan as TamTinh  FROM DocSo as DS RIGHT OUTER JOIN KHACHHANG AS K ON DS.DANHBA = K.DANHBA Inner JOIN MayDS as m on DS.May = m.may WHERE ds.nam = " + year + " and DS.KY = " + month + " AND DS.DOT = " + date + " and ds.may = " + machine + " AND DS.CodeMoi LIKE'F%' ORDER BY K.MLT2").ToList();
            DataTable table = new DataTable();
            table.Columns.Add(DC_COLUMN_MyUserNAME, typeof(string));
            table.Columns.Add(DC_COLUMN_TODS, typeof(string));
            table.Columns.Add(DC_COLUMN_KY, typeof(string));
            table.Columns.Add(DC_COLUMN_DOT, typeof(string));
            table.Columns.Add(DC_COLUMN_MAY, typeof(string));
            table.Columns.Add(DC_COLUMN_STT, typeof(string));
            table.Columns.Add(DC_COLUMN_MLT, typeof(string));
            table.Columns.Add(DC_COLUMN_SDT, typeof(string));
            table.Columns.Add(DC_COLUMN_DANHBA, typeof(string));
            table.Columns.Add(DC_COLUMN_TENKH, typeof(string));
            table.Columns.Add(DC_COLUMN_DUONG, typeof(string));
            table.Columns.Add(DC_COLUMN_SOTHANCU, typeof(string));
            table.Columns.Add(DC_COLUMN_CODEMOI, typeof(string));
            table.Columns.Add(DC_COLUMN_CSCU, typeof(string));
            table.Columns.Add(DC_COLUMN_TBTT, typeof(string));
            table.Columns.Add(DC_COLUMN_CSMOI, typeof(string));

            int stt = 0;
            foreach (var item in query)
            {
                DataRow row = table.NewRow();
                if (!machine.Equals(ALL))
                    row[DC_COLUMN_MyUserNAME] = item.MyUsername;
                row[DC_COLUMN_TODS] = item.ToDS;
                row[DC_COLUMN_KY] = month;
                row[DC_COLUMN_DOT] = date;
                row[DC_COLUMN_MAY] = machine;
                row[DC_COLUMN_STT] = ++stt;
                row[DC_COLUMN_MLT] = item.MLT2;
                row[DC_COLUMN_SDT] = item.SDT;
                row[DC_COLUMN_DANHBA] = item.DANHBA;
                row[DC_COLUMN_TENKH] = item.TENKH;
                row[DC_COLUMN_DUONG] = item.Duong;
                row[DC_COLUMN_SOTHANCU] = item.SoThanCu;
                row[DC_COLUMN_CODEMOI] = item.CodeMoi;
                row[DC_COLUMN_CSCU] = item.CSCu;
                row[DC_COLUMN_TBTT] = item.TBTT;
                row[DC_COLUMN_CSMOI] = item.CSMoi;

                table.Rows.Add(row);
            }
            return table;
        }
        public DataTable GetListDate_Machine(object selectedValue)
        {
            List<TKDHNTheoDotSo> query;
            if (selectedValue.Equals(ALL))
                query = serverContext.ExecuteQuery<TKDHNTheoDotSo>("Select Dot, May, COUNT(Danhba) AS Danhba FROM KhachHang WHERE HieuLuc = 1 GROUP BY Dot, May").ToList();
            else query = serverContext.ExecuteQuery<TKDHNTheoDotSo>("SELECT m.ToID, kh.Dot, kh.May, COUNT(kh.DanhBa) as DanhBa FROM KhachHang kh Inner Join MayDS m on kh.May = m.May " +
                "WHERE m.ToID = '" + selectedValue + "' AND kh.HieuLuc = 1 GROUP BY kh.Dot, kh.May,m.ToID").ToList();
            DataTable table = new DataTable();
            table.Columns.Add(TKDHNTHEODOTSO_COLUMN_TOID, typeof(string));
            table.Columns.Add(TKDHNTHEODOTSO_COLUMN_DOT, typeof(string));
            table.Columns.Add(TKDHNTHEODOTSO_COLUMN_MAY, typeof(string));
            table.Columns.Add(TKDHNTHEODOTSO_COLUMN_DANHBA, typeof(string));
            foreach (var item in query)
            {
                DataRow row = table.NewRow();
                row[TKDHNTHEODOTSO_COLUMN_TOID] = item.ToID;
                row[TKDHNTHEODOTSO_COLUMN_DOT] = item.Dot;
                row[TKDHNTHEODOTSO_COLUMN_MAY] = item.May;
                row[TKDHNTHEODOTSO_COLUMN_DANHBA] = item.Danhba;
                table.Rows.Add(row);
            }
            return table;
        }

        public DataTable GetInfoCheckCustomer2(int count, string ghiChu, string danhBa)
        {


            var query = (from d in serverContext.DocSoLuuTrus
                         join k in serverContext.KhachHangs on d.DanhBa equals k.DanhBa
                         where d.DanhBa == danhBa
                         orderby d.DocSoID descending
                         select new { d.MLT1, d.CodeMoi, d.CSMoi, d.TieuThuMoi, d.DenNgay, k.TenKH, k.Hieu, k.Co, k.SoThan, k.HopDong, k.GB, k.DM, k.So, k.SoMoi, k.Duong, d.Ky, d.Nam })
                         .Take(12).ToList();
            DataTable table = new DataTable();
            table.Columns.Add("TenKH", typeof(string));
            table.Columns.Add("Hieu", typeof(string));
            table.Columns.Add("Co", typeof(string));
            table.Columns.Add("DanhBa", typeof(string));
            table.Columns.Add("MLT1", typeof(string));
            table.Columns.Add("Code", typeof(string));
            table.Columns.Add("GhiChuKH", typeof(string));

            foreach (var item in query)
            {
                DataRow row = table.NewRow();
                row["TenKH"] = item.TenKH;
                row["Hieu"] = item.Hieu;
                row["Co"] = item.Co;
                row["DanhBa"] = danhBa;
                row["MLT1"] = item.MLT1;
                row["Code"] = item.CodeMoi;
                row["GhiChuKH"] = ghiChu;

                table.Rows.Add(row);
            }
            return table;
        }

        public DataTable GetInfoCheckCustomer1(string ghiChu, string danhBa)
        {
            var query = (from d in serverContext.DocSos
                         join k in serverContext.KhachHangs on d.DanhBa equals k.DanhBa
                         where d.DanhBa == danhBa
                         orderby d.DocSoID descending
                         select new { d.GhiChuDS, d.MLT1, d.CodeMoi, d.CSMoi, d.TieuThuMoi, d.DenNgay, k.TenKH, k.Hieu, k.Co, k.SoThan, k.HopDong, k.GB, k.DM, k.So, k.SoMoi, k.Duong, d.Ky, d.Nam, k.SDT }).Take(12).ToList();
            DataTable table = new DataTable();
            table.Columns.Add(TTKH_COLUMN_TENKH, typeof(string));
            table.Columns.Add(TTKH_COLUMN_HIEU, typeof(string));
            table.Columns.Add(TTKH_COLUMN_CO, typeof(string));
            table.Columns.Add(TTKH_COLUMN_SDT, typeof(string));
            table.Columns.Add(TTKH_COLUMN_DUONG, typeof(string));
            table.Columns.Add(TTKH_COLUMN_SOTHAN, typeof(string));
            table.Columns.Add(TTKH_COLUMN_MLT1, typeof(string));
            table.Columns.Add(TTKH_COLUMN_HOPDONG, typeof(string));
            table.Columns.Add(TTKH_COLUMN_DANHBA, typeof(string));
            table.Columns.Add(TTKH_COLUMN_GB, typeof(string));
            table.Columns.Add(TTKH_COLUMN_DM, typeof(string));
            table.Columns.Add(TTKH_COLUMN_KY, typeof(string));
            table.Columns.Add(TTKH_COLUMN_NAM, typeof(string));
            table.Columns.Add(TTKH_COLUMN_NGAYDOC, typeof(string));
            table.Columns.Add(TTKH_COLUMN_CODE, typeof(string));
            table.Columns.Add(TTKH_COLUMN_CSMOI, typeof(string));
            table.Columns.Add(TTKH_COLUMN_TIEUTHU, typeof(string));
            table.Columns.Add(TTKH_COLUMN_GHICHU, typeof(string));



            foreach (var item in query)
            {
                DataRow row = table.NewRow();
                row[TTKH_COLUMN_TENKH] = item.TenKH;
                row[TTKH_COLUMN_HIEU] = item.Hieu;
                row[TTKH_COLUMN_CO] = item.Co;
                row[TTKH_COLUMN_SDT] = item.SDT;
                row[TTKH_COLUMN_DUONG] = item.SoMoi.Trim().Length == 0 ?
                    String.Format("{0} {1}", item.So, item.Duong) :
                    String.Format("{0} {1}", item.SoMoi, item.Duong);
                row[TTKH_COLUMN_SOTHAN] = item.SoThan;
                row[TTKH_COLUMN_MLT1] = item.MLT1;
                row[TTKH_COLUMN_HOPDONG] = item.HopDong;
                row[TTKH_COLUMN_DANHBA] = danhBa;
                row[TTKH_COLUMN_GB] = item.GB;
                row[TTKH_COLUMN_DM] = item.DM;
                row[TTKH_COLUMN_KY] = item.Ky;
                row[TTKH_COLUMN_NAM] = item.Nam;
                row[TTKH_COLUMN_NGAYDOC] = item.DenNgay.Value.ToString(pattern);
                row[TTKH_COLUMN_CODE] = item.CodeMoi;
                row[TTKH_COLUMN_CSMOI] = item.CSMoi;
                row[TTKH_COLUMN_TIEUTHU] = item.TieuThuMoi;
                row[TTKH_COLUMN_GHICHU] = ghiChu;
                table.Rows.Add(row);
            }
            return table;

        }

        public bool ChuyenMaHoa(string code, string csm, string tieuThu, string ghiChuDS, string ghiChuMH, string ghiChuKH, string KHDS, DateTime ngayCapNhat, int nam, string ky, string dot, string danhBa)
        {
            serverContext = new DataClasses_thanleDataContext(ConnectionViewModel.Instance.ConnectionString);
            ConnectionViewModel.Instance.Connect();
            string query = "Update DocSo set GhiChuDS = '" + ghiChuDS + "', GhiChuTV = '" + ghiChuMH + "', GhiChuKH = '" + ghiChuKH + "', NgayCapNhat = '" + ngayCapNhat + "'," +
                " NVCapNhat = '" + MyUser.Instance.UserID + "' where docsoid = '" + nam + ky + danhBa + "' and Dot = '" + dot + "'";
            var value = ConnectionViewModel.Instance.GetExecuteNonQuerry(query);
            ConnectionViewModel.Instance.DisConnect();
            return value > 0;
        }
        public bool UpdateDocSo(string ttdhn, string code, string csm, string tieuThu, string ghiChuDS, string ghiChuMH, string ghiChuKH, string KHDS, DateTime ngayCapNhat, int nam, string ky, string dot, string danhBa)
        {
            serverContext = new DataClasses_thanleDataContext(ConnectionViewModel.Instance.ConnectionString);
            ConnectionViewModel.Instance.Connect();
            string query = "Update DocSo set CodeMoi = '" + code + "',CSMoi = " + csm + ",TieuThuMoi = " + tieuThu + ", GhiChuDS ='" + ghiChuDS + "', GhiChuTV ='" + ghiChuMH
                + "', GhiChuKH = '" + ghiChuKH + "', TTDHNMoi = '" + ttdhn + "' where docsoid = '" + nam + ky + danhBa + "' and Dot = '" + dot + "'";
            var value = ConnectionViewModel.Instance.GetExecuteNonQuerry(query);
            ConnectionViewModel.Instance.DisConnect();
            return value > 0;
        }

        public bool HoanTatDocSo()
        {
            ConnectionViewModel.Instance.Connect();
            string query = "update BillState set izds = '1' where billid = '" + MyUser.Instance.Year + MyUser.Instance.Month + MyUser.Instance.Date + "'";
            int value = Convert.ToInt16(ConnectionViewModel.Instance.GetExecuteNonQuerry(query));
            ConnectionViewModel.Instance.DisConnect();
            return value > 0;
        }
        public bool HoanTatThuongVu()
        {
            ConnectionViewModel.Instance.Connect();
            string query = "update BillState set iztv = '1' where billid = '" + MyUser.Instance.Year + MyUser.Instance.Month + MyUser.Instance.Date + "'";
            int value = Convert.ToInt16(ConnectionViewModel.Instance.GetExecuteNonQuerry(query));
            ConnectionViewModel.Instance.DisConnect();
            return value > 0;
        }
        public List<String> getDocsosByConditionCount(int year, String month, String date, int xGroup, String machine)
        {
            var getData = (from x in serverContext.DocSos
                           where x.DocSoID.StartsWith(year + month) && x.Dot == date && x.TODS == xGroup && x.May == machine
                           select x.DanhBa).ToList();
            return getData;
        }



        public bool getDocSosByDanhBa(String danhBa, int year, String month, String date, int xGroup, String machine)
        {

            bool result = false;

            DataClasses_thanleDataContext serverContext = new DataClasses_thanleDataContext(ConnectionViewModel.Instance.ConnectionString);
            var getData = (from x in serverContext.DocSos
                           where x.DocSoID.StartsWith(year + month) && x.Dot == date && x.TODS == xGroup && x.DanhBa == danhBa && x.May == machine
                           select x).ToList();

            DataClassesLocalDataContext localContext = new DataClassesLocalDataContext();
            foreach (var item in getData)
            {
                try
                {
                    localContext.DocSoLocals.InsertOnSubmit(new DocSoLocal()
                    {
                        DocSoID = item.DocSoID,
                        DanhBa = item.DanhBa,
                        MLT1 = item.MLT1,
                        MLT2 = item.MLT2,
                        SoNhaCu = item.SoNhaCu,
                        SoNhaMoi = item.SoNhaMoi,
                        Duong = item.Duong,
                        SDT = item.SDT,
                        GB = item.GB,
                        DM = item.DM,
                        Nam = item.Nam,
                        Ky = item.Ky,
                        Dot = item.Dot,
                        May = item.May,
                        TBTT = item.TBTT,
                        CSCu = item.CSCu,
                        CSMoi = item.CSMoi,
                        CodeCu = item.CodeCu,
                        CodeMoi = item.CodeMoi,
                        TTDHNCu = item.TTDHNCu,
                        TTDHNMoi = item.TTDHNMoi,
                        TieuThuCu = item.TieuThuCu,
                        TieuThuMoi = item.TieuThuMoi,
                        TuNgay = (DateTime)item.TuNgay,
                        DenNgay = (DateTime)item.DenNgay,
                        TienNuoc = item.TienNuoc,
                        BVMT = item.BVMT,
                        Thue = item.Thue,
                        TongTien = item.TongTien,
                        SoThanCu = item.SoThanCu,
                        SoThanMoi = item.SoThanMoi,
                        TODS = item.TODS,
                        HieuCu = item.HieuCu,
                        HieuMoi = item.HieuMoi,
                        ViTriCu = item.ViTriCu,
                        ViTriMoi = item.ViTriMoi,
                        GhiChuDS = item.GhiChuDS,
                        GIOGHI = (DateTime)item.GIOGHI
                    });

                    localContext.SubmitChanges();

                    var imageData = (from x in serverContext.HinhDHNs
                                     from y in serverContext.DocSos
                                     where x.DanhBo == y.DanhBa && x.CreateDate == y.GIOGHI && x.DanhBo == item.DanhBa
                                     select x).SingleOrDefault();
                    if (imageData != null)
                    {
                        localContext.HinhDHNLocals.InsertOnSubmit(new HinhDHNLocal()
                        {
                            ID = imageData.ID,
                            DanhBo = imageData.DanhBo,
                            Image = imageData.Image,
                            CreateDate = imageData.CreateDate
                        });
                        localContext.SubmitChanges();
                    }
                }
                catch
                {
                }
            }

            return result;
        }


        public void update(ItemCollection items)
        {
            foreach (var item in items)
            {
                DocSo docSo = item as DocSo;
                var v = (from x in serverContext.DocSos
                         where x.DanhBa == docSo.DanhBa && x.Nam == docSo.Nam && x.Ky == docSo.Ky && x.Dot == docSo.Dot
                         select x).FirstOrDefault();

                v.CodeMoi = docSo.CodeMoi;
                v.CSMoi = docSo.CSMoi;
                v.TieuThuMoi = docSo.TieuThuMoi;
                serverContext.SubmitChanges();


            }
        }
        public void update(DocSo docSo)
        {
            var v = (from x in serverContext.DocSos
                     where x.DanhBa == docSo.DanhBa && x.Nam == docSo.Nam && x.Ky == docSo.Ky && x.Dot == docSo.Dot
                     select x).FirstOrDefault();

            v.CodeMoi = docSo.CodeMoi;
            v.CSMoi = docSo.CSMoi;
            v.TieuThuMoi = docSo.TieuThuMoi;
            serverContext.SubmitChanges();

        }

        public List<ChuyenMayDS> getKH_ChuyenMayDS(string date, string machineLeft)
        {
            List<ChuyenMayDS> listData = new List<ChuyenMayDS>();
            var getData = (from x in localContext.DocSoLocals
                           where x.Dot == date && x.May == machineLeft
                           select new { x.DanhBa, x.MLT1, x.SoNhaCu, x.Duong }).ToList();
            foreach (var item in getData)
            {
                listData.Add(new ChuyenMayDS()
                {
                    DanhBa = item.DanhBa,
                    MLT = item.MLT1,
                    DiaChi = item.SoNhaCu + " " + item.Duong
                });
            }
            return listData;
        }



        public List<string> getDanhBasByCondition(int year, string month, string date, int xGroup, string machine)
        {
            var data = (from x in serverContext.DocSos
                        where x.DocSoID.StartsWith(year + month) && x.Dot == date && x.TODS == xGroup && x.May == machine && x.CSMoi > 0
                        select x.DanhBa).ToList();
            return data;
        }

        public DocSo getDocSoByDanhBa(string danhBa, int year, string month, string date, short xGroup, string machine)
        {
            var data = (from x in serverContext.DocSos
                        where x.DocSoID.StartsWith(year + month) && x.Dot == date && x.TODS == xGroup && x.May == machine && x.DanhBa == danhBa && x.CSMoi > 0
                        select x).FirstOrDefault();
            return data;
        }
        public List<DocSo> getAllDocSos(int year, string month, string date, int xGroup, string machine)
        {
            try
            {
                if (xGroup == 0)
                {
                    if (machine.Equals(ALL))
                    {
                        var data = (from x in serverContext.DocSos
                                    where x.DocSoID.StartsWith(year + month) && x.Dot == date
                                    select x).OrderBy(x => x.MLT1).ToList();
                        return data;
                    }
                    else
                    {
                        var data = (from x in serverContext.DocSos
                                    where x.DocSoID.StartsWith(year + month) && x.Dot == date && x.May == machine
                                    select x).OrderBy(x => x.MLT1).ToList();
                        return data;
                    }
                }
                else
                {
                    if (machine.Equals(ALL))
                    {
                        var data = (from x in serverContext.DocSos
                                    where x.DocSoID.StartsWith(year + month) && x.Dot == date && x.TODS == xGroup
                                    select x).OrderBy(x => x.MLT1).ToList();
                        return data;
                    }
                    else
                    {
                        var data = (from x in serverContext.DocSos
                                    where x.DocSoID.StartsWith(year + month) && x.Dot == date && x.May == machine
                                    select x).OrderBy(x => x.MLT1).ToList();
                        return data;
                    }
                }
            }
            catch
            {
                return new List<DocSo>();
            }
        }

        public byte[] getImageByDanhBa(string danhBa, string ky, int? nam)
        {
            //SqlConnection cnn = null;
            try
            {
                ConnectionViewModel.Instance.Connect();


                string query = "select [image] from DocSoTH_Hinh..HinhDHN  where HinhDHNID = '" + nam + ky.PadLeft(2, '0') + danhBa + "'";

                SqlDataReader reader = ConnectionViewModel.Instance.GetExecuteReader(query);
                //var data = tempServer.ExecuteQuery<byte[]>(query).FirstOrDefault();
                //(from x in tempServer.HinhDHNs
                // where x.DanhBo == danhBa && x.CreateDate == gioGhi
                // orderby x.CreateDate
                // select x.Image).FirstOrDefault();
                //if (data == null)
                //    return null;

                //return ((System.Data.Linq.Binary)data).ToArray();
                byte[] data = null;
                if (reader.Read())
                {
                    data = (byte[])reader[0];
                }
                ConnectionViewModel.Instance.DisConnect();
                return data;

            }
            catch
            {
                ConnectionViewModel.Instance.DisConnect();
            }
            try
            {
                ConnectionViewModel.Instance.DisConnect();
            }
            catch { }
            return null;

        }
        public ObservableCollection<DocSoLocal> getDistinctHoaDon(SoDaNhan selectedSoDaNhan)
        {
            ObservableCollection<DocSoLocal> listHoaDon = new ObservableCollection<DocSoLocal>();
            var hoaDons = (from x in localContext.DocSoLocals
                           where x.Nam == selectedSoDaNhan.nam && x.Ky == selectedSoDaNhan.ky && x.Dot == selectedSoDaNhan.dot && x.May == selectedSoDaNhan.may
                           select x).ToList();
            foreach (var item in hoaDons)
                listHoaDon.Add(item);
            return listHoaDon;
        }

        public XuLyCode8 GetXuLyCode8(string danhba)
        {
            var data = serverContext.ExecuteQuery<XuLyCode8>("declare @NgayDoc datetime select top 1 @NgayDoc = DenNgay from DocSo where DanhBa = '" + danhba + "' order by DocSoID desc Select top 1 csgo,csgan, DATEDIFF(DAY, NgayThay, @NgayDoc) as SoNgay from BaoThay where DanhBa ='" + danhba + "' and NgayThay is not null  order by BaoThayID desc").First();
            return data;
        }
        public int GetTieuThuMoi(string danhBa, string docSoID)
        {
            var data = serverContext.ExecuteQuery<int>("Select top 1 TieuThuMoi from DocSo where DanhBa='" + danhBa + "' and DocSoID <> " + docSoID + " order by DocSoID desc").First();
            return data;
        }
        public bool WriteSoDaNhan(int year, String month, String date, String machine, int count, int xGroup)
        {
            DataClassesLocalDataContext localDataContext = new DataClassesLocalDataContext();
            try
            {

                localDataContext.SoDaNhans.InsertOnSubmit(new SoDaNhan()
                {
                    So = year + "_" + month + "_" + date + "_" + machine,
                    SoLuong = count,
                    nam = year,
                    ky = month,
                    dot = date,
                    may = machine,
                    ToDS = xGroup
                });
                localDataContext.SubmitChanges();
                return true;
            }
            catch
            {

            }
            return false;
        }

        public bool getDocSosByCondition(int year, String month, String date, int xGroup)
        {

            bool result = false;

            DataClasses_thanleDataContext serverContext = new DataClasses_thanleDataContext(ConnectionViewModel.Instance.ConnectionString);
            var getData = (from x in serverContext.DocSos
                           where x.DocSoID.StartsWith(year + month) && x.Dot == date && x.TODS == xGroup
                           select x).ToList();

            DataClassesLocalDataContext localContext = new DataClassesLocalDataContext();
            foreach (var item in getData)
            {
                try
                {
                    localContext.DocSoLocals.InsertOnSubmit(new DocSoLocal()
                    {
                        DocSoID = item.DocSoID,
                        DanhBa = item.DanhBa,
                        MLT1 = item.MLT1,
                        MLT2 = item.MLT2,
                        SoNhaCu = item.SoNhaCu,
                        SoNhaMoi = item.SoNhaMoi,
                        Duong = item.Duong,
                        SDT = item.SDT,
                        GB = item.GB,
                        DM = item.DM,
                        Nam = item.Nam,
                        Ky = item.Ky,
                        Dot = item.Dot,
                        May = item.May,
                        TBTT = item.TBTT,
                        CSCu = item.CSCu,
                        CSMoi = item.CSMoi,
                        CodeCu = item.CodeCu,
                        CodeMoi = item.CodeMoi,
                        TTDHNCu = item.TTDHNCu,
                        TTDHNMoi = item.TTDHNMoi,
                        TieuThuCu = item.TieuThuCu,
                        TieuThuMoi = item.TieuThuMoi,
                        TuNgay = (DateTime)item.TuNgay,
                        DenNgay = (DateTime)item.DenNgay,
                        TienNuoc = item.TienNuoc,
                        BVMT = item.BVMT,
                        Thue = item.Thue,
                        TongTien = item.TongTien,
                        SoThanCu = item.SoThanCu,
                        SoThanMoi = item.SoThanMoi,
                        HieuCu = item.HieuCu,
                        HieuMoi = item.HieuMoi,
                        ViTriCu = item.ViTriCu,
                        ViTriMoi = item.ViTriMoi,
                        GhiChuDS = item.GhiChuDS,
                        GIOGHI = (DateTime)item.GIOGHI
                    });

                    localContext.SubmitChanges();

                    var imageData = (from x in serverContext.HinhDHNs
                                     from y in serverContext.DocSos
                                     where x.DanhBo == y.DanhBa && x.CreateDate == y.GIOGHI && x.DanhBo == item.DanhBa
                                     select x).SingleOrDefault();
                    if (imageData != null)
                    {
                        localContext.HinhDHNLocals.InsertOnSubmit(new HinhDHNLocal()
                        {
                            ID = imageData.ID,
                            DanhBo = imageData.DanhBo,
                            Image = imageData.Image,
                            CreateDate = imageData.CreateDate
                        });
                        localContext.SubmitChanges();
                    }
                }
                catch
                {
                }
            }

            return result;
        }

        internal List<String> getTenKH(string danhBa)
        {
            var data = (from x in serverContext.KhachHangs
                        where x.DanhBa == danhBa
                        select new { x.TenKH, x.HopDong }).FirstOrDefault();
            if (data != null)
                return new List<string>() { data.TenKH, data.HopDong };
            else return new List<string>();
        }

        public IEnumerable<object> getDistinctKHDS()
        {
            var data = (from x in serverContext.TTDHNs
                        orderby x.Stt
                        select new { x.TTDHN1, x.CODE }).ToList();
            return data;
        }

        public MySoLenh getSoLenh(string danhBa)
        {
            var data = (from b in serverContext.ThongBaos
                        join t in serverContext.ThamSos on b.LoaiLenh.ToString() equals t.Code
                        where b.DanhBa == danhBa && t.CodeType == "TB"
                        orderby b.ID descending
                        select new
                        {
                            b.SoLenh,
                            t.CodeDesc,
                            b.ChiSo,
                            b.NgayKiem,
                            b.NoiDung,
                            b.Hieu,
                            b.Co,
                            b.NgayCapNhat
                        }).FirstOrDefault();
            if (data == null)
                return null;
            return new MySoLenh(danhBa, data.CodeDesc, data.ChiSo + "", data.NgayKiem.Value.ToString(pattern),
                data.NoiDung, data.Hieu, data.Co + "", data.NgayCapNhat.Value.ToString(pattern));
        }
        public string GetNVGHI(int? nam, string ky, string dot, string may)
        {
            var data = (from l in serverContext.LichSuDs
                        where l.Nam == nam && l.Ky == ky && l.Dot == dot && l.May == may
                        select l.NhanVienDS).FirstOrDefault();
            return data;
        }
        public MyBaoThay getBaoThay(string danhBa)
        {
            var data = (from b in serverContext.BaoThays
                        join t in serverContext.ThamSos on b.LoaiBT.ToString() equals t.Code
                        where b.DanhBa == danhBa && t.CodeType == "BT"
                        select new
                        {
                            danhBa,
                            t.CodeDesc,
                            b.CSGo,
                            b.CSGan,
                            b.SoThanMoi,
                            b.NgayThay,
                            b.NgayCapNhat
                        }).FirstOrDefault();
            if (data == null)
                return null;
            return new MyBaoThay(data.danhBa, data.CodeDesc, data.CSGo + "", data.CSGan + "",
                data.SoThanMoi, data.NgayThay.Value.ToString(pattern), data.NgayCapNhat.Value.ToString(pattern));
        }

        public ObservableCollection<int> getDistinctYear()
        {
            ObservableCollection<int> lstYear = new ObservableCollection<int>();
            var items = (from x in localContext.DocSoLocals
                         select x.Nam).Distinct().ToList();
            foreach (int item in items)
                lstYear.Add(item);
            return lstYear;
        }
        class Item
        {
            public int Year { get; set; }
            public int Month { get; set; }
        }
        public ObservableCollection<DocSo_1Ky> get12Months(int year, string Month, string danhBa)
        {
            int count = 0;
            int month = Int16.Parse(Month);
            List<Item> r = new List<Item>();
            month++;
            DateTime dateTime = new DateTime(year, month, 1);
            while (count < 13)
            {
                dateTime = dateTime.AddMonths(-1);

                r.Add(new Item()
                {
                    Year = dateTime.Year,
                    Month = dateTime.Month
                });
                count++;
            }
            var whereStr = " (";
            foreach (var s in r)
            {
                whereStr += String.Format(" '{0}' ,", s.Year + "" + s.Month.ToString("00") + danhBa);
            }
            whereStr = whereStr.Substring(0, whereStr.Length - 3);
            whereStr += "')";

            String query = String.Format("select docsoID, denngay, codemoi, csmoi, tieuthumoi from Docso where docsoid in " + " {0} " +
                "order by nam desc, ky desc", whereStr);
            List<DocSo_1Ky> datas = serverContext.ExecuteQuery<DocSo>(query).Select(data => new DocSo_1Ky(data.DocSoID.Substring(4, 2) + "/" + data.DocSoID.Substring(0, 4), data.DenNgay.GetValueOrDefault().ToString(pattern), data.CodeMoi, data.CSMoi + "", data.TieuThuMoi + "")).ToList();
            ObservableCollection<DocSo_1Ky> listDocSo = new ObservableCollection<DocSo_1Ky>(datas);
            return listDocSo;
        }
        public DataTable getDistinctYearMonthDateServer()
        {
            try
            {
                var query = "select distinct nam,ky, dot from docso";
                ConnectionViewModel.Instance.Connect();
                DataTable table = ConnectionViewModel.Instance.GetDataTable(query);
                return table;
            }
            catch (Exception e)
            {

                System.Windows.Forms.MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + e.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            finally
            {
                ConnectionViewModel.Instance.DisConnect();
            }
            return null;
        }
        public ObservableCollection<int> getDistinctYearServer()
        {
            ObservableCollection<int> lstYear = new ObservableCollection<int>();
            try
            {
                var items = (from x in serverContext.DocSos
                             select x.Nam).Distinct().OrderByDescending(x => x).ToList();
                foreach (int item in items)
                    lstYear.Add(item);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + e.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return lstYear;
        }
        public ObservableCollection<String> getDistinctMonthServer(int year)
        {
            ObservableCollection<String> lstMonth = new ObservableCollection<String>();
            try
            {
                var items = (from x in serverContext.DocSos
                             where x.DocSoID.StartsWith(year + "")
                             select x.Ky).Distinct().ToList();
                foreach (String item in items)
                    lstMonth.Add(item);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + e.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return lstMonth;
        }
        public ObservableCollection<String> getDistinctDate()
        {
            ObservableCollection<String> lstDate = new ObservableCollection<String>();
            try
            {
                var items = (from x in localContext.DocSoLocals
                             select x.Dot).Distinct().ToList();
                foreach (String item in items)
                    lstDate.Add(item);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + e.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return lstDate;
        }
        public ObservableCollection<String> getDistinctDateServer(int year, String month)
        {
            ObservableCollection<String> lstDate = new ObservableCollection<String>();
            try
            {
                var items = (from x in serverContext.DocSos
                             where x.DocSoID.StartsWith(year + month)
                             select x.Dot).Distinct().ToList();
                foreach (String item in items)
                    lstDate.Add(item);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + e.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return lstDate;
        }
        public ObservableCollection<int> getDistinctGroup()
        {
            ObservableCollection<int> lstGroup = new ObservableCollection<int>();
            try
            {
                var items = (from x in localContext.DocSoLocals
                             select x.TODS).Distinct().ToList();
                foreach (int item in items)
                    lstGroup.Add(item);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + e.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return lstGroup;
        }
        public ObservableCollection<String> getDistinctMachine()
        {
            ObservableCollection<String> lstGroup = new ObservableCollection<String>();
            try
            {
                var items = (from x in localContext.DocSoLocals
                             select x.May).Distinct().ToList();
                foreach (String item in items)
                    lstGroup.Add(item);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + e.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return lstGroup;
        }
        public ObservableCollection<int> getDistinctGroupServer(int year, String month, String date)
        {
            ObservableCollection<int> lstGroup = new ObservableCollection<int>();
            try
            {
                var items = (from x in serverContext.DocSos
                             where x.DocSoID.StartsWith(year + month) && x.Dot == date
                             select x.TODS).Distinct().ToList();
                foreach (int item in items)
                    lstGroup.Add(item);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + e.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return lstGroup;
        }
        public List<String> getDistinctMachineServer(int year, String month, String date, int xGroup)
        {
            try
            {
                string query = "";
                if (xGroup == 0)
                    query = "select distinct may from docso where docsoid like '" + year + month + "%' and dot = '" + date + "'";
                //items = (from x in serverContext.DocSos
                //         where x.DocSoID.StartsWith(year + month) && x.Dot == date
                //         select x.May).Distinct().ToList();
                else
                    query = "select distinct may from docso where docsoid like '" + year + month + "%' and dot = '" + date + "' and tods =" + xGroup;
                //items = (from x in serverContext.DocSos
                //             where x.DocSoID.StartsWith(year + month) && x.Dot == date && x.TODS == xGroup
                //             select x.May).Distinct().ToList();
                var mayList = serverContext.ExecuteQuery<string>(query).ToList();
                mayList.Add(ALL);
                return mayList;

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + e.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return new List<string>();
        }
        public ObservableCollection<SoDaNhan> getDistinctSoDaNhan(int year, String month, String date, int xGroup)
        {
            ObservableCollection<SoDaNhan> listSoDaNhan = new ObservableCollection<SoDaNhan>();
            try
            {
                var soDaNhans = (from x in localContext.SoDaNhans
                                 where x.nam == year && x.ky == month && x.dot == date && x.ToDS == xGroup
                                 select x).ToList();
                foreach (var item in soDaNhans)
                    listSoDaNhan.Add(item);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + e.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return listSoDaNhan;
        }

        #region Báo thay
        #region Báo thay định kỳ
        public DataTable BaoThay_BaoThayDinhKy_LoadPhuong()
        {
            try
            {
                string sqlStatement = "Select Quan + Phuong as 'MaPhuongQuan',TenPhuong from Phuong";
                ConnectionViewModel.Instance.Connect();
                DataTable table = ConnectionViewModel.Instance.GetDataTable(sqlStatement);
                return table;
            }
            catch
            {

            }
            finally
            {
                ConnectionViewModel.Instance.DisConnect();
            }
            return null;
        }
        public DataTable BaoThay_BaoThayDinhKy_LoadNam()
        {
            try
            {
                string sqlStatement = "select Distinct(YEAR(NgayGan)) as NamGan from KhachHang order by NamGan desc";
                ConnectionViewModel.Instance.Connect();
                DataTable table = ConnectionViewModel.Instance.GetDataTable(sqlStatement);
                return table;
            }
            catch
            {

            }
            finally
            {
                ConnectionViewModel.Instance.DisConnect();
            }
            return null;
        }
        public DataTable BaoThay_BaoThayDinhKy_LoadDanhMucBaoThay(DateTime dateTime)
        {
            try
            {
                string sqlStatement = "select distinct b.DanhBa ,k.MLT2 ,k.TenKH ,k.So + ' ' + k.Duong as DiaChi,Convert(varchar(10), tb.NgayKiem, 103) as NgayBao, " +
                    "k.Hieu as HieuCu ,  k.Co as CoCu ,k.ChiThan as ChiThanCu ,k.ChiCo as ChiCoCu , k.SoThan as SoThanCu ,tb.ChiSo as ChiSoBao ,tb.LoaiLenh as SoBKThay, " +
                    "b.HieuMoi, b.CoMoi, b.ChiThanMoi, b.ChiCoMoi, b.SoThanMoi from BaoThay b inner join  KhachHang k on b.DanhBa = k.DanhBa" +
                    " inner join Thongbao tb on tb.DanhBa = k.DanhBa where b.NgayCapNhat > '" + dateTime + "'";
                ConnectionViewModel.Instance.Connect();
                DataTable table = ConnectionViewModel.Instance.GetDataTable(sqlStatement);
                return table;
            }
            catch
            {

            }
            finally
            {
                ConnectionViewModel.Instance.DisConnect();
            }
            return null;
        }
        public DataTable BaoThay_BaoThayDinhKy_LoadDanhSachLoc(string sobk, string dkloc, string nam, string thang, bool isLocTheoNgayGan, string coDHN)
        {
            try
            {
                string sqlStatement = "SELECT distinct(k.DanhBa),k.Dot, k.TenKH, '" + sobk + "' as SoBKThay, CASE WHEN k.SoMoi IS NOT NULL AND LEN(k.SoMoi) > 1" +
                    " THEN k.So + '('+ k.SoMoi +')' + k.Duong ELSE So + ' ' + k.Duong END AS DiaChi, k.Hieu, k.Co, k.SoThan, k.NgayGan,k.NgayKD,Year(k.NgayGan) as Nam," +
                    " k.May, k.MLT2, k.ChiThan, k.ChiCo,k.ViTri, k.ChiCo as ChiGoc FROM KhachHang k,DocSo d WHERE k.DanhBa = d.DanhBa AND k.HieuLuc = 1 AND k.TTBaoThay = 0 " +
                    "AND k.Co = '" + coDHN + "' AND Left(d.CodeMoi,1) not in ('6','K','N') ";
                if (isLocTheoNgayGan)
                {
                    sqlStatement = sqlStatement + "AND YEAR(k.NgayGan) " + dkloc + " '" + nam + "' ";
                    if (thang != "")
                        sqlStatement = sqlStatement + "AND MONTH(k.NgayGan) = '" + thang + "' ";
                }
                else
                {
                    sqlStatement = sqlStatement + "AND YEAR(k.NgayKD) " + dkloc + " '" + nam + "' ";
                    if (thang != "")
                        sqlStatement = sqlStatement + "AND MONTH(k.NgayKD) = '" + thang + "' ";
                }
                //if (str != "")
                //    sqlStatement = sqlStatement + "AND Quan = '" + str.Substring(0, 2) + "' AND Phuong = '" + str.Substring(2, 2) + "' ";
                ConnectionViewModel.Instance.Connect();
                DataTable table = ConnectionViewModel.Instance.GetDataTable(sqlStatement);
                return table;
            }
            catch
            {

            }
            finally
            {
                ConnectionViewModel.Instance.DisConnect();
            }
            return null;
        }
        public bool BaoThay_BaoThayDinhKy_BaoThay(DataTable table, string csGo, string csGan, string ngayThay)
        {
            bool result = true;
            try
            {
                if (table.Rows.Count == 0)
                    return false;
                foreach (DataRow row in table.Rows)
                {
                    string danhBa = row["DanhBa"].ToString();
                    string hieu = row["Hieu"].ToString();
                    string co = row["Co"].ToString();
                    string soThan = row["SoThan"].ToString();

                    string viTri = row["ViTri"].ToString();
                    string loaiBT = "1";
                    string soBK = row["SoBKThay"].ToString();
                    string maChiGoc = row["ChiGoc"].ToString();
                    string maChiThan = row["ChiThan"].ToString();
                    string id = MyUser.Instance.Year + MyUser.Instance.Month + danhBa;

                    ConnectionViewModel.Instance.Connect();
                    string query = "select top 1 CSMoi from DocSo where DanhBa = '" + danhBa + "' and Nam = " + MyUser.Instance.Year + " and DocSoID <> " + id + " order by DocSoID desc";
                    //string csGo = Convert.ToInt32(ConnectionViewModel.Instance.GetExecuteScalar(query)).ToString();
                    ConnectionViewModel.Instance.DisConnect();


                    ConnectionViewModel.Instance.Connect();
                    //query = "insert into BaoThay " +
                    //    "values((select top 1 baothayid+1 from baothay order by BaoThayID desc),'" + danhBa + "'," + loaiBT + ",'" + ngayThay + "','" + hieu + "','" + co + "','" + soThan + "','" + viTri + "','"
                    //    + maChiThan + "','" + maChiGoc + "','" + csGo + "','" + csGan + "',GetDate(),'" + MyUser.Instance.UserID + "')";
                    query = "insert into BaoThay " +
                     "values('" + danhBa + "'," + loaiBT + ",'" + ngayThay + "','" + hieu + "','" + co + "','" + soThan + "','" + viTri + "','"
                     + maChiThan + "','" + maChiGoc + "','" + csGo + "','" + csGan + "',GetDate(),'" + MyUser.Instance.UserID + "')";
                    ConnectionViewModel.Instance.GetExecuteReader(query);
                    ConnectionViewModel.Instance.DisConnect();

                    ConnectionViewModel.Instance.Connect();
                    query = "Update KhachHang set TTBaoThay = 1 where DanhBa = '" + danhBa + "'";
                    ConnectionViewModel.Instance.GetExecuteReader(query);
                    ConnectionViewModel.Instance.DisConnect();

                }
            }
            catch (Exception e)
            {
                result = false;
                MessageBox.Show(e.ToString());
            }
            finally
            {
                ConnectionViewModel.Instance.DisConnect();
            }
            return result;
        }
        #endregion
        #region Hoàn công

        public DataTable BaoThay_HoanCong_LoadDanhSachBao(DateTime dateTime)
        {
            try
            {
                string sqlStatement = "select BaoThayID, ROW_NUMBER() OVER(ORDER BY SoBKThay) as 'STT', k.DanhBa 'Danh bạ', k.MLT2 'Mã LT'," +
                    "k.TenKH 'Tên KH', k.So +' '+ k.Duong 'Địa chỉ',  Convert(varchar(10),NgayBao,103) 'Ngày báo', HieuCu 'Hiệu cũ', CoCu 'Cỡ cũ'," +
                    "s.CodeDesc 'Loại BT', ChiThanCu 'CT cũ', ChiCoCu 'CC cũ',ViTriCu 'VT cũ',SoThanCu 'Số thân cũ',  CSBao 'CS báo'," +
                    " SoBKThay 'Số BK',ToID 'Tổ đọc số' from BaoThay b inner join KhachHang k on b.DanhBa = k.DanhBa " +
                    "inner join ThamSo s on b.LoaiBT = s.Code where (SoBKHoanCong is Null or SoBKHoanCong = '') and (TroNgai = 0 or TroNgai is Null) " +
                    "and (HuyBaoThay = 0 or HuyBaoThay is Null)  and s.CodeType = 'BT'  and convert(date,NgayBao) = convert(date,'" + dateTime + "')";
                ConnectionViewModel.Instance.Connect();
                DataTable table = ConnectionViewModel.Instance.GetDataTable(sqlStatement);
                return table;
            }
            catch
            {

            }
            finally
            {
                ConnectionViewModel.Instance.DisConnect();
            }
            return null;
        }

        #endregion
        #region Nhap Thong Bao
        public DataTable BaoThay_NhapThongBao_TimKiemKhachHang(string loaiTK, string doituongTK)
        {
            string sqlStatment = "select k.DanhBa, k.TenKH, k.HopDong, k.MLT2, k.So, k.SoMoi, k.Duong, k.Hieu, k.Co, k.SoThan, k.ChiThan, k.ChiCo,m.ToID,k.ViTri from KhachHang k inner join MayDS m on k.May = m.May where " + loaiTK + " = '" + doituongTK + "'";
            DataTable table = null;
            try
            {
                ConnectionViewModel.Instance.Connect();
                table = ConnectionViewModel.Instance.GetDataTable(sqlStatment);
            }
            catch
            {

            }
            finally
            {
                ConnectionViewModel.Instance.DisConnect();
            }

            return table;
        }
        public int BaoThay_NhapThongBao_LayChiSo(string danhBa, string docSoID)
        {
            try
            {
                string sqlStatement = "select top 1 CSMoi from DocSo where DanhBa = '" + danhBa + "'  and DocSoID <> '" + docSoID + "' order by DocSoID desc";
                ConnectionViewModel.Instance.Connect();
                int num = Convert.ToInt32(ConnectionViewModel.Instance.GetExecuteScalar(sqlStatement));
                return num;
            }
            catch
            {

            }
            finally
            {
                ConnectionViewModel.Instance.DisConnect();
            }


            return 0;
        }
        public int BaoThay_NhapThongBao_LaySoTB()
        {
            try
            {
                string sqlStatement = "select convert(int,MAX(Stt)) from ThamSo where CodeType = 'TB'";
                ConnectionViewModel.Instance.Connect();
                int num = Convert.ToInt32(ConnectionViewModel.Instance.GetExecuteScalar(sqlStatement));
                return num;
            }
            catch
            {

            }
            finally
            {
                ConnectionViewModel.Instance.DisConnect();
            }


            return 0;
        }
        public DataTable BaoThay_NhapThongBao_LoadCoDHN()
        {
            try
            {
                string sqlStatement = "select Code, CodeDesc from ThamSo where CodeType = 'Co' order by Stt";
                ConnectionViewModel.Instance.Connect();
                DataTable table = ConnectionViewModel.Instance.GetDataTable(sqlStatement);
                return table;
            }
            catch
            {

            }
            finally
            {
                ConnectionViewModel.Instance.DisConnect();
            }


            return null;
        }
        public bool BaoThay_NhapThongBao_ThemCoDHN(string maCo, string coDHN)
        {
            try
            {
                ConnectionViewModel.Instance.Connect();
                string sqlStatement = "select convert(int,MAX(Stt)) from ThamSo where CodeType = 'Co'";
                int stt = Convert.ToInt32(ConnectionViewModel.Instance.GetExecuteScalar(sqlStatement));
                ConnectionViewModel.Instance.DisConnect();

                sqlStatement = "Insert into ThamSo values('" + maCo + "', '" + coDHN + "', '" + "Co" + "', " + (object)(stt + 1) + ")";
                ConnectionViewModel.Instance.Connect();
                int result = ConnectionViewModel.Instance.GetExecuteNonQuerry(sqlStatement);

                ConnectionViewModel.Instance.DisConnect();
                return result > 0;
            }
            catch
            {

            }
            finally
            {
                ConnectionViewModel.Instance.DisConnect();
            }


            return false;
        }
        public bool BaoThay_NhapThongBao_ThemHieuDHN(string maHieu, string hieuDHN)
        {
            try
            {
                ConnectionViewModel.Instance.Connect();
                string sqlStatement = "select convert(int,MAX(Stt)) from ThamSo where CodeType = 'Hieu'";
                int stt = Convert.ToInt32(ConnectionViewModel.Instance.GetExecuteScalar(sqlStatement));
                ConnectionViewModel.Instance.DisConnect();

                sqlStatement = "Insert into ThamSo values('" + maHieu + "',N'" + hieuDHN + "','" + "Hieu" + "'," + (object)(stt + 1) + ")";
                ConnectionViewModel.Instance.Connect();
                int result = ConnectionViewModel.Instance.GetExecuteNonQuerry(sqlStatement);

                ConnectionViewModel.Instance.DisConnect();
                return result > 0;
            }
            catch
            {

            }
            finally
            {
                ConnectionViewModel.Instance.DisConnect();
            }


            return false;
        }
        public bool BaoThay_NhapThongBao_ThemLoaiTB(string maTB, string TB)
        {
            try
            {
                string sqlStatement = "Insert into ThamSo values('" + maTB + "',N'" + TB + "','" + "TB" + "'," + maTB + ")";
                ConnectionViewModel.Instance.Connect();
                int result = ConnectionViewModel.Instance.GetExecuteNonQuerry(sqlStatement);

                ConnectionViewModel.Instance.DisConnect();
                return result > 0;
            }
            catch
            {

            }
            finally
            {
                ConnectionViewModel.Instance.DisConnect();
            }


            return false;
        }
        public bool BaoThay_NhapThongBao_ThemThongBao(string danhBa, string soLenh, string tieuDe, string hieu, string co, int chiSo, string noiDung, string soThan, string ngayKiem, string ngayCapNhat)
        {
            try
            {


                string sqlStatement = "Insert into ThongBao Values((SELECT TOP 1 id  FROM thongbao  order by id desc) + 1," +
                    "'" + tieuDe + "','" + danhBa + "','" + soLenh + "','" + hieu + "'," + co + "," +
                    chiSo + ",N'" + noiDung + "','" + ngayKiem + "','" + ngayCapNhat + "','" + MyUser.Instance.UserID + "','" + soThan + "')";
                ConnectionViewModel.Instance.Connect();
                int result = ConnectionViewModel.Instance.GetExecuteNonQuerry(sqlStatement);

                ConnectionViewModel.Instance.DisConnect();
                return result > 0;
            }
            catch
            {

            }
            finally
            {
                ConnectionViewModel.Instance.DisConnect();
            }


            return false;
        }

        public bool BaoThay_NhapThongBao_CapNhatThongBao(string id, string danhBa, string soLenh, string tieuDe, string hieu, string co, int chiSo, string noiDung, string soThan, string ngayKiem, string ngayCapNhat)
        {
            try
            {


                string sqlStatement = "Update ThongBao Set LoaiLenh ='" + tieuDe + "',DanhBa ='" + danhBa + "',SoLenh ='" + soLenh + "',Hieu ='" + hieu +
                    "',Co = " + co + ",ChiSo = " + chiSo + ",NoiDung = N'" + noiDung + "',NgayKiem = '" + ngayKiem + "', NgayCapNhat = '" + ngayCapNhat +
                    "',NVCapNhat = '" + MyUser.Instance.UserID + "',SoThan = '" + soThan + "' where ID = '" + id + "'";
                ConnectionViewModel.Instance.Connect();
                int result = ConnectionViewModel.Instance.GetExecuteNonQuerry(sqlStatement);

                ConnectionViewModel.Instance.DisConnect();
                return result > 0;
            }
            catch
            {

            }
            finally
            {
                ConnectionViewModel.Instance.DisConnect();
            }


            return false;
        }
        public DataTable BaoThay_NhapThongBao_LoadDanhSach(string ngayCapNhat)
        {
            try
            {
                string sqlStatement = "SELECT distinct ID,SoLenh,t.DanhBa,k.TenKH, k.So + ' ' + k.Duong AS DiaChi, t.NoiDung,t.ChiSo,k.MLT2,k.HopDong,t.Hieu,t.SoThan," +
                    "t.Co,ts.CodeDesc as LoaiLenh, CONVERT(VARCHAR(10),t.NgayKiem,103) AS NgayKiem, CONVERT(VARCHAR(10),t.NgayCapNhat,103) AS " +
                    "NgayCapNhat FROM ThongBao t INNER JOIN KhachHang k ON t.DanhBa = k.DanhBa INNER JOIN ThamSo ts ON t.LoaiLenh = ts.Code WHERE " +
                    "NgayCapNhat = '" + ngayCapNhat + "' AND ts.CodeType = 'TB'";
                ConnectionViewModel.Instance.Connect();
                DataTable table = ConnectionViewModel.Instance.GetDataTable(sqlStatement);
                return table;
            }
            catch
            {

            }
            finally
            {
                ConnectionViewModel.Instance.DisConnect();
            }
            return null;
        }
        public DataTable BaoThay_NhapThongBao_LoadTieuDe()
        {
            try
            {
                string sqlStatement = "SELECT Code,CodeDesc FROM ThamSo WHERE CodeType = 'TB' ORDER BY Stt";
                ConnectionViewModel.Instance.Connect();
                DataTable table = ConnectionViewModel.Instance.GetDataTable(sqlStatement);
                return table;
            }
            catch
            {

            }
            finally
            {
                ConnectionViewModel.Instance.DisConnect();
            }
            return null;
        }
        public DataTable BaoThay_NhapThongBao_LoadHieu()
        {
            try
            {
                string sqlStatement = "select Code, CodeDesc from ThamSo where CodeType = 'Hieu' order by Stt";
                ConnectionViewModel.Instance.Connect();
                DataTable table = ConnectionViewModel.Instance.GetDataTable(sqlStatement);
                return table;
            }
            catch
            {

            }
            finally
            {
                ConnectionViewModel.Instance.DisConnect();
            }
            return null;
        }
        #endregion

        #region Lich su DHN
        public DataTable BaoThay_LichSuDHN_TimKiemKhachHang(string danhBa)
        {
            string sqlStatment = "select* from baothay where DanhBa = '" + danhBa + "'";
            DataTable table = null;
            try
            {
                ConnectionViewModel.Instance.Connect();
                table = ConnectionViewModel.Instance.GetDataTable(sqlStatment);
            }
            catch
            {

            }
            finally
            {
                ConnectionViewModel.Instance.DisConnect();
            }

            return table;
        }
        #endregion

        #endregion

    }
}

