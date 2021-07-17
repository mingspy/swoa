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

    public class WorkFlow : IWorkFlow
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkFlow model)
        {
            string sql = "INSERT INTO WorkFlow\r\n\t\t\t\t(ID,Name,Type,Manager,InstanceManager,CreateDate,CreateUserID,DesignJSON,InstallDate,InstallUserID,RunJSON,Status) \r\n\t\t\t\tVALUES(:ID,:Name,:Type,:Manager,:InstanceManager,:CreateDate,:CreateUserID,:DesignJSON,:InstallDate,:InstallUserID,:RunJSON,:Status)";
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
            OracleParameter parameter4 = new OracleParameter(":Manager", OracleDbType.Varchar2, 0x1388) {
                Value = model.Manager
            };
            parameterArray1[3] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":InstanceManager", OracleDbType.Varchar2, 0x1388) {
                Value = model.InstanceManager
            };
            parameterArray1[4] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":CreateDate", OracleDbType.Date, 8) {
                Value = model.CreateDate
            };
            parameterArray1[5] = parameter6;
            OracleParameter parameter7 = new OracleParameter(":CreateUserID", OracleDbType.Varchar2, 40) {
                Value = model.CreateUserID
            };
            parameterArray1[6] = parameter7;
            parameterArray1[7] = (model.DesignJSON == null) ? new OracleParameter(":DesignJSON", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":DesignJSON", OracleDbType.Clob) { Value = model.DesignJSON };
            parameterArray1[8] = !model.InstallDate.HasValue ? new OracleParameter(":InstallDate", OracleDbType.Date, 8) { Value = DBNull.Value } : new OracleParameter(":InstallDate", OracleDbType.Date, 8) { Value = model.InstallDate };
            parameterArray1[9] = !model.InstallUserID.HasValue ? new OracleParameter(":InstallUserID", OracleDbType.Varchar2, 40) { Value = DBNull.Value } : new OracleParameter(":InstallUserID", OracleDbType.Varchar2, 40) { Value = model.InstallUserID };
            parameterArray1[10] = (model.RunJSON == null) ? new OracleParameter(":RunJSON", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":RunJSON", OracleDbType.Clob) { Value = model.RunJSON };
            OracleParameter parameter16 = new OracleParameter(":Status", OracleDbType.Int32) {
                Value = model.Status
            };
            parameterArray1[11] = parameter16;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.WorkFlow> DataReaderToList(OracleDataReader dataReader)
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
            string sql = "DELETE FROM WorkFlow WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.WorkFlow Get(Guid id)
        {
            string sql = "SELECT * FROM WorkFlow WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlow> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkFlow> GetAll()
        {
            string sql = "SELECT * FROM WorkFlow";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlow> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public Dictionary<Guid, string> GetAllIDAndName()
        {
            string sql = "SELECT ID,Name FROM WorkFlow WHERE Status<4 ORDER BY Name";
            Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>();
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            while (dataReader.Read())
            {
                dictionary.Add(dataReader.GetString(0).ToGuid(), dataReader.GetString(1));
            }
            dataReader.Close();
            return dictionary;
        }

        public List<string> GetAllTypes()
        {
            string sql = "SELECT Type FROM WorkFlow GROUP BY Type";
            List<string> list = new List<string>();
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
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
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlow> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM WorkFlow";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public List<YJ.Data.Model.WorkFlow> GetPagerData(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string userid, [Optional, DefaultParameterValue("")] string typeid, [Optional, DefaultParameterValue("")] string name, [Optional, DefaultParameterValue(15)] int pagesize)
        {
            long num;
            StringBuilder builder = new StringBuilder("AND Status!=4 ");
            List<OracleParameter> list = new List<OracleParameter>();
            if (!userid.IsNullOrEmpty())
            {
                builder.Append("AND INSTR(Manager,:Manager,1,1)>0 ");
                OracleParameter item = new OracleParameter(":Manager", OracleDbType.Varchar2) {
                    Value = userid
                };
                list.Add(item);
            }
            if (!name.IsNullOrEmpty())
            {
                builder.Append("AND INSTR(Name,:Name,1,1)>0 ");
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
            string sql = this.dbHelper.GetPaerSql("WorkFlow", "ID,Name,Type,Manager,InstanceManager,CreateDate,CreateUserID,'' DesignJSON,InstallDate,InstallUserID,'' RunJSON,Status", builder.ToString(), "CreateDate DESC", size, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, size, pageNumber, query);
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.WorkFlow> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public List<YJ.Data.Model.WorkFlow> GetPagerData(out long count, int pageSize, int pageNumber, [Optional, DefaultParameterValue("")] string userid, [Optional, DefaultParameterValue("")] string typeid, [Optional, DefaultParameterValue("")] string name, [Optional, DefaultParameterValue("")] string order)
        {
            StringBuilder builder = new StringBuilder("AND Status!=4 ");
            List<OracleParameter> list = new List<OracleParameter>();
            if (!userid.IsNullOrEmpty())
            {
                builder.Append("AND INSTR(Manager,:Manager,1,1)>0 ");
                OracleParameter item = new OracleParameter(":Manager", OracleDbType.Varchar2) {
                    Value = userid
                };
                list.Add(item);
            }
            if (!name.IsNullOrEmpty())
            {
                builder.Append("AND INSTR(Name,:Name,1,1)>0 ");
                OracleParameter parameter2 = new OracleParameter(":Name", OracleDbType.Varchar2) {
                    Value = name
                };
                list.Add(parameter2);
            }
            if (!typeid.IsNullOrEmpty())
            {
                builder.Append("AND Type IN (" + Tools.GetSqlInString(typeid, true, ",") + ")");
            }
            string sql = this.dbHelper.GetPaerSql("WorkFlow", "ID,Name,Type,Manager,InstanceManager,CreateDate,CreateUserID,'' DesignJSON,InstallDate,InstallUserID,'' RunJSON,Status", builder.ToString(), order.IsNullOrEmpty() ? "CreateDate DESC" : order, pageSize, pageNumber, out count, list.ToArray());
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.WorkFlow> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public int Update(YJ.Data.Model.WorkFlow model)
        {
            string sql = "UPDATE WorkFlow SET \r\n\t\t\t\tName=:Name,Type=:Type,Manager=:Manager,InstanceManager=:InstanceManager,CreateDate=:CreateDate,CreateUserID=:CreateUserID,DesignJSON=:DesignJSON,InstallDate=:InstallDate,InstallUserID=:InstallUserID,RunJSON=:RunJSON,Status=:Status\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[12];
            OracleParameter parameter1 = new OracleParameter(":Name", OracleDbType.NVarchar2, 0x3e8) {
                Value = model.Name
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Type", OracleDbType.Varchar2, 40) {
                Value = model.Type
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":Manager", OracleDbType.Varchar2, 0x1388) {
                Value = model.Manager
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":InstanceManager", OracleDbType.Varchar2, 0x1388) {
                Value = model.InstanceManager
            };
            parameterArray1[3] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":CreateDate", OracleDbType.Date, 8) {
                Value = model.CreateDate
            };
            parameterArray1[4] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":CreateUserID", OracleDbType.Varchar2, 40) {
                Value = model.CreateUserID
            };
            parameterArray1[5] = parameter6;
            parameterArray1[6] = (model.DesignJSON == null) ? new OracleParameter(":DesignJSON", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":DesignJSON", OracleDbType.Clob) { Value = model.DesignJSON };
            parameterArray1[7] = !model.InstallDate.HasValue ? new OracleParameter(":InstallDate", OracleDbType.Date, 8) { Value = DBNull.Value } : new OracleParameter(":InstallDate", OracleDbType.Date, 8) { Value = model.InstallDate };
            parameterArray1[8] = !model.InstallUserID.HasValue ? new OracleParameter(":InstallUserID", OracleDbType.Varchar2, 40) { Value = DBNull.Value } : new OracleParameter(":InstallUserID", OracleDbType.Varchar2, 40) { Value = model.InstallUserID };
            parameterArray1[9] = (model.RunJSON == null) ? new OracleParameter(":RunJSON", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":RunJSON", OracleDbType.Clob) { Value = model.RunJSON };
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

