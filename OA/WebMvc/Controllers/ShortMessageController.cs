using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Controllers
{
    public class ShortMessageController : MyController
    {
        //
        // GET: /ShortMessage/
        [MyAttribute(CheckApp = false)]
        public ActionResult Index()
        {
            return View();
        }
        [MyAttribute(CheckApp = false)]
        public ActionResult NoRead()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAttribute(CheckApp = false)]
        public string QueryNoRead()
        {
            string s_Title1 = Request.Form["Title1"];
            string s_Contents = Request.Form["Contents"];
            string s_SenderID = Request.Form["SenderID"];
            string s_Date1 = Request.Form["Date1"];
            string s_Date2 = Request.Form["Date2"];
            string sidx = Request.Form["sidx"];
            string sord = Request.Form["sord"];
            int status = Request.Form["status"].ToInt(0);
            string s_RecevieID = YJ.Platform.Users.CurrentUserID.ToString();
            int[] status1 = new int[] { 0 };
            if (2 == status)
            {
                status1 = new int[] { 0, 1 };
                s_SenderID = "'" + CurrentUserID.ToString() + "'";
                s_RecevieID = "";
            }
            else if (1 == status)
            {
                status1 = new int[] { 1 };
                s_SenderID = YJ.Utility.Tools.GetSqlInString(new YJ.Platform.Organize().GetAllUsersIdList(s_SenderID).ToArray());
            }
            
            List<YJ.Data.Model.ShortMessage> MsgList = new List<YJ.Data.Model.ShortMessage>();
            long count;
            int pageSize = YJ.Utility.Tools.GetPageSize();
            int pageNumber = YJ.Utility.Tools.GetPageNumber();
            string order = (sidx.IsNullOrEmpty() ? "SenderTime" : sidx) + " " + (sord.IsNullOrEmpty() ? "asc" : sord);
            MsgList = new YJ.Platform.ShortMessage().GetList(status1,s_Title1, s_Contents, s_SenderID, s_Date1, s_Date2, s_RecevieID, order);
            LitJson.JsonData json = new LitJson.JsonData();
            IEnumerable<IGrouping<string, YJ.Data.Model.ShortMessage>> query =
    MsgList.GroupBy(pet => pet.GroupID);
            count = query.Count();
            query=query.Skip((pageNumber-1) * pageSize).Take(pageSize);
            foreach (IGrouping<string, YJ.Data.Model.ShortMessage> info in query)
            {
                //分组后的集合 
                List<YJ.Data.Model.ShortMessage> msgs = info.ToList<YJ.Data.Model.ShortMessage>();
                LitJson.JsonData j = new LitJson.JsonData();
                j["id"] = msgs[0].ID.ToString();
                j["Title"] = "<a href=\"javascript:show('" + msgs[0].ID + "');\" class=\"blue1\">" + msgs[0].Title + "</a><input type=\"hidden\" id=\"status_" + msgs[0].ID.ToString() + "\" value=\"" + msgs[0].Status + "\"/>";
                j["Contents"] = YJ.Utility.Tools.RemoveHTML(msgs[0].Contents).CutString(100);
                j["SendUserName"] = msgs[0].SendUserName;
                j["SendTime"] = msgs[0].SendTime.ToDateTimeStringS();
                string receiveNames=string.Empty;
                //也可循环得到分组后，集合中的对象，你可以用info.Key去控制 
                foreach (YJ.Data.Model.ShortMessage msg in msgs)
                {
                    receiveNames += msg.ReceiveUserName+",";
                }
                j["ReceiveUserName"] = receiveNames.TrimEnd(',');
                json.Add(j);
            }

            
            //foreach (var msg in MsgList)
            //{
            //    LitJson.JsonData j = new LitJson.JsonData();
            //    j["id"] = msg.ID.ToString();
            //    j["Title"] = "<a href=\"javascript:show('" + msg.ID + "');\" class=\"blue1\">" + msg.Title + "</a><input type=\"hidden\" id=\"status_" + msg.ID.ToString() + "\" value=\"" + msg.Status + "\"/>";
            //    j["Contents"] = YJ.Utility.Tools.RemoveHTML(msg.Contents).CutString(100);
            //    j["SendUserName"] = msg.SendUserName;
            //    j["SendTime"] = msg.SendTime.ToDateTimeStringS();
            //    json.Add(j);
            //}

            return "{\"userdata\":{\"total\":" + count + ",\"pagesize\":" + pageSize + ",\"pagenumber\":" + pageNumber + "},\"rows\":" + json.ToJson() + "}";
        }

        /// <summary>
        /// 将未读标记为已读
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAttribute(CheckApp = false)]
        public string NoReadToRead()
        {
            string checkbox_apps = Request.Form["ids"] ?? "";
            YJ.Platform.ShortMessage SM = new YJ.Platform.ShortMessage();
            foreach (string app in checkbox_apps.Split(','))
            {
                if (!app.IsGuid()) continue;
                SM.UpdateStatus(app.ToGuid());
            }
            return "操作成功!";
        }

        [MyAttribute(CheckApp = false)]
        public void UpdateStatus()
        {
            string id = Request.QueryString["id"];
            if (id.IsGuid())
            {
                new YJ.Platform.ShortMessage().UpdateStatus(id.ToGuid());
            }
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult Send()
        {
            return Send(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAttribute(CheckApp = false)]
        public ActionResult Send(FormCollection collection)
        {
            if (collection != null)
            {
                string Title1 = Request.Form["Title1"];
                string Contents = Request.Form["Contents"];
                string ReceiveUserID = Request.Form["ReceiveUserID"];
                string Files = Request.Form["Files"];
                string sendtoseixin = Request.Form["sendtoseixin"];

                if (Title1.IsNullOrEmpty() || Contents.IsNullOrEmpty() || ReceiveUserID.IsNullOrEmpty())
                {
                    ViewBag.script = "alert('数据验证错误!')";
                    return View();
                }

                var users = new YJ.Platform.Organize().GetAllUsers(ReceiveUserID);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                System.Text.StringBuilder userAccounts = new System.Text.StringBuilder();
                YJ.Data.Model.ShortMessage sm1 = null;
                YJ.Platform.ShortMessage SM = new YJ.Platform.ShortMessage();
                Guid GroupID = Guid.NewGuid();
                foreach (var user in users)
                {
                    YJ.Data.Model.ShortMessage sm = new YJ.Data.Model.ShortMessage();
                    sm.Contents = Contents;
                    sm.ID = Guid.NewGuid();
                    sm.ReceiveUserID = user.ID;
                    sm.ReceiveUserName = user.Name;
                    sm.SendTime = YJ.Utility.DateTimeNew.Now;
                    sm.SendUserID = YJ.Platform.Users.CurrentUserID;
                    sm.SendUserName = YJ.Platform.Users.CurrentUserName;
                    sm.Status = 0;
                    sm.Title = Title1;
                    sm.Type = 0;
                    sm.Files = Files;
                    sm.GroupID = GroupID.ToString();
                    SM.Add(sm);
                    YJ.Platform.ShortMessage.SiganalR(user.ID, sm.ID.ToString(), Title1, Contents.RemoveHTML());
                    sb.Append(user.Name);
                    sb.Append(",");
                    userAccounts.Append(user.Account);
                    userAccounts.Append('|');
                    if (sm1 == null)
                    {
                        sm1 = sm;
                    }
                }

                if ("1" == sendtoseixin && sm1 != null && userAccounts.Length > 0)//发送到微信
                {
                    SM.SendToWeiXin(sm1, userAccounts.ToString().TrimEnd('|'));
                }
                ViewBag.script = string.Format("alert('成功将消息发送给了：{0}!');window.location=window.location;", sb.ToString());
            }

            return View();
        }

        [MyAttribute(CheckApp = false, CheckLogin = false, CheckUrl = false)]
        public ActionResult Show()
        {
            return View();
        }

        [MyAttribute(CheckApp = false, CheckUrl = false)]
        public ActionResult Read()
        {
            return View();
        }
        
        [MyAttribute(CheckApp = false)]
        public ActionResult SendList()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAttribute(CheckApp = false)]
        public string Delete()
        {
            string ids = Request.Form["ids"] ?? "";
            YJ.Platform.ShortMessage SM = new YJ.Platform.ShortMessage();
            if (ids.IsNullOrEmpty())
            {
                return "没有选择要删除的消息!";
            }
            foreach (string id in ids.Split(','))
            {
                if (id.IsGuid())
                {
                    var msg = SM.Get(id.ToGuid());
                    if (msg != null)
                    {
                        SM.Delete(msg.ID);
                        YJ.Platform.Log.Add("删除了站内消息", msg.Serialize(), YJ.Platform.Log.Types.信息管理);
                    }
                }
            }
            return "操作成功!";
        }

        
    }
}
