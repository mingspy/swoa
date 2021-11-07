using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace WebMvc
{
    public class MyController : Controller
    {
        public static List<string> loginlist = new List<string>();
        /// <summary>
        /// Action执行前判断
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var myAttribute = filterContext.ActionDescriptor.GetCustomAttributes(typeof(MyAttributeAttribute), false);
            bool isCheckLogin = true;
            bool isCheckApp = true;
            bool isCheckUrl = true;
            if (myAttribute.Length == 1)
            {
                MyAttributeAttribute myAttr = (MyAttributeAttribute)myAttribute[0];
                isCheckLogin = myAttr.CheckLogin;
                isCheckApp = myAttr.CheckApp;
                isCheckUrl = myAttr.CheckUrl;
            }
            if (isCheckUrl)
            {
                if (!Common.Tools.CheckReferrer(false))
                {
                    filterContext.Result = Content("地址验证错误");
                    return;
                }
            }
            if (isCheckLogin)
            {
                string msg;
                if (!this.CheckLogin(out msg))
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = Content("{\"loginstatus\":-1, \"url\":\"\"}");
                    }
                    else
                    {
                        string lastURL = System.Web.HttpContext.Current.Request.Url.PathAndQuery.UrlEncode();
                        filterContext.Result = Content(string.Concat("<script>",
                            msg.IsNullOrEmpty() ? "" : string.Format("alert('{0}');", msg),
                            string.Compare(filterContext.Controller.ToString(), "WebMvc.Controllers.HomeController", true) == 0 ? "top.location='" + Url.Content("~/Login") + "'" : "top.lastURL='" + lastURL + "';top.currentWindow=window;top.login();", "</script>"), "text/html");
                    }
                    return;
                }
            }
            if (isCheckApp)
            {
                string appMsg;
                if (!Common.Tools.CheckApp(out appMsg))
                {
                    filterContext.Result = Content("权限验证错误");
                    return;
                }
            }
        }
        
        /// <summary>
        /// 验证登录
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected virtual bool CheckLogin(out string msg)
        {
            return WebMvc.Common.Tools.CheckLogin(out msg)|| YJ.Platform.WeiXin.Organize.CheckLogin();
        }

        /// <summary>
        /// 当前登录用户ID
        /// </summary>
        public static Guid CurrentUserID
        {
            get
            {
                return YJ.Platform.Users.CurrentUserID;
            }
        }

        /// <summary>
        /// 当前用户
        /// </summary>
        public static YJ.Data.Model.Users CurrentUser
        {
            get
            {
                return YJ.Platform.Users.CurrentUser;
            }
        }

        /// <summary>
        /// 当前用户姓名
        /// </summary>
        public static string CurrentUserName
        {
            get
            {
                return YJ.Platform.Users.CurrentUserName;
            }
        }

        /// <summary>
        /// 当前用户部门
        /// </summary>
        public static YJ.Data.Model.Organize CurrentUserDept
        {
            get
            {
                return YJ.Platform.Users.CurrentDept;
            }
        }

        /// <summary>
        /// 当前用户部门ID
        /// </summary>
        public static Guid CurrentUserDeptID
        {
            get
            {
                return YJ.Platform.Users.CurrentDeptID;
            }
        }

        /// <summary>
        /// 当前用户部门名称
        /// </summary>
        public static string CurrentUserDeptName
        {
            get
            {
                return YJ.Platform.Users.CurrentDeptName;
            }
        }

        /// <summary>
        /// 当前用户单位
        /// </summary>
        public static YJ.Data.Model.Organize CurrentUserUnit
        {
            get
            {
                return YJ.Platform.Users.CurrentUnit;
            }
        }

        /// <summary>
        /// 当前用户单位ID
        /// </summary>
        public static Guid CurrentUserUnitID
        {
            get
            {
                return YJ.Platform.Users.CurrentUnitID;
            }
        }

        /// <summary>
        /// 当前用户单位名称
        /// </summary>
        public static string CurrentUserUnitName
        {
            get
            {
                return YJ.Platform.Users.CurrentUnitName;
            }
        }

        /// <summary>
        /// 当前日期时间
        /// </summary>
        public static DateTime CurrentDateTime
        {
            get
            {
                return YJ.Utility.DateTimeNew.Now;
            }
        }
    }
}