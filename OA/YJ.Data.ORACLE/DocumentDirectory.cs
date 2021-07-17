namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class DocumentDirectory : IDocumentDirectory
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.DocumentDirectory model)
        {
            string sql = "INSERT INTO DocumentDirectory\r\n\t\t\t\t(ID,ParentID,Name,ReadUsers,ManageUsers,PublishUsers,Sort) \r\n\t\t\t\tVALUES(:ID,:ParentID,:Name,:ReadUsers,:ManageUsers,:PublishUsers,:Sort)";
            OracleParameter[] parameterArray1 = new OracleParameter[7];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":ParentID", OracleDbType.Varchar2) {
                Value = model.ParentID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":Name", OracleDbType.Varchar2) {
                Value = model.Name
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = (model.ReadUsers == null) ? new OracleParameter(":ReadUsers", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":ReadUsers", OracleDbType.Varchar2) { Value = model.ReadUsers };
            parameterArray1[4] = (model.ManageUsers == null) ? new OracleParameter(":ManageUsers", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":ManageUsers", OracleDbType.Varchar2) { Value = model.ManageUsers };
            parameterArray1[5] = (model.PublishUsers == null) ? new OracleParameter(":PublishUsers", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":PublishUsers", OracleDbType.Varchar2) { Value = model.PublishUsers };
            OracleParameter parameter10 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[6] = parameter10;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.DocumentDirectory> DataReaderToList(OracleDataReader dataReader)
        {
            List<YJ.Data.Model.DocumentDirectory> list = new List<YJ.Data.Model.DocumentDirectory>();
            YJ.Data.Model.DocumentDirectory item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.DocumentDirectory {
                    ID = dataReader.GetString(0).ToGuid(),
                    ParentID = dataReader.GetString(1).ToGuid(),
                    Name = dataReader.GetString(2)
                };
                if (!dataReader.IsDBNull(3))
                {
                    item.ReadUsers = dataReader.GetString(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    item.ManageUsers = dataReader.GetString(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    item.PublishUsers = dataReader.GetString(5);
                }
                item.Sort = dataReader.GetInt32(6);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM DocumentDirectory WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.DocumentDirectory Get(Guid id)
        {
            string sql = "SELECT * FROM DocumentDirectory WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.DocumentDirectory> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.DocumentDirectory> GetAll()
        {
            string sql = "SELECT * FROM DocumentDirectory";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.DocumentDirectory> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.DocumentDirectory> GetChilds(Guid id)
        {
            string sql = "SELECT * FROM DocumentDirectory WHERE ParentID=:ParentID ORDER BY Sort";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ParentID", OracleDbType.Varchar2) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.DocumentDirectory> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM DocumentDirectory";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int GetMaxSort(Guid dirID)
        {
            string sql = "SELECT MAX(Sort) Sort FROM DocumentDirectory WHERE ParentID=:ParentID ";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ParentID", OracleDbType.Varchar2) {
                Value = dirID.ToString()
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return (this.dbHelper.GetFieldValue(sql, parameter).ToInt(0) + 5);
        }

        public int Update(YJ.Data.Model.DocumentDirectory model)
        {
            string sql = "UPDATE DocumentDirectory SET \r\n\t\t\t\tParentID=:ParentID,Name=:Name,ReadUsers=:ReadUsers,ManageUsers=:ManageUsers,PublishUsers=:PublishUsers,Sort=:Sort\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[7];
            OracleParameter parameter1 = new OracleParameter(":ParentID", OracleDbType.Varchar2) {
                Value = model.ParentID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Name", OracleDbType.Varchar2) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = (model.ReadUsers == null) ? new OracleParameter(":ReadUsers", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":ReadUsers", OracleDbType.Varchar2) { Value = model.ReadUsers };
            parameterArray1[3] = (model.ManageUsers == null) ? new OracleParameter(":ManageUsers", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":ManageUsers", OracleDbType.Varchar2) { Value = model.ManageUsers };
            parameterArray1[4] = (model.PublishUsers == null) ? new OracleParameter(":PublishUsers", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":PublishUsers", OracleDbType.Varchar2) { Value = model.PublishUsers };
            OracleParameter parameter9 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[5] = parameter9;
            OracleParameter parameter10 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[6] = parameter10;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

