namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;
    using YJ.Data.Interface;
    using YJ.Data.Model;
    using YJ.Utility;

    public class AppLibraryButtons : IAppLibraryButtons
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.AppLibraryButtons model)
        {
            string sql = "INSERT INTO AppLibraryButtons\r\n\t\t\t\t(ID,Name,Events,Ico,Sort,Note) \r\n\t\t\t\tVALUES(:ID,:Name,:Events,:Ico,:Sort,:Note)";
            OracleParameter[] parameterArray1 = new OracleParameter[6];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Name", OracleDbType.Varchar2) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":Events", OracleDbType.Varchar2) {
                Value = model.Events
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = (model.Ico == null) ? new OracleParameter(":Ico", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Ico", OracleDbType.Varchar2) { Value = model.Ico };
            OracleParameter parameter6 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[4] = parameter6;
            parameterArray1[5] = (model.Note == null) ? new OracleParameter(":Note", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.Varchar2) { Value = model.Note };
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.AppLibraryButtons> DataReaderToList(OracleDataReader dataReader)
        {
            List<YJ.Data.Model.AppLibraryButtons> list = new List<YJ.Data.Model.AppLibraryButtons>();
            YJ.Data.Model.AppLibraryButtons item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.AppLibraryButtons {
                    ID = dataReader.GetString(0).ToGuid(),
                    Name = dataReader.GetString(1),
                    Events = dataReader.GetString(2)
                };
                if (!dataReader.IsDBNull(3))
                {
                    item.Ico = dataReader.GetString(3);
                }
                item.Sort = dataReader.GetInt32(4);
                if (!dataReader.IsDBNull(5))
                {
                    item.Note = dataReader.GetString(5);
                }
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM AppLibraryButtons WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.AppLibraryButtons Get(Guid id)
        {
            string sql = "SELECT * FROM AppLibraryButtons WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.AppLibraryButtons> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.AppLibraryButtons> GetAll()
        {
            string sql = "SELECT * FROM AppLibraryButtons";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.AppLibraryButtons> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM AppLibraryButtons";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int GetMaxSort()
        {
            string sql = "SELECT MAX(Sort) FROM AppLibraryButtons";
            return (this.dbHelper.GetFieldValue(sql).ToInt(0) + 5);
        }

        public List<YJ.Data.Model.AppLibraryButtons> GetPagerData(out long count, int size, int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string order)
        {
            StringBuilder builder = new StringBuilder();
            List<OracleParameter> list = new List<OracleParameter>();
            if (!title.IsNullOrEmpty())
            {
                builder.Append("AND INSTR(Name,:Name,1,1)>0 ");
                OracleParameter item = new OracleParameter(":Name", OracleDbType.Varchar2) {
                    Value = title
                };
                list.Add(item);
            }
            string sql = this.dbHelper.GetPaerSql("AppLibraryButtons", "*", builder.ToString(), order.IsNullOrEmpty() ? "Sort DESC" : order, size, number, out count, list.ToArray());
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.AppLibraryButtons> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public List<YJ.Data.Model.AppLibraryButtons> GetPagerData(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue(15)] int size, [Optional, DefaultParameterValue(1)] int number, [Optional, DefaultParameterValue("")] string title)
        {
            long num;
            StringBuilder builder = new StringBuilder();
            List<OracleParameter> list = new List<OracleParameter>();
            if (!title.IsNullOrEmpty())
            {
                builder.Append("AND INSTR(Name,:Name,1,1)>0 ");
                OracleParameter item = new OracleParameter(":Name", OracleDbType.Varchar2) {
                    Value = title
                };
                list.Add(item);
            }
            string sql = this.dbHelper.GetPaerSql("AppLibraryButtons", "*", builder.ToString(), "Sort DESC", size, number, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, size, number, query);
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.AppLibraryButtons> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public int Update(YJ.Data.Model.AppLibraryButtons model)
        {
            string sql = "UPDATE AppLibraryButtons SET \r\n\t\t\t\tName=:Name,Events=:Events,Ico=:Ico,Sort=:Sort,Note=:Note\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[6];
            OracleParameter parameter1 = new OracleParameter(":Name", OracleDbType.Varchar2) {
                Value = model.Name
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Events", OracleDbType.Varchar2) {
                Value = model.Events
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = (model.Ico == null) ? new OracleParameter(":Ico", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Ico", OracleDbType.Varchar2) { Value = model.Ico };
            OracleParameter parameter5 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[3] = parameter5;
            parameterArray1[4] = (model.Note == null) ? new OracleParameter(":Note", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.Varchar2) { Value = model.Note };
            OracleParameter parameter8 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[5] = parameter8;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

