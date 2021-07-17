using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Controllers
{
    public class WorkFlowDesignerController : MyController
    {
        //
        // GET: /WorkFlowDesigner/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index1()
        {
            return View();
        }

        public ActionResult Open()
        {
            return View();
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult Open_Tree()
        {
            return View();
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult Open_Tree1()
        {
            return View();
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult Open_List()
        {
            return View();
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult Open_List1()
        {
            return View();
        }

        /// <summary>
        /// 查询一页数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Query()
        {
            List<YJ.Data.Model.WorkFlow> list = new List<YJ.Data.Model.WorkFlow>();
            YJ.Platform.Users busers = new YJ.Platform.Users();
            YJ.Platform.WorkFlow bwf = new YJ.Platform.WorkFlow();
            string name = Request.Form["flow_name"];
            string typeid = Request.Form["typeid"];
            string sidx = Request.Form["sidx"];
            string sord = Request.Form["sord"];
            bool openlist = "1" == Request.Form["openlist"];//区分是在更表中打开还是在设计器中打开

            string typeids = "";
            if (typeid.IsGuid())
            {
                typeids = new YJ.Platform.Dictionary().GetAllChildsIDString(typeid.ToGuid());
            }

            long count;
            int pageSize = openlist ? 10 : YJ.Utility.Tools.GetPageSize();
            int pageNumber = YJ.Utility.Tools.GetPageNumber();
            string order = (sidx.IsNullOrEmpty() ? "CreateDate" : sidx) + " " + (sord.IsNullOrEmpty() ? "asc" : sord);
            list = bwf.GetPagerData(out count, pageSize, pageNumber, YJ.Platform.Users.CurrentUserID.ToString(), typeids, name, order);
            LitJson.JsonData json = new LitJson.JsonData();
            foreach (var li in list)
            {
                LitJson.JsonData j = new LitJson.JsonData();
                j["id"] = li.ID.ToString();
                j["Name"] = li.Name;
                j["CreateDate"] = li.CreateDate.ToDateTimeString();
                j["CreateUserID"] = busers.GetName(li.CreateUserID);
                j["Status"] = bwf.GetStatusTitle(li.Status);
                if (openlist)
                {
                    j["Edit"] = "<a href=\"javascript:void(0);\" onclick=\"openflow('" + li.ID + "');return false;\">"
                    + "<img src=\"" + Url.Content("~/Images/ico/topic_edit.gif") + "\" alt=\"\" style=\"vertical-align:middle; border:0;\" />"
                    + "<span style=\"vertical-align:middle; margin-left:3px;\">编辑</span></a>";
                }
                else
                {
                    j["Edit"] = "<a class=\"editlink\" href=\"javascript:void(0);\" onclick=\"openflow('" + li.ID + "','" + li.Name + "');return false;\">"
                        + "<span style=\"vertical-align:middle;\">编辑</span></a>"
                        + "<a class=\"deletelink\" href=\"javascript:void(0);\" style=\"margin-left:5px\" onclick=\"delflow('" + li.ID + "'); return false;\"><span style=\"vertical-align:middle;\">删除</span></a>"
                        + "<a href=\"javascript:void(0);\" style=\"margin-left:5px\" onclick=\"ExportFlow('" + li.ID + "'); return false;\"><span style=\"vertical-align:middle; background:url(../Images/ico/arrow_medium_right.png) no-repeat;padding-left:18px;\">导出</span></a>";
                }
                    json.Add(j);
            }
            return "{\"userdata\":{\"total\":" + count + ",\"pagesize\":" + pageSize + ",\"pagenumber\":" + pageNumber + "},\"rows\":" + json.ToJson() + "}";
        }

        [MyAttribute(CheckApp = false, CheckLogin = false, CheckUrl = false)]
        public string GetJSON()
        {
            string loginMsg;
            if (!WebMvc.Common.Tools.CheckLogin(out loginMsg) && !YJ.Platform.WeiXin.Organize.CheckLogin())
            {
                return "{}";
            }
            string flowid = Request.QueryString["flowid"];
            string type = Request.QueryString["type"];
            if (!flowid.IsGuid())
            {
                return "{}";
            }
            var flow = new YJ.Platform.WorkFlow().Get(flowid.ToGuid());
            if (flow == null)
            {
                return "{}";
            }
            else
            {
                return "0" == type ? flow.RunJSON : flow.DesignJSON;
            }
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult Set_Flow()
        {
            return View();
        }

        [MyAttribute(CheckApp = false)]
        public string GetTables()
        {
            string loginMsg;
            if (!WebMvc.Common.Tools.CheckLogin(out loginMsg))
            {
                return "";
            }
            Response.Charset = "utf-8";
            string connID = Request.QueryString["connid"];
            if (!connID.IsGuid())
            {
                return "[]";
            }
            List<string> tables = new YJ.Platform.DBConnection().GetTables(connID.ToGuid());
            System.Text.StringBuilder sb = new System.Text.StringBuilder("[", 1000);
            foreach (string table in tables)
            {
                sb.Append("{\"name\":");
                sb.AppendFormat("\"{0}\"", table);
                sb.Append("},");
            }
            return sb.ToString().TrimEnd(',') + "]";
        }

        [MyAttribute(CheckApp = false)]
        public string GetFields()
        {
            string loginMsg;
            if (!WebMvc.Common.Tools.CheckLogin(out loginMsg))
            {
                return "";
            }
            string table = Request.QueryString["table"];
            string connid = Request.QueryString["connid"];

            if (table.IsNullOrEmpty() || !connid.IsGuid())
            {
                return "[]";
            }
            Dictionary<string, string> fields = new YJ.Platform.DBConnection().GetFields(connid.ToGuid(), table);
            System.Text.StringBuilder sb = new System.Text.StringBuilder("[", 1000);

            foreach (var field in fields)
            {
                sb.Append("{");
                sb.AppendFormat("\"name\":\"{0}\",\"note\":\"{1}\"", field.Key, field.Value);
                sb.Append("},");
            }
            return sb.ToString().TrimEnd(',') + "]";
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult Set_Step()
        {
            return View();
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult Set_SubFlow()
        {
            return View();
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult Set_Line()
        {
            return View();
        }

        public ActionResult Opation()
        {
            return View();
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult Save()
        {
            return View();
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult Install()
        {
            return View();
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult UnInstall()
        {
            return View();
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult SaveAs()
        {
            return View();
        }

        /// <summary>
        /// 测试连接的sql条件
        /// </summary>
        /// <returns></returns>
        [MyAttribute(CheckApp = false)]
        public string TestLineSqlWhere()
        {
            string loginMsg;
            if (!WebMvc.Common.Tools.CheckLogin(out loginMsg))
            {
                return "";
            }
            string connid = Request["connid"];
            string table = Request["table"];
            string tablepk = Request["tablepk"];
            string where = Request["where"] ?? "";

            YJ.Platform.DBConnection dbconn = new YJ.Platform.DBConnection();

            if (!connid.IsGuid())
            {
                return "流程未设置数据连接!";
            }
            var conn = dbconn.Get(connid.ToGuid());
            if (conn == null)
            {
                return "未找到连接!";
            }
            string sql = "SELECT * FROM " + table + " WHERE 1=1 AND " + where.FilterWildcard();
            if (dbconn.TestSql(conn, sql))
            {
                return "SQL条件正确!";
            }
            else
            {
                return "SQL条件错误!";
            }
        }

        [MyAttribute(CheckApp = false)]
        public void Export()
        {
            string loginMsg;
            if (!WebMvc.Common.Tools.CheckLogin(out loginMsg))
            {
                return;
            }
            YJ.Platform.WorkFlow WF = new YJ.Platform.WorkFlow();
            var file = WF.Export(Request.QueryString["flowid"].ToGuid(), 1);
            if (!file.IsNullOrEmpty())
            {
                Response.Redirect("../Files/Show?id=" + file.DesEncrypt() + "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"]);
            }
            else
            {
                Response.Write("导出失败");
                Response.End();
                return;
            }
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult Import()
        {
            return Import(null);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAttribute(CheckApp = false)]
        public ActionResult Import(FormCollection collection)
        {
            if (collection != null)
            {
                var FileUpload1 = Request.Files["FileUpload1"];
                if (FileUpload1 == null || FileUpload1.FileName.IsNullOrEmpty())
                {
                    ViewBag.script = "alert('请选择要导入的文件!');";
                    return View();
                }
                string filePath = YJ.Utility.Config.FilePath + "WorkFlowImportFiles\\" + Guid.NewGuid().ToString("N");
                if (!System.IO.Directory.Exists(filePath))
                {
                    System.IO.Directory.CreateDirectory(filePath);
                }
                string zipFile = filePath + "\\" + FileUpload1.FileName;
                FileUpload1.SaveAs(zipFile);
                string msg = new YJ.Platform.WorkFlow().Import(zipFile, 1);
                if ("1" == msg)
                {
                    ViewBag.script = "alert('导入成功!');new RoadUI.Window().reloadOpener();";
                }
                else
                {
                    ViewBag.script = "alert('" + msg + "');new RoadUI.Window().reloadOpener();";
                }
            }
            
            return View();
        }

        
    }
}
