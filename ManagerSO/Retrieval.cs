using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MESIII;
using System.Data;
using System.Data.OleDb;
using System.Xml;

namespace ManagerSO
{
    public class Retrieval:ISDServer.IRetrieval
    {
        public System.Data.DataSet Execute(IProcess oContext, string sServiceID, string sParams, int nServiceID, int nPageIndex = 1, int nPageSize = 10)
        {
            DataSet ds = null;
            try
            {
                TArgsRV oArgsRv = new TArgsRV();
                oArgsRv.ServiceID = sServiceID;
                oArgsRv.PageIndex = nPageIndex;
                oArgsRv.PageSize = nPageSize;
                oArgsRv.Param = sParams;
                oArgsRv.Context = oContext;
                oArgsRv.Connecion = oContext.CNEstablish("FleaMarket", nServiceID);
                oArgsRv.Command = oContext.CMD("FleaMarket", nServiceID);
                //****************************************************************
                ManagerRv Qry = new ManagerRv();
                //****************************************************************
                switch (sServiceID)
                {
                    #region //Manager QUERY

                    //QryProgramInfo001 -- QUERY ProgramInfo MODE DATA
                    case "QryProgramInfo001": ds = Qry.QryProgramInfo001(oArgsRv); break;

                    //QryStatusInfo001 -- QUERY StatusInfo MODE DATA
                    case "QryStatusInfo001": ds = Qry.QryStatusInfo001(oArgsRv); break;

                    //QryTypeInfo001 -- QUERY TypeInfo MODE DATA
                    case "QryTypeInfo001": ds = Qry.QryTypeInfo001(oArgsRv); break;

                    //QryRoleInfo001 -- QUERY RoleInfo MODE DATA
                    case "QryRoleInfo001": ds = Qry.QryRoleInfo001(oArgsRv); break;

                    //QryUserInfo001 -- QUERY UserInfo MODE DATA
                    case "QryUserInfo001": ds = Qry.QryUserInfo001(oArgsRv); break;

                    //QrySenseWord001 -- QUERY SenseWord MODE DATA
                    case "QrySenseWord001": ds = Qry.QrySenseWord001(oArgsRv); break;

                    //QryCategory001 -- QUERY Category MODE DATA
                    case "QryCategory001": ds = Qry.QryCategory001(oArgsRv); break;

                    #endregion 
                    default: throw new SystemException("Unkown retrieval specifier [ " + sServiceID + " ] !!");
                }
                return ds;
            }
            catch (MyException e)
            {
                throw e;
            }
        }
        class ManagerRv
        {
            CmnSrvLib cmn = new CmnSrvLib(0);
            DataSet oResult = new DataSet("root");
            string qry = "", sql = "";

            #region //Manager QUERY

            #region //QryProgramInfo001 -- QUERY ProgramInfo MODE DATA
            public DataSet QryProgramInfo001(TArgsRV oArgsRv)
            {
                OleDbCommand cmd = oArgsRv.Command;
                qry = oArgsRv.ServiceID;
                //****************************************************************
                XmlDocument x = new XmlDocument();
                x.LoadXml(oArgsRv.Param);
                int ProgramID = cmn.ParserXML(x, "//ProgramInfo/ProgramID", -999);
                string Path = cmn.ParserXML(x, "//ProgramInfo/Path");
                string ProgramName = cmn.ParserXML(x, "//ProgramInfo/ProgramName");
                string ProgramDesc = cmn.ParserXML(x, "//ProgramInfo/ProgramDesc");
                int Sequence = cmn.ParserXML(x, "//ProgramInfo/Sequence", -999);
                string StatusNO = cmn.ParserXML(x, "//ProgramInfo/StatusNO");
                int PAGE_INDEX = cmn.ParserXML(x, "//PAGE_INFO/PAGE_INDEX", -1, false);
                int PAGE_SIZE = cmn.ParserXML(x, "//PAGE_INFO/PAGE_SIZE", oArgsRv.PageSize, false);
                string ORDER_BY = cmn.ParserXML(x, "//PAGE_INFO/ORDER_BY", false);
                //****************************************************************
                sql = " Select ProgramID,Path,ProgramName,ProgramDesc,Sequence,StatusNO";
                sql += " From ProgramInfo";
                sql += " Where ProgramID is not null";

                if (ProgramID > -1) { sql += " AND ProgramID = " + cmn.SQLQ(ProgramID); }
                if (!Path.Equals("")) { sql += " AND Path = " + cmn.SQLQ(Path); }
                if (!ProgramName.Equals("")) { sql += " AND ProgramName = " + cmn.SQLQ(ProgramName); }
                if (!ProgramDesc.Equals("")) { sql += " AND ProgramDesc = " + cmn.SQLQ(ProgramDesc); }
                if (Sequence > -1) { sql += " AND Sequence = " + cmn.SQLQ(Sequence); }
                if (!StatusNO.Equals("")) { sql += " AND StatusNO = " + cmn.SQLQ(StatusNO); }
                if (!ORDER_BY.Equals("")) { sql += " Order By " + ORDER_BY; }

                oResult = cmn.CmnRvEnumerate(sql, cmd, oArgsRv.ServiceID, PAGE_INDEX, PAGE_SIZE);
                return oResult;
            }
            #endregion

            #region //QryStatusInfo001 -- QUERY StatusInfo MODE DATA
            public DataSet QryStatusInfo001(TArgsRV oArgsRv)
            {
                OleDbCommand cmd = oArgsRv.Command;
                qry = oArgsRv.ServiceID;
                //****************************************************************
                XmlDocument x = new XmlDocument();
                x.LoadXml(oArgsRv.Param);
                int StatusID = cmn.ParserXML(x, "//StatusInfo/StatusID", -999);
                string StatusNum = cmn.ParserXML(x, "//StatusInfo/StatusNum");
                string TableName = cmn.ParserXML(x, "//StatusInfo/TableName");
                string StatusName = cmn.ParserXML(x, "//StatusInfo/StatusName");
                string StatusDesc = cmn.ParserXML(x, "//StatusInfo/StatusDesc");
                string StatusNO = cmn.ParserXML(x, "//StatusInfo/StatusNO");
                int PAGE_INDEX = cmn.ParserXML(x, "//PAGE_INFO/PAGE_INDEX", -1, false);
                int PAGE_SIZE = cmn.ParserXML(x, "//PAGE_INFO/PAGE_SIZE", oArgsRv.PageSize, false);
                string ORDER_BY = cmn.ParserXML(x, "//PAGE_INFO/ORDER_BY", false);
                //****************************************************************
                sql = " Select StatusID,StatusNum,TableName,StatusName,StatusDesc,StatusNO";
                sql += " From StatusInfo";
                sql += " Where StatusID is not null";

                if (StatusID > -1) { sql += " AND StatusID = " + cmn.SQLQ(StatusID); }
                if (!StatusNum.Equals("")) { sql += " AND StatusNum = " + cmn.SQLQ(StatusNum); }
                if (!TableName.Equals("")) { sql += " AND TableName = " + cmn.SQLQ(TableName); }
                if (!StatusName.Equals("")) { sql += " AND StatusName = " + cmn.SQLQ(StatusName); }
                if (!StatusDesc.Equals("")) { sql += " AND StatusDesc = " + cmn.SQLQ(StatusDesc); }
                if (!StatusNO.Equals("")) { sql += " AND StatusNO = " + cmn.SQLQ(StatusNO); }
                if (!ORDER_BY.Equals("")) { sql += " Order By " + ORDER_BY; }

                oResult = cmn.CmnRvEnumerate(sql, cmd, oArgsRv.ServiceID, PAGE_INDEX, PAGE_SIZE);
                return oResult;
            }
            #endregion

            #region //QryTypeInfo001 -- QUERY TypeInfo MODE DATA
            public DataSet QryTypeInfo001(TArgsRV oArgsRv)
            {
                OleDbCommand cmd = oArgsRv.Command;
                qry = oArgsRv.ServiceID;
                //****************************************************************
                XmlDocument x = new XmlDocument();
                x.LoadXml(oArgsRv.Param);
                int TypeID = cmn.ParserXML(x, "//TypeInfo/TypeID", -999);
                string TypeNO = cmn.ParserXML(x, "//TypeInfo/TypeNO");
                string TableName = cmn.ParserXML(x, "//TypeInfo/TableName");
                string TypeName = cmn.ParserXML(x, "//TypeInfo/TypeName");
                string TypeDesc = cmn.ParserXML(x, "//TypeInfo/TypeDesc");
                string StatusNO = cmn.ParserXML(x, "//TypeInfo/StatusNO");
                int PAGE_INDEX = cmn.ParserXML(x, "//PAGE_INFO/PAGE_INDEX", -1, false);
                int PAGE_SIZE = cmn.ParserXML(x, "//PAGE_INFO/PAGE_SIZE", oArgsRv.PageSize, false);
                string ORDER_BY = cmn.ParserXML(x, "//PAGE_INFO/ORDER_BY", false);
                //****************************************************************
                sql = " Select TypeID,TypeNO,TableName,TypeName,TypeDesc,StatusNO";
                sql += " From TypeInfo";
                sql += " Where TypeID is not null";

                if (TypeID > -1) { sql += " AND TypeID = " + cmn.SQLQ(TypeID); }
                if (!TypeNO.Equals("")) { sql += " AND TypeNO = " + cmn.SQLQ(TypeNO); }
                if (!TableName.Equals("")) { sql += " AND TableName = " + cmn.SQLQ(TableName); }
                if (!TypeName.Equals("")) { sql += " AND TypeName = " + cmn.SQLQ(TypeName); }
                if (!TypeDesc.Equals("")) { sql += " AND TypeDesc = " + cmn.SQLQ(TypeDesc); }
                if (!StatusNO.Equals("")) { sql += " AND StatusNO = " + cmn.SQLQ(StatusNO); }
                if (!ORDER_BY.Equals("")) { sql += " Order By " + ORDER_BY; }

                oResult = cmn.CmnRvEnumerate(sql, cmd, oArgsRv.ServiceID, PAGE_INDEX, PAGE_SIZE);
                return oResult;
            }
            #endregion

            #region //QryRoleInfo001 -- QUERY RoleInfo MODE DATA
            public DataSet QryRoleInfo001(TArgsRV oArgsRv)
            {
                OleDbCommand cmd = oArgsRv.Command;
                qry = oArgsRv.ServiceID;
                //****************************************************************
                XmlDocument x = new XmlDocument();
                x.LoadXml(oArgsRv.Param);
                int RoleID = cmn.ParserXML(x, "//RoleInfo/RoleID", -999);
                string RoleName = cmn.ParserXML(x, "//RoleInfo/RoleName");
                string ProgramList = cmn.ParserXML(x, "//RoleInfo/ProgramList");
                string RoleDesc = cmn.ParserXML(x, "//RoleInfo/RoleDesc");
                string StatusNO = cmn.ParserXML(x, "//RoleInfo/StatusNO");
                int PAGE_INDEX = cmn.ParserXML(x, "//PAGE_INFO/PAGE_INDEX", -1, false);
                int PAGE_SIZE = cmn.ParserXML(x, "//PAGE_INFO/PAGE_SIZE", oArgsRv.PageSize, false);
                string ORDER_BY = cmn.ParserXML(x, "//PAGE_INFO/ORDER_BY", false);
                //****************************************************************
                sql = " Select RoleID,RoleName,ProgramList,RoleDesc,StatusNO";
                sql += " From RoleInfo";
                sql += " Where RoleID is not null";

                if (RoleID > -1) { sql += " AND RoleID = " + cmn.SQLQ(RoleID); }
                if (!RoleName.Equals("")) { sql += " AND RoleName = " + cmn.SQLQ(RoleName); }
                if (!ProgramList.Equals("")) { sql += " AND ProgramList = " + cmn.SQLQ(ProgramList); }
                if (!RoleDesc.Equals("")) { sql += " AND RoleDesc = " + cmn.SQLQ(RoleDesc); }
                if (!StatusNO.Equals("")) { sql += " AND StatusNO = " + cmn.SQLQ(StatusNO); }
                if (!ORDER_BY.Equals("")) { sql += " Order By " + ORDER_BY; }

                oResult = cmn.CmnRvEnumerate(sql, cmd, oArgsRv.ServiceID, PAGE_INDEX, PAGE_SIZE);
                return oResult;
            }
            #endregion

            #region //QryUserInfo001 -- QUERY UserInfo MODE DATA
            public DataSet QryUserInfo001(TArgsRV oArgsRv)
            {
                OleDbCommand cmd = oArgsRv.Command;
                qry = oArgsRv.ServiceID;
                //****************************************************************
                XmlDocument x = new XmlDocument();
                x.LoadXml(oArgsRv.Param);
                int UserID = cmn.ParserXML(x, "//UserInfo/UserID", -999);
                string UserNO = cmn.ParserXML(x, "//UserInfo/UserNO");
                string UserName = cmn.ParserXML(x, "//UserInfo/UserName",false);
                string MobilePhone = cmn.ParserXML(x, "//UserInfo/MobilePhone", false);
                string PassWD = cmn.ParserXML(x, "//UserInfo/PassWD", false);
                string Email = cmn.ParserXML(x, "//UserInfo/Email", false);
                string IconSrc = cmn.ParserXML(x, "//UserInfo/IconSrc", false);
                string LastLoginTime = cmn.ParserXML(x, "//UserInfo/LastLoginTime", false);
                string LastLoginIP = cmn.ParserXML(x, "//UserInfo/LastLoginIP", false);
                int LoginCount = cmn.ParserXML(x, "//UserInfo/LoginCount", -999,false);
                string RoleCollect = cmn.ParserXML(x, "//UserInfo/RoleCollect",false);
                string TypeNO = cmn.ParserXML(x, "//UserInfo/TypeNO", false);
                string StatusNO = cmn.ParserXML(x, "//UserInfo/StatusNO", false);
                int PAGE_INDEX = cmn.ParserXML(x, "//PAGE_INFO/PAGE_INDEX", -1, false);
                int PAGE_SIZE = cmn.ParserXML(x, "//PAGE_INFO/PAGE_SIZE", oArgsRv.PageSize, false);
                string ORDER_BY = cmn.ParserXML(x, "//PAGE_INFO/ORDER_BY", false);
                //****************************************************************
                sql = " Select UserID,UserNO,UserName,MobilePhone,PassWD,Email,IconSrc,IsValidate,LastLoginTime,LastLoginIP,LoginCount,RoleCollect,a.TypeNO,a.StatusNO";
                sql += ",b.TypeName,b.TypeDesc";
                sql += " From UserInfo a";
                sql += " Inner join TypeInfo b on b.TableName='UserInfo' And a.TypeNO=b.TypeNO";
                sql += " Where UserID is not null";

                if (UserID > -1) { sql += " AND UserID = " + cmn.SQLQ(UserID); }
                if (!UserNO.Equals("")) { sql += " AND UserNO = " + cmn.SQLQ(UserNO); }
                if (!UserName.Equals("")) { sql += " AND UserName = " + cmn.SQLQ(UserName); }
                if (!MobilePhone.Equals("")) { sql += " AND MobilePhone = " + cmn.SQLQ(MobilePhone); }
                if (!PassWD.Equals("")) { sql += " AND PassWD = " + cmn.SQLQ(PassWD); }
                if (!Email.Equals("")) { sql += " AND Email = " + cmn.SQLQ(Email); }
                if (!IconSrc.Equals("")) { sql += " AND IconSrc = " + cmn.SQLQ(IconSrc); }
                if (!LastLoginTime.Equals("")) { sql += " AND LastLoginTime = " + cmn.SQLQ(LastLoginTime); }
                if (!LastLoginIP.Equals("")) { sql += " AND LastLoginIP = " + cmn.SQLQ(LastLoginIP); }
                if (LoginCount > -1) { sql += " AND LoginCount = " + cmn.SQLQ(LoginCount); }
                if (!RoleCollect.Equals("")) { sql += " AND RoleCollect = " + cmn.SQLQ(RoleCollect); }
                if (!TypeNO.Equals("")) { sql += " AND a.TypeNO = " + cmn.SQLQ(TypeNO); }
                if (!StatusNO.Equals("")) { sql += " AND a.StatusNO = " + cmn.SQLQ(StatusNO); }
                if (!ORDER_BY.Equals("")) { sql += " Order By " + ORDER_BY; }

                oResult = cmn.CmnRvEnumerate(sql, cmd, oArgsRv.ServiceID, PAGE_INDEX, PAGE_SIZE);
                return oResult;
            }
            #endregion

            #region //QrySenseWord001 -- QUERY SenseWord MODE DATA
            public DataSet QrySenseWord001(TArgsRV oArgsRv)
            {
                OleDbCommand cmd = oArgsRv.Command;
                qry = oArgsRv.ServiceID;
                //****************************************************************
                XmlDocument x = new XmlDocument();
                x.LoadXml(oArgsRv.Param);
                int WordID = cmn.ParserXML(x, "//SenseWord/WordID", -999);
                string WordName = cmn.ParserXML(x, "//SenseWord/WordName");
                string WordTypeNO = cmn.ParserXML(x, "//SenseWord/WordTypeNO");
                string WordDesc = cmn.ParserXML(x, "//SenseWord/WordDesc");
                string StatusNO = cmn.ParserXML(x, "//SenseWord/StatusNO");
                int PAGE_INDEX = cmn.ParserXML(x, "//PAGE_INFO/PAGE_INDEX", -1, false);
                int PAGE_SIZE = cmn.ParserXML(x, "//PAGE_INFO/PAGE_SIZE", oArgsRv.PageSize, false);
                string ORDER_BY = cmn.ParserXML(x, "//PAGE_INFO/ORDER_BY", false);
                //****************************************************************
                sql = " Select WordID,WordName,WordTypeNO,WordDesc,a.StatusNO";
                sql += ",b.TypeName,b.TypeDesc";
                sql += " From SenseWord a";
                sql += " Left join TypeInfo b on a.WordTypeNO=b.TypeNO and b.TableName='SenseWord'";
                sql += " Where WordID is not null";

                if (WordID > -1) { sql += " AND WordID = " + cmn.SQLQ(WordID); }
                if (!WordName.Equals("")) { sql += " AND WordName = " + cmn.SQLQ(WordName); }
                if (!WordTypeNO.Equals("")) { sql += " AND WordTypeNO = " + cmn.SQLQ(WordTypeNO); }
                if (!WordDesc.Equals("")) { sql += " AND WordDesc = " + cmn.SQLQ(WordDesc); }
                if (!StatusNO.Equals("")) { sql += " AND StatusNO = " + cmn.SQLQ(StatusNO); }
                if (!ORDER_BY.Equals("")) { sql += " Order By " + ORDER_BY; }

                oResult = cmn.CmnRvEnumerate(sql, cmd, oArgsRv.ServiceID, PAGE_INDEX, PAGE_SIZE);
                return oResult;
            }
            #endregion

            #region //QryCategory001 -- QUERY Category MODE DATA
            public DataSet QryCategory001(TArgsRV oArgsRv)
            {
                OleDbCommand cmd = oArgsRv.Command;
                qry = oArgsRv.ServiceID;
                //****************************************************************
                XmlDocument x = new XmlDocument();
                x.LoadXml(oArgsRv.Param);
                int CategoryID = cmn.ParserXML(x, "//Category/CategoryID", -999);
                string CategoryName = cmn.ParserXML(x, "//Category/CategoryName");
                string CategoryDesc = cmn.ParserXML(x, "//Category/CategoryDesc");
                string StatusNO = cmn.ParserXML(x, "//Category/StatusNO");
                int ParentID = cmn.ParserXML(x, "//Category/ParentID", -999);
                int PAGE_INDEX = cmn.ParserXML(x, "//PAGE_INFO/PAGE_INDEX", -1, false);
                int PAGE_SIZE = cmn.ParserXML(x, "//PAGE_INFO/PAGE_SIZE", oArgsRv.PageSize, false);
                string ORDER_BY = cmn.ParserXML(x, "//PAGE_INFO/ORDER_BY", false);
                //****************************************************************
                sql = " Select CategoryID,CategoryName,CategoryDesc,StatusNO,ParentID";
                sql += " From Category";
                sql += " Where CategoryID is not null";

                if (CategoryID > -1) { sql += " AND CategoryID = " + cmn.SQLQ(CategoryID); }
                if (!CategoryName.Equals("")) { sql += " AND CategoryName = " + cmn.SQLQ(CategoryName); }
                if (!CategoryDesc.Equals("")) { sql += " AND CategoryDesc = " + cmn.SQLQ(CategoryDesc); }
                if (!StatusNO.Equals("")) { sql += " AND StatusNO = " + cmn.SQLQ(StatusNO); }
                if (ParentID > -1) { sql += " AND ParentID = " + cmn.SQLQ(ParentID); }
                if (!ORDER_BY.Equals("")) { sql += " Order By " + ORDER_BY; }

                oResult = cmn.CmnRvEnumerate(sql, cmd, oArgsRv.ServiceID, PAGE_INDEX, PAGE_SIZE);
                return oResult;
            }
            #endregion

            #endregion
        }
    }
}
