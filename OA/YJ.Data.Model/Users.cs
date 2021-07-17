using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class Users
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [DisplayName("姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 帐号
        /// </summary>
        [DisplayName("帐号")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("密码")]
        public string Password { get; set; }

        /// <summary>
        /// 状态 0 正常 1 冻结
        /// </summary>
        [DisplayName("状态 0 正常 1 冻结")]
        public int Status { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Note { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [DisplayName("手机")]
        public string Mobile { get; set; }

        /// <summary>
        /// 办公电话
        /// </summary>
        [DisplayName("办公电话")]
        public string Tel { get; set; }

        /// <summary>
        /// 其它联系方式
        /// </summary>
        [DisplayName("其它联系方式")]
        public string OtherTel { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        [DisplayName("传真")]
        public string Fax { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [DisplayName("邮箱")]
        public string Email { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        [DisplayName("QQ")]
        public string QQ { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [DisplayName("头像")]
        public string HeadImg { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        [DisplayName("微信号")]
        public string WeiXin { get; set; }

        /// <summary>
        /// 性别 0男 1女
        /// </summary>
        [DisplayName("性别 0男 1女")]
        public int? Sex { get; set; }

    }
}
