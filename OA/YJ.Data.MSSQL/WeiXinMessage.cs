namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class WeiXinMessage : IWeiXinMessage
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.WeiXinMessage model)
        {
            string sql = "INSERT INTO WeiXinMessage\r\n\t\t\t\t(ID,ToUserName,FromUserName,CreateTime,CreateTime1,MsgType,MsgId,AgentID,Contents,PicUrl,MediaId,Format,ThumbMediaId,Location_X,Location_Y,Scale,Label,Title,Description,AddTime) \r\n\t\t\t\tVALUES(@ID,@ToUserName,@FromUserName,@CreateTime,@CreateTime1,@MsgType,@MsgId,@AgentID,@Contents,@PicUrl,@MediaId,@Format,@ThumbMediaId,@Location_X,@Location_Y,@Scale,@Label,@Title,@Description,@AddTime)";
            SqlParameter[] parameterArray1 = new SqlParameter[20];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@ToUserName", SqlDbType.VarChar, 200) {
                Value = model.ToUserName
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@FromUserName", SqlDbType.VarChar, 200) {
                Value = model.FromUserName
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@CreateTime", SqlDbType.Int, -1) {
                Value = model.CreateTime
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@CreateTime1", SqlDbType.DateTime, 8) {
                Value = model.CreateTime1
            };
            parameterArray1[4] = parameter5;
            SqlParameter parameter6 = new SqlParameter("@MsgType", SqlDbType.VarChar, 50) {
                Value = model.MsgType
            };
            parameterArray1[5] = parameter6;
            SqlParameter parameter7 = new SqlParameter("@MsgId", SqlDbType.BigInt, -1) {
                Value = model.MsgId
            };
            parameterArray1[6] = parameter7;
            SqlParameter parameter8 = new SqlParameter("@AgentID", SqlDbType.Int, -1) {
                Value = model.AgentID
            };
            parameterArray1[7] = parameter8;
            parameterArray1[8] = (model.Contents == null) ? new SqlParameter("@Contents", SqlDbType.NVarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Contents", SqlDbType.NVarChar, -1) { Value = model.Contents };
            parameterArray1[9] = (model.PicUrl == null) ? new SqlParameter("@PicUrl", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@PicUrl", SqlDbType.VarChar, 500) { Value = model.PicUrl };
            parameterArray1[10] = (model.MediaId == null) ? new SqlParameter("@MediaId", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@MediaId", SqlDbType.VarChar, 500) { Value = model.MediaId };
            parameterArray1[11] = (model.Format == null) ? new SqlParameter("@Format", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Format", SqlDbType.VarChar, 50) { Value = model.Format };
            parameterArray1[12] = (model.ThumbMediaId == null) ? new SqlParameter("@ThumbMediaId", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@ThumbMediaId", SqlDbType.VarChar, 50) { Value = model.ThumbMediaId };
            parameterArray1[13] = (model.Location_X == null) ? new SqlParameter("@Location_X", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Location_X", SqlDbType.VarChar, 50) { Value = model.Location_X };
            parameterArray1[14] = (model.Location_Y == null) ? new SqlParameter("@Location_Y", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Location_Y", SqlDbType.VarChar, 50) { Value = model.Location_Y };
            parameterArray1[15] = (model.Scale == null) ? new SqlParameter("@Scale", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Scale", SqlDbType.VarChar, 50) { Value = model.Scale };
            parameterArray1[0x10] = (model.Label == null) ? new SqlParameter("@Label", SqlDbType.NVarChar, 0x3e8) { Value = DBNull.Value } : new SqlParameter("@Label", SqlDbType.NVarChar, 0x3e8) { Value = model.Label };
            parameterArray1[0x11] = (model.Title == null) ? new SqlParameter("@Title", SqlDbType.NVarChar, 0x3e8) { Value = DBNull.Value } : new SqlParameter("@Title", SqlDbType.NVarChar, 0x3e8) { Value = model.Title };
            parameterArray1[0x12] = (model.Description == null) ? new SqlParameter("@Description", SqlDbType.NVarChar, 0x7d0) { Value = DBNull.Value } : new SqlParameter("@Description", SqlDbType.NVarChar, 0x7d0) { Value = model.Description };
            SqlParameter parameter31 = new SqlParameter("@AddTime", SqlDbType.DateTime, 8) {
                Value = model.AddTime
            };
            parameterArray1[0x13] = parameter31;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.WeiXinMessage> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.WeiXinMessage> list = new List<YJ.Data.Model.WeiXinMessage>();
            YJ.Data.Model.WeiXinMessage item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.WeiXinMessage {
                    ID = dataReader.GetGuid(0),
                    ToUserName = dataReader.GetString(1),
                    CreateTime = dataReader.GetInt32(3),
                    CreateTime1 = dataReader.GetDateTime(4),
                    MsgType = dataReader.GetString(5),
                    MsgId = dataReader.GetInt64(6),
                    AgentID = dataReader.GetInt32(7)
                };
                if (!dataReader.IsDBNull(8))
                {
                    item.Contents = dataReader.GetString(8);
                }
                if (!dataReader.IsDBNull(9))
                {
                    item.PicUrl = dataReader.GetString(9);
                }
                if (!dataReader.IsDBNull(10))
                {
                    item.MediaId = dataReader.GetString(10);
                }
                if (!dataReader.IsDBNull(11))
                {
                    item.Format = dataReader.GetString(11);
                }
                if (!dataReader.IsDBNull(12))
                {
                    item.ThumbMediaId = dataReader.GetString(12);
                }
                if (!dataReader.IsDBNull(13))
                {
                    item.Location_X = dataReader.GetString(13);
                }
                if (!dataReader.IsDBNull(14))
                {
                    item.Location_Y = dataReader.GetString(14);
                }
                if (!dataReader.IsDBNull(15))
                {
                    item.Scale = dataReader.GetString(15);
                }
                if (!dataReader.IsDBNull(0x10))
                {
                    item.Label = dataReader.GetString(0x10);
                }
                if (!dataReader.IsDBNull(0x11))
                {
                    item.Title = dataReader.GetString(0x11);
                }
                if (!dataReader.IsDBNull(0x12))
                {
                    item.Description = dataReader.GetString(0x12);
                }
                item.AddTime = dataReader.GetDateTime(0x13);
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM WeiXinMessage WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.WeiXinMessage Get(Guid id)
        {
            string sql = "SELECT * FROM WeiXinMessage WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.WeiXinMessage> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.WeiXinMessage> GetAll()
        {
            string sql = "SELECT * FROM WeiXinMessage";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.WeiXinMessage> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM WeiXinMessage";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.WeiXinMessage model)
        {
            string sql = "UPDATE WeiXinMessage SET \r\n\t\t\t\tToUserName=@ToUserName,FromUserName=@FromUserName,CreateTime=@CreateTime,CreateTime1=@CreateTime1,MsgType=@MsgType,MsgId=@MsgId,AgentID=@AgentID,Contents=@Contents,PicUrl=@PicUrl,MediaId=@MediaId,Format=@Format,ThumbMediaId=@ThumbMediaId,Location_X=@Location_X,Location_Y=@Location_Y,Scale=@Scale,Label=@Label,Title=@Title,Description=@Description,AddTime=@AddTime\r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[20];
            SqlParameter parameter1 = new SqlParameter("@ToUserName", SqlDbType.VarChar, 200) {
                Value = model.ToUserName
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@FromUserName", SqlDbType.VarChar, 200) {
                Value = model.FromUserName
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@CreateTime", SqlDbType.Int, -1) {
                Value = model.CreateTime
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@CreateTime1", SqlDbType.DateTime, 8) {
                Value = model.CreateTime1
            };
            parameterArray1[3] = parameter4;
            SqlParameter parameter5 = new SqlParameter("@MsgType", SqlDbType.VarChar, 50) {
                Value = model.MsgType
            };
            parameterArray1[4] = parameter5;
            SqlParameter parameter6 = new SqlParameter("@MsgId", SqlDbType.BigInt, -1) {
                Value = model.MsgId
            };
            parameterArray1[5] = parameter6;
            SqlParameter parameter7 = new SqlParameter("@AgentID", SqlDbType.Int, -1) {
                Value = model.AgentID
            };
            parameterArray1[6] = parameter7;
            parameterArray1[7] = (model.Contents == null) ? new SqlParameter("@Contents", SqlDbType.NVarChar, -1) { Value = DBNull.Value } : new SqlParameter("@Contents", SqlDbType.NVarChar, -1) { Value = model.Contents };
            parameterArray1[8] = (model.PicUrl == null) ? new SqlParameter("@PicUrl", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@PicUrl", SqlDbType.VarChar, 500) { Value = model.PicUrl };
            parameterArray1[9] = (model.MediaId == null) ? new SqlParameter("@MediaId", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@MediaId", SqlDbType.VarChar, 500) { Value = model.MediaId };
            parameterArray1[10] = (model.Format == null) ? new SqlParameter("@Format", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Format", SqlDbType.VarChar, 50) { Value = model.Format };
            parameterArray1[11] = (model.ThumbMediaId == null) ? new SqlParameter("@ThumbMediaId", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@ThumbMediaId", SqlDbType.VarChar, 50) { Value = model.ThumbMediaId };
            parameterArray1[12] = (model.Location_X == null) ? new SqlParameter("@Location_X", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Location_X", SqlDbType.VarChar, 50) { Value = model.Location_X };
            parameterArray1[13] = (model.Location_Y == null) ? new SqlParameter("@Location_Y", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Location_Y", SqlDbType.VarChar, 50) { Value = model.Location_Y };
            parameterArray1[14] = (model.Scale == null) ? new SqlParameter("@Scale", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Scale", SqlDbType.VarChar, 50) { Value = model.Scale };
            parameterArray1[15] = (model.Label == null) ? new SqlParameter("@Label", SqlDbType.NVarChar, 0x3e8) { Value = DBNull.Value } : new SqlParameter("@Label", SqlDbType.NVarChar, 0x3e8) { Value = model.Label };
            parameterArray1[0x10] = (model.Title == null) ? new SqlParameter("@Title", SqlDbType.NVarChar, 0x3e8) { Value = DBNull.Value } : new SqlParameter("@Title", SqlDbType.NVarChar, 0x3e8) { Value = model.Title };
            parameterArray1[0x11] = (model.Description == null) ? new SqlParameter("@Description", SqlDbType.NVarChar, 0x7d0) { Value = DBNull.Value } : new SqlParameter("@Description", SqlDbType.NVarChar, 0x7d0) { Value = model.Description };
            SqlParameter parameter30 = new SqlParameter("@AddTime", SqlDbType.DateTime, 8) {
                Value = model.AddTime
            };
            parameterArray1[0x12] = parameter30;
            SqlParameter parameter31 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0x13] = parameter31;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

