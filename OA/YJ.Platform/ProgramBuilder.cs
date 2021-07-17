using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;

namespace YJ.Platform
{
    public class ProgramBuilder
    {
        public static string exportCackeKey = "ExportToExcel_";
        private YJ.Data.Interface.IProgramBuilder dataProgramBuilder;
        public ProgramBuilder()
        {
            this.dataProgramBuilder = Data.Factory.Factory.GetProgramBuilder();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.ProgramBuilder model)
        {
            return dataProgramBuilder.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.ProgramBuilder model)
        {
            return dataProgramBuilder.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.ProgramBuilder> GetAll()
        {
            return dataProgramBuilder.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.ProgramBuilder Get(Guid id)
        {
            return dataProgramBuilder.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            return dataProgramBuilder.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataProgramBuilder.GetCount();
        }

        /// <summary>
        /// 得到分页数据
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<YJ.Data.Model.ProgramBuilder> GetList(out string pager, string query = "", string name = "", string typeid = "")
        {
            if (typeid.IsGuid())
            {
                typeid = new Dictionary().GetAllChildsIDString(typeid.ToGuid());
            }
            return dataProgramBuilder.GetList(out pager, query, name, typeid);
        }

        /// <summary>
        /// 得到列表所有字段
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<string> GetFields(Guid id)
        {
            var pb = Get(id);
            if (pb == null)
            {
                return new List<string>();
            }
            DBConnection conn = new DBConnection();
            return conn.GetFieldsBySQL(pb.DBConnID, pb.SQL);
        }

        /// <summary>
        /// 得到列表所有字段下拉列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetFieldsOptions(Guid id, string value)
        {
            List<string> fields = GetFields(id);
            StringBuilder sb = new StringBuilder();
            foreach (var field in fields)
            {
                sb.AppendFormat("<option value=\"{0}\" {1}>{2}</option>", field, field == value ? "selected=\"selected\"" : "", field);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 删除程序及所有设置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void DeleteAllSet(Guid id)
        {
            var pb = Get(id);
            if (pb != null)
            {
                pb.Status = 2;
                Update(pb);
                Log.Add("删除了应用程序设计", id.ToString(), Log.Types.其它分类);
            }
        }

        /// <summary>
        /// 将设计添加到缓存
        /// </summary>
        /// <param name="pb"></param>
        public void AddToCache(Data.Model.ProgramBuilderCache pb)
        {
            string cacheKey = Utility.Keys.CacheKeys.ProgramBuilder.ToString() + pb.Program.ID.ToString("N");
            Cache.IO.Opation.Set(cacheKey, pb);
        }

        /// <summary>
        /// 得到所有设置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Data.Model.ProgramBuilderCache GetSet(Guid id)
        {
            string cacheKey = Utility.Keys.CacheKeys.ProgramBuilder.ToString() + id.ToString("N");
            var obj = Cache.IO.Opation.Get(cacheKey);
            if (obj != null)
            {
                return (Data.Model.ProgramBuilderCache)obj;
            }
            var pb = new Data.Model.ProgramBuilderCache();
            var program = Get(id);
            if (null == program)
            {
                return null;
            }
            pb.Program = program;
            pb.Fields = new ProgramBuilderFields().GetAll(id).OrderBy(p => p.Sort).ToList();
            pb.Querys = new ProgramBuilderQuerys().GetAll(id).OrderBy(p => p.Sort).ToList();
            pb.Buttons = new ProgramBuilderButtons().GetAll(id).OrderBy(p => p.Sort).ToList();
            pb.Validates = new ProgramBuilderValidates().GetAll(id);
            pb.Export = new ProgramBuilderExport().GetAll(id).OrderBy(p => p.Sort).ToList();
            AddToCache(pb);
            return pb;
        }

        /// <summary>
        /// 发布程序
        /// </summary>
        /// <param name="id"></param>
        public bool Publish(Guid id, bool isMvc = false)
        {
            var pb = Get(id);
            if (pb == null || pb.Name.IsNullOrEmpty() || pb.SQL.IsNullOrEmpty())
            {
                return false;
            }
            AppLibrary App = new AppLibrary();
            var app = App.GetByCode(id.ToString());
            bool isAdd = false;
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                if (app == null)
                {
                    isAdd = true;
                    app = new Data.Model.AppLibrary();
                    app.ID = Guid.NewGuid();
                }
                app.Address = !isMvc ? "/Platform/ProgramBuilder/Run.aspx?programid=" + id.ToString() 
                    : "/ProgramBuilder/Run?programid=" + id.ToString();
                app.Code = id.ToString();
                app.OpenMode = 0;
                app.Title = pb.Name;
                app.Type = pb.Type;
                if (isAdd)
                {
                    App.Add(app);
                }
                else
                {
                    App.Update(app);
                }
                pb.Status = 1;
                Update(pb);

                var buttons1 = new ProgramBuilderButtons().GetAll(pb.ID);
                AppLibraryButtons1 But1 = new AppLibraryButtons1();
                var buttons = But1.GetAllByAppID(app.ID);
                List<Data.Model.AppLibraryButtons1> buttons2 = new List<Data.Model.AppLibraryButtons1>();
                foreach (var button in buttons1)
                {
                    Data.Model.AppLibraryButtons1 but1 = buttons.Find(p => p.ID == button.ID);
                    bool isAddBut = false;
                    if (but1 == null)
                    {
                        but1 = new Data.Model.AppLibraryButtons1();
                        isAddBut = true;
                    }
                    but1.AppLibraryID = app.ID;
                    but1.ButtonID = button.ButtonID;
                    but1.Events = button.ClientScript;
                    but1.Ico = button.Ico ?? "";
                    but1.ID = button.ID;
                    but1.Name = button.ButtonName;
                    but1.ShowType = button.ShowType;
                    but1.Sort = button.Sort;
                    but1.Type = 0;
                    but1.IsValidateShow = button.IsValidateShow;
                    if (isAddBut)
                    {
                        But1.Add(but1);
                    }
                    else
                    {
                        But1.Update(but1);
                    }
                    buttons2.Add(but1);//记录下本次的按钮列表，以删除原有列表中多余的按钮。
                }
                foreach (var button in buttons)
                {
                    if (buttons2.Find(p => p.ID == button.ID) == null)
                    {
                        But1.Delete(button.ID);
                    }
                }

                scope.Complete();
            }
            new YJ.Platform.AppLibrary().ClearCache();
            new YJ.Platform.Menu().ClearAllDataTableCache();
            new YJ.Platform.AppLibraryButtons1().ClearCache();
            new YJ.Platform.AppLibrarySubPages().ClearCache();
            Data.Model.ProgramBuilderCache pbCache = new Data.Model.ProgramBuilderCache();
            pbCache.Program = pb;
            pbCache.Fields = new ProgramBuilderFields().GetAll(pb.ID).OrderBy(p=>p.Sort).ToList();
            pbCache.Querys = new ProgramBuilderQuerys().GetAll(pb.ID).OrderBy(p => p.Sort).ToList();
            pbCache.Buttons = new ProgramBuilderButtons().GetAll(id).OrderBy(p => p.Sort).ToList();
            pbCache.Validates = new ProgramBuilderValidates().GetAll(id);
            pbCache.Export = new ProgramBuilderExport().GetAll(id).OrderBy(p => p.Sort).ToList();
            AddToCache(pbCache);
            return true;
        }

        /// <summary>
        /// 得到验证状态JSON字符串
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetJsonString(Guid id)
        {
            var pb = GetSet(id);
            if (pb == null || pb.Validates == null)
            {
                return "{}";
            }
            StringBuilder sb = new StringBuilder();
            foreach (var val in pb.Validates)
            {
                var fieldName = val.TableName + "_" + val.FieldName;
                sb.AppendFormat("\"{0}\":\"{1}\"", fieldName.ToLower(), string.Concat("0_", val.Validate));
                sb.Append(",");
            }
            return "{" + sb.ToString().TrimEnd(',') + "}";
        }

        /// <summary>
        /// 将查询SQL及参数添加到缓存以便导出
        /// </summary>
        public void AddToExportCache(Guid programID, Guid dbConnID, string querySql, List<IDbDataParameter> parList)
        {
            string cacheKey = exportCackeKey + programID.ToString("N") + System.Web.HttpContext.Current.Session.SessionID;
            Cache.IO.Opation.Set(cacheKey + "_DbConnID", dbConnID);
            Cache.IO.Opation.Set(cacheKey + "_QuerySql", querySql);
            Cache.IO.Opation.Set(cacheKey + "_QueryParameter", parList);
        }

        /// <summary>
        /// 得到缓存的导出SQL及参数
        /// </summary>
        /// <param name="programID"></param>
        /// <returns></returns>
        public Guid GetExportCache(Guid programID, out string querySql, out List<IDbDataParameter> parList)
        {
            querySql = "";
            parList = new List<IDbDataParameter>();
            string cacheKey = exportCackeKey + programID.ToString("N") + System.Web.HttpContext.Current.Session.SessionID;
            var querySqlObj = Cache.IO.Opation.Get(cacheKey + "_QuerySql");
            if (querySqlObj != null)
            {
                querySql = querySqlObj.ToString();
            }

            var parListObj = Cache.IO.Opation.Get(cacheKey + "_QueryParameter");
            if (parListObj != null)
            {
                parList = (List<IDbDataParameter>)parListObj;
            }

            var dbconnIDObj = Cache.IO.Opation.Get(cacheKey + "_DbConnID");
            if (dbconnIDObj != null)
            {
                return dbconnIDObj.ToString().ToGuid();
            }

            return Guid.Empty;
        }

        /// <summary>
        /// 得到导出的DataTable
        /// </summary>
        /// <param name="programID"></param>
        /// <param name="template">模板</param>
        /// <param name="headerText">表头</param>
        /// <param name="fileName">导出的文件名</param>
        /// <returns></returns>
        public DataTable GetExportDataTable(Guid programID, out string template, out string headerText, out string fileName)
        {
            template = "";
            headerText = "";
            fileName = "";
            var programSet = GetSet(programID);
            if (programSet == null)
            {
                return new DataTable();
            }
            template = Files.FilePath + programSet.Program.ExportTemplate.DesDecrypt();
            headerText = programSet.Program.ExportHeaderText;
            fileName = programSet.Program.ExportFileName;
            string querySql;
            List<IDbDataParameter> parList;
            Guid connID = GetExportCache(programID, out querySql, out parList);
            DataTable dt = new DBConnection().GetDataTable(connID, querySql, parList.ToArray());
            var exprotFileds = programSet.Export;
            DataTable dt1 = new DataTable();
            foreach (var filed in exprotFileds)
            {
                DataColumn dc = new DataColumn(filed.ShowTitle, GetExportColumnsType(filed.DataType.HasValue ? filed.DataType.Value : 0));
                dc.Caption = filed.Width.ToString();
                dt1.Columns.Add(dc);
            }
            int index = 1;
            Dictionary BDict = new Dictionary();
            Organize BOrganize = new Organize();
            foreach (DataRow dr in dt.Rows)
            {
                DataRow dr1 = dt1.NewRow();
                foreach (var field in exprotFileds)
                {
                    object text = string.Empty;
                    object obj = field.Field.IsNullOrEmpty() ? "" : dr[field.Field];
                    switch (field.ShowType)
                    {
                        case 0://直接输出
                            text = obj;
                            break;
                        case 1://序号
                            text = index.ToString();
                            break;
                        case 2://日期时间
                            text = obj.ToString().ToDateTime().ToString(field.ShowFormat);
                            break;
                        case 3://数字
                            text = obj.ToString().ToDecimal().ToString(field.ShowFormat);
                            break;
                        case 4://数据字典ID显示为标题
                            text = BDict.GetTitle(obj.ToString().ToGuid());
                            break;
                        case 5://组织机构ID显示为名称
                            text = BOrganize.GetNames(obj.ToString());
                            break;
                        case 6://自定义
                            text = field.CustomString;
                            break;
                        default:
                            text = obj;
                            break;
                    }
                    dr1[field.ShowTitle] = text;
                }
                index++;
                dt1.Rows.Add(dr1);
            }
            return dt1;
        }
        /// <summary>
        /// 得到列数据类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private Type GetExportColumnsType(int type)
        {
            Type type1 = "".GetType();
            switch (type)
            { 
                case 0:
                case 1:
                    type1 = "".GetType();
                    break;
                case 2:
                    type1 = decimal.MaxValue.GetType();
                    break;
                case 3:
                    type1 = DateTime.Now.GetType();
                    break;
            }
            return type1;
        }

        /// <summary>
        /// 从EXCEL文件导入数据
        /// </summary>
        /// <param name="programID"></param>
        /// <param name="file">EXCEL文件路径</param>
        /// <param name="table">表名</param>
        /// <param name="numberFiled">标识字段，每次导入的标识相同</param>
        /// <returns></returns>
        public int InDataFormExcel(Guid programID, string table, string file, out string msg, string numberFiled = "")
        {
            int count = 0;
            msg = "";
            if (table.IsNullOrEmpty())
            {
                msg = "没有选择表";
                return count;
            }

            DBConnection dbConn = new DBConnection();
            var program = Get(programID);
            if (program == null)
            {
                msg = "未找到应用程序设计";
                return count;
            }

            Data.Model.DBConnection conn = dbConn.Get(program.DBConnID);
            if (conn == null)
            {
                msg = "未找到相应的数据库连接";
                return count;
            }

            var filedList = new ProgramBuilderFields().GetAll(programID);
            if (filedList.Count == 0)
            {
                msg = "应用程序未设置列表字段";
                return count;
            }

            try
            {
                DataTable dt = Utility.NPOIHelper.ReadToDataTable(file);
                if (dt.Rows.Count == 0)
                {
                    msg = "未发现要导入的数据";
                    return count;
                }
                var tableFileds = dbConn.GetFieldsBySQL(program.DBConnID, "select * from " + table + " where 1=0");
                DataTable dt1 = new DataTable(table);
                string number = Utility.DateTimeNew.Now.ToString("yyyyMMddHHmmssfffff");
                foreach (var filed in filedList)
                {
                    if (filed.Field.IsNullOrEmpty() || tableFileds.Find(p => p.Equals(filed.Field, StringComparison.CurrentCultureIgnoreCase)) == null)
                    {
                        continue;
                    }
                    dt1.Columns.Add(filed.Field);
                }
                if (!numberFiled.IsNullOrEmpty())
                {
                    dt1.Columns.Add(numberFiled);
                }
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow dr1 = dt1.NewRow();
                    foreach (DataColumn col in dt1.Columns)
                    {
                        var dtFiled = filedList.Find(p => p.Field.Equals(col.ColumnName));
                        if(dtFiled == null)
                        {
                            continue;
                        }
                        dr1[col.ColumnName] = dr[dtFiled.ShowTitle];
                    }
                    if (!numberFiled.IsNullOrEmpty())
                    {
                        dr1[numberFiled] = number;
                    }
                    dt1.Rows.Add(dr1);
                }
                count = dbConn.DataTableToDB(conn, dt1);
                YJ.Platform.Log.Add("通过应用程序导入了数据-表(" + table + ")标识(" + number + ")", file, Log.Types.其它分类);
                return count;
            }
            catch (Exception err)
            {
                msg = err.Message;
                return count;
            }
        }
        /// <summary>
        /// 从EXCEL文件导入数据（指定列）
        /// </summary>
        /// <param name="programID"></param>
        /// <param name="file">EXCEL文件路径</param>
        /// <param name="table">表名</param>
        /// <param name="numberFiled">标识字段，每次导入的标识相同</param>
        /// <returns></returns>
        public int InDataFormExcel1(Guid programID, string table, string file, out string msg, string numberFiled = "")
        {
            int count = 0;
            msg = "";
            if (table.IsNullOrEmpty())
            {
                msg = "没有选择表";
                return count;
            }

            DBConnection dbConn = new DBConnection();
            var program = new ProgramBuilder().Get(programID);
            if (program == null)
            {
                msg = "未找到应用程序设计";
                return count;
            }

            YJ.Data.Model.DBConnection conn = dbConn.Get(program.DBConnID);
            if (conn == null)
            {
                msg = "未找到相应的数据库连接";
                return count;
            }

            var pb = new ProgramBuilder().Get(programID);
            if (pb == null)
            {
                msg = "未找到应用程序";
                return count;
            }
            try
            {
                DataTable dt = YJ.Utility.NPOIHelper.ReadToDataTable(file);
                if (dt.Rows.Count == 0)
                {
                    msg = "未发现要导入的数据";
                    return count;
                }
                var tableFileds = dbConn.GetFieldsBySQL(program.DBConnID, "select * from " + table + " where 1=0");
                DataTable dt1 = new DataTable(table);
                string number = YJ.Utility.DateTimeNew.Now.ToString("yyyyMMddHHmmssfffff");
                var filedList = pb.InDataFiledName.Split(',').ToList();
                foreach (var filed in filedList)
                {
                    if (filed.IsNullOrEmpty() || tableFileds.Find(p => p.Equals(filed.Split('-')[1], StringComparison.CurrentCultureIgnoreCase)) == null)
                    {
                        continue;
                    }
                    dt1.Columns.Add(filed.Split('-')[1]);
                }

                foreach (DataRow dr in dt.Rows)
                {
                    DataRow dr1 = dt1.NewRow();
                    foreach (DataColumn col in dt1.Columns)
                    {
                        var dtFiled = filedList.Find(p => p.Split('-')[1].Equals(col.ColumnName));
                        if (dtFiled == null)
                        {
                            continue;
                        }
                        dr1[col.ColumnName] = dr[dtFiled.Split('-')[0].ToInt32()];
                    }
                    dt1.Rows.Add(dr1);
                }
                count = dbConn.DataTableToDB(conn, dt1);
                YJ.Platform.Log.Add("通过应用程序导入了数据-表(" + table + ")标识(" + number + ")", file, Log.Types.其它分类);
                return count;
            }
            catch (Exception err)
            {
                msg = err.Message;
                return count;
            }
        }
    }
}
