using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class WorkFlowArchives
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 流程ID
        /// </summary>
        [DisplayName("流程ID")]
        public Guid FlowID { get; set; }

        /// <summary>
        /// 步骤ID
        /// </summary>
        [DisplayName("步骤ID")]
        public Guid StepID { get; set; }

        /// <summary>
        /// 流程名称
        /// </summary>
        [DisplayName("流程名称")]
        public string FlowName { get; set; }

        /// <summary>
        /// 步骤名称
        /// </summary>
        [DisplayName("步骤名称")]
        public string StepName { get; set; }

        /// <summary>
        /// 任务ID
        /// </summary>
        [DisplayName("任务ID")]
        public Guid TaskID { get; set; }

        /// <summary>
        /// 组
        /// </summary>
        [DisplayName("组")]
        public Guid GroupID { get; set; }

        /// <summary>
        /// 实例ID
        /// </summary>
        [DisplayName("实例ID")]
        public string InstanceID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [DisplayName("标题")]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DisplayName("内容")]
        public string Contents { get; set; }

        /// <summary>
        /// 意见内容
        /// </summary>
        [DisplayName("意见内容")]
        public string Comments { get; set; }

        /// <summary>
        /// 写入时间
        /// </summary>
        [DisplayName("写入时间")]
        public DateTime WriteTime { get; set; }

    }
}
