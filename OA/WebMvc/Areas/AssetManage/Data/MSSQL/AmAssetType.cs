using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace WebMvc.Areas.AssetManage.Data.MSSQL
{
    public class AmAssetType
    {
        private YJ.Data.MSSQL.DBHelper dbHelper = new YJ.Data.MSSQL.DBHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        public AmAssetType()
        {
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model">WebMvc.Areas.AssetManage.Data.Model.AmAssetType实体类</param>
        /// <returns>新增记录的ID</returns>
        public int Add(WebMvc.Areas.AssetManage.Data.Model.AmAssetType model)
        {
            string sql = @"INSERT INTO AmAssetType
				(ParentAssetTypeId,Code,Name,CreateDate,CreateUId) 
				VALUES(@ParentAssetTypeId,@Code,@Name,@CreateDate,@CreateUId);
				SELECT SCOPE_IDENTITY();";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@ParentAssetTypeId", SqlDbType.Int, -1){ Value = model.ParentAssetTypeId },
                new SqlParameter("@Code", SqlDbType.VarChar, 50){ Value = model.Code },
                new SqlParameter("@Name", SqlDbType.NVarChar, 100){ Value = model.Name },
                new SqlParameter("@CreateDate", SqlDbType.DateTime, 8){ Value = model.CreateDate },
                model.CreateUId == null ? new SqlParameter("@CreateUId", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@CreateUId", SqlDbType.NVarChar, 100) { Value = model.CreateUId }
            };
            int maxID;
            return int.TryParse(dbHelper.ExecuteScalar(sql, parameters), out maxID) ? maxID : -1;
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">WebMvc.Areas.AssetManage.Data.Model.AmAssetType实体类</param>
        public int Update(WebMvc.Areas.AssetManage.Data.Model.AmAssetType model)
        {
            string sql = @"UPDATE AmAssetType SET 
				ParentAssetTypeId=@ParentAssetTypeId,Code=@Code,Name=@Name,CreateDate=@CreateDate,CreateUId=@CreateUId
				WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@ParentAssetTypeId", SqlDbType.Int, -1){ Value = model.ParentAssetTypeId },
                new SqlParameter("@Code", SqlDbType.VarChar, 50){ Value = model.Code },
                new SqlParameter("@Name", SqlDbType.NVarChar, 100){ Value = model.Name },
                new SqlParameter("@CreateDate", SqlDbType.DateTime, 8){ Value = model.CreateDate },
                model.CreateUId == null ? new SqlParameter("@CreateUId", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@CreateUId", SqlDbType.NVarChar, 100) { Value = model.CreateUId },
                new SqlParameter("@ID", SqlDbType.Int, -1){ Value = model.ID }
            };
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(int id)
        {
            string sql = "DELETE FROM AmAssetType WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@ID", SqlDbType.Int){ Value = id }
            };
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 将DataRedar转换为List
        /// </summary>
        private List<WebMvc.Areas.AssetManage.Data.Model.AmAssetType> DataReaderToList(SqlDataReader dataReader)
        {
            List<WebMvc.Areas.AssetManage.Data.Model.AmAssetType> List = new List<WebMvc.Areas.AssetManage.Data.Model.AmAssetType>();
            WebMvc.Areas.AssetManage.Data.Model.AmAssetType model = null;
            while (dataReader.Read())
            {
                model = new WebMvc.Areas.AssetManage.Data.Model.AmAssetType();
                model.ID = dataReader.GetInt32(0);
                model.ParentAssetTypeId = dataReader.GetInt32(1);
                model.Code = dataReader.GetString(2);
                model.Name = dataReader.GetString(3);
                model.CreateDate = dataReader.GetDateTime(4);
                if (!dataReader.IsDBNull(5))
                    model.CreateUId = dataReader.GetString(5);
                List.Add(model);
            }
            return List;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<WebMvc.Areas.AssetManage.Data.Model.AmAssetType> GetAll()
        {
            string sql = "SELECT * FROM AmAssetType";
            SqlDataReader dataReader = dbHelper.GetDataReader(sql);
            List<WebMvc.Areas.AssetManage.Data.Model.AmAssetType> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        public long GetCount()
        {
            string sql = "SELECT COUNT(*) FROM AmAssetType";
            long count;
            return long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;
        }
        /// <summary>
        /// 根据主键查询一条记录
        /// </summary>
        public WebMvc.Areas.AssetManage.Data.Model.AmAssetType Get(int id)
        {
            string sql = "SELECT * FROM AmAssetType WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@ID", SqlDbType.Int){ Value = id }
            };
            SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<WebMvc.Areas.AssetManage.Data.Model.AmAssetType> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }
    }
}