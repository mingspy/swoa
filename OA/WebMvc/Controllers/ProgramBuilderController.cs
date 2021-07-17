using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;

namespace WebMvc.Controllers
{
    public class ProgramBuilderController : MyController
    {
        //
        // GET: /ProgramBuilder/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult List()
        {
            return List(null);
        }
        [HttpPost]
        public ActionResult List(FormCollection form)
        {
            YJ.Platform.ProgramBuilder PB = new YJ.Platform.ProgramBuilder();

            string s_name = Request.QueryString["Name1"];
            string typeid = Request.QueryString["typeid"];

            if (!Request.Form["delete"].IsNullOrEmpty())
            {
                string checkbox_app = Request.Form["checkbox_app"] ?? "";
                foreach (string id in checkbox_app.Split(','))
                {
                    if (!id.IsGuid()) continue;
                    PB.DeleteAllSet(id.ToGuid());
                    YJ.Platform.Log.Add("删除的应用程序设计", id, YJ.Platform.Log.Types.系统管理);
                }
            }

            if (!Request.Form["publish"].IsNullOrEmpty())
            {
                string checkbox_app = Request.Form["checkbox_app"] ?? "";
                int i = 0;
                foreach (string id in checkbox_app.Split(','))
                {
                    if (!id.IsGuid()) continue;
                    i += PB.Publish(id.ToGuid(), true) ? 1 : 0;
                }
                ViewBag.script = "alert('成功发布" + i.ToString() + "个应用!');";
            }

            if (form != null)
            {
                s_name = Request.Form["Name1"];
            }
            
            string Query1 = "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&typeid=" + typeid + "&Name1=" + s_name;
            string pager;
            var pbList = PB.GetList(out pager, Query1, s_name, typeid);
            ViewBag.pager = pager;
            ViewBag.query1 = Query1;
            return View(pbList);
        }

        public ActionResult Tree()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Set_Attr()
        {

            return Set_Attr(null);
        }

        [HttpPost]
        public ActionResult Set_Attr(FormCollection form)
        {
            YJ.Data.Model.ProgramBuilder pb = null;
            YJ.Platform.ProgramBuilder PB = new YJ.Platform.ProgramBuilder();
            YJ.Platform.Dictionary BDict = new YJ.Platform.Dictionary();
            string isadd = "1";
            string ispager = "1";
            string connid = "";
            string apptypes = "";
            string formid = string.Empty;
            string typeid = Request.QueryString["typeid"];
            string pid = Request.QueryString["pid"];
            string tablename = "";
            string inDataNumberFiledName = "";
            if(pid.IsGuid())
            {
                pb = PB.Get(pid.ToGuid());
                if(pb!=null)
                {
                    typeid=pb.Type.ToString();
                    isadd=pb.IsAdd.ToString();
                    ispager = pb.IsPager.ToString();
                    connid=pb.DBConnID.ToString();
                    tablename = pb.TableName;
                    inDataNumberFiledName = pb.InDataNumberFiledName;
                    if (pb.FormID.IsGuid())
                    {
                        formid = pb.FormID.ToString();
                        var app = new YJ.Platform.AppLibrary().Get(pb.FormID.ToGuid());
                        if (app != null)
                        {
                            apptypes = app.Type.ToString();
                        }
                    }
                }
            }


            ViewBag.TypeOptions = new YJ.Platform.AppLibrary().GetTypeOptions(typeid);
            ViewBag.IsAddOptions = BDict.GetOptionsByCode("yesno", value: isadd);
            ViewBag.IsPagerOptions = BDict.GetOptionsByCode("yesno", value: ispager);
            ViewBag.DbConnOptions = new YJ.Platform.DBConnection().GetAllOptions(connid);
            ViewBag.TypeOptions1 = new YJ.Platform.AppLibrary().GetTypeOptions(apptypes);
            ViewBag.TableName = tablename;
            ViewBag.InDataNumberFiledName = inDataNumberFiledName;
            ViewBag.formid = formid;

            string query = "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&typeid=" + Request.QueryString["typeid"] + "&Name1=" + Request.QueryString["Name1"];
            if(form!=null)
            {
                string title1 = Request.Form["Title1"];
                string sql = Request.Form["sql"];
                string typeid1 = Request.Form["Type"];
                string IsAdd = Request.Form["IsAdd"];
                string DBConnID = Request.Form["DBConnID"];
                string form_forms = Request.Form["form_forms"];
                string form_editmodel = Request.Form["form_editmodel"];
                string form_editmodel_width = Request.Form["form_editmodel_width"];
                string form_editmodel_height = Request.Form["form_editmodel_height"];
                string ButtonLocation = Request.Form["ButtonLocation"];
                string IsPager = Request.Form["IsPager"];
                string ClientScript = Request.Form["ClientScript"];
                string ExportTemplate = Request.Form["ExportTemplate"];
                string ExportHeaderText = Request.Form["ExportHeaderText"];
                string ExportFileName = Request.Form["ExportFileName"];
                string TableStyle = Request.Form["TableStyle"];
                string TableHead = Request.Form["TableHead"];
                string DBTable = Request.Form["DBTable"];
                string DBFiled = Request.Form["DBFiled"];
                string ExportInFiled = Request.Form["ExportInFiled"];

                bool isadd1 = false;
                if (pb == null)
                {
                    isadd1 = true;
                    pb = new YJ.Data.Model.ProgramBuilder();
                    pb.ID = Guid.NewGuid();
                    pb.CreateTime = YJ.Utility.DateTimeNew.Now;
                    pb.CreateUser = YJ.Platform.Users.CurrentUserID ;
                    pb.Status = 0;
                }
                pb.IsAdd = IsAdd.ToInt(0);
                pb.Name = title1.Trim();
                pb.SQL = sql;
                pb.DBConnID = DBConnID.ToGuid();
                pb.Type = typeid1.ToGuid();
                pb.FormID = form_forms;
                pb.EditModel = form_editmodel.ToInt(0);
                pb.Width = form_editmodel_width;
                pb.Height = form_editmodel_height;
                pb.ButtonLocation = ButtonLocation.ToInt(0);
                pb.IsPager = IsPager.ToInt(1);
                pb.ClientScript = ClientScript;
                pb.ExportTemplate = ExportTemplate;
                pb.ExportHeaderText = ExportHeaderText.Trim1();
                pb.ExportFileName = ExportFileName.Trim1();
                pb.TableStyle = TableStyle;
                pb.TableHead = TableHead;
                pb.TableName = DBTable;
                pb.InDataNumberFiledName = DBFiled;
                pb.InDataFiledName= ExportInFiled;
                if (isadd1)
                {
                    PB.Add(pb);
                }
                else
                {
                    PB.Update(pb);
                }
                YJ.Platform.Log.Add("保存了应用程序设计属性", pb.Serialize(), YJ.Platform.Log.Types.其它分类);
                ViewBag.script = "alert('保存成功');parent.location='Add?pid=" + pb.ID + query + "';";
            }
            if(pb == null)
            {
                pb = new YJ.Data.Model.ProgramBuilder();
            }

            return View(pb);
        }

        public ActionResult Set_ListField()
        {
            return Set_ListField(null);
        }
        [HttpPost]
        public ActionResult Set_ListField(FormCollection form)
        {
            YJ.Platform.ProgramBuilderFields PBF = new YJ.Platform.ProgramBuilderFields();
            List<YJ.Data.Model.ProgramBuilderFields> fieldList = new List<YJ.Data.Model.ProgramBuilderFields>();
            string pid = Request.QueryString["pid"];

            if (!Request.Form["delete"].IsNullOrEmpty())
            {
                string[] ids = (Request.Form["checkbox_app"] ?? "").Split(',');
                foreach (string id in ids)
                {
                    PBF.Delete(id.ToGuid());
                }
                YJ.Platform.Log.Add("删除了应用程序设计字段", pid, YJ.Platform.Log.Types.其它分类);
            }
            
            fieldList = PBF.GetAll(pid.ToGuid());
            string maxSort = fieldList.Count > 0 ? fieldList.Max(p => p.Sort).ToString() : "0";
            string query = "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&typeid=" + Request.QueryString["typeid"]
                + "&Name1=" + Request.QueryString["Name1"] + "&pid=" + Request.QueryString["pid"] + "&maxSort=" + maxSort;
            ViewBag.query = query;
            return View(fieldList);
        }

        public ActionResult Set_ListField_Add()
        {
            return Set_ListField_Add(null);
        }
        [HttpPost]
        public ActionResult Set_ListField_Add(FormCollection form)
        {
            YJ.Data.Model.ProgramBuilderFields pbf = null;
            YJ.Platform.ProgramBuilderFields PBF = new YJ.Platform.ProgramBuilderFields();
            YJ.Platform.Dictionary BDict = new YJ.Platform.Dictionary();
            YJ.Platform.ProgramBuilder ProgramBuilder = new YJ.Platform.ProgramBuilder();

            string pid = Request.QueryString["pid"];
            string fid = Request.QueryString["fid"];
            string maxSort = Request.QueryString["maxSort"];
            string field = "";
            string showType = "";
            string align = "";
             ViewBag.sort  = (maxSort.ToInt() + 5).ToString();
            if (fid.IsGuid())
            {
                pbf = PBF.Get(fid.ToGuid());
                if (pbf != null)
                {
                    field = pbf.Field;
                    showType = pbf.ShowType.ToString();
                    align = pbf.Align;
                    ViewBag.sort = pbf.Sort.ToString();
                }
            }
           
            ViewBag.ShowTypeOptions = BDict.GetOptionsByCode("programbuilder_showtype", value: showType);
            ViewBag.AlignOptions = BDict.GetOptionsByCode("programbuilder_align", value: align);
            ViewBag.FieldOptions = ProgramBuilder.GetFieldsOptions(pid.ToGuid(), field);

            if (form != null)
            {
                string Field = Request.Form["Field"];
                string ShowTitle = Request.Form["ShowTitle"];
                string ShowType = Request.Form["ShowType"];
                string ShowFormat = Request.Form["ShowFormat"];
                string Align = Request.Form["Align"];
                string Width = Request.Form["Width"];
                string CustomString = Request.Form["CustomString"];
                string Sort = Request.Form["Sort"];

                bool isadd = false;
                if (pbf == null)
                {
                    isadd = true;
                    pbf = new YJ.Data.Model.ProgramBuilderFields();
                    pbf.ID = Guid.NewGuid();
                    pbf.ProgramID = pid.ToGuid();
                }

                pbf.Align = Align;
                pbf.CustomString = CustomString;
                pbf.Field = Field;
                pbf.ShowFormat = ShowFormat;
                pbf.ShowTitle = ShowTitle.IsNullOrEmpty() ? "" : ShowTitle;
                pbf.ShowType = ShowType.ToInt();
                pbf.Sort = Sort.ToInt();
                pbf.Width = Width;

                if (isadd)
                {
                    PBF.Add(pbf);
                }
                else
                {
                    PBF.Update(pbf);
                }
                ViewBag.script = "alert('保存成功!');window.location='Set_ListField" + Request.Url.Query + "';";
            }

            if (pbf == null)
            {
                pbf = new YJ.Data.Model.ProgramBuilderFields();
            }
            return View(pbf);
        }

        public ActionResult Set_Query()
        {
            return Set_Query(null);
        }
        [HttpPost]
        public ActionResult Set_Query(FormCollection form)
        {
            List<YJ.Data.Model.ProgramBuilderQuerys> fieldList = new List<YJ.Data.Model.ProgramBuilderQuerys>();
            YJ.Platform.ProgramBuilderQuerys Querys = new YJ.Platform.ProgramBuilderQuerys();
            string maxSort = string.Empty;
            string pid = string.Empty;

            pid = Request.QueryString["pid"];

            if (!Request.Form["delete"].IsNullOrEmpty())
            {
                string[] ids = (Request.Form["checkbox_app"] ?? "").Split(',');
                foreach (string id in ids)
                {
                    Querys.Delete(id.ToGuid());
                }
                YJ.Platform.Log.Add("删除了应用程序设计查询", pid, YJ.Platform.Log.Types.其它分类);
            }

            fieldList = Querys.GetAll(pid.ToGuid());
            maxSort = fieldList.Count > 0 ? fieldList.Max(p => p.Sort).ToString() : "0";
            string query = "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&typeid=" + Request.QueryString["typeid"]
                + "&Name1=" + Request.QueryString["Name1"] + "&pid=" + Request.QueryString["pid"] + "&maxSort=" + maxSort;
            ViewBag.query = query;
            return View(fieldList);
        }

        public ActionResult Set_Query_Add()
        {
            return Set_Query_Add(null);
        }
        [HttpPost]
        public ActionResult Set_Query_Add(FormCollection form)
        {
            YJ.Platform.ProgramBuilder ProgramBuilder = new YJ.Platform.ProgramBuilder();
            YJ.Platform.Dictionary BDict = new YJ.Platform.Dictionary();
            string pid = string.Empty;
            string queryid = string.Empty;
            YJ.Data.Model.ProgramBuilderQuerys pbq = null;
            YJ.Platform.ProgramBuilderQuerys PBQ = new YJ.Platform.ProgramBuilderQuerys();

            pid = Request.QueryString["pid"];
            queryid = Request.QueryString["queryid"];
            string maxSort = ((Request.QueryString["maxSort"] ?? "0").ToInt() + 5).ToString();
            string field = "";
            string operators = "";
            string inputtype = "";
            string datasource = "";
            string linkid = "";
            
            if (queryid.IsGuid())
            {
                pbq = PBQ.Get(queryid.ToGuid());
                if (pbq != null)
                {
                    field = pbq.Field;
                    operators = pbq.Operators;
                    inputtype = pbq.InputType.ToString();
                    datasource = pbq.DataSource.ToString();
                    linkid = pbq.DataLinkID;
                    maxSort = pbq.Sort.ToString();
                }
            }
            ViewBag.sort = maxSort.ToString();
            ViewBag.FieldOptions = ProgramBuilder.GetFieldsOptions(pid.ToGuid(), field);
            ViewBag.OperatorsOptions = BDict.GetOptionsByCode("programbuilder_operators", value: operators);
            ViewBag.InputTypeOptions = BDict.GetOptionsByCode("programbuilder_inputtype", value: inputtype);
            ViewBag.ControlHidden = YJ.Utility.Tools.GetRandomString(6);
            ViewBag.DataSource = BDict.GetRadiosByCode("programbuilder_datasource", "DataSource", value: datasource, attr: "onclick=\"dataSourceChange(this.value)\";");
            ViewBag.DataSource_String_SQL_LinkOptions = new YJ.Platform.DBConnection().GetAllOptions(linkid);

            if (form != null)
            {
                string Field = Request.Form["Field"];
                string ShowTitle = Request.Form["ShowTitle"];
                string ControlName = Request.Form["ControlName"];
                string Operators = Request.Form["Operators"];
                string InputType = Request.Form["InputType"];
                string Width = Request.Form["Width"];
                string Sort = Request.Form["Sort"];
                string DataSource = Request.Form["DataSource"];
                string DataSource_String_SQL_Link = Request.Form["DataSource_String_SQL_Link"];

                bool isadd = false;
                if (pbq == null)
                {
                    isadd = true;
                    pbq = new YJ.Data.Model.ProgramBuilderQuerys();
                    pbq.ID = Guid.NewGuid();
                    pbq.ProgramID = pid.ToGuid();
                }
                pbq.ControlName = ControlName;
                pbq.Field = Field;
                pbq.InputType = InputType.ToInt();
                pbq.Operators = Operators;
                pbq.ShowTitle = ShowTitle;
                pbq.Sort = Sort.ToInt();
                pbq.Width = Width;
                pbq.DataLinkID = DataSource_String_SQL_Link;

                if (pbq.InputType == 6)
                {
                    if (DataSource.IsInt())
                    {
                        pbq.DataSource = DataSource.ToInt();
                        if (pbq.DataSource == 1 || pbq.DataSource == 3)
                        {
                            pbq.DataSourceString = Request.Form["DataSource_String"];
                        }
                        else if (pbq.DataSource == 2)
                        {
                            pbq.DataSourceString = Request.Form["DataSource_Dict"];
                        }
                    }
                    else
                    {
                        pbq.DataSource = null;
                    }
                }
                else if (pbq.InputType == 8)
                {
                    pbq.DataSourceString = Request.Form["DataSource_Dict_Value"];
                }
                else if (pbq.InputType == 7)
                {
                    string DataSource_Organize_Range = Request.Form["DataSource_Organize_Range"];
                    string DataSource_Organize_Type_Unit = Request.Form["DataSource_Organize_Type_Unit"];
                    string DataSource_Organize_Type_Dept = Request.Form["DataSource_Organize_Type_Dept"];
                    string DataSource_Organize_Type_Station = Request.Form["DataSource_Organize_Type_Station"];
                    string DataSource_Organize_Type_WorkGroup = Request.Form["DataSource_Organize_Type_WorkGroup"];
                    string DataSource_Organize_Type_Role = Request.Form["DataSource_Organize_Type_Role"];
                    string DataSource_Organize_Type_User = Request.Form["DataSource_Organize_Type_User"];
                    string DataSource_Organize_Type_More = Request.Form["DataSource_Organize_Type_More"];
                    string DataSource_Organize_Type_QueryUsers = Request.Form["DataSource_Organize_Type_QueryUsers"];
                    pbq.DataSourceString = DataSource_Organize_Range + "|" +
                        DataSource_Organize_Type_Unit + "|" +
                        DataSource_Organize_Type_Dept + "|" +
                        DataSource_Organize_Type_Station + "|" +
                        DataSource_Organize_Type_WorkGroup + "|" +
                        DataSource_Organize_Type_Role + "|" +
                        DataSource_Organize_Type_User + "|" +
                        DataSource_Organize_Type_More;
                    pbq.IsQueryUsers = DataSource_Organize_Type_QueryUsers.ToInt(0);
                }


                if (isadd)
                {
                    PBQ.Add(pbq);
                }
                else
                {
                    PBQ.Update(pbq);
                }
                ViewBag.script = "alert('保存成功!');window.location='Set_Query" + Request.Url.Query + "';";
            }

            if (pbq == null)
            {
                pbq = new YJ.Data.Model.ProgramBuilderQuerys();
            }
            return View(pbq);
        }

        public ActionResult Set_Button()
        {
            return Set_Button(null);
        }
        [HttpPost]
        public ActionResult Set_Button(FormCollection form)
        {
            string query = string.Empty;
            string maxSort = string.Empty;
            List<YJ.Data.Model.ProgramBuilderButtons> buttons = new List<YJ.Data.Model.ProgramBuilderButtons>();
            YJ.Platform.ProgramBuilderButtons PBB = new YJ.Platform.ProgramBuilderButtons();
            string pid = Request.QueryString["pid"];

            if (form != null)
            {
                if (!Request.Form["delete"].IsNullOrEmpty())
                {
                    string checkbox_app = Request.Form["checkbox_app"] ?? "";
                    foreach (string bid in checkbox_app.Split(','))
                    {
                        if (bid.IsGuid())
                        {
                            PBB.Delete(bid.ToGuid());
                        }
                    }
                }
            }

            buttons = PBB.GetAll(pid.ToGuid()).OrderBy(p=>p.ShowType).ThenBy(p => p.Sort).ToList();
            maxSort = buttons.Count > 0 ? (buttons.Max(p => p.Sort) + 5).ToString() : "0";
            query = "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&typeid=" + Request.QueryString["typeid"]
                + "&Name1=" + Request.QueryString["Name1"] + "&pid=" + Request.QueryString["pid"] + "&maxSort=" + maxSort;
            ViewBag.query = query;

            return View(buttons);
        }

        public ActionResult Set_Button_Add()
        {
            return Set_Button_Add(null);
        }
        [HttpPost]
        public ActionResult Set_Button_Add(FormCollection form)
        {
            YJ.Platform.ProgramBuilderButtons PBB = new YJ.Platform.ProgramBuilderButtons();
            YJ.Data.Model.ProgramBuilderButtons pbb = null;
            string bid = string.Empty;
            string pid = string.Empty;
            bid = Request.QueryString["bid"];
            pid = Request.QueryString["pid"];
            
            string maxSort = Request.QueryString["maxSort"];
            if (bid.IsGuid())
            {
                pbb = PBB.Get(bid.ToGuid());
            }

            if (form != null)
            {
                string buttonname = Request.Form["buttonname"];
                string ClientScript = Request.Form["ClientScript"];
                string Sort = Request.Form["Sort"];
                string buttonid = Request.Form["buttonid"];
                string Ico = Request.Form["Ico"];
                string showtype = Request.Form["showtype"];
                string IsValidateShow = Request.Form["IsValidateShow"];

                bool isAdd = false;
                if (pbb == null)
                {
                    isAdd = true;
                    pbb = new YJ.Data.Model.ProgramBuilderButtons();
                    pbb.ID = Guid.NewGuid();
                    pbb.ProgramID = pid.ToGuid();
                }
                pbb.ButtonName = buttonname;
                pbb.ClientScript = ClientScript;
                pbb.Sort = Sort.ToInt(0);
                pbb.IsValidateShow = IsValidateShow.ToInt(1);
                if (buttonid.IsGuid())
                {
                    pbb.ButtonID = buttonid.ToGuid();
                }
                pbb.Ico = Ico;
                pbb.ShowType = showtype.ToInt(1);

                if (isAdd)
                {
                    PBB.Add(pbb);
                }
                else
                {
                    PBB.Update(pbb);
                }
                ViewBag.script = "alert('保存成功!');window.location='Set_Button" + Request.Url.Query + "';";
            }
            if (pbb == null)
            {
                pbb = new YJ.Data.Model.ProgramBuilderButtons();
                pbb.Sort = maxSort.ToInt();
                pbb.ShowType = 1;
            }

            return View(pbb);
        }

        public ActionResult Set_Validate()
        {
            return Set_Validate(null);
        }
        [HttpPost]
        public ActionResult Set_Validate(FormCollection form)
        {
            string pid = Request.QueryString["pid"];
            YJ.Platform.ProgramBuilderValidates PBV = new YJ.Platform.ProgramBuilderValidates();
            List<YJ.Data.Model.ProgramBuilderValidates> validateList = new List<YJ.Data.Model.ProgramBuilderValidates>();
            List<YJ.Data.Model.ProgramBuilderValidates> validateList1 = new List<YJ.Data.Model.ProgramBuilderValidates>();
            List<Tuple<string, string, string>> filedList = new List<Tuple<string, string, string>>();
            #region 从表单设置中加载表字段
            if (pid.IsGuid())
            {
                var pro = new YJ.Platform.ProgramBuilder().Get(pid.ToGuid());
                if (pro != null && !pro.FormID.IsNullOrEmpty() && pro.FormID.IsGuid())
                {
                    
                    var applibary = new YJ.Platform.AppLibrary().Get(pro.FormID.ToGuid());
                    if (applibary != null && applibary.Code.IsGuid())
                    {
                        var proform = new YJ.Platform.WorkFlowForm().Get(applibary.Code.ToGuid());
                        if (proform != null)
                        {
                            LitJson.JsonData formAttr = LitJson.JsonMapper.ToObject(proform.Attribute);
                            string dbconn = formAttr.ContainsKey("dbconn") ? formAttr["dbconn"].ToString() : "";
                            string dbtable = formAttr.ContainsKey("dbtable") ? formAttr["dbtable"].ToString() : "";
                            if (dbconn.IsGuid() && !dbtable.IsNullOrEmpty())
                            {
                                var mainTableFields = new YJ.Platform.DBConnection().GetFields(dbconn.ToGuid(), dbtable);
                                foreach (var field in mainTableFields)
                                {
                                    filedList.Add(new Tuple<string, string, string>(dbtable, field.Key, field.Value));
                                }
                            }
                            LitJson.JsonData subtables = LitJson.JsonMapper.ToObject(proform.SubTableJson);
                            if (subtables.IsArray)
                            {
                                foreach (LitJson.JsonData jd in subtables)
                                {
                                    string secondtable = jd.ContainsKey("secondtable") ? jd["secondtable"].ToString() : "";
                                    if (jd.ContainsKey("colnums"))
                                    {
                                        LitJson.JsonData colnums = jd["colnums"];
                                        if (colnums.IsArray)
                                        {
                                            foreach (LitJson.JsonData col in colnums)
                                            {
                                                string fieldname = col.ContainsKey("fieldname") ? col["fieldname"].ToString() : "";
                                                string showname = col.ContainsKey("showname") ? col["showname"].ToString() : "";
                                                if (fieldname.IsNullOrEmpty())
                                                {
                                                    continue;
                                                }
                                                filedList.Add(new Tuple<string, string, string>(secondtable, fieldname, showname));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            validateList1 = PBV.GetAll(pid.ToGuid());
            foreach (var filed in filedList)
            {
                var val = validateList1.Find(p => p.TableName.Equals(filed.Item1, StringComparison.CurrentCultureIgnoreCase) &&
                    p.FieldName.Equals(filed.Item2, StringComparison.CurrentCultureIgnoreCase));
                validateList.Add(new YJ.Data.Model.ProgramBuilderValidates()
                {
                    ID = Guid.NewGuid(),
                    ProgramID = pid.ToGuid(),
                    TableName = filed.Item1,
                    FieldName = filed.Item2,
                    FieldNote = filed.Item3,
                    Validate = val != null ? val.Validate : 0
                });
            }

            if (form != null)
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    PBV.DeleteByProgramID(pid.ToGuid());
                    foreach (var val in validateList)
                    {
                        val.Validate = Request.Form["valdate_" + val.TableName + "_" + val.FieldName].ToInt(0);
                        PBV.Add(val);
                    }
                    scope.Complete();
                }
            }

            return View(validateList);
        }

        public ActionResult Set_Export()
        {
            return Set_Export(null);
        }
        [HttpPost]
        public ActionResult Set_Export(FormCollection form)
        {
            string query = string.Empty;
            List<YJ.Data.Model.ProgramBuilderExport> fieldList = new List<YJ.Data.Model.ProgramBuilderExport>();
            string pid = Request.QueryString["pid"];
            string maxSort = string.Empty;

            YJ.Platform.ProgramBuilderExport PBE = new YJ.Platform.ProgramBuilderExport();

            if (form != null)
            {
                if (!Request.Form["delete"].IsNullOrEmpty())
                {
                    string checkbox_app = Request.Form["checkbox_app"] ?? "";
                    foreach (string bid in checkbox_app.Split(','))
                    {
                        if (bid.IsGuid())
                        {
                            PBE.Delete(bid.ToGuid());
                        }
                    }
                }
            }

            fieldList = PBE.GetAll(pid.ToGuid());
            maxSort = fieldList.Count > 0 ? fieldList.Max(p => p.Sort).ToString() : "0";
            query = "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&typeid=" + Request.QueryString["typeid"]
                + "&Name1=" + Request.QueryString["Name1"] + "&pid=" + Request.QueryString["pid"] + "&maxSort=" + maxSort;

            ViewBag.query = query;

            return View(fieldList);
        }

        public ActionResult Set_Export_Add()
        {
            return Set_Export_Add(null);
        }
        [HttpPost]
        public ActionResult Set_Export_Add(FormCollection form)
        {
            YJ.Data.Model.ProgramBuilderExport pbe = null;
            YJ.Platform.ProgramBuilderExport PBE = new YJ.Platform.ProgramBuilderExport();
            YJ.Platform.Dictionary BDict = new YJ.Platform.Dictionary();
            YJ.Platform.ProgramBuilder ProgramBuilder = new YJ.Platform.ProgramBuilder();

            string pid = Request.QueryString["pid"];
            string eid = Request.QueryString["eid"];
            string maxSort = Request.QueryString["maxSort"];
            string field = "";
            string showType = "";
            string align = "";
            string dataType = "";
            ViewBag.sort = (maxSort.ToInt() + 5).ToString();
            if (eid.IsGuid())
            {
                pbe = PBE.Get(eid.ToGuid());
                if (pbe != null)
                {
                    field = pbe.Field;
                    showType = pbe.ShowType.ToString();
                    align = pbe.Align.ToString();
                    ViewBag.sort = pbe.Sort.ToString();
                    dataType = pbe.DataType.ToString();
                }
            }

            ViewBag.ShowTypeOptions = BDict.GetOptionsByCode("programbuilder_showtype", value: showType);
            ViewBag.AlignOptions = BDict.GetOptionsByCode("programbuilder_align", value: align);
            ViewBag.FieldOptions = ProgramBuilder.GetFieldsOptions(pid.ToGuid(), field);
            ViewBag.DataTypeOptions = PBE.GetDataTypeOptions(dataType);

            if (form != null)
            {
                string Field = Request.Form["Field"];
                string ShowTitle = Request.Form["ShowTitle"];
                string ShowType = Request.Form["ShowType"];
                string ShowFormat = Request.Form["ShowFormat"];
                string Align = Request.Form["Align"];
                string Width = Request.Form["Width"];
                string CustomString = Request.Form["CustomString"];
                string Sort = Request.Form["Sort"];
                string DataType = Request.Form["DataType"];

                bool isadd = false;
                if (pbe == null)
                {
                    isadd = true;
                    pbe = new YJ.Data.Model.ProgramBuilderExport();
                    pbe.ID = Guid.NewGuid();
                    pbe.ProgramID = pid.ToGuid();
                }
                pbe.Align = "left" == Align ? 0 : "center" == Align ? 1 : 2;
                pbe.CustomString = CustomString;
                pbe.Field = Field;
                pbe.ShowFormat = ShowFormat;
                pbe.ShowTitle = ShowTitle;
                pbe.ShowType = ShowType.ToInt();
                pbe.Sort = Sort.ToInt();
                if (Width.IsInt())
                {
                    pbe.Width = Width.ToInt();
                }
                else
                {
                    pbe.Width = null;
                }
                pbe.DataType = DataType.ToInt(0);

                if (isadd)
                {
                    PBE.Add(pbe);
                }
                else
                {
                    PBE.Update(pbe);
                }
                ViewBag.script = "alert('保存成功!');window.location='Set_Export" + Request.Url.Query + "';";
            }

            if (pbe == null)
            {
                pbe = new YJ.Data.Model.ProgramBuilderExport();
            }
            return View(pbe);
        }
        /// <summary>
        /// 复制列表字段为导出字段
        /// </summary>
        /// <returns></returns>
        public string Set_Export_CopyForList()
        {
            Guid pid = Request.QueryString["pid"].ToGuid();
            if (pid.IsEmptyGuid())
            {
                return "参数错误";
            }
            var listFileds = new YJ.Platform.ProgramBuilderFields().GetAll(pid);
            YJ.Platform.ProgramBuilderExport pbe = new YJ.Platform.ProgramBuilderExport();
            var pbes = pbe.GetAll(pid);
            foreach (var p in pbes)
            {
                pbe.Delete(p.ID);
            }
            foreach (var filed in listFileds)
            {
                if (filed.Field.IsNullOrEmpty())
                {
                    continue;
                }
                YJ.Data.Model.ProgramBuilderExport mpbe = new YJ.Data.Model.ProgramBuilderExport();
                mpbe.Align = "left" == filed.Align ? 0 : "center" == filed.Align ? 1 : 2;
                mpbe.CustomString = filed.CustomString;
                mpbe.DataType = 0;
                mpbe.Field = filed.Field;
                mpbe.ID = Guid.NewGuid();
                mpbe.ProgramID = pid;
                mpbe.ShowFormat = filed.ShowFormat;
                mpbe.ShowTitle = filed.ShowTitle;
                mpbe.ShowType = filed.ShowType;
                mpbe.Sort = filed.Sort;
                
                pbe.Add(mpbe);
            }
            return "复制成功";
        }

        [MyAttribute(CheckApp=false, CheckUrl=false)]
        public string GetFieldsOptions()
        {
            string appid = Request["applibaryid"];
            var app = new YJ.Platform.AppLibrary().Get(appid.ToGuid());
            if (app == null || !app.Code.IsGuid())
            {
                return "";
            }
            var options = new YJ.Platform.ProgramBuilder().GetFieldsOptions(app.Code.ToGuid(), "");
            return options;
        
        }

        public string Publish()
        {
            string id = Request.QueryString["pid"];
            if (!id.IsGuid()) return "成功失败!";
            if (new YJ.Platform.ProgramBuilder().Publish(id.ToGuid(), true))
            {
                return "成功发布!";
            }
            else
            {
                return "成功失败!";
            }
        }

        [MyAttribute(CheckApp=false, CheckLogin = false, CheckUrl = false)]
        public ActionResult Run()
        {
            return Run(null);
        }
        [HttpPost]
        [MyAttribute(CheckApp = false, CheckLogin = false, CheckUrl = false)]
        public ActionResult Run(FormCollection form)
        {
            if (!Common.Tools.CheckLogin() && !YJ.Platform.WeiXin.Organize.CheckLogin())
            {
                Response.End();
                return null;
            }
            YJ.Data.Model.ProgramBuilderCache PBModel = null;
            YJ.Platform.ProgramBuilder PB = new YJ.Platform.ProgramBuilder();
            YJ.Platform.ProgramBuilderQuerys PBQ = new YJ.Platform.ProgramBuilderQuerys();
            string pid = string.Empty;
            System.Data.DataTable Dt = null;
            YJ.Platform.DBConnection DBConn = new YJ.Platform.DBConnection();
            YJ.Platform.Dictionary BDict = new YJ.Platform.Dictionary();
            YJ.Platform.Organize BOrganize = new YJ.Platform.Organize();
            string Query = string.Empty;
            string PrevUrl = string.Empty;

            pid = Request.QueryString["programid"];
            Guid pguid;
            if (pid.IsGuid(out pguid))
            {
                PBModel = PB.GetSet(pguid);
            }
            if (PBModel == null)
            {
                Response.Write("未找到程序设置!");
                Response.End();
                return null;
            }

            if (form!=null && !Request.Form["searchbutton"].IsNullOrEmpty())
            {
                foreach (var query in PBModel.Querys)
                {
                    if (query.InputType.In(3, 5))//日期时间范围
                    {
                        query.Value = Request.Form[query.ControlName + "_start"].Trim1() + "," + Request.Form[query.ControlName + "_end"].Trim1();
                    }
                    else
                    {
                        query.Value = Request.Form[query.ControlName].Trim1();
                    }
                }
            }
            else
            {
                foreach (var query in PBModel.Querys)
                {
                    if (query.InputType.In(3, 5))//日期时间范围
                    {
                        query.Value = Request.QueryString[query.ControlName + "_start"].Trim1() + "," + Request.QueryString[query.ControlName + "_end"].Trim1();
                    }
                    else
                    {
                        query.Value = Request.QueryString[query.ControlName].Trim1();
                    }
                }
            }

            Query = "&programid=" + pid + "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"];
            foreach (var query in PBModel.Querys)
            {
                if (query.InputType.In(3, 5))//日期时间范围
                {
                    string[] val = query.Value.Split(',');
                    Query += "&" + query.ControlName + "_start=" + val[0] + "&" + query.ControlName + "_end=" + val[1];
                }
                else
                {
                    Query += "&" + query.ControlName + "=" + query.Value;
                }
            }
            PrevUrl = (Common.Tools.BaseUrl + "/ProgramBuilder/Run?1=1" + Query).UrlEncode();
            string pager;
            StringBuilder sql = new StringBuilder(PBModel.Program.SQL);
            List<IDbDataParameter> parList = new List<IDbDataParameter>();
            var dbconn = new YJ.Platform.DBConnection().Get(PBModel.Program.DBConnID);
            if (dbconn == null)
            {
                Response.Write("未找到数据连接!");
                Response.End();
                return null;
            }
            string parameter = string.Empty;
            switch (dbconn.Type)
            {
                case "SqlServer":
                case "MySql":
                    parameter = "@";
                    break;
                case "Oracle":
                    parameter = ":";
                    break;
            }
            foreach (var query in PBModel.Querys)
            {
                if (query.Value.IsNullOrEmpty())
                {
                    continue;
                }
                string queryValue = query.Value.ReplaceSelectSql();
                string operatiors = query.Operators;
                if (query.InputType == 7 && query.IsQueryUsers == 1)//查询时将组织组织机构转换为人员
                {
                    queryValue = new YJ.Platform.Organize().GetAllUsersIdList(queryValue).ToArray().Join1();
                }
                switch (operatiors)
                {
                    case "%LIKE%":
                        sql.AppendFormat(" AND {0} LIKE '%{1}%'", query.Field, queryValue);
                        break;
                    case "LIKE%":
                        sql.AppendFormat(" AND {0} LIKE '{1}%'", query.Field, queryValue);
                        break;
                    case "%LIKE":
                        sql.AppendFormat(" AND {0} LIKE '%{1}'", query.Field, queryValue);
                        break;
                    case "IN":
                        sql.AppendFormat(" AND {0} IN({1})", query.Field, YJ.Utility.Tools.GetSqlInString(queryValue));
                        break;
                    case "NOT IN":
                        sql.AppendFormat(" AND {0} NOT IN({1})", query.Field, YJ.Utility.Tools.GetSqlInString(queryValue));
                        break;
                    default:
                        if (query.InputType.In(3, 5))//日期时间范围
                        {
                            string[] val = queryValue.Split(',');
                            if (val[0].IsDateTime())
                            {
                                val[0] = query.InputType == 3 ? val[0].ToDateString() : val[0].ToDateTimeString();
                                sql.AppendFormat(" AND {0}{1}{2}{3}_start", query.Field, operatiors, parameter, query.ControlName);
                                parList.Add(new SqlParameter(parameter + query.ControlName + "_start", val[0]));
                            }
                            if (val[1].IsDateTime())
                            {
                                val[1] = query.InputType == 3 ? val[1].ToDateTime().AddDays(1).ToDateString() : val[1].ToDateTimeString();
                                sql.AppendFormat(" AND {0}{1}{2}{3}_end", query.Field, operatiors == ">" ? "<" : "<=", parameter, query.ControlName);
                                parList.Add(new SqlParameter(parameter + query.ControlName + "_end", val[1]));
                            }
                        }
                        else
                        {
                            sql.AppendFormat(" AND {0}{1}{2}{3}", query.Field, operatiors, parameter, query.ControlName);
                            parList.Add(new SqlParameter(parameter + query.ControlName, queryValue));
                        }
                        break;
                }
            }
            string querySql = sql.ToString().FilterWildcard(YJ.Platform.Users.CurrentUserID.ToString());
            Dt = DBConn.GetDataTable(dbconn, querySql, out pager, Query, parList, PBModel.Program.IsPager.HasValue && PBModel.Program.IsPager.Value == 0 ? -1 : 0);
            ViewBag.pager = pager;
            ViewBag.PBModel = PBModel;
            ViewBag.query = Query;
            ViewBag.prevurl = PrevUrl;
            PB.AddToExportCache(PBModel.Program.ID, dbconn.ID, querySql, parList);
            if (Dt == null)
            {
                Response.Write("查询错误!");
                return View(Dt);
            }

            return View(Dt);
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult RunDelete()
        {
            string formid = Request.QueryString["secondtableeditform"];
            string instanceids = Request.QueryString["instanceid"] ?? "";
            var app = new YJ.Platform.AppLibrary().Get(formid.ToGuid());
            if (app != null)
            {
                var wform = new YJ.Platform.WorkFlowForm().Get(app.Code.ToGuid());
                if (wform != null)
                {
                    YJ.Platform.DBConnection dbconn = new YJ.Platform.DBConnection();
                    LitJson.JsonData json = LitJson.JsonMapper.ToObject(wform.Attribute);
                    var dbconnid = json.ContainsKey("dbconn") ? json["dbconn"].ToString() : "";
                    var dbtable = json.ContainsKey("dbtable") ? json["dbtable"].ToString() : "";
                    var dbtablepk = json.ContainsKey("dbtablepk") ? json["dbtablepk"].ToString() : "";

                    if (dbconnid.IsGuid() && !dbtable.IsNullOrEmpty() && !dbtablepk.IsNullOrEmpty())
                    {
                        foreach (string delid in (instanceids ?? "").Split(','))
                        {
                            var dt = dbconn.GetDataTable(dbconnid, dbtable, dbtablepk, delid);
                            if (dt.Rows.Count > 0)
                            {
                                dbconn.DeleteData(dbconnid.ToGuid(), dbtable, dbtablepk, delid);
                                YJ.Platform.Log.Add("删除了数据(生成程序)(" + dbtable + ")", "连接ID:" + dbconnid + ",表名:" + dbtable + ",主键:" + delid, YJ.Platform.Log.Types.其它分类, dt.ToJsonString());
                            }
                        }
                    }
                }
            }
            ViewBag.script = "alert('删除成功!');window.location='" + Request.QueryString["prevurl"] + "'";
            return View();
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult OutToExcel()
        {
            return View();
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult InFromExcel()
        {
            return InFromExcel(null);
        }

        [HttpPost]
        [MyAttribute(CheckApp = false,CheckUrl =false)]
        [ValidateAntiForgeryToken]
        public ActionResult InFromExcel(FormCollection coll)
        {
            string programid = Request.QueryString["programid"];
            int type = (Request.QueryString["type"]??"0").ToInt32();
            var program = new YJ.Platform.ProgramBuilder().Get(programid.ToGuid());
            YJ.Platform.DBConnection dbConn = new YJ.Platform.DBConnection();
            ViewBag.TableOptions = dbConn.GetAllTableOptions(program.DBConnID, "");
            ViewBag.ConnID = program.DBConnID.ToString();
            ViewBag.TableName = program.TableName;
            ViewBag.NumberFiled = program.InDataNumberFiledName;
            if (coll != null)
            {
                HttpPostedFileBase file = Request.Files["excel"];
                if (file != null && !file.FileName.IsNullOrEmpty())
                {
                    string numberFiled = Request.Form["NumberFiled"];
                    string tableName = Request.Form["TableName"];
                    string filePath = Server.MapPath(Url.Content("~/Content/UploadFiles/ProgramInExcel/" + programid + "/" + Guid.NewGuid().ToString()));
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filePath1 = System.IO.Path.Combine(filePath, file.FileName);
                    file.SaveAs(filePath1);
                    string msg;
                    int count = 0;
                    switch (type)
                    {
                        //正常导入
                        case 0:
                            count = new YJ.Platform.ProgramBuilder().InDataFormExcel(programid.ToGuid(), tableName, filePath1, out msg, numberFiled);
                            ViewBag.script = "alert('" + (msg.IsNullOrEmpty() ? "本次共导入了" + count.ToString() + "条数据!" : msg) + "');new RoadUI.Window().close();";
                            break;
                         //自定义字段导入
                        case 1:
                            count = new YJ.Platform.ProgramBuilder().InDataFormExcel1(programid.ToGuid(), tableName, filePath1, out msg, numberFiled);
                            ViewBag.script = "alert('" + (msg.IsNullOrEmpty() ? "本次共导入了" + count.ToString() + "条数据!" : msg) + "');new RoadUI.Window().close();";
                            break;
                         //样品报批导入
                        case 2:
                            count = new Areas.AssetManage.Data.Business.AmSample().InExcelData( filePath1, out msg);
                            ViewBag.script = "alert('" + (msg.IsNullOrEmpty() ? "本次共导入了" + count.ToString() + "条数据!" : msg) + "');new RoadUI.Window().close();";
                            break;
                        //固定资产（办公用户）导入
                        case 3:
                            count = new Areas.AssetManage.Data.Business.AmSample().InExcelData1(filePath1, out msg);
                            ViewBag.script = "alert('" + (msg.IsNullOrEmpty() ? "本次共导入了" + count.ToString() + "条数据!" : msg) + "');new RoadUI.Window().close();";
                            break;
                        //固定资产（仪器）导入
                        case 4:
                            count = new Areas.AssetManage.Data.Business.AmSample().InExcelData2(filePath1, out msg);
                            ViewBag.script = "alert('" + (msg.IsNullOrEmpty() ? "本次共导入了" + count.ToString() + "条数据!" : msg) + "');new RoadUI.Window().close();";
                            break;
                        default:
                            break;
                    }
                   
                    
                }
                else
                {
                    ViewBag.script = "alert('要导入的Excel文件为空!');";
                }
            }
            return View();
        }
    }
}
