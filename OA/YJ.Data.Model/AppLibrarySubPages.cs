using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class AppLibrarySubPages
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// AppLibraryID
        /// </summary>
        [DisplayName("AppLibraryID")]
        public Guid AppLibraryID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [DisplayName("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        [DisplayName("Address")]
        public string Address { get; set; }

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
        /// Note
        /// </summary>
        [DisplayName("Note")]
        public string Note { get; set; }

    }
}
