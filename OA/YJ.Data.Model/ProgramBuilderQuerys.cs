using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class ProgramBuilderQuerys
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// ProgramID
        /// </summary>
        [DisplayName("ProgramID")]
        public Guid ProgramID { get; set; }

        /// <summary>
        /// 字段
        /// </summary>
        [DisplayName("字段")]
        public string Field { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [DisplayName("显示名称")]
        public string ShowTitle { get; set; }

        /// <summary>
        /// 操作符
        /// </summary>
        [DisplayName("操作符")]
        public string Operators { get; set; }

        /// <summary>
        /// 控件名称
        /// </summary>
        [DisplayName("控件名称")]
        public string ControlName { get; set; }

        /// <summary>
        /// 输入类型 1文本 2日期 3日期范围 4日期时间 5日期时间范围 6下拉选择 7组织机构 8数据字典
        /// </summary>
        [DisplayName("输入类型 1文本 2日期 3日期范围 4日期时间 5日期时间范围 6下拉选择 7组织机构 8数据字典")]
        public int InputType { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        [DisplayName("宽度")]
        public string Width { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [DisplayName("显示顺序")]
        public int Sort { get; set; }

        /// <summary>
        /// 数据来源 1.字符串表达式 2.数据字典 3.SQL
        /// </summary>
        [DisplayName("数据来源 1.字符串表达式 2.数据字典 3.SQL")]
        public int? DataSource { get; set; }

        /// <summary>
        /// DataSourceString
        /// </summary>
        [DisplayName("DataSourceString")]
        public string DataSourceString { get; set; }

        /// <summary>
        /// 数据连接ID
        /// </summary>
        [DisplayName("数据连接ID")]
        public string DataLinkID { get; set; }

        /// <summary>
        /// 组织机构查询时是否将选择转换为人员
        /// </summary>
        [DisplayName("组织机构查询时是否将选择转换为人员")]
        public int? IsQueryUsers { get; set; }

        /// <summary>
        /// 查询时的值
        /// </summary>
        [DisplayName("查询时的值")]
        public string Value { get; set; }

    }
}
