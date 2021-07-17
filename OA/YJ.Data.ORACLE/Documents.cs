namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using System.Data;
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
            string sql = "INSERT INTO Documents\r\n\t\t\t\t(ID,DirectoryID,DirectoryName,Title,Source,Contents,Files,WriteTime,WriteUserID,WriteUserName,EditTime,EditUserID,EditUserName,ReadUsers,ReadCount) \r\n\t\t\t\tVALUES(:ID,:DirectoryID,:DirectoryName,:Title,:Source,:Contents,:Files,:WriteTime,:WriteUserID,:WriteUserName,:EditTime,:EditUserID,:EditUserName,:ReadUsers,:ReadCount)";
            OracleParameter[] parameterArray1 = new OracleParameter[15];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":DirectoryID", OracleDbType.Varchar2) {
                Value = model.DirectoryID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":DirectoryName", OracleDbType.Varchar2) {
                Value = model.DirectoryName
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":Title", OracleDbType.Varchar2) {
                Value = model.Title
            };
            parameterArray1[3] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":Source", OracleDbType.Varchar2) {
                Value = model.Source
            };
            parameterArray1[4] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":Contents", OracleDbType.Varchar2) {
                Value = model.Contents
            };
            parameterArray1[5] = parameter6;
            parameterArray1[6] = (model.Files == null) ? new OracleParameter(":Files", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Files", OracleDbType.Varchar2) { Value = model.Files };
            OracleParameter parameter9 = new OracleParameter(":WriteTime", OracleDbType.Date) {
                Value = model.WriteTime
            };
            parameterArray1[7] = parameter9;
            OracleParameter parameter10 = new OracleParameter(":WriteUserID", OracleDbType.Varchar2) {
                Value = model.WriteUserID
            };
            parameterArray1[8] = parameter10;
            OracleParameter parameter11 = new OracleParameter(":WriteUserName", OracleDbType.Varchar2) {
                Value = model.WriteUserName
            };
            parameterArray1[9] = parameter11;
            parameterArray1[10] = !model.EditTime.HasValue ? new OracleParameter(":EditTime", OracleDbType.Date) { Value = DBNull.Value } : new OracleParameter(":EditTime", OracleDbType.Date) { Value = model.EditTime };
            parameterArray1[11] = !model.EditUserID.HasValue ? new OracleParameter(":EditUserID", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":EditUserID", OracleDbType.Varchar2) { Value = model.EditUserID };
            parameterArray1[12] = (model.EditUserName == null) ? new OracleParameter(":EditUserName", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":EditUserName", OracleDbType.Varchar2) { Value = model.EditUserName };
            parameterArray1[13] = (model.ReadUsers == null) ? new OracleParameter(":ReadUsers", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":ReadUsers", OracleDbType.Varchar2) { Value = model.ReadUsers };
            OracleParameter parameter20 = new OracleParameter(":ReadCount", OracleDbType.Int32) {
                Value = model.ReadCount
            };
            parameterArray1[14] = parameter20;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.Documents> DataReaderToList(OracleDataReader dataReader)
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
            string sql = "DELETE FROM Documents WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public int DeleteByDirectoryID(Guid directoryID)
        {
            string sql = "DELETE FROM Documents WHERE DirectoryID=:DirectoryID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":DirectoryID", OracleDbType.Varchar2) {
                Value = directoryID
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.Documents Get(Guid id)
        {
            string sql = "SELECT * FROM Documents WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Documents> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.Documents> GetAll()
        {
            string sql = "SELECT * FROM Documents";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
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
            List<OracleParameter> list = new List<OracleParameter>();
            StringBuilder builder = new StringBuilder("SELECT ID,DirectoryID,DirectoryName,Title,WriteTime,WriteUserName,IsRead,ReadCount,ROWNUM AS PagerAutoRowNumber FROM Documents a LEFT JOIN DocumentsReadUsers b ON a.ID=b.DocumentID WHERE b.UserID=:UserID");
            OracleParameter item = new OracleParameter(":UserID", OracleDbType.Varchar2) {
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
                builder.Append(" AND INSTR(Title,:Title,1,1)>0");
                OracleParameter parameter2 = new OracleParameter(":Title", OracleDbType.Varchar2) {
                    Value = title
                };
                list.Add(parameter2);
            }
            if (date1.IsDateTime())
            {
                builder.Append(" AND WriteTime>=:WriteTime");
                OracleParameter parameter3 = new OracleParameter(":WriteTime", OracleDbType.Date) {
                    Value = date1.ToDateTime().Date
                };
                list.Add(parameter3);
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND WriteTime<=:WriteTime1");
                OracleParameter parameter4 = new OracleParameter(":WriteTime1", OracleDbType.Date) {
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
            List<OracleParameter> list = new List<OracleParameter>();
            StringBuilder builder = new StringBuilder("SELECT ID,DirectoryID,DirectoryName,Title,WriteTime,WriteUserName,IsRead,ReadCount,ROWNUM AS PagerAutoRowNumber FROM Documents a LEFT JOIN DocumentsReadUsers b ON a.ID=b.DocumentID WHERE b.UserID=:UserID");
            OracleParameter item = new OracleParameter(":UserID", OracleDbType.Varchar2) {
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
                builder.Append(" AND INSTR(Title,:Title,1,1)>0");
                OracleParameter parameter2 = new OracleParameter(":Title", OracleDbType.Varchar2) {
                    Value = title
                };
                list.Add(parameter2);
            }
            if (date1.IsDateTime())
            {
                builder.Append(" AND WriteTime>=:WriteTime");
                OracleParameter parameter3 = new OracleParameter(":WriteTime", OracleDbType.Date) {
                    Value = date1.ToDateTime().Date
                };
                list.Add(parameter3);
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND WriteTime<=:WriteTime1");
                OracleParameter parameter4 = new OracleParameter(":WriteTime1", OracleDbType.Date) {
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
            string sql = "UPDATE Documents SET \r\n\t\t\t\tDirectoryID=:DirectoryID,DirectoryName=:DirectoryName,Title=:Title,Source=:Source,Contents=:Contents,Files=:Files,WriteTime=:WriteTime,WriteUserID=:WriteUserID,WriteUserName=:WriteUserName,EditTime=:EditTime,EditUserID=:EditUserID,EditUserName=:EditUserName,ReadUsers=:ReadUsers,ReadCount=:ReadCount\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[15];
            OracleParameter parameter1 = new OracleParameter(":DirectoryID", OracleDbType.Varchar2) {
                Value = model.DirectoryID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":DirectoryName", OracleDbType.Varchar2) {
                Value = model.DirectoryName
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":Title", OracleDbType.Varchar2) {
                Value = model.Title
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":Source", OracleDbType.Varchar2) {
                Value = model.Source
            };
            parameterArray1[3] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":Contents", OracleDbType.Varchar2) {
                Value = model.Contents
            };
            parameterArray1[4] = parameter5;
            parameterArray1[5] = (model.Files == null) ? new OracleParameter(":Files", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Files", OracleDbType.Varchar2) { Value = model.Files };
            OracleParameter parameter8 = new OracleParameter(":WriteTime", OracleDbType.Date) {
                Value = model.WriteTime
            };
            parameterArray1[6] = parameter8;
            OracleParameter parameter9 = new OracleParameter(":WriteUserID", OracleDbType.Varchar2) {
                Value = model.WriteUserID
            };
            parameterArray1[7] = parameter9;
            OracleParameter parameter10 = new OracleParameter(":WriteUserName", OracleDbType.Varchar2) {
                Value = model.WriteUserName
            };
            parameterArray1[8] = parameter10;
            parameterArray1[9] = !model.EditTime.HasValue ? new OracleParameter(":EditTime", OracleDbType.Date) { Value = DBNull.Value } : new OracleParameter(":EditTime", OracleDbType.Date) { Value = model.EditTime };
            parameterArray1[10] = !model.EditUserID.HasValue ? new OracleParameter(":EditUserID", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":EditUserID", OracleDbType.Varchar2) { Value = model.EditUserID };
            parameterArray1[11] = (model.EditUserName == null) ? new OracleParameter(":EditUserName", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":EditUserName", OracleDbType.Varchar2) { Value = model.EditUserName };
            parameterArray1[12] = (model.ReadUsers == null) ? new OracleParameter(":ReadUsers", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":ReadUsers", OracleDbType.Varchar2) { Value = model.ReadUsers };
            OracleParameter parameter19 = new OracleParameter(":ReadCount", OracleDbType.Int32) {
                Value = model.ReadCount
            };
            parameterArray1[13] = parameter19;
            OracleParameter parameter20 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[14] = parameter20;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public void UpdateReadCount(Guid id)
        {
            string sql = "UPDATE Documents SET ReadCount=ReadCount+1 WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            this.dbHelper.Execute(sql, parameter);
        }
    }
}

