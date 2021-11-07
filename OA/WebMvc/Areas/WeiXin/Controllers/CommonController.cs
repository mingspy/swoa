using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;

namespace WebMvc.Areas.WeiXin.Controllers
{
    public class CommonController : Controller
    {
        //
        // GET: /WeiXin/Common/

        //string token = "iTak7rMl7ItStvRFbDqA6wDdTeDOOoqX";
        //string encodingAESKey = "J6B1ZF1bAx77hVYHhd6aNs6Yyha2BsNxtZq1dprOX2v";
        string corpId = YJ.Platform.WeiXin.Config.CorpID;
        string suitId = YJ.Platform.WeiXin.Config.SuitID;
        string token = YJ.Platform.WeiXin.Config.MsgToken;
        string encodingAESKey = YJ.Platform.WeiXin.Config.MsgAESKey;


        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 接收微信端发送的消息
        /// </summary>
        public void ReceiveMessage()
        {
            if (Request.HttpMethod.ToUpper() == "GET")
            {
                Auth();
            }
            else if (Request.HttpMethod.ToUpper() == "POST")
            {
                string signature = Request.QueryString["msg_signature"];//企业号的 msg_signature
                string timestamp = Request.QueryString["timestamp"];
                string nonce = Request.QueryString["nonce"];
                YJ.Platform.WeiXin.WXBizMsgCrypt wxcpt = new YJ.Platform.WeiXin.WXBizMsgCrypt(token, encodingAESKey, corpId);
                Stream s = System.Web.HttpContext.Current.Request.InputStream;
                byte[] b = new byte[s.Length];
                s.Read(b, 0, (int)s.Length);
                string msgBody = Encoding.UTF8.GetString(b);
                string sMsg = "";
                int flag = wxcpt.DecryptMsg(signature, timestamp, nonce, msgBody, ref sMsg);
                if (flag == 0)
                {
                    try
                    {
                        new YJ.Platform.WeiXin.Message().Receive(sMsg);
                    }
                    catch (Exception e)
                    {

                    }
                  
                    
                    Response.Write("success");
                    Response.End();
                }
                else
                {
                    YJ.Platform.Log.Add("消息解密失败", msgBody, YJ.Platform.Log.Types.微信企业号);
                }
            }
        }

        /// <summary>
        /// 验证相应服务器的数据
        /// </summary>
        private void Auth()
        {
            string echoString = Request.QueryString["echoStr"];
            string signature = Request.QueryString["msg_signature"];//企业号的 msg_signature
            string timestamp = Request.QueryString["timestamp"];
            string nonce = Request.QueryString["nonce"];
            string decryptEchoString = "";
            if (CheckSignature(token, signature, timestamp, nonce, corpId, encodingAESKey, echoString, ref decryptEchoString))
            {
                if (!string.IsNullOrEmpty(decryptEchoString))
                {
                    Response.Write(decryptEchoString);
                    Response.End();
                    return;
                }
            }
        }

        /// <summary>
        /// 验证企业号签名
        /// </summary>
        /// <param name="token">企业号配置的Token</param>
        /// <param name="signature">签名内容</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">nonce参数</param>
        /// <param name="corpId">企业号ID标识</param>
        /// <param name="encodingAESKey">加密键</param>
        /// <param name="echostr">内容字符串</param>
        /// <param name="retEchostr">返回的字符串</param>
        /// <returns></returns>
        public bool CheckSignature(string token, string signature, string timestamp, string nonce, string corpId, string encodingAESKey, string echostr, ref string retEchostr)
        {
            YJ.Platform.WeiXin.WXBizMsgCrypt wxcpt = new YJ.Platform.WeiXin.WXBizMsgCrypt(token, encodingAESKey, corpId);
            int result = wxcpt.VerifyURL(signature, timestamp, nonce, echostr, ref retEchostr);
            if (result != 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 得到微信端用户账号
        /// </summary>
        public void GetUserAccount()
        {
            string code = Request.QueryString["code"];
            if (code.IsNullOrEmpty())
            {
                Response.Write("身份验证失败");
                Response.End();
                return;
            }
            string account = new YJ.Platform.WeiXin.Organize().GetUserAccountByCode(code);
            if (account.IsNullOrEmpty())
            {
                Response.Write("身份验证失败");
                Response.End();
                return;
            }
            var user = new YJ.Platform.Users().GetByAccount(account);
            if (user == null)
            {
                Response.Write("未找到帐号对应的人员");
                Response.End();
                return;
            }
            System.Web.HttpContext.Current.Response.Cookies.Add(new HttpCookie("weixin_userid", user.ID.ToString()) { Expires = YJ.Utility.DateTimeNew.Now.AddDays(30) });
            System.Web.HttpContext.Current.Session.Add(YJ.Utility.Keys.SessionKeys.UserID.ToString(), user.ID.ToString());

            var lastURLCookie = Request.Cookies.Get("LastURL");
            var lastURL = lastURLCookie == null ? "" : lastURLCookie.Value;
            if (!lastURL.IsNullOrEmpty())
            {
                Response.Redirect(lastURL);
            }
        }
    }
}
