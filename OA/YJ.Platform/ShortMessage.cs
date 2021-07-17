using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNet.SignalR;

namespace YJ.Platform
{
    public class ShortMessage
    {
        private YJ.Data.Interface.IShortMessage dataShortMessage;
        private static string cacheKey = YJ.Utility.Keys.CacheKeys.ShortMessage.ToString();
        public ShortMessage()
        {
            this.dataShortMessage = Data.Factory.Factory.GetShortMessage();
        }

        /// <summary>
        /// 发送站内短信
        /// </summary>
        /// <param name="userID">接收人员ID</param>
        /// <param name="userName">接收人员姓名</param>
        /// <param name="title">标题</param>
        /// <param name="contents">内容</param>
        /// <param name="msgType">类别0用户消息 1系统消息</param>
        /// <param name="linkUrl">连接地址，点击消息的操作</param>
        /// <param name="linkID">连接ID，用着特殊操作的标识</param>
        /// <param name="msgID">消息ID,用于提前需要预知ID的情况</param>
        public static Guid Send(Guid userID,string userName, string title, string contents, int msgType = 0, string linkUrl = "", string linkID = "", string msgID = "",string groupID="")
        {
            if (userID.IsEmptyGuid() || title.IsNullOrEmpty() || contents.IsNullOrEmpty())
            {
                return Guid.Empty;
            }
            if (userName.IsNullOrEmpty())
            {
                userName = new Users().GetName(userID);
            }
            Data.Model.ShortMessage sm = new Data.Model.ShortMessage();
            sm.Contents = contents;
            sm.ID = msgID.IsGuid() ? msgID.ToGuid() : Guid.NewGuid();
            sm.LinkID = linkID;
            sm.LinkUrl = linkUrl;
            sm.ReceiveUserID = userID;
            sm.ReceiveUserName = userName;
            sm.SendTime = YJ.Utility.DateTimeNew.Now;
            sm.SendUserID = Users.CurrentUserID.IsEmptyGuid()? new Guid(): Users.CurrentUserID;
            sm.SendUserName = Users.CurrentUserID.IsEmptyGuid()? "系统发送": Users.CurrentUserName;
            sm.Status = 0;
            sm.Title = title;
            sm.Type = msgType;
            new ShortMessage().Add(sm);
            
            string msgContents = string.Empty;
            if (!linkUrl.IsNullOrEmpty())
            {
                msgContents = "<a class=\"blue1\" href=\"" + linkUrl + "\">" + sm.Contents + "</a>";
            }
            else
            {
                msgContents = sm.Contents;
            }
            SiganalR(userID, sm.ID.ToString(), title, msgContents);
            return sm.ID;
        }

        /// <summary>
        /// 调用signalr向客户端发消息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="contents"></param>
        /// <param name="count"></param>
        public static void SiganalR(Guid userID, string id, string title, string contents)
        {
            var context1 = GlobalHost.ConnectionManager.GetConnectionContext<SignalR.SignalRConnection>();
            LitJson.JsonData jd = new LitJson.JsonData();
            jd["id"] = id;
            jd["title"] = title;
            jd["contents"] = contents;
            jd["count"] = new ShortMessage().GetAllNoReadByUserID(userID).Count;
            context1.Groups.Send(userID.ToString().ToLower(), jd.ToJson());
        }

        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.ShortMessage model)
        {
            int i = dataShortMessage.Add(model);
            return i;
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.ShortMessage model)
        {
            return dataShortMessage.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.ShortMessage> GetAll()
        {
            return dataShortMessage.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.ShortMessage Get(Guid id)
        {
            return dataShortMessage.Get(id);
        }

        /// <summary>
        /// 根据主键查询一条已读记录
        /// </summary>
        public YJ.Data.Model.ShortMessage GetRead(Guid id)
        {
            return dataShortMessage.GetRead(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            int i = dataShortMessage.Delete(id);
            return i;
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataShortMessage.GetCount();
        }
        /// <summary>
        /// 查询一个人员未读记录
        /// </summary>
        public List<YJ.Data.Model.ShortMessage> GetAllNoReadByUserID(Guid userID)
        {
            return dataShortMessage.GetAllNoReadByUserID(userID);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.ShortMessage> GetAllNoRead()
        {
            return dataShortMessage.GetAllNoRead();
        }

        /// <summary>
        /// 将一个消息标记为已读
        /// </summary>
        public int UpdateStatus(Guid id)
        {
            return dataShortMessage.UpdateStatus(id);
        }

        public List<YJ.Data.Model.ShortMessage> GetList(out string pager, int[] status, string query = "", string title = "", string contents = "", string senderID = "", string date1 = "", string date2 = "", string receiveID = "")
        {
            senderID = Utility.Tools.GetSqlInString(new YJ.Platform.Organize().GetAllUsersIdList(senderID).ToArray());
            return dataShortMessage.GetList(out pager, status, query, title, contents, senderID, date1, date2, receiveID);
        }

        public List<YJ.Data.Model.ShortMessage> GetList(out long count, int[] status, int size, int number, string title = "", 
            string contents = "", string senderID = "", string date1 = "", string date2 = "", string receiveID = "", string order = "")
        {
            //senderID = Utility.Tools.GetSqlInString(new YJ.Platform.Organize().GetAllUsersIdList(senderID).ToArray());
            return dataShortMessage.GetList(out count, status, size, number, title, contents, senderID, date1, date2, receiveID, order);
        }
        /// <summary>
        /// 不分页查询
        /// </summary>
        /// <param name="count"></param>
        /// <param name="status"></param>
        /// <param name="size"></param>
        /// <param name="number"></param>
        /// <param name="title"></param>
        /// <param name="contents"></param>
        /// <param name="senderID"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="receiveID"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<YJ.Data.Model.ShortMessage> GetList(int[] status, string title = "",
           string contents = "", string senderID = "", string date1 = "", string date2 = "", string receiveID = "", string order = "")
        {
            //senderID = Utility.Tools.GetSqlInString(new YJ.Platform.Organize().GetAllUsersIdList(senderID).ToArray());
            return dataShortMessage.GetList(status,title, contents, senderID, date1, date2, receiveID, order);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(string linkID, int type)
        {
            int i = dataShortMessage.Delete(linkID, type);
            return i;
        }

        /// <summary>
        /// 发送一条消息到微信
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="userAccounts">接收人员账号</param>
        /// <returns></returns>
        public bool SendToWeiXin(Data.Model.ShortMessage msg, string userAccounts)
        {
            if (msg == null)
            {
                return false;
            }
            WeiXin.Message Message = new WeiXin.Message();
            string[] imags = Utility.Tools.GetImageUrlFromHTML(msg.Contents);
            int agentId = new WeiXin.Agents().GetAgentIDByCode("weixinagents_infocenter");
            if (imags.Length >0)
            {
                string img = YJ.Platform.WeiXin.Config.WebUrl + imags[0];
                //消息列表Tuple title,description,url,picurl
                List<Tuple<string, string, string, string>> articleList = new List<Tuple<string, string, string, string>>();
                articleList.Add(new Tuple<string, string, string, string>(msg.Title, msg.Contents.RemoveHTML().CutString(100), WeiXin.Config.WebUrl+ "ShortMessage/Show?id=" + msg.ID.ToString(), img));
                Message.SendNews(articleList, userAccounts, agentid: agentId);
            }
            else
            {
                Message.SendText(msg.Contents.RemoveHTML().CutString(100) + "\n点击链接阅读全文：" + WeiXin.Config.WebUrl  + "ShortMessage/Show?id=" + msg.ID.ToString(), userAccounts, agentid: agentId);
            }
            
            return true;
        }
    }
}
