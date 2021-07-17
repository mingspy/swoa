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


    public class AppLibraryButtons : IAppLibraryButtons
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.AppLibraryButtons model)
        {
            string sql = "INSERT INTO applibrarybuttons\r\n\t\t\t\t(ID,Name,Events,Ico,Sort,Note) \r\n\t\t\t\tVALUES(@ID,@Name,@Events,@Ico,@Sort,@Note)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[6];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Name", MySqlDbType.VarChar, 50) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Events", MySqlDbType.Text, -1) {
                Value = model.Events
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = (model.Ico == null) ? new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = model.Ico };
            MySqlParameter parameter6 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[4] = parameter6;
            parameterArray1[5] = (model.Note == null) ? new MySqlParameter("@Note", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Note", MySqlDbType.Text, -1) { Value = model.Note };
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.AppLibraryButtons> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.AppLibraryButtons> list = new List<YJ.Data.Model.AppLibraryButtons>();
            YJ.Data.Model.AppLibraryButtons item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.AppLibraryButtons {
                    ID = dataReader.GetString(0).ToGuid(),
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
            string sql = "DELETE FROM applibrarybuttons WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.AppLibraryButtons Get(Guid id)
        {
            string sql = "SELECT * FROM applibrarybuttons WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.AppLibraryButtons> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.AppLibraryButtons> GetAll()
        {
            string sql = "SELECT * FROM applibrarybuttons";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.AppLibraryButtons> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM applibrarybuttons";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int GetMaxSort()
        {
            string sql = "SELECT IFNULL(MAX(Sort),0)+5 FROM AppLibraryButtons";
            return this.dbHelper.GetFieldValue(sql).ToInt();
        }

        public List<YJ.Data.Model.AppLibraryButtons> GetPagerData(out long count, int size, int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string order)
        {
            StringBuilder builder = new StringBuilder();
            List<MySqlParameter> list = new List<MySqlParameter>();
            if (!title.IsNullOrEmpty())
            {
                builder.Append("AND INSTR(Name,@Name)>0 ");
                MySqlParameter item = new MySqlParameter("@Name", MySqlDbType.VarChar) {
                    Value = title
                };
                list.Add(item);
            }
            string sql = this.dbHelper.GetPaerSql("AppLibraryButtons", "*", builder.ToString(), order.IsNullOrEmpty() ? "Sort DESC" : order, size, number, out count, list.ToArray());
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.AppLibraryButtons> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public List<YJ.Data.Model.AppLibraryButtons> GetPagerData(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue(15)] int size, [Optional, DefaultParameterValue(1)] int number, [Optional, DefaultParameterValue("")] string title)
        {
            long num;
            StringBuilder builder = new StringBuilder();
            List<MySqlParameter> list = new List<MySqlParameter>();
            if (!title.IsNullOrEmpty())
            {
                builder.Append("AND INSTR(Name,@Name)>0 ");
                MySqlParameter item = new MySqlParameter("@Name", MySqlDbType.VarChar) {
                    Value = title
                };
                list.Add(item);
            }
            string sql = this.dbHelper.GetPaerSql("AppLibraryButtons", "*", builder.ToString(), "Sort DESC", size, number, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, size, number, query);
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.AppLibraryButtons> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public int Update(YJ.Data.Model.AppLibraryButtons model)
        {
            string sql = "UPDATE applibrarybuttons SET \r\n\t\t\t\tName=@Name,Events=@Events,Ico=@Ico,Sort=@Sort,Note=@Note\r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[6];
            MySqlParameter parameter1 = new MySqlParameter("@Name", MySqlDbType.VarChar, 50) {
                Value = model.Name
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Events", MySqlDbType.Text, -1) {
                Value = model.Events
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = (model.Ico == null) ? new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = model.Ico };
            MySqlParameter parameter5 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[3] = parameter5;
            parameterArray1[4] = (model.Note == null) ? new MySqlParameter("@Note", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Note", MySqlDbType.Text, -1) { Value = model.Note };
            MySqlParameter parameter8 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[5] = parameter8;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

