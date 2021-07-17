using System;
using System.Collections.Generic;
using System.Text;

namespace YJ.Platform
{
    public class SMSLog
    {
        private static YJ.Data.Interface.ISMSLog dataSMSLog = YJ.Data.Factory.Factory.GetSMSLog();
        private delegate void dgWriteLog(YJ.Data.Model.SMSLog smsLog);
        public SMSLog()
        {
            
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.SMSLog model)
        {
            return dataSMSLog.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.SMSLog model)
        {
            return dataSMSLog.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.SMSLog> GetAll()
        {
            return dataSMSLog.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.SMSLog Get(Guid id)
        {
            return dataSMSLog.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            return dataSMSLog.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataSMSLog.GetCount();
        }

        /// <summary>
        /// 新增
        /// </summary>
        private static void add(YJ.Data.Model.SMSLog model)
        {
            dataSMSLog.Add(model);
        }

        /// <summary>
        /// 新增
        /// </summary>
        public static void AddSync(YJ.Data.Model.SMSLog model)
        {
            dgWriteLog wl = new dgWriteLog(add);
            wl.BeginInvoke(model, null, null);
        }

        /// <summary>
        /// 添加一条短信发送日志
        /// </summary>
        /// <param name="mobileNumber">手机号码</param>
        /// <param name="contents">短信内容</param>
        /// <param name="status">状态：1 成功 0失败</param>
        /// <param name="sendUserID">发送人ID</param>
        /// <param name="sendUserName">发送人姓名</param>
        /// <param name="sendUserName">其它信息</param>
        public static void AddSync(string mobileNumber, string contents, int status, Guid sendUserID, string sendUserName = "", string note="")
        {
            Data.Model.SMSLog model = new Data.Model.SMSLog();
            model.Contents = contents;
            model.ID = Guid.NewGuid();
            model.MobileNumber = mobileNumber;
            model.SendTime = YJ.Utility.DateTimeNew.Now;
            model.SendUserID = sendUserID == null || sendUserID.IsEmptyGuid() ? YJ.Platform.Users.CurrentUserID : sendUserID;
            model.SendUserName = sendUserName.IsNullOrEmpty() ? YJ.Platform.Users.CurrentUserName : sendUserName;
            model.Status = status;
            model.Note = note;
            AddSync(model);
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mobileNumber">手机号码，多个用,号隔开</param>
        /// <param name="contents">短信内容</param>
        /// <returns></returns>
        public static string SendSMS(string mobileNumber, string contents)
        {
            if (mobileNumber.IsNullOrEmpty() || contents.IsNullOrEmpty())
            {
                return "";
            }
            foreach (string number in mobileNumber.Split(','))
            {
                //在这里实现自己的发送短信方法



                AddSync(number, contents, 1, Guid.Empty, "", "");
            }
            
            return "";
        }
    }
}
