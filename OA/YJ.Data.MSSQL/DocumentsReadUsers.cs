namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class DocumentsReadUsers : IDocumentsReadUsers
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.DocumentsReadUsers model)
        {
            string sql = "INSERT INTO DocumentsReadUsers\r\n\t\t\t\t(DocumentID,UserID,IsRead) \r\n\t\t\t\tVALUES(@DocumentID,@UserID,@IsRead)";
            SqlParameter[] parameterArray1 = new SqlParameter[3];
            SqlParameter parameter1 = new SqlParameter("@DocumentID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.DocumentID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.UserID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@IsRead", SqlDbType.Int, -1) {
                Value = model.IsRead
            };
            parameterArray1[2] = parameter3;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.DocumentsReadUsers> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.DocumentsReadUsers> list = new List<YJ.Data.Model.DocumentsReadUsers>();
            YJ.Data.Model.DocumentsReadUsers item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.DocumentsReadUsers {
                    DocumentID = dataReader.GetGuid(0),
                    UserID = dataReader.GetGuid(1),
                    IsRead = dataReader.GetInt32(2)
                };
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid documentid)
        {
            string sql = "DELETE FROM DocumentsReadUsers WHERE DocumentID=@DocumentID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@DocumentID", SqlDbType.UniqueIdentifier) {
                Value = documentid
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int Delete(Guid documentid, Guid userid)
        {
            string sql = "DELETE FROM DocumentsReadUsers WHERE DocumentID=@DocumentID AND UserID=@UserID";
            SqlParameter[] parameterArray1 = new SqlParameter[2];
            SqlParameter parameter1 = new SqlParameter("@DocumentID", SqlDbType.UniqueIdentifier) {
                Value = documentid
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier) {
                Value = userid
            };
            parameterArray1[1] = parameter2;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.DocumentsReadUsers Get(Guid documentid, Guid userid)
        {
            string sql = "SELECT * FROM DocumentsReadUsers WHERE DocumentID=@DocumentID AND UserID=@UserID";
            SqlParameter[] parameterArray1 = new SqlParameter[2];
            SqlParameter parameter1 = new SqlParameter("@DocumentID", SqlDbType.UniqueIdentifier) {
                Value = documentid
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier) {
                Value = userid
            };
            parameterArray1[1] = parameter2;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.DocumentsReadUsers> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.DocumentsReadUsers> GetAll()
        {
            string sql = "SELECT * FROM DocumentsReadUsers";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.DocumentsReadUsers> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM DocumentsReadUsers";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.DocumentsReadUsers model)
        {
            string sql = "UPDATE DocumentsReadUsers SET \r\n\t\t\t\tIsRead=@IsRead\r\n\t\t\t\tWHERE DocumentID=@DocumentID and UserID=@UserID";
            SqlParameter[] parameterArray1 = new SqlParameter[3];
            SqlParameter parameter1 = new SqlParameter("@IsRead", SqlDbType.Int, -1) {
                Value = model.IsRead
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@DocumentID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.DocumentID
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.UserID
            };
            parameterArray1[2] = parameter3;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public void UpdateRead(Guid docID, Guid userID)
        {
            string sql = "UPDATE DocumentsReadUsers SET IsRead=1 WHERE DocumentID=@DocumentID AND UserID=@UserID";
            SqlParameter[] parameterArray1 = new SqlParameter[2];
            SqlParameter parameter1 = new SqlParameter("@DocumentID", SqlDbType.UniqueIdentifier) {
                Value = docID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier) {
                Value = userID
            };
            parameterArray1[1] = parameter2;
            SqlParameter[] parameter = parameterArray1;
            this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

