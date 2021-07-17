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

    public class WorkFlowArchives : IWorkFlowArchives
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkFlowArchives model)
        {
            string sql = "INSERT INTO WorkFlowArchives\r\n\t\t\t\t(ID,FlowID,StepID,FlowName,StepName,TaskID,GroupID,InstanceID,Title,Contents,Comments,WriteTime) \r\n\t\t\t\tVALUES(@ID,@FlowID,@StepID,@FlowName,@StepName,@TaskID,@GroupID,@InstanceID,@Title,@Contents,@Comments,@WriteTime)";
            SqlParameter[] parameterArray1 = new SqlParameter[12];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.FlowID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@StepID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.StepID
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@FlowName", SqlDbType.NVarChar, 0x3e8) {
                Value = model.FlowName
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@StepName", SqlDbType.NVarChar, 0x3e8) {
                Value = model.StepName
            };
            parameterArray1[4] = parameter5;
            SqlParameter parameter6 = new SqlParameter("@TaskID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.TaskID
            };
            parameterArray1[5] = parameter6;
            SqlParameter parameter7 = new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.GroupID
            };
            parameterArray1[6] = parameter7;
            SqlParameter parameter8 = new SqlParameter("@InstanceID", SqlDbType.VarChar, 500) {
                Value = model.InstanceID
            };
            parameterArray1[7] = parameter8;
            SqlParameter parameter9 = new SqlParameter("@Title", SqlDbType.NVarChar, 0x1f40) {
                Value = model.Title
            };
            parameterArray1[8] = parameter9;
            SqlParameter parameter10 = new SqlParameter("@Contents", SqlDbType.Text, -1) {
                Value = model.Contents
            };
            parameterArray1[9] = parameter10;
            SqlParameter parameter11 = new SqlParameter("@Comments", SqlDbType.Text, -1) {
                Value = model.Comments
            };
            parameterArray1[10] = parameter11;
            SqlParameter parameter12 = new SqlParameter("@WriteTime", SqlDbType.DateTime, 8) {
                Value = model.WriteTime
            };
            parameterArray1[11] = parameter12;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.WorkFlowArchives> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.WorkFlowArchives> list = new List<YJ.Data.Model.WorkFlowArchives>();
            YJ.Data.Model.WorkFlowArchives item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.WorkFlowArchives {
                    ID = dataReader.GetGuid(0),
                    FlowID = dataReader.GetGuid(1),
                    StepID = dataReader.GetGuid(2),
                    FlowName = dataReader.GetString(3),
                    StepName = dataReader.GetString(4),
                    TaskID = dataReader.GetGuid(5),
                    GroupID = dataReader.GetGuid(6),
                    InstanceID = dataReader.GetString(7),
                    Title = dataReader.GetString(8),
                    Contents = dataReader.GetString(9),
                    Comments = dataReader.GetString(10),
                    WriteTime = dataReader.GetDateTime(11)
                };
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM WorkFlowArchives WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.WorkFlowArchives Get(Guid id)
        {
            string sql = "SELECT * FROM WorkFlowArchives WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowArchives> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkFlowArchives> GetAll()
        {
            string sql = "SELECT * FROM WorkFlowArchives";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowArchives> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM WorkFlowArchives";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public DataTable GetPagerData(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string flowIDString)
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
            if (!flowIDString.IsNullOrEmpty())
            {
                builder.AppendFormat("AND FlowID IN({0}) ", Tools.GetSqlInString(flowIDString, true, ","));
            }
            int pageSize = Tools.GetPageSize();
            int pageNumber = Tools.GetPageNumber();
            string sql = this.dbHelper.GetPaerSql("WorkFlowArchives", "*", builder.ToString(), "WriteTime DESC", pageSize, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, pageSize, pageNumber, query);
            return this.dbHelper.GetDataTable(sql, list.ToArray());
        }

        public DataTable GetPagerData(out long count, int pageSize, int pageNumber, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string flowIDString, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue("")] string order)
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
            if (flowIDString.IsGuid())
            {
                builder.Append("AND FlowID=@FlowID ");
                list.Add(new SqlParameter("@FlowID", flowIDString));
            }
            if (date1.IsDateTime())
            {
                builder.Append("AND WriteTime>=@WriteTime1 ");
                list.Add(new SqlParameter("@WriteTime1", date1));
            }
            if (date2.IsDateTime())
            {
                builder.Append("AND WriteTime<=@WriteTime2 ");
                list.Add(new SqlParameter("@WriteTime2", date2.ToDateTime().ToString("yyyy-MM-dd 23:59:59")));
            }
            string sql = this.dbHelper.GetPaerSql("WorkFlowArchives", "ID,FlowName,StepName,Title,WriteTime", builder.ToString(), order.IsNullOrEmpty() ? "WriteTime DESC" : order, pageSize, pageNumber, out count, list.ToArray());
            return this.dbHelper.GetDataTable(sql, list.ToArray());
        }

        public int Update(YJ.Data.Model.WorkFlowArchives model)
        {
            string sql = "UPDATE WorkFlowArchives SET \r\n\t\t\t\tFlowID=@FlowID,StepID=@StepID,FlowName=@FlowName,StepName=@StepName,TaskID=@TaskID,GroupID=@GroupID,InstanceID=@InstanceID,Title=@Title,Contents=@Contents,Comments=@Comments,WriteTime=@WriteTime\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[12];
            SqlParameter parameter1 = new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.FlowID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@StepID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.StepID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@FlowName", SqlDbType.NVarChar, 0x3e8) {
                Value = model.FlowName
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@StepName", SqlDbType.NVarChar, 0x3e8) {
                Value = model.StepName
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@TaskID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.TaskID
            };
            parameterArray1[4] = parameter5;
            SqlParameter parameter6 = new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.GroupID
            };
            parameterArray1[5] = parameter6;
            SqlParameter parameter7 = new SqlParameter("@InstanceID", SqlDbType.VarChar, 500) {
                Value = model.InstanceID
            };
            parameterArray1[6] = parameter7;
            SqlParameter parameter8 = new SqlParameter("@Title", SqlDbType.NVarChar, 0x1f40) {
                Value = model.Title
            };
            parameterArray1[7] = parameter8;
            SqlParameter parameter9 = new SqlParameter("@Contents", SqlDbType.Text, -1) {
                Value = model.Contents
            };
            parameterArray1[8] = parameter9;
            SqlParameter parameter10 = new SqlParameter("@Comments", SqlDbType.Text, -1) {
                Value = model.Comments
            };
            parameterArray1[9] = parameter10;
            SqlParameter parameter11 = new SqlParameter("@WriteTime", SqlDbType.DateTime, 8) {
                Value = model.WriteTime
            };
            parameterArray1[10] = parameter11;
            SqlParameter parameter12 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[11] = parameter12;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

