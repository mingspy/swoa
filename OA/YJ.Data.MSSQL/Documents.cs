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

    public class Documents : IDocuments
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.Documents model)
        {
            string sql = "INSERT INTO Documents\r\n\t\t\t\t(ID,DirectoryID,DirectoryName,Title,Source,Contents,Files,WriteTime,WriteUserID,WriteUserName,EditTime,EditUserID,EditUserName,ReadUsers,ReadCount) \r\n\t\t\t\tVALUES(@ID,@DirectoryID,@DirectoryName,@Title,@Source,@Contents,@Files,@WriteTime,@WriteUserID,@WriteUserName,@EditTime,@EditUserID,@EditUserName,@ReadUsers,@ReadCount)";
            SqlParameter[] parameterArray1 = new SqlParameter[15];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@DirectoryID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.DirectoryID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@DirectoryName", SqlDbType.NVarChar, 400) {
                Value = model.DirectoryName
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@Title", SqlDbType.NVarChar, 600) {
                Value = model.Title
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@Source", SqlDbType.NVarChar, 100) {
                Value = model.Source
            };
            parameterArray1[4] = parameter5;
            SqlParameter parameter6 = new SqlParameter("@Contents", SqlDbType.VarChar, -1) {
                Value = model.Contents
            };
            parameterArray1[5] = parameter6;
            parameterArray1[6] = (model.Files == null) ? new SqlParameter("@Files", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Files", SqlDbType.VarChar, -1) { Value = model.Files };
            SqlParameter parameter9 = new SqlParameter("@WriteTime", SqlDbType.DateTime, 8) {
                Value = model.WriteTime
            };
            parameterArray1[7] = parameter9;
            SqlParameter parameter10 = new SqlParameter("@WriteUserID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.WriteUserID
            };
            parameterArray1[8] = parameter10;
            SqlParameter parameter11 = new SqlParameter("@WriteUserName", SqlDbType.NVarChar, 100) {
                Value = model.WriteUserName
            };
            parameterArray1[9] = parameter11;
            parameterArray1[10] = !model.EditTime.HasValue ? new SqlParameter("@EditTime", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@EditTime", SqlDbType.DateTime, 8) { Value = model.EditTime };
            parameterArray1[11] = !model.EditUserID.HasValue ? new SqlParameter("@EditUserID", SqlDbType.UniqueIdentifier, -1) { Value = DBNull.Value } : new SqlParameter("@EditUserID", SqlDbType.UniqueIdentifier, -1) { Value = model.EditUserID };
            parameterArray1[12] = (model.EditUserName == null) ? new SqlParameter("@EditUserName", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@EditUserName", SqlDbType.NVarChar, 100) { Value = model.EditUserName };
            parameterArray1[13] = (model.ReadUsers == null) ? new SqlParameter("@ReadUsers", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@ReadUsers", SqlDbType.VarChar, -1) { Value = model.ReadUsers };
            SqlParameter parameter20 = new SqlParameter("@ReadCount", SqlDbType.Int, -1) {
                Value = model.ReadCount
            };
            parameterArray1[14] = parameter20;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.Documents> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.Documents> list = new List<YJ.Data.Model.Documents>();
            YJ.Data.Model.Documents item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.Documents {
                    ID = dataReader.GetGuid(0),
                    DirectoryID = dataReader.GetGuid(1),
                    DirectoryName = dataReader.GetString(2),
                    Title = dataReader.GetString(3),
                    Source = dataReader.GetString(4),
                    Contents = dataReader.GetString(5)
                };
                if (!dataReader.IsDBNull(6))
                {
                    item.Files = dataReader.GetString(6);
                }
                item.WriteTime = dataReader.GetDateTime(7);
                item.WriteUserID = dataReader.GetGuid(8);
                item.WriteUserName = dataReader.GetString(9);
                if (!dataReader.IsDBNull(10))
                {
                    item.EditTime = new DateTime?(dataReader.GetDateTime(10));
                }
                if (!dataReader.IsDBNull(11))
                {
                    item.EditUserID = new Guid?(dataReader.GetGuid(11));
                }
                if (!dataReader.IsDBNull(12))
                {
                    item.EditUserName = dataReader.GetString(12);
                }
                if (!dataReader.IsDBNull(13))
                {
                    item.ReadUsers = dataReader.GetString(13);
                }
                item.ReadCount = dataReader.GetInt32(14);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM Documents WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int DeleteByDirectoryID(Guid directoryID)
        {
            string sql = "DELETE FROM Documents WHERE DirectoryID=@DirectoryID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@DirectoryID", SqlDbType.UniqueIdentifier) {
                Value = directoryID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.Documents Get(Guid id)
        {
            string sql = "SELECT * FROM Documents WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Documents> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.Documents> GetAll()
        {
            string sql = "SELECT * FROM Documents";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Documents> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM Documents";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public DataTable GetList(out string pager, string dirID, string userID, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(false)] bool isNoRead)
        {
            long num;
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder("SELECT ID,DirectoryID,DirectoryName,Title,WriteTime,WriteUserName,IsRead,ReadCount,ROW_NUMBER() OVER(ORDER BY WriteTime DESC) AS PagerAutoRowNumber FROM Documents a LEFT JOIN DocumentsReadUsers b ON a.ID=b.DocumentID WHERE b.UserID=@UserID");
            SqlParameter item = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier) {
                Value = userID.ToGuid()
            };
            list.Add(item);
            if (isNoRead)
            {
                builder.Append(" AND IsRead=0");
            }
            if (!dirID.IsNullOrEmpty())
            {
                builder.Append(" AND DirectoryID IN(" + Tools.GetSqlInString(dirID, true, ",") + ")");
            }
            else
            {
                builder.Append(isNoRead ? "" : " AND 1=0");
            }
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND CHARINDEX(@Title,Title)>0");
                SqlParameter parameter2 = new SqlParameter("@Title", SqlDbType.NVarChar) {
                    Value = title
                };
                list.Add(parameter2);
            }
            if (date1.IsDateTime())
            {
                builder.Append(" AND WriteTime>=@WriteTime");
                SqlParameter parameter3 = new SqlParameter("@WriteTime", SqlDbType.DateTime) {
                    Value = date1.ToDateTime().Date
                };
                list.Add(parameter3);
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND WriteTime<=@WriteTime1");
                SqlParameter parameter4 = new SqlParameter("@WriteTime1", SqlDbType.DateTime) {
                    Value = date2.ToDateTime().AddDays(1.0).Date
                };
                list.Add(parameter4);
            }
            int pageSize = Tools.GetPageSize();
            int pageNumber = Tools.GetPageNumber();
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), pageSize, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, pageSize, pageNumber, query);
            return this.dbHelper.GetDataTable(sql, list.ToArray());
        }

        public DataTable GetList(out long count, int size, int number, string dirID, string userID, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(false)] bool isNoRead, [Optional, DefaultParameterValue("")] string order)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder("SELECT ID,DirectoryID,DirectoryName,Title,WriteTime,WriteUserName,IsRead,ReadCount,ROW_NUMBER() OVER(ORDER BY " + (order.IsNullOrEmpty() ? "WriteTime DESC" : order) + ") AS PagerAutoRowNumber FROM Documents a LEFT JOIN DocumentsReadUsers b ON a.ID=b.DocumentID WHERE b.UserID=@UserID");
            SqlParameter item = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier) {
                Value = userID.ToGuid()
            };
            list.Add(item);
            if (isNoRead)
            {
                builder.Append(" AND IsRead=0");
            }
            if (!dirID.IsNullOrEmpty())
            {
                builder.Append(" AND DirectoryID IN(" + Tools.GetSqlInString(dirID, true, ",") + ")");
            }
            else
            {
                builder.Append(isNoRead ? "" : " AND 1=0");
            }
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND CHARINDEX(@Title,Title)>0");
                SqlParameter parameter2 = new SqlParameter("@Title", SqlDbType.NVarChar) {
                    Value = title
                };
                list.Add(parameter2);
            }
            if (date1.IsDateTime())
            {
                builder.Append(" AND WriteTime>=@WriteTime");
                SqlParameter parameter3 = new SqlParameter("@WriteTime", SqlDbType.DateTime) {
                    Value = date1.ToDateTime().Date
                };
                list.Add(parameter3);
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND WriteTime<=@WriteTime1");
                SqlParameter parameter4 = new SqlParameter("@WriteTime1", SqlDbType.DateTime) {
                    Value = date2.ToDateTime().AddDays(1.0).Date
                };
                list.Add(parameter4);
            }
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), size, number, out count, list.ToArray());
            return this.dbHelper.GetDataTable(sql, list.ToArray());
        }

        public int Update(YJ.Data.Model.Documents model)
        {
            string sql = "UPDATE Documents SET \r\n\t\t\t\tDirectoryID=@DirectoryID,DirectoryName=@DirectoryName,Title=@Title,Source=@Source,Contents=@Contents,Files=@Files,WriteTime=@WriteTime,WriteUserID=@WriteUserID,WriteUserName=@WriteUserName,EditTime=@EditTime,EditUserID=@EditUserID,EditUserName=@EditUserName,ReadUsers=@ReadUsers,ReadCount=@ReadCount\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[15];
            SqlParameter parameter1 = new SqlParameter("@DirectoryID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.DirectoryID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@DirectoryName", SqlDbType.NVarChar, 400) {
                Value = model.DirectoryName
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Title", SqlDbType.NVarChar, 600) {
                Value = model.Title
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@Source", SqlDbType.NVarChar, 100) {
                Value = model.Source
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@Contents", SqlDbType.VarChar, -1) {
                Value = model.Contents
            };
            parameterArray1[4] = parameter5;
            parameterArray1[5] = (model.Files == null) ? new SqlParameter("@Files", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Files", SqlDbType.VarChar, -1) { Value = model.Files };
            SqlParameter parameter8 = new SqlParameter("@WriteTime", SqlDbType.DateTime, 8) {
                Value = model.WriteTime
            };
            parameterArray1[6] = parameter8;
            SqlParameter parameter9 = new SqlParameter("@WriteUserID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.WriteUserID
            };
            parameterArray1[7] = parameter9;
            SqlParameter parameter10 = new SqlParameter("@WriteUserName", SqlDbType.NVarChar, 100) {
                Value = model.WriteUserName
            };
            parameterArray1[8] = parameter10;
            parameterArray1[9] = !model.EditTime.HasValue ? new SqlParameter("@EditTime", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@EditTime", SqlDbType.DateTime, 8) { Value = model.EditTime };
            parameterArray1[10] = !model.EditUserID.HasValue ? new SqlParameter("@EditUserID", SqlDbType.UniqueIdentifier, -1) { Value = DBNull.Value } : new SqlParameter("@EditUserID", SqlDbType.UniqueIdentifier, -1) { Value = model.EditUserID };
            parameterArray1[11] = (model.EditUserName == null) ? new SqlParameter("@EditUserName", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@EditUserName", SqlDbType.NVarChar, 100) { Value = model.EditUserName };
            parameterArray1[12] = (model.ReadUsers == null) ? new SqlParameter("@ReadUsers", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@ReadUsers", SqlDbType.VarChar, -1) { Value = model.ReadUsers };
            SqlParameter parameter19 = new SqlParameter("@ReadCount", SqlDbType.Int, -1) {
                Value = model.ReadCount
            };
            parameterArray1[13] = parameter19;
            SqlParameter parameter20 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[14] = parameter20;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public void UpdateReadCount(Guid id)
        {
            string sql = "UPDATE Documents SET ReadCount=ReadCount+1 WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

