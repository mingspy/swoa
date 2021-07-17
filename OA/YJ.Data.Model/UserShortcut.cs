using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class UserShortcut
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// MenuID
        /// </summary>
        [DisplayName("MenuID")]
        public Guid MenuID { get; set; }

        /// <summary>
        /// UserID
        /// </summary>
        [DisplayName("UserID")]
        public Guid UserID { get; set; }

        /// <summary>
        /// Sort
        /// </summary>
        [DisplayName("Sort")]
        public int Sort { get; set; }

    }
}
