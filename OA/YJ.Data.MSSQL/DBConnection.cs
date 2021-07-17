namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class DBConnection : IDBConnection
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.DBConnection model)
        {
            string sql = "INSERT INTO DBConnection\r\n\t\t\t\t(ID,Name,Type,ConnectionString,Note) \r\n\t\t\t\tVALUES(@ID,@Name,@Type,@ConnectionString,@Note)";
            SqlParameter[] parameterArray1 = new SqlParameter[5];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Name", SqlDbType.VarChar, 500) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Type", SqlDbType.VarChar, 500) {
                Value = model.Type
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@ConnectionString", SqlDbType.VarChar, -1) {
                Value = model.ConnectionString
            };
            parameterArray1[3] = parameter4;
            parameterArray1[4] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.VarChar, -1) { Value = model.Note };
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.DBConnection> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.DBConnection> list = new List<YJ.Data.Model.DBConnection>();
            YJ.Data.Model.DBConnection item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.DBConnection {
                    ID = dataReader.GetGuid(0),
                    Name = dataReader.GetString(1),
                    Type = dataReader.GetString(2),
                    ConnectionString = dataReader.GetString(3)
                };
                if (!dataReader.IsDBNull(4))
                {
                    item.Note = dataReader.GetString(4);
                }
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM DBConnection WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.DBConnection Get(Guid id)
        {
            string sql = "SELECT * FROM DBConnection WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.DBConnection> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.DBConnection> GetAll()
        {
            string sql = "SELECT * FROM DBConnection";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.DBConnection> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM DBConnection";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.DBConnection model)
        {
            string sql = "UPDATE DBConnection SET \r\n\t\t\t\tName=@Name,Type=@Type,ConnectionString=@ConnectionString,Note=@Note\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[5];
            SqlParameter parameter1 = new SqlParameter("@Name", SqlDbType.VarChar, 500) {
                Value = model.Name
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Type", SqlDbType.VarChar, 500) {
                Value = model.Type
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@ConnectionString", SqlDbType.VarChar, -1) {
                Value = model.ConnectionString
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.VarChar, -1) { Value = model.Note };
            SqlParameter parameter6 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[4] = parameter6;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

