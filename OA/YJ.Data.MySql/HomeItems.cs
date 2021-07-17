using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using YJ.Data.Interface;
using YJ.Data.Model;
using YJ.Utility;
namespace YJ.Data.MySql
{


    public class HomeItems : IHomeItems
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.HomeItems model)
        {
            string sql = "INSERT INTO homeitems\r\n\t\t\t\t(ID,Type,Name,Title,DataSourceType,DataSource,Ico,BgColor,Color,DBConnID,LinkURL,UseOrganizes,UseUsers,Sort,Note) \r\n\t\t\t\tVALUES(@ID,@Type,@Name,@Title,@DataSourceType,@DataSource,@Ico,@BgColor,@Color,@DBConnID,@LinkURL,@UseOrganizes,@UseUsers,@Sort,@Note)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[15];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Type", MySqlDbType.Int32, 11) {
                Value = model.Type
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Name", MySqlDbType.Text, -1) {
                Value = model.Name
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@Title", MySqlDbType.Text, -1) {
                Value = model.Title
            };
            parameterArray1[3] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@DataSourceType", MySqlDbType.Int32, 11) {
                Value = model.DataSourceType
            };
            parameterArray1[4] = parameter5;
            parameterArray1[5] = (model.DataSource == null) ? new MySqlParameter("@DataSource", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@DataSource", MySqlDbType.LongText, -1) { Value = model.DataSource };
            parameterArray1[6] = (model.Ico == null) ? new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = model.Ico };
            parameterArray1[7] = (model.BgColor == null) ? new MySqlParameter("@BgColor", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@BgColor", MySqlDbType.VarChar, 50) { Value = model.BgColor };
            parameterArray1[8] = (model.Color == null) ? new MySqlParameter("@Color", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@Color", MySqlDbType.VarChar, 50) { Value = model.Color };
            parameterArray1[9] = !model.DBConnID.HasValue ? new MySqlParameter("@DBConnID", MySqlDbType.VarChar, 0x24) { Value = DBNull.Value } : new MySqlParameter("@DBConnID", MySqlDbType.VarChar, 0x24) { Value = model.DBConnID };
            parameterArray1[10] = (model.LinkURL == null) ? new MySqlParameter("@LinkURL", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@LinkURL", MySqlDbType.Text, -1) { Value = model.LinkURL };
            parameterArray1[11] = (model.UseOrganizes == null) ? new MySqlParameter("@UseOrganizes", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@UseOrganizes", MySqlDbType.LongText, -1) { Value = model.UseOrganizes };
            parameterArray1[12] = (model.UseUsers == null) ? new MySqlParameter("@UseUsers", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@UseUsers", MySqlDbType.LongText, -1) { Value = model.UseUsers };
            parameterArray1[13] = !model.Sort.HasValue ? new MySqlParameter("@Sort", MySqlDbType.Int32, 11) { Value = DBNull.Value } : new MySqlParameter("@Sort", MySqlDbType.Int32, 11) { Value = model.Sort };
            parameterArray1[14] = (model.Note == null) ? new MySqlParameter("@Note", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Note", MySqlDbType.Text, -1) { Value = model.Note };
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.HomeItems> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.HomeItems> list = new List<YJ.Data.Model.HomeItems>();
            YJ.Data.Model.HomeItems item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.HomeItems {
                    ID = dataReader.GetString(0).ToGuid(),
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
                    item.DBConnID = new Guid?(dataReader.GetString(9).ToGuid());
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
            string sql = "DELETE FROM homeitems WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.HomeItems Get(Guid id)
        {
            string sql = "SELECT * FROM homeitems WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.HomeItems> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.HomeItems> GetAll()
        {
            string sql = "SELECT * FROM homeitems";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.HomeItems> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM homeitems";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public List<YJ.Data.Model.HomeItems> GetList(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string name, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string type)
        {
            long num;
            List<MySqlParameter> list = new List<MySqlParameter>();
            StringBuilder builder = new StringBuilder("SELECT * FROM HomeItems WHERE 1=1 ");
            if (!name.IsNullOrEmpty())
            {
                builder.Append(" AND INSTR(Name,@Name)>0");
                list.Add(new MySqlParameter("@Name", name));
            }
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND INSTR(Title,@Title)>0");
                list.Add(new MySqlParameter("@Title", title));
            }
            if (!type.IsNullOrEmpty())
            {
                builder.Append(" AND Type=@Type");
                list.Add(new MySqlParameter("@Type", type));
            }
            builder.Append(" ORDER BY Type,Sort");
            int pageSize = Tools.GetPageSize();
            int pageNumber = Tools.GetPageNumber();
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), pageSize, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, pageSize, pageNumber, query);
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.HomeItems> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public List<YJ.Data.Model.HomeItems> GetList(out long count, int size, int number, [Optional, DefaultParameterValue("")] string name, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string type, [Optional, DefaultParameterValue("")] string order)
        {
            List<MySqlParameter> list = new List<MySqlParameter>();
            StringBuilder builder = new StringBuilder("SELECT * FROM HomeItems WHERE 1=1 ");
            if (!name.IsNullOrEmpty())
            {
                builder.Append(" AND INSTR(Name,@Name)>0");
                list.Add(new MySqlParameter("@Name", name));
            }
            if (!title.IsNullOrEmpty())
            {
                builder.Append(" AND INSTR(Title,@Title)>0");
                list.Add(new MySqlParameter("@Title", title));
            }
            if (!type.IsNullOrEmpty())
            {
                builder.Append(" AND Type=@Type");
                list.Add(new MySqlParameter("@Type", type));
            }
            builder.Append(order.IsNullOrEmpty() ? " ORDER BY Type,Sort" : (" ORDER BY " + order));
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), size, number, out count, list.ToArray());
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
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
            string sql = "UPDATE homeitems SET \r\n\t\t\t\tType=@Type,Name=@Name,Title=@Title,DataSourceType=@DataSourceType,DataSource=@DataSource,Ico=@Ico,BgColor=@BgColor,Color=@Color,DBConnID=@DBConnID,LinkURL=@LinkURL,UseOrganizes=@UseOrganizes,UseUsers=@UseUsers,Sort=@Sort,Note=@Note\r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[15];
            MySqlParameter parameter1 = new MySqlParameter("@Type", MySqlDbType.Int32, 11) {
                Value = model.Type
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Name", MySqlDbType.Text, -1) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Title", MySqlDbType.Text, -1) {
                Value = model.Title
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@DataSourceType", MySqlDbType.Int32, 11) {
                Value = model.DataSourceType
            };
            parameterArray1[3] = parameter4;
            parameterArray1[4] = (model.DataSource == null) ? new MySqlParameter("@DataSource", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@DataSource", MySqlDbType.LongText, -1) { Value = model.DataSource };
            parameterArray1[5] = (model.Ico == null) ? new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = model.Ico };
            parameterArray1[6] = (model.BgColor == null) ? new MySqlParameter("@BgColor", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@BgColor", MySqlDbType.VarChar, 50) { Value = model.BgColor };
            parameterArray1[7] = (model.Color == null) ? new MySqlParameter("@Color", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@Color", MySqlDbType.VarChar, 50) { Value = model.Color };
            parameterArray1[8] = !model.DBConnID.HasValue ? new MySqlParameter("@DBConnID", MySqlDbType.VarChar, 0x24) { Value = DBNull.Value } : new MySqlParameter("@DBConnID", MySqlDbType.VarChar, 0x24) { Value = model.DBConnID };
            parameterArray1[9] = (model.LinkURL == null) ? new MySqlParameter("@LinkURL", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@LinkURL", MySqlDbType.Text, -1) { Value = model.LinkURL };
            parameterArray1[10] = (model.UseOrganizes == null) ? new MySqlParameter("@UseOrganizes", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@UseOrganizes", MySqlDbType.LongText, -1) { Value = model.UseOrganizes };
            parameterArray1[11] = (model.UseUsers == null) ? new MySqlParameter("@UseUsers", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@UseUsers", MySqlDbType.LongText, -1) { Value = model.UseUsers };
            parameterArray1[12] = !model.Sort.HasValue ? new MySqlParameter("@Sort", MySqlDbType.Int32, 11) { Value = DBNull.Value } : new MySqlParameter("@Sort", MySqlDbType.Int32, 11) { Value = model.Sort };
            parameterArray1[13] = (model.Note == null) ? new MySqlParameter("@Note", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Note", MySqlDbType.Text, -1) { Value = model.Note };
            MySqlParameter parameter25 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[14] = parameter25;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

