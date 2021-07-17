namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class DocumentDirectory : IDocumentDirectory
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.DocumentDirectory model)
        {
            string sql = "INSERT INTO DocumentDirectory\r\n\t\t\t\t(ID,ParentID,Name,ReadUsers,ManageUsers,PublishUsers,Sort) \r\n\t\t\t\tVALUES(@ID,@ParentID,@Name,@ReadUsers,@ManageUsers,@PublishUsers,@Sort)";
            SqlParameter[] parameterArray1 = new SqlParameter[7];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@ParentID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ParentID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Name", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Name
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = (model.ReadUsers == null) ? new SqlParameter("@ReadUsers", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@ReadUsers", SqlDbType.VarChar, -1) { Value = model.ReadUsers };
            parameterArray1[4] = (model.ManageUsers == null) ? new SqlParameter("@ManageUsers", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@ManageUsers", SqlDbType.VarChar, -1) { Value = model.ManageUsers };
            parameterArray1[5] = (model.PublishUsers == null) ? new SqlParameter("@PublishUsers", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@PublishUsers", SqlDbType.VarChar, -1) { Value = model.PublishUsers };
            SqlParameter parameter10 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[6] = parameter10;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.DocumentDirectory> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.DocumentDirectory> list = new List<YJ.Data.Model.DocumentDirectory>();
            YJ.Data.Model.DocumentDirectory item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.DocumentDirectory {
                    ID = dataReader.GetGuid(0),
                    ParentID = dataReader.GetGuid(1),
                    Name = dataReader.GetString(2)
                };
                if (!dataReader.IsDBNull(3))
                {
                    item.ReadUsers = dataReader.GetString(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    item.ManageUsers = dataReader.GetString(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    item.PublishUsers = dataReader.GetString(5);
                }
                item.Sort = dataReader.GetInt32(6);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM DocumentDirectory WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.DocumentDirectory Get(Guid id)
        {
            string sql = "SELECT * FROM DocumentDirectory WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.DocumentDirectory> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.DocumentDirectory> GetAll()
        {
            string sql = "SELECT * FROM DocumentDirectory";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.DocumentDirectory> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.DocumentDirectory> GetChilds(Guid id)
        {
            string sql = "SELECT * FROM DocumentDirectory WHERE ParentID=@ParentID ORDER BY Sort";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ParentID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.DocumentDirectory> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM DocumentDirectory";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int GetMaxSort(Guid dirID)
        {
            string sql = "SELECT (ISNULL(MAX(Sort),0)+5) Sort FROM DocumentDirectory WHERE ParentID=@ParentID ";
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("@ParentID", dirID) };
            return this.dbHelper.GetFieldValue(sql, parameter).ToInt();
        }

        public int Update(YJ.Data.Model.DocumentDirectory model)
        {
            string sql = "UPDATE DocumentDirectory SET \r\n\t\t\t\tParentID=@ParentID,Name=@Name,ReadUsers=@ReadUsers,ManageUsers=@ManageUsers,PublishUsers=@PublishUsers,Sort=@Sort\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[7];
            SqlParameter parameter1 = new SqlParameter("@ParentID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ParentID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Name", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = (model.ReadUsers == null) ? new SqlParameter("@ReadUsers", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@ReadUsers", SqlDbType.VarChar, -1) { Value = model.ReadUsers };
            parameterArray1[3] = (model.ManageUsers == null) ? new SqlParameter("@ManageUsers", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@ManageUsers", SqlDbType.VarChar, -1) { Value = model.ManageUsers };
            parameterArray1[4] = (model.PublishUsers == null) ? new SqlParameter("@PublishUsers", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@PublishUsers", SqlDbType.VarChar, -1) { Value = model.PublishUsers };
            SqlParameter parameter9 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[5] = parameter9;
            SqlParameter parameter10 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[6] = parameter10;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

