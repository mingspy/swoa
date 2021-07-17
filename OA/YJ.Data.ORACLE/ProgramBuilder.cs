namespace YJ.Data.ORACLE
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
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
            string sql = "INSERT INTO ProgramBuilder\r\n\t\t\t\t(ID,Name,Type,CreateTime,PublishTime,CreateUser,SQL,IsAdd,DBConnID,Status,FormID,EditModel,Width,Height,ButtonLocation,IsPager,ClientScript,ExportTemplate,ExportHeaderText,ExportFileName,TableStyle,TableHead,TableName,InDataNumberFiledName) \r\n\t\t\t\tVALUES(:ID,:Name,:Type,:CreateTime,:PublishTime,:CreateUser,:SQL,:IsAdd,:DBConnID,:Status,:FormID,:EditModel,:Width,:Height,:ButtonLocation,:IsPager,:ClientScript,:ExportTemplate,:ExportHeaderText,:ExportFileName,:TableStyle,:TableHead,:TableName,:InDataNumberFiledName)";
            OracleParameter[] parameterArray1 = new OracleParameter[0x18];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Name", OracleDbType.Varchar2) {
                Value = model.Name
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":Type", OracleDbType.Varchar2) {
                Value = model.Type
            };
            parameterArray1[2] = parameter3;
            OracleParameter parameter4 = new OracleParameter(":CreateTime", OracleDbType.Date) {
                Value = model.CreateTime
            };
            parameterArray1[3] = parameter4;
            parameterArray1[4] = !model.PublishTime.HasValue ? new OracleParameter(":PublishTime", OracleDbType.Date) { Value = DBNull.Value } : new OracleParameter(":PublishTime", OracleDbType.Date) { Value = model.PublishTime };
            OracleParameter parameter7 = new OracleParameter(":CreateUser", OracleDbType.Varchar2) {
                Value = model.CreateUser
            };
            parameterArray1[5] = parameter7;
            OracleParameter parameter8 = new OracleParameter(":SQL", OracleDbType.Varchar2) {
                Value = model.SQL
            };
            parameterArray1[6] = parameter8;
            OracleParameter parameter9 = new OracleParameter(":IsAdd", OracleDbType.Int32) {
                Value = model.IsAdd
            };
            parameterArray1[7] = parameter9;
            OracleParameter parameter10 = new OracleParameter(":DBConnID", OracleDbType.Varchar2) {
                Value = model.DBConnID
            };
            parameterArray1[8] = parameter10;
            OracleParameter parameter11 = new OracleParameter(":Status", OracleDbType.Int32) {
                Value = model.Status
            };
            parameterArray1[9] = parameter11;
            parameterArray1[10] = (model.FormID == null) ? new OracleParameter(":FormID", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":FormID", OracleDbType.Varchar2) { Value = model.FormID };
            parameterArray1[11] = !model.EditModel.HasValue ? new OracleParameter(":EditModel", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter(":EditModel", OracleDbType.Int32) { Value = model.EditModel };
            parameterArray1[12] = (model.Width == null) ? new OracleParameter(":Width", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Width", OracleDbType.Varchar2) { Value = model.Width };
            parameterArray1[13] = (model.Height == null) ? new OracleParameter(":Height", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Height", OracleDbType.Varchar2) { Value = model.Height };
            parameterArray1[14] = !model.ButtonLocation.HasValue ? new OracleParameter(":ButtonLocation", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter(":ButtonLocation", OracleDbType.Int32) { Value = model.ButtonLocation };
            parameterArray1[15] = !model.IsPager.HasValue ? new OracleParameter(":IsPager", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter(":IsPager", OracleDbType.Int32) { Value = model.IsPager };
            parameterArray1[0x10] = (model.ClientScript == null) ? new OracleParameter(":ClientScript", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":ClientScript", OracleDbType.Varchar2) { Value = model.ClientScript };
            parameterArray1[0x11] = (model.ExportTemplate == null) ? new OracleParameter(":ExportTemplate", OracleDbType.Varchar2, 0xfa0) { Value = DBNull.Value } : new OracleParameter(":ExportTemplate", OracleDbType.Varchar2, 0xfa0) { Value = model.ExportTemplate };
            parameterArray1[0x12] = (model.ExportHeaderText == null) ? new OracleParameter(":ExportHeaderText", OracleDbType.NVarchar2, 500) { Value = DBNull.Value } : new OracleParameter(":ExportHeaderText", OracleDbType.NVarchar2, 500) { Value = model.ExportHeaderText };
            parameterArray1[0x13] = (model.ExportFileName == null) ? new OracleParameter(":ExportFileName", OracleDbType.Varchar2, 500) { Value = DBNull.Value } : new OracleParameter(":ExportFileName", OracleDbType.Varchar2, 500) { Value = model.ExportFileName };
            parameterArray1[20] = (model.TableStyle == null) ? new OracleParameter(":TableStyle", OracleDbType.Varchar2, 0xff) { Value = DBNull.Value } : new OracleParameter(":TableStyle", OracleDbType.Varchar2, 0xff) { Value = model.TableStyle };
            parameterArray1[0x15] = (model.TableHead == null) ? new OracleParameter(":TableHead", OracleDbType.Varchar2, 0xfa0) { Value = DBNull.Value } : new OracleParameter(":TableHead", OracleDbType.Varchar2, 0xfa0) { Value = model.TableHead };
            parameterArray1[0x16] = (model.TableName == null) ? new OracleParameter(":TableName", OracleDbType.Varchar2, 500) { Value = DBNull.Value } : new OracleParameter(":TableName", OracleDbType.Varchar2, 500) { Value = model.TableName };
            parameterArray1[0x17] = (model.InDataNumberFiledName == null) ? new OracleParameter(":InDataNumberFiledName", OracleDbType.Varchar2, 500) { Value = DBNull.Value } : new OracleParameter(":InDataNumberFiledName", OracleDbType.Varchar2, 500) { Value = model.InDataNumberFiledName };
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        private List<YJ.Data.Model.ProgramBuilder> DataReaderToList(OracleDataReader dataReader)
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
            string sql = "DELETE FROM ProgramBuilder WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }

        public YJ.Data.Model.ProgramBuilder Get(Guid id)
        {
            string sql = "SELECT * FROM ProgramBuilder WHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[1];
            OracleParameter parameter1 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = id
            };
            parameterArray1[0] = parameter1;
            OracleParameter[] parameter = parameterArray1;
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, parameter);
            List<YJ.Data.Model.ProgramBuilder> list = this.DataReaderToList(dataReader);
            dataReader.Close();
            return ((list.Count > 0) ? list[0] : null);
        }

        public List<YJ.Data.Model.ProgramBuilder> GetAll()
        {
            string sql = "SELECT * FROM ProgramBuilder";
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql);
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
            List<OracleParameter> list = new List<OracleParameter>();
            StringBuilder builder = new StringBuilder("SELECT PagerTempTable.*,ROWNUM AS PagerAutoRowNumber FROM (SELECT * FROM ProgramBuilder WHERE Status<>2 ");
            if (!name.IsNullOrEmpty())
            {
                builder.Append(" AND INSTR(Name,:Name,1,1)>0");
                list.Add(new OracleParameter(":Name", name));
            }
            if (!typeid.IsNullOrEmpty())
            {
                builder.Append(" AND Type IN(" + Tools.GetSqlInString(typeid, true, ",") + ")");
            }
            builder.Append(" ORDER BY CreateTime DESC) PagerTempTable");
            int pageSize = Tools.GetPageSize();
            int pageNumber = Tools.GetPageNumber();
            string sql = this.dbHelper.GetPaerSql(builder.ToString(), pageSize, pageNumber, out num, list.ToArray());
            pager = Tools.GetPagerHtml(num, pageSize, pageNumber, query);
            OracleDataReader dataReader = this.dbHelper.GetDataReader(sql, list.ToArray());
            List<YJ.Data.Model.ProgramBuilder> list2 = this.DataReaderToList(dataReader);
            dataReader.Close();
            return list2;
        }

        public int Update(YJ.Data.Model.ProgramBuilder model)
        {
            string sql = "UPDATE ProgramBuilder SET \r\n\t\t\t\tName=:Name,Type=:Type,CreateTime=:CreateTime,PublishTime=:PublishTime,CreateUser=:CreateUser,SQL=:SQL,IsAdd=:IsAdd,DBConnID=:DBConnID,Status=:Status,FormID=:FormID,EditModel=:EditModel,Width=:Width,Height=:Height,ButtonLocation=:ButtonLocation,IsPager=:IsPager,ClientScript=:ClientScript,ExportTemplate=:ExportTemplate,ExportHeaderText=:ExportHeaderText,ExportFileName=:ExportFileName,TableStyle=:TableStyle,TableHead=:TableHead,TableName=:TableName,InDataNumberFiledName=:InDataNumberFiledName   \r\n\t\t\t\tWHERE ID=:ID";
            OracleParameter[] parameterArray1 = new OracleParameter[0x18];
            OracleParameter parameter1 = new OracleParameter(":Name", OracleDbType.Varchar2) {
                Value = model.Name
            };
            parameterArray1[0] = parameter1;
            OracleParameter parameter2 = new OracleParameter(":Type", OracleDbType.Varchar2) {
                Value = model.Type
            };
            parameterArray1[1] = parameter2;
            OracleParameter parameter3 = new OracleParameter(":CreateTime", OracleDbType.Date) {
                Value = model.CreateTime
            };
            parameterArray1[2] = parameter3;
            parameterArray1[3] = !model.PublishTime.HasValue ? new OracleParameter(":PublishTime", OracleDbType.Date) { Value = DBNull.Value } : new OracleParameter(":PublishTime", OracleDbType.Date) { Value = model.PublishTime };
            OracleParameter parameter6 = new OracleParameter(":CreateUser", OracleDbType.Varchar2) {
                Value = model.CreateUser
            };
            parameterArray1[4] = parameter6;
            OracleParameter parameter7 = new OracleParameter(":SQL", OracleDbType.Varchar2) {
                Value = model.SQL
            };
            parameterArray1[5] = parameter7;
            OracleParameter parameter8 = new OracleParameter(":IsAdd", OracleDbType.Int32) {
                Value = model.IsAdd
            };
            parameterArray1[6] = parameter8;
            OracleParameter parameter9 = new OracleParameter(":DBConnID", OracleDbType.Varchar2) {
                Value = model.DBConnID
            };
            parameterArray1[7] = parameter9;
            OracleParameter parameter10 = new OracleParameter(":Status", OracleDbType.Int32) {
                Value = model.Status
            };
            parameterArray1[8] = parameter10;
            parameterArray1[9] = (model.FormID == null) ? new OracleParameter(":FormID", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":FormID", OracleDbType.Varchar2) { Value = model.FormID };
            parameterArray1[10] = !model.EditModel.HasValue ? new OracleParameter(":EditModel", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter(":EditModel", OracleDbType.Int32) { Value = model.EditModel };
            parameterArray1[11] = (model.Width == null) ? new OracleParameter(":Width", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Width", OracleDbType.Varchar2) { Value = model.Width };
            parameterArray1[12] = (model.Height == null) ? new OracleParameter(":Height", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":Height", OracleDbType.Varchar2) { Value = model.Height };
            parameterArray1[13] = !model.ButtonLocation.HasValue ? new OracleParameter(":ButtonLocation", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter(":ButtonLocation", OracleDbType.Int32) { Value = model.ButtonLocation };
            parameterArray1[14] = !model.IsPager.HasValue ? new OracleParameter(":IsPager", OracleDbType.Int32) { Value = DBNull.Value } : new OracleParameter(":IsPager", OracleDbType.Int32) { Value = model.IsPager };
            parameterArray1[15] = (model.ClientScript == null) ? new OracleParameter(":ClientScript", OracleDbType.Varchar2) { Value = DBNull.Value } : new OracleParameter(":ClientScript", OracleDbType.Varchar2) { Value = model.ClientScript };
            parameterArray1[0x10] = (model.ExportTemplate == null) ? new OracleParameter(":ExportTemplate", OracleDbType.Varchar2, 0xfa0) { Value = DBNull.Value } : new OracleParameter(":ExportTemplate", OracleDbType.Varchar2, 0xfa0) { Value = model.ExportTemplate };
            parameterArray1[0x11] = (model.ExportHeaderText == null) ? new OracleParameter(":ExportHeaderText", OracleDbType.NVarchar2, 500) { Value = DBNull.Value } : new OracleParameter(":ExportHeaderText", OracleDbType.NVarchar2, 500) { Value = model.ExportHeaderText };
            parameterArray1[0x12] = (model.ExportFileName == null) ? new OracleParameter(":ExportFileName", OracleDbType.Varchar2, 500) { Value = DBNull.Value } : new OracleParameter(":ExportFileName", OracleDbType.Varchar2, 500) { Value = model.ExportFileName };
            parameterArray1[0x13] = (model.TableStyle == null) ? new OracleParameter(":TableStyle", OracleDbType.Varchar2, 0xff) { Value = DBNull.Value } : new OracleParameter(":TableStyle", OracleDbType.Varchar2, 0xff) { Value = model.TableStyle };
            parameterArray1[20] = (model.TableHead == null) ? new OracleParameter(":TableHead", OracleDbType.Varchar2, 0xfa0) { Value = DBNull.Value } : new OracleParameter(":TableHead", OracleDbType.Varchar2, 0xfa0) { Value = model.TableHead };
            parameterArray1[0x15] = (model.TableName == null) ? new OracleParameter(":TableName", OracleDbType.Varchar2, 500) { Value = DBNull.Value } : new OracleParameter(":TableName", OracleDbType.Varchar2, 500) { Value = model.TableName };
            parameterArray1[0x16] = (model.InDataNumberFiledName == null) ? new OracleParameter(":InDataNumberFiledName", OracleDbType.Varchar2, 500) { Value = DBNull.Value } : new OracleParameter(":InDataNumberFiledName", OracleDbType.Varchar2, 500) { Value = model.InDataNumberFiledName };
            OracleParameter parameter39 = new OracleParameter(":ID", OracleDbType.Varchar2) {
                Value = model.ID
            };
            parameterArray1[0x17] = parameter39;
            OracleParameter[] parameter = parameterArray1;
            return this.dbHelper.Execute(sql, parameter);
        }
    }
}

