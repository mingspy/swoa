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

    public class WorkFlowForm : IWorkFlowForm
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkFlowForm model)
        {
            string sql = "INSERT INTO WorkFlowForm\r\n\t\t\t\t(ID,Name,Type,CreateUserID,CreateUserName,CreateTime,LastModifyTime,Html,SubTableJson,EventsJson,Attribute,Status) \r\n\t\t\t\tVALUES(:ID,:Name,:Type,:CreateUserID,:CreateUserName,:CreateTime,:LastModifyTime,:Html,:SubTableJson,:EventsJson,:Attribute,:Status)";
            OracleParameter[] parameterArray1 = new OracleParameter[12];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2, 40) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Name", OracleDbType.NVarchar2, 0x3e8) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":Type", OracleDbType.Varchar2, 40) {
                Value = model.Type
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":CreateUserID", OracleDbType.Varchar2, 40) {
                Value = model.CreateUserID
            };
            parameterArray1[3] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":CreateUserName", OracleDbType.NVarchar2, 100) {
                Value = model.CreateUserName
            };
            parameterArray1[4] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":CreateTime", OracleDbType.Date, 8) {
                Value = model.CreateTime
            };
            parameterArray1[5] = parameter6;
            OracleParameter parameter7 = new OracleParameter(":LastModifyTime", OracleDbType.Date, 8) {
                Value = model.LastModifyTime
            };
            parameterArray1[6] = parameter7;
            parameterArray1[7] = (model.Html == null) ? new OracleParameter(":Html", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Html", OracleDbType.Clob) { Value = model.Html };
            parameterArray1[8] = (model.SubTableJson == null) ? new OracleParameter(":SubTableJson", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":SubTableJson", OracleDbType.Clob) { Value = model.SubTableJson };
            parameterArray1[9] = (model.EventsJson == null) ? new OracleParameter(":EventsJson", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":EventsJson", OracleDbType.Clob) { Value = model.EventsJson };
            parameterArray1[10] = (model.Attribute == null) ? new OracleParameter(":Attribute", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Attribute", OracleDbType.Clob) { Value = model.Attribute };
            OracleParameter parameter16 = new OracleParameter(":Status", OracleDbType.Int32) {
                Value = model.Status
            };
            parameterArray1[11] = parameter16;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.WorkFlowForm> DataReaderToList(OracleDataReader dataReader)
        {
            List<YJ.Data.Model.WorkFlowForm> list = new List<YJ.Data.Model.WorkFlowForm>();
            YJ.Data.Model.WorkFlowForm item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.WorkFlowForm {
                    ID = dataReader.GetString(0).ToGuid(),
                    Name = dataReader.GetString(1),
                    Type = dataReader.GetString(2).ToGuid(),
                    CreateUserID = dataReader.GetString(3).ToGuid(),
                    CreateUserName = dataReader.GetString(4),
                    CreateTime = dataReader.GetDateTime(5),
                    LastModifyTime = dataReader.GetDateTime(6)
                };
                if (!dataReader.IsDBNull(7))
                {
                    item.Html = dataReader.GetString(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    item.SubTableJson = dataReader.GetString(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    item.EventsJson = dataReader.GetString(9);
                }
                if (!dataReader.IsDBNull(10))
                {
                    item.Attribute = dataReader.GetString(10);
                }
                item.Status = dataReader.GetInt32(11);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM WorkFlowForm WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.WorkFlowForm Get(Guid id)
        {
            string sql = "SELECT * FROM WorkFlowForm WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowForm> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkFlowForm> GetAll()
        {
            string sql = "SELECT * FROM WorkFlowForm";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowForm> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkFlowForm> GetAllByType(string types)
        {
            string sql = "SELECT * FROM WorkFlowForm where Type IN(" + Tools.GetSqlInString(types, true, ",") + ")";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowForm> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM WorkFlowForm";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public List<YJ.Data.Model.WorkFlowForm> GetPagerData(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string userid, [Optional, DefaultParameterValue("")] string typeid, [Optional, DefaultParameterValue("")] string name, [Optional, DefaultParameterValue(15)] int pagesize)
        {
            long num;
            StringBuilder builder = new StringBuilder("AND Status IN(0,1) ");
            List<OracleParameter> list = new List<OracleParameter>();
            if (!userid.IsNullOrEmpty())
            {
                builder.Append("AND CreateUserID=:CreateUserID ");
                OracleParameter item = new OracleParameter(":CreateUserID", OracleDbType.Varchar2) {
                    Value = userid
                };
                list.Add(item);
            }
            if (!name.IsNullOrEmpty())
            {
                builder.Append("AND CHARINDEX(:Name,Name)>0 ");
                OracleParameter parameter2 = new OracleParameter(":Name", OracleDbType.Varchar2) {
                    Value = name
                };
                list.Add(parameter2);
            }
            if (!typeid.IsNullOrEmpty())
            {
                builder.Append("AND Type IN (" + Tools.GetSqlInString(typeid, true, ",") + ")");
            }
            int size = pagesize;
            int pageNumber = Tools.GetPageNumber();
            string sql = this.dbHelper.GetPaerSql("WorkFlowForm", "ID,Name,Type,CreateUserID,CreateUserName,CreateTime,LastModifyTime,'' Html,'' SubTableJson,'' EventsJson,'' Attribute,Status", builder.ToString(), "CreateTime DESC", size, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, size, pageNumber, query);
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.WorkFlowForm> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public List<YJ.Data.Model.WorkFlowForm> GetPagerData(out long count, int pageSize, int pageNumber, [Optional, DefaultParameterValue("")] string userid, [Optional, DefaultParameterValue("")] string typeid, [Optional, DefaultParameterValue("")] string name, [Optional, DefaultParameterValue("")] string order)
        {
            StringBuilder builder = new StringBuilder("AND Status IN(0,1) ");
            List<OracleParameter> list = new List<OracleParameter>();
            if (!userid.IsNullOrEmpty())
            {
                builder.Append("AND CreateUserID=:CreateUserID ");
                OracleParameter item = new OracleParameter(":CreateUserID", OracleDbType.Varchar2) {
                    Value = userid
                };
                list.Add(item);
            }
            if (!name.IsNullOrEmpty())
            {
                builder.Append("AND CHARINDEX(:Name,Name)>0 ");
                OracleParameter parameter2 = new OracleParameter(":Name", OracleDbType.Varchar2) {
                    Value = name
                };
                list.Add(parameter2);
            }
            if (!typeid.IsNullOrEmpty())
            {
                builder.Append("AND Type IN (" + Tools.GetSqlInString(typeid, true, ",") + ")");
            }
            string sql = this.dbHelper.GetPaerSql("WorkFlowForm", "ID,Name,Type,CreateUserID,CreateUserName,CreateTime,LastModifyTime,'' Html,'' SubTableJson,'' EventsJson,'' Attribute,Status", builder.ToString(), order.IsNullOrEmpty() ? "CreateTime DESC" : order, pageSize, pageNumber, out count, list.ToArray());
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.WorkFlowForm> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public int Update(YJ.Data.Model.WorkFlowForm model)
        {
            string sql = "UPDATE WorkFlowForm SET \r\n\t\t\t\tName=:Name,Type=:Type,CreateUserID=:CreateUserID,CreateUserName=:CreateUserName,CreateTime=:CreateTime,LastModifyTime=:LastModifyTime,Html=:Html,SubTableJson=:SubTableJson,EventsJson=:EventsJson,Attribute=:Attribute,Status=:Status\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[12];
            OracleParameter parameter1 = new OracleParameter(":Name", OracleDbType.NVarchar2, 0x3e8) {
                Value = model.Name
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Type", OracleDbType.Varchar2, 40) {
                Value = model.Type
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":CreateUserID", OracleDbType.Varchar2, 40) {
                Value = model.CreateUserID
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":CreateUserName", OracleDbType.NVarchar2, 100) {
                Value = model.CreateUserName
            };
            parameterArray1[3] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":CreateTime", OracleDbType.Date, 8) {
                Value = model.CreateTime
            };
            parameterArray1[4] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":LastModifyTime", OracleDbType.Date, 8) {
                Value = model.LastModifyTime
            };
            parameterArray1[5] = parameter6;
            parameterArray1[6] = (model.Html == null) ? new OracleParameter(":Html", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Html", OracleDbType.Clob) { Value = model.Html };
            parameterArray1[7] = (model.SubTableJson == null) ? new OracleParameter(":SubTableJson", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":SubTableJson", OracleDbType.Clob) { Value = model.SubTableJson };
            parameterArray1[8] = (model.EventsJson == null) ? new OracleParameter(":EventsJson", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":EventsJson", OracleDbType.Clob) { Value = model.EventsJson };
            parameterArray1[9] = (model.Attribute == null) ? new OracleParameter(":Attribute", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Attribute", OracleDbType.Clob) { Value = model.Attribute };
            OracleParameter parameter15 = new OracleParameter(":Status", OracleDbType.Int32) {
                Value = model.Status
            };
            parameterArray1[10] = parameter15;
            OracleParameter parameter16 = new OracleParameter(":ID", OracleDbType.Varchar2, 40) {
                Value = model.ID
            };
            parameterArray1[11] = parameter16;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

