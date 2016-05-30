using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MESIII;
using System.Data;

namespace FleaMarketMS
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start()//会话开始时记录用户的一些信息
        {
            //Session["ORA_SERVER_ID"] = 1;
            Session["SERVER_ID"] = 0;
            //Session["ERP_SERVER_ID"] = 2;
            Session["LANGUAGE"] = "T";

            Session["CLIENT_IP"] = Request.ServerVariables["REMOTE_ADDR"];
            //Session["LOCAL_PATH"] = Request.ServerVariables["APPL_PHYSICAL_PATH"];
            //Session["XML_PATH"] = Request.ServerVariables["APPL_PHYSICAL_PATH"] + @"\App_Data\";
            //Session["HTTP_PATH"] = Request.ServerVariables["SERVER_NAME"] + Request.ApplicationPath;
            Session["SERVER_PATH"] = "http://" + Request.ServerVariables["SERVER_NAME"] + Request.ApplicationPath;
            //Session["IMAGE_PATH"] = Session["SERVER_PATH"].ToString() + "/img/";

            Session["AUTHENTICATED"] = false;
            Session["LOGIN_NO"] = "";
            Session["USER_ID"] =-2;
            Session["ROLE_ID"] = -1;
            Session["DEPT_ID"] = -1;
            Session["ROLE_NAME"] = "";
            Session["LOGIN_TIME"] = "";
            Session["LOGIN_COUNT"] = 0;
            Session["USER_MENU"] = "";
            Session["VAILD_CODE"] = "";

        }

        protected void Application_Error(Object sender, EventArgs e)//会话中如果应用程序出现错误会执行这个
        {
            Exception lastError = Server.GetLastError();
            if (lastError != null)
            {
                //Response.StatusCode = 404;
                Response.Redirect("~/Home/Error");

                string path = Context.Server.MapPath("~/App_Data/Error/") + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                string content = "=========================出错了!==============================\r\n";
                content += "出错时间:" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\r\n";
                content += "错误源:" + Server.GetLastError().Source + "\r\n";
                content += "异常消息:" + Server.GetLastError().Message + "\r\n";
                content += "当前方法:" + Server.GetLastError().TargetSite + "\r\n";
                content += "堆栈信息:" + Server.GetLastError().StackTrace + "\r\n";
                content += "=============================================================\r\n\r\n";
                File.AppendAllText(path, content);
                Server.ClearError();
            }
        }
    }
}