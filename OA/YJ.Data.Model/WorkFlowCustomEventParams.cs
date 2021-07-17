using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YJ.Data.Model
{
    /// <summary>
    /// 调用流程事件时的参数实体
    /// </summary>
    [Serializable]
    public struct WorkFlowCustomEventParams
    {
        /// <summary>
        /// 流程ID
        /// </summary>
        public Guid FlowID { get; set; }
        /// <summary>
        /// 步骤ID
        /// </summary>
        public Guid StepID { get; set; }
        /// <summary>
        /// 组ID
        /// </summary>
        public Guid GroupID { get; set; }
        /// <summary>
        /// 任务ID
        /// </summary>
        public Guid TaskID { get; set; }
        /// <summary>
        /// 实例ID
        /// </summary>
        public string InstanceID { get; set; }
        /// <summary>
        /// 其它参数
        /// </summary>
        public object Other { get; set; }
    }
}
