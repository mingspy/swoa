namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class AppLibrarySubPages : IAppLibrarySubPages
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.AppLibrarySubPages model)
        {
            string sql = "INSERT INTO AppLibrarySubPages\r\n\t\t\t\t(ID,AppLibraryID,Name,Address,Ico,Sort,Note) \r\n\t\t\t\tVALUES(:ID,:AppLibraryID,:Name,:Address,:Ico,:Sort,:Note)";
            OracleParameter[] parameterArray1 = new OracleParameter[7];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":AppLibraryID", OracleDbType.Varchar2) {
                Value = model.AppLibraryID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":Name", OracleDbType.Varchar2) {
                Value = model.Name
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":Address", OracleDbType.Varchar2) {
                Value = model.Address
            };
            parameterArray1[3] = parameter4;
            parameterArray1[4] = (model.Ico == null) ? new OracleParameter(":Ico", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Ico", OracleDbType.Varchar2) { Value = model.Ico };
            OracleParameter parameter7 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[5] = parameter7;
            parameterArray1[6] = (model.Note == null) ? new OracleParameter(":Note", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.Varchar2) { Value = model.Note };
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.AppLibrarySubPages> DataReaderToList(OracleDataReader dataReader)
        {
            List<YJ.Data.Model.AppLibrarySubPages> list = new List<YJ.Data.Model.AppLibrarySubPages>();
            YJ.Data.Model.AppLibrarySubPages item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.AppLibrarySubPages {
                    ID = dataReader.GetString(0).ToGuid(),
                    AppLibraryID = dataReader.GetString(1).ToGuid(),
                    Name = dataReader.GetString(2),
                    Address = dataReader.GetString(3)
                };
                if (!dataReader.IsDBNull(4))
                {
                    item.Ico = dataReader.GetString(4);
                }
                item.Sort = dataReader.GetInt32(5);
                if (!dataReader.IsDBNull(6))
                {
                    item.Note = dataReader.GetString(6);
                }
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM AppLibrarySubPages WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public int DeleteByAppID(Guid id)
        {
            string sql = "DELETE FROM AppLibrarySubPages WHERE AppLibraryID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.AppLibrarySubPages Get(Guid id)
        {
            string sql = "SELECT * FROM AppLibrarySubPages WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.AppLibrarySubPages> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.AppLibrarySubPages> GetAll()
        {
            string sql = "SELECT * FROM AppLibrarySubPages";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.AppLibrarySubPages> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.AppLibrarySubPages> GetAllByAppID(Guid id)
        {
            string sql = "SELECT * FROM AppLibrarySubPages WHERE AppLibraryID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.AppLibrarySubPages> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM AppLibrarySubPages";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.AppLibrarySubPages model)
        {
            string sql = "UPDATE AppLibrarySubPages SET \r\n\t\t\t\tAppLibraryID=:AppLibraryID,Name=:Name,Address=:Address,Ico=:Ico,Sort=:Sort,Note=:Note\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[7];
            OracleParameter parameter1 = new OracleParameter(":AppLibraryID", OracleDbType.Varchar2) {
                Value = model.AppLibraryID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Name", OracleDbType.Varchar2) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":Address", OracleDbType.Varchar2) {
                Value = model.Address
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = (model.Ico == null) ? new OracleParameter(":Ico", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Ico", OracleDbType.Varchar2) { Value = model.Ico };
            OracleParameter parameter6 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[4] = parameter6;
            parameterArray1[5] = (model.Note == null) ? new OracleParameter(":Note", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.Varchar2) { Value = model.Note };
            OracleParameter parameter9 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[6] = parameter9;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

