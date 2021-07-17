namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class UserShortcut : IUserShortcut
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.UserShortcut model)
        {
            string sql = "INSERT INTO UserShortcut\r\n\t\t\t\t(ID,MenuID,UserID,Sort) \r\n\t\t\t\tVALUES(@ID,@MenuID,@UserID,@Sort)";
            SqlParameter[] parameterArray1 = new SqlParameter[4];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@MenuID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.MenuID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.UserID
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[3] = parameter4;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.UserShortcut> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.UserShortcut> list = new List<YJ.Data.Model.UserShortcut>();
            YJ.Data.Model.UserShortcut item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.UserShortcut {
                    ID = dataReader.GetGuid(0),
                    MenuID = dataReader.GetGuid(1),
                    UserID = dataReader.GetGuid(2),
                    Sort = dataReader.GetInt32(3)
                };
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM UserShortcut WHERE ID=@ID";
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
            string sql = "DELETE FROM UserShortcut WHERE MenuID=@MenuID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@MenuID", SqlDbType.UniqueIdentifier) {
                Value = menuID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int DeleteByUserID(Guid userID)
        {
            string sql = "DELETE FROM UserShortcut WHERE UserID=@UserID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier) {
                Value = userID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.UserShortcut Get(Guid id)
        {
            string sql = "SELECT * FROM UserShortcut WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.UserShortcut> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.UserShortcut> GetAll()
        {
            string sql = "SELECT * FROM UserShortcut";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.UserShortcut> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.UserShortcut> GetAllByUserID(Guid userID)
        {
            string sql = "SELECT * FROM UserShortcut WHERE UserID=@UserID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier) {
                Value = userID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.UserShortcut> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM UserShortcut";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public DataTable GetDataTableByUserID(Guid userID)
        {
            string sql = "SELECT * FROM UserShortcut WHERE UserID=@UserID ORDER BY Sort";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier) {
                Value = userID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.GetDataTable(sql, parameter);
        }

        public int Update(YJ.Data.Model.UserShortcut model)
        {
            string sql = "UPDATE UserShortcut SET \r\n\t\t\t\tMenuID=@MenuID,UserID=@UserID,Sort=@Sort\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[4];
            SqlParameter parameter1 = new SqlParameter("@MenuID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.MenuID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.UserID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[3] = parameter4;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

