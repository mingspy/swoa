using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using YJ.Data.Interface;
using YJ.Data.Model;
namespace YJ.Data.MySql
{
 

    public class ProgramBuilderButtons : IProgramBuilderButtons
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.ProgramBuilderButtons model)
        {
            string sql = "INSERT INTO programbuilderbuttons\r\n\t\t\t\t(ID,ProgramID,ButtonID,ButtonName,ClientScript,Ico,ShowType,Sort,IsValidateShow) \r\n\t\t\t\tVALUES(@ID,@ProgramID,@ButtonID,@ButtonName,@ClientScript,@Ico,@ShowType,@Sort,@IsValidateShow)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[9];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@ProgramID", MySqlDbType.VarChar, 0x24) {
                Value = model.ProgramID
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = !model.ButtonID.HasValue ? new MySqlParameter("@ButtonID", MySqlDbType.VarChar, 0x24) { Value = DBNull.Value } : new MySqlParameter("@ButtonID", MySqlDbType.VarChar, 0x24) { Value = model.ButtonID };
            MySqlParameter parameter5 = new MySqlParameter("@ButtonName", MySqlDbType.VarChar, 200) {
                Value = model.ButtonName
            };
            parameterArray1[3] = parameter5;
            parameterArray1[4] = (model.ClientScript == null) ? new MySqlParameter("@ClientScript", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@ClientScript", MySqlDbType.LongText, -1) { Value = model.ClientScript };
            parameterArray1[5] = (model.Ico == null) ? new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = model.Ico };
            MySqlParameter parameter10 = new MySqlParameter("@ShowType", MySqlDbType.Int32, 11) {
                Value = model.ShowType
            };
            parameterArray1[6] = parameter10;
            MySqlParameter parameter11 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[7] = parameter11;
            MySqlParameter parameter12 = new MySqlParameter("@IsValidateShow", MySqlDbType.Int32, 11) {
                Value = model.IsValidateShow
            };
            parameterArray1[8] = parameter12;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.ProgramBuilderButtons> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.ProgramBuilderButtons> list = new List<YJ.Data.Model.ProgramBuilderButtons>();
            YJ.Data.Model.ProgramBuilderButtons item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.ProgramBuilderButtons {
                    ID = dataReader.GetString(0).ToGuid(),
                    ProgramID = dataReader.GetString(1).ToGuid()
                };
                if (!dataReader.IsDBNull(2))
                {
                    item.ButtonID = new Guid?(dataReader.GetString(2).ToGuid());
                }
                item.ButtonName = dataReader.GetString(3);
                if (!dataReader.IsDBNull(4))
                {
                    item.ClientScript = dataReader.GetString(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    item.Ico = dataReader.GetString(5);
                }
                item.ShowType = dataReader.GetInt32(6);
                item.Sort = dataReader.GetInt32(7);
                item.IsValidateShow = dataReader.GetInt32(8);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM programbuilderbuttons WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.ProgramBuilderButtons Get(Guid id)
        {
            string sql = "SELECT * FROM programbuilderbuttons WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilderButtons> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.ProgramBuilderButtons> GetAll()
        {
            string sql = "SELECT * FROM programbuilderbuttons";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.ProgramBuilderButtons> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.ProgramBuilderButtons> GetAll(Guid id)
        {
            string sql = "SELECT * FROM ProgramBuilderButtons WHERE ProgramID=@ProgramID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ProgramID", MySqlDbType.VarChar) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilderButtons> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM programbuilderbuttons";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.ProgramBuilderButtons model)
        {
            string sql = "UPDATE programbuilderbuttons SET \r\n\t\t\t\tProgramID=@ProgramID,ButtonID=@ButtonID,ButtonName=@ButtonName,ClientScript=@ClientScript,Ico=@Ico,ShowType=@ShowType,Sort=@Sort,IsValidateShow=@IsValidateShow\r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[9];
            MySqlParameter parameter1 = new MySqlParameter("@ProgramID", MySqlDbType.VarChar, 0x24) {
                Value = model.ProgramID
            };
            parameterArray1[0] = parameter1;
            parameterArray1[1] = !model.ButtonID.HasValue ? new MySqlParameter("@ButtonID", MySqlDbType.VarChar, 0x24) { Value = DBNull.Value } : new MySqlParameter("@ButtonID", MySqlDbType.VarChar, 0x24) { Value = model.ButtonID };
            MySqlParameter parameter4 = new MySqlParameter("@ButtonName", MySqlDbType.VarChar, 200) {
                Value = model.ButtonName
            };
            parameterArray1[2] = parameter4;
            parameterArray1[3] = (model.ClientScript == null) ? new MySqlParameter("@ClientScript", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@ClientScript", MySqlDbType.LongText, -1) { Value = model.ClientScript };
            parameterArray1[4] = (model.Ico == null) ? new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = model.Ico };
            MySqlParameter parameter9 = new MySqlParameter("@ShowType", MySqlDbType.Int32, 11) {
                Value = model.ShowType
            };
            parameterArray1[5] = parameter9;
            MySqlParameter parameter10 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[6] = parameter10;
            MySqlParameter parameter11 = new MySqlParameter("@IsValidateShow", MySqlDbType.Int32, 11) {
                Value = model.IsValidateShow
            };
            parameterArray1[7] = parameter11;
            MySqlParameter parameter12 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[8] = parameter12;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

