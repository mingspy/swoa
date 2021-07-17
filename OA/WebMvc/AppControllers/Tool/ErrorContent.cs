using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace WebMvc.AppControllers.Tool
{
    public static class ErrorContent
    {
        public static StringContent GetError(ErrorType type,string msg="",string data="")
        {
            if (data.IsNullOrEmpty())
            {
                return new StringContent("{\"status\":" + type.GetHashCode() + ",\"msg\":\"" + (msg.IsNullOrEmpty() ? type.ToString() : msg) + "\"}", Encoding.UTF8, "application/json");
            }
           
           return new StringContent("{\"data\":" +data+ ",\"status\":" + type.GetHashCode() + ",\"msg\":\"" + (msg.IsNullOrEmpty() ? type.ToString() : msg) + "\"}", Encoding.UTF8, "application/json");
        }
    }
    public enum ErrorType
    {
        失败= 0,
        成功= 1,
        程序错误=101,
        接口已停用拒绝访问=102,
        缺少输入参数=104,
        输入参数格式错误=105,
        缺少AppKey参数接口=109,
        非法的AppKey参数=110
    }
}