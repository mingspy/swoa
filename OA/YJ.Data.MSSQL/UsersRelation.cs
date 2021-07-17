namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class UsersRelation : IUsersRelation
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.UsersRelation model)
        {
            string sql = "INSERT INTO UsersRelation\r\n\t\t\t\t(UserID,OrganizeID,IsMain,Sort) \r\n\t\t\t\tVALUES(@UserID,@OrganizeID,@IsMain,@Sort)";
            SqlParameter[] parameterArray1 = new SqlParameter[4];
            SqlParameter parameter1 = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.UserID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@OrganizeID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.OrganizeID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@IsMain", SqlDbType.Int, -1) {
                Value = model.IsMain
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[3] = parameter4;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.UsersRelation> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.UsersRelation> list = new List<YJ.Data.Model.UsersRelation>();
            YJ.Data.Model.UsersRelation item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.UsersRelation {
                    UserID = dataReader.GetGuid(0),
                    OrganizeID = dataReader.GetGuid(1),
                    IsMain = dataReader.GetInt32(2),
                    Sort = dataReader.GetInt32(3)
                };
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid userid, Guid organizeid)
        {
            string sql = "DELETE FROM UsersRelation WHERE UserID=@UserID AND OrganizeID=@OrganizeID";
            SqlParameter[] parameterArray1 = new SqlParameter[2];
            SqlParameter parameter1 = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier) {
                Value = userid
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@OrganizeID", SqlDbType.UniqueIdentifier) {
                Value = organizeid
            };
            parameterArray1[1] = parameter2;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int DeleteByOrganizeID(Guid organizeID)
        {
            string sql = "DELETE FROM UsersRelation WHERE OrganizeID=@OrganizeID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@OrganizeID", SqlDbType.UniqueIdentifier) {
                Value = organizeID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int DeleteByUserID(Guid userID)
        {
            string sql = "DELETE FROM UsersRelation WHERE UserID=@UserID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier) {
                Value = userID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int DeleteNotIsMainByUserID(Guid userID)
        {
            string sql = "DELETE FROM UsersRelation WHERE IsMain=0 AND UserID=@UserID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier) {
                Value = userID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.UsersRelation Get(Guid userid, Guid organizeid)
        {
            string sql = "SELECT * FROM UsersRelation WHERE UserID=@UserID AND OrganizeID=@OrganizeID";
            SqlParameter[] parameterArray1 = new SqlParameter[2];
            SqlParameter parameter1 = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier) {
                Value = userid
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@OrganizeID", SqlDbType.UniqueIdentifier) {
                Value = organizeid
            };
            parameterArray1[1] = parameter2;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.UsersRelation> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.UsersRelation> GetAll()
        {
            string sql = "SELECT * FROM UsersRelation";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.UsersRelation> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.UsersRelation> GetAllByOrganizeID(Guid organizeID)
        {
            string sql = "SELECT * FROM UsersRelation WHERE OrganizeID=@OrganizeID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@OrganizeID", SqlDbType.UniqueIdentifier) {
                Value = organizeID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.UsersRelation> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.UsersRelation> GetAllByUserID(Guid userID)
        {
            string sql = "SELECT * FROM UsersRelation WHERE UserID=@UserID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier) {
                Value = userID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.UsersRelation> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM UsersRelation";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public YJ.Data.Model.UsersRelation GetMainByUserID(Guid userID)
        {
            string sql = "SELECT * FROM UsersRelation WHERE UserID=@UserID AND IsMain=1";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier) {
                Value = userID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.UsersRelation> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public int GetMaxSort(Guid organizeID)
        {
            string sql = "SELECT ISNULL(MAX(Sort),0)+1 FROM UsersRelation WHERE OrganizeID=@OrganizeID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@OrganizeID", SqlDbType.UniqueIdentifier) {
                Value = organizeID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            DBHelper helper = new DBHelper();
            return helper.GetFieldValue(sql, parameter).ToInt();
        }

        public int Update(YJ.Data.Model.UsersRelation model)
        {
            string sql = "UPDATE UsersRelation SET \r\n\t\t\t\tIsMain=@IsMain,Sort=@Sort\r\n\t\t\t\tWHERE UserID=@UserID and OrganizeID=@OrganizeID";
            SqlParameter[] parameterArray1 = new SqlParameter[4];
            SqlParameter parameter1 = new SqlParameter("@IsMain", SqlDbType.Int, -1) {
                Value = model.IsMain
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.UserID
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@OrganizeID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.OrganizeID
            };
            parameterArray1[3] = parameter4;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

