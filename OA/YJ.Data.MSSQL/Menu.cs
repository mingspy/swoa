namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class Menu : IMenu
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.Menu model)
        {
            string sql = "INSERT INTO Menu\r\n\t\t\t\t(ID,ParentID,AppLibraryID,Title,Params,Ico,Sort,IcoColor) \r\n\t\t\t\tVALUES(@ID,@ParentID,@AppLibraryID,@Title,@Params,@Ico,@Sort,@IcoColor)";
            SqlParameter[] parameterArray1 = new SqlParameter[8];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@ParentID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ParentID
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = !model.AppLibraryID.HasValue ? new SqlParameter("@AppLibraryID", SqlDbType.UniqueIdentifier, -1) { Value = DBNull.Value } : new SqlParameter("@AppLibraryID", SqlDbType.UniqueIdentifier, -1) { Value = model.AppLibraryID };
            SqlParameter parameter5 = new SqlParameter("@Title", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Title
            };
            parameterArray1[3] = parameter5;
            parameterArray1[4] = (model.Params == null) ? new SqlParameter("@Params", SqlDbType.VarChar, 0x1388) { Value = DBNull.Value } : new SqlParameter("@Params", SqlDbType.VarChar, 0x1388) { Value = model.Params };
            parameterArray1[5] = (model.Ico == null) ? new SqlParameter("@Ico", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@Ico", SqlDbType.VarChar, 500) { Value = model.Ico };
            SqlParameter parameter10 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[6] = parameter10;
            parameterArray1[7] = (model.IcoColor == null) ? new SqlParameter("@IcoColor", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@IcoColor", SqlDbType.VarChar, 50) { Value = model.IcoColor };
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.Menu> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.Menu> list = new List<YJ.Data.Model.Menu>();
            YJ.Data.Model.Menu item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.Menu {
                    ID = dataReader.GetGuid(0),
                    ParentID = dataReader.GetGuid(1)
                };
                if (!dataReader.IsDBNull(2))
                {
                    item.AppLibraryID = new Guid?(dataReader.GetGuid(2));
                }
                item.Title = dataReader.GetString(3);
                if (!dataReader.IsDBNull(4))
                {
                    item.Params = dataReader.GetString(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    item.Ico = dataReader.GetString(5);
                }
                item.Sort = dataReader.GetInt32(6);
                if (!dataReader.IsDBNull(7))
                {
                    item.IcoColor = dataReader.GetString(7);
                }
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM Menu WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.Menu Get(Guid id)
        {
            string sql = "SELECT * FROM Menu WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Menu> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.Menu> GetAll()
        {
            string sql = "SELECT * FROM Menu";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Menu> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.Menu> GetAllByApplibaryID(Guid applibaryID)
        {
            string sql = "SELECT * FROM Menu WHERE AppLibraryID=@AppLibraryID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@AppLibraryID", SqlDbType.UniqueIdentifier) {
                Value = applibaryID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Menu> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public DataTable GetAllDataTable()
        {
            string sql = "SELECT a.*,b.Address,b.OpenMode,b.Width,b.Height,b.Params AS Params1,b.Ico AS AppIco,b.Color AS IcoColor1 FROM Menu a LEFT JOIN AppLibrary b ON a.AppLibraryID=b.ID ORDER BY a.Sort";
            return this.dbHelper.GetDataTable(sql);
        }

        public List<YJ.Data.Model.Menu> GetChild(Guid id)
        {
            string sql = "SELECT * FROM Menu WHERE ParentID=@ParentID ORDER BY Sort";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ParentID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Menu> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM Menu";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int GetMaxSort(Guid id)
        {
            int num;
            string sql = "SELECT ISNULL(MAX(Sort),0)+1 FROM Menu WHERE ParentID=@ParentID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ParentID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return (this.dbHelper.GetFieldValue(sql, parameter).IsInt(out num) ? num : 1);
        }

        public bool HasChild(Guid id)
        {
            string sql = "SELECT TOP 1 ID FROM Menu WHERE ParentID=@ParentID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ParentID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            bool hasRows = dataReader.HasRows;
            dataReader.Close();
            return hasRows;
        }

        public int Update(YJ.Data.Model.Menu model)
        {
            string sql = "UPDATE Menu SET \r\n\t\t\t\tParentID=@ParentID,AppLibraryID=@AppLibraryID,Title=@Title,Params=@Params,Ico=@Ico,Sort=@Sort,IcoColor=@IcoColor \r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[8];
            SqlParameter parameter1 = new SqlParameter("@ParentID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ParentID
            };
            parameterArray1[0] = parameter1;
            parameterArray1[1] = !model.AppLibraryID.HasValue ? new SqlParameter("@AppLibraryID", SqlDbType.UniqueIdentifier, -1) { Value = DBNull.Value } : new SqlParameter("@AppLibraryID", SqlDbType.UniqueIdentifier, -1) { Value = model.AppLibraryID };
            SqlParameter parameter4 = new SqlParameter("@Title", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Title
            };
            parameterArray1[2] = parameter4;
            parameterArray1[3] = (model.Params == null) ? new SqlParameter("@Params", SqlDbType.VarChar, 0x1388) { Value = DBNull.Value } : new SqlParameter("@Params", SqlDbType.VarChar, 0x1388) { Value = model.Params };
            parameterArray1[4] = (model.Ico == null) ? new SqlParameter("@Ico", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@Ico", SqlDbType.VarChar, 500) { Value = model.Ico };
            SqlParameter parameter9 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[5] = parameter9;
            parameterArray1[6] = (model.IcoColor == null) ? new SqlParameter("@IcoColor", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@IcoColor", SqlDbType.VarChar, 50) { Value = model.IcoColor };
            SqlParameter parameter12 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[7] = parameter12;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int UpdateSort(Guid id, int sort)
        {
            string sql = "UPDATE Menu SET Sort=@Sort WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[2];
            SqlParameter parameter1 = new SqlParameter("@Sort", SqlDbType.Int) {
                Value = sort
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[1] = parameter2;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

