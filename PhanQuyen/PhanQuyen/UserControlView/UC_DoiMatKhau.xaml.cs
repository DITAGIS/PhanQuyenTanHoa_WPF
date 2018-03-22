using Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModel;

namespace PhanQuyen.UserControlView
{
    /// <summary>
    /// Interaction logic for UC_DoiMatkhau.xaml
    /// </summary>
    public partial class UC_DoiMatkhau : System.Windows.Controls.UserControl
    {
        public UC_DoiMatkhau()
        {
            InitializeComponent();
        }
        private void Clear()
        {
            txtOldPassword.Clear();
            txtNewPassword.Clear();
            txtNewPasswordConfirm.Clear();
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }
        private bool KiemTraMKCu()
        {
            if (txtOldPassword.Password.Trim().Length == 0)
                return false;
            bool flag = true;
            try
            {
                ConnectionViewModel.Instance.Connect();
                string query = "Select Password from Users where UserID ='" + MyUser.Instance.UserID + "'";
                System.Data.SqlClient.SqlDataReader reader = ConnectionViewModel.Instance.GetExecuteReader(query);
                if (reader.Read())
                    if (!reader.GetString(0).Trim().Equals(txtOldPassword.Password.Trim()))
                        flag = false;
                ConnectionViewModel.Instance.DisConnect();
            }
            catch (Exception ex)
            {
                int num = (int)System.Windows.MessageBox.Show("Lỗi KiemTraMKCu: " + ex.Message);
                ConnectionViewModel.Instance.DisConnect();
            }
            return flag;
        }

        private bool KiemTraMKMoi()
        {
            string str1 = this.txtNewPassword.Password.Trim();
            string str2 = this.txtNewPasswordConfirm.Password.Trim();
            bool flag = true;
            if (str1.Trim().Length == 0 || !str1.Equals(str2))
                flag = false;
            return flag;
        }
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (!this.KiemTraMKCu())
            {
                int num = (int)System.Windows.Forms.MessageBox.Show("Mật khẩu cũ nhập vào không đúng, vui lòng nhập lại mật khẩu cũ", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txtOldPassword.Focus();
            }
            else if (!this.KiemTraMKMoi())
            {
                int num = (int)System.Windows.Forms.MessageBox.Show("Mật khẩu mới bạn nhập không khớp nhau, vui lòng nhập lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txtNewPasswordConfirm.Focus();
            }
            else
            {
                try
                {
                    ConnectionViewModel.Instance.Connect();
                    string sqlstatement = "Update Users set Password = '" + this.txtNewPasswordConfirm.Password.Trim() + "' where UserID ='" + MyUser.Instance.UserID + "'";
                    int value = ConnectionViewModel.Instance.GetExecuteNonQuerry(sqlstatement);
                    int num = (int)System.Windows.Forms.MessageBox.Show("Đổi mật khẩu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    ConnectionViewModel.Instance.DisConnect();
                    Clear();
                }
                catch (Exception ex)
                {
                    int num = (int)System.Windows.MessageBox.Show("Lỗi btnDoiMK_Click: " + ex.Message);
                }
            }
        }
    }
}
