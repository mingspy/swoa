using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using YJ.Data.Interface;
using YJ.Data.Model;
namespace YJ.Data.MySql
{


    public class UsersRelation : IUsersRelation
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.UsersRelation model)
        {
            string sql = "INSERT INTO usersrelation\r\n\t\t\t\t(UserID,OrganizeID,IsMain,Sort) \r\n\t\t\t\tVALUES(@UserID,@OrganizeID,@IsMain,@Sort)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[4];
            MySqlParameter parameter1 = new MySqlParameter("@UserID", MySqlDbType.VarChar, 0x24) {
                Value = model.UserID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@OrganizeID", MySqlDbType.VarChar, 0x24) {
                Value = model.OrganizeID
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@IsMain", MySqlDbType.Int32, 11) {
                Value = model.IsMain
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[3] = parameter4;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.UsersRelation> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.UsersRelation> list = new List<YJ.Data.Model.UsersRelation>();
            YJ.Data.Model.UsersRelation item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.UsersRelation {
                    UserID = dataReader.GetString(0).ToGuid(),
                    OrganizeID = dataReader.GetString(1).ToGuid(),
                    IsMain = dataReader.GetInt32(2),
                    Sort = dataReader.GetInt32(3)
                };
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid userid, Guid organizeid)
        {
            string sql = "DELETE FROM usersrelation WHERE UserID=@UserID AND OrganizeID=@OrganizeID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[2];
            MySqlParameter parameter1 = new MySqlParameter("@UserID", MySqlDbType.VarChar, 0x24) {
                Value = userid.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@OrganizeID", MySqlDbType.VarChar, 0x24) {
                Value = organizeid.ToString()
            };
            parameterArray1[1] = parameter2;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int DeleteByOrganizeID(Guid organizeID)
        {
            string sql = "DELETE FROM UsersRelation WHERE OrganizeID=@OrganizeID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@OrganizeID", MySqlDbType.VarChar) {
                Value = organizeID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int DeleteByUserID(Guid userID)
        {
            string sql = "DELETE FROM UsersRelation WHERE UserID=@UserID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@UserID", MySqlDbType.VarChar) {
                Value = userID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int DeleteNotIsMainByUserID(Guid userID)
        {
            string sql = "DELETE FROM UsersRelation WHERE IsMain=0 AND UserID=@UserID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@UserID", MySqlDbType.VarChar) {
                Value = userID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.UsersRelation Get(Guid userid, Guid organizeid)
        {
            string sql = "SELECT * FROM usersrelation WHERE UserID=@UserID AND OrganizeID=@OrganizeID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[2];
            MySqlParameter parameter1 = new MySqlParameter("@UserID", MySqlDbType.VarChar, 0x24) {
                Value = userid.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@OrganizeID", MySqlDbType.VarChar, 0x24) {
                Value = organizeid.ToString()
            };
            parameterArray1[1] = parameter2;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.UsersRelation> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.UsersRelation> GetAll()
        {
            string sql = "SELECT * FROM usersrelation";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.UsersRelation> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.UsersRelation> GetAllByOrganizeID(Guid organizeID)
        {
            string sql = "SELECT * FROM UsersRelation WHERE OrganizeID=@OrganizeID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@OrganizeID", MySqlDbType.VarChar) {
                Value = organizeID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.UsersRelation> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.UsersRelation> GetAllByUserID(Guid userID)
        {
            string sql = "SELECT * FROM UsersRelation WHERE UserID=@UserID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@UserID", MySqlDbType.VarChar) {
                Value = userID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.UsersRelation> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM usersrelation";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public YJ.Data.Model.UsersRelation GetMainByUserID(Guid userID)
        {
            string sql = "SELECT * FROM UsersRelation WHERE UserID=@UserID AND IsMain=1";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@UserID", MySqlDbType.VarChar) {
                Value = userID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.UsersRelation> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public int GetMaxSort(Guid organizeID)
        {
            string sql = "SELECT IFNULL(MAX(Sort),0)+1 FROM UsersRelation WHERE OrganizeID=@OrganizeID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@OrganizeID", MySqlDbType.VarChar) {
                Value = organizeID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            DBHelper helper = new DBHelper();
            return helper.GetFieldValue(sql, parameter).ToInt();
        }

        public int Update(YJ.Data.Model.UsersRelation model)
        {
            string sql = "UPDATE usersrelation SET \r\n\t\t\t\tIsMain=@IsMain,Sort=@Sort\r\n\t\t\t\tWHERE UserID=@UserID and OrganizeID=@OrganizeID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[4];
            MySqlParameter parameter1 = new MySqlParameter("@IsMain", MySqlDbType.Int32, 11) {
                Value = model.IsMain
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@UserID", MySqlDbType.VarChar, 0x24) {
                Value = model.UserID
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@OrganizeID", MySqlDbType.VarChar, 0x24) {
                Value = model.OrganizeID
            };
            parameterArray1[3] = parameter4;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

