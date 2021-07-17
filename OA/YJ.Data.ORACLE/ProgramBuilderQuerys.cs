namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class ProgramBuilderQuerys : IProgramBuilderQuerys
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.ProgramBuilderQuerys model)
        {
            string sql = "INSERT INTO ProgramBuilderQuerys\r\n\t\t\t\t(ID,ProgramID,Field,ShowTitle,Operators,ControlName,InputType,Width,Sort,DataSource,DataSourceString,DataLinkID,IsQueryUsers,Value) \r\n\t\t\t\tVALUES(:ID,:ProgramID,:Field,:ShowTitle,:Operators,:ControlName,:InputType,:Width,:Sort,:DataSource,:DataSourceString,:DataLinkID,:IsQueryUsers,:Value)";
            OracleParameter[] parameterArray1 = new OracleParameter[14];
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
            OracleParameter parameter5 = new OracleParameter(":Operators", OracleDbType.Varchar2) {
                Value = model.Operators
            };
            parameterArray1[4] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":ControlName", OracleDbType.Varchar2) {
                Value = model.ControlName
            };
            parameterArray1[5] = parameter6;
            OracleParameter parameter7 = new OracleParameter(":InputType", OracleDbType.Int32) {
                Value = model.InputType
            };
            parameterArray1[6] = parameter7;
            parameterArray1[7] = (model.Width == null) ? new OracleParameter(":Width", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Width", OracleDbType.Varchar2) { Value = model.Width };
            OracleParameter parameter10 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[8] = parameter10;
            parameterArray1[9] = !model.DataSource.HasValue ? new OracleParameter(":DataSource", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter(":DataSource", OracleDbType.Int32) { Value = model.DataSource };
            parameterArray1[10] = (model.DataSourceString == null) ? new OracleParameter(":DataSourceString", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":DataSourceString", OracleDbType.Varchar2) { Value = model.DataSourceString };
            parameterArray1[11] = (model.DataLinkID == null) ? new OracleParameter(":DataLinkID", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":DataLinkID", OracleDbType.Varchar2) { Value = model.DataLinkID };
            parameterArray1[12] = !model.IsQueryUsers.HasValue ? new OracleParameter(":IsQueryUsers", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter(":IsQueryUsers", OracleDbType.Int32) { Value = model.IsQueryUsers };
            parameterArray1[13] = (model.Value == null) ? new OracleParameter(":Value", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Value", OracleDbType.Varchar2) { Value = model.Value };
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.ProgramBuilderQuerys> DataReaderToList(OracleDataReader dataReader)
        {
            List<YJ.Data.Model.ProgramBuilderQuerys> list = new List<YJ.Data.Model.ProgramBuilderQuerys>();
            YJ.Data.Model.ProgramBuilderQuerys item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.ProgramBuilderQuerys {
                    ID = dataReader.GetString(0).ToGuid(),
                    ProgramID = dataReader.GetString(1).ToGuid(),
                    Field = dataReader.GetString(2),
                    ShowTitle = dataReader.GetString(3),
                    Operators = dataReader.GetString(4),
                    ControlName = dataReader.GetString(5),
                    InputType = dataReader.GetInt32(6)
                };
                if (!dataReader.IsDBNull(7))
                {
                    item.Width = dataReader.GetString(7);
                }
                item.Sort = dataReader.GetInt32(8);
                if (!dataReader.IsDBNull(9))
                {
                    item.DataSource = new int?(dataReader.GetInt32(9));
                }
                if (!dataReader.IsDBNull(10))
                {
                    item.DataSourceString = dataReader.GetString(10);
                }
                if (!dataReader.IsDBNull(11))
                {
                    item.DataLinkID = dataReader.GetString(11);
                }
                if (!dataReader.IsDBNull(12))
                {
                    item.IsQueryUsers = new int?(dataReader.GetInt32(12));
                }
                if (!dataReader.IsDBNull(13))
                {
                    item.Value = dataReader.GetString(13);
                }
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM ProgramBuilderQuerys WHERE ID=:ID";
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
            string sql = "DELETE FROM ProgramBuilderQuerys WHERE ProgramID=:ProgramID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ProgramID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.ProgramBuilderQuerys Get(Guid id)
        {
            string sql = "SELECT * FROM ProgramBuilderQuerys WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilderQuerys> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.ProgramBuilderQuerys> GetAll()
        {
            string sql = "SELECT * FROM ProgramBuilderQuerys";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.ProgramBuilderQuerys> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.ProgramBuilderQuerys> GetAll(Guid programID)
        {
            string sql = "SELECT * FROM ProgramBuilderQuerys WHERE ProgramID=:ProgramID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ProgramID", OracleDbType.Varchar2) {
                Value = programID
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilderQuerys> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM ProgramBuilderQuerys";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.ProgramBuilderQuerys model)
        {
            string sql = "UPDATE ProgramBuilderQuerys SET \r\n\t\t\t\tProgramID=:ProgramID,Field=:Field,ShowTitle=:ShowTitle,Operators=:Operators,ControlName=:ControlName,InputType=:InputType,Width=:Width,Sort=:Sort,DataSource=:DataSource,DataSourceString=:DataSourceString,DataLinkID=:DataLinkID,IsQueryUsers=:IsQueryUsers,Value=:Value\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[14];
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
            OracleParameter parameter4 = new OracleParameter(":Operators", OracleDbType.Varchar2) {
                Value = model.Operators
            };
            parameterArray1[3] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":ControlName", OracleDbType.Varchar2) {
                Value = model.ControlName
            };
            parameterArray1[4] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":InputType", OracleDbType.Int32) {
                Value = model.InputType
            };
            parameterArray1[5] = parameter6;
            parameterArray1[6] = (model.Width == null) ? new OracleParameter(":Width", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Width", OracleDbType.Varchar2) { Value = model.Width };
            OracleParameter parameter9 = new OracleParameter(":Sort", OracleDbType.Int32, -1) {
                Value = model.Sort
            };
            parameterArray1[7] = parameter9;
            parameterArray1[8] = !model.DataSource.HasValue ? new OracleParameter(":DataSource", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter(":DataSource", OracleDbType.Int32) { Value = model.DataSource };
            parameterArray1[9] = (model.DataSourceString == null) ? new OracleParameter(":DataSourceString", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":DataSourceString", OracleDbType.Varchar2) { Value = model.DataSourceString };
            parameterArray1[10] = (model.DataLinkID == null) ? new OracleParameter(":DataLinkID", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":DataLinkID", OracleDbType.Varchar2) { Value = model.DataLinkID };
            parameterArray1[11] = !model.IsQueryUsers.HasValue ? new OracleParameter(":IsQueryUsers", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter(":IsQueryUsers", OracleDbType.Int32) { Value = model.IsQueryUsers };
            parameterArray1[12] = (model.Value == null) ? new OracleParameter(":Value", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Value", OracleDbType.Varchar2) { Value = model.Value };
            OracleParameter parameter20 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[13] = parameter20;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

