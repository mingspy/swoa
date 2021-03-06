using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using YJ.Data.Interface;
using YJ.Data.Model;
namespace YJ.Data.MySql
{


    public class WorkTime : IWorkTime
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkTime model)
        {
            string sql = "INSERT INTO WorkTime\r\n\t\t\t\t(ID,Year,Date1,Date2,AmTime1,AmTime2,PmTime1,PmTime2) \r\n\t\t\t\tVALUES(@ID,@Year,@Date1,@Date2,@AmTime1,@AmTime2,@PmTime1,@PmTime2)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[8];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 50) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Year", MySqlDbType.Int32) {
                Value = model.Year
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Date1", MySqlDbType.DateTime) {
                Value = model.Date1
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@Date2", MySqlDbType.DateTime) {
                Value = model.Date2
            };
            parameterArray1[3] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@AmTime1", MySqlDbType.VarChar, 50) {
                Value = model.AmTime1
            };
            parameterArray1[4] = parameter5;
            MySqlParameter parameter6 = new MySqlParameter("@AmTime2", MySqlDbType.VarChar, 50) {
                Value = model.AmTime2
            };
            parameterArray1[5] = parameter6;
            MySqlParameter parameter7 = new MySqlParameter("@PmTime1", MySqlDbType.VarChar, 50) {
                Value = model.PmTime1
            };
            parameterArray1[6] = parameter7;
            MySqlParameter parameter8 = new MySqlParameter("@PmTime2", MySqlDbType.VarChar, 50) {
                Value = model.PmTime2
            };
            parameterArray1[7] = parameter8;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.WorkTime> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.WorkTime> list = new List<YJ.Data.Model.WorkTime>();
            YJ.Data.Model.WorkTime item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.WorkTime {
                    ID = dataReader.GetGuid(0),
                    Year = dataReader.GetInt32(1),
                    Date1 = dataReader.GetDateTime(2),
                    Date2 = dataReader.GetDateTime(3),
                    AmTime1 = dataReader.GetString(4),
                    AmTime2 = dataReader.GetString(5),
                    PmTime1 = dataReader.GetString(6),
                    PmTime2 = dataReader.GetString(7)
                };
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM WorkTime WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.WorkTime Get(Guid id)
        {
            string sql = "SELECT * FROM WorkTime WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkTime> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkTime> GetAll()
        {
            string sql = "SELECT * FROM WorkTime";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkTime> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkTime> GetAll(int year)
        {
            string sql = "SELECT * FROM WorkTime WHERE Year=" + year;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkTime> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<int> GetAllYear()
        {
            string sql = "SELECT DISTINCT Year FROM WorkTime";
            DataTable dataTable = this.dbHelper.GetDataTable(sql);
            List<int> list = new List<int>();
            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(row[0].ToString().ToInt());
            }
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM WorkTime";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.WorkTime model)
        {
            string sql = "UPDATE WorkTime SET \r\n\t\t\t\tYear=@Year,Date1=@Date1,Date2=@Date2,AmTime1=@AmTime1,AmTime2=@AmTime2,PmTime1=@PmTime1,PmTime2=@PmTime2\r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[8];
            MySqlParameter parameter1 = new MySqlParameter("@Year", MySqlDbType.Int32) {
                Value = model.Year
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Date1", MySqlDbType.DateTime) {
                Value = model.Date1
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Date2", MySqlDbType.DateTime) {
                Value = model.Date2
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@AmTime1", MySqlDbType.VarChar, 50) {
                Value = model.AmTime1
            };
            parameterArray1[3] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@AmTime2", MySqlDbType.VarChar, 50) {
                Value = model.AmTime2
            };
            parameterArray1[4] = parameter5;
            MySqlParameter parameter6 = new MySqlParameter("@PmTime1", MySqlDbType.VarChar, 50) {
                Value = model.PmTime1
            };
            parameterArray1[5] = parameter6;
            MySqlParameter parameter7 = new MySqlParameter("@PmTime2", MySqlDbType.VarChar, 50) {
                Value = model.PmTime2
            };
            parameterArray1[6] = parameter7;
            MySqlParameter parameter8 = new MySqlParameter("@ID", MySqlDbType.VarChar, 50) {
                Value = model.ID
            };
            parameterArray1[7] = parameter8;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

