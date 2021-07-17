using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Text;
using YJ.Data.Interface;
using YJ.Data.Model;
using YJ.Utility;
namespace YJ.Data.MSSQL
{


    public class WorkFlowTask : IWorkFlowTask
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkFlowTask model)
        {
            string sql = "INSERT INTO WorkFlowTask\r\n\t\t\t\t(ID,PrevID,PrevStepID,FlowID,StepID,StepName,InstanceID,GroupID,Type,Title,SenderID,SenderName,SenderTime,ReceiveID,ReceiveName,ReceiveTime,OpenTime,CompletedTime,CompletedTime1,Comment,IsSign,Status,Note,Sort,SubFlowGroupID,OtherType,Files,IsExpiredAutoSubmit,StepSort) \r\n\t\t\t\tVALUES(@ID,@PrevID,@PrevStepID,@FlowID,@StepID,@StepName,@InstanceID,@GroupID,@Type,@Title,@SenderID,@SenderName,@SenderTime,@ReceiveID,@ReceiveName,@ReceiveTime,@OpenTime,@CompletedTime,@CompletedTime1,@Comment,@IsSign,@Status,@Note,@Sort,@SubFlowGroupID,@OtherType,@Files,@IsExpiredAutoSubmit,@StepSort)";
            SqlParameter[] parameterArray1 = new SqlParameter[0x1d];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@PrevID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.PrevID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@PrevStepID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.PrevStepID
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.FlowID
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@StepID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.StepID
            };
            parameterArray1[4] = parameter5;
            SqlParameter parameter6 = new SqlParameter("@StepName", SqlDbType.NVarChar, 0x3e8) {
                Value = model.StepName
            };
            parameterArray1[5] = parameter6;
            SqlParameter parameter7 = new SqlParameter("@InstanceID", SqlDbType.VarChar, 50) {
                Value = model.InstanceID
            };
            parameterArray1[6] = parameter7;
            SqlParameter parameter8 = new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.GroupID
            };
            parameterArray1[7] = parameter8;
            SqlParameter parameter9 = new SqlParameter("@Type", SqlDbType.Int, -1) {
                Value = model.Type
            };
            parameterArray1[8] = parameter9;
            SqlParameter parameter10 = new SqlParameter("@Title", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Title
            };
            parameterArray1[9] = parameter10;
            SqlParameter parameter11 = new SqlParameter("@SenderID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.SenderID
            };
            parameterArray1[10] = parameter11;
            SqlParameter parameter12 = new SqlParameter("@SenderName", SqlDbType.NVarChar, 100) {
                Value = model.SenderName
            };
            parameterArray1[11] = parameter12;
            SqlParameter parameter13 = new SqlParameter("@SenderTime", SqlDbType.DateTime, 8) {
                Value = model.SenderTime
            };
            parameterArray1[12] = parameter13;
            SqlParameter parameter14 = new SqlParameter("@ReceiveID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ReceiveID
            };
            parameterArray1[13] = parameter14;
            SqlParameter parameter15 = new SqlParameter("@ReceiveName", SqlDbType.NVarChar, 100) {
                Value = model.ReceiveName
            };
            parameterArray1[14] = parameter15;
            SqlParameter parameter16 = new SqlParameter("@ReceiveTime", SqlDbType.DateTime, 8) {
                Value = model.ReceiveTime
            };
            parameterArray1[15] = parameter16;
            parameterArray1[0x10] = !model.OpenTime.HasValue ? new SqlParameter("@OpenTime", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@OpenTime", SqlDbType.DateTime, 8) { Value = model.OpenTime };
            parameterArray1[0x11] = !model.CompletedTime.HasValue ? new SqlParameter("@CompletedTime", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@CompletedTime", SqlDbType.DateTime, 8) { Value = model.CompletedTime };
            parameterArray1[0x12] = !model.CompletedTime1.HasValue ? new SqlParameter("@CompletedTime1", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@CompletedTime1", SqlDbType.DateTime, 8) { Value = model.CompletedTime1 };
            parameterArray1[0x13] = (model.Comment == null) ? new SqlParameter("@Comment", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Comment", SqlDbType.VarChar, -1) { Value = model.Comment };
            parameterArray1[20] = !model.IsSign.HasValue ? new SqlParameter("@IsSign", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@IsSign", SqlDbType.Int, -1) { Value = model.IsSign };
            SqlParameter parameter27 = new SqlParameter("@Status", SqlDbType.Int, -1) {
                Value = model.Status
            };
            parameterArray1[0x15] = parameter27;
            parameterArray1[0x16] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.NVarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.NVarChar, -1) { Value = model.Note };
            SqlParameter parameter30 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[0x17] = parameter30;
            parameterArray1[0x18] = (model.SubFlowGroupID == null) ? new SqlParameter("@SubFlowGroupID", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@SubFlowGroupID", SqlDbType.VarChar, -1) { Value = model.SubFlowGroupID };
            parameterArray1[0x19] = !model.OtherType.HasValue ? new SqlParameter("@OtherType", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@OtherType", SqlDbType.Int, -1) { Value = model.OtherType };
            parameterArray1[0x1a] = (model.Files == null) ? new SqlParameter("@Files", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Files", SqlDbType.VarChar, -1) { Value = model.Files };
            SqlParameter parameter37 = new SqlParameter("@IsExpiredAutoSubmit", SqlDbType.Int, -1) {
                Value = model.IsExpiredAutoSubmit
            };
            parameterArray1[0x1b] = parameter37;
            SqlParameter parameter38 = new SqlParameter("@StepSort", SqlDbType.Int, -1) {
                Value = model.StepSort
            };
            parameterArray1[0x1c] = parameter38;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int Completed(Guid taskID, [Optional, DefaultParameterValue("")] string comment, [Optional, DefaultParameterValue(false)] bool isSign, [Optional, DefaultParameterValue(2)] int status, [Optional, DefaultParameterValue("")] string note, [Optional, DefaultParameterValue("")] string files)
        {
            string sql = "UPDATE WorkFlowTask SET Comment=@Comment,CompletedTime1=@CompletedTime1,IsSign=@IsSign,Status=@Status,Note=@Note,Files=@Files WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[7];
            parameterArray1[0] = comment.IsNullOrEmpty() ? new SqlParameter("@Comment", SqlDbType.VarChar) { Value = DBNull.Value } : new SqlParameter("@Comment", SqlDbType.VarChar) { Value = comment };
            SqlParameter parameter3 = new SqlParameter("@CompletedTime1", SqlDbType.DateTime) {
                Value = DateTimeNew.Now
            };
            parameterArray1[1] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@IsSign", SqlDbType.Int) {
                Value = isSign ? 1 : 0
            };
            parameterArray1[2] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@Status", SqlDbType.Int) {
                Value = status
            };
            parameterArray1[3] = parameter5;
            parameterArray1[4] = note.IsNullOrEmpty() ? new SqlParameter("@Note", SqlDbType.NVarChar) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.NVarChar) { Value = note };
            parameterArray1[5] = files.IsNullOrEmpty() ? new SqlParameter("@Files", SqlDbType.VarChar) { Value = DBNull.Value } : new SqlParameter("@Files", SqlDbType.VarChar) { Value = files };
            SqlParameter parameter10 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = taskID
            };
            parameterArray1[6] = parameter10;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.WorkFlowTask> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.WorkFlowTask> list = new List<YJ.Data.Model.WorkFlowTask>();
            YJ.Data.Model.WorkFlowTask item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.WorkFlowTask {
                    ID = dataReader.GetGuid(0),
                    PrevID = dataReader.GetGuid(1),
                    PrevStepID = dataReader.GetGuid(2),
                    FlowID = dataReader.GetGuid(3),
                    StepID = dataReader.GetGuid(4),
                    StepName = dataReader.GetString(5),
                    InstanceID = dataReader.GetString(6),
                    GroupID = dataReader.GetGuid(7),
                    Type = dataReader.GetInt32(8),
                    Title = dataReader.GetString(9),
                    SenderID = dataReader.GetGuid(10),
                    SenderName = dataReader.GetString(11),
                    SenderTime = dataReader.GetDateTime(12),
                    ReceiveID = dataReader.GetGuid(13),
                    ReceiveName = dataReader.GetString(14),
                    ReceiveTime = dataReader.GetDateTime(15)
                };
                if (!dataReader.IsDBNull(0x10))
                {
                    item.OpenTime = new DateTime?(dataReader.GetDateTime(0x10));
                }
                if (!dataReader.IsDBNull(0x11))
                {
                    item.CompletedTime = new DateTime?(dataReader.GetDateTime(0x11));
                }
                if (!dataReader.IsDBNull(0x12))
                {
                    item.CompletedTime1 = new DateTime?(dataReader.GetDateTime(0x12));
                }
                if (!dataReader.IsDBNull(0x13))
                {
                    item.Comment = dataReader.GetString(0x13);
                }
                if (!dataReader.IsDBNull(20))
                {
                    item.IsSign = new int?(dataReader.GetInt32(20));
                }
                item.Status = dataReader.GetInt32(0x15);
                if (!dataReader.IsDBNull(0x16))
                {
                    item.Note = dataReader.GetString(0x16);
                }
                item.Sort = dataReader.GetInt32(0x17);
                if (!dataReader.IsDBNull(0x18))
                {
                    item.SubFlowGroupID = dataReader.GetString(0x18);
                }
                if (!dataReader.IsDBNull(0x19))
                {
                    item.OtherType = new int?(dataReader.GetInt32(0x19));
                }
                if (!dataReader.IsDBNull(0x1a))
                {
                    item.Files = dataReader.GetString(0x1a);
                }
                item.IsExpiredAutoSubmit = dataReader.GetInt32(0x1b);
                item.StepSort = dataReader.GetInt32(0x1c);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM WorkFlowTask WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int Delete(Guid flowID, Guid groupID)
        {
            string sql = "DELETE FROM WorkFlowTask WHERE GroupID=@GroupID";
            List<SqlParameter> list1 = new List<SqlParameter>();
            SqlParameter item = new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier) {
                Value = groupID
            };
            list1.Add(item);
            List<SqlParameter> list = list1;
            if (!flowID.IsEmptyGuid())
            {
                sql = sql + " AND FlowID=@FlowID";
                SqlParameter parameter2 = new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier) {
                    Value = flowID
                };
                list.Add(parameter2);
            }
            return this.dbHelper.Execute(sql, list.ToArray(), false);
        }

        public int DeleteTempTasks(Guid flowID, Guid stepID, Guid groupID, Guid prevStepID)
        {
            string sql = "DELETE WorkFlowTask WHERE FlowID=@FlowID AND StepID=@StepID AND GroupID=@GroupID AND Status=-1";
            List<SqlParameter> list1 = new List<SqlParameter>();
            SqlParameter item = new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier) {
                Value = flowID
            };
            list1.Add(item);
            SqlParameter parameter2 = new SqlParameter("@StepID", SqlDbType.UniqueIdentifier) {
                Value = stepID
            };
            list1.Add(parameter2);
            SqlParameter parameter3 = new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier) {
                Value = groupID
            };
            list1.Add(parameter3);
            List<SqlParameter> list = list1;
            if (!prevStepID.IsEmptyGuid())
            {
                sql = sql + " AND PrevStepID=@PrevStepID";
                SqlParameter parameter4 = new SqlParameter("@PrevStepID", SqlDbType.UniqueIdentifier) {
                    Value = prevStepID
                };
                list.Add(parameter4);
            }
            return this.dbHelper.Execute(sql, list.ToArray(), false);
        }

        public YJ.Data.Model.WorkFlowTask Get(Guid id)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkFlowTask> GetAll()
        {
            string sql = "SELECT * FROM WorkFlowTask";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkFlowTask> GetBySubFlowGroupID(Guid subflowGroupID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE CHARINDEX(@SubFlowGroupID,SubFlowGroupID)>0";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@SubFlowGroupID", SqlDbType.VarChar) {
                Value = subflowGroupID.ToString()
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM WorkFlowTask";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public List<YJ.Data.Model.WorkFlowTask> GetExpiredAutoSubmitTasks()
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE CompletedTime<'" + DateTimeNew.Now.ToDateTimeStringS() + "' AND IsExpiredAutoSubmit=1 AND Status IN(0,1)";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public Guid GetFirstSnderID(Guid flowID, Guid groupID)
        {
            string sql = "SELECT SenderID FROM WorkFlowTask WHERE FlowID=@FlowID AND GroupID=@GroupID AND PrevID=@PrevID";
            SqlParameter[] parameterArray1 = new SqlParameter[3];
            SqlParameter parameter1 = new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier) {
                Value = flowID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier) {
                Value = groupID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@PrevID", SqlDbType.UniqueIdentifier) {
                Value = Guid.Empty
            };
            parameterArray1[2] = parameter3;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.GetFieldValue(sql, parameter).ToGuid();
        }

        public List<YJ.Data.Model.WorkFlowTask> GetInstances(Guid[] flowID, Guid[] senderID, Guid[] receiveID, out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string flowid, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(0)] int status)
        {
            long num;
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder("SELECT a.*,ROW_NUMBER() OVER(ORDER BY a.SenderTime DESC) AS PagerAutoRowNumber FROM WorkFlowTask a\r\n                WHERE a.ID=(SELECT TOP 1 ID FROM WorkFlowTask WHERE FlowID=a.FlowID AND GroupID=a.GroupID ORDER BY Sort DESC)");
            if (status > 0)
            {
                if (status == 1)
                {
                    builder.Append(" AND a.Status IN(0,1,5)");
                }
                else if (status == 2)
                {
                    builder.Append(" AND a.Status IN(2,3,4)");
                }
            }
            if ((flowID != null) && (flowID.Length > 0))
            {
                builder.Append(string.Format(" AND a.FlowID IN({0})", Tools.GetSqlInString<Guid>(flowID, true)));
            }
            if ((senderID != null) && (senderID.Length > 0))
            {
                if (senderID.Length == 1)
                {
                    builder.Append(" AND a.SenderID=@SenderID");
                    SqlParameter item = new SqlParameter("@SenderID", SqlDbType.UniqueIdentifier) {
                        Value = senderID[0]
                    };
                    list.Add(item);
                }
                else
                {
                    builder.Append(string.Format(" AND a.SenderID IN({0})", Tools.GetSqlInString<Guid>(senderID, true)));
                }
            }
            if ((receiveID != null) && (receiveID.Length > 0))
            {
                if (senderID.Length == 1)
                {
                    builder.Append(" AND a.ReceiveID=@ReceiveID");
                    SqlParameter parameter2 = new SqlParameter("@ReceiveID", SqlDbType.UniqueIdentifier) {
                        Value = receiveID[0]
                    };
                    list.Add(parameter2);
                }
                else
                {
                    builder.Append(string.Format(" AND a.ReceiveID IN({0})", Tools.GetSqlInString<Guid>(receiveID, true)));
                }
            }
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND CHARINDEX(@Title,a.Title)>0");
                SqlParameter parameter3 = new SqlParameter("@Title", SqlDbType.NVarChar, 0x7d0) {
                    Value = title
                };
                list.Add(parameter3);
            }
            if (flowid.IsGuid())
            {
                builder.Append(" AND a.FlowID=@FlowID");
                SqlParameter parameter4 = new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier) {
                    Value = flowid.ToGuid()
                };
                list.Add(parameter4);
            }
            else if (!flowid.IsNullOrEmpty() && (flowid.IndexOf(',') >= 0))
            {
                builder.Append(" AND a.FlowID IN(" + Tools.GetSqlInString(flowid, true, ",") + ")");
            }
            if (date1.IsDateTime())
            {
                builder.Append(" AND a.SenderTime>=@SenderTime");
                SqlParameter parameter5 = new SqlParameter("@SenderTime", SqlDbType.DateTime) {
                    Value = date1.ToDateTime().Date
                };
                list.Add(parameter5);
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND a.SenderTime<=@SenderTime1");
                SqlParameter parameter6 = new SqlParameter("@SenderTime1", SqlDbType.DateTime) {
                    Value = date2.ToDateTime().AddDays(1.0).Date
                };
                list.Add(parameter6);
            }
            int pageSize = Tools.GetPageSize();
            int pageNumber = Tools.GetPageNumber();
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), pageSize, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, pageSize, pageNumber, query);
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.WorkFlowTask> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public DataTable GetInstances1(Guid[] flowID, Guid[] senderID, Guid[] receiveID, out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string flowid, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(0)] int status)
        {
            long num;
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder();
            if (status > 0)
            {
                if (status == 1)
                {
                    builder.Append(" AND Status IN(0,1,5)");
                }
                else if (status == 2)
                {
                    builder.Append(" AND Status IN(2,3,4)");
                }
            }
            if ((flowID != null) && (flowID.Length > 0))
            {
                builder.Append(string.Format(" AND FlowID IN({0})", Tools.GetSqlInString<Guid>(flowID, true)));
            }
            if ((senderID != null) && (senderID.Length > 0))
            {
                if (senderID.Length == 1)
                {
                    builder.Append(" AND SenderID=@SenderID");
                    SqlParameter item = new SqlParameter("@SenderID", SqlDbType.UniqueIdentifier) {
                        Value = senderID[0]
                    };
                    list.Add(item);
                }
                else
                {
                    builder.Append(string.Format(" AND SenderID IN({0})", Tools.GetSqlInString<Guid>(senderID, true)));
                }
            }
            if ((receiveID != null) && (receiveID.Length > 0))
            {
                if (receiveID.Length == 1)
                {
                    builder.Append(" AND ReceiveID=@ReceiveID");
                    SqlParameter parameter2 = new SqlParameter("@ReceiveID", SqlDbType.UniqueIdentifier) {
                        Value = receiveID[0]
                    };
                    list.Add(parameter2);
                }
                else
                {
                    builder.Append(string.Format(" AND ReceiveID IN({0})", Tools.GetSqlInString<Guid>(receiveID, true)));
                }
            }
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND CHARINDEX(@Title,Title)>0");
                SqlParameter parameter3 = new SqlParameter("@Title", SqlDbType.NVarChar, 0x7d0) {
                    Value = title
                };
                list.Add(parameter3);
            }
            if (flowid.IsGuid())
            {
                builder.Append(" AND FlowID=@FlowID");
                SqlParameter parameter4 = new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier) {
                    Value = flowid.ToGuid()
                };
                list.Add(parameter4);
            }
            else if (!flowid.IsNullOrEmpty() && (flowid.IndexOf(',') >= 0))
            {
                builder.Append(" AND FlowID IN(" + Tools.GetSqlInString(flowid, true, ",") + ")");
            }
            if (date1.IsDateTime())
            {
                builder.Append(" AND SenderTime>=@SenderTime");
                SqlParameter parameter5 = new SqlParameter("@SenderTime", SqlDbType.DateTime) {
                    Value = date1.ToDateTime().Date
                };
                list.Add(parameter5);
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND SenderTime<=@SenderTime1");
                SqlParameter parameter6 = new SqlParameter("@SenderTime1", SqlDbType.DateTime) {
                    Value = date2.ToDateTime().AddDays(1.0).Date
                };
                list.Add(parameter6);
            }
            string sql = string.Format("select *,ROW_NUMBER() OVER(ORDER BY SenderTime DESC) AS PagerAutoRowNumber from(\r\nselect flowid,groupid,MAX(SenderTime) SenderTime from WorkFlowTask WHERE 1=1 {0} group by FlowID, GroupID\r\n) temp", builder.ToString());
            int pageSize = Tools.GetPageSize();
            int pageNumber = Tools.GetPageNumber();
            string str2 = this.dbHelper.GetPaerSql(sql, pageSize, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, pageSize, pageNumber, query);
            return this.dbHelper.GetDataTable(str2, list.ToArray());
        }

        public DataTable GetInstances1(Guid[] flowID, Guid[] senderID, Guid[] receiveID, out long count, int size, int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string flowid, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(0)] int status, [Optional, DefaultParameterValue("")] string order)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder();
            if (status > 0)
            {
                if (status == 1)
                {
                    builder.Append(" AND Status IN(0,1,5)");
                }
                else if (status == 2)
                {
                    builder.Append(" AND Status IN(2,3,4)");
                }
            }
            if ((flowID != null) && (flowID.Length > 0))
            {
                builder.Append(string.Format(" AND FlowID IN({0})", Tools.GetSqlInString<Guid>(flowID, true)));
            }
            if ((senderID != null) && (senderID.Length > 0))
            {
                if (senderID.Length == 1)
                {
                    builder.Append(" AND SenderID=@SenderID");
                    SqlParameter item = new SqlParameter("@SenderID", SqlDbType.UniqueIdentifier) {
                        Value = senderID[0]
                    };
                    list.Add(item);
                }
                else
                {
                    builder.Append(string.Format(" AND SenderID IN({0})", Tools.GetSqlInString<Guid>(senderID, true)));
                }
            }
            if ((receiveID != null) && (receiveID.Length > 0))
            {
                if (receiveID.Length == 1)
                {
                    builder.Append(" AND SenderID=@ReceiveID");
                    SqlParameter parameter2 = new SqlParameter("@ReceiveID", SqlDbType.UniqueIdentifier) {
                        Value = receiveID[0]
                    };
                    list.Add(parameter2);
                }
                else
                {
                    builder.Append(string.Format(" AND SenderID IN({0})", Tools.GetSqlInString<Guid>(receiveID, true)));
                }
            }
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND CHARINDEX(@Title,Title)>0");
                SqlParameter parameter3 = new SqlParameter("@Title", SqlDbType.NVarChar, 0x7d0) {
                    Value = title
                };
                list.Add(parameter3);
            }
            if (flowid.IsGuid())
            {
                builder.Append(" AND FlowID=@FlowID");
                SqlParameter parameter4 = new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier) {
                    Value = flowid.ToGuid()
                };
                list.Add(parameter4);
            }
            else if (!flowid.IsNullOrEmpty() && (flowid.IndexOf(',') >= 0))
            {
                builder.Append(" AND FlowID IN(" + Tools.GetSqlInString(flowid, true, ",") + ")");
            }
            if (date1.IsDateTime())
            {
                builder.Append(" AND SenderTime>=@SenderTime");
                SqlParameter parameter5 = new SqlParameter("@SenderTime", SqlDbType.DateTime) {
                    Value = date1.ToDateTime().Date
                };
                list.Add(parameter5);
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND SenderTime<=@SenderTime1");
                SqlParameter parameter6 = new SqlParameter("@SenderTime1", SqlDbType.DateTime) {
                    Value = date2.ToDateTime().AddDays(1.0).Date
                };
                list.Add(parameter6);
            }
            string sql = string.Format("select *,ROW_NUMBER() OVER(ORDER BY " + (order.IsNullOrEmpty() ? "SenderTime DESC" : order) + ") AS PagerAutoRowNumber from(select flowid,groupid,MAX(SenderTime) SenderTime from WorkFlowTask WHERE 1=1 {0} group by FlowID, GroupID) temp", builder.ToString());
            string str2 = this.dbHelper.GetPaerSql(sql, size, number, out count, list.ToArray());
            return this.dbHelper.GetDataTable(str2, list.ToArray());
        }

        public YJ.Data.Model.WorkFlowTask GetLastTask(Guid flowID, Guid groupID)
        {
            string sql = "SELECT TOP 1 * FROM WorkFlowTask WHERE FlowID=@FlowID AND GroupID=@GroupID ORDER BY Sort DESC";
            SqlParameter[] parameterArray1 = new SqlParameter[2];
            SqlParameter parameter1 = new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier) {
                Value = flowID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier) {
                Value = groupID
            };
            parameterArray1[1] = parameter2;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkFlowTask> GetNextTaskList(Guid taskID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE PrevID=@PrevID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@PrevID", SqlDbType.UniqueIdentifier) {
                Value = taskID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }
        /// <summary>
        /// 得到一个流程实例前一步骤的处理者
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<Guid> GetPrevSnderID(Guid flowID, Guid stepID, Guid groupID)
        {
            string sql = "SELECT ReceiveID FROM WorkFlowTask WHERE ID=(SELECT PrevID FROM WorkFlowTask WHERE FlowID=@FlowID AND StepID=@StepID AND GroupID=@GroupID)";
            SqlParameter[] parameterArray1 = new SqlParameter[3];
            SqlParameter parameter1 = new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier) {
                Value = flowID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@StepID", SqlDbType.UniqueIdentifier) {
                Value = stepID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier) {
                Value = groupID
            };
            parameterArray1[2] = parameter3;
            SqlParameter[] parameter = parameterArray1;
            DataTable dt = this.dbHelper.GetDataTable(sql, parameter);
            List<Guid> senderList = new List<Guid>();
            foreach (DataRow dr in dt.Rows)
            {
                Guid senderID;
                if (Guid.TryParse(dr[0].ToString(), out senderID))
                {
                    senderList.Add(senderID);
                }
            }
            return senderList;
        }

        public List<YJ.Data.Model.WorkFlowTask> GetPrevTaskList(Guid taskID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE ID=(SELECT PrevID FROM WorkFlowTask WHERE ID=@ID)";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = taskID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<Guid> GetStepSnderID(Guid flowID, Guid stepID, Guid groupID)
        {
            string sql = "SELECT ReceiveID, Sort FROM WorkFlowTask WHERE FlowID=@FlowID AND StepID=@StepID AND GroupID=@GroupID AND Sort=(SELECT ISNULL(MAX(Sort),0) FROM WorkFlowTask WHERE FlowID=@FlowID AND StepID=@StepID AND GroupID=@GroupID)";
            SqlParameter[] parameterArray1 = new SqlParameter[3];
            SqlParameter parameter1 = new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier) {
                Value = flowID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@StepID", SqlDbType.UniqueIdentifier) {
                Value = stepID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier) {
                Value = groupID
            };
            parameterArray1[2] = parameter3;
            SqlParameter[] parameter = parameterArray1;
            DataTable dataTable = this.dbHelper.GetDataTable(sql, parameter);
            List<Guid> list = new List<Guid>();
            foreach (DataRow row in dataTable.Rows)
            {
                Guid guid;
                if (Guid.TryParse(row[0].ToString(), out guid))
                {
                    list.Add(guid);
                }
            }
            return list;
        }

        public List<YJ.Data.Model.WorkFlowTask> GetTaskList(Guid taskID, [Optional, DefaultParameterValue(true)] bool isStepID)
        {
            YJ.Data.Model.WorkFlowTask task = this.Get(taskID);
            if (task == null)
            {
                return new List<YJ.Data.Model.WorkFlowTask>();
            }
            string sql = string.Format("SELECT * FROM WorkFlowTask WHERE FlowID=@FlowID AND GroupID=@GroupID AND PrevID=@PrevID AND {0}", isStepID ? "StepID=@StepID" : "PrevStepID=@StepID");
            SqlParameter[] parameterArray1 = new SqlParameter[4];
            SqlParameter parameter1 = new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier) {
                Value = task.FlowID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier) {
                Value = task.GroupID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@PrevID", SqlDbType.UniqueIdentifier) {
                Value = task.PrevID
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@StepID", SqlDbType.UniqueIdentifier) {
                Value = isStepID ? task.StepID : task.PrevStepID
            };
            parameterArray1[3] = parameter4;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkFlowTask> GetTaskList(Guid flowID, Guid groupID)
        {
            SqlParameter[] parameterArray;
            string sql = string.Empty;
            if (flowID.IsEmptyGuid())
            {
                sql = "SELECT * FROM WorkFlowTask WHERE GroupID=@GroupID";
                SqlParameter[] parameterArray1 = new SqlParameter[1];
                SqlParameter parameter1 = new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier) {
                    Value = groupID
                };
                parameterArray1[0] = parameter1;
                parameterArray = parameterArray1;
            }
            else
            {
                sql = "SELECT * FROM WorkFlowTask WHERE FlowID=@FlowID AND GroupID=@GroupID";
                SqlParameter[] parameterArray2 = new SqlParameter[2];
                SqlParameter parameter2 = new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier) {
                    Value = flowID
                };
                parameterArray2[0] = parameter2;
                SqlParameter parameter3 = new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier) {
                    Value = groupID
                };
                parameterArray2[1] = parameter3;
                parameterArray = parameterArray2;
            }
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameterArray);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkFlowTask> GetTaskList(Guid flowID, Guid stepID, Guid groupID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE StepID=@StepID AND GroupID=@GroupID";
            SqlParameter[] parameterArray1 = new SqlParameter[2];
            SqlParameter parameter1 = new SqlParameter("@StepID", SqlDbType.UniqueIdentifier) {
                Value = stepID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier) {
                Value = groupID
            };
            parameterArray1[1] = parameter2;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkFlowTask> GetTasks(Guid userID, out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string flowid, [Optional, DefaultParameterValue("")] string sender, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(0)] int type)
        {
            long num;
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder("SELECT *,ROW_NUMBER() OVER(ORDER BY " + ((type == 0) ? "SenderTime DESC" : "CompletedTime1 DESC") + ") AS PagerAutoRowNumber FROM WorkFlowTask WHERE ReceiveID=@ReceiveID");
            builder.Append((type == 0) ? " AND Status IN(0,1)" : " AND Status IN(2,3,4,5)");
            SqlParameter item = new SqlParameter("@ReceiveID", SqlDbType.UniqueIdentifier) {
                Value = userID
            };
            list.Add(item);
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND CHARINDEX(@Title,Title)>0");
                SqlParameter parameter2 = new SqlParameter("@Title", SqlDbType.NVarChar, 0x7d0) {
                    Value = title
                };
                list.Add(parameter2);
            }
            if (flowid.IsGuid())
            {
                builder.Append(" AND FlowID=@FlowID");
                SqlParameter parameter3 = new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier) {
                    Value = flowid.ToGuid()
                };
                list.Add(parameter3);
            }
            else if (!flowid.IsNullOrEmpty() && (flowid.IndexOf(',') >= 0))
            {
                builder.Append(" AND FlowID IN(" + Tools.GetSqlInString(flowid, true, ",") + ")");
            }
            if (sender.IsGuid())
            {
                builder.Append(" AND SenderID=@SenderID");
                SqlParameter parameter4 = new SqlParameter("@SenderID", SqlDbType.UniqueIdentifier) {
                    Value = sender.ToGuid()
                };
                list.Add(parameter4);
            }
            if (date1.IsDateTime())
            {
                builder.Append(" AND ReceiveTime>=@ReceiveTime");
                SqlParameter parameter5 = new SqlParameter("@ReceiveTime", SqlDbType.DateTime) {
                    Value = date1.ToDateTime().Date
                };
                list.Add(parameter5);
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND SenderTime<=@ReceiveTime1");
                SqlParameter parameter6 = new SqlParameter("@ReceiveTime1", SqlDbType.DateTime) {
                    Value = date2.ToDateTime().AddDays(1.0).Date
                };
                list.Add(parameter6);
            }
            int pageSize = Tools.GetPageSize();
            int pageNumber = Tools.GetPageNumber();
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), pageSize, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, pageSize, pageNumber, query);
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.WorkFlowTask> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public List<YJ.Data.Model.WorkFlowTask> GetTasks(Guid userID, out long count, int size, int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string flowid, [Optional, DefaultParameterValue("")] string sender, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(0)] int type, [Optional, DefaultParameterValue("")] string order)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder("SELECT *,ROW_NUMBER() OVER(ORDER BY " + (order.IsNullOrEmpty() ? ((type == 0) ? "SenderTime DESC" : "CompletedTime1 DESC") : order) + ") AS PagerAutoRowNumber FROM WorkFlowTask WHERE ReceiveID=@ReceiveID");
            builder.Append((type == 0) ? " AND Status IN(0,1)" : " AND Status IN(2,3,4,5)");
            SqlParameter item = new SqlParameter("@ReceiveID", SqlDbType.UniqueIdentifier) {
                Value = userID
            };
            list.Add(item);
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND CHARINDEX(@Title,Title)>0");
                SqlParameter parameter2 = new SqlParameter("@Title", SqlDbType.NVarChar, 0x7d0) {
                    Value = title
                };
                list.Add(parameter2);
            }
            if (flowid.IsGuid())
            {
                builder.Append(" AND FlowID=@FlowID");
                SqlParameter parameter3 = new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier) {
                    Value = flowid.ToGuid()
                };
                list.Add(parameter3);
            }
            else if (!flowid.IsNullOrEmpty() && (flowid.IndexOf(',') >= 0))
            {
                builder.Append(" AND FlowID IN(" + Tools.GetSqlInString(flowid, true, ",") + ")");
            }
            if (sender.IsGuid())
            {
                builder.Append(" AND SenderID=@SenderID");
                SqlParameter parameter4 = new SqlParameter("@SenderID", SqlDbType.UniqueIdentifier) {
                    Value = sender.ToGuid()
                };
                list.Add(parameter4);
            }
            if (date1.IsDateTime())
            {
                builder.Append(" AND ReceiveTime>=@ReceiveTime");
                SqlParameter parameter5 = new SqlParameter("@ReceiveTime", SqlDbType.DateTime) {
                    Value = date1.ToDateTime().Date
                };
                list.Add(parameter5);
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND SenderTime<=@ReceiveTime1");
                SqlParameter parameter6 = new SqlParameter("@ReceiveTime1", SqlDbType.DateTime) {
                    Value = date2.ToDateTime().AddDays(1.0).Date
                };
                list.Add(parameter6);
            }
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), size, number, out count, list.ToArray());
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.WorkFlowTask> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public int GetTaskStatus(Guid taskID)
        {
            int num;
            string sql = "SELECT Status FROM WorkFlowTask WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = taskID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return (this.dbHelper.GetFieldValue(sql, parameter).IsInt(out num) ? num : -1);
        }

        public List<YJ.Data.Model.WorkFlowTask> GetUserTaskList(Guid flowID, Guid stepID, Guid groupID, Guid userID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE StepID=@StepID AND GroupID=@GroupID AND ReceiveID=@ReceiveID";
            SqlParameter[] parameterArray1 = new SqlParameter[3];
            SqlParameter parameter1 = new SqlParameter("@StepID", SqlDbType.UniqueIdentifier) {
                Value = stepID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier) {
                Value = groupID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@ReceiveID", SqlDbType.UniqueIdentifier) {
                Value = userID
            };
            parameterArray1[2] = parameter3;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public bool HasNoCompletedTasks(Guid flowID, Guid stepID, Guid groupID, Guid userID)
        {
            string sql = "SELECT TOP 1 ID FROM WorkFlowTask WHERE FlowID=@FlowID AND StepID=@StepID AND GroupID=@GroupID AND ReceiveID=@ReceiveID AND Status IN(-1,0,1)";
            SqlParameter[] parameterArray1 = new SqlParameter[4];
            SqlParameter parameter1 = new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier) {
                Value = flowID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@StepID", SqlDbType.UniqueIdentifier) {
                Value = stepID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier) {
                Value = groupID
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@ReceiveID", SqlDbType.UniqueIdentifier) {
                Value = userID
            };
            parameterArray1[3] = parameter4;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            bool hasRows = dataReader.HasRows;
            dataReader.Close();
            return hasRows;
        }

        public bool HasTasks(Guid flowID)
        {
            string sql = "SELECT TOP 1 ID FROM WorkFlowTask WHERE FlowID=@FlowID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier) {
                Value = flowID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            bool hasRows = dataReader.HasRows;
            dataReader.Close();
            return hasRows;
        }

        public int Update(YJ.Data.Model.WorkFlowTask model)
        {
            string sql = "UPDATE WorkFlowTask SET \r\n\t\t\t\tPrevID=@PrevID,PrevStepID=@PrevStepID,FlowID=@FlowID,StepID=@StepID,StepName=@StepName,InstanceID=@InstanceID,GroupID=@GroupID,Type=@Type,Title=@Title,SenderID=@SenderID,SenderName=@SenderName,SenderTime=@SenderTime,ReceiveID=@ReceiveID,ReceiveName=@ReceiveName,ReceiveTime=@ReceiveTime,OpenTime=@OpenTime,CompletedTime=@CompletedTime,CompletedTime1=@CompletedTime1,Comment=@Comment,IsSign=@IsSign,Status=@Status,Note=@Note,Sort=@Sort,SubFlowGroupID=@SubFlowGroupID,OtherType=@OtherType,Files=@Files,IsExpiredAutoSubmit=@IsExpiredAutoSubmit,StepSort=@StepSort  \r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[0x1d];
            SqlParameter parameter1 = new SqlParameter("@PrevID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.PrevID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@PrevStepID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.PrevStepID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.FlowID
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@StepID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.StepID
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@StepName", SqlDbType.NVarChar, 0x3e8) {
                Value = model.StepName
            };
            parameterArray1[4] = parameter5;
            SqlParameter parameter6 = new SqlParameter("@InstanceID", SqlDbType.VarChar, 50) {
                Value = model.InstanceID
            };
            parameterArray1[5] = parameter6;
            SqlParameter parameter7 = new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.GroupID
            };
            parameterArray1[6] = parameter7;
            SqlParameter parameter8 = new SqlParameter("@Type", SqlDbType.Int, -1) {
                Value = model.Type
            };
            parameterArray1[7] = parameter8;
            SqlParameter parameter9 = new SqlParameter("@Title", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Title
            };
            parameterArray1[8] = parameter9;
            SqlParameter parameter10 = new SqlParameter("@SenderID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.SenderID
            };
            parameterArray1[9] = parameter10;
            SqlParameter parameter11 = new SqlParameter("@SenderName", SqlDbType.NVarChar, 100) {
                Value = model.SenderName
            };
            parameterArray1[10] = parameter11;
            SqlParameter parameter12 = new SqlParameter("@SenderTime", SqlDbType.DateTime, 8) {
                Value = model.SenderTime
            };
            parameterArray1[11] = parameter12;
            SqlParameter parameter13 = new SqlParameter("@ReceiveID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ReceiveID
            };
            parameterArray1[12] = parameter13;
            SqlParameter parameter14 = new SqlParameter("@ReceiveName", SqlDbType.NVarChar, 100) {
                Value = model.ReceiveName
            };
            parameterArray1[13] = parameter14;
            SqlParameter parameter15 = new SqlParameter("@ReceiveTime", SqlDbType.DateTime, 8) {
                Value = model.ReceiveTime
            };
            parameterArray1[14] = parameter15;
            parameterArray1[15] = !model.OpenTime.HasValue ? new SqlParameter("@OpenTime", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@OpenTime", SqlDbType.DateTime, 8) { Value = model.OpenTime };
            parameterArray1[0x10] = !model.CompletedTime.HasValue ? new SqlParameter("@CompletedTime", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@CompletedTime", SqlDbType.DateTime, 8) { Value = model.CompletedTime };
            parameterArray1[0x11] = !model.CompletedTime1.HasValue ? new SqlParameter("@CompletedTime1", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@CompletedTime1", SqlDbType.DateTime, 8) { Value = model.CompletedTime1 };
            parameterArray1[0x12] = (model.Comment == null) ? new SqlParameter("@Comment", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Comment", SqlDbType.VarChar, -1) { Value = model.Comment };
            parameterArray1[0x13] = !model.IsSign.HasValue ? new SqlParameter("@IsSign", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@IsSign", SqlDbType.Int, -1) { Value = model.IsSign };
            SqlParameter parameter26 = new SqlParameter("@Status", SqlDbType.Int, -1) {
                Value = model.Status
            };
            parameterArray1[20] = parameter26;
            parameterArray1[0x15] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.NVarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.NVarChar, -1) { Value = model.Note };
            SqlParameter parameter29 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[0x16] = parameter29;
            parameterArray1[0x17] = (model.SubFlowGroupID == null) ? new SqlParameter("@SubFlowGroupID", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@SubFlowGroupID", SqlDbType.VarChar, -1) { Value = model.SubFlowGroupID };
            parameterArray1[0x18] = !model.OtherType.HasValue ? new SqlParameter("@OtherType", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@OtherType", SqlDbType.Int, -1) { Value = model.OtherType };
            parameterArray1[0x19] = (model.Files == null) ? new SqlParameter("@Files", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Files", SqlDbType.VarChar, -1) { Value = model.Files };
            SqlParameter parameter36 = new SqlParameter("@IsExpiredAutoSubmit", SqlDbType.Int, -1) {
                Value = model.IsExpiredAutoSubmit
            };
            parameterArray1[0x1a] = parameter36;
            SqlParameter parameter37 = new SqlParameter("@StepSort", SqlDbType.Int, -1) {
                Value = model.StepSort
            };
            parameterArray1[0x1b] = parameter37;
            SqlParameter parameter38 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0x1c] = parameter38;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int UpdateNextTaskStatus(Guid taskID, int status)
        {
            string sql = "UPDATE WorkFlowTask SET Status=@Status WHERE PrevID=@PrevID";
            SqlParameter[] parameterArray1 = new SqlParameter[2];
            SqlParameter parameter1 = new SqlParameter("@Status", SqlDbType.Int) {
                Value = status
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@PrevID", SqlDbType.UniqueIdentifier) {
                Value = taskID
            };
            parameterArray1[1] = parameter2;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public void UpdateOpenTime(Guid id, DateTime openTime, [Optional, DefaultParameterValue(false)] bool isStatus)
        {
            string sql = "UPDATE WorkFlowTask SET OpenTime=@OpenTime " + (isStatus ? ", Status=1" : "") + " WHERE ID=@ID AND OpenTime IS NULL";
            SqlParameter[] parameterArray1 = new SqlParameter[2];
            parameterArray1[0] = (openTime == DateTime.MinValue) ? new SqlParameter("@OpenTime", SqlDbType.DateTime) { Value = DBNull.Value } : new SqlParameter("@OpenTime", SqlDbType.DateTime) { Value = openTime };
            SqlParameter parameter3 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[1] = parameter3;
            SqlParameter[] parameter = parameterArray1;
            this.dbHelper.Execute(sql, parameter, false);
        }

        public int UpdateTempTasks(Guid flowID, Guid stepID, Guid groupID, DateTime? completedTime, DateTime receiveTime)
        {
            string sql = "UPDATE WorkFlowTask SET CompletedTime=@CompletedTime,ReceiveTime=@ReceiveTime,SenderTime=@SenderTime,Status=0 WHERE FlowID=@FlowID AND StepID=@StepID AND GroupID=@GroupID AND Status=-1";
            SqlParameter[] parameterArray1 = new SqlParameter[6];
            parameterArray1[0] = !completedTime.HasValue ? new SqlParameter("@CompletedTime", SqlDbType.DateTime) { Value = DBNull.Value } : new SqlParameter("@CompletedTime", SqlDbType.DateTime) { Value = completedTime.Value };
            SqlParameter parameter3 = new SqlParameter("@ReceiveTime", SqlDbType.DateTime) {
                Value = receiveTime
            };
            parameterArray1[1] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@SenderTime", SqlDbType.DateTime) {
                Value = receiveTime
            };
            parameterArray1[2] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@FlowID", SqlDbType.UniqueIdentifier) {
                Value = flowID
            };
            parameterArray1[3] = parameter5;
            SqlParameter parameter6 = new SqlParameter("@StepID", SqlDbType.UniqueIdentifier) {
                Value = stepID
            };
            parameterArray1[4] = parameter6;
            SqlParameter parameter7 = new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier) {
                Value = groupID
            };
            parameterArray1[5] = parameter7;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

