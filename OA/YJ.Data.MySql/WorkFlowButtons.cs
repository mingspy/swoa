using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using YJ.Data.Interface;
using YJ.Data.Model;
namespace YJ.Data.MySql
{


    public class WorkFlowButtons : IWorkFlowButtons
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkFlowButtons model)
        {
            string sql = "INSERT INTO workflowbuttons\r\n\t\t\t\t(ID,Title,Ico,Script,Note,Sort) \r\n\t\t\t\tVALUES(@ID,@Title,@Ico,@Script,@Note,@Sort)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[6];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Title", MySqlDbType.Text, -1) {
                Value = model.Title
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = (model.Ico == null) ? new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = model.Ico };
            parameterArray1[3] = (model.Script == null) ? new MySqlParameter("@Script", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Script", MySqlDbType.LongText, -1) { Value = model.Script };
            parameterArray1[4] = (model.Note == null) ? new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = model.Note };
            MySqlParameter parameter9 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[5] = parameter9;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.WorkFlowButtons> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.WorkFlowButtons> list = new List<YJ.Data.Model.WorkFlowButtons>();
            YJ.Data.Model.WorkFlowButtons item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.WorkFlowButtons {
                    ID = dataReader.GetString(0).ToGuid(),
                    Title = dataReader.GetString(1)
                };
                if (!dataReader.IsDBNull(2))
                {
                    item.Ico = dataReader.GetString(2);
                }
                if (!dataReader.IsDBNull(3))
                {
                    item.Script = dataReader.GetString(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    item.Note = dataReader.GetString(4);
                }
                item.Sort = dataReader.GetInt32(5);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM workflowbuttons WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.WorkFlowButtons Get(Guid id)
        {
            string sql = "SELECT * FROM workflowbuttons WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowButtons> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkFlowButtons> GetAll()
        {
            string sql = "SELECT * FROM workflowbuttons";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowButtons> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM workflowbuttons";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int GetMaxSort()
        {
            string sql = "SELECT IFNULL(MAX(Sort),0)+1 FROM WorkFlowButtons";
            string fieldValue = this.dbHelper.GetFieldValue(sql);
            return (fieldValue.IsInt() ? fieldValue.ToInt() : 1);
        }

        public int Update(YJ.Data.Model.WorkFlowButtons model)
        {
            string sql = "UPDATE workflowbuttons SET \r\n\t\t\t\tTitle=@Title,Ico=@Ico,Script=@Script,Note=@Note,Sort=@Sort\r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[6];
            MySqlParameter parameter1 = new MySqlParameter("@Title", MySqlDbType.Text, -1) {
                Value = model.Title
            };
            parameterArray1[0] = parameter1;
            parameterArray1[1] = (model.Ico == null) ? new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@Ico", MySqlDbType.Text, -1) { Value = model.Ico };
            parameterArray1[2] = (model.Script == null) ? new MySqlParameter("@Script", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Script", MySqlDbType.LongText, -1) { Value = model.Script };
            parameterArray1[3] = (model.Note == null) ? new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = model.Note };
            MySqlParameter parameter8 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[4] = parameter8;
            MySqlParameter parameter9 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[5] = parameter9;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

