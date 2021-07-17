namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using YJ.Data.Interface;
    using YJ.Data.Model;
    using YJ.Utility;

    public class Users : IUsers
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.Users model)
        {
            string sql = "INSERT INTO Users\r\n\t\t\t\t(ID,Name,Account,Password,Status,Sort,Note,Mobile,Tel,OtherTel,Fax,Email,QQ,HeadImg,WeiXin,Sex) \r\n\t\t\t\tVALUES(@ID,@Name,@Account,@Password,@Status,@Sort,@Note,@Mobile,@Tel,@OtherTel,@Fax,@Email,@QQ,@HeadImg,@WeiXin,@Sex)";
            SqlParameter[] parameterArray1 = new SqlParameter[0x10];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Name", SqlDbType.NVarChar, 100) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Account", SqlDbType.VarChar, 0xff) {
                Value = model.Account
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@Password", SqlDbType.VarChar, 500) {
                Value = model.Password
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@Status", SqlDbType.Int, -1) {
                Value = model.Status
            };
            parameterArray1[4] = parameter5;
            SqlParameter parameter6 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[5] = parameter6;
            parameterArray1[6] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.NVarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.NVarChar, -1) { Value = model.Note };
            parameterArray1[7] = (model.Mobile == null) ? new SqlParameter("@Mobile", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Mobile", SqlDbType.VarChar, 50) { Value = model.Mobile };
            parameterArray1[8] = (model.Tel == null) ? new SqlParameter("@Tel", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@Tel", SqlDbType.VarChar, 500) { Value = model.Tel };
            parameterArray1[9] = (model.OtherTel == null) ? new SqlParameter("@OtherTel", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@OtherTel", SqlDbType.VarChar, 500) { Value = model.OtherTel };
            parameterArray1[10] = (model.Fax == null) ? new SqlParameter("@Fax", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Fax", SqlDbType.VarChar, 50) { Value = model.Fax };
            parameterArray1[11] = (model.Email == null) ? new SqlParameter("@Email", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@Email", SqlDbType.VarChar, 500) { Value = model.Email };
            parameterArray1[12] = (model.QQ == null) ? new SqlParameter("@QQ", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@QQ", SqlDbType.VarChar, 50) { Value = model.QQ };
            parameterArray1[13] = (model.HeadImg == null) ? new SqlParameter("@HeadImg", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@HeadImg", SqlDbType.VarChar, 500) { Value = model.HeadImg };
            parameterArray1[14] = (model.WeiXin == null) ? new SqlParameter("@WeiXin", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@WeiXin", SqlDbType.VarChar, 50) { Value = model.WeiXin };
            parameterArray1[15] = !model.Sex.HasValue ? new SqlParameter("@Sex", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@Sex", SqlDbType.Int, -1) { Value = model.Sex };
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.Users> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.Users> list = new List<YJ.Data.Model.Users>();
            YJ.Data.Model.Users item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.Users {
                    ID = dataReader.GetGuid(0),
                    Name = dataReader.GetString(1),
                    Account = dataReader.GetString(2),
                    Password = dataReader.GetString(3),
                    Status = dataReader.GetInt32(4),
                    Sort = dataReader.GetInt32(5)
                };
                if (!dataReader.IsDBNull(6))
                {
                    item.Note = dataReader.GetString(6);
                }
                if (!dataReader.IsDBNull(7))
                {
                    item.Mobile = dataReader.GetString(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    item.Tel = dataReader.GetString(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    item.OtherTel = dataReader.GetString(9);
                }
                if (!dataReader.IsDBNull(10))
                {
                    item.Fax = dataReader.GetString(10);
                }
                if (!dataReader.IsDBNull(11))
                {
                    item.Email = dataReader.GetString(11);
                }
                if (!dataReader.IsDBNull(12))
                {
                    item.QQ = dataReader.GetString(12);
                }
                if (!dataReader.IsDBNull(13))
                {
                    item.HeadImg = dataReader.GetString(13);
                }
                if (!dataReader.IsDBNull(14))
                {
                    item.WeiXin = dataReader.GetString(14);
                }
                if (!dataReader.IsDBNull(15))
                {
                    item.Sex = new int?(dataReader.GetInt32(15));
                }
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM Users WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.Users Get(Guid id)
        {
            string sql = "SELECT * FROM Users WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Users> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.Users> GetAll()
        {
            string sql = "SELECT * FROM Users";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Users> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.Users> GetAllByIDString(string idString)
        {
            string sql = "SELECT * FROM Users WHERE ID IN(" + Tools.GetSqlInString(idString, true, ",") + ")";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Users> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.Users> GetAllByOrganizeID(Guid organizeID)
        {
            string sql = "SELECT * FROM Users WHERE ID in(SELECT UserID FROM UsersRelation WHERE OrganizeID=@OrganizeID) ORDER BY Sort";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@OrganizeID", SqlDbType.UniqueIdentifier) {
                Value = organizeID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Users> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.Users> GetAllByOrganizeIDArray(Guid[] organizeIDArray)
        {
            if ((organizeIDArray == null) || (organizeIDArray.Length == 0))
            {
                return new List<YJ.Data.Model.Users>();
            }
            string sql = "SELECT * FROM Users WHERE ID in(SELECT UserID FROM UsersRelation WHERE OrganizeID in(" + Tools.GetSqlInString<Guid>(organizeIDArray, true) + ")) ORDER BY Sort";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Users> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.Users> GetAllByWorkGroupID(Guid workgroupid)
        {
            string sql = "SELECT * FROM Users WHERE ID IN(SELECT UserID FROM WorkGroupUsers WHERE WorkGroupID='" + workgroupid + "')";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Users> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public YJ.Data.Model.Users GetByAccount(string account)
        {
            string sql = "SELECT * FROM Users WHERE Account=@Account";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@Account", SqlDbType.VarChar, 0xff) {
                Value = account
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Users> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM Users";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public bool HasAccount(string account, [Optional, DefaultParameterValue("")] string userID)
        {
            string sql = "SELECT ID FROM Users WHERE Account=@Account";
            List<SqlParameter> list = new List<SqlParameter>();
            SqlParameter item = new SqlParameter("@Account", SqlDbType.VarChar) {
                Value = account
            };
            list.Add(item);
            if (userID.IsGuid())
            {
                sql = sql + " and ID<>@ID";
                SqlParameter parameter2 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                    Value = userID.ToGuid()
                };
                list.Add(parameter2);
            }
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            bool hasRows = dataReader.HasRows;
            dataReader.Close();
            return hasRows;
        }

        public int Update(YJ.Data.Model.Users model)
        {
            string sql = "UPDATE Users SET \r\n\t\t\t\tName=@Name,Account=@Account,Password=@Password,Status=@Status,Sort=@Sort,Note=@Note,Mobile=@Mobile,Tel=@Tel,OtherTel=@OtherTel,Fax=@Fax,Email=@Email,QQ=@QQ,HeadImg=@HeadImg,WeiXin=@WeiXin,Sex=@Sex\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[0x10];
            SqlParameter parameter1 = new SqlParameter("@Name", SqlDbType.NVarChar, 100) {
                Value = model.Name
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Account", SqlDbType.VarChar, 0xff) {
                Value = model.Account
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Password", SqlDbType.VarChar, 500) {
                Value = model.Password
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@Status", SqlDbType.Int, -1) {
                Value = model.Status
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[4] = parameter5;
            parameterArray1[5] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.NVarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.NVarChar, -1) { Value = model.Note };
            parameterArray1[6] = (model.Mobile == null) ? new SqlParameter("@Mobile", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Mobile", SqlDbType.VarChar, 50) { Value = model.Mobile };
            parameterArray1[7] = (model.Tel == null) ? new SqlParameter("@Tel", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@Tel", SqlDbType.VarChar, 500) { Value = model.Tel };
            parameterArray1[8] = (model.OtherTel == null) ? new SqlParameter("@OtherTel", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@OtherTel", SqlDbType.VarChar, 500) { Value = model.OtherTel };
            parameterArray1[9] = (model.Fax == null) ? new SqlParameter("@Fax", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Fax", SqlDbType.VarChar, 50) { Value = model.Fax };
            parameterArray1[10] = (model.Email == null) ? new SqlParameter("@Email", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@Email", SqlDbType.VarChar, 500) { Value = model.Email };
            parameterArray1[11] = (model.QQ == null) ? new SqlParameter("@QQ", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@QQ", SqlDbType.VarChar, 50) { Value = model.QQ };
            parameterArray1[12] = (model.HeadImg == null) ? new SqlParameter("@HeadImg", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@HeadImg", SqlDbType.VarChar, 500) { Value = model.HeadImg };
            parameterArray1[13] = (model.WeiXin == null) ? new SqlParameter("@WeiXin", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@WeiXin", SqlDbType.VarChar, 50) { Value = model.WeiXin };
            parameterArray1[14] = !model.Sex.HasValue ? new SqlParameter("@Sex", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@Sex", SqlDbType.Int, -1) { Value = model.Sex };
            SqlParameter parameter26 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[15] = parameter26;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public bool UpdatePassword(string password, Guid userID)
        {
            string sql = "UPDATE Users SET Password=@Password WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[2];
            SqlParameter parameter1 = new SqlParameter("@Password", SqlDbType.VarChar) {
                Value = password
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = userID
            };
            parameterArray1[1] = parameter2;
            SqlParameter[] parameter = parameterArray1;
            return (this.dbHelper.Execute(sql, parameter, false) == 1);
        }

        public int UpdateSort(Guid userID, int sort)
        {
            string sql = "UPDATE Users SET Sort=@Sort WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[2];
            SqlParameter parameter1 = new SqlParameter("@Sort", SqlDbType.Int) {
                Value = sort
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = userID
            };
            parameterArray1[1] = parameter2;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

