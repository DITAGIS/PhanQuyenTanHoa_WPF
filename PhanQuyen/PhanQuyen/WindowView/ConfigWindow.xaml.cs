using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PhanQuyen.WindowView
{
    /// <summary>
    /// Interaction logic for ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow : Window
    {
        public ConfigWindow()
        {
            InitializeComponent();
        }

        private void btnTestConnection_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=" + this.passIPName.Password + ";Initial Catalog=" + this.txtDatabase.Text + ";user=" + this.passUser.Password + ";password=" + this.passPassword.Password);
            try
            {
                sqlConnection.Open();
                int num = (int)System.Windows.Forms.MessageBox.Show("Kết nối thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch
            {
                int num = (int)System.Windows.Forms.MessageBox.Show("Kết nối thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            String connectionString = "Data Source=" + this.passIPName.Password + ";Initial Catalog=" + this.txtDatabase.Text + ";user=" + this.passUser.Password + ";password=" + this.passPassword.Password;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                string key = "PhanQuyen.Properties.Settings.DocSoTHConnectionString1_THANLE";
                config.ConnectionStrings.ConnectionStrings[key].ConnectionString = connectionString;
                config.ConnectionStrings.ConnectionStrings[key].ProviderName = "System.Data.SqlClient";
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");
                int num = (int)System.Windows.Forms.MessageBox.Show("Lưu kết nối thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch
            {
                int num = (int)System.Windows.Forms.MessageBox.Show("Kết nối thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
        }
    }
}
