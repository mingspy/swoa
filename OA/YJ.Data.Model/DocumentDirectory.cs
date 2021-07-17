using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class DocumentDirectory
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// ParentID
        /// </summary>
        [DisplayName("ParentID")]
        public Guid ParentID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [DisplayName("Name")]
        public string Name { get; set; }

        /// <summary>
        /// 阅读人员
        /// </summary>
        [DisplayName("阅读人员")]
        public string ReadUsers { get; set; }

        /// <summary>
        /// 管理人员
        /// </summary>
        [DisplayName("管理人员")]
        public string ManageUsers { get; set; }

        /// <summary>
        /// 发布人员
        /// </summary>
        [DisplayName("发布人员")]
        public string PublishUsers { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int Sort { get; set; }

    }
}
