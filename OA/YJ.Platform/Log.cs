using System;
using System.Collections.Generic;
using System.Text;

namespace YJ.Platform
{
    public class Log
    {
        private YJ.Data.Interface.ILog dataLog;
        private static YJ.Data.Interface.ILog dataLog1 = Data.Factory.Factory.GetLog();
        private delegate void dgWriteLog(YJ.Data.Model.Log log);
        public Log()
        {
            this.dataLog = dataLog1;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.Log model)
        {
            return dataLog.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.Log> GetAll()
        {
            return dataLog.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.Log Get(Guid id)
        {
            return dataLog.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            return dataLog.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataLog.GetCount();
        }

        public enum Types
        {
            组织机构,
            用户登录,
            菜单权限,
            数据字典,
            流程相关,
            系统错误,
            信息管理,
            系统管理,
            文档中心,
            数据连接,
            微信企业号,
            任务调度,
            其它分类
        }


        /// <summary>
        /// 新增
        /// </summary>
        private static void add(YJ.Data.Model.Log model)
        {
            dataLog1.Add(model);
        }

        /// <summary>
        /// 新增
        /// </summary>
        public static void Add(YJ.Data.Model.Log model)
        {
            dgWriteLog wl = new dgWriteLog(add);
            wl.BeginInvoke(model, null, null);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="err"></param>
        public static void Add(string title, string contents, Types type = Types.其它分类, string oldXML = "", string newXML = "", YJ.Data.Model.Users user = null)
        {
            YJ.Data.Model.Log log = new YJ.Data.Model.Log();
            log.Contents = contents;
            log.ID = Guid.NewGuid();
            log.Title = title;
            log.OldXml = oldXML.IsNullOrEmpty() ? null : oldXML;
            log.NewXml = newXML.IsNullOrEmpty() ? null : newXML;
            log.Type = type.ToString();
            try
            {
                if (user == null)
                {
                    user = Platform.Users.CurrentUser;
                }
                if (user != null)
                {
                    log.UserID = user.ID;
                    log.UserName = user.Name;
                }
                log.IPAddress = YJ.Utility.Tools.GetIPAddress();
                log.Others = string.Format("操作系统：{0} 浏览器：{1}", YJ.Utility.Tools.GetOSName(), YJ.Utility.Tools.GetBrowse());
                log.URL = System.Web.HttpContext.Current.Request.Url.ToString();
            }
            catch { }
            log.WriteTime = YJ.Utility.DateTimeNew.Now;
            Add(log);
        }

        public static void Add(Exception err)
        {
            Add(err.Message, string.Concat(err.Source, err.StackTrace), Types.系统错误);
        }

        /// <summary>
        /// 得到类别下接选择
        /// </summary>
        /// <returns></returns>
        public string GetTypeOptions(string value = "")
        {
            StringBuilder options = new StringBuilder();
            var array = Enum.GetValues(typeof(Types));
            foreach (var arr in array)
            {
                options.AppendFormat("<option value=\"{0}\" {1}>{0}</option>", arr, arr.ToString() == value ? "selected=\"selected\"" : "");
            }
            return options.ToString();
        }

        /// <summary>
        /// 得到一页日志数据
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="order"></param>
        /// <param name="size"></param>
        /// <param name="number"></param>
        /// <param name="title"></param>
        /// <param name="type"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public System.Data.DataTable GetPagerData(out string pager, string query = "", string title = "", string type = "", string date1 = "", string date2 = "", string userID = "")
        {
            return dataLog.GetPagerData(out pager, query, YJ.Utility.Tools.GetPageSize(), YJ.Utility.Tools.GetPageNumber(),
                title, type, date1, date2, Users.RemovePrefix(userID));
        }

        /// <summary>
        /// 得到一页日志数据
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="order"></param>
        /// <param name="size"></param>
        /// <param name="number"></param>
        /// <param name="title"></param>
        /// <param name="type"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public System.Data.DataTable GetPagerData(out long count, int size = 15, int number = 1, string title = "", string type = "", string date1 = "", string date2 = "", string userID = "", string order = "")
        {
            return dataLog.GetPagerData(out count, size, number, title, type, date1, date2, Users.RemovePrefix(userID), order);
        }
    }
}
