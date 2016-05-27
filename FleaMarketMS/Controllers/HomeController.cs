using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MESIII;
using System.Data;
using MES.Models;

namespace FleaMarketMS.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ISDWeb client = new ISDWeb(0);
        public string res;
        public UserModel userModel = new UserModel();
        public CmnSrvLib cmn = new CmnSrvLib();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        public void Login(string UserNO, string PassWD)
        {
            try
            {
                string sParam = "<root><UserInfo>";
                sParam += "<UserID>-1</UserID>";
                sParam += "<UserNO>" + UserNO + "</UserNO>";
                sParam += "<PassWD></PassWD>";
                sParam += "</UserInfo></root>";
                DataSet oDS = client.ctEnumerateData("ManagerSO.QryUserInfo001", sParam, -1, -1);
                if (cmn.CheckEOF(oDS))
                {
                    string passwd = cmn.cap_string(oDS.Tables[0].Rows[0]["PassWD"]);
                    if (cmn.MyEncode001(PassWD).Equals(passwd))
                    {

                        Session["UserIcon"] = oDS.Tables[0].Rows[0]["IconSrc"].ToString();
                        Session["UserName"] = oDS.Tables[0].Rows[0]["UserName"].ToString();
                        sParam = "<root><ProgramInfo>";
                        sParam += "<UserID>" + Session["USER_ID"].ToString() + "</UserID>";
                        sParam += "<RoleIDList></RoleIDList>";
                        sParam += "</ProgramInfo></root>";
                        oDS = client.ctEnumerateData("SystemSO.QryProgramInfo002", sParam, -1, -1);
                        Session["USER_PROGRAM"] = oDS.GetXml();
                        res = "{\"status\" : \"OK\",\"msg\": \"success\",\"username\":\"" + Session["UserName"] + "\",\"usericon\":\"" + Url.Content("~/assets/userIcon/" + Session["userIcon"]) + "\"}";
                    }
                    else
                        res = "{\"status\" : \"OK\",\"msg\": \"fail\",\"username\":\"\",\"usericon\":\"\"}";
                }
                else
                {
                    res = "{\"status\" : \"OK\",\"msg\": \"fail\",\"username\":\"\",\"usericon\":\"\"}";
                }
            }
            catch (Exception e1)
            {
                userModel.MESSAGE = e1.Message;
                string error_message = Microsoft.JScript.GlobalObject.escape(e1.Message);
                res = "{\"status\" : \"error\",\"msg\": \"error\",\"error_desc\":\"" + error_message + "\"}";
            }
            Response.Write(res);
        }

        public void Logout()
        {
            try
            {
                Session.Remove("UserName");
                Session.Remove("UserIcon");
                res = "{\"status\" : \"OK\",\"msg\": \"OK\"}";
            }
            catch (Exception e1)
            {
                userModel.MESSAGE = e1.Message;
                string error_message = Microsoft.JScript.GlobalObject.escape(e1.Message);
                res = "{\"status\" : \"error\",\"msg\": \"error\",\"error_desc\":\"" + error_message + "\"}";
            }
            Response.Write(res);

        }
    }
}
