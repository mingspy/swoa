namespace YJ.Data.MSSQL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;
    using YJ.Data.Interface;
    using YJ.Data.Model;
    using YJ.Utility;

    public class ProgramBuilder : IProgramBuilder
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.ProgramBuilder model)
        {
            string sql = "INSERT INTO ProgramBuilder\r\n\t\t\t\t(ID,Name,Type,CreateTime,PublishTime,CreateUser,SQL,IsAdd,DBConnID,Status,FormID,EditModel,Width,Height,ButtonLocation,IsPager,ClientScript,ExportTemplate,ExportHeaderText,ExportFileName,TableStyle,TableHead,TableName,InDataNumberFiledName,InDataFiledName) \r\n\t\t\t\tVALUES(@ID,@Name,@Type,@CreateTime,@PublishTime,@CreateUser,@SQL,@IsAdd,@DBConnID,@Status,@FormID,@EditModel,@Width,@Height,@ButtonLocation,@IsPager,@ClientScript,@ExportTemplate,@ExportHeaderText,@ExportFileName,@TableStyle,@TableHead,@TableName,@InDataNumberFiledName,@InDataFiledName)";
            SqlParameter[] parameterArray1 = new SqlParameter[0x19];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Name", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@Type", SqlDbType.UniqueIdentifier, -1) {
                Value = model.Type
            };
            parameterArray1[2] = parameter3;
            SqlParameter parameter4 = new SqlParameter("@CreateTime", SqlDbType.DateTime, 8) {
                Value = model.CreateTime
            };
            parameterArray1[3] = parameter4;
            parameterArray1[4] = !model.PublishTime.HasValue ? new SqlParameter("@PublishTime", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@PublishTime", SqlDbType.DateTime, 8) { Value = model.PublishTime };
            SqlParameter parameter7 = new SqlParameter("@CreateUser", SqlDbType.UniqueIdentifier, -1) {
                Value = model.CreateUser
            };
            parameterArray1[5] = parameter7;
            SqlParameter parameter8 = new SqlParameter("@SQL", SqlDbType.VarChar, -1) {
                Value = model.SQL
            };
            parameterArray1[6] = parameter8;
            SqlParameter parameter9 = new SqlParameter("@IsAdd", SqlDbType.Int, -1) {
                Value = model.IsAdd
            };
            parameterArray1[7] = parameter9;
            SqlParameter parameter10 = new SqlParameter("@DBConnID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.DBConnID
            };
            parameterArray1[8] = parameter10;
            SqlParameter parameter11 = new SqlParameter("@Status", SqlDbType.Int, -1) {
                Value = model.Status
            };
            parameterArray1[9] = parameter11;
            parameterArray1[10] = (model.FormID == null) ? new SqlParameter("@FormID", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@FormID", SqlDbType.VarChar, 500) { Value = model.FormID };
            parameterArray1[11] = !model.EditModel.HasValue ? new SqlParameter("@EditModel", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@EditModel", SqlDbType.Int, -1) { Value = model.EditModel };
            parameterArray1[12] = (model.Width == null) ? new SqlParameter("@Width", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Width", SqlDbType.VarChar, 50) { Value = model.Width };
            parameterArray1[13] = (model.Height == null) ? new SqlParameter("@Height", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Height", SqlDbType.VarChar, 50) { Value = model.Height };
            parameterArray1[14] = !model.ButtonLocation.HasValue ? new SqlParameter("@ButtonLocation", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@ButtonLocation", SqlDbType.Int, -1) { Value = model.ButtonLocation };
            parameterArray1[15] = !model.IsPager.HasValue ? new SqlParameter("@IsPager", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@IsPager", SqlDbType.Int, -1) { Value = model.IsPager };
            parameterArray1[0x10] = (model.ClientScript == null) ? new SqlParameter("@ClientScript", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@ClientScript", SqlDbType.VarChar, -1) { Value = model.ClientScript };
            parameterArray1[0x11] = (model.ExportTemplate == null) ? new SqlParameter("@ExportTemplate", SqlDbType.VarChar, 0xfa0) { Value = DBNull.Value } : new SqlParameter("@ExportTemplate", SqlDbType.VarChar, 0xfa0) { Value = model.ExportTemplate };
            parameterArray1[0x12] = (model.ExportHeaderText == null) ? new SqlParameter("@ExportHeaderText", SqlDbType.NVarChar, 0x3e8) { Value = DBNull.Value } : new SqlParameter("@ExportHeaderText", SqlDbType.NVarChar, 0x3e8) { Value = model.ExportHeaderText };
            parameterArray1[0x13] = (model.ExportFileName == null) ? new SqlParameter("@ExportFileName", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@ExportFileName", SqlDbType.VarChar, 500) { Value = model.ExportFileName };
            parameterArray1[20] = (model.TableStyle == null) ? new SqlParameter("@TableStyle", SqlDbType.VarChar, 0xff) { Value = DBNull.Value } : new SqlParameter("@TableStyle", SqlDbType.VarChar, 0xff) { Value = model.TableStyle };
            parameterArray1[0x15] = (model.TableHead == null) ? new SqlParameter("@TableHead", SqlDbType.VarChar, 0xff) { Value = DBNull.Value } : new SqlParameter("@TableHead", SqlDbType.VarChar, 0xff) { Value = model.TableHead };
            parameterArray1[0x16] = (model.TableName == null) ? new SqlParameter("@TableName", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@TableName", SqlDbType.VarChar, 500) { Value = model.TableName };
            parameterArray1[0x17] = (model.InDataNumberFiledName == null) ? new SqlParameter("@InDataNumberFiledName", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@InDataNumberFiledName", SqlDbType.VarChar, 500) { Value = model.InDataNumberFiledName };
            parameterArray1[0x18] = (model.InDataFiledName == null) ? new SqlParameter("@InDataFiledName", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@InDataFiledName", SqlDbType.VarChar, 500) { Value = model.InDataFiledName };
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.ProgramBuilder> DataReaderToList(SqlDataReader dataReader)
        {
            List<YJ.Data.Model.ProgramBuilder> list = new List<YJ.Data.Model.ProgramBuilder>();
            YJ.Data.Model.ProgramBuilder item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.ProgramBuilder {
                    ID = dataReader.GetGuid(0),
                    Name = dataReader.GetString(1),
                    Type = dataReader.GetGuid(2),
                    CreateTime = dataReader.GetDateTime(3)
                };
                if (!dataReader.IsDBNull(4))
                {
                    item.PublishTime = new DateTime?(dataReader.GetDateTime(4));
                }
                item.CreateUser = dataReader.GetGuid(5);
                item.SQL = dataReader.GetString(6);
                item.IsAdd = dataReader.GetInt32(7);
                item.DBConnID = dataReader.GetGuid(8);
                item.Status = dataReader.GetInt32(9);
                if (!dataReader.IsDBNull(10))
                {
                    item.FormID = dataReader.GetString(10);
                }
                if (!dataReader.IsDBNull(11))
                {
                    item.EditModel = new int?(dataReader.GetInt32(11));
                }
                if (!dataReader.IsDBNull(12))
                {
                    item.Width = dataReader.GetString(12);
                }
                if (!dataReader.IsDBNull(13))
                {
                    item.Height = dataReader.GetString(13);
                }
                if (!dataReader.IsDBNull(14))
                {
                    item.ButtonLocation = new int?(dataReader.GetInt32(14));
                }
                if (!dataReader.IsDBNull(15))
                {
                    item.IsPager = new int?(dataReader.GetInt32(15));
                }
                if (!dataReader.IsDBNull(0x10))
                {
                    item.ClientScript = dataReader.GetString(0x10);
                }
                if (!dataReader.IsDBNull(0x11))
                {
                    item.ExportTemplate = dataReader.GetString(0x11);
                }
                if (!dataReader.IsDBNull(0x12))
                {
                    item.ExportHeaderText = dataReader.GetString(0x12);
                }
                if (!dataReader.IsDBNull(0x13))
                {
                    item.ExportFileName = dataReader.GetString(0x13);
                }
                if (!dataReader.IsDBNull(20))
                {
                    item.TableStyle = dataReader.GetString(20);
                }
                if (!dataReader.IsDBNull(0x15))
                {
                    item.TableHead = dataReader.GetString(0x15);
                }
                if (!dataReader.IsDBNull(0x16))
                {
                    item.TableName = dataReader.GetString(0x16);
                }
                if (!dataReader.IsDBNull(0x17))
                {
                    item.InDataNumberFiledName = dataReader.GetString(0x17);
                }
                if (!dataReader.IsDBNull(0x18))
                {
                    item.InDataFiledName = dataReader.GetString(0x18);
                }
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM ProgramBuilder WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.ProgramBuilder Get(Guid id)
        {
            string sql = "SELECT * FROM ProgramBuilder WHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[1];
            SqlParameter parameter1 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            SqlParameter[] parameter = parameterArray1;
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilder> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.ProgramBuilder> GetAll()
        {
            string sql = "SELECT * FROM ProgramBuilder";
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.ProgramBuilder> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM ProgramBuilder";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public List<YJ.Data.Model.ProgramBuilder> GetList(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string name, [Optional, DefaultParameterValue("")] string typeid)
        {
            long num;
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder("SELECT *,ROW_NUMBER() OVER(ORDER BY CreateTime DESC) AS PagerAutoRowNumber FROM ProgramBuilder WHERE Status<>2 ");
            if (!name.IsNullOrEmpty())
            {
                builder.Append(" AND CHARINDEX(@Name,Name)>0");
                list.Add(new SqlParameter("@Name", name));
            }
            if (!typeid.IsNullOrEmpty())
            {
                builder.Append(" AND Type IN(" + Tools.GetSqlInString(typeid, true, ",") + ")");
            }
            int pageSize = Tools.GetPageSize();
            int pageNumber = Tools.GetPageNumber();
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), pageSize, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, pageSize, pageNumber, query);
            SqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.ProgramBuilder> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public int Update(YJ.Data.Model.ProgramBuilder model)
        {
            string sql = "UPDATE ProgramBuilder SET \r\n\t\t\t\tName=@Name,Type=@Type,CreateTime=@CreateTime,PublishTime=@PublishTime,CreateUser=@CreateUser,SQL=@SQL,IsAdd=@IsAdd,DBConnID=@DBConnID,Status=@Status,FormID=@FormID,EditModel=@EditModel,Width=@Width,Height=@Height,ButtonLocation=@ButtonLocation,IsPager=@IsPager,ClientScript=@ClientScript,ExportTemplate=@ExportTemplate,ExportHeaderText=@ExportHeaderText,ExportFileName=@ExportFileName,TableStyle=@TableStyle,TableHead=@TableHead,TableName=@TableName,InDataNumberFiledName=@InDataNumberFiledName,InDataFiledName=@InDataFiledName    \r\n\t\t\t\tWHERE ID=@ID";
            SqlParameter[] parameterArray1 = new SqlParameter[0x19];
            SqlParameter parameter1 = new SqlParameter("@Name", SqlDbType.NVarChar, 0x3e8) {
                Value = model.Name
            };
            parameterArray1[0] = parameter1;
            SqlParameter parameter2 = new SqlParameter("@Type", SqlDbType.UniqueIdentifier, -1) {
                Value = model.Type
            };
            parameterArray1[1] = parameter2;
            SqlParameter parameter3 = new SqlParameter("@CreateTime", SqlDbType.DateTime, 8) {
                Value = model.CreateTime
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = !model.PublishTime.HasValue ? new SqlParameter("@PublishTime", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@PublishTime", SqlDbType.DateTime, 8) { Value = model.PublishTime };
            SqlParameter parameter6 = new SqlParameter("@CreateUser", SqlDbType.UniqueIdentifier, -1) {
                Value = model.CreateUser
            };
            parameterArray1[4] = parameter6;
            SqlParameter parameter7 = new SqlParameter("@SQL", SqlDbType.VarChar, -1) {
                Value = model.SQL
            };
            parameterArray1[5] = parameter7;
            SqlParameter parameter8 = new SqlParameter("@IsAdd", SqlDbType.Int, -1) {
                Value = model.IsAdd
            };
            parameterArray1[6] = parameter8;
            SqlParameter parameter9 = new SqlParameter("@DBConnID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.DBConnID
            };
            parameterArray1[7] = parameter9;
            SqlParameter parameter10 = new SqlParameter("@Status", SqlDbType.Int, -1) {
                Value = model.Status
            };
            parameterArray1[8] = parameter10;
            parameterArray1[9] = (model.FormID == null) ? new SqlParameter("@FormID", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@FormID", SqlDbType.VarChar, 500) { Value = model.FormID };
            parameterArray1[10] = !model.EditModel.HasValue ? new SqlParameter("@EditModel", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@EditModel", SqlDbType.Int, -1) { Value = model.EditModel };
            parameterArray1[11] = (model.Width == null) ? new SqlParameter("@Width", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Width", SqlDbType.VarChar, 50) { Value = model.Width };
            parameterArray1[12] = (model.Height == null) ? new SqlParameter("@Height", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@Height", SqlDbType.VarChar, 50) { Value = model.Height };
            parameterArray1[13] = !model.ButtonLocation.HasValue ? new SqlParameter("@ButtonLocation", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@ButtonLocation", SqlDbType.Int, -1) { Value = model.ButtonLocation };
            parameterArray1[14] = !model.IsPager.HasValue ? new SqlParameter("@IsPager", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@IsPager", SqlDbType.Int, -1) { Value = model.IsPager };
            parameterArray1[15] = (model.ClientScript == null) ? new SqlParameter("@ClientScript", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@ClientScript", SqlDbType.VarChar, -1) { Value = model.ClientScript };
            parameterArray1[0x10] = (model.ExportTemplate == null) ? new SqlParameter("@ExportTemplate", SqlDbType.VarChar, 0xfa0) { Value = DBNull.Value } : new SqlParameter("@ExportTemplate", SqlDbType.VarChar, 0xfa0) { Value = model.ExportTemplate };
            parameterArray1[0x11] = (model.ExportHeaderText == null) ? new SqlParameter("@ExportHeaderText", SqlDbType.NVarChar, 0x3e8) { Value = DBNull.Value } : new SqlParameter("@ExportHeaderText", SqlDbType.NVarChar, 0x3e8) { Value = model.ExportHeaderText };
            parameterArray1[0x12] = (model.ExportFileName == null) ? new SqlParameter("@ExportFileName", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@ExportFileName", SqlDbType.VarChar, 500) { Value = model.ExportFileName };
            parameterArray1[0x13] = (model.TableStyle == null) ? new SqlParameter("@TableStyle", SqlDbType.VarChar, 0xff) { Value = DBNull.Value } : new SqlParameter("@TableStyle", SqlDbType.VarChar, 0xff) { Value = model.TableStyle };
            parameterArray1[20] = (model.TableHead == null) ? new SqlParameter("@TableHead", SqlDbType.VarChar, -1) { Value = DBNull.Value } : new SqlParameter("@TableHead", SqlDbType.VarChar, -1) { Value = model.TableHead };
            parameterArray1[0x15] = (model.TableName == null) ? new SqlParameter("@TableName", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@TableName", SqlDbType.VarChar, 500) { Value = model.TableName };
            parameterArray1[0x16] = (model.InDataNumberFiledName == null) ? new SqlParameter("@InDataNumberFiledName", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@InDataNumberFiledName", SqlDbType.VarChar, 500) { Value = model.InDataNumberFiledName };
            parameterArray1[0x17] = (model.InDataFiledName == null) ? new SqlParameter("@InDataFiledName", SqlDbType.VarChar, 500) { Value = DBNull.Value } : new SqlParameter("@InDataFiledName", SqlDbType.VarChar, 500) { Value = model.InDataFiledName };
            SqlParameter parameter41 = new SqlParameter("@ID", SqlDbType.UniqueIdentifier, -1) {
                Value = model.ID
            };
            parameterArray1[0x18] = parameter41;
            SqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

