using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using YJ.Data.Interface;
using YJ.Data.Model;
using YJ.Utility;
namespace YJ.Data.MySql
{


    public class ShortMessage : IShortMessage
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.ShortMessage model)
        {
            string sql = "INSERT INTO shortmessage\r\n\t\t\t\t(ID,Title,Contents,SendUserID,SendUserName,ReceiveUserID,ReceiveUserName,SendTime,LinkUrl,LinkID,Type,Files) \r\n\t\t\t\tVALUES(@ID,@Title,@Contents,@SendUserID,@SendUserName,@ReceiveUserID,@ReceiveUserName,@SendTime,@LinkUrl,@LinkID,@Type,@Files)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[12];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Title", MySqlDbType.Text, -1) {
                Value = model.Title
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = (model.Contents == null) ? new MySqlParameter("@Contents", MySqlDbType.VarChar, 0xfa0) { Value = DBNull.Value } : new MySqlParameter("@Contents", MySqlDbType.VarChar, 0xfa0) { Value = model.Contents };
            MySqlParameter parameter5 = new MySqlParameter("@SendUserID", MySqlDbType.VarChar, 0x24) {
                Value = model.SendUserID
            };
            parameterArray1[3] = parameter5;
            MySqlParameter parameter6 = new MySqlParameter("@SendUserName", MySqlDbType.Text, -1) {
                Value = model.SendUserName
            };
            parameterArray1[4] = parameter6;
            MySqlParameter parameter7 = new MySqlParameter("@ReceiveUserID", MySqlDbType.VarChar, 0x24) {
                Value = model.ReceiveUserID
            };
            parameterArray1[5] = parameter7;
            MySqlParameter parameter8 = new MySqlParameter("@ReceiveUserName", MySqlDbType.Text, -1) {
                Value = model.ReceiveUserName
            };
            parameterArray1[6] = parameter8;
            MySqlParameter parameter9 = new MySqlParameter("@SendTime", MySqlDbType.DateTime, -1) {
                Value = model.SendTime
            };
            parameterArray1[7] = parameter9;
            parameterArray1[8] = (model.LinkUrl == null) ? new MySqlParameter("@LinkUrl", MySqlDbType.VarChar, 0x7d0) { Value = DBNull.Value } : new MySqlParameter("@LinkUrl", MySqlDbType.VarChar, 0x7d0) { Value = model.LinkUrl };
            parameterArray1[9] = (model.LinkID == null) ? new MySqlParameter("@LinkID", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@LinkID", MySqlDbType.VarChar, 50) { Value = model.LinkID };
            MySqlParameter parameter14 = new MySqlParameter("@Type", MySqlDbType.Int32, 11) {
                Value = model.Type
            };
            parameterArray1[10] = parameter14;
            MySqlParameter parameter15 = new MySqlParameter("@Files", MySqlDbType.Text, -1) {
                Value = model.Files
            };
            parameterArray1[11] = parameter15;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.ShortMessage> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.ShortMessage> list = new List<YJ.Data.Model.ShortMessage>();
            YJ.Data.Model.ShortMessage item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.ShortMessage {
                    ID = dataReader.GetString(0).ToGuid(),
                    Title = dataReader.GetString(1)
                };
                if (!dataReader.IsDBNull(2))
                {
                    item.Contents = dataReader.GetString(2);
                }
                item.SendUserID = dataReader.GetString(3).ToGuid();
                item.SendUserName = dataReader.GetString(4);
                item.ReceiveUserID = dataReader.GetString(5).ToGuid();
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
                if (dataReader.FieldCount > 11)
                {
                    item.Status = dataReader.GetInt32(12);
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
            string sql = "DELETE FROM shortmessage WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int Delete(string linkID, int Type)
        {
            string sql = "DELETE FROM ShortMessage WHERE LinkID=@LinkID AND Type=@Type";
            MySqlParameter[] parameterArray1 = new MySqlParameter[2];
            MySqlParameter parameter1 = new MySqlParameter("@LinkID", MySqlDbType.VarChar) {
                Value = linkID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Type", MySqlDbType.Int32) {
                Value = Type
            };
            parameterArray1[1] = parameter2;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.ShortMessage Get(Guid id)
        {
            string sql = "SELECT *,0 AS Status FROM shortmessage WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ShortMessage> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.ShortMessage> GetAll()
        {
            string sql = "SELECT *,0 AS Status FROM shortmessage";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.ShortMessage> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.ShortMessage> GetAllNoRead()
        {
            string sql = "SELECT *,0 AS Status FROM ShortMessage";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.ShortMessage> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.ShortMessage> GetAllNoReadByUserID(Guid userID)
        {
            string sql = "SELECT *,0 AS Status FROM ShortMessage WHERE ReceiveUserID=@ReceiveUserID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ReceiveUserID", MySqlDbType.VarChar) {
                Value = userID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ShortMessage> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM shortmessage";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public List<YJ.Data.Model.ShortMessage> GetList(int[] status, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string contents, [Optional, DefaultParameterValue("")] string senderID, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue("")] string receiveID, [Optional, DefaultParameterValue("")] string order)
        {
            List<MySqlParameter> list = new List<MySqlParameter>();
            string str = string.Empty;
            if ((status.Length == 1) && (status[0] == 0))
            {
                str = "SELECT *,0 AS Status FROM ShortMessage WHERE 1=1";
            }
            else if ((status.Length == 1) && (status[0] == 1))
            {
                str = "SELECT *,1 AS Status FROM ShortMessage1 WHERE 1=1";
            }
            else if (status.Length == 2)
            {
                str = "SELECT * FROM(SELECT *,0 AS Status FROM ShortMessage WHERE 1=1 UNION ALL SELECT *,1 AS Status FROM ShortMessage1 WHERE 1=1) temp WHERE 1=1";
            }
            StringBuilder builder = new StringBuilder(str);
            if (receiveID.IsGuid())
            {
                builder.Append(" AND ReceiveUserID=@ReceiveUserID");
                list.Add(new MySqlParameter("@ReceiveUserID", receiveID));
            }
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND INSTR(Title,@Title)>0");
                list.Add(new MySqlParameter("@Title", title));
            }
            if (!contents.IsNullOrEmpty())
            {
                builder.Append(" AND INSTR(Contents,@Contents)>0");
                list.Add(new MySqlParameter("@Contents", contents));
            }
            if (!senderID.IsNullOrEmpty())
            {
                builder.AppendFormat(" AND SendUserID IN({0})", senderID);
            }
            if (date1.IsDateTime())
            {
                builder.Append(" AND SendTime>=@SendTime");
                list.Add(new MySqlParameter("@SendTime", date1.ToDateTime()));
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND SendTime<=@SendTime1");
                list.Add(new MySqlParameter("@SendTime1", date2.ToDateTime().AddDays(1.0).ToDateString()));
            }
            builder.Append(" ORDER BY " + (order.IsNullOrEmpty() ? "SendTime DESC" : order));
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(builder.ToString(), list.ToArray());
            List<YJ.Data.Model.ShortMessage> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public List<YJ.Data.Model.ShortMessage> GetList(out string pager, int[] status, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string contents, [Optional, DefaultParameterValue("")] string senderID, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue("")] string receiveID)
        {
            long num;
            List<MySqlParameter> list = new List<MySqlParameter>();
            string str = string.Empty;
            if ((status.Length == 1) && (status[0] == 0))
            {
                str = "SELECT *,0 AS Status FROM ShortMessage WHERE 1=1";
            }
            else if ((status.Length == 1) && (status[0] == 1))
            {
                str = "SELECT *,1 AS Status FROM ShortMessage1 WHERE 1=1";
            }
            else if (status.Length == 2)
            {
                str = "SELECT * FROM(SELECT *,0 AS Status FROM ShortMessage WHERE 1=1 UNION ALL SELECT *,1 AS Status FROM ShortMessage1 WHERE 1=1) temp WHERE 1=1";
            }
            StringBuilder builder = new StringBuilder(str);
            if (receiveID.IsGuid())
            {
                builder.Append(" AND ReceiveUserID=@ReceiveUserID");
                list.Add(new MySqlParameter("@ReceiveUserID", receiveID));
            }
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND INSTR(Title,@Title)>0");
                list.Add(new MySqlParameter("@Title", title));
            }
            if (!contents.IsNullOrEmpty())
            {
                builder.Append(" AND INSTR(Contents,@Contents)>0");
                list.Add(new MySqlParameter("@Contents", contents));
            }
            if (!senderID.IsNullOrEmpty())
            {
                builder.AppendFormat(" AND SendUserID IN({0})", senderID);
            }
            if (date1.IsDateTime())
            {
                builder.Append(" AND SendTime>=@SendTime");
                list.Add(new MySqlParameter("@SendTime", date1.ToDateTime()));
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND SendTime<=@SendTime1");
                list.Add(new MySqlParameter("@SendTime1", date2.ToDateTime().AddDays(1.0).ToDateString()));
            }
            builder.Append(" ORDER BY SendTime DESC");
            int pageSize = Tools.GetPageSize();
            int pageNumber = Tools.GetPageNumber();
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), pageSize, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, pageSize, pageNumber, query);
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.ShortMessage> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public List<YJ.Data.Model.ShortMessage> GetList(out long count, int[] status, int size, int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string contents, [Optional, DefaultParameterValue("")] string senderID, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue("")] string receiveID, [Optional, DefaultParameterValue("")] string order)
        {
            List<MySqlParameter> list = new List<MySqlParameter>();
            string str = string.Empty;
            if ((status.Length == 1) && (status[0] == 0))
            {
                str = "SELECT *,0 AS Status FROM ShortMessage WHERE 1=1";
            }
            else if ((status.Length == 1) && (status[0] == 1))
            {
                str = "SELECT *,1 AS Status FROM ShortMessage1 WHERE 1=1";
            }
            else if (status.Length == 2)
            {
                str = "SELECT * FROM(SELECT *,0 AS Status FROM ShortMessage WHERE 1=1 UNION ALL SELECT *,1 AS Status FROM ShortMessage1 WHERE 1=1) temp WHERE 1=1";
            }
            StringBuilder builder = new StringBuilder(str);
            if (receiveID.IsGuid())
            {
                builder.Append(" AND ReceiveUserID=@ReceiveUserID");
                list.Add(new MySqlParameter("@ReceiveUserID", receiveID));
            }
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND INSTR(Title,@Title)>0");
                list.Add(new MySqlParameter("@Title", title));
            }
            if (!contents.IsNullOrEmpty())
            {
                builder.Append(" AND INSTR(Contents,@Contents)>0");
                list.Add(new MySqlParameter("@Contents", contents));
            }
            if (!senderID.IsNullOrEmpty())
            {
                builder.AppendFormat(" AND SendUserID IN({0})", senderID);
            }
            if (date1.IsDateTime())
            {
                builder.Append(" AND SendTime>=@SendTime");
                list.Add(new MySqlParameter("@SendTime", date1.ToDateTime()));
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND SendTime<=@SendTime1");
                list.Add(new MySqlParameter("@SendTime1", date2.ToDateTime().AddDays(1.0).ToDateString()));
            }
            builder.Append(" ORDER BY " + (order.IsNullOrEmpty() ? "SendTime DESC" : order));
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), size, number, out count, list.ToArray());
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.ShortMessage> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public YJ.Data.Model.ShortMessage GetRead(Guid id)
        {
            string sql = "SELECT *,0 AS Status FROM ShortMessage1 WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ShortMessage> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public int Update(YJ.Data.Model.ShortMessage model)
        {
            string sql = "UPDATE shortmessage SET \r\n\t\t\t\tTitle=@Title,Contents=@Contents,SendUserID=@SendUserID,SendUserName=@SendUserName,ReceiveUserID=@ReceiveUserID,ReceiveUserName=@ReceiveUserName,SendTime=@SendTime,LinkUrl=@LinkUrl,LinkID=@LinkID,Type=@Type,Files=@Files\r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[12];
            MySqlParameter parameter1 = new MySqlParameter("@Title", MySqlDbType.Text, -1) {
                Value = model.Title
            };
            parameterArray1[0] = parameter1;
            parameterArray1[1] = (model.Contents == null) ? new MySqlParameter("@Contents", MySqlDbType.VarChar, 0xfa0) { Value = DBNull.Value } : new MySqlParameter("@Contents", MySqlDbType.VarChar, 0xfa0) { Value = model.Contents };
            MySqlParameter parameter4 = new MySqlParameter("@SendUserID", MySqlDbType.VarChar, 0x24) {
                Value = model.SendUserID
            };
            parameterArray1[2] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@SendUserName", MySqlDbType.Text, -1) {
                Value = model.SendUserName
            };
            parameterArray1[3] = parameter5;
            MySqlParameter parameter6 = new MySqlParameter("@ReceiveUserID", MySqlDbType.VarChar, 0x24) {
                Value = model.ReceiveUserID
            };
            parameterArray1[4] = parameter6;
            MySqlParameter parameter7 = new MySqlParameter("@ReceiveUserName", MySqlDbType.Text, -1) {
                Value = model.ReceiveUserName
            };
            parameterArray1[5] = parameter7;
            MySqlParameter parameter8 = new MySqlParameter("@SendTime", MySqlDbType.DateTime, -1) {
                Value = model.SendTime
            };
            parameterArray1[6] = parameter8;
            parameterArray1[7] = (model.LinkUrl == null) ? new MySqlParameter("@LinkUrl", MySqlDbType.VarChar, 0x7d0) { Value = DBNull.Value } : new MySqlParameter("@LinkUrl", MySqlDbType.VarChar, 0x7d0) { Value = model.LinkUrl };
            parameterArray1[8] = (model.LinkID == null) ? new MySqlParameter("@LinkID", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@LinkID", MySqlDbType.VarChar, 50) { Value = model.LinkID };
            MySqlParameter parameter13 = new MySqlParameter("@Type", MySqlDbType.Int32, 11) {
                Value = model.Type
            };
            parameterArray1[9] = parameter13;
            MySqlParameter parameter14 = new MySqlParameter("@Files", MySqlDbType.Text, -1) {
                Value = model.Files
            };
            parameterArray1[10] = parameter14;
            MySqlParameter parameter15 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[11] = parameter15;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int UpdateStatus(Guid id)
        {
            string sql = "INSERT INTO ShortMessage1 SELECT * FROM ShortMessage WHERE ID=@ID;DELETE FROM ShortMessage WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

