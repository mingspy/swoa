using System;
using System.Collections.Generic;
using System.Text;

namespace YJ.Platform
{
    public class AppLibrarySubPages
    {
        private static readonly string cachekey = Utility.Keys.CacheKeys.MenuTable.ToString() + "_AppLibrarySubPages";
        private YJ.Data.Interface.IAppLibrarySubPages dataAppLibrarySubPages;
        public AppLibrarySubPages()
        {
            this.dataAppLibrarySubPages = YJ.Data.Factory.Factory.GetAppLibrarySubPages();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.AppLibrarySubPages model)
        {
            return dataAppLibrarySubPages.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.AppLibrarySubPages model)
        {
            return dataAppLibrarySubPages.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.AppLibrarySubPages> GetAll(bool cache=true)
        {
            if (!cache)
            {
                return dataAppLibrarySubPages.GetAll();
            }
            else
            {
                var obj = Cache.IO.Opation.Get(cachekey);
                if (obj == null || !(obj is List<Data.Model.AppLibrarySubPages>))
                {
                    var list = dataAppLibrarySubPages.GetAll();
                    Cache.IO.Opation.Set(cachekey, list);
                    return list;
                }
                else
                {
                    return (List<Data.Model.AppLibrarySubPages>)obj;
                }
            }
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.AppLibrarySubPages Get(Guid id)
        {
            return dataAppLibrarySubPages.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            return dataAppLibrarySubPages.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataAppLibrarySubPages.GetCount();
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        public int DeleteByAppID(Guid id)
        {
            return dataAppLibrarySubPages.DeleteByAppID(id);
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.AppLibrarySubPages> GetAllByAppID(Guid id)
        {
            return dataAppLibrarySubPages.GetAllByAppID(id);
        }

        public void ClearCache()
        {
            Cache.IO.Opation.Remove(cachekey);
        }
    }
}
