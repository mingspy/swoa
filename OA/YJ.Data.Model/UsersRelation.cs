using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class UsersRelation
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        [DisplayName("人员ID")]
        public Guid UserID { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        [DisplayName("组织机构ID")]
        public Guid OrganizeID { get; set; }

        /// <summary>
        /// 是否主要岗位/部门
        /// </summary>
        [DisplayName("是否主要岗位/部门")]
        public int IsMain { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int Sort { get; set; }

    }
}
