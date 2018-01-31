using System;
using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;

namespace ViewModel
{
  public  class C_connOracle
    {
        public OdbcConnection connTongCT = new OdbcConnection();

        public void OpenConnectionTCT(string name, string pass)
        {
            try
            {
                this.connTongCT = new OdbcConnection("Dsn=oracle71;uid=hatq;pwd=123;server=hatq");
               // this.connTongCT = new OdbcConnection("Dsn=oracle7;uid=th_handheld;pwd=th_hh;server=center");
                //this.connTongCT = new OdbcConnection(@"Dsn=oracle7;uid=nb_hh;pwd =nb_hh;server = center");
                if (this.connTongCT.State == ConnectionState.Open)
                    return;
                this.connTongCT.Open();
            }
            catch (OdbcException ex1)
            {
                try
                {
                    this.connTongCT = new OdbcConnection("Dsn=oracle71;uid=hatq;pwd=123;server=hatq");
                  //  this.connTongCT = new OdbcConnection("Dsn=oracle7;uid=th_handheld;pwd =th_hh;server=center");
                    if (this.connTongCT.State == ConnectionState.Open)
                        return;
                    this.connTongCT.Open();
                  
                }
                catch (Exception ex2)
                {
                    int num = (int)MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu Billing");
                }
            }
        }

        public void CloseConnectionTCT()
        {
            if (this.connTongCT.State != ConnectionState.Closed)
                this.connTongCT.Close();
            this.connTongCT.Dispose();
        }
    }
}
