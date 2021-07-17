namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class Menu : IMenu
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.Menu model)
        {
            string sql = "INSERT INTO Menu\r\n\t\t\t\t(ID,ParentID,AppLibraryID,Title,Params,Ico,Sort,IcoColor) \r\n\t\t\t\tVALUES(:ID,:ParentID,:AppLibraryID,:Title,:Params,:Ico,:Sort,:IcoColor)";
            OracleParameter[] parameterArray1 = new OracleParameter[8];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":ParentID", OracleDbType.Varchar2) {
                Value = model.ParentID
            };
            parameterArray1[1] = parameter2;
            parameterArray1[2] = !model.AppLibraryID.HasValue ? new OracleParameter(":AppLibraryID", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":AppLibraryID", OracleDbType.Varchar2) { Value = model.AppLibraryID };
            OracleParameter parameter5 = new OracleParameter(":Title", OracleDbType.Varchar2) {
                Value = model.Title
            };
            parameterArray1[3] = parameter5;
            parameterArray1[4] = (model.Params == null) ? new OracleParameter(":Params", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Params", OracleDbType.Varchar2) { Value = model.Params };
            parameterArray1[5] = (model.Ico == null) ? new OracleParameter(":Ico", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Ico", OracleDbType.Varchar2) { Value = model.Ico };
            OracleParameter parameter10 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[6] = parameter10;
            parameterArray1[7] = (model.IcoColor == null) ? new OracleParameter(":IcoColor", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":IcoColor", OracleDbType.Varchar2) { Value = model.IcoColor };
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.Menu> DataReaderToList(OracleDataReader dataReader)
        {
            List<YJ.Data.Model.Menu> list = new List<YJ.Data.Model.Menu>();
            YJ.Data.Model.Menu item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.Menu {
                    ID = dataReader.GetString(0).ToGuid(),
                    ParentID = dataReader.GetString(1).ToGuid()
                };
                if (!dataReader.IsDBNull(2))
                {
                    item.AppLibraryID = new Guid?(dataReader.GetString(2).ToGuid());
                }
                item.Title = dataReader.GetString(3);
                if (!dataReader.IsDBNull(4))
                {
                    item.Params = dataReader.GetString(4);
                }
                if (!dataReader.IsDBNull(5))
                {
                    item.Ico = dataReader.GetString(5);
                }
                item.Sort = dataReader.GetInt32(6);
                if (!dataReader.IsDBNull(7))
                {
                    item.IcoColor = dataReader.GetString(7);
                }
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM Menu WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.Menu Get(Guid id)
        {
            string sql = "SELECT * FROM Menu WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Menu> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.Menu> GetAll()
        {
            string sql = "SELECT * FROM Menu";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Menu> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.Menu> GetAllByApplibaryID(Guid applibaryID)
        {
            string sql = "SELECT * FROM Menu WHERE AppLibraryID=:AppLibraryID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":AppLibraryID", OracleDbType.Varchar2) {
                Value = applibaryID.ToString()
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Menu> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public DataTable GetAllDataTable()
        {
            string sql = "SELECT a.*,b.Address,b.OpenMode,b.Width,b.Height,b.Params AS Params1,b.Ico AS AppIco,b.Color AS IcoColor1 FROM Menu a LEFT JOIN AppLibrary b ON a.AppLibraryID=b.ID ORDER BY a.Sort";
            return this.dbHelper.GetDataTable(sql);
        }

        public List<YJ.Data.Model.Menu> GetChild(Guid id)
        {
            string sql = "SELECT * FROM Menu WHERE ParentID=:ParentID ORDER BY Sort";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ParentID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Menu> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM Menu";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int GetMaxSort(Guid id)
        {
            string sql = "SELECT MAX(Sort) FROM Menu WHERE ParentID=:ParentID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ParentID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return (this.dbHelper.GetFieldValue(sql, parameter).ToInt(0) + 1);
        }

        public bool HasChild(Guid id)
        {
            string sql = "SELECT ID FROM Menu WHERE ROWNUM<=1 AND ParentID=:ParentID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ParentID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            bool hasRows = dataReader.HasRows;
            dataReader.Close();
            return hasRows;
        }

        public int Update(YJ.Data.Model.Menu model)
        {
            string sql = "UPDATE Menu SET \r\n\t\t\t\tParentID=:ParentID,AppLibraryID=:AppLibraryID,Title=:Title,Params=:Params,Ico=:Ico,Sort=:Sort,IcoColor=:IcoColor \r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[8];
            OracleParameter parameter1 = new OracleParameter(":ParentID", OracleDbType.Varchar2) {
                Value = model.ParentID
            };
            parameterArray1[0] = parameter1;
            parameterArray1[1] = !model.AppLibraryID.HasValue ? new OracleParameter(":AppLibraryID", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":AppLibraryID", OracleDbType.Varchar2) { Value = model.AppLibraryID };
            OracleParameter parameter4 = new OracleParameter(":Title", OracleDbType.Varchar2) {
                Value = model.Title
            };
            parameterArray1[2] = parameter4;
            parameterArray1[3] = (model.Params == null) ? new OracleParameter(":Params", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Params", OracleDbType.Varchar2) { Value = model.Params };
            parameterArray1[4] = (model.Ico == null) ? new OracleParameter(":Ico", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Ico", OracleDbType.Varchar2) { Value = model.Ico };
            OracleParameter parameter9 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[5] = parameter9;
            parameterArray1[6] = (model.IcoColor == null) ? new OracleParameter(":IcoColor", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":IcoColor", OracleDbType.Varchar2) { Value = model.IcoColor };
            OracleParameter parameter12 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[7] = parameter12;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public int UpdateSort(Guid id, int sort)
        {
            string sql = "UPDATE Menu SET Sort=:Sort WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[2];
            OracleParameter parameter1 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = sort
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[1] = parameter2;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

