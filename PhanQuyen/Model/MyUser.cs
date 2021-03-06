﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MyUser
    {
        public String UserID { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String UserGroup { get; set; }
        public String ToID { get; set; }
        public String MayID { get; set; }
        public String Year { get; set; }
        public String Month { get; set; }
        public String Date { get; set; }
        public MyUser(String UserID, String UserName, String Password, String UserGroup, String ToID, String MayID)
        {
            this.UserID = UserID;
            this.UserName = UserName;
            this.Password = Password;
            this.UserGroup = UserGroup;
            this.ToID = ToID;
            this.MayID = MayID;
        }
        private static MyUser _instance;
        public static MyUser Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MyUser();
                return _instance;
            }
        }
        private MyUser() { }
    }
}
