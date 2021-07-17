namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;
    using YJ.Data.Interface;
    using YJ.Data.Model;
    using YJ.Utility;

    public class LoginBlacklist : ILoginBlacklist
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.LoginBlacklist model)
        {
            string sql = "INSERT INTO LoginBlacklist\r\n\t\t\t\t(IPaddress,Account,BlockTime) \r\n\t\t\t\tVALUES(@IPaddress,@Account,@BlockTime)";
            SqlParameter[] parameterArray1 = new SqlParameter[3];
            SqlParameter Account = new SqlParameter("@Account", SqlDbType.NVarChar, -1) {
                Value = model.Account
            };parameterArray1[0] = Account;

            SqlParameter IPaddress = new SqlParameter("@IPaddress", SqlDbType.NVarChar, 100) {
                Value = model.IPaddress
            };parameterArray1[1] = IPaddress;

            SqlParameter BlockTime = new SqlParameter("@BlockTime", SqlDbType.DateTime, 50) {
                Value = model.BlockTime
            }; parameterArray1[2] = BlockTime;

            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.LoginBlacklist> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.LoginBlacklist> list = new List<YJ.Data.Model.LoginBlacklist>();
            YJ.Data.Model.LoginBlacklist item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.LoginBlacklist {
                    ID = dataReader.GetGuid(0),
                    IPaddress= dataReader.GetString(1),
                    Account = dataReader.GetString(2),
                    BlockTime = dataReader.GetDateTime(3)
                };
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM LoginBlacklist WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.LoginBlacklist Get(Guid id)
        {
            string sql = "SELECT * FROM LoginBlacklist WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.LoginBlacklist> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public YJ.Data.Model.LoginBlacklist GetByIPaddress(string IPaddress)
        {
            string sql = "SELECT * FROM LoginBlacklist WHERE IPaddress=@IPaddress";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@IPaddress", SqlDbType.NVarChar,100)
            {
                Value = IPaddress
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.LoginBlacklist> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }


        public List<YJ.Data.Model.LoginBlacklist> GetAll()
        {
            string sql = "SELECT * FROM LoginBlacklist";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.LoginBlacklist> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM LoginBlacklist";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public DataTable GetPagerData(out long count, [Optional, DefaultParameterValue(15)] int size, [Optional, DefaultParameterValue(1)] int number, [Optional, DefaultParameterValue("")] string ID, [Optional, DefaultParameterValue("")] string IPaddress, [Optional, DefaultParameterValue("")] string Account, [Optional, DefaultParameterValue("")] string BlockTime, [Optional, DefaultParameterValue("")] string order)
        {
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            if (!ID.IsNullOrEmpty())
            {
                builder.Append("AND CHARINDEX(@ID,ID)>0 ");
                SqlParameter item = new SqlParameter("@ID", SqlDbType.NVarChar) {
                    Value = ID
                };
                list.Add(item);
            }
            if (!IPaddress.IsNullOrEmpty())
            {
                builder.Append("AND IPaddress=@IPaddress");
                SqlParameter parameter2 = new SqlParameter("@IPaddress", SqlDbType.NVarChar) {
                    Value = IPaddress
                };
                list.Add(parameter2);
            }
            if (BlockTime.IsDateTime())
            {
               
                SqlParameter parameter3 = new SqlParameter("@BlockTime", SqlDbType.DateTime) {
                    Value = BlockTime.ToDateTime().ToString("yyyy-MM-dd 00:00:00")
                };
                list.Add(parameter3);
            }
            if (!Account.IsNullOrEmpty())
            {
                
                SqlParameter parameter4 = new SqlParameter("@Account", SqlDbType.NVarChar) {
                    Value = Account
                };
                list.Add(parameter4);
            }
            string str = this.dbHelper.GetPaerSql("LoginBlacklist", "ID,IPaddress,Account,BlockTime", builder.ToString(), order.IsNullOrEmpty() ? "BlockTime DESC" : order, size, number, out count, list.ToArray());
            return this.dbHelper.GetDataTable(str.ToString(), list.ToArray());
        }

        public DataTable GetPagerData(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue(15)] int size, [Optional, DefaultParameterValue(1)] int number, [Optional, DefaultParameterValue("")] string ID, [Optional, DefaultParameterValue("")] string IPaddress, [Optional, DefaultParameterValue("")] string Account, [Optional, DefaultParameterValue("")] string BlockTime)
        {
            long num;
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            if (!ID.IsNullOrEmpty())
            {
                builder.Append("AND CHARINDEX(@ID,ID)>0 ");
                SqlParameter item = new SqlParameter("@ID", SqlDbType.NVarChar) {
                    Value = ID
                };
                list.Add(item);
            }
            if (!IPaddress.IsNullOrEmpty())
            {
                builder.Append("AND IPaddress=@IPaddress ");
                SqlParameter parameter2 = new SqlParameter("@IPaddress", SqlDbType.NVarChar) {
                    Value = IPaddress
                };
                list.Add(parameter2);
            }
            if (BlockTime.IsDateTime())
            {
                SqlParameter parameter3 = new SqlParameter("@BlockTime", SqlDbType.DateTime) {
                    Value = BlockTime.ToDateTime().ToString("yyyy-MM-dd 00:00:00")
                };
                list.Add(parameter3);
            }
            if (Account.IsNullOrEmpty())
            {
                SqlParameter parameter4 = new SqlParameter("@Acount", SqlDbType.NVarChar) {
                    Value = BlockTime
                };
                list.Add(parameter4);
            }
           
            string sql = this.dbHelper.GetPaerSql("LoginBlockList", "ID,IPaddress,BlockTime,Account", builder.ToString(), "BlockTime DESC", size, number, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, size, number, query);
            return this.dbHelper.GetDataTable(sql, list.ToArray());
        }

        public int Update(YJ.Data.Model.LoginBlacklist model)
        {
            string sql = "UPDATE Log SET \r\n\t\t\t\tIPaddress=@IPaddress,BlockTime=@BlockTime,Account=@Account,\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[12];
            SqlParameter parameter1 = new SqlParameter("@IPaddress", SqlDbType.NVarChar, -1) {
                Value = model.IPaddress
            };
            parameterArray1[0] = parameter1;

            SqlParameter parameter2 = new SqlParameter("@BlockTime", SqlDbType.NVarChar, 100) {
                Value = model.BlockTime
            };
            parameterArray1[1] = parameter2;

            SqlParameter parameter3 = new SqlParameter("@Account", SqlDbType.DateTime, 8) {
                Value = model.Account
            };
            parameterArray1[2] = parameter3;

            SqlParameter parameter4 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };

            parameterArray1[3] = parameter4;

            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

