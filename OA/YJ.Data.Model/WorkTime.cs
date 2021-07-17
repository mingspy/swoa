using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class WorkTime
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        [DisplayName("年份")]
        public int Year { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [DisplayName("开始时间")]
        public DateTime Date1 { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [DisplayName("结束时间")]
        public DateTime Date2 { get; set; }

        /// <summary>
        /// 上午上班时间
        /// </summary>
        [DisplayName("上午上班时间")]
        public string AmTime1 { get; set; }

        /// <summary>
        /// 上午下班时间
        /// </summary>
        [DisplayName("上午下班时间")]
        public string AmTime2 { get; set; }

        /// <summary>
        /// 下午上班时间
        /// </summary>
        [DisplayName("下午上班时间")]
        public string PmTime1 { get; set; }

        /// <summary>
        /// 下午下班时间
        /// </summary>
        [DisplayName("下午下班时间")]
        public string PmTime2 { get; set; }

    }
}
