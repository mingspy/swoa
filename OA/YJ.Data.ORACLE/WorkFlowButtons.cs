namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class WorkFlowButtons : IWorkFlowButtons
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkFlowButtons model)
        {
            string sql = "INSERT INTO WorkFlowButtons\r\n\t\t\t\t(ID,Title,Ico,Script,Note,Sort) \r\n\t\t\t\tVALUES(:ID,:Title,:Ico,:Script,:Note,:Sort)";
            OracleParameter[] parameterArray1 = new OracleParameter[6];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2, 40) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Title", OracleDbType.NVarchar2, 0x3e8) {
                Value = model.Title
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = (model.Ico == null) ? new OracleParameter(":Ico", OracleDbType.Varchar2, 500) { Value = DBNull.Value } : new OracleParameter(":Ico", OracleDbType.Varchar2, 500) { Value = model.Ico };
            parameterArray1[3] = (model.Script == null) ? new OracleParameter(":Script", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Script", OracleDbType.Clob) { Value = model.Script };
            parameterArray1[4] = (model.Note == null) ? new OracleParameter(":Note", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.Clob) { Value = model.Note };
            OracleParameter parameter9 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[5] = parameter9;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.WorkFlowButtons> DataReaderToList(OracleDataReader dataReader)
        {
            List<YJ.Data.Model.WorkFlowButtons> list = new List<YJ.Data.Model.WorkFlowButtons>();
            YJ.Data.Model.WorkFlowButtons item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.WorkFlowButtons {
                    ID = dataReader.GetString(0).ToGuid(),
                    Title = dataReader.GetString(1)
                };
                if (!dataReader.IsDBNull(2))
                {
                    item.Ico = dataReader.GetString(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    item.Script = dataReader.GetString(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    item.Note = dataReader.GetString(4);
                }
                item.Sort = dataReader.GetInt32(5);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM WorkFlowButtons WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.WorkFlowButtons Get(Guid id)
        {
            string sql = "SELECT * FROM WorkFlowButtons WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowButtons> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkFlowButtons> GetAll()
        {
            string sql = "SELECT * FROM WorkFlowButtons";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowButtons> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM WorkFlowButtons";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int GetMaxSort()
        {
            string sql = "SELECT nvl(MAX(Sort),0)+1 FROM WorkFlowButtons";
            string fieldValue = this.dbHelper.GetFieldValue(sql);
            return (fieldValue.IsInt() ? fieldValue.ToInt() : 1);
        }

        public int Update(YJ.Data.Model.WorkFlowButtons model)
        {
            string sql = "UPDATE WorkFlowButtons SET \r\n\t\t\t\tTitle=:Title,Ico=:Ico,Script=:Script,Note=:Note,Sort=:Sort\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[6];
            OracleParameter parameter1 = new OracleParameter(":Title", OracleDbType.NVarchar2, 0x3e8) {
                Value = model.Title
            };
            parameterArray1[0] = parameter1;
            parameterArray1[1] = (model.Ico == null) ? new OracleParameter(":Ico", OracleDbType.Varchar2, 500) { Value = DBNull.Value } : new OracleParameter(":Ico", OracleDbType.Varchar2, 500) { Value = model.Ico };
            parameterArray1[2] = (model.Script == null) ? new OracleParameter(":Script", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Script", OracleDbType.Clob) { Value = model.Script };
            parameterArray1[3] = (model.Note == null) ? new OracleParameter(":Note", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.Clob) { Value = model.Note };
            OracleParameter parameter8 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[4] = parameter8;
            OracleParameter parameter9 = new OracleParameter(":ID", OracleDbType.Varchar2, 40) {
                Value = model.ID
            };
            parameterArray1[5] = parameter9;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

