using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using YJ.Data.Interface;
using YJ.Data.Model;
namespace YJ.Data.MySql
{


    public class DBConnection : IDBConnection
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.DBConnection model)
        {
            string sql = "INSERT INTO dbconnection\r\n\t\t\t\t(ID,Name,Type,ConnectionString,Note) \r\n\t\t\t\tVALUES(@ID,@Name,@Type,@ConnectionString,@Note)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[5];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Name", MySqlDbType.Text, -1) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Type", MySqlDbType.Text, -1) {
                Value = model.Type
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@ConnectionString", MySqlDbType.LongText, -1) {
                Value = model.ConnectionString
            };
            parameterArray1[3] = parameter4;
            parameterArray1[4] = (model.Note == null) ? new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = model.Note };
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.DBConnection> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.DBConnection> list = new List<YJ.Data.Model.DBConnection>();
            YJ.Data.Model.DBConnection item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.DBConnection {
                    ID = dataReader.GetString(0).ToGuid(),
                    Name = dataReader.GetString(1),
                    Type = dataReader.GetString(2),
                    ConnectionString = dataReader.GetString(3)
                };
                if (!dataReader.IsDBNull(4))
                {
                    item.Note = dataReader.GetString(4);
                }
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM dbconnection WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.DBConnection Get(Guid id)
        {
            string sql = "SELECT * FROM dbconnection WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.DBConnection> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.DBConnection> GetAll()
        {
            string sql = "SELECT * FROM dbconnection";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.DBConnection> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM dbconnection";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.DBConnection model)
        {
            string sql = "UPDATE dbconnection SET \r\n\t\t\t\tName=@Name,Type=@Type,ConnectionString=@ConnectionString,Note=@Note\r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[5];
            MySqlParameter parameter1 = new MySqlParameter("@Name", MySqlDbType.Text, -1) {
                Value = model.Name
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Type", MySqlDbType.Text, -1) {
                Value = model.Type
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@ConnectionString", MySqlDbType.LongText, -1) {
                Value = model.ConnectionString
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = (model.Note == null) ? new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = model.Note };
            MySqlParameter parameter6 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[4] = parameter6;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

