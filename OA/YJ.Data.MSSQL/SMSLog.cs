namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class SMSLog : ISMSLog
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.SMSLog model)
        {
            string sql = "INSERT INTO SMSLog\r\n\t\t\t\t(ID,MobileNumber,Contents,SendUserID,SendUserName,SendTime,Status,Note) \r\n\t\t\t\tVALUES(@ID,@MobileNumber,@Contents,@SendUserID,@SendUserName,@SendTime,@Status,@Note)";
            SqlParameter[] parameterArray1 = new SqlParameter[8];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@MobileNumber", SqlDbType.VarChar, -1) {
                Value = model.MobileNumber
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Contents", SqlDbType.NVarChar, 400) {
                Value = model.Contents
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = !model.SendUserID.HasValue ? new SqlParameter("@SendUserID", SqlDbType.UniqueIdentifier, -1) { Value = DBNull.Value } : new SqlParameter("@SendUserID", SqlDbType.UniqueIdentifier, -1) { Value = model.SendUserID };
            parameterArray1[4] = (model.SendUserName == null) ? new SqlParameter("@SendUserName", SqlDbType.NVarChar, 0x3e8) { Value = DBNull.Value } : new SqlParameter("@SendUserName", SqlDbType.NVarChar, 0x3e8) { Value = model.SendUserName };
            SqlParameter parameter8 = new SqlParameter("@SendTime", SqlDbType.DateTime, 8) {
                Value = model.SendTime
            };
            parameterArray1[5] = parameter8;
            SqlParameter parameter9 = new SqlParameter("@Status", SqlDbType.Int, -1) {
                Value = model.Status
            };
            parameterArray1[6] = parameter9;
            parameterArray1[7] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.VarChar, -1) { Value = model.Note };
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.SMSLog> DataReaderToList(SqlDataReader dataReader)
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
                if (!dataReader.IsDBNull(7))
                {
                    item.Note = dataReader.GetString(7);
                }
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM SMSLog WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.SMSLog Get(Guid id)
        {
            string sql = "SELECT * FROM SMSLog WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.SMSLog> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.SMSLog> GetAll()
        {
            string sql = "SELECT * FROM SMSLog";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
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
            string sql = "UPDATE SMSLog SET \r\n\t\t\t\tMobileNumber=@MobileNumber,Contents=@Contents,SendUserID=@SendUserID,SendUserName=@SendUserName,SendTime=@SendTime,Status=@Status,Note=@Note\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[8];
            SqlParameter parameter1 = new SqlParameter("@MobileNumber", SqlDbType.VarChar, -1) {
                Value = model.MobileNumber
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Contents", SqlDbType.NVarChar, 400) {
                Value = model.Contents
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = !model.SendUserID.HasValue ? new SqlParameter("@SendUserID", SqlDbType.UniqueIdentifier, -1) { Value = DBNull.Value } : new SqlParameter("@SendUserID", SqlDbType.UniqueIdentifier, -1) { Value = model.SendUserID };
            parameterArray1[3] = (model.SendUserName == null) ? new SqlParameter("@SendUserName", SqlDbType.NVarChar, 0x3e8) { Value = DBNull.Value } : new SqlParameter("@SendUserName", SqlDbType.NVarChar, 0x3e8) { Value = model.SendUserName };
            SqlParameter parameter7 = new SqlParameter("@SendTime", SqlDbType.DateTime, 8) {
                Value = model.SendTime
            };
            parameterArray1[4] = parameter7;
            SqlParameter parameter8 = new SqlParameter("@Status", SqlDbType.Int, -1) {
                Value = model.Status
            };
            parameterArray1[5] = parameter8;
            parameterArray1[6] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.VarChar, -1) { Value = model.Note };
            SqlParameter parameter11 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[7] = parameter11;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

