using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class HastenLog
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 其它参数
        /// </summary>
        [DisplayName("其它参数")]
        public string OthersParams { get; set; }

        /// <summary>
        /// 被催办人员
        /// </summary>
        [DisplayName("被催办人员")]
        public string Users { get; set; }

        /// <summary>
        /// 催办方式
        /// </summary>
        [DisplayName("催办方式")]
        public string Types { get; set; }

        /// <summary>
        /// 催办内容
        /// </summary>
        [DisplayName("催办内容")]
        public string Contents { get; set; }

        /// <summary>
        /// 发送人员
        /// </summary>
        [DisplayName("发送人员")]
        public Guid SendUser { get; set; }

        /// <summary>
        /// 发送人员姓名
        /// </summary>
        [DisplayName("发送人员姓名")]
        public string SendUserName { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        [DisplayName("发送时间")]
        public DateTime SendTime { get; set; }

    }
}
