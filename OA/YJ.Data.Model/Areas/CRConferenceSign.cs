using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model.Areas
{
    [Serializable]
    public class CRConferenceSign
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public int ID { get; set; }

        /// <summary>
        /// CRMeetingID
        /// </summary>
        [DisplayName("CRMeetingID")]
        public string CRMeetingID { get; set; }

        /// <summary>
        /// 签到人员ID
        /// </summary>
        [DisplayName("签到人员ID")]
        public string UserID { get; set; }

        /// <summary>
        /// 签到人员
        /// </summary>
        [DisplayName("签到人员")]
        public string UserName { get; set; }

        /// <summary>
        /// 签到时间
        /// </summary>
        [DisplayName("签到时间")]
        public DateTime? SignDate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime? CreationTime { get; set; }

        /// <summary>
        /// 备注,说明
        /// </summary>
        [DisplayName("备注,说明")]
        public string Note { get; set; }

    }
}
