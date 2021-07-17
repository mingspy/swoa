namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class HastenLog : IHastenLog
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.HastenLog model)
        {
            string sql = "INSERT INTO HastenLog\r\n\t\t\t\t(ID,OthersParams,Users,Types,Contents,SendUser,SendUserName,SendTime) \r\n\t\t\t\tVALUES(:ID,:OthersParams,:Users,:Types,:Contents,:SendUser,:SendUserName,:SendTime)";
            OracleParameter[] parameterArray1 = new OracleParameter[8];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":OthersParams", OracleDbType.Varchar2) {
                Value = model.OthersParams
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":Users", OracleDbType.Varchar2) {
                Value = model.Users
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":Types", OracleDbType.Varchar2) {
                Value = model.Types
            };
            parameterArray1[3] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":Contents", OracleDbType.NVarchar2) {
                Value = model.Contents
            };
            parameterArray1[4] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":SendUser", OracleDbType.Varchar2) {
                Value = model.SendUser
            };
            parameterArray1[5] = parameter6;
            OracleParameter parameter7 = new OracleParameter(":SendUserName", OracleDbType.Varchar2) {
                Value = model.SendUserName
            };
            parameterArray1[6] = parameter7;
            OracleParameter parameter8 = new OracleParameter(":SendTime", OracleDbType.Date) {
                Value = model.SendTime
            };
            parameterArray1[7] = parameter8;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.HastenLog> DataReaderToList(OracleDataReader dataReader)
        {
            List<YJ.Data.Model.HastenLog> list = new List<YJ.Data.Model.HastenLog>();
            YJ.Data.Model.HastenLog item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.HastenLog {
                    ID = dataReader.GetGuid(0),
                    OthersParams = dataReader.GetString(1),
                    Users = dataReader.GetString(2),
                    Types = dataReader.GetString(3),
                    Contents = dataReader.GetString(4),
                    SendUser = dataReader.GetGuid(5),
                    SendUserName = dataReader.GetString(6),
                    SendTime = dataReader.GetDateTime(7)
                };
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM HastenLog WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.HastenLog Get(Guid id)
        {
            string sql = "SELECT * FROM HastenLog WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.HastenLog> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.HastenLog> GetAll()
        {
            string sql = "SELECT * FROM HastenLog";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.HastenLog> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM HastenLog";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.HastenLog model)
        {
            string sql = "UPDATE HastenLog SET \r\n\t\t\t\tOthersParams=:OthersParams,Users=:Users,Types=:Types,Contents=:Contents,SendUser=:SendUser,SendUserName=:SendUserName,SendTime=:SendTime\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[8];
            OracleParameter parameter1 = new OracleParameter(":OthersParams", OracleDbType.Varchar2) {
                Value = model.OthersParams
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Users", OracleDbType.Varchar2) {
                Value = model.Users
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":Types", OracleDbType.Varchar2) {
                Value = model.Types
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":Contents", OracleDbType.NVarchar2) {
                Value = model.Contents
            };
            parameterArray1[3] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":SendUser", OracleDbType.Varchar2) {
                Value = model.SendUser
            };
            parameterArray1[4] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":SendUserName", OracleDbType.Varchar2) {
                Value = model.SendUserName
            };
            parameterArray1[5] = parameter6;
            OracleParameter parameter7 = new OracleParameter(":SendTime", OracleDbType.Date) {
                Value = model.SendTime
            };
            parameterArray1[6] = parameter7;
            OracleParameter parameter8 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[7] = parameter8;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

