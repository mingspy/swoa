using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebMvc.Areas.AssetManage.Data.MSSQL
{
    public class AmAssetInOut
    {
        private YJ.Data.MSSQL.DBHelper dbHelper = new YJ.Data.MSSQL.DBHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        public AmAssetInOut()
        {
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model">Data.Model.AmAssetInOut实体类</param>
        /// <returns>新增记录的ID</returns>
        public int Add(Data.Model.AmAssetInOut model)
        {
            string sql = @"INSERT INTO AmAssetInOut
				(AmAssetsId,Type,UseUId,UseDate,Address,CreateDate,Remark,ExtendField1,ExtendField2) 
				VALUES(@AmAssetsId,@Type,@UseUId,@UseDate,@Address,@CreateDate,@Remark,@ExtendField1,@ExtendField2);
				SELECT SCOPE_IDENTITY();";
            SqlParameter[] parameters = new SqlParameter[]{
                model.AmAssetsId == null ? new SqlParameter("@AmAssetsId", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@AmAssetsId", SqlDbType.Int, -1) { Value = model.AmAssetsId },
                model.Type == null ? new SqlParameter("@Type", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@Type", SqlDbType.NVarChar, 100) { Value = model.Type },
                model.UseUId == null ? new SqlParameter("@UseUId", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@UseUId", SqlDbType.NVarChar, 100) { Value = model.UseUId },
                model.UseDate == null ? new SqlParameter("@UseDate", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@UseDate", SqlDbType.DateTime, 8) { Value = model.UseDate },
                model.Address == null ? new SqlParameter("@Address", SqlDbType.NVarChar, 200) { Value = DBNull.Value } : new SqlParameter("@Address", SqlDbType.NVarChar, 200) { Value = model.Address },
                new SqlParameter("@CreateDate", SqlDbType.DateTime, 8){ Value = model.CreateDate },
                model.Remark == null ? new SqlParameter("@Remark", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@Remark", SqlDbType.NVarChar, 100) { Value = model.Remark },
                model.ExtendField1 == null ? new SqlParameter("@ExtendField1", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@ExtendField1", SqlDbType.NVarChar, 100) { Value = model.ExtendField1 },
                model.ExtendField2 == null ? new SqlParameter("@ExtendField2", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@ExtendField2", SqlDbType.NVarChar, 100) { Value = model.ExtendField2 }
            };
            int maxID;
            return int.TryParse(dbHelper.ExecuteScalar(sql, parameters), out maxID) ? maxID : -1;
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">Data.Model.AmAssetInOut实体类</param>
        public int Update(Data.Model.AmAssetInOut model)
        {
            string sql = @"UPDATE AmAssetInOut SET 
				AmAssetsId=@AmAssetsId,Type=@Type,UseUId=@UseUId,UseDate=@UseDate,Address=@Address,CreateDate=@CreateDate,Remark=@Remark,ExtendField1=@ExtendField1,ExtendField2=@ExtendField2
				WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
                model.AmAssetsId == null ? new SqlParameter("@AmAssetsId", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@AmAssetsId", SqlDbType.Int, -1) { Value = model.AmAssetsId },
                model.Type == null ? new SqlParameter("@Type", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@Type", SqlDbType.NVarChar, 100) { Value = model.Type },
                model.UseUId == null ? new SqlParameter("@UseUId", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@UseUId", SqlDbType.NVarChar, 100) { Value = model.UseUId },
                model.UseDate == null ? new SqlParameter("@UseDate", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@UseDate", SqlDbType.DateTime, 8) { Value = model.UseDate },
                model.Address == null ? new SqlParameter("@Address", SqlDbType.NVarChar, 200) { Value = DBNull.Value } : new SqlParameter("@Address", SqlDbType.NVarChar, 200) { Value = model.Address },
                new SqlParameter("@CreateDate", SqlDbType.DateTime, 8){ Value = model.CreateDate },
                model.Remark == null ? new SqlParameter("@Remark", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@Remark", SqlDbType.NVarChar, 100) { Value = model.Remark },
                model.ExtendField1 == null ? new SqlParameter("@ExtendField1", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@ExtendField1", SqlDbType.NVarChar, 100) { Value = model.ExtendField1 },
                model.ExtendField2 == null ? new SqlParameter("@ExtendField2", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@ExtendField2", SqlDbType.NVarChar, 100) { Value = model.ExtendField2 },
                new SqlParameter("@ID", SqlDbType.Int, -1){ Value = model.ID }
            };
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(int id)
        {
            string sql = "DELETE FROM AmAssetInOut WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@ID", SqlDbType.Int){ Value = id }
            };
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 将DataRedar转换为List
        /// </summary>
        private List<Data.Model.AmAssetInOut> DataReaderToList(SqlDataReader dataReader)
        {
            List<Data.Model.AmAssetInOut> List = new List<Data.Model.AmAssetInOut>();
            Data.Model.AmAssetInOut model = null;
            while (dataReader.Read())
            {
                model = new Data.Model.AmAssetInOut();
                model.ID = dataReader.GetInt32(0);
                if (!dataReader.IsDBNull(1))
                    model.AmAssetsId = dataReader.GetInt32(1);
                if (!dataReader.IsDBNull(2))
                    model.Type = dataReader.GetString(2);
                if (!dataReader.IsDBNull(3))
                    model.UseUId = dataReader.GetString(3);
                if (!dataReader.IsDBNull(4))
                    model.UseDate = dataReader.GetDateTime(4);
                if (!dataReader.IsDBNull(5))
                    model.Address = dataReader.GetString(5);
                model.CreateDate = dataReader.GetDateTime(6);
                if (!dataReader.IsDBNull(7))
                    model.Remark = dataReader.GetString(7);
                if (!dataReader.IsDBNull(8))
                    model.ExtendField1 = dataReader.GetString(8);
                if (!dataReader.IsDBNull(9))
                    model.ExtendField2 = dataReader.GetString(9);
                List.Add(model);
            }
            return List;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<Data.Model.AmAssetInOut> GetAll()
        {
            string sql = "SELECT * FROM AmAssetInOut";
            SqlDataReader dataReader = dbHelper.GetDataReader(sql);
            List<Data.Model.AmAssetInOut> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        public long GetCount()
        {
            string sql = "SELECT COUNT(*) FROM AmAssetInOut";
            long count;
            return long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;
        }
        /// <summary>
        /// 根据主键查询一条记录
        /// </summary>
        public Data.Model.AmAssetInOut Get(int id)
        {
            string sql = "SELECT * FROM AmAssetInOut WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@ID", SqlDbType.Int){ Value = id }
            };
            SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<Data.Model.AmAssetInOut> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }
    }
}