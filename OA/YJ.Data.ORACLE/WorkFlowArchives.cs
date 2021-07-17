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

    public class WorkFlowArchives : IWorkFlowArchives
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkFlowArchives model)
        {
            string sql = "INSERT INTO WorkFlowArchives\r\n\t\t\t\t(ID,FlowID,StepID,FlowName,StepName,TaskID,GroupID,InstanceID,Title,Contents,Comments,WriteTime) \r\n\t\t\t\tVALUES(:ID,:FlowID,:StepID,:FlowName,:StepName,:TaskID,:GroupID,:InstanceID,:Title,:Contents,:Comments,:WriteTime)";
            OracleParameter[] parameterArray1 = new OracleParameter[12];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2, 40) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":FlowID", OracleDbType.Varchar2, 40) {
                Value = model.FlowID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":StepID", OracleDbType.Varchar2, 40) {
                Value = model.StepID
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":FlowName", OracleDbType.NVarchar2, 0x3e8) {
                Value = model.FlowName
            };
            parameterArray1[3] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":StepName", OracleDbType.NVarchar2, 0x3e8) {
                Value = model.StepName
            };
            parameterArray1[4] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":TaskID", OracleDbType.Varchar2, 40) {
                Value = model.TaskID
            };
            parameterArray1[5] = parameter6;
            OracleParameter parameter7 = new OracleParameter(":GroupID", OracleDbType.Varchar2, 40) {
                Value = model.GroupID
            };
            parameterArray1[6] = parameter7;
            OracleParameter parameter8 = new OracleParameter(":InstanceID", OracleDbType.Varchar2, 500) {
                Value = model.InstanceID
            };
            parameterArray1[7] = parameter8;
            OracleParameter parameter9 = new OracleParameter(":Title", OracleDbType.NVarchar2, 0x1f40) {
                Value = model.Title
            };
            parameterArray1[8] = parameter9;
            OracleParameter parameter10 = new OracleParameter(":Contents", OracleDbType.Clob) {
                Value = model.Contents
            };
            parameterArray1[9] = parameter10;
            OracleParameter parameter11 = new OracleParameter(":Comments", OracleDbType.Clob) {
                Value = model.Comments
            };
            parameterArray1[10] = parameter11;
            OracleParameter parameter12 = new OracleParameter(":WriteTime", OracleDbType.Date, 8) {
                Value = model.WriteTime
            };
            parameterArray1[11] = parameter12;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.WorkFlowArchives> DataReaderToList(OracleDataReader dataReader)
        {
            List<YJ.Data.Model.WorkFlowArchives> list = new List<YJ.Data.Model.WorkFlowArchives>();
            YJ.Data.Model.WorkFlowArchives item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.WorkFlowArchives {
                    ID = dataReader.GetString(0).ToGuid(),
                    FlowID = dataReader.GetString(1).ToGuid(),
                    StepID = dataReader.GetString(2).ToGuid(),
                    FlowName = dataReader.GetString(3),
                    StepName = dataReader.GetString(4),
                    TaskID = dataReader.GetString(5).ToGuid(),
                    GroupID = dataReader.GetString(6).ToGuid(),
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
            string sql = "DELETE FROM WorkFlowArchives WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.WorkFlowArchives Get(Guid id)
        {
            string sql = "SELECT * FROM WorkFlowArchives WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowArchives> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkFlowArchives> GetAll()
        {
            string sql = "SELECT * FROM WorkFlowArchives";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
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
            List<OracleParameter> list = new List<OracleParameter>();
            if (!title.IsNullOrEmpty())
            {
                builder.Append("AND INSTR(Title,:Title,1,1)>0 ");
                OracleParameter item = new OracleParameter(":Title", OracleDbType.NVarchar2) {
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
            List<OracleParameter> list = new List<OracleParameter>();
            if (!title.IsNullOrEmpty())
            {
                builder.Append("AND INSTR(Title,:Title,1,1)>0 ");
                OracleParameter item = new OracleParameter(":Title", OracleDbType.NVarchar2) {
                    Value = title
                };
                list.Add(item);
            }
            if (flowIDString.IsGuid())
            {
                builder.Append("AND FlowID=:FlowID ");
                list.Add(new OracleParameter(":FlowID", flowIDString));
            }
            if (date1.IsDateTime())
            {
                builder.Append("AND WriteTime>=:WriteTime1 ");
                list.Add(new OracleParameter(":WriteTime1", date1));
            }
            if (date2.IsDateTime())
            {
                builder.Append("AND WriteTime<=:WriteTime2 ");
                list.Add(new OracleParameter(":WriteTime2", date2.ToDateTime().ToString("yyyy-MM-dd 23:59:59")));
            }
            string sql = this.dbHelper.GetPaerSql("WorkFlowArchives", "ID,FlowName,StepName,Title,WriteTime", builder.ToString(), order.IsNullOrEmpty() ? "WriteTime DESC" : order, pageSize, pageNumber, out count, list.ToArray());
            return this.dbHelper.GetDataTable(sql, list.ToArray());
        }

        public int Update(YJ.Data.Model.WorkFlowArchives model)
        {
            string sql = "UPDATE WorkFlowArchives SET \r\n\t\t\t\tFlowID=:FlowID,StepID=:StepID,FlowName=:FlowName,StepName=:StepName,TaskID=:TaskID,GroupID=:GroupID,InstanceID=:InstanceID,Title=:Title,Contents=:Contents,Comments=:Comments,WriteTime=:WriteTime\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[12];
            OracleParameter parameter1 = new OracleParameter(":FlowID", OracleDbType.Varchar2, 40) {
                Value = model.FlowID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":StepID", OracleDbType.Varchar2, 40) {
                Value = model.StepID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":FlowName", OracleDbType.NVarchar2, 0x3e8) {
                Value = model.FlowName
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":StepName", OracleDbType.NVarchar2, 0x3e8) {
                Value = model.StepName
            };
            parameterArray1[3] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":TaskID", OracleDbType.Varchar2, 40) {
                Value = model.TaskID
            };
            parameterArray1[4] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":GroupID", OracleDbType.Varchar2, 40) {
                Value = model.GroupID
            };
            parameterArray1[5] = parameter6;
            OracleParameter parameter7 = new OracleParameter(":InstanceID", OracleDbType.Varchar2, 500) {
                Value = model.InstanceID
            };
            parameterArray1[6] = parameter7;
            OracleParameter parameter8 = new OracleParameter(":Title", OracleDbType.NVarchar2, 0x1f40) {
                Value = model.Title
            };
            parameterArray1[7] = parameter8;
            OracleParameter parameter9 = new OracleParameter(":Contents", OracleDbType.Clob) {
                Value = model.Contents
            };
            parameterArray1[8] = parameter9;
            OracleParameter parameter10 = new OracleParameter(":Comments", OracleDbType.Clob) {
                Value = model.Comments
            };
            parameterArray1[9] = parameter10;
            OracleParameter parameter11 = new OracleParameter(":WriteTime", OracleDbType.Date, 8) {
                Value = model.WriteTime
            };
            parameterArray1[10] = parameter11;
            OracleParameter parameter12 = new OracleParameter(":ID", OracleDbType.Varchar2, 40) {
                Value = model.ID
            };
            parameterArray1[11] = parameter12;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

