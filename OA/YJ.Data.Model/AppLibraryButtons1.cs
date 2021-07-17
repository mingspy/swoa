using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class AppLibraryButtons1
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
        /// 按钮库ID
        /// </summary>
        [DisplayName("按钮库ID")]
        public Guid? ButtonID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        public string Name { get; set; }

        /// <summary>
        /// 执行脚本
        /// </summary>
        [DisplayName("执行脚本")]
        public string Events { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [DisplayName("图标")]
        public string Ico { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 类型0属于主页面1属于子页面
        /// </summary>
        [DisplayName("类型0属于主页面1属于子页面")]
        public int Type { get; set; }

        /// <summary>
        /// 显示类型 0工具栏按钮 1普通按钮 2列表按键
        /// </summary>
        [DisplayName("显示类型 0工具栏按钮 1普通按钮 2列表按键")]
        public int ShowType { get; set; }

        /// <summary>
        /// 是否验证权限，要验证则要在权限中授权了才能显示
        /// </summary>
        [DisplayName("是否验证权限，要验证则要在权限中授权了才能显示")]
        public int IsValidateShow { get; set; }

    }
}
