using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using YJ.Data.Interface;
using YJ.Data.Model;
namespace YJ.Data.MySql
{


    public class ProgramBuilderQuerys : IProgramBuilderQuerys
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.ProgramBuilderQuerys model)
        {
            string sql = "INSERT INTO programbuilderquerys\r\n\t\t\t\t(ID,ProgramID,Field,ShowTitle,Operators,ControlName,InputType,Width,Sort,DataSource,DataSourceString,DataLinkID,IsQueryUsers,Value) \r\n\t\t\t\tVALUES(@ID,@ProgramID,@Field,@ShowTitle,@Operators,@ControlName,@InputType,@Width,@Sort,@DataSource,@DataSourceString,@DataLinkID,@IsQueryUsers,@Value)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[14];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@ProgramID", MySqlDbType.VarChar, 0x24) {
                Value = model.ProgramID
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Field", MySqlDbType.Text, -1) {
                Value = model.Field
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@ShowTitle", MySqlDbType.Text, -1) {
                Value = model.ShowTitle
            };
            parameterArray1[3] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@Operators", MySqlDbType.VarChar, 50) {
                Value = model.Operators
            };
            parameterArray1[4] = parameter5;
            MySqlParameter parameter6 = new MySqlParameter("@ControlName", MySqlDbType.VarChar, 50) {
                Value = model.ControlName
            };
            parameterArray1[5] = parameter6;
            MySqlParameter parameter7 = new MySqlParameter("@InputType", MySqlDbType.Int32, 11) {
                Value = model.InputType
            };
            parameterArray1[6] = parameter7;
            parameterArray1[7] = (model.Width == null) ? new MySqlParameter("@Width", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@Width", MySqlDbType.VarChar, 50) { Value = model.Width };
            MySqlParameter parameter10 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[8] = parameter10;
            parameterArray1[9] = !model.DataSource.HasValue ? new MySqlParameter("@DataSource", MySqlDbType.Int32, 11) { Value = DBNull.Value } : new MySqlParameter("@DataSource", MySqlDbType.Int32, 11) { Value = model.DataSource };
            parameterArray1[10] = (model.DataSourceString == null) ? new MySqlParameter("@DataSourceString", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@DataSourceString", MySqlDbType.LongText, -1) { Value = model.DataSourceString };
            parameterArray1[11] = (model.DataLinkID == null) ? new MySqlParameter("@DataLinkID", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@DataLinkID", MySqlDbType.VarChar, 50) { Value = model.DataLinkID };
            parameterArray1[12] = !model.IsQueryUsers.HasValue ? new MySqlParameter("@IsQueryUsers", MySqlDbType.Int32, 11) { Value = DBNull.Value } : new MySqlParameter("@IsQueryUsers", MySqlDbType.Int32, 11) { Value = model.IsQueryUsers };
            parameterArray1[13] = (model.Value == null) ? new MySqlParameter("@Value", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@Value", MySqlDbType.VarChar, 50) { Value = model.Value };
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.ProgramBuilderQuerys> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.ProgramBuilderQuerys> list = new List<YJ.Data.Model.ProgramBuilderQuerys>();
            YJ.Data.Model.ProgramBuilderQuerys item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.ProgramBuilderQuerys {
                    ID = dataReader.GetString(0).ToGuid(),
                    ProgramID = dataReader.GetString(1).ToGuid(),
                    Field = dataReader.GetString(2),
                    ShowTitle = dataReader.GetString(3),
                    Operators = dataReader.GetString(4),
                    ControlName = dataReader.GetString(5),
                    InputType = dataReader.GetInt32(6)
                };
                if (!dataReader.IsDBNull(7))
                {
                    item.Width = dataReader.GetString(7);
                }
                item.Sort = dataReader.GetInt32(8);
                if (!dataReader.IsDBNull(9))
                {
                    item.DataSource = new int?(dataReader.GetInt32(9));
                }
                if (!dataReader.IsDBNull(10))
                {
                    item.DataSourceString = dataReader.GetString(10);
                }
                if (!dataReader.IsDBNull(11))
                {
                    item.DataLinkID = dataReader.GetString(11);
                }
                if (!dataReader.IsDBNull(12))
                {
                    item.IsQueryUsers = new int?(dataReader.GetInt32(12));
                }
                if (!dataReader.IsDBNull(13))
                {
                    item.Value = dataReader.GetString(13);
                }
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM programbuilderquerys WHERE ID=@ID";
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
            string sql = "DELETE FROM ProgramBuilderQuerys WHERE ProgramID=@ProgramID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ProgramID", MySqlDbType.VarChar) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.ProgramBuilderQuerys Get(Guid id)
        {
            string sql = "SELECT * FROM programbuilderquerys WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilderQuerys> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.ProgramBuilderQuerys> GetAll()
        {
            string sql = "SELECT * FROM programbuilderquerys";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.ProgramBuilderQuerys> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.ProgramBuilderQuerys> GetAll(Guid programID)
        {
            string sql = "SELECT * FROM ProgramBuilderQuerys WHERE ProgramID=@ProgramID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ProgramID", MySqlDbType.VarChar) {
                Value = programID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilderQuerys> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM programbuilderquerys";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.ProgramBuilderQuerys model)
        {
            string sql = "UPDATE programbuilderquerys SET \r\n\t\t\t\tProgramID=@ProgramID,Field=@Field,ShowTitle=@ShowTitle,Operators=@Operators,ControlName=@ControlName,InputType=@InputType,Width=@Width,Sort=@Sort,DataSource=@DataSource,DataSourceString=@DataSourceString,DataLinkID=@DataLinkID,IsQueryUsers=@IsQueryUsers,Value=@Value\r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[14];
            MySqlParameter parameter1 = new MySqlParameter("@ProgramID", MySqlDbType.VarChar, 0x24) {
                Value = model.ProgramID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Field", MySqlDbType.Text, -1) {
                Value = model.Field
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@ShowTitle", MySqlDbType.Text, -1) {
                Value = model.ShowTitle
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@Operators", MySqlDbType.VarChar, 50) {
                Value = model.Operators
            };
            parameterArray1[3] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@ControlName", MySqlDbType.VarChar, 50) {
                Value = model.ControlName
            };
            parameterArray1[4] = parameter5;
            MySqlParameter parameter6 = new MySqlParameter("@InputType", MySqlDbType.Int32, 11) {
                Value = model.InputType
            };
            parameterArray1[5] = parameter6;
            parameterArray1[6] = (model.Width == null) ? new MySqlParameter("@Width", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@Width", MySqlDbType.VarChar, 50) { Value = model.Width };
            MySqlParameter parameter9 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[7] = parameter9;
            parameterArray1[8] = !model.DataSource.HasValue ? new MySqlParameter("@DataSource", MySqlDbType.Int32, 11) { Value = DBNull.Value } : new MySqlParameter("@DataSource", MySqlDbType.Int32, 11) { Value = model.DataSource };
            parameterArray1[9] = (model.DataSourceString == null) ? new MySqlParameter("@DataSourceString", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@DataSourceString", MySqlDbType.LongText, -1) { Value = model.DataSourceString };
            parameterArray1[10] = (model.DataLinkID == null) ? new MySqlParameter("@DataLinkID", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@DataLinkID", MySqlDbType.VarChar, 50) { Value = model.DataLinkID };
            parameterArray1[11] = !model.IsQueryUsers.HasValue ? new MySqlParameter("@IsQueryUsers", MySqlDbType.Int32, 11) { Value = DBNull.Value } : new MySqlParameter("@IsQueryUsers", MySqlDbType.Int32, 11) { Value = model.IsQueryUsers };
            parameterArray1[12] = (model.Value == null) ? new MySqlParameter("@Value", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@Value", MySqlDbType.VarChar, 50) { Value = model.Value };
            MySqlParameter parameter20 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[13] = parameter20;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

