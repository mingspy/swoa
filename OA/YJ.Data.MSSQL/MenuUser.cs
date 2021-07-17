namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class MenuUser : IMenuUser
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.MenuUser model)
        {
            string sql = "INSERT INTO MenuUser\r\n\t\t\t\t(ID,MenuID,SubPageID,Organizes,Users,Buttons,Params) \r\n\t\t\t\tVALUES(@ID,@MenuID,@SubPageID,@Organizes,@Users,@Buttons,@Params)";
            SqlParameter[] parameterArray1 = new SqlParameter[7];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@MenuID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.MenuID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@SubPageID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.SubPageID
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@Organizes", SqlDbType.VarChar, 100) {
                Value = model.Organizes
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@Users", SqlDbType.VarChar, -1) {
                Value = model.Users
            };
            parameterArray1[4] = parameter5;
            parameterArray1[5] = (model.Buttons == null) ? new SqlParameter("@Buttons", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Buttons", SqlDbType.VarChar, -1) { Value = model.Buttons };
            parameterArray1[6] = (model.Params == null) ? new SqlParameter("@Params", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Params", SqlDbType.VarChar, -1) { Value = model.Params };
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.MenuUser> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.MenuUser> list = new List<YJ.Data.Model.MenuUser>();
            YJ.Data.Model.MenuUser item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.MenuUser {
                    ID = dataReader.GetGuid(0),
                    MenuID = dataReader.GetGuid(1),
                    SubPageID = dataReader.GetGuid(2),
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
            string sql = "DELETE FROM MenuUser WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int DeleteByMenuID(Guid menuID)
        {
            string sql = "DELETE FROM MenuUser WHERE MenuID=@MenuID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@MenuID", SqlDbType.UniqueIdentifier) {
                Value = menuID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int DeleteByOrganizes(string organizes)
        {
            string sql = "DELETE FROM MenuUser WHERE Organizes=@Organizes";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@Organizes", SqlDbType.VarChar) {
                Value = organizes
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.MenuUser Get(Guid id)
        {
            string sql = "SELECT * FROM MenuUser WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.MenuUser> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.MenuUser> GetAll()
        {
            string sql = "SELECT * FROM MenuUser";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.MenuUser> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM MenuUser";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.MenuUser model)
        {
            string sql = "UPDATE MenuUser SET \r\n\t\t\t\tMenuID=@MenuID,SubPageID=@SubPageID,Organizes=@Organizes,Users=@Users,Buttons=@Buttons,Params=@Params\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[7];
            SqlParameter parameter1 = new SqlParameter("@MenuID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.MenuID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@SubPageID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.SubPageID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Organizes", SqlDbType.VarChar, 100) {
                Value = model.Organizes
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@Users", SqlDbType.VarChar, -1) {
                Value = model.Users
            };
            parameterArray1[3] = parameter4;
            parameterArray1[4] = (model.Buttons == null) ? new SqlParameter("@Buttons", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Buttons", SqlDbType.VarChar, -1) { Value = model.Buttons };
            parameterArray1[5] = (model.Params == null) ? new SqlParameter("@Params", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Params", SqlDbType.VarChar, -1) { Value = model.Params };
            SqlParameter parameter9 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[6] = parameter9;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

