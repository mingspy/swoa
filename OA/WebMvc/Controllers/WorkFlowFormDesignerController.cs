using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;

namespace WebMvc.Controllers
{
    public class WorkFlowFormDesignerController : MyController
    {
        //
        // GET: /WorkFlowFormDesigner/
        [MyAttribute(CheckApp = false)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index1()
        {
            return View();
        }

        public ActionResult Open_Tree1()
        {
            return View();
        }

        public ActionResult Open_List1(FormCollection collection)
        {
            return View();
        }

        [HttpPost]
        [MyAttribute(CheckApp=false)]
        public string Query()
        {
            List<YJ.Data.Model.WorkFlowForm> list = new List<YJ.Data.Model.WorkFlowForm>();
            YJ.Platform.WorkFlowForm bwff = new YJ.Platform.WorkFlowForm();
            string name = Request.Form["form_name"];
            string typeid = Request.Form["typeid"];
            string sidx = Request.Form["sidx"];
            string sord = Request.Form["sord"];
            bool openlist = "1" == Request.Form["openlist"];//是否在设计器中打开

            string typeids = "";
            if (typeid.IsGuid())
            {
                typeids = new YJ.Platform.Dictionary().GetAllChildsIDString(typeid.ToGuid());
            }
            long count;
            int pageSize = openlist ? 10 : YJ.Utility.Tools.GetPageSize();
            int pageNumber = YJ.Utility.Tools.GetPageNumber();
            string order = (sidx.IsNullOrEmpty() ? "WriteTime" : sidx) + " " + (sord.IsNullOrEmpty() ? "asc" : sord);

            string query = "&appid=" + Request.QueryString["appid"] + "&typeid=" + typeid + "&name=" + name.UrlEncode();
            list = bwff.GetPagerData(out count, pageSize, pageNumber, "", typeids, name, order);
            LitJson.JsonData json = new LitJson.JsonData();
            foreach (var li in list)
            {
                LitJson.JsonData j = new LitJson.JsonData();
                j["id"] = li.ID.ToString();
                j["Name"] = li.Name;
                j["CreateUserName"] = li.CreateUserName;
                j["CreateTime"] = li.CreateTime.ToDateTimeString();
                j["LastModifyTime"] = li.LastModifyTime.ToDateTimeString();
                if (openlist)
                {
                    j["Edit"] = string.Format(@"<a href=""javascript:void(0);"" onclick=""openform('{0}');return false;""><img src=""{1}/Images/ico/topic_edit.gif"" alt="""" style=""vertical-align:middle; border:0;"" /><span style=""vertical-align:middle;margin-left:2px;"">编辑</span></a><a href=""javascript:void(0);"" onclick=""delform('{0}');return false;""><img src=""{1}/Images/ico/topic_del.gif"" alt="""" style=""vertical-align:middle; border:0; margin-left:5px;"" /><span style=""vertical-align:middle;margin-left:2px;"">删除</span></a>", li.ID.ToString(), WebMvc.Common.Tools.BaseUrl);
                }
                else
                {
                    j["Edit"] = "<a class=\"editlink\" href=\"javascript:void(0);\" onclick=\"openform('" + li.ID.ToString() + "','" + li.Name + "');return false;\"><span style=\"vertical-align:middle;\">编辑</span></a>"
                        + "<a class=\"deletelink\" href=\"javascript:void(0);\" style=\"margin-left:5px;\" onclick=\"delform('" + li.ID.ToString() + "'); return false;\"><span style=\"vertical-align:middle;\">删除</span></a>"
                        + "<a href=\"javascript:void(0);\" style=\"margin-left:5px;\" onclick=\"ExportForm('" + li.ID.ToString() + "'); return false;\"><span style=\"vertical-align:middle; background:url(../Images/ico/arrow_medium_right.png) no-repeat;padding-left:18px;\">导出</span></a>";
                }
                json.Add(j);

            }
            return "{\"userdata\":{\"total\":" + count + ",\"pagesize\":" + pageSize + ",\"pagenumber\":" + pageNumber + "},\"rows\":" + json.ToJson() + "}";
        }

        [MyAttribute(CheckApp = false)]
        public string GetHtml()
        {
            string id = Request["id"];
            Guid gid;
            if (!id.IsGuid(out gid))
            {
                return "";
            }

            var wff = new YJ.Platform.WorkFlowForm().Get(gid);
            if (wff == null)
            {
                return "";
            }
            else
            {
                return wff.Html;
            }
        }

        [MyAttribute(CheckApp = false)]
        public string GetAttribute()
        {
            string id = Request["id"];
            Guid gid;
            if (!id.IsGuid(out gid))
            {
                return "";
            }

            var wff = new YJ.Platform.WorkFlowForm().Get(gid);
            if (wff == null)
            {
                return "";
            }
            else
            {
                return wff.Attribute;
            }
        }

        [MyAttribute(CheckApp = false)]
        public string Getsubtable()
        {
            string id = Request["id"];
            Guid gid;
            if (!id.IsGuid(out gid))
            {
                return "";
            }

            var wff = new YJ.Platform.WorkFlowForm().Get(gid);
            if (wff == null)
            {
                return "";
            }
            else
            {
                return wff.SubTableJson;
            }
        }

        [MyAttribute(CheckApp = false)]
        public string GetEvents()
        {
            string id = Request["id"];
            Guid gid;
            if (!id.IsGuid(out gid))
            {
                return "";
            }

            var wff = new YJ.Platform.WorkFlowForm().Get(gid);
            if (wff == null)
            {
                return "";
            }
            else
            {
                return wff.EventsJson;
            }
        }

        [MyAttribute(CheckApp = false)]
        public string TestSql()
        {
            string sql = Request["sql"];
            string dbconn = Request["dbconn"];

            if (sql.IsNullOrEmpty() || !dbconn.IsGuid())
            {
                return "SQL语句为空或未设置数据连接";
            }

            YJ.Platform.DBConnection bdbconn = new YJ.Platform.DBConnection();
            var dbconn1 = bdbconn.Get(dbconn.ToGuid());
            if (bdbconn.TestSql(dbconn1, sql.ReplaceSelectSql().FilterWildcard()))
            {
                return "SQL语句测试正确";
            }
            else
            {
                return "SQL语句测试错误";
            }
        }

        [MyAttribute(CheckApp = false)]
        public string Save()
        {
            string html = Request["html"];
            string name = Request["name"];
            string att = Request["att"];
            string id = Request["id"];
            string type = Request["type"];
            string subtable = Request["subtable"];
            string formEvents = Request["formEvents"];
          
            if (name.IsNullOrEmpty())
            {
                return "表单名称不能为空!";
            }

            Guid formID;
            if (!id.IsGuid(out formID))
            {
                return "表单ID无效!";
            }

            YJ.Platform.WorkFlowForm WFF = new YJ.Platform.WorkFlowForm();
            YJ.Data.Model.WorkFlowForm wff = WFF.Get(formID);
            bool isAdd = false;
            string oldXML = string.Empty;
            if (wff == null)
            {
                wff = new YJ.Data.Model.WorkFlowForm();
                wff.ID = formID;
                wff.CreateUserID = YJ.Platform.Users.CurrentUserID;
                wff.CreateUserName = YJ.Platform.Users.CurrentUserName;
                wff.CreateTime = YJ.Utility.DateTimeNew.Now;
                wff.Status = 0;
                isAdd = true;
            }
            else
            {
                oldXML = wff.Serialize();
            }

            wff.Attribute = att;
            wff.Html = html;
            wff.LastModifyTime = YJ.Utility.DateTimeNew.Now;
            wff.Name = name;
            wff.Type = type.ToGuid();
            wff.SubTableJson = subtable;
            wff.EventsJson = formEvents;

            if (isAdd)
            {
                WFF.Add(wff);
                YJ.Platform.Log.Add("添加了流程表单", wff.Serialize(), YJ.Platform.Log.Types.流程相关);
            }
            else
            {
                WFF.Update(wff);
                YJ.Platform.Log.Add("修改了流程表单", "", YJ.Platform.Log.Types.流程相关, oldXML, wff.Serialize());
            }
            return "保存成功!";
        }

        [HttpPost]
        [MyAttribute(CheckApp = false, CheckLogin = false)]
        public string GetSubTableData()
        {
            string msg;
            if (!Common.Tools.CheckLogin(out msg) && !YJ.Platform.WeiXin.Organize.CheckLogin())
            {
                return "{}";
            }
            
            string secondtable = Request["secondtable"];
            string primarytablefiled = Request["primarytablefiled"];
            string secondtableprimarykey = Request["secondtableprimarykey"];
            string primarytablefiledvalue = Request["primarytablefiledvalue"];
            string secondtablerelationfield = Request["secondtablerelationfield"];
            string dbconnid = Request["dbconnid"];
            string subtableformat = Request["subtableformat"];
            string sortstring = Request["sortstring"];
            string sortField = sortstring.IsNullOrEmpty() ? secondtableprimarykey : sortstring;

            LitJson.JsonData data = new YJ.Platform.WorkFlow().GetSubTableData(dbconnid, secondtable, secondtablerelationfield, primarytablefiledvalue, sortField, subtableformat);
            return data.ToJson();
        }

        [MyAttribute(CheckApp = false)]
        public string Publish()
        {
            string html = Request["html"];
            string name = Request["name"];
            string att = Request["att"];
            string id = Request["id"];
            string formats = Request["formats"];

            Guid gid;
            if (!id.IsGuid(out gid) || name.IsNullOrEmpty() || att.IsNullOrEmpty())
            {
                return "参数错误!";
            }
            YJ.Platform.WorkFlowForm WFF = new YJ.Platform.WorkFlowForm();

            YJ.Data.Model.WorkFlowForm wff = WFF.Get(gid);
            if (wff == null)
            {
                return "请先保存表单再发布!";
            }

            string fileName = id + ".cshtml";

            System.Text.StringBuilder serverScript = new System.Text.StringBuilder("@{\r\n");
            var attrJSON = LitJson.JsonMapper.ToObject(att);
            serverScript.Append("\tstring FlowID = Request.QueryString[\"flowid\"];\r\n");
            serverScript.Append("\tstring StepID = Request.QueryString[\"stepid\"];\r\n");
            serverScript.Append("\tstring GroupID = Request.QueryString[\"groupid\"];\r\n");
            serverScript.Append("\tstring TaskID = Request.QueryString[\"taskid\"];\r\n");
            serverScript.Append("\tstring InstanceID = Request.QueryString[\"instanceid\"];\r\n");
            serverScript.Append("\tstring DisplayModel = Request.QueryString[\"display\"] ?? \"0\";\r\n");
            serverScript.AppendFormat("\tstring DBConnID = \"{0}\";\r\n", attrJSON["dbconn"].ToString());
            serverScript.AppendFormat("\tstring DBTable = \"{0}\";\r\n", attrJSON["dbtable"].ToString());
            serverScript.AppendFormat("\tstring DBTablePK = \"{0}\";\r\n", attrJSON["dbtablepk"].ToString());
            serverScript.AppendFormat("\tstring DBTableTitle = \"{0}\";\r\n", attrJSON["dbtabletitle"].ToString());
            serverScript.Append("\tif(InstanceID.IsNullOrEmpty()){InstanceID = Request.QueryString[\"instanceid1\"];}\r\n");

            serverScript.Append("\tYJ.Platform.Dictionary BDictionary = new YJ.Platform.Dictionary();\r\n");
            serverScript.Append("\tYJ.Platform.WorkFlow BWorkFlow = new YJ.Platform.WorkFlow();\r\n");
            serverScript.Append("\tYJ.Platform.WorkFlowTask BWorkFlowTask = new YJ.Platform.WorkFlowTask();\r\n");
            serverScript.Append("\tstring fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);\r\n");
            serverScript.Append("\tLitJson.JsonData initData = BWorkFlow.GetFormData(DBConnID, DBTable, DBTablePK, InstanceID, fieldStatus, \"" + formats + "\");\r\n");
            serverScript.Append("\tstring TaskTitle = BWorkFlow.GetFromFieldData(initData, DBTable, DBTableTitle);\r\n");

            serverScript.Append("}\r\n");
            serverScript.Append("<link href=\"~/Scripts/FlowRun/Forms/flowform.css\" rel=\"stylesheet\" type=\"text/css\" />\r\n");
            serverScript.Append("<script src=\"~/Scripts/FlowRun/Forms/common.js\" type=\"text/javascript\" ></script>\r\n");

            if (attrJSON.ContainsKey("hasEditor") && "1" == attrJSON["hasEditor"].ToString())
            {
                serverScript.Append("<script src=\"~/Scripts/Ueditor/ueditor.config.js\" type=\"text/javascript\" ></script>\r\n");
                serverScript.Append("<script src=\"~/Scripts/Ueditor/ueditor.all.min.js\" type=\"text/javascript\" ></script>\r\n");
                serverScript.Append("<script src=\"~/Scripts/Ueditor/lang/zh-cn/zh-cn.js\" type=\"text/javascript\" ></script>\r\n");
                serverScript.Append("<input type=\"hidden\" id=\"Form_HasUEditor\" name=\"Form_HasUEditor\" value=\"1\" />\r\n");
            }
            string validatePropType = attrJSON.ContainsKey("validatealerttype") ? attrJSON["validatealerttype"].ToString() : "2";
            serverScript.Append("<input type=\"hidden\" id=\"Form_ValidateAlertType\" name=\"Form_ValidateAlertType\" value=\"" + validatePropType + "\" />\r\n");
            if (attrJSON.ContainsKey("autotitle") && attrJSON["autotitle"].ToString().ToLower() == "true")
            {
                serverScript.AppendFormat("<input type=\"hidden\" id=\"{0}\" name=\"{0}\" value=\"{1}\" />\r\n",
                    string.Concat(attrJSON["dbtable"].ToString(), ".", attrJSON["dbtabletitle"].ToString()),
                    "@(TaskTitle.IsNullOrEmpty() ? BWorkFlow.GetAutoTaskTitle(FlowID, StepID, Request.QueryString[\"groupid\"]) : TaskTitle)"
                    );
            }
            serverScript.AppendFormat("<input type=\"hidden\" id=\"Form_TitleField\" name=\"Form_TitleField\" value=\"{0}\" />\r\n", string.Concat(attrJSON["dbtable"].ToString(), ".", attrJSON["dbtabletitle"].ToString()));
            //serverScript.AppendFormat("<input type=\"hidden\" id=\"Form_Name\" name=\"Form_Name\" value=\"{0}\" />\r\n", attrJSON["name"].ToString());
            serverScript.AppendFormat("<input type=\"hidden\" id=\"Form_DBConnID\" name=\"Form_DBConnID\" value=\"{0}\" />\r\n", attrJSON["dbconn"].ToString());
            serverScript.AppendFormat("<input type=\"hidden\" id=\"Form_DBTable\" name=\"Form_DBTable\" value=\"{0}\" />\r\n", attrJSON["dbtable"].ToString());
            serverScript.AppendFormat("<input type=\"hidden\" id=\"Form_DBTablePk\" name=\"Form_DBTablePk\" value=\"{0}\" />\r\n", attrJSON["dbtablepk"].ToString());
            serverScript.AppendFormat("<input type=\"hidden\" id=\"Form_DBTableTitle\" name=\"Form_DBTableTitle\" value=\"{0}\" />\r\n", attrJSON["dbtabletitle"].ToString());
            serverScript.AppendFormat("<input type=\"hidden\" id=\"Form_AutoSaveData\" name=\"Form_AutoSaveData\" value=\"{0}\" />\r\n", "1");
            serverScript.AppendFormat("<textarea id=\"Form_DBTableTitleExpression\" name=\"Form_DBTableTitleExpression\" style=\"display:none;width:0;height:0;\">{0}</textarea>\r\n", attrJSON.ContainsKey("dbtabletitle1") ? attrJSON["dbtabletitle1"].ToString() : "");
            serverScript.Append("<script type=\"text/javascript\">\r\n");
            serverScript.Append("\tvar initData = @Html.Raw(BWorkFlow.GetFormDataJsonString(initData));\r\n");
            serverScript.Append("\tvar fieldStatus = \"1\"==\"@Request.QueryString[\"isreadonly\"]\" ? {} : @Html.Raw(fieldStatus);\r\n");
            serverScript.Append("\tvar displayModel = '@DisplayModel';\r\n");
            serverScript.Append("\t$(window).load(function (){\r\n");
            serverScript.AppendFormat("\t\tformrun.initData(initData, \"{0}\", fieldStatus, displayModel);\r\n", attrJSON["dbtable"].ToString());
            serverScript.Append("\t});\r\n");
            serverScript.Append("</script>\r\n");


            string file = Server.MapPath("~/Views/WorkFlowFormDesigner/Forms/" + fileName);
            System.IO.Stream stream = System.IO.File.Open(file, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            stream.SetLength(0);
           
            StreamWriter sw = new StreamWriter(stream, System.Text.Encoding.UTF8);
            sw.Write(serverScript.ToString());
            sw.Write(Server.HtmlDecode(html));
            
            sw.Close();
            stream.Close();


            string attr = wff.Attribute;
            string appType = LitJson.JsonMapper.ToObject(attr)["apptype"].ToString();
            YJ.Platform.AppLibrary App = new YJ.Platform.AppLibrary();
            var app = App.GetByCode(id);
            bool isAdd = false;
            if (app == null)
            {
                app = new YJ.Data.Model.AppLibrary();
                app.ID = Guid.NewGuid();
                app.Code = id;
                isAdd = true;
            }
            app.Address = "/Views/WorkFlowFormDesigner/Forms/" + fileName;
            app.Note = "流程表单";
            app.OpenMode = 0;
            app.Params = "";
            app.Title = name.Trim();
            app.Type = appType.IsGuid() ? appType.ToGuid() : new YJ.Platform.Dictionary().GetIDByCode("FormTypes");
            if (isAdd)
            {
                App.Add(app);
            }
            else
            {
                App.Update(app);
            }

            YJ.Platform.Log.Add("发布了流程表单", app.Serialize() + "内容：" + html, YJ.Platform.Log.Types.流程相关);
            wff.Status = 1;
            WFF.Update(wff);
            return "发布成功!";
        }

        [MyAttribute(CheckApp = false, CheckLogin = false)]
        public string GetFormGridHtml()
        {
            string msg;
            if (!Common.Tools.CheckLogin(out msg) && !YJ.Platform.WeiXin.Organize.CheckLogin())
            {
                return "";
            }

            string dbconnID = Request.Form["dbconnid"];
            string dataFormat = Request.Form["dataformat"];
            string dataSource = Request.Form["datasource"];
            string dataSource1 = Request.Form["datasource1"] ?? "";
            string params1 = Request.Form["params"];

            return new YJ.Platform.WorkFlowForm().GetFormGridHtml(dbconnID, dataFormat, dataSource, dataSource1.FilterWildcard(), params1);
        }

        [MyAttribute(CheckApp = false)]
        public void Export()
        {
            YJ.Platform.WorkFlowForm WFF = new YJ.Platform.WorkFlowForm();
            var file = WFF.Export(Request.QueryString["formid"].ToGuid(), 1);
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
                string filePath = YJ.Utility.Config.FilePath + "WorkFlowFormImportFiles\\" + Guid.NewGuid().ToString("N");
                if (!System.IO.Directory.Exists(filePath))
                {
                    System.IO.Directory.CreateDirectory(filePath);
                }
                string zipFile = filePath + "\\" + FileUpload1.FileName;
                FileUpload1.SaveAs(zipFile);
                string msg = new YJ.Platform.WorkFlowForm().Import(zipFile, 1);
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
