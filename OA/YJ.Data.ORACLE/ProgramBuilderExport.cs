﻿namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class ProgramBuilderExport : IProgramBuilderExport
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.ProgramBuilderExport model)
        {
            string sql = "INSERT INTO programbuilderexport\r\n\t\t\t\t(ID,ProgramID,Field,ShowTitle,Align,Width,ShowType,DataType,ShowFormat,CustomString,Sort) \r\n\t\t\t\tVALUES(:ID,:ProgramID,:Field,:ShowTitle,:Align,:Width,:ShowType,:DataType,:ShowFormat,:CustomString,:Sort)";
            OracleParameter[] parameterArray1 = new OracleParameter[11];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":ProgramID", OracleDbType.Varchar2, 0x24) {
                Value = model.ProgramID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":Field", OracleDbType.Blob, -1) {
                Value = model.Field
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = (model.ShowTitle == null) ? new OracleParameter(":ShowTitle", OracleDbType.Blob, -1) { Value = DBNull.Value } : new OracleParameter(":ShowTitle", OracleDbType.Blob, -1) { Value = model.ShowTitle };
            OracleParameter parameter6 = new OracleParameter(":Align", OracleDbType.Int32, 11) {
                Value = model.Align
            };
            parameterArray1[4] = parameter6;
            parameterArray1[5] = !model.Width.HasValue ? new OracleParameter(":Width", OracleDbType.Int32, 11) { Value = DBNull.Value } : new OracleParameter(":Width", OracleDbType.Int32, 11) { Value = model.Width };
            parameterArray1[6] = !model.ShowType.HasValue ? new OracleParameter(":ShowType", OracleDbType.Int32, 11) { Value = DBNull.Value } : new OracleParameter(":ShowType", OracleDbType.Int32, 11) { Value = model.ShowType };
            parameterArray1[7] = !model.DataType.HasValue ? new OracleParameter(":DataType", OracleDbType.Int32, 11) { Value = DBNull.Value } : new OracleParameter(":DataType", OracleDbType.Int32, 11) { Value = model.DataType };
            parameterArray1[8] = (model.ShowFormat == null) ? new OracleParameter(":ShowFormat", OracleDbType.Varchar2, 50) { Value = DBNull.Value } : new OracleParameter(":ShowFormat", OracleDbType.Varchar2, 50) { Value = model.ShowFormat };
            parameterArray1[9] = (model.CustomString == null) ? new OracleParameter(":CustomString", OracleDbType.Blob, -1) { Value = DBNull.Value } : new OracleParameter(":CustomString", OracleDbType.Blob, -1) { Value = model.CustomString };
            OracleParameter parameter17 = new OracleParameter(":Sort", OracleDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[10] = parameter17;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.ProgramBuilderExport> DataReaderToList(OracleDataReader dataReader)
        {
            List<YJ.Data.Model.ProgramBuilderExport> list = new List<YJ.Data.Model.ProgramBuilderExport>();
            YJ.Data.Model.ProgramBuilderExport item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.ProgramBuilderExport {
                    ID = dataReader.GetString(0).ToGuid(),
                    ProgramID = dataReader.GetString(1).ToGuid(),
                    Field = dataReader.GetString(2)
                };
                if (!dataReader.IsDBNull(3))
                {
                    item.ShowTitle = dataReader.GetString(3);
                }
                item.Align = dataReader.GetInt32(4);
                if (!dataReader.IsDBNull(5))
                {
                    item.Width = new int?(dataReader.GetInt32(5));
                }
                if (!dataReader.IsDBNull(6))
                {
                    item.ShowType = new int?(dataReader.GetInt32(6));
                }
                if (!dataReader.IsDBNull(7))
                {
                    item.DataType = new int?(dataReader.GetInt32(7));
                }
                if (!dataReader.IsDBNull(8))
                {
                    item.ShowFormat = dataReader.GetString(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    item.CustomString = dataReader.GetString(9);
                }
                item.Sort = dataReader.GetInt32(10);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM programbuilderexport WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.ProgramBuilderExport Get(Guid id)
        {
            string sql = "SELECT * FROM programbuilderexport WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilderExport> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.ProgramBuilderExport> GetAll()
        {
            string sql = "SELECT * FROM programbuilderexport";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.ProgramBuilderExport> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.ProgramBuilderExport> GetAll(Guid programID)
        {
            string sql = "SELECT * FROM ProgramBuilderExport WHERE ProgramID=:ProgramID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ProgramID", OracleDbType.Varchar2) {
                Value = programID
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilderExport> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM programbuilderexport";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.ProgramBuilderExport model)
        {
            string sql = "UPDATE programbuilderexport SET \r\n\t\t\t\tProgramID=:ProgramID,Field=:Field,ShowTitle=:ShowTitle,Align=:Align,Width=:Width,ShowType=:ShowType,DataType=:DataType,ShowFormat=:ShowFormat,CustomString=:CustomString,Sort=:Sort\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[11];
            OracleParameter parameter1 = new OracleParameter(":ProgramID", OracleDbType.Varchar2, 0x24) {
                Value = model.ProgramID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Field", OracleDbType.Blob, -1) {
                Value = model.Field
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = (model.ShowTitle == null) ? new OracleParameter(":ShowTitle", OracleDbType.Blob, -1) { Value = DBNull.Value } : new OracleParameter(":ShowTitle", OracleDbType.Blob, -1) { Value = model.ShowTitle };
            OracleParameter parameter5 = new OracleParameter(":Align", OracleDbType.Int32, 11) {
                Value = model.Align
            };
            parameterArray1[3] = parameter5;
            parameterArray1[4] = !model.Width.HasValue ? new OracleParameter(":Width", OracleDbType.Int32, 11) { Value = DBNull.Value } : new OracleParameter(":Width", OracleDbType.Int32, 11) { Value = model.Width };
            parameterArray1[5] = !model.ShowType.HasValue ? new OracleParameter(":ShowType", OracleDbType.Int32, 11) { Value = DBNull.Value } : new OracleParameter(":ShowType", OracleDbType.Int32, 11) { Value = model.ShowType };
            parameterArray1[6] = !model.DataType.HasValue ? new OracleParameter(":DataType", OracleDbType.Int32, 11) { Value = DBNull.Value } : new OracleParameter(":DataType", OracleDbType.Int32, 11) { Value = model.DataType };
            parameterArray1[7] = (model.ShowFormat == null) ? new OracleParameter(":ShowFormat", OracleDbType.Varchar2, 50) { Value = DBNull.Value } : new OracleParameter(":ShowFormat", OracleDbType.Varchar2, 50) { Value = model.ShowFormat };
            parameterArray1[8] = (model.CustomString == null) ? new OracleParameter(":CustomString", OracleDbType.Blob, -1) { Value = DBNull.Value } : new OracleParameter(":CustomString", OracleDbType.Blob, -1) { Value = model.CustomString };
            OracleParameter parameter16 = new OracleParameter(":Sort", OracleDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[9] = parameter16;
            OracleParameter parameter17 = new OracleParameter(":ID", OracleDbType.Varchar2, 0x24) {
                Value = model.ID
            };
            parameterArray1[10] = parameter17;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

