using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    class ConnectionViewModel
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
                return "Data Source=103.74.117.51;Initial Catalog=DocSoTH;Integrated Security=False;User ID=docsotanhoa;Password=Docso111;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            }
        }
        private ConnectionViewModel()
        {
            conn = new SqlConnection(ConnectionString);
            Connect();

        }
        private static ConnectionViewModel _instance;
        public static ConnectionViewModel getInstance
        {
            get
            {
                if (_instance == null)
                    _instance = new ConnectionViewModel();
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
