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


    public class WorkFlowArchives : IWorkFlowArchives
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkFlowArchives model)
        {
            string sql = "INSERT INTO workflowarchives\r\n\t\t\t\t(ID,FlowID,StepID,FlowName,StepName,TaskID,GroupID,InstanceID,Title,Contents,Comments,WriteTime) \r\n\t\t\t\tVALUES(@ID,@FlowID,@StepID,@FlowName,@StepName,@TaskID,@GroupID,@InstanceID,@Title,@Contents,@Comments,@WriteTime)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[12];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@FlowID", MySqlDbType.VarChar, 0x24) {
                Value = model.FlowID
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@StepID", MySqlDbType.VarChar, 0x24) {
                Value = model.StepID
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@FlowName", MySqlDbType.Text, -1) {
                Value = model.FlowName
            };
            parameterArray1[3] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@StepName", MySqlDbType.Text, -1) {
                Value = model.StepName
            };
            parameterArray1[4] = parameter5;
            MySqlParameter parameter6 = new MySqlParameter("@TaskID", MySqlDbType.VarChar, 0x24) {
                Value = model.TaskID
            };
            parameterArray1[5] = parameter6;
            MySqlParameter parameter7 = new MySqlParameter("@GroupID", MySqlDbType.VarChar, 0x24) {
                Value = model.GroupID
            };
            parameterArray1[6] = parameter7;
            MySqlParameter parameter8 = new MySqlParameter("@InstanceID", MySqlDbType.Text, -1) {
                Value = model.InstanceID
            };
            parameterArray1[7] = parameter8;
            MySqlParameter parameter9 = new MySqlParameter("@Title", MySqlDbType.Text, -1) {
                Value = model.Title
            };
            parameterArray1[8] = parameter9;
            MySqlParameter parameter10 = new MySqlParameter("@Contents", MySqlDbType.LongText, -1) {
                Value = model.Contents
            };
            parameterArray1[9] = parameter10;
            MySqlParameter parameter11 = new MySqlParameter("@Comments", MySqlDbType.LongText, -1) {
                Value = model.Comments
            };
            parameterArray1[10] = parameter11;
            MySqlParameter parameter12 = new MySqlParameter("@WriteTime", MySqlDbType.DateTime, -1) {
                Value = model.WriteTime
            };
            parameterArray1[11] = parameter12;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.WorkFlowArchives> DataReaderToList(MySqlDataReader dataReader)
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
            string sql = "DELETE FROM workflowarchives WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.WorkFlowArchives Get(Guid id)
        {
            string sql = "SELECT * FROM workflowarchives WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowArchives> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkFlowArchives> GetAll()
        {
            string sql = "SELECT * FROM workflowarchives";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowArchives> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM workflowarchives";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public DataTable GetPagerData(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string flowIDString)
        {
            long num;
            StringBuilder builder = new StringBuilder();
            List<MySqlParameter> list = new List<MySqlParameter>();
            if (!title.IsNullOrEmpty())
            {
                builder.Append("AND INSTR(Title,@Title)>0 ");
                MySqlParameter item = new MySqlParameter("@Title", MySqlDbType.VarChar) {
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
            List<MySqlParameter> list = new List<MySqlParameter>();
            if (!title.IsNullOrEmpty())
            {
                builder.Append("AND INSTR(Title,@Title)>0 ");
                MySqlParameter item = new MySqlParameter("@Title", MySqlDbType.VarChar) {
                    Value = title
                };
                list.Add(item);
            }
            if (flowIDString.IsGuid())
            {
                builder.Append("AND FlowID=@FlowID ");
                list.Add(new MySqlParameter("@FlowID", flowIDString));
            }
            if (date1.IsDateTime())
            {
                builder.Append("AND WriteTime>=@WriteTime1 ");
                list.Add(new MySqlParameter("@WriteTime1", date1));
            }
            if (date2.IsDateTime())
            {
                builder.Append("AND WriteTime<=@WriteTime2 ");
                list.Add(new MySqlParameter("@WriteTime2", date2.ToDateTime().ToString("yyyy-MM-dd 23:59:59")));
            }
            string sql = this.dbHelper.GetPaerSql("WorkFlowArchives", "ID,FlowName,StepName,Title,WriteTime", builder.ToString(), order.IsNullOrEmpty() ? "WriteTime DESC" : order, pageSize, pageNumber, out count, list.ToArray());
            return this.dbHelper.GetDataTable(sql, list.ToArray());
        }

        public int Update(YJ.Data.Model.WorkFlowArchives model)
        {
            string sql = "UPDATE workflowarchives SET \r\n\t\t\t\tFlowID=@FlowID,StepID=@StepID,FlowName=@FlowName,StepName=@StepName,TaskID=@TaskID,GroupID=@GroupID,InstanceID=@InstanceID,Title=@Title,Contents=@Contents,Comments=@Comments,WriteTime=@WriteTime\r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[12];
            MySqlParameter parameter1 = new MySqlParameter("@FlowID", MySqlDbType.VarChar, 0x24) {
                Value = model.FlowID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@StepID", MySqlDbType.VarChar, 0x24) {
                Value = model.StepID
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@FlowName", MySqlDbType.Text, -1) {
                Value = model.FlowName
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@StepName", MySqlDbType.Text, -1) {
                Value = model.StepName
            };
            parameterArray1[3] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@TaskID", MySqlDbType.VarChar, 0x24) {
                Value = model.TaskID
            };
            parameterArray1[4] = parameter5;
            MySqlParameter parameter6 = new MySqlParameter("@GroupID", MySqlDbType.VarChar, 0x24) {
                Value = model.GroupID
            };
            parameterArray1[5] = parameter6;
            MySqlParameter parameter7 = new MySqlParameter("@InstanceID", MySqlDbType.Text, -1) {
                Value = model.InstanceID
            };
            parameterArray1[6] = parameter7;
            MySqlParameter parameter8 = new MySqlParameter("@Title", MySqlDbType.Text, -1) {
                Value = model.Title
            };
            parameterArray1[7] = parameter8;
            MySqlParameter parameter9 = new MySqlParameter("@Contents", MySqlDbType.LongText, -1) {
                Value = model.Contents
            };
            parameterArray1[8] = parameter9;
            MySqlParameter parameter10 = new MySqlParameter("@Comments", MySqlDbType.LongText, -1) {
                Value = model.Comments
            };
            parameterArray1[9] = parameter10;
            MySqlParameter parameter11 = new MySqlParameter("@WriteTime", MySqlDbType.DateTime, -1) {
                Value = model.WriteTime
            };
            parameterArray1[10] = parameter11;
            MySqlParameter parameter12 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[11] = parameter12;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

