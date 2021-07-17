namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class ProgramBuilderFields : IProgramBuilderFields
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.ProgramBuilderFields model)
        {
            string sql = "INSERT INTO ProgramBuilderFields\r\n\t\t\t\t(ID,ProgramID,Field,ShowTitle,Align,Width,ShowType,ShowFormat,CustomString,Sort) \r\n\t\t\t\tVALUES(@ID,@ProgramID,@Field,@ShowTitle,@Align,@Width,@ShowType,@ShowFormat,@CustomString,@Sort)";
            SqlParameter[] parameterArray1 = new SqlParameter[10];
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
            SqlParameter parameter4 = new SqlParameter("@ShowTitle", SqlDbType.VarChar, -1) {
                Value = model.ShowTitle
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@Align", SqlDbType.VarChar, 50) {
                Value = model.Align
            };
            parameterArray1[4] = parameter5;
            parameterArray1[5] = (model.Width == null) ? new SqlParameter("@Width", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Width", SqlDbType.VarChar, 50) { Value = model.Width };
            SqlParameter parameter8 = new SqlParameter("@ShowType", SqlDbType.Int, -1) {
                Value = model.ShowType
            };
            parameterArray1[6] = parameter8;
            parameterArray1[7] = (model.ShowFormat == null) ? new SqlParameter("@ShowFormat", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@ShowFormat", SqlDbType.VarChar, 50) { Value = model.ShowFormat };
            parameterArray1[8] = (model.CustomString == null) ? new SqlParameter("@CustomString", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@CustomString", SqlDbType.VarChar, -1) { Value = model.CustomString };
            SqlParameter parameter13 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[9] = parameter13;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.ProgramBuilderFields> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.ProgramBuilderFields> list = new List<YJ.Data.Model.ProgramBuilderFields>();
            YJ.Data.Model.ProgramBuilderFields item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.ProgramBuilderFields {
                    ID = dataReader.GetGuid(0),
                    ProgramID = dataReader.GetGuid(1)
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
            string sql = "DELETE FROM ProgramBuilderFields WHERE ID=@ID";
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
            string sql = "DELETE FROM ProgramBuilderFields WHERE ProgramID=@ProgramID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ProgramID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.ProgramBuilderFields Get(Guid id)
        {
            string sql = "SELECT * FROM ProgramBuilderFields WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilderFields> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.ProgramBuilderFields> GetAll()
        {
            string sql = "SELECT * FROM ProgramBuilderFields";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.ProgramBuilderFields> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.ProgramBuilderFields> GetAll(Guid programID)
        {
            string sql = "SELECT * FROM ProgramBuilderFields WHERE ProgramID=@ProgramID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ProgramID", SqlDbType.UniqueIdentifier) {
                Value = programID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
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
            string sql = "UPDATE ProgramBuilderFields SET \r\n\t\t\t\tProgramID=@ProgramID,Field=@Field,ShowTitle=@ShowTitle,Align=@Align,Width=@Width,ShowType=@ShowType,ShowFormat=@ShowFormat,CustomString=@CustomString,Sort=@Sort\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[10];
            SqlParameter parameter1 = new SqlParameter("@ProgramID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ProgramID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Field", SqlDbType.VarChar, 500) {
                Value = model.Field
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@ShowTitle", SqlDbType.VarChar, -1) {
                Value = model.ShowTitle
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@Align", SqlDbType.VarChar, 50) {
                Value = model.Align
            };
            parameterArray1[3] = parameter4;
            parameterArray1[4] = (model.Width == null) ? new SqlParameter("@Width", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Width", SqlDbType.VarChar, 50) { Value = model.Width };
            SqlParameter parameter7 = new SqlParameter("@ShowType", SqlDbType.Int, -1) {
                Value = model.ShowType
            };
            parameterArray1[5] = parameter7;
            parameterArray1[6] = (model.ShowFormat == null) ? new SqlParameter("@ShowFormat", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@ShowFormat", SqlDbType.VarChar, 50) { Value = model.ShowFormat };
            parameterArray1[7] = (model.CustomString == null) ? new SqlParameter("@CustomString", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@CustomString", SqlDbType.VarChar, -1) { Value = model.CustomString };
            SqlParameter parameter12 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[8] = parameter12;
            SqlParameter parameter13 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[9] = parameter13;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

