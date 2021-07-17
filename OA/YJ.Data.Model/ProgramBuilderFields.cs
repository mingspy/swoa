using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class ProgramBuilderFields
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
        /// 显示标题
        /// </summary>
        [DisplayName("显示标题")]
        public string ShowTitle { get; set; }

        /// <summary>
        /// 对齐方式
        /// </summary>
        [DisplayName("对齐方式")]
        public string Align { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        [DisplayName("宽度")]
        public string Width { get; set; }

        /// <summary>
        /// 0直接输出 1序号 2日期时间 3数字 4数据字典ID显示标题  5组织机构id显示为名称 6自定义 7按钮列
        /// </summary>
        [DisplayName("0直接输出 1序号 2日期时间 3数字 4数据字典ID显示标题  5组织机构id显示为名称 6自定义 7按钮列")]
        public int ShowType { get; set; }

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
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int Sort { get; set; }

    }
}
