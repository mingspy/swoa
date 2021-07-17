namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class HastenLog : IHastenLog
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.HastenLog model)
        {
            string sql = "INSERT INTO HastenLog\r\n\t\t\t\t(ID,OthersParams,Users,Types,Contents,SendUser,SendUserName,SendTime) \r\n\t\t\t\tVALUES(@ID,@OthersParams,@Users,@Types,@Contents,@SendUser,@SendUserName,@SendTime)";
            SqlParameter[] parameterArray1 = new SqlParameter[8];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@OthersParams", SqlDbType.VarChar, 0x1388) {
                Value = model.OthersParams
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Users", SqlDbType.VarChar, 0x1388) {
                Value = model.Users
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@Types", SqlDbType.VarChar, 500) {
                Value = model.Types
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@Contents", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Contents
            };
            parameterArray1[4] = parameter5;
            SqlParameter parameter6 = new SqlParameter("@SendUser", SqlDbType.UniqueIdentifier, -1) {
                Value = model.SendUser
            };
            parameterArray1[5] = parameter6;
            SqlParameter parameter7 = new SqlParameter("@SendUserName", SqlDbType.NVarChar, 100) {
                Value = model.SendUserName
            };
            parameterArray1[6] = parameter7;
            SqlParameter parameter8 = new SqlParameter("@SendTime", SqlDbType.DateTime, 8) {
                Value = model.SendTime
            };
            parameterArray1[7] = parameter8;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.HastenLog> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.HastenLog> list = new List<YJ.Data.Model.HastenLog>();
            YJ.Data.Model.HastenLog item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.HastenLog {
                    ID = dataReader.GetGuid(0),
                    OthersParams = dataReader.GetString(1),
                    Users = dataReader.GetString(2),
                    Types = dataReader.GetString(3),
                    Contents = dataReader.GetString(4),
                    SendUser = dataReader.GetGuid(5),
                    SendUserName = dataReader.GetString(6),
                    SendTime = dataReader.GetDateTime(7)
                };
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM HastenLog WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.HastenLog Get(Guid id)
        {
            string sql = "SELECT * FROM HastenLog WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.HastenLog> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.HastenLog> GetAll()
        {
            string sql = "SELECT * FROM HastenLog";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.HastenLog> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM HastenLog";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.HastenLog model)
        {
            string sql = "UPDATE HastenLog SET \r\n\t\t\t\tOthersParams=@OthersParams,Users=@Users,Types=@Types,Contents=@Contents,SendUser=@SendUser,SendUserName=@SendUserName,SendTime=@SendTime\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[8];
            SqlParameter parameter1 = new SqlParameter("@OthersParams", SqlDbType.VarChar, 0x1388) {
                Value = model.OthersParams
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Users", SqlDbType.VarChar, 0x1388) {
                Value = model.Users
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Types", SqlDbType.VarChar, 500) {
                Value = model.Types
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@Contents", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Contents
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@SendUser", SqlDbType.UniqueIdentifier, -1) {
                Value = model.SendUser
            };
            parameterArray1[4] = parameter5;
            SqlParameter parameter6 = new SqlParameter("@SendUserName", SqlDbType.NVarChar, 100) {
                Value = model.SendUserName
            };
            parameterArray1[5] = parameter6;
            SqlParameter parameter7 = new SqlParameter("@SendTime", SqlDbType.DateTime, 8) {
                Value = model.SendTime
            };
            parameterArray1[6] = parameter7;
            SqlParameter parameter8 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[7] = parameter8;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

