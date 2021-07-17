using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using YJ.Data.Interface;
using YJ.Data.Model;
namespace YJ.Data.MySql
{


    public class WorkCalendar : IWorkCalendar
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkCalendar model)
        {
            string sql = "INSERT INTO workcalendar\r\n\t\t\t\t(WorkDate) \r\n\t\t\t\tVALUES(@WorkDate)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@WorkDate", MySqlDbType.Date, -1) {
                Value = model.WorkDate
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.WorkCalendar> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.WorkCalendar> list = new List<YJ.Data.Model.WorkCalendar>();
            YJ.Data.Model.WorkCalendar item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.WorkCalendar {
                    WorkDate = dataReader.GetDateTime(0)
                };
                list.Add(item);
            }
            return list;
        }

        public int Delete(DateTime workdate)
        {
            string sql = "DELETE FROM workcalendar WHERE WorkDate=@WorkDate";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@WorkDate", MySqlDbType.Date) {
                Value = workdate
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int Delete(int year)
        {
            string sql = "DELETE FROM WorkCalendar WHERE YEAR(WorkDate)=@WorkDate";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@WorkDate", MySqlDbType.Int32) {
                Value = year
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.WorkCalendar Get(DateTime workdate)
        {
            string sql = "SELECT * FROM workcalendar WHERE WorkDate=@WorkDate";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@WorkDate", MySqlDbType.Date) {
                Value = workdate
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkCalendar> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkCalendar> GetAll()
        {
            string sql = "SELECT * FROM workcalendar";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkCalendar> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkCalendar> GetAll(int year)
        {
            string sql = "SELECT * FROM WorkCalendar WHERE YEAR(WorkDate)=@WorkDate";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@WorkDate", MySqlDbType.Int32) {
                Value = year
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkCalendar> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM workcalendar";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.WorkCalendar model)
        {
            string sql = "UPDATE workcalendar SET \r\n\t\t\t\t\r\n\t\t\t\tWHERE WorkDate=@WorkDate";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@WorkDate", MySqlDbType.Date, -1) {
                Value = model.WorkDate
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

