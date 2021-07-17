using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using YJ.Data.Interface;
using YJ.Data.Model;

namespace YJ.Data.MySql
{
    

    public class AppLibraryButtons1 : IAppLibraryButtons1
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.AppLibraryButtons1 model)
        {
            string sql = "INSERT INTO applibrarybuttons1\r\n\t\t\t\t(ID,AppLibraryID,ButtonID,Name,Events,Ico,Sort,Type,ShowType,IsValidateShow) \r\n\t\t\t\tVALUES(@ID,@AppLibraryID,@ButtonID,@Name,@Events,@Ico,@Sort,@Type,@ShowType,@IsValidateShow)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[10];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@AppLibraryID", MySqlDbType.VarChar, 0x24) {
                Value = model.AppLibraryID
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = !model.ButtonID.HasValue ? new MySqlParameter("@ButtonID", MySqlDbType.VarChar, 0x24) { Value = DBNull.Value } : new MySqlParameter("@ButtonID", MySqlDbType.VarChar, 0x24) { Value = model.ButtonID };
            MySqlParameter parameter5 = new MySqlParameter("@Name", MySqlDbType.Text, -1) {
                Value = model.Name
            };
            parameterArray1[3] = parameter5;
            MySqlParameter parameter6 = new MySqlParameter("@Events", MySqlDbType.Text, -1) {
                Value = model.Events
            };
            parameterArray1[4] = parameter6;
            parameterArray1[5] = (model.Ico == null) ? new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = model.Ico };
            MySqlParameter parameter9 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[6] = parameter9;
            MySqlParameter parameter10 = new MySqlParameter("@Type", MySqlDbType.Int32, 11) {
                Value = model.Type
            };
            parameterArray1[7] = parameter10;
            MySqlParameter parameter11 = new MySqlParameter("@ShowType", MySqlDbType.Int32, 11) {
                Value = model.ShowType
            };
            parameterArray1[8] = parameter11;
            MySqlParameter parameter12 = new MySqlParameter("@IsValidateShow", MySqlDbType.Int32, 11) {
                Value = model.IsValidateShow
            };
            parameterArray1[9] = parameter12;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.AppLibraryButtons1> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.AppLibraryButtons1> list = new List<YJ.Data.Model.AppLibraryButtons1>();
            YJ.Data.Model.AppLibraryButtons1 item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.AppLibraryButtons1 {
                    ID = dataReader.GetString(0).ToGuid(),
                    AppLibraryID = dataReader.GetString(1).ToGuid()
                };
                if (!dataReader.IsDBNull(2))
                {
                    item.ButtonID = new Guid?(dataReader.GetString(2).ToGuid());
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
            string sql = "DELETE FROM applibrarybuttons1 WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int DeleteByAppID(Guid id)
        {
            string sql = "DELETE FROM AppLibraryButtons1 WHERE AppLibraryID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.AppLibraryButtons1 Get(Guid id)
        {
            string sql = "SELECT * FROM applibrarybuttons1 WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.AppLibraryButtons1> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.AppLibraryButtons1> GetAll()
        {
            string sql = "SELECT * FROM applibrarybuttons1";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.AppLibraryButtons1> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.AppLibraryButtons1> GetAllByAppID(Guid id)
        {
            string sql = "SELECT * FROM AppLibraryButtons1 WHERE AppLibraryID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.AppLibraryButtons1> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM applibrarybuttons1";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.AppLibraryButtons1 model)
        {
            string sql = "UPDATE applibrarybuttons1 SET \r\n\t\t\t\tAppLibraryID=@AppLibraryID,ButtonID=@ButtonID,Name=@Name,Events=@Events,Ico=@Ico,Sort=@Sort,Type=@Type,ShowType=@ShowType,IsValidateShow=@IsValidateShow\r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[10];
            MySqlParameter parameter1 = new MySqlParameter("@AppLibraryID", MySqlDbType.VarChar, 0x24) {
                Value = model.AppLibraryID
            };
            parameterArray1[0] = parameter1;
            parameterArray1[1] = !model.ButtonID.HasValue ? new MySqlParameter("@ButtonID", MySqlDbType.VarChar, 0x24) { Value = DBNull.Value } : new MySqlParameter("@ButtonID", MySqlDbType.VarChar, 0x24) { Value = model.ButtonID };
            MySqlParameter parameter4 = new MySqlParameter("@Name", MySqlDbType.Text, -1) {
                Value = model.Name
            };
            parameterArray1[2] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@Events", MySqlDbType.Text, -1) {
                Value = model.Events
            };
            parameterArray1[3] = parameter5;
            parameterArray1[4] = (model.Ico == null) ? new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = model.Ico };
            MySqlParameter parameter8 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[5] = parameter8;
            MySqlParameter parameter9 = new MySqlParameter("@Type", MySqlDbType.Int32, 11) {
                Value = model.Type
            };
            parameterArray1[6] = parameter9;
            MySqlParameter parameter10 = new MySqlParameter("@ShowType", MySqlDbType.Int32, 11) {
                Value = model.ShowType
            };
            parameterArray1[7] = parameter10;
            MySqlParameter parameter11 = new MySqlParameter("@IsValidateShow", MySqlDbType.Int32, 11) {
                Value = model.IsValidateShow
            };
            parameterArray1[8] = parameter11;
            MySqlParameter parameter12 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[9] = parameter12;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

