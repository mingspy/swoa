using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using YJ.Data.Interface;
using YJ.Data.Model;
namespace YJ.Data.MySql
{


    public class WorkFlowComment : IWorkFlowComment
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkFlowComment model)
        {
            string sql = "INSERT INTO workflowcomment\r\n\t\t\t\t(ID,MemberID,Comment,Type,Sort) \r\n\t\t\t\tVALUES(@ID,@MemberID,@Comment,@Type,@Sort)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[5];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@MemberID", MySqlDbType.LongText, -1) {
                Value = model.MemberID
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Comment", MySqlDbType.Text, -1) {
                Value = model.Comment
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@Type", MySqlDbType.Int32, 11) {
                Value = model.Type
            };
            parameterArray1[3] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[4] = parameter5;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.WorkFlowComment> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.WorkFlowComment> list = new List<YJ.Data.Model.WorkFlowComment>();
            YJ.Data.Model.WorkFlowComment item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.WorkFlowComment {
                    ID = dataReader.GetString(0).ToGuid(),
                    MemberID = dataReader.GetString(1),
                    Comment = dataReader.GetString(2),
                    Type = dataReader.GetInt32(3),
                    Sort = dataReader.GetInt32(4)
                };
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM workflowcomment WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.WorkFlowComment Get(Guid id)
        {
            string sql = "SELECT * FROM workflowcomment WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowComment> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkFlowComment> GetAll()
        {
            string sql = "SELECT * FROM workflowcomment";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowComment> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM workflowcomment";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public List<YJ.Data.Model.WorkFlowComment> GetManagerAll()
        {
            string sql = "SELECT * FROM WorkFlowComment WHERE Type=0";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowComment> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public int GetManagerMaxSort()
        {
            string sql = "SELECT IFNULL(MAX(Sort)+1,1) FROM WorkFlowComment WHERE Type=0";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            if (dataReader.HasRows)
            {
                dataReader.Read();
                int num = dataReader.GetInt32(0);
                dataReader.Close();
                return num;
            }
            return 1;
        }

        public int GetUserMaxSort(Guid userID)
        {
            string sql = "SELECT IFNULL(MAX(Sort)+1,1) FROM WorkFlowComment WHERE MemberID=@MemberID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@MemberID", MySqlDbType.VarChar) {
                Value = userID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            if (dataReader.HasRows)
            {
                dataReader.Read();
                int num = dataReader.GetInt32(0);
                dataReader.Close();
                return num;
            }
            return 1;
        }

        public int Update(YJ.Data.Model.WorkFlowComment model)
        {
            string sql = "UPDATE workflowcomment SET \r\n\t\t\t\tMemberID=@MemberID,Comment=@Comment,Type=@Type,Sort=@Sort\r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[5];
            MySqlParameter parameter1 = new MySqlParameter("@MemberID", MySqlDbType.LongText, -1) {
                Value = model.MemberID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Comment", MySqlDbType.Text, -1) {
                Value = model.Comment
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Type", MySqlDbType.Int32, 11) {
                Value = model.Type
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[3] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[4] = parameter5;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

