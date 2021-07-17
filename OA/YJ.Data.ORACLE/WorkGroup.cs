namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class WorkGroup : IWorkGroup
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkGroup model)
        {
            string sql = "INSERT INTO WorkGroup\r\n\t\t\t\t(ID,Name,Members,Note,IntID) \r\n\t\t\t\tVALUES(:ID,:Name,:Members,:Note,:IntID)";
            OracleParameter[] parameterArray1 = new OracleParameter[5];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2, 40) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Name", OracleDbType.NVarchar2, 0x3e8) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":Members", OracleDbType.Clob) {
                Value = model.Members
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = (model.Note == null) ? new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = model.Note };
            OracleParameter parameter6 = new OracleParameter(":IntID", OracleDbType.Int32) {
                Value = model.IntID
            };
            parameterArray1[4] = parameter6;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.WorkGroup> DataReaderToList(OracleDataReader dataReader)
        {
            List<YJ.Data.Model.WorkGroup> list = new List<YJ.Data.Model.WorkGroup>();
            YJ.Data.Model.WorkGroup item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.WorkGroup {
                    ID = dataReader.GetString(0).ToGuid(),
                    Name = dataReader.GetString(1),
                    Members = dataReader.GetString(2)
                };
                if (!dataReader.IsDBNull(3))
                {
                    item.Note = dataReader.GetString(3);
                }
                item.IntID = dataReader.GetInt32(4);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM WorkGroup WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.WorkGroup Get(Guid id)
        {
            string sql = "SELECT * FROM WorkGroup WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkGroup> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkGroup> GetAll()
        {
            string sql = "SELECT * FROM WorkGroup";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkGroup> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM WorkGroup";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.WorkGroup model)
        {
            string sql = "UPDATE WorkGroup SET \r\n\t\t\t\tName=:Name,Members=:Members,Note=:Note,IntID=:IntID \r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[5];
            OracleParameter parameter1 = new OracleParameter(":Name", OracleDbType.NVarchar2, 0x3e8) {
                Value = model.Name
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Members", OracleDbType.Clob) {
                Value = model.Members
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = (model.Note == null) ? new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = model.Note };
            OracleParameter parameter5 = new OracleParameter(":IntID", OracleDbType.Int32) {
                Value = model.IntID
            };
            parameterArray1[3] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":ID", OracleDbType.Varchar2, 40) {
                Value = model.ID
            };
            parameterArray1[4] = parameter6;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

