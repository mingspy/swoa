namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;
    using YJ.Data.Interface;
    using YJ.Utility;

    public class DBHelper : IDBHelper
    {
        private string connectionString;

        public DBHelper()
        {
            this.connectionString = Config.PlatformConnectionStringMSSQL;
        }

        public DBHelper(string connString)
        {
            this.connectionString = connString;
        }

        public int DataTableToDB(DataTable dt)
        {
            int num2;
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter("select * from " + dt.TableName + " where 1=0", connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        foreach (DataRow row in dt.Rows)
                        {
                            dataTable.ImportRow(row);
                        }
                        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                        num2 = adapter.Update(dataTable);
                    }
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            return num2;
        }

        public void Dispose()
        {
        }

        public int Execute(List<string> sqlList)
        {
            int num2;
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    int num = 0;
                    command.Connection = connection;
                    foreach (string str in sqlList)
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = str;
                        command.Prepare();
                        num += command.ExecuteNonQuery();
                    }
                    num2 = num;
                }
            }
            return num2;
        }

        public int Execute(string sql)
        {
            int num;
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Prepare();
                    num = command.ExecuteNonQuery();
                }
            }
            return num;
        }

        public int Execute(List<string> sqlList, List<SqlParameter[]> parameterList)
        {
            int num3;
            if (sqlList.Count > parameterList.Count)
            {
                throw new Exception("参数错误");
            }
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    int num = 0;
                    command.Connection = connection;
                    for (int i = 0; i < sqlList.Count; i++)
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = sqlList[i];
                        if ((parameterList[i] != null) && (parameterList[i].Length > 0))
                        {
                            command.Parameters.AddRange(parameterList[i]);
                        }
                        num += command.ExecuteNonQuery();
                        command.Parameters.Clear();
                        command.Prepare();
                    }
                    num3 = num;
                }
            }
            return num3;
        }

        public int Execute(string sql, SqlParameter[] parameter, [Optional, DefaultParameterValue(false)] bool returnIdentity)
        {
            int num2;
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if ((parameter != null) && (parameter.Length > 0))
                    {
                        command.Parameters.AddRange(parameter);
                    }
                    int defaultValue = command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    command.Prepare();
                    if (returnIdentity)
                    {
                        command.CommandText = "select @@IDENTITY";
                        object obj2 = command.ExecuteScalar();
                        defaultValue = (obj2 == null) ? defaultValue : obj2.ToString().ToInt(defaultValue);
                    }
                    num2 = defaultValue;
                }
            }
            return num2;
        }

        public string ExecuteScalar(string sql)
        {
            string str;
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    object obj2 = command.ExecuteScalar();
                    command.Prepare();
                    str = (obj2 != null) ? obj2.ToString() : string.Empty;
                }
            }
            return str;
        }

        public string ExecuteScalar(string sql, SqlParameter[] parameter)
        {
            string str;
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if ((parameter != null) && (parameter.Length > 0))
                    {
                        command.Parameters.AddRange(parameter);
                    }
                    object obj2 = command.ExecuteScalar();
                    command.Parameters.Clear();
                    command.Prepare();
                    str = (obj2 != null) ? obj2.ToString() : string.Empty;
                }
            }
            return str;
        }

        public SqlDataReader GetDataReader(string sql)
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);
            connection.Open();
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.CommandTimeout = 180;
                command.Prepare();
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }

        public SqlDataReader GetDataReader(string sql, SqlParameter[] parameter)
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);
            connection.Open();
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.CommandTimeout = 180;
                if ((parameter != null) && (parameter.Length > 0))
                {
                    command.Parameters.AddRange(parameter);
                }
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                command.Parameters.Clear();
                command.Prepare();
                return reader;
            }
        }

        public DataSet GetDataSet(string sql)
        {
            DataSet set2;
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    set2 = dataSet;
                }
            }
            return set2;
        }

        public DataTable GetDataTable(string sql)
        {
            DataTable table2;
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    adapter.Dispose();
                    table2 = dataTable;
                }
            }
            return table2;
        }

        public DataTable GetDataTable(string sql, SqlParameter[] parameter)
        {
            DataTable table2;
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if ((parameter != null) && (parameter.Length > 0))
                    {
                        command.Parameters.AddRange(parameter);
                    }
                    command.CommandTimeout = 180;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        adapter.Dispose();
                        command.Parameters.Clear();
                        command.Prepare();
                        table2 = dataTable;
                    }
                }
            }
            return table2;
        }

        public string GetFields(string sql, SqlParameter[] param)
        {
            string str;
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                StringBuilder builder = new StringBuilder(500);
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if ((param != null) && (param.Length > 0))
                    {
                        command.Parameters.AddRange(param);
                    }
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.SchemaOnly);
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        builder.Append("[" + reader.GetName(i) + "]" + ((i < (reader.FieldCount - 1)) ? "," : string.Empty));
                    }
                    command.Parameters.Clear();
                    reader.Close();
                    reader.Dispose();
                    command.Prepare();
                    str = builder.ToString();
                }
            }
            return str;
        }

        public string GetFields(string sql, SqlParameter[] param, out string tableName)
        {
            string str;
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                StringBuilder builder = new StringBuilder(500);
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if ((param != null) && (param.Length > 0))
                    {
                        command.Parameters.AddRange(param);
                    }
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.SchemaOnly);
                    tableName = reader.GetSchemaTable().TableName;
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        builder.Append("[" + reader.GetName(i) + "]" + ((i < (reader.FieldCount - 1)) ? "," : string.Empty));
                    }
                    command.Parameters.Clear();
                    reader.Close();
                    reader.Dispose();
                    command.Prepare();
                    str = builder.ToString();
                }
            }
            return str;
        }

        public string GetFieldValue(string sql)
        {
            return this.ExecuteScalar(sql);
        }

        public string GetFieldValue(string sql, SqlParameter[] parameter)
        {
            return this.ExecuteScalar(sql, parameter);
        }

        public string GetPaerSql(string sql, int size, int number, out long count, [Optional, DefaultParameterValue(null)] SqlParameter[] param)
        {
            long num;
            string fieldValue = this.GetFieldValue(string.Format("select count(*) from ({0}) as PagerCountTemp", sql), param);
            count = fieldValue.IsLong(out num) ? num : 0L;
            if (count < (((number * size) - size) + 1))
            {
                number = 1;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM (");
            builder.Append(sql);
            builder.AppendFormat(") AS PagerTempTable", new object[0]);
            builder.AppendFormat(" WHERE PagerAutoRowNumber BETWEEN {0} AND {1}", ((number * size) - size) + 1, number * size);
            return builder.ToString();
        }

        public string GetPaerSql(string table, string fileds, string where, string order, int size, int number, out long count, [Optional, DefaultParameterValue(null)] SqlParameter[] param)
        {
            long num;
            string str = string.Empty;
            if (where.IsNullOrEmpty())
            {
                str = "";
            }
            else
            {
                str = where.Trim();
                if (str.StartsWith("and", StringComparison.CurrentCultureIgnoreCase))
                {
                    str = str.Substring(3);
                }
            }
            string str2 = str.IsNullOrEmpty() ? "" : ("WHERE " + str);
            object[] args = new object[] { fileds, order, table, str2 };
            string str3 = string.Format("select {0},ROW_NUMBER() OVER(ORDER BY {1}) as PagerAutoRowNumber from {2} {3}", args);
            string fieldValue = this.GetFieldValue(string.Format("select COUNT(*) FROM {0} {1}", table, str2), param);
            count = fieldValue.IsLong(out num) ? num : 0L;
            if (count < (((number * size) - size) + 1))
            {
                number = 1;
            }
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT * FROM (", new object[0]);
            builder.Append(str3);
            builder.AppendFormat(") as PagerTempTable", new object[0]);
            if (count > size)
            {
                builder.AppendFormat(" WHERE PagerAutoRowNumber BETWEEN {0} AND {1}", ((number * size) - size) + 1, number * size);
            }
            return builder.ToString();
        }

        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }
        }
    }
}

