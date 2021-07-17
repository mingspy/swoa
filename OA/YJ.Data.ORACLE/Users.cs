namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using YJ.Data.Interface;
    using YJ.Data.Model;
    using YJ.Utility;

    public class Users : IUsers
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.Users model)
        {
            string sql = "INSERT INTO Users\r\n\t\t\t\t(ID,Name,Account,Password,Status,Sort,Note,Mobile,Tel,OtherTel,Fax,Email,QQ,HeadImg,WeiXin,Sex) \r\n\t\t\t\tVALUES(:ID,:Name,:Account,:Password,:Status,:Sort,:Note,:Mobile,:Tel,:OtherTel,:Fax,:Email,:QQ,:HeadImg,:WeiXin,:Sex)";
            OracleParameter[] parameterArray1 = new OracleParameter[0x10];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2, 40) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Name", OracleDbType.NVarchar2, 100) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":Account", OracleDbType.Varchar2, 0xff) {
                Value = model.Account
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":Password", OracleDbType.Varchar2, 500) {
                Value = model.Password
            };
            parameterArray1[3] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":Status", OracleDbType.Int32) {
                Value = model.Status
            };
            parameterArray1[4] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[5] = parameter6;
            parameterArray1[6] = (model.Note == null) ? new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = model.Note };
            parameterArray1[7] = (model.Mobile == null) ? new OracleParameter("@Mobile", OracleDbType.NVarchar2, 50) { Value = DBNull.Value } : new OracleParameter("@Mobile", OracleDbType.NVarchar2, 50) { Value = model.Mobile };
            parameterArray1[8] = (model.Tel == null) ? new OracleParameter("@Tel", OracleDbType.NVarchar2, 500) { Value = DBNull.Value } : new OracleParameter("@Tel", OracleDbType.NVarchar2, 500) { Value = model.Tel };
            parameterArray1[9] = (model.OtherTel == null) ? new OracleParameter("@OtherTel", OracleDbType.NVarchar2, 500) { Value = DBNull.Value } : new OracleParameter("@OtherTel", OracleDbType.NVarchar2, 500) { Value = model.OtherTel };
            parameterArray1[10] = (model.Fax == null) ? new OracleParameter("@Fax", OracleDbType.NVarchar2, 50) { Value = DBNull.Value } : new OracleParameter("@Fax", OracleDbType.NVarchar2, 50) { Value = model.Fax };
            parameterArray1[11] = (model.Email == null) ? new OracleParameter("@Email", OracleDbType.NVarchar2, 500) { Value = DBNull.Value } : new OracleParameter("@Email", OracleDbType.NVarchar2, 500) { Value = model.Email };
            parameterArray1[12] = (model.QQ == null) ? new OracleParameter("@QQ", OracleDbType.NVarchar2, 50) { Value = DBNull.Value } : new OracleParameter("@QQ", OracleDbType.NVarchar2, 50) { Value = model.QQ };
            parameterArray1[13] = (model.HeadImg == null) ? new OracleParameter("@HeadImg", OracleDbType.NVarchar2, 500) { Value = DBNull.Value } : new OracleParameter("@HeadImg", OracleDbType.NVarchar2, 500) { Value = model.HeadImg };
            parameterArray1[14] = (model.WeiXin == null) ? new OracleParameter("@WeiXin", OracleDbType.NVarchar2, 50) { Value = DBNull.Value } : new OracleParameter("@WeiXin", OracleDbType.NVarchar2, 50) { Value = model.WeiXin };
            parameterArray1[15] = !model.Sex.HasValue ? new OracleParameter("@Sex", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter("@Sex", OracleDbType.Int32, 11) { Value = model.Sex.Value };
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.Users> DataReaderToList(OracleDataReader dataReader)
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
            string sql = "DELETE FROM Users WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.Users Get(Guid id)
        {
            string sql = "SELECT * FROM Users WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Users> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.Users> GetAll()
        {
            string sql = "SELECT * FROM Users";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Users> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.Users> GetAllByIDString(string idString)
        {
            string sql = "SELECT * FROM Users WHERE ID IN(" + Tools.GetSqlInString(idString, true, ",") + ")";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Users> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.Users> GetAllByOrganizeID(Guid organizeID)
        {
            string sql = "SELECT * FROM Users WHERE ID in(SELECT UserID FROM UsersRelation WHERE OrganizeID=:OrganizeID) ORDER BY Sort";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":OrganizeID", OracleDbType.Varchar2) {
                Value = organizeID
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
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
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Users> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.Users> GetAllByWorkGroupID(Guid workgroupid)
        {
            string sql = "SELECT * FROM Users WHERE ID IN(SELECT UserID FROM WorkGroupUsers WHERE WorkGroupID='" + workgroupid + "')";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Users> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public YJ.Data.Model.Users GetByAccount(string account)
        {
            string sql = "SELECT * FROM Users WHERE Account=:Account";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":Account", OracleDbType.Varchar2, 0xff) {
                Value = account
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
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
            string sql = "SELECT ID FROM Users WHERE Account=:Account";
            List<OracleParameter> list = new List<OracleParameter>();
            OracleParameter item = new OracleParameter(":Account", OracleDbType.Varchar2) {
                Value = account
            };
            list.Add(item);
            if (userID.IsGuid())
            {
                sql = sql + " and ID<>:ID";
                OracleParameter parameter2 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                    Value = userID.ToGuid()
                };
                list.Add(parameter2);
            }
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            bool hasRows = dataReader.HasRows;
            dataReader.Close();
            return hasRows;
        }

        public int Update(YJ.Data.Model.Users model)
        {
            string sql = "UPDATE Users SET \r\n\t\t\t\tName=:Name,Account=:Account,Password=:Password,Status=:Status,Sort=:Sort,Note=:Note,Mobile=:Mobile,Tel=:Tel,OtherTel=:OtherTel,Fax=:Fax,Email=:Email,QQ=:QQ,HeadImg=:HeadImg,WeiXin=:WeiXin,Sex=:Sex\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[0x10];
            OracleParameter parameter1 = new OracleParameter(":Name", OracleDbType.NVarchar2, 100) {
                Value = model.Name
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Account", OracleDbType.Varchar2, 0xff) {
                Value = model.Account
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":Password", OracleDbType.Varchar2, 500) {
                Value = model.Password
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":Status", OracleDbType.Int32) {
                Value = model.Status
            };
            parameterArray1[3] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[4] = parameter5;
            parameterArray1[5] = (model.Note == null) ? new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = model.Note };
            parameterArray1[6] = (model.Mobile == null) ? new OracleParameter("@Mobile", OracleDbType.NVarchar2, 50) { Value = DBNull.Value } : new OracleParameter("@Mobile", OracleDbType.NVarchar2, 50) { Value = model.Mobile };
            parameterArray1[7] = (model.Tel == null) ? new OracleParameter("@Tel", OracleDbType.NVarchar2, 500) { Value = DBNull.Value } : new OracleParameter("@Tel", OracleDbType.NVarchar2, 500) { Value = model.Tel };
            parameterArray1[8] = (model.OtherTel == null) ? new OracleParameter("@OtherTel", OracleDbType.NVarchar2, 500) { Value = DBNull.Value } : new OracleParameter("@OtherTel", OracleDbType.NVarchar2, 500) { Value = model.OtherTel };
            parameterArray1[9] = (model.Fax == null) ? new OracleParameter("@Fax", OracleDbType.NVarchar2, 50) { Value = DBNull.Value } : new OracleParameter("@Fax", OracleDbType.NVarchar2, 50) { Value = model.Fax };
            parameterArray1[10] = (model.Email == null) ? new OracleParameter("@Email", OracleDbType.NVarchar2, 500) { Value = DBNull.Value } : new OracleParameter("@Email", OracleDbType.NVarchar2, 500) { Value = model.Email };
            parameterArray1[11] = (model.QQ == null) ? new OracleParameter("@QQ", OracleDbType.NVarchar2, 50) { Value = DBNull.Value } : new OracleParameter("@QQ", OracleDbType.NVarchar2, 50) { Value = model.QQ };
            parameterArray1[12] = (model.HeadImg == null) ? new OracleParameter("@HeadImg", OracleDbType.NVarchar2, 500) { Value = DBNull.Value } : new OracleParameter("@HeadImg", OracleDbType.NVarchar2, 500) { Value = model.HeadImg };
            parameterArray1[13] = (model.WeiXin == null) ? new OracleParameter("@WeiXin", OracleDbType.NVarchar2, 50) { Value = DBNull.Value } : new OracleParameter("@WeiXin", OracleDbType.NVarchar2, 50) { Value = model.WeiXin };
            parameterArray1[14] = !model.Sex.HasValue ? new OracleParameter("@Sex", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter("@Sex", OracleDbType.Int32, 11) { Value = model.Sex.Value };
            OracleParameter parameter26 = new OracleParameter(":ID", OracleDbType.Varchar2, 40) {
                Value = model.ID
            };
            parameterArray1[15] = parameter26;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public bool UpdatePassword(string password, Guid userID)
        {
            string sql = "UPDATE Users SET Password=:Password WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[2];
            OracleParameter parameter1 = new OracleParameter(":Password", OracleDbType.Varchar2) {
                Value = password
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = userID
            };
            parameterArray1[1] = parameter2;
            OracleParameter[] parameter = parameterArray1;
            return (this.dbHelper.Execute(sql, parameter) == 1);
        }

        public int UpdateSort(Guid userID, int sort)
        {
            string sql = "UPDATE Users SET Sort=:Sort WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[2];
            OracleParameter parameter1 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = sort
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = userID
            };
            parameterArray1[1] = parameter2;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

