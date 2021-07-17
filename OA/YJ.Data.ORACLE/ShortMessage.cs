namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
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
            string sql = "INSERT INTO ShortMessage\r\n\t\t\t\t(ID,Title,Contents,SendUserID,SendUserName,ReceiveUserID,ReceiveUserName,SendTime,LinkUrl,LinkID,Type,Files) \r\n\t\t\t\tVALUES(:ID,:Title,:Contents,:SendUserID,:SendUserName,:ReceiveUserID,:ReceiveUserName,:SendTime,:LinkUrl,:LinkID,:Type,:Files)";
            OracleParameter[] parameterArray1 = new OracleParameter[12];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Title", OracleDbType.NVarchar2) {
                Value = model.Title
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = (model.Contents == null) ? new OracleParameter(":Contents", OracleDbType.NVarchar2) { Value = DBNull.Value } : new OracleParameter(":Contents", OracleDbType.NVarchar2) { Value = model.Contents };
            OracleParameter parameter5 = new OracleParameter(":SendUserID", OracleDbType.Varchar2) {
                Value = model.SendUserID
            };
            parameterArray1[3] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":SendUserName", OracleDbType.NVarchar2) {
                Value = model.SendUserName
            };
            parameterArray1[4] = parameter6;
            OracleParameter parameter7 = new OracleParameter(":ReceiveUserID", OracleDbType.Varchar2) {
                Value = model.ReceiveUserID
            };
            parameterArray1[5] = parameter7;
            OracleParameter parameter8 = new OracleParameter(":ReceiveUserName", OracleDbType.NVarchar2) {
                Value = model.ReceiveUserName
            };
            parameterArray1[6] = parameter8;
            OracleParameter parameter9 = new OracleParameter(":SendTime", OracleDbType.Date) {
                Value = model.SendTime
            };
            parameterArray1[7] = parameter9;
            parameterArray1[8] = (model.LinkUrl == null) ? new OracleParameter(":LinkUrl", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":LinkUrl", OracleDbType.Varchar2) { Value = model.LinkUrl };
            parameterArray1[9] = (model.LinkID == null) ? new OracleParameter(":LinkID", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":LinkID", OracleDbType.Varchar2) { Value = model.LinkID };
            OracleParameter parameter14 = new OracleParameter(":Type", OracleDbType.Int32) {
                Value = model.Type
            };
            parameterArray1[10] = parameter14;
            OracleParameter parameter15 = new OracleParameter(":Files", OracleDbType.Varchar2) {
                Value = model.Files
            };
            parameterArray1[11] = parameter15;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.ShortMessage> DataReaderToList(OracleDataReader dataReader)
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
            string sql = "DELETE FROM ShortMessage WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public int Delete(string linkID, int Type)
        {
            string sql = "DELETE FROM ShortMessage WHERE LinkID=:LinkID AND Type=:Type";
            OracleParameter[] parameterArray1 = new OracleParameter[2];
            OracleParameter parameter1 = new OracleParameter(":LinkID", OracleDbType.Varchar2) {
                Value = linkID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Type", OracleDbType.Int32) {
                Value = Type
            };
            parameterArray1[1] = parameter2;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.ShortMessage Get(Guid id)
        {
            string sql = "SELECT ID,Title,Contents,SendUserID,SendUserName,ReceiveUserID,ReceiveUserName,SendTime,LinkUrl,LinkID,Type,Files,0 AS Status FROM ShortMessage WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ShortMessage> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.ShortMessage> GetAll()
        {
            string sql = "SELECT ID,Title,Contents,SendUserID,SendUserName,ReceiveUserID,ReceiveUserName,SendTime,LinkUrl,LinkID,Type,Files,0 AS Status FROM ShortMessage";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.ShortMessage> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.ShortMessage> GetAllNoRead()
        {
            string sql = "SELECT ID,Title,Contents,SendUserID,SendUserName,ReceiveUserID,ReceiveUserName,SendTime,LinkUrl,LinkID,Type,Files,0 AS Status FROM ShortMessage";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.ShortMessage> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.ShortMessage> GetAllNoReadByUserID(Guid userID)
        {
            string sql = "SELECT ID,Title,Contents,SendUserID,SendUserName,ReceiveUserID,ReceiveUserName,SendTime,LinkUrl,LinkID,Type,Files,0 AS Status FROM ShortMessage WHERE ReceiveUserID=:ReceiveUserID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ReceiveUserID", OracleDbType.Varchar2) {
                Value = userID.ToString()
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
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
            List<OracleParameter> list = new List<OracleParameter>();
            string str = string.Empty;
            if ((status.Length == 1) && (status[0] == 0))
            {
                str = "SELECT ID,Title,Contents,SendUserID,SendUserName,ReceiveUserID,ReceiveUserName,SendTime,LinkUrl,LinkID,Type,Files,0 AS Status FROM ShortMessage WHERE 1=1";
            }
            else if ((status.Length == 1) && (status[0] == 1))
            {
                str = "SELECT ID,Title,Contents,SendUserID,SendUserName,ReceiveUserID,ReceiveUserName,SendTime,LinkUrl,LinkID,Type,Files,1 AS Status FROM ShortMessage1 WHERE 1=1";
            }
            else if (status.Length == 2)
            {
                str = "SELECT * FROM(SELECT ID,Title,Contents,SendUserID,SendUserName,ReceiveUserID,ReceiveUserName,SendTime,LinkUrl,LinkID,Type,Files,0 AS Status FROM ShortMessage WHERE 1=1 UNION ALL SELECT ID,Title,Contents,SendUserID,SendUserName,ReceiveUserID,ReceiveUserName,SendTime,LinkUrl,LinkID,Type,Files,1 AS Status FROM ShortMessage1 WHERE 1=1) temp WHERE 1=1";
            }
            StringBuilder builder = new StringBuilder(str);
            if (receiveID.IsGuid())
            {
                builder.Append(" AND ReceiveUserID=:ReceiveUserID");
                list.Add(new OracleParameter(":ReceiveUserID", receiveID));
            }
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND INSTR(Title,:Title,1,1)>0");
                list.Add(new OracleParameter(":Title", title));
            }
            if (!contents.IsNullOrEmpty())
            {
                builder.Append(" AND INSTR(Contents,:Contents,1,1)>0");
                list.Add(new OracleParameter(":Contents", contents));
            }
            if (!senderID.IsNullOrEmpty())
            {
                builder.AppendFormat(" AND SendUserID IN({0})", senderID);
            }
            if (date1.IsDateTime())
            {
                builder.Append(" AND SendTime>=:SendTime");
                list.Add(new OracleParameter(":SendTime", date1.ToDateTime()));
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND SendTime<=:SendTime1");
                list.Add(new OracleParameter(":SendTime1", date2.ToDateTime().AddDays(1.0).ToDateString()));
            }
            builder.Append(" ORDER BY " + (order.IsNullOrEmpty() ? "SendTime DESC" : order));
            OracleDataReader dataReader = this.dbHelper.GetDataReader(builder.ToString(), list.ToArray());
            List<YJ.Data.Model.ShortMessage> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public List<YJ.Data.Model.ShortMessage> GetList(out string pager, int[] status, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string contents, [Optional, DefaultParameterValue("")] string senderID, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue("")] string receiveID)
        {
            long num;
            List<OracleParameter> list = new List<OracleParameter>();
            string str = string.Empty;
            if ((status.Length == 1) && (status[0] == 0))
            {
                str = "SELECT ID,Title,Contents,SendUserID,SendUserName,ReceiveUserID,ReceiveUserName,SendTime,LinkUrl,LinkID,Type,Files,0 AS Status FROM ShortMessage WHERE 1=1";
            }
            else if ((status.Length == 1) && (status[0] == 1))
            {
                str = "SELECT ID,Title,Contents,SendUserID,SendUserName,ReceiveUserID,ReceiveUserName,SendTime,LinkUrl,LinkID,Type,Files,1 AS Status FROM ShortMessage1 WHERE 1=1";
            }
            else if (status.Length == 2)
            {
                str = "SELECT * FROM(SELECT ID,Title,Contents,SendUserID,SendUserName,ReceiveUserID,ReceiveUserName,SendTime,LinkUrl,LinkID,Type,Files,0 AS Status FROM ShortMessage WHERE 1=1 UNION ALL SELECT ID,Title,Contents,SendUserID,SendUserName,ReceiveUserID,ReceiveUserName,SendTime,LinkUrl,LinkID,Type,Files,1 AS Status FROM ShortMessage1 WHERE 1=1) temp WHERE 1=1";
            }
            StringBuilder builder = new StringBuilder(str);
            if (receiveID.IsGuid())
            {
                builder.Append(" AND ReceiveUserID=:ReceiveUserID");
                list.Add(new OracleParameter(":ReceiveUserID", receiveID));
            }
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND INSTR(Title,:Title,1,1)>0");
                list.Add(new OracleParameter(":Title", title));
            }
            if (!contents.IsNullOrEmpty())
            {
                builder.Append(" AND INSTR(Contents,:Contents,1,1)>0");
                list.Add(new OracleParameter(":Contents", contents));
            }
            if (!senderID.IsNullOrEmpty())
            {
                builder.AppendFormat(" AND SendUserID IN({0})", senderID);
            }
            if (date1.IsDateTime())
            {
                builder.Append(" AND SendTime>=:SendTime");
                list.Add(new OracleParameter(":SendTime", date1.ToDateTime()));
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND SendTime<=:SendTime1");
                list.Add(new OracleParameter(":SendTime1", date2.ToDateTime().AddDays(1.0).ToDateString()));
            }
            int pageSize = Tools.GetPageSize();
            int pageNumber = Tools.GetPageNumber();
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), pageSize, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, pageSize, pageNumber, query);
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.ShortMessage> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public List<YJ.Data.Model.ShortMessage> GetList(out long count, int[] status, int size, int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string contents, [Optional, DefaultParameterValue("")] string senderID, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue("")] string receiveID, [Optional, DefaultParameterValue("")] string order)
        {
            List<OracleParameter> list = new List<OracleParameter>();
            string str = string.Empty;
            if ((status.Length == 1) && (status[0] == 0))
            {
                str = "SELECT ID,Title,Contents,SendUserID,SendUserName,ReceiveUserID,ReceiveUserName,SendTime,LinkUrl,LinkID,Type,Files,0 AS Status FROM ShortMessage WHERE 1=1";
            }
            else if ((status.Length == 1) && (status[0] == 1))
            {
                str = "SELECT ID,Title,Contents,SendUserID,SendUserName,ReceiveUserID,ReceiveUserName,SendTime,LinkUrl,LinkID,Type,Files,1 AS Status FROM ShortMessage1 WHERE 1=1";
            }
            else if (status.Length == 2)
            {
                str = "SELECT * FROM(SELECT ID,Title,Contents,SendUserID,SendUserName,ReceiveUserID,ReceiveUserName,SendTime,LinkUrl,LinkID,Type,Files,0 AS Status FROM ShortMessage WHERE 1=1 UNION ALL SELECT ID,Title,Contents,SendUserID,SendUserName,ReceiveUserID,ReceiveUserName,SendTime,LinkUrl,LinkID,Type,Files,1 AS Status FROM ShortMessage1 WHERE 1=1) temp WHERE 1=1";
            }
            StringBuilder builder = new StringBuilder(str);
            if (receiveID.IsGuid())
            {
                builder.Append(" AND ReceiveUserID=:ReceiveUserID");
                list.Add(new OracleParameter(":ReceiveUserID", receiveID));
            }
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND INSTR(Title,:Title,1,1)>0");
                list.Add(new OracleParameter(":Title", title));
            }
            if (!contents.IsNullOrEmpty())
            {
                builder.Append(" AND INSTR(Contents,:Contents,1,1)>0");
                list.Add(new OracleParameter(":Contents", contents));
            }
            if (!senderID.IsNullOrEmpty())
            {
                builder.AppendFormat(" AND SendUserID IN({0})", senderID);
            }
            if (date1.IsDateTime())
            {
                builder.Append(" AND SendTime>=:SendTime");
                list.Add(new OracleParameter(":SendTime", date1.ToDateTime()));
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND SendTime<=:SendTime1");
                list.Add(new OracleParameter(":SendTime1", date2.ToDateTime().AddDays(1.0).ToDateString()));
            }
            builder.Append(" ORDER BY " + (order.IsNullOrEmpty() ? "SendTime DESC" : order));
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), size, number, out count, list.ToArray());
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.ShortMessage> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public YJ.Data.Model.ShortMessage GetRead(Guid id)
        {
            string sql = "SELECT ID,Title,Contents,SendUserID,SendUserName,ReceiveUserID,ReceiveUserName,SendTime,LinkUrl,LinkID,Type,Files,0 AS Status FROM ShortMessage1 WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ShortMessage> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public int Update(YJ.Data.Model.ShortMessage model)
        {
            string sql = "UPDATE ShortMessage SET \r\n\t\t\t\tTitle=:Title,Contents=:Contents,SendUserID=:SendUserID,SendUserName=:SendUserName,ReceiveUserID=:ReceiveUserID,ReceiveUserName=:ReceiveUserName,SendTime=:SendTime,LinkUrl=:LinkUrl,LinkID=:LinkID,Type=:Type,Files=:Files\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[12];
            OracleParameter parameter1 = new OracleParameter(":Title", OracleDbType.NVarchar2) {
                Value = model.Title
            };
            parameterArray1[0] = parameter1;
            parameterArray1[1] = (model.Contents == null) ? new OracleParameter(":Contents", OracleDbType.NVarchar2) { Value = DBNull.Value } : new OracleParameter(":Contents", OracleDbType.Varchar2) { Value = model.Contents };
            OracleParameter parameter4 = new OracleParameter(":SendUserID", OracleDbType.Varchar2) {
                Value = model.SendUserID
            };
            parameterArray1[2] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":SendUserName", OracleDbType.NVarchar2) {
                Value = model.SendUserName
            };
            parameterArray1[3] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":ReceiveUserID", OracleDbType.Varchar2) {
                Value = model.ReceiveUserID
            };
            parameterArray1[4] = parameter6;
            OracleParameter parameter7 = new OracleParameter(":ReceiveUserName", OracleDbType.NVarchar2) {
                Value = model.ReceiveUserName
            };
            parameterArray1[5] = parameter7;
            OracleParameter parameter8 = new OracleParameter(":SendTime", OracleDbType.Date) {
                Value = model.SendTime
            };
            parameterArray1[6] = parameter8;
            parameterArray1[7] = (model.LinkUrl == null) ? new OracleParameter(":LinkUrl", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":LinkUrl", OracleDbType.Varchar2) { Value = model.LinkUrl };
            parameterArray1[8] = (model.LinkID == null) ? new OracleParameter(":LinkID", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":LinkID", OracleDbType.Varchar2) { Value = model.LinkID };
            OracleParameter parameter13 = new OracleParameter(":Type", OracleDbType.Int32) {
                Value = model.Type
            };
            parameterArray1[9] = parameter13;
            OracleParameter parameter14 = new OracleParameter(":Files", OracleDbType.Varchar2) {
                Value = model.Files
            };
            parameterArray1[10] = parameter14;
            OracleParameter parameter15 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[11] = parameter15;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public int UpdateStatus(Guid id)
        {
            string sql = "BEGIN INSERT INTO ShortMessage1 SELECT * FROM ShortMessage WHERE ID=:ID; DELETE FROM ShortMessage WHERE ID=:ID; END;";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

