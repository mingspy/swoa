namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;
    using YJ.Data.Interface;
    using YJ.Data.Model;
    using YJ.Utility;

    public class Log : ILog
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.Log model)
        {
            string sql = "INSERT INTO Log\r\n\t\t\t\t(ID,Title,Type,WriteTime,UserID,UserName,IPAddress,URL,Contents,Others,OldXml,NewXml) \r\n\t\t\t\tVALUES(@ID,@Title,@Type,@WriteTime,@UserID,@UserName,@IPAddress,@URL,@Contents,@Others,@OldXml,@NewXml)";
            SqlParameter[] parameterArray1 = new SqlParameter[12];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Title", SqlDbType.NVarChar, -1) {
                Value = model.Title
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Type", SqlDbType.NVarChar, 100) {
                Value = model.Type
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@WriteTime", SqlDbType.DateTime, 8) {
                Value = model.WriteTime
            };
            parameterArray1[3] = parameter4;
            parameterArray1[4] = !model.UserID.HasValue ? new SqlParameter("@UserID", SqlDbType.UniqueIdentifier, -1) { Value = DBNull.Value } : new SqlParameter("@UserID", SqlDbType.UniqueIdentifier, -1) { Value = model.UserID };
            parameterArray1[5] = (model.UserName == null) ? new SqlParameter("@UserName", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@UserName", SqlDbType.NVarChar, 100) { Value = model.UserName };
            parameterArray1[6] = (model.IPAddress == null) ? new SqlParameter("@IPAddress", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@IPAddress", SqlDbType.VarChar, 50) { Value = model.IPAddress };
            parameterArray1[7] = (model.URL == null) ? new SqlParameter("@URL", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@URL", SqlDbType.VarChar, -1) { Value = model.URL };
            parameterArray1[8] = (model.Contents == null) ? new SqlParameter("@Contents", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Contents", SqlDbType.VarChar, -1) { Value = model.Contents };
            parameterArray1[9] = (model.Others == null) ? new SqlParameter("@Others", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Others", SqlDbType.VarChar, -1) { Value = model.Others };
            parameterArray1[10] = (model.OldXml == null) ? new SqlParameter("@OldXml", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@OldXml", SqlDbType.VarChar, -1) { Value = model.OldXml };
            parameterArray1[11] = (model.NewXml == null) ? new SqlParameter("@NewXml", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@NewXml", SqlDbType.VarChar, -1) { Value = model.NewXml };
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.Log> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.Log> list = new List<YJ.Data.Model.Log>();
            YJ.Data.Model.Log item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.Log {
                    ID = dataReader.GetGuid(0),
                    Title = dataReader.GetString(1),
                    Type = dataReader.GetString(2),
                    WriteTime = dataReader.GetDateTime(3)
                };
                if (!dataReader.IsDBNull(4))
                {
                    item.UserID = new Guid?(dataReader.GetGuid(4));
                }
                if (!dataReader.IsDBNull(5))
                {
                    item.UserName = dataReader.GetString(5);
                }
                if (!dataReader.IsDBNull(6))
                {
                    item.IPAddress = dataReader.GetString(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    item.URL = dataReader.GetString(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    item.Contents = dataReader.GetString(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    item.Others = dataReader.GetString(9);
                }
                if (!dataReader.IsDBNull(10))
                {
                    item.OldXml = dataReader.GetString(10);
                }
                if (!dataReader.IsDBNull(11))
                {
                    item.NewXml = dataReader.GetString(11);
                }
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM Log WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.Log Get(Guid id)
        {
            string sql = "SELECT * FROM Log WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Log> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.Log> GetAll()
        {
            string sql = "SELECT * FROM Log";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Log> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM Log";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public DataTable GetPagerData(out long count, [Optional, DefaultParameterValue(15)] int size, [Optional, DefaultParameterValue(1)] int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string type, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue("")] string userID, [Optional, DefaultParameterValue("")] string order)
        {
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            if (!title.IsNullOrEmpty())
            {
                builder.Append("AND CHARINDEX(@Title,Title)>0 ");
                SqlParameter item = new SqlParameter("@Title", SqlDbType.NVarChar) {
                    Value = title
                };
                list.Add(item);
            }
            if (!type.IsNullOrEmpty())
            {
                builder.Append("AND Type=@Type ");
                SqlParameter parameter2 = new SqlParameter("@Type", SqlDbType.NVarChar) {
                    Value = type
                };
                list.Add(parameter2);
            }
            if (date1.IsDateTime())
            {
                builder.Append("AND WriteTime>=@Date1 ");
                SqlParameter parameter3 = new SqlParameter("@Date1", SqlDbType.DateTime) {
                    Value = date1.ToDateTime().ToString("yyyy-MM-dd 00:00:00")
                };
                list.Add(parameter3);
            }
            if (date2.IsDateTime())
            {
                builder.Append("AND WriteTime<=@Date2 ");
                SqlParameter parameter4 = new SqlParameter("@Date2", SqlDbType.DateTime) {
                    Value = date2.ToDateTime().AddDays(1.0).ToString("yyyy-MM-dd 00:00:00")
                };
                list.Add(parameter4);
            }
            if (userID.IsGuid())
            {
                builder.Append("AND UserID=@UserID ");
                SqlParameter parameter5 = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier) {
                    Value = userID.ToGuid()
                };
                list.Add(parameter5);
            }
            string str = this.dbHelper.GetPaerSql("Log", "ID,Title,Type,WriteTime,UserName,IPAddress", builder.ToString(), order.IsNullOrEmpty() ? "WriteTime DESC" : order, size, number, out count, list.ToArray());
            return this.dbHelper.GetDataTable(str.ToString(), list.ToArray());
        }

        public DataTable GetPagerData(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue(15)] int size, [Optional, DefaultParameterValue(1)] int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string type, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue("")] string userID)
        {
            long num;
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            if (!title.IsNullOrEmpty())
            {
                builder.Append("AND CHARINDEX(@Title,Title)>0 ");
                SqlParameter item = new SqlParameter("@Title", SqlDbType.NVarChar) {
                    Value = title
                };
                list.Add(item);
            }
            if (!type.IsNullOrEmpty())
            {
                builder.Append("AND Type=@Type ");
                SqlParameter parameter2 = new SqlParameter("@Type", SqlDbType.NVarChar) {
                    Value = type
                };
                list.Add(parameter2);
            }
            if (date1.IsDateTime())
            {
                builder.Append("AND WriteTime>=@Date1 ");
                SqlParameter parameter3 = new SqlParameter("@Date1", SqlDbType.DateTime) {
                    Value = date1.ToDateTime().ToString("yyyy-MM-dd 00:00:00")
                };
                list.Add(parameter3);
            }
            if (date2.IsDateTime())
            {
                builder.Append("AND WriteTime<=@Date2 ");
                SqlParameter parameter4 = new SqlParameter("@Date2", SqlDbType.DateTime) {
                    Value = date2.ToDateTime().AddDays(1.0).ToString("yyyy-MM-dd 00:00:00")
                };
                list.Add(parameter4);
            }
            if (userID.IsGuid())
            {
                builder.Append("AND UserID=@UserID ");
                SqlParameter parameter5 = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier) {
                    Value = userID.ToGuid()
                };
                list.Add(parameter5);
            }
            string sql = this.dbHelper.GetPaerSql("Log", "ID,Title,Type,WriteTime,UserName,IPAddress", builder.ToString(), "WriteTime DESC", size, number, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, size, number, query);
            return this.dbHelper.GetDataTable(sql, list.ToArray());
        }

        public int Update(YJ.Data.Model.Log model)
        {
            string sql = "UPDATE Log SET \r\n\t\t\t\tTitle=@Title,Type=@Type,WriteTime=@WriteTime,UserID=@UserID,UserName=@UserName,IPAddress=@IPAddress,URL=@URL,Contents=@Contents,Others=@Others,OldXml=@OldXml,NewXml=@NewXml\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[12];
            SqlParameter parameter1 = new SqlParameter("@Title", SqlDbType.NVarChar, -1) {
                Value = model.Title
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Type", SqlDbType.NVarChar, 100) {
                Value = model.Type
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@WriteTime", SqlDbType.DateTime, 8) {
                Value = model.WriteTime
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = !model.UserID.HasValue ? new SqlParameter("@UserID", SqlDbType.UniqueIdentifier, -1) { Value = DBNull.Value } : new SqlParameter("@UserID", SqlDbType.UniqueIdentifier, -1) { Value = model.UserID };
            parameterArray1[4] = (model.UserName == null) ? new SqlParameter("@UserName", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@UserName", SqlDbType.NVarChar, 100) { Value = model.UserName };
            parameterArray1[5] = (model.IPAddress == null) ? new SqlParameter("@IPAddress", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@IPAddress", SqlDbType.VarChar, 50) { Value = model.IPAddress };
            parameterArray1[6] = (model.URL == null) ? new SqlParameter("@URL", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@URL", SqlDbType.VarChar, -1) { Value = model.URL };
            parameterArray1[7] = (model.Contents == null) ? new SqlParameter("@Contents", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Contents", SqlDbType.VarChar, -1) { Value = model.Contents };
            parameterArray1[8] = (model.Others == null) ? new SqlParameter("@Others", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Others", SqlDbType.VarChar, -1) { Value = model.Others };
            parameterArray1[9] = (model.OldXml == null) ? new SqlParameter("@OldXml", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@OldXml", SqlDbType.VarChar, -1) { Value = model.OldXml };
            parameterArray1[10] = (model.NewXml == null) ? new SqlParameter("@NewXml", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@NewXml", SqlDbType.VarChar, -1) { Value = model.NewXml };
            SqlParameter parameter20 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[11] = parameter20;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

