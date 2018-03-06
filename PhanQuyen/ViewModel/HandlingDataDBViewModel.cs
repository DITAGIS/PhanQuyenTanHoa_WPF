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

namespace ViewModel
{
    public class HandlingDataDBViewModel
    {


        private static HandlingDataDBViewModel _instance;
        private DataClassesLocalDataContext localContext;
        private DataClassServerDataContext serverContext;

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

        private const String TKSAUDOCSO_COLUMN_TOID = "ToDS";
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


        public List<CapNhatHoaDon> GetCapNhatHoaDon(string month, int year)
        {
            List<CapNhatHoaDon> data = new List<CapNhatHoaDon>(); ;
            try
            {
                data = serverContext.ExecuteQuery<CapNhatHoaDon>("SELECT Dot , COUNT(DANHBA) as SoDanhBa, SUM(TieuThu) as TieuThu FROM HoaDon WHERE KY = " + month + " and NAM = '" + year + "' GROUP BY DOT ORDER BY DOT")
                    //.Select(x => new CapNhatHoaDon()
                    //{
                    //    Dot = x.Dot,
                    //    SoDanhBa = x.SoDanhBa,
                    //    TieuThu = x.TieuThu
                    //})
                    .ToList();
            }
            catch (Exception e)
            {

            }
            return data;

        }



        public int CheckIzDS()
        {
            return serverContext.ExecuteQuery<int>("select count(*) from BillState where izDS = '1' and BillID ='" + MyUser.Instance.Year + MyUser.Instance.Month + MyUser.Instance.Date + "'").First();
        }

        private HandlingDataDBViewModel()
        {
            localContext = new DataClassesLocalDataContext();
            serverContext = new DataClassServerDataContext();
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
            return 0;
        }
        public void InsertHoaDon(ViewModel.HoaDon hoaDon)
        {
            try
            {
                //var query = "insert into HoaDon values('" + hoaDon.HoaDonID + "'," + hoaDon.Nam + ",'" + hoaDon.Ky + "','" + hoaDon.Dot + "','" + hoaDon.DanhBa + "','" + hoaDon.TenKH + "','" + hoaDon.So + "','" + hoaDon.Duong + "'," + hoaDon.GB + "," + hoaDon.DM
                //    + ",'" + hoaDon.Code + "'," + hoaDon.CSCu + "," + hoaDon.CSMoi + "," + hoaDon.TieuThu + ",'" + hoaDon.TuNgay + "','" + hoaDon.DenNgay + "','" +
                //    hoaDon.SoHoaDon + "','" + hoaDon.NgayCapNhat + "','" + hoaDon.NVCapNhat + "'";

                //var insert = serverContext.ExecuteQuery<object>(query).ToList();
                if (!serverContext.HoaDons.Contains(hoaDon))
                {
                    serverContext.HoaDons.InsertOnSubmit(hoaDon);

                    serverContext.SubmitChanges();
                }
                else
                {
                    UpdateHoaDon(hoaDon);
                }
            }
            catch
            {
                try
                {
                    serverContext.SubmitChanges();
                }
                catch
                {
                    
                }
            }
        }
        private void UpdateHoaDon(ViewModel.HoaDon hoaDon)
        {
            try
            {
                //var query = "Update HoaDon set Nam ='" + hoaDon.Nam + "',Ky = '" + hoaDon.Ky + "',Dot = '" + hoaDon.Dot + "',DanhBa = '" + hoaDon.DanhBa + "',TenKH = '" + hoaDon.TenKH
                //    + "',So = '" + hoaDon.So + "',Duong = '" + hoaDon.Duong + "',GB = '" + hoaDon.GB + "',DM = '" + hoaDon.DM + "',Code = '" + hoaDon.Code + "',CSCu ='" + hoaDon.CSCu +
                //    "',CSMoi ='" + hoaDon.CSMoi + "',TieuThu ='" + hoaDon.TieuThu + "',TuNgay = '" + hoaDon.TuNgay + "',DenNgay = '" + hoaDon.DenNgay +
                //    "',NgayCapNhat = '" + hoaDon.NgayCapNhat + "',NVCapNhat = '" + hoaDon.NVCapNhat + "' where HoaDonID = '" + hoaDon.HoaDonID + "'";
                //var update = serverContext.ExecuteQuery<object>(query).ToList();
                var hoaDonServer = (from x in serverContext.HoaDons
                                    where x.HoaDonID == hoaDon.HoaDonID
                                    select x).Single();
                hoaDonServer.Nam = hoaDon.Nam;
                hoaDonServer.Ky = hoaDon.Ky;
                hoaDonServer.Dot = hoaDon.Dot;
                hoaDonServer.DanhBa = hoaDon.DanhBa;
                hoaDonServer.TenKH = hoaDon.TenKH;
                hoaDonServer.So = hoaDon.So;
                hoaDonServer.Duong = hoaDon.Duong;
                hoaDonServer.GB = hoaDon.GB;
                hoaDonServer.DM = hoaDon.DM;
                hoaDonServer.Code = hoaDon.Code;
                hoaDonServer.CSCu = hoaDon.CSCu;
                hoaDonServer.CSMoi = hoaDon.CSMoi;
                hoaDonServer.TieuThu = hoaDon.TieuThu;
                hoaDonServer.TuNgay = hoaDon.TuNgay;
                hoaDonServer.DenNgay = hoaDon.DenNgay;
                hoaDonServer.NgayCapNhat = hoaDon.NgayCapNhat;
                hoaDonServer.NVCapNhat = hoaDon.NVCapNhat;

                serverContext.SubmitChanges();
            }
            catch (Exception e)
            {

            }
        }
        public List<String> getListBaoCaoTongHop()
        {
            var query = serverContext.ExecuteQuery<String>("select codedesc from ThamSo where CodeType='BC' order by code").ToList();
            return query;
        }
        public int CapNhatKH(int year, string month, string date)
        {

            var query = (from B in serverContext.BienDongs
                         where B.Nam == year && B.Ky == month && B.Dot == date
                         select new { B.DanhBa, B.HopDong, B.MLT1, B.So, B.May, B.Duong, B.GB, B.DM, B.SX, B.SH, B.DV, B.HC, B.Dot })
                         .ToList();
            foreach (var item in query)
            {
                var KH = serverContext.KhachHangs.SingleOrDefault(row => row.DanhBa == item.DanhBa);
                if (KH != null)
                {
                    //
                    serverContext.SubmitChanges();
                }
            }
            return query.Count;
        }

        public List<Model.ChuyenBilling> GetBilling()
        {
            var tmp = serverContext.ExecuteQuery<Model.ChuyenBilling>("SELECT May, COUNT(DanhBa) as DHN, SUM(TieuThuMoi) as TieuThu FROM DocSo " +
                "WHERE Ky = '" + MyUser.Instance.Month + "' AND Dot = '" + MyUser.Instance.Date + "' AND Nam = " + MyUser.Instance.Year + " GROUP BY May ORDER BY May").ToList();
            //int year = Int16.Parse(MyUser.Instance.Year);
            //var query = (from x in serverContext.DocSos
            //            where x.Nam == year && x.Ky == MyUser.Instance.Month && x.Dot == MyUser.Instance.Date
            //            select new { x.May, x.DanhBa, x.TieuThuMoi }).ToList();

            //List<ChuyenBilling> data = new List<ChuyenBilling>();
            //int countDanhBa = 0;
            //int tieuthu = 0;
            //string may = query.ElementAt(0).May ;
            //for(int i = 0; i < query.Count; i++)
            //{
            //    var item = query.ElementAt(i);
            //    if (may.Equals(item.May))
            //    {
            //        countDanhBa++;
            //        tieuthu += item.TieuThuMoi.Value;
            //    }
            //    else
            //    {
            //        may = item.May;
            //        countDanhBa = 0;
            //        tieuthu = 0;
            //        data.Add(new ChuyenBilling()
            //        {
            //            May = may,
            //            DHN = countDanhBa,
            //            TieuThu = tieuthu
            //        });
            //    }
            //}
            //(from x in serverContext.DocSos
            //            where x.DanhBa == danhBa
            //            orderby x.DocSoID descending
            //            select new { x.Ky, x.Nam, danhBa, x.CodeMoi, x.TTDHNMoi, x.CSCu, x.CSMoi, x.TieuThuMoi, x.GhiChuDS, x.GhiChuKH, x.GhiChuTV }).ToList();
            return tmp;
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
            var value = serverContext.ExecuteQuery<string>("select top 1 hoadonid from hoadon where hoadonid like '" + nam + ky + "%' and dot = '" + dot + "'").ToList();
            if (value.Count > 0)
                return true;
            return false;
        }
        public bool CheckExistBienDong(int nam, string ky, string dot)
        {
            var value = serverContext.ExecuteQuery<string>("select top 1 biendongid from biendong where biendongid like '" + nam + ky + "%' and dot = '" + dot + "'").ToList();
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
        public DataTable GetDHNTrenMang()
        {
            List<DHNTrenMang> query;
            query = serverContext.ExecuteQuery<DHNTrenMang>("SELECT '(1)=(2) + (3)' AS TITLE1, N'Hiện có trên mạng' AS TITLE, HIEU, CO" +
                ", 0 AS LOAI, COUNT(DANHBA) AS DANHBA FROM KHACHHANG WHERE HIEULUC =1 GROUP BY HIEU, CO").ToList();
            query.AddRange(serverContext.ExecuteQuery<DHNTrenMang>("SELECT '(2)' AS TITLE1,N'Hiện có trên mạng (sử dụng < 5 năm)' AS TITLE, HIEU, CO, 1 AS LOAI, COUNT(DANHBA) AS DANHBA" +
                " FROM KHACHHANG WHERE HIEULUC =1 AND YEAR(getDate()) - Convert(varchar(4),NgayGan) < 5 GROUP BY HIEU, CO").ToList());
            query.AddRange(serverContext.ExecuteQuery<DHNTrenMang>("SELECT '(3)' AS TITLE1,N'Hiện có trên mạng (sử dụng > 5 năm)' AS TITLE, HIEU, CO, 2 AS LOAI, COUNT(DANHBA) AS DANHBA" +
                " FROM KHACHHANG WHERE HIEULUC =1 AND YEAR(getDate()) - Convert(varchar(4),NgayGan) >= 5 GROUP BY HIEU, CO").ToList());
            DataTable table = new DataTable();
            table.Columns.Add(DHNTRENMANG_COLUMN_KY, typeof(string));
            table.Columns.Add(DHNTRENMANG_COLUMN_NAM, typeof(string));
            table.Columns.Add(DHNTRENMANG_COLUMN_TITLE, typeof(string));
            table.Columns.Add(DHNTRENMANG_COLUMN_TITLE1, typeof(string));
            table.Columns.Add(DHNTRENMANG_COLUMN_HIEU, typeof(string));
            table.Columns.Add(DHNTRENMANG_COLUMN_CO, typeof(string));
            table.Columns.Add(DHNTRENMANG_COLUMN_DANHBA, typeof(string));
            foreach (var item in query)
            {
                DataRow row = table.NewRow();
                row[DHNTRENMANG_COLUMN_KY] = item.Ky;
                row[DHNTRENMANG_COLUMN_NAM] = item.Nam;
                row[DHNTRENMANG_COLUMN_TITLE] = item.Title;
                row[DHNTRENMANG_COLUMN_TITLE1] = item.Title1;
                row[DHNTRENMANG_COLUMN_HIEU] = item.Hieu;
                row[DHNTRENMANG_COLUMN_CO] = item.Co;
                row[DHNTRENMANG_COLUMN_DANHBA] = item.DanhBa;
                table.Rows.Add(row);
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
                if (machine.Equals(ALL) || group == 0)
                {
                    queryStr += " Group by ds.May,m.ToID,ds.CodeMoi,ds.Ky,ds.Dot";
                }
                else
                {
                    queryStr += " and ToID = " + group + " and ds.May ='" + machine + "' Group by ds.May,m.ToID,ds.CodeMoi,ds.Ky,ds.Dot";
                }
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
                if (num1 < 0.0 ||
                    (num1 == 0.0 && num2 > 3.0) ||
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

        public bool Update(string code, string csm, string tieuThu, string ghiChuDS, string ghiChuMH, string ghiChuKH, string KHDS, DateTime ngayCapNhat, int nam, string ky, string dot, string danhBa)
        {
            var docSo = serverContext.DocSos.SingleOrDefault(row => row.DanhBa == danhBa && row.Nam == nam && row.Ky == ky && row.Dot == dot);
            if (docSo != null)
            {
                docSo.CodeMoi = code;
                docSo.CSMoi = Int16.Parse(csm);
                docSo.TieuThuMoi = Int16.Parse(tieuThu);
                docSo.GhiChuDS = ghiChuDS;
                docSo.GhiChuTV = ghiChuMH;
                docSo.GhiChuKH = ghiChuKH;
                docSo.TTDHNMoi = KHDS;
                docSo.StaCapNhat = "1";
                docSo.NgayCapNhat = ngayCapNhat;
                docSo.NVCapNhat = "";//todo         
                serverContext.SubmitChanges();

                return true;
            }
            return false;
        }

        public bool HoanTatDocSo()
        {
            var data = serverContext.BillStates.SingleOrDefault(row => row.BillID == MyUser.Instance.Year + MyUser.Instance.Month + MyUser.Instance.Date);
            if (data != null)
            {
                data.izDS = "1";
                serverContext.SubmitChanges();
                return true;
            }
            return false;
        }

        public List<String> getDocsosByConditionCount(int year, String month, String date, int xGroup, String machine)
        {
            var getData = (from x in serverContext.DocSos
                           where x.DocSoID.StartsWith(year + month) && x.Dot == date && x.TODS == xGroup && x.May == machine
                           select x.DanhBa).ToList();
            return getData;
        }

        public bool HoanTatThuongVu()
        {
            var data = serverContext.BillStates.SingleOrDefault(row => row.BillID == MyUser.Instance.Year + MyUser.Instance.Month + MyUser.Instance.Date);
            if (data != null)
            {
                data.izTV = "1";
                serverContext.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool getDocSosByDanhBa(String danhBa, int year, String month, String date, int xGroup, String machine)
        {

            bool result = false;

            DataClassServerDataContext serverContext = new DataClassServerDataContext();
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
                catch (Exception e)
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
            if (machine.Equals(ALL))
            {
                var data = (from x in serverContext.DocSos
                            where x.DocSoID.StartsWith(year + month) && x.Dot == date && x.TODS == xGroup
                            select x).ToList();
                return data;
            }
            else
            {
                var data = (from x in serverContext.DocSos
                            where x.DocSoID.StartsWith(year + month) && x.Dot == date && x.May == machine
                            select x).ToList();
                return data;
            }
        }

        public byte[] getImageByDanhBa(String danhBa, DateTime gioGhi)
        {

            using (DataClassServerDataContext tempServer = new DataClassServerDataContext())
            {
                var data = (from x in tempServer.HinhDHNs
                            where x.DanhBo == danhBa && x.CreateDate == gioGhi
                            orderby x.CreateDate
                            select x.Image).FirstOrDefault();
                if (data == null)
                    return null;

                return ((System.Data.Linq.Binary)data).ToArray();
            }
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

            DataClassServerDataContext serverContext = new DataClassServerDataContext();
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
                catch (Exception e)
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
            String kyString;
            month++;
            while (count <= 12)
            {
                month--;
                if (month == 0)
                {
                    year--;
                    month = 12;
                }
                kyString = month + "";
                if (month < 10)
                    kyString = "0" + month;
                r.Add(new Item()
                {
                    Year = year,
                    Month = month
                });
                //var data = (from x in serverContext.DocSos
                //            where x.DanhBa == danhBa && x.Nam == year && x.Ky == kyString
                //            select new { x.Ky, x.GIOGHI, x.CodeMoi, x.CSMoi, x.TieuThuMoi }).FirstOrDefault();
                //if (data != null)
                //    listDocSo.Add(new DocSo_1Ky(data.Ky + "/" + year, data.GIOGHI.GetValueOrDefault().ToString(pattern), data.CodeMoi, data.CSMoi + "", data.TieuThuMoi + ""));
                count++;
            }
            var whereStr = "and (";
            foreach (var s in r)
            {
                whereStr += String.Format(" docsoid like '{0}%' or", s.Year + "" + s.Month.ToString().PadLeft(2, '0'));
            }
            whereStr = whereStr.Substring(0, whereStr.Length - 3);
            whereStr += ")";
            List<DocSo_1Ky> datas = serverContext.ExecuteQuery<DocSo>(String.Format("select docsoID, denngay, codemoi, csmoi, tieuthumoi from Docso where danhba = '" + danhBa + "' {0} " +
                "order by nam desc, ky desc", whereStr)).Select(data => new DocSo_1Ky(data.DocSoID.Substring(4, 2) + "/" + data.DocSoID.Substring(0, 4), data.DenNgay.GetValueOrDefault().ToString(pattern), data.CodeMoi, data.CSMoi + "", data.TieuThuMoi + "")).ToList();
            ObservableCollection<DocSo_1Ky> listDocSo = new ObservableCollection<DocSo_1Ky>(datas);
            return listDocSo;
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
            catch
            {

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
            catch { }
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
            catch { }
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
            catch { }
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
            catch { }
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
            catch { }
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
            catch { }
            return lstGroup;
        }
        public ObservableCollection<String> getDistinctMachineServer(int year, String month, String date, int xGroup)
        {
            ObservableCollection<String> lstMachine = new ObservableCollection<String>();
            try
            {
                List<String> items;
                if (xGroup == 0)
                    items = (from x in serverContext.DocSos
                             where x.DocSoID.StartsWith(year + month) && x.Dot == date
                             select x.May).Distinct().ToList();
                else
                    items = (from x in serverContext.DocSos
                             where x.DocSoID.StartsWith(year + month) && x.Dot == date && x.TODS == xGroup
                             select x.May).Distinct().ToList();
                lstMachine.Add(ALL);
                foreach (String item in items)
                    lstMachine.Add(item);
            }
            catch { }
            return lstMachine;
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
            catch { }
            return listSoDaNhan;
        }

    }
}
