using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebMvc.Areas.AssetManage.Data.Model
{
    [Serializable]
    public class AmAssetType
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public int ID { get; set; }

        /// <summary>
        /// 上级id
        /// </summary>
        [DisplayName("上级id")]
        public int ParentAssetTypeId { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        [DisplayName("编号")]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        public string Name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [DisplayName("创建人")]
        public string CreateUId { get; set; }

    }
}