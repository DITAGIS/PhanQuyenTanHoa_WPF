using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ConnectionViewModel
    {
        private static SqlDataAdapter da = (SqlDataAdapter)null;
        public static SqlDataAdapter PCAdapter
        {
            get
            {
                return da;
            }
        }

        private static SqlCommand cmd = (SqlCommand)null;
        public static SqlCommand PCCommand
        {
            get
            {
                return cmd;
            }
        }
        private static SqlConnection conn;
        public SqlConnection getConnection
        {
            get
            {
                return conn;
            }
        }
        public String ConnectionString
        {
            get
            {
                string key = "PhanQuyen.Properties.Settings.DocSoTHConnectionString1_THANLE";
                string connectionString = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).ConnectionStrings.ConnectionStrings[key].ConnectionString;
                return connectionString;
            }
        }
     
        private ConnectionViewModel()
        {
            conn = new SqlConnection(ConnectionString);
            Connect();

        }
        private static ConnectionViewModel _instance;
        public static ConnectionViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConnectionViewModel();
                    cmd = new SqlCommand();
                    cmd.Connection = conn;
                    da = new SqlDataAdapter(cmd);
                }
                return _instance;
            }
        }
        public void Connect()
        {
            try
            {
                conn.Open();
            }
            catch
            {
            }
        }
        public void DisConnect()
        {
            try
            {
                conn.Close();
            }
            catch
            {
            }
        }

        public SqlDataReader GetExecuteReader(string sqlStatment)
        {
            if (cmd != null)
                cmd.CommandText = sqlStatment;
            else
                cmd = new SqlCommand(sqlStatment, conn);
            return cmd.ExecuteReader();
        }

        public DataTable GetDataTable(string sqlStatement)
        {
            DataTable dataTable = new DataTable();
            da.SelectCommand.CommandText = sqlStatement;
            da.FillSchema(dataTable, SchemaType.Mapped);
            da.Fill(dataTable);
            return dataTable;
        }

        public int GetExecuteScalar(string sqlStatement)
        {
            if (cmd == null)
                cmd = new SqlCommand(sqlStatement, conn);
            else
                cmd.CommandText = sqlStatement;
            return (int)cmd.ExecuteScalar();
        }

        public int GetExecuteNonQuerry(string sqlstatement)
        {
            if (cmd == null)
                cmd = new SqlCommand(sqlstatement, conn);
            else
                cmd.CommandText = sqlstatement;
            return cmd.ExecuteNonQuery();
        }

    }
}
