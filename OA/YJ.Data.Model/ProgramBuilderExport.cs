using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class ProgramBuilderExport
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
        /// 显示列名
        /// </summary>
        [DisplayName("显示列名")]
        public string ShowTitle { get; set; }

        /// <summary>
        /// 对齐方式 0左对齐 1居中 2右对齐
        /// </summary>
        [DisplayName("对齐方式 0左对齐 1居中 2右对齐")]
        public int Align { get; set; }

        /// <summary>
        /// 列宽度
        /// </summary>
        [DisplayName("列宽度")]
        public int? Width { get; set; }

        /// <summary>
        /// 显示类型 0直接输出 1序号 2日期时间 3数字 4数据字典ID显示标题  5组织机构id显示为名称 6自定义
        /// </summary>
        [DisplayName("显示类型 0直接输出 1序号 2日期时间 3数字 4数据字典ID显示标题  5组织机构id显示为名称 6自定义")]
        public int? ShowType { get; set; }

        /// <summary>
        /// 单元格类型：0常规 1文本 2数字 3日期时间
        /// </summary>
        [DisplayName("单元格类型：0常规 1文本 2数字 3日期时间")]
        public int? DataType { get; set; }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        [DisplayName("格式化字符串")]
        public string ShowFormat { get; set; }

        /// <summary>
        /// 自定义字符串
        /// </summary>
        [DisplayName("自定义字符串")]
        public string CustomString { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [DisplayName("显示顺序")]
        public int Sort { get; set; }

    }
}
