namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class UserShortcut : IUserShortcut
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.UserShortcut model)
        {
            string sql = "INSERT INTO UserShortcut\r\n\t\t\t\t(ID,MenuID,UserID,Sort) \r\n\t\t\t\tVALUES(:ID,:MenuID,:UserID,:Sort)";
            OracleParameter[] parameterArray1 = new OracleParameter[4];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":MenuID", OracleDbType.Varchar2) {
                Value = model.MenuID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":UserID", OracleDbType.Varchar2) {
                Value = model.UserID
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[3] = parameter4;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.UserShortcut> DataReaderToList(OracleDataReader dataReader)
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
            string sql = "DELETE FROM UserShortcut WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public int DeleteByMenuID(Guid menuID)
        {
            string sql = "DELETE FROM UserShortcut WHERE MenuID=:MenuID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":MenuID", OracleDbType.Varchar2) {
                Value = menuID.ToString()
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public int DeleteByUserID(Guid userID)
        {
            string sql = "DELETE FROM UserShortcut WHERE UserID=:UserID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":UserID", OracleDbType.Varchar2) {
                Value = userID
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.UserShortcut Get(Guid id)
        {
            string sql = "SELECT * FROM UserShortcut WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.UserShortcut> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.UserShortcut> GetAll()
        {
            string sql = "SELECT * FROM UserShortcut";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.UserShortcut> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.UserShortcut> GetAllByUserID(Guid userID)
        {
            string sql = "SELECT * FROM UserShortcut WHERE UserID=:UserID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":UserID", OracleDbType.Varchar2) {
                Value = userID
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
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
            string sql = "SELECT * FROM UserShortcut WHERE UserID=:UserID ORDER BY Sort";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":UserID", OracleDbType.Varchar2) {
                Value = userID
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.GetDataTable(sql, parameter);
        }

        public int Update(YJ.Data.Model.UserShortcut model)
        {
            string sql = "UPDATE UserShortcut SET \r\n\t\t\t\tMenuID=:MenuID,UserID=:UserID,Sort=:Sort\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[4];
            OracleParameter parameter1 = new OracleParameter(":MenuID", OracleDbType.Varchar2) {
                Value = model.MenuID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":UserID", OracleDbType.Varchar2) {
                Value = model.UserID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[3] = parameter4;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

