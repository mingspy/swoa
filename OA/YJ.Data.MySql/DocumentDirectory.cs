using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using YJ.Data.Interface;
using YJ.Data.Model;
namespace YJ.Data.MySql
{


    public class DocumentDirectory : IDocumentDirectory
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.DocumentDirectory model)
        {
            string sql = "INSERT INTO documentdirectory\r\n\t\t\t\t(ID,ParentID,Name,ReadUsers,ManageUsers,PublishUsers,Sort) \r\n\t\t\t\tVALUES(@ID,@ParentID,@Name,@ReadUsers,@ManageUsers,@PublishUsers,@Sort)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[7];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@ParentID", MySqlDbType.VarChar, 0x24) {
                Value = model.ParentID
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Name", MySqlDbType.Text, -1) {
                Value = model.Name
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = (model.ReadUsers == null) ? new MySqlParameter("@ReadUsers", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@ReadUsers", MySqlDbType.LongText, -1) { Value = model.ReadUsers };
            parameterArray1[4] = (model.ManageUsers == null) ? new MySqlParameter("@ManageUsers", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@ManageUsers", MySqlDbType.LongText, -1) { Value = model.ManageUsers };
            parameterArray1[5] = (model.PublishUsers == null) ? new MySqlParameter("@PublishUsers", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@PublishUsers", MySqlDbType.LongText, -1) { Value = model.PublishUsers };
            MySqlParameter parameter10 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[6] = parameter10;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.DocumentDirectory> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.DocumentDirectory> list = new List<YJ.Data.Model.DocumentDirectory>();
            YJ.Data.Model.DocumentDirectory item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.DocumentDirectory {
                    ID = dataReader.GetString(0).ToGuid(),
                    ParentID = dataReader.GetString(1).ToGuid(),
                    Name = dataReader.GetString(2)
                };
                if (!dataReader.IsDBNull(3))
                {
                    item.ReadUsers = dataReader.GetString(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    item.ManageUsers = dataReader.GetString(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    item.PublishUsers = dataReader.GetString(5);
                }
                item.Sort = dataReader.GetInt32(6);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM documentdirectory WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.DocumentDirectory Get(Guid id)
        {
            string sql = "SELECT * FROM documentdirectory WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.DocumentDirectory> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.DocumentDirectory> GetAll()
        {
            string sql = "SELECT * FROM documentdirectory";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.DocumentDirectory> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.DocumentDirectory> GetChilds(Guid id)
        {
            string sql = "SELECT * FROM DocumentDirectory WHERE ParentID=@ParentID ORDER BY Sort";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ParentID", MySqlDbType.VarChar) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.DocumentDirectory> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM documentdirectory";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int GetMaxSort(Guid dirID)
        {
            string sql = "SELECT (IFNULL(MAX(Sort),0)+5) Sort FROM DocumentDirectory WHERE ParentID=@ParentID ";
            MySqlParameter[] parameter = new MySqlParameter[] { new MySqlParameter("@ParentID", dirID) };
            return this.dbHelper.GetFieldValue(sql, parameter).ToInt();
        }

        public int Update(YJ.Data.Model.DocumentDirectory model)
        {
            string sql = "UPDATE documentdirectory SET \r\n\t\t\t\tParentID=@ParentID,Name=@Name,ReadUsers=@ReadUsers,ManageUsers=@ManageUsers,PublishUsers=@PublishUsers,Sort=@Sort\r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[7];
            MySqlParameter parameter1 = new MySqlParameter("@ParentID", MySqlDbType.VarChar, 0x24) {
                Value = model.ParentID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Name", MySqlDbType.Text, -1) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = (model.ReadUsers == null) ? new MySqlParameter("@ReadUsers", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@ReadUsers", MySqlDbType.LongText, -1) { Value = model.ReadUsers };
            parameterArray1[3] = (model.ManageUsers == null) ? new MySqlParameter("@ManageUsers", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@ManageUsers", MySqlDbType.LongText, -1) { Value = model.ManageUsers };
            parameterArray1[4] = (model.PublishUsers == null) ? new MySqlParameter("@PublishUsers", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@PublishUsers", MySqlDbType.LongText, -1) { Value = model.PublishUsers };
            MySqlParameter parameter9 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[5] = parameter9;
            MySqlParameter parameter10 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[6] = parameter10;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

