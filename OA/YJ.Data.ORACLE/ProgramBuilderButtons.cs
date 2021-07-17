namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class ProgramBuilderButtons : IProgramBuilderButtons
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.ProgramBuilderButtons model)
        {
            string sql = "INSERT INTO ProgramBuilderButtons\r\n\t\t\t\t(ID,ProgramID,ButtonID,ButtonName,ClientScript,Ico,ShowType,Sort,IsValidateShow) \r\n\t\t\t\tVALUES(:ID,:ProgramID,:ButtonID,:ButtonName,:ClientScript,:Ico,:ShowType,:Sort,:IsValidateShow)";
            OracleParameter[] parameterArray1 = new OracleParameter[9];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":ProgramID", OracleDbType.Varchar2) {
                Value = model.ProgramID
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = !model.ButtonID.HasValue ? new OracleParameter(":ButtonID", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":ButtonID", OracleDbType.Varchar2) { Value = model.ButtonID };
            OracleParameter parameter5 = new OracleParameter(":ButtonName", OracleDbType.Varchar2) {
                Value = model.ButtonName
            };
            parameterArray1[3] = parameter5;
            parameterArray1[4] = (model.ClientScript == null) ? new OracleParameter(":ClientScript", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":ClientScript", OracleDbType.Varchar2) { Value = model.ClientScript };
            parameterArray1[5] = (model.Ico == null) ? new OracleParameter(":Ico", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Ico", OracleDbType.Varchar2) { Value = model.Ico };
            OracleParameter parameter10 = new OracleParameter(":ShowType", OracleDbType.Int32) {
                Value = model.ShowType
            };
            parameterArray1[6] = parameter10;
            OracleParameter parameter11 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[7] = parameter11;
            OracleParameter parameter12 = new OracleParameter(":IsValidateShow", OracleDbType.Int32) {
                Value = model.IsValidateShow
            };
            parameterArray1[8] = parameter12;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.ProgramBuilderButtons> DataReaderToList(OracleDataReader dataReader)
        {
            List<YJ.Data.Model.ProgramBuilderButtons> list = new List<YJ.Data.Model.ProgramBuilderButtons>();
            YJ.Data.Model.ProgramBuilderButtons item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.ProgramBuilderButtons {
                    ID = dataReader.GetString(0).ToGuid(),
                    ProgramID = dataReader.GetString(1).ToGuid()
                };
                if (!dataReader.IsDBNull(2))
                {
                    item.ButtonID = new Guid?(dataReader.GetString(2).ToGuid());
                }
                item.ButtonName = dataReader.GetString(3);
                if (!dataReader.IsDBNull(4))
                {
                    item.ClientScript = dataReader.GetString(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    item.Ico = dataReader.GetString(5);
                }
                item.ShowType = dataReader.GetInt32(6);
                item.Sort = dataReader.GetInt32(7);
                item.IsValidateShow = dataReader.GetInt32(8);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM ProgramBuilderButtons WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.ProgramBuilderButtons Get(Guid id)
        {
            string sql = "SELECT * FROM ProgramBuilderButtons WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilderButtons> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.ProgramBuilderButtons> GetAll()
        {
            string sql = "SELECT * FROM ProgramBuilderButtons";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.ProgramBuilderButtons> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.ProgramBuilderButtons> GetAll(Guid id)
        {
            string sql = "SELECT * FROM ProgramBuilderButtons WHERE ProgramID=:ProgramID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ProgramID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilderButtons> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM ProgramBuilderButtons";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.ProgramBuilderButtons model)
        {
            string sql = "UPDATE ProgramBuilderButtons SET \r\n\t\t\t\tProgramID=:ProgramID,ButtonID=:ButtonID,ButtonName=:ButtonName,ClientScript=:ClientScript,Ico=:Ico,ShowType=:ShowType,Sort=:Sort,IsValidateShow=:IsValidateShow\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[9];
            OracleParameter parameter1 = new OracleParameter(":ProgramID", OracleDbType.Varchar2) {
                Value = model.ProgramID
            };
            parameterArray1[0] = parameter1;
            parameterArray1[1] = !model.ButtonID.HasValue ? new OracleParameter(":ButtonID", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":ButtonID", OracleDbType.Varchar2) { Value = model.ButtonID };
            OracleParameter parameter4 = new OracleParameter(":ButtonName", OracleDbType.Varchar2) {
                Value = model.ButtonName
            };
            parameterArray1[2] = parameter4;
            parameterArray1[3] = (model.ClientScript == null) ? new OracleParameter(":ClientScript", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":ClientScript", OracleDbType.Varchar2) { Value = model.ClientScript };
            parameterArray1[4] = (model.Ico == null) ? new OracleParameter(":Ico", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Ico", OracleDbType.Varchar2) { Value = model.Ico };
            OracleParameter parameter9 = new OracleParameter(":ShowType", OracleDbType.Int32) {
                Value = model.ShowType
            };
            parameterArray1[5] = parameter9;
            OracleParameter parameter10 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[6] = parameter10;
            OracleParameter parameter11 = new OracleParameter(":IsValidateShow", OracleDbType.Int32) {
                Value = model.IsValidateShow
            };
            parameterArray1[7] = parameter11;
            OracleParameter parameter12 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[8] = parameter12;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

