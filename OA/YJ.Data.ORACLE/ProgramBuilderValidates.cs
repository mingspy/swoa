namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class ProgramBuilderValidates : IProgramBuilderValidates
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.ProgramBuilderValidates model)
        {
            string sql = "INSERT INTO ProgramBuilderValidates\r\n\t\t\t\t(ID,ProgramID,TableName,FieldName,FieldNote,Validate1) \r\n\t\t\t\tVALUES(:ID,:ProgramID,:TableName,:FieldName,:FieldNote,:Validate1)";
            OracleParameter[] parameterArray1 = new OracleParameter[6];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":ProgramID", OracleDbType.Varchar2) {
                Value = model.ProgramID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":TableName", OracleDbType.Varchar2) {
                Value = model.TableName
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":FieldName", OracleDbType.Varchar2) {
                Value = model.FieldName
            };
            parameterArray1[3] = parameter4;
            parameterArray1[4] = (model.FieldNote == null) ? new OracleParameter(":FieldNote", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":FieldNote", OracleDbType.Varchar2) { Value = model.FieldNote };
            OracleParameter parameter7 = new OracleParameter(":Validate1", OracleDbType.Int32) {
                Value = model.Validate
            };
            parameterArray1[5] = parameter7;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.ProgramBuilderValidates> DataReaderToList(OracleDataReader dataReader)
        {
            List<YJ.Data.Model.ProgramBuilderValidates> list = new List<YJ.Data.Model.ProgramBuilderValidates>();
            YJ.Data.Model.ProgramBuilderValidates item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.ProgramBuilderValidates {
                    ID = dataReader.GetString(0).ToGuid(),
                    ProgramID = dataReader.GetString(1).ToGuid(),
                    TableName = dataReader.GetString(2),
                    FieldName = dataReader.GetString(3)
                };
                if (!dataReader.IsDBNull(4))
                {
                    item.FieldNote = dataReader.GetString(4);
                }
                item.Validate = dataReader.GetInt32(5);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM ProgramBuilderValidates WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public int DeleteByProgramID(Guid id)
        {
            string sql = "DELETE FROM ProgramBuilderValidates WHERE ProgramID=:ProgramID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ProgramID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.ProgramBuilderValidates Get(Guid id)
        {
            string sql = "SELECT * FROM ProgramBuilderValidates WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilderValidates> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.ProgramBuilderValidates> GetAll()
        {
            string sql = "SELECT * FROM ProgramBuilderValidates";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.ProgramBuilderValidates> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.ProgramBuilderValidates> GetAll(Guid programID)
        {
            string sql = "SELECT * FROM ProgramBuilderValidates WHERE ProgramID=:ProgramID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ProgramID", OracleDbType.Varchar2) {
                Value = programID
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilderValidates> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM ProgramBuilderValidates";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.ProgramBuilderValidates model)
        {
            string sql = "UPDATE ProgramBuilderValidates SET \r\n\t\t\t\tProgramID=:ProgramID,TableName=:TableName,FieldName=:FieldName,FieldNote=:FieldNote,Validate1=:Validate1\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[6];
            OracleParameter parameter1 = new OracleParameter(":ProgramID", OracleDbType.Varchar2) {
                Value = model.ProgramID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":TableName", OracleDbType.Varchar2) {
                Value = model.TableName
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":FieldName", OracleDbType.Varchar2) {
                Value = model.FieldName
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = (model.FieldNote == null) ? new OracleParameter(":FieldNote", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":FieldNote", OracleDbType.Varchar2) { Value = model.FieldNote };
            OracleParameter parameter6 = new OracleParameter(":Validate1", OracleDbType.Int32) {
                Value = model.Validate
            };
            parameterArray1[4] = parameter6;
            OracleParameter parameter7 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[5] = parameter7;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

