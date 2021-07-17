using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class ProgramBuilderButtons
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// ProgramID
        /// </summary>
        [DisplayName("ProgramID")]
        public Guid ProgramID { get; set; }

        /// <summary>
        /// 按钮库ID
        /// </summary>
        [DisplayName("按钮库ID")]
        public Guid? ButtonID { get; set; }

        /// <summary>
        /// ButtonName
        /// </summary>
        [DisplayName("ButtonName")]
        public string ButtonName { get; set; }

        /// <summary>
        /// ClientScript
        /// </summary>
        [DisplayName("ClientScript")]
        public string ClientScript { get; set; }

        /// <summary>
        /// Ico
        /// </summary>
        [DisplayName("Ico")]
        public string Ico { get; set; }

        /// <summary>
        /// 显示类型 0工具栏按钮 1普通按钮 2列表按键
        /// </summary>
        [DisplayName("显示类型 0工具栏按钮 1普通按钮 2列表按键")]
        public int ShowType { get; set; }

        /// <summary>
        /// Sort
        /// </summary>
        [DisplayName("Sort")]
        public int Sort { get; set; }

        /// <summary>
        /// IsValidateShow
        /// </summary>
        [DisplayName("IsValidateShow")]
        public int IsValidateShow { get; set; }

    }
}
