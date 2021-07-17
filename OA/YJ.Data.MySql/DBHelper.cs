using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using YJ.Data.Interface;
using YJ.Utility;
namespace YJ.Data.MySql
{


    public class DBHelper : IDBHelper
    {
        private string connectionString;

        public DBHelper()
        {
            this.connectionString = Config.PlatformConnectionStringMySql;
        }

        public DBHelper(string connString)
        {
            this.connectionString = connString;
        }

        public int DataTableToDB(DataTable dt)
        {
            int num2;
            using (MySqlConnection connection = new MySqlConnection(this.ConnectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter("select * from " + dt.TableName + " where 1=0", connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        foreach (DataRow row in dt.Rows)
                        {
                            dataTable.ImportRow(row);
                        }
                        MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
                        num2 = adapter.Update(dataTable);
                    }
                }
                catch (MySqlException exception)
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
            using (MySqlConnection connection = new MySqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand())
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
            using (MySqlConnection connection = new MySqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Prepare();
                    num = command.ExecuteNonQuery();
                }
            }
            return num;
        }

        public int Execute(List<string> sqlList, List<MySqlParameter[]> parameterList)
        {
            int num3;
            if (sqlList.Count > parameterList.Count)
            {
                throw new Exception("参数错误");
            }
            using (MySqlConnection connection = new MySqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand())
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

        public int Execute(string sql, MySqlParameter[] parameter, [Optional, DefaultParameterValue(false)] bool returnIdentity)
        {
            int num2;
            using (MySqlConnection connection = new MySqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(sql, connection))
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
            using (MySqlConnection connection = new MySqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    object obj2 = command.ExecuteScalar();
                    command.Prepare();
                    str = (obj2 != null) ? obj2.ToString() : string.Empty;
                }
            }
            return str;
        }

        public string ExecuteScalar(string sql, MySqlParameter[] parameter)
        {
            string str;
            using (MySqlConnection connection = new MySqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(sql, connection))
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

        public MySqlDataReader GetDataReader(string sql)
        {
            MySqlConnection connection = new MySqlConnection(this.ConnectionString);
            connection.Open();
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.CommandTimeout = 180;
                command.Prepare();
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }

        public MySqlDataReader GetDataReader(string sql, MySqlParameter[] parameter)
        {
            MySqlConnection connection = new MySqlConnection(this.ConnectionString);
            connection.Open();
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.CommandTimeout = 180;
                if ((parameter != null) && (parameter.Length > 0))
                {
                    command.Parameters.AddRange(parameter);
                }
                MySqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                command.Parameters.Clear();
                command.Prepare();
                return reader;
            }
        }

        public DataSet GetDataSet(string sql)
        {
            DataSet set2;
            using (MySqlConnection connection = new MySqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(sql, connection))
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
            using (MySqlConnection connection = new MySqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(sql, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    adapter.Dispose();
                    table2 = dataTable;
                }
            }
            return table2;
        }

        public DataTable GetDataTable(string sql, MySqlParameter[] parameter)
        {
            DataTable table2;
            using (MySqlConnection connection = new MySqlConnection(this.ConnectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    if ((parameter != null) && (parameter.Length > 0))
                    {
                        command.Parameters.AddRange(parameter);
                    }
                    command.CommandTimeout = 180;
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
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

        public string GetFields(string sql, MySqlParameter[] param)
        {
            string str;
            using (MySqlConnection connection = new MySqlConnection(this.ConnectionString))
            {
                connection.Open();
                StringBuilder builder = new StringBuilder(500);
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    if ((param != null) && (param.Length > 0))
                    {
                        command.Parameters.AddRange(param);
                    }
                    MySqlDataReader reader = command.ExecuteReader(CommandBehavior.SchemaOnly);
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

        public string GetFields(string sql, MySqlParameter[] param, out string tableName)
        {
            string str;
            using (MySqlConnection connection = new MySqlConnection(this.ConnectionString))
            {
                connection.Open();
                StringBuilder builder = new StringBuilder(500);
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    if ((param != null) && (param.Length > 0))
                    {
                        command.Parameters.AddRange(param);
                    }
                    MySqlDataReader reader = command.ExecuteReader(CommandBehavior.SchemaOnly);
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

        public string GetFieldValue(string sql, MySqlParameter[] parameter)
        {
            return this.ExecuteScalar(sql, parameter);
        }

        public string GetPaerSql(string sql, int size, int number, out long count, [Optional, DefaultParameterValue(null)] MySqlParameter[] param)
        {
            long num;
            string fieldValue = this.GetFieldValue(string.Format("select count(*) from ({0}) PagerCountTemp", sql), param);
            count = fieldValue.IsLong(out num) ? num : 0L;
            if (count < (((number * size) - size) + 1))
            {
                number = 1;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from (");
            builder.Append(sql);
            builder.AppendFormat(") PagerTempTable", new object[0]);
            if (count > size)
            {
                builder.AppendFormat(" LIMIT {0},{1}", (number * size) - size, size);
            }
            return builder.ToString();
        }

        public string GetPaerSql(string table, string fileds, string where, string order, int size, int number, out long count, [Optional, DefaultParameterValue(null)] MySqlParameter[] param)
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
            object[] args = new object[] { fileds, table, str2, order };
            string str3 = string.Format("SELECT PagerTempTable.* FROM (SELECT {0} FROM {1} {2} ORDER BY {3}) PagerTempTable", args);
            string fieldValue = this.GetFieldValue(string.Format("SELECT COUNT(*) FROM {0} {1}", table, str2), param);
            count = fieldValue.IsLong(out num) ? num : 0L;
            if (count < (((number * size) - size) + 1))
            {
                number = 1;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM (");
            builder.Append(str3);
            builder.AppendFormat(") PagerTempTable", new object[0]);
            if (count > size)
            {
                builder.AppendFormat(" LIMIT {0},{1}", (number * size) - size, size);
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

