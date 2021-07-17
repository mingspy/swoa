namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class WorkCalendar : IWorkCalendar
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkCalendar model)
        {
            string sql = "INSERT INTO WorkCalendar\r\n\t\t\t\t(WorkDate) \r\n\t\t\t\tVALUES(@WorkDate)";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@WorkDate", SqlDbType.Date, -1) {
                Value = model.WorkDate
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.WorkCalendar> DataReaderToList(SqlDataReader dataReader)
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
            string sql = "DELETE FROM WorkCalendar WHERE WorkDate=@WorkDate";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@WorkDate", SqlDbType.Date) {
                Value = workdate
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int Delete(int year)
        {
            string sql = "DELETE FROM WorkCalendar WHERE YEAR(WorkDate)=@WorkDate";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@WorkDate", SqlDbType.Int) {
                Value = year
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.WorkCalendar Get(DateTime workdate)
        {
            string sql = "SELECT * FROM WorkCalendar WHERE WorkDate=@WorkDate";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@WorkDate", SqlDbType.Date) {
                Value = workdate
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkCalendar> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkCalendar> GetAll()
        {
            string sql = "SELECT * FROM WorkCalendar";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkCalendar> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkCalendar> GetAll(int year)
        {
            string sql = "SELECT * FROM WorkCalendar WHERE YEAR(WorkDate)=@WorkDate";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@WorkDate", SqlDbType.Int) {
                Value = year
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkCalendar> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM WorkCalendar";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.WorkCalendar model)
        {
            string sql = "UPDATE WorkCalendar SET \r\n\t\t\t\t\r\n\t\t\t\tWHERE WorkDate=@WorkDate";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@WorkDate", SqlDbType.Date, -1) {
                Value = model.WorkDate
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

