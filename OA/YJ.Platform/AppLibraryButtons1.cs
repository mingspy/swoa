using System;
using System.Collections.Generic;
using System.Text;

namespace YJ.Platform
{
    public class AppLibraryButtons1
    {
        private static readonly string cachekey = Utility.Keys.CacheKeys.MenuTable.ToString() + "_AppLibraryButtons1";
        private YJ.Data.Interface.IAppLibraryButtons1 dataAppLibraryButtons1;
        public AppLibraryButtons1()
        {
            this.dataAppLibraryButtons1 = YJ.Data.Factory.Factory.GetAppLibraryButtons1();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.AppLibraryButtons1 model)
        {
            return dataAppLibraryButtons1.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.AppLibraryButtons1 model)
        {
            return dataAppLibraryButtons1.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.AppLibraryButtons1> GetAll(bool cache = true)
        {
            if (!cache)
            {
                return dataAppLibraryButtons1.GetAll();
            }
            else
            {
                var obj = Cache.IO.Opation.Get(cachekey);
                if (obj == null || !(obj is List<Data.Model.AppLibraryButtons1>))
                {
                    var list = dataAppLibraryButtons1.GetAll();
                    Cache.IO.Opation.Set(cachekey, list);
                    return list;
                }
                else
                {
                    return (List<Data.Model.AppLibraryButtons1>)obj;
                }
            }
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.AppLibraryButtons1 Get(Guid id, bool cache = false)
        {
            if (!cache)
            {
                return dataAppLibraryButtons1.Get(id);
            }
            else
            {
                return GetAll().Find(p => p.ID == id);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            return dataAppLibraryButtons1.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataAppLibraryButtons1.GetCount();
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        public int DeleteByAppID(Guid id)
        {
            return dataAppLibraryButtons1.DeleteByAppID(id);
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.AppLibraryButtons1> GetAllByAppID(Guid id)
        {
            return dataAppLibraryButtons1.GetAllByAppID(id);
        }

        public void ClearCache()
        {
            Cache.IO.Opation.Remove(cachekey);
        }
    }
}
