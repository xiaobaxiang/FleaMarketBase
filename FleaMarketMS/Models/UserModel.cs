using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Xml;

namespace MES.Models
{
    public class UserModel
    {
        private string msAddnew = "", msUpdate = "", msDelete = "", msChange = "";

        public int USER_ID { get; set; }
        public string LOGIN_NO { get; set; }
        public string LOGIN_NAME_CH { get; set; }
        public string LOGIN_NAME_EN { get; set; }
        public string LOGIN_PWD { get; set; }
        public string VAILD_CODE { get; set; }
        public string GUI_LANGUAGE { get; set; }
        public string USER_MENU { get; set; }

        public string USER_DOMAIN { get; set; }
        public string USER_PACKAGE { get; set; }
        public string USER_PROGRAM { get; set; }

        public string CURR_DOMAIN { get; set; }
        public string CURR_DOMAIN_NAME { get; set; }
        public string CURR_PACKAGE { get; set; }
        public string CURR_PACKAGE_NAME { get; set; }
        public string CURR_PROGRAM { get; set; }
        public string CURR_PROGRAM_NAME { get; set; }

        public string Addnew { get { return msAddnew; } set { msAddnew = value; } }
        public string Update { get { return msUpdate; } set { msUpdate = value; } }
        public string Delete { get { return msDelete; } set { msDelete = value; } }
        public string Change { get { return msChange; } set { msChange = value; } }

        public string CLIENT_IP { get; set; }
        public string MESSAGE { get; set; }
    }
}