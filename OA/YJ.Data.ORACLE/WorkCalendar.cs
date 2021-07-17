namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class WorkCalendar : IWorkCalendar
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WorkCalendar model)
        {
            string sql = "INSERT INTO WorkCalendar\r\n\t\t\t\t(WorkDate) \r\n\t\t\t\tVALUES(:WorkDate)";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter("@WorkDate", OracleDbType.Date) {
                Value = model.WorkDate
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.WorkCalendar> DataReaderToList(OracleDataReader dataReader)
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
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter("@WorkDate", OracleDbType.Date) {
                Value = workdate
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public int Delete(int year)
        {
            string sql = "DELETE FROM WorkCalendar WHERE to_char(WorkDate,'yyyy')=:WorkDate";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter("@WorkDate", OracleDbType.Varchar2) {
                Value = year
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.WorkCalendar Get(DateTime workdate)
        {
            string sql = "SELECT * FROM WorkCalendar WHERE WorkDate=:WorkDate";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":WorkDate", OracleDbType.Date) {
                Value = workdate
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WorkCalendar> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WorkCalendar> GetAll()
        {
            string sql = "SELECT * FROM WorkCalendar";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WorkCalendar> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.WorkCalendar> GetAll(int year)
        {
            string sql = "SELECT * FROM WorkCalendar WHERE to_char(WorkDate,'yyyy')=:WorkDate";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":WorkDate", OracleDbType.Varchar2) {
                Value = year
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
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
            string sql = "UPDATE WorkCalendar SET \r\n\t\t\t\tWHERE WorkDate=:WorkDate";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter("@WorkDate", OracleDbType.Date) {
                Value = model.WorkDate
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

