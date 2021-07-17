namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class WorkFlowButtons : IWorkFlowButtons
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkFlowButtons model)
        {
            string sql = "INSERT INTO WorkFlowButtons\r\n\t\t\t\t(ID,Title,Ico,Script,Note,Sort) \r\n\t\t\t\tVALUES(@ID,@Title,@Ico,@Script,@Note,@Sort)";
            SqlParameter[] parameterArray1 = new SqlParameter[6];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Title", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Title
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = (model.Ico == null) ? new SqlParameter("@Ico", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@Ico", SqlDbType.VarChar, 500) { Value = model.Ico };
            parameterArray1[3] = (model.Script == null) ? new SqlParameter("@Script", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Script", SqlDbType.VarChar, -1) { Value = model.Script };
            parameterArray1[4] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.VarChar, -1) { Value = model.Note };
            SqlParameter parameter9 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[5] = parameter9;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.WorkFlowButtons> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.WorkFlowButtons> list = new List<YJ.Data.Model.WorkFlowButtons>();
            YJ.Data.Model.WorkFlowButtons item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.WorkFlowButtons {
                    ID = dataReader.GetGuid(0),
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
            string sql = "DELETE FROM WorkFlowButtons WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.WorkFlowButtons Get(Guid id)
        {
            string sql = "SELECT * FROM WorkFlowButtons WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowButtons> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkFlowButtons> GetAll()
        {
            string sql = "SELECT * FROM WorkFlowButtons";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowButtons> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM WorkFlowButtons";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int GetMaxSort()
        {
            string sql = "SELECT ISNULL(MAX(Sort),0)+1 FROM WorkFlowButtons";
            string fieldValue = this.dbHelper.GetFieldValue(sql);
            return (fieldValue.IsInt() ? fieldValue.ToInt() : 1);
        }

        public int Update(YJ.Data.Model.WorkFlowButtons model)
        {
            string sql = "UPDATE WorkFlowButtons SET \r\n\t\t\t\tTitle=@Title,Ico=@Ico,Script=@Script,Note=@Note,Sort=@Sort\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[6];
            SqlParameter parameter1 = new SqlParameter("@Title", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Title
            };
            parameterArray1[0] = parameter1;
            parameterArray1[1] = (model.Ico == null) ? new SqlParameter("@Ico", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@Ico", SqlDbType.VarChar, 500) { Value = model.Ico };
            parameterArray1[2] = (model.Script == null) ? new SqlParameter("@Script", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Script", SqlDbType.VarChar, -1) { Value = model.Script };
            parameterArray1[3] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.VarChar, -1) { Value = model.Note };
            SqlParameter parameter8 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[4] = parameter8;
            SqlParameter parameter9 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[5] = parameter9;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

