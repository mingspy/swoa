namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class WorkFlowData : IWorkFlowData
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkFlowData model)
        {
            string sql = "INSERT INTO WorkFlowData\r\n\t\t\t\t(ID,InstanceID,LinkID,TableName,FieldName,Value) \r\n\t\t\t\tVALUES(@ID,@InstanceID,@LinkID,@TableName,@FieldName,@Value)";
            SqlParameter[] parameterArray1 = new SqlParameter[6];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@InstanceID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.InstanceID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@LinkID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.LinkID
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@TableName", SqlDbType.VarChar, 500) {
                Value = model.TableName
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@FieldName", SqlDbType.VarChar, 500) {
                Value = model.FieldName
            };
            parameterArray1[4] = parameter5;
            SqlParameter parameter6 = new SqlParameter("@Value", SqlDbType.VarChar, 0x1f40) {
                Value = model.Value
            };
            parameterArray1[5] = parameter6;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.WorkFlowData> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.WorkFlowData> list = new List<YJ.Data.Model.WorkFlowData>();
            YJ.Data.Model.WorkFlowData item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.WorkFlowData {
                    ID = dataReader.GetGuid(0),
                    InstanceID = dataReader.GetGuid(1),
                    LinkID = dataReader.GetGuid(2),
                    TableName = dataReader.GetString(3),
                    FieldName = dataReader.GetString(4),
                    Value = dataReader.GetString(5)
                };
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM WorkFlowData WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.WorkFlowData Get(Guid id)
        {
            string sql = "SELECT * FROM WorkFlowData WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowData> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkFlowData> GetAll()
        {
            string sql = "SELECT * FROM WorkFlowData";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkFlowData> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkFlowData> GetAll(Guid instanceID)
        {
            string sql = "SELECT * FROM WorkFlowData WHERE InstanceID=@InstanceID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@InstanceID", SqlDbType.UniqueIdentifier) {
                Value = instanceID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkFlowData> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM WorkFlowData";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.WorkFlowData model)
        {
            string sql = "UPDATE WorkFlowData SET \r\n\t\t\t\tInstanceID=@InstanceID,LinkID=@LinkID,TableName=@TableName,FieldName=@FieldName,Value=@Value\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[6];
            SqlParameter parameter1 = new SqlParameter("@InstanceID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.InstanceID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@LinkID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.LinkID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@TableName", SqlDbType.VarChar, 500) {
                Value = model.TableName
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@FieldName", SqlDbType.VarChar, 500) {
                Value = model.FieldName
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@Value", SqlDbType.VarChar, 0x1f40) {
                Value = model.Value
            };
            parameterArray1[4] = parameter5;
            SqlParameter parameter6 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[5] = parameter6;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

