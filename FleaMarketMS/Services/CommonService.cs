using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace MES.Services
{
    public class CommonService
    {

        #region //GET DOMAIN_NAME
        public static string GetDomainName(string XML_DAT, string TARGET_DOMAIN)
        {
            string DOMAIN_NAME = "";
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(XML_DAT);
            XmlNodeList ns = xmldoc.SelectNodes("//USER_DOMAIN");
            foreach (XmlNode node in ns)
            {
                string DOMAIN_NO = node.SelectSingleNode("DOMAIN_NO").InnerText;
                if (TARGET_DOMAIN.Equals(DOMAIN_NO))
                {
                    DOMAIN_NAME = node.SelectSingleNode("DOMAIN_NAME_CH").InnerText;
                    break;
                }
            }
            return DOMAIN_NAME;
        }
        #endregion

        #region //GET PACKAGE_NAME
        public static string GetPackageName(string XML_DAT, string TARGET_DOMAIN, string TARGET_PACKAGE)
        {
            string PACK_NAME = "";
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(XML_DAT);
            XmlNodeList ns = xmldoc.SelectNodes("//USER_PACKAGE");
            foreach (XmlNode node in ns)
            {
                string PACK_DOAMIN = node.SelectSingleNode("DOMAIN_NO").InnerText;
                string PACK_NO = node.SelectSingleNode("PACK_NO").InnerText;
                if (TARGET_DOMAIN.Equals(PACK_DOAMIN) && TARGET_PACKAGE.Equals(PACK_NO))
                {
                    PACK_NAME = node.SelectSingleNode("PACK_NAME_CH").InnerText;
                    break;
                }
            }
            return PACK_NAME;
        }
        #endregion

        #region //GET PROGRAM_NAME
        public static string GetProgramName(string XML_DAT, string TARGET_PRO)
        {
            string PRO_NAME = "";
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(XML_DAT);
            XmlNodeList ns = xmldoc.SelectNodes("//USER_PROGRAM");
            foreach (XmlNode node in ns)
            {
                string PRO_PACK = node.SelectSingleNode("PACK_NO").InnerText;
                string PRO_NO = node.SelectSingleNode("PRO_NO").InnerText;
                if (TARGET_PRO.Equals(PRO_NO))
                {
                    PRO_NAME = node.SelectSingleNode("PRO_NAME_CH").InnerText;
                    break;
                }
            }
            return PRO_NAME;
        }
        #endregion
    }
}