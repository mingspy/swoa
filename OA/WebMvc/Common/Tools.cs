using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.Script.Serialization;
using YJ.Platform;

namespace WebMvc.Common
{
    public class Tools
    {
        /// <summary>
        /// 包含文件
        /// </summary>
        public static string IncludeFiles
        {
            get 
            {
                return
                    string.Format(@"<link href=""{0}Content/Theme/Common.css"" rel=""stylesheet"" type=""text/css"" media=""screen""/>
    <link href=""{0}Content/Theme/{1}/Style/style.css"" id=""style_style"" rel=""stylesheet"" type=""text/css"" media=""screen""/>
    <link href=""{0}Content/Theme/{1}/Style/ui.css"" id=""style_ui"" rel=""stylesheet"" type=""text/css"" media=""screen""/> 
    <link href=""{0}Content/Theme/{1}/jqgrid/ui.jqgrid.css"" id=""style_style"" rel=""stylesheet"" type=""text/css"" media=""screen""/>
    <link href=""{0}Content/Theme/{1}/jqgrid/jquery-ui.css"" id=""style_ui"" rel=""stylesheet"" type=""text/css"" media=""screen""/> 
    <link href=""{0}Scripts/font-awesome/css/font-awesome.min.css"" rel=""stylesheet"" />
    <script type=""text/javascript"" src=""{0}Scripts/My97DatePicker/WdatePicker.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/jquery-1.12.4.min.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/jquery.cookie.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/json.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/roadui.core.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/roadui.button.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/roadui.calendar.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/roadui.file.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/roadui.member.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/roadui.dict.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/roadui.menu.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/roadui.select.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/roadui.combox.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/roadui.tab.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/roadui.text.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/roadui.textarea.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/roadui.editor.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/roadui.tree.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/roadui.validate.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/roadui.window.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/roadui.dragsort.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/roadui.selectico.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/roadui.selectdiv.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/roadui.accordion.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/roadui.grid.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/roadui.init.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/jqGrid/grid.locale-cn.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/jqGrid/jquery.jqGrid.min.js""></script>
    <script type=""text/javascript"" src=""{0}Scripts/jqGrid/jquery-ui.min.js""></script>
    <link href=""{0}Scripts/select2/select2.css"" rel=""stylesheet"" />
    <script src=""{0}Scripts/select2/select2.full.js""></script>"
    , BaseUrl + "/", YJ.Utility.Config.Theme);
            }
        }
        
    
        public static string BaseUrl
        {
            get
            {
                return YJ.Utility.Config.BaseUrl;
            }
        }

        public static bool CheckLogin(out string msg)
        {
            msg = "";
            bool isLogin = true;
            object session = System.Web.HttpContext.Current.Session[YJ.Utility.Keys.SessionKeys.UserID.ToString()];
            Guid uid;
            if (session == null || !session.ToString().IsGuid(out uid) || uid == Guid.Empty)
            {
                isLogin = false;
            }
            uid = YJ.Platform.Users.CurrentUserID;
            string uniqueIDSessionKey = YJ.Utility.Keys.SessionKeys.UserUniqueID.ToString();
            var user = new YJ.Platform.OnlineUsers().Get(uid);
            if (user == null)
            {
                isLogin = false;
            }
            else if (System.Web.HttpContext.Current.Session[uniqueIDSessionKey] == null)
            {
                isLogin = false;
            }
            else if (string.Compare(System.Web.HttpContext.Current.Session[uniqueIDSessionKey].ToString(), user.UniqueID.ToString(), true) != 0)
            {
                msg = string.Format("您的帐号在{0}登录,您被迫下线!", user.IP);
                isLogin = false;
            }


            return isLogin;
        }

        public static bool CheckLogin(bool redirect = true)
        {
            string msg;
            if (!CheckLogin(out msg))
            {
                if (!redirect)
                {
                    System.Web.HttpContext.Current.Response.Write("登录验证失败!");
                    System.Web.HttpContext.Current.Response.End();
                    return false;
                }
                else
                {
                    if (!YJ.Utility.Tools.IsPhoneAccess())
                    {
                        string lastURL = System.Web.HttpContext.Current.Request.Url.PathAndQuery;
                        System.Web.HttpContext.Current.Response.Write("<script>top.lastURL='" + lastURL + "';top.currentWindow=window;top.login();</script>");
                        System.Web.HttpContext.Current.Response.End();
                    }
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 检查访问地址
        /// </summary>
        /// <param name="isEnd"></param>
        /// <returns></returns>
        public static bool CheckReferrer(bool isEnd = true)
        {
            var urlReferrer = HttpContext.Current.Request.UrlReferrer;
            if (urlReferrer == null)
            {
                if (isEnd)
                {
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.Write("访问地址错误!");
                    HttpContext.Current.Response.End();
                }
                return false;
            }
            bool IsUri = HttpContext.Current.Request.Url.Host.Equals(urlReferrer.Host, StringComparison.CurrentCultureIgnoreCase);
            if (!IsUri && isEnd)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Write("访问地址错误!");
                HttpContext.Current.Response.End();
            }
            return IsUri;
        }

        /// <summary>
        /// 检查应用程序权限
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        public static bool CheckApp(out string msg, string appid = "")
        {
            msg = "";
            var userID = YJ.Platform.Users.CurrentUserID;
            if (userID.IsEmptyGuid())
            {
                msg = "<script>top.login();</script>";
                return false;
            }
            appid = appid.IsNullOrEmpty() ? System.Web.HttpContext.Current.Request["appid"] : appid;
            Guid appGuid;
            if (!appid.IsGuid(out appGuid))
            {
                return false;
            }
            List<YJ.Data.Model.MenuUser> menuusers = new YJ.Platform.MenuUser().GetAll();
            string soureMember;
            string params1;
            bool isUse = new YJ.Platform.Menu().HasUse(appGuid, userID, menuusers, out soureMember, out params1);
            if (!isUse)
            {
                return false;
            }
            else
            {
                string url = HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"].ToString();
                if (!url.IsNullOrEmpty())
                {
                    url = url.TrimStart('/');
                    if (!url.IsNullOrEmpty())
                    {
                        var subpageList = new YJ.Platform.AppLibrarySubPages().GetAll().FindAll(p => p.Address.Contains(url, StringComparison.CurrentCultureIgnoreCase));
                        if (subpageList.Count > 0)
                        {
                            foreach (var sub in subpageList)
                            {
                                var menu = menuusers.Find(p => p.MenuID == appGuid && p.SubPageID == sub.ID
                                    && p.Users.Contains(userID.ToString(), StringComparison.CurrentCultureIgnoreCase));
                                if (menu != null)
                                {
                                    return true;
                                }
                            }
                            return false;
                        }
                    }
                }
                return isUse;
            }
        }

        /// <summary>
        /// 将当前url参数转换为RouteValueDictionary(以便于在mvc中重定向时的参数)
        /// </summary>
        /// <returns></returns>
        public static System.Web.Routing.RouteValueDictionary GetRouteValueDictionary()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            string query = System.Web.HttpContext.Current.Request.Url.Query;
            if (query.IsNullOrEmpty())
            {
                return new System.Web.Routing.RouteValueDictionary(dict);
            }
            string[] queryArray = query.TrimStart('?').Split('&');
            foreach (string q in queryArray)
            {
                string[] qArray = q.Split('=');
                if (qArray.Length < 2)
                {
                    continue;
                }
                dict.Add(qArray[0], qArray[1]);
            }
            return new System.Web.Routing.RouteValueDictionary(dict);
        }

        /// <summary>
        /// 将对象序列化为JSON字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObject(object obj)
        {
            return LitJson.JsonMapper.ToJson(obj);
        }

        /// <summary>
        /// 得到一个应用的按钮显示HTML
        /// </summary>
        /// <param name="showType"></param>
        /// <param name="appid"></param>
        /// <returns></returns>
        public static string GetAppButtonHtml(int showType, string appid = "", string subpageid = "")
        {
            if (appid.IsNullOrEmpty())
            {
                appid = System.Web.HttpContext.Current.Request.QueryString["appid"];
            }
            var dicts = YJ.Platform.MenuUser.getButtonsHtml(appid, subpageid);
            return dicts[showType];
        }

        /// <summary>
        /// 得到一个应用的按钮显示HTML
        /// </summary>
        /// <param name="showType"></param>
        /// <param name="appid"></param>
        /// <returns></returns>
        public static Dictionary<int,string> GetAppButtonHtml(string appid = "", string subpageid = "")
        {
            if (appid.IsNullOrEmpty())
            {
                appid = System.Web.HttpContext.Current.Request.QueryString["appid"];
            }
            var dicts = YJ.Platform.MenuUser.getButtonsHtml(appid, subpageid, System.Web.HttpContext.Current.Request.QueryString["programid"]);
            return dicts;
        }

        /// <summary>
        /// 定时检查超时自动提交的任务
        /// </summary>
        public static void ExpiredAutoSubmit(object source, System.Timers.ElapsedEventArgs e)
        {
            new YJ.Platform.WorkFlowTask().ExpiredAutoSubmit();
        }

        /// <summary>
        /// 定时检查车辆年检时间是否到期
        /// </summary>
        public static void ExpiredSelectCarYrar(object source, System.Timers.ElapsedEventArgs e)
        {
            SelectCarYear();
        }
        /// <summary>
        /// 定时检查当前时间是否为23:59如果是抓取考勤数据
        /// </summary>
        public static void ExpiredSelectWorkAttendance(object source, System.Timers.ElapsedEventArgs e)
        {
            SelectWorkAttendance();
        }

        /// <summary>
        /// 定时检查当前时间是否为23:59如果是抓取考勤数据
        /// </summary>
        public static void ImportSampleResult(object source, System.Timers.ElapsedEventArgs e)
        {
            ImportSampleResultAuto();
        }

        public static void ImportSampleResultAuto()
        {
            try
            {
                DateTime now = DateTime.Now;
                // 21点以后不导入
                if(now.Hour > 20)
                {
                    //return;
                }
                int count = 0;
                string oneMonth = now.AddDays(-30).ToString("yyyy-MM-dd");
                YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
                string last_10 = now.AddMinutes(-10).ToString("yyyy-MM-dd HH:mm:ss");

                YJ.Data.MSSQL.DBHelper lmsDb = new YJ.Data.MSSQL.DBHelper(System.Configuration.ConfigurationManager.ConnectionStrings["SampleConnection"].ConnectionString);
                string sql = string.Format(
                    @"select bgbh from AmSample 
                        where (panding is null or trim(panding)='') and InDate > '{0}' 
                        and bgbh not in (
                            select bgbh from AmSampleSync where (last_time > '{1}' and status != 0) or last_time > '{2}'
                        ) 
                    order by InDate",
                    oneMonth, oneMonth, last_10);
                DataTable dt = db.GetDataTable(sql);
                foreach (DataRow item in dt.Rows)
                {
                    string now_str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string bgbh = item["bgbh"].ToString();
                    sql = string.Format("select * from OA_SampleView where bgbh='{0}' ", bgbh);
                    DataTable dt2 = lmsDb.GetDataTable(sql);
                     foreach (DataRow item2 in dt2.Rows)
                    {
                        sql = string.Format("MERGE INTO [AmSampleSync] as T"
                        + " USING(SELECT '{0}' AS bgbh, '{1}' AS panding, '{2}' as last_time, '{3}' as pz_bz, '{4}' as pz_time) AS S"
                        + " ON T.bgbh = S.bgbh "
                        + " WHEN MATCHED THEN"
                        + " UPDATE SET T.panding = S.panding, T.last_time = S.last_time, T.pz_bz = S.pz_bz, T.pz_time = S.pz_time"
                        + " WHEN NOT MATCHED THEN"
                        + " INSERT([bgbh], [panding],[pz_bz],[pz_time],[last_time] ) VALUES(S.bgbh, S.panding, S.pz_bz, S.pz_time, S.last_time); ",
                            bgbh, item2["panding"].ToString(), now_str,
                            item2["pz_bz"].ToString().ToInt32(),
                            item2["pz_time"].ToString().IsNullOrEmpty() ? DateTime.Now : item2["pz_time"].ToString().ToDateTime());
                        using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                        {
                            db.Execute(sql);

                            scope.Complete();
                            count += 1;
                        }
                    }
                     
                }

            }
            catch (Exception e)
            {

                YJ.Platform.Log.Add("样品结果导入失败，失败原因" + e + "", "", YJ.Platform.Log.Types.微信企业号);
            }

        }

        /// <summary>
        /// 抓取数据
        /// </summary>
        public static void SelectWorkAttendance()
        {
           
            try
            {
                //获取数据字典里的secret
                YJ.Platform.Dictionary dic = new YJ.Platform.Dictionary();
                YJ.Data.Model.Dictionary secrets = dic.GetByCode("weixinsign");
                string secret = secrets.Note;
                string BDTime = DateTime.Now.ToString("HH");
                if (Convert.ToInt32(BDTime) == 1)
                {
                    //查询token
                    string token = YJ.Platform.WeiXin.Config.GetAccessToken(secret);
                    //获得考勤数据
                    POSTCheckindata(token);
                    YJ.Platform.Log.Add("考勤记录抓取成功", "", YJ.Platform.Log.Types.微信企业号);
                }
                YJ.Platform.Log.Add(""+ Convert.ToInt32(BDTime) + "点尝试抓取考勤记录", "", YJ.Platform.Log.Types.微信企业号);
            }
            catch (Exception e)
            {

                YJ.Platform.Log.Add("考勤记录抓取失败，失败原因"+e+"", "", YJ.Platform.Log.Types.微信企业号);
            }

        }
        /// <summary>
        /// 通过appID和appsecret获取Access_token
        /// </summary>
        /// <returns></returns>
        public static string GetAccessToken()
        {
            //appid
            string appid = YJ.Utility.Config.wxqy_CorpID;
            //secret
            string secret = YJ.Utility.Config.wxqy_Secret;
            //微信企业号接口的URL
            string tokenUrl = string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", appid, secret);
            var wc = new WebClient();
            //请求
            var strReturn = wc.DownloadString(tokenUrl);
            return strReturn;
         }
        /// <summary>
        /// 通过appID和appsecret获取Access_token
        /// </summary>
        /// <returns></returns>
        public static void POSTCheckindata(string token)
        {
            YJ.Platform.Users user = new YJ.Platform.Users();
            List<YJ.Data.Model.Users> list = new List<YJ.Data.Model.Users>();
            List<object> lists = new List<object>();
            string error = "";
            //定义转换时间戳的最低基数
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            //获取所有人员信息
            list = user.GetAll();
            //遍历所有人员信息
            foreach (var item in list)
            {
                //判断是否是微信公众号的成员
                if (item.Mobile.IsNullOrEmpty() && item.Email.IsNullOrEmpty() && item.WeiXin.IsNullOrEmpty())
                {
                    continue;
                }
                    lists.Add(item.Account);
            }
            WebClient wc = new WebClient();
            //定义编码类型
            Encoding encoding = Encoding.UTF8;
            //获取前一天日期
            string dt = DateTime.Now.AddDays(-1).Date.ToString("yyyy-MM-dd");
            //开始抓取时间
            string BDTime = dt + " 01:01";
            //结束抓取时间
            string EDTime = dt + " 23:59";
            //转换成时间日期格式
            DateTime begin =Convert.ToDateTime(BDTime);
            //转换成时间日期格式
            DateTime endgin = Convert.ToDateTime(EDTime);
            //转换时间戳
            long b = (begin.Ticks - startTime.Ticks) / 10000000;
            //转换时间戳
            long e = (endgin.Ticks - startTime.Ticks) / 10000000;
            //微信企业号打卡记录请求url
            string tokenUrl = string.Format("https://qyapi.weixin.qq.com/cgi-bin/checkin/getcheckindata?access_token={0}", token);
            foreach (var item in lists)
            {

                    //请求参数data
                    object data = new
                    {
                        opencheckindatatype = 3,
                        starttime = b,
                        endtime = e,
                        useridlist = item
                    };
                    //转成json格式
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    string myJson = jss.Serialize(data);
                    //将字符串转成字节
                    var postData = encoding.GetBytes(myJson);
                    //开始请求
                    var result = wc.UploadData(tokenUrl, "post", postData);
                    //解析返回字节
                    string datas = encoding.GetString(result);
                    JObject jo = (JObject)JsonConvert.DeserializeObject(datas);
                    JToken checkindata = jo["checkindata"];
                   if (checkindata.Count()==0)
                   {
                    string errcode = jo["errcode"].ToString();
                    string errmsg= jo["errmsg"].ToString();
                    error += "账号："+item+"抓取考勤失败，错误码："+ errcode + ",详情："+ errmsg + " \r\n";
                   }
                else
                 {
                    AddCheckindata(checkindata);
                 }
                    
            }

            YJ.Platform.Log.Add("考勤记录抓取失败", error, YJ.Platform.Log.Types.微信企业号);
        }
        /// <summary>
        /// 插入考勤数据
        /// </summary>
        /// <param name="checkindata"></param>
        public static void AddCheckindata(JToken checkindata) {
            foreach (var items in checkindata)
            {
                YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
                Guid ID = Guid.NewGuid();
                string userid = items["userid"].ToString();
                string groupname = items["groupname"].ToString();
                string checkin_type = items["checkin_type"].ToString();
                string exception_type = items["exception_type"].ToString();
                string checkin_time = items["checkin_time"].ToString();
                DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                long lTime = long.Parse(checkin_time+ "0000000");
                TimeSpan toNow = new TimeSpan(lTime);
                DateTime time= dtStart.Add(toNow);
                string location_title = items["location_title"].ToString();
                string location_detail = items["location_detail"].ToString();
                string wifiname = items["wifiname"].ToString();
                string notes = items["notes"].ToString();
                string wifimac = items["wifimac"].ToString();
                string sql = @"INSERT INTO OaworkAttendance(ID,userid,groupname,checkin_type,exception_type,checkin_time,location_title,location_detail,wifiname,notes,wifimac) 
				VALUES('"+ ID + "','"+ userid + "','"+ groupname + "','"+ checkin_type + "','"+ exception_type + "','"+ time + "','"+ location_title + "','"+ location_detail + "','"+ wifiname + "','"+ notes + "','"+ wifimac + "')";
                db.Execute(sql);
            }

        }
        /// <summary>
        /// 定时检查车辆年检和保养日期
        /// </summary>
        public static void SelectCarYear()
        {
            YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
            YJ.Platform.Users User = new YJ.Platform.Users();
            Users bUsers = new Users();
            YJ.Platform.WeiXin.Agents bAgents = new YJ.Platform.WeiXin.Agents();
            YJ.Platform.ShortMessage SM = new YJ.Platform.ShortMessage();
            System.Text.StringBuilder userAccounts = new System.Text.StringBuilder();
            //查询年检时间
            string sql = "select ID,CarID,CarYearCheck from OaCarMaintain";
            DataTable CarYearCheck = db.GetDataTable(sql);
            //查询保养时间
            string sql1 = "select ID,CarID,CarMaintain from OaCarMaintain";
            DataTable CarMaintain = db.GetDataTable(sql1);
            WorkFlowTask btask = new WorkFlowTask();
            Guid CarYearCheckGroup = Guid.NewGuid();
            Guid CarMaintainGroup = Guid.NewGuid();
            //循环遍历对比年检时间
            foreach (DataRow dr in CarYearCheck.Rows)
            {

                if (CarYearCheck.Rows.Count > 0)
                {
                    if (dr["CarYearCheck"].ToString() != "")
                    {
                        //获取当前时间
                        string BDTime = DateTime.Now.ToString("yyyy-MM-dd");
                        string CarYearCheckTime = Convert.ToDateTime(dr["CarYearCheck"]).ToString("yyyy-MM-dd");
                        string sql3 = "select CarController from OaCar where ID='" + dr["CarID"] + "'";
                        string Users = db.ExecuteScalar(sql3);
                        string sql4 = "select CarName from OaCar where ID='" + dr["CarID"] + "'";
                        string Name = db.ExecuteScalar(sql4);
                        string userid = User.RemovePrefix1(Users);
                        string UserName = User.GetName(userid.ToGuid());
                        string UserAccounts = User.GetAccountByID(userid.ToGuid());
                        if (BDTime == CarYearCheckTime)
                        {
                            YJ.Data.Model.ShortMessage sm = new YJ.Data.Model.ShortMessage();
                            sm.Contents = "<p>您的名为" + Name + "的车辆就要到年检日期了请您及时处理 </p>";
                            sm.ID = Guid.NewGuid();
                            sm.ReceiveUserID = userid.ToGuid();
                            sm.ReceiveUserName = UserName;
                            sm.Status = 0;
                            sm.Title = "年检日期到期提醒";
                            sm.Type = 0;
                            Guid msgID = Guid.NewGuid();
                            string msgContents = "您的名为'" + Name + "'的车辆马上到年检日期！";
                            string linkUrl = "";
                            ShortMessage.Send(userid.ToGuid(), UserName, "车辆年检到期提醒", msgContents, 1, linkUrl, Guid.NewGuid().ToString(), msgID.ToString(), CarYearCheckGroup.ToString());

                            SM.SendToWeiXin(sm, UserAccounts.ToString());
                            //new YJ.Platform.WeiXin.Message().SendText(msgContents, bUsers.GetAccountByID(userid.ToGuid()), agentid: bAgents.GetAgentIDByCode("weixinagents_waittasks"), async: true);
                        }
                    }

                }

            }
            //循环遍历对比保养时间
            foreach (DataRow dr in CarMaintain.Rows)
            {
                if (CarMaintain.Rows.Count > 0)
                {
                    if (dr["CarMaintain"].ToString() != "")
                    {
                        //获取当前时间
                        string BDTime = DateTime.Now.ToString("yyyy-MM-dd");
                        string CarMaintainTime = Convert.ToDateTime(dr["CarMaintain"]).ToString("yyyy-MM-dd");
                        string sql3 = "select CarController from OaCar where ID='" + dr["CarID"] + "'";
                        string Users = db.ExecuteScalar(sql3);
                        string sql4 = "select CarName from OaCar where ID='" + dr["CarID"] + "'";
                        string Name = db.ExecuteScalar(sql4);
                        string userid = User.RemovePrefix1(Users);
                        string UserName = User.GetName(userid.ToGuid());
                        string UserAccounts = User.GetAccountByID(userid.ToGuid());
                        if (BDTime == CarMaintainTime)
                        {
                            YJ.Data.Model.ShortMessage sm = new YJ.Data.Model.ShortMessage();
                            sm.Contents = "<p>您的" + Name + "就要到保养日期了请您及时处理</p>";
                            sm.ID = Guid.NewGuid();
                            sm.ReceiveUserID = userid.ToGuid();
                            sm.ReceiveUserName = UserName;
                            sm.Status = 0;
                            sm.Title = "保养日期到期提醒";
                            sm.Type = 0;
                            Guid msgID = Guid.NewGuid();
                            string msgContents = "您的名为'" + Name + "'的车辆马上到保养日期！";
                            string linkUrl = "";
                            ShortMessage.Send(userid.ToGuid(), UserName, "车辆保养到期提醒", msgContents, 1, linkUrl, Guid.NewGuid().ToString(), msgID.ToString(), CarMaintainGroup.ToString());
                           SM.SendToWeiXin(sm, UserAccounts.ToString());
                            //new YJ.Platform.WeiXin.Message().SendText(msgContents, bUsers.GetAccountByID(userid.ToGuid()), agentid: bAgents.GetAgentIDByCode("weixinagents_waittasks"), async: true);
                        }
                    }
                }

            }
        }

    }

}