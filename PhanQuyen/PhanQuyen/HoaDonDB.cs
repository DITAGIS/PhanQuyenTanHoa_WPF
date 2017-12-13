using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanQuyen
{
    class HoaDonDB
    {
        private const String TABLE_NAME = "Docso";
        private const String SQL_SELECT = "select top 100 * from " + TABLE_NAME;

        public List<HoaDon> getAllHoaDon()
        {
            List<HoaDon> hoaDons = new List<HoaDon>();
            try
            {
                SqlCommand command = new SqlCommand(SQL_SELECT, Connection.getInstance.getConnection);
                Connection.getInstance.Connect() ;
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
            catch (Exception e){

            }
            return hoaDons;
        }
    }
}
