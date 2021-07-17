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

    public class WorkFlowForm : IWorkFlowForm
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkFlowForm model)
        {
            string sql = "INSERT INTO WorkFlowForm\r\n\t\t\t\t(ID,Name,Type,CreateUserID,CreateUserName,CreateTime,LastModifyTime,Html,SubTableJson,EventsJson,Attribute,Status) \r\n\t\t\t\tVALUES(@ID,@Name,@Type,@CreateUserID,@CreateUserName,@CreateTime,@LastModifyTime,@Html,@SubTableJson,@EventsJson,@Attribute,@Status)";
            SqlParameter[] parameterArray1 = new SqlParameter[12];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Name", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Type", SqlDbType.UniqueIdentifier, -1) {
                Value = model.Type
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@CreateUserID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.CreateUserID
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@CreateUserName", SqlDbType.NVarChar, 100) {
                Value = model.CreateUserName
            };
            parameterArray1[4] = parameter5;
            SqlParameter parameter6 = new SqlParameter("@CreateTime", SqlDbType.DateTime, 8) {
                Value = model.CreateTime
            };
            parameterArray1[5] = parameter6;
            SqlParameter parameter7 = new SqlParameter("@LastModifyTime", SqlDbType.DateTime, 8) {
                Value = model.LastModifyTime
            };
            parameterArray1[6] = parameter7;
            parameterArray1[7] = (model.Html == null) ? new SqlParameter("@Html", SqlDbType.Text, -1) { Value = DBNull.Value } : new SqlParameter("@Html", SqlDbType.Text, -1) { Value = model.Html };
            parameterArray1[8] = (model.SubTableJson == null) ? new SqlParameter("@SubTableJson", SqlDbType.Text, -1) { Value = DBNull.Value } : new SqlParameter("@SubTableJson", SqlDbType.Text, -1) { Value = model.SubTableJson };
            parameterArray1[9] = (model.EventsJson == null) ? new SqlParameter("@EventsJson", SqlDbType.Text, -1) { Value = DBNull.Value } : new SqlParameter("@EventsJson", SqlDbType.Text, -1) { Value = model.EventsJson };
            parameterArray1[10] = (model.Attribute == null) ? new SqlParameter("@Attribute", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Attribute", SqlDbType.VarChar, -1) { Value = model.Attribute };
            SqlParameter parameter16 = new SqlParameter("@Status", SqlDbType.Int, -1) {
                Value = model.Status
            };
            parameterArray1[11] = parameter16;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.WorkFlowForm> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.WorkFlowForm> list = new List<YJ.Data.Model.WorkFlowForm>();
            YJ.Data.Model.WorkFlowForm item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.WorkFlowForm {
                    ID = dataReader.GetGuid(0),
                    Name = dataReader.GetString(1),
                    Type = dataReader.GetGuid(2),
                    CreateUserID = dataReader.GetGuid(3),
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
            string sql = "DELETE FROM WorkFlowForm WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.WorkFlowForm Get(Guid id)
        {
            string sql = "SELECT * FROM WorkFlowForm WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowForm> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkFlowForm> GetAll()
        {
            string sql = "SELECT * FROM WorkFlowForm";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowForm> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkFlowForm> GetAllByType(string types)
        {
            string sql = "SELECT ID, Name, Type, CreateUserID, CreateUserName, CreateTime, LastModifyTime, '' as Html, '' as SubTableJson, '' as EventsJson, '' as Attribute, Status FROM WorkFlowForm where Type IN(" + Tools.GetSqlInString(types, true, ",") + ")";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
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
            List<SqlParameter> list = new List<SqlParameter>();
            if (!userid.IsNullOrEmpty())
            {
                builder.Append("AND CreateUserID=@CreateUserID ");
                SqlParameter item = new SqlParameter("@CreateUserID", SqlDbType.UniqueIdentifier) {
                    Value = userid
                };
                list.Add(item);
            }
            if (!name.IsNullOrEmpty())
            {
                builder.Append("AND CHARINDEX(@Name,Name)>0 ");
                SqlParameter parameter2 = new SqlParameter("@Name", SqlDbType.VarChar) {
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
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.WorkFlowForm> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public List<YJ.Data.Model.WorkFlowForm> GetPagerData(out long count, int pageSize, int pageNumber, [Optional, DefaultParameterValue("")] string userid, [Optional, DefaultParameterValue("")] string typeid, [Optional, DefaultParameterValue("")] string name, [Optional, DefaultParameterValue("")] string order)
        {
            StringBuilder builder = new StringBuilder("AND Status IN(0,1) ");
            List<SqlParameter> list = new List<SqlParameter>();
            if (!userid.IsNullOrEmpty())
            {
                builder.Append("AND CreateUserID=@CreateUserID ");
                SqlParameter item = new SqlParameter("@CreateUserID", SqlDbType.UniqueIdentifier) {
                    Value = userid
                };
                list.Add(item);
            }
            if (!name.IsNullOrEmpty())
            {
                builder.Append("AND CHARINDEX(@Name,Name)>0 ");
                SqlParameter parameter2 = new SqlParameter("@Name", SqlDbType.VarChar) {
                    Value = name
                };
                list.Add(parameter2);
            }
            if (!typeid.IsNullOrEmpty())
            {
                builder.Append("AND Type IN (" + Tools.GetSqlInString(typeid, true, ",") + ")");
            }
            string sql = this.dbHelper.GetPaerSql("WorkFlowForm", "ID,Name,Type,CreateUserID,CreateUserName,CreateTime,LastModifyTime,'' Html,'' SubTableJson,'' EventsJson,'' Attribute,Status", builder.ToString(), order.IsNullOrEmpty() ? "CreateTime DESC" : order, pageSize, pageNumber, out count, list.ToArray());
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.WorkFlowForm> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public int Update(YJ.Data.Model.WorkFlowForm model)
        {
            string sql = "UPDATE WorkFlowForm SET \r\n\t\t\t\tName=@Name,Type=@Type,CreateUserID=@CreateUserID,CreateUserName=@CreateUserName,CreateTime=@CreateTime,LastModifyTime=@LastModifyTime,Html=@Html,SubTableJson=@SubTableJson,EventsJson=@EventsJson,Attribute=@Attribute,Status=@Status\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[12];
            SqlParameter parameter1 = new SqlParameter("@Name", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Name
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Type", SqlDbType.UniqueIdentifier, -1) {
                Value = model.Type
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@CreateUserID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.CreateUserID
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@CreateUserName", SqlDbType.NVarChar, 100) {
                Value = model.CreateUserName
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@CreateTime", SqlDbType.DateTime, 8) {
                Value = model.CreateTime
            };
            parameterArray1[4] = parameter5;
            SqlParameter parameter6 = new SqlParameter("@LastModifyTime", SqlDbType.DateTime, 8) {
                Value = model.LastModifyTime
            };
            parameterArray1[5] = parameter6;
            parameterArray1[6] = (model.Html == null) ? new SqlParameter("@Html", SqlDbType.Text, -1) { Value = DBNull.Value } : new SqlParameter("@Html", SqlDbType.Text, -1) { Value = model.Html };
            parameterArray1[7] = (model.SubTableJson == null) ? new SqlParameter("@SubTableJson", SqlDbType.Text, -1) { Value = DBNull.Value } : new SqlParameter("@SubTableJson", SqlDbType.Text, -1) { Value = model.SubTableJson };
            parameterArray1[8] = (model.EventsJson == null) ? new SqlParameter("@EventsJson", SqlDbType.Text, -1) { Value = DBNull.Value } : new SqlParameter("@EventsJson", SqlDbType.Text, -1) { Value = model.EventsJson };
            parameterArray1[9] = (model.Attribute == null) ? new SqlParameter("@Attribute", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Attribute", SqlDbType.VarChar, -1) { Value = model.Attribute };
            SqlParameter parameter15 = new SqlParameter("@Status", SqlDbType.Int, -1) {
                Value = model.Status
            };
            parameterArray1[10] = parameter15;
            SqlParameter parameter16 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[11] = parameter16;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

