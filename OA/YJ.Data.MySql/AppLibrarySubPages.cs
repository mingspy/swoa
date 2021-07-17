using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using YJ.Data.Interface;
using YJ.Data.Model;
namespace YJ.Data.MySql
{
 

    public class AppLibrarySubPages : IAppLibrarySubPages
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.AppLibrarySubPages model)
        {
            string sql = "INSERT INTO applibrarysubpages\r\n\t\t\t\t(ID,AppLibraryID,Name,Address,Ico,Sort,Note) \r\n\t\t\t\tVALUES(@ID,@AppLibraryID,@Name,@Address,@Ico,@Sort,@Note)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[7];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@AppLibraryID", MySqlDbType.VarChar, 0x24) {
                Value = model.AppLibraryID
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Name", MySqlDbType.Text, -1) {
                Value = model.Name
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@Address", MySqlDbType.Text, -1) {
                Value = model.Address
            };
            parameterArray1[3] = parameter4;
            parameterArray1[4] = (model.Ico == null) ? new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = model.Ico };
            MySqlParameter parameter7 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[5] = parameter7;
            parameterArray1[6] = (model.Note == null) ? new MySqlParameter("@Note", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Note", MySqlDbType.Text, -1) { Value = model.Note };
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.AppLibrarySubPages> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.AppLibrarySubPages> list = new List<YJ.Data.Model.AppLibrarySubPages>();
            YJ.Data.Model.AppLibrarySubPages item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.AppLibrarySubPages {
                    ID = dataReader.GetString(0).ToGuid(),
                    AppLibraryID = dataReader.GetString(1).ToGuid(),
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
            string sql = "DELETE FROM applibrarysubpages WHERE ID=@ID";
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
            string sql = "DELETE FROM AppLibrarySubPages WHERE AppLibraryID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.AppLibrarySubPages Get(Guid id)
        {
            string sql = "SELECT * FROM applibrarysubpages WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.AppLibrarySubPages> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.AppLibrarySubPages> GetAll()
        {
            string sql = "SELECT * FROM applibrarysubpages";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.AppLibrarySubPages> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.AppLibrarySubPages> GetAllByAppID(Guid id)
        {
            string sql = "SELECT * FROM AppLibrarySubPages WHERE AppLibraryID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.AppLibrarySubPages> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM applibrarysubpages";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.AppLibrarySubPages model)
        {
            string sql = "UPDATE applibrarysubpages SET \r\n\t\t\t\tAppLibraryID=@AppLibraryID,Name=@Name,Address=@Address,Ico=@Ico,Sort=@Sort,Note=@Note\r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[7];
            MySqlParameter parameter1 = new MySqlParameter("@AppLibraryID", MySqlDbType.VarChar, 0x24) {
                Value = model.AppLibraryID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Name", MySqlDbType.Text, -1) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Address", MySqlDbType.Text, -1) {
                Value = model.Address
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = (model.Ico == null) ? new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = model.Ico };
            MySqlParameter parameter6 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[4] = parameter6;
            parameterArray1[5] = (model.Note == null) ? new MySqlParameter("@Note", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Note", MySqlDbType.Text, -1) { Value = model.Note };
            MySqlParameter parameter9 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[6] = parameter9;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

