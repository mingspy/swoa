using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class WorkGroup
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 工作组名称
        /// </summary>
        [DisplayName("工作组名称")]
        public string Name { get; set; }

        /// <summary>
        /// 工作组成员
        /// </summary>
        [DisplayName("工作组成员")]
        public string Members { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Note { get; set; }

        /// <summary>
        /// 数字ID，用于微信
        /// </summary>
        [DisplayName("数字ID，用于微信")]
        public int IntID { get; set; }
    }
}
