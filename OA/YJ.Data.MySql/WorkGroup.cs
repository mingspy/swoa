using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using YJ.Data.Interface;
using YJ.Data.Model;
namespace YJ.Data.MySql
{


    public class WorkGroup : IWorkGroup
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkGroup model)
        {
            string sql = "INSERT INTO workgroup\r\n\t\t\t\t(ID,Name,Members,Note,IntID) \r\n\t\t\t\tVALUES(@ID,@Name,@Members,@Note,@IntID)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[5];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Name", MySqlDbType.Text, -1) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Members", MySqlDbType.LongText, -1) {
                Value = model.Members
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = (model.Note == null) ? new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = model.Note };
            MySqlParameter parameter6 = new MySqlParameter("@IntID", MySqlDbType.Int32, -1) {
                Value = model.IntID
            };
            parameterArray1[4] = parameter6;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.WorkGroup> DataReaderToList(MySqlDataReader dataReader)
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
            string sql = "DELETE FROM workgroup WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.WorkGroup Get(Guid id)
        {
            string sql = "SELECT * FROM workgroup WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkGroup> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkGroup> GetAll()
        {
            string sql = "SELECT * FROM workgroup";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkGroup> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM workgroup";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.WorkGroup model)
        {
            string sql = "UPDATE workgroup SET \r\n\t\t\t\tName=@Name,Members=@Members,Note=@Note,IntID=@IntID \r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[5];
            MySqlParameter parameter1 = new MySqlParameter("@Name", MySqlDbType.Text, -1) {
                Value = model.Name
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Members", MySqlDbType.LongText, -1) {
                Value = model.Members
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = (model.Note == null) ? new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = model.Note };
            MySqlParameter parameter5 = new MySqlParameter("@IntID", MySqlDbType.Int32, -1) {
                Value = model.IntID
            };
            parameterArray1[3] = parameter5;
            MySqlParameter parameter6 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[4] = parameter6;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

