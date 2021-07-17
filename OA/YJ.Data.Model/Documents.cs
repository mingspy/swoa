using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class Documents
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// DirectoryID
        /// </summary>
        [DisplayName("DirectoryID")]
        public Guid DirectoryID { get; set; }

        /// <summary>
        /// DirectoryName
        /// </summary>
        [DisplayName("DirectoryName")]
        public string DirectoryName { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [DisplayName("Title")]
        public string Title { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        [DisplayName("来源")]
        public string Source { get; set; }

        /// <summary>
        /// Contents
        /// </summary>
        [DisplayName("Contents")]
        public string Contents { get; set; }

        /// <summary>
        /// 相关附件
        /// </summary>
        [DisplayName("相关附件")]
        public string Files { get; set; }

        /// <summary>
        /// WriteTime
        /// </summary>
        [DisplayName("WriteTime")]
        public DateTime WriteTime { get; set; }

        /// <summary>
        /// WriteUserID
        /// </summary>
        [DisplayName("WriteUserID")]
        public Guid WriteUserID { get; set; }

        /// <summary>
        /// WriteUserName
        /// </summary>
        [DisplayName("WriteUserName")]
        public string WriteUserName { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [DisplayName("最后修改时间")]
        public DateTime? EditTime { get; set; }

        /// <summary>
        /// 修改用户ID
        /// </summary>
        [DisplayName("修改用户ID")]
        public Guid? EditUserID { get; set; }

        /// <summary>
        /// 修改人姓名
        /// </summary>
        [DisplayName("修改人姓名")]
        public string EditUserName { get; set; }

        /// <summary>
        /// 阅读人员
        /// </summary>
        [DisplayName("阅读人员")]
        public string ReadUsers { get; set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        [DisplayName("阅读次数")]
        public int ReadCount { get; set; }

    }
}
