using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class HomeItems
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 模块类型 0上方信息提示模块 1左边模块 2右边模块
        /// </summary>
        [DisplayName("模块类型 0上方信息提示模块 1左边模块 2右边模块")]
        public int Type { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        public string Name { get; set; }

        /// <summary>
        /// 显示标题
        /// </summary>
        [DisplayName("显示标题")]
        public string Title { get; set; }

        /// <summary>
        /// 数据来源 0:sql,1:c#方法 2:url
        /// </summary>
        [DisplayName("数据来源 0:sql,1:c#方法 2:url")]
        public int DataSourceType { get; set; }

        /// <summary>
        /// DataSource
        /// </summary>
        [DisplayName("DataSource")]
        public string DataSource { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [DisplayName("图标")]
        public string Ico { get; set; }

        /// <summary>
        /// 背景色
        /// </summary>
        [DisplayName("背景色")]
        public string BgColor { get; set; }

        /// <summary>
        /// 字体色
        /// </summary>
        [DisplayName("字体色")]
        public string Color { get; set; }

        /// <summary>
        /// 数据连接ID
        /// </summary>
        [DisplayName("数据连接ID")]
        public Guid? DBConnID { get; set; }

        /// <summary>
        /// 连接地址
        /// </summary>
        [DisplayName("连接地址")]
        public string LinkURL { get; set; }

        /// <summary>
        /// 使用对象
        /// </summary>
        [DisplayName("使用对象")]
        public string UseOrganizes { get; set; }

        /// <summary>
        /// 使用人员
        /// </summary>
        [DisplayName("使用人员")]
        public string UseUsers { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int? Sort { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Note { get; set; }

    }
}
