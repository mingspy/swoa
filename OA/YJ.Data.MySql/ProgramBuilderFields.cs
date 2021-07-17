using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using YJ.Data.Interface;
using YJ.Data.Model;
namespace YJ.Data.MySql
{


    public class ProgramBuilderFields : IProgramBuilderFields
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.ProgramBuilderFields model)
        {
            string sql = "INSERT INTO programbuilderfields\r\n\t\t\t\t(ID,ProgramID,Field,ShowTitle,Align,Width,ShowType,ShowFormat,CustomString,Sort) \r\n\t\t\t\tVALUES(@ID,@ProgramID,@Field,@ShowTitle,@Align,@Width,@ShowType,@ShowFormat,@CustomString,@Sort)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[10];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@ProgramID", MySqlDbType.VarChar, 0x24) {
                Value = model.ProgramID
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = (model.Field == null) ? new MySqlParameter("@Field", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Field", MySqlDbType.Text, -1) { Value = model.Field };
            MySqlParameter parameter5 = new MySqlParameter("@ShowTitle", MySqlDbType.LongText, -1) {
                Value = model.ShowTitle
            };
            parameterArray1[3] = parameter5;
            MySqlParameter parameter6 = new MySqlParameter("@Align", MySqlDbType.VarChar, 50) {
                Value = model.Align
            };
            parameterArray1[4] = parameter6;
            parameterArray1[5] = (model.Width == null) ? new MySqlParameter("@Width", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@Width", MySqlDbType.VarChar, 50) { Value = model.Width };
            MySqlParameter parameter9 = new MySqlParameter("@ShowType", MySqlDbType.Int32, 11) {
                Value = model.ShowType
            };
            parameterArray1[6] = parameter9;
            parameterArray1[7] = (model.ShowFormat == null) ? new MySqlParameter("@ShowFormat", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@ShowFormat", MySqlDbType.VarChar, 50) { Value = model.ShowFormat };
            parameterArray1[8] = (model.CustomString == null) ? new MySqlParameter("@CustomString", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@CustomString", MySqlDbType.LongText, -1) { Value = model.CustomString };
            MySqlParameter parameter14 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[9] = parameter14;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.ProgramBuilderFields> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.ProgramBuilderFields> list = new List<YJ.Data.Model.ProgramBuilderFields>();
            YJ.Data.Model.ProgramBuilderFields item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.ProgramBuilderFields {
                    ID = dataReader.GetString(0).ToGuid(),
                    ProgramID = dataReader.GetString(1).ToGuid()
                };
                if (!dataReader.IsDBNull(2))
                {
                    item.Field = dataReader.GetString(2);
                }
                item.ShowTitle = dataReader.GetString(3);
                item.Align = dataReader.GetString(4);
                if (!dataReader.IsDBNull(5))
                {
                    item.Width = dataReader.GetString(5);
                }
                item.ShowType = dataReader.GetInt32(6);
                if (!dataReader.IsDBNull(7))
                {
                    item.ShowFormat = dataReader.GetString(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    item.CustomString = dataReader.GetString(8);
                }
                item.Sort = dataReader.GetInt32(9);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM programbuilderfields WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int DeleteByProgramID(Guid id)
        {
            string sql = "DELETE FROM ProgramBuilderFields WHERE ProgramID=@ProgramID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ProgramID", MySqlDbType.VarChar) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.ProgramBuilderFields Get(Guid id)
        {
            string sql = "SELECT * FROM programbuilderfields WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilderFields> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.ProgramBuilderFields> GetAll()
        {
            string sql = "SELECT * FROM programbuilderfields";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.ProgramBuilderFields> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.ProgramBuilderFields> GetAll(Guid programID)
        {
            string sql = "SELECT * FROM ProgramBuilderFields WHERE ProgramID=@ProgramID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ProgramID", MySqlDbType.VarChar) {
                Value = programID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilderFields> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM programbuilderfields";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.ProgramBuilderFields model)
        {
            string sql = "UPDATE programbuilderfields SET \r\n\t\t\t\tProgramID=@ProgramID,Field=@Field,ShowTitle=@ShowTitle,Align=@Align,Width=@Width,ShowType=@ShowType,ShowFormat=@ShowFormat,CustomString=@CustomString,Sort=@Sort\r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[10];
            MySqlParameter parameter1 = new MySqlParameter("@ProgramID", MySqlDbType.VarChar, 0x24) {
                Value = model.ProgramID
            };
            parameterArray1[0] = parameter1;
            parameterArray1[1] = (model.Field == null) ? new MySqlParameter("@Field", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Field", MySqlDbType.Text, -1) { Value = model.Field };
            MySqlParameter parameter4 = new MySqlParameter("@ShowTitle", MySqlDbType.LongText, -1) {
                Value = model.ShowTitle
            };
            parameterArray1[2] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@Align", MySqlDbType.VarChar, 50) {
                Value = model.Align
            };
            parameterArray1[3] = parameter5;
            parameterArray1[4] = (model.Width == null) ? new MySqlParameter("@Width", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@Width", MySqlDbType.VarChar, 50) { Value = model.Width };
            MySqlParameter parameter8 = new MySqlParameter("@ShowType", MySqlDbType.Int32, 11) {
                Value = model.ShowType
            };
            parameterArray1[5] = parameter8;
            parameterArray1[6] = (model.ShowFormat == null) ? new MySqlParameter("@ShowFormat", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@ShowFormat", MySqlDbType.VarChar, 50) { Value = model.ShowFormat };
            parameterArray1[7] = (model.CustomString == null) ? new MySqlParameter("@CustomString", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@CustomString", MySqlDbType.LongText, -1) { Value = model.CustomString };
            MySqlParameter parameter13 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[8] = parameter13;
            MySqlParameter parameter14 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[9] = parameter14;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

