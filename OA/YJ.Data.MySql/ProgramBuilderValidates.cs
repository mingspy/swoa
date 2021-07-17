using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using YJ.Data.Interface;
using YJ.Data.Model;
namespace YJ.Data.MySql
{


    public class ProgramBuilderValidates : IProgramBuilderValidates
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.ProgramBuilderValidates model)
        {
            string sql = "INSERT INTO programbuildervalidates\r\n\t\t\t\t(ID,ProgramID,TableName,FieldName,FieldNote,Validate) \r\n\t\t\t\tVALUES(@ID,@ProgramID,@TableName,@FieldName,@FieldNote,@Validate)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[6];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@ProgramID", MySqlDbType.VarChar, 0x24) {
                Value = model.ProgramID
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@TableName", MySqlDbType.Text, -1) {
                Value = model.TableName
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@FieldName", MySqlDbType.Text, -1) {
                Value = model.FieldName
            };
            parameterArray1[3] = parameter4;
            parameterArray1[4] = (model.FieldNote == null) ? new MySqlParameter("@FieldNote", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@FieldNote", MySqlDbType.Text, -1) { Value = model.FieldNote };
            MySqlParameter parameter7 = new MySqlParameter("@Validate", MySqlDbType.Int32, 11) {
                Value = model.Validate
            };
            parameterArray1[5] = parameter7;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.ProgramBuilderValidates> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.ProgramBuilderValidates> list = new List<YJ.Data.Model.ProgramBuilderValidates>();
            YJ.Data.Model.ProgramBuilderValidates item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.ProgramBuilderValidates {
                    ID = dataReader.GetString(0).ToGuid(),
                    ProgramID = dataReader.GetString(1).ToGuid(),
                    TableName = dataReader.GetString(2),
                    FieldName = dataReader.GetString(3)
                };
                if (!dataReader.IsDBNull(4))
                {
                    item.FieldNote = dataReader.GetString(4);
                }
                item.Validate = dataReader.GetInt32(5);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM programbuildervalidates WHERE ID=@ID";
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
            string sql = "DELETE FROM ProgramBuilderValidates WHERE ProgramID=@ProgramID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ProgramID", MySqlDbType.VarChar) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.ProgramBuilderValidates Get(Guid id)
        {
            string sql = "SELECT * FROM programbuildervalidates WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilderValidates> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.ProgramBuilderValidates> GetAll()
        {
            string sql = "SELECT * FROM programbuildervalidates";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.ProgramBuilderValidates> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.ProgramBuilderValidates> GetAll(Guid programID)
        {
            string sql = "SELECT * FROM ProgramBuilderValidates WHERE ProgramID=@ProgramID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ProgramID", MySqlDbType.VarChar) {
                Value = programID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilderValidates> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM programbuildervalidates";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.ProgramBuilderValidates model)
        {
            string sql = "UPDATE programbuildervalidates SET \r\n\t\t\t\tProgramID=@ProgramID,TableName=@TableName,FieldName=@FieldName,FieldNote=@FieldNote,Validate=@Validate\r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[6];
            MySqlParameter parameter1 = new MySqlParameter("@ProgramID", MySqlDbType.VarChar, 0x24) {
                Value = model.ProgramID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@TableName", MySqlDbType.Text, -1) {
                Value = model.TableName
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@FieldName", MySqlDbType.Text, -1) {
                Value = model.FieldName
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = (model.FieldNote == null) ? new MySqlParameter("@FieldNote", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@FieldNote", MySqlDbType.Text, -1) { Value = model.FieldNote };
            MySqlParameter parameter6 = new MySqlParameter("@Validate", MySqlDbType.Int32, 11) {
                Value = model.Validate
            };
            parameterArray1[4] = parameter6;
            MySqlParameter parameter7 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[5] = parameter7;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

