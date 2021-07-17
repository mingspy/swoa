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

    public class WorkFlowDelegation : IWorkFlowDelegation
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkFlowDelegation model)
        {
            string sql = "INSERT INTO WorkFlowDelegation\r\n\t\t\t\t(ID,UserID,StartTime,EndTime,FlowID,ToUserID,WriteTime,Note) \r\n\t\t\t\tVALUES(:ID,:UserID,:StartTime,:EndTime,:FlowID,:ToUserID,:WriteTime,:Note)";
            OracleParameter[] parameterArray1 = new OracleParameter[8];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2, 40) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":UserID", OracleDbType.Varchar2, 40) {
                Value = model.UserID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":StartTime", OracleDbType.Date, 8) {
                Value = model.StartTime
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":EndTime", OracleDbType.Date, 8) {
                Value = model.EndTime
            };
            parameterArray1[3] = parameter4;
            parameterArray1[4] = !model.FlowID.HasValue ? new OracleParameter(":FlowID", OracleDbType.Varchar2, 40) { Value = DBNull.Value } : new OracleParameter(":FlowID", OracleDbType.Varchar2, 40) { Value = model.FlowID };
            OracleParameter parameter7 = new OracleParameter(":ToUserID", OracleDbType.Varchar2, 40) {
                Value = model.ToUserID
            };
            parameterArray1[5] = parameter7;
            OracleParameter parameter8 = new OracleParameter(":WriteTime", OracleDbType.Date, 8) {
                Value = model.WriteTime
            };
            parameterArray1[6] = parameter8;
            parameterArray1[7] = (model.Note == null) ? new OracleParameter(":Note", OracleDbType.NVarchar2, 0x1f40) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.NVarchar2, 0x1f40) { Value = model.Note };
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.WorkFlowDelegation> DataReaderToList(OracleDataReader dataReader)
        {
            List<YJ.Data.Model.WorkFlowDelegation> list = new List<YJ.Data.Model.WorkFlowDelegation>();
            YJ.Data.Model.WorkFlowDelegation item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.WorkFlowDelegation {
                    ID = dataReader.GetString(0).ToGuid(),
                    UserID = dataReader.GetString(1).ToGuid(),
                    StartTime = dataReader.GetDateTime(2),
                    EndTime = dataReader.GetDateTime(3)
                };
                if (!dataReader.IsDBNull(4))
                {
                    item.FlowID = new Guid?(dataReader.GetString(4).ToGuid());
                }
                item.ToUserID = dataReader.GetString(5).ToGuid();
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
            string sql = "DELETE FROM WorkFlowDelegation WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.WorkFlowDelegation Get(Guid id)
        {
            string sql = "SELECT * FROM WorkFlowDelegation WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowDelegation> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkFlowDelegation> GetAll()
        {
            string sql = "SELECT * FROM WorkFlowDelegation";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowDelegation> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkFlowDelegation> GetByUserID(Guid userID)
        {
            string sql = "SELECT * FROM WorkFlowDelegation WHERE UserID=:UserID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":UserID", OracleDbType.Varchar2) {
                Value = userID
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
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
            string sql = "SELECT * FROM WorkFlowDelegation WHERE EndTime>=:EndTime";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":EndTime", OracleDbType.Date) {
                Value = DateTimeNew.Now
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowDelegation> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkFlowDelegation> GetPagerData(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string userID, [Optional, DefaultParameterValue("")] string startTime, [Optional, DefaultParameterValue("")] string endTime)
        {
            long num;
            StringBuilder builder = new StringBuilder();
            List<OracleParameter> list = new List<OracleParameter>();
            if (userID.IsGuid())
            {
                builder.Append("AND UserID=:UserID ");
                OracleParameter item = new OracleParameter(":UserID", OracleDbType.Varchar2) {
                    Value = userID.ToGuid()
                };
                list.Add(item);
            }
            if (startTime.IsDateTime())
            {
                builder.Append("AND StartTime>=:StartTime ");
                OracleParameter parameter2 = new OracleParameter(":StartTime", OracleDbType.Date) {
                    Value = startTime.ToDateTime().ToString("yyyy-MM-dd").ToDateTime()
                };
                list.Add(parameter2);
            }
            if (endTime.IsDateTime())
            {
                builder.Append("AND EndTime<=:EndTime ");
                OracleParameter parameter3 = new OracleParameter(":EndTime", OracleDbType.Date) {
                    Value = endTime.ToDateTime().AddDays(1.0).ToString("yyyy-MM-dd").ToDateTime()
                };
                list.Add(parameter3);
            }
            int pageSize = Tools.GetPageSize();
            int pageNumber = Tools.GetPageNumber();
            string sql = this.dbHelper.GetPaerSql("WorkFlowDelegation", "*", builder.ToString(), "WriteTime Desc", pageSize, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, pageSize, pageNumber, query);
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.WorkFlowDelegation> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public List<YJ.Data.Model.WorkFlowDelegation> GetPagerData(out long count, int pageSize, int pageNumber, [Optional, DefaultParameterValue("")] string userID, [Optional, DefaultParameterValue("")] string startTime, [Optional, DefaultParameterValue("")] string endTime, [Optional, DefaultParameterValue("")] string order)
        {
            StringBuilder builder = new StringBuilder();
            List<OracleParameter> list = new List<OracleParameter>();
            if (userID.IsGuid())
            {
                builder.Append("AND UserID=:UserID ");
                OracleParameter item = new OracleParameter(":UserID", OracleDbType.Varchar2) {
                    Value = userID.ToGuid()
                };
                list.Add(item);
            }
            if (startTime.IsDateTime())
            {
                builder.Append("AND StartTime>=:StartTime ");
                OracleParameter parameter2 = new OracleParameter(":StartTime", OracleDbType.Date) {
                    Value = startTime.ToDateTime().ToString("yyyy-MM-dd").ToDateTime()
                };
                list.Add(parameter2);
            }
            if (endTime.IsDateTime())
            {
                builder.Append("AND EndTime<=:EndTime ");
                OracleParameter parameter3 = new OracleParameter(":EndTime", OracleDbType.Date) {
                    Value = endTime.ToDateTime().AddDays(1.0).ToString("yyyy-MM-dd").ToDateTime()
                };
                list.Add(parameter3);
            }
            string sql = this.dbHelper.GetPaerSql("WorkFlowDelegation", "*", builder.ToString(), order.IsNullOrEmpty() ? "WriteTime Desc" : order, pageSize, pageNumber, out count, list.ToArray());
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.WorkFlowDelegation> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public int Update(YJ.Data.Model.WorkFlowDelegation model)
        {
            string sql = "UPDATE WorkFlowDelegation SET \r\n\t\t\t\tUserID=:UserID,StartTime=:StartTime,EndTime=:EndTime,FlowID=:FlowID,ToUserID=:ToUserID,WriteTime=:WriteTime,Note=:Note\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[8];
            OracleParameter parameter1 = new OracleParameter(":UserID", OracleDbType.Varchar2, 40) {
                Value = model.UserID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":StartTime", OracleDbType.Date, 8) {
                Value = model.StartTime
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":EndTime", OracleDbType.Date, 8) {
                Value = model.EndTime
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = !model.FlowID.HasValue ? new OracleParameter(":FlowID", OracleDbType.Varchar2, 40) { Value = DBNull.Value } : new OracleParameter(":FlowID", OracleDbType.Varchar2, 40) { Value = model.FlowID };
            OracleParameter parameter6 = new OracleParameter(":ToUserID", OracleDbType.Varchar2, 40) {
                Value = model.ToUserID
            };
            parameterArray1[4] = parameter6;
            OracleParameter parameter7 = new OracleParameter(":WriteTime", OracleDbType.Date, 8) {
                Value = model.WriteTime
            };
            parameterArray1[5] = parameter7;
            parameterArray1[6] = (model.Note == null) ? new OracleParameter(":Note", OracleDbType.NVarchar2, 0x1f40) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.NVarchar2, 0x1f40) { Value = model.Note };
            OracleParameter parameter10 = new OracleParameter(":ID", OracleDbType.Varchar2, 40) {
                Value = model.ID
            };
            parameterArray1[7] = parameter10;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

