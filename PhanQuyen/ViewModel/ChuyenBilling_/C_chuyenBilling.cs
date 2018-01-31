using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;
//using VUti;
using System.Data.SqlClient;
namespace ViewModel
{
    public class C_chuyenBilling
    {

        private C_connOracle conn = new C_connOracle();
        private string ky;
        private string dot;
        private string nam;
        private string username;
        private string pass;
        private string cty;
        private SqlConnection gSqlConnec;
        public C_chuyenBilling(string _ky, string _dot, string _nam, string _usename, string _pass, string _branch, SqlConnection sqlcn)
        {
            this.ky = _ky;
            this.dot = _dot;
            this.nam = _nam;
            this.username = _usename;
            this.pass = _pass;
            this.cty = _branch;
            this.gSqlConnec = sqlcn;
        }

        protected string getID()
        {
            string str = "0";
            try
            {
                this.conn.OpenConnectionTCT(this.username, this.pass);
                OdbcDataReader odbcDataReader = new OdbcCommand("SELECT ADMIN.\"TMP$MR_SEQ\".NEXTVAL AS ID FROM SYS.DUAL", this.conn.connTongCT).ExecuteReader();
                if (odbcDataReader.Read())
                    str = odbcDataReader["ID"].ToString();
                odbcDataReader.Close();
                this.conn.CloseConnectionTCT();
            }
            catch
            {
            }
            return str;
        }

        protected string getRST_ID(string code)
        {
            this.conn.OpenConnectionTCT(this.username, this.pass);
            string cmdText = "SELECT ID FROM READING_STATUS WHERE STATUS_CODE='" + code + "'";
            string str = "";
            OdbcDataReader odbcDataReader = new OdbcCommand(cmdText, this.conn.connTongCT).ExecuteReader();
            if (odbcDataReader.Read())
                str = odbcDataReader["ID"].ToString().Trim();
            odbcDataReader.Close();
            this.conn.CloseConnectionTCT();
            return str;
        }

        public bool CapNhatDuLieuBilling(DateTime ngaydoc, ToolStripProgressBar tp)
        {
            bool flag = false;
            DataTable dataTable = GetDataTable(gSqlConnec, "SELECT DanhBa, CSCu,  CASE WHEN LEFT(CodeMoi, 1) = 'F' OR LEFT(CodeMoi, 1) = '6'" +
                " THEN TieuThuMoi ELSE CSMOI END AS CSMoi, TieuThuMoi, CASE WHEN LEFT(CodeMoi,1) = '4' THEN '4' ELSE CodeMoi END AS CodeMoi, MLT2, TTDHNMoi FROM DocSo WHERE Ky = '" + this.ky + "' AND Dot= '" + this.dot + "' AND Nam= " + this.nam + " and may ='01'"
                , nam, ky, dot);
            int num1 = 0;
            int num2 = 0;
            int count = dataTable.Rows.Count;
            tp.Maximum = count;
            tp.Minimum = 0;
            for (int index1 = 0; index1 < dataTable.Rows.Count; ++index1)
            {
                string str1 = dataTable.Rows[index1]["DanhBa"].ToString().Trim();
                string code = dataTable.Rows[index1]["CodeMoi"].ToString().Trim();
                string str2 = dataTable.Rows[index1]["CSCu"].ToString().Trim();
                string str3 = dataTable.Rows[index1]["CSMoi"].ToString().Trim();
                string str4 = dataTable.Rows[index1]["TieuThuMoi"].ToString().Trim();
                double num3 = Convert.ToDouble(this.getID());
                string str5 = dataTable.Rows[index1]["MLT2"].ToString().Trim();
                if (this.dot.Length == 1)
                    this.dot = "0" + this.dot;
                string str6 = str5.Substring(2, 2);
                string str7 = str5.Substring(4, 3);
                string rstId = this.getRST_ID(code);
                if (!(rstId.Trim() == ""))
                    ;
                if (code.Length == 0)
                {
                    int num4 = (int)MessageBox.Show("Kiểm tra lại code mới của danh bạ " + str1);
                    return false;
                }
                object[] objArray1 = new object[31]
                {
                  (object) "INSERT INTO ADMIN.\"TMP$MR\" (ID, BRANCH_CODE, \"YEAR\", PERIOD, BC_CODE, CUSTOMER_NO, MR_STATUS, THIS_READING, CONSUMPTION, DATE_READING, CREATED_ON, CREATED_BY, BOOK_NO, OIB, EMP_ID, RST_ID) VALUES (",
                  (object) num3,
                  (object) ",'",
                  (object) this.cty,
                  (object) "',",
                  (object) this.nam,
                  (object) ",",
                  (object) this.ky,
                  (object) ",'",
                  (object) this.dot,
                  (object) "','",
                  (object) str1,
                  (object) "','",
                  (object) code,
                  (object) "',",
                  (object) str3,
                  (object) ",",
                  (object) str4,
                  (object) ",'",
                  (object) ngaydoc.ToString("dd/MM/yyyy"),
                  (object) "','",
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null,
                  null
                };
                object[] objArray2 = objArray1;
                int index2 = 21;
                DateTime now = DateTime.Now;
                string str8 = now.ToString("dd/MM/yyyy");
                objArray2[index2] = (object)str8;
                objArray1[22] = (object)"','";
                objArray1[23] = (object)this.username;
                objArray1[24] = (object)"','";
                objArray1[25] = (object)str6;
                objArray1[26] = (object)"','";
                objArray1[27] = (object)str7;
                objArray1[28] = (object)"','100000002','";
                objArray1[29] = (object)rstId;
                objArray1[30] = (object)"')";
                string cmdText = string.Concat(objArray1);
                if (code.Length > 0 && (code.Substring(0, 1) == "5" || code.Substring(0, 1) == "8" || code.Substring(0, 1) == "M"))
                {
                    object[] objArray3 = new object[33]
          {
                    (object) "INSERT INTO ADMIN.\"TMP$MR\" (ID, BRANCH_CODE, \"YEAR\", PERIOD, BC_CODE, CUSTOMER_NO, MR_STATUS, LAST_READING, THIS_READING, CONSUMPTION, DATE_READING, CREATED_ON, CREATED_BY, BOOK_NO, OIB, EMP_ID,RST_ID) VALUES (",
                    (object) num3,
                    (object) ",'",
                    (object) this.cty,
                    (object) "',",
                    (object) this.nam,
                    (object) ",",
                    (object) this.ky,
                    (object) ",'",
                    (object) this.dot,
                    (object) "','",
                    (object) str1,
                    (object) "','",
                    (object) code,
                    (object) "',",
                    (object) str2,
                    (object) ",",
                    (object) str3,
                    (object) ",",
                    (object) str4,
                    (object) ",'",
                    (object) ngaydoc.ToString("dd/MM/yyyy"),
                    (object) "','",
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null
          };
                    object[] objArray4 = objArray3;
                    int index3 = 23;
                    now = DateTime.Now;
                    string str9 = now.ToString("dd/MM/yyyy");
                    objArray4[index3] = (object)str9;
                    objArray3[24] = (object)"','";
                    objArray3[25] = (object)this.username;
                    objArray3[26] = (object)"','";
                    objArray3[27] = (object)str6;
                    objArray3[28] = (object)"','";
                    objArray3[29] = (object)str7;
                    objArray3[30] = (object)"','100000002','";
                    objArray3[31] = (object)rstId;
                    objArray3[32] = (object)"')";
                    cmdText = string.Concat(objArray3);
                }
                try
                {
                    if (str5.Length > 6)
                    {
                        try
                        {
                            this.conn.OpenConnectionTCT(this.username, this.pass);
                            if (new OdbcCommand(cmdText, this.conn.connTongCT).ExecuteNonQuery() > 0)
                            {
                                ++num1;
                                tp.Value = count;
                            }
                        }
                        catch
                        {
                        }
                        this.conn.CloseConnectionTCT();
                    }
                }
                catch (Exception ex)
                {
                    int num4 = (int)MessageBox.Show(ex.Message);
                    ++num2;
                    this.conn.CloseConnectionTCT();
                }
            }
            this.conn.CloseConnectionTCT();
            if (num2 > 0)
            {
                flag = true;
                int num3 = (int)MessageBox.Show("Chuyển billing thành công " + num1.ToString() + " danh bạ, lỗi " + num2.ToString() + " danh bạ");
            }
            if (num2 == 0 && num1 == count)
            {
                int num5 = (int)MessageBox.Show("Chuyển billing thành công " + num1.ToString() + " danh bạ");
            }
            return flag;
        }
        public DataTable GetDataTable(SqlConnection pSqlConnect, string sql, string nam, string ky, string dot)
        {
            // TODO: On Error GoTo Warning!!!: The statement is not translatable 
            try
            {
                int namInt = Int16.Parse(nam);
                string strconnect = "Server='103.74.117.51';database='DocSoTH';uid='sa';pwd='Ditagis123'";
                SqlConnection connec = new SqlConnection(strconnect);
                connec.Open();
                connec.Close();
                pSqlConnect = connec;
                //DataClassServerDataContext serverDataContext = new DataClassServerDataContext();
                //var data = (from x in serverDataContext.DocSos
                //            where x.Nam == namInt && x.Ky == ky && x.Dot == dot
                //            select x).ToList();
                pSqlConnect.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, pSqlConnect);
                sda.SelectCommand.CommandTimeout = 120;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                pSqlConnect.Close();
                return dt;
            }
            catch (Exception e)
            {
                pSqlConnect.Close();
                MessageBox.Show("Lỗi: " + e.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
