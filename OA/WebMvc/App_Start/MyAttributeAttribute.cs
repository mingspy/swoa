using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvc
{
    public class MyAttributeAttribute : Attribute
    {
        private bool checkLogin = true;
        private bool checkApp = true;
        private bool checkUrl = true;
        /// <summary>
        /// 是否验证登录
        /// </summary>
        public bool CheckLogin
        {
            get
            {
                return this.checkLogin;
            }
            set
            {
                this.checkLogin = value;
            }
        }
        /// <summary>
        /// 是否验证权限
        /// </summary>
        public bool CheckApp
        {
            get
            {
                return this.checkApp;
            }
            set
            {
                this.checkApp = value;
            }
        }
        /// <summary>
        /// 是否验证URL
        /// </summary>
        public bool CheckUrl
        {
            get
            {
                return this.checkUrl;
            }
            set
            {
                this.checkUrl = value;
            }
        }
    }
}