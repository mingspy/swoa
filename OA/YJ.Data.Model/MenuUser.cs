using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class MenuUser
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        [DisplayName("菜单ID")]
        public Guid MenuID { get; set; }

        /// <summary>
        /// 可使用的子页面
        /// </summary>
        [DisplayName("可使用的子页面")]
        public Guid SubPageID { get; set; }

        /// <summary>
        /// 使用对象（组织机构ID）
        /// </summary>
        [DisplayName("使用对象（组织机构ID）")]
        public string Organizes { get; set; }

        /// <summary>
        /// Users
        /// </summary>
        [DisplayName("Users")]
        public string Users { get; set; }

        /// <summary>
        /// 可使用的按钮
        /// </summary>
        [DisplayName("可使用的按钮")]
        public string Buttons { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        [DisplayName("参数")]
        public string Params { get; set; }

    }
}
