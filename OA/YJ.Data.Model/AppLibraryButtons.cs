using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class AppLibraryButtons
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 按钮名称
        /// </summary>
        [DisplayName("按钮名称")]
        public string Name { get; set; }

        /// <summary>
        /// 按钮事件
        /// </summary>
        [DisplayName("按钮事件")]
        public string Events { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [DisplayName("图标")]
        public string Ico { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [DisplayName("显示顺序")]
        public int Sort { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [DisplayName("说明")]
        public string Note { get; set; }

    }
}
