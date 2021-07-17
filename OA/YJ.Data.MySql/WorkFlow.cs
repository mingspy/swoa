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


    public class WorkFlow : IWorkFlow
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkFlow model)
        {
            string sql = "INSERT INTO workflow\r\n\t\t\t\t(ID,Name,Type,Manager,InstanceManager,CreateDate,CreateUserID,DesignJSON,InstallDate,InstallUserID,RunJSON,Status) \r\n\t\t\t\tVALUES(@ID,@Name,@Type,@Manager,@InstanceManager,@CreateDate,@CreateUserID,@DesignJSON,@InstallDate,@InstallUserID,@RunJSON,@Status)";
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
            MySqlParameter parameter4 = new MySqlParameter("@Manager", MySqlDbType.Text, -1) {
                Value = model.Manager
            };
            parameterArray1[3] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@InstanceManager", MySqlDbType.Text, -1) {
                Value = model.InstanceManager
            };
            parameterArray1[4] = parameter5;
            MySqlParameter parameter6 = new MySqlParameter("@CreateDate", MySqlDbType.DateTime, -1) {
                Value = model.CreateDate
            };
            parameterArray1[5] = parameter6;
            MySqlParameter parameter7 = new MySqlParameter("@CreateUserID", MySqlDbType.VarChar, 0x24) {
                Value = model.CreateUserID
            };
            parameterArray1[6] = parameter7;
            parameterArray1[7] = (model.DesignJSON == null) ? new MySqlParameter("@DesignJSON", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@DesignJSON", MySqlDbType.LongText, -1) { Value = model.DesignJSON };
            parameterArray1[8] = !model.InstallDate.HasValue ? new MySqlParameter("@InstallDate", MySqlDbType.DateTime, -1) { Value = DBNull.Value } : new MySqlParameter("@InstallDate", MySqlDbType.DateTime, -1) { Value = model.InstallDate };
            parameterArray1[9] = !model.InstallUserID.HasValue ? new MySqlParameter("@InstallUserID", MySqlDbType.VarChar, 0x24) { Value = DBNull.Value } : new MySqlParameter("@InstallUserID", MySqlDbType.VarChar, 0x24) { Value = model.InstallUserID };
            parameterArray1[10] = (model.RunJSON == null) ? new MySqlParameter("@RunJSON", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@RunJSON", MySqlDbType.LongText, -1) { Value = model.RunJSON };
            MySqlParameter parameter16 = new MySqlParameter("@Status", MySqlDbType.Int32, 11) {
                Value = model.Status
            };
            parameterArray1[11] = parameter16;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.WorkFlow> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.WorkFlow> list = new List<YJ.Data.Model.WorkFlow>();
            YJ.Data.Model.WorkFlow item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.WorkFlow {
                    ID = dataReader.GetString(0).ToGuid(),
                    Name = dataReader.GetString(1),
                    Type = dataReader.GetString(2).ToGuid(),
                    Manager = dataReader.GetString(3),
                    InstanceManager = dataReader.GetString(4),
                    CreateDate = dataReader.GetDateTime(5),
                    CreateUserID = dataReader.GetString(6).ToGuid()
                };
                if (!dataReader.IsDBNull(7))
                {
                    item.DesignJSON = dataReader.GetString(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    item.InstallDate = new DateTime?(dataReader.GetDateTime(8));
                }
                if (!dataReader.IsDBNull(9))
                {
                    item.InstallUserID = new Guid?(dataReader.GetString(9).ToGuid());
                }
                if (!dataReader.IsDBNull(10))
                {
                    item.RunJSON = dataReader.GetString(10);
                }
                item.Status = dataReader.GetInt32(11);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM workflow WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.WorkFlow Get(Guid id)
        {
            string sql = "SELECT * FROM workflow WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlow> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkFlow> GetAll()
        {
            string sql = "SELECT * FROM workflow";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlow> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public Dictionary<Guid, string> GetAllIDAndName()
        {
            string sql = "SELECT ID,Name FROM WorkFlow WHERE Status<4 ORDER BY Name";
            Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>();
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            while (dataReader.Read())
            {
                dictionary.Add(dataReader.GetGuid(0), dataReader.GetString(1));
            }
            dataReader.Close();
            return dictionary;
        }

        public List<string> GetAllTypes()
        {
            string sql = "SELECT Type FROM WorkFlow GROUP BY Type";
            List<string> list = new List<string>();
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            while (dataReader.Read())
            {
                list.Add(dataReader.GetString(0));
            }
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkFlow> GetByTypes(string typeString)
        {
            string sql = "SELECT * FROM WorkFlow where Type IN(" + Tools.GetSqlInString(typeString, true, ",") + ")";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlow> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM workflow";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public List<YJ.Data.Model.WorkFlow> GetPagerData(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string userid, [Optional, DefaultParameterValue("")] string typeid, [Optional, DefaultParameterValue("")] string name, [Optional, DefaultParameterValue(15)] int pagesize)
        {
            long num;
            StringBuilder builder = new StringBuilder("AND Status<>4 ");
            List<MySqlParameter> list = new List<MySqlParameter>();
            if (!userid.IsNullOrEmpty())
            {
                builder.Append("AND INSTR(Manager,@Manager)>0 ");
                MySqlParameter item = new MySqlParameter("@Manager", MySqlDbType.VarChar) {
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
            string sql = this.dbHelper.GetPaerSql("WorkFlow", "ID,Name,Type,Manager,InstanceManager,CreateDate,CreateUserID,'' DesignJSON,InstallDate,InstallUserID,'' RunJSON,Status", builder.ToString(), "CreateDate DESC", size, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, size, pageNumber, query);
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.WorkFlow> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public List<YJ.Data.Model.WorkFlow> GetPagerData(out long count, int pageSize, int pageNumber, [Optional, DefaultParameterValue("")] string userid, [Optional, DefaultParameterValue("")] string typeid, [Optional, DefaultParameterValue("")] string name, [Optional, DefaultParameterValue("")] string order)
        {
            StringBuilder builder = new StringBuilder("AND Status<>4 ");
            List<MySqlParameter> list = new List<MySqlParameter>();
            if (!userid.IsNullOrEmpty())
            {
                builder.Append("AND INSTR(Manager,@Manager)>0 ");
                MySqlParameter item = new MySqlParameter("@Manager", MySqlDbType.VarChar) {
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
            string sql = this.dbHelper.GetPaerSql("WorkFlow", "ID,Name,Type,Manager,InstanceManager,CreateDate,CreateUserID,'' DesignJSON,InstallDate,InstallUserID,'' RunJSON,Status", builder.ToString(), order.IsNullOrEmpty() ? "CreateDate DESC" : order, pageSize, pageNumber, out count, list.ToArray());
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.WorkFlow> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public int Update(YJ.Data.Model.WorkFlow model)
        {
            string sql = "UPDATE workflow SET \r\n\t\t\t\tName=@Name,Type=@Type,Manager=@Manager,InstanceManager=@InstanceManager,CreateDate=@CreateDate,CreateUserID=@CreateUserID,DesignJSON=@DesignJSON,InstallDate=@InstallDate,InstallUserID=@InstallUserID,RunJSON=@RunJSON,Status=@Status\r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[12];
            MySqlParameter parameter1 = new MySqlParameter("@Name", MySqlDbType.Text, -1) {
                Value = model.Name
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Type", MySqlDbType.VarChar, 0x24) {
                Value = model.Type
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Manager", MySqlDbType.Text, -1) {
                Value = model.Manager
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@InstanceManager", MySqlDbType.Text, -1) {
                Value = model.InstanceManager
            };
            parameterArray1[3] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@CreateDate", MySqlDbType.DateTime, -1) {
                Value = model.CreateDate
            };
            parameterArray1[4] = parameter5;
            MySqlParameter parameter6 = new MySqlParameter("@CreateUserID", MySqlDbType.VarChar, 0x24) {
                Value = model.CreateUserID
            };
            parameterArray1[5] = parameter6;
            parameterArray1[6] = (model.DesignJSON == null) ? new MySqlParameter("@DesignJSON", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@DesignJSON", MySqlDbType.LongText, -1) { Value = model.DesignJSON };
            parameterArray1[7] = !model.InstallDate.HasValue ? new MySqlParameter("@InstallDate", MySqlDbType.DateTime, -1) { Value = DBNull.Value } : new MySqlParameter("@InstallDate", MySqlDbType.DateTime, -1) { Value = model.InstallDate };
            parameterArray1[8] = !model.InstallUserID.HasValue ? new MySqlParameter("@InstallUserID", MySqlDbType.VarChar, 0x24) { Value = DBNull.Value } : new MySqlParameter("@InstallUserID", MySqlDbType.VarChar, 0x24) { Value = model.InstallUserID };
            parameterArray1[9] = (model.RunJSON == null) ? new MySqlParameter("@RunJSON", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@RunJSON", MySqlDbType.LongText, -1) { Value = model.RunJSON };
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

