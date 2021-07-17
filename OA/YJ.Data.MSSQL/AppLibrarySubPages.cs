namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class AppLibrarySubPages : IAppLibrarySubPages
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.AppLibrarySubPages model)
        {
            string sql = "INSERT INTO AppLibrarySubPages\r\n\t\t\t\t(ID,AppLibraryID,Name,Address,Ico,Sort,Note) \r\n\t\t\t\tVALUES(@ID,@AppLibraryID,@Name,@Address,@Ico,@Sort,@Note)";
            SqlParameter[] parameterArray1 = new SqlParameter[7];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@AppLibraryID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.AppLibraryID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Name", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Name
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@Address", SqlDbType.VarChar, 0x1388) {
                Value = model.Address
            };
            parameterArray1[3] = parameter4;
            parameterArray1[4] = (model.Ico == null) ? new SqlParameter("@Ico", SqlDbType.VarChar, 0x1388) { Value = DBNull.Value } : new SqlParameter("@Ico", SqlDbType.VarChar, 0x1388) { Value = model.Ico };
            SqlParameter parameter7 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[5] = parameter7;
            parameterArray1[6] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.NVarChar, 0xfa0) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.NVarChar, 0xfa0) { Value = model.Note };
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.AppLibrarySubPages> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.AppLibrarySubPages> list = new List<YJ.Data.Model.AppLibrarySubPages>();
            YJ.Data.Model.AppLibrarySubPages item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.AppLibrarySubPages {
                    ID = dataReader.GetGuid(0),
                    AppLibraryID = dataReader.GetGuid(1),
                    Name = dataReader.GetString(2),
                    Address = dataReader.GetString(3)
                };
                if (!dataReader.IsDBNull(4))
                {
                    item.Ico = dataReader.GetString(4);
                }
                item.Sort = dataReader.GetInt32(5);
                if (!dataReader.IsDBNull(6))
                {
                    item.Note = dataReader.GetString(6);
                }
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM AppLibrarySubPages WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int DeleteByAppID(Guid id)
        {
            string sql = "DELETE FROM AppLibrarySubPages WHERE AppLibraryID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.AppLibrarySubPages Get(Guid id)
        {
            string sql = "SELECT * FROM AppLibrarySubPages WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.AppLibrarySubPages> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.AppLibrarySubPages> GetAll()
        {
            string sql = "SELECT * FROM AppLibrarySubPages";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.AppLibrarySubPages> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.AppLibrarySubPages> GetAllByAppID(Guid id)
        {
            string sql = "SELECT * FROM AppLibrarySubPages WHERE AppLibraryID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.AppLibrarySubPages> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM AppLibrarySubPages";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.AppLibrarySubPages model)
        {
            string sql = "UPDATE AppLibrarySubPages SET \r\n\t\t\t\tAppLibraryID=@AppLibraryID,Name=@Name,Address=@Address,Ico=@Ico,Sort=@Sort,Note=@Note\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[7];
            SqlParameter parameter1 = new SqlParameter("@AppLibraryID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.AppLibraryID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Name", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Address", SqlDbType.VarChar, 0x1388) {
                Value = model.Address
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = (model.Ico == null) ? new SqlParameter("@Ico", SqlDbType.VarChar, 0x1388) { Value = DBNull.Value } : new SqlParameter("@Ico", SqlDbType.VarChar, 0x1388) { Value = model.Ico };
            SqlParameter parameter6 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[4] = parameter6;
            parameterArray1[5] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.NVarChar, 0xfa0) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.NVarChar, 0xfa0) { Value = model.Note };
            SqlParameter parameter9 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[6] = parameter9;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

