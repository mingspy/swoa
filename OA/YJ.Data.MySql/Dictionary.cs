using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using YJ.Data.Interface;
using YJ.Data.Model;
namespace YJ.Data.MySql
{


    public class Dictionary : IDictionary
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.Dictionary model)
        {
            string sql = "INSERT INTO dictionary\r\n\t\t\t\t(ID,ParentID,Title,Code,Value,Note,Other,Sort) \r\n\t\t\t\tVALUES(@ID,@ParentID,@Title,@Code,@Value,@Note,@Other,@Sort)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[8];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@ParentID", MySqlDbType.VarChar, 0x24) {
                Value = model.ParentID
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Title", MySqlDbType.LongText, -1) {
                Value = model.Title
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = (model.Code == null) ? new MySqlParameter("@Code", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Code", MySqlDbType.Text, -1) { Value = model.Code };
            parameterArray1[4] = (model.Value == null) ? new MySqlParameter("@Value", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Value", MySqlDbType.LongText, -1) { Value = model.Value };
            parameterArray1[5] = (model.Note == null) ? new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = model.Note };
            parameterArray1[6] = (model.Other == null) ? new MySqlParameter("@Other", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Other", MySqlDbType.LongText, -1) { Value = model.Other };
            MySqlParameter parameter12 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[7] = parameter12;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.Dictionary> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.Dictionary> list = new List<YJ.Data.Model.Dictionary>();
            YJ.Data.Model.Dictionary item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.Dictionary {
                    ID = dataReader.GetString(0).ToGuid(),
                    ParentID = dataReader.GetString(1).ToGuid(),
                    Title = dataReader.GetString(2)
                };
                if (!dataReader.IsDBNull(3))
                {
                    item.Code = dataReader.GetString(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    item.Value = dataReader.GetString(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    item.Note = dataReader.GetString(5);
                }
                if (!dataReader.IsDBNull(6))
                {
                    item.Other = dataReader.GetString(6);
                }
                item.Sort = dataReader.GetInt32(7);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM dictionary WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.Dictionary Get(Guid id)
        {
            string sql = "SELECT * FROM dictionary WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Dictionary> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.Dictionary> GetAll()
        {
            string sql = "SELECT * FROM dictionary";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Dictionary> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public YJ.Data.Model.Dictionary GetByCode(string code)
        {
            string sql = "SELECT * FROM Dictionary WHERE Code=@Code";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@Code", MySqlDbType.VarChar) {
                Value = code
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Dictionary> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.Dictionary> GetChilds(Guid id)
        {
            string sql = "SELECT * FROM Dictionary WHERE ParentID=@ParentID ORDER BY Sort";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ParentID", MySqlDbType.VarChar) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Dictionary> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.Dictionary> GetChilds(string code)
        {
            string sql = "SELECT * FROM Dictionary WHERE ParentID=(SELECT ID FROM Dictionary WHERE Code=@Code) ORDER BY Sort";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@Code", MySqlDbType.VarChar) {
                Value = code
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Dictionary> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM dictionary";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int GetMaxSort(Guid id)
        {
            int num;
            string sql = "SELECT MAX(Sort)+1 FROM Dictionary WHERE ParentID=@ParentID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ParentID", MySqlDbType.VarChar) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return (this.dbHelper.GetFieldValue(sql, parameter).IsInt(out num) ? num : 1);
        }

        public YJ.Data.Model.Dictionary GetParent(Guid id)
        {
            string sql = "SELECT * FROM Dictionary WHERE ID=(SELECT ParentID FROM Dictionary WHERE ID=@ID)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Dictionary> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public YJ.Data.Model.Dictionary GetRoot()
        {
            string sql = "SELECT * FROM Dictionary WHERE ParentID=@ParentID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ParentID", MySqlDbType.VarChar) {
                Value = Guid.Empty
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Dictionary> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public bool HasChilds(Guid id)
        {
            string sql = "SELECT ID FROM Dictionary WHERE ParentID=@ParentID";
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

        public int Update(YJ.Data.Model.Dictionary model)
        {
            string sql = "UPDATE dictionary SET \r\n\t\t\t\tParentID=@ParentID,Title=@Title,Code=@Code,Value=@Value,Note=@Note,Other=@Other,Sort=@Sort\r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[8];
            MySqlParameter parameter1 = new MySqlParameter("@ParentID", MySqlDbType.VarChar, 0x24) {
                Value = model.ParentID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Title", MySqlDbType.LongText, -1) {
                Value = model.Title
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = (model.Code == null) ? new MySqlParameter("@Code", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Code", MySqlDbType.Text, -1) { Value = model.Code };
            parameterArray1[3] = (model.Value == null) ? new MySqlParameter("@Value", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Value", MySqlDbType.LongText, -1) { Value = model.Value };
            parameterArray1[4] = (model.Note == null) ? new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = model.Note };
            parameterArray1[5] = (model.Other == null) ? new MySqlParameter("@Other", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Other", MySqlDbType.LongText, -1) { Value = model.Other };
            MySqlParameter parameter11 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[6] = parameter11;
            MySqlParameter parameter12 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[7] = parameter12;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int UpdateSort(Guid id, int sort)
        {
            string sql = "UPDATE Dictionary SET Sort=@Sort WHERE ID=@ID";
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

