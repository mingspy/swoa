using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace WebMvc.Controllers
{
    public class LoginController : MyController
    {

        [MyAttribute(CheckApp = false, CheckLogin = false, CheckUrl = false)]
        public ActionResult Index()
        {
            return View();
        }

        [MyAttribute(CheckApp = false, CheckLogin = false, CheckUrl = false)]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [MyAttribute(CheckApp = false, CheckLogin = false, CheckUrl = false)]
        public string ValidateLogin()
        {

            string account = Request.Form["Account"];
            string password = Request.Form["Password"];
            if (account.IsNullOrEmpty() || password.IsNullOrEmpty())
            {
                return "{\"id\":\"\",\"status\":0,\"msg\":\"帐号或密码不能为空\"}";
            }


            YJ.Platform.Users busers = new YJ.Platform.Users();
            var user = busers.GetByAccount(account.Trim());
            if (user == null || string.Compare(user.Password, busers.GetUserEncryptionPassword(user.ID.ToString(), password.Trim()), false) != 0)
            {
                return "{\"id\":\"\",\"status\":0,\"msg\":\"帐号或密码错误\"}";
            }
            if (user.Status == 1)
            {
                return "{\"id\":\"\",\"status\":0,\"msg\":\"帐号已被冻结\"}";
            }

            Session[YJ.Utility.Keys.SessionKeys.UserID.ToString()] = user.ID;
            Session[YJ.Utility.Keys.SessionKeys.BaseUrl.ToString()] = Url.Content("~/");
            Session[YJ.Utility.Keys.SessionKeys.UserName.ToString()] = user.Name;
            Response.Cookies.Add(new HttpCookie(YJ.Utility.Keys.SessionKeys.UserID.ToString(), user.ID.ToString()) { Expires = CurrentDateTime.AddDays(7) });
            YJ.Platform.Log.Add("用户登录成功-test" + "(帐号:" + account + ")", "", YJ.Platform.Log.Types.用户登录);
            return "{\"id\":\"" + user.ID.ToString() + "\",\"token\":\"" + YJ.Utility.Config.GetTokenByUserId(user.ID) + "\",\"status\":1,\"msg\":\"用户登录成功\"}";
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAttribute(CheckApp = false, CheckLogin = false, CheckUrl = false)]
        public string CheckLogin()
        {
            string currentIP = YJ.Utility.Tools.GetIPAddress();//当前会话IP
            YJ.Platform.LoginBlacklist lbl = new YJ.Platform.LoginBlacklist();
            string account = Request.Form["Account"];

            if (currentIP == ""||currentIP=="0.0.0.0")//是否能检测到请求的IP
            {
               return "{\"status\":0,\"msg\":\"登录地址不合法!\"}";
            }

            if (false && !(currentIP.Substring(0, 7) == "10.143."))//不属于10.143网段的访问
            {
                if (account == "wfb")//特权账户，可以在外网访问
                {
                    ;
                }
                else {
                    return "{\"status\":0,\"msg\":\"当前地址不允许登录!\"}";
                }
            }

            YJ.Data.Model.LoginBlacklist isblocked = lbl.GetByIPaddress(currentIP);//当前会话IP是否被禁
            if (isblocked != null)//检查当前IP是否已在黑名单
            {
                if (!isblocked.IPaddress.IsNullOrEmpty())
                {
                    YJ.Platform.Log.Add("黑名单" + "(IP:" + currentIP + "正在尝试登录，已被禁止)", "", YJ.Platform.Log.Types.用户登录);
                    return "{\"status\":0,\"msg\":\"已被禁止登录!\"}";
                }
            }




            string isVcodeSessionKey = YJ.Utility.Keys.SessionKeys.IsValidateCode.ToString();
            string vcodeSessionKey = YJ.Utility.Keys.SessionKeys.ValidateCode.ToString();
            string password = Request.Form["Password"];
            string vcode = Request.Form["VCode"];
            string force = Request.Form["Force"];//是否强行登录

            
           
            string historylogin = "";//历史登录IP
            int listindex=0;//获取集合元素下标
            int historylogintimes = 0;//历史登录次数
            bool isinloginlist=false;

            if (loginlist.Count == 0)
            {
                loginlist.Add("0.0.0.0|0");
            }
            else {//查找当前IP是否在全局已经有登录记录，如果有则记录登录IP，已登录次数，集合中的位置，如果没有，则增加一个记录。
                for (int i =0; i<loginlist.Count;i++) {

                    int index = loginlist[i].LastIndexOf("|");
                    if (loginlist[i].Substring(0, index) == currentIP)
                    {
                        //查找是否已在记录中有，已有记录则取出记录的值和记录所在位置
                        historylogin = currentIP;
                        historylogintimes = Convert.ToInt16(loginlist[i].Substring(index+1));
                        listindex = i;//历史记录的索引
                        isinloginlist = true;
                        break;
                    }
                    else {
                        ;
                    }
                }
                if (!isinloginlist) { 
                //未找到记录
                loginlist.Add(currentIP + "|0");//增加记录
                historylogintimes = 0;//第一次登录
                listindex = loginlist.Count-1;//新增加的记录的索引\
                }
            }

            
            string logMsg = "(帐号:" + account + " 密码:" + password + " 验证码:" + vcode + ")";
            if (account.IsNullOrEmpty() || password.IsNullOrEmpty())
            {
                YJ.Platform.Log.Add("用户登录失败(帐号或密码为空)" + logMsg, "", YJ.Platform.Log.Types.用户登录);
                return "{\"status\":0,\"msg\":\"帐号或密码不能为空!\"}";
            }
            if (Session[isVcodeSessionKey] != null && "1" == Session[isVcodeSessionKey].ToString() &&
                (Session[vcodeSessionKey] == null
                || string.Compare(Session[vcodeSessionKey].ToString(), vcode.Trim1(), true) != 0))
            {
                YJ.Platform.Log.Add("用户登录失败(验证码错误)" + logMsg, "", YJ.Platform.Log.Types.用户登录);
                return "{\"status\":0,\"msg\":\"验证码错误!\"}";
            }

            YJ.Platform.Users busers = new YJ.Platform.Users();
            var user = busers.GetByAccount(account.Trim());
            //if (user == null || string.Compare(user.Password, busers.GetUserEncryptionPassword(user.ID.ToString(), password.Trim()), false) != 0)
            if(user == null)
            {
                Session[isVcodeSessionKey] = "1";
                
                YJ.Platform.Log.Add("用户登录失败(帐号或密码错误)|IP地址："+currentIP+"|" + logMsg, "", YJ.Platform.Log.Types.用户登录);
                historylogintimes += 1;
                

                if (historylogintimes >= 5)
                {
                    YJ.Data.Model.LoginBlacklist blacklist = new YJ.Data.Model.LoginBlacklist();
                    blacklist.IPaddress = YJ.Utility.Tools.GetIPAddress();
                    blacklist.Account = account.Trim();
                    blacklist.BlockTime = System.DateTime.Now;
                    YJ.Platform.LoginBlacklist.Add(blacklist);
                    YJ.Platform.Log.Add("用户登录失败" + "(IP:" + blacklist.IPaddress + "已加入黑名单)", "", YJ.Platform.Log.Types.用户登录);
                    loginlist[listindex] = historylogin + "|" + "0";//将登陆次数清0。
                    return "{\"status\":0,\"msg\":\"已被禁止登录！\"}";
                }
                else { 
                loginlist[listindex] = currentIP + "|" + historylogintimes;
                return "{\"status\":0,\"msg\":\"帐号或密码错误,连续输错5次将被禁止登录，当前第" + historylogintimes + "次!\"}";
                }
            }
            if (user.Status == 1)
            {
                Session[isVcodeSessionKey] = "1";
                YJ.Platform.Log.Add("用户登录失败(帐号已被冻结)" + logMsg, "", YJ.Platform.Log.Types.用户登录);
                return "{\"status\":0,\"msg\":\"帐号已被冻结!\"}";
            }
            YJ.Platform.OnlineUsers bou = new YJ.Platform.OnlineUsers();
            var onUser = bou.Get(user.ID);
            if (onUser != null && "1" != force)
            {
                string ip = onUser.IP;
                Session.Remove(isVcodeSessionKey);
                return "{\"status\":2,\"msg\":\"当前帐号已经在" + ip + "登录,您要强行登录吗?\"}";
            }

            Guid uniqueID = Guid.NewGuid();
            Session[YJ.Utility.Keys.SessionKeys.UserID.ToString()] = user.ID;
            Session[YJ.Utility.Keys.SessionKeys.UserUniqueID.ToString()] = uniqueID;
            Session[YJ.Utility.Keys.SessionKeys.BaseUrl.ToString()] = Url.Content("~/");
            Session[YJ.Utility.Keys.SessionKeys.UserName.ToString()] = user.Name;
            Response.Cookies.Add(new HttpCookie(YJ.Utility.Keys.SessionKeys.UserID.ToString(), user.ID.ToString()) { Expires = CurrentDateTime.AddDays(7) });
            bou.Add(user, uniqueID);
            Session.Remove(isVcodeSessionKey);
            YJ.Platform.Log.Add("用户登录成功" + "(帐号:" + account + ")", "", YJ.Platform.Log.Types.用户登录);
            loginlist[listindex] = historylogin + "|" + "0";
            return "{\"status\":1,\"msg\":\"成功!\"}";
            
        }

        [MyAttribute(CheckApp = false, CheckLogin = false)]
        public void VCode()
        {
            string code;
            System.IO.MemoryStream ms = YJ.Utility.Tools.GetValidateImg(out code, Url.Content("~/Images/vcodebg.png"));
            System.Web.HttpContext.Current.Session[YJ.Utility.Keys.SessionKeys.ValidateCode.ToString()] = code;
            Response.ClearContent();
            Response.ContentType = "image/gif";
            Response.BinaryWrite(ms.ToArray());
        }

        [MyAttribute(CheckApp = false, CheckLogin = false, CheckUrl = false)]
        public ActionResult Quit()
        {
            new YJ.Platform.OnlineUsers().Remove(YJ.Platform.Users.CurrentUserID);
            Session.RemoveAll();
            string cookieName = YJ.Utility.Keys.SessionKeys.UserID.ToString();
            Response.Cookies[cookieName].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies[cookieName].Value = "";
            return Redirect("~/Login");
        }
    }
}
