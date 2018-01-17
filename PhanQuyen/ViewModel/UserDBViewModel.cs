using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class UserDBViewModel
    {
        private const String TABLE_NAME_USER = "Users";
        private const String SQL_SELECT_LOGIN = "select username, usergroup, toid, mayid from " + TABLE_NAME_USER +
            " where userid = @userid and password = @password";


        private static UserDBViewModel _instance;
        public static UserDBViewModel getInstance
        {
            get
            {
                if (_instance == null)
                    _instance = new UserDBViewModel();
                return _instance;
            }
        }
        private UserDBViewModel() { }

        public User getUser(String userID, String password)
        {
            
            try
            {
                SqlCommand command = new SqlCommand(SQL_SELECT_LOGIN, ConnectionViewModel.getInstance.getConnection);
                //ConnectionViewModel.getInstance.Connect();
                command.Parameters.AddWithValue("@userid", userID);
                command.Parameters.AddWithValue("@password", password);

                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    User.getInstance.UserID = userID;
                    User.getInstance.Password = password;
                    User.getInstance.UserName = dataReader["username"].ToString();
                    User.getInstance.UserGroup = dataReader["usergroup"].ToString();
                    User.getInstance.ToID = dataReader["toid"].ToString().Trim();
                    User.getInstance.MayID = dataReader["mayid"].ToString();
                }
            }
            catch (Exception e)
            {

            }
            return User.getInstance;
        }
    }
}
