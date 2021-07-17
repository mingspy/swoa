namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class Dictionary : IDictionary
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.Dictionary model)
        {
            string sql = "INSERT INTO Dictionary\r\n\t\t\t\t(ID,ParentID,Title,Code,Value,Note,Other,Sort) \r\n\t\t\t\tVALUES(:ID,:ParentID,:Title,:Code,:Value,:Note,:Other,:Sort)";
            OracleParameter[] parameterArray1 = new OracleParameter[8];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2, 40) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":ParentID", OracleDbType.Varchar2, 40) {
                Value = model.ParentID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":Title", OracleDbType.NVarchar2) {
                Value = model.Title
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = (model.Code == null) ? new OracleParameter(":Code", OracleDbType.Varchar2, 500) { Value = DBNull.Value } : new OracleParameter(":Code", OracleDbType.Varchar2, 500) { Value = model.Code };
            parameterArray1[4] = (model.Value == null) ? new OracleParameter(":Value", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Value", OracleDbType.Clob) { Value = model.Value };
            parameterArray1[5] = (model.Note == null) ? new OracleParameter(":Note", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.Clob) { Value = model.Note };
            parameterArray1[6] = (model.Other == null) ? new OracleParameter(":Other", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Other", OracleDbType.Clob) { Value = model.Other };
            OracleParameter parameter12 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[7] = parameter12;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.Dictionary> DataReaderToList(OracleDataReader dataReader)
        {
            List<YJ.Data.Model.Dictionary> list = new List<YJ.Data.Model.Dictionary>();
            YJ.Data.Model.Dictionary item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.Dictionary {
                    ID = dataReader.GetString(0).ToGuid(),
                    ParentID = dataReader.GetString(1).ToGuid(),
                    Title = dataReader.GetString(2)
                };
                if (!dataReader.IsDBNull(3))
                {
                    item.Code = dataReader.GetString(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    item.Value = dataReader.GetString(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    item.Note = dataReader.GetString(5);
                }
                if (!dataReader.IsDBNull(6))
                {
                    item.Other = dataReader.GetString(6);
                }
                item.Sort = dataReader.GetInt32(7);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM Dictionary WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.Dictionary Get(Guid id)
        {
            string sql = "SELECT * FROM Dictionary WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Dictionary> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.Dictionary> GetAll()
        {
            string sql = "SELECT * FROM Dictionary";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Dictionary> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public YJ.Data.Model.Dictionary GetByCode(string code)
        {
            string sql = "SELECT * FROM Dictionary WHERE Code=:Code";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":Code", OracleDbType.Varchar2) {
                Value = code
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Dictionary> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.Dictionary> GetChilds(Guid id)
        {
            string sql = "SELECT * FROM Dictionary WHERE ParentID=:ParentID ORDER BY Sort";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ParentID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Dictionary> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.Dictionary> GetChilds(string code)
        {
            string sql = "SELECT * FROM Dictionary WHERE ParentID=(SELECT ID FROM Dictionary WHERE Code=:Code) ORDER BY Sort";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":Code", OracleDbType.Varchar2) {
                Value = code
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Dictionary> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM Dictionary";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int GetMaxSort(Guid id)
        {
            int num;
            string sql = "SELECT MAX(Sort)+1 FROM Dictionary WHERE ParentID=:ParentID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ParentID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return (this.dbHelper.GetFieldValue(sql, parameter).IsInt(out num) ? num : 1);
        }

        public YJ.Data.Model.Dictionary GetParent(Guid id)
        {
            string sql = "SELECT * FROM Dictionary WHERE ID=(SELECT ParentID FROM Dictionary WHERE ID=:ID)";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Dictionary> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public YJ.Data.Model.Dictionary GetRoot()
        {
            string sql = "SELECT * FROM Dictionary WHERE ParentID=:ParentID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ParentID", OracleDbType.Varchar2) {
                Value = Guid.Empty
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Dictionary> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public bool HasChilds(Guid id)
        {
            string sql = "SELECT ID FROM Dictionary WHERE ParentID=:ParentID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ParentID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            bool hasRows = dataReader.HasRows;
            dataReader.Close();
            return hasRows;
        }

        public int Update(YJ.Data.Model.Dictionary model)
        {
            string sql = "UPDATE Dictionary SET \r\n\t\t\t\tParentID=:ParentID,Title=:Title,Code=:Code,Value=:Value,Note=:Note,Other=:Other,Sort=:Sort\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[8];
            OracleParameter parameter1 = new OracleParameter(":ParentID", OracleDbType.Varchar2, 40) {
                Value = model.ParentID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Title", OracleDbType.NVarchar2) {
                Value = model.Title
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = (model.Code == null) ? new OracleParameter(":Code", OracleDbType.Varchar2, 500) { Value = DBNull.Value } : new OracleParameter(":Code", OracleDbType.Varchar2, 500) { Value = model.Code };
            parameterArray1[3] = (model.Value == null) ? new OracleParameter(":Value", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Value", OracleDbType.Clob) { Value = model.Value };
            parameterArray1[4] = (model.Note == null) ? new OracleParameter(":Note", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.Clob) { Value = model.Note };
            parameterArray1[5] = (model.Other == null) ? new OracleParameter(":Other", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Other", OracleDbType.Clob) { Value = model.Other };
            OracleParameter parameter11 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[6] = parameter11;
            OracleParameter parameter12 = new OracleParameter(":ID", OracleDbType.Varchar2, 40) {
                Value = model.ID
            };
            parameterArray1[7] = parameter12;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public int UpdateSort(Guid id, int sort)
        {
            string sql = "UPDATE Dictionary SET Sort=:Sort WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[2];
            OracleParameter parameter1 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = sort
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[1] = parameter2;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

