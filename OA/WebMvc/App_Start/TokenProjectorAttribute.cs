using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Security;

namespace WebMvc.App_Start
{
    public class TokenProjectorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //从http请求的头里面获取身份验证信息，验证是否是请求发起方的ticket
            var authorization = actionContext.Request.Headers.Authorization;
            if ((authorization != null) && (authorization.Scheme != null))
            {
                //解密用户ticket,并校验用户名密码是否匹配
                object obj = YJ.Cache.IO.Opation.Get(authorization.Scheme.ToString());
                if (obj == null)
                {
                    var response = new HttpResponseMessage()
                    {
                        Content = new StringContent("{\"status\":110,\"msg\":\"非法的AppKey参数,请从新登录请求AppKey\"}", Encoding.UTF8, "application/json")
                    };
                    //在这里为了不继续走流程，要throw出来，才会立马返回到客户端
                    throw new HttpResponseException(response);
                }
            }
            else
            {
                //……
                var response = new HttpResponseMessage()
                {
                    Content = new StringContent("{\"status\":109,\"msg\":\"缺少AppKey参数接口\"}", Encoding.UTF8, "application/json")
                };
                //在这里为了不继续走流程，要throw出来，才会立马返回到客户端
                throw new HttpResponseException(response);
            }
            base.OnActionExecuting(actionContext);
        }
        //校验用户名密码（正式环境中应该是数据库校验）
        private bool ValidateTicket(string encryptTicket)
        {
            //解密Ticket
            var strTicket = FormsAuthentication.Decrypt(encryptTicket).UserData;

            //从Ticket里面获取用户名和密码
            var index = strTicket.IndexOf("&");
            string strUser = strTicket.Substring(0, index);
            string strPwd = strTicket.Substring(index + 1);

            if (strUser == "admin" && strPwd == "123456")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 参考方法
        /// </summary>
        /// <param name="actionContext"></param>
        public  void OnActionExecuting2(HttpActionContext actionContext)
        {
            try
            {
                var cookie = actionContext.Request.Headers.GetCookies();
                if (cookie == null || cookie.Count < 1)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    return;
                }

                FormsAuthenticationTicket ticket = null;

                foreach (var perCookie in cookie[0].Cookies)
                {
                    if (perCookie.Name == FormsAuthentication.FormsCookieName)
                    {
                        ticket = FormsAuthentication.Decrypt(perCookie.Value);
                        break;
                    }
                }

                if (ticket == null)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    return;
                }

                // TODO: 添加其它验证方法

                base.OnActionExecuting(actionContext);
            }
            catch
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
            {
                base.OnActionExecuted(actionExecutedContext);
            }
    }
}