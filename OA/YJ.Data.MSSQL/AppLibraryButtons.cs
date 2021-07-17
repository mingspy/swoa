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

    public class AppLibraryButtons : IAppLibraryButtons
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.AppLibraryButtons model)
        {
            string sql = "INSERT INTO AppLibraryButtons\r\n\t\t\t\t(ID,Name,Events,Ico,Sort,Note) \r\n\t\t\t\tVALUES(@ID,@Name,@Events,@Ico,@Sort,@Note)";
            SqlParameter[] parameterArray1 = new SqlParameter[6];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Name", SqlDbType.NVarChar, 100) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Events", SqlDbType.VarChar, 0x1388) {
                Value = model.Events
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = (model.Ico == null) ? new SqlParameter("@Ico", SqlDbType.VarChar, 0x1388) { Value = DBNull.Value } : new SqlParameter("@Ico", SqlDbType.VarChar, 0x1388) { Value = model.Ico };
            SqlParameter parameter6 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[4] = parameter6;
            parameterArray1[5] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.NVarChar, 0x3e8) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.NVarChar, 0x3e8) { Value = model.Note };
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.AppLibraryButtons> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.AppLibraryButtons> list = new List<YJ.Data.Model.AppLibraryButtons>();
            YJ.Data.Model.AppLibraryButtons item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.AppLibraryButtons {
                    ID = dataReader.GetGuid(0),
                    Name = dataReader.GetString(1),
                    Events = dataReader.GetString(2)
                };
                if (!dataReader.IsDBNull(3))
                {
                    item.Ico = dataReader.GetString(3);
                }
                item.Sort = dataReader.GetInt32(4);
                if (!dataReader.IsDBNull(5))
                {
                    item.Note = dataReader.GetString(5);
                }
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM AppLibraryButtons WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.AppLibraryButtons Get(Guid id)
        {
            string sql = "SELECT * FROM AppLibraryButtons WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.AppLibraryButtons> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.AppLibraryButtons> GetAll()
        {
            string sql = "SELECT * FROM AppLibraryButtons";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.AppLibraryButtons> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM AppLibraryButtons";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int GetMaxSort()
        {
            string sql = "SELECT ISNULL(MAX(Sort),0)+5 FROM AppLibraryButtons";
            return this.dbHelper.GetFieldValue(sql).ToInt();
        }

        public List<YJ.Data.Model.AppLibraryButtons> GetPagerData(out long count, int size, int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string order)
        {
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            if (!title.IsNullOrEmpty())
            {
                builder.Append("AND CHARINDEX(@Name,Name)>0 ");
                SqlParameter item = new SqlParameter("@Name", SqlDbType.NVarChar) {
                    Value = title
                };
                list.Add(item);
            }
            string sql = this.dbHelper.GetPaerSql("AppLibraryButtons", "*", builder.ToString(), order.IsNullOrEmpty() ? "Sort DESC" : order, size, number, out count, list.ToArray());
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.AppLibraryButtons> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public List<YJ.Data.Model.AppLibraryButtons> GetPagerData(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue(15)] int size, [Optional, DefaultParameterValue(1)] int number, [Optional, DefaultParameterValue("")] string title)
        {
            long num;
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            if (!title.IsNullOrEmpty())
            {
                builder.Append("AND CHARINDEX(@Name,Name)>0 ");
                SqlParameter item = new SqlParameter("@Name", SqlDbType.NVarChar) {
                    Value = title
                };
                list.Add(item);
            }
            string sql = this.dbHelper.GetPaerSql("AppLibraryButtons", "*", builder.ToString(), "Sort DESC", size, number, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, size, number, query);
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.AppLibraryButtons> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public int Update(YJ.Data.Model.AppLibraryButtons model)
        {
            string sql = "UPDATE AppLibraryButtons SET \r\n\t\t\t\tName=@Name,Events=@Events,Ico=@Ico,Sort=@Sort,Note=@Note\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[6];
            SqlParameter parameter1 = new SqlParameter("@Name", SqlDbType.NVarChar, 100) {
                Value = model.Name
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Events", SqlDbType.VarChar, 0x1388) {
                Value = model.Events
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = (model.Ico == null) ? new SqlParameter("@Ico", SqlDbType.VarChar, 0x1388) { Value = DBNull.Value } : new SqlParameter("@Ico", SqlDbType.VarChar, 0x1388) { Value = model.Ico };
            SqlParameter parameter5 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[3] = parameter5;
            parameterArray1[4] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.NVarChar, 0x3e8) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.NVarChar, 0x3e8) { Value = model.Note };
            SqlParameter parameter8 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[5] = parameter8;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

