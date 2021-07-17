namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class WorkFlowComment : IWorkFlowComment
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkFlowComment model)
        {
            string sql = "INSERT INTO WorkFlowComment\r\n\t\t\t\t(ID,MemberID,Comment1,Type,Sort) \r\n\t\t\t\tVALUES(:ID,:MemberID,:Comment1,:Type,:Sort)";
            OracleParameter[] parameterArray1 = new OracleParameter[5];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            parameterArray1[1] = (model.MemberID == null) ? new OracleParameter(":MemberID", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":MemberID", OracleDbType.Clob) { Value = model.MemberID };
            OracleParameter parameter4 = new OracleParameter(":Comment1", OracleDbType.NVarchar2) {
                Value = model.Comment
            };
            parameterArray1[2] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":Type", OracleDbType.Int32) {
                Value = model.Type
            };
            parameterArray1[3] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[4] = parameter6;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.WorkFlowComment> DataReaderToList(OracleDataReader dataReader)
        {
            List<YJ.Data.Model.WorkFlowComment> list = new List<YJ.Data.Model.WorkFlowComment>();
            YJ.Data.Model.WorkFlowComment item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.WorkFlowComment {
                    ID = dataReader.GetString(0).ToGuid()
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
            string sql = "DELETE FROM WorkFlowComment WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.WorkFlowComment Get(Guid id)
        {
            string sql = "SELECT * FROM WorkFlowComment WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowComment> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkFlowComment> GetAll()
        {
            string sql = "SELECT * FROM WorkFlowComment";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
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
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowComment> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public int GetManagerMaxSort()
        {
            string sql = "SELECT nvl(MAX(Sort)+1,1) FROM WorkFlowComment WHERE Type=0";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
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
            string sql = "SELECT nvl(MAX(Sort)+1,1) FROM WorkFlowComment WHERE MemberID=:MemberID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":MemberID", OracleDbType.Varchar2) {
                Value = userID
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
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
            string sql = "UPDATE WorkFlowComment SET \r\n\t\t\t\tMemberID=:MemberID,Comment1=:Comment1,Type=:Type,Sort=:Sort\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[5];
            parameterArray1[0] = (model.MemberID == null) ? new OracleParameter(":MemberID", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":MemberID", OracleDbType.Clob) { Value = model.MemberID };
            OracleParameter parameter3 = new OracleParameter(":Comment1", OracleDbType.NVarchar2) {
                Value = model.Comment
            };
            parameterArray1[1] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":Type", OracleDbType.Int32) {
                Value = model.Type
            };
            parameterArray1[2] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[3] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[4] = parameter6;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

