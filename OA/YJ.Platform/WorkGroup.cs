using System;
using System.Collections.Generic;
using System.Text;

namespace YJ.Platform
{
    public class WorkGroup
    {
        /// <summary>
        /// 工作组在机构字符串中的前缀
        /// </summary>
        public const string PREFIX = "w_";
        private YJ.Data.Interface.IWorkGroup dataWorkGroup;
        public WorkGroup()
        {
            this.dataWorkGroup = Data.Factory.Factory.GetWorkGroup();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.WorkGroup model)
        {
            return dataWorkGroup.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.WorkGroup model)
        {
            return dataWorkGroup.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.WorkGroup> GetAll()
        {
            return dataWorkGroup.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.WorkGroup Get(Guid id)
        {
            return dataWorkGroup.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            return dataWorkGroup.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataWorkGroup.GetCount();
        }

        /// <summary>
        /// 得到工作组名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetName(Guid id)
        {
            var wg = Get(id);
            return wg == null ? "" : wg.Name;
        }

        /// <summary>
        /// 去除ID前缀
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string RemovePrefix(string id)
        {
            return id.IsNullOrEmpty() ? "" : id.Replace(PREFIX, "");
        }

        /// <summary>
        /// 去除ID前缀
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string RemovePrefix1(string id)
        {
            return id.IsNullOrEmpty() ? "" : id.Replace(PREFIX, "");
        }

        /// <summary>
        /// 得到工作组下的人员名称字符串
        /// </summary>
        /// <param name="members">工作组成员字符串</param>
        /// <param name="split"></param>
        /// <returns></returns>
        public string GetUsersNames(string members, char split = ',')
        {
            if (members.IsNullOrEmpty())
            {
                return "";
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            var users = new YJ.Platform.Organize().GetAllUsers(members);
            foreach (var user in users)
            {
                sb.Append(user.Name);
                sb.Append(split);
            }
            return sb.ToString().TrimEnd(split);
        }

        /// <summary>
        /// 得到工作组下的人员名称字符串
        /// </summary>
        /// <param name="wg">工作组实体</param>
        /// <param name="split"></param>
        /// <returns></returns>
        public string GetUsersNames(YJ.Data.Model.WorkGroup wg, char split = ',')
        {
            if (wg == null || wg.Members.IsNullOrEmpty())
            {
                return "";
            }
            return GetUsersNames(wg.Members, split);
        }

        /// <summary>
        /// 得到工作组下的人员名称字符串
        /// </summary>
        /// <param name="wgID">工作组ID</param>
        /// <param name="split"></param>
        /// <returns></returns>
        public string GetUsersNames(Guid wgID, char split = ',')
        {
            var wg = Get(wgID);
            return GetUsersNames(wg, split);
        }

        /// <summary>
        /// 得到一个用户所在的工作组
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<Data.Model.WorkGroup> GetAllByUserID(Guid userID)
        {
            var list = GetAll();
            Organize borg = new Organize();
            List<Data.Model.WorkGroup> wgs = new List<Data.Model.WorkGroup>();
            foreach (var wg in list)
            {
                if (borg.GetAllUsers(wg.Members).Find(p => p.ID == userID) != null)
                {
                    wgs.Add(wg);
                }
            }
            return wgs;
        }

        /// <summary>
        /// 得到一个人员所在的工作组名称
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetAllNamesByUserID(Guid userID)
        {
            var wgs = GetAllByUserID(userID);
            StringBuilder sb = new StringBuilder();
            foreach (var wg in wgs)
            {
                sb.Append(wg.Name);
                sb.Append(",");
            }
            return sb.ToString().TrimEnd(',');
        }
    }
}
