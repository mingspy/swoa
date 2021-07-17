using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebMvc.Areas.AssetManage.Data.Model
{
    [Serializable]
    public class AmAssetInOut
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public int ID { get; set; }

        /// <summary>
        /// 资产表id
        /// </summary>
        [DisplayName("资产表id")]
        public int? AmAssetsId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [DisplayName("类型")]
        public string Type { get; set; }

        /// <summary>
        /// 责任人
        /// </summary>
        [DisplayName("责任人")]
        public string UseUId { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [DisplayName("时间")]
        public DateTime? UseDate { get; set; }

        /// <summary>
        /// 存放位置
        /// </summary>
        [DisplayName("存放位置")]
        public string Address { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 附加字段
        /// </summary>
        [DisplayName("附加字段")]
        public string ExtendField1 { get; set; }

        /// <summary>
        /// 附加字段
        /// </summary>
        [DisplayName("附加字段")]
        public string ExtendField2 { get; set; }

    }
}