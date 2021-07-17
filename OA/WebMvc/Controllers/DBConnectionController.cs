using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text;

namespace WebMvc.Controllers
{
    public class DBConnectionController : MyController
    {
        //
        // GET: /DBConnection/

        public ActionResult Index()
        {
            string query1 = string.Format("&appid={0}&tabid={1}", Request.QueryString["appid"], Request.QueryString["tabid"]);
            YJ.Platform.DBConnection bdbconn = new YJ.Platform.DBConnection();
            var connList = bdbconn.GetAll();
            LitJson.JsonData json = new LitJson.JsonData();
            foreach (var conn in connList)
            {
                LitJson.JsonData j = new LitJson.JsonData();
                j["id"] = conn.ID.ToString();
                j["Name"] = conn.Name;
                j["Type"] = conn.Type;
                j["ConnectionString"] = conn.ConnectionString;
                j["Note"] = conn.Note;
                j["Opation"] = "<a class=\"editlink\" href=\"javascript:edit('" + conn.ID + "');\">编辑</a>" +
                "<a onclick=\"test('" + conn.ID + "');\" style=\"background:url(" + Url.Content("~/Images/ico/hammer_screwdriver.png") + ") no-repeat left center; padding-left:18px; margin-left:5px;\" href=\"javascript:void(0);\">测试</a>" +
                "<a onclick=\"table('" + conn.ID + "','" + conn.Name + "');\" style=\"background:url(" + Url.Content("~/Images/ico/topic_search.gif") + ") no-repeat left center; padding-left:18px; margin-left:5px;\" href=\"javascript:void(0);\">管理表</a>";
                json.Add(j);
            }
            ViewBag.Query1 = query1;
            ViewBag.list = json.ToJson();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Delete()
        {
            YJ.Platform.DBConnection bdbconn = new YJ.Platform.DBConnection();
            string deleteID = Request.Form["ids"];
            System.Text.StringBuilder delxml = new System.Text.StringBuilder();
            foreach (string id in deleteID.Split(','))
            {
                Guid gid;
                if (id.IsGuid(out gid))
                {
                    delxml.Append(bdbconn.Get(gid).Serialize());
                    bdbconn.Delete(gid);
                }
            }
            bdbconn.ClearCache();
            YJ.Platform.Log.Add("删除了数据连接", delxml.ToString(), YJ.Platform.Log.Types.流程相关);
            return "删除成功!";
        }

        public ActionResult Edit()
        {
            return Edit(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection collection)
        {
            string editid = Request.QueryString["id"];
            YJ.Platform.DBConnection bdbConn = new YJ.Platform.DBConnection();
            YJ.Data.Model.DBConnection dbconn = null;
            if (editid.IsGuid())
            {
                dbconn = bdbConn.Get(editid.ToGuid());
            }
            bool isAdd = !editid.IsGuid();
            string oldXML = string.Empty;
            if (dbconn == null)
            {
                dbconn = new YJ.Data.Model.DBConnection();
                dbconn.ID = Guid.NewGuid();
            }
            else
            {
                oldXML = dbconn.Serialize();
            }

            if (collection != null)
            {
                string Name = Request.Form["Name"];
                string LinkType = Request.Form["LinkType"];
                string ConnStr = Request.Form["ConnStr"];
                string Note = Request.Form["Note"];
                dbconn.Name = Name.Trim();
                dbconn.Type = LinkType;
                dbconn.ConnectionString = ConnStr;
                dbconn.Note = Note;

                if (isAdd)
                {
                    bdbConn.Add(dbconn);
                    YJ.Platform.Log.Add("添加了数据库连接", dbconn.Serialize(), YJ.Platform.Log.Types.数据连接);
                    ViewBag.Script = "alert('添加成功!');new RoadUI.Window().reloadOpener();new RoadUI.Window().close();";
                }
                else
                {
                    bdbConn.Update(dbconn);
                    YJ.Platform.Log.Add("修改了数据库连接", "", YJ.Platform.Log.Types.数据连接, oldXML, dbconn.Serialize());
                    ViewBag.Script = "alert('修改成功!');new RoadUI.Window().reloadOpener();new RoadUI.Window().close();";
                }
                bdbConn.ClearCache();
            }

            ViewBag.TypeOptions = bdbConn.GetAllTypeOptions(dbconn.Type);

            return View(dbconn);
        }

        public string Test()
        {
            string connid = Request.QueryString["id"];
            Guid cid;
            if (!connid.IsGuid(out cid))
            {
                return "参数错误";
            }
            return new YJ.Platform.DBConnection().Test(cid);
        }

        public ActionResult Table(FormCollection collection)
        {
            string dbconnID = Request.QueryString["connid"];
            List<Tuple<string, int>> List = new List<Tuple<string, int>>();
            YJ.Platform.DBConnection DBConn = new YJ.Platform.DBConnection();
            string Query = string.Empty;
            string DBType = string.Empty;
            List<string> SystemTables = YJ.Utility.Config.SystemDataTables;
            
            if (!dbconnID.IsGuid())
            {
                Response.Write("数据连接ID错误");
                Response.End();
                return null;
            }
            var dbconn = DBConn.Get(dbconnID.ToGuid());
            if (dbconn == null)
            {
                Response.Write("未找到数据连接");
                Response.End();
                return null;
            }
            DBType = dbconn.Type;
            foreach (var table in DBConn.GetTables(dbconn.ID, 1))
            {
                List.Add(new Tuple<string, int>(table, 0));
            }
            foreach (var view in DBConn.GetTables(dbconn.ID, 2))
            {
                List.Add(new Tuple<string, int>(view, 1));
            }

            LitJson.JsonData json = new LitJson.JsonData();
            foreach (var table in List)
            {
                bool isSystemTable = SystemTables.Find(p => p.Equals(table.Item1, StringComparison.CurrentCultureIgnoreCase)) != null;
                StringBuilder sb = new StringBuilder("<a class=\"viewlink\" href=\"javascript:void(0);\" onclick=\"queryTable('" + dbconnID + "','" + table.Item1 + "');\">查询</a>");
                if (!isSystemTable && table.Item2 == 0 && "Oracle" != DBType)
                {
                    sb.Append("<a class=\"editlink\" href=\"javascript:void(0);\" onclick=\"edit('" + dbconnID + "','" + table.Item1 + "');\">修改</a>");
                    sb.Append("<a class=\"deletelink\" href=\"javascript:void(0);\" onclick=\"del('" + dbconnID + "','" + table.Item1 + "');\">删除</a>");
                }
                LitJson.JsonData j = new LitJson.JsonData();
                j["Name"] = table.Item1;
                j["Type"] = table.Item2 == 0 ? isSystemTable ? "系统表" : "表" : "视图";
                j["Opation"] = sb.ToString();
                json.Add(j);
            }
           
            Query = "&connid=" + dbconnID + "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"];
            ViewBag.Query = Query;
            ViewBag.dbconnID = dbconnID;
            ViewBag.DBType = DBType;
            ViewBag.list = json.ToJson();
            return View();
        }


        public ActionResult TableQuery()
        {
            return TableQuery(null);
        }
        [HttpPost]
        public ActionResult TableQuery(FormCollection collection)
        {
            YJ.Platform.DBConnection DBConn = new YJ.Platform.DBConnection();
            string TableName = string.Empty;
            string ConnID = string.Empty;
            YJ.Data.Model.DBConnection MDBConn = null;
            string SqlString = string.Empty;
            TableName = Request.QueryString["tablename"];
            ConnID = Request.QueryString["dbconnid"];
            MDBConn = DBConn.Get(ConnID.ToGuid());
            if (MDBConn == null)
            {
                ViewBag.LiteralResult = "未找到数据连接";
                ViewBag.LiteralResultCount.Text = "";
                return View();
            }

            if (collection != null)
            {
                SqlString = Request.Form["sqltext"];
            }
            else if (!TableName.IsNullOrEmpty())
            {
                SqlString = DBConn.GetDefaultQuerySql(MDBConn, TableName);
            }
            else
            {
                ViewBag.LiteralResult = "";
                ViewBag.LiteralResultCount = "";
                return View();
            }

            if (SqlString.IsNullOrEmpty())
            {
                ViewBag.LiteralResult = "SQL为空！";
                ViewBag.LiteralResultCount = "";
                return View();
            }
            if (!DBConn.CheckSql(SqlString))
            {
                ViewBag.LiteralResult = "SQL含有破坏系统表的语句，禁止执行！";
                ViewBag.LiteralResultCount = "";
                YJ.Platform.Log.Add("尝试执行有破坏系统表的SQL语句", SqlString, YJ.Platform.Log.Types.数据连接);
                return View();
            }
            DataTable dt = DBConn.GetDataTable(MDBConn, SqlString);
            YJ.Platform.Log.Add("执行了SQL", SqlString, YJ.Platform.Log.Types.数据连接, dt.ToJsonString());
            ViewBag.LiteralResult = YJ.Utility.Tools.DataTableToHtml(dt);
            ViewBag.LiteralResultCount = "(共" + dt.Rows.Count + "行)";
            ViewBag.sqltext = SqlString;
            return View();
        }

        public ActionResult TableDelete()
        {
            //return View();
            string tableName = Request.QueryString["tablename"];
            string connid = Request.QueryString["dbconnid"];

            string prevUrl = "Table" + Request.Url.Query; ;
            YJ.Platform.DBConnection DBConn = new YJ.Platform.DBConnection();
            var conn = DBConn.Get(connid.ToGuid());
            if (conn == null)
            {
                ViewBag.ClientScript = "alert('未找到数据连接!');window.location='" + prevUrl + "';";
                return View();
            }
            string sql = string.Empty;
            switch (conn.Type.ToLower())
            {
                case "sqlserver":
                    sql = "DROP TABLE [" + tableName + "]";
                    break;
                case "oracle":
                    sql = "DROP TABLE " + tableName + "";
                    break;
                case "mysql":
                    sql = "DROP TABLE `" + tableName + "`";
                    break;
            }

            if (YJ.Utility.Config.SystemDataTables.Find(p => p.Equals(tableName, StringComparison.CurrentCultureIgnoreCase)) != null)
            {
                ViewBag.ClientScript = "alert('不能删除系统表!');window.location='" + prevUrl + "';";
                YJ.Platform.Log.Add("删除表-不能删除系统表-" + tableName, sql, YJ.Platform.Log.Types.数据连接);
                return View();
            }

            if (conn != null)
            {
                if (DBConn.TestSql(conn, sql, false))
                {
                    ViewBag.ClientScript = "alert('删除成功!');window.location='" + prevUrl + "';";
                    YJ.Platform.Log.Add("删除表-成功-" + tableName, sql, YJ.Platform.Log.Types.数据连接);
                    return View();
                }
                else
                {
                    ViewBag.ClientScript = "alert('删除失败!');window.location='" + prevUrl + "';";
                    YJ.Platform.Log.Add("删除表-失败-" + tableName, sql, YJ.Platform.Log.Types.数据连接);
                    return View();
                }
            }
            else
            {
                ViewBag.ClientScript = "alert('未找到数据连接!');window.location='" + prevUrl + "';";
                YJ.Platform.Log.Add("删除表-未找到连接-" + tableName, sql, YJ.Platform.Log.Types.数据连接);
                return View();
            }
  
        }

        public ActionResult TableEdit_SqlServer()
        {
            return TableEdit_SqlServer(null);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TableEdit_SqlServer(FormCollection collection)
        {
            string dbconnID = string.Empty;
            string tableName = string.Empty;
            YJ.Platform.DBConnection DBConn = new YJ.Platform.DBConnection();
            DataTable SchemaDt = new DataTable();
            IDbConnection conn = null;
            List<string> PrimaryKeyList = new List<string>();
            YJ.Data.Model.DBConnection dbconn = null;
            bool IsAddTable = false;
            List<string> SystemTables = YJ.Utility.Config.SystemDataTables;
            dbconnID = Request.QueryString["dbconnid"];
            tableName = Request.QueryString["tablename"];

            if (tableName.IsNullOrEmpty())
            {
                tableName = "NEWTABLE_" + YJ.Utility.Tools.GetRandomString();
                IsAddTable = true;
            }
            if (dbconnID.IsGuid() && !tableName.IsNullOrEmpty())
            {
                dbconn = DBConn.Get(dbconnID.ToGuid());
                if (dbconn != null)
                {
                    conn = DBConn.GetConnection(dbconn);
                    if (conn != null)
                    {
                        if (conn.State != ConnectionState.Open)
                        {
                            conn.Open();
                        }
                        SchemaDt = DBConn.GetTableSchema(conn, tableName, dbconn.Type);
                        PrimaryKeyList = DBConn.GetPrimaryKey(dbconn, tableName);
                    }
                }
            }
            if (IsAddTable)
            {
                tableName = "";
            }
            if (SchemaDt.Rows.Count == 0)
            {
                DataRow dr = SchemaDt.NewRow();
                dr["f_name"] = "ID";
                dr["t_name"] = "int";
                dr["is_null"] = 0;
                dr["isidentity"] = 1;
                SchemaDt.Rows.Add(dr);
                PrimaryKeyList.Add("ID");
            }

            ViewBag.PrimaryKeyList = PrimaryKeyList;
            ViewBag.IsAddTable = IsAddTable;
            ViewBag.tableName = tableName;

            if (collection != null)
            {
                if (dbconn == null)
                {
                    ViewBag.ClientScript = "alert('未找到数据连接!');";
                    return View(SchemaDt);
                }

                string f_name = Request.Form["f_name"] ?? "";
                string[] f_nameArray = f_name.Split(',');
                string tablename = Request.Form["tablename"];
                string oldtablename = Request.Form["oldtablename"];
                string delfield = Request.Form["delfield"] ?? "";
                StringBuilder sql = new StringBuilder();
                StringBuilder sql2 = new StringBuilder();
                if (SystemTables.Find(p => p.Equals(oldtablename, StringComparison.CurrentCultureIgnoreCase)) != null)
                {
                    ViewBag.ClientScript = "alert('您不能修改系统表!');";
                    return View(SchemaDt);
                }
                if (IsAddTable)
                {
                    sql.Append("CREATE TABLE [" + tablename + "] (");
                    oldtablename = tablename;
                }
                else
                {
                    List<string> constraints = DBConn.GetConstraints(dbconn, oldtablename);
                    foreach (string constraint in constraints)
                    {
                        sql.Append("ALTER TABLE [" + oldtablename + "] DROP CONSTRAINT [" + constraint + "];");
                    }
                    StringBuilder dropColnum = new StringBuilder();
                    foreach (var del in delfield.Split(','))
                    {
                        if (!del.IsNullOrEmpty() && SchemaDt.Select("f_name='" + del + "'").Length > 0)
                        {
                            dropColnum.Append("[" + del + "],");
                        }
                    }
                    if (dropColnum.Length > 0)
                    {
                        sql.Append("ALTER TABLE [" + oldtablename + "] DROP COLUMN " + dropColnum.ToString().TrimEnd(',') + ";");
                    }
                }

                List<string> pkList = new List<string>();
                foreach (var fname in f_nameArray)
                {
                    string fieldName = Request.Form[fname + "_name1"];
                    string fieldType = Request.Form[fname + "_type"];
                    string fieldLength = Request.Form[fname + "_length"];
                    string fieldIsNull = Request.Form[fname + "_isnull"];
                    string fieldIsIdentity = Request.Form[fname + "_isidentity"];
                    string fieldPrimarykey = Request.Form[fname + "_primarykey"];
                    string fieldDefaultValue = Request.Form[fname + "_defaultvalue"];
                    string fieldIsAdd = Request.Form[fname + "_isadd"];

                    if (fieldName.IsNullOrEmpty() || fieldType.IsNullOrEmpty())
                    {
                        continue;
                    }
                    string fieldType1 = string.Empty;
                    switch (fieldType)
                    {
                        case "varchar":
                        case "nvarchar":
                            fieldType1 = fieldType + "(" + (fieldLength.IsInt() ? fieldLength.ToInt() == -1 ? "MAX" : fieldLength : "50") + ")";
                            break;
                        case "char":
                            fieldType1 = fieldType + "(" + (fieldLength.IsInt() ? fieldLength : "50") + ")";
                            break;
                        case "datetime":
                        case "text":
                        case "uniqueidentifier":
                        case "int":
                        case "money":
                        case "float":
                            fieldType1 = fieldType;
                            break;
                        case "decimal":
                            fieldType1 = fieldType + "(" + (fieldLength.IsNullOrEmpty() ? "18,2" : fieldLength) + ")";
                            break;
                    }
                    string isNull = "1" == fieldIsNull ? " NULL" : " NOT NULL";
                    string identity = "1" == fieldIsIdentity ? " IDENTITY(1,1)" : "";
                    bool isNew = "1" == fieldIsAdd;
                    if ("1" == fieldPrimarykey)
                    {
                        pkList.Add(fieldName);
                    }
                    if (IsAddTable)
                    {
                        sql.Append("[" + fieldName + "] ");
                        sql.Append(fieldType1);
                        sql.Append(" " + isNull);
                        sql.Append(" " + identity);
                        if (!fieldDefaultValue.IsNullOrEmpty())
                        {
                            sql.Append(" DEFAULT " + fieldDefaultValue);
                        }
                        if (!fname.Equals(f_nameArray.Last(), StringComparison.CurrentCultureIgnoreCase))
                        {
                            sql.Append(",");
                        }
                    }
                    else
                    {
                        //如果以前是自增，现在又取消了
                        if (!isNew && identity.IsNullOrEmpty() && SchemaDt.Select("f_name='" + fname + "' and isidentity=1").Length > 0)
                        {
                            sql.Append("ALTER TABLE [" + oldtablename + "] DROP COLUMN [" + fname + "];");
                            sql.Append("ALTER TABLE [" + oldtablename + "] ADD [" + fieldName + "] " + fieldType1 + identity + isNull + ";");
                        }
                        else
                        {
                            if (!identity.IsNullOrEmpty() && !isNew)
                            {
                                sql.Append("ALTER TABLE [" + oldtablename + "] DROP COLUMN [" + fname + "];ALTER TABLE [" + oldtablename + "] ADD [" + fieldName + "] int NOT NULL IDENTITY(1,1);");
                            }
                            else
                            {
                                if (isNew)
                                {
                                    sql.Append("ALTER TABLE [" + oldtablename + "] ADD [" + fieldName + "] " + fieldType1 + identity + isNull + ";");
                                }
                                else
                                {
                                    sql.Append("ALTER TABLE [" + oldtablename + "] ALTER COLUMN [" + fname + "] " + fieldType1 + identity + isNull + ";");
                                }
                                if (!isNew && !fname.Equals(fieldName, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    sql.Append("EXEC sp_rename N'[" + oldtablename + "].[" + fname + "]', N'" + fieldName + "', 'COLUMN';");
                                }
                            }
                        }

                        if (!fieldDefaultValue.IsNullOrEmpty())
                        {
                            sql.Append("ALTER TABLE [" + oldtablename + "] ADD CONSTRAINT [DF_" + tablename + "_" + fname + "] DEFAULT (" + fieldDefaultValue + ") FOR [" + fname + "];");
                        }
                    }
                }
                if (IsAddTable)
                {
                    if (pkList.Count > 0)
                    {
                        sql.Append(", PRIMARY KEY (");
                        foreach (string pk in pkList)
                        {
                            sql.Append("[" + pk + "]");
                            if (!pk.Equals(pkList.Last()))
                            {
                                sql.Append(",");
                            }
                        }
                        sql.Append(")");
                    }
                    sql.Append(")");
                }
                else
                {
                    if (pkList.Count > 0)
                    {
                        sql2.Append("ALTER TABLE [" + tablename + "] ADD CONSTRAINT [PK_" + tablename + "] PRIMARY KEY (");
                        foreach (string pk in pkList)
                        {
                            sql2.Append("[" + pk + "]");
                            if (!pk.Equals(pkList.Last()))
                            {
                                sql2.Append(",");
                            }
                        }
                        sql2.Append(");");
                    }
                    if (!tablename.Equals(oldtablename, StringComparison.CurrentCultureIgnoreCase))
                    {
                        sql.Append("EXEC sp_rename '" + oldtablename + "', '" + tablename + "';");
                    }
                }
                string sql1 = sql.ToString();
                string errmsg;
                bool isSuccess = DBConn.TestSql(dbconn, sql1, out errmsg, false);
                if (isSuccess && sql2.Length > 0)
                {
                    isSuccess = DBConn.TestSql(dbconn, sql2.ToString(), out errmsg, false);
                }
                string url = "TableEdit_SqlServer?dbconnid=" + dbconnID + "&tablename=" + tablename + "&connid=" + dbconnID + "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&s_Name=" + Request.QueryString["s_Name"];
                if (isSuccess)
                {
                    YJ.Platform.Log.Add("修改表结构成功-" + dbconn.Name + "-" + oldtablename, sql1, YJ.Platform.Log.Types.数据连接);
                    ViewBag.ClientScript = "alert('保存成功!');window.location='" + url + "';";
                    return View(SchemaDt);
                }
                else
                {
                    YJ.Platform.Log.Add("修改表结构失败-" + dbconn.Name + "-" + oldtablename, sql1, YJ.Platform.Log.Types.数据连接);
                    ViewBag.ClientScript = "alert('保存失败-" + errmsg.Replace("'", "") + "!');window.location='" + url + "';";
                    return View(SchemaDt);
                }
            }
            
            return View(SchemaDt);
        }


        public ActionResult TableEdit_Oracle()
        {
            return TableEdit_Oracle(null);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TableEdit_Oracle(FormCollection collection)
        {
            string dbconnID = string.Empty;
            string tableName = string.Empty;
            YJ.Platform.DBConnection DBConn = new YJ.Platform.DBConnection();
            DataTable SchemaDt = new DataTable();
            IDbConnection conn = null;
            List<string> PrimaryKeyList = new List<string>();
            YJ.Data.Model.DBConnection dbconn = null;
            bool IsAddTable = false;
            List<string> SystemTables;

            SystemTables = YJ.Utility.Config.SystemDataTables;
            dbconnID = Request.QueryString["dbconnid"];
            tableName = Request.QueryString["tablename"];
            if (tableName.IsNullOrEmpty())
            {
                tableName = "NEWTABLE_" + YJ.Utility.Tools.GetRandomString();
                IsAddTable = true;
            }
            if (dbconnID.IsGuid() && !tableName.IsNullOrEmpty())
            {
                dbconn = DBConn.Get(dbconnID.ToGuid());
                if (dbconn != null)
                {
                    conn = DBConn.GetConnection(dbconn);
                    if (conn != null)
                    {
                        if (conn.State != ConnectionState.Open)
                        {
                            conn.Open();
                        }
                        SchemaDt = DBConn.GetTableSchema(conn, tableName, dbconn.Type);
                        PrimaryKeyList = DBConn.GetPrimaryKey(dbconn, tableName);
                    }
                }
            }
            if (IsAddTable)
            {
                tableName = "";
            }
            if (SchemaDt.Rows.Count == 0)
            {
                DataRow dr = SchemaDt.NewRow();
                dr["f_name"] = "ID";
                dr["t_name"] = "int";
                dr["is_null"] = 0;
                dr["isidentity"] = 1;
                SchemaDt.Rows.Add(dr);
                PrimaryKeyList.Add("ID");
            }
            ViewBag.PrimaryKeyList = PrimaryKeyList;
            ViewBag.IsAddTable = IsAddTable;
            ViewBag.tableName = tableName;

            if (collection != null)
            {
                if (dbconn == null)
                {
                    ViewBag.ClientScript = "alert('未找到数据连接!');";
                    return View(SchemaDt);
                }

                string f_name = Request.Form["f_name"] ?? "";
                string[] f_nameArray = f_name.Split(',');
                string tablename = Request.Form["tablename"];
                string oldtablename = Request.Form["oldtablename"];
                string delfield = Request.Form["delfield"] ?? "";
                StringBuilder sql = new StringBuilder();
                List<string> sqlList = new List<string>();
                string tempColumn = "temp_" + Guid.NewGuid().ToString("N");
                if (SystemTables.Find(p => p.Equals(oldtablename, StringComparison.CurrentCultureIgnoreCase)) != null)
                {
                    ViewBag.ClientScript = "alert('您不能修改系统表!');";
                    return View(SchemaDt);
                }
                if (IsAddTable)
                {
                    sqlList.Add("CREATE TABLE \"" + tablename + "\" (\"" + tempColumn + "\" varchar2(50) NULL)");
                    oldtablename = tablename;
                }

                if (PrimaryKeyList.Count > 0)
                {
                    sqlList.Add("ALTER TABLE \"" + oldtablename + "\" DROP PRIMARY KEY");
                }
                StringBuilder dropColnum = new StringBuilder();
                foreach (var del in delfield.Split(','))
                {
                    if (!del.IsNullOrEmpty())
                    {
                        dropColnum.Append("\"" + del + "\",");
                    }
                }

                if (dropColnum.Length > 0)
                {
                    sqlList.Add("ALTER TABLE \"" + oldtablename + "\" DROP (" + dropColnum.ToString().TrimEnd(',') + ")");
                }
                StringBuilder pks = new StringBuilder();
                foreach (var fname in f_nameArray)
                {
                    string fieldName = Request.Form[fname + "_name1"];
                    string fieldType = Request.Form[fname + "_type"];
                    string fieldLength = Request.Form[fname + "_length"];
                    string fieldIsNull = Request.Form[fname + "_isnull"];
                    string fieldIsIdentity = Request.Form[fname + "_isidentity"];
                    string fieldPrimarykey = Request.Form[fname + "_primarykey"];
                    string fieldDefaultValue = Request.Form[fname + "_defaultvalue"];
                    string fieldIsAdd = Request.Form[fname + "_isadd"];

                    if (fieldName.IsNullOrEmpty() || fieldType.IsNullOrEmpty())
                    {
                        continue;
                    }
                    string fieldType1 = string.Empty;
                    switch (fieldType.ToLower())
                    {
                        case "varchar2":
                        case "nvarchar2":
                            fieldType1 = fieldType + "(" + (fieldLength.IsInt() ? fieldLength.ToInt() == -1 ? "50" : fieldLength : "50") + ")";
                            break;
                        case "char":
                            fieldType1 = fieldType + "(" + (fieldLength.IsInt() ? fieldLength : "50") + ")";
                            break;
                        case "date":
                        case "clog":
                        case "nclog":
                        case "int":
                        case "float":
                            fieldType1 = fieldType;
                            break;
                        case "number":
                            fieldType1 = fieldType + "(" + (fieldLength.IsNullOrEmpty() ? "18,2" : fieldLength) + ")";
                            break;
                    }

                    int fieldIsNull1 = SchemaDt.Select("F_Name='" + fname + "'").Length > 0 ? SchemaDt.Select("F_Name='" + fname + "'")[0]["IS_NULL"].ToString().ToInt() : -1;
                    string isNull = "";
                    if ("1" == fieldIsNull)
                    {
                        if (fieldIsNull1 == 0)
                        {
                            isNull = " NULL";
                        }
                    }
                    else
                    {
                        if (fieldIsNull1 == 1)
                        {
                            isNull = " NOT NULL";
                        }
                    }
                    string identity = "1" == fieldIsIdentity ? " " : "";
                    string defaultvalue = !fieldDefaultValue.IsNullOrEmpty() ? " DEFAULT " + fieldDefaultValue : "";
                    bool isNew = "1" == fieldIsAdd;
                    if (isNew)
                    {
                        sqlList.Add("ALTER TABLE \"" + oldtablename + "\" ADD (\"" + fieldName + "\" " + fieldType1 + identity + defaultvalue + isNull + ")");
                    }
                    else
                    {
                        if (!fieldIsIdentity.IsNullOrEmpty())
                        {
                            sqlList.Add("ALTER TABLE \"" + oldtablename + "\" MODIFY (\"" + fieldName + "\" " + fieldType1 + identity + defaultvalue + isNull + ")");
                        }
                        else
                        {
                            sqlList.Add("ALTER TABLE \"" + oldtablename + "\" MODIFY (\"" + fname + "\" " + fieldType1 + identity + defaultvalue + isNull + ")");
                        }
                    }
                    if ("1" == fieldPrimarykey)
                    {
                        pks.Append("\"" + fieldName + "\",");
                    }
                    if (!isNew && !fname.Equals(fieldName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        sqlList.Add("ALTER TABLE \"" + oldtablename + "\" RENAME COLUMN \"" + fname + "\" TO \"" + fieldName + "\"");
                    }
                }
                if (pks.Length > 0)
                {
                    sqlList.Add("ALTER TABLE \"" + oldtablename + "\" ADD CONSTRAINT \"" + tablename + "_PK\" PRIMARY KEY (" + pks.ToString().TrimEnd(',') + ")");
                }
                if (!tablename.Equals(oldtablename, StringComparison.CurrentCultureIgnoreCase))
                {
                    sqlList.Add("ALTER TABLE \"" + oldtablename + "\" RENAME TO \"" + tablename + "\"");
                }
                if (IsAddTable)
                {
                    sqlList.Add("ALTER TABLE \"" + oldtablename + "\" DROP (\"" + tempColumn + "\")");
                }
                string sql1 = sqlList.ToString(";");
                bool isSuccess = true;
                foreach (var s in sqlList)
                {
                    if (!DBConn.TestSql(dbconn, s, false) && isSuccess)
                    {
                        isSuccess = false;
                    }
                }
                string url = "TableEdit_Oracle?dbconnid=" + dbconnID + "&tablename=" + tablename + "&connid=" + dbconnID + "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&s_Name=" + Request.QueryString["s_Name"];
                if (isSuccess)
                {
                    YJ.Platform.Log.Add("修改表结构成功-" + dbconn.Name + "-" + oldtablename, sql1, YJ.Platform.Log.Types.数据连接);
                    ViewBag.ClientScript = "alert('保存成功!');window.location='" + url + "';";
                    return View(SchemaDt);
                }
                else
                {
                    YJ.Platform.Log.Add("修改表结构失败-" + dbconn.Name + "-" + oldtablename, sql1, YJ.Platform.Log.Types.数据连接);
                    ViewBag.ClientScript = "alert('保存失败!');window.location='" + url + "';";
                    return View(SchemaDt);
                }
            }


            return View(SchemaDt);
        }


        public ActionResult TableEdit_MySql()
        {
            return TableEdit_MySql(null);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TableEdit_MySql(FormCollection collection)
        {
            string dbconnID = string.Empty;
            string tableName = string.Empty;
            YJ.Platform.DBConnection DBConn = new YJ.Platform.DBConnection();
            DataTable SchemaDt = new DataTable();
            IDbConnection conn = null;
            List<string> PrimaryKeyList = new List<string>();
            YJ.Data.Model.DBConnection dbconn = null;
            bool IsAddTable = false;
            List<string> SystemTables;

            SystemTables = YJ.Utility.Config.SystemDataTables;
            dbconnID = Request.QueryString["dbconnid"];
            tableName = Request.QueryString["tablename"];
            if (tableName.IsNullOrEmpty())
            {
                tableName = "NEWTABLE_" + YJ.Utility.Tools.GetRandomString();
                IsAddTable = true;
            }
            if (dbconnID.IsGuid() && !tableName.IsNullOrEmpty())
            {
                dbconn = DBConn.Get(dbconnID.ToGuid());
                if (dbconn != null)
                {
                    conn = DBConn.GetConnection(dbconn);
                    if (conn != null)
                    {
                        if (conn.State != ConnectionState.Open)
                        {
                            conn.Open();
                        }
                        if (!IsAddTable)
                        {
                            SchemaDt = DBConn.GetTableSchema(conn, tableName, dbconn.Type);
                            PrimaryKeyList = DBConn.GetPrimaryKey(dbconn, tableName);
                        }
                        else
                        {
                            SchemaDt = DBConn.GetTableSchema(conn, "Log", dbconn.Type);
                            SchemaDt.Rows.Clear();
                        }
                    }
                }
            }
            if (IsAddTable)
            {
                tableName = "";
            }
            if (SchemaDt.Rows.Count == 0)
            {
                DataRow dr = SchemaDt.NewRow();
                dr["f_name"] = "ID";
                dr["t_name"] = "int";
                dr["is_null"] = 0;
                dr["isidentity"] = 1;
                SchemaDt.Rows.Add(dr);
                PrimaryKeyList.Add("ID");
            }

            ViewBag.PrimaryKeyList = PrimaryKeyList;
            ViewBag.IsAddTable = IsAddTable;
            ViewBag.tableName = tableName;

            if (collection != null)
            {
                if (dbconn == null)
                {
                    ViewBag.ClientScript = "alert('未找到数据连接!');";
                    return View(SchemaDt);
                }

                string f_name = Request.Form["f_name"] ?? "";
                string[] f_nameArray = f_name.Split(',');
                string tablename = Request.Form["tablename"];
                string oldtablename = Request.Form["oldtablename"];
                string delfield = Request.Form["delfield"] ?? "";
                StringBuilder sql = new StringBuilder();
                string tempColumn = "temp_" + Guid.NewGuid().ToString("N");
                if (SystemTables.Find(p => p.Equals(oldtablename, StringComparison.CurrentCultureIgnoreCase)) != null)
                {
                    ViewBag.ClientScript = "alert('您不能修改系统表!');";
                    return View(SchemaDt);
                }
                if (IsAddTable)
                {
                    sql.Append("CREATE TABLE `" + tablename + "` (`" + tempColumn + "` varchar(255) PRIMARY KEY NOT NULL);");
                    oldtablename = tablename;
                }

                sql.Append("ALTER TABLE `" + oldtablename + "` ");
                if (PrimaryKeyList.Count > 0)
                {
                    sql.Append("DROP PRIMARY KEY,");
                }

                foreach (var del in delfield.Split(','))
                {
                    if (!del.IsNullOrEmpty())
                    {
                        sql.Append("DROP COLUMN `" + del + "`,");
                    }
                }

                foreach (var fname in f_nameArray)
                {
                    string fieldName = Request.Form[fname + "_name1"];
                    string fieldType = Request.Form[fname + "_type"];
                    string fieldLength = Request.Form[fname + "_length"];
                    string fieldIsNull = Request.Form[fname + "_isnull"];
                    string fieldIsIdentity = Request.Form[fname + "_isidentity"];
                    string fieldPrimarykey = Request.Form[fname + "_primarykey"];
                    string fieldDefaultValue = Request.Form[fname + "_defaultvalue"];
                    string fieldIsAdd = Request.Form[fname + "_isadd"];

                    if (fieldName.IsNullOrEmpty() || fieldType.IsNullOrEmpty())
                    {
                        continue;
                    }
                    string fieldType1 = string.Empty;
                    switch (fieldType)
                    {
                        case "varchar":
                            fieldType1 = fieldType + "(" + (fieldLength.IsInt() ? fieldLength.ToInt() <= -1 ? "255" : fieldLength : "255") + ")";
                            break;
                        case "char":
                            fieldType1 = fieldType + "(" + (fieldLength.IsInt() ? fieldLength : "255") + ")";
                            break;
                        case "datetime":
                        case "text":
                        case "longtext":
                        case "int":
                        case "float":
                            fieldType1 = fieldType;
                            break;
                        case "decimal":
                            fieldType1 = fieldType + "(" + (fieldLength.IsNullOrEmpty() ? "18,2" : fieldLength) + ")";
                            break;
                    }
                    string isNull = "1" == fieldIsNull ? " NULL" : " NOT NULL";
                    string identity = "1" == fieldIsIdentity ? " AUTO_INCREMENT" : "";
                    string defaultValue = fieldDefaultValue.IsNullOrEmpty() ? "" : " DEFAULT " + fieldDefaultValue;
                    bool isNew = "1" == fieldIsAdd;
                    if (isNew)
                    {
                        sql.Append("ADD COLUMN `" + fieldName + "` " + fieldType1 + identity + isNull + ",");
                    }
                    else
                    {
                        if (!fieldIsIdentity.IsNullOrEmpty())
                        {
                            sql.Append("MODIFY COLUMN `" + fieldName + "` " + fieldType1 + identity + isNull + defaultValue + ",");
                        }
                        else
                        {
                            if (!isNew && !fname.Equals(fieldName, StringComparison.CurrentCultureIgnoreCase))
                            {
                                sql.Append("CHANGE COLUMN `" + fname + "` `" + fieldName + "` " + fieldType1 + identity + isNull + defaultValue + ",");
                            }
                            else
                            {
                                sql.Append("MODIFY COLUMN `" + fname + "` " + fieldType1 + identity + isNull + defaultValue + ",");
                            }
                        }
                    }
                    if ("1" == fieldPrimarykey)
                    {
                        sql.Append("ADD PRIMARY KEY (`" + fname + "`),");
                    }


                }
                if (!tablename.Equals(oldtablename, StringComparison.CurrentCultureIgnoreCase))
                {
                    sql.Append("RENAME TABLE `" + oldtablename + "` TO `" + tablename + "`,");
                }
                if (IsAddTable)
                {
                    sql.Append("DROP COLUMN `" + tempColumn + "`,");
                }
                string sql1 = sql.ToString().TrimEnd(',') + ";";
                bool isSuccess = DBConn.TestSql(dbconn, sql1, false);
                string url = "TableEdit_MySql?dbconnid=" + dbconnID + "&tablename=" + tablename + "&connid=" + dbconnID + "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&s_Name=" + Request.QueryString["s_Name"];
                if (isSuccess)
                {
                    YJ.Platform.Log.Add("修改表结构成功-" + dbconn.Name + "-" + oldtablename, sql1, YJ.Platform.Log.Types.数据连接);
                    ViewBag.ClientScript = "alert('保存成功!');window.location='" + url + "';";
                    return View(SchemaDt);
                }
                else
                {
                    YJ.Platform.Log.Add("修改表结构失败-" + dbconn.Name + "-" + oldtablename, sql1, YJ.Platform.Log.Types.数据连接);
                    ViewBag.ClientScript = "alert('保存失败!');window.location='" + url + "';";
                    return View(SchemaDt);
                }
            }

            return View(SchemaDt);
        }
    }
}
