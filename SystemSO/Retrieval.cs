using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MESIII;
using System.Data;
using System.Data.OleDb;
using System.Xml;

namespace SystemSO
{
    public class Retrieval : ISDServer.IRetrieval
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
                    case "QryProgramInfo002": ds = Qry.QryProgramInfo002(oArgsRv); break;

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
            public DataSet QryProgramInfo002(TArgsRV oArgsRv)
            {
                OleDbCommand cmd = oArgsRv.Command;
                qry = oArgsRv.ServiceID;
                //****************************************************************
                XmlDocument x = new XmlDocument();
                x.LoadXml(oArgsRv.Param);
                int UserID = cmn.ParserXML(x, "//ProgramInfo/UserID", -999);
                string RoleIDList = cmn.ParserXML(x, "//ProgramInfo/RoleIDList", false);
                //****************************************************************
                sql = " Select ProgramID,Path,ProgramName,ProgramDesc,Sequence,StatusNO";
                sql += " From ProgramInfo a Where a.StatusNO='A' and ProgramID in(select value from dbo.F_Split((";
                sql += " Select ProgramList from RoleInfo ";
                if (!RoleIDList.Equals(""))
                    sql += " Where RoleID in("+RoleIDList+")";
                else
                {
                    sql += " Where RoleID in (Select value from dbo.F_Split((select top 1 RoleCollect from UserInfo where UserID="+UserID+"),','))),',' ))";
                }
                sql += " Order By Sequence";

                oResult = cmn.CmnRvEnumerate(sql, cmd, oArgsRv.ServiceID, -1, -1);
                return oResult;
            }
            #endregion
        }
    }
}
