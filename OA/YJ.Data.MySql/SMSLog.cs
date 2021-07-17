using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using YJ.Data.Interface;
using YJ.Data.Model;
namespace YJ.Data.MySql
{


    public class SMSLog : ISMSLog
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.SMSLog model)
        {
            string sql = "INSERT INTO smslog\r\n\t\t\t\t(ID,MobileNumber,Contents,SendUserID,SendUserName,SendTime,Status,Note) \r\n\t\t\t\tVALUES(@ID,@MobileNumber,@Contents,@SendUserID,@SendUserName,@SendTime,@Status,@Note)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[8];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@MobileNumber", MySqlDbType.LongText, -1) {
                Value = model.MobileNumber
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Contents", MySqlDbType.VarChar, 200) {
                Value = model.Contents
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = !model.SendUserID.HasValue ? new MySqlParameter("@SendUserID", MySqlDbType.VarChar, 0x24) { Value = DBNull.Value } : new MySqlParameter("@SendUserID", MySqlDbType.VarChar, 0x24) { Value = model.SendUserID };
            parameterArray1[4] = (model.SendUserName == null) ? new MySqlParameter("@SendUserName", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@SendUserName", MySqlDbType.Text, -1) { Value = model.SendUserName };
            MySqlParameter parameter8 = new MySqlParameter("@SendTime", MySqlDbType.DateTime, -1) {
                Value = model.SendTime
            };
            parameterArray1[5] = parameter8;
            MySqlParameter parameter9 = new MySqlParameter("@Status", MySqlDbType.Int32, 11) {
                Value = model.Status
            };
            parameterArray1[6] = parameter9;
            parameterArray1[7] = (model.Note == null) ? new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = model.Note };
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.SMSLog> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.SMSLog> list = new List<YJ.Data.Model.SMSLog>();
            YJ.Data.Model.SMSLog item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.SMSLog {
                    ID = dataReader.GetString(0).ToGuid(),
                    MobileNumber = dataReader.GetString(1),
                    Contents = dataReader.GetString(2)
                };
                if (!dataReader.IsDBNull(3))
                {
                    item.SendUserID = new Guid?(dataReader.GetString(3).ToGuid());
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
            string sql = "DELETE FROM smslog WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.SMSLog Get(Guid id)
        {
            string sql = "SELECT * FROM smslog WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.SMSLog> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.SMSLog> GetAll()
        {
            string sql = "SELECT * FROM smslog";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.SMSLog> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM smslog";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.SMSLog model)
        {
            string sql = "UPDATE smslog SET \r\n\t\t\t\tMobileNumber=@MobileNumber,Contents=@Contents,SendUserID=@SendUserID,SendUserName=@SendUserName,SendTime=@SendTime,Status=@Status,Note=@Note\r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[8];
            MySqlParameter parameter1 = new MySqlParameter("@MobileNumber", MySqlDbType.LongText, -1) {
                Value = model.MobileNumber
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Contents", MySqlDbType.VarChar, 200) {
                Value = model.Contents
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = !model.SendUserID.HasValue ? new MySqlParameter("@SendUserID", MySqlDbType.VarChar, 0x24) { Value = DBNull.Value } : new MySqlParameter("@SendUserID", MySqlDbType.VarChar, 0x24) { Value = model.SendUserID };
            parameterArray1[3] = (model.SendUserName == null) ? new MySqlParameter("@SendUserName", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@SendUserName", MySqlDbType.Text, -1) { Value = model.SendUserName };
            MySqlParameter parameter7 = new MySqlParameter("@SendTime", MySqlDbType.DateTime, -1) {
                Value = model.SendTime
            };
            parameterArray1[4] = parameter7;
            MySqlParameter parameter8 = new MySqlParameter("@Status", MySqlDbType.Int32, 11) {
                Value = model.Status
            };
            parameterArray1[5] = parameter8;
            parameterArray1[6] = (model.Note == null) ? new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = model.Note };
            MySqlParameter parameter11 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[7] = parameter11;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

