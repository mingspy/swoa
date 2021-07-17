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

    public class WorkFlowDelegation : IWorkFlowDelegation
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkFlowDelegation model)
        {
            string sql = "INSERT INTO WorkFlowDelegation\r\n\t\t\t\t(ID,UserID,StartTime,EndTime,FlowID,ToUserID,WriteTime,Note) \r\n\t\t\t\tVALUES(@ID,@UserID,@StartTime,@EndTime,@FlowID,@ToUserID,@WriteTime,@Note)";
            SqlParameter[] parameterArray1 = new SqlParameter[8];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.UserID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@StartTime", SqlDbType.DateTime, 8) {
                Value = model.StartTime
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@EndTime", SqlDbType.DateTime, 8) {
                Value = model.EndTime
            };
            parameterArray1[3] = parameter4;
            parameterArray1[4] = !model.FlowID.HasValue ? new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier, -1) { Value = DBNull.Value } : new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier, -1) { Value = model.FlowID };
            SqlParameter parameter7 = new SqlParameter("@ToUserID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ToUserID
            };
            parameterArray1[5] = parameter7;
            SqlParameter parameter8 = new SqlParameter("@WriteTime", SqlDbType.DateTime, 8) {
                Value = model.WriteTime
            };
            parameterArray1[6] = parameter8;
            parameterArray1[7] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.NVarChar, 0x1f40) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.NVarChar, 0x1f40) { Value = model.Note };
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.WorkFlowDelegation> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.WorkFlowDelegation> list = new List<YJ.Data.Model.WorkFlowDelegation>();
            YJ.Data.Model.WorkFlowDelegation item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.WorkFlowDelegation {
                    ID = dataReader.GetGuid(0),
                    UserID = dataReader.GetGuid(1),
                    StartTime = dataReader.GetDateTime(2),
                    EndTime = dataReader.GetDateTime(3)
                };
                if (!dataReader.IsDBNull(4))
                {
                    item.FlowID = new Guid?(dataReader.GetGuid(4));
                }
                item.ToUserID = dataReader.GetGuid(5);
                item.WriteTime = dataReader.GetDateTime(6);
                if (!dataReader.IsDBNull(7))
                {
                    item.Note = dataReader.GetString(7);
                }
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM WorkFlowDelegation WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.WorkFlowDelegation Get(Guid id)
        {
            string sql = "SELECT * FROM WorkFlowDelegation WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowDelegation> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkFlowDelegation> GetAll()
        {
            string sql = "SELECT * FROM WorkFlowDelegation";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowDelegation> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkFlowDelegation> GetByUserID(Guid userID)
        {
            string sql = "SELECT * FROM WorkFlowDelegation WHERE UserID=@UserID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier) {
                Value = userID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowDelegation> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM WorkFlowDelegation";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public List<YJ.Data.Model.WorkFlowDelegation> GetNoExpiredList()
        {
            string sql = "SELECT * FROM WorkFlowDelegation WHERE EndTime>=@EndTime";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@EndTime", SqlDbType.DateTime) {
                Value = DateTimeNew.Now
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowDelegation> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkFlowDelegation> GetPagerData(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string userID, [Optional, DefaultParameterValue("")] string startTime, [Optional, DefaultParameterValue("")] string endTime)
        {
            long num;
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            if (userID.IsGuid())
            {
                builder.Append("AND UserID=@UserID ");
                SqlParameter item = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier) {
                    Value = userID.ToGuid()
                };
                list.Add(item);
            }
            if (startTime.IsDateTime())
            {
                builder.Append("AND StartTime>=@StartTime ");
                SqlParameter parameter2 = new SqlParameter("@StartTime", SqlDbType.DateTime) {
                    Value = startTime.ToDateTime().ToString("yyyy-MM-dd").ToDateTime()
                };
                list.Add(parameter2);
            }
            if (endTime.IsDateTime())
            {
                builder.Append("AND EndTime<=@EndTime ");
                SqlParameter parameter3 = new SqlParameter("@EndTime", SqlDbType.DateTime) {
                    Value = endTime.ToDateTime().AddDays(1.0).ToString("yyyy-MM-dd").ToDateTime()
                };
                list.Add(parameter3);
            }
            int pageSize = Tools.GetPageSize();
            int pageNumber = Tools.GetPageNumber();
            string sql = this.dbHelper.GetPaerSql("WorkFlowDelegation", "*", builder.ToString(), "WriteTime Desc", pageSize, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, pageSize, pageNumber, query);
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.WorkFlowDelegation> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public List<YJ.Data.Model.WorkFlowDelegation> GetPagerData(out long count, int pageSize, int pageNumber, [Optional, DefaultParameterValue("")] string userID, [Optional, DefaultParameterValue("")] string startTime, [Optional, DefaultParameterValue("")] string endTime, [Optional, DefaultParameterValue("")] string order)
        {
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            if (userID.IsGuid())
            {
                builder.Append("AND UserID=@UserID ");
                SqlParameter item = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier) {
                    Value = userID.ToGuid()
                };
                list.Add(item);
            }
            if (startTime.IsDateTime())
            {
                builder.Append("AND StartTime>=@StartTime ");
                SqlParameter parameter2 = new SqlParameter("@StartTime", SqlDbType.DateTime) {
                    Value = startTime.ToDateTime().ToString("yyyy-MM-dd").ToDateTime()
                };
                list.Add(parameter2);
            }
            if (endTime.IsDateTime())
            {
                builder.Append("AND EndTime<=@EndTime ");
                SqlParameter parameter3 = new SqlParameter("@EndTime", SqlDbType.DateTime) {
                    Value = endTime.ToDateTime().AddDays(1.0).ToString("yyyy-MM-dd").ToDateTime()
                };
                list.Add(parameter3);
            }
            string sql = this.dbHelper.GetPaerSql("WorkFlowDelegation", "*", builder.ToString(), order.IsNullOrEmpty() ? "WriteTime Desc" : order, pageSize, pageNumber, out count, list.ToArray());
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.WorkFlowDelegation> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public int Update(YJ.Data.Model.WorkFlowDelegation model)
        {
            string sql = "UPDATE WorkFlowDelegation SET \r\n\t\t\t\tUserID=@UserID,StartTime=@StartTime,EndTime=@EndTime,FlowID=@FlowID,ToUserID=@ToUserID,WriteTime=@WriteTime,Note=@Note\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[8];
            SqlParameter parameter1 = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.UserID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@StartTime", SqlDbType.DateTime, 8) {
                Value = model.StartTime
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@EndTime", SqlDbType.DateTime, 8) {
                Value = model.EndTime
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = !model.FlowID.HasValue ? new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier, -1) { Value = DBNull.Value } : new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier, -1) { Value = model.FlowID };
            SqlParameter parameter6 = new SqlParameter("@ToUserID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ToUserID
            };
            parameterArray1[4] = parameter6;
            SqlParameter parameter7 = new SqlParameter("@WriteTime", SqlDbType.DateTime, 8) {
                Value = model.WriteTime
            };
            parameterArray1[5] = parameter7;
            parameterArray1[6] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.NVarChar, 0x1f40) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.NVarChar, 0x1f40) { Value = model.Note };
            SqlParameter parameter10 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[7] = parameter10;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

