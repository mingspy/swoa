using System;
using System.Collections.Generic;
using System.Text;

namespace YJ.Platform
{
    public class HastenLog
    {
        private YJ.Data.Interface.IHastenLog dataHastenLog;
        public HastenLog()
        {
            this.dataHastenLog = YJ.Data.Factory.Factory.GetHastenLog();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.HastenLog model)
        {
            return dataHastenLog.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.HastenLog model)
        {
            return dataHastenLog.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.HastenLog> GetAll()
        {
            return dataHastenLog.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.HastenLog Get(Guid id)
        {
            return dataHastenLog.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            return dataHastenLog.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataHastenLog.GetCount();
        }

        /// <summary>
        /// 得到催办方式
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetHastenTypeCheckboxString(string name, string value)
        {
            List<System.Web.UI.WebControls.ListItem> userItem = new List<System.Web.UI.WebControls.ListItem>();
            Dictionary<int, string> dicts = new Dictionary<int, string>();
            dicts.Add(1, "手机短信");
            dicts.Add(2, "邮件");
            dicts.Add(3, "站内消息");
            if (WeiXin.Config.IsUse)
            {
                dicts.Add(4, "微信");
            }
            foreach (var dict in dicts)
            {
                userItem.Add(new System.Web.UI.WebControls.ListItem(dict.Value, dict.Key.ToString()) { Selected = true });
            }
            return YJ.Utility.Tools.GetCheckBoxString(userItem.ToArray(), name, (value ?? "").Split(','), "validate='checkbox'");
        }

        /// <summary>
        /// 催办
        /// </summary>
        /// <param name="types">催办类型</param>
        /// <param name="users">被催办人</param>
        /// <param name="contents">催办内容</param>
        /// <param name="task">催办任务</param>
        public static void Hasten(string types, string users, string contents, Data.Model.WorkFlowTask task, string othersParams = "")
        {
            if (users.IsNullOrEmpty() || types.IsNullOrEmpty() || task == null)
            {
                return;
            }
            string[] usersArray = users.Split(',');
            Guid msgID = Guid.NewGuid();
            var nextTasks = new WorkFlowTask().GetNextTaskList(task.ID).FindAll(p => p.Status.In(0, 1));
            string url = "";
            if (System.Web.HttpContext.Current.Request.Url != null
                && System.Web.HttpContext.Current.Request.Url.AbsolutePath.EndsWith(".aspx", StringComparison.CurrentCultureIgnoreCase))
            {
                url = "/Platform/WorkFlowRun/Default.aspx";
            }
            else
            {
                url = "/WorkFlowRun/Index";
            }
            Guid groupID = Guid.NewGuid();
            foreach (string type in types.Split(','))
            {
                int type1;
                if (!type.IsInt(out type1))
                {
                    continue;
                }
                foreach(string user in usersArray)
                {
                    string userid = YJ.Platform.Users.RemovePrefix(user);
                    Guid userGuid;
                    if(!userid.IsGuid(out userGuid))
                    {
                        continue;
                    }
                    var waitTask = nextTasks.Find(p => p.ReceiveID == userGuid);
                    string linkUrl = waitTask == null ? "" : "javascript:openApp('" + url + "?flowid=" + waitTask.FlowID + "&stepid=" + waitTask.StepID + "&instanceid=" + waitTask.InstanceID + "&taskid=" + waitTask.ID + "&groupid=" + waitTask.GroupID + "',0,'" + waitTask.Title.Replace1(",", "") + "','tab_" + waitTask.ID + "');closeMessage('" + msgID + "');";
                    switch (type1)
                    { 
                        case 1: //手机短信
                            string mobileNumber = new YJ.Platform.Users().GetMobileNumber(userGuid);
                            SMSLog.SendSMS(mobileNumber, contents);
                            break;
                        case 2: //邮件
                            Email.Send(userGuid, "任务催办", contents);
                            break;
                        case 3: //站内短信
                            var user1 = new Users().Get(userGuid);
                            if (user1 != null)
                            {
                                ShortMessage.Send(user1.ID, user1.Name, "任务催办", contents, 0, linkUrl, task.ID.ToString(), msgID.ToString());
                            }
                            break;
                        case 4://微信
                            var user2 = new Users().Get(userGuid);
                            if (user2 != null)
                            {
                                WeiXin.Message wxMsg = new WeiXin.Message();
                                wxMsg.SendText(contents, user2.Account, agentid: new WeiXin.Agents().GetAgentIDByCode("weixinagents_waittasks"), async: true);
                            }
                            break;
                    }
                }
            }
            YJ.Data.Model.HastenLog hastenLog = new YJ.Data.Model.HastenLog();
            hastenLog.Contents = contents;
            hastenLog.ID = Guid.NewGuid();
            hastenLog.SendTime = YJ.Utility.DateTimeNew.Now;
            hastenLog.SendUser = YJ.Platform.Users.CurrentUserID;
            hastenLog.SendUserName = YJ.Platform.Users.CurrentUserName;
            hastenLog.OthersParams = othersParams.IsNullOrEmpty() ? task.ID.ToString() : othersParams;
            hastenLog.Types = types;
            hastenLog.Users = users;
            new HastenLog().Add(hastenLog);
        }
    }
}
