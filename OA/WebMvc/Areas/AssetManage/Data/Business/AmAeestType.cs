using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvc.Areas.AssetManage.Data.Business
{
    public class AmAssetType
    {
        private WebMvc.Areas.AssetManage.Data.MSSQL.AmAssetType dataAmAssetType;
        public AmAssetType()
        {
            this.dataAmAssetType = new WebMvc.Areas.AssetManage.Data.MSSQL.AmAssetType();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(WebMvc.Areas.AssetManage.Data.Model.AmAssetType model)
        {
            return dataAmAssetType.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(WebMvc.Areas.AssetManage.Data.Model.AmAssetType model)
        {
            return dataAmAssetType.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<WebMvc.Areas.AssetManage.Data.Model.AmAssetType> GetAll()
        {
            return dataAmAssetType.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public WebMvc.Areas.AssetManage.Data.Model.AmAssetType Get(int id)
        {
            return dataAmAssetType.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(int id)
        {
            return dataAmAssetType.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataAmAssetType.GetCount();
        }
    }
}