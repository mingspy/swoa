namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class MenuUser : IMenuUser
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.MenuUser model)
        {
            string sql = "INSERT INTO MenuUser\r\n\t\t\t\t(ID,MenuID,SubPageID,Organizes,Users,Buttons) \r\n\t\t\t\tVALUES(:ID,:MenuID,:SubPageID,:Organizes,:Users,:Buttons)";
            OracleParameter[] parameterArray1 = new OracleParameter[6];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":MenuID", OracleDbType.Varchar2) {
                Value = model.MenuID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":SubPageID", OracleDbType.Varchar2) {
                Value = model.SubPageID
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":Organizes", OracleDbType.Varchar2) {
                Value = model.Organizes
            };
            parameterArray1[3] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":Users", OracleDbType.Varchar2) {
                Value = model.Users
            };
            parameterArray1[4] = parameter5;
            parameterArray1[5] = (model.Buttons == null) ? new OracleParameter(":Buttons", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Buttons", OracleDbType.Varchar2) { Value = model.Buttons };
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.MenuUser> DataReaderToList(OracleDataReader dataReader)
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
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM MenuUser WHERE ID=:ID";
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
            string sql = "DELETE FROM MenuUser WHERE MenuID=:MenuID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":MenuID", OracleDbType.Varchar2) {
                Value = menuID.ToString()
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public int DeleteByOrganizes(string organizes)
        {
            string sql = "DELETE FROM MenuUser WHERE Organizes=:Organizes";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":Organizes", OracleDbType.Varchar2) {
                Value = organizes
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.MenuUser Get(Guid id)
        {
            string sql = "SELECT * FROM MenuUser WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.MenuUser> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.MenuUser> GetAll()
        {
            string sql = "SELECT * FROM MenuUser";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
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
            string sql = "UPDATE MenuUser SET \r\n\t\t\t\tMenuID=:MenuID,SubPageID=:SubPageID,Organizes=:Organizes,Users=:Users,Buttons=:Buttons\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[6];
            OracleParameter parameter1 = new OracleParameter(":MenuID", OracleDbType.Varchar2) {
                Value = model.MenuID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":SubPageID", OracleDbType.Varchar2) {
                Value = model.SubPageID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":Organizes", OracleDbType.Varchar2) {
                Value = model.Organizes
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":Users", OracleDbType.Varchar2) {
                Value = model.Users
            };
            parameterArray1[3] = parameter4;
            parameterArray1[4] = (model.Buttons == null) ? new OracleParameter(":Buttons", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Buttons", OracleDbType.Varchar2) { Value = model.Buttons };
            OracleParameter parameter7 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[5] = parameter7;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

