namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;
    using YJ.Data.Interface;
    using YJ.Data.Model;
    using YJ.Utility;

    public class HomeItems : IHomeItems
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.HomeItems model)
        {
            string sql = "INSERT INTO HomeItems\r\n\t\t\t\t(ID,Type,Name,Title,DataSourceType,DataSource,Ico,BgColor,Color,DBConnID,LinkURL,UseOrganizes,UseUsers,Sort,Note) \r\n\t\t\t\tVALUES(@ID,@Type,@Name,@Title,@DataSourceType,@DataSource,@Ico,@BgColor,@Color,@DBConnID,@LinkURL,@UseOrganizes,@UseUsers,@Sort,@Note)";
            SqlParameter[] parameterArray1 = new SqlParameter[15];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Type", SqlDbType.Int, -1) {
                Value = model.Type
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Name", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Name
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@Title", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Title
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@DataSourceType", SqlDbType.Int, -1) {
                Value = model.DataSourceType
            };
            parameterArray1[4] = parameter5;
            parameterArray1[5] = (model.DataSource == null) ? new SqlParameter("@DataSource", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@DataSource", SqlDbType.VarChar, -1) { Value = model.DataSource };
            parameterArray1[6] = (model.Ico == null) ? new SqlParameter("@Ico", SqlDbType.VarChar, 0x7d0) { Value = DBNull.Value } : new SqlParameter("@Ico", SqlDbType.VarChar, 0x7d0) { Value = model.Ico };
            parameterArray1[7] = (model.BgColor == null) ? new SqlParameter("@BgColor", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@BgColor", SqlDbType.VarChar, 50) { Value = model.BgColor };
            parameterArray1[8] = (model.Color == null) ? new SqlParameter("@Color", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Color", SqlDbType.VarChar, 50) { Value = model.Color };
            parameterArray1[9] = !model.DBConnID.HasValue ? new SqlParameter("@DBConnID", SqlDbType.UniqueIdentifier, -1) { Value = DBNull.Value } : new SqlParameter("@DBConnID", SqlDbType.UniqueIdentifier, -1) { Value = model.DBConnID };
            parameterArray1[10] = (model.LinkURL == null) ? new SqlParameter("@LinkURL", SqlDbType.VarChar, 0x7d0) { Value = DBNull.Value } : new SqlParameter("@LinkURL", SqlDbType.VarChar, 0x7d0) { Value = model.LinkURL };
            parameterArray1[11] = (model.UseOrganizes == null) ? new SqlParameter("@UseOrganizes", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@UseOrganizes", SqlDbType.VarChar, -1) { Value = model.UseOrganizes };
            parameterArray1[12] = (model.UseUsers == null) ? new SqlParameter("@UseUsers", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@UseUsers", SqlDbType.VarChar, -1) { Value = model.UseUsers };
            parameterArray1[13] = !model.Sort.HasValue ? new SqlParameter("@Sort", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@Sort", SqlDbType.Int, -1) { Value = model.Sort };
            parameterArray1[14] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.NVarChar, 0xfa0) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.NVarChar, 0xfa0) { Value = model.Note };
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.HomeItems> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.HomeItems> list = new List<YJ.Data.Model.HomeItems>();
            YJ.Data.Model.HomeItems item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.HomeItems {
                    ID = dataReader.GetGuid(0),
                    Type = dataReader.GetInt32(1),
                    Name = dataReader.GetString(2),
                    Title = dataReader.GetString(3),
                    DataSourceType = dataReader.GetInt32(4)
                };
                if (!dataReader.IsDBNull(5))
                {
                    item.DataSource = dataReader.GetString(5);
                }
                if (!dataReader.IsDBNull(6))
                {
                    item.Ico = dataReader.GetString(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    item.BgColor = dataReader.GetString(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    item.Color = dataReader.GetString(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    item.DBConnID = new Guid?(dataReader.GetGuid(9));
                }
                if (!dataReader.IsDBNull(10))
                {
                    item.LinkURL = dataReader.GetString(10);
                }
                if (!dataReader.IsDBNull(11))
                {
                    item.UseOrganizes = dataReader.GetString(11);
                }
                if (!dataReader.IsDBNull(12))
                {
                    item.UseUsers = dataReader.GetString(12);
                }
                if (!dataReader.IsDBNull(13))
                {
                    item.Sort = new int?(dataReader.GetInt32(13));
                }
                if (!dataReader.IsDBNull(14))
                {
                    item.Note = dataReader.GetString(14);
                }
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM HomeItems WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.HomeItems Get(Guid id)
        {
            string sql = "SELECT * FROM HomeItems WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.HomeItems> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.HomeItems> GetAll()
        {
            string sql = "SELECT * FROM HomeItems";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.HomeItems> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM HomeItems";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public List<YJ.Data.Model.HomeItems> GetList(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string name, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string type)
        {
            long num;
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder("SELECT *,ROW_NUMBER() OVER(ORDER BY Type,Sort) AS PagerAutoRowNumber FROM HomeItems WHERE 1=1 ");
            if (!name.IsNullOrEmpty())
            {
                builder.Append(" AND CHARINDEX(@Name,Name)>0");
                list.Add(new SqlParameter("@Name", name));
            }
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND CHARINDEX(@Title,Title)>0");
                list.Add(new SqlParameter("@Title", title));
            }
            if (!type.IsNullOrEmpty())
            {
                builder.Append(" AND Type=@Type");
                list.Add(new SqlParameter("@Type", type));
            }
            int pageSize = Tools.GetPageSize();
            int pageNumber = Tools.GetPageNumber();
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), pageSize, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, pageSize, pageNumber, query);
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.HomeItems> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public List<YJ.Data.Model.HomeItems> GetList(out long count, int size, int number, [Optional, DefaultParameterValue("")] string name, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string type, [Optional, DefaultParameterValue("")] string order)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder("SELECT *,ROW_NUMBER() OVER(ORDER BY " + (order.IsNullOrEmpty() ? "Type,Sort" : order) + ") AS PagerAutoRowNumber FROM HomeItems WHERE 1=1 ");
            if (!name.IsNullOrEmpty())
            {
                builder.Append(" AND CHARINDEX(@Name,Name)>0");
                list.Add(new SqlParameter("@Name", name));
            }
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND CHARINDEX(@Title,Title)>0");
                list.Add(new SqlParameter("@Title", title));
            }
            if (!type.IsNullOrEmpty())
            {
                builder.Append(" AND Type=@Type");
                list.Add(new SqlParameter("@Type", type));
            }
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), size, number, out count, list.ToArray());
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.HomeItems> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public int GetMaxSort(int type)
        {
            string sql = "SELECT MAX(Sort) FROM HomeItems WHERE Type=" + type;
            return (this.dbHelper.GetFieldValue(sql).ToInt(0) + 5);
        }

        public int Update(YJ.Data.Model.HomeItems model)
        {
            string sql = "UPDATE HomeItems SET \r\n\t\t\t\tType=@Type,Name=@Name,Title=@Title,DataSourceType=@DataSourceType,DataSource=@DataSource,Ico=@Ico,BgColor=@BgColor,Color=@Color,DBConnID=@DBConnID,LinkURL=@LinkURL,UseOrganizes=@UseOrganizes,UseUsers=@UseUsers,Sort=@Sort,Note=@Note\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[15];
            SqlParameter parameter1 = new SqlParameter("@Type", SqlDbType.Int, -1) {
                Value = model.Type
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Name", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Title", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Title
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@DataSourceType", SqlDbType.Int, -1) {
                Value = model.DataSourceType
            };
            parameterArray1[3] = parameter4;
            parameterArray1[4] = (model.DataSource == null) ? new SqlParameter("@DataSource", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@DataSource", SqlDbType.VarChar, -1) { Value = model.DataSource };
            parameterArray1[5] = (model.Ico == null) ? new SqlParameter("@Ico", SqlDbType.VarChar, 0x7d0) { Value = DBNull.Value } : new SqlParameter("@Ico", SqlDbType.VarChar, 0x7d0) { Value = model.Ico };
            parameterArray1[6] = (model.BgColor == null) ? new SqlParameter("@BgColor", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@BgColor", SqlDbType.VarChar, 50) { Value = model.BgColor };
            parameterArray1[7] = (model.Color == null) ? new SqlParameter("@Color", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Color", SqlDbType.VarChar, 50) { Value = model.Color };
            parameterArray1[8] = !model.DBConnID.HasValue ? new SqlParameter("@DBConnID", SqlDbType.UniqueIdentifier, -1) { Value = DBNull.Value } : new SqlParameter("@DBConnID", SqlDbType.UniqueIdentifier, -1) { Value = model.DBConnID };
            parameterArray1[9] = (model.LinkURL == null) ? new SqlParameter("@LinkURL", SqlDbType.VarChar, 0x7d0) { Value = DBNull.Value } : new SqlParameter("@LinkURL", SqlDbType.VarChar, 0x7d0) { Value = model.LinkURL };
            parameterArray1[10] = (model.UseOrganizes == null) ? new SqlParameter("@UseOrganizes", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@UseOrganizes", SqlDbType.VarChar, -1) { Value = model.UseOrganizes };
            parameterArray1[11] = (model.UseUsers == null) ? new SqlParameter("@UseUsers", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@UseUsers", SqlDbType.VarChar, -1) { Value = model.UseUsers };
            parameterArray1[12] = !model.Sort.HasValue ? new SqlParameter("@Sort", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@Sort", SqlDbType.Int, -1) { Value = model.Sort };
            parameterArray1[13] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.NVarChar, 0xfa0) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.NVarChar, 0xfa0) { Value = model.Note };
            SqlParameter parameter25 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[14] = parameter25;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

