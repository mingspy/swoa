namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class WorkFlowComment : IWorkFlowComment
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkFlowComment model)
        {
            string sql = "INSERT INTO WorkFlowComment\r\n\t\t\t\t(ID,MemberID,Comment,Type,Sort) \r\n\t\t\t\tVALUES(@ID,@MemberID,@Comment,@Type,@Sort)";
            SqlParameter[] parameterArray1 = new SqlParameter[5];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@MemberID", SqlDbType.VarChar, -1) {
                Value = model.MemberID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Comment", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Comment
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@Type", SqlDbType.Int, -1) {
                Value = model.Type
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[4] = parameter5;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.WorkFlowComment> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.WorkFlowComment> list = new List<YJ.Data.Model.WorkFlowComment>();
            YJ.Data.Model.WorkFlowComment item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.WorkFlowComment {
                    ID = dataReader.GetGuid(0)
                };
                if (!dataReader.IsDBNull(1))
                {
                    item.MemberID = dataReader.GetString(1);
                }
                item.Comment = dataReader.GetString(2);
                item.Type = dataReader.GetInt32(3);
                item.Sort = dataReader.GetInt32(4);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM WorkFlowComment WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.WorkFlowComment Get(Guid id)
        {
            string sql = "SELECT * FROM WorkFlowComment WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowComment> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkFlowComment> GetAll()
        {
            string sql = "SELECT * FROM WorkFlowComment";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowComment> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM WorkFlowComment";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public List<YJ.Data.Model.WorkFlowComment> GetManagerAll()
        {
            string sql = "SELECT * FROM WorkFlowComment WHERE Type=0";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowComment> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public int GetManagerMaxSort()
        {
            string sql = "SELECT ISNULL(MAX(Sort)+1,1) FROM WorkFlowComment WHERE Type=0";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
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
            string sql = "SELECT ISNULL(MAX(Sort)+1,1) FROM WorkFlowComment WHERE MemberID=@MemberID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@MemberID", SqlDbType.UniqueIdentifier) {
                Value = userID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
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
            string sql = "UPDATE WorkFlowComment SET \r\n\t\t\t\tMemberID=@MemberID,Comment=@Comment,Type=@Type,Sort=@Sort\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[5];
            SqlParameter parameter1 = new SqlParameter("@MemberID", SqlDbType.VarChar, -1) {
                Value = model.MemberID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Comment", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Comment
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Type", SqlDbType.Int, -1) {
                Value = model.Type
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[4] = parameter5;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

