using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanQuyen
{
    class Connection
    {
        private SqlConnection conn;
        public SqlConnection getConnection
        {
            get
            {
                return conn;
            }
        }
        private String ConnectionString
        {
            get
            {
                return "Data Source=THANLE;Initial Catalog=CapNuocTanHoa;Integrated Security=False;User ID=sa;Password=123456;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            }
        }
        private Connection()
        {
            conn = new SqlConnection(ConnectionString);
        

        }
        private static Connection _instance;
        public static Connection getInstance
        {
            get
            {
                if (_instance == null)
                    _instance = new Connection();
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
        public void disConnect()
        {
            try
            {
                conn.Close();
            }
            catch
            {
            }
        }

    }
}
