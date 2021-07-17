using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace YJ.Platform
{
    public class DBConnection
    {
        private YJ.Data.Interface.IDBConnection dataDBConnection;
        public DBConnection()
        {
            this.dataDBConnection = Data.Factory.Factory.GetDBConnection();
        }

        /// <summary>
        /// 连接类型
        /// </summary>
        public enum Types
        { 
            SqlServer
        }

        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.DBConnection model)
        {
            int i = dataDBConnection.Add(model);
            ClearCache();
            return i;
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.DBConnection model)
        {
            int i = dataDBConnection.Update(model);
            ClearCache();
            return i;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.DBConnection> GetAll(bool fromCache=false)
        {
            if (!fromCache)
            {
                return dataDBConnection.GetAll();
            }
            else
            {
                string key = YJ.Utility.Keys.CacheKeys.DBConnnections.ToString();
                object obj = YJ.Cache.IO.Opation.Get(key);
                if (obj != null && obj is List<YJ.Data.Model.DBConnection>)
                {
                    return (List<YJ.Data.Model.DBConnection>)obj;
                }
                else
                {
                    var list = dataDBConnection.GetAll();
                    YJ.Cache.IO.Opation.Set(key, list);
                    return list;
                }
            }
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.DBConnection Get(Guid id, bool fromCache=true)
        {
            if (fromCache)
            {
                var conn = GetAll(true).Find(p => p.ID == id);
                if (conn != null)
                {
                    return conn;
                }
                else
                {
                    return dataDBConnection.Get(id);
                }
                
            }
            return dataDBConnection.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            int i = dataDBConnection.Delete(id);
            ClearCache();
            return i;
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataDBConnection.GetCount();
        }
        /// <summary>
        /// 连接类型
        /// </summary>
        public enum ConnTypes
        { 
            SqlServer
        }
        /// <summary>
        /// 得到所有数据连接类型的下拉选择
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetAllTypeOptions(string value = "")
        {
            StringBuilder options = new StringBuilder();
            var array = Enum.GetValues(typeof(ConnTypes));
            foreach (var arr in array)
            {
                options.AppendFormat("<option value=\"{0}\" {1}>{0}</option>", arr, arr.ToString() == value ? "selected=\"selected\"" : "");
            }
            return options.ToString();
        }
        /// <summary>
        /// 得到所有数据连接的下拉选择
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetAllOptions(string value = "")
        {
            var conns = GetAll(true);
            StringBuilder options = new StringBuilder();
            foreach (var conn in conns.OrderBy(p=>p.Name))
            {
                options.AppendFormat("<option value=\"{0}\" {1}>{2}</option>", conn.ID,
                    string.Compare(conn.ID.ToString(), value, true) == 0 ? "selected=\"selected\"" : "", conn.Name);
            }
            return options.ToString();
        }
        /// <summary>
        /// 清除缓存
        /// </summary>
        public void ClearCache()
        {
            string key = YJ.Utility.Keys.CacheKeys.DBConnnections.ToString();
            YJ.Cache.IO.Opation.Remove(key);
        }

        /// <summary>
        /// 根据连接ID得到所有表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type">0、包含表和视图 1、只取表 2、只取视图</param>
        /// <returns></returns>
        public List<string> GetTables(Guid id, int type = 0)
        {
            var allConns = GetAll(true);
            var conn = allConns.Find(p => p.ID == id);
            if (conn == null) return new List<string>();
            List<string> tables = new List<string>();
            switch (conn.Type)
            {
                case "SqlServer":
                    tables = getTables_SqlServer(conn, type);
                    break;
            }
            return tables;
        }

        /// <summary>
        /// 得到所有字段
        /// </summary>
        /// <param name="id">连接ID</param>
        /// <param name="table">表名</param>
        /// <returns></returns>
        public Dictionary<string, string> GetFields(Guid id, string table)
        {
            if (table.IsNullOrEmpty()) return new Dictionary<string, string>();
            var allConns = GetAll(true);
            var conn = allConns.Find(p => p.ID == id);
            if (conn == null) return new Dictionary<string, string>();
            Dictionary<string, string> fields = new Dictionary<string, string>();
            switch (conn.Type)
            {
                case "SqlServer":
                    fields = getFields_SqlServer(conn, table);
                    break;
            }
            return fields;
        }

        /// <summary>
        /// 得到一个连接一个字段的值
        /// </summary>
        /// <param name="connID"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string GetFieldValue(Guid connID, string sql, IDbDataParameter[] param = null)
        {
            var dbconn = Get(connID);
            if (dbconn == null)
            {
                return "";
            }
            switch (dbconn.Type.ToLower())
            {
                case "sqlserver":
                    using (SqlConnection conn = new SqlConnection(dbconn.ConnectionString))
                    {
                        try
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand(sql, conn))
                            {
                                if (param != null && param.Length > 0)
                                {
                                    foreach (IDbDataParameter par in param)
                                    {
                                        cmd.Parameters.Add((SqlParameter)par);
                                    }
                                }
                                object obj = cmd.ExecuteScalar();
                                cmd.Parameters.Clear();
                                return obj.ToString();
                            }
                        }
                        catch
                        { }
                        finally
                        {
                            conn.Close();
                            conn.Dispose();
                        }
                    }
                    break;
            }
            return "";
        }

        /// <summary>
        /// 得到一个连接一个表一个字段的值
        /// </summary>
        /// <param name="link_table_field"></param>
        /// <returns></returns>
        public string GetFieldValue(string link_table_field, Dictionary<string,string> pkFieldValue)
        {
            if (link_table_field.IsNullOrEmpty()) return "";
            string[] array = link_table_field.Split('.');
            if (array.Length != 3) return "";
            string link = array[0];
            string table = array[1];
            string field = array[2];
            var allConns = GetAll(true);
            Guid linkid;
            if (!link.IsGuid(out linkid)) return "";
            var conn = allConns.Find(p => p.ID == linkid);
            if (conn == null) return "";
            List<string> fields = new List<string>();
            string value = string.Empty;
            switch (conn.Type)
            {
                case "SqlServer":
                    value = getFieldValue_SqlServer(conn, table, field, pkFieldValue);
                    break;
            }
            return value;
        }
        /// <summary>
        /// 得到一个连接一个表一个字段的值
        /// </summary>
        /// <param name="conn">连接ID</param>
        /// <param name="table">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="pkFieldValue">主键和值字典</param>
        /// <returns></returns>
        private string getFieldValue_SqlServer(YJ.Data.Model.DBConnection conn, string table, string field, Dictionary<string, string> pkFieldValue)
        {
            using (SqlConnection sqlConn = new SqlConnection(conn.ConnectionString))
            {
                try
                {
                    sqlConn.Open();
                }
                catch (SqlException err)
                {
                    Log.Add(err);
                    return "";
                }
                List<string> fields = new List<string>();
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("select {0} from {1} where 1=1", field, table);
                foreach (var pk in pkFieldValue)
                {
                    sql.AppendFormat(" and {0}='{1}'", pk.Key, pk.Value);
                }

                using (SqlCommand sqlCmd = new SqlCommand(sql.ToString(), sqlConn))
                {
                    SqlDataReader dr = sqlCmd.ExecuteReader();
                    string value = string.Empty;
                    if (dr.HasRows)
                    {
                        dr.Read();
                        value = dr.GetString(0);
                    }
                    dr.Close();
                    return value;
                }
            }
        }
        /// <summary>
        /// 得到一个连接一个表一个字段的值
        /// </summary>
        /// <param name="link_table_field"></param>
        /// <returns></returns>
        public string GetFieldValue(string link_table_field, string pkField, string pkFieldValue)
        {
            if (link_table_field.IsNullOrEmpty())
            {
                return "";
            }
            string[] array = link_table_field.Split('.');
            if (array.Length != 3)
            {
                return "";
            }
            string link = array[0];
            string table = array[1];
            string field = array[2];
            var allConns = GetAll(true);
            Guid linkid;
            if (!link.IsGuid(out linkid))
            {
                return "";
            }
            var conn = allConns.Find(p => p.ID == linkid);
            if (conn == null)
            {
                return "";
            }
            string value = string.Empty;
            switch (conn.Type)
            {
                case "SqlServer":
                    value = getFieldValue_SqlServer(conn, table, field, pkField, pkFieldValue);
                    break;
            }
            return value;
        }

        /// <summary>
        /// 测试一个连接
        /// </summary>
        /// <param name="connID"></param>
        /// <returns></returns>
        public string Test(Guid connID)
        {
            var link = Get(connID);
            if (link == null) return "未找到连接!";
            switch (link.Type)
            { 
                case "SqlServer":
                    return test_SqlServer(link);
            }

            return "";
        }

        /// <summary>
        /// 测试一个连接
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        private string test_SqlServer(YJ.Data.Model.DBConnection conn)
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(conn.ConnectionString))
                {
                    sqlConn.Open();
                    return "连接成功!";
                }
            }
            catch (SqlException err)
            {
                return err.Message;
            }
        }
        /// <summary>
        /// 测试一个sql条件合法性
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        private string testSql_SqlServer(YJ.Data.Model.DBConnection conn, string sql)
        {
            using (SqlConnection sqlConn = new SqlConnection(conn.ConnectionString))
            {
                try
                {
                    sqlConn.Open();
                }
                catch (SqlException err)
                {
                    return err.Message;
                }
                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException err)
                    {
                        return err.Message;
                    }
                }
                return "";
            }
        }



        /// <summary>
        /// 得到一个连接所有表
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="type">0、包含表和视图 1、只取表 2、只取视图</param>
        /// <returns></returns>
        private List<string> getTables_SqlServer(YJ.Data.Model.DBConnection conn, int type)
        {
            using (SqlConnection sqlConn = new SqlConnection(conn.ConnectionString))
            {
                try
                {
                    sqlConn.Open();
                }
                catch (SqlException err)
                {
                    Log.Add(err);
                    return new List<string>();
                }
                List<string> tables = new List<string>();
                string where = string.Empty;
                switch (type)
                { 
                    case 0:
                        where = "xtype='U' or xtype='V'";
                        break;
                    case 1:
                        where = "xtype='U'";
                        break;
                    case 2:
                        where = "xtype='V'";
                        break;
                }
                string sql = "SELECT name FROM sysobjects WHERE " + where + " ORDER BY xtype, name";
                using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
                {
                    SqlDataReader dr = sqlCmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tables.Add(dr.GetString(0));
                    }
                    dr.Close();
                    return tables;
                }
            }
        }




        /// <summary>
        /// 得到一个连接一个表所有字段
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="table"></param>
        /// <returns>Dictionary<字段名称, 字段说明备注></returns>
        private Dictionary<string, string> getFields_SqlServer(YJ.Data.Model.DBConnection conn, string table)
        {
            using (SqlConnection sqlConn = new SqlConnection(conn.ConnectionString))
            {
                try
                {
                    sqlConn.Open();
                }
                catch (SqlException err)
                {
                    Log.Add(err);
                    return new Dictionary<string, string>();
                }
                Dictionary<string, string> fields = new Dictionary<string, string>();
                string sql = string.Format(@"SELECT a.name as f_name, b.value from 
sys.syscolumns a LEFT JOIN sys.extended_properties b on a.id=b.major_id AND a.colid=b.minor_id AND b.name='MS_Description' 
WHERE object_id('{0}')=a.id ORDER BY a.colid", table);
                using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
                {
                    SqlDataReader dr = sqlCmd.ExecuteReader();
                    while (dr.Read())
                    {
                        fields.Add(dr.GetString(0), dr.IsDBNull(1) ? "" : dr.GetString(1).Replace1("\r\n", ""));
                    }
                    dr.Close();
                    return fields;
                }
            }
        }






        /// <summary>
        /// 得到一个连接一个表一个字段的值
        /// </summary>
        /// <param name="linkID">连接ID</param>
        /// <param name="table">表</param>
        /// <param name="field">字段</param>
        /// <param name="pkField">主键字段</param>
        /// <param name="pkFieldValue">主键值</param>
        /// <returns></returns>
        private string getFieldValue_SqlServer(YJ.Data.Model.DBConnection conn, string table, string field, string pkField, string pkFieldValue)
        {
            string v = "";
            using (SqlConnection sqlConn = new SqlConnection(conn.ConnectionString))
            {
                try
                {
                    sqlConn.Open();
                }
                catch (SqlException err)
                {
                    Log.Add(err);
                    return "";
                }
                string sql = string.Format("SELECT {0} FROM {1} WHERE {2} = '{3}'", field, table, pkField, pkFieldValue);
                using (SqlDataAdapter dap = new SqlDataAdapter(sql, sqlConn))
                {
                    try
                    {
                        DataTable dt = new DataTable();
                        dap.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            v = dt.Rows[0][0].ToString();
                        }
                    }
                    catch (SqlException err)
                    {
                        Log.Add(err);
                    }
                    return v;
                }
            }
        }

        /// <summary>

        /// <summary>
        /// 根据连接实体得到连接
        /// </summary>
        /// <param name="linkID"></param>
        /// <returns></returns>
        public System.Data.IDbConnection GetConnection(YJ.Data.Model.DBConnection dbconn)
        {
            if (dbconn == null || dbconn.Type.IsNullOrEmpty() || dbconn.ConnectionString.IsNullOrEmpty())
            {
                return null;
            }
            IDbConnection conn = null;
            try
            {
                switch (dbconn.Type)
                {
                    case "SqlServer":
                        conn = new SqlConnection(dbconn.ConnectionString);
                        break;

                }
            }
            catch(Exception err)
            {
                Platform.Log.Add(err);
            }
            return conn;
        }

        /// <summary>
        /// 根据连接实体得到数据适配器
        /// </summary>
        /// <param name="linkID"></param>
        /// <returns></returns>
        public System.Data.IDbDataAdapter GetDataAdapter(IDbConnection conn, string connType, string cmdText, IDataParameter[] parArray)
        {
            IDbDataAdapter dataAdapter = null;
            switch (connType)
            {
                case "SqlServer":
                    using (SqlCommand cmd = new SqlCommand(cmdText, (SqlConnection)conn))
                    {
                        if (parArray != null && parArray.Length > 0)
                        {
                            cmd.Parameters.AddRange(parArray);
                        }
                        dataAdapter = new SqlDataAdapter(cmd);
                    }
                    break;
             
            }
            return dataAdapter;
        }

        /// <summary>
        /// 得到流水号
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="connType"></param>
        /// <param name="table">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="serialNumberConfig"></param>
        /// <returns></returns>
        public string GetSerialNumber(IDbConnection conn, string connType, string table, string field, LitJson.JsonData serialNumberJson, out int maxNumber)
        {
            maxNumber = 0;
            if (serialNumberJson == null)
            {
                return "";
            }
            //int lstype = serialNumberJson.ContainsKey("lstype") ? serialNumberJson["lstype"].ToString().ToInt() : 0;
            string formatstring = serialNumberJson.ContainsKey("formatstring") ? serialNumberJson["formatstring"].ToString() : "";
            //string ynfiled = serialNumberJson.ContainsKey("ynfiled") ? serialNumberJson["ynfiled"].ToString() : "";
            string sqlwhere = serialNumberJson.ContainsKey("sqlwhere") ? serialNumberJson["sqlwhere"].ToString().UrlDecode() : "";
            int length = serialNumberJson.ContainsKey("length") ? serialNumberJson["length"].ToString().ToInt() : 0;
            string maxfiled = serialNumberJson.ContainsKey("maxfiled") ? serialNumberJson["maxfiled"].ToString() : "";
            string snumber = string.Empty;
            if (!sqlwhere.IsNullOrEmpty())
            {
                sqlwhere.FilterWildcard(YJ.Platform.Users.CurrentUserID.ToString());
                if (!sqlwhere.Trim1().StartsWith("and", StringComparison.CurrentCultureIgnoreCase))
                {
                    sqlwhere = "AND " + sqlwhere;
                }
            }
            bool isSerial = formatstring.Contains("$serialnumber$", StringComparison.CurrentCultureIgnoreCase);
            string  formatstring1 = formatstring.FilterWildcard(YJ.Platform.Users.CurrentUserID.ToString());
            if (maxfiled.IsNullOrEmpty())
            {
                #region 没有设置最大序号字段
                string selectField = string.Empty;
                string sql = string.Empty;
                string replaceStr = isSerial ? formatstring1.Substring(formatstring1.IndexOf("$serialnumber$")).Replace1("$serialnumber$", "") : "";
                string replaceStr1 = isSerial ? formatstring1.Substring(0, formatstring1.IndexOf("$serialnumber$")) : "";
                switch (connType)
                {
                    case "SqlServer":
                        selectField = isSerial ? "ISNULL(MAX(CAST(REPLACE(REPLACE(" + field + ",'" + replaceStr + "',''),'" + replaceStr1 + "','') as INT)),0)+1"
                            : "ISNULL(MAX(CAST(RIGHT(" + field + "," + length + ") as INT)),0)+1";
                        sql = "SELECT " + selectField + " FROM " + table + " WHERE 1=1 " + sqlwhere;

                        using (SqlCommand sqlCmd = new SqlCommand(sql, (SqlConnection)conn))
                        {
                            try
                            {
                                snumber = sqlCmd.ExecuteScalar().ToString().PadLeft(length, '0');
                            }
                            catch
                            {
                                snumber = "1".PadLeft(length, '0');
                            }
                        }
                        break;
                   
                }
                #endregion
            }
            else
            {
                string sql = string.Empty;
                switch (connType)
                {
                    case "SqlServer":
                        sql = "SELECT ISNULL(MAX(" + maxfiled + "),0) FROM " + table + " WHERE 1=1 " + sqlwhere;
                        using (SqlCommand sqlCmd = new SqlCommand(sql, (SqlConnection)conn))
                        {
                            try
                            {
                                maxNumber = sqlCmd.ExecuteScalar().ToString().ToInt(0) + 1;
                                snumber = maxNumber.ToString().PadLeft(length, '0');
                            }
                            catch
                            {
                                snumber = "1".PadLeft(length, '0');
                            }
                        }
                        break;
                    
                }
            }
            snumber = isSerial ? formatstring1.Replace1("$serialnumber$", snumber) : formatstring1 + snumber;
            return snumber;
        }

        /// <summary>
        /// 测试一个sql是否合法
        /// </summary>
        /// <param name="dbconn"></param>
        /// <param name="sql"></param>
        /// <param name="replaceSql">是否过滤SQL</param>
        /// <returns></returns>
        public bool TestSql(YJ.Data.Model.DBConnection dbconn, string sql, bool replaceSql = true)
        {
            if (dbconn == null)
            {
                return false;
            }
            if (replaceSql)
            {
                sql = sql.ReplaceSelectSql().FilterWildcard(Users.CurrentUserID.ToString());
            }
            switch (dbconn.Type.ToLower())
            {
                #region SqlServer
                case "sqlserver":
                    using (SqlConnection conn = new SqlConnection(dbconn.ConnectionString))
                    {
                        try
                        {
                            conn.Open();
                        }
                        catch (SqlException err)
                        {
                            Log.Add(err);
                            return false;
                        }
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            try
                            {
                                cmd.ExecuteNonQuery();
                                return true;
                            }
                            catch(SqlException err)
                            {
                                Log.Add("执行SqlServer语句发生了错误", err.Message + err.StackTrace, Log.Types.数据连接, sql);
                                return false;
                            }
                        }
                    }
                #endregion


            }
            return false;
        }

        /// <summary>
        /// 测试一个sql是否合法
        /// </summary>
        /// <param name="dbconn"></param>
        /// <param name="sql"></param>
        /// <param name="replaceSql">是否过滤SQL</param>
        /// <returns></returns>
        public bool TestSql(YJ.Data.Model.DBConnection dbconn, string sql, out string msg, bool replaceSql = true)
        {
            msg = "";
            if (dbconn == null)
            {
                return false;
            }
            if (replaceSql)
            {
                sql = sql.ReplaceSelectSql().FilterWildcard(Users.CurrentUserID.ToString());
            }
            switch (dbconn.Type.ToLower())
            {
                #region SqlServer
                case "sqlserver":
                    using (SqlConnection conn = new SqlConnection(dbconn.ConnectionString))
                    {
                        try
                        {
                            conn.Open();
                        }
                        catch (SqlException err)
                        {
                            Log.Add(err);
                            return false;
                        }
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            try
                            {
                                cmd.ExecuteNonQuery();
                                return true;
                            }
                            catch (SqlException err)
                            {
                                msg = err.Message;
                                Log.Add("执行SqlServer语句发生了错误", err.Message + err.StackTrace, Log.Types.数据连接, sql);
                                return false;
                            }
                        }
                    }
                #endregion


            }
            return false;
        }

        /// <summary>
        /// 根据连接实体得到数据表
        /// </summary>
        /// <param name="dbconn"></param>
        /// <param name="table"></param>
        /// <param name="field"></param>
        /// <param name="fieldValue"></param>
        /// <param name="sortString">排序</param>
        /// <returns></returns>
        public DataTable GetDataTable(string dbconn, string table, string field, string fieldValue, string sortString = "")
        {
            if (dbconn.IsNullOrEmpty() || table.IsNullOrEmpty() || field.IsNullOrEmpty() || fieldValue.IsNullOrEmpty())
            {
                return new DataTable();
            }
            string sort = sortString.IsNullOrEmpty() ? field : sortString;
            var conn = Get(dbconn.ToGuid());
            if (conn == null)
            {
                return new DataTable();
            }
            if (conn.Type == "SqlServer")
            {
                string sql = "SELECT * FROM " + table + " WHERE " + field + " = @" + field + " ORDER BY " + sort; 
                IDataParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@" + field, fieldValue) };
                return GetDataTable(conn, sql, parameterArray);
            }
            else
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// 根据连接实体得到数据表
        /// </summary>
        /// <param name="linkID"></param>
        /// <returns></returns>
        public DataTable GetDataTable(YJ.Data.Model.DBConnection dbconn, string sql, IDataParameter[] parameterArray = null)
        {
            if (dbconn == null || dbconn.Type.IsNullOrEmpty() || dbconn.ConnectionString.IsNullOrEmpty())
            {
                return null;
            }
            DataTable dt = new DataTable();
            switch (dbconn.Type)
            {
                #region SqlServer
                case "SqlServer":
                    using (SqlConnection conn = new SqlConnection(dbconn.ConnectionString))
                    {
                        try
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand(sql, conn))
                            {
                                if (parameterArray != null && parameterArray.Length > 0)
                                {
                                    cmd.Parameters.AddRange((SqlParameter[])parameterArray);
                                }
                                using (SqlDataAdapter dap = new SqlDataAdapter(cmd))
                                {
                                    dap.Fill(dt);
                                    cmd.Parameters.Clear();
                                }
                            }
                        }
                        catch (SqlException err)
                        {
                            Log.Add("获取DataTable发生了错误", err.Message + err.StackTrace + err.TargetSite, Log.Types.数据连接, sql);
                        }
                    }
                    break;
                #endregion

            }

            return dt;
        }

        /// <summary>
        /// 根据连接ID得到数据表
        /// </summary>
        /// <param name="connID"></param>
        /// <param name="sql"></param>
        /// <param name="parameterArray"></param>
        /// <returns></returns>
        public DataTable GetDataTable(Guid connID, string sql, IDataParameter[] param = null)
        {
            var dbconn = Get(connID);
            if (dbconn == null)
            {
                return new DataTable();
            }
            string tableName = "Table_" + Guid.NewGuid().ToString("N");
            switch (dbconn.Type.ToLower())
            {
                case "sqlserver":
                    using (SqlConnection conn = new SqlConnection(dbconn.ConnectionString))
                    {
                        try
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand(sql, conn))
                            {
                                if (param != null && param.Length > 0)
                                {
                                    foreach (IDbDataParameter par in param)
                                    {
                                        cmd.Parameters.Add(new SqlParameter(par.ParameterName, par.Value));
                                    }
                                }
                                using (SqlDataAdapter dap = new SqlDataAdapter(cmd))
                                {
                                    DataTable dt = new DataTable();
                                    dt.TableName = tableName;
                                    dap.Fill(dt);
                                    cmd.Parameters.Clear();
                                    return dt;
                                }
                            }
                        }
                        catch(SqlException err)
                        {
                            Log.Add(err);
                        }
                        finally
                        {
                            conn.Close();
                            conn.Dispose();
                        }
                    }
                    break;
 
            }
            return new DataTable();
        }

        /// <summary>
        /// 得到一个连接一个SQL返回的列
        /// </summary>
        /// <param name="connID"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<string> GetFieldsBySQL(Guid connID, string sql)
        {
            var conn = Get(connID);
            if (conn == null)
            {
                return new List<string>();
            }
            List<string> fields = new List<string>();
            switch (conn.Type)
            {
                case "SqlServer":
                    using (SqlConnection conn1 = new SqlConnection(conn.ConnectionString))
                    {
                        conn1.Open();
                        using (SqlDataAdapter dap = new SqlDataAdapter(sql.FilterWildcard().ReplaceSelectSql(), conn1))
                        {
                            DataTable dt = new DataTable();
                            dap.FillSchema(dt, SchemaType.Source);
                            foreach (DataColumn col in dt.Columns)
                            {
                                fields.Add(col.ColumnName);
                            }
                        }
                    }
                    break;

            }
            return fields;
        }

        
        /// <summary>
        /// 得到一个表的结构
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="tableName"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public System.Data.DataTable GetTableSchema(System.Data.IDbConnection conn, string tableName, string dbType)
        {
            DataTable dt = new DataTable();
            switch (dbType)
            {
                case "SqlServer":
                    string sql = string.Format(@"select a.name as f_name,b.name as t_name,a.prec as [length],a.scale,a.isnullable as is_null, a.cdefault as cdefault,COLUMNPROPERTY( OBJECT_ID('{0}'),a.name,'IsIdentity') as isidentity, 
(select top 1 text from sysobjects d inner join syscolumns e on e.id=d.id inner join syscomments f on f.id=e.cdefault 
where d.name='{0}' and e.name=a.name) as defaultvalue from 
                    sys.syscolumns a inner join sys.types b on b.user_type_id=a.xtype 
                    where object_id('{0}')=id order by a.colid", tableName);
                    SqlDataAdapter dap = new SqlDataAdapter(sql, (SqlConnection)conn);
                    dap.Fill(dt);
                    break;
            }
            return dt;
        }

        /// <summary>
        /// 得到数据库默认值-sqlserver
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public object GetDbDefaultValue_SqlServer(string text)
        {
            if (text.IsNullOrEmpty())
            {
                return null;
            }
            object value = text;
            if (text.StartsWith("(("))
            {
                value = text.Replace1("((", "").Replace1("))", "");
            }
            else if (text.StartsWith("('"))
            {
                value = text.Replace1("('", "").Replace1("')", "");
            }
            else
            {
                switch (text.ToLower())
                {
                    case "(getdate())":
                        value = Utility.DateTimeNew.Now;
                        break;
                    case "(newid())":
                        value = Guid.NewGuid();
                        break;
                }
            }
            return value;
        }

        /// <summary>
        /// 得到数据库默认值-oracle
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public object GetDbDefaultValue_Oracle(string text)
        {
            if (text.IsNullOrEmpty())
            {
                return null;
            }
            object value = text;
            if (text.StartsWith("'"))
            {
                value = text.Replace1("'", "");
            }
            else
            {
                switch (text.ToLower())
                {
                    case "sysdate":
                        value = Utility.DateTimeNew.Now;
                        break;
                   
                }
            }
            return value;
        }

        /// <summary>
        /// 得到数据库默认值-MySql
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public object GetDbDefaultValue_MySql(string text)
        {
            if (text.IsNullOrEmpty())
            {
                return null;
            }
            object value = text;
            switch (text.ToUpper())
            {
                case "CURRENT_TIMESTAMP":
                    value = Utility.DateTimeNew.Now;
                    break;

            }
            return value;
        }

        /// <summary>
        /// 更新一个连接一个表一个字段的值
        /// </summary>
        /// <param name="connID"></param>
        /// <param name="table"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        public void UpdateFieldValue(Guid connID, string table, string field, string value, string where)
        {
            var conn = Get(connID);
            if (conn == null)
            {
                return;
            }
            switch (conn.Type)
            {
                #region SqlServer
                case "SqlServer":
                    using (var dbconn = GetConnection(conn))
                    {
                        try
                        {
                            dbconn.Open();
                        }
                        catch(SqlException ex) 
                        {
                            Platform.Log.Add(ex);
                        }
                        string sql = string.Format("UPDATE {0} SET {1}=@value WHERE {2}", table, field, where);
                        SqlParameter par = new SqlParameter("@value", value);
                        using (SqlCommand cmd = new SqlCommand(sql, (SqlConnection)dbconn))
                        {
                            cmd.Parameters.Add(par);
                            try
                            {
                                cmd.ExecuteNonQuery();
                            }
                            catch (SqlException ex)
                            {
                                Platform.Log.Add(ex);
                            }
                        }
                    }
                    break;
                #endregion


            }
        }

        /// <summary>
        /// 删除一个连接表的数据
        /// </summary>
        /// <param name="connID"></param>
        /// <param name="table"></param>
        /// <param name="pkFiled"></param>
        /// <param name="pkValue"></param>
        public int DeleteData(Guid connID, string table, string pkFiled, string pkValue)
        {
            int count = 0;
            var conn = Get(connID);
            if (conn == null)
            {
                return count;
            }
            switch (conn.Type)
            {
                #region SqlServer
                case "SqlServer":
                    using (var dbconn = GetConnection(conn))
                    {
                        try
                        {
                            dbconn.Open();
                        }
                        catch (SqlException ex)
                        {
                            Platform.Log.Add(ex);
                        }
                        string sql = string.Format("DELETE FROM {0} WHERE {1}=@{1}", table, pkFiled);
                        SqlParameter par = new SqlParameter("@" + pkFiled, pkValue);
                        using (SqlCommand cmd = new SqlCommand(sql, (SqlConnection)dbconn))
                        {
                            cmd.Parameters.Add(par);
                            try
                            {
                                count = cmd.ExecuteNonQuery();
                            }
                            catch (SqlException ex)
                            {
                                Platform.Log.Add(ex);
                            }
                        }
                    }
                    break;
                #endregion

            }
            return count;
        }

        /// <summary>
        /// 得到一页数据
        /// </summary>
        /// <param name="dbconn"></param>
        /// <param name="sql"></param>
        /// <param name="pager"></param>
        /// <param name="query"></param>
        /// <param name="parList"></param>
        /// <param name="pageSize">-1表示不分页</param>
        /// <returns></returns>
        public DataTable GetDataTable(Data.Model.DBConnection dbconn, string sql, out string pager, string query="", List<IDbDataParameter> parList = null,  int pageSize = 0)
        {
            pager = "";
            if (dbconn == null)
            {
                return null;
            }
            string pagerSql = string.Empty;
            switch (dbconn.Type)
            { 
                case "SqlServer":
                    using (SqlConnection conn = new SqlConnection(dbconn.ConnectionString))
                    {
                        try
                        {
                            conn.Open();
                            long count;
                            List<SqlParameter> sqlParList = new List<SqlParameter>();
                            if (parList != null && parList.Count > 0)
                            {
                                foreach (var par in parList)
                                {
                                    sqlParList.Add(new SqlParameter(par.ParameterName, par.Value));
                                }
                            }
                            DataTable dt = new DataTable();
                            if (pageSize == -1)
                            {
                                using (SqlCommand cmd = new SqlCommand(sql, conn))
                                {
                                    if (sqlParList != null && sqlParList.Count > 0)
                                    {
                                        cmd.Parameters.AddRange(sqlParList.ToArray());
                                    }
                                    using (SqlDataAdapter dap = new SqlDataAdapter(cmd))
                                    {
                                        dap.Fill(dt);
                                        return dt;
                                    }
                                }
                            }
                            else
                            {
                                pageSize = pageSize == 0 ? Utility.Tools.GetPageSize() : pageSize;
                                int pageNumber = Utility.Tools.GetPageNumber();
                                pagerSql = new Data.MSSQL.DBHelper(dbconn.ConnectionString).GetPaerSql(sql, pageSize, pageNumber, out count, sqlParList.ToArray());
                                pager = Utility.Tools.GetPagerHtml(count, pageSize, pageNumber, query);

                                using (SqlCommand cmd = new SqlCommand(pagerSql, conn))
                                {
                                    if (sqlParList != null && sqlParList.Count > 0)
                                    {
                                        cmd.Parameters.AddRange(sqlParList.ToArray());
                                    }
                                    using (SqlDataAdapter dap = new SqlDataAdapter(cmd))
                                    {
                                        dap.Fill(dt);
                                        return dt;
                                    }
                                }
                            }
                        }
                        catch (SqlException ex)
                        {
                            Platform.Log.Add(ex.Message, ex.StackTrace, Log.Types.系统错误, sql, pagerSql);
                            return null;
                        }
                    }
            }
            return null;
        }

        /// <summary>
        /// 根据SQL得到下拉选择项
        /// </summary>
        /// <param name="connID"></param>
        /// <param name="sql"></param>
        /// <param name="parList"></param>
        /// <returns></returns>
        public string GetOptionsFromSql(Data.Model.DBConnection conn, string sql, IDbDataParameter[] parList = null, string value = "")
        {
            DataTable dt = GetDataTable(conn, sql.ReplaceSelectSql().FilterWildcard(Users.CurrentUserID.ToString()), parList);
            StringBuilder options = new StringBuilder();
            if (dt.Columns.Count == 0) return "";
            foreach (DataRow dr in dt.Rows)
            {
                string value1 = dr[0].ToString();
                string title1 = dt.Columns.Count > 1 ? dr[1].ToString() : value1;
                options.AppendFormat("<option value=\"{0}\"{1}>{2}</option>",
                    value1, value1.Equals(value, StringComparison.CurrentCultureIgnoreCase) ? " selected=\"selected\"" : "", title1
                    );
            }

            return options.ToString();
        }

        /// <summary>
        /// 根据SQL得到下拉选择项
        /// </summary>
        /// <param name="connID"></param>
        /// <param name="sql"></param>
        /// <param name="parList"></param>
        /// <returns></returns>
        public string GetOptionsFromSql(Guid connID, string sql, IDbDataParameter[] parList = null, string value = "")
        {
            var dbconn = Get(connID);
            if (dbconn == null) return "";
            return GetOptionsFromSql(dbconn, sql, parList, value);
        }

        /// <summary>
        /// 得到一个连接的数据库名称
        /// </summary>
        /// <param name="dbConn"></param>
        /// <returns></returns>
        public string GetDatabaseName(Data.Model.DBConnection dbConn)
        {
            if (dbConn == null)
            {
                return string.Empty;
            }
            string dbName = string.Empty;
            var conn = GetConnection(dbConn);
            if (conn != null)
            {
                dbName = conn.Database;
            }
            return dbName;
        }


        /// <summary>
        /// 得到一个表的主键
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public List<string> GetPrimaryKey(Data.Model.DBConnection conn, string table)
        {
            switch (conn.Type.ToLower())
            { 
                case "sqlserver":
                    return getPrimaryKey_SqlServer(conn, table);
                case "oracle":
                    return getPrimaryKey_Oracle(conn, table);
                case "mysql":
                    return getPrimaryKey_MySql(conn, table);
            }
            return new List<string>();
        }

        private List<string> getPrimaryKey_SqlServer(Data.Model.DBConnection conn, string table)
        {
            string sql = string.Format(@"select b.column_name
from information_schema.table_constraints a
inner join information_schema.constraint_column_usage b
on a.constraint_name = b.constraint_name
where a.constraint_type = 'PRIMARY KEY' and a.table_name = '{0}'", table);
            DataTable dt = GetDataTable(conn, sql);
            List<string> pkList = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                pkList.Add(dr[0].ToString());
            }
            return pkList;
        }

        private List<string> getPrimaryKey_Oracle(Data.Model.DBConnection conn, string table)
        {
            string sql = string.Format(@"select b.column_name from user_constraints a, user_cons_columns b where a.constraint_name = b.constraint_name and a.constraint_type = 'P' and a.table_name = UPPER('{0}')", table);
            DataTable dt = GetDataTable(conn, sql);
            List<string> pkList = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                pkList.Add(dr[0].ToString());
            }
            return pkList;
        }

        private List<string> getPrimaryKey_MySql(Data.Model.DBConnection conn, string table)
        {
            string sql = string.Format(@"show full fields from `{0}`", table);
            DataTable dt = GetDataTable(conn, sql);
            List<string> pkList = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["key"].ToString().ToUpper() == "PRI")
                {
                    pkList.Add(dr[0].ToString());
                }
            }
            return pkList;
        }

        /// <summary>
        /// 得到字段类型选项
        /// </summary>
        /// <param name="value">默认值</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public string GetFieldDataTypeOptions(string value, string dbType)
        {
            string options = string.Empty;
            switch (dbType.ToLower())
            { 
                case "sqlserver":
                    options = getFieldDataTypeOptions_SqlServer(value);
                    break;
                case "oracle":
                    options = getFieldDataTypeOptions_Oracle(value);
                    break;
                case "mysql":
                    options = getFieldDataTypeOptions_MySql(value);
                    break;
            }

            return options;
        }

        private string getFieldDataTypeOptions_SqlServer(string value)
        {
            List<Tuple<string, string, string>> types = new List<Tuple<string, string, string>>();
            types.Add(new Tuple<string,string,string>("varchar", "英文字符串","50"));
            types.Add(new Tuple<string,string,string>("nvarchar", "中文字符串","50"));
            types.Add(new Tuple<string,string,string>("char", "字符","10"));
            types.Add(new Tuple<string,string,string>("datetime", "日期时间",""));
            types.Add(new Tuple<string,string,string>("text", "长文本",""));
            types.Add(new Tuple<string,string,string>("uniqueidentifier", "全局唯一ID",""));
            types.Add(new Tuple<string,string,string>("int", "整数",""));
            types.Add(new Tuple<string, string, string>("decimal", "小数", ""));
            types.Add(new Tuple<string, string, string>("money", "货币", ""));
            types.Add(new Tuple<string, string, string>("float", "浮点数", ""));
            StringBuilder sb = new StringBuilder();
            foreach (var type in types)
            {
                sb.Append("<option data-length=\"" + type.Item3 + "\" value=\"" + type.Item1 + "\"" + (type.Item1.Equals(value, StringComparison.CurrentCultureIgnoreCase) ? " selected=\"selected\"" : "") + ">"
                    + type.Item2 + "</option>");
            }
            return sb.ToString();
        }

        private string getFieldDataTypeOptions_Oracle(string value)
        {
            List<Tuple<string, string, string>> types = new List<Tuple<string, string, string>>();
            types.Add(new Tuple<string, string, string>("VARCHAR2", "英文字符串", "50"));
            types.Add(new Tuple<string, string, string>("NVARCHAR2", "中文字符串", "50"));
            types.Add(new Tuple<string, string, string>("CHAR", "字符", "10"));
            types.Add(new Tuple<string, string, string>("DATE", "日期时间", ""));
            types.Add(new Tuple<string, string, string>("CLOB", "长文本", ""));
            types.Add(new Tuple<string, string, string>("NCLOB", "中文长文本", ""));
            types.Add(new Tuple<string, string, string>("NUMBER", "数字", ""));
            types.Add(new Tuple<string, string, string>("FLOAT", "浮点数", ""));
            StringBuilder sb = new StringBuilder();
            foreach (var type in types)
            {
                sb.Append("<option data-length=\"" + type.Item3 + "\" value=\"" + type.Item1 + "\"" + (type.Item1.Equals(value, StringComparison.CurrentCultureIgnoreCase) ? " selected=\"selected\"" : "") + ">"
                    + type.Item2 + "</option>");
            }
            return sb.ToString();
        }

        private string getFieldDataTypeOptions_MySql(string value)
        {
            List<Tuple<string, string, string>> types = new List<Tuple<string, string, string>>();
            types.Add(new Tuple<string, string, string>("varchar", "字符串", "255"));
            types.Add(new Tuple<string, string, string>("char", "字符", "255"));
            types.Add(new Tuple<string, string, string>("datetime", "日期时间", ""));
            types.Add(new Tuple<string, string, string>("timestamp", "时间戳", ""));
            types.Add(new Tuple<string, string, string>("text", "文本", ""));
            types.Add(new Tuple<string, string, string>("longtext", "长文本", ""));
            types.Add(new Tuple<string, string, string>("int", "整数", ""));
            types.Add(new Tuple<string, string, string>("decimal", "小数", ""));
            types.Add(new Tuple<string, string, string>("float", "浮点数", ""));
            StringBuilder sb = new StringBuilder();
            foreach (var type in types)
            {
                sb.Append("<option data-length=\"" + type.Item3 + "\" value=\"" + type.Item1 + "\"" + (type.Item1.Equals(value, StringComparison.CurrentCultureIgnoreCase) ? " selected=\"selected\"" : "") + ">"
                    + type.Item2 + "</option>");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 得到一个表的所有约束
        /// </summary>
        /// <param name="dbConn"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public List<string> GetConstraints(Data.Model.DBConnection dbConn, string tableName)
        {
            List<string> list = new List<string>();
            switch (dbConn.Type.ToLower())
            {
                case "sqlserver":
                    string sql = "select name from sysobjects where parent_obj=(select id from sysobjects where name='" + tableName + "' and type='U')";
                    DataTable dt = GetDataTable(dbConn, sql);
                    foreach (DataRow dr in dt.Rows)
                    {
                        list.Add(dr[0].ToString());
                    }
                    break;
                case "oracle":

                    break;
                case "mysql":

                    break;
            }
            return list;
        }

        /// <summary>
        /// 检查SQL，防止对系统表进行操作
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool CheckSql(string sql)
        {
            if (sql.Contains("delete", StringComparison.CurrentCultureIgnoreCase) 
                || sql.Contains("drop", StringComparison.CurrentCultureIgnoreCase) 
                || sql.Contains("alter", StringComparison.CurrentCultureIgnoreCase)
                || sql.Contains("truncate", StringComparison.CurrentCultureIgnoreCase)
                ||sql.Contains("xp_cmdshell",StringComparison.CurrentCultureIgnoreCase))//预防webshell攻击
            {
                foreach (string table in Utility.Config.SystemDataTables)
                {
                    foreach (string s in sql.Split(' '))
                    {
                        if (s.Equals(table, StringComparison.CurrentCultureIgnoreCase) 
                            || ("[" + s + "]").Equals(("[" + table + "]"), StringComparison.CurrentCultureIgnoreCase))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 得到表默认查询SQL
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string GetDefaultQuerySql(Data.Model.DBConnection conn, string tableName)
        {
            string sql = string.Empty;
            switch (conn.Type.ToLower())
            {
                case "sqlserver":
                    sql = "SELECT TOP 50 * FROM " + tableName;
                    break;
                case "mysql":
                    sql = "SELECT * FROM " + tableName + " LIMIT 0,50";
                    break;
                case "oracle":
                    sql = "SELECT * FROM " + tableName + " WHERE ROWNUM BETWEEN 0 AND 50";
                    break;
            }
            return sql;
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int DataTableToDB(Data.Model.DBConnection conn, DataTable dt)
        {
            int count = 0;
            switch (conn.Type.ToLower())
            { 
                case "sqlserver":
                    count = new Data.MSSQL.DBHelper().DataTableToDB(dt);
                    break;
            }
            return count;
        }

        /// <summary>
        /// 得到一个连接所有表的下拉选项
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetAllTableOptions(Guid connID, string value)
        {
            var tables = GetTables(connID, 1);
            StringBuilder options = new StringBuilder();
            foreach (var table in tables)
            {
                options.Append("<option value=\"" + table + "\"" + (table.Equals(value, StringComparison.CurrentCultureIgnoreCase) ? " selected=\"selected\"" : "") + ">" + table + "</option>");
            }
            return options.ToString();
        }
    }
}
