namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class ProgramBuilderValidates : IProgramBuilderValidates
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.ProgramBuilderValidates model)
        {
            string sql = "INSERT INTO ProgramBuilderValidates\r\n\t\t\t\t(ID,ProgramID,TableName,FieldName,FieldNote,Validate) \r\n\t\t\t\tVALUES(@ID,@ProgramID,@TableName,@FieldName,@FieldNote,@Validate)";
            SqlParameter[] parameterArray1 = new SqlParameter[6];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@ProgramID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ProgramID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@TableName", SqlDbType.VarChar, 500) {
                Value = model.TableName
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@FieldName", SqlDbType.VarChar, 500) {
                Value = model.FieldName
            };
            parameterArray1[3] = parameter4;
            parameterArray1[4] = (model.FieldNote == null) ? new SqlParameter("@FieldNote", SqlDbType.NVarChar, 0x3e8) { Value = DBNull.Value } : new SqlParameter("@FieldNote", SqlDbType.NVarChar, 0x3e8) { Value = model.FieldNote };
            SqlParameter parameter7 = new SqlParameter("@Validate", SqlDbType.Int, -1) {
                Value = model.Validate
            };
            parameterArray1[5] = parameter7;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.ProgramBuilderValidates> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.ProgramBuilderValidates> list = new List<YJ.Data.Model.ProgramBuilderValidates>();
            YJ.Data.Model.ProgramBuilderValidates item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.ProgramBuilderValidates {
                    ID = dataReader.GetGuid(0),
                    ProgramID = dataReader.GetGuid(1),
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
            string sql = "DELETE FROM ProgramBuilderValidates WHERE ID=@ID";
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
            string sql = "DELETE FROM ProgramBuilderValidates WHERE ProgramID=@ProgramID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ProgramID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.ProgramBuilderValidates Get(Guid id)
        {
            string sql = "SELECT * FROM ProgramBuilderValidates WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilderValidates> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.ProgramBuilderValidates> GetAll()
        {
            string sql = "SELECT * FROM ProgramBuilderValidates";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.ProgramBuilderValidates> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.ProgramBuilderValidates> GetAll(Guid programID)
        {
            string sql = "SELECT * FROM ProgramBuilderValidates WHERE ProgramID=@ProgramID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ProgramID", SqlDbType.UniqueIdentifier) {
                Value = programID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
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
            string sql = "UPDATE ProgramBuilderValidates SET \r\n\t\t\t\tProgramID=@ProgramID,TableName=@TableName,FieldName=@FieldName,FieldNote=@FieldNote,Validate=@Validate\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[6];
            SqlParameter parameter1 = new SqlParameter("@ProgramID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ProgramID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@TableName", SqlDbType.VarChar, 500) {
                Value = model.TableName
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@FieldName", SqlDbType.VarChar, 500) {
                Value = model.FieldName
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = (model.FieldNote == null) ? new SqlParameter("@FieldNote", SqlDbType.NVarChar, 0x3e8) { Value = DBNull.Value } : new SqlParameter("@FieldNote", SqlDbType.NVarChar, 0x3e8) { Value = model.FieldNote };
            SqlParameter parameter6 = new SqlParameter("@Validate", SqlDbType.Int, -1) {
                Value = model.Validate
            };
            parameterArray1[4] = parameter6;
            SqlParameter parameter7 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[5] = parameter7;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

