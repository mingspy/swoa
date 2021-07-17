using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using YJ.Data.Interface;
using YJ.Data.Model;
using YJ.Utility;
namespace YJ.Data.MySql
{
 

    public class ProgramBuilder : IProgramBuilder
    {
        private DBHelper dbHelper = new DBHelper();

        public int Add(YJ.Data.Model.ProgramBuilder model)
        {
            string sql = "INSERT INTO programbuilder\r\n\t\t\t\t(ID,Name,Type,CreateTime,PublishTime,CreateUser,`SQL`,IsAdd,DBConnID,Status,FormID,EditModel,Width,Height,ButtonLocation,IsPager,ClientScript,ExportTemplate,ExportHeaderText,ExportFileName,TableStyle,TableHead,`TableName`,InDataNumberFiledName) \r\n\t\t\t\tVALUES(@ID,@Name,@Type,@CreateTime,@PublishTime,@CreateUser,@SQL,@IsAdd,@DBConnID,@Status,@FormID,@EditModel,@Width,@Height,@ButtonLocation,@IsPager,@ClientScript,@ExportTemplate,@ExportHeaderText,@ExportFileName,@TableStyle,@TableHead,@TableName,@InDataNumberFiledName)";
            MySqlParameter[] parameterArray1 = new MySqlParameter[0x18];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Name", MySqlDbType.Text, -1) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@Type", MySqlDbType.VarChar, 0x24) {
                Value = model.Type
            };
            parameterArray1[2] = parameter3;
            MySqlParameter parameter4 = new MySqlParameter("@CreateTime", MySqlDbType.DateTime, -1) {
                Value = model.CreateTime
            };
            parameterArray1[3] = parameter4;
            parameterArray1[4] = !model.PublishTime.HasValue ? new MySqlParameter("@PublishTime", MySqlDbType.DateTime, -1) { Value = DBNull.Value } : new MySqlParameter("@PublishTime", MySqlDbType.DateTime, -1) { Value = model.PublishTime };
            MySqlParameter parameter7 = new MySqlParameter("@CreateUser", MySqlDbType.VarChar, 0x24) {
                Value = model.CreateUser
            };
            parameterArray1[5] = parameter7;
            MySqlParameter parameter8 = new MySqlParameter("@SQL", MySqlDbType.LongText, -1) {
                Value = model.SQL
            };
            parameterArray1[6] = parameter8;
            MySqlParameter parameter9 = new MySqlParameter("@IsAdd", MySqlDbType.Int32, 11) {
                Value = model.IsAdd
            };
            parameterArray1[7] = parameter9;
            MySqlParameter parameter10 = new MySqlParameter("@DBConnID", MySqlDbType.VarChar, 0x24) {
                Value = model.DBConnID
            };
            parameterArray1[8] = parameter10;
            MySqlParameter parameter11 = new MySqlParameter("@Status", MySqlDbType.Int32, 11) {
                Value = model.Status
            };
            parameterArray1[9] = parameter11;
            parameterArray1[10] = (model.FormID == null) ? new MySqlParameter("@FormID", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@FormID", MySqlDbType.Text, -1) { Value = model.FormID };
            parameterArray1[11] = !model.EditModel.HasValue ? new MySqlParameter("@EditModel", MySqlDbType.Int32, 11) { Value = DBNull.Value } : new MySqlParameter("@EditModel", MySqlDbType.Int32, 11) { Value = model.EditModel };
            parameterArray1[12] = (model.Width == null) ? new MySqlParameter("@Width", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@Width", MySqlDbType.VarChar, 50) { Value = model.Width };
            parameterArray1[13] = (model.Height == null) ? new MySqlParameter("@Height", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@Height", MySqlDbType.VarChar, 50) { Value = model.Height };
            parameterArray1[14] = !model.ButtonLocation.HasValue ? new MySqlParameter("@ButtonLocation", MySqlDbType.Int32, 11) { Value = DBNull.Value } : new MySqlParameter("@ButtonLocation", MySqlDbType.Int32, 11) { Value = model.ButtonLocation };
            parameterArray1[15] = !model.IsPager.HasValue ? new MySqlParameter("@IsPager", MySqlDbType.Int32, 11) { Value = DBNull.Value } : new MySqlParameter("@IsPager", MySqlDbType.Int32, 11) { Value = model.IsPager };
            parameterArray1[0x10] = (model.ClientScript == null) ? new MySqlParameter("@ClientScript", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@ClientScript", MySqlDbType.LongText, -1) { Value = model.ClientScript };
            parameterArray1[0x11] = (model.ExportTemplate == null) ? new MySqlParameter("@ExportTemplate", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@ExportTemplate", MySqlDbType.Text, -1) { Value = model.ExportTemplate };
            parameterArray1[0x12] = (model.ExportHeaderText == null) ? new MySqlParameter("@ExportHeaderText", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@ExportHeaderText", MySqlDbType.Text, -1) { Value = model.ExportHeaderText };
            parameterArray1[0x13] = (model.ExportFileName == null) ? new MySqlParameter("@ExportFileName", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@ExportFileName", MySqlDbType.Text, -1) { Value = model.ExportFileName };
            parameterArray1[20] = (model.TableStyle == null) ? new MySqlParameter("@TableStyle", MySqlDbType.VarChar, 0xff) { Value = DBNull.Value } : new MySqlParameter("@TableStyle", MySqlDbType.VarChar, 0xff) { Value = model.TableStyle };
            parameterArray1[0x15] = (model.TableHead == null) ? new MySqlParameter("@TableHead", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@TableHead", MySqlDbType.Text, -1) { Value = model.TableHead };
            parameterArray1[0x16] = (model.TableName == null) ? new MySqlParameter("@TableName", MySqlDbType.VarChar, 0xff) { Value = DBNull.Value } : new MySqlParameter("@TableName", MySqlDbType.VarChar, 0xff) { Value = model.TableName };
            parameterArray1[0x17] = (model.InDataNumberFiledName == null) ? new MySqlParameter("@InDataNumberFiledName", MySqlDbType.VarChar, 0xff) { Value = DBNull.Value } : new MySqlParameter("@InDataNumberFiledName", MySqlDbType.VarChar, 0xff) { Value = model.InDataNumberFiledName };
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        private List<YJ.Data.Model.ProgramBuilder> DataReaderToList(MySqlDataReader dataReader)
        {
            List<YJ.Data.Model.ProgramBuilder> list = new List<YJ.Data.Model.ProgramBuilder>();
            YJ.Data.Model.ProgramBuilder item = null;
            while (dataReader.Read())
            {
                item = new YJ.Data.Model.ProgramBuilder {
                    ID = dataReader.GetString(0).ToGuid(),
                    Name = dataReader.GetString(1),
                    Type = dataReader.GetString(2).ToGuid(),
                    CreateTime = dataReader.GetDateTime(3)
                };
                if (!dataReader.IsDBNull(4))
                {
                    item.PublishTime = new DateTime?(dataReader.GetDateTime(4));
                }
                item.CreateUser = dataReader.GetString(5).ToGuid();
                item.SQL = dataReader.GetString(6);
                item.IsAdd = dataReader.GetInt32(7);
                item.DBConnID = dataReader.GetString(8).ToGuid();
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
                list.Add(item);
            }
            return list;
        }

        public int Delete(Guid id)
        {
            string sql = "DELETE FROM programbuilder WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }

        public YJ.Data.Model.ProgramBuilder Get(Guid id)
        {
            string sql = "SELECT * FROM programbuilder WHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[1];
            MySqlParameter parameter1 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = id.ToString()
            };
            parameterArray1[0] = parameter1;
            MySqlParameter[] parameter = parameterArray1;
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilder> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.ProgramBuilder> GetAll()
        {
            string sql = "SELECT * FROM programbuilder";
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql);
            List<YJ.Data.Model.ProgramBuilder> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list;
        }

        public long GetCount()
        {
            long num;
            string sql = "SELECT COUNT(*) FROM programbuilder";
            return (long.TryParse(this.dbHelper.GetFieldValue(sql), out num) ? num : 0L);
        }

        public List<YJ.Data.Model.ProgramBuilder> GetList(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string name, [Optional, DefaultParameterValue("")] string typeid)
        {
            long num;
            List<MySqlParameter> list = new List<MySqlParameter>();
            StringBuilder builder = new StringBuilder("SELECT * FROM ProgramBuilder WHERE Status<>2 ");
            if (!name.IsNullOrEmpty())
            {
                builder.Append(" AND INSTR(Name,@Name)>0");
                list.Add(new MySqlParameter("@Name", name));
            }
            if (!typeid.IsNullOrEmpty())
            {
                builder.Append(" AND Type IN(" + Tools.GetSqlInString(typeid, true, ",") + ")");
            }
            builder.Append(" ORDER BY CreateTime DESC");
            int pageSize = Tools.GetPageSize();
            int pageNumber = Tools.GetPageNumber();
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), pageSize, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, pageSize, pageNumber, query);
            MySqlDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.ProgramBuilder> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public int Update(YJ.Data.Model.ProgramBuilder model)
        {
            string sql = "UPDATE programbuilder SET \r\n\t\t\t\tName=@Name,Type=@Type,CreateTime=@CreateTime,PublishTime=@PublishTime,CreateUser=@CreateUser,`SQL`=@SQL,IsAdd=@IsAdd,DBConnID=@DBConnID,Status=@Status,FormID=@FormID,EditModel=@EditModel,Width=@Width,Height=@Height,ButtonLocation=@ButtonLocation,IsPager=@IsPager,ClientScript=@ClientScript,ExportTemplate=@ExportTemplate,ExportHeaderText=@ExportHeaderText,ExportFileName=@ExportFileName,TableStyle=@TableStyle,TableHead=@TableHead,TableName=@TableName,InDataNumberFiledName=@InDataNumberFiledName    \r\n\t\t\t\tWHERE ID=@ID";
            MySqlParameter[] parameterArray1 = new MySqlParameter[0x18];
            MySqlParameter parameter1 = new MySqlParameter("@Name", MySqlDbType.Text, -1) {
                Value = model.Name
            };
            parameterArray1[0] = parameter1;
            MySqlParameter parameter2 = new MySqlParameter("@Type", MySqlDbType.VarChar, 0x24) {
                Value = model.Type
            };
            parameterArray1[1] = parameter2;
            MySqlParameter parameter3 = new MySqlParameter("@CreateTime", MySqlDbType.DateTime, -1) {
                Value = model.CreateTime
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = !model.PublishTime.HasValue ? new MySqlParameter("@PublishTime", MySqlDbType.DateTime, -1) { Value = DBNull.Value } : new MySqlParameter("@PublishTime", MySqlDbType.DateTime, -1) { Value = model.PublishTime };
            MySqlParameter parameter6 = new MySqlParameter("@CreateUser", MySqlDbType.VarChar, 0x24) {
                Value = model.CreateUser
            };
            parameterArray1[4] = parameter6;
            MySqlParameter parameter7 = new MySqlParameter("@SQL", MySqlDbType.LongText, -1) {
                Value = model.SQL
            };
            parameterArray1[5] = parameter7;
            MySqlParameter parameter8 = new MySqlParameter("@IsAdd", MySqlDbType.Int32, 11) {
                Value = model.IsAdd
            };
            parameterArray1[6] = parameter8;
            MySqlParameter parameter9 = new MySqlParameter("@DBConnID", MySqlDbType.VarChar, 0x24) {
                Value = model.DBConnID
            };
            parameterArray1[7] = parameter9;
            MySqlParameter parameter10 = new MySqlParameter("@Status", MySqlDbType.Int32, 11) {
                Value = model.Status
            };
            parameterArray1[8] = parameter10;
            parameterArray1[9] = (model.FormID == null) ? new MySqlParameter("@FormID", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@FormID", MySqlDbType.Text, -1) { Value = model.FormID };
            parameterArray1[10] = !model.EditModel.HasValue ? new MySqlParameter("@EditModel", MySqlDbType.Int32, 11) { Value = DBNull.Value } : new MySqlParameter("@EditModel", MySqlDbType.Int32, 11) { Value = model.EditModel };
            parameterArray1[11] = (model.Width == null) ? new MySqlParameter("@Width", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@Width", MySqlDbType.VarChar, 50) { Value = model.Width };
            parameterArray1[12] = (model.Height == null) ? new MySqlParameter("@Height", MySqlDbType.VarChar, 50) { Value = DBNull.Value } : new MySqlParameter("@Height", MySqlDbType.VarChar, 50) { Value = model.Height };
            parameterArray1[13] = !model.ButtonLocation.HasValue ? new MySqlParameter("@ButtonLocation", MySqlDbType.Int32, 11) { Value = DBNull.Value } : new MySqlParameter("@ButtonLocation", MySqlDbType.Int32, 11) { Value = model.ButtonLocation };
            parameterArray1[14] = !model.IsPager.HasValue ? new MySqlParameter("@IsPager", MySqlDbType.Int32, 11) { Value = DBNull.Value } : new MySqlParameter("@IsPager", MySqlDbType.Int32, 11) { Value = model.IsPager };
            parameterArray1[15] = (model.ClientScript == null) ? new MySqlParameter("@ClientScript", MySqlDbType.LongText, -1) { Value = DBNull.Value } : new MySqlParameter("@ClientScript", MySqlDbType.LongText, -1) { Value = model.ClientScript };
            parameterArray1[0x10] = (model.ExportTemplate == null) ? new MySqlParameter("@ExportTemplate", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@ExportTemplate", MySqlDbType.Text, -1) { Value = model.ExportTemplate };
            parameterArray1[0x11] = (model.ExportHeaderText == null) ? new MySqlParameter("@ExportHeaderText", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@ExportHeaderText", MySqlDbType.Text, -1) { Value = model.ExportHeaderText };
            parameterArray1[0x12] = (model.ExportFileName == null) ? new MySqlParameter("@ExportFileName", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@ExportFileName", MySqlDbType.Text, -1) { Value = model.ExportFileName };
            parameterArray1[0x13] = (model.TableStyle == null) ? new MySqlParameter("@TableStyle", MySqlDbType.VarChar, 0xff) { Value = DBNull.Value } : new MySqlParameter("@TableStyle", MySqlDbType.VarChar, 0xff) { Value = model.TableStyle };
            parameterArray1[20] = (model.TableHead == null) ? new MySqlParameter("@TableHead", MySqlDbType.Text, -1) { Value = DBNull.Value } : new MySqlParameter("@TableHead", MySqlDbType.Text, -1) { Value = model.TableHead };
            parameterArray1[0x15] = (model.TableName == null) ? new MySqlParameter("@TableName", MySqlDbType.VarChar, 0xff) { Value = DBNull.Value } : new MySqlParameter("@TableName", MySqlDbType.VarChar, 0xff) { Value = model.TableName };
            parameterArray1[0x16] = (model.InDataNumberFiledName == null) ? new MySqlParameter("@InDataNumberFiledName", MySqlDbType.VarChar, 0xff) { Value = DBNull.Value } : new MySqlParameter("@InDataNumberFiledName", MySqlDbType.VarChar, 0xff) { Value = model.InDataNumberFiledName };
            MySqlParameter parameter39 = new MySqlParameter("@ID", MySqlDbType.VarChar, 0x24) {
                Value = model.ID
            };
            parameterArray1[0x17] = parameter39;
            MySqlParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter, false);
        }
    }
}

