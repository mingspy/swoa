using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using YJ.Data.Interface;
using YJ.Data.Model;
namespace YJ.Data.MySql
{


    public class Menu : IMenu
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.Menu model)
        {
            string sql = "INSERT INTO menu\r\n\t\t\t\t(ID,ParentID,AppLibraryID,Title,Params,Ico,Sort,IcoColor) \r\n\t\t\t\tVALUES(@ID,@ParentID,@AppLibraryID,@Title,@Params,@Ico,@Sort,@IcoColor)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[8];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@ParentID", MySqlDbType.VarChar, 0x24) {
                Value = model.ParentID
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = !model.AppLibraryID.HasValue ? new MySqlParameter("@AppLibraryID", MySqlDbType.VarChar, 0x24) { Value = DBNull.Value } : new MySqlParameter("@AppLibraryID", MySqlDbType.VarChar, 0x24) { Value = model.AppLibraryID };
            MySqlParameter parameter5 = new MySqlParameter("@Title", MySqlDbType.Text, -1) {
                Value = model.Title
            };
            parameterArray1[3] = parameter5;
            parameterArray1[4] = (model.Params == null) ? new MySqlParameter("@Params", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Params", MySqlDbType.Text, -1) { Value = model.Params };
            parameterArray1[5] = (model.Ico == null) ? new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = model.Ico };
            MySqlParameter parameter10 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[6] = parameter10;
            parameterArray1[7] = (model.IcoColor == null) ? new MySqlParameter("@IcoColor", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@IcoColor", MySqlDbType.VarChar, 50) { Value = model.IcoColor };
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.Menu> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.Menu> list = new List<YJ.Data.Model.Menu>();
            YJ.Data.Model.Menu item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.Menu {
                    ID = dataReader.GetString(0).ToGuid(),
                    ParentID = dataReader.GetString(1).ToGuid()
                };
                if (!dataReader.IsDBNull(2))
                {
                    item.AppLibraryID = new Guid?(dataReader.GetString(2).ToGuid());
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
            string sql = "DELETE FROM menu WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.Menu Get(Guid id)
        {
            string sql = "SELECT * FROM menu WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Menu> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.Menu> GetAll()
        {
            string sql = "SELECT * FROM menu";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Menu> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.Menu> GetAllByApplibaryID(Guid applibaryID)
        {
            string sql = "SELECT * FROM Menu WHERE AppLibraryID=@AppLibraryID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@AppLibraryID", MySqlDbType.VarChar) {
                Value = applibaryID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
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
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ParentID", MySqlDbType.VarChar) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Menu> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM menu";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int GetMaxSort(Guid id)
        {
            int num;
            string sql = "SELECT IFNULL(MAX(Sort),0)+1 FROM Menu WHERE ParentID=@ParentID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ParentID", MySqlDbType.VarChar) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return (this.dbHelper.GetFieldValue(sql, parameter).IsInt(out num) ? num : 1);
        }

        public bool HasChild(Guid id)
        {
            string sql = "SELECT ID FROM Menu WHERE ParentID=@ParentID LIMIT 0,1";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ParentID", MySqlDbType.VarChar) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            bool hasRows = dataReader.HasRows;
            dataReader.Close();
            return hasRows;
        }

        public int Update(YJ.Data.Model.Menu model)
        {
            string sql = "UPDATE menu SET \r\n\t\t\t\tParentID=@ParentID,AppLibraryID=@AppLibraryID,Title=@Title,Params=@Params,Ico=@Ico,Sort=@Sort,IcoColor=@IcoColor \r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[8];
            MySqlParameter parameter1 = new MySqlParameter("@ParentID", MySqlDbType.VarChar, 0x24) {
                Value = model.ParentID
            };
            parameterArray1[0] = parameter1;
            parameterArray1[1] = !model.AppLibraryID.HasValue ? new MySqlParameter("@AppLibraryID", MySqlDbType.VarChar, 0x24) { Value = DBNull.Value } : new MySqlParameter("@AppLibraryID", MySqlDbType.VarChar, 0x24) { Value = model.AppLibraryID };
            MySqlParameter parameter4 = new MySqlParameter("@Title", MySqlDbType.Text, -1) {
                Value = model.Title
            };
            parameterArray1[2] = parameter4;
            parameterArray1[3] = (model.Params == null) ? new MySqlParameter("@Params", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Params", MySqlDbType.Text, -1) { Value = model.Params };
            parameterArray1[4] = (model.Ico == null) ? new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = model.Ico };
            MySqlParameter parameter9 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[5] = parameter9;
            parameterArray1[6] = (model.IcoColor == null) ? new MySqlParameter("@IcoColor", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@IcoColor", MySqlDbType.VarChar, 50) { Value = model.IcoColor };
            MySqlParameter parameter12 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[7] = parameter12;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int UpdateSort(Guid id, int sort)
        {
            string sql = "UPDATE Menu SET Sort=@Sort WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[2];
            MySqlParameter parameter1 = new MySqlParameter("@Sort", MySqlDbType.Int32) {
                Value = sort
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@ID", MySqlDbType.VarChar) {
                Value = id
            };
            parameterArray1[1] = parameter2;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

