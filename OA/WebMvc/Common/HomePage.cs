using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

namespace WebMvc.Common
{
    public class HomePage
    {
        public static string GetShortMessageList()
        {
            string sql = "select top 5 id,title,contents,sendusername,sendtime from ShortMessage where ReceiveUserID='" + YJ.Platform.Users.CurrentUserID + "' order by sendtime desc";
            DataTable dt = new YJ.Data.MSSQL.DBHelper().GetDataTable(sql);
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("<div style='margin:10px 0 5px 0;'>");
                sb.Append("<a href=\"javascript:show('" + dr["ID"].ToString() + "');\">" + dr["title"].ToString() + "</a>");
                sb.Append("</div>");
                sb.Append("<div style='color:#999; padding:3px 0 5px 0;border-bottom:1px dotted #e8e8e8;'>");
                sb.Append("<span>发送人：" + dr["sendusername"] + "</span>");
                sb.Append("<span style='margin:0 6px;'>|</span>");
                sb.Append("<span>时间：" + dr["sendtime"].ToString().ToDateTimeStringS() + "</span>");
                sb.Append("</div>");
            }
            sb.Append("<script>");
            sb.Append("function show(id){new RoadUI.Window().open({ url: RoadUI.Core.rooturl() + '/ShortMessage/Show?id=' + id, width: 900, height: 500, title: '查看消息' });}");
            sb.Append("</script>");
            return sb.ToString();
        }

        /// <summary>
        /// 得到在线人数
        /// </summary>
        /// <returns></returns>
        public static string GetOnlineUsersCount()
        {
            return new YJ.Platform.OnlineUsers().GetAll().Count.ToString();
        }
    }
}