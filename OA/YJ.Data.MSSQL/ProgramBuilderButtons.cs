namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class ProgramBuilderButtons : IProgramBuilderButtons
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.ProgramBuilderButtons model)
        {
            string sql = "INSERT INTO ProgramBuilderButtons\r\n\t\t\t\t(ID,ProgramID,ButtonID,ButtonName,ClientScript,Ico,ShowType,Sort,IsValidateShow) \r\n\t\t\t\tVALUES(@ID,@ProgramID,@ButtonID,@ButtonName,@ClientScript,@Ico,@ShowType,@Sort,@IsValidateShow)";
            SqlParameter[] parameterArray1 = new SqlParameter[9];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@ProgramID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ProgramID
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = !model.ButtonID.HasValue ? new SqlParameter("@ButtonID", SqlDbType.UniqueIdentifier, -1) { Value = DBNull.Value } : new SqlParameter("@ButtonID", SqlDbType.UniqueIdentifier, -1) { Value = model.ButtonID };
            SqlParameter parameter5 = new SqlParameter("@ButtonName", SqlDbType.NVarChar, 400) {
                Value = model.ButtonName
            };
            parameterArray1[3] = parameter5;
            parameterArray1[4] = (model.ClientScript == null) ? new SqlParameter("@ClientScript", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@ClientScript", SqlDbType.VarChar, -1) { Value = model.ClientScript };
            parameterArray1[5] = (model.Ico == null) ? new SqlParameter("@Ico", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@Ico", SqlDbType.VarChar, 500) { Value = model.Ico };
            SqlParameter parameter10 = new SqlParameter("@ShowType", SqlDbType.Int, -1) {
                Value = model.ShowType
            };
            parameterArray1[6] = parameter10;
            SqlParameter parameter11 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[7] = parameter11;
            SqlParameter parameter12 = new SqlParameter("@IsValidateShow", SqlDbType.Int, -1) {
                Value = model.IsValidateShow
            };
            parameterArray1[8] = parameter12;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.ProgramBuilderButtons> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.ProgramBuilderButtons> list = new List<YJ.Data.Model.ProgramBuilderButtons>();
            YJ.Data.Model.ProgramBuilderButtons item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.ProgramBuilderButtons {
                    ID = dataReader.GetGuid(0),
                    ProgramID = dataReader.GetGuid(1)
                };
                if (!dataReader.IsDBNull(2))
                {
                    item.ButtonID = new Guid?(dataReader.GetGuid(2));
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
            string sql = "DELETE FROM ProgramBuilderButtons WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.ProgramBuilderButtons Get(Guid id)
        {
            string sql = "SELECT * FROM ProgramBuilderButtons WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilderButtons> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.ProgramBuilderButtons> GetAll()
        {
            string sql = "SELECT * FROM ProgramBuilderButtons";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.ProgramBuilderButtons> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.ProgramBuilderButtons> GetAll(Guid id)
        {
            string sql = "SELECT * FROM ProgramBuilderButtons WHERE ProgramID=@ProgramID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ProgramID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
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
            string sql = "UPDATE ProgramBuilderButtons SET \r\n\t\t\t\tProgramID=@ProgramID,ButtonID=@ButtonID,ButtonName=@ButtonName,ClientScript=@ClientScript,Ico=@Ico,ShowType=@ShowType,Sort=@Sort,IsValidateShow=@IsValidateShow\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[9];
            SqlParameter parameter1 = new SqlParameter("@ProgramID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ProgramID
            };
            parameterArray1[0] = parameter1;
            parameterArray1[1] = !model.ButtonID.HasValue ? new SqlParameter("@ButtonID", SqlDbType.UniqueIdentifier, -1) { Value = DBNull.Value } : new SqlParameter("@ButtonID", SqlDbType.UniqueIdentifier, -1) { Value = model.ButtonID };
            SqlParameter parameter4 = new SqlParameter("@ButtonName", SqlDbType.NVarChar, 400) {
                Value = model.ButtonName
            };
            parameterArray1[2] = parameter4;
            parameterArray1[3] = (model.ClientScript == null) ? new SqlParameter("@ClientScript", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@ClientScript", SqlDbType.VarChar, -1) { Value = model.ClientScript };
            parameterArray1[4] = (model.Ico == null) ? new SqlParameter("@Ico", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@Ico", SqlDbType.VarChar, 500) { Value = model.Ico };
            SqlParameter parameter9 = new SqlParameter("@ShowType", SqlDbType.Int, -1) {
                Value = model.ShowType
            };
            parameterArray1[5] = parameter9;
            SqlParameter parameter10 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[6] = parameter10;
            SqlParameter parameter11 = new SqlParameter("@IsValidateShow", SqlDbType.Int, -1) {
                Value = model.IsValidateShow
            };
            parameterArray1[7] = parameter11;
            SqlParameter parameter12 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[8] = parameter12;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

