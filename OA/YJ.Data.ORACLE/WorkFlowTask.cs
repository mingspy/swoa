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

    public class WorkFlowTask : IWorkFlowTask
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkFlowTask model)
        {
            string sql = "INSERT INTO WorkFlowTask\r\n\t\t\t\t(ID,PrevID,PrevStepID,FlowID,StepID,StepName,InstanceID,GroupID,Type,Title,SenderID,SenderName,SenderTime,ReceiveID,ReceiveName,ReceiveTime,OpenTime,CompletedTime,CompletedTime1,Comment1,IsSign,Status,Note,Sort,SubFlowGroupID,OtherType,Files,IsExpiredAutoSubmit,StepSort) \r\n\t\t\t\tVALUES(:ID,:PrevID,:PrevStepID,:FlowID,:StepID,:StepName,:InstanceID,:GroupID,:Type,:Title,:SenderID,:SenderName,:SenderTime,:ReceiveID,:ReceiveName,:ReceiveTime,:OpenTime,:CompletedTime,:CompletedTime1,:Comment1,:IsSign,:Status,:Note,:Sort,:SubFlowGroupID,:OtherType,:Files,:IsExpiredAutoSubmit,:StepSort)";
            OracleParameter[] parameterArray1 = new OracleParameter[0x1d];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2, 40) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":PrevID", OracleDbType.Varchar2, 40) {
                Value = model.PrevID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":PrevStepID", OracleDbType.Varchar2, 40) {
                Value = model.PrevStepID
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":FlowID", OracleDbType.Varchar2, 40) {
                Value = model.FlowID
            };
            parameterArray1[3] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":StepID", OracleDbType.Varchar2, 40) {
                Value = model.StepID
            };
            parameterArray1[4] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":StepName", OracleDbType.NVarchar2, 0x3e8) {
                Value = model.StepName
            };
            parameterArray1[5] = parameter6;
            OracleParameter parameter7 = new OracleParameter(":InstanceID", OracleDbType.Varchar2, 50) {
                Value = model.InstanceID
            };
            parameterArray1[6] = parameter7;
            OracleParameter parameter8 = new OracleParameter(":GroupID", OracleDbType.Varchar2, 40) {
                Value = model.GroupID
            };
            parameterArray1[7] = parameter8;
            OracleParameter parameter9 = new OracleParameter(":Type", OracleDbType.Int32) {
                Value = model.Type
            };
            parameterArray1[8] = parameter9;
            OracleParameter parameter10 = new OracleParameter(":Title", OracleDbType.NVarchar2, 0xfa0) {
                Value = model.Title
            };
            parameterArray1[9] = parameter10;
            OracleParameter parameter11 = new OracleParameter(":SenderID", OracleDbType.Varchar2, 40) {
                Value = model.SenderID
            };
            parameterArray1[10] = parameter11;
            OracleParameter parameter12 = new OracleParameter(":SenderName", OracleDbType.NVarchar2, 100) {
                Value = model.SenderName
            };
            parameterArray1[11] = parameter12;
            OracleParameter parameter13 = new OracleParameter(":SenderTime", OracleDbType.Date, 8) {
                Value = model.SenderTime
            };
            parameterArray1[12] = parameter13;
            OracleParameter parameter14 = new OracleParameter(":ReceiveID", OracleDbType.Varchar2, 40) {
                Value = model.ReceiveID
            };
            parameterArray1[13] = parameter14;
            OracleParameter parameter15 = new OracleParameter(":ReceiveName", OracleDbType.NVarchar2, 100) {
                Value = model.ReceiveName
            };
            parameterArray1[14] = parameter15;
            OracleParameter parameter16 = new OracleParameter(":ReceiveTime", OracleDbType.Date, 8) {
                Value = model.ReceiveTime
            };
            parameterArray1[15] = parameter16;
            parameterArray1[0x10] = !model.OpenTime.HasValue ? new OracleParameter(":OpenTime", OracleDbType.Date, 8) { Value = DBNull.Value } : new OracleParameter(":OpenTime", OracleDbType.Date, 8) { Value = model.OpenTime };
            parameterArray1[0x11] = !model.CompletedTime.HasValue ? new OracleParameter(":CompletedTime", OracleDbType.Date, 8) { Value = DBNull.Value } : new OracleParameter(":CompletedTime", OracleDbType.Date, 8) { Value = model.CompletedTime };
            parameterArray1[0x12] = !model.CompletedTime1.HasValue ? new OracleParameter(":CompletedTime1", OracleDbType.Date, 8) { Value = DBNull.Value } : new OracleParameter(":CompletedTime1", OracleDbType.Date, 8) { Value = model.CompletedTime1 };
            parameterArray1[0x13] = (model.Comment == null) ? new OracleParameter(":Comment1", OracleDbType.NVarchar2) { Value = DBNull.Value } : new OracleParameter(":Comment1", OracleDbType.NVarchar2) { Value = model.Comment };
            parameterArray1[20] = !model.IsSign.HasValue ? new OracleParameter(":IsSign", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter(":IsSign", OracleDbType.Int32) { Value = model.IsSign };
            OracleParameter parameter27 = new OracleParameter(":Status", OracleDbType.Int32) {
                Value = model.Status
            };
            parameterArray1[0x15] = parameter27;
            parameterArray1[0x16] = (model.Note == null) ? new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = model.Note };
            OracleParameter parameter30 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[0x17] = parameter30;
            parameterArray1[0x18] = (model.SubFlowGroupID == null) ? new OracleParameter(":SubFlowGroupID", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":SubFlowGroupID", OracleDbType.Varchar2) { Value = model.SubFlowGroupID };
            parameterArray1[0x19] = !model.OtherType.HasValue ? new OracleParameter(":OtherType", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter(":OtherType", OracleDbType.Int32) { Value = model.OtherType };
            parameterArray1[0x1a] = (model.Files == null) ? new OracleParameter(":Files", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Files", OracleDbType.Varchar2) { Value = model.Files };
            OracleParameter parameter37 = new OracleParameter(":IsExpiredAutoSubmit", OracleDbType.Int32) {
                Value = model.IsExpiredAutoSubmit
            };
            parameterArray1[0x1b] = parameter37;
            OracleParameter parameter38 = new OracleParameter(":StepSort", OracleDbType.Int32) {
                Value = model.StepSort
            };
            parameterArray1[0x1c] = parameter38;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public int Completed(Guid taskID, [Optional, DefaultParameterValue("")] string comment, [Optional, DefaultParameterValue(false)] bool isSign, [Optional, DefaultParameterValue(2)] int status, [Optional, DefaultParameterValue("")] string note, [Optional, DefaultParameterValue("")] string files)
        {
            string sql = "UPDATE WorkFlowTask SET Comment1=:Comment1,CompletedTime1=:CompletedTime1,IsSign=:IsSign,Status=:Status,Note=:Note,Files=:Files WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[7];
            parameterArray1[0] = comment.IsNullOrEmpty() ? new OracleParameter(":Comment1", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Comment1", OracleDbType.Varchar2) { Value = comment };
            OracleParameter parameter3 = new OracleParameter(":CompletedTime1", OracleDbType.Date) {
                Value = DateTimeNew.Now
            };
            parameterArray1[1] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":IsSign", OracleDbType.Int32) {
                Value = isSign ? 1 : 0
            };
            parameterArray1[2] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":Status", OracleDbType.Int32) {
                Value = status
            };
            parameterArray1[3] = parameter5;
            parameterArray1[4] = note.IsNullOrEmpty() ? new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = note };
            parameterArray1[5] = files.IsNullOrEmpty() ? new OracleParameter(":Files", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Files", OracleDbType.Varchar2) { Value = files };
            OracleParameter parameter10 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = taskID
            };
            parameterArray1[6] = parameter10;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.WorkFlowTask> DataReaderToList(OracleDataReader dataReader)
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
            string sql = "DELETE FROM WorkFlowTask WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public int Delete(Guid flowID, Guid groupID)
        {
            string sql = "DELETE FROM WorkFlowTask WHERE GroupID=:GroupID";
            List<OracleParameter> list1 = new List<OracleParameter>();
            OracleParameter item = new OracleParameter(":GroupID", OracleDbType.Varchar2) {
                Value = groupID
            };
            list1.Add(item);
            List<OracleParameter> list = list1;
            if (!flowID.IsEmptyGuid())
            {
                sql = sql + " AND FlowID=:FlowID";
                OracleParameter parameter2 = new OracleParameter(":FlowID", OracleDbType.Varchar2) {
                    Value = flowID
                };
                list.Add(parameter2);
            }
            return this.dbHelper.Execute(sql, list.ToArray());
        }

        public int DeleteTempTasks(Guid flowID, Guid stepID, Guid groupID, Guid prevStepID)
        {
            string sql = "DELETE WorkFlowTask WHERE FlowID=:FlowID AND StepID=:StepID AND GroupID=:GroupID AND Status=-1";
            List<OracleParameter> list1 = new List<OracleParameter>();
            OracleParameter item = new OracleParameter(":FlowID", OracleDbType.Varchar2) {
                Value = flowID
            };
            list1.Add(item);
            OracleParameter parameter2 = new OracleParameter(":StepID", OracleDbType.Varchar2) {
                Value = stepID
            };
            list1.Add(parameter2);
            OracleParameter parameter3 = new OracleParameter(":GroupID", OracleDbType.Varchar2) {
                Value = groupID
            };
            list1.Add(parameter3);
            List<OracleParameter> list = list1;
            if (!prevStepID.IsEmptyGuid())
            {
                sql = sql + " AND PrevStepID=:PrevStepID";
                OracleParameter parameter4 = new OracleParameter(":PrevStepID", OracleDbType.Varchar2) {
                    Value = prevStepID
                };
                list.Add(parameter4);
            }
            return this.dbHelper.Execute(sql, list.ToArray());
        }

        public YJ.Data.Model.WorkFlowTask Get(Guid id)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkFlowTask> GetAll()
        {
            string sql = "SELECT * FROM WorkFlowTask";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkFlowTask> GetBySubFlowGroupID(Guid subflowGroupID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE INSTR(SubFlowGroupID,:SubFlowGroupID,1,1)>0";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":SubFlowGroupID", OracleDbType.Varchar2) {
                Value = subflowGroupID.ToString()
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
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
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public Guid GetFirstSnderID(Guid flowID, Guid groupID)
        {
            string sql = "SELECT SenderID FROM WorkFlowTask WHERE FlowID=:FlowID AND GroupID=:GroupID AND PrevID=:PrevID";
            OracleParameter[] parameterArray1 = new OracleParameter[3];
            OracleParameter parameter1 = new OracleParameter(":FlowID", OracleDbType.Varchar2) {
                Value = flowID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":GroupID", OracleDbType.Varchar2) {
                Value = groupID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":PrevID", OracleDbType.Varchar2) {
                Value = Guid.Empty
            };
            parameterArray1[2] = parameter3;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.GetFieldValue(sql, parameter).ToGuid();
        }

        public List<YJ.Data.Model.WorkFlowTask> GetInstances(Guid[] flowID, Guid[] senderID, Guid[] receiveID, out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string flowid, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(0)] int status)
        {
            long num;
            List<OracleParameter> list = new List<OracleParameter>();
            StringBuilder builder = new StringBuilder("SELECT a.*,ROW_NUMBER() OVER(ORDER BY a.SenderTime DESC) PagerAutoRowNumber FROM WorkFlowTask a\r\n                WHERE a.ID=(SELECT ID FROM RF_WORKFLOWTASK WHERE GroupID=a.GroupID AND sort=(select MAX(sort) from RF_WORKFLOWTASK where GroupID=a.GROUPID) AND ROWNUM=1)");
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
                    builder.Append(" AND a.SenderID=:SenderID");
                    OracleParameter item = new OracleParameter(":SenderID", OracleDbType.Varchar2) {
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
                    builder.Append(" AND a.ReceiveID=:ReceiveID");
                    OracleParameter parameter2 = new OracleParameter(":ReceiveID", OracleDbType.Varchar2) {
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
                builder.Append(" AND INSTR(a.Title,:Title,1,1)>0");
                OracleParameter parameter3 = new OracleParameter(":Title", OracleDbType.NVarchar2, 0x7d0) {
                    Value = title
                };
                list.Add(parameter3);
            }
            if (flowid.IsGuid())
            {
                builder.Append(" AND a.FlowID=:FlowID");
                OracleParameter parameter4 = new OracleParameter(":FlowID", OracleDbType.Varchar2) {
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
                builder.Append(" AND a.SenderTime>=:SenderTime");
                OracleParameter parameter5 = new OracleParameter(":SenderTime", OracleDbType.Date) {
                    Value = date1.ToDateTime().Date
                };
                list.Add(parameter5);
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND a.SenderTime<=:SenderTime1");
                OracleParameter parameter6 = new OracleParameter(":SenderTime1", OracleDbType.Date) {
                    Value = date2.ToDateTime().AddDays(1.0).Date
                };
                list.Add(parameter6);
            }
            builder.Append(" AND ROWNUM<=1) ORDER BY Sort DESC) PagerTempTable");
            int pageSize = Tools.GetPageSize();
            int pageNumber = Tools.GetPageNumber();
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), pageSize, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, pageSize, pageNumber, query);
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.WorkFlowTask> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public DataTable GetInstances1(Guid[] flowID, Guid[] senderID, Guid[] receiveID, out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string flowid, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(0)] int status)
        {
            long num;
            List<OracleParameter> list = new List<OracleParameter>();
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
                    builder.Append(" AND SenderID=:SenderID");
                    OracleParameter item = new OracleParameter(":SenderID", OracleDbType.Varchar2) {
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
                if (senderID.Length == 1)
                {
                    builder.Append(" AND ReceiveID=:ReceiveID");
                    OracleParameter parameter2 = new OracleParameter(":ReceiveID", OracleDbType.Varchar2) {
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
                builder.Append(" AND INSTR(Title,:Title,1,1)>0");
                OracleParameter parameter3 = new OracleParameter(":Title", OracleDbType.NVarchar2, 0x7d0) {
                    Value = title
                };
                list.Add(parameter3);
            }
            if (flowid.IsGuid())
            {
                builder.Append(" AND FlowID=:FlowID");
                OracleParameter parameter4 = new OracleParameter(":FlowID", OracleDbType.Varchar2) {
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
                builder.Append(" AND SenderTime>=:SenderTime");
                OracleParameter parameter5 = new OracleParameter(":SenderTime", OracleDbType.Date) {
                    Value = date1.ToDateTime().Date
                };
                list.Add(parameter5);
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND SenderTime<=:SenderTime1");
                OracleParameter parameter6 = new OracleParameter(":SenderTime1", OracleDbType.Date) {
                    Value = date2.ToDateTime().AddDays(1.0).Date
                };
                list.Add(parameter6);
            }
            string sql = string.Format("select PagerTempTable.*,ROWNUM AS PagerAutoRowNumber FROM(\r\nselect flowid,groupid,MAX(SenderTime) SenderTime from WorkFlowTask where 1=1 {0} group by FlowID, GroupID\r\n) PagerTempTable", builder.ToString());
            int pageSize = Tools.GetPageSize();
            int pageNumber = Tools.GetPageNumber();
            string str2 = this.dbHelper.GetPaerSql(sql, pageSize, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, pageSize, pageNumber, query);
            return this.dbHelper.GetDataTable(str2, list.ToArray());
        }

        public DataTable GetInstances1(Guid[] flowID, Guid[] senderID, Guid[] receiveID, out long count, int size, int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string flowid, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(0)] int status, [Optional, DefaultParameterValue("")] string order)
        {
            List<OracleParameter> list = new List<OracleParameter>();
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
                    builder.Append(" AND SenderID=:SenderID");
                    OracleParameter item = new OracleParameter(":SenderID", OracleDbType.Varchar2) {
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
                if (senderID.Length == 1)
                {
                    builder.Append(" AND ReceiveID=:ReceiveID");
                    OracleParameter parameter2 = new OracleParameter(":ReceiveID", OracleDbType.Varchar2) {
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
                builder.Append(" AND INSTR(Title,:Title,1,1)>0");
                OracleParameter parameter3 = new OracleParameter(":Title", OracleDbType.NVarchar2, 0x7d0) {
                    Value = title
                };
                list.Add(parameter3);
            }
            if (flowid.IsGuid())
            {
                builder.Append(" AND FlowID=:FlowID");
                OracleParameter parameter4 = new OracleParameter(":FlowID", OracleDbType.Varchar2) {
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
                builder.Append(" AND SenderTime>=:SenderTime");
                OracleParameter parameter5 = new OracleParameter(":SenderTime", OracleDbType.Date) {
                    Value = date1.ToDateTime().Date
                };
                list.Add(parameter5);
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND SenderTime<=:SenderTime1");
                OracleParameter parameter6 = new OracleParameter(":SenderTime1", OracleDbType.Date) {
                    Value = date2.ToDateTime().AddDays(1.0).Date
                };
                list.Add(parameter6);
            }
            string sql = string.Format("select PagerTempTable.*,ROWNUM AS PagerAutoRowNumber FROM(select flowid,groupid,MAX(SenderTime) SenderTime \r\n            from WorkFlowTask where 1=1 {0} group by FlowID, GroupID ORDER BY " + (order.IsNullOrEmpty() ? "SenderTime DESC" : order) + ") PagerTempTable", builder.ToString());
            string str2 = this.dbHelper.GetPaerSql(sql, size, number, out count, list.ToArray());
            return this.dbHelper.GetDataTable(str2, list.ToArray());
        }

        public YJ.Data.Model.WorkFlowTask GetLastTask(Guid flowID, Guid groupID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE ROWNUM=1 AND FlowID=:FlowID AND GroupID=:GroupID ORDER BY Sort DESC";
            OracleParameter[] parameterArray1 = new OracleParameter[2];
            OracleParameter parameter1 = new OracleParameter(":FlowID", OracleDbType.Varchar2) {
                Value = flowID.ToString()
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":GroupID", OracleDbType.Varchar2) {
                Value = groupID.ToString()
            };
            parameterArray1[1] = parameter2;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkFlowTask> GetNextTaskList(Guid taskID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE PrevID=:PrevID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":PrevID", OracleDbType.Varchar2) {
                Value = taskID
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<Guid> GetPrevSnderID(Guid flowID, Guid stepID, Guid groupID)
        {
            string sql = "SELECT ReceiveID FROM WorkFlowTask WHERE ID=(SELECT PrevID FROM WorkFlowTask WHERE FlowID=:FlowID AND StepID=:StepID AND GroupID=:GroupID)";
            OracleParameter[] parameterArray1 = new OracleParameter[3];
            OracleParameter parameter1 = new OracleParameter(":FlowID", OracleDbType.Varchar2) {
                Value = flowID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":StepID", OracleDbType.Varchar2) {
                Value = stepID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":GroupID", OracleDbType.Varchar2) {
                Value = groupID
            };
            parameterArray1[2] = parameter3;
            OracleParameter[] parameter = parameterArray1;
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
            string sql = "SELECT * FROM WorkFlowTask WHERE ID=(SELECT PrevID FROM WorkFlowTask WHERE ID=:ID)";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = taskID
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<Guid> GetStepSnderID(Guid flowID, Guid stepID, Guid groupID)
        {
            string sql = "SELECT ReceiveID FROM WorkFlowTask WHERE FlowID=:FlowID AND StepID=:StepID AND GroupID=:GroupID AND Sort=(SELECT ISNULL(MAX(Sort),0) FROM WorkFlowTask WHERE FlowID=:FlowID AND StepID=:StepID AND GroupID=:GroupID)";
            OracleParameter[] parameterArray1 = new OracleParameter[3];
            OracleParameter parameter1 = new OracleParameter(":FlowID", OracleDbType.Varchar2) {
                Value = flowID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":StepID", OracleDbType.Varchar2) {
                Value = stepID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":GroupID", OracleDbType.Varchar2) {
                Value = groupID
            };
            parameterArray1[2] = parameter3;
            OracleParameter[] parameter = parameterArray1;
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
            string sql = string.Format("SELECT * FROM WorkFlowTask WHERE FlowID=:FlowID AND GroupID=:GroupID AND PrevID=:PrevID AND {0}", isStepID ? "StepID=:StepID" : "PrevStepID=:StepID");
            OracleParameter[] parameterArray1 = new OracleParameter[4];
            OracleParameter parameter1 = new OracleParameter(":FlowID", OracleDbType.Varchar2) {
                Value = task.FlowID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":GroupID", OracleDbType.Varchar2) {
                Value = task.GroupID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":PrevID", OracleDbType.Varchar2) {
                Value = task.PrevID
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":StepID", OracleDbType.Varchar2) {
                Value = isStepID ? task.StepID : task.PrevStepID
            };
            parameterArray1[3] = parameter4;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkFlowTask> GetTaskList(Guid flowID, Guid groupID)
        {
            OracleParameter[] parameterArray;
            string sql = string.Empty;
            if (flowID.IsEmptyGuid())
            {
                sql = "SELECT * FROM WorkFlowTask WHERE GroupID=:GroupID";
                OracleParameter[] parameterArray1 = new OracleParameter[1];
                OracleParameter parameter1 = new OracleParameter(":GroupID", OracleDbType.Varchar2) {
                    Value = groupID
                };
                parameterArray1[0] = parameter1;
                parameterArray = parameterArray1;
            }
            else
            {
                sql = "SELECT * FROM WorkFlowTask WHERE FlowID=:FlowID AND GroupID=:GroupID";
                OracleParameter[] parameterArray2 = new OracleParameter[2];
                OracleParameter parameter2 = new OracleParameter(":FlowID", OracleDbType.Varchar2) {
                    Value = flowID
                };
                parameterArray2[0] = parameter2;
                OracleParameter parameter3 = new OracleParameter(":GroupID", OracleDbType.Varchar2) {
                    Value = groupID
                };
                parameterArray2[1] = parameter3;
                parameterArray = parameterArray2;
            }
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameterArray);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkFlowTask> GetTaskList(Guid flowID, Guid stepID, Guid groupID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE FlowID=:FlowID AND StepID=:StepID AND GroupID=:GroupID";
            OracleParameter[] parameterArray1 = new OracleParameter[3];
            OracleParameter parameter1 = new OracleParameter(":FlowID", OracleDbType.Varchar2) {
                Value = flowID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":StepID", OracleDbType.Varchar2) {
                Value = stepID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":GroupID", OracleDbType.Varchar2) {
                Value = groupID
            };
            parameterArray1[2] = parameter3;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkFlowTask> GetTasks(Guid userID, out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string flowid, [Optional, DefaultParameterValue("")] string sender, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(0)] int type)
        {
            long num;
            List<OracleParameter> list = new List<OracleParameter>();
            StringBuilder builder = new StringBuilder("SELECT PagerTempTable.*,ROWNUM AS PagerAutoRowNumber FROM(SELECT * FROM WorkFlowTask WHERE ReceiveID=:ReceiveID ");
            builder.Append((type == 0) ? " AND Status IN(0,1)" : " AND Status IN(2,3,4,5)");
            OracleParameter item = new OracleParameter(":ReceiveID", OracleDbType.Varchar2) {
                Value = userID
            };
            list.Add(item);
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND INSTR(Title,:Title,1,1)>0");
                OracleParameter parameter2 = new OracleParameter(":Title", OracleDbType.NVarchar2, 0x7d0) {
                    Value = title
                };
                list.Add(parameter2);
            }
            if (flowid.IsGuid())
            {
                builder.Append(" AND FlowID=:FlowID");
                OracleParameter parameter3 = new OracleParameter(":FlowID", OracleDbType.Varchar2) {
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
                builder.Append(" AND SenderID=:SenderID");
                OracleParameter parameter4 = new OracleParameter(":SenderID", OracleDbType.Varchar2) {
                    Value = sender.ToGuid()
                };
                list.Add(parameter4);
            }
            if (date1.IsDateTime())
            {
                builder.Append(" AND ReceiveTime>=:ReceiveTime");
                OracleParameter parameter5 = new OracleParameter(":ReceiveTime", OracleDbType.Date) {
                    Value = date1.ToDateTime().Date
                };
                list.Add(parameter5);
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND ReceiveTime<=:ReceiveTime1");
                OracleParameter parameter6 = new OracleParameter(":ReceiveTime1", OracleDbType.Date) {
                    Value = date2.ToDateTime().AddDays(1.0).Date
                };
                list.Add(parameter6);
            }
            builder.Append(" ORDER BY " + ((type == 0) ? "ReceiveTime DESC" : "CompletedTime1 DESC") + ") PagerTempTable");
            int pageSize = Tools.GetPageSize();
            int pageNumber = Tools.GetPageNumber();
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), pageSize, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, pageSize, pageNumber, query);
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.WorkFlowTask> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public List<YJ.Data.Model.WorkFlowTask> GetTasks(Guid userID, out long count, int size, int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string flowid, [Optional, DefaultParameterValue("")] string sender, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(0)] int type, [Optional, DefaultParameterValue("")] string order)
        {
            List<OracleParameter> list = new List<OracleParameter>();
            StringBuilder builder = new StringBuilder("SELECT PagerTempTable.*,ROWNUM AS PagerAutoRowNumber FROM(SELECT * FROM WorkFlowTask WHERE ReceiveID=:ReceiveID ");
            builder.Append((type == 0) ? " AND Status IN(0,1)" : " AND Status IN(2,3,4,5)");
            OracleParameter item = new OracleParameter(":ReceiveID", OracleDbType.Varchar2) {
                Value = userID
            };
            list.Add(item);
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND INSTR(Title,:Title,1,1)>0");
                OracleParameter parameter2 = new OracleParameter(":Title", OracleDbType.NVarchar2, 0x7d0) {
                    Value = title
                };
                list.Add(parameter2);
            }
            if (flowid.IsGuid())
            {
                builder.Append(" AND FlowID=:FlowID");
                OracleParameter parameter3 = new OracleParameter(":FlowID", OracleDbType.Varchar2) {
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
                builder.Append(" AND SenderID=:SenderID");
                OracleParameter parameter4 = new OracleParameter(":SenderID", OracleDbType.Varchar2) {
                    Value = sender.ToGuid()
                };
                list.Add(parameter4);
            }
            if (date1.IsDateTime())
            {
                builder.Append(" AND ReceiveTime>=:ReceiveTime");
                OracleParameter parameter5 = new OracleParameter(":ReceiveTime", OracleDbType.Date) {
                    Value = date1.ToDateTime().Date
                };
                list.Add(parameter5);
            }
            if (date2.IsDateTime())
            {
                builder.Append(" AND ReceiveTime<=:ReceiveTime1");
                OracleParameter parameter6 = new OracleParameter(":ReceiveTime1", OracleDbType.Date) {
                    Value = date2.ToDateTime().AddDays(1.0).Date
                };
                list.Add(parameter6);
            }
            builder.Append(" ORDER BY " + (order.IsNullOrEmpty() ? ((type == 0) ? "ReceiveTime DESC" : "CompletedTime1 DESC") : order) + ") PagerTempTable");
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), size, number, out count, list.ToArray());
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.WorkFlowTask> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public int GetTaskStatus(Guid taskID)
        {
            int num;
            string sql = "SELECT Status FROM WorkFlowTask WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = taskID
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return (this.dbHelper.GetFieldValue(sql, parameter).IsInt(out num) ? num : -1);
        }

        public List<YJ.Data.Model.WorkFlowTask> GetUserTaskList(Guid flowID, Guid stepID, Guid groupID, Guid userID)
        {
            string sql = "SELECT * FROM WorkFlowTask WHERE FlowID=:FlowID AND StepID=:StepID AND GroupID=:GroupID AND ReceiveID=:ReceiveID";
            OracleParameter[] parameterArray1 = new OracleParameter[4];
            OracleParameter parameter1 = new OracleParameter(":FlowID", OracleDbType.Varchar2) {
                Value = flowID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":StepID", OracleDbType.Varchar2) {
                Value = stepID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":GroupID", OracleDbType.Varchar2) {
                Value = groupID
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":ReceiveID", OracleDbType.Varchar2) {
                Value = userID
            };
            parameterArray1[3] = parameter4;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowTask> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public bool HasNoCompletedTasks(Guid flowID, Guid stepID, Guid groupID, Guid userID)
        {
            string sql = "SELECT ID FROM WorkFlowTask WHERE FlowID=:FlowID AND StepID=:StepID AND GroupID=:GroupID AND ReceiveID=:ReceiveID AND Status IN(-1,0,1) AND ROWNUM<=1";
            OracleParameter[] parameterArray1 = new OracleParameter[4];
            OracleParameter parameter1 = new OracleParameter(":FlowID", OracleDbType.Varchar2) {
                Value = flowID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":StepID", OracleDbType.Varchar2) {
                Value = stepID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":GroupID", OracleDbType.Varchar2) {
                Value = groupID
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":ReceiveID", OracleDbType.Varchar2) {
                Value = userID
            };
            parameterArray1[3] = parameter4;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            bool hasRows = dataReader.HasRows;
            dataReader.Close();
            return hasRows;
        }

        public bool HasTasks(Guid flowID)
        {
            string sql = "SELECT ID FROM WorkFlowTask WHERE FlowID=:FlowID AND ROWNUM<=1";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":FlowID", OracleDbType.Varchar2) {
                Value = flowID
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            bool hasRows = dataReader.HasRows;
            dataReader.Close();
            return hasRows;
        }

        public int Update(YJ.Data.Model.WorkFlowTask model)
        {
            string sql = "UPDATE WorkFlowTask SET \r\n\t\t\t\tPrevID=:PrevID,PrevStepID=:PrevStepID,FlowID=:FlowID,StepID=:StepID,StepName=:StepName,InstanceID=:InstanceID,GroupID=:GroupID,Type=:Type,Title=:Title,SenderID=:SenderID,SenderName=:SenderName,SenderTime=:SenderTime,ReceiveID=:ReceiveID,ReceiveName=:ReceiveName,ReceiveTime=:ReceiveTime,OpenTime=:OpenTime,CompletedTime=:CompletedTime,CompletedTime1=:CompletedTime1,Comment1=:Comment1,IsSign=:IsSign,Status=:Status,Note=:Note,Sort=:Sort,SubFlowGroupID=:SubFlowGroupID,OtherType=:OtherType,Files=:Files,IsExpiredAutoSubmit=:IsExpiredAutoSubmit,StepSort=:StepSort  \r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[0x1d];
            OracleParameter parameter1 = new OracleParameter(":PrevID", OracleDbType.Varchar2, 40) {
                Value = model.PrevID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":PrevStepID", OracleDbType.Varchar2, 40) {
                Value = model.PrevStepID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":FlowID", OracleDbType.Varchar2, 40) {
                Value = model.FlowID
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":StepID", OracleDbType.Varchar2, 40) {
                Value = model.StepID
            };
            parameterArray1[3] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":StepName", OracleDbType.NVarchar2, 0x3e8) {
                Value = model.StepName
            };
            parameterArray1[4] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":InstanceID", OracleDbType.Varchar2, 50) {
                Value = model.InstanceID
            };
            parameterArray1[5] = parameter6;
            OracleParameter parameter7 = new OracleParameter(":GroupID", OracleDbType.Varchar2, 40) {
                Value = model.GroupID
            };
            parameterArray1[6] = parameter7;
            OracleParameter parameter8 = new OracleParameter(":Type", OracleDbType.Int32) {
                Value = model.Type
            };
            parameterArray1[7] = parameter8;
            OracleParameter parameter9 = new OracleParameter(":Title", OracleDbType.NVarchar2, 0xfa0) {
                Value = model.Title
            };
            parameterArray1[8] = parameter9;
            OracleParameter parameter10 = new OracleParameter(":SenderID", OracleDbType.Varchar2, 40) {
                Value = model.SenderID
            };
            parameterArray1[9] = parameter10;
            OracleParameter parameter11 = new OracleParameter(":SenderName", OracleDbType.NVarchar2, 100) {
                Value = model.SenderName
            };
            parameterArray1[10] = parameter11;
            OracleParameter parameter12 = new OracleParameter(":SenderTime", OracleDbType.Date, 8) {
                Value = model.SenderTime
            };
            parameterArray1[11] = parameter12;
            OracleParameter parameter13 = new OracleParameter(":ReceiveID", OracleDbType.Varchar2, 40) {
                Value = model.ReceiveID
            };
            parameterArray1[12] = parameter13;
            OracleParameter parameter14 = new OracleParameter(":ReceiveName", OracleDbType.NVarchar2, 100) {
                Value = model.ReceiveName
            };
            parameterArray1[13] = parameter14;
            OracleParameter parameter15 = new OracleParameter(":ReceiveTime", OracleDbType.Date, 8) {
                Value = model.ReceiveTime
            };
            parameterArray1[14] = parameter15;
            parameterArray1[15] = !model.OpenTime.HasValue ? new OracleParameter(":OpenTime", OracleDbType.Date, 8) { Value = DBNull.Value } : new OracleParameter(":OpenTime", OracleDbType.Date, 8) { Value = model.OpenTime };
            parameterArray1[0x10] = !model.CompletedTime.HasValue ? new OracleParameter(":CompletedTime", OracleDbType.Date, 8) { Value = DBNull.Value } : new OracleParameter(":CompletedTime", OracleDbType.Date, 8) { Value = model.CompletedTime };
            parameterArray1[0x11] = !model.CompletedTime1.HasValue ? new OracleParameter(":CompletedTime1", OracleDbType.Date, 8) { Value = DBNull.Value } : new OracleParameter(":CompletedTime1", OracleDbType.Date, 8) { Value = model.CompletedTime1 };
            parameterArray1[0x12] = (model.Comment == null) ? new OracleParameter(":Comment1", OracleDbType.NVarchar2) { Value = DBNull.Value } : new OracleParameter(":Comment1", OracleDbType.NVarchar2) { Value = model.Comment };
            parameterArray1[0x13] = !model.IsSign.HasValue ? new OracleParameter(":IsSign", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter(":IsSign", OracleDbType.Int32) { Value = model.IsSign };
            OracleParameter parameter26 = new OracleParameter(":Status", OracleDbType.Int32) {
                Value = model.Status
            };
            parameterArray1[20] = parameter26;
            parameterArray1[0x15] = (model.Note == null) ? new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = model.Note };
            OracleParameter parameter29 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[0x16] = parameter29;
            parameterArray1[0x17] = (model.SubFlowGroupID == null) ? new OracleParameter(":SubFlowGroupID", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":SubFlowGroupID", OracleDbType.Varchar2) { Value = model.SubFlowGroupID };
            parameterArray1[0x18] = !model.OtherType.HasValue ? new OracleParameter(":OtherType", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter(":OtherType", OracleDbType.Int32) { Value = model.OtherType };
            parameterArray1[0x19] = (model.Files == null) ? new OracleParameter(":Files", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Files", OracleDbType.Varchar2) { Value = model.Files };
            OracleParameter parameter36 = new OracleParameter(":IsExpiredAutoSubmit", OracleDbType.Int32) {
                Value = model.IsExpiredAutoSubmit
            };
            parameterArray1[0x1a] = parameter36;
            OracleParameter parameter37 = new OracleParameter(":StepSort", OracleDbType.Int32) {
                Value = model.StepSort
            };
            parameterArray1[0x1b] = parameter37;
            OracleParameter parameter38 = new OracleParameter(":ID", OracleDbType.Varchar2, 40) {
                Value = model.ID
            };
            parameterArray1[0x1c] = parameter38;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public int UpdateNextTaskStatus(Guid taskID, int status)
        {
            string sql = "UPDATE WorkFlowTask SET Status=:Status WHERE PrevID=:PrevID";
            OracleParameter[] parameterArray1 = new OracleParameter[2];
            OracleParameter parameter1 = new OracleParameter(":Status", OracleDbType.Int32) {
                Value = status
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":PrevID", OracleDbType.Varchar2) {
                Value = taskID
            };
            parameterArray1[1] = parameter2;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public void UpdateOpenTime(Guid id, DateTime openTime, [Optional, DefaultParameterValue(false)] bool isStatus)
        {
            string sql = "UPDATE WorkFlowTask SET OpenTime=:OpenTime " + (isStatus ? ", Status=1" : "") + " WHERE ID=:ID AND OpenTime IS NULL";
            OracleParameter[] parameterArray1 = new OracleParameter[2];
            parameterArray1[0] = (openTime == DateTime.MinValue) ? new OracleParameter(":OpenTime", OracleDbType.Date) { Value = DBNull.Value } : new OracleParameter(":OpenTime", OracleDbType.Date) { Value = openTime };
            OracleParameter parameter3 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[1] = parameter3;
            OracleParameter[] parameter = parameterArray1;
            this.dbHelper.Execute(sql, parameter);
        }

        public int UpdateTempTasks(Guid flowID, Guid stepID, Guid groupID, DateTime? completedTime, DateTime receiveTime)
        {
            string sql = "UPDATE WorkFlowTask SET CompletedTime=:CompletedTime,ReceiveTime=:ReceiveTime,SenderTime=:SenderTime,Status=0 WHERE FlowID=:FlowID AND StepID=:StepID AND GroupID=:GroupID AND Status=-1";
            OracleParameter[] parameterArray1 = new OracleParameter[6];
            parameterArray1[0] = !completedTime.HasValue ? new OracleParameter(":CompletedTime", OracleDbType.Date) { Value = DBNull.Value } : new OracleParameter(":CompletedTime", OracleDbType.Date) { Value = completedTime.Value };
            OracleParameter parameter3 = new OracleParameter(":ReceiveTime", OracleDbType.Date) {
                Value = receiveTime
            };
            parameterArray1[1] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":SenderTime", OracleDbType.Date) {
                Value = receiveTime
            };
            parameterArray1[2] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":FlowID", OracleDbType.Varchar2) {
                Value = flowID
            };
            parameterArray1[3] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":StepID", OracleDbType.Varchar2) {
                Value = stepID
            };
            parameterArray1[4] = parameter6;
            OracleParameter parameter7 = new OracleParameter(":GroupID", OracleDbType.Varchar2) {
                Value = groupID
            };
            parameterArray1[5] = parameter7;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

