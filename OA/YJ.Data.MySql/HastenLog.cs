using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using YJ.Data.Interface;
using YJ.Data.Model;
namespace YJ.Data.MySql
{


    public class HastenLog : IHastenLog
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.HastenLog model)
        {
            string sql = "INSERT INTO hastenlog\r\n\t\t\t\t(ID,OthersParams,Users,Types,Contents,SendUser,SendUserName,SendTime) \r\n\t\t\t\tVALUES(@ID,@OthersParams,@Users,@Types,@Contents,@SendUser,@SendUserName,@SendTime)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[8];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@OthersParams", MySqlDbType.Text, -1) {
                Value = model.OthersParams
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Users", MySqlDbType.Text, -1) {
                Value = model.Users
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@Types", MySqlDbType.Text, -1) {
                Value = model.Types
            };
            parameterArray1[3] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@Contents", MySqlDbType.Text, -1) {
                Value = model.Contents
            };
            parameterArray1[4] = parameter5;
            MySqlParameter parameter6 = new MySqlParameter("@SendUser", MySqlDbType.VarChar, 0x24) {
                Value = model.SendUser
            };
            parameterArray1[5] = parameter6;
            MySqlParameter parameter7 = new MySqlParameter("@SendUserName", MySqlDbType.VarChar, 50) {
                Value = model.SendUserName
            };
            parameterArray1[6] = parameter7;
            MySqlParameter parameter8 = new MySqlParameter("@SendTime", MySqlDbType.DateTime, -1) {
                Value = model.SendTime
            };
            parameterArray1[7] = parameter8;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.HastenLog> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.HastenLog> list = new List<YJ.Data.Model.HastenLog>();
            YJ.Data.Model.HastenLog item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.HastenLog {
                    ID = dataReader.GetString(0).ToGuid(),
                    OthersParams = dataReader.GetString(1),
                    Users = dataReader.GetString(2),
                    Types = dataReader.GetString(3),
                    Contents = dataReader.GetString(4),
                    SendUser = dataReader.GetString(5).ToGuid(),
                    SendUserName = dataReader.GetString(6),
                    SendTime = dataReader.GetDateTime(7)
                };
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM hastenlog WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.HastenLog Get(Guid id)
        {
            string sql = "SELECT * FROM hastenlog WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.HastenLog> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.HastenLog> GetAll()
        {
            string sql = "SELECT * FROM hastenlog";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.HastenLog> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM hastenlog";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.HastenLog model)
        {
            string sql = "UPDATE hastenlog SET \r\n\t\t\t\tOthersParams=@OthersParams,Users=@Users,Types=@Types,Contents=@Contents,SendUser=@SendUser,SendUserName=@SendUserName,SendTime=@SendTime\r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[8];
            MySqlParameter parameter1 = new MySqlParameter("@OthersParams", MySqlDbType.Text, -1) {
                Value = model.OthersParams
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Users", MySqlDbType.Text, -1) {
                Value = model.Users
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Types", MySqlDbType.Text, -1) {
                Value = model.Types
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@Contents", MySqlDbType.Text, -1) {
                Value = model.Contents
            };
            parameterArray1[3] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@SendUser", MySqlDbType.VarChar, 0x24) {
                Value = model.SendUser
            };
            parameterArray1[4] = parameter5;
            MySqlParameter parameter6 = new MySqlParameter("@SendUserName", MySqlDbType.VarChar, 50) {
                Value = model.SendUserName
            };
            parameterArray1[5] = parameter6;
            MySqlParameter parameter7 = new MySqlParameter("@SendTime", MySqlDbType.DateTime, -1) {
                Value = model.SendTime
            };
            parameterArray1[6] = parameter7;
            MySqlParameter parameter8 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[7] = parameter8;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

