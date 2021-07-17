using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class WorkFlowComment
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 使用者
        /// </summary>
        [DisplayName("使用者")]
        public string MemberID { get; set; }

        /// <summary>
        /// 意见
        /// </summary>
        [DisplayName("意见")]
        public string Comment { get; set; }

        /// <summary>
        /// 类型 0管理员添加 1用户添加
        /// </summary>
        [DisplayName("类型 0管理员添加 1用户添加")]
        public int Type { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int Sort { get; set; }

    }
}
