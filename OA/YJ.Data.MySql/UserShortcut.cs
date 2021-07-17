using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using YJ.Data.Interface;
using YJ.Data.Model;

namespace YJ.Data.MySql
{
 
    public class UserShortcut : IUserShortcut
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.UserShortcut model)
        {
            string sql = "INSERT INTO usershortcut\r\n\t\t\t\t(ID,MenuID,UserID,Sort) \r\n\t\t\t\tVALUES(@ID,@MenuID,@UserID,@Sort)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[4];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@MenuID", MySqlDbType.VarChar, 0x24) {
                Value = model.MenuID
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@UserID", MySqlDbType.VarChar, 0x24) {
                Value = model.UserID
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[3] = parameter4;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.UserShortcut> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.UserShortcut> list = new List<YJ.Data.Model.UserShortcut>();
            YJ.Data.Model.UserShortcut item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.UserShortcut {
                    ID = dataReader.GetString(0).ToGuid(),
                    MenuID = dataReader.GetString(1).ToGuid(),
                    UserID = dataReader.GetString(2).ToGuid(),
                    Sort = dataReader.GetInt32(3)
                };
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM usershortcut WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int DeleteByMenuID(Guid menuID)
        {
            string sql = "DELETE FROM UserShortcut WHERE MenuID=@MenuID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@MenuID", MySqlDbType.VarChar) {
                Value = menuID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int DeleteByUserID(Guid userID)
        {
            string sql = "DELETE FROM UserShortcut WHERE UserID=@UserID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@UserID", MySqlDbType.VarChar) {
                Value = userID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.UserShortcut Get(Guid id)
        {
            string sql = "SELECT * FROM usershortcut WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.UserShortcut> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.UserShortcut> GetAll()
        {
            string sql = "SELECT * FROM usershortcut";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.UserShortcut> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.UserShortcut> GetAllByUserID(Guid userID)
        {
            string sql = "SELECT * FROM UserShortcut WHERE UserID=@UserID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@UserID", MySqlDbType.VarChar) {
                Value = userID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.UserShortcut> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM usershortcut";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public DataTable GetDataTableByUserID(Guid userID)
        {
            string sql = "SELECT * FROM UserShortcut WHERE UserID=@UserID ORDER BY Sort";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@UserID", MySqlDbType.VarChar) {
                Value = userID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.GetDataTable(sql, parameter);
        }

        public int Update(YJ.Data.Model.UserShortcut model)
        {
            string sql = "UPDATE usershortcut SET \r\n\t\t\t\tMenuID=@MenuID,UserID=@UserID,Sort=@Sort\r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[4];
            MySqlParameter parameter1 = new MySqlParameter("@MenuID", MySqlDbType.VarChar, 0x24) {
                Value = model.MenuID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@UserID", MySqlDbType.VarChar, 0x24) {
                Value = model.UserID
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[3] = parameter4;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

