using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using MES.Models;
using MESIII;

namespace MES.Services
{
    #region //UserState
    public class UserState
    {
        public string PassCode { set; get; }
        public string UserId { set; get; }
        public string UserName { set; get; }    
    }
    #endregion

    public class BaseController : Controller
    {
        public int SERVER_ID { set; get; }

        public ISDWeb client;
        public CmnSrvLib cmn = new CmnSrvLib();
        public UserModel userModel = new UserModel();
        public string res = "";
        public int nResult = -1;

        public BaseController()
        {
        }

        public BaseController(System.Web.Routing.RequestContext requestContext)
        {
            SERVER_ID = int.Parse(this.HttpContext.Session["SERVER_ID"].ToString());
            this.OnInit(requestContext);
        }

        public virtual void OnInit(System.Web.Routing.RequestContext requestContext)
        {
            //if (requestContext.HttpContext.Session["USER_ID"] != null)
            //{
            //    string controllerName = requestContext.RouteData.Values["controller"].ToString() + "Controller";
            //    string actionName = requestContext.RouteData.Values["action"].ToString();
            //}
            //else
            //{

            //    requestContext.HttpContext.Response.Redirect("~/");
            //}
        }

         public virtual void RedirectUrl(string url)
         {
             this.Response.Clear();//这里是关键，清除在返回前已经设置好的标头信息，这样后面的跳转才不会报错
             this.Response.BufferOutput = true;//设置输出缓冲
             if (!this.Response.IsRequestBeingRedirected)//在跳转之前做判断,防止重复
             {
                 this.Response.Redirect(url, true);
             }
         }

        public virtual void OnInit()
        {
            if (this.HttpContext.Session["SERVER_ID"] == null)
            {
                this.HttpContext.Response.Redirect("~/");
            }
            else
            {
                SERVER_ID = int.Parse(this.HttpContext.Session["SERVER_ID"].ToString());
                client = new ISDWeb(SERVER_ID);
                string controllerName = this.RouteData.Values["controller"].ToString().ToUpper();
                string actionName = this.RouteData.Values["action"].ToString().ToUpper();

                if (!actionName.Substring(0, 3).Equals("EXCEL")
                    && !actionName.Substring(0, 2).Equals("TX")
                    && !actionName.Substring(0, 3).Equals("QRY"))
                {
                    //cap_xml();

                    switch (controllerName + "_" + actionName)
                    {
                        case "HOME_INDEX": break;
                        case "HOME_A": break;
                        case "HOME_QRYTIME001": break;
                        case "SYS_LOGINBANNER": break;
                        case "SYS_LOGINBANNERPDA": break;
                        case "SYS_USERMENU": break;
                        case "SYS_PAGEVAILDCODE": break;
                        case "SYS_QRYBULLETININFO": break;
                        case "SYS_TXLOGIN": break;
                        case "SYS_TXLOGOUT": break;
                        case "SYS_TXPDALOGIN": break;
        
                        default:
                            if (this.HttpContext.Session["USER_ID"] != null)
                            {
                                //if (Convert.ToInt64(this.HttpContext.Session["USER_ID"]) > -1)//这里是判断是否登陆的
                                //{
                                //    try
                                //    {
                                //    }
                                //    catch (MyException e1)
                                //    {
                                //        Console.WriteLine(e1.Message);
                                //    }
                                //}
                                //else
                                //{
                                //    this.HttpContext.Response.Redirect("~/");
                                //}
                            }
                            else
                            {
                                this.HttpContext.Response.Redirect("~/");
                            }
                            break;
                    }
                }
            }
        }

        protected override void Execute(System.Web.Routing.RequestContext requestContext)
        {
            base.Execute(requestContext);            
        }

        protected override void ExecuteCore()
        {
            base.ExecuteCore();            
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.OnInit();
        }

        protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);        
        }

        protected override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);            
        }

        #region //cap_xml
        private void cap_xml()
        {
            #region //GET XML DATA , ERROR DATA
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(Server.MapPath("~/App_Data/GUI001T.xml"));
            ViewData["GUI_DATA"] = xmldoc;

            XmlDocument xmlError = new XmlDocument();
            xmlError.Load(Server.MapPath("~/App_Data/ERROR001T.xml"));
            ViewData["ERROR_DATA"] = xmlError;

            XmlDocument xmlDefault = new XmlDocument();
            xmlDefault.Load(Server.MapPath("~/App_Data/DEFAULT001T.xml"));
            ViewData["DEFAULT_DATA"] = xmlDefault;
            #endregion
        }
        #endregion

        #region //JsonCharFilter
        public static string JsonCharFilter(string sourceStr)
        {

            sourceStr = sourceStr.Replace("\\", "\\\\");

            sourceStr = sourceStr.Replace("\b", "\\\b");

            sourceStr = sourceStr.Replace("\t", "\\\t");

            sourceStr = sourceStr.Replace("\n", "\\\n");

            sourceStr = sourceStr.Replace("\n", "\\\n");

            sourceStr = sourceStr.Replace("\f", "\\\f");

            sourceStr = sourceStr.Replace("\r", "\\\r");

            return sourceStr.Replace("\"", "\\\"");

        }
        #endregion
    }
}
