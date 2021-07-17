namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class WorkGroup : IWorkGroup
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkGroup model)
        {
            string sql = "INSERT INTO WorkGroup\r\n\t\t\t\t(ID,Name,Members,Note,IntID) \r\n\t\t\t\tVALUES(@ID,@Name,@Members,@Note,@IntID)";
            SqlParameter[] parameterArray1 = new SqlParameter[5];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Name", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Members", SqlDbType.VarChar, -1) {
                Value = model.Members
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.NVarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.NVarChar, -1) { Value = model.Note };
            SqlParameter parameter6 = new SqlParameter("@IntID", SqlDbType.Int, -1) {
                Value = model.IntID
            };
            parameterArray1[4] = parameter6;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.WorkGroup> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.WorkGroup> list = new List<YJ.Data.Model.WorkGroup>();
            YJ.Data.Model.WorkGroup item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.WorkGroup {
                    ID = dataReader.GetGuid(0),
                    Name = dataReader.GetString(1),
                    Members = dataReader.GetString(2)
                };
                if (!dataReader.IsDBNull(3))
                {
                    item.Note = dataReader.GetString(3);
                }
                item.IntID = dataReader.GetInt32(4);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM WorkGroup WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.WorkGroup Get(Guid id)
        {
            string sql = "SELECT * FROM WorkGroup WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkGroup> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkGroup> GetAll()
        {
            string sql = "SELECT * FROM WorkGroup";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkGroup> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM WorkGroup";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.WorkGroup model)
        {
            string sql = "UPDATE WorkGroup SET \r\n\t\t\t\tName=@Name,Members=@Members,Note=@Note,IntID=@IntID \r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[5];
            SqlParameter parameter1 = new SqlParameter("@Name", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Name
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Members", SqlDbType.VarChar, -1) {
                Value = model.Members
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.NVarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.NVarChar, -1) { Value = model.Note };
            SqlParameter parameter5 = new SqlParameter("@IntID", SqlDbType.Int, -1) {
                Value = model.IntID
            };
            parameterArray1[3] = parameter5;
            SqlParameter parameter6 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[4] = parameter6;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

