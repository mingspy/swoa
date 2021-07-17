namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using YJ.Data.Interface;
    using YJ.Data.Model;

    public class DocumentsReadUsers : IDocumentsReadUsers
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.DocumentsReadUsers model)
        {
            string sql = "INSERT INTO DocumentsReadUsers\r\n\t\t\t\t(DocumentID,UserID,IsRead) \r\n\t\t\t\tVALUES(:DocumentID,:UserID,:IsRead)";
            OracleParameter[] parameterArray1 = new OracleParameter[3];
            OracleParameter parameter1 = new OracleParameter(":DocumentID", OracleDbType.Varchar2) {
                Value = model.DocumentID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":UserID", OracleDbType.Varchar2) {
                Value = model.UserID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":IsRead", OracleDbType.Int32) {
                Value = model.IsRead
            };
            parameterArray1[2] = parameter3;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.DocumentsReadUsers> DataReaderToList(OracleDataReader dataReader)
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
            string sql = "DELETE FROM DocumentsReadUsers WHERE DocumentID=:DocumentID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":DocumentID", OracleDbType.Varchar2) {
                Value = documentid
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public int Delete(Guid documentid, Guid userid)
        {
            string sql = "DELETE FROM DocumentsReadUsers WHERE DocumentID=:DocumentID AND UserID=:UserID";
            OracleParameter[] parameterArray1 = new OracleParameter[2];
            OracleParameter parameter1 = new OracleParameter(":DocumentID", OracleDbType.Varchar2) {
                Value = documentid
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":UserID", OracleDbType.Varchar2) {
                Value = userid
            };
            parameterArray1[1] = parameter2;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.DocumentsReadUsers Get(Guid documentid, Guid userid)
        {
            string sql = "SELECT * FROM DocumentsReadUsers WHERE DocumentID=:DocumentID AND UserID=:UserID";
            OracleParameter[] parameterArray1 = new OracleParameter[2];
            OracleParameter parameter1 = new OracleParameter(":DocumentID", OracleDbType.Varchar2) {
                Value = documentid
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":UserID", OracleDbType.Varchar2) {
                Value = userid
            };
            parameterArray1[1] = parameter2;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.DocumentsReadUsers> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.DocumentsReadUsers> GetAll()
        {
            string sql = "SELECT * FROM DocumentsReadUsers";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
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
            string sql = "UPDATE DocumentsReadUsers SET \r\n\t\t\t\tIsRead=:IsRead\r\n\t\t\t\tWHERE DocumentID=:DocumentID and UserID=:UserID";
            OracleParameter[] parameterArray1 = new OracleParameter[3];
            OracleParameter parameter1 = new OracleParameter(":IsRead", OracleDbType.Int32) {
                Value = model.IsRead
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":DocumentID", OracleDbType.Varchar2) {
                Value = model.DocumentID
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":UserID", OracleDbType.Varchar2) {
                Value = model.UserID
            };
            parameterArray1[2] = parameter3;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public void UpdateRead(Guid docID, Guid userID)
        {
            string sql = "UPDATE DocumentsReadUsers SET IsRead=1 WHERE DocumentID=:DocumentID AND UserID=:UserID";
            OracleParameter[] parameterArray1 = new OracleParameter[2];
            OracleParameter parameter1 = new OracleParameter(":DocumentID", OracleDbType.Varchar2) {
                Value = docID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":UserID", OracleDbType.Varchar2) {
                Value = userID
            };
            parameterArray1[1] = parameter2;
            OracleParameter[] parameter = parameterArray1;
            this.dbHelper.Execute(sql, parameter);
        }
    }
}

