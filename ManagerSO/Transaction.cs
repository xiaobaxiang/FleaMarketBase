using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MESIII;
using System.Data.OleDb;
using System.Xml;
using System.Data;

namespace ManagerSO
{
    public class Transaction:ISDServer.ITransaction
    {
        public int Execute(IProcess oContext, string sServiceID, string sParams, TxTypeConsts nTxType, int nServerID)
        {
            try
            {
                TArgsTX oArgsTx = new TArgsTX();
                oArgsTx.ServiceID = sServiceID;
                oArgsTx.Param = sParams;
                oArgsTx.Context = oContext;
                oArgsTx.Connecion = oContext.CNEstablish("EMSSO", nServerID);
                oArgsTx.Command = oContext.CMD("EMSSO", nServerID);
                oArgsTx.ServerID = nServerID;
                oArgsTx.TxType = nTxType;
                oArgsTx.Result = -1;
                int nResult = 0;
                //****************************************************************
                oContext.TxBegin("FleaMarket", nServerID);
                //****************************************************************
                ManagerTx Tx = new ManagerTx();
                //****************************************************************
                switch (sServiceID)
                {
                    #region //Manager TRANSACTION

                    //TxProgramInfo001 -- DEAL ProgramInfo DATA MAINTAIN
                    case "TxProgramInfo001": nResult = Tx.TxProgramInfo001(oArgsTx); break;

                    //TxStatusInfo001 -- DEAL StatusInfo DATA MAINTAIN
                    case "TxStatusInfo001": nResult = Tx.TxStatusInfo001(oArgsTx); break;

                    //TxTypeInfo001 -- DEAL TypeInfo DATA MAINTAIN
                    case "TxTypeInfo001": nResult = Tx.TxTypeInfo001(oArgsTx); break;

                    //TxRoleInfo001 -- DEAL RoleInfo DATA MAINTAIN
                    case "TxRoleInfo001": nResult = Tx.TxRoleInfo001(oArgsTx); break;

                    //TxUserInfo001 -- DEAL UserInfo DATA MAINTAIN
                    case "TxUserInfo001": nResult = Tx.TxUserInfo001(oArgsTx); break;

                    //TxSenseWord001 -- DEAL SenseWord DATA MAINTAIN
                    case "TxSenseWord001": nResult = Tx.TxSenseWord001(oArgsTx); break;

                    //TxCategory001 -- DEAL Category DATA MAINTAIN
                    case "TxCategory001": nResult = Tx.TxCategory001(oArgsTx); break;

                    #endregion
                    default: throw new SystemException("Unkown transaction specifier [ " + sServiceID + " ]");
                }
                //****************************************************************			
                oContext.TxCommit("FleaMarket", nServerID);
                return nResult;
            }
            catch (MyException e)
            {
                oContext.TxRollback("FleaMarket", nServerID);
                throw e;
            }
        }

        class ManagerTx
        {
            CmnSrvLib cmn = new CmnSrvLib(0);
            ISDServer srv = new ISDServer();
            int nResult = 0;
            string sql = "", tx = "";

            #region //Manager TRANSACTION
            #region  //TxProgramInfo001 -- DEAL ProgramInfo DATA MAINTAIN
            public int TxProgramInfo001(TArgsTX oArgsTx)
            {
                OleDbCommand cmd = oArgsTx.Command;
                int nUserID = oArgsTx.Context.UserID();
                tx = oArgsTx.ServiceID;
                //***********************************************************************
                XmlDocument x = new XmlDocument();
                x.LoadXml(oArgsTx.Param);
                int ProgramID = cmn.ParserXML(x, "//ProgramInfo/ProgramID", -999);
                string Path = cmn.ParserXML(x, "//ProgramInfo/Path");
                string ProgramName = cmn.ParserXML(x, "//ProgramInfo/ProgramName");
                string ProgramDesc = cmn.ParserXML(x, "//ProgramInfo/ProgramDesc");
                int Sequence = cmn.ParserXML(x, "//ProgramInfo/Sequence", -999);
                string StatusNO = cmn.ParserXML(x, "//ProgramInfo/StatusNO");
                //***********************************************************************

                DataSet oDS = new DataSet();
                switch (oArgsTx.TxType)
                {
                    case TxTypeConsts.TxTypeAddNew:
                        if (Path.Equals("")) { throw new MyException("路径不能为空"); }
                        if (ProgramName.Equals("")) { throw new MyException("应用名称不能为空"); }
                        if (Sequence <= -1) { throw new MyException("顺序不能为空"); }

                        sql = " Insert Into ProgramInfo (Path,ProgramName,ProgramDesc,Sequence,StatusNO,";
                        sql += "AddUserID,AddDateTime,UpdateUserID,UpdateDateTime)";
                        sql += "Values(" + cmn.SQLQC(Path) + cmn.SQLQC(ProgramName) + cmn.SQLQC(ProgramDesc) + cmn.SQLQC(Sequence) + cmn.SQLQC("A");
                        sql += cmn.SQLQC(nUserID) + cmn.SQLQC(DateTime.Now) + cmn.SQLQC(nUserID) + cmn.SQLQ(DateTime.Now) + ")";
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                    case TxTypeConsts.TxTypeUpdate:
                        if (ProgramID <= -1) { throw new MyException("ProgramID不能为空"); }
                        if (Path.Equals("")) { throw new MyException("路径不能为空"); }
                        if (ProgramName.Equals("")) { throw new MyException("应用名称不能为空"); }
                        if (Sequence <= -1) { throw new MyException("顺序不能为空"); }

                        sql = " Update ProgramInfo Set ";
                        sql += "Path=" + cmn.SQLQC(Path);
                        sql += "ProgramName=" + cmn.SQLQC(ProgramName);
                        sql += "ProgramDesc=" + cmn.SQLQC(ProgramDesc);
                        sql += "Sequence=" + cmn.SQLQC(Sequence);
                        sql += "UpdateUserID=" + cmn.SQLQC(nUserID);
                        sql += "UpdateDateTime=" + cmn.SQLQ(DateTime.Now);
                        sql += " Where ProgramID =" + cmn.SQLQ(ProgramID);
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                    case TxTypeConsts.TxTypeDelete:
                        if (ProgramID <= -1) { throw new MyException("ProgramID不能为空"); }

                        sql = " Delete From ProgramInfo Where ProgramID =" + cmn.SQLQ(ProgramID);
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                    case TxTypeConsts.TxTypeChange:
                        if (ProgramID.Equals("")) { throw new MyException("ProgramID不能为空"); }
                        if (StatusNO.Equals("")) { throw new MyException("状态编号不能为空"); }

                        sql = " Update ProgramInfo Set ";
                        sql += "StatusNO=" + cmn.SQLQC(StatusNO);
                        sql += "UpdateUserID=" + cmn.SQLQC(nUserID);
                        sql += "UpdateDateTime=" + cmn.SQLQ(DateTime.Now);
                        sql += " Where ProgramID =" + cmn.SQLQ(ProgramID);
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                }
                return nResult;
            }
            #endregion

            #region  //TxStatusInfo001 -- DEAL StatusInfo DATA MAINTAIN
            public int TxStatusInfo001(TArgsTX oArgsTx)
            {
                OleDbCommand cmd = oArgsTx.Command;
                int nUserID = oArgsTx.Context.UserID();
                tx = oArgsTx.ServiceID;
                //***********************************************************************
                XmlDocument x = new XmlDocument();
                x.LoadXml(oArgsTx.Param);
                int StatusID = cmn.ParserXML(x, "//StatusInfo/StatusID", -999);
                string StatusNum = cmn.ParserXML(x, "//StatusInfo/StatusNum");
                string TableName = cmn.ParserXML(x, "//StatusInfo/TableName");
                string StatusName = cmn.ParserXML(x, "//StatusInfo/StatusName");
                string StatusDesc = cmn.ParserXML(x, "//StatusInfo/StatusDesc");
                string StatusNO = cmn.ParserXML(x, "//StatusInfo/StatusNO");
                //***********************************************************************

                DataSet oDS = new DataSet();
                switch (oArgsTx.TxType)
                {
                    case TxTypeConsts.TxTypeAddNew:
                        if (StatusNum.Equals("")) { throw new MyException("状态编号不能为空"); }
                        if (TableName.Equals("")) { throw new MyException("实体名称不能为空"); }
                        if (StatusName.Equals("")) { throw new MyException("状态名称不能为空"); }

                        sql = " Insert Into StatusInfo (StatusNum,TableName,StatusName,StatusDesc,StatusNO,";
                        sql += "AddUserID,AddDateTime,UpdateUserID,UpdateDateTime)";
                        sql += "Values(" + cmn.SQLQC(StatusNum) + cmn.SQLQC(TableName) + cmn.SQLQC(StatusName) + cmn.SQLQC(StatusDesc) + cmn.SQLQC("A");
                        sql += cmn.SQLQC(nUserID) + cmn.SQLQC(DateTime.Now) + cmn.SQLQC(nUserID) + cmn.SQLQ(DateTime.Now) + ")";
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                    case TxTypeConsts.TxTypeUpdate:
                        if (StatusID <= -1) { throw new MyException("ID不能为空"); }
                        if (StatusNum.Equals("")) { throw new MyException("状态编号不能为空"); }
                        if (TableName.Equals("")) { throw new MyException("实体名称不能为空"); }
                        if (StatusName.Equals("")) { throw new MyException("状态名称不能为空"); }

                        sql = " Update StatusInfo Set ";
                        sql += "StatusNum=" + cmn.SQLQC(StatusNum);
                        sql += "TableName=" + cmn.SQLQC(TableName);
                        sql += "StatusName=" + cmn.SQLQC(StatusName);
                        sql += "StatusDesc=" + cmn.SQLQC(StatusDesc);
                        sql += "UpdateUserID=" + cmn.SQLQC(nUserID);
                        sql += "UpdateDateTime=" + cmn.SQLQ(DateTime.Now);
                        sql += " Where StatusID =" + cmn.SQLQ(StatusID);
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                    case TxTypeConsts.TxTypeDelete:
                        if (StatusID <= -1) { throw new MyException("ID不能为空"); }

                        sql = " Delete From StatusInfo Where StatusID =" + cmn.SQLQ(StatusID);
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                    case TxTypeConsts.TxTypeChange:
                        if (StatusID.Equals("")) { throw new MyException("ID不能为空"); }
                        if (StatusNO.Equals("")) { throw new MyException("状态编号不能为空"); }

                        sql = " Update StatusInfo Set ";
                        sql += "StatusNO=" + cmn.SQLQC(StatusNO);
                        sql += "UpdateUserID=" + cmn.SQLQC(nUserID);
                        sql += "UpdateDateTime=" + cmn.SQLQ(DateTime.Now);
                        sql += " Where StatusID =" + cmn.SQLQ(StatusID);
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                }
                return nResult;
            }
            #endregion

            #region  //TxTypeInfo001 -- DEAL TypeInfo DATA MAINTAIN
            public int TxTypeInfo001(TArgsTX oArgsTx)
            {
                OleDbCommand cmd = oArgsTx.Command;
                int nUserID = oArgsTx.Context.UserID();
                tx = oArgsTx.ServiceID;
                //***********************************************************************
                XmlDocument x = new XmlDocument();
                x.LoadXml(oArgsTx.Param);
                int TypeID = cmn.ParserXML(x, "//TypeInfo/TypeID", -999);
                string TypeNO = cmn.ParserXML(x, "//TypeInfo/TypeNO");
                string TableName = cmn.ParserXML(x, "//TypeInfo/TableName");
                string TypeName = cmn.ParserXML(x, "//TypeInfo/TypeName");
                string TypeDesc = cmn.ParserXML(x, "//TypeInfo/TypeDesc");
                string StatusNO = cmn.ParserXML(x, "//TypeInfo/StatusNO");
                //***********************************************************************

                DataSet oDS = new DataSet();
                switch (oArgsTx.TxType)
                {
                    case TxTypeConsts.TxTypeAddNew:
                        if (TypeNO.Equals("")) { throw new MyException("类型编号不能为空"); }
                        if (TableName.Equals("")) { throw new MyException("实体名称不能为空"); }
                        if (TypeName.Equals("")) { throw new MyException("类型名称不能为空"); }

                        sql = " Insert Into TypeInfo (TypeNO,TableName,TypeName,TypeDesc,StatusNO,";
                        sql += "AddUserID,AddDateTime,UpdateUserID,UpdateDateTime)";
                        sql += "Values(" + cmn.SQLQC(TypeNO) + cmn.SQLQC(TableName) + cmn.SQLQC(TypeName) + cmn.SQLQC(TypeDesc) + cmn.SQLQC("A");
                        sql += cmn.SQLQC(nUserID) + cmn.SQLQC(DateTime.Now) + cmn.SQLQC(nUserID) + cmn.SQLQ(DateTime.Now) + ")";
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                    case TxTypeConsts.TxTypeUpdate:
                        if (TypeID <= -1) { throw new MyException("TypeID不能为空"); }
                        if (TypeNO.Equals("")) { throw new MyException("类型编号不能为空"); }
                        if (TableName.Equals("")) { throw new MyException("实体名称不能为空"); }
                        if (TypeName.Equals("")) { throw new MyException("类型名称不能为空"); }

                        sql = " Update TypeInfo Set ";
                        sql += "TypeNO=" + cmn.SQLQC(TypeNO);
                        sql += "TableName=" + cmn.SQLQC(TableName);
                        sql += "TypeName=" + cmn.SQLQC(TypeName);
                        sql += "TypeDesc=" + cmn.SQLQC(TypeDesc);
                        sql += "UpdateUserID=" + cmn.SQLQC(nUserID);
                        sql += "UpdateDateTime=" + cmn.SQLQ(DateTime.Now);
                        sql += " Where TypeID =" + cmn.SQLQ(TypeID);
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                    case TxTypeConsts.TxTypeDelete:
                        if (TypeID <= -1) { throw new MyException("TypeID不能为空"); }

                        sql = " Delete From TypeInfo Where TypeID =" + cmn.SQLQ(TypeID);
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                    case TxTypeConsts.TxTypeChange:
                        if (TypeID.Equals("")) { throw new MyException("TypeID不能为空"); }
                        if (StatusNO.Equals("")) { throw new MyException("状态编号不能为空"); }

                        sql = " Update TypeInfo Set ";
                        sql += "StatusNO=" + cmn.SQLQC(StatusNO);
                        sql += "UpdateUserID=" + cmn.SQLQC(nUserID);
                        sql += "UpdateDateTime=" + cmn.SQLQ(DateTime.Now);
                        sql += " Where TypeID =" + cmn.SQLQ(TypeID);
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                }
                return nResult;
            }
            #endregion

            #region  //TxRoleInfo001 -- DEAL RoleInfo DATA MAINTAIN
            public int TxRoleInfo001(TArgsTX oArgsTx)
            {
                OleDbCommand cmd = oArgsTx.Command;
                int nUserID = oArgsTx.Context.UserID();
                tx = oArgsTx.ServiceID;
                //***********************************************************************
                XmlDocument x = new XmlDocument();
                x.LoadXml(oArgsTx.Param);
                int RoleID = cmn.ParserXML(x, "//RoleInfo/RoleID", -999);
                string RoleName = cmn.ParserXML(x, "//RoleInfo/RoleName");
                string ProgramList = cmn.ParserXML(x, "//RoleInfo/ProgramList");
                string RoleDesc = cmn.ParserXML(x, "//RoleInfo/RoleDesc");
                string StatusNO = cmn.ParserXML(x, "//RoleInfo/StatusNO");
                //***********************************************************************

                DataSet oDS = new DataSet();
                switch (oArgsTx.TxType)
                {
                    case TxTypeConsts.TxTypeAddNew:
                        if (RoleName.Equals("")) { throw new MyException("角色名称不能为空"); }
                        if (ProgramList.Equals("")) { throw new MyException("应用列表不能为空"); }

                        sql = " Insert Into RoleInfo (RoleName,ProgramList,RoleDesc,StatusNO,";
                        sql += "AddUserID,AddDateTime,UpdateUserID,UpdateDateTime)";
                        sql += "Values(" + cmn.SQLQC(RoleName) + cmn.SQLQC(ProgramList) + cmn.SQLQC(RoleDesc) + cmn.SQLQC("A");
                        sql += cmn.SQLQC(nUserID) + cmn.SQLQC(DateTime.Now) + cmn.SQLQC(nUserID) + cmn.SQLQ(DateTime.Now) + ")";
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                    case TxTypeConsts.TxTypeUpdate:
                        if (RoleID <= -1) { throw new MyException("RoleID不能为空"); }
                        if (RoleName.Equals("")) { throw new MyException("角色名称不能为空"); }
                        if (ProgramList.Equals("")) { throw new MyException("应用列表不能为空"); }

                        sql = " Update RoleInfo Set ";
                        sql += "RoleName=" + cmn.SQLQC(RoleName);
                        sql += "ProgramList=" + cmn.SQLQC(ProgramList);
                        sql += "RoleDesc=" + cmn.SQLQC(RoleDesc);
                        sql += "UpdateUserID=" + cmn.SQLQC(nUserID);
                        sql += "UpdateDateTime=" + cmn.SQLQ(DateTime.Now);
                        sql += " Where RoleID =" + cmn.SQLQ(RoleID);
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                    case TxTypeConsts.TxTypeDelete:
                        if (RoleID <= -1) { throw new MyException("RoleID不能为空"); }

                        sql = " Delete From RoleInfo Where RoleID =" + cmn.SQLQ(RoleID);
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                    case TxTypeConsts.TxTypeChange:
                        if (RoleID.Equals("")) { throw new MyException("RoleID不能为空"); }
                        if (StatusNO.Equals("")) { throw new MyException("状态编号不能为空"); }

                        sql = " Update RoleInfo Set ";
                        sql += "StatusNO=" + cmn.SQLQC(StatusNO);
                        sql += "UpdateUserID=" + cmn.SQLQC(nUserID);
                        sql += "UpdateDateTime=" + cmn.SQLQ(DateTime.Now);
                        sql += " Where RoleID =" + cmn.SQLQ(RoleID);
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                }
                return nResult;
            }
            #endregion

            #region  //TxUserInfo001 -- DEAL UserInfo DATA MAINTAIN
            public int TxUserInfo001(TArgsTX oArgsTx)
            {
                OleDbCommand cmd = oArgsTx.Command;
                int nUserID = oArgsTx.Context.UserID();
                tx = oArgsTx.ServiceID;
                //***********************************************************************
                XmlDocument x = new XmlDocument();
                x.LoadXml(oArgsTx.Param);
                int UserID = cmn.ParserXML(x, "//UserInfo/UserID", -999);
                string UserNO = cmn.ParserXML(x, "//UserInfo/UserNO", false);
                string UserName = cmn.ParserXML(x, "//UserInfo/UserName", false);
                string MobilePhone = cmn.ParserXML(x, "//UserInfo/MobilePhone", false);
                string PassWD = cmn.ParserXML(x, "//UserInfo/PassWD",false);
                string Email = cmn.ParserXML(x, "//UserInfo/Email", false);
                string IconSrc = cmn.ParserXML(x, "//UserInfo/IconSrc", false);
                string LastLoginTime = cmn.ParserXML(x, "//UserInfo/LastLoginTime", false);
                string LastLoginIP = cmn.ParserXML(x, "//UserInfo/LastLoginIP", false);
                int LoginCount = cmn.ParserXML(x, "//UserInfo/LoginCount", -999,false);
                string RoleCollect = cmn.ParserXML(x, "//UserInfo/RoleCollect", false);
                string TypeNO = cmn.ParserXML(x, "//UserInfo/TypeNO", false);
                string StatusNO = cmn.ParserXML(x, "//UserInfo/StatusNO", false);
                //***********************************************************************

                DataSet oDS = null;
                switch (oArgsTx.TxType)
                {
                    case TxTypeConsts.TxTypeAddNew:
                        sql = "Select UserID From UserInfo Where UserNO="+cmn.SQLQ(UserNO);
                        oDS = cmn.CmnRvEnumerate(sql, cmd, -1, -1);
                        if (cmn.CheckEOF(oDS)) { throw new MyException("用户登陆账号已存在"); }
                        string current = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        if (UserNO.Equals("")) { throw new MyException("用户登陆账号不能为空"); }
                        if (UserName.Equals("")) { throw new MyException("用户名不能为空"); }
                        if (PassWD.Equals("")) { PassWD = cmn.MyEncode001(UserNO); }
                        if (LastLoginTime.Equals("")) { LastLoginTime = current; }
                        if (LastLoginIP.Equals("")) { LastLoginIP = "0.0.0.0"; }
                        if (LoginCount <= -1) { LoginCount = 1; }
                        if (RoleCollect.Equals("")) { throw new MyException("角色不能为空"); }
                        if (TypeNO.Equals("")) { throw new MyException("类型不能为空"); }

                        sql = " Insert Into UserInfo (UserNO,UserName,MobilePhone,PassWD,Email,IconSrc,LastLoginTime,LastLoginIP,LoginCount,RoleCollect,TypeNO,StatusNO,";
                        sql += "AddUserID,AddDateTime,UpdateUserID,UpdateDateTime)";
                        sql += "Values(" + cmn.SQLQC(UserNO) + cmn.SQLQC(UserName) + cmn.SQLQC(MobilePhone) + cmn.SQLQC(PassWD) + cmn.SQLQC(Email) + cmn.SQLQC(IconSrc) + cmn.SQLQC(LastLoginTime) + cmn.SQLQC(LastLoginIP) + cmn.SQLQC(LoginCount) + cmn.SQLQC(RoleCollect) + cmn.SQLQC(TypeNO) + cmn.SQLQC("A");
                        sql += cmn.SQLQC(nUserID) + cmn.SQLQC(DateTime.Now) + cmn.SQLQC(nUserID) + cmn.SQLQ(DateTime.Now) + ")";
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                    case TxTypeConsts.TxTypeUpdate:
                        if (UserID <= -1) { throw new MyException("UserID不能为空"); }

                        sql = "Update UserInfo Set IsValidate =0 Where UserID=" + UserID + " and Email!=" + cmn.SQLQ(Email);
                        sql += ";Update UserInfo Set ";
                        if (!UserName.Equals("")) {sql += "UserName=" + cmn.SQLQC(UserName);}
                        sql += "MobilePhone=" + cmn.SQLQC(MobilePhone);
                        sql += "Email="+cmn.SQLQC(Email);
                        if (!PassWD.Equals("")) { sql += "PassWD=" + cmn.SQLQC(cmn.MyEncode001(PassWD)); }
                        if (!IconSrc.Equals("")) { sql += "IconSrc=" + cmn.SQLQC(IconSrc); }
                        if (!LastLoginTime.Equals("")) { sql += "LastLoginTime=" + cmn.SQLQC(LastLoginTime); }
                        if (!LastLoginIP.Equals("")) { sql += "LastLoginIP=" + cmn.SQLQC(LastLoginIP); }
                        if (LoginCount > 0) { sql += "LoginCount=LoginCount+1,"; }
                        if (!RoleCollect.Equals("")) { sql += "RoleCollect=" + cmn.SQLQC(RoleCollect); }
                        if (!TypeNO.Equals("")) { sql += "TypeNO=" + cmn.SQLQC(TypeNO); }
                        sql += "UpdateUserID=" + cmn.SQLQC(nUserID);
                        sql += "UpdateDateTime=" + cmn.SQLQ(DateTime.Now);
                        sql += " Where UserID =" + cmn.SQLQ(UserID);

                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                    case TxTypeConsts.TxTypeDelete:
                        if (UserID <= -1) { throw new MyException("UserID不能为空"); }

                        sql = " Delete From UserInfo Where UserID =" + cmn.SQLQ(UserID);
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                    case TxTypeConsts.TxTypeChange:
                        if (UserID.Equals("")) { throw new MyException("UserID不能为空"); }
                        if (StatusNO.Equals("")) { throw new MyException("状态编号不能为空"); }

                        sql = " Update UserInfo Set ";
                        sql += "StatusNO=" + cmn.SQLQC(StatusNO);
                        sql += "UpdateUserID=" + cmn.SQLQC(nUserID);
                        sql += "UpdateDateTime=" + cmn.SQLQ(DateTime.Now);
                        sql += " Where UserID =" + cmn.SQLQ(UserID);
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                }
                return nResult;
            }
            #endregion

            #region  //TxSenseWord001 -- DEAL SenseWord DATA MAINTAIN
            public int TxSenseWord001(TArgsTX oArgsTx)
            {
                OleDbCommand cmd = oArgsTx.Command;
                int nUserID = oArgsTx.Context.UserID();
                tx = oArgsTx.ServiceID;
                //***********************************************************************
                XmlDocument x = new XmlDocument();
                x.LoadXml(oArgsTx.Param);
                int WordID = cmn.ParserXML(x, "//SenseWord/WordID", -999);
                string WordName = cmn.ParserXML(x, "//SenseWord/WordName");
                string WordTypeNO = cmn.ParserXML(x, "//SenseWord/WordTypeNO");
                string WordDesc = cmn.ParserXML(x, "//SenseWord/WordDesc");
                string StatusNO = cmn.ParserXML(x, "//SenseWord/StatusNO");
                //***********************************************************************

                DataSet oDS = new DataSet();
                switch (oArgsTx.TxType)
                {
                    case TxTypeConsts.TxTypeAddNew:
                        if (WordName.Equals("")) { throw new MyException("词语不能为空"); }
                        if (WordTypeNO.Equals("")) { throw new MyException("词语类型不能为空"); }

                        sql = " Insert Into SenseWord (WordName,WordTypeNO,WordDesc,StatusNO,";
                        sql += "AddUserID,AddDateTime,UpdateUserID,UpdateDateTime)";
                        sql += "Values(" + cmn.SQLQC(WordName) + cmn.SQLQC(WordTypeNO) + cmn.SQLQC(WordDesc) + cmn.SQLQC("A");
                        sql += cmn.SQLQC(nUserID) + cmn.SQLQC(DateTime.Now) + cmn.SQLQC(nUserID) + cmn.SQLQ(DateTime.Now) + ")";
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                    case TxTypeConsts.TxTypeUpdate:
                        if (WordID <= -1) { throw new MyException("WordID不能为空"); }
                        if (WordName.Equals("")) { throw new MyException("词语不能为空"); }
                        if (WordTypeNO.Equals("")) { throw new MyException("词语类型不能为空"); }

                        sql = " Update SenseWord Set ";
                        sql += "WordName=" + cmn.SQLQC(WordName);
                        sql += "WordTypeNO=" + cmn.SQLQC(WordTypeNO);
                        sql += "WordDesc=" + cmn.SQLQC(WordDesc);
                        sql += "UpdateUserID=" + cmn.SQLQC(nUserID);
                        sql += "UpdateDateTime=" + cmn.SQLQ(DateTime.Now);
                        sql += " Where WordID =" + cmn.SQLQ(WordID);
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                    case TxTypeConsts.TxTypeDelete:
                        if (WordID <= -1) { throw new MyException("WordID不能为空"); }

                        sql = " Delete From SenseWord Where WordID =" + cmn.SQLQ(WordID);
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                    case TxTypeConsts.TxTypeChange:
                        if (WordID.Equals("")) { throw new MyException("WordID不能为空"); }
                        if (StatusNO.Equals("")) { throw new MyException("状态编号不能为空"); }

                        sql = " Update SenseWord Set ";
                        sql += "StatusNO=" + cmn.SQLQC(StatusNO);
                        sql += "UpdateUserID=" + cmn.SQLQC(nUserID);
                        sql += "UpdateDateTime=" + cmn.SQLQ(DateTime.Now);
                        sql += " Where WordID =" + cmn.SQLQ(WordID);
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                }
                return nResult;
            }
            #endregion

            #region  //TxCategory001 -- DEAL Category DATA MAINTAIN
            public int TxCategory001(TArgsTX oArgsTx)
            {
                OleDbCommand cmd = oArgsTx.Command;
                int nUserID = oArgsTx.Context.UserID();
                tx = oArgsTx.ServiceID;
                //***********************************************************************
                XmlDocument x = new XmlDocument();
                x.LoadXml(oArgsTx.Param);
                int CategoryID = cmn.ParserXML(x, "//Category/CategoryID", -999);
                string CategoryName = cmn.ParserXML(x, "//Category/CategoryName");
                string CategoryDesc = cmn.ParserXML(x, "//Category/CategoryDesc");
                string StatusNO = cmn.ParserXML(x, "//Category/StatusNO");
                int ParentID = cmn.ParserXML(x, "//Category/ParentID", -999);
                //***********************************************************************

                DataSet oDS = new DataSet();
                switch (oArgsTx.TxType)
                {
                    case TxTypeConsts.TxTypeAddNew:
                        if (CategoryName.Equals("")) { throw new MyException("类别名称不能为空"); }
                        if (ParentID <= -1) { throw new MyException("父级ID不能为空"); }

                        sql = " Insert Into Category (CategoryName,CategoryDesc,ParentID,StatusNO,";
                        sql += "AddUserID,AddDateTime,UpdateUserID,UpdateDateTime)";
                        sql += "Values(" + cmn.SQLQC(CategoryName) + cmn.SQLQC(CategoryDesc) + cmn.SQLQC(ParentID) + cmn.SQLQC("A");
                        sql += cmn.SQLQC(nUserID) + cmn.SQLQC(DateTime.Now) + cmn.SQLQC(nUserID) + cmn.SQLQ(DateTime.Now) + ")";
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                    case TxTypeConsts.TxTypeUpdate:
                        if (CategoryID <= -1) { throw new MyException("CategoryID不能为空"); }
                        if (CategoryName.Equals("")) { throw new MyException("类别名称不能为空"); }
                        if (ParentID <= -1) { throw new MyException("父级ID不能为空"); }

                        sql = " Update Category Set ";
                        sql += "CategoryName=" + cmn.SQLQC(CategoryName);
                        sql += "CategoryDesc=" + cmn.SQLQC(CategoryDesc);
                        sql += "ParentID=" + cmn.SQLQC(ParentID);
                        sql += "UpdateUserID=" + cmn.SQLQC(nUserID);
                        sql += "UpdateDateTime=" + cmn.SQLQ(DateTime.Now);
                        sql += " Where CategoryID =" + cmn.SQLQ(CategoryID);
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                    case TxTypeConsts.TxTypeDelete:
                        if (CategoryID <= -1) { throw new MyException("CategoryID不能为空"); }

                        sql = " Delete From Category Where CategoryID =" + cmn.SQLQ(CategoryID);
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                    case TxTypeConsts.TxTypeChange:
                        if (CategoryID.Equals("")) { throw new MyException("CategoryID不能为空"); }
                        if (StatusNO.Equals("")) { throw new MyException("状态编号不能为空"); }

                        sql = " Update Category Set ";
                        sql += "StatusNO=" + cmn.SQLQC(StatusNO);
                        sql += "UpdateUserID=" + cmn.SQLQC(nUserID);
                        sql += "UpdateDateTime=" + cmn.SQLQ(DateTime.Now);
                        sql += " Where CategoryID =" + cmn.SQLQ(CategoryID);
                        nResult = cmn.CmnExecute(sql, cmd);
                        break;
                    //***********************************************************************
                }
                return nResult;
            }
            #endregion

            #endregion

        }
    }

  
}
