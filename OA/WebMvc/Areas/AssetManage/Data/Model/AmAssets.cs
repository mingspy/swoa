using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebMvc.Areas.AssetManage.Data.Model
{
    [Serializable]
    public class AmAssets
    {
        /// <summary>
		/// ID
		/// </summary>
		[DisplayName("ID")]
        public int ID { get; set; }

        /// <summary>
        /// Module
        /// </summary>
        [DisplayName("Module")]
        public int? Module { get; set; }

        /// <summary>
        /// 办公物品 ： 类别    设备仪器：经手人
        /// </summary>
        [DisplayName("办公物品 ： 类别    设备仪器：经手人")]
        public string TypeId { get; set; }

        /// <summary>
        /// 资产名称
        /// </summary>
        [DisplayName("资产名称")]
        public string Name { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        [DisplayName("规格型号")]
        public string Specs { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DisplayName("数量")]
        public int? Num { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        [DisplayName("计量单位")]
        public string MeasureUnit { get; set; }

        /// <summary>
        /// 价值
        /// </summary>
        [DisplayName("价值")]
        public decimal? Money { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        [DisplayName("品牌")]
        public string Brand { get; set; }

        /// <summary>
        /// 取得时间
        /// </summary>
        [DisplayName("取得时间")]
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// 是否为固定资产
        /// </summary>
        [DisplayName("是否为固定资产")]
        public int? IsFixedAsset { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DisplayName("状态")]
        public int? Status { get; set; }

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

        /// <summary>
        /// 附件
        /// </summary>
        [DisplayName("附件")]
        public string Files { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 固定资产编号
        /// </summary>
        [DisplayName("固定资产编号")]
        public string FixedAssetCode { get; set; }

        /// <summary>
        /// 计量监测单位
        /// </summary>
        [DisplayName("计量监测单位")]
        public string AppraisalUnit { get; set; }

        /// <summary>
        /// 仪器设备：计量类型
        /// </summary>
        [DisplayName("仪器设备：计量类型")]
        public string MeasureType { get; set; }

        /// <summary>
        /// 仪器设备：仪器设备编号
        /// </summary>
        [DisplayName("仪器设备：仪器设备编号")]
        public string Code { get; set; }

    }

}