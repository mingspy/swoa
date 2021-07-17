namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using YJ.Data.Interface;
    using YJ.Data.Model;
    using YJ.Utility;

    public class Organize : IOrganize
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.Organize model)
        {
            string sql = "INSERT INTO Organize\r\n\t\t\t\t(ID,Name,Number1,Type,Status,ParentID,Sort,Depth,ChildsLength,ChargeLeader,Leader,Note,IntID) \r\n\t\t\t\tVALUES(:ID,:Name,:Number1,:Type,:Status,:ParentID,:Sort,:Depth,:ChildsLength,:ChargeLeader,:Leader,:Note,:IntID)";
            OracleParameter[] parameterArray1 = new OracleParameter[13];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2, 40) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Name", OracleDbType.Varchar2, 0x7d0) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":Number1", OracleDbType.Varchar2, 900) {
                Value = model.Number
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":Type", OracleDbType.Int32) {
                Value = model.Type
            };
            parameterArray1[3] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":Status", OracleDbType.Int32) {
                Value = model.Status
            };
            parameterArray1[4] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":ParentID", OracleDbType.Varchar2, 40) {
                Value = model.ParentID
            };
            parameterArray1[5] = parameter6;
            OracleParameter parameter7 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[6] = parameter7;
            OracleParameter parameter8 = new OracleParameter(":Depth", OracleDbType.Int32) {
                Value = model.Depth
            };
            parameterArray1[7] = parameter8;
            OracleParameter parameter9 = new OracleParameter(":ChildsLength", OracleDbType.Int32) {
                Value = model.ChildsLength
            };
            parameterArray1[8] = parameter9;
            parameterArray1[9] = (model.ChargeLeader == null) ? new OracleParameter(":ChargeLeader", OracleDbType.Varchar2, 200) { Value = DBNull.Value } : new OracleParameter(":ChargeLeader", OracleDbType.Varchar2, 200) { Value = model.ChargeLeader };
            parameterArray1[10] = (model.Leader == null) ? new OracleParameter(":Leader", OracleDbType.Varchar2, 200) { Value = DBNull.Value } : new OracleParameter(":Leader", OracleDbType.Varchar2, 200) { Value = model.Leader };
            parameterArray1[11] = (model.Note == null) ? new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = model.Note };
            OracleParameter parameter16 = new OracleParameter(":IntID", OracleDbType.Int32) {
                Value = model.IntID
            };
            parameterArray1[12] = parameter16;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.Organize> DataReaderToList(OracleDataReader dataReader)
        {
            List<YJ.Data.Model.Organize> list = new List<YJ.Data.Model.Organize>();
            YJ.Data.Model.Organize item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.Organize {
                    ID = dataReader.GetString(0).ToGuid(),
                    Name = dataReader.GetString(1),
                    Number = dataReader.GetString(2),
                    Type = dataReader.GetInt32(3),
                    Status = dataReader.GetInt32(4),
                    ParentID = dataReader.GetString(5).ToGuid(),
                    Sort = dataReader.GetInt32(6),
                    Depth = dataReader.GetInt32(7),
                    ChildsLength = dataReader.GetInt32(8)
                };
                if (!dataReader.IsDBNull(9))
                {
                    item.ChargeLeader = dataReader.GetString(9);
                }
                if (!dataReader.IsDBNull(10))
                {
                    item.Leader = dataReader.GetString(10);
                }
                if (!dataReader.IsDBNull(11))
                {
                    item.Note = dataReader.GetString(11);
                }
                item.IntID = dataReader.GetInt32(12);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM Organize WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.Organize Get(Guid id)
        {
            string sql = "SELECT * FROM Organize WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Organize> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.Organize> GetAll()
        {
            string sql = "SELECT * FROM Organize";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Organize> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.Organize> GetAllChild(string number)
        {
            string sql = "SELECT * FROM Organize WHERE NUMBER1 LIKE '" + number.ReplaceSql() + "%' ORDER BY Sort";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Organize> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.Organize> GetAllParent(string number)
        {
            string sql = "SELECT * FROM Organize WHERE ID IN(" + Tools.GetSqlInString(number, true, ",") + ") ORDER BY Depth";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Organize> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.Organize> GetChilds(Guid ID)
        {
            string sql = "SELECT * FROM Organize WHERE ParentID=:ParentID ORDER BY Sort";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ParentID", OracleDbType.Varchar2) {
                Value = ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Organize> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM Organize";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int GetMaxSort(Guid id)
        {
            string sql = "SELECT nvl(MAX(Sort),0)+1 FROM Organize WHERE ParentID=:ParentID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ParentID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.GetFieldValue(sql, parameter).ToInt();
        }

        public YJ.Data.Model.Organize GetRoot()
        {
            string sql = "SELECT * FROM Organize WHERE ParentID=:ParentID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ParentID", OracleDbType.Varchar2) {
                Value = Guid.Empty
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Organize> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public int Update(YJ.Data.Model.Organize model)
        {
            string sql = "UPDATE Organize SET \r\n\t\t\t\tName=:Name,Number1=:Number1,Type=:Type,Status=:Status,ParentID=:ParentID,Sort=:Sort,Depth=:Depth,ChildsLength=:ChildsLength,ChargeLeader=:ChargeLeader,Leader=:Leader,Note=:Note,IntID=:IntID\r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[13];
            OracleParameter parameter1 = new OracleParameter(":Name", OracleDbType.Varchar2, 0x7d0) {
                Value = model.Name
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Number1", OracleDbType.Varchar2, 900) {
                Value = model.Number
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":Type", OracleDbType.Int32) {
                Value = model.Type
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":Status", OracleDbType.Int32) {
                Value = model.Status
            };
            parameterArray1[3] = parameter4;
            OracleParameter parameter5 = new OracleParameter(":ParentID", OracleDbType.Varchar2, 40) {
                Value = model.ParentID
            };
            parameterArray1[4] = parameter5;
            OracleParameter parameter6 = new OracleParameter(":Sort", OracleDbType.Int32) {
                Value = model.Sort
            };
            parameterArray1[5] = parameter6;
            OracleParameter parameter7 = new OracleParameter(":Depth", OracleDbType.Int32) {
                Value = model.Depth
            };
            parameterArray1[6] = parameter7;
            OracleParameter parameter8 = new OracleParameter(":ChildsLength", OracleDbType.Int32) {
                Value = model.ChildsLength
            };
            parameterArray1[7] = parameter8;
            parameterArray1[8] = (model.ChargeLeader == null) ? new OracleParameter(":ChargeLeader", OracleDbType.Varchar2, 200) { Value = DBNull.Value } : new OracleParameter(":ChargeLeader", OracleDbType.Varchar2, 200) { Value = model.ChargeLeader };
            parameterArray1[9] = (model.Leader == null) ? new OracleParameter(":Leader", OracleDbType.Varchar2, 200) { Value = DBNull.Value } : new OracleParameter(":Leader", OracleDbType.Varchar2, 200) { Value = model.Leader };
            parameterArray1[10] = (model.Note == null) ? new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = DBNull.Value } : new OracleParameter(":Note", OracleDbType.NVarchar2) { Value = model.Note };
            OracleParameter parameter15 = new OracleParameter(":IntID", OracleDbType.Int32) {
                Value = model.IntID
            };
            parameterArray1[11] = parameter15;
            OracleParameter parameter16 = new OracleParameter(":ID", OracleDbType.Varchar2, 40) {
                Value = model.ID
            };
            parameterArray1[12] = parameter16;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public int UpdateChildsLength(Guid id, int length)
        {
            string sql = "UPDATE Organize SET ChildsLength=:ChildsLength WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[2];
            OracleParameter parameter1 = new OracleParameter(":ChildsLength", OracleDbType.Int32) {
                Value = length
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[1] = parameter2;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public int UpdateSort(Guid id, int sort)
        {
            string sql = "UPDATE Organize SET Sort=:Sort WHERE ID=:ID";
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

