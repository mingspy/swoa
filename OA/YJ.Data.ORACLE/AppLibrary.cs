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

    public class AppLibrary : IAppLibrary
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.AppLibrary model)
        {
            string sql = "INSERT INTO AppLibrary\r\n\t\t\t\t(ID,Title,Address,Type,OpenMode,Width,Height,Params,Manager,Note,Code,Ico,Color) \r\n\t\t\t\tVALUES(:ID,:Title,:Address,:Type,:OpenMode,:Width,:Height,:Params,:Manager,:Note,:Code,:Ico,:Color)";
            OracleParameter[] parameterArray1 = new OracleParameter[13];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2, 40) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Title", OracleDbType.NVarchar2, 510) {
                Value = model.Title
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":Address", OracleDbType.Varchar2, 200) {
                Value = model.Address
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":Type", OracleDbType.Varchar2, 40) {
                Value = model.Type
            };
            parameterArray1[3] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":OpenMode", OracleDbType.Int32) {
                Value = model.OpenMode
            };
            parameterArray1[4] = parameter5;
            parameterArray1[5] = !model.Width.HasValue ? new OracleParameter(":Width", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter(":Width", OracleDbType.Int32) { Value = model.Width };
            parameterArray1[6] = !model.Height.HasValue ? new OracleParameter(":Height", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter(":Height", OracleDbType.Int32) { Value = model.Height };
            parameterArray1[7] = (model.Params == null) ? new OracleParameter(":Params", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Params", OracleDbType.Clob) { Value = model.Params };
            parameterArray1[8] = (model.Manager == null) ? new OracleParameter(":Manager", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Manager", OracleDbType.Clob) { Value = model.Manager };
            parameterArray1[9] = (model.Note == null) ? new OracleParameter(":Note", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.Clob) { Value = model.Note };
            parameterArray1[10] = (model.Code == null) ? new OracleParameter(":Code", OracleDbType.Varchar2, 50) { Value = DBNull.Value } : new OracleParameter(":Code", OracleDbType.Varchar2, 50) { Value = model.Code };
            parameterArray1[11] = (model.Ico == null) ? new OracleParameter(":Ico", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Ico", OracleDbType.Varchar2) { Value = model.Ico };
            parameterArray1[12] = (model.Color == null) ? new OracleParameter(":Color", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Color", OracleDbType.Varchar2) { Value = model.Color };
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.AppLibrary> DataReaderToList(OracleDataReader dataReader)
        {
            List<YJ.Data.Model.AppLibrary> list = new List<YJ.Data.Model.AppLibrary>();
            YJ.Data.Model.AppLibrary item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.AppLibrary {
                    ID = dataReader.GetString(0).ToGuid(),
                    Title = dataReader.GetString(1),
                    Address = dataReader.GetString(2),
                    Type = dataReader.GetString(3).ToGuid(),
                    OpenMode = dataReader.GetInt32(4)
                };
                if (!dataReader.IsDBNull(5))
                {
                    item.Width = new int?(dataReader.GetInt32(5));
                }
                if (!dataReader.IsDBNull(6))
                {
                    item.Height = new int?(dataReader.GetInt32(6));
                }
                if (!dataReader.IsDBNull(7))
                {
                    item.Params = dataReader.GetString(7);
                }
                if (!dataReader.IsDBNull(8))
                {
                    item.Manager = dataReader.GetString(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    item.Note = dataReader.GetString(9);
                }
                if (!dataReader.IsDBNull(10))
                {
                    item.Code = dataReader.GetString(10);
                }
                if (!dataReader.IsDBNull(11))
                {
                    item.Ico = dataReader.GetString(11);
                }
                if (!dataReader.IsDBNull(12))
                {
                    item.Color = dataReader.GetString(12);
                }
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM AppLibrary WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public int Delete(string[] idArray)
        {
            string sql = "DELETE FROM AppLibrary WHERE ID IN(" + Tools.GetSqlInString<string>(idArray, true) + ")";
            return this.dbHelper.Execute(sql);
        }

        public YJ.Data.Model.AppLibrary Get(Guid id)
        {
            string sql = "SELECT * FROM AppLibrary WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.AppLibrary> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.AppLibrary> GetAll()
        {
            string sql = "SELECT * FROM AppLibrary";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.AppLibrary> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.AppLibrary> GetAllByType(string types)
        {
            string sql = "SELECT * FROM AppLibrary WHERE Type IN(" + Tools.GetSqlInString(types, true, ",") + ")";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.AppLibrary> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public YJ.Data.Model.AppLibrary GetByCode(string code)
        {
            string sql = "SELECT * FROM AppLibrary WHERE Code=:Code";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":Code", OracleDbType.Varchar2, 50) {
                Value = code
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.AppLibrary> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM AppLibrary";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public List<YJ.Data.Model.AppLibrary> GetPagerData(out long count, [Optional, DefaultParameterValue(15)] int size, [Optional, DefaultParameterValue(1)] int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string type, [Optional, DefaultParameterValue("")] string address, [Optional, DefaultParameterValue("")] string order)
        {
            StringBuilder builder = new StringBuilder();
            List<OracleParameter> list = new List<OracleParameter>();
            if (!title.IsNullOrEmpty())
            {
                builder.Append("AND INSTR(Title,:Title,1,1)>0 ");
                OracleParameter item = new OracleParameter(":Title", OracleDbType.NVarchar2) {
                    Value = title
                };
                list.Add(item);
            }
            if (!type.IsNullOrEmpty())
            {
                builder.AppendFormat("AND Type IN({0}) ", Tools.GetSqlInString(type, true, ","));
            }
            if (!address.IsNullOrEmpty())
            {
                builder.Append("AND INSTR(Address,:Address,1,1)>0 ");
                OracleParameter parameter2 = new OracleParameter(":Address", OracleDbType.Varchar2) {
                    Value = address
                };
                list.Add(parameter2);
            }
            string sql = this.dbHelper.GetPaerSql("AppLibrary", "*", builder.ToString(), order.IsNullOrEmpty() ? "Type,Title" : order, size, number, out count, list.ToArray());
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.AppLibrary> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public List<YJ.Data.Model.AppLibrary> GetPagerData(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("Type,Title")] string order, [Optional, DefaultParameterValue(15)] int size, [Optional, DefaultParameterValue(1)] int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string type, [Optional, DefaultParameterValue("")] string address)
        {
            long num;
            StringBuilder builder = new StringBuilder();
            List<OracleParameter> list = new List<OracleParameter>();
            if (!title.IsNullOrEmpty())
            {
                builder.Append("AND INSTR(Title,:Title,1,1)>0 ");
                OracleParameter item = new OracleParameter(":Title", OracleDbType.NVarchar2) {
                    Value = title
                };
                list.Add(item);
            }
            if (!type.IsNullOrEmpty())
            {
                builder.AppendFormat("AND Type IN({0}) ", Tools.GetSqlInString(type, true, ","));
            }
            if (!address.IsNullOrEmpty())
            {
                builder.Append("AND INSTR(Address,:Address,1,1)>0 ");
                OracleParameter parameter2 = new OracleParameter(":Address", OracleDbType.Varchar2) {
                    Value = address
                };
                list.Add(parameter2);
            }
            string sql = this.dbHelper.GetPaerSql("AppLibrary", "*", builder.ToString(), order, size, number, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, size, number, query);
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.AppLibrary> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public int Update(YJ.Data.Model.AppLibrary model)
        {
            string sql = "UPDATE AppLibrary SET \r\n\t\t\t\tTitle=:Title,Address=:Address,Type=:Type,OpenMode=:OpenMode,Width=:Width,Height=:Height,Params=:Params,Manager=:Manager,Note=:Note,Code=:Code,Ico=:Ico,Color=:Color\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[13];
            OracleParameter parameter1 = new OracleParameter(":Title", OracleDbType.NVarchar2, 510) {
                Value = model.Title
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Address", OracleDbType.Varchar2, 200) {
                Value = model.Address
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":Type", OracleDbType.Varchar2, 40) {
                Value = model.Type
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":OpenMode", OracleDbType.Int32) {
                Value = model.OpenMode
            };
            parameterArray1[3] = parameter4;
            parameterArray1[4] = !model.Width.HasValue ? new OracleParameter(":Width", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter(":Width", OracleDbType.Int32) { Value = model.Width };
            parameterArray1[5] = !model.Height.HasValue ? new OracleParameter(":Height", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter(":Height", OracleDbType.Int32) { Value = model.Height };
            parameterArray1[6] = (model.Params == null) ? new OracleParameter(":Params", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Params", OracleDbType.Clob) { Value = model.Params };
            parameterArray1[7] = (model.Manager == null) ? new OracleParameter(":Manager", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Manager", OracleDbType.Clob) { Value = model.Manager };
            parameterArray1[8] = (model.Note == null) ? new OracleParameter(":Note", OracleDbType.Clob) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.Clob) { Value = model.Note };
            parameterArray1[9] = (model.Code == null) ? new OracleParameter(":Code", OracleDbType.Varchar2, 50) { Value = DBNull.Value } : new OracleParameter(":Code", OracleDbType.Varchar2, 50) { Value = model.Code };
            parameterArray1[10] = (model.Ico == null) ? new OracleParameter(":Ico", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Ico", OracleDbType.Varchar2) { Value = model.Ico };
            parameterArray1[11] = (model.Color == null) ? new OracleParameter(":Color", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Color", OracleDbType.Varchar2) { Value = model.Color };
            OracleParameter parameter21 = new OracleParameter(":ID", OracleDbType.Varchar2, 40) {
                Value = model.ID
            };
            parameterArray1[12] = parameter21;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

