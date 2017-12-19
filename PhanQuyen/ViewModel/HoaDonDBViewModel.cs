using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class HoaDonDBViewModel
    {
        private const String TABLE_NAME_DOCSO = "Docso";
        private const String TABLE_NAME_KHACHHANG = "KhachHang";
        private const String TABLE_NAME_HINHDHN = "HinhDHN";
        private const String TABLE_NAME_TO = "[DocSoTH].[dbo].[To]";
        private const String SQL_SELECT = "select top 100 * from " + TABLE_NAME_DOCSO;
        private const String SQL_SELECT_DANH_BA_CONDITION = "select top 2 docso.danhba  from " +
        TABLE_NAME_DOCSO + ", " + TABLE_NAME_KHACHHANG + ", " + TABLE_NAME_HINHDHN + " where nam = @year and ky = @month and docso.Dot = @date and docso.may = @machine and KhachHang.DanhBa = DocSo.DanhBa " +
        "and docso.DanhBa = HinhDHN.DanhBo and docso.GIOGHI = HinhDHN.CreateDate";
        private const String SQL_SELECT_INCLUDE_IMAGE_CONDITION = "select TTDHNCu, TTDHNMoi, CodeMoi, CodeCu, CSCu, CSMOI, Tieuthumoi, TBTT, ghichuds," +
            " KhachHang.So, KhachHang.Duong, KhachHang.TenKH, KhachHang.GB, KhachHang.DM, KhachHang.HopDong, KhachHang.Hieu, KhachHang.Co, KhachHang.SoThan, KhachHang.MLT1, [Image]  from " +
            TABLE_NAME_DOCSO + ", " + TABLE_NAME_KHACHHANG + ", " + TABLE_NAME_HINHDHN + " where docso.danhba = @danhba and nam = @year and ky = @month and docso.Dot = @date and docso.may = @machine and KhachHang.DanhBa = DocSo.DanhBa " +
            "and docso.DanhBa = HinhDHN.DanhBo and docso.GIOGHI = HinhDHN.CreateDate";
        private const String SQL_SELECT_CONDITION = "select docso.danhba, TTDHNCu, TTDHNMoi, CodeMoi, CodeCu, CSCu, CSMOI, Tieuthumoi, TBTT, ghichuds," +
         " KhachHang.So, KhachHang.Duong, KhachHang.TenKH, KhachHang.GB, KhachHang.DM, KhachHang.HopDong, KhachHang.Hieu, KhachHang.Co, KhachHang.SoThan, KhachHang.MLT1  from " +
         TABLE_NAME_DOCSO + ", " + TABLE_NAME_KHACHHANG + " where nam = @year and ky = @month and docso.Dot = @date and docso.may = @machine and KhachHang.DanhBa = DocSo.DanhBa";
        private const String SQL_SELECT_DISTINCT_YEAR = "select distinct nam from " + TABLE_NAME_DOCSO;
        private const String SQL_SELECT_DISTINCT_MONTH = "select distinct ky from " + TABLE_NAME_DOCSO + " where nam = @year";
        private const String SQL_SELECT_DISTINCT_DATE = "select distinct dot from " + TABLE_NAME_DOCSO + " where nam = @year and ky = @month";
        private const String SQL_SELECT_DISTINCT_GROUP = "select distinct mato from " + TABLE_NAME_TO;
        private const String SQL_SELECT_DISTINCT_MACHINE = "select [TuMay], [DenMay] from " + TABLE_NAME_TO + " where mato = @group";
        public static int MAX = 1;
        public static int VALUE = 0;
        private static HoaDonDBViewModel _instance;
        public static HoaDonDBViewModel getInstance
        {
            get
            {
                if (_instance == null)
                    _instance = new HoaDonDBViewModel();
                return _instance;
            }
        }
        private HoaDonDBViewModel() { }

        public List<HoaDon> getHoaDonsByCondition(String year, String month, String date, String group, String machine)
        {
            List<HoaDon> hoaDons = new List<HoaDon>();
            try
            {
                SqlCommand command = new SqlCommand(SQL_SELECT_CONDITION, ConnectionViewModel.getInstance.getConnection);
                //ConnectionViewModel.getInstance.Connect();
                command.Parameters.AddWithValue("@year", year);
                command.Parameters.AddWithValue("@month", month);
                command.Parameters.AddWithValue("@date", date);
                //command.Parameters.AddWithValue("@group", group);
                command.Parameters.AddWithValue("@machine", machine);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    HoaDon hoaDon = new HoaDon();
                    hoaDon.DanhBa = dataReader["danhba"].ToString();
                    hoaDon.TTHDNCu = dataReader["TTDHNCU"].ToString();
                    hoaDon.TTHDNMoi = dataReader["TTDHNMoi"].ToString();
                    hoaDon.CodeMoi = dataReader["CodeMoi"].ToString();
                    hoaDon.CodeCu = dataReader["CodeCu"].ToString();
                    hoaDon.CSC = dataReader["CSCU"].ToString();
                    hoaDon.CSM = dataReader["CSMOI"].ToString();
                    hoaDon.TieuThuMoi = dataReader["TieuThuMoi"].ToString();
                    hoaDon.TBTT = dataReader["TBTT"].ToString();
                    hoaDon.DiaChi = dataReader["so"].ToString() + " " + dataReader["duong"].ToString(); ;
                    hoaDon.GhiChuDS = dataReader["GhiChuDS"].ToString();
                    hoaDon.TenKH = dataReader["TenKH"].ToString();
                    hoaDon.HopDong = dataReader["HopDong"].ToString();
                    hoaDon.Hieu = dataReader["Hieu"].ToString();
                    hoaDon.Co = dataReader["Co"].ToString();
                    hoaDon.GB = dataReader["GB"].ToString();
                    hoaDon.DM = dataReader["DM"].ToString();
                    hoaDon.SoThan = dataReader["SoThan"].ToString();
                    hoaDon.MLT = dataReader["MLT1"].ToString();
                    hoaDons.Add(hoaDon);
                }
            }
            catch (Exception e)
            {

            }
            return hoaDons;
        }
        public HoaDon getHoaDonsIncludeImageByCondition(String year, String month, String date, String group, String machine, String danhba)
        {
            HoaDon hoaDon = new HoaDon(); ;
            SqlDataReader dataReader = null;
            try
            {
                SqlCommand command = new SqlCommand(SQL_SELECT_INCLUDE_IMAGE_CONDITION, ConnectionViewModel.getInstance.getConnection);
                //ConnectionViewModel.getInstance.Connect();
                command.Parameters.AddWithValue("@danhba", danhba);
                command.Parameters.AddWithValue("@year", year);
                command.Parameters.AddWithValue("@month", month);
                command.Parameters.AddWithValue("@date", date);
                //command.Parameters.AddWithValue("@group", group);
                command.Parameters.AddWithValue("@machine", machine);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    hoaDon.DanhBa = danhba;
                    hoaDon.TTHDNCu = dataReader["TTDHNCU"].ToString();
                    hoaDon.TTHDNMoi = dataReader["TTDHNMoi"].ToString();
                    hoaDon.CodeMoi = dataReader["CodeMoi"].ToString();
                    hoaDon.CodeCu = dataReader["CodeCu"].ToString();
                    hoaDon.CSC = dataReader["CSCU"].ToString();
                    hoaDon.CSM = dataReader["CSMOI"].ToString();
                    hoaDon.TieuThuMoi = dataReader["TieuThuMoi"].ToString();
                    hoaDon.TBTT = dataReader["TBTT"].ToString();
                    hoaDon.DiaChi = dataReader["so"].ToString() + " " + dataReader["duong"].ToString(); ;
                    hoaDon.GhiChuDS = dataReader["GhiChuDS"].ToString();
                    hoaDon.TenKH = dataReader["TenKH"].ToString();
                    hoaDon.HopDong = dataReader["HopDong"].ToString();
                    hoaDon.Hieu = dataReader["Hieu"].ToString();
                    hoaDon.Co = dataReader["Co"].ToString();
                    hoaDon.GB = dataReader["GB"].ToString();
                    hoaDon.DM = dataReader["DM"].ToString();
                    hoaDon.SoThan = dataReader["SoThan"].ToString();
                    hoaDon.MLT = dataReader["MLT1"].ToString();
                    hoaDon.Image = dataReader["Image"] as Byte[];
                }
            }
            catch (Exception e)
            {

            }
            if (!dataReader.IsClosed)
                dataReader.Close();
            return hoaDon;
        }
        public List<String> getDanhBasByCondition(String year, String month, String date, String group, String machine)
        {
            List<String> danhBas = new List<String>();
            SqlCommand command = new SqlCommand(SQL_SELECT_DANH_BA_CONDITION, ConnectionViewModel.getInstance.getConnection);
            SqlDataReader dataReader = null;
            try
            {

                //ConnectionViewModel.getInstance.Connect();
                command.Parameters.AddWithValue("@year", year);
                command.Parameters.AddWithValue("@month", month);
                command.Parameters.AddWithValue("@date", date);
                //command.Parameters.AddWithValue("@group", group);
                command.Parameters.AddWithValue("@machine", machine);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    danhBas.Add(dataReader["danhba"].ToString());
                }
            }
            catch (Exception e)
            {

            }
            if (!dataReader.IsClosed)
                dataReader.Close();
            return danhBas;
        }
        public List<String> getDistinctYear()
        {
            List<String> listYear = new List<String>();
            try
            {
                SqlCommand command = new SqlCommand(SQL_SELECT_DISTINCT_YEAR, ConnectionViewModel.getInstance.getConnection);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                    listYear.Add(dataReader["nam"].ToString());
                dataReader.Close();
            }
            catch (Exception e)
            {

            }
            return listYear;
        }
        public List<String> getDistinctMonth(String year)
        {
            List<String> listMonth = new List<String>();
            try
            {
                SqlCommand command = new SqlCommand(SQL_SELECT_DISTINCT_MONTH, ConnectionViewModel.getInstance.getConnection);
                command.Parameters.AddWithValue("@year", year);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                    listMonth.Add(dataReader["ky"].ToString());
                dataReader.Close();
            }
            catch (Exception e)
            {

            }
            return listMonth;
        }
        public List<String> getDistinctDate(String year, String month)
        {
            List<String> listDate = new List<String>();
            try
            {
                SqlCommand command = new SqlCommand(SQL_SELECT_DISTINCT_DATE, ConnectionViewModel.getInstance.getConnection);
                command.Parameters.AddWithValue("@year", year);
                command.Parameters.AddWithValue("@month", month);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                    listDate.Add(dataReader["dot"].ToString());
                dataReader.Close();
            }
            catch (Exception e)
            {

            }
            return listDate;
        }
        public List<String> getDistinctGroup()
        {
            List<String> listGroup = new List<String>();
            try
            {
                SqlCommand command = new SqlCommand(SQL_SELECT_DISTINCT_GROUP, ConnectionViewModel.getInstance.getConnection);

                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                    listGroup.Add(dataReader["mato"].ToString());
                dataReader.Close();
            }
            catch (Exception e)
            {

            }
            return listGroup;
        }
        public List<String> getDistinctMachine(String group)
        {
            List<String> listMachine = new List<String>();
            try
            {
                SqlCommand command = new SqlCommand(SQL_SELECT_DISTINCT_MACHINE, ConnectionViewModel.getInstance.getConnection);

                command.Parameters.AddWithValue("@group", group);
                SqlDataReader dataReader = command.ExecuteReader();
                int start = 0, end = 0;
                while (dataReader.Read())
                {
                    start = Int16.Parse(dataReader["tumay"].ToString());
                    end = Int16.Parse(dataReader["denmay"].ToString());
                }
                dataReader.Close();
                for (int i = start; i <= end; i++)
                    if (i < 10)
                        listMachine.Add("0" + i);
                    else
                        listMachine.Add(i.ToString());
            }
            catch (Exception e)
            {

            }
            return listMachine;
        }
    }
}
