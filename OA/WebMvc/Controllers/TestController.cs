using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Controllers
{
    public class TestController : MyController
    {
        //
        // GET: /Test/
        [MyAttribute(CheckApp = false, CheckUrl = false, CheckLogin = false)]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 自定义表单例子
        /// </summary>
        /// <returns></returns>
        [MyAttribute(CheckApp = false, CheckUrl = false)]
        public ActionResult CustomForm()
        {
            return CustomForm(null);
        }

        /// <summary>
        /// 自定义表单例子
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [MyAttribute(CheckApp = false, CheckUrl = false)]
        [ValidateAntiForgeryToken]
        public ActionResult CustomForm(FormCollection coll)
        {
            string instanceid = Request.QueryString["instanceid"];

            if (coll != null)
            {
                string Title1 = Request.Form["Title"];
                string Contents = Request.Form["Contents"];
                string sql = string.Empty;
                if (!instanceid.IsNullOrEmpty())
                {
                    sql = "update TempTest_CustomForm set Title=@Title,Contents=@Contents where id=" + instanceid;
                }
                else
                {
                    sql = "insert into TempTest_CustomForm(Title,Contents) values(@Title,@Contents)";
                }
                System.Data.SqlClient.SqlParameter[] par = new System.Data.SqlClient.SqlParameter[] { 
                    new System.Data.SqlClient.SqlParameter("@Title", Title1),
                    new System.Data.SqlClient.SqlParameter("@Contents", Contents)
                };
                int insid = new YJ.Data.MSSQL.DBHelper().Execute(sql, par, true);
                ViewBag.title1 = Title1;
                ViewBag.contents = Contents;

                //注意保存后这里的脚本
                ViewBag.script =
                "$('#instanceid',parent.document).val('" + insid + "');" +
                "$('#customformtitle',parent.document).val('" + Title1 + "');parent.flowSaveAndSendIframe(true);";
            }
            else
            {
                if (!instanceid.IsNullOrEmpty())
                {
                    System.Data.DataTable dt = new YJ.Data.MSSQL.DBHelper().GetDataTable("select * from TempTest_CustomForm where id=" + instanceid);
                    if (dt.Rows.Count > 0)
                    {
                        ViewBag.title1 = dt.Rows[0]["Title"].ToString();
                        ViewBag.contents = dt.Rows[0]["Contents"].ToString();
                    }
                }
            }

            return View();
        }



        [MyAttribute(CheckApp = false, CheckUrl = false)]
        public ActionResult CustomForm1()
        {
            string instanceid = Request.QueryString["instanceid"];
            if (!instanceid.IsNullOrEmpty())
            {
                string sql = "select * from TempTest_CustomForm where id=" + instanceid;
                System.Data.DataTable dt = new YJ.Data.MSSQL.DBHelper().GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    ViewBag.title1 = dt.Rows[0]["Title"].ToString();
                    ViewBag.contents = dt.Rows[0]["Contents"].ToString();
                }
            }

            return View();
        }
        [MyAttribute(CheckApp = false, CheckUrl = false)]
        public string saveCustomForm1()
        {
            string instanceid = Request.QueryString["instanceid"];
            string Title1 = Request.Form["Title"];
            string Contents = Request.Form["Contents"];
            string sql = string.Empty;
            if (!instanceid.IsNullOrEmpty())
            {
                sql = "update TempTest_CustomForm set Title=@Title,Contents=@Contents where id=" + instanceid;
            }
            else
            {
                sql = "insert into TempTest_CustomForm(Title,Contents) values(@Title,@Contents)";
            }
            System.Data.SqlClient.SqlParameter[] par = new System.Data.SqlClient.SqlParameter[] { 
                    new System.Data.SqlClient.SqlParameter("@Title", Title1),
                    new System.Data.SqlClient.SqlParameter("@Contents", Contents)
                };
            int insid = new YJ.Data.MSSQL.DBHelper().Execute(sql, par, true);
            return "{\"msg\":\"保存成功\",\"instanceid\":\"" + insid + "\"}";
        }

    }
}
