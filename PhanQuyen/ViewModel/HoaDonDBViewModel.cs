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
        private const String TABLE_NAME = "Docso";
        private const String SQL_SELECT = "select top 100 * from " + TABLE_NAME;
        private const String SQL_SELECT_CONDITION = "select * from " + TABLE_NAME + " where nam = @year and ky = @month and dot = @date and may = @machine";
        private const String SQL_SELECT_DISTINCT_YEAR = "select distinct nam from " + TABLE_NAME;
        private const String SQL_SELECT_DISTINCT_MONTH = "select distinct ky from " + TABLE_NAME + " where nam = @year";
        private const String SQL_SELECT_DISTINCT_DATE = "select distinct dot from " + TABLE_NAME + " wherer nam = @year and ky = @month";
        //private const String SQL_SELECT_DISTINCT_GROUP = "select distinct nam from " + TABLE_NAME;
        private const String SQL_SELECT_DISTINCT_MACHINE = "select distinct may from " + TABLE_NAME + " where nam = @year and ky = @month and dot = @date";

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
        public List<HoaDon> getAllHoaDon()
        {
            List<HoaDon> hoaDons = new List<HoaDon>();
            try
            {
                SqlCommand command = new SqlCommand(SQL_SELECT, ConnectionViewModel.getInstance.getConnection);
                ConnectionViewModel.getInstance.Connect();

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
                    hoaDon.GhiChuDS = dataReader["GhiChuDS"].ToString();
                    hoaDons.Add(hoaDon);
                }
            }
            catch
            {

            }
            return hoaDons;
        }
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
                    hoaDon.GhiChuDS = dataReader["GhiChuDS"].ToString();
                    hoaDons.Add(hoaDon);
                }
            }
            catch (Exception e)
            {

            }
            return hoaDons;
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
        public List<String> getDistinctMachine(String year, String month, String date)
        {
            List<String> listMachine = new List<String>();
            try
            {
                SqlCommand command = new SqlCommand(SQL_SELECT_DISTINCT_MACHINE, ConnectionViewModel.getInstance.getConnection);
                command.Parameters.AddWithValue("@year", year);
                command.Parameters.AddWithValue("@month", month);
                command.Parameters.AddWithValue("@date", date);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                    listMachine.Add(dataReader["may"].ToString());
                dataReader.Close();
            }
            catch (Exception e)
            {

            }
            return listMachine;
        }
    }
}
