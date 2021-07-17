using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
    [Serializable]
    public class LoginBlacklist
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [DisplayName("IP地址")]
        public string IPaddress { get; set; }

        /// <summary>
        /// 登录使用的账户
        /// </summary>
        [DisplayName("登录使用的账户")]
        public string Account { get; set; }

        /// <summary>
        /// 封禁时间
        /// </summary>
        [DisplayName("封禁时间")]
        public DateTime BlockTime { get; set; }

    }
}
