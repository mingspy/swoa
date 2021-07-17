using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using YJ.Data.Interface;
using YJ.Data.Model;
using YJ.Utility;
namespace YJ.Data.MySql
{


    public class Organize : IOrganize
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.Organize model)
        {
            string sql = "INSERT INTO organize\r\n\t\t\t\t(ID,Name,Number,Type,Status,ParentID,Sort,Depth,ChildsLength,ChargeLeader,Leader,Note,IntID) \r\n\t\t\t\tVALUES(@ID,@Name,@Number,@Type,@Status,@ParentID,@Sort,@Depth,@ChildsLength,@ChargeLeader,@Leader,@Note,@IntID)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[13];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Name", MySqlDbType.Text, -1) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Number", MySqlDbType.Text, -1) {
                Value = model.Number
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@Type", MySqlDbType.Int32, 11) {
                Value = model.Type
            };
            parameterArray1[3] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@Status", MySqlDbType.Int32, 11) {
                Value = model.Status
            };
            parameterArray1[4] = parameter5;
            MySqlParameter parameter6 = new MySqlParameter("@ParentID", MySqlDbType.VarChar, 0x24) {
                Value = model.ParentID
            };
            parameterArray1[5] = parameter6;
            MySqlParameter parameter7 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[6] = parameter7;
            MySqlParameter parameter8 = new MySqlParameter("@Depth", MySqlDbType.Int32, 11) {
                Value = model.Depth
            };
            parameterArray1[7] = parameter8;
            MySqlParameter parameter9 = new MySqlParameter("@ChildsLength", MySqlDbType.Int32, 11) {
                Value = model.ChildsLength
            };
            parameterArray1[8] = parameter9;
            parameterArray1[9] = (model.ChargeLeader == null) ? new MySqlParameter("@ChargeLeader", MySqlDbType.VarChar, 200) { Value = DBNull.Value } : new MySqlParameter("@ChargeLeader", MySqlDbType.VarChar, 200) { Value = model.ChargeLeader };
            parameterArray1[10] = (model.Leader == null) ? new MySqlParameter("@Leader", MySqlDbType.VarChar, 200) { Value = DBNull.Value } : new MySqlParameter("@Leader", MySqlDbType.VarChar, 200) { Value = model.Leader };
            parameterArray1[11] = (model.Note == null) ? new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = model.Note };
            MySqlParameter parameter16 = new MySqlParameter("@IntID", MySqlDbType.Int32, 11) {
                Value = model.IntID
            };
            parameterArray1[12] = parameter16;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.Organize> DataReaderToList(MySqlDataReader dataReader)
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
            string sql = "DELETE FROM organize WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.Organize Get(Guid id)
        {
            string sql = "SELECT * FROM organize WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Organize> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.Organize> GetAll()
        {
            string sql = "SELECT * FROM organize";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Organize> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.Organize> GetAllChild(string number)
        {
            string sql = "SELECT * FROM Organize WHERE Number LIKE '" + number.ReplaceSql() + "%' ORDER BY Sort";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Organize> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.Organize> GetAllParent(string number)
        {
            string sql = "SELECT * FROM Organize WHERE ID IN(" + Tools.GetSqlInString(number, true, ",") + ") ORDER BY Depth";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.Organize> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public List<YJ.Data.Model.Organize> GetChilds(Guid ID)
        {
            string sql = "SELECT * FROM Organize WHERE ParentID=@ParentID ORDER BY Sort";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ParentID", MySqlDbType.VarChar) {
                Value = ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Organize> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM organize";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int GetMaxSort(Guid id)
        {
            string sql = "SELECT IFNULL(MAX(Sort),0)+1 FROM Organize WHERE ParentID=@ParentID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ParentID", MySqlDbType.VarChar) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.GetFieldValue(sql, parameter).ToInt();
        }

        public YJ.Data.Model.Organize GetRoot()
        {
            string sql = "SELECT * FROM Organize WHERE ParentID=@ParentID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ParentID", MySqlDbType.VarChar) {
                Value = Guid.Empty.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.Organize> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public int Update(YJ.Data.Model.Organize model)
        {
            string sql = "UPDATE organize SET \r\n\t\t\t\tName=@Name,Number=@Number,Type=@Type,Status=@Status,ParentID=@ParentID,Sort=@Sort,Depth=@Depth,ChildsLength=@ChildsLength,ChargeLeader=@ChargeLeader,Leader=@Leader,Note=@Note,IntID=@IntID \r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[13];
            MySqlParameter parameter1 = new MySqlParameter("@Name", MySqlDbType.Text, -1) {
                Value = model.Name
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Number", MySqlDbType.Text, -1) {
                Value = model.Number
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Type", MySqlDbType.Int32, 11) {
                Value = model.Type
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@Status", MySqlDbType.Int32, 11) {
                Value = model.Status
            };
            parameterArray1[3] = parameter4;
            MySqlParameter parameter5 = new MySqlParameter("@ParentID", MySqlDbType.VarChar, 0x24) {
                Value = model.ParentID
            };
            parameterArray1[4] = parameter5;
            MySqlParameter parameter6 = new MySqlParameter("@Sort", MySqlDbType.Int32, 11) {
                Value = model.Sort
            };
            parameterArray1[5] = parameter6;
            MySqlParameter parameter7 = new MySqlParameter("@Depth", MySqlDbType.Int32, 11) {
                Value = model.Depth
            };
            parameterArray1[6] = parameter7;
            MySqlParameter parameter8 = new MySqlParameter("@ChildsLength", MySqlDbType.Int32, 11) {
                Value = model.ChildsLength
            };
            parameterArray1[7] = parameter8;
            parameterArray1[8] = (model.ChargeLeader == null) ? new MySqlParameter("@ChargeLeader", MySqlDbType.VarChar, 200) { Value = DBNull.Value } : new MySqlParameter("@ChargeLeader", MySqlDbType.VarChar, 200) { Value = model.ChargeLeader };
            parameterArray1[9] = (model.Leader == null) ? new MySqlParameter("@Leader", MySqlDbType.VarChar, 200) { Value = DBNull.Value } : new MySqlParameter("@Leader", MySqlDbType.VarChar, 200) { Value = model.Leader };
            parameterArray1[10] = (model.Note == null) ? new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@Note", MySqlDbType.LongText, -1) { Value = model.Note };
            MySqlParameter parameter15 = new MySqlParameter("@IntID", MySqlDbType.Int32, 11) {
                Value = model.IntID
            };
            parameterArray1[11] = parameter15;
            MySqlParameter parameter16 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[12] = parameter16;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int UpdateChildsLength(Guid id, int length)
        {
            string sql = "UPDATE Organize SET ChildsLength=@ChildsLength WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[2];
            MySqlParameter parameter1 = new MySqlParameter("@ChildsLength", MySqlDbType.Int32) {
                Value = length
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@ID", MySqlDbType.VarChar) {
                Value = id
            };
            parameterArray1[1] = parameter2;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int UpdateSort(Guid id, int sort)
        {
            string sql = "UPDATE Organize SET Sort=@Sort WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[2];
            MySqlParameter parameter1 = new MySqlParameter("@Sort", MySqlDbType.Int32) {
                Value = sort
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@ID", MySqlDbType.VarChar) {
                Value = id
            };
            parameterArray1[1] = parameter2;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

