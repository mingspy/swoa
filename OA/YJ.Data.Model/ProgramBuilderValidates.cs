using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class ProgramBuilderValidates
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
        /// 表名
        /// </summary>
        [DisplayName("表名")]
        public string TableName { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        [DisplayName("字段名")]
        public string FieldName { get; set; }

        /// <summary>
        /// 字段说明
        /// </summary>
        [DisplayName("字段说明")]
        public string FieldNote { get; set; }

        /// <summary>
        /// 验证类型 0不检查 1允许为空,非空时检查 2不允许为空,并检查
        /// </summary>
        [DisplayName("验证类型 0不检查 1允许为空,非空时检查 2不允许为空,并检查")]
        public int Validate { get; set; }

    }
}
