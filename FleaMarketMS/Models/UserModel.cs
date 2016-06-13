using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Xml;
using System.Reflection;

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

    public class PostMsg
    {
        /// <summary>
        /// UserID
        /// </summary>		
        private int _userid;
        public int UserID
        {
            get { return _userid; }
            set { _userid = value; }
        }
        /// <summary>
        /// 用户登陆账号
        /// </summary>		
        private string _userno;
        public string UserNO
        {
            get { return _userno; }
            set { _userno = value; }
        }
        /// <summary>
        /// 用户名
        /// </summary>		
        private string _username;
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }
        /// <summary>
        /// 图像地址
        /// </summary>		
        private string _iconsrc;
        public string IconSrc
        {
            get { return _iconsrc; }
            set { _iconsrc = value; }
        }

        /// <summary>
        /// MsgID
        /// </summary>		
        private int _msgid;
        public int MsgID
        {
            get { return _msgid; }
            set { _msgid = value; }
        }
        /// <summary>
        /// 消息标题
        /// </summary>		
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// 消息体
        /// </summary>		
        private string _msgcontent;
        public string MsgContent
        {
            get { return _msgcontent; }
            set { _msgcontent = value; }
        }
        /// <summary>
        /// 图片链接，多个用;分割
        /// </summary>		
        private string _picturesrc;
        public string PictureSrc
        {
            get { return _picturesrc; }
            set { _picturesrc = value; }
        }

        /// <summary>
        /// 一级评论数目
        /// </summary>	
        private int _replaycount;
         public int ReplyCount
        {
            get { return _replaycount; }
            set { _replaycount = value; }
        }

        /// <summary>
        /// 关注计数
        /// </summary>
         private int _attentioncount;
        public int AttentionCount
         {
             get { return _attentioncount; }
             set { _attentioncount = value; }
         }
        /// <summary>
        /// 状态编号
        /// </summary>		
        private string _statusno;
        public string StatusNO
        {
            get { return _statusno; }
            set { _statusno = value; }
        }
        /// <summary>
        /// AddDateTime
        /// </summary>		
        private DateTime _adddatetime;
        public DateTime AddDateTime
        {
            get { return _adddatetime; }
            set { _adddatetime = value; }
        }

        /// <summary>
        /// CategoryID
        /// </summary>		
        private int _categoryid;
        public int CategoryID
        {
            get { return _categoryid; }
            set { _categoryid = value; }
        }
        /// <summary>
        /// 类别名称
        /// </summary>		
        private string _categoryname;
        public string CategoryName
        {
            get { return _categoryname; }
            set { _categoryname = value; }
        }
        /// <summary>
        /// 父级ID
        /// </summary>		
        private int _parentid;
        public int ParentID
        {
            get { return _parentid; }
            set { _parentid = value; }
        }     
    }

    public static class ListExten
    {
        //封装一个方法首先要明确目的，要实现什么效果
        //然后设计实现步骤，先做师门，再做什么，最后做什么
        //在这个过程中，要清楚需要传入什么参数，返回什么结果，传入的参数是否可能为null，返回的结果是否可能为null
        //最后想清楚调用这个方法的大环境是什么，是独立的静态函数还是类的成员函数
        public static IEnumerable<T> ForeachOne<T>(this IEnumerable<T> eles, Action<T> act)
        {
            if (act != null)
                foreach (T e in eles)
                {
                    act(e);
                }
            return eles;
        }
        //根据model将Table转成list
        public static IEnumerable<T> TabeToList<T>(this DataTable dt) where T : class,new()
        {
            if (dt != null)
            {
                List<T> lt = new List<T>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    //T t = default(T);//获得当前对象的默认值
                    T s = new T();
                    for (int j = 0; j < dr.ItemArray.Length; j++)
                    {
                        PropertyInfo[] pis = s.GetType().GetProperties();
                        pis.Where(e => e.Name.ToLower().Equals(dt.Columns[j].ColumnName.ToLower()))
                           .ForeachOne(e =>
                           {
                               object ores = null;
                               if (dr[j].Equals(DBNull.Value))
                               {
                                   switch (e.PropertyType.Name)//如果数据库是DBNull类型数值就0，引用就null,下面可能没有考虑完全，可以自己在添加上
                                   {
                                       case "int":
                                       case "Int16":
                                       case "Int32":
                                       case "Int64":
                                       case "float":
                                       case "double": ores = 0; break;
                                       default: ores = default(object); break;
                                   }
                               }
                               else
                               {
                                   ores = dr[j];
                               }
                               e.SetValue(s, ores,null);
                           });
                    }
                    lt.Add(s);
                }
                return lt.AsEnumerable<T>();
            }
            return null;
        }
    }
}