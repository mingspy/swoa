namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class SMSLog : ISMSLog
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.SMSLog model)
        {
            string sql = "INSERT INTO SMSLog\r\n\t\t\t\t(ID,MobileNumber,Contents,SendUserID,SendUserName,SendTime,Status,Note) \r\n\t\t\t\tVALUES(:ID,:MobileNumber,:Contents,:SendUserID,:SendUserName,:SendTime,:Status,:Note)";
            OracleParameter[] parameterArray1 = new OracleParameter[8];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":MobileNumber", OracleDbType.Varchar2) {
                Value = model.MobileNumber
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":Contents", OracleDbType.NVarchar2) {
                Value = model.Contents
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = !model.SendUserID.HasValue ? new OracleParameter(":SendUserID", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":SendUserID", OracleDbType.Varchar2) { Value = model.SendUserID };
            parameterArray1[4] = (model.SendUserName == null) ? new OracleParameter(":SendUserName", OracleDbType.NVarchar2, 0x3e8) { Value = DBNull.Value } : new OracleParameter(":SendUserName", OracleDbType.NVarchar2) { Value = model.SendUserName };
            OracleParameter parameter8 = new OracleParameter(":SendTime", OracleDbType.Date) {
                Value = model.SendTime
            };
            parameterArray1[5] = parameter8;
            OracleParameter parameter9 = new OracleParameter(":Status", OracleDbType.Int32) {
                Value = model.Status
            };
            parameterArray1[6] = parameter9;
            OracleParameter parameter10 = new OracleParameter(":Note", OracleDbType.Varchar2) {
                Value = model.Note
            };
            parameterArray1[7] = parameter10;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.SMSLog> DataReaderToList(OracleDataReader dataReader)
        {
            List<YJ.Data.Model.SMSLog> list = new List<YJ.Data.Model.SMSLog>();
            YJ.Data.Model.SMSLog item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.SMSLog {
                    ID = dataReader.GetGuid(0),
                    MobileNumber = dataReader.GetString(1),
                    Contents = dataReader.GetString(2)
                };
                if (!dataReader.IsDBNull(3))
                {
                    item.SendUserID = new Guid?(dataReader.GetGuid(3));
                }
                if (!dataReader.IsDBNull(4))
                {
                    item.SendUserName = dataReader.GetString(4);
                }
                item.SendTime = dataReader.GetDateTime(5);
                item.Status = dataReader.GetInt32(6);
                item.Note = dataReader.GetString(7);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM SMSLog WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.SMSLog Get(Guid id)
        {
            string sql = "SELECT * FROM SMSLog WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.SMSLog> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.SMSLog> GetAll()
        {
            string sql = "SELECT * FROM SMSLog";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.SMSLog> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM SMSLog";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.SMSLog model)
        {
            string sql = "UPDATE SMSLog SET \r\n\t\t\t\tMobileNumber=:MobileNumber,Contents=:Contents,SendUserID=:SendUserID,SendUserName=:SendUserName,SendTime=:SendTime,Status=:Status,Note=:Note\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[8];
            OracleParameter parameter1 = new OracleParameter(":MobileNumber", OracleDbType.Varchar2) {
                Value = model.MobileNumber
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Contents", OracleDbType.NVarchar2) {
                Value = model.Contents
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = !model.SendUserID.HasValue ? new OracleParameter(":SendUserID", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":SendUserID", OracleDbType.Varchar2) { Value = model.SendUserID };
            parameterArray1[3] = (model.SendUserName == null) ? new OracleParameter(":SendUserName", OracleDbType.NVarchar2) { Value = DBNull.Value } : new OracleParameter(":SendUserName", OracleDbType.NVarchar2) { Value = model.SendUserName };
            OracleParameter parameter7 = new OracleParameter(":SendTime", OracleDbType.Date) {
                Value = model.SendTime
            };
            parameterArray1[4] = parameter7;
            OracleParameter parameter8 = new OracleParameter(":Status", OracleDbType.Int32) {
                Value = model.Status
            };
            parameterArray1[5] = parameter8;
            OracleParameter parameter9 = new OracleParameter(":Note", OracleDbType.Varchar2) {
                Value = model.Note
            };
            parameterArray1[6] = parameter9;
            OracleParameter parameter10 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[7] = parameter10;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

