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

    public class AppLibrary : IAppLibrary
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.AppLibrary model)
        {
            string sql = "INSERT INTO AppLibrary\r\n\t\t\t\t(ID,Title,Address,Type,OpenMode,Width,Height,Params,Manager,Note,Code,Ico,Color) \r\n\t\t\t\tVALUES(@ID,@Title,@Address,@Type,@OpenMode,@Width,@Height,@Params,@Manager,@Note,@Code,@Ico,@Color)";
            SqlParameter[] parameterArray1 = new SqlParameter[13];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Title", SqlDbType.NVarChar, 510) {
                Value = model.Title
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Address", SqlDbType.VarChar, 200) {
                Value = model.Address
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@Type", SqlDbType.UniqueIdentifier, -1) {
                Value = model.Type
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@OpenMode", SqlDbType.Int, -1) {
                Value = model.OpenMode
            };
            parameterArray1[4] = parameter5;
            parameterArray1[5] = !model.Width.HasValue ? new SqlParameter("@Width", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@Width", SqlDbType.Int, -1) { Value = model.Width };
            parameterArray1[6] = !model.Height.HasValue ? new SqlParameter("@Height", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@Height", SqlDbType.Int, -1) { Value = model.Height };
            parameterArray1[7] = (model.Params == null) ? new SqlParameter("@Params", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Params", SqlDbType.VarChar, -1) { Value = model.Params };
            parameterArray1[8] = (model.Manager == null) ? new SqlParameter("@Manager", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Manager", SqlDbType.VarChar, -1) { Value = model.Manager };
            parameterArray1[9] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.VarChar, -1) { Value = model.Note };
            parameterArray1[10] = (model.Code == null) ? new SqlParameter("@Code", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Code", SqlDbType.VarChar, 50) { Value = model.Code };
            parameterArray1[11] = (model.Ico == null) ? new SqlParameter("@Ico", SqlDbType.VarChar, 0x7d0) { Value = DBNull.Value } : new SqlParameter("@Ico", SqlDbType.VarChar, 0x7d0) { Value = model.Ico };
            parameterArray1[12] = (model.Color == null) ? new SqlParameter("@Color", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Color", SqlDbType.VarChar, 50) { Value = model.Color };
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.AppLibrary> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.AppLibrary> list = new List<YJ.Data.Model.AppLibrary>();
            YJ.Data.Model.AppLibrary item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.AppLibrary {
                    ID = dataReader.GetGuid(0),
                    Title = dataReader.GetString(1),
                    Address = dataReader.GetString(2),
                    Type = dataReader.GetGuid(3),
                    OpenMode = dataReader.GetInt32(4)
                };
                if (!dataReader.IsDBNull(5))
                {
                    item.Width = new int?(dataReader.GetInt32(5));
                }
                if (!dataReader.IsDBNull(6))
                {
                    item.Height = new int?(dataReader.GetInt32(6));
                }
                if (!dataReader.IsDBNull(7))
                {
                    item.Params = dataReader.GetString(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    item.Manager = dataReader.GetString(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    item.Note = dataReader.GetString(9);
                }
                if (!dataReader.IsDBNull(10))
                {
                    item.Code = dataReader.GetString(10);
                }
                if (!dataReader.IsDBNull(11))
                {
                    item.Ico = dataReader.GetString(11);
                }
                if (!dataReader.IsDBNull(12))
                {
                    item.Color = dataReader.GetString(12);
                }
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM AppLibrary WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int Delete(string[] idArray)
        {
            string sql = "DELETE FROM AppLibrary WHERE ID in(" + Tools.GetSqlInString<string>(idArray, true) + ")";
            return this.dbHelper.Execute(sql);
        }

        public YJ.Data.Model.AppLibrary Get(Guid id)
        {
            string sql = "SELECT * FROM AppLibrary WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.AppLibrary> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.AppLibrary> GetAll()
        {
            string sql = "SELECT * FROM AppLibrary";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.AppLibrary> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.AppLibrary> GetAllByType(string types)
        {
            string sql = "SELECT * FROM AppLibrary WHERE Type IN(" + Tools.GetSqlInString(types, true, ",") + ")";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.AppLibrary> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public YJ.Data.Model.AppLibrary GetByCode(string code)
        {
            string sql = "SELECT * FROM AppLibrary WHERE Code=@Code";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@Code", SqlDbType.VarChar, 50) {
                Value = code
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.AppLibrary> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM AppLibrary";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public List<YJ.Data.Model.AppLibrary> GetPagerData(out long count, [Optional, DefaultParameterValue(15)] int size, [Optional, DefaultParameterValue(1)] int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string type, [Optional, DefaultParameterValue("")] string address, [Optional, DefaultParameterValue("")] string order)
        {
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            if (!title.IsNullOrEmpty())
            {
                builder.Append("AND CHARINDEX(@Title,Title)>0 ");
                SqlParameter item = new SqlParameter("@Title", SqlDbType.NVarChar) {
                    Value = title
                };
                list.Add(item);
            }
            if (!type.IsNullOrEmpty())
            {
                builder.AppendFormat("AND Type IN({0}) ", Tools.GetSqlInString(type, true, ","));
            }
            if (!address.IsNullOrEmpty())
            {
                builder.Append("AND CHARINDEX(@Address,Address)>0 ");
                SqlParameter parameter2 = new SqlParameter("@Address", SqlDbType.VarChar) {
                    Value = address
                };
                list.Add(parameter2);
            }
            string sql = this.dbHelper.GetPaerSql("AppLibrary", "*", builder.ToString(), order.IsNullOrEmpty() ? "Type,Title" : order, size, number, out count, list.ToArray());
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.AppLibrary> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public List<YJ.Data.Model.AppLibrary> GetPagerData(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("Type,Title")] string order, [Optional, DefaultParameterValue(15)] int size, [Optional, DefaultParameterValue(1)] int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string type, [Optional, DefaultParameterValue("")] string address)
        {
            long num;
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            if (!title.IsNullOrEmpty())
            {
                builder.Append("AND CHARINDEX(@Title,Title)>0 ");
                SqlParameter item = new SqlParameter("@Title", SqlDbType.NVarChar) {
                    Value = title
                };
                list.Add(item);
            }
            if (!type.IsNullOrEmpty())
            {
                builder.AppendFormat("AND Type IN({0}) ", Tools.GetSqlInString(type, true, ","));
            }
            if (!address.IsNullOrEmpty())
            {
                builder.Append("AND CHARINDEX(@Address,Address)>0 ");
                SqlParameter parameter2 = new SqlParameter("@Address", SqlDbType.VarChar) {
                    Value = address
                };
                list.Add(parameter2);
            }
            string sql = this.dbHelper.GetPaerSql("AppLibrary", "*", builder.ToString(), order, size, number, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, size, number, query);
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.AppLibrary> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public int Update(YJ.Data.Model.AppLibrary model)
        {
            string sql = "UPDATE AppLibrary SET \r\n\t\t\t\tTitle=@Title,Address=@Address,Type=@Type,OpenMode=@OpenMode,Width=@Width,Height=@Height,Params=@Params,Manager=@Manager,Note=@Note,Code=@Code,Ico=@Ico,Color=@Color\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[13];
            SqlParameter parameter1 = new SqlParameter("@Title", SqlDbType.NVarChar, 510) {
                Value = model.Title
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Address", SqlDbType.VarChar, 200) {
                Value = model.Address
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Type", SqlDbType.UniqueIdentifier, -1) {
                Value = model.Type
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@OpenMode", SqlDbType.Int, -1) {
                Value = model.OpenMode
            };
            parameterArray1[3] = parameter4;
            parameterArray1[4] = !model.Width.HasValue ? new SqlParameter("@Width", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@Width", SqlDbType.Int, -1) { Value = model.Width };
            parameterArray1[5] = !model.Height.HasValue ? new SqlParameter("@Height", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@Height", SqlDbType.Int, -1) { Value = model.Height };
            parameterArray1[6] = (model.Params == null) ? new SqlParameter("@Params", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Params", SqlDbType.VarChar, -1) { Value = model.Params };
            parameterArray1[7] = (model.Manager == null) ? new SqlParameter("@Manager", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Manager", SqlDbType.VarChar, -1) { Value = model.Manager };
            parameterArray1[8] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.VarChar, -1) { Value = model.Note };
            parameterArray1[9] = (model.Code == null) ? new SqlParameter("@Code", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Code", SqlDbType.VarChar, 50) { Value = model.Code };
            parameterArray1[10] = (model.Ico == null) ? new SqlParameter("@Ico", SqlDbType.VarChar, 0x7d0) { Value = DBNull.Value } : new SqlParameter("@Ico", SqlDbType.VarChar, 0x7d0) { Value = model.Ico };
            parameterArray1[11] = (model.Color == null) ? new SqlParameter("@Color", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Color", SqlDbType.VarChar, 50) { Value = model.Color };
            SqlParameter parameter21 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[12] = parameter21;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

