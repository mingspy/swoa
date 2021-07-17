using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class ShortMessage
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 消息标题
        /// </summary>
        [DisplayName("消息标题")]
        public string Title { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        [DisplayName("消息内容")]
        public string Contents { get; set; }

        /// <summary>
        /// 发送人
        /// </summary>
        [DisplayName("发送人")]
        public Guid SendUserID { get; set; }

        /// <summary>
        /// 发送人姓名
        /// </summary>
        [DisplayName("发送人姓名")]
        public string SendUserName { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        [DisplayName("接收人")]
        public Guid ReceiveUserID { get; set; }

        /// <summary>
        /// 接收人姓名
        /// </summary>
        [DisplayName("接收人姓名")]
        public string ReceiveUserName { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        [DisplayName("发送时间")]
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 消息连接地址
        /// </summary>
        [DisplayName("消息连接地址")]
        public string LinkUrl { get; set; }

        /// <summary>
        /// 消息对应的ID，如:流程任务ID
        /// </summary>
        [DisplayName("消息对应的ID，如:流程任务ID")]
        public string LinkID { get; set; }

        /// <summary>
        /// 0:用户发送消息 1：系统消息
        /// </summary>
        [DisplayName("0:用户发送消息 1：系统消息")]
        public int Type { get; set; }

        /// <summary>
        /// 相关附件
        /// </summary>
        [DisplayName("相关附件")]
        public string Files { get; set; }

        /// <summary>
        /// 消息状态 0未读 1已读
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 消息组
        /// </summary>
        public string GroupID { get; set; }
    }
}
