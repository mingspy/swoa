namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class AppLibraryButtons1 : IAppLibraryButtons1
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.AppLibraryButtons1 model)
        {
            string sql = "INSERT INTO AppLibraryButtons1\r\n\t\t\t\t(ID,AppLibraryID,ButtonID,Name,Events,Ico,Sort,Type,ShowType,IsValidateShow) \r\n\t\t\t\tVALUES(@ID,@AppLibraryID,@ButtonID,@Name,@Events,@Ico,@Sort,@Type,@ShowType,@IsValidateShow)";
            SqlParameter[] parameterArray1 = new SqlParameter[10];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@AppLibraryID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.AppLibraryID
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = !model.ButtonID.HasValue ? new SqlParameter("@ButtonID", SqlDbType.UniqueIdentifier, -1) { Value = DBNull.Value } : new SqlParameter("@ButtonID", SqlDbType.UniqueIdentifier, -1) { Value = model.ButtonID };
            SqlParameter parameter5 = new SqlParameter("@Name", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Name
            };
            parameterArray1[3] = parameter5;
            SqlParameter parameter6 = new SqlParameter("@Events", SqlDbType.VarChar, 0x1388) {
                Value = model.Events
            };
            parameterArray1[4] = parameter6;
            SqlParameter parameter7 = new SqlParameter("@Ico", SqlDbType.VarChar, 0x7d0) {
                Value = model.Ico
            };
            parameterArray1[5] = parameter7;
            SqlParameter parameter8 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[6] = parameter8;
            SqlParameter parameter9 = new SqlParameter("@Type", SqlDbType.Int, -1) {
                Value = model.Type
            };
            parameterArray1[7] = parameter9;
            SqlParameter parameter10 = new SqlParameter("@ShowType", SqlDbType.Int, -1) {
                Value = model.ShowType
            };
            parameterArray1[8] = parameter10;
            SqlParameter parameter11 = new SqlParameter("@IsValidateShow", SqlDbType.Int, -1) {
                Value = model.IsValidateShow
            };
            parameterArray1[9] = parameter11;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.AppLibraryButtons1> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.AppLibraryButtons1> list = new List<YJ.Data.Model.AppLibraryButtons1>();
            YJ.Data.Model.AppLibraryButtons1 item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.AppLibraryButtons1 {
                    ID = dataReader.GetGuid(0),
                    AppLibraryID = dataReader.GetGuid(1)
                };
                if (!dataReader.IsDBNull(2))
                {
                    item.ButtonID = new Guid?(dataReader.GetGuid(2));
                }
                item.Name = dataReader.GetString(3);
                item.Events = dataReader.GetString(4);
                if (!dataReader.IsDBNull(5))
                {
                    item.Ico = dataReader.GetString(5);
                }
                item.Sort = dataReader.GetInt32(6);
                item.Type = dataReader.GetInt32(7);
                item.ShowType = dataReader.GetInt32(8);
                item.IsValidateShow = dataReader.GetInt32(9);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM AppLibraryButtons1 WHERE ID=@ID";
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
            string sql = "DELETE FROM AppLibraryButtons1 WHERE AppLibraryID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.AppLibraryButtons1 Get(Guid id)
        {
            string sql = "SELECT * FROM AppLibraryButtons1 WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.AppLibraryButtons1> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.AppLibraryButtons1> GetAll()
        {
            string sql = "SELECT * FROM AppLibraryButtons1";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.AppLibraryButtons1> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.AppLibraryButtons1> GetAllByAppID(Guid id)
        {
            string sql = "SELECT * FROM AppLibraryButtons1 WHERE AppLibraryID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.AppLibraryButtons1> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM AppLibraryButtons1";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.AppLibraryButtons1 model)
        {
            string sql = "UPDATE AppLibraryButtons1 SET \r\n\t\t\t\tAppLibraryID=@AppLibraryID,ButtonID=@ButtonID,Name=@Name,Events=@Events,Ico=@Ico,Sort=@Sort,Type=@Type,ShowType=@ShowType,IsValidateShow=@IsValidateShow\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[10];
            SqlParameter parameter1 = new SqlParameter("@AppLibraryID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.AppLibraryID
            };
            parameterArray1[0] = parameter1;
            parameterArray1[1] = !model.ButtonID.HasValue ? new SqlParameter("@ButtonID", SqlDbType.UniqueIdentifier, -1) { Value = DBNull.Value } : new SqlParameter("@ButtonID", SqlDbType.UniqueIdentifier, -1) { Value = model.ButtonID };
            SqlParameter parameter4 = new SqlParameter("@Name", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Name
            };
            parameterArray1[2] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@Events", SqlDbType.VarChar, 0x1388) {
                Value = model.Events
            };
            parameterArray1[3] = parameter5;
            SqlParameter parameter6 = new SqlParameter("@Ico", SqlDbType.VarChar, 0x7d0) {
                Value = model.Ico
            };
            parameterArray1[4] = parameter6;
            SqlParameter parameter7 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[5] = parameter7;
            SqlParameter parameter8 = new SqlParameter("@Type", SqlDbType.Int, -1) {
                Value = model.Type
            };
            parameterArray1[6] = parameter8;
            SqlParameter parameter9 = new SqlParameter("@ShowType", SqlDbType.Int, -1) {
                Value = model.ShowType
            };
            parameterArray1[7] = parameter9;
            SqlParameter parameter10 = new SqlParameter("@IsValidateShow", SqlDbType.Int, -1) {
                Value = model.IsValidateShow
            };
            parameterArray1[8] = parameter10;
            SqlParameter parameter11 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[9] = parameter11;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

