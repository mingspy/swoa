using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace YJ.Platform
{
    public class UserShortcut
    {
        private YJ.Data.Interface.IUserShortcut dataUserShortcut;
        private string cacheKey = Utility.Keys.CacheKeys.UserShortcut.ToString();
        public UserShortcut()
        {
            this.dataUserShortcut = YJ.Data.Factory.Factory.GetUserShortcut();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.UserShortcut model)
        {
            return dataUserShortcut.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.UserShortcut model)
        {
            return dataUserShortcut.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.UserShortcut> GetAll(bool fromCache = false)
        {
            if (!fromCache)
            {
                return dataUserShortcut.GetAll();
            }
            else
            {
                var obj = Cache.IO.Opation.Get(cacheKey);
                if (obj != null && (obj is List<YJ.Data.Model.UserShortcut>))
                {
                    return (List<YJ.Data.Model.UserShortcut>)obj;
                }
                else
                {
                    var all = dataUserShortcut.GetAll();
                    Cache.IO.Opation.Set(cacheKey, all);
                    return all;
                }
            }
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.UserShortcut Get(Guid id)
        {
            return dataUserShortcut.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            return dataUserShortcut.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataUserShortcut.GetCount();
        }

        /// <summary>
        /// 删除一个人员记录
        /// </summary>
        public int DeleteByUserID(Guid userID)
        {
            return dataUserShortcut.DeleteByUserID(userID);
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.UserShortcut> GetAllByUserID(Guid userID, bool fromCache = false)
        {
            if (!fromCache)
            {
                return dataUserShortcut.GetAllByUserID(userID);
            }
            else
            {
                var all = GetAll(true);
                return all.FindAll(p => p.UserID == userID).OrderBy(p => p.Sort).ToList();
            }
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        public System.Data.DataTable GetDataTableByUserID(Guid userID)
        {
            return dataUserShortcut.GetDataTableByUserID(userID);
        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        public void ClearCache()
        {
            Cache.IO.Opation.Remove(cacheKey);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        public int DeleteByMenuID(Guid menuID)
        {
            return dataUserShortcut.DeleteByMenuID(menuID);
        }
    }
}
