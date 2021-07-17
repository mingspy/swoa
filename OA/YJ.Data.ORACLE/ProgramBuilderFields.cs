namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class ProgramBuilderFields : IProgramBuilderFields
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.ProgramBuilderFields model)
        {
            string sql = "INSERT INTO ProgramBuilderFields\r\n\t\t\t\t(ID,ProgramID,Field,ShowTitle,Align,Width,ShowType,ShowFormat,CustomString,Sort) \r\n\t\t\t\tVALUES(:ID,:ProgramID,:Field,:ShowTitle,:Align,:Width,:ShowType,:ShowFormat,:CustomString,:Sort)";
            OracleParameter[] parameterArray1 = new OracleParameter[10];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":ProgramID", OracleDbType.Varchar2) {
                Value = model.ProgramID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":Field", OracleDbType.Varchar2) {
                Value = model.Field
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":ShowTitle", OracleDbType.Varchar2) {
                Value = model.ShowTitle
            };
            parameterArray1[3] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":Align", OracleDbType.Varchar2) {
                Value = model.Align
            };
            parameterArray1[4] = parameter5;
            parameterArray1[5] = (model.Width == null) ? new OracleParameter(":Width", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Width", OracleDbType.Varchar2) { Value = model.Width };
            OracleParameter parameter8 = new OracleParameter(":ShowType", OracleDbType.Int32) {
                Value = model.ShowType
            };
            parameterArray1[6] = parameter8;
            parameterArray1[7] = (model.ShowFormat == null) ? new OracleParameter(":ShowFormat", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":ShowFormat", OracleDbType.Varchar2) { Value = model.ShowFormat };
            parameterArray1[8] = (model.CustomString == null) ? new OracleParameter(":CustomString", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":CustomString", OracleDbType.Varchar2) { Value = model.CustomString };
            OracleParameter parameter13 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[9] = parameter13;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.ProgramBuilderFields> DataReaderToList(OracleDataReader dataReader)
        {
            List<YJ.Data.Model.ProgramBuilderFields> list = new List<YJ.Data.Model.ProgramBuilderFields>();
            YJ.Data.Model.ProgramBuilderFields item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.ProgramBuilderFields {
                    ID = dataReader.GetString(0).ToGuid(),
                    ProgramID = dataReader.GetString(1).ToGuid()
                };
                if (!dataReader.IsDBNull(2))
                {
                    item.Field = dataReader.GetString(2);
                }
                item.ShowTitle = dataReader.GetString(3);
                item.Align = dataReader.GetString(4);
                if (!dataReader.IsDBNull(5))
                {
                    item.Width = dataReader.GetString(5);
                }
                item.ShowType = dataReader.GetInt32(6);
                if (!dataReader.IsDBNull(7))
                {
                    item.ShowFormat = dataReader.GetString(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    item.CustomString = dataReader.GetString(8);
                }
                item.Sort = dataReader.GetInt32(9);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM ProgramBuilderFields WHERE ID=:ID";
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
            string sql = "DELETE FROM ProgramBuilderFields WHERE ProgramID=:ProgramID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ProgramID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.ProgramBuilderFields Get(Guid id)
        {
            string sql = "SELECT * FROM ProgramBuilderFields WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilderFields> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.ProgramBuilderFields> GetAll()
        {
            string sql = "SELECT * FROM ProgramBuilderFields";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.ProgramBuilderFields> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.ProgramBuilderFields> GetAll(Guid programID)
        {
            string sql = "SELECT * FROM ProgramBuilderFields WHERE ProgramID=:ProgramID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ProgramID", OracleDbType.Varchar2) {
                Value = programID
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilderFields> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM ProgramBuilderFields";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.ProgramBuilderFields model)
        {
            string sql = "UPDATE ProgramBuilderFields SET \r\n\t\t\t\tProgramID=:ProgramID,Field=:Field,ShowTitle=:ShowTitle,Align=:Align,Width=:Width,ShowType=:ShowType,ShowFormat=:ShowFormat,CustomString=:CustomString,Sort=:Sort\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[10];
            OracleParameter parameter1 = new OracleParameter(":ProgramID", OracleDbType.Varchar2) {
                Value = model.ProgramID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Field", OracleDbType.Varchar2) {
                Value = model.Field
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":ShowTitle", OracleDbType.Varchar2) {
                Value = model.ShowTitle
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":Align", OracleDbType.Varchar2) {
                Value = model.Align
            };
            parameterArray1[3] = parameter4;
            parameterArray1[4] = (model.Width == null) ? new OracleParameter(":Width", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Width", OracleDbType.Varchar2) { Value = model.Width };
            OracleParameter parameter7 = new OracleParameter(":ShowType", OracleDbType.Int32) {
                Value = model.ShowType
            };
            parameterArray1[5] = parameter7;
            parameterArray1[6] = (model.ShowFormat == null) ? new OracleParameter(":ShowFormat", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":ShowFormat", OracleDbType.Varchar2) { Value = model.ShowFormat };
            parameterArray1[7] = (model.CustomString == null) ? new OracleParameter(":CustomString", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":CustomString", OracleDbType.Varchar2) { Value = model.CustomString };
            OracleParameter parameter12 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[8] = parameter12;
            OracleParameter parameter13 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[9] = parameter13;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

