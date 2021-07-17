namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class WorkFlowData : IWorkFlowData
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkFlowData model)
        {
            string sql = "INSERT INTO WorkFlowData\r\n\t\t\t\t(ID,InstanceID,LinkID,TableName,FieldName,Value) \r\n\t\t\t\tVALUES(:ID,:InstanceID,:LinkID,:TableName,:FieldName,:Value)";
            OracleParameter[] parameterArray1 = new OracleParameter[6];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2, 40) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":InstanceID", OracleDbType.Varchar2, 40) {
                Value = model.InstanceID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":LinkID", OracleDbType.Varchar2, 40) {
                Value = model.LinkID
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":TableName", OracleDbType.Varchar2, 500) {
                Value = model.TableName
            };
            parameterArray1[3] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":FieldName", OracleDbType.Varchar2, 500) {
                Value = model.FieldName
            };
            parameterArray1[4] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":Value", OracleDbType.Varchar2, 0xfa0) {
                Value = model.Value
            };
            parameterArray1[5] = parameter6;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.WorkFlowData> DataReaderToList(OracleDataReader dataReader)
        {
            List<YJ.Data.Model.WorkFlowData> list = new List<YJ.Data.Model.WorkFlowData>();
            YJ.Data.Model.WorkFlowData item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.WorkFlowData {
                    ID = dataReader.GetString(0).ToGuid(),
                    InstanceID = dataReader.GetString(1).ToGuid(),
                    LinkID = dataReader.GetString(2).ToGuid(),
                    TableName = dataReader.GetString(3),
                    FieldName = dataReader.GetString(4),
                    Value = dataReader.GetString(5)
                };
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM WorkFlowData WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.WorkFlowData Get(Guid id)
        {
            string sql = "SELECT * FROM WorkFlowData WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowData> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkFlowData> GetAll()
        {
            string sql = "SELECT * FROM WorkFlowData";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowData> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkFlowData> GetAll(Guid instanceID)
        {
            string sql = "SELECT * FROM WorkFlowData WHERE InstanceID=:InstanceID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":InstanceID", OracleDbType.Varchar2) {
                Value = instanceID
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowData> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM WorkFlowData";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.WorkFlowData model)
        {
            string sql = "UPDATE WorkFlowData SET \r\n\t\t\t\tInstanceID=:InstanceID,LinkID=:LinkID,TableName=:TableName,FieldName=:FieldName,Value=:Value\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[6];
            OracleParameter parameter1 = new OracleParameter(":InstanceID", OracleDbType.Varchar2, 40) {
                Value = model.InstanceID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":LinkID", OracleDbType.Varchar2, 40) {
                Value = model.LinkID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":TableName", OracleDbType.Varchar2, 500) {
                Value = model.TableName
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":FieldName", OracleDbType.Varchar2, 500) {
                Value = model.FieldName
            };
            parameterArray1[3] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":Value", OracleDbType.Varchar2, 0xfa0) {
                Value = model.Value
            };
            parameterArray1[4] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":ID", OracleDbType.Varchar2, 40) {
                Value = model.ID
            };
            parameterArray1[5] = parameter6;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

