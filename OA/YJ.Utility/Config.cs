using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;

namespace YJ.Utility
{
    public class Config
    {
        public static string PlatformConnectionStringMSSQL
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["PlatformConnection"].ConnectionString;
            }
        }

        public static string PlatformConnectionStringORACLE
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["PlatformConnectionOracle"].ConnectionString;
            }
        }

        public static string PlatformConnectionStringMySql
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["PlatformConnectionMySql"].ConnectionString;
            }
        }

        public static string DataBaseType
        {
            get
            {
                string text = ConfigurationManager.AppSettings["DatabaseType"];
                return text.IsNullOrEmpty() ? "MSSQL" : text.ToUpper();
            }
        }

        public static string DataBaseType1
        {
            get
            {
                string text = ConfigurationManager.AppSettings["DatabaseType"];
                return text.IsNullOrEmpty() ? "SqlServer" : (text.ToUpper().Equals("MSSQL") ? "SqlServer" : text.ToUpper());
            }
        }

        public static string SystemInitPassword
        {
            get
            {
                string text = ConfigurationManager.AppSettings["InitPassword"];
                return text.IsNullOrEmpty() ? "111" : text.Trim();
            }
        }

        public static string FilePath
        {
            get
            {
                string text = ConfigurationManager.AppSettings["FilePath"];
                return text.IsNullOrEmpty() ? "d:\\YunJianFiles\\" : (text.EndsWith("\\") ? text : (text + "\\"));
            }
        }

        public static string Theme
        {
            get
            {
                HttpCookie httpCookie = HttpContext.Current.Request.Cookies["theme_platform"];
                return (httpCookie != null && !httpCookie.Value.IsNullOrEmpty()) ? httpCookie.Value : "Blue";
            }
        }

        public static string UploadFileType
        {
            get
            {
                return ConfigurationManager.AppSettings["UploadFileType"];
            }
        }

        public static bool IsDemo
        {
            get
            {
                return "1".Equals(ConfigurationManager.AppSettings["IsDemo"]);
            }
        }

        public static string DateFormat
        {
            get
            {
                return "yyyy-MM-dd";
            }
        }

        public static string DateTimeFormat
        {
            get
            {
                return "yyyy-MM-dd HH:mm";
            }
        }

        public static string DateTimeFormatS
        {
            get
            {
                return "yyyy-MM-dd HH:mm:ss";
            }
        }

        public static List<string> SystemDataTables
        {
            get
            {
                List<string> list = new List<string>();
                string text = ConfigurationManager.AppSettings["SystemTables"];
                bool flag = text.IsNullOrEmpty();
                List<string> result;
                if (flag)
                {
                    result = list;
                }
                else
                {
                    string[] array = text.Split(new char[]
                    {
                        ','
                    });
                    for (int i = 0; i < array.Length; i++)
                    {
                        string text2 = array[i];
                        bool flag2 = !text2.IsNullOrEmpty();
                        if (flag2)
                        {
                            list.Add(text2);
                        }
                    }
                    result = list;
                }
                return result;
            }
        }

        public static string WebUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["WebUrl"];
            }
        }

        public static string wxqy_CorpID
        {
            get
            {
                return ConfigurationManager.AppSettings["appid"];
            }
        }

        public static string wxqy_Secret
        {
            get
            {
                return ConfigurationManager.AppSettings["secret"];
            }
        }

        public static string BaseUrl
        {
            get
            {
                string appDomainAppVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
                return (appDomainAppVirtualPath == "/") ? "" : appDomainAppVirtualPath;
            }
        }

        public static string GetTokenByUserId(Guid userID)
        {
            Guid guid = Guid.NewGuid();
            return (userID.ToString() + "|" + guid.ToString()).DesEncrypt();
        }

        public static Guid? GetUserIdByToken(string token)
        {
            bool flag = token.IsNullOrEmpty();
            Guid? result;
            if (flag)
            {
                result = null;
            }
            else
            {
                string text = token.DesDecrypt();
                bool flag2 = text.IsNullOrEmpty();
                if (flag2)
                {
                    result = null;
                }
                else
                {
                    string[] array = text.Split(new char[]
                    {
                        '|'
                    });
                    bool flag3 = array == null || array.Length != 2;
                    if (flag3)
                    {
                        result = null;
                    }
                    else
                    {
                        string str = array[0];
                        result = (str.IsGuid() ? new Guid?(str.ToGuid()) : null);
                    }
                }
            }
            return result;
        }
    }
}
