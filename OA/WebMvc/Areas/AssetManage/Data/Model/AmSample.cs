using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebMvc.Areas.AssetManage.Data.Model
{
    [Serializable]
    public class AmSample
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public int ID { get; set; }

        /// <summary>
        /// 报告编号
        /// </summary>
        [DisplayName("报告编号")]
        public string bgbh { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        [DisplayName("任务类型")]
        public string rwlx { get; set; }

        /// <summary>
        /// 样品编号
        /// </summary>
        [DisplayName("样品编号")]
        public string cydh { get; set; }

        /// <summary>
        /// 被抽样单位名称
        /// </summary>
        [DisplayName("被抽样单位名称")]
        public string sjqy_mc { get; set; }

        /// <summary>
        /// 样品名称
        /// </summary>
        [DisplayName("样品名称")]
        public string ypmc { get; set; }

        /// <summary>
        /// 样品规格型号
        /// </summary>
        [DisplayName("样品规格型号")]
        public string yp_ggxh { get; set; }

        /// <summary>
        /// 样品数量
        /// </summary>
        [DisplayName("样品数量")]
        public string yp_sl { get; set; }

        /// <summary>
        /// 到样日期
        /// </summary>
        [DisplayName("到样日期")]
        public DateTime? yp_ddrq { get; set; }

        /// <summary>
        /// wtdw
        /// </summary>
        [DisplayName("wtdw")]
        public string wtdw { get; set; }

        /// <summary>
        /// scdw
        /// </summary>
        [DisplayName("scdw")]
        public string scdw { get; set; }

        /// <summary>
        /// yp_bzq
        /// </summary>
        [DisplayName("yp_bzq")]
        public string yp_bzq { get; set; }

        /// <summary>
        /// 结果判定
        /// </summary>
        [DisplayName("结果判定")]
        public string panding { get; set; }

        /// <summary>
        /// pz_time
        /// </summary>
        [DisplayName("pz_time")]
        public DateTime? pz_time { get; set; }

        /// <summary>
        /// pz_bz
        /// </summary>
        [DisplayName("pz_bz")]
        public int? pz_bz { get; set; }

        /// <summary>
        /// 样品生产日期
        /// </summary>
        [DisplayName("样品生产日期")]
        public string yp_scrq { get; set; }

        /// <summary>
        /// 样品状态
        /// </summary>
        [DisplayName("样品状态")]
        public string ypzt { get; set; }

        /// <summary>
        /// 复检样数量
        /// </summary>
        [DisplayName("复检样数量")]
        public string yp_byl { get; set; }

        /// <summary>
        /// 存储位置
        /// </summary>
        [DisplayName("存储位置")]
        public string Address { get; set; }

        /// <summary>
        /// 处理方式
        /// </summary>
        [DisplayName("处理方式")]
        public string DisposeWay { get; set; }

        /// <summary>
        /// 处理结果：0已销毁  1未销毁  2已调出 3已报批
        /// </summary>
        [DisplayName("处理结果：0已销毁  1未销毁  2已调出 3已报批")]
        public int? DisposeResult { get; set; }
        /// <summary>
        /// 处理原因
        /// </summary>
        [DisplayName("处理原因")]
        public string DisposeCause { get; set; }
        /// <summary>
        /// 处理意见
        /// </summary>
        [DisplayName("处理意见")]
        public string DisposeOpinion { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        [DisplayName("入库时间")]
        public DateTime? InDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 组编号
        /// </summary>
        [DisplayName("组编号")]
        public string GroupCode { get; set; }

        /// <summary>
        /// 检查类型 0 备检 1复检
        /// </summary>
        [DisplayName("检查类型 0 备检 1复检")]
        public int? Type { get; set; }

        /// <summary>
        /// 扩展字段
        /// </summary>
        [DisplayName("扩展字段")]
        public string ExtendField1 { get; set; }

        /// <summary>
        /// 扩展字段2
        /// </summary>
        [DisplayName("扩展字段2")]
        public int? ExtendField2 { get; set; }

        /// <summary>
        /// 扩展字段2
        /// </summary>
        [DisplayName("到期日期")]
        public DateTime? expire { get; set; }

        /// <summary>
        /// 扩展字段2
        /// </summary>
        [DisplayName("剩余数量")]
        public float? rest { get; set; }

        /// <summary>
        /// 扩展字段2
        /// </summary>
        [DisplayName("复检样剩余数量")]
        public float? byl_rest { get; set; }

        /// <summary>
        /// 扩展字段2
        /// </summary>
        [DisplayName("单位")]
        public string unit{ get; set; }

    }
}