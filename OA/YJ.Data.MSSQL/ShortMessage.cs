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

    public class ShortMessage : IShortMessage
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.ShortMessage model)
        {
            string sql = "INSERT INTO ShortMessage\r\n\t\t\t\t(ID,Title,Contents,SendUserID,SendUserName,ReceiveUserID,ReceiveUserName,SendTime,LinkUrl,LinkID,Type,Files,GroupID) \r\n\t\t\t\tVALUES(@ID,@Title,@Contents,@SendUserID,@SendUserName,@ReceiveUserID,@ReceiveUserName,@SendTime,@LinkUrl,@LinkID,@Type,@Files,@GroupID)";
            SqlParameter[] parameterArray1 = new SqlParameter[13];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Title", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Title
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = (model.Contents == null) ? new SqlParameter("@Contents", SqlDbType.NVarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Contents", SqlDbType.NVarChar, -1) { Value = model.Contents };
            SqlParameter parameter5 = new SqlParameter("@SendUserID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.SendUserID
            };
            parameterArray1[3] = parameter5;
            SqlParameter parameter6 = new SqlParameter("@SendUserName", SqlDbType.NVarChar, 0x3e8) {
                Value = model.SendUserName
            };
            parameterArray1[4] = parameter6;
            SqlParameter parameter7 = new SqlParameter("@ReceiveUserID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ReceiveUserID
            };
            parameterArray1[5] = parameter7;
            SqlParameter parameter8 = new SqlParameter("@ReceiveUserName", SqlDbType.NVarChar, 0x3e8) {
                Value = model.ReceiveUserName
            };
            parameterArray1[6] = parameter8;
            SqlParameter parameter9 = new SqlParameter("@SendTime", SqlDbType.DateTime, 8) {
                Value = model.SendTime
            };
            parameterArray1[7] = parameter9;
            parameterArray1[8] = (model.LinkUrl == null) ? new SqlParameter("@LinkUrl", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@LinkUrl", SqlDbType.VarChar, -1) { Value = model.LinkUrl };
            parameterArray1[9] = (model.LinkID == null) ? new SqlParameter("@LinkID", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@LinkID", SqlDbType.VarChar, 50) { Value = model.LinkID };
            SqlParameter parameter14 = new SqlParameter("@Type", SqlDbType.Int, -1) {
                Value = model.Type
            };
            parameterArray1[10] = parameter14;
            parameterArray1[11] = (model.Files == null) ? new SqlParameter("@Files", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Files", SqlDbType.VarChar, -1) { Value = model.Files };
            parameterArray1[12] = (model.GroupID == null) ? new SqlParameter("@GroupID", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@GroupID", SqlDbType.VarChar, 50) { Value = model.GroupID };
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.ShortMessage> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.ShortMessage> list = new List<YJ.Data.Model.ShortMessage>();
            YJ.Data.Model.ShortMessage item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.ShortMessage {
                    ID = dataReader.GetGuid(0),
                    Title = dataReader.GetString(1)
                };
                if (!dataReader.IsDBNull(2))
                {
                    item.Contents = dataReader.GetString(2);
                }
                item.SendUserID = dataReader.GetGuid(3);
                item.SendUserName = dataReader.GetString(4);
                item.ReceiveUserID = dataReader.GetGuid(5);
                item.ReceiveUserName = dataReader.GetString(6);
                item.SendTime = dataReader.GetDateTime(7);
                if (!dataReader.IsDBNull(8))
                {
                    item.LinkUrl = dataReader.GetString(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    item.LinkID = dataReader.GetString(9);
                }
                item.Type = dataReader.GetInt32(10);
                if (!dataReader.IsDBNull(11))
                {
                    item.Files = dataReader.GetString(11);
                }
                if (!dataReader.IsDBNull(12))
                {
                    item.GroupID = dataReader.GetString(12);
                }
                if (dataReader.FieldCount > 12)
                {
                    item.Status = dataReader.GetInt32(13);
                }
                else
                {
                    item.Status = 0;
                }
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM ShortMessage WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int Delete(string linkID, int Type)
        {
            string sql = "DELETE FROM ShortMessage WHERE LinkID=@LinkID AND Type=@Type";
            SqlParameter[] parameterArray1 = new SqlParameter[2];
            SqlParameter parameter1 = new SqlParameter("@LinkID", SqlDbType.VarChar) {
                Value = linkID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Type", SqlDbType.Int) {
                Value = Type
            };
            parameterArray1[1] = parameter2;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.ShortMessage Get(Guid id)
        {
            string sql = "SELECT *,0 AS Status FROM ShortMessage WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ShortMessage> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.ShortMessage> GetAll()
        {
            string sql = "SELECT *,0 AS Status FROM ShortMessage";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.ShortMessage> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.ShortMessage> GetAllNoRead()
        {
            string sql = "SELECT *,0 AS Status FROM ShortMessage";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.ShortMessage> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.ShortMessage> GetAllNoReadByUserID(Guid userID)
        {
            string sql = "SELECT *,0 AS Status FROM ShortMessage WHERE ReceiveUserID=@ReceiveUserID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ReceiveUserID", SqlDbType.UniqueIdentifier) {
                Value = userID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ShortMessage> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM ShortMessage";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public List<YJ.Data.Model.ShortMessage> GetList(int[] status, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string contents, [Optional, DefaultParameterValue("")] string senderID, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue("")] string receiveID, [Optional, DefaultParameterValue("")] string order)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            string str = string.Empty;
            if ((status.Length == 1) && (status[0] == 0))
            {
                str = "SELECT *,0 AS Status,ROW_NUMBER() OVER(ORDER BY " + (order.IsNullOrEmpty() ? "SendTime DESC" : order) + ") AS PagerAutoRowNumber FROM ShortMessage WHERE 1=1";
            }
            else if ((status.Length == 1) && (status[0] == 1))
            {
                str = "SELECT *,1 AS Status,ROW_NUMBER() OVER(ORDER BY " + (order.IsNullOrEmpty() ? "SendTime DESC" : order) + ") AS PagerAutoRowNumber FROM ShortMessage1 WHERE 1=1";
            }
            else if (status.Length == 2)
            {
                str = "SELECT *,ROW_NUMBER() OVER(ORDER BY " + (order.IsNullOrEmpty() ? "SendTime DESC" : order) + ") AS PagerAutoRowNumber FROM(SELECT *,0 AS Status FROM ShortMessage WHERE 1=1 UNION ALL SELECT *,1 AS Status FROM ShortMessage1 WHERE 1=1) temp WHERE 1=1";
            }
            StringBuilder builder = new StringBuilder(str);
            if (receiveID.IsGuid())
            {
                builder.Append(" AND ReceiveUserID=@ReceiveUserID");
                list.Add(new SqlParameter("@ReceiveUserID", receiveID));
            }
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND CHARINDEX(@Title,Title)>0");
                list.Add(new SqlParameter("@Title", title));
            }
            if (!contents.IsNullOrEmpty())
            {
                builder.Append(" AND CHARINDEX(@Contents,Contents)>0");
                list.Add(new SqlParameter("@Contents", contents));
            }
            if (!senderID.IsNullOrEmpty())
            {
                builder.AppendFormat(" AND SendUserID IN({0})", senderID);
            }
            if (date1.IsDateTime())
            {
                builder.Append(" AND SendTime>=@SendTime");
                list.Add(new SqlParameter("@SendTime", date1.ToDateTime()));
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND SendTime<=@SendTime1");
                list.Add(new SqlParameter("@SendTime1", date2.ToDateTime().AddDays(1.0).ToDateString()));
            }
            SqlDataReader dataReader = this.dbHelper.GetDataReader(builder.ToString(), list.ToArray());
            List<YJ.Data.Model.ShortMessage> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public List<YJ.Data.Model.ShortMessage> GetList(out string pager, int[] status, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string contents, [Optional, DefaultParameterValue("")] string senderID, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue("")] string receiveID)
        {
            long num;
            List<SqlParameter> list = new List<SqlParameter>();
            string str = string.Empty;
            if ((status.Length == 1) && (status[0] == 0))
            {
                str = "SELECT *,0 AS Status,ROW_NUMBER() OVER(ORDER BY SendTime DESC) AS PagerAutoRowNumber FROM ShortMessage WHERE 1=1";
            }
            else if ((status.Length == 1) && (status[0] == 1))
            {
                str = "SELECT *,1 AS Status,ROW_NUMBER() OVER(ORDER BY SendTime DESC) AS PagerAutoRowNumber FROM ShortMessage1 WHERE 1=1";
            }
            else if (status.Length == 2)
            {
                str = "SELECT *,ROW_NUMBER() OVER(ORDER BY SendTime DESC) AS PagerAutoRowNumber FROM(SELECT *,0 AS Status FROM ShortMessage WHERE 1=1 UNION ALL SELECT *,1 AS Status FROM ShortMessage1 WHERE 1=1) temp WHERE 1=1";
            }
            StringBuilder builder = new StringBuilder(str);
            if (receiveID.IsGuid())
            {
                builder.Append(" AND ReceiveUserID=@ReceiveUserID");
                list.Add(new SqlParameter("@ReceiveUserID", receiveID));
            }
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND CHARINDEX(@Title,Title)>0");
                list.Add(new SqlParameter("@Title", title));
            }
            if (!contents.IsNullOrEmpty())
            {
                builder.Append(" AND CHARINDEX(@Contents,Contents)>0");
                list.Add(new SqlParameter("@Contents", contents));
            }
            if (!senderID.IsNullOrEmpty())
            {
                builder.AppendFormat(" AND SendUserID IN({0})", senderID);
            }
            if (date1.IsDateTime())
            {
                builder.Append(" AND SendTime>=@SendTime");
                list.Add(new SqlParameter("@SendTime", date1.ToDateTime()));
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND SendTime<=@SendTime1");
                list.Add(new SqlParameter("@SendTime1", date2.ToDateTime().AddDays(1.0).ToDateString()));
            }
            int pageSize = Tools.GetPageSize();
            int pageNumber = Tools.GetPageNumber();
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), pageSize, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, pageSize, pageNumber, query);
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.ShortMessage> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public List<YJ.Data.Model.ShortMessage> GetList(out long count, int[] status, int size, int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string contents, [Optional, DefaultParameterValue("")] string senderID, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue("")] string receiveID, [Optional, DefaultParameterValue("")] string order)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            string str = string.Empty;
            if ((status.Length == 1) && (status[0] == 0))
            {
                str = "SELECT *,0 AS Status,ROW_NUMBER() OVER(ORDER BY " + (order.IsNullOrEmpty() ? "SendTime DESC" : order) + ") AS PagerAutoRowNumber FROM ShortMessage WHERE 1=1";
            }
            else if ((status.Length == 1) && (status[0] == 1))
            {
                str = "SELECT *,1 AS Status,ROW_NUMBER() OVER(ORDER BY " + (order.IsNullOrEmpty() ? "SendTime DESC" : order) + ") AS PagerAutoRowNumber FROM ShortMessage1 WHERE 1=1";
            }
            else if (status.Length == 2)
            {
                str = "SELECT *,ROW_NUMBER() OVER(ORDER BY " + (order.IsNullOrEmpty() ? "SendTime DESC" : order) + ") AS PagerAutoRowNumber FROM(SELECT *,0 AS Status FROM ShortMessage WHERE 1=1 UNION ALL SELECT *,1 AS Status FROM ShortMessage1 WHERE 1=1) temp WHERE 1=1";
            }
            StringBuilder builder = new StringBuilder(str);
            if (receiveID.IsGuid())
            {
                builder.Append(" AND ReceiveUserID=@ReceiveUserID");
                list.Add(new SqlParameter("@ReceiveUserID", receiveID));
            }
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND CHARINDEX(@Title,Title)>0");
                list.Add(new SqlParameter("@Title", title));
            }
            if (!contents.IsNullOrEmpty())
            {
                builder.Append(" AND CHARINDEX(@Contents,Contents)>0");
                list.Add(new SqlParameter("@Contents", contents));
            }
            if (!senderID.IsNullOrEmpty())
            {
                builder.AppendFormat(" AND SendUserID IN({0})", senderID);
            }
            if (date1.IsDateTime())
            {
                builder.Append(" AND SendTime>=@SendTime");
                list.Add(new SqlParameter("@SendTime", date1.ToDateTime()));
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND SendTime<=@SendTime1");
                list.Add(new SqlParameter("@SendTime1", date2.ToDateTime().AddDays(1.0).ToDateString()));
            }
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), size, number, out count, list.ToArray());
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.ShortMessage> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public YJ.Data.Model.ShortMessage GetRead(Guid id)
        {
            string sql = "SELECT *,0 AS Status FROM ShortMessage1 WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ShortMessage> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public int Update(YJ.Data.Model.ShortMessage model)
        {
            string sql = "UPDATE ShortMessage SET \r\n\t\t\t\tTitle=@Title,Contents=@Contents,SendUserID=@SendUserID,SendUserName=@SendUserName,ReceiveUserID=@ReceiveUserID,ReceiveUserName=@ReceiveUserName,SendTime=@SendTime,LinkUrl=@LinkUrl,LinkID=@LinkID,Type=@Type,Files=@Files,@GroupID=GroupID\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[13];
            SqlParameter parameter1 = new SqlParameter("@Title", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Title
            };
            parameterArray1[0] = parameter1;
            parameterArray1[1] = (model.Contents == null) ? new SqlParameter("@Contents", SqlDbType.NVarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Contents", SqlDbType.NVarChar, -1) { Value = model.Contents };
            SqlParameter parameter4 = new SqlParameter("@SendUserID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.SendUserID
            };
            parameterArray1[2] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@SendUserName", SqlDbType.NVarChar, 0x3e8) {
                Value = model.SendUserName
            };
            parameterArray1[3] = parameter5;
            SqlParameter parameter6 = new SqlParameter("@ReceiveUserID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ReceiveUserID
            };
            parameterArray1[4] = parameter6;
            SqlParameter parameter7 = new SqlParameter("@ReceiveUserName", SqlDbType.NVarChar, 0x3e8) {
                Value = model.ReceiveUserName
            };
            parameterArray1[5] = parameter7;
            SqlParameter parameter8 = new SqlParameter("@SendTime", SqlDbType.DateTime, 8) {
                Value = model.SendTime
            };
            parameterArray1[6] = parameter8;
            parameterArray1[7] = (model.LinkUrl == null) ? new SqlParameter("@LinkUrl", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@LinkUrl", SqlDbType.VarChar, -1) { Value = model.LinkUrl };
            parameterArray1[8] = (model.LinkID == null) ? new SqlParameter("@LinkID", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@LinkID", SqlDbType.VarChar, 50) { Value = model.LinkID };
            SqlParameter parameter13 = new SqlParameter("@Type", SqlDbType.Int, -1) {
                Value = model.Type
            };
            parameterArray1[9] = parameter13;
            parameterArray1[10] = (model.Files == null) ? new SqlParameter("@Files", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Files", SqlDbType.VarChar, -1) { Value = model.Files };
            parameterArray1[11] = (model.GroupID == null) ? new SqlParameter("@GroupID", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@GroupID", SqlDbType.VarChar, 50) { Value = model.GroupID };
            SqlParameter parameter18 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[12] = parameter18;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int UpdateStatus(Guid id)
        {
            string sql = "INSERT INTO ShortMessage1 SELECT * FROM ShortMessage WHERE ID=@ID;DELETE FROM ShortMessage WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

