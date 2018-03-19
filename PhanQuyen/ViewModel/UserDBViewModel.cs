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

        public MyUser getUser(String userID, String password)
        {

            try
            {
                DataClasses_thanleDataContext serverDataContext = new DataClasses_thanleDataContext();
                //SqlCommand command = new SqlCommand(SQL_SELECT_LOGIN, ConnectionViewModel.getInstance.getConnection);
                ////ConnectionViewModel.getInstance.Connect();
                //command.Parameters.AddWithValue("@userid", userID);
                //command.Parameters.AddWithValue("@password", password);
                var user = (from x in serverDataContext.Users
                            where x.UserID == userID && x.Password == password
                            select x).First();
                //SqlDataReader dataReader = command.ExecuteReader();
                if (user != null)
                {

                    MyUser.Instance.UserID = user.UserID;
                    MyUser.Instance.Password = user.Password;
                    MyUser.Instance.UserName = user.Username;
                    MyUser.Instance.UserGroup = user.UserGroup;
                    MyUser.Instance.ToID = user.ToID;
                    MyUser.Instance.MayID = user.MayID;
                }
            }
            catch (Exception e)
            {

            }
            return MyUser.Instance;
        }
    }
}
