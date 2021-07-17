namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class ProgramBuilderExport : IProgramBuilderExport
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.ProgramBuilderExport model)
        {
            string sql = "INSERT INTO ProgramBuilderExport\r\n\t\t\t\t(ID,ProgramID,Field,ShowTitle,Align,Width,ShowType,DataType,ShowFormat,CustomString,Sort) \r\n\t\t\t\tVALUES(@ID,@ProgramID,@Field,@ShowTitle,@Align,@Width,@ShowType,@DataType,@ShowFormat,@CustomString,@Sort)";
            SqlParameter[] parameterArray1 = new SqlParameter[11];
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
            parameterArray1[3] = (model.ShowTitle == null) ? new SqlParameter("@ShowTitle", SqlDbType.NVarChar, 0x3e8) { Value = DBNull.Value } : new SqlParameter("@ShowTitle", SqlDbType.NVarChar, 0x3e8) { Value = model.ShowTitle };
            SqlParameter parameter6 = new SqlParameter("@Align", SqlDbType.Int, -1) {
                Value = model.Align
            };
            parameterArray1[4] = parameter6;
            parameterArray1[5] = !model.Width.HasValue ? new SqlParameter("@Width", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@Width", SqlDbType.Int, -1) { Value = model.Width };
            parameterArray1[6] = !model.ShowType.HasValue ? new SqlParameter("@ShowType", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@ShowType", SqlDbType.Int, -1) { Value = model.ShowType };
            parameterArray1[7] = !model.DataType.HasValue ? new SqlParameter("@DataType", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@DataType", SqlDbType.Int, -1) { Value = model.DataType };
            parameterArray1[8] = (model.ShowFormat == null) ? new SqlParameter("@ShowFormat", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@ShowFormat", SqlDbType.VarChar, 50) { Value = model.ShowFormat };
            parameterArray1[9] = (model.CustomString == null) ? new SqlParameter("@CustomString", SqlDbType.VarChar, 0x1388) { Value = DBNull.Value } : new SqlParameter("@CustomString", SqlDbType.VarChar, 0x1388) { Value = model.CustomString };
            SqlParameter parameter17 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[10] = parameter17;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.ProgramBuilderExport> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.ProgramBuilderExport> list = new List<YJ.Data.Model.ProgramBuilderExport>();
            YJ.Data.Model.ProgramBuilderExport item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.ProgramBuilderExport {
                    ID = dataReader.GetGuid(0),
                    ProgramID = dataReader.GetGuid(1),
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
            string sql = "DELETE FROM ProgramBuilderExport WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.ProgramBuilderExport Get(Guid id)
        {
            string sql = "SELECT * FROM ProgramBuilderExport WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilderExport> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.ProgramBuilderExport> GetAll()
        {
            string sql = "SELECT * FROM ProgramBuilderExport";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.ProgramBuilderExport> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.ProgramBuilderExport> GetAll(Guid programID)
        {
            string sql = "SELECT * FROM ProgramBuilderExport WHERE ProgramID=@ProgramID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ProgramID", SqlDbType.UniqueIdentifier) {
                Value = programID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilderExport> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM ProgramBuilderExport";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.ProgramBuilderExport model)
        {
            string sql = "UPDATE ProgramBuilderExport SET \r\n\t\t\t\tProgramID=@ProgramID,Field=@Field,ShowTitle=@ShowTitle,Align=@Align,Width=@Width,ShowType=@ShowType,DataType=@DataType,ShowFormat=@ShowFormat,CustomString=@CustomString,Sort=@Sort\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[11];
            SqlParameter parameter1 = new SqlParameter("@ProgramID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ProgramID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Field", SqlDbType.VarChar, 500) {
                Value = model.Field
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = (model.ShowTitle == null) ? new SqlParameter("@ShowTitle", SqlDbType.NVarChar, 0x3e8) { Value = DBNull.Value } : new SqlParameter("@ShowTitle", SqlDbType.NVarChar, 0x3e8) { Value = model.ShowTitle };
            SqlParameter parameter5 = new SqlParameter("@Align", SqlDbType.Int, -1) {
                Value = model.Align
            };
            parameterArray1[3] = parameter5;
            parameterArray1[4] = !model.Width.HasValue ? new SqlParameter("@Width", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@Width", SqlDbType.Int, -1) { Value = model.Width };
            parameterArray1[5] = !model.ShowType.HasValue ? new SqlParameter("@ShowType", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@ShowType", SqlDbType.Int, -1) { Value = model.ShowType };
            parameterArray1[6] = !model.DataType.HasValue ? new SqlParameter("@DataType", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@DataType", SqlDbType.Int, -1) { Value = model.DataType };
            parameterArray1[7] = (model.ShowFormat == null) ? new SqlParameter("@ShowFormat", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@ShowFormat", SqlDbType.VarChar, 50) { Value = model.ShowFormat };
            parameterArray1[8] = (model.CustomString == null) ? new SqlParameter("@CustomString", SqlDbType.VarChar, 0x1388) { Value = DBNull.Value } : new SqlParameter("@CustomString", SqlDbType.VarChar, 0x1388) { Value = model.CustomString };
            SqlParameter parameter16 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[9] = parameter16;
            SqlParameter parameter17 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[10] = parameter17;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

