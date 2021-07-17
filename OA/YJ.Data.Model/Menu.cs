using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class Menu
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
        /// AppLibraryID
        /// </summary>
        [DisplayName("AppLibraryID")]
        public Guid? AppLibraryID { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [DisplayName("Title")]
        public string Title { get; set; }

        /// <summary>
        /// Params
        /// </summary>
        [DisplayName("Params")]
        public string Params { get; set; }

        /// <summary>
        /// Ico
        /// </summary>
        [DisplayName("Ico")]
        public string Ico { get; set; }

        /// <summary>
        /// Sort
        /// </summary>
        [DisplayName("Sort")]
        public int Sort { get; set; }

        /// <summary>
        /// 图标颜色
        /// </summary>
        [DisplayName("IcoColor")]
        public string IcoColor { get; set; }
    }
}
