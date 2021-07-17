using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YJ.Platform
{
    public class Email
    {
        /// <summary>
        /// 发送外部邮件
        /// </summary>
        /// <param name="addrss"></param>
        /// <param name="title"></param>
        /// <param name="contents"></param>
        /// <param name="files"></param>
        public static void Send(string addrss, string title, string contents, string files = "")
        { 
        
        }

        /// <summary>
        /// 发送内部邮件
        /// </summary>
        /// <param name="addrss"></param>
        /// <param name="title"></param>
        /// <param name="contents"></param>
        /// <param name="files"></param>
        public static void Send(Guid userID, string title, string contents, string files = "")
        {

        }

    }
}
