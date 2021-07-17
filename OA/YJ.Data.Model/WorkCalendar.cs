using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class WorkCalendar
    {
        /// <summary>
        /// WorkDate
        /// </summary>
        [DisplayName("WorkDate")]
        public DateTime WorkDate { get; set; }

    }
}
