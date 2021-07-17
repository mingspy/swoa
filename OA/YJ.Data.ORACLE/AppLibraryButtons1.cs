namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class AppLibraryButtons1 : IAppLibraryButtons1
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.AppLibraryButtons1 model)
        {
            string sql = "INSERT INTO AppLibraryButtons1\r\n\t\t\t\t(ID,AppLibraryID,ButtonID,Name,Events,Ico,Sort,Type,ShowType,IsValidateShow) \r\n\t\t\t\tVALUES(:ID,:AppLibraryID,:ButtonID,:Name,:Events,:Ico,:Sort,:Type,:ShowType,:IsValidateShow)";
            OracleParameter[] parameterArray1 = new OracleParameter[10];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":AppLibraryID", OracleDbType.Varchar2) {
                Value = model.AppLibraryID
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = !model.ButtonID.HasValue ? new OracleParameter(":ButtonID", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":ButtonID", OracleDbType.Varchar2) { Value = model.ButtonID };
            OracleParameter parameter5 = new OracleParameter(":Name", OracleDbType.Varchar2) {
                Value = model.Name
            };
            parameterArray1[3] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":Events", OracleDbType.Varchar2) {
                Value = model.Events
            };
            parameterArray1[4] = parameter6;
            OracleParameter parameter7 = new OracleParameter(":Ico", OracleDbType.Varchar2) {
                Value = model.Ico
            };
            parameterArray1[5] = parameter7;
            OracleParameter parameter8 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[6] = parameter8;
            OracleParameter parameter9 = new OracleParameter(":Type", OracleDbType.Int32) {
                Value = model.Type
            };
            parameterArray1[7] = parameter9;
            OracleParameter parameter10 = new OracleParameter(":ShowType", OracleDbType.Int32) {
                Value = model.ShowType
            };
            parameterArray1[8] = parameter10;
            OracleParameter parameter11 = new OracleParameter(":IsValidateShow", OracleDbType.Int32) {
                Value = model.IsValidateShow
            };
            parameterArray1[9] = parameter11;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.AppLibraryButtons1> DataReaderToList(OracleDataReader dataReader)
        {
            List<YJ.Data.Model.AppLibraryButtons1> list = new List<YJ.Data.Model.AppLibraryButtons1>();
            YJ.Data.Model.AppLibraryButtons1 item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.AppLibraryButtons1 {
                    ID = dataReader.GetString(0).ToGuid(),
                    AppLibraryID = dataReader.GetString(1).ToGuid()
                };
                if (!dataReader.IsDBNull(2))
                {
                    item.ButtonID = new Guid?(dataReader.GetString(2).ToGuid());
                }
                item.Name = dataReader.GetString(3);
                item.Events = dataReader.GetString(4);
                if (!dataReader.IsDBNull(5))
                {
                    item.Ico = dataReader.GetString(5);
                }
                item.Sort = dataReader.GetInt32(6);
                item.Type = dataReader.GetInt32(7);
                item.ShowType = dataReader.GetInt32(8);
                item.IsValidateShow = dataReader.GetInt32(9);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM AppLibraryButtons1 WHERE ID=:ID";
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
            string sql = "DELETE FROM AppLibraryButtons1 WHERE AppLibraryID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.AppLibraryButtons1 Get(Guid id)
        {
            string sql = "SELECT * FROM AppLibraryButtons1 WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.AppLibraryButtons1> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.AppLibraryButtons1> GetAll()
        {
            string sql = "SELECT * FROM AppLibraryButtons1";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.AppLibraryButtons1> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.AppLibraryButtons1> GetAllByAppID(Guid id)
        {
            string sql = "SELECT * FROM AppLibraryButtons1 WHERE AppLibraryID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.AppLibraryButtons1> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM AppLibraryButtons1";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.AppLibraryButtons1 model)
        {
            string sql = "UPDATE AppLibraryButtons1 SET \r\n\t\t\t\tAppLibraryID=:AppLibraryID,ButtonID=:ButtonID,Name=:Name,Events=:Events,Ico=:Ico,Sort=:Sort,Type=:Type,ShowType=:ShowType,IsValidateShow=:IsValidateShow\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[10];
            OracleParameter parameter1 = new OracleParameter(":AppLibraryID", OracleDbType.Varchar2) {
                Value = model.AppLibraryID
            };
            parameterArray1[0] = parameter1;
            parameterArray1[1] = !model.ButtonID.HasValue ? new OracleParameter(":ButtonID", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":ButtonID", OracleDbType.Varchar2) { Value = model.ButtonID };
            OracleParameter parameter4 = new OracleParameter(":Name", OracleDbType.Varchar2) {
                Value = model.Name
            };
            parameterArray1[2] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":Events", OracleDbType.Varchar2) {
                Value = model.Events
            };
            parameterArray1[3] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":Ico", OracleDbType.Varchar2) {
                Value = model.Ico
            };
            parameterArray1[4] = parameter6;
            OracleParameter parameter7 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[5] = parameter7;
            OracleParameter parameter8 = new OracleParameter(":Type", OracleDbType.Int32) {
                Value = model.Type
            };
            parameterArray1[6] = parameter8;
            OracleParameter parameter9 = new OracleParameter(":ShowType", OracleDbType.Int32) {
                Value = model.ShowType
            };
            parameterArray1[7] = parameter9;
            OracleParameter parameter10 = new OracleParameter(":IsValidateShow", OracleDbType.Int32) {
                Value = model.IsValidateShow
            };
            parameterArray1[8] = parameter10;
            OracleParameter parameter11 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[9] = parameter11;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

