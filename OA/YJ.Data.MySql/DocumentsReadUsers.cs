using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using YJ.Data.Interface;
using YJ.Data.Model;
namespace YJ.Data.MySql
{


    public class DocumentsReadUsers : IDocumentsReadUsers
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.DocumentsReadUsers model)
        {
            string sql = "INSERT INTO documentsreadusers\r\n\t\t\t\t(DocumentID,UserID,IsRead) \r\n\t\t\t\tVALUES(@DocumentID,@UserID,@IsRead)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[3];
            MySqlParameter parameter1 = new MySqlParameter("@DocumentID", MySqlDbType.VarChar, 0x24) {
                Value = model.DocumentID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@UserID", MySqlDbType.VarChar, 0x24) {
                Value = model.UserID
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@IsRead", MySqlDbType.Int32, 11) {
                Value = model.IsRead
            };
            parameterArray1[2] = parameter3;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.DocumentsReadUsers> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.DocumentsReadUsers> list = new List<YJ.Data.Model.DocumentsReadUsers>();
            YJ.Data.Model.DocumentsReadUsers item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.DocumentsReadUsers {
                    DocumentID = dataReader.GetString(0).ToGuid(),
                    UserID = dataReader.GetString(1).ToGuid(),
                    IsRead = dataReader.GetInt32(2)
                };
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid documentid)
        {
            string sql = "DELETE FROM DocumentsReadUsers WHERE DocumentID=@DocumentID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@DocumentID", MySqlDbType.VarChar) {
                Value = documentid
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public int Delete(Guid documentid, Guid userid)
        {
            string sql = "DELETE FROM documentsreadusers WHERE DocumentID=@DocumentID AND UserID=@UserID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[2];
            MySqlParameter parameter1 = new MySqlParameter("@DocumentID", MySqlDbType.VarChar, 0x24) {
                Value = documentid.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@UserID", MySqlDbType.VarChar, 0x24) {
                Value = userid.ToString()
            };
            parameterArray1[1] = parameter2;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.DocumentsReadUsers Get(Guid documentid, Guid userid)
        {
            string sql = "SELECT * FROM documentsreadusers WHERE DocumentID=@DocumentID AND UserID=@UserID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[2];
            MySqlParameter parameter1 = new MySqlParameter("@DocumentID", MySqlDbType.VarChar, 0x24) {
                Value = documentid.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@UserID", MySqlDbType.VarChar, 0x24) {
                Value = userid.ToString()
            };
            parameterArray1[1] = parameter2;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.DocumentsReadUsers> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.DocumentsReadUsers> GetAll()
        {
            string sql = "SELECT * FROM documentsreadusers";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.DocumentsReadUsers> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM documentsreadusers";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public int Update(YJ.Data.Model.DocumentsReadUsers model)
        {
            string sql = "UPDATE documentsreadusers SET \r\n\t\t\t\tIsRead=@IsRead\r\n\t\t\t\tWHERE DocumentID=@DocumentID and UserID=@UserID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[3];
            MySqlParameter parameter1 = new MySqlParameter("@IsRead", MySqlDbType.Int32, 11) {
                Value = model.IsRead
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@DocumentID", MySqlDbType.VarChar, 0x24) {
                Value = model.DocumentID
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@UserID", MySqlDbType.VarChar, 0x24) {
                Value = model.UserID
            };
            parameterArray1[2] = parameter3;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public void UpdateRead(Guid docID, Guid userID)
        {
            string sql = "UPDATE DocumentsReadUsers SET IsRead=1 WHERE DocumentID=@DocumentID AND UserID=@UserID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[2];
            MySqlParameter parameter1 = new MySqlParameter("@DocumentID", MySqlDbType.VarChar) {
                Value = docID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@UserID", MySqlDbType.VarChar) {
                Value = userID
            };
            parameterArray1[1] = parameter2;
            MySqlParameter[] parameter = parameterArray1;
            this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

