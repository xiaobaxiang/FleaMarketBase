using MES.Models;
using MES.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MESIII;
using System.Text;

namespace FleaMarketMS.Controllers
{
    public class ManagerController : BaseController
    {
        //
        // GET: /Manager/

        public ActionResult Index()
        {
            return View();
        }

        #region //ActionResult

        public ActionResult ProgramInfo(UserModel userModel)
        {
            userModel.CURR_DOMAIN = "";
            userModel.CURR_PACKAGE = "管理";
            userModel.CURR_PROGRAM = "功能模块";
            return View(userModel);
        }

        public ActionResult StatusInfo(UserModel userModel)
        {
            userModel.CURR_DOMAIN = "";
            userModel.CURR_PACKAGE = "管理";
            userModel.CURR_PROGRAM = "状态管理";
            return View(userModel);
        }

        public ActionResult TypeInfo(UserModel userModel)
        {
            userModel.CURR_DOMAIN = "";
            userModel.CURR_PACKAGE = "管理";
            userModel.CURR_PROGRAM = "类型管理";
            return View(userModel);
        }

        public ActionResult RoleInfo(UserModel userModel)
        {
            userModel.CURR_DOMAIN = "";
            userModel.CURR_PACKAGE = "管理";
            userModel.CURR_PROGRAM = "角色管理";
            return View(userModel);
        }

        public ActionResult UserInfo(UserModel userModel)
        {
            userModel.CURR_DOMAIN = "";
            userModel.CURR_PACKAGE = "管理";
            userModel.CURR_PROGRAM = "用户管理";
            return View(userModel);
        }

        public ActionResult SenseWord(UserModel userModel)
        {
            userModel.CURR_DOMAIN = "";
            userModel.CURR_PACKAGE = "管理";
            userModel.CURR_PROGRAM = "词语管理";
            return View(userModel);
        }
        #endregion

        public ActionResult Category(UserModel userModel)
        {
            userModel.CURR_DOMAIN = "";
            userModel.CURR_PACKAGE = "管理";
            userModel.CURR_PROGRAM = "物品类别管理";
            return View(userModel);
        }

        public ActionResult PostHistory(UserModel userModel)
        {
            userModel.CURR_DOMAIN = "";
            userModel.CURR_PACKAGE = "管理";
            userModel.CURR_PROGRAM = "发布历史";

            int PAGE_INDEX = 1;
            if (Request["PAGE_INDEX"] != null) { int.TryParse(Request["PAGE_INDEX"], out PAGE_INDEX); }
            string sParam = "<root><PostMsg>";
            sParam += "<MsgID>-1</MsgID>";
            sParam += "<UserID>" + Session["USER_ID"].ToString() + "</UserID>";
            sParam += "<CategoryID>-1</CategoryID>";
            sParam += "<Title></Title>";
            sParam += "<MsgContent></MsgContent>";
            sParam += "<PictureSrc></PictureSrc>";
            sParam += "<StatusNO>A</StatusNO>";
            sParam += "</PostMsg>";
            sParam += "<PAGE_INFO>";
            sParam += "<PAGE_INDEX>" + PAGE_INDEX + "</PAGE_INDEX>";
            sParam += "<PAGE_SIZE>20</PAGE_SIZE>";
            sParam += "<ORDER_BY>a.AddDateTime DESC</ORDER_BY>";
            sParam += "</PAGE_INFO>";
            sParam += "</root>";
            DataSet oDS = client.ctEnumerateData("ManagerSO.QryPostMsg002", sParam);
            IEnumerable<PostMsg> postMsgls = oDS.Tables["QryPostMsg002"].TabeToList<PostMsg>();
            ViewBag.PostMstLst = postMsgls;

            return View(userModel);
        }

        public ActionResult Review(UserModel userModel)
        {
            userModel.CURR_DOMAIN = "";
            userModel.CURR_PACKAGE = "管理";
            userModel.CURR_PROGRAM = "信息审核";
            return View(userModel);
        }

        public ActionResult Attention(UserModel userModel)
        {
            userModel.CURR_DOMAIN = "";
            userModel.CURR_PACKAGE = "管理";
            userModel.CURR_PROGRAM = "关注信息";
            return View(userModel);
        }

        public ActionResult Search(UserModel userModel)
        {
            userModel.CURR_DOMAIN = "";
            userModel.CURR_PACKAGE = "管理";
            userModel.CURR_PROGRAM = "搜索";
            return View(userModel);
        }

        [HttpGet]
        public ActionResult UserInfoSelf(UserModel userModel)
        {
            string sParam = "<root><UserInfo>";
            sParam += "<UserID>" + Session["USER_ID"].ToString() + "</UserID>";
            sParam += "<UserNO></UserNO>";
            sParam += "<UserName></UserName>";
            sParam += "<MobilePhone></MobilePhone>";
            sParam += "<PassWD></PassWD>";
            sParam += "<Email></Email>";
            sParam += "<IconSrc></IconSrc>";
            sParam += "<RoleCollect></RoleCollect>";
            sParam += "<LastLoginTime></LastLoginTime>";
            sParam += "<LastLoginIP></LastLoginIP>";
            sParam += "<LoginCount>-1</LoginCount>";
            sParam += "<RoleCollect>-1</RoleCollect>";
            sParam += "<TypeNO></TypeNO>";
            sParam += "<StatusNO>A</StatusNO>";
            sParam += "</UserInfo>";
            sParam += "<PAGE_INFO>";
            sParam += "<PAGE_INDEX>-1</PAGE_INDEX>";
            sParam += "<PAGE_SIZE>-1</PAGE_SIZE>";
            sParam += "<ORDER_BY></ORDER_BY>";
            sParam += "</PAGE_INFO>";
            sParam += "</root>";
            DataSet oDS = client.ctEnumerateData("ManagerSO.QryUserInfo001", sParam);
            if (cmn.CheckEOF(oDS))
            {
                ViewBag.UserID = cmn.cap_int(oDS.Tables[0].Rows[0]["UserID"]);
                ViewBag.UserNO = cmn.cap_string(oDS.Tables[0].Rows[0]["UserNO"]);
                ViewBag.IconSrc = cmn.cap_string(oDS.Tables[0].Rows[0]["IconSrc"]);
                ViewBag.UserName = cmn.cap_string(oDS.Tables[0].Rows[0]["UserName"]);
                ViewBag.MobilePhone = cmn.cap_string(oDS.Tables[0].Rows[0]["MobilePhone"]);
                ViewBag.Email = cmn.cap_string(oDS.Tables[0].Rows[0]["Email"]);
                ViewBag.IsValidate = cmn.cap_int(oDS.Tables[0].Rows[0]["IsValidate"]);
            }

            userModel.CURR_DOMAIN = "";
            userModel.CURR_PACKAGE = "管理";
            userModel.CURR_PROGRAM = "个人信息";
            return View(userModel);
        }

        [HttpPost]
        public ActionResult UserInfoSelf()
        {
            string sParam = "";
            try
            {
                string UserID = Request.Form["UserID"];
                string UserNO = Request.Form["UserNO"];
                string UserName = Request.Form["txtUserName"];
                string MobilePhone = Request.Form["txtMobilePhone"];
                string Email = Request.Form["txtEmail"];
                string IconSrc = Request.Form["txtIconSrc"];
                if (Request.Files[0].ContentLength>0)
                {
                    
                    Request.Files[0].SaveAs(Server.MapPath("~/assets/userIcon/" + UserNO + "." + Request.Files[0].FileName.Split('.')[1]));
                    IconSrc = UserNO + "." + Request.Files[0].FileName.Split('.')[1];
                }

                sParam = "<root><UserInfo>";
                sParam += "<UserID>" + UserID + "</UserID>";
                sParam += "<UserNO>" + UserNO + "</UserNO>";
                sParam += "<UserName>" + UserName + "</UserName>";
                sParam += "<MobilePhone>" + MobilePhone + "</MobilePhone>";
                sParam += "<Email>" + Email + "</Email>";
                sParam += "<IconSrc>" + IconSrc + "</IconSrc>";
                sParam += "</UserInfo></root>";
                nResult = client.ctPostTxact("ManagerSO.TxUserInfo001", sParam, TxTypeConsts.TxTypeUpdate);
            }
            catch (Exception e1)
            {
                userModel.MESSAGE = e1.Message;
                string error_message = Microsoft.JScript.GlobalObject.escape(e1.Message);
                res = "{\"status\" : \"error\",\"msg\": \"error\",\"error_desc\":\"" + error_message + "\"}";
                return Content(res);
            }

            sParam = "<root><UserInfo>";
            sParam += "<UserID>" + Session["USER_ID"].ToString() + "</UserID>";
            sParam += "<UserNO></UserNO>";
            sParam += "<UserName></UserName>";
            sParam += "<MobilePhone></MobilePhone>";
            sParam += "<PassWD></PassWD>";
            sParam += "<Email></Email>";
            sParam += "<IconSrc></IconSrc>";
            sParam += "<RoleCollect></RoleCollect>";
            sParam += "<LastLoginTime></LastLoginTime>";
            sParam += "<LastLoginIP></LastLoginIP>";
            sParam += "<LoginCount>-1</LoginCount>";
            sParam += "<RoleCollect>-1</RoleCollect>";
            sParam += "<TypeNO></TypeNO>";
            sParam += "<StatusNO>A</StatusNO>";
            sParam += "</UserInfo>";
            sParam += "<PAGE_INFO>";
            sParam += "<PAGE_INDEX>-1</PAGE_INDEX>";
            sParam += "<PAGE_SIZE>-1</PAGE_SIZE>";
            sParam += "<ORDER_BY></ORDER_BY>";
            sParam += "</PAGE_INFO>";
            sParam += "</root>";
            DataSet oDS = client.ctEnumerateData("ManagerSO.QryUserInfo001", sParam);
            if (cmn.CheckEOF(oDS))
            {
                ViewBag.UserID = cmn.cap_int(oDS.Tables[0].Rows[0]["UserID"]);
                ViewBag.UserNO = cmn.cap_string(oDS.Tables[0].Rows[0]["UserNO"]);
                ViewBag.IconSrc = cmn.cap_string(oDS.Tables[0].Rows[0]["IconSrc"]);
                ViewBag.UserName = cmn.cap_string(oDS.Tables[0].Rows[0]["UserName"]);
                ViewBag.MobilePhone = cmn.cap_string(oDS.Tables[0].Rows[0]["MobilePhone"]);
                ViewBag.Email = cmn.cap_string(oDS.Tables[0].Rows[0]["Email"]);
                ViewBag.UserNO = cmn.cap_string(oDS.Tables[0].Rows[0]["UserNO"]);
                ViewBag.IsValidate = cmn.cap_int(oDS.Tables[0].Rows[0]["IsValidate"]);
            }
            userModel.CURR_DOMAIN = "";
            userModel.CURR_PACKAGE = "管理";
            userModel.CURR_PROGRAM = "个人信息";
            return View(userModel);
        }

        [HttpGet]
        public ActionResult ChangePassWord(UserModel userModel)
        {
            string sParam = "<root><UserInfo>";
            sParam += "<UserID>" + Session["USER_ID"].ToString() + "</UserID>";
            sParam += "<UserNO></UserNO>";
            sParam += "<UserName></UserName>";
            sParam += "<MobilePhone></MobilePhone>";
            sParam += "<PassWD></PassWD>";
            sParam += "<Email></Email>";
            sParam += "<IconSrc></IconSrc>";
            sParam += "<RoleCollect></RoleCollect>";
            sParam += "<LastLoginTime></LastLoginTime>";
            sParam += "<LastLoginIP></LastLoginIP>";
            sParam += "<LoginCount>-1</LoginCount>";
            sParam += "<RoleCollect>-1</RoleCollect>";
            sParam += "<TypeNO></TypeNO>";
            sParam += "<StatusNO>A</StatusNO>";
            sParam += "</UserInfo>";
            sParam += "<PAGE_INFO>";
            sParam += "<PAGE_INDEX>-1</PAGE_INDEX>";
            sParam += "<PAGE_SIZE>-1</PAGE_SIZE>";
            sParam += "<ORDER_BY></ORDER_BY>";
            sParam += "</PAGE_INFO>";
            sParam += "</root>";
            DataSet oDS = client.ctEnumerateData("ManagerSO.QryUserInfo001", sParam);
            if (cmn.CheckEOF(oDS))
            {
                ViewBag.UserID = cmn.cap_int(oDS.Tables[0].Rows[0]["UserID"]);
                ViewBag.UserNO = cmn.cap_string(oDS.Tables[0].Rows[0]["UserNO"]);

            }

            userModel.CURR_DOMAIN = "";
            userModel.CURR_PACKAGE = "管理";
            userModel.CURR_PROGRAM = "修改密码";
            return View(userModel);
        }

        public void ChangePassWord001(int UserID, string PassWD)
        {
            try
            {
                string sParam = "<root><UserInfo>";
                sParam += "<UserID>" + UserID + "</UserID>";
                sParam += "<PassWD>" + PassWD + "</PassWD>";
                sParam += "</UserInfo></root>";
                nResult = client.ctPostTxact("ManagerSO.TxUserInfo001", sParam, TxTypeConsts.TxTypeUpdate);
                if (nResult > 0) { res = "{\"status\" : \"success\",\"msg\": \"OK\",\"qrydata\":\"" + nResult + "\"}"; }
            }
            catch (Exception e1)
            {
                userModel.MESSAGE = e1.Message;
                string error_message = Microsoft.JScript.GlobalObject.escape(e1.Message);
                res = "{\"status\" : \"error\",\"msg\": \"error\",\"error_desc\":\"" + error_message + "\"}";
            }
            Response.Write(res);
        }
        #region //Qry

        #region //QryProgramInfo001
        public void QryProgramInfo001(int ProgramID = -1, string Path = "", string ProgramName = "", string ProgramDesc = "", int Sequence = -1, string StatusNO = ""
        , int PAGE_INDEX = -1, int PAGE_SIZE = -1, string ORDER_BY = "")
        {
            try
            {
                string sParam = "<root><ProgramInfo>";
                sParam += "<ProgramID>" + ProgramID + "</ProgramID>";
                sParam += "<Path>" + Path + "</Path>";
                sParam += "<ProgramName>" + ProgramName + "</ProgramName>";
                sParam += "<ProgramDesc>" + ProgramDesc + "</ProgramDesc>";
                sParam += "<Sequence>" + Sequence + "</Sequence>";
                sParam += "<StatusNO>" + StatusNO + "</StatusNO>";
                sParam += "</ProgramInfo>";
                sParam += "<PAGE_INFO>";
                sParam += "<PAGE_INDEX>" + PAGE_INDEX + "</PAGE_INDEX>";
                sParam += "<PAGE_SIZE>" + PAGE_SIZE + "</PAGE_SIZE>";
                sParam += "<ORDER_BY>" + ORDER_BY + "</ORDER_BY>";
                sParam += "</PAGE_INFO>";
                sParam += "</root>";
                DataSet oDS = client.ctEnumerateData("ManagerSO.QryProgramInfo001", sParam);
                string sData = CmnSrvLib.Dtb2Json(oDS.Tables["QryProgramInfo001"]).ToString();

                int TOTAL_COUNT = cmn.cap_int(oDS.Tables["TABLE_ROWS"].Rows[0]["TOTAL_COUNT"]);
                res = "{\"status\" : \"OK\",\"msg\": \"OK\",\"table_rows\": \"" + TOTAL_COUNT + "\",\"qrydata\":" + sData + "}";
            }
            catch (Exception e1)
            {
                userModel.MESSAGE = e1.Message;
                string error_message = Microsoft.JScript.GlobalObject.escape(e1.Message);
                res = "{\"status\" : \"error\",\"msg\": \"error\",\"error_desc\":\"" + error_message + "\"}";
            }

            Response.Write(res);
        }
        #endregion

        #region //QryStatusInfo001
        public void QryStatusInfo001(int StatusID = -1, string StatusNum = "", string TableName = "", string StatusName = "", string StatusDesc = "", string StatusNO = ""
        , int PAGE_INDEX = -1, int PAGE_SIZE = -1, string ORDER_BY = "")
        {
            try
            {
                string sParam = "<root><StatusInfo>";
                sParam += "<StatusID>" + StatusID + "</StatusID>";
                sParam += "<StatusNum>" + StatusNum + "</StatusNum>";
                sParam += "<TableName>" + TableName + "</TableName>";
                sParam += "<StatusName>" + StatusName + "</StatusName>";
                sParam += "<StatusDesc>" + StatusDesc + "</StatusDesc>";
                sParam += "<StatusNO>" + StatusNO + "</StatusNO>";
                sParam += "</StatusInfo>";
                sParam += "<PAGE_INFO>";
                sParam += "<PAGE_INDEX>" + PAGE_INDEX + "</PAGE_INDEX>";
                sParam += "<PAGE_SIZE>" + PAGE_SIZE + "</PAGE_SIZE>";
                sParam += "<ORDER_BY>" + ORDER_BY + "</ORDER_BY>";
                sParam += "</PAGE_INFO>";
                sParam += "</root>";
                DataSet oDS = client.ctEnumerateData("ManagerSO.QryStatusInfo001", sParam);
                string sData = CmnSrvLib.Dtb2Json(oDS.Tables["QryStatusInfo001"]).ToString();

                int TOTAL_COUNT = cmn.cap_int(oDS.Tables["TABLE_ROWS"].Rows[0]["TOTAL_COUNT"]);
                res = "{\"status\" : \"OK\",\"msg\": \"OK\",\"table_rows\": \"" + TOTAL_COUNT + "\",\"qrydata\":" + sData + "}";
            }
            catch (Exception e1)
            {
                userModel.MESSAGE = e1.Message;
                string error_message = Microsoft.JScript.GlobalObject.escape(e1.Message);
                res = "{\"status\" : \"error\",\"msg\": \"error\",\"error_desc\":\"" + error_message + "\"}";
            }

            Response.Write(res);
        }
        #endregion

        #region //QryTypeInfo001
        public void QryTypeInfo001(int TypeID = -1, string TypeNO = "", string TableName = "", string TypeName = "", string TypeDesc = "", string StatusNO = ""
        , int PAGE_INDEX = -1, int PAGE_SIZE = -1, string ORDER_BY = "")
        {
            try
            {
                string sParam = "<root><TypeInfo>";
                sParam += "<TypeID>" + TypeID + "</TypeID>";
                sParam += "<TypeNO>" + TypeNO + "</TypeNO>";
                sParam += "<TableName>" + TableName + "</TableName>";
                sParam += "<TypeName>" + TypeName + "</TypeName>";
                sParam += "<TypeDesc>" + TypeDesc + "</TypeDesc>";
                sParam += "<StatusNO>" + StatusNO + "</StatusNO>";
                sParam += "</TypeInfo>";
                sParam += "<PAGE_INFO>";
                sParam += "<PAGE_INDEX>" + PAGE_INDEX + "</PAGE_INDEX>";
                sParam += "<PAGE_SIZE>" + PAGE_SIZE + "</PAGE_SIZE>";
                sParam += "<ORDER_BY>" + ORDER_BY + "</ORDER_BY>";
                sParam += "</PAGE_INFO>";
                sParam += "</root>";
                DataSet oDS = client.ctEnumerateData("ManagerSO.QryTypeInfo001", sParam);
                string sData = CmnSrvLib.Dtb2Json(oDS.Tables["QryTypeInfo001"]).ToString();

                int TOTAL_COUNT = cmn.cap_int(oDS.Tables["TABLE_ROWS"].Rows[0]["TOTAL_COUNT"]);
                res = "{\"status\" : \"OK\",\"msg\": \"OK\",\"table_rows\": \"" + TOTAL_COUNT + "\",\"qrydata\":" + sData + "}";
            }
            catch (Exception e1)
            {
                userModel.MESSAGE = e1.Message;
                string error_message = Microsoft.JScript.GlobalObject.escape(e1.Message);
                res = "{\"status\" : \"error\",\"msg\": \"error\",\"error_desc\":\"" + error_message + "\"}";
            }

            Response.Write(res);
        }
        #endregion

        #region //QryRoleInfo001
        public void QryRoleInfo001(int RoleID = -1, string RoleName = "", string ProgramList = "", string RoleDesc = "", string StatusNO = ""
        , int PAGE_INDEX = -1, int PAGE_SIZE = -1, string ORDER_BY = "")
        {
            try
            {
                string sParam = "<root><RoleInfo>";
                sParam += "<RoleID>" + RoleID + "</RoleID>";
                sParam += "<RoleName>" + RoleName + "</RoleName>";
                sParam += "<ProgramList>" + ProgramList + "</ProgramList>";
                sParam += "<RoleDesc>" + RoleDesc + "</RoleDesc>";
                sParam += "<StatusNO>" + StatusNO + "</StatusNO>";
                sParam += "</RoleInfo>";
                sParam += "<PAGE_INFO>";
                sParam += "<PAGE_INDEX>" + PAGE_INDEX + "</PAGE_INDEX>";
                sParam += "<PAGE_SIZE>" + PAGE_SIZE + "</PAGE_SIZE>";
                sParam += "<ORDER_BY>" + ORDER_BY + "</ORDER_BY>";
                sParam += "</PAGE_INFO>";
                sParam += "</root>";
                DataSet oDS = client.ctEnumerateData("ManagerSO.QryRoleInfo001", sParam);
                string sData = CmnSrvLib.Dtb2Json(oDS.Tables["QryRoleInfo001"]).ToString();

                int TOTAL_COUNT = cmn.cap_int(oDS.Tables["TABLE_ROWS"].Rows[0]["TOTAL_COUNT"]);
                res = "{\"status\" : \"OK\",\"msg\": \"OK\",\"table_rows\": \"" + TOTAL_COUNT + "\",\"qrydata\":" + sData + "}";
            }
            catch (Exception e1)
            {
                userModel.MESSAGE = e1.Message;
                string error_message = Microsoft.JScript.GlobalObject.escape(e1.Message);
                res = "{\"status\" : \"error\",\"msg\": \"error\",\"error_desc\":\"" + error_message + "\"}";
            }

            Response.Write(res);
        }
        #endregion

        #region //QryUserInfo001
        public void QryUserInfo001(int UserID = -1, string UserNO = "", string UserName = "", string MobilePhone = "", string PassWD = "", string Email = "", string IconSrc = "", string LastLoginTime = "", string LastLoginIP = "", int LoginCount = -1, string RoleCollect = "", string TypeNO = "", string StatusNO = ""
        , int PAGE_INDEX = -1, int PAGE_SIZE = -1, string ORDER_BY = "")
        {
            try
            {
                string sParam = "<root><UserInfo>";
                sParam += "<UserID>" + UserID + "</UserID>";
                sParam += "<UserNO>" + UserNO + "</UserNO>";
                sParam += "<UserName>" + UserName + "</UserName>";
                sParam += "<MobilePhone>" + MobilePhone + "</MobilePhone>";
                sParam += "<PassWD>" + PassWD + "</PassWD>";
                sParam += "<Email>" + Email + "</Email>";
                sParam += "<IconSrc>" + IconSrc + "</IconSrc>";
                sParam += "<LastLoginTime>" + LastLoginTime + "</LastLoginTime>";
                sParam += "<LastLoginIP>" + LastLoginIP + "</LastLoginIP>";
                sParam += "<LoginCount>" + LoginCount + "</LoginCount>";
                sParam += "<RoleCollect>" + RoleCollect + "</RoleCollect>";
                sParam += "<TypeNO>" + TypeNO + "</TypeNO>";
                sParam += "<StatusNO>" + StatusNO + "</StatusNO>";
                sParam += "</UserInfo>";
                sParam += "<PAGE_INFO>";
                sParam += "<PAGE_INDEX>" + PAGE_INDEX + "</PAGE_INDEX>";
                sParam += "<PAGE_SIZE>" + PAGE_SIZE + "</PAGE_SIZE>";
                sParam += "<ORDER_BY>" + ORDER_BY + "</ORDER_BY>";
                sParam += "</PAGE_INFO>";
                sParam += "</root>";
                DataSet oDS = client.ctEnumerateData("ManagerSO.QryUserInfo001", sParam);
                string sData = CmnSrvLib.Dtb2Json(oDS.Tables["QryUserInfo001"]).ToString();

                int TOTAL_COUNT = cmn.cap_int(oDS.Tables["TABLE_ROWS"].Rows[0]["TOTAL_COUNT"]);
                res = "{\"status\" : \"OK\",\"msg\": \"OK\",\"table_rows\": \"" + TOTAL_COUNT + "\",\"qrydata\":" + sData + "}";
            }
            catch (Exception e1)
            {
                userModel.MESSAGE = e1.Message;
                string error_message = Microsoft.JScript.GlobalObject.escape(e1.Message);
                res = "{\"status\" : \"error\",\"msg\": \"error\",\"error_desc\":\"" + error_message + "\"}";
            }

            Response.Write(res);
        }
        #endregion

        #region //QrySenseWord001
        public void QrySenseWord001(int WordID = -1, string WordName = "", string WordTypeNO = "", string WordDesc = "", string StatusNO = ""
        , int PAGE_INDEX = -1, int PAGE_SIZE = -1, string ORDER_BY = "")
        {
            try
            {
                string sParam = "<root><SenseWord>";
                sParam += "<WordID>" + WordID + "</WordID>";
                sParam += "<WordName>" + WordName + "</WordName>";
                sParam += "<WordTypeNO>" + WordTypeNO + "</WordTypeNO>";
                sParam += "<WordDesc>" + WordDesc + "</WordDesc>";
                sParam += "<StatusNO>" + StatusNO + "</StatusNO>";
                sParam += "</SenseWord>";
                sParam += "<PAGE_INFO>";
                sParam += "<PAGE_INDEX>" + PAGE_INDEX + "</PAGE_INDEX>";
                sParam += "<PAGE_SIZE>" + PAGE_SIZE + "</PAGE_SIZE>";
                sParam += "<ORDER_BY>" + ORDER_BY + "</ORDER_BY>";
                sParam += "</PAGE_INFO>";
                sParam += "</root>";
                DataSet oDS = client.ctEnumerateData("ManagerSO.QrySenseWord001", sParam);
                string sData = CmnSrvLib.Dtb2Json(oDS.Tables["QrySenseWord001"]).ToString();

                int TOTAL_COUNT = cmn.cap_int(oDS.Tables["TABLE_ROWS"].Rows[0]["TOTAL_COUNT"]);
                res = "{\"status\" : \"OK\",\"msg\": \"OK\",\"table_rows\": \"" + TOTAL_COUNT + "\",\"qrydata\":" + sData + "}";
            }
            catch (Exception e1)
            {
                userModel.MESSAGE = e1.Message;
                string error_message = Microsoft.JScript.GlobalObject.escape(e1.Message);
                res = "{\"status\" : \"error\",\"msg\": \"error\",\"error_desc\":\"" + error_message + "\"}";
            }

            Response.Write(res);
        }
        #endregion

        #region //QryCategory001
        public void QryCategory001(int CategoryID = -1, string CategoryName = "", string CategoryDesc = "", string StatusNO = "", int ParentID = -1
        , int PAGE_INDEX = -1, int PAGE_SIZE = -1, string ORDER_BY = "")
        {
            try
            {
                string sParam = "<root><Category>";
                sParam += "<CategoryID>" + CategoryID + "</CategoryID>";
                sParam += "<CategoryName>" + CategoryName + "</CategoryName>";
                sParam += "<CategoryDesc>" + CategoryDesc + "</CategoryDesc>";
                sParam += "<StatusNO>" + StatusNO + "</StatusNO>";
                sParam += "<ParentID>" + ParentID + "</ParentID>";
                sParam += "</Category>";
                sParam += "<PAGE_INFO>";
                sParam += "<PAGE_INDEX>" + PAGE_INDEX + "</PAGE_INDEX>";
                sParam += "<PAGE_SIZE>" + PAGE_SIZE + "</PAGE_SIZE>";
                sParam += "<ORDER_BY>" + ORDER_BY + "</ORDER_BY>";
                sParam += "</PAGE_INFO>";
                sParam += "</root>";
                DataSet oDS = client.ctEnumerateData("ManagerSO.QryCategory001", sParam);
                string sData = CmnSrvLib.Dtb2Json(oDS.Tables["QryCategory001"]).ToString();

                int TOTAL_COUNT = cmn.cap_int(oDS.Tables["TABLE_ROWS"].Rows[0]["TOTAL_COUNT"]);
                res = "{\"status\" : \"OK\",\"msg\": \"OK\",\"table_rows\": \"" + TOTAL_COUNT + "\",\"qrydata\":" + sData + "}";
            }
            catch (Exception e1)
            {
                userModel.MESSAGE = e1.Message;
                string error_message = Microsoft.JScript.GlobalObject.escape(e1.Message);
                res = "{\"status\" : \"error\",\"msg\": \"error\",\"error_desc\":\"" + error_message + "\"}";
            }

            Response.Write(res);
        }
        #endregion

        #endregion

        #region //Tx

        #region //TxProgramInfo001
        public void TxProgramInfo001(string TxType, int ProgramID, string Path, string ProgramName, string ProgramDesc, int Sequence, string StatusNO)
        {
            try
            {
                string sParam = "<root><ProgramInfo>";
                sParam += "<ProgramID>" + ProgramID + "</ProgramID>";
                sParam += "<Path>" + Path + "</Path>";
                sParam += "<ProgramName>" + ProgramName + "</ProgramName>";
                sParam += "<ProgramDesc>" + ProgramDesc + "</ProgramDesc>";
                sParam += "<Sequence>" + Sequence + "</Sequence>";
                sParam += "<StatusNO>" + StatusNO + "</StatusNO>";
                sParam += "</ProgramInfo></root>";
                switch (TxType)
                {
                    case "A": nResult = client.ctPostTxact("ManagerSO.TxProgramInfo001", sParam, TxTypeConsts.TxTypeAddNew); break;
                    case "U": nResult = client.ctPostTxact("ManagerSO.TxProgramInfo001", sParam, TxTypeConsts.TxTypeUpdate); break;
                    case "D": nResult = client.ctPostTxact("ManagerSO.TxProgramInfo001", sParam, TxTypeConsts.TxTypeDelete); break;
                    case "C": nResult = client.ctPostTxact("ManagerSO.TxProgramInfo001", sParam, TxTypeConsts.TxTypeChange); break;
                    default: throw new MyException("transaction type error!");
                }
                if (nResult > 0) { res = "{\"status\" : \"success\",\"msg\": \"OK\",\"qrydata\":\"" + nResult + "\"}"; }
            }
            catch (Exception e1)
            {
                userModel.MESSAGE = e1.Message;
                string error_message = Microsoft.JScript.GlobalObject.escape(e1.Message);
                res = "{\"status\" : \"error\",\"msg\": \"error\",\"error_desc\":\"" + error_message + "\"}";
            }
            Response.Write(res);
        }
        #endregion

        #region //TxStatusInfo001
        public void TxStatusInfo001(string TxType, int StatusID, string StatusNum, string TableName, string StatusName, string StatusDesc, string StatusNO)
        {
            try
            {
                string sParam = "<root><StatusInfo>";
                sParam += "<StatusID>" + StatusID + "</StatusID>";
                sParam += "<StatusNum>" + StatusNum + "</StatusNum>";
                sParam += "<TableName>" + TableName + "</TableName>";
                sParam += "<StatusName>" + StatusName + "</StatusName>";
                sParam += "<StatusDesc>" + StatusDesc + "</StatusDesc>";
                sParam += "<StatusNO>" + StatusNO + "</StatusNO>";
                sParam += "</StatusInfo></root>";
                switch (TxType)
                {
                    case "A": nResult = client.ctPostTxact("ManagerSO.TxStatusInfo001", sParam, TxTypeConsts.TxTypeAddNew); break;
                    case "U": nResult = client.ctPostTxact("ManagerSO.TxStatusInfo001", sParam, TxTypeConsts.TxTypeUpdate); break;
                    case "D": nResult = client.ctPostTxact("ManagerSO.TxStatusInfo001", sParam, TxTypeConsts.TxTypeDelete); break;
                    case "C": nResult = client.ctPostTxact("ManagerSO.TxStatusInfo001", sParam, TxTypeConsts.TxTypeChange); break;
                    default: throw new MyException("transaction type error!");
                }
                if (nResult > 0) { res = "{\"status\" : \"success\",\"msg\": \"OK\",\"qrydata\":\"" + nResult + "\"}"; }
            }
            catch (Exception e1)
            {
                userModel.MESSAGE = e1.Message;
                string error_message = Microsoft.JScript.GlobalObject.escape(e1.Message);
                res = "{\"status\" : \"error\",\"msg\": \"error\",\"error_desc\":\"" + error_message + "\"}";
            }
            Response.Write(res);
        }
        #endregion

        #region //TxTypeInfo001
        public void TxTypeInfo001(string TxType, int TypeID, string TypeNO, string TableName, string TypeName, string TypeDesc, string StatusNO)
        {
            try
            {
                string sParam = "<root><TypeInfo>";
                sParam += "<TypeID>" + TypeID + "</TypeID>";
                sParam += "<TypeNO>" + TypeNO + "</TypeNO>";
                sParam += "<TableName>" + TableName + "</TableName>";
                sParam += "<TypeName>" + TypeName + "</TypeName>";
                sParam += "<TypeDesc>" + TypeDesc + "</TypeDesc>";
                sParam += "<StatusNO>" + StatusNO + "</StatusNO>";
                sParam += "</TypeInfo></root>";
                switch (TxType)
                {
                    case "A": nResult = client.ctPostTxact("ManagerSO.TxTypeInfo001", sParam, TxTypeConsts.TxTypeAddNew); break;
                    case "U": nResult = client.ctPostTxact("ManagerSO.TxTypeInfo001", sParam, TxTypeConsts.TxTypeUpdate); break;
                    case "D": nResult = client.ctPostTxact("ManagerSO.TxTypeInfo001", sParam, TxTypeConsts.TxTypeDelete); break;
                    case "C": nResult = client.ctPostTxact("ManagerSO.TxTypeInfo001", sParam, TxTypeConsts.TxTypeChange); break;
                    default: throw new MyException("transaction type error!");
                }
                if (nResult > 0) { res = "{\"status\" : \"success\",\"msg\": \"OK\",\"qrydata\":\"" + nResult + "\"}"; }
            }
            catch (Exception e1)
            {
                userModel.MESSAGE = e1.Message;
                string error_message = Microsoft.JScript.GlobalObject.escape(e1.Message);
                res = "{\"status\" : \"error\",\"msg\": \"error\",\"error_desc\":\"" + error_message + "\"}";
            }
            Response.Write(res);
        }
        #endregion

        #region //TxRoleInfo001
        public void TxRoleInfo001(string TxType, int RoleID, string RoleName, string ProgramList, string RoleDesc, string StatusNO)
        {
            try
            {
                string sParam = "<root><RoleInfo>";
                sParam += "<RoleID>" + RoleID + "</RoleID>";
                sParam += "<RoleName>" + RoleName + "</RoleName>";
                sParam += "<ProgramList>" + ProgramList + "</ProgramList>";
                sParam += "<RoleDesc>" + RoleDesc + "</RoleDesc>";
                sParam += "<StatusNO>" + StatusNO + "</StatusNO>";
                sParam += "</RoleInfo></root>";
                switch (TxType)
                {
                    case "A": nResult = client.ctPostTxact("ManagerSO.TxRoleInfo001", sParam, TxTypeConsts.TxTypeAddNew); break;
                    case "U": nResult = client.ctPostTxact("ManagerSO.TxRoleInfo001", sParam, TxTypeConsts.TxTypeUpdate); break;
                    case "D": nResult = client.ctPostTxact("ManagerSO.TxRoleInfo001", sParam, TxTypeConsts.TxTypeDelete); break;
                    case "C": nResult = client.ctPostTxact("ManagerSO.TxRoleInfo001", sParam, TxTypeConsts.TxTypeChange); break;
                    default: throw new MyException("transaction type error!");
                }
                if (nResult > 0) { res = "{\"status\" : \"success\",\"msg\": \"OK\",\"qrydata\":\"" + nResult + "\"}"; }
            }
            catch (Exception e1)
            {
                userModel.MESSAGE = e1.Message;
                string error_message = Microsoft.JScript.GlobalObject.escape(e1.Message);
                res = "{\"status\" : \"error\",\"msg\": \"error\",\"error_desc\":\"" + error_message + "\"}";
            }
            Response.Write(res);
        }
        #endregion

        #region //TxUserInfo001
        public void TxUserInfo001(string TxType, int UserID, string UserNO, string UserName
            , string MobilePhone, string Email, string IconSrc, string RoleCollect, string TypeNO, string StatusNO)
        {
            try
            {
                string sParam = "<root><UserInfo>";
                sParam += "<UserID>" + UserID + "</UserID>";
                sParam += "<UserNO>" + UserNO + "</UserNO>";
                sParam += "<UserName>" + UserName + "</UserName>";
                sParam += "<MobilePhone>" + MobilePhone + "</MobilePhone>";
                sParam += "<Email>" + Email + "</Email>";
                sParam += "<IconSrc>" + IconSrc + "</IconSrc>";
                sParam += "<LoginCount></LoginCount>";
                sParam += "<RoleCollect>" + RoleCollect + "</RoleCollect>";
                sParam += "<TypeNO>" + TypeNO + "</TypeNO>";
                sParam += "<StatusNO>" + StatusNO + "</StatusNO>";
                sParam += "</UserInfo></root>";
                switch (TxType)
                {
                    case "A": nResult = client.ctPostTxact("ManagerSO.TxUserInfo001", sParam, TxTypeConsts.TxTypeAddNew); break;
                    case "U": nResult = client.ctPostTxact("ManagerSO.TxUserInfo001", sParam, TxTypeConsts.TxTypeUpdate); break;
                    case "D": nResult = client.ctPostTxact("ManagerSO.TxUserInfo001", sParam, TxTypeConsts.TxTypeDelete); break;
                    case "C": nResult = client.ctPostTxact("ManagerSO.TxUserInfo001", sParam, TxTypeConsts.TxTypeChange); break;
                    default: throw new MyException("transaction type error!");
                }
                if (nResult > 0) { res = "{\"status\" : \"success\",\"msg\": \"OK\",\"qrydata\":\"" + nResult + "\"}"; }
            }
            catch (Exception e1)
            {
                userModel.MESSAGE = e1.Message;
                string error_message = Microsoft.JScript.GlobalObject.escape(e1.Message);
                res = "{\"status\" : \"error\",\"msg\": \"error\",\"error_desc\":\"" + error_message + "\"}";
            }
            Response.Write(res);
        }
        #endregion

        #region //TxSenseWord001
        public void TxSenseWord001(string TxType, int WordID, string WordName, string WordTypeNO, string WordDesc, string StatusNO)
        {
            try
            {
                string sParam = "<root><SenseWord>";
                sParam += "<WordID>" + WordID + "</WordID>";
                sParam += "<WordName>" + WordName + "</WordName>";
                sParam += "<WordTypeNO>" + WordTypeNO + "</WordTypeNO>";
                sParam += "<WordDesc>" + WordDesc + "</WordDesc>";
                sParam += "<StatusNO>" + StatusNO + "</StatusNO>";
                sParam += "</SenseWord></root>";
                switch (TxType)
                {
                    case "A": nResult = client.ctPostTxact("ManagerSO.TxSenseWord001", sParam, TxTypeConsts.TxTypeAddNew); break;
                    case "U": nResult = client.ctPostTxact("ManagerSO.TxSenseWord001", sParam, TxTypeConsts.TxTypeUpdate); break;
                    case "D": nResult = client.ctPostTxact("ManagerSO.TxSenseWord001", sParam, TxTypeConsts.TxTypeDelete); break;
                    case "C": nResult = client.ctPostTxact("ManagerSO.TxSenseWord001", sParam, TxTypeConsts.TxTypeChange); break;
                    default: throw new MyException("transaction type error!");
                }
                if (nResult > 0) { res = "{\"status\" : \"success\",\"msg\": \"OK\",\"qrydata\":\"" + nResult + "\"}"; }
            }
            catch (Exception e1)
            {
                userModel.MESSAGE = e1.Message;
                string error_message = Microsoft.JScript.GlobalObject.escape(e1.Message);
                res = "{\"status\" : \"error\",\"msg\": \"error\",\"error_desc\":\"" + error_message + "\"}";
            }
            Response.Write(res);
        }
        #endregion

        #region //TxCategory001
        public void TxCategory001(string TxType, int CategoryID, string CategoryName, string CategoryDesc, string StatusNO, int ParentID)
        {
            try
            {
                string sParam = "<root><Category>";
                sParam += "<CategoryID>" + CategoryID + "</CategoryID>";
                sParam += "<CategoryName>" + CategoryName + "</CategoryName>";
                sParam += "<CategoryDesc>" + CategoryDesc + "</CategoryDesc>";
                sParam += "<StatusNO>" + StatusNO + "</StatusNO>";
                sParam += "<ParentID>" + ParentID + "</ParentID>";
                sParam += "</Category></root>";
                switch (TxType)
                {
                    case "A": nResult = client.ctPostTxact("ManagerSO.TxCategory001", sParam, TxTypeConsts.TxTypeAddNew); break;
                    case "U": nResult = client.ctPostTxact("ManagerSO.TxCategory001", sParam, TxTypeConsts.TxTypeUpdate); break;
                    case "D": nResult = client.ctPostTxact("ManagerSO.TxCategory001", sParam, TxTypeConsts.TxTypeDelete); break;
                    case "C": nResult = client.ctPostTxact("ManagerSO.TxCategory001", sParam, TxTypeConsts.TxTypeChange); break;
                    default: throw new MyException("transaction type error!");
                }
                if (nResult > 0) { res = "{\"status\" : \"success\",\"msg\": \"OK\",\"qrydata\":\"" + nResult + "\"}"; }
            }
            catch (Exception e1)
            {
                userModel.MESSAGE = e1.Message;
                string error_message = Microsoft.JScript.GlobalObject.escape(e1.Message);
                res = "{\"status\" : \"error\",\"msg\": \"error\",\"error_desc\":\"" + error_message + "\"}";
            }
            Response.Write(res);
        }
        #endregion

        #region //TxPostMsg001
        [ValidateInput(false)]
        public void TxPostMsg001(string TxType, int MsgID, int CategoryID, string Title, string MsgContent, string PictureSrc, string StatusNO)
        {
            try
            {
                string sParam = "<root><PostMsg>";
                sParam += "<MsgID>" + MsgID + "</MsgID>";
                sParam += "<UserID>" + Session["USER_ID"].ToString() + "</UserID>";
                sParam += "<CategoryID>" + CategoryID + "</CategoryID>";
                sParam += "<Title>" + Title + "</Title>";
                sParam += "<MsgContent>" + MsgContent + "</MsgContent>";
                sParam += "<PictureSrc>" + PictureSrc + "</PictureSrc>";
                sParam += "<StatusNO>" + StatusNO + "</StatusNO>";
                sParam += "</PostMsg></root>";
                switch (TxType)
                {
                    case "A": nResult = client.ctPostTxact("ManagerSO.TxPostMsg001", sParam, TxTypeConsts.TxTypeAddNew); break;
                    case "U": nResult = client.ctPostTxact("ManagerSO.TxPostMsg001", sParam, TxTypeConsts.TxTypeUpdate); break;
                    case "D": nResult = client.ctPostTxact("ManagerSO.TxPostMsg001", sParam, TxTypeConsts.TxTypeDelete); break;
                    case "C": nResult = client.ctPostTxact("ManagerSO.TxPostMsg001", sParam, TxTypeConsts.TxTypeChange); break;
                    default: throw new MyException("transaction type error!");
                }
                if (nResult > 0) { res = "{\"status\" : \"success\",\"msg\": \"OK\",\"qrydata\":\"" + nResult + "\"}"; }
            }
            catch (Exception e1)
            {
                userModel.MESSAGE = e1.Message;
                string error_message = Microsoft.JScript.GlobalObject.escape(e1.Message);
                res = "{\"status\" : \"error\",\"msg\": \"error\",\"error_desc\":\"" + error_message + "\"}";
            }
            Response.Write(res);
        }
        #endregion
        #endregion


    }
}
