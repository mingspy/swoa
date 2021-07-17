using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebMvc.Areas.AssetManage.Data.MSSQL
{
    public class AmAssets
    {
        private YJ.Data.MSSQL.DBHelper dbHelper = new YJ.Data.MSSQL.DBHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        public AmAssets()
        {
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model">Data.Model.AmAssets实体类</param>
        /// <returns>新增记录的ID</returns>
        public int Add(Data.Model.AmAssets model)
        {
            string sql = @"INSERT INTO AmAssets
				(Module,TypeId,Name,Specs,Num,MeasureUnit,Money,Brand,PurchaseDate,IsFixedAsset,Status,CreateDate,CreateUId,Files,Remark,FixedAssetCode,AppraisalUnit,MeasureType,Code) 
				VALUES(@Module,@TypeId,@Name,@Specs,@Num,@MeasureUnit,@Money,@Brand,@PurchaseDate,@IsFixedAsset,@Status,@CreateDate,@CreateUId,@Files,@Remark,@FixedAssetCode,@AppraisalUnit,@MeasureType,@Code);
				SELECT SCOPE_IDENTITY();";
            SqlParameter[] parameters = new SqlParameter[]{
                model.Module == null ? new SqlParameter("@Module", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@Module", SqlDbType.Int, -1) { Value = model.Module },
                model.TypeId == null ? new SqlParameter("@TypeId", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@TypeId", SqlDbType.NVarChar, 100) { Value = model.TypeId },
                model.Name == null ? new SqlParameter("@Name", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@Name", SqlDbType.NVarChar, 100) { Value = model.Name },
                model.Specs == null ? new SqlParameter("@Specs", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@Specs", SqlDbType.NVarChar, 100) { Value = model.Specs },
                model.Num == null ? new SqlParameter("@Num", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@Num", SqlDbType.Int, -1) { Value = model.Num },
                model.MeasureUnit == null ? new SqlParameter("@MeasureUnit", SqlDbType.NVarChar, 16) { Value = DBNull.Value } : new SqlParameter("@MeasureUnit", SqlDbType.NVarChar, 16) { Value = model.MeasureUnit },
                model.Money == null ? new SqlParameter("@Money", SqlDbType.Money, -1) { Value = DBNull.Value } : new SqlParameter("@Money", SqlDbType.Money, -1) { Value = model.Money },
                model.Brand == null ? new SqlParameter("@Brand", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@Brand", SqlDbType.NVarChar, 100) { Value = model.Brand },
                model.PurchaseDate == null ? new SqlParameter("@PurchaseDate", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@PurchaseDate", SqlDbType.DateTime, 8) { Value = model.PurchaseDate },
                model.IsFixedAsset == null ? new SqlParameter("@IsFixedAsset", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@IsFixedAsset", SqlDbType.Int, -1) { Value = model.IsFixedAsset },
                model.Status == null ? new SqlParameter("@Status", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@Status", SqlDbType.Int, -1) { Value = model.Status },
                model.CreateDate==null? new SqlParameter("@CreateDate", SqlDbType.DateTime, 8) { Value = DBNull.Value } :  new SqlParameter("@CreateDate", SqlDbType.DateTime, 8){ Value = model.CreateDate },
                model.CreateUId == null ? new SqlParameter("@CreateUId", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@CreateUId", SqlDbType.NVarChar, 100) { Value = model.CreateUId },
                model.Files == null ? new SqlParameter("@Files", SqlDbType.NVarChar, 1000) { Value = DBNull.Value } : new SqlParameter("@Files", SqlDbType.NVarChar, 1000) { Value = model.Files },
                model.Remark == null ? new SqlParameter("@Remark", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@Remark", SqlDbType.NVarChar, 100) { Value = model.Remark },
                model.FixedAssetCode == null ? new SqlParameter("@FixedAssetCode", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@FixedAssetCode", SqlDbType.NVarChar, 100) { Value = model.FixedAssetCode },
                model.AppraisalUnit == null ? new SqlParameter("@AppraisalUnit", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@AppraisalUnit", SqlDbType.NVarChar, 100) { Value = model.AppraisalUnit },
                model.MeasureType == null ? new SqlParameter("@MeasureType", SqlDbType.NVarChar, 20) { Value = DBNull.Value } : new SqlParameter("@MeasureType", SqlDbType.NVarChar, 20) { Value = model.MeasureType },
                model.Code == null ? new SqlParameter("@Code", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@Code", SqlDbType.NVarChar, 100) { Value = model.Code }
            };
            int maxID;
            return int.TryParse(dbHelper.ExecuteScalar(sql, parameters), out maxID) ? maxID : -1;
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">Data.Model.AmAssets实体类</param>
        public int Update(Data.Model.AmAssets model)
        {
            string sql = @"UPDATE AmAssets SET 
				Module=@Module,TypeId=@TypeId,Name=@Name,Specs=@Specs,Num=@Num,MeasureUnit=@MeasureUnit,Money=@Money,Brand=@Brand,PurchaseDate=@PurchaseDate,IsFixedAsset=@IsFixedAsset,Status=@Status,CreateDate=@CreateDate,CreateUId=@CreateUId,Files=@Files,Remark=@Remark,FixedAssetCode=@FixedAssetCode,AppraisalUnit=@AppraisalUnit,MeasureType=@MeasureType,Code=@Code
				WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
                model.Module == null ? new SqlParameter("@Module", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@Module", SqlDbType.Int, -1) { Value = model.Module },
                model.TypeId == null ? new SqlParameter("@TypeId", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@TypeId", SqlDbType.NVarChar, 100) { Value = model.TypeId },
                model.Name == null ? new SqlParameter("@Name", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@Name", SqlDbType.NVarChar, 100) { Value = model.Name },
                model.Specs == null ? new SqlParameter("@Specs", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@Specs", SqlDbType.NVarChar, 100) { Value = model.Specs },
                model.Num == null ? new SqlParameter("@Num", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@Num", SqlDbType.Int, -1) { Value = model.Num },
                model.MeasureUnit == null ? new SqlParameter("@MeasureUnit", SqlDbType.NVarChar, 16) { Value = DBNull.Value } : new SqlParameter("@MeasureUnit", SqlDbType.NVarChar, 16) { Value = model.MeasureUnit },
                model.Money == null ? new SqlParameter("@Money", SqlDbType.Money, -1) { Value = DBNull.Value } : new SqlParameter("@Money", SqlDbType.Money, -1) { Value = model.Money },
                model.Brand == null ? new SqlParameter("@Brand", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@Brand", SqlDbType.NVarChar, 100) { Value = model.Brand },
                model.PurchaseDate == null ? new SqlParameter("@PurchaseDate", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@PurchaseDate", SqlDbType.DateTime, 8) { Value = model.PurchaseDate },
                model.IsFixedAsset == null ? new SqlParameter("@IsFixedAsset", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@IsFixedAsset", SqlDbType.Int, -1) { Value = model.IsFixedAsset },
                model.Status == null ? new SqlParameter("@Status", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@Status", SqlDbType.Int, -1) { Value = model.Status },
                new SqlParameter("@CreateDate", SqlDbType.DateTime, 8){ Value = model.CreateDate },
                model.CreateUId == null ? new SqlParameter("@CreateUId", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@CreateUId", SqlDbType.NVarChar, 100) { Value = model.CreateUId },
                model.Files == null ? new SqlParameter("@Files", SqlDbType.NVarChar, 1000) { Value = DBNull.Value } : new SqlParameter("@Files", SqlDbType.NVarChar, 1000) { Value = model.Files },
                model.Remark == null ? new SqlParameter("@Remark", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@Remark", SqlDbType.NVarChar, 100) { Value = model.Remark },
                model.FixedAssetCode == null ? new SqlParameter("@FixedAssetCode", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@FixedAssetCode", SqlDbType.NVarChar, 100) { Value = model.FixedAssetCode },
                model.AppraisalUnit == null ? new SqlParameter("@AppraisalUnit", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@AppraisalUnit", SqlDbType.NVarChar, 100) { Value = model.AppraisalUnit },
                model.MeasureType == null ? new SqlParameter("@MeasureType", SqlDbType.NVarChar, 20) { Value = DBNull.Value } : new SqlParameter("@MeasureType", SqlDbType.NVarChar, 20) { Value = model.MeasureType },
                model.Code == null ? new SqlParameter("@Code", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@Code", SqlDbType.NVarChar, 100) { Value = model.Code },
                new SqlParameter("@ID", SqlDbType.Int, -1){ Value = model.ID }
            };
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(int id)
        {
            string sql = "DELETE FROM AmAssets WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@ID", SqlDbType.Int){ Value = id }
            };
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 将DataRedar转换为List
        /// </summary>
        private List<Data.Model.AmAssets> DataReaderToList(SqlDataReader dataReader)
        {
            List<Data.Model.AmAssets> List = new List<Data.Model.AmAssets>();
            Data.Model.AmAssets model = null;
            while (dataReader.Read())
            {
                model = new Data.Model.AmAssets();
                model.ID = dataReader.GetInt32(0);
                if (!dataReader.IsDBNull(1))
                    model.Module = dataReader.GetInt32(1);
                if (!dataReader.IsDBNull(2))
                    model.TypeId = dataReader.GetString(2);
                if (!dataReader.IsDBNull(3))
                    model.Name = dataReader.GetString(3);
                if (!dataReader.IsDBNull(4))
                    model.Specs = dataReader.GetString(4);
                if (!dataReader.IsDBNull(5))
                    model.Num = dataReader.GetInt32(5);
                if (!dataReader.IsDBNull(6))
                    model.MeasureUnit = dataReader.GetString(6);
                if (!dataReader.IsDBNull(7))
                    model.Money = dataReader.GetDecimal(7);
                if (!dataReader.IsDBNull(8))
                    model.Brand = dataReader.GetString(8);
                if (!dataReader.IsDBNull(9))
                    model.PurchaseDate = dataReader.GetDateTime(9);
                if (!dataReader.IsDBNull(10))
                    model.IsFixedAsset = dataReader.GetInt32(10);
                if (!dataReader.IsDBNull(11))
                    model.Status = dataReader.GetInt32(11);
                model.CreateDate = dataReader.GetDateTime(12);
                if (!dataReader.IsDBNull(13))
                    model.CreateUId = dataReader.GetString(13);
                if (!dataReader.IsDBNull(14))
                    model.Files = dataReader.GetString(14);
                if (!dataReader.IsDBNull(15))
                    model.Remark = dataReader.GetString(15);
                if (!dataReader.IsDBNull(16))
                    model.FixedAssetCode = dataReader.GetString(16);
                if (!dataReader.IsDBNull(17))
                    model.AppraisalUnit = dataReader.GetString(17);
                if (!dataReader.IsDBNull(18))
                    model.MeasureType = dataReader.GetString(18);
                if (!dataReader.IsDBNull(19))
                    model.Code = dataReader.GetString(19);
                List.Add(model);
            }
            return List;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<Data.Model.AmAssets> GetAll()
        {
            string sql = "SELECT * FROM AmAssets";
            SqlDataReader dataReader = dbHelper.GetDataReader(sql);
            List<Data.Model.AmAssets> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        public long GetCount()
        {
            string sql = "SELECT COUNT(*) FROM AmAssets";
            long count;
            return long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;
        }
        /// <summary>
        /// 根据主键查询一条记录
        /// </summary>
        public Data.Model.AmAssets Get(int id)
        {
            string sql = "SELECT * FROM AmAssets WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@ID", SqlDbType.Int){ Value = id }
            };
            SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<Data.Model.AmAssets> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }
    }
}