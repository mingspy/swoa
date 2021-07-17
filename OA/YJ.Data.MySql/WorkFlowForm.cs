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


    public class WorkFlowForm : IWorkFlowForm
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkFlowForm model)
        {
            string sql = "INSERT INTO workflowform\r\n\t\t\t\t(ID,Name,Type,CreateUserID,CreateUserName,CreateTime,LastModifyTime,Html,SubTableJson,EventsJson,Attribute,Status) \r\n\t\t\t\tVALUES(@ID,@Name,@Type,@CreateUserID,@CreateUserName,@CreateTime,@LastModifyTime,@Html,@SubTableJson,@EventsJson,@Attribute,@Status)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[12];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Name", MySqlDbType.Text, -1) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Type", MySqlDbType.VarChar, 0x24) {
                Value = model.Type
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@CreateUserID", MySqlDbType.VarChar, 0x24) {
                Value = model.CreateUserID
            };
            parameterArray1[3] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@CreateUserName", MySqlDbType.VarChar, 50) {
                Value = model.CreateUserName
            };
            parameterArray1[4] = parameter5;
            MySqlParameter parameter6 = new MySqlParameter("@CreateTime", MySqlDbType.DateTime, -1) {
                Value = model.CreateTime
            };
            parameterArray1[5] = parameter6;
            MySqlParameter parameter7 = new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime, -1) {
                Value = model.LastModifyTime
            };
            parameterArray1[6] = parameter7;
            parameterArray1[7] = (model.Html == null) ? new MySqlParameter("@Html", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Html", MySqlDbType.LongText, -1) { Value = model.Html };
            parameterArray1[8] = (model.SubTableJson == null) ? new MySqlParameter("@SubTableJson", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@SubTableJson", MySqlDbType.LongText, -1) { Value = model.SubTableJson };
            parameterArray1[9] = (model.EventsJson == null) ? new MySqlParameter("@EventsJson", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@EventsJson", MySqlDbType.LongText, -1) { Value = model.EventsJson };
            parameterArray1[10] = (model.Attribute == null) ? new MySqlParameter("@Attribute", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Attribute", MySqlDbType.LongText, -1) { Value = model.Attribute };
            MySqlParameter parameter16 = new MySqlParameter("@Status", MySqlDbType.Int32, 11) {
                Value = model.Status
            };
            parameterArray1[11] = parameter16;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.WorkFlowForm> DataReaderToList(MySqlDataReader dataReader)
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
            string sql = "DELETE FROM workflowform WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.WorkFlowForm Get(Guid id)
        {
            string sql = "SELECT * FROM workflowform WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowForm> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkFlowForm> GetAll()
        {
            string sql = "SELECT * FROM workflowform";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowForm> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkFlowForm> GetAllByType(string types)
        {
            string sql = "SELECT ID, Name, Type, CreateUserID, CreateUserName, CreateTime, LastModifyTime, '' as Html, '' as SubTableJson, '' as EventsJson, '' as Attribute, Status FROM WorkFlowForm where Type IN(" + Tools.GetSqlInString(types, true, ",") + ")";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowForm> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM workflowform";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public List<YJ.Data.Model.WorkFlowForm> GetPagerData(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string userid, [Optional, DefaultParameterValue("")] string typeid, [Optional, DefaultParameterValue("")] string name, [Optional, DefaultParameterValue(15)] int pagesize)
        {
            long num;
            StringBuilder builder = new StringBuilder("AND Status IN(0,1) ");
            List<MySqlParameter> list = new List<MySqlParameter>();
            if (!userid.IsNullOrEmpty())
            {
                builder.Append("AND CreateUserID=@CreateUserID ");
                MySqlParameter item = new MySqlParameter("@CreateUserID", MySqlDbType.VarChar) {
                    Value = userid
                };
                list.Add(item);
            }
            if (!name.IsNullOrEmpty())
            {
                builder.Append("AND INSTR(Name,@Name)>0 ");
                MySqlParameter parameter2 = new MySqlParameter("@Name", MySqlDbType.VarChar) {
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
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.WorkFlowForm> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public List<YJ.Data.Model.WorkFlowForm> GetPagerData(out long count, int pageSize, int pageNumber, [Optional, DefaultParameterValue("")] string userid, [Optional, DefaultParameterValue("")] string typeid, [Optional, DefaultParameterValue("")] string name, [Optional, DefaultParameterValue("")] string order)
        {
            StringBuilder builder = new StringBuilder("AND Status IN(0,1) ");
            List<MySqlParameter> list = new List<MySqlParameter>();
            if (!userid.IsNullOrEmpty())
            {
                builder.Append("AND CreateUserID=@CreateUserID ");
                MySqlParameter item = new MySqlParameter("@CreateUserID", MySqlDbType.VarChar) {
                    Value = userid
                };
                list.Add(item);
            }
            if (!name.IsNullOrEmpty())
            {
                builder.Append("AND INSTR(Name,@Name)>0 ");
                MySqlParameter parameter2 = new MySqlParameter("@Name", MySqlDbType.VarChar) {
                    Value = name
                };
                list.Add(parameter2);
            }
            if (!typeid.IsNullOrEmpty())
            {
                builder.Append("AND Type IN (" + Tools.GetSqlInString(typeid, true, ",") + ")");
            }
            string sql = this.dbHelper.GetPaerSql("WorkFlowForm", "ID,Name,Type,CreateUserID,CreateUserName,CreateTime,LastModifyTime,'' Html,'' SubTableJson,'' EventsJson,'' Attribute,Status", builder.ToString(), order.IsNullOrEmpty() ? "CreateTime DESC" : order, pageSize, pageNumber, out count, list.ToArray());
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.WorkFlowForm> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public int Update(YJ.Data.Model.WorkFlowForm model)
        {
            string sql = "UPDATE workflowform SET \r\n\t\t\t\tName=@Name,Type=@Type,CreateUserID=@CreateUserID,CreateUserName=@CreateUserName,CreateTime=@CreateTime,LastModifyTime=@LastModifyTime,Html=@Html,SubTableJson=@SubTableJson,EventsJson=@EventsJson,Attribute=@Attribute,Status=@Status\r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[12];
            MySqlParameter parameter1 = new MySqlParameter("@Name", MySqlDbType.Text, -1) {
                Value = model.Name
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Type", MySqlDbType.VarChar, 0x24) {
                Value = model.Type
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@CreateUserID", MySqlDbType.VarChar, 0x24) {
                Value = model.CreateUserID
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@CreateUserName", MySqlDbType.VarChar, 50) {
                Value = model.CreateUserName
            };
            parameterArray1[3] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@CreateTime", MySqlDbType.DateTime, -1) {
                Value = model.CreateTime
            };
            parameterArray1[4] = parameter5;
            MySqlParameter parameter6 = new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime, -1) {
                Value = model.LastModifyTime
            };
            parameterArray1[5] = parameter6;
            parameterArray1[6] = (model.Html == null) ? new MySqlParameter("@Html", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Html", MySqlDbType.LongText, -1) { Value = model.Html };
            parameterArray1[7] = (model.SubTableJson == null) ? new MySqlParameter("@SubTableJson", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@SubTableJson", MySqlDbType.LongText, -1) { Value = model.SubTableJson };
            parameterArray1[8] = (model.EventsJson == null) ? new MySqlParameter("@EventsJson", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@EventsJson", MySqlDbType.LongText, -1) { Value = model.EventsJson };
            parameterArray1[9] = (model.Attribute == null) ? new MySqlParameter("@Attribute", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Attribute", MySqlDbType.LongText, -1) { Value = model.Attribute };
            MySqlParameter parameter15 = new MySqlParameter("@Status", MySqlDbType.Int32, 11) {
                Value = model.Status
            };
            parameterArray1[10] = parameter15;
            MySqlParameter parameter16 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[11] = parameter16;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

