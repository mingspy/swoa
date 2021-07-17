using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using YJ.Data.Interface;
using YJ.Data.Model;
using YJ.Utility;
namespace YJ.Data.MySql
{


    public class Users : IUsers
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.Users model)
        {
            string sql = "INSERT INTO users\r\n\t\t\t\t(ID,Name,Account,Password,Status,Sort,Note,Mobile,Tel,OtherTel,Fax,Email,QQ,HeadImg,WeiXin,Sex) \r\n\t\t\t\tVALUES(@ID,@Name,@Account,@Password,@Status,@Sort,@Note,@Mobile,@Tel,@OtherTel,@Fax,@Email,@QQ,@HeadImg,@WeiXin,@Sex)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[0x10];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Name", MySqlDbType.VarChar, 50) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Account", MySqlDbType.VarChar, 0xff) {
                Value = model.Account
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@Password", MySqlDbType.Text, -1) {
                Value = model.Password
            };
            parameterArray1[3] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@Status", MySqlDbType.Int32, 11) {
                Value = model.Status
            };
            parameterArray1[4] = parameter5;
            MySqlParameter parameter6 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[5] = parameter6;
            parameterArray1[6] = (model.Note == null) ? new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = model.Note };
            parameterArray1[7] = (model.Mobile == null) ? new MySqlParameter("@Mobile", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@Mobile", MySqlDbType.VarChar, 50) { Value = model.Mobile };
            parameterArray1[8] = (model.Tel == null) ? new MySqlParameter("@Tel", MySqlDbType.VarChar, 500) { Value = DBNull.Value } : new MySqlParameter("@Tel", MySqlDbType.VarChar, 500) { Value = model.Tel };
            parameterArray1[9] = (model.OtherTel == null) ? new MySqlParameter("@OtherTel", MySqlDbType.VarChar, 500) { Value = DBNull.Value } : new MySqlParameter("@OtherTel", MySqlDbType.VarChar, 500) { Value = model.OtherTel };
            parameterArray1[10] = (model.Fax == null) ? new MySqlParameter("@Fax", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@Fax", MySqlDbType.VarChar, 50) { Value = model.Fax };
            parameterArray1[11] = (model.Email == null) ? new MySqlParameter("@Email", MySqlDbType.VarChar, 500) { Value = DBNull.Value } : new MySqlParameter("@Email", MySqlDbType.VarChar, 500) { Value = model.Email };
            parameterArray1[12] = (model.QQ == null) ? new MySqlParameter("@QQ", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@QQ", MySqlDbType.VarChar, 50) { Value = model.QQ };
            parameterArray1[13] = (model.HeadImg == null) ? new MySqlParameter("@HeadImg", MySqlDbType.VarChar, 500) { Value = DBNull.Value } : new MySqlParameter("@HeadImg", MySqlDbType.VarChar, 500) { Value = model.HeadImg };
            parameterArray1[14] = (model.WeiXin == null) ? new MySqlParameter("@WeiXin", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@WeiXin", MySqlDbType.VarChar, 50) { Value = model.WeiXin };
            parameterArray1[15] = !model.Sex.HasValue ? new MySqlParameter("@Sex", MySqlDbType.Int32, 11) { Value = DBNull.Value } : new MySqlParameter("@Sex", MySqlDbType.Int32, 11) { Value = model.Sex.Value };
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.Users> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.Users> list = new List<YJ.Data.Model.Users>();
            YJ.Data.Model.Users item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.Users {
                    ID = dataReader.GetString(0).ToGuid(),
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
            string sql = "DELETE FROM users WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.Users Get(Guid id)
        {
            string sql = "SELECT * FROM users WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Users> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.Users> GetAll()
        {
            string sql = "SELECT * FROM users";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Users> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.Users> GetAllByIDString(string idString)
        {
            string sql = "SELECT * FROM Users WHERE ID IN(" + Tools.GetSqlInString(idString, true, ",") + ")";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Users> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.Users> GetAllByOrganizeID(Guid organizeID)
        {
            string sql = "SELECT * FROM Users WHERE ID in(SELECT UserID FROM UsersRelation WHERE OrganizeID=@OrganizeID) ORDER BY Sort";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@OrganizeID", MySqlDbType.VarChar) {
                Value = organizeID.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
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
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Users> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.Users> GetAllByWorkGroupID(Guid workgroupid)
        {
            string sql = "SELECT * FROM Users WHERE ID IN(SELECT UserID FROM WorkGroupUsers WHERE WorkGroupID='" + workgroupid + "')";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Users> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public YJ.Data.Model.Users GetByAccount(string account)
        {
            string sql = "SELECT * FROM Users WHERE Account=@Account";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@Account", MySqlDbType.VarChar, 0xff) {
                Value = account
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Users> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM users";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public bool HasAccount(string account, [Optional, DefaultParameterValue("")] string userID)
        {
            string sql = "SELECT ID FROM Users WHERE Account=@Account";
            List<MySqlParameter> list = new List<MySqlParameter>();
            MySqlParameter item = new MySqlParameter("@Account", MySqlDbType.VarChar) {
                Value = account
            };
            list.Add(item);
            if (userID.IsGuid())
            {
                sql = sql + " and ID<>@ID";
                MySqlParameter parameter2 = new MySqlParameter("@ID", MySqlDbType.VarChar) {
                    Value = userID
                };
                list.Add(parameter2);
            }
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            bool hasRows = dataReader.HasRows;
            dataReader.Close();
            return hasRows;
        }

        public int Update(YJ.Data.Model.Users model)
        {
            string sql = "UPDATE Users SET \r\n\t\t\t\tName=@Name,Account=@Account,Password=@Password,Status=@Status,Sort=@Sort,Note=@Note,Mobile=@Mobile,Tel=@Tel,OtherTel=@OtherTel,Fax=@Fax,Email=@Email,QQ=@QQ,HeadImg=@HeadImg,WeiXin=@WeiXin,Sex=@Sex\r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[0x10];
            MySqlParameter parameter1 = new MySqlParameter("@Name", MySqlDbType.VarChar, 50) {
                Value = model.Name
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Account", MySqlDbType.VarChar, 0xff) {
                Value = model.Account
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Password", MySqlDbType.Text, -1) {
                Value = model.Password
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@Status", MySqlDbType.Int32, 11) {
                Value = model.Status
            };
            parameterArray1[3] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[4] = parameter5;
            parameterArray1[5] = (model.Note == null) ? new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = model.Note };
            parameterArray1[6] = (model.Mobile == null) ? new MySqlParameter("@Mobile", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@Mobile", MySqlDbType.VarChar, 50) { Value = model.Mobile };
            parameterArray1[7] = (model.Tel == null) ? new MySqlParameter("@Tel", MySqlDbType.VarChar, 500) { Value = DBNull.Value } : new MySqlParameter("@Tel", MySqlDbType.VarChar, 500) { Value = model.Tel };
            parameterArray1[8] = (model.OtherTel == null) ? new MySqlParameter("@OtherTel", MySqlDbType.VarChar, 500) { Value = DBNull.Value } : new MySqlParameter("@OtherTel", MySqlDbType.VarChar, 500) { Value = model.OtherTel };
            parameterArray1[9] = (model.Fax == null) ? new MySqlParameter("@Fax", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@Fax", MySqlDbType.VarChar, 50) { Value = model.Fax };
            parameterArray1[10] = (model.Email == null) ? new MySqlParameter("@Email", MySqlDbType.VarChar, 500) { Value = DBNull.Value } : new MySqlParameter("@Email", MySqlDbType.VarChar, 500) { Value = model.Email };
            parameterArray1[11] = (model.QQ == null) ? new MySqlParameter("@QQ", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@QQ", MySqlDbType.VarChar, 50) { Value = model.QQ };
            parameterArray1[12] = (model.HeadImg == null) ? new MySqlParameter("@HeadImg", MySqlDbType.VarChar, 500) { Value = DBNull.Value } : new MySqlParameter("@HeadImg", MySqlDbType.VarChar, 500) { Value = model.HeadImg };
            parameterArray1[13] = (model.WeiXin == null) ? new MySqlParameter("@WeiXin", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@WeiXin", MySqlDbType.VarChar, 50) { Value = model.WeiXin };
            parameterArray1[14] = !model.Sex.HasValue ? new MySqlParameter("@Sex", MySqlDbType.Int32, 11) { Value = DBNull.Value } : new MySqlParameter("@Sex", MySqlDbType.Int32, 11) { Value = model.Sex.Value };
            MySqlParameter parameter26 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[15] = parameter26;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public bool UpdatePassword(string password, Guid userID)
        {
            string sql = "UPDATE Users SET Password=@Password WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[2];
            MySqlParameter parameter1 = new MySqlParameter("@Password", MySqlDbType.VarChar) {
                Value = password
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@ID", MySqlDbType.VarChar) {
                Value = userID.ToString()
            };
            parameterArray1[1] = parameter2;
            MySqlParameter[] parameter = parameterArray1;
            return (this.dbHelper.Execute(sql, parameter, false) == 1);
        }

        public int UpdateSort(Guid userID, int sort)
        {
            string sql = "UPDATE Users SET Sort=@Sort WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[2];
            MySqlParameter parameter1 = new MySqlParameter("@Sort", MySqlDbType.Int32) {
                Value = sort
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@ID", MySqlDbType.VarChar) {
                Value = userID.ToString()
            };
            parameterArray1[1] = parameter2;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

