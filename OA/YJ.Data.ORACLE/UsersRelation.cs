namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class UsersRelation : IUsersRelation
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.UsersRelation model)
        {
            string sql = "INSERT INTO UsersRelation\r\n\t\t\t\t(UserID,OrganizeID,IsMain,Sort) \r\n\t\t\t\tVALUES(:UserID,:OrganizeID,:IsMain,:Sort)";
            OracleParameter[] parameterArray1 = new OracleParameter[4];
            OracleParameter parameter1 = new OracleParameter(":UserID", OracleDbType.Varchar2, 40) {
                Value = model.UserID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":OrganizeID", OracleDbType.Varchar2, 40) {
                Value = model.OrganizeID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":IsMain", OracleDbType.Int32) {
                Value = model.IsMain
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[3] = parameter4;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.UsersRelation> DataReaderToList(OracleDataReader dataReader)
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
            string sql = "DELETE FROM UsersRelation WHERE UserID=:UserID AND OrganizeID=:OrganizeID";
            OracleParameter[] parameterArray1 = new OracleParameter[2];
            OracleParameter parameter1 = new OracleParameter(":UserID", OracleDbType.Varchar2) {
                Value = userid
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":OrganizeID", OracleDbType.Varchar2) {
                Value = organizeid
            };
            parameterArray1[1] = parameter2;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public int DeleteByOrganizeID(Guid organizeID)
        {
            string sql = "DELETE FROM UsersRelation WHERE OrganizeID=:OrganizeID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":OrganizeID", OracleDbType.Varchar2) {
                Value = organizeID
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public int DeleteByUserID(Guid userID)
        {
            string sql = "DELETE FROM UsersRelation WHERE UserID=:UserID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":UserID", OracleDbType.Varchar2) {
                Value = userID
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public int DeleteNotIsMainByUserID(Guid userID)
        {
            string sql = "DELETE FROM UsersRelation WHERE IsMain=0 AND UserID=:UserID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":UserID", OracleDbType.Varchar2) {
                Value = userID
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.UsersRelation Get(Guid userid, Guid organizeid)
        {
            string sql = "SELECT * FROM UsersRelation WHERE UserID=:UserID AND OrganizeID=:OrganizeID";
            OracleParameter[] parameterArray1 = new OracleParameter[2];
            OracleParameter parameter1 = new OracleParameter(":UserID", OracleDbType.Varchar2) {
                Value = userid
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":OrganizeID", OracleDbType.Varchar2) {
                Value = organizeid
            };
            parameterArray1[1] = parameter2;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.UsersRelation> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.UsersRelation> GetAll()
        {
            string sql = "SELECT * FROM UsersRelation";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.UsersRelation> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.UsersRelation> GetAllByOrganizeID(Guid organizeID)
        {
            string sql = "SELECT * FROM UsersRelation WHERE OrganizeID=:OrganizeID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":OrganizeID", OracleDbType.Varchar2) {
                Value = organizeID
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.UsersRelation> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.UsersRelation> GetAllByUserID(Guid userID)
        {
            string sql = "SELECT * FROM UsersRelation WHERE UserID=:UserID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":UserID", OracleDbType.Varchar2) {
                Value = userID
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
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
            string sql = "SELECT * FROM UsersRelation WHERE UserID=:UserID AND IsMain=1";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":UserID", OracleDbType.Varchar2) {
                Value = userID
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.UsersRelation> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public int GetMaxSort(Guid organizeID)
        {
            string sql = "SELECT nvl(MAX(Sort),0)+1 FROM UsersRelation WHERE OrganizeID=:OrganizeID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":OrganizeID", OracleDbType.Varchar2) {
                Value = organizeID
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            DBHelper helper = new DBHelper();
            return helper.GetFieldValue(sql, parameter).ToInt();
        }

        public int Update(YJ.Data.Model.UsersRelation model)
        {
            string sql = "UPDATE UsersRelation SET \r\n\t\t\t\tIsMain=:IsMain,Sort=:Sort\r\n\t\t\t\tWHERE UserID=:UserID and OrganizeID=:OrganizeID";
            OracleParameter[] parameterArray1 = new OracleParameter[4];
            OracleParameter parameter1 = new OracleParameter(":IsMain", OracleDbType.Int32) {
                Value = model.IsMain
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":UserID", OracleDbType.Varchar2, 40) {
                Value = model.UserID
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":OrganizeID", OracleDbType.Varchar2, 40) {
                Value = model.OrganizeID
            };
            parameterArray1[3] = parameter4;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

