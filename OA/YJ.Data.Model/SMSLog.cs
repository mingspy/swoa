using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class SMSLog
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [DisplayName("手机号码")]
        public string MobileNumber { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DisplayName("内容")]
        public string Contents { get; set; }

        /// <summary>
        /// 发送人ID
        /// </summary>
        [DisplayName("发送人ID")]
        public Guid? SendUserID { get; set; }

        /// <summary>
        /// 发送人姓名
        /// </summary>
        [DisplayName("发送人姓名")]
        public string SendUserName { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        [DisplayName("发送时间")]
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DisplayName("状态")]
        public int Status { get; set; }

        /// <summary>
        /// 备注说明
        /// </summary>
        [DisplayName("备注说明")]
        public string Note { get; set; }

    }
}
