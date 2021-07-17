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


    public class WorkFlowTask : IWorkFlowTask
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkFlowTask model)
        {
            string sql = "INSERT INTO workflowtask\r\n\t\t\t\t(ID,PrevID,PrevStepID,FlowID,StepID,StepName,InstanceID,GroupID,Type,Title,SenderID,SenderName,SenderTime,ReceiveID,ReceiveName,ReceiveTime,OpenTime,CompletedTime,CompletedTime1,Comment,IsSign,Status,Note,Sort,SubFlowGroupID,OtherType,Files,IsExpiredAutoSubmit,StepSort) \r\n\t\t\t\tVALUES(@ID,@PrevID,@PrevStepID,@FlowID,@StepID,@StepName,@InstanceID,@GroupID,@Type,@Title,@SenderID,@SenderName,@SenderTime,@ReceiveID,@ReceiveName,@ReceiveTime,@OpenTime,@CompletedTime,@CompletedTime1,@Comment,@IsSign,@Status,@Note,@Sort,@SubFlowGroupID,@OtherType,@Files,@IsExpiredAutoSubmit,@StepSort)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[0x1d];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@PrevID", MySqlDbType.VarChar, 0x24) {
                Value = model.PrevID
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@PrevStepID", MySqlDbType.VarChar, 0x24) {
                Value = model.PrevStepID
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@FlowID", MySqlDbType.VarChar, 0x24) {
                Value = model.FlowID
            };
            parameterArray1[3] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@StepID", MySqlDbType.VarChar, 0x24) {
                Value = model.StepID
            };
            parameterArray1[4] = parameter5;
            MySqlParameter parameter6 = new MySqlParameter("@StepName", MySqlDbType.VarChar, 0xff) {
                Value = model.StepName
            };
            parameterArray1[5] = parameter6;
            MySqlParameter parameter7 = new MySqlParameter("@InstanceID", MySqlDbType.VarChar, 50) {
                Value = model.InstanceID
            };
            parameterArray1[6] = parameter7;
            MySqlParameter parameter8 = new MySqlParameter("@GroupID", MySqlDbType.VarChar, 0x24) {
                Value = model.GroupID
            };
            parameterArray1[7] = parameter8;
            MySqlParameter parameter9 = new MySqlParameter("@Type", MySqlDbType.Int32, 11) {
                Value = model.Type
            };
            parameterArray1[8] = parameter9;
            MySqlParameter parameter10 = new MySqlParameter("@Title", MySqlDbType.VarChar, 0xff) {
                Value = model.Title
            };
            parameterArray1[9] = parameter10;
            MySqlParameter parameter11 = new MySqlParameter("@SenderID", MySqlDbType.VarChar, 0x24) {
                Value = model.SenderID
            };
            parameterArray1[10] = parameter11;
            MySqlParameter parameter12 = new MySqlParameter("@SenderName", MySqlDbType.VarChar, 50) {
                Value = model.SenderName
            };
            parameterArray1[11] = parameter12;
            MySqlParameter parameter13 = new MySqlParameter("@SenderTime", MySqlDbType.DateTime, -1) {
                Value = model.SenderTime
            };
            parameterArray1[12] = parameter13;
            MySqlParameter parameter14 = new MySqlParameter("@ReceiveID", MySqlDbType.VarChar, 0x24) {
                Value = model.ReceiveID
            };
            parameterArray1[13] = parameter14;
            MySqlParameter parameter15 = new MySqlParameter("@ReceiveName", MySqlDbType.VarChar, 50) {
                Value = model.ReceiveName
            };
            parameterArray1[14] = parameter15;
            MySqlParameter parameter16 = new MySqlParameter("@ReceiveTime", MySqlDbType.DateTime, -1) {
                Value = model.ReceiveTime
            };
            parameterArray1[15] = parameter16;
            parameterArray1[0x10] = !model.OpenTime.HasValue ? new MySqlParameter("@OpenTime", MySqlDbType.DateTime, -1) { Value = DBNull.Value } : new MySqlParameter("@OpenTime", MySqlDbType.DateTime, -1) { Value = model.OpenTime };
            parameterArray1[0x11] = !model.CompletedTime.HasValue ? new MySqlParameter("@CompletedTime", MySqlDbType.DateTime, -1) { Value = DBNull.Value } : new MySqlParameter("@CompletedTime", MySqlDbType.DateTime, -1) { Value = model.CompletedTime };
            parameterArray1[0x12] = !model.CompletedTime1.HasValue ? new MySqlParameter("@CompletedTime1", MySqlDbType.DateTime, -1) { Value = DBNull.Value } : new MySqlParameter("@CompletedTime1", MySqlDbType.DateTime, -1) { Value = model.CompletedTime1 };
            parameterArray1[0x13] = (model.Comment == null) ? new MySqlParameter("@Comment", MySqlDbType.VarChar, 0xff) { Value = DBNull.Value } : new MySqlParameter("@Comment", MySqlDbType.VarChar, 0xff) { Value = model.Comment };
            parameterArray1[20] = !model.IsSign.HasValue ? new MySqlParameter("@IsSign", MySqlDbType.Int32, 11) { Value = DBNull.Value } : new MySqlParameter("@IsSign", MySqlDbType.Int32, 11) { Value = model.IsSign };
            MySqlParameter parameter27 = new MySqlParameter("@Status", MySqlDbType.Int32, 11) {
                Value = model.Status
            };
            parameterArray1[0x15] = parameter27;
            parameterArray1[0x16] = (model.Note == null) ? new MySqlParameter("@Note", MySqlDbType.VarChar, 0xff) { Value = DBNull.Value } : new MySqlParameter("@Note", MySqlDbType.VarChar, 0xff) { Value = model.Note };
            MySqlParameter parameter30 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[0x17] = parameter30;
            parameterArray1[0x18] = (model.SubFlowGroupID == null) ? new MySqlParameter("@SubFlowGroupID", MySqlDbType.VarChar, 0x7d0) { Value = DBNull.Value } : new MySqlParameter("@SubFlowGroupID", MySqlDbType.VarChar, 0x7d0) { Value = model.SubFlowGroupID };
            parameterArray1[0x19] = !model.OtherType.HasValue ? new MySqlParameter("@OtherType", MySqlDbType.Int32, 11) { Value = DBNull.Value } : new MySqlParameter("@OtherType", MySqlDbType.Int32, 11) { Value = model.OtherType };
            parameterArray1[0x1a] = (model.Files == null) ? new MySqlParameter("@Files", MySqlDbType.VarChar, 0xfa0) { Value = DBNull.Value } : new MySqlParameter("@Files", MySqlDbType.VarChar, 0xfa0) { Value = model.Files };
            MySqlParameter parameter37 = new MySqlParameter("@IsExpiredAutoSubmit", MySqlDbType.Int32) {
                Value = model.IsExpiredAutoSubmit
            };
            parameterArray1[0x1b] = parameter37;
            MySqlParameter parameter38 = new MySqlParameter("@StepSort", MySqlDbType.Int32) {
                Value = model.StepSort
            };
            parameterArray1[0x1c] = parameter38;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int Completed(Guid taskID, [Optional, DefaultParameterValue("")] string comment, [Optional, DefaultParameterValue(false)] bool isSign, [Optional, DefaultParameterValue(2)] int status, [Optional, DefaultParameterValue("")] string note, [Optional, DefaultParameterValue("")] string files)
        {
            string sql = "UPDATE WorkFlowTask SET Comment=@Comment,CompletedTime1=@CompletedTime1,IsSign=@IsSign,Status=@Status,Note=@Note,Files=@Files WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[7];
            parameterArray1[0] = comment.IsNullOrEmpty() ? new MySqlParameter("@Comment", MySqlDbType.VarChar) { Value = DBNull.Value } : new MySqlParameter("@Comment", MySqlDbType.VarChar) { Value = comment };
            MySqlParameter parameter3 = new MySqlParameter("@CompletedTime1", MySqlDbType.DateTime) {
                Value = DateTimeNew.Now
            };
            parameterArray1[1] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@IsSign", MySqlDbType.Int32) {
                Value = isSign ? 1 : 0
            };
            parameterArray1[2] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@Status", MySqlDbType.Int32) {
                Value = status
            };
            parameterArray1[3] = parameter5;
            parameterArray1[4] = note.IsNullOrEmpty() ? new MySqlParameter("@Note", MySqlDbType.VarChar) { Value = DBNull.Value } : new MySqlParameter("@Note", MySqlDbType.VarChar) { Value = note };
            parameterArray1[5] = files.IsNullOrEmpty() ? new MySqlParameter("@Files", MySqlDbType.VarChar) { Value = DBNull.Value } : new MySqlParameter("@Files", MySqlDbType.VarChar) { Value = files };
            MySqlParameter parameter10 = new MySqlParameter("@ID", MySqlDbType.VarChar) {
                Value = taskID.ToString()
            };
            parameterArray1[6] = parameter10;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.WorkFlowTask> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.WorkFlowTask> list = new List<YJ.Data.Model.WorkFlowTask>();
            YJ.Data.Model.WorkFlowTask item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.WorkFlowTask {
                    ID = dataReader.GetString(0).ToGuid(),
                    PrevID = dataReader.GetString(1).ToGuid(),
                    PrevStepID = dataReader.GetString(2).ToGuid(),
                    FlowID = dataReader.GetString(3).ToGuid(),
                    StepID = dataReader.GetString(4).ToGuid(),
                    StepName = dataReader.GetString(5),
                    InstanceID = dataReader.GetString(6),
                    GroupID = dataReader.GetString(7).ToGuid(),
                    Type = dataReader.GetInt32(8),
                    Title = dataReader.GetString(9),
                    SenderID = dataReader.GetString(10).ToGuid(),
                    SenderName = dataReader.GetString(11),
                    SenderTime = dataReader.GetDateTime(12),
                    ReceiveID = dataReader.GetString(13).ToGuid(),
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
            string sql = "DELETE FROM workflowtask WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int Delete(Guid flowID, Guid groupID)
        {
            string sql = "DELETE FROM WorkFlowTask WHERE GroupID=@GroupID";
            List<MySqlParameter> list1 = new List<MySqlParameter>();
            MySqlParameter item = new MySqlParameter("@GroupID", MySqlDbType.VarChar) {
                Value = groupID.ToString()
            };
            list1.Add(item);
            List<MySqlParameter> list = list1;
            if (!flowID.IsEmptyGuid())
            {
                sql = sql + " AND FlowID=@FlowID";
                MySqlParameter parameter2 = new MySqlParameter("@FlowID", MySqlDbType.VarChar) {
                    Value = flowID.ToString()
                };
                list.Add(parameter2);
            }
            return this.dbHelper.Execute(sql, list.ToArray(), false);
        }

        public int DeleteTempTasks(Guid flowID, Guid stepID, Guid groupID, Guid prevStepID)
        {
            string sql = "DELETE WorkFlowTask WHERE FlowID=@FlowID AND StepID=@StepID AND GroupID=@GroupID AND Status=-1";
            List<MySqlParameter> list1 = new List<MySqlParameter>();
            MySqlParameter item = new MySqlParameter("@FlowID", MySqlDbType.VarChar) {
                Value = flowID.ToString()
            };
            list1.Add(item);
            MySqlParameter parameter2 = new MySqlParameter("@StepID", MySqlDbType.VarChar) {
                Value = stepID.ToString()
            };
            list1.Add(parameter2);
            MySqlParameter parameter3 = new MySqlParameter("@GroupID", MySqlDbType.VarChar) {
                Value = groupID.ToString()
            };
            list1.Add(parameter3);
            List<MySqlParameter> list = list1;
            if (!prevStepID.IsEmptyGuid())
            {
                sql = sql + " AND PrevStepID=@PrevStepID";
                MySqlParameter parameter4 = new MySqlParameter("@PrevStepID", MySqlDbType.VarChar) {
                    Value = prevStepID.ToString()
                };
                list.Add(parameter4);
            }
            return this.dbHelper.Execute(sql, list.ToArray(), false);
        }

        public YJ.Data.Model.WorkFlowTask Get(Guid id)
        {
            string sql = "SELECT * FROM workflowtask WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkFlowTask> GetAll()
        {
            string sql = "SELECT * FROM workflowtask";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkFlowTask> GetBySubFlowGroupID(Guid subflowGroupID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE INSTR(SubFlowGroupID,@SubFlowGroupID)>0";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@SubFlowGroupID", MySqlDbType.VarChar) {
                Value = subflowGroupID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM workflowtask";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public List<YJ.Data.Model.WorkFlowTask> GetExpiredAutoSubmitTasks()
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE CompletedTime<'" + DateTimeNew.Now.ToDateTimeStringS() + "' AND IsExpiredAutoSubmit=1 AND Status IN(0,1)";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public Guid GetFirstSnderID(Guid flowID, Guid groupID)
        {
            string sql = "SELECT SenderID FROM WorkFlowTask WHERE FlowID=@FlowID AND GroupID=@GroupID AND PrevID=@PrevID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[3];
            MySqlParameter parameter1 = new MySqlParameter("@FlowID", MySqlDbType.VarChar) {
                Value = flowID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@GroupID", MySqlDbType.VarChar) {
                Value = groupID.ToString()
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@PrevID", MySqlDbType.VarChar) {
                Value = Guid.Empty.ToString()
            };
            parameterArray1[2] = parameter3;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.GetFieldValue(sql, parameter).ToGuid();
        }

        public List<YJ.Data.Model.WorkFlowTask> GetInstances(Guid[] flowID, Guid[] senderID, Guid[] receiveID, out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string flowid, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(0)] int status)
        {
            long num;
            List<MySqlParameter> list = new List<MySqlParameter>();
            StringBuilder builder = new StringBuilder("SELECT a.* FROM WorkFlowTask a\r\n                WHERE a.ID=(SELECT ID FROM WorkFlowTask WHERE FlowID=a.FlowID AND GroupID=a.GroupID ORDER BY Sort DESC LIMIT 0,1)");
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
                    MySqlParameter item = new MySqlParameter("@SenderID", MySqlDbType.VarChar) {
                        Value = senderID[0].ToString()
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
                    MySqlParameter parameter2 = new MySqlParameter("@ReceiveID", MySqlDbType.VarChar) {
                        Value = receiveID[0].ToString()
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
                builder.Append(" AND INSTR(a.Title,@Title)>0");
                MySqlParameter parameter3 = new MySqlParameter("@Title", MySqlDbType.VarChar, 0x7d0) {
                    Value = title
                };
                list.Add(parameter3);
            }
            if (flowid.IsGuid())
            {
                builder.Append(" AND a.FlowID=@FlowID");
                MySqlParameter parameter4 = new MySqlParameter("@FlowID", MySqlDbType.VarChar) {
                    Value = flowid
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
                MySqlParameter parameter5 = new MySqlParameter("@SenderTime", MySqlDbType.DateTime) {
                    Value = date1.ToDateTime().Date
                };
                list.Add(parameter5);
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND a.SenderTime<=@SenderTime1");
                MySqlParameter parameter6 = new MySqlParameter("@SenderTime1", MySqlDbType.DateTime) {
                    Value = date2.ToDateTime().AddDays(1.0).Date
                };
                list.Add(parameter6);
            }
            builder.Append(" ORDER BY a.SenderTime DESC");
            int pageSize = Tools.GetPageSize();
            int pageNumber = Tools.GetPageNumber();
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), pageSize, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, pageSize, pageNumber, query);
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.WorkFlowTask> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public DataTable GetInstances1(Guid[] flowID, Guid[] senderID, Guid[] receiveID, out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string flowid, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(0)] int status)
        {
            long num;
            List<MySqlParameter> list = new List<MySqlParameter>();
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
                    MySqlParameter item = new MySqlParameter("@SenderID", MySqlDbType.VarChar) {
                        Value = senderID[0].ToString()
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
                if (senderID.Length == 1)
                {
                    builder.Append(" AND ReceiveID=@ReceiveID");
                    MySqlParameter parameter2 = new MySqlParameter("@ReceiveID", MySqlDbType.VarChar) {
                        Value = receiveID[0].ToString()
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
                builder.Append(" AND INSTR(Title,@Title)>0");
                MySqlParameter parameter3 = new MySqlParameter("@Title", MySqlDbType.VarChar, 0x7d0) {
                    Value = title
                };
                list.Add(parameter3);
            }
            if (flowid.IsGuid())
            {
                builder.Append(" AND FlowID=@FlowID");
                MySqlParameter parameter4 = new MySqlParameter("@FlowID", MySqlDbType.VarChar) {
                    Value = flowid
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
                MySqlParameter parameter5 = new MySqlParameter("@SenderTime", MySqlDbType.DateTime) {
                    Value = date1.ToDateTime().Date
                };
                list.Add(parameter5);
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND SenderTime<=@SenderTime1");
                MySqlParameter parameter6 = new MySqlParameter("@SenderTime1", MySqlDbType.DateTime) {
                    Value = date2.ToDateTime().AddDays(1.0).Date
                };
                list.Add(parameter6);
            }
            string sql = string.Format("select * from(\r\nselect flowid,groupid,MAX(SenderTime) SenderTime from WorkFlowTask WHERE 1=1 {0} group by FlowID, GroupID\r\n) temp ORDER BY SenderTime DESC", builder.ToString());
            int pageSize = Tools.GetPageSize();
            int pageNumber = Tools.GetPageNumber();
            string str2 = this.dbHelper.GetPaerSql(sql, pageSize, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, pageSize, pageNumber, query);
            return this.dbHelper.GetDataTable(str2, list.ToArray());
        }

        public DataTable GetInstances1(Guid[] flowID, Guid[] senderID, Guid[] receiveID, out long count, int size, int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string flowid, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(0)] int status, [Optional, DefaultParameterValue("")] string order)
        {
            List<MySqlParameter> list = new List<MySqlParameter>();
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
                    MySqlParameter item = new MySqlParameter("@SenderID", MySqlDbType.VarChar) {
                        Value = senderID[0].ToString()
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
                if (senderID.Length == 1)
                {
                    builder.Append(" AND ReceiveID=@ReceiveID");
                    MySqlParameter parameter2 = new MySqlParameter("@ReceiveID", MySqlDbType.VarChar) {
                        Value = receiveID[0].ToString()
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
                builder.Append(" AND INSTR(Title,@Title)>0");
                MySqlParameter parameter3 = new MySqlParameter("@Title", MySqlDbType.VarChar, 0x7d0) {
                    Value = title
                };
                list.Add(parameter3);
            }
            if (flowid.IsGuid())
            {
                builder.Append(" AND FlowID=@FlowID");
                MySqlParameter parameter4 = new MySqlParameter("@FlowID", MySqlDbType.VarChar) {
                    Value = flowid
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
                MySqlParameter parameter5 = new MySqlParameter("@SenderTime", MySqlDbType.DateTime) {
                    Value = date1.ToDateTime().Date
                };
                list.Add(parameter5);
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND SenderTime<=@SenderTime1");
                MySqlParameter parameter6 = new MySqlParameter("@SenderTime1", MySqlDbType.DateTime) {
                    Value = date2.ToDateTime().AddDays(1.0).Date
                };
                list.Add(parameter6);
            }
            string sql = string.Format("select * from(\r\nselect flowid,groupid,MAX(SenderTime) SenderTime from WorkFlowTask WHERE 1=1 {0} group by FlowID, GroupID\r\n) temp ORDER BY " + (order.IsNullOrEmpty() ? "SenderTime DESC" : order), builder.ToString());
            string str2 = this.dbHelper.GetPaerSql(sql, size, number, out count, list.ToArray());
            return this.dbHelper.GetDataTable(str2, list.ToArray());
        }

        public YJ.Data.Model.WorkFlowTask GetLastTask(Guid flowID, Guid groupID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE FlowID=@FlowID AND GroupID=@GroupID ORDER BY Sort DESC LIMIT 0,1";
            MySqlParameter[] parameterArray1 = new MySqlParameter[2];
            MySqlParameter parameter1 = new MySqlParameter("@FlowID", MySqlDbType.VarChar) {
                Value = flowID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@GroupID", MySqlDbType.VarChar) {
                Value = groupID.ToString()
            };
            parameterArray1[1] = parameter2;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkFlowTask> GetNextTaskList(Guid taskID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE PrevID=@PrevID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@PrevID", MySqlDbType.VarChar) {
                Value = taskID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<Guid> GetPrevSnderID(Guid flowID, Guid stepID, Guid groupID)
        {
            string sql = "SELECT ReceiveID FROM WorkFlowTask WHERE ID=(SELECT PrevID FROM WorkFlowTask WHERE FlowID=@FlowID AND StepID=@StepID AND GroupID=@GroupID)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[3];
            MySqlParameter parameter1 = new MySqlParameter("@FlowID", MySqlDbType.VarChar) {
                Value = flowID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@StepID", MySqlDbType.VarChar) {
                Value = stepID.ToString()
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@GroupID", MySqlDbType.VarChar) {
                Value = groupID.ToString()
            };
            parameterArray1[2] = parameter3;
            MySqlParameter[] parameter = parameterArray1;
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

        public List<YJ.Data.Model.WorkFlowTask> GetPrevTaskList(Guid taskID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE ID=(SELECT PrevID FROM WorkFlowTask WHERE ID=@ID)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar) {
                Value = taskID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<Guid> GetStepSnderID(Guid flowID, Guid stepID, Guid groupID)
        {
            string sql = "SELECT ReceiveID, Sort FROM WorkFlowTask WHERE FlowID=@FlowID AND StepID=@StepID AND GroupID=@GroupID AND Sort=(SELECT IFNULL(MAX(Sort),0) FROM WorkFlowTask WHERE FlowID=@FlowID AND StepID=@StepID AND GroupID=@GroupID)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[3];
            MySqlParameter parameter1 = new MySqlParameter("@FlowID", MySqlDbType.VarChar) {
                Value = flowID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@StepID", MySqlDbType.VarChar) {
                Value = stepID.ToString()
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@GroupID", MySqlDbType.VarChar) {
                Value = groupID.ToString()
            };
            parameterArray1[2] = parameter3;
            MySqlParameter[] parameter = parameterArray1;
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
            MySqlParameter[] parameterArray1 = new MySqlParameter[4];
            MySqlParameter parameter1 = new MySqlParameter("@FlowID", MySqlDbType.VarChar) {
                Value = task.FlowID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@GroupID", MySqlDbType.VarChar) {
                Value = task.GroupID.ToString()
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@PrevID", MySqlDbType.VarChar) {
                Value = task.PrevID.ToString()
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@StepID", MySqlDbType.VarChar) {
                Value = isStepID ? task.StepID.ToString() : task.PrevStepID.ToString()
            };
            parameterArray1[3] = parameter4;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkFlowTask> GetTaskList(Guid flowID, Guid groupID)
        {
            MySqlParameter[] parameterArray;
            string sql = string.Empty;
            if (flowID.IsEmptyGuid())
            {
                sql = "SELECT * FROM WorkFlowTask WHERE GroupID=@GroupID";
                MySqlParameter[] parameterArray1 = new MySqlParameter[1];
                MySqlParameter parameter1 = new MySqlParameter("@GroupID", MySqlDbType.VarChar) {
                    Value = groupID.ToString()
                };
                parameterArray1[0] = parameter1;
                parameterArray = parameterArray1;
            }
            else
            {
                sql = "SELECT * FROM WorkFlowTask WHERE FlowID=@FlowID AND GroupID=@GroupID";
                MySqlParameter[] parameterArray2 = new MySqlParameter[2];
                MySqlParameter parameter2 = new MySqlParameter("@FlowID", MySqlDbType.VarChar) {
                    Value = flowID.ToString()
                };
                parameterArray2[0] = parameter2;
                MySqlParameter parameter3 = new MySqlParameter("@GroupID", MySqlDbType.VarChar) {
                    Value = groupID.ToString()
                };
                parameterArray2[1] = parameter3;
                parameterArray = parameterArray2;
            }
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameterArray);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkFlowTask> GetTaskList(Guid flowID, Guid stepID, Guid groupID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE StepID=@StepID AND GroupID=@GroupID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[2];
            MySqlParameter parameter1 = new MySqlParameter("@StepID", MySqlDbType.VarChar) {
                Value = stepID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@GroupID", MySqlDbType.VarChar) {
                Value = groupID.ToString()
            };
            parameterArray1[1] = parameter2;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkFlowTask> GetTasks(Guid userID, out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string flowid, [Optional, DefaultParameterValue("")] string sender, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(0)] int type)
        {
            long num;
            List<MySqlParameter> list = new List<MySqlParameter>();
            StringBuilder builder = new StringBuilder("SELECT * FROM WorkFlowTask WHERE ReceiveID=@ReceiveID");
            builder.Append((type == 0) ? " AND Status IN(0,1)" : " AND Status IN(2,3,4,5)");
            MySqlParameter item = new MySqlParameter("@ReceiveID", MySqlDbType.VarChar) {
                Value = userID.ToString()
            };
            list.Add(item);
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND INSTR(Title,@Title)>0");
                MySqlParameter parameter2 = new MySqlParameter("@Title", MySqlDbType.VarChar, 0x7d0) {
                    Value = title
                };
                list.Add(parameter2);
            }
            if (flowid.IsGuid())
            {
                builder.Append(" AND FlowID=@FlowID");
                MySqlParameter parameter3 = new MySqlParameter("@FlowID", MySqlDbType.VarChar) {
                    Value = flowid
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
                MySqlParameter parameter4 = new MySqlParameter("@SenderID", MySqlDbType.VarChar) {
                    Value = sender
                };
                list.Add(parameter4);
            }
            if (date1.IsDateTime())
            {
                builder.Append(" AND ReceiveTime>=@ReceiveTime");
                MySqlParameter parameter5 = new MySqlParameter("@ReceiveTime", MySqlDbType.DateTime) {
                    Value = date1.ToDateTime().Date
                };
                list.Add(parameter5);
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND SenderTime<=@ReceiveTime1");
                MySqlParameter parameter6 = new MySqlParameter("@ReceiveTime1", MySqlDbType.DateTime) {
                    Value = date2.ToDateTime().AddDays(1.0).Date
                };
                list.Add(parameter6);
            }
            builder.Append(" ORDER BY " + ((type == 0) ? "SenderTime DESC" : "CompletedTime1 DESC"));
            int pageSize = Tools.GetPageSize();
            int pageNumber = Tools.GetPageNumber();
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), pageSize, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, pageSize, pageNumber, query);
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.WorkFlowTask> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public List<YJ.Data.Model.WorkFlowTask> GetTasks(Guid userID, out long count, int size, int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string flowid, [Optional, DefaultParameterValue("")] string sender, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(0)] int type, [Optional, DefaultParameterValue("")] string order)
        {
            List<MySqlParameter> list = new List<MySqlParameter>();
            StringBuilder builder = new StringBuilder("SELECT * FROM WorkFlowTask WHERE ReceiveID=@ReceiveID");
            builder.Append((type == 0) ? " AND Status IN(0,1)" : " AND Status IN(2,3,4,5)");
            MySqlParameter item = new MySqlParameter("@ReceiveID", MySqlDbType.VarChar) {
                Value = userID.ToString()
            };
            list.Add(item);
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND INSTR(Title,@Title)>0");
                MySqlParameter parameter2 = new MySqlParameter("@Title", MySqlDbType.VarChar, 0x7d0) {
                    Value = title
                };
                list.Add(parameter2);
            }
            if (flowid.IsGuid())
            {
                builder.Append(" AND FlowID=@FlowID");
                MySqlParameter parameter3 = new MySqlParameter("@FlowID", MySqlDbType.VarChar) {
                    Value = flowid
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
                MySqlParameter parameter4 = new MySqlParameter("@SenderID", MySqlDbType.VarChar) {
                    Value = sender
                };
                list.Add(parameter4);
            }
            if (date1.IsDateTime())
            {
                builder.Append(" AND ReceiveTime>=@ReceiveTime");
                MySqlParameter parameter5 = new MySqlParameter("@ReceiveTime", MySqlDbType.DateTime) {
                    Value = date1.ToDateTime().Date
                };
                list.Add(parameter5);
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND SenderTime<=@ReceiveTime1");
                MySqlParameter parameter6 = new MySqlParameter("@ReceiveTime1", MySqlDbType.DateTime) {
                    Value = date2.ToDateTime().AddDays(1.0).Date
                };
                list.Add(parameter6);
            }
            builder.Append("ORDER BY " + (order.IsNullOrEmpty() ? ((type == 0) ? "SenderTime DESC" : "CompletedTime1 DESC") : order));
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), size, number, out count, list.ToArray());
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.WorkFlowTask> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public int GetTaskStatus(Guid taskID)
        {
            int num;
            string sql = "SELECT Status FROM WorkFlowTask WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar) {
                Value = taskID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return (this.dbHelper.GetFieldValue(sql, parameter).IsInt(out num) ? num : -1);
        }

        public List<YJ.Data.Model.WorkFlowTask> GetUserTaskList(Guid flowID, Guid stepID, Guid groupID, Guid userID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE StepID=@StepID AND GroupID=@GroupID AND ReceiveID=@ReceiveID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[3];
            MySqlParameter parameter1 = new MySqlParameter("@StepID", MySqlDbType.VarChar) {
                Value = stepID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@GroupID", MySqlDbType.VarChar) {
                Value = groupID.ToString()
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@ReceiveID", MySqlDbType.VarChar) {
                Value = userID.ToString()
            };
            parameterArray1[2] = parameter3;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public bool HasNoCompletedTasks(Guid flowID, Guid stepID, Guid groupID, Guid userID)
        {
            string sql = "SELECT ID FROM WorkFlowTask WHERE FlowID=@FlowID AND StepID=@StepID AND GroupID=@GroupID AND ReceiveID=@ReceiveID AND Status IN(-1,0,1) LIMIT 0,1";
            MySqlParameter[] parameterArray1 = new MySqlParameter[4];
            MySqlParameter parameter1 = new MySqlParameter("@FlowID", MySqlDbType.VarChar) {
                Value = flowID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@StepID", MySqlDbType.VarChar) {
                Value = stepID.ToString()
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@GroupID", MySqlDbType.VarChar) {
                Value = groupID.ToString()
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@ReceiveID", MySqlDbType.VarChar) {
                Value = userID.ToString()
            };
            parameterArray1[3] = parameter4;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            bool hasRows = dataReader.HasRows;
            dataReader.Close();
            return hasRows;
        }

        public bool HasTasks(Guid flowID)
        {
            string sql = "SELECT ID FROM WorkFlowTask WHERE FlowID=@FlowID LIMIT 0,1";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@FlowID", MySqlDbType.VarChar) {
                Value = flowID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            bool hasRows = dataReader.HasRows;
            dataReader.Close();
            return hasRows;
        }

        public int Update(YJ.Data.Model.WorkFlowTask model)
        {
            string sql = "UPDATE workflowtask SET \r\n\t\t\t\tPrevID=@PrevID,PrevStepID=@PrevStepID,FlowID=@FlowID,StepID=@StepID,StepName=@StepName,InstanceID=@InstanceID,GroupID=@GroupID,Type=@Type,Title=@Title,SenderID=@SenderID,SenderName=@SenderName,SenderTime=@SenderTime,ReceiveID=@ReceiveID,ReceiveName=@ReceiveName,ReceiveTime=@ReceiveTime,OpenTime=@OpenTime,CompletedTime=@CompletedTime,CompletedTime1=@CompletedTime1,Comment=@Comment,IsSign=@IsSign,Status=@Status,Note=@Note,Sort=@Sort,SubFlowGroupID=@SubFlowGroupID,OtherType=@OtherType,Files=@Files,IsExpiredAutoSubmit=@IsExpiredAutoSubmit,StepSort=@StepSort  \r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[0x1d];
            MySqlParameter parameter1 = new MySqlParameter("@PrevID", MySqlDbType.VarChar, 0x24) {
                Value = model.PrevID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@PrevStepID", MySqlDbType.VarChar, 0x24) {
                Value = model.PrevStepID
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@FlowID", MySqlDbType.VarChar, 0x24) {
                Value = model.FlowID
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@StepID", MySqlDbType.VarChar, 0x24) {
                Value = model.StepID
            };
            parameterArray1[3] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@StepName", MySqlDbType.VarChar, 0xff) {
                Value = model.StepName
            };
            parameterArray1[4] = parameter5;
            MySqlParameter parameter6 = new MySqlParameter("@InstanceID", MySqlDbType.VarChar, 50) {
                Value = model.InstanceID
            };
            parameterArray1[5] = parameter6;
            MySqlParameter parameter7 = new MySqlParameter("@GroupID", MySqlDbType.VarChar, 0x24) {
                Value = model.GroupID
            };
            parameterArray1[6] = parameter7;
            MySqlParameter parameter8 = new MySqlParameter("@Type", MySqlDbType.Int32, 11) {
                Value = model.Type
            };
            parameterArray1[7] = parameter8;
            MySqlParameter parameter9 = new MySqlParameter("@Title", MySqlDbType.VarChar, 0xff) {
                Value = model.Title
            };
            parameterArray1[8] = parameter9;
            MySqlParameter parameter10 = new MySqlParameter("@SenderID", MySqlDbType.VarChar, 0x24) {
                Value = model.SenderID
            };
            parameterArray1[9] = parameter10;
            MySqlParameter parameter11 = new MySqlParameter("@SenderName", MySqlDbType.VarChar, 50) {
                Value = model.SenderName
            };
            parameterArray1[10] = parameter11;
            MySqlParameter parameter12 = new MySqlParameter("@SenderTime", MySqlDbType.DateTime, -1) {
                Value = model.SenderTime
            };
            parameterArray1[11] = parameter12;
            MySqlParameter parameter13 = new MySqlParameter("@ReceiveID", MySqlDbType.VarChar, 0x24) {
                Value = model.ReceiveID
            };
            parameterArray1[12] = parameter13;
            MySqlParameter parameter14 = new MySqlParameter("@ReceiveName", MySqlDbType.VarChar, 50) {
                Value = model.ReceiveName
            };
            parameterArray1[13] = parameter14;
            MySqlParameter parameter15 = new MySqlParameter("@ReceiveTime", MySqlDbType.DateTime, -1) {
                Value = model.ReceiveTime
            };
            parameterArray1[14] = parameter15;
            parameterArray1[15] = !model.OpenTime.HasValue ? new MySqlParameter("@OpenTime", MySqlDbType.DateTime, -1) { Value = DBNull.Value } : new MySqlParameter("@OpenTime", MySqlDbType.DateTime, -1) { Value = model.OpenTime };
            parameterArray1[0x10] = !model.CompletedTime.HasValue ? new MySqlParameter("@CompletedTime", MySqlDbType.DateTime, -1) { Value = DBNull.Value } : new MySqlParameter("@CompletedTime", MySqlDbType.DateTime, -1) { Value = model.CompletedTime };
            parameterArray1[0x11] = !model.CompletedTime1.HasValue ? new MySqlParameter("@CompletedTime1", MySqlDbType.DateTime, -1) { Value = DBNull.Value } : new MySqlParameter("@CompletedTime1", MySqlDbType.DateTime, -1) { Value = model.CompletedTime1 };
            parameterArray1[0x12] = (model.Comment == null) ? new MySqlParameter("@Comment", MySqlDbType.VarChar, 0xff) { Value = DBNull.Value } : new MySqlParameter("@Comment", MySqlDbType.VarChar, 0xff) { Value = model.Comment };
            parameterArray1[0x13] = !model.IsSign.HasValue ? new MySqlParameter("@IsSign", MySqlDbType.Int32, 11) { Value = DBNull.Value } : new MySqlParameter("@IsSign", MySqlDbType.Int32, 11) { Value = model.IsSign };
            MySqlParameter parameter26 = new MySqlParameter("@Status", MySqlDbType.Int32, 11) {
                Value = model.Status
            };
            parameterArray1[20] = parameter26;
            parameterArray1[0x15] = (model.Note == null) ? new MySqlParameter("@Note", MySqlDbType.VarChar, 0xff) { Value = DBNull.Value } : new MySqlParameter("@Note", MySqlDbType.VarChar, 0xff) { Value = model.Note };
            MySqlParameter parameter29 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[0x16] = parameter29;
            parameterArray1[0x17] = (model.SubFlowGroupID == null) ? new MySqlParameter("@SubFlowGroupID", MySqlDbType.VarChar, 0x7d0) { Value = DBNull.Value } : new MySqlParameter("@SubFlowGroupID", MySqlDbType.VarChar, 0x7d0) { Value = model.SubFlowGroupID };
            parameterArray1[0x18] = !model.OtherType.HasValue ? new MySqlParameter("@OtherType", MySqlDbType.Int32, 11) { Value = DBNull.Value } : new MySqlParameter("@OtherType", MySqlDbType.Int32, 11) { Value = model.OtherType };
            parameterArray1[0x19] = (model.Files == null) ? new MySqlParameter("@Files", MySqlDbType.VarChar, 0xfa0) { Value = DBNull.Value } : new MySqlParameter("@Files", MySqlDbType.VarChar, 0xfa0) { Value = model.Files };
            MySqlParameter parameter36 = new MySqlParameter("@IsExpiredAutoSubmit", MySqlDbType.Int32) {
                Value = model.IsExpiredAutoSubmit
            };
            parameterArray1[0x1a] = parameter36;
            MySqlParameter parameter37 = new MySqlParameter("@StepSort", MySqlDbType.Int32) {
                Value = model.StepSort
            };
            parameterArray1[0x1b] = parameter37;
            MySqlParameter parameter38 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0x1c] = parameter38;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int UpdateNextTaskStatus(Guid taskID, int status)
        {
            string sql = "UPDATE WorkFlowTask SET Status=@Status WHERE PrevID=@PrevID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[2];
            MySqlParameter parameter1 = new MySqlParameter("@Status", MySqlDbType.Int32) {
                Value = status
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@PrevID", MySqlDbType.VarChar) {
                Value = taskID.ToString()
            };
            parameterArray1[1] = parameter2;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public void UpdateOpenTime(Guid id, DateTime openTime, [Optional, DefaultParameterValue(false)] bool isStatus)
        {
            string sql = "UPDATE WorkFlowTask SET OpenTime=@OpenTime " + (isStatus ? ", Status=1" : "") + " WHERE ID=@ID AND OpenTime IS NULL";
            MySqlParameter[] parameterArray1 = new MySqlParameter[2];
            parameterArray1[0] = (openTime == DateTime.MinValue) ? new MySqlParameter("@OpenTime", MySqlDbType.DateTime) { Value = DBNull.Value } : new MySqlParameter("@OpenTime", MySqlDbType.DateTime) { Value = openTime };
            MySqlParameter parameter3 = new MySqlParameter("@ID", MySqlDbType.VarChar) {
                Value = id.ToString()
            };
            parameterArray1[1] = parameter3;
            MySqlParameter[] parameter = parameterArray1;
            this.dbHelper.Execute(sql, parameter, false);
        }

        public int UpdateTempTasks(Guid flowID, Guid stepID, Guid groupID, DateTime? completedTime, DateTime receiveTime)
        {
            string sql = "UPDATE WorkFlowTask SET CompletedTime=@CompletedTime,ReceiveTime=@ReceiveTime,SenderTime=@SenderTime,Status=0 WHERE FlowID=@FlowID AND StepID=@StepID AND GroupID=@GroupID AND Status=-1";
            MySqlParameter[] parameterArray1 = new MySqlParameter[6];
            parameterArray1[0] = !completedTime.HasValue ? new MySqlParameter("@CompletedTime", MySqlDbType.DateTime) { Value = DBNull.Value } : new MySqlParameter("@CompletedTime", MySqlDbType.DateTime) { Value = completedTime.Value };
            MySqlParameter parameter3 = new MySqlParameter("@ReceiveTime", MySqlDbType.DateTime) {
                Value = receiveTime
            };
            parameterArray1[1] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@SenderTime", MySqlDbType.DateTime) {
                Value = receiveTime
            };
            parameterArray1[2] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@FlowID", MySqlDbType.VarChar) {
                Value = flowID.ToString()
            };
            parameterArray1[3] = parameter5;
            MySqlParameter parameter6 = new MySqlParameter("@StepID", MySqlDbType.VarChar) {
                Value = stepID.ToString()
            };
            parameterArray1[4] = parameter6;
            MySqlParameter parameter7 = new MySqlParameter("@GroupID", MySqlDbType.VarChar) {
                Value = groupID.ToString()
            };
            parameterArray1[5] = parameter7;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

