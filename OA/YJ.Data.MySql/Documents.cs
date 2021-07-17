using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using YJ.Data.Interface;
using YJ.Data.Model;
using YJ.Utility;
namespace YJ.Data.MySql
{


    public class Documents : IDocuments
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.Documents model)
        {
            string sql = "INSERT INTO documents\r\n\t\t\t\t(ID,DirectoryID,DirectoryName,Title,Source,Contents,Files,WriteTime,WriteUserID,WriteUserName,EditTime,EditUserID,EditUserName,ReadUsers,ReadCount) \r\n\t\t\t\tVALUES(@ID,@DirectoryID,@DirectoryName,@Title,@Source,@Contents,@Files,@WriteTime,@WriteUserID,@WriteUserName,@EditTime,@EditUserID,@EditUserName,@ReadUsers,@ReadCount)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[15];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@DirectoryID", MySqlDbType.VarChar, 0x24) {
                Value = model.DirectoryID
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@DirectoryName", MySqlDbType.VarChar, 200) {
                Value = model.DirectoryName
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@Title", MySqlDbType.Text, -1) {
                Value = model.Title
            };
            parameterArray1[3] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@Source", MySqlDbType.VarChar, 50) {
                Value = model.Source
            };
            parameterArray1[4] = parameter5;
            MySqlParameter parameter6 = new MySqlParameter("@Contents", MySqlDbType.LongText, -1) {
                Value = model.Contents
            };
            parameterArray1[5] = parameter6;
            parameterArray1[6] = (model.Files == null) ? new MySqlParameter("@Files", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Files", MySqlDbType.LongText, -1) { Value = model.Files };
            MySqlParameter parameter9 = new MySqlParameter("@WriteTime", MySqlDbType.DateTime, -1) {
                Value = model.WriteTime
            };
            parameterArray1[7] = parameter9;
            MySqlParameter parameter10 = new MySqlParameter("@WriteUserID", MySqlDbType.VarChar, 0x24) {
                Value = model.WriteUserID
            };
            parameterArray1[8] = parameter10;
            MySqlParameter parameter11 = new MySqlParameter("@WriteUserName", MySqlDbType.VarChar, 50) {
                Value = model.WriteUserName
            };
            parameterArray1[9] = parameter11;
            parameterArray1[10] = !model.EditTime.HasValue ? new MySqlParameter("@EditTime", MySqlDbType.DateTime, -1) { Value = DBNull.Value } : new MySqlParameter("@EditTime", MySqlDbType.DateTime, -1) { Value = model.EditTime };
            parameterArray1[11] = !model.EditUserID.HasValue ? new MySqlParameter("@EditUserID", MySqlDbType.VarChar, 0x24) { Value = DBNull.Value } : new MySqlParameter("@EditUserID", MySqlDbType.VarChar, 0x24) { Value = model.EditUserID };
            parameterArray1[12] = (model.EditUserName == null) ? new MySqlParameter("@EditUserName", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@EditUserName", MySqlDbType.VarChar, 50) { Value = model.EditUserName };
            parameterArray1[13] = (model.ReadUsers == null) ? new MySqlParameter("@ReadUsers", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@ReadUsers", MySqlDbType.LongText, -1) { Value = model.ReadUsers };
            MySqlParameter parameter20 = new MySqlParameter("@ReadCount", MySqlDbType.Int32, 11) {
                Value = model.ReadCount
            };
            parameterArray1[14] = parameter20;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.Documents> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.Documents> list = new List<YJ.Data.Model.Documents>();
            YJ.Data.Model.Documents item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.Documents {
                    ID = dataReader.GetString(0).ToGuid(),
                    DirectoryID = dataReader.GetString(1).ToGuid(),
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
                item.WriteUserID = dataReader.GetString(8).ToGuid();
                item.WriteUserName = dataReader.GetString(9);
                if (!dataReader.IsDBNull(10))
                {
                    item.EditTime = new DateTime?(dataReader.GetDateTime(10));
                }
                if (!dataReader.IsDBNull(11))
                {
                    item.EditUserID = new Guid?(dataReader.GetString(11).ToGuid());
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
            string sql = "DELETE FROM documents WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int DeleteByDirectoryID(Guid directoryID)
        {
            string sql = "DELETE FROM Documents WHERE DirectoryID=@DirectoryID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@DirectoryID", MySqlDbType.VarChar) {
                Value = directoryID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.Documents Get(Guid id)
        {
            string sql = "SELECT * FROM documents WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Documents> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.Documents> GetAll()
        {
            string sql = "SELECT * FROM documents";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Documents> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM documents";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public DataTable GetList(out string pager, string dirID, string userID, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(false)] bool isNoRead)
        {
            long num;
            List<MySqlParameter> list = new List<MySqlParameter>();
            StringBuilder builder = new StringBuilder("SELECT ID,DirectoryID,DirectoryName,Title,WriteTime,WriteUserName,IsRead,ReadCount FROM Documents a LEFT JOIN DocumentsReadUsers b ON a.ID=b.DocumentID WHERE b.UserID=@UserID");
            MySqlParameter item = new MySqlParameter("@UserID", MySqlDbType.VarChar) {
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
                builder.Append(isNoRead ? " AND IsRead=0" : " AND 1=0");
            }
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND INSTR(Title,@Title)>0");
                MySqlParameter parameter2 = new MySqlParameter("@Title", MySqlDbType.VarChar) {
                    Value = title
                };
                list.Add(parameter2);
            }
            if (date1.IsDateTime())
            {
                builder.Append(" AND WriteTime>=@WriteTime");
                MySqlParameter parameter3 = new MySqlParameter("@WriteTime", MySqlDbType.DateTime) {
                    Value = date1.ToDateTime().Date
                };
                list.Add(parameter3);
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND WriteTime<=@WriteTime1");
                MySqlParameter parameter4 = new MySqlParameter("@WriteTime1", MySqlDbType.DateTime) {
                    Value = date2.ToDateTime().AddDays(1.0).Date
                };
                list.Add(parameter4);
            }
            builder.Append(" ORDER BY WriteTime DESC");
            int pageSize = Tools.GetPageSize();
            int pageNumber = Tools.GetPageNumber();
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), pageSize, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, pageSize, pageNumber, query);
            return this.dbHelper.GetDataTable(sql, list.ToArray());
        }

        public DataTable GetList(out long count, int size, int number, string dirID, string userID, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(false)] bool isNoRead, [Optional, DefaultParameterValue("")] string order)
        {
            List<MySqlParameter> list = new List<MySqlParameter>();
            StringBuilder builder = new StringBuilder("SELECT ID,DirectoryID,DirectoryName,Title,WriteTime,WriteUserName,IsRead,ReadCount FROM Documents a LEFT JOIN DocumentsReadUsers b ON a.ID=b.DocumentID WHERE b.UserID=@UserID");
            MySqlParameter item = new MySqlParameter("@UserID", MySqlDbType.VarChar) {
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
                builder.Append(isNoRead ? " AND IsRead=0" : " AND 1=0");
            }
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND INSTR(Title,@Title)>0");
                MySqlParameter parameter2 = new MySqlParameter("@Title", MySqlDbType.VarChar) {
                    Value = title
                };
                list.Add(parameter2);
            }
            if (date1.IsDateTime())
            {
                builder.Append(" AND WriteTime>=@WriteTime");
                MySqlParameter parameter3 = new MySqlParameter("@WriteTime", MySqlDbType.DateTime) {
                    Value = date1.ToDateTime().Date
                };
                list.Add(parameter3);
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND WriteTime<=@WriteTime1");
                MySqlParameter parameter4 = new MySqlParameter("@WriteTime1", MySqlDbType.DateTime) {
                    Value = date2.ToDateTime().AddDays(1.0).Date
                };
                list.Add(parameter4);
            }
            builder.Append(" ORDER BY " + (order.IsNullOrEmpty() ? "WriteTime DESC" : order));
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), size, number, out count, list.ToArray());
            return this.dbHelper.GetDataTable(sql, list.ToArray());
        }

        public int Update(YJ.Data.Model.Documents model)
        {
            string sql = "UPDATE documents SET \r\n\t\t\t\tDirectoryID=@DirectoryID,DirectoryName=@DirectoryName,Title=@Title,Source=@Source,Contents=@Contents,Files=@Files,WriteTime=@WriteTime,WriteUserID=@WriteUserID,WriteUserName=@WriteUserName,EditTime=@EditTime,EditUserID=@EditUserID,EditUserName=@EditUserName,ReadUsers=@ReadUsers,ReadCount=@ReadCount\r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[15];
            MySqlParameter parameter1 = new MySqlParameter("@DirectoryID", MySqlDbType.VarChar, 0x24) {
                Value = model.DirectoryID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@DirectoryName", MySqlDbType.VarChar, 200) {
                Value = model.DirectoryName
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Title", MySqlDbType.Text, -1) {
                Value = model.Title
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@Source", MySqlDbType.VarChar, 50) {
                Value = model.Source
            };
            parameterArray1[3] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@Contents", MySqlDbType.LongText, -1) {
                Value = model.Contents
            };
            parameterArray1[4] = parameter5;
            parameterArray1[5] = (model.Files == null) ? new MySqlParameter("@Files", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Files", MySqlDbType.LongText, -1) { Value = model.Files };
            MySqlParameter parameter8 = new MySqlParameter("@WriteTime", MySqlDbType.DateTime, -1) {
                Value = model.WriteTime
            };
            parameterArray1[6] = parameter8;
            MySqlParameter parameter9 = new MySqlParameter("@WriteUserID", MySqlDbType.VarChar, 0x24) {
                Value = model.WriteUserID
            };
            parameterArray1[7] = parameter9;
            MySqlParameter parameter10 = new MySqlParameter("@WriteUserName", MySqlDbType.VarChar, 50) {
                Value = model.WriteUserName
            };
            parameterArray1[8] = parameter10;
            parameterArray1[9] = !model.EditTime.HasValue ? new MySqlParameter("@EditTime", MySqlDbType.DateTime, -1) { Value = DBNull.Value } : new MySqlParameter("@EditTime", MySqlDbType.DateTime, -1) { Value = model.EditTime };
            parameterArray1[10] = !model.EditUserID.HasValue ? new MySqlParameter("@EditUserID", MySqlDbType.VarChar, 0x24) { Value = DBNull.Value } : new MySqlParameter("@EditUserID", MySqlDbType.VarChar, 0x24) { Value = model.EditUserID };
            parameterArray1[11] = (model.EditUserName == null) ? new MySqlParameter("@EditUserName", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@EditUserName", MySqlDbType.VarChar, 50) { Value = model.EditUserName };
            parameterArray1[12] = (model.ReadUsers == null) ? new MySqlParameter("@ReadUsers", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@ReadUsers", MySqlDbType.LongText, -1) { Value = model.ReadUsers };
            MySqlParameter parameter19 = new MySqlParameter("@ReadCount", MySqlDbType.Int32, 11) {
                Value = model.ReadCount
            };
            parameterArray1[13] = parameter19;
            MySqlParameter parameter20 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[14] = parameter20;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public void UpdateReadCount(Guid id)
        {
            string sql = "UPDATE Documents SET ReadCount=ReadCount+1 WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

