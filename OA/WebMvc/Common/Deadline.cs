using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace WebMvc.Common
{
    public class Deadline
    {
        public static string GetHandlingTerm()
        {
            //查询通讯录sql语句
            //string sql = "select t.姓名,t.部门,t.办公电话,t.私人电话,t.邮箱  from(select U.ID,C.ID as 部门ID,C.Name as 部门,U.Name as 姓名,U.Tel as 办公电话,U.Mobile as 私人电话,U.Email as 邮箱,U.QQ as QQ,U.WeiXin as 微信,C.Sort as depSort,U.Sort as uSort from Users as U inner join UsersRelation As A on U.ID = A.UserID inner join Organize AS C on A.OrganizeID = C.ID where A.IsMain = 1and u.Status = 0)t where 1 = 1";
            string sql = "select t.姓名,t.部门,t.办公电话,t.私人电话,t.邮箱 from (select U.ID, C.ID as 部门ID, C.Name as 部门, U.Name as 姓名,U.Tel as 办公电话,U.Mobile as 私人电话,U.Email as 邮箱,U.QQ as QQ,U.WeiXin as 微信,C.Sort as depSort,U.Sort as uSort from Users as U inner join UsersRelation As A on U.ID = A.UserID inner join Organize AS C on A.OrganizeID = C.ID where A.IsMain = 1 and u.Status = 0)t where 1 = 1 order by t.depSort,t.uSort";
            //定义datetable dt接收查询出的数据
            DataTable dt = new YJ.Data.MSSQL.DBHelper().GetDataTable(sql);
            //定义拼接sb
            StringBuilder sb = new StringBuilder();
            //拼接模块头table
            sb.Append("<div style=\"min-height: 76px;display: inline-block;\">");
            //循环遍历dt标题列
            //foreach (System.Data.DataColumn dc in dt.Columns)
            //{
            //    //dc为标题
            //    sb.Append("<th>" + dc+ "</th>");
            //}
            //sb.Append("</tr></thead><tbody>");
            //循环遍历dt行
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                var tel1 = dr["办公电话"].ToString();
                var tel2 = dr["私人电话"].ToString();
                if (tel1 == "" && tel2 == "")
                {
                    tel1 = "暂无";
                }
                var email = dr["邮箱"].ToString();
                if (email == "")
                {
                    email = "暂无";
                }
                sb.Append("<style>.telItem:hover .card{display:inline-block}.telItem .card{display:none}</style>" +
                    "<div class=\"telItem\" style=\"width: 40px;margin-right: 17px;float: left;position: relative;\" > ");
                sb.Append("<div style=\"height: 40px;width: 40px;margin-top: 10px;border-radius: 20px;cursor: pointer;font-size: 12px;background: #0197f5;color:#fff;line-height:40px;text-align: center;\">" + dr["姓名"].ToString().Substring(dr["姓名"].ToString().Length - 2) + "</div>");
                sb.Append("<div style=\"color: #9e9e9e;text-align: center;margin-top: 5px;font-size: 12px;cursor: pointer;line-height: 12px;vertical-align: middle;overflow: hidden;text-overflow: ellipsis;white-space: nowrap;\">" + dr["姓名"].ToString() + "</div>");
                //sb.Append("<td>" + dr["姓名"].ToString() + " </td>");
                //sb.Append("<td>" + dr["部门"].ToString() + " </td>");
                //sb.Append("<td>" + dr["办公电话"].ToString() + " </td>");
                //sb.Append("<td>" + dr["私人电话"].ToString() + " </td>");
                //sb.Append("<td>" + dr["邮箱"].ToString() + "</td>");
                sb.Append("<div class=\"card\" style=\"position: absolute;background: #fff; border-radius: 2px; border: 1px solid #ededed;font-size: 12px;min-width: 160px!important;transform-origin: right center 0px;z-index: 2021;right: 42px;    top: 0;\">");
                sb.Append("<div style=\"position: relative;padding: 20px 0 0;color: #9e9e9e; font-size: 14px;z-index: 999;\">");

                sb.Append("<div style=\"padding: 0 20px;position: relative;\">");

                sb.Append("<div><div style=\"border-radius: 100%;overflow: hidden;color: #fff;background:#ffffff;position: absolute;top: 0;left: 20px;width: 40px!important;height: 40px!important;line-height: 40px!important;background: #0197f5;color:#fff;line-height:40px;text-align: center;\">" + dr["姓名"].ToString().Substring(dr["姓名"].ToString().Length - 2) + "</div><div style=\"padding: 0 0 0 60px;font-size: 16px;color: #262626;height: 40px;\"><div style=\"\">" + dr["姓名"].ToString() + "</div><div style=\"float: left;font-size: 12px;margin-right: 10px;text-align: center;\">" + dr["部门"].ToString() + "</div></div></div>");

                sb.Append("</div>");

                sb.Append("<div style=\"margin-top: 15px;font-size: 12px;white-space: nowrap;text-overflow: ellipsis;           overflow: hidden;padding: 0 20px;height: 40px;right: 0;line-height: 40px;background-color: #f5f5f5;\">");
                sb.Append("<i class=\"fa fa-phone\" style=\"margin: 0 5px 0 0;\" ></i>" + tel1 + "&nbsp;&nbsp;" + tel2 + "<i class=\"fa fa-envelope-o\" style=\"margin: 0 5px 0 18px;\" ></i>" + email);

                sb.Append("</div></div>");
                sb.Append("</div></div>");
            }
            sb.Append("</div>");
            return sb.ToString();
        }
    }
}