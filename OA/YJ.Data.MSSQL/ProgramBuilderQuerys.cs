namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class ProgramBuilderQuerys : IProgramBuilderQuerys
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.ProgramBuilderQuerys model)
        {
            string sql = "INSERT INTO ProgramBuilderQuerys\r\n\t\t\t\t(ID,ProgramID,Field,ShowTitle,Operators,ControlName,InputType,Width,Sort,DataSource,DataSourceString,DataLinkID,IsQueryUsers,Value) \r\n\t\t\t\tVALUES(@ID,@ProgramID,@Field,@ShowTitle,@Operators,@ControlName,@InputType,@Width,@Sort,@DataSource,@DataSourceString,@DataLinkID,@IsQueryUsers,@Value)";
            SqlParameter[] parameterArray1 = new SqlParameter[14];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@ProgramID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ProgramID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Field", SqlDbType.VarChar, 500) {
                Value = model.Field
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@ShowTitle", SqlDbType.NVarChar, 0x3e8) {
                Value = model.ShowTitle
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@Operators", SqlDbType.VarChar, 50) {
                Value = model.Operators
            };
            parameterArray1[4] = parameter5;
            SqlParameter parameter6 = new SqlParameter("@ControlName", SqlDbType.VarChar, 50) {
                Value = model.ControlName
            };
            parameterArray1[5] = parameter6;
            SqlParameter parameter7 = new SqlParameter("@InputType", SqlDbType.Int, -1) {
                Value = model.InputType
            };
            parameterArray1[6] = parameter7;
            parameterArray1[7] = (model.Width == null) ? new SqlParameter("@Width", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Width", SqlDbType.VarChar, 50) { Value = model.Width };
            SqlParameter parameter10 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[8] = parameter10;
            parameterArray1[9] = !model.DataSource.HasValue ? new SqlParameter("@DataSource", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@DataSource", SqlDbType.Int, -1) { Value = model.DataSource };
            parameterArray1[10] = (model.DataSourceString == null) ? new SqlParameter("@DataSourceString", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@DataSourceString", SqlDbType.VarChar, -1) { Value = model.DataSourceString };
            parameterArray1[11] = (model.DataLinkID == null) ? new SqlParameter("@DataLinkID", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@DataLinkID", SqlDbType.VarChar, 50) { Value = model.DataLinkID };
            parameterArray1[12] = !model.IsQueryUsers.HasValue ? new SqlParameter("@IsQueryUsers", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@IsQueryUsers", SqlDbType.Int, -1) { Value = model.IsQueryUsers };
            parameterArray1[13] = (model.Value == null) ? new SqlParameter("@Value", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Value", SqlDbType.VarChar, 50) { Value = model.Value };
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.ProgramBuilderQuerys> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.ProgramBuilderQuerys> list = new List<YJ.Data.Model.ProgramBuilderQuerys>();
            YJ.Data.Model.ProgramBuilderQuerys item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.ProgramBuilderQuerys {
                    ID = dataReader.GetGuid(0),
                    ProgramID = dataReader.GetGuid(1),
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
            string sql = "DELETE FROM ProgramBuilderQuerys WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int DeleteByProgramID(Guid id)
        {
            string sql = "DELETE FROM ProgramBuilderQuerys WHERE ProgramID=@ProgramID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ProgramID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.ProgramBuilderQuerys Get(Guid id)
        {
            string sql = "SELECT * FROM ProgramBuilderQuerys WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilderQuerys> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.ProgramBuilderQuerys> GetAll()
        {
            string sql = "SELECT * FROM ProgramBuilderQuerys";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.ProgramBuilderQuerys> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.ProgramBuilderQuerys> GetAll(Guid programID)
        {
            string sql = "SELECT * FROM ProgramBuilderQuerys WHERE ProgramID=@ProgramID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ProgramID", SqlDbType.UniqueIdentifier) {
                Value = programID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
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
            string sql = "UPDATE ProgramBuilderQuerys SET \r\n\t\t\t\tProgramID=@ProgramID,Field=@Field,ShowTitle=@ShowTitle,Operators=@Operators,ControlName=@ControlName,InputType=@InputType,Width=@Width,Sort=@Sort,DataSource=@DataSource,DataSourceString=@DataSourceString,DataLinkID=@DataLinkID,IsQueryUsers=@IsQueryUsers,Value=@Value\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[14];
            SqlParameter parameter1 = new SqlParameter("@ProgramID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ProgramID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Field", SqlDbType.VarChar, 500) {
                Value = model.Field
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@ShowTitle", SqlDbType.NVarChar, 0x3e8) {
                Value = model.ShowTitle
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@Operators", SqlDbType.VarChar, 50) {
                Value = model.Operators
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@ControlName", SqlDbType.VarChar, 50) {
                Value = model.ControlName
            };
            parameterArray1[4] = parameter5;
            SqlParameter parameter6 = new SqlParameter("@InputType", SqlDbType.Int, -1) {
                Value = model.InputType
            };
            parameterArray1[5] = parameter6;
            parameterArray1[6] = (model.Width == null) ? new SqlParameter("@Width", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Width", SqlDbType.VarChar, 50) { Value = model.Width };
            SqlParameter parameter9 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[7] = parameter9;
            parameterArray1[8] = !model.DataSource.HasValue ? new SqlParameter("@DataSource", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@DataSource", SqlDbType.Int, -1) { Value = model.DataSource };
            parameterArray1[9] = (model.DataSourceString == null) ? new SqlParameter("@DataSourceString", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@DataSourceString", SqlDbType.VarChar, -1) { Value = model.DataSourceString };
            parameterArray1[10] = (model.DataLinkID == null) ? new SqlParameter("@DataLinkID", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@DataLinkID", SqlDbType.VarChar, 50) { Value = model.DataLinkID };
            parameterArray1[11] = !model.IsQueryUsers.HasValue ? new SqlParameter("@IsQueryUsers", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@IsQueryUsers", SqlDbType.Int, -1) { Value = model.IsQueryUsers };
            parameterArray1[12] = (model.Value == null) ? new SqlParameter("@Value", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Value", SqlDbType.VarChar, 50) { Value = model.Value };
            SqlParameter parameter20 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[13] = parameter20;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

