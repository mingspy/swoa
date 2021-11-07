using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace YJ.Platform.WeiXin
{
    public class Config
    {
        /// <summary>
        /// 是否使用微信
        /// </summary>
        public static bool IsUse
        {
            get
            {
                return "1" == ConfigurationManager.AppSettings["wxqy_IsUse"];
            }
        }
        /// <summary>
        /// 微信企业ID
        /// </summary>
        public static string CorpID
        {
            get
            {
                return ConfigurationManager.AppSettings["wxqy_CorpID"];
            }
        }

        public static string SuitID
        {
            get
            {
                return ConfigurationManager.AppSettings["wxqy_SuitID"];
            }
        }

        public static string SuitSecret
        {
            get
            {
                return ConfigurationManager.AppSettings["wxqy_SuitSecret"];
            }
        }

        public static string MsgToken
        {
            get
            {
                return ConfigurationManager.AppSettings["wxqy_msg_token"];
            }
        }

        public static string MsgAESKey
        {
            get
            {
                return ConfigurationManager.AppSettings["wxqy_msg_encodingAESKey"];
            }
        }

        /// <summary>
        /// 微信secret
        /// </summary>
        public static string OrganizeSecret
        {
            get
            {
                return ConfigurationManager.AppSettings["wxqy_Secret"];
            }
        }

        /// <summary>
        /// 网站外网地址
        /// </summary>
        public static string WebUrl
        {
            get
            {
                string url = ConfigurationManager.AppSettings["WebUrl"];
                return url.EndsWith("/") ? url : url + "/";
            }
        }

        /// <summary>
        /// 获取每次操作微信API的Token访问令牌
        /// </summary>
        /// <param name="secret">管理组的凭证密钥</param>
        /// <returns></returns>
        public static string GetAccessToken(string secret)
        {

            if (secret.IsNullOrEmpty())
            {
                secret = Config.OrganizeSecret;
            }
            if (secret.IsNullOrEmpty())
            {
                return "";
            }
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", CorpID, secret);
            //string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", SuitID, SuitSecret);
            string token = Utility.HttpHelper.SendGet(url);//token:{"access_token":"7p-2wZwAYnGwE80-VXubyqXNS9YZxIbmcHscjdfB78FA1zNPRKOGftJQdHAR8447","expires_in":7200}
            LitJson.JsonData jd = LitJson.JsonMapper.ToObject(token);
            if (jd.ContainsKey("access_token"))
            {
                return jd["access_token"].ToString();
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 根据CODE得到用户帐号地址
        /// </summary>
        public static string GetAccountUrl
        {
            get
            {
                string url = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
                if (url.Contains(".aspx", StringComparison.CurrentCultureIgnoreCase))
                {
                    return (Config.WebUrl + "Applications/WeiXin/GetUserAccount.ashx").UrlEncode();
                }
                else
                {
                    return (Config.WebUrl + "WeiXin/Common/GetUserAccount").UrlEncode();
                }
            }
        }

        /// <summary>
        /// 根据代码得到secret
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetSecret(string code)
        {
            var dict = new Dictionary().GetByCode(code, true);
            return dict == null ? "" : dict.Note.Trim1();
        }

        /// <summary>
        /// 根据应用ID得到secret
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetSecret(int agentId)
        {
            var dict = new Dictionary().GetChilds("weixinagents", true).Find(p => p.Value.ToInt(-1) == agentId);
            return dict == null ? "" : dict.Note.Trim1();
        }
    }
}
