using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class DocumentsReadUsers
    {
        /// <summary>
        /// DocumentID
        /// </summary>
        [DisplayName("DocumentID")]
        public Guid DocumentID { get; set; }

        /// <summary>
        /// UserID
        /// </summary>
        [DisplayName("UserID")]
        public Guid UserID { get; set; }

        /// <summary>
        /// IsRead
        /// </summary>
        [DisplayName("IsRead")]
        public int IsRead { get; set; }

    }
}
