using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvc.Areas.AssetManage.Data.Business
{
    public class AmAssets
    {
        private Data.MSSQL.AmAssets dataAmAssets;
        public AmAssets()
        {
            this.dataAmAssets = new Data.MSSQL.AmAssets();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(Data.Model.AmAssets model)
        {
            return dataAmAssets.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(Data.Model.AmAssets model)
        {
            return dataAmAssets.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<Data.Model.AmAssets> GetAll()
        {
            return dataAmAssets.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public Data.Model.AmAssets Get(int id)
        {
            return dataAmAssets.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(int id)
        {
            return dataAmAssets.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataAmAssets.GetCount();
        }
    }
}