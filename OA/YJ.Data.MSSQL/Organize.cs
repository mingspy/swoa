namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using YJ.Data.Interface;
    using YJ.Data.Model;
    using YJ.Utility;

    public class Organize : IOrganize
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.Organize model)
        {
            string sql = "INSERT INTO Organize\r\n\t\t\t\t(ID,Name,Number,Type,Status,ParentID,Sort,Depth,ChildsLength,ChargeLeader,Leader,Note,IntID) \r\n\t\t\t\tVALUES(@ID,@Name,@Number,@Type,@Status,@ParentID,@Sort,@Depth,@ChildsLength,@ChargeLeader,@Leader,@Note,@IntID)";
            SqlParameter[] parameterArray1 = new SqlParameter[13];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Name", SqlDbType.VarChar, 0x7d0) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Number", SqlDbType.VarChar, 900) {
                Value = model.Number
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@Type", SqlDbType.Int, -1) {
                Value = model.Type
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@Status", SqlDbType.Int, -1) {
                Value = model.Status
            };
            parameterArray1[4] = parameter5;
            SqlParameter parameter6 = new SqlParameter("@ParentID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ParentID
            };
            parameterArray1[5] = parameter6;
            SqlParameter parameter7 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[6] = parameter7;
            SqlParameter parameter8 = new SqlParameter("@Depth", SqlDbType.Int, -1) {
                Value = model.Depth
            };
            parameterArray1[7] = parameter8;
            SqlParameter parameter9 = new SqlParameter("@ChildsLength", SqlDbType.Int, -1) {
                Value = model.ChildsLength
            };
            parameterArray1[8] = parameter9;
            parameterArray1[9] = (model.ChargeLeader == null) ? new SqlParameter("@ChargeLeader", SqlDbType.VarChar, 200) { Value = DBNull.Value } : new SqlParameter("@ChargeLeader", SqlDbType.VarChar, 200) { Value = model.ChargeLeader };
            parameterArray1[10] = (model.Leader == null) ? new SqlParameter("@Leader", SqlDbType.VarChar, 200) { Value = DBNull.Value } : new SqlParameter("@Leader", SqlDbType.VarChar, 200) { Value = model.Leader };
            parameterArray1[11] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.NVarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.NVarChar, -1) { Value = model.Note };
            SqlParameter parameter16 = new SqlParameter("@IntID", SqlDbType.Int, -1) {
                Value = model.IntID
            };
            parameterArray1[12] = parameter16;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.Organize> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.Organize> list = new List<YJ.Data.Model.Organize>();
            YJ.Data.Model.Organize item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.Organize {
                    ID = dataReader.GetGuid(0),
                    Name = dataReader.GetString(1),
                    Number = dataReader.GetString(2),
                    Type = dataReader.GetInt32(3),
                    Status = dataReader.GetInt32(4),
                    ParentID = dataReader.GetGuid(5),
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
            string sql = "DELETE FROM Organize WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.Organize Get(Guid id)
        {
            string sql = "SELECT * FROM Organize WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Organize> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.Organize> GetAll()
        {
            string sql = "SELECT * FROM Organize";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Organize> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.Organize> GetAllChild(string number)
        {
            string sql = "SELECT * FROM Organize WHERE Number LIKE '" + number.ReplaceSql() + "%' ORDER BY Sort";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Organize> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.Organize> GetAllParent(string number)
        {
            string sql = "SELECT * FROM Organize WHERE ID IN(" + Tools.GetSqlInString(number, true, ",") + ") ORDER BY Depth";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Organize> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.Organize> GetChilds(Guid ID)
        {
            string sql = "SELECT * FROM Organize WHERE ParentID=@ParentID ORDER BY Sort";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ParentID", SqlDbType.UniqueIdentifier) {
                Value = ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
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
            string sql = "SELECT ISNULL(MAX(Sort),0)+1 FROM Organize WHERE ParentID=@ParentID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ParentID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.GetFieldValue(sql, parameter).ToInt();
        }

        public YJ.Data.Model.Organize GetRoot()
        {
            string sql = "SELECT * FROM Organize WHERE ParentID=@ParentID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ParentID", SqlDbType.UniqueIdentifier) {
                Value = Guid.Empty
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Organize> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public int Update(YJ.Data.Model.Organize model)
        {
            string sql = "UPDATE Organize SET \r\n\t\t\t\tName=@Name,Number=@Number,Type=@Type,Status=@Status,ParentID=@ParentID,Sort=@Sort,Depth=@Depth,ChildsLength=@ChildsLength,ChargeLeader=@ChargeLeader,Leader=@Leader,Note=@Note,IntID=@IntID \r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[13];
            SqlParameter parameter1 = new SqlParameter("@Name", SqlDbType.VarChar, 0x7d0) {
                Value = model.Name
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Number", SqlDbType.VarChar, 900) {
                Value = model.Number
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Type", SqlDbType.Int, -1) {
                Value = model.Type
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@Status", SqlDbType.Int, -1) {
                Value = model.Status
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@ParentID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ParentID
            };
            parameterArray1[4] = parameter5;
            SqlParameter parameter6 = new SqlParameter("@Sort", SqlDbType.Int, -1) {
                Value = model.Sort
            };
            parameterArray1[5] = parameter6;
            SqlParameter parameter7 = new SqlParameter("@Depth", SqlDbType.Int, -1) {
                Value = model.Depth
            };
            parameterArray1[6] = parameter7;
            SqlParameter parameter8 = new SqlParameter("@ChildsLength", SqlDbType.Int, -1) {
                Value = model.ChildsLength
            };
            parameterArray1[7] = parameter8;
            parameterArray1[8] = (model.ChargeLeader == null) ? new SqlParameter("@ChargeLeader", SqlDbType.VarChar, 200) { Value = DBNull.Value } : new SqlParameter("@ChargeLeader", SqlDbType.VarChar, 200) { Value = model.ChargeLeader };
            parameterArray1[9] = (model.Leader == null) ? new SqlParameter("@Leader", SqlDbType.VarChar, 200) { Value = DBNull.Value } : new SqlParameter("@Leader", SqlDbType.VarChar, 200) { Value = model.Leader };
            parameterArray1[10] = (model.Note == null) ? new SqlParameter("@Note", SqlDbType.NVarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Note", SqlDbType.NVarChar, -1) { Value = model.Note };
            SqlParameter parameter15 = new SqlParameter("@IntID", SqlDbType.Int, -1) {
                Value = model.IntID
            };
            parameterArray1[11] = parameter15;
            SqlParameter parameter16 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[12] = parameter16;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int UpdateChildsLength(Guid id, int length)
        {
            string sql = "UPDATE Organize SET ChildsLength=@ChildsLength WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[2];
            SqlParameter parameter1 = new SqlParameter("@ChildsLength", SqlDbType.Int) {
                Value = length
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[1] = parameter2;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int UpdateSort(Guid id, int sort)
        {
            string sql = "UPDATE Organize SET Sort=@Sort WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[2];
            SqlParameter parameter1 = new SqlParameter("@Sort", SqlDbType.Int) {
                Value = sort
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[1] = parameter2;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

