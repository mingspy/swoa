using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvc.Areas.AssetManage.Data.Business
{
    public class AmAssetInOut
    {
        private Data.MSSQL.AmAssetInOut dataAmAssetInOut;
        public AmAssetInOut()
        {
            this.dataAmAssetInOut = new Data.MSSQL.AmAssetInOut();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(Data.Model.AmAssetInOut model)
        {
            return dataAmAssetInOut.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(Data.Model.AmAssetInOut model)
        {
            return dataAmAssetInOut.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<Data.Model.AmAssetInOut> GetAll()
        {
            return dataAmAssetInOut.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public Data.Model.AmAssetInOut Get(int id)
        {
            return dataAmAssetInOut.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(int id)
        {
            return dataAmAssetInOut.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataAmAssetInOut.GetCount();
        }
    }
}