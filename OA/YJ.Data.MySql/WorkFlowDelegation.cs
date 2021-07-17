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


    public class WorkFlowDelegation : IWorkFlowDelegation
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkFlowDelegation model)
        {
            string sql = "INSERT INTO workflowdelegation\r\n\t\t\t\t(ID,UserID,StartTime,EndTime,FlowID,ToUserID,WriteTime,Note) \r\n\t\t\t\tVALUES(@ID,@UserID,@StartTime,@EndTime,@FlowID,@ToUserID,@WriteTime,@Note)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[8];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@UserID", MySqlDbType.VarChar, 0x24) {
                Value = model.UserID
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@StartTime", MySqlDbType.DateTime, -1) {
                Value = model.StartTime
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@EndTime", MySqlDbType.DateTime, -1) {
                Value = model.EndTime
            };
            parameterArray1[3] = parameter4;
            parameterArray1[4] = !model.FlowID.HasValue ? new MySqlParameter("@FlowID", MySqlDbType.VarChar, 0x24) { Value = DBNull.Value } : new MySqlParameter("@FlowID", MySqlDbType.VarChar, 0x24) { Value = model.FlowID };
            MySqlParameter parameter7 = new MySqlParameter("@ToUserID", MySqlDbType.VarChar, 0x24) {
                Value = model.ToUserID
            };
            parameterArray1[5] = parameter7;
            MySqlParameter parameter8 = new MySqlParameter("@WriteTime", MySqlDbType.DateTime, -1) {
                Value = model.WriteTime
            };
            parameterArray1[6] = parameter8;
            parameterArray1[7] = (model.Note == null) ? new MySqlParameter("@Note", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Note", MySqlDbType.Text, -1) { Value = model.Note };
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.WorkFlowDelegation> DataReaderToList(MySqlDataReader dataReader)
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
            string sql = "DELETE FROM workflowdelegation WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.WorkFlowDelegation Get(Guid id)
        {
            string sql = "SELECT * FROM workflowdelegation WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowDelegation> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkFlowDelegation> GetAll()
        {
            string sql = "SELECT * FROM workflowdelegation";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowDelegation> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkFlowDelegation> GetByUserID(Guid userID)
        {
            string sql = "SELECT * FROM WorkFlowDelegation WHERE UserID=@UserID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@UserID", MySqlDbType.VarChar) {
                Value = userID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowDelegation> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM workflowdelegation";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public List<YJ.Data.Model.WorkFlowDelegation> GetNoExpiredList()
        {
            string sql = "SELECT * FROM WorkFlowDelegation WHERE EndTime>=@EndTime";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@EndTime", MySqlDbType.DateTime) {
                Value = DateTimeNew.Now
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowDelegation> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkFlowDelegation> GetPagerData(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string userID, [Optional, DefaultParameterValue("")] string startTime, [Optional, DefaultParameterValue("")] string endTime)
        {
            long num;
            StringBuilder builder = new StringBuilder();
            List<MySqlParameter> list = new List<MySqlParameter>();
            if (userID.IsGuid())
            {
                builder.Append("AND UserID=@UserID ");
                MySqlParameter item = new MySqlParameter("@UserID", MySqlDbType.VarChar) {
                    Value = userID
                };
                list.Add(item);
            }
            if (startTime.IsDateTime())
            {
                builder.Append("AND StartTime>=@StartTime ");
                MySqlParameter parameter2 = new MySqlParameter("@StartTime", MySqlDbType.DateTime) {
                    Value = startTime.ToDateTime().ToString("yyyy-MM-dd").ToDateTime()
                };
                list.Add(parameter2);
            }
            if (endTime.IsDateTime())
            {
                builder.Append("AND EndTime<=@EndTime ");
                MySqlParameter parameter3 = new MySqlParameter("@EndTime", MySqlDbType.DateTime) {
                    Value = endTime.ToDateTime().AddDays(1.0).ToString("yyyy-MM-dd").ToDateTime()
                };
                list.Add(parameter3);
            }
            int pageSize = Tools.GetPageSize();
            int pageNumber = Tools.GetPageNumber();
            string sql = this.dbHelper.GetPaerSql("WorkFlowDelegation", "*", builder.ToString(), "WriteTime Desc", pageSize, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, pageSize, pageNumber, query);
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.WorkFlowDelegation> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public List<YJ.Data.Model.WorkFlowDelegation> GetPagerData(out long count, int pageSize, int pageNumber, [Optional, DefaultParameterValue("")] string userID, [Optional, DefaultParameterValue("")] string startTime, [Optional, DefaultParameterValue("")] string endTime, [Optional, DefaultParameterValue("")] string order)
        {
            StringBuilder builder = new StringBuilder();
            List<MySqlParameter> list = new List<MySqlParameter>();
            if (userID.IsGuid())
            {
                builder.Append("AND UserID=@UserID ");
                MySqlParameter item = new MySqlParameter("@UserID", MySqlDbType.VarChar) {
                    Value = userID
                };
                list.Add(item);
            }
            if (startTime.IsDateTime())
            {
                builder.Append("AND StartTime>=@StartTime ");
                MySqlParameter parameter2 = new MySqlParameter("@StartTime", MySqlDbType.DateTime) {
                    Value = startTime.ToDateTime().ToString("yyyy-MM-dd").ToDateTime()
                };
                list.Add(parameter2);
            }
            if (endTime.IsDateTime())
            {
                builder.Append("AND EndTime<=@EndTime ");
                MySqlParameter parameter3 = new MySqlParameter("@EndTime", MySqlDbType.DateTime) {
                    Value = endTime.ToDateTime().AddDays(1.0).ToString("yyyy-MM-dd").ToDateTime()
                };
                list.Add(parameter3);
            }
            string sql = this.dbHelper.GetPaerSql("WorkFlowDelegation", "*", builder.ToString(), order.IsNullOrEmpty() ? "WriteTime Desc" : order, pageSize, pageNumber, out count, list.ToArray());
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.WorkFlowDelegation> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public int Update(YJ.Data.Model.WorkFlowDelegation model)
        {
            string sql = "UPDATE workflowdelegation SET \r\n\t\t\t\tUserID=@UserID,StartTime=@StartTime,EndTime=@EndTime,FlowID=@FlowID,ToUserID=@ToUserID,WriteTime=@WriteTime,Note=@Note\r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[8];
            MySqlParameter parameter1 = new MySqlParameter("@UserID", MySqlDbType.VarChar, 0x24) {
                Value = model.UserID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@StartTime", MySqlDbType.DateTime, -1) {
                Value = model.StartTime
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@EndTime", MySqlDbType.DateTime, -1) {
                Value = model.EndTime
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = !model.FlowID.HasValue ? new MySqlParameter("@FlowID", MySqlDbType.VarChar, 0x24) { Value = DBNull.Value } : new MySqlParameter("@FlowID", MySqlDbType.VarChar, 0x24) { Value = model.FlowID };
            MySqlParameter parameter6 = new MySqlParameter("@ToUserID", MySqlDbType.VarChar, 0x24) {
                Value = model.ToUserID
            };
            parameterArray1[4] = parameter6;
            MySqlParameter parameter7 = new MySqlParameter("@WriteTime", MySqlDbType.DateTime, -1) {
                Value = model.WriteTime
            };
            parameterArray1[5] = parameter7;
            parameterArray1[6] = (model.Note == null) ? new MySqlParameter("@Note", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Note", MySqlDbType.Text, -1) { Value = model.Note };
            MySqlParameter parameter10 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[7] = parameter10;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

