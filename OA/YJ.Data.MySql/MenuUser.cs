using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using YJ.Data.Interface;
using YJ.Data.Model;
namespace YJ.Data.MySql
{


    public class MenuUser : IMenuUser
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.MenuUser model)
        {
            string sql = "INSERT INTO menuuser\r\n\t\t\t\t(ID,MenuID,SubPageID,Organizes,Users,Buttons,Params) \r\n\t\t\t\tVALUES(@ID,@MenuID,@SubPageID,@Organizes,@Users,@Buttons,@Params)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[7];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@MenuID", MySqlDbType.VarChar, 0x24) {
                Value = model.MenuID
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@SubPageID", MySqlDbType.VarChar, 0x24) {
                Value = model.SubPageID
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@Organizes", MySqlDbType.VarChar, 100) {
                Value = model.Organizes
            };
            parameterArray1[3] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@Users", MySqlDbType.LongText, -1) {
                Value = model.Users
            };
            parameterArray1[4] = parameter5;
            parameterArray1[5] = (model.Buttons == null) ? new MySqlParameter("@Buttons", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Buttons", MySqlDbType.LongText, -1) { Value = model.Buttons };
            parameterArray1[6] = (model.Params == null) ? new MySqlParameter("@Params", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Params", MySqlDbType.LongText, -1) { Value = model.Params };
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.MenuUser> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.MenuUser> list = new List<YJ.Data.Model.MenuUser>();
            YJ.Data.Model.MenuUser item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.MenuUser {
                    ID = dataReader.GetString(0).ToGuid(),
                    MenuID = dataReader.GetString(1).ToGuid(),
                    SubPageID = dataReader.GetString(2).ToGuid(),
                    Organizes = dataReader.GetString(3),
                    Users = dataReader.GetString(4)
                };
                if (!dataReader.IsDBNull(5))
                {
                    item.Buttons = dataReader.GetString(5);
                }
                if (!dataReader.IsDBNull(6))
                {
                    item.Params = dataReader.GetString(6);
                }
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM menuuser WHERE ID=@ID";
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
            string sql = "DELETE FROM MenuUser WHERE MenuID=@MenuID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@MenuID", MySqlDbType.VarChar) {
                Value = menuID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int DeleteByOrganizes(string organizes)
        {
            string sql = "DELETE FROM MenuUser WHERE Organizes=@Organizes";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@Organizes", MySqlDbType.VarChar) {
                Value = organizes
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.MenuUser Get(Guid id)
        {
            string sql = "SELECT * FROM menuuser WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.MenuUser> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.MenuUser> GetAll()
        {
            string sql = "SELECT * FROM menuuser";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.MenuUser> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM menuuser";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.MenuUser model)
        {
            string sql = "UPDATE menuuser SET \r\n\t\t\t\tMenuID=@MenuID,SubPageID=@SubPageID,Organizes=@Organizes,Users=@Users,Buttons=@Buttons,Params=@Params\r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[7];
            MySqlParameter parameter1 = new MySqlParameter("@MenuID", MySqlDbType.VarChar, 0x24) {
                Value = model.MenuID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@SubPageID", MySqlDbType.VarChar, 0x24) {
                Value = model.SubPageID
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Organizes", MySqlDbType.VarChar, 100) {
                Value = model.Organizes
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@Users", MySqlDbType.LongText, -1) {
                Value = model.Users
            };
            parameterArray1[3] = parameter4;
            parameterArray1[4] = (model.Buttons == null) ? new MySqlParameter("@Buttons", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Buttons", MySqlDbType.LongText, -1) { Value = model.Buttons };
            parameterArray1[5] = (model.Params == null) ? new MySqlParameter("@Params", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Params", MySqlDbType.LongText, -1) { Value = model.Params };
            MySqlParameter parameter9 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[6] = parameter9;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

