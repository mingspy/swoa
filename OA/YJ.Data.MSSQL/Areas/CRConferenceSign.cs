namespace YJ.Data.MSSQL.Areas
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using YJ.Data.Interface.Areas;
    using YJ.Data.Model.Areas;
    using YJ.Data.MSSQL;

    public class CRConferenceSign : ICRConferenceSign
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.Areas.CRConferenceSign model)
        {
            int num;
            string sql = "INSERT INTO CRConferenceSign\r\n\t\t\t\t(CRMeetingID,UserID,UserName,SignDate,CreationTime,Note) \r\n\t\t\t\tVALUES(@CRMeetingID,@UserID,@UserName,@SignDate,@CreationTime,@Note);\r\n\t\t\t\tSELECT SCOPE_IDENTITY();";
            SqlParameter[] parameterArray1 = new SqlParameter[6];
            SqlParameter parameter1 = new SqlParameter("@CRMeetingID", SqlDbType.VarChar, 50) {
                Value = model.CRMeetingID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@UserID", SqlDbType.VarChar, 50) {
                Value = model.UserID
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = (model.UserName == null) ? new SqlParameter("@UserName", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@UserName", SqlDbType.NVarChar, 100) { Value = model.UserName };
            parameterArray1[3] = !model.SignDate.HasValue ? new SqlParameter("@SignDate", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@SignDate", SqlDbType.DateTime, 8) { Value = model.SignDate };
            parameterArray1[4] = !model.CreationTime.HasValue ? new SqlParameter("@CreationTime", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@CreationTime", SqlDbType.DateTime, 8) { Value = model.CreationTime };
            parameterArray1[5] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.NVarChar, 100) { Value = model.Note };
            SqlParameter[] parameter = parameterArray1;
            return (int.TryParse(this.dbHelper.ExecuteScalar(sql, parameter), out num) ? num : -1);
        }

        private List<YJ.Data.Model.Areas.CRConferenceSign> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.Areas.CRConferenceSign> list = new List<YJ.Data.Model.Areas.CRConferenceSign>();
            YJ.Data.Model.Areas.CRConferenceSign item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.Areas.CRConferenceSign {
                    ID = dataReader.GetInt32(0),
                    CRMeetingID = dataReader.GetString(1),
                    UserID = dataReader.GetString(2)
                };
                if (!dataReader.IsDBNull(3))
                {
                    item.UserName = dataReader.GetString(3);
                }
                if (!dataReader.IsDBNull(4))
                {
                    item.SignDate = new DateTime?(dataReader.GetDateTime(4));
                }
                if (!dataReader.IsDBNull(5))
                {
                    item.CreationTime = new DateTime?(dataReader.GetDateTime(5));
                }
                if (!dataReader.IsDBNull(6))
                {
                    item.Note = dataReader.GetString(6);
                }
                list.Add(item);
            }
            return list;
        }

        public int Delete(int id)
        {
            string sql = "DELETE FROM CRConferenceSign WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.Int) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.Areas.CRConferenceSign Get(int id)
        {
            string sql = "SELECT * FROM CRConferenceSign WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.Int) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Areas.CRConferenceSign> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.Areas.CRConferenceSign> GetAll()
        {
            string sql = "SELECT * FROM CRConferenceSign";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Areas.CRConferenceSign> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM CRConferenceSign";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.Areas.CRConferenceSign model)
        {
            string sql = "UPDATE CRConferenceSign SET \r\n\t\t\t\tCRMeetingID=@CRMeetingID,UserID=@UserID,UserName=@UserName,SignDate=@SignDate,CreationTime=@CreationTime,Note=@Note\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[7];
            SqlParameter parameter1 = new SqlParameter("@CRMeetingID", SqlDbType.VarChar, 50) {
                Value = model.CRMeetingID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@UserID", SqlDbType.VarChar, 50) {
                Value = model.UserID
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = (model.UserName == null) ? new SqlParameter("@UserName", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@UserName", SqlDbType.NVarChar, 100) { Value = model.UserName };
            parameterArray1[3] = !model.SignDate.HasValue ? new SqlParameter("@SignDate", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@SignDate", SqlDbType.DateTime, 8) { Value = model.SignDate };
            parameterArray1[4] = !model.CreationTime.HasValue ? new SqlParameter("@CreationTime", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@CreationTime", SqlDbType.DateTime, 8) { Value = model.CreationTime };
            parameterArray1[5] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.NVarChar, 100) { Value = model.Note };
            SqlParameter parameter11 = new SqlParameter("@ID", SqlDbType.Int, -1) {
                Value = model.ID
            };
            parameterArray1[6] = parameter11;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

