using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebMvc.Areas.AssetManage.Data.MSSQL
{
    public class AmSample
    {
        private YJ.Data.MSSQL.DBHelper dbHelper = new YJ.Data.MSSQL.DBHelper();
        /// <summary>
        /// 构造函数
        /// </summary>
        public AmSample()
        {
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model">AssetManage.Data.Model.AmSample实体类</param>
        /// <returns>新增记录的ID</returns>
        public int Add(AssetManage.Data.Model.AmSample model)
        {
            string sql = @"INSERT INTO AmSample
				(bgbh,rwlx,cydh,sjqy_mc,ypmc,yp_ggxh,yp_sl,yp_ddrq,wtdw,scdw,yp_bzq,panding,pz_time,pz_bz,yp_scrq,ypzt,yp_byl,Address,DisposeWay,DisposeResult,InDate,Remark,GroupCode,Type,ExtendField1,ExtendField2,DisposeCause,DisposeOpinion,expire, rest, unit, byl_rest) 
				VALUES(@bgbh,@rwlx,@cydh,@sjqy_mc,@ypmc,@yp_ggxh,@yp_sl,@yp_ddrq,@wtdw,@scdw,@yp_bzq,@panding,@pz_time,@pz_bz,@yp_scrq,@ypzt,@yp_byl,@Address,@DisposeWay,@DisposeResult,@InDate,@Remark,@GroupCode,@Type,@ExtendField1,@ExtendField2,@DisposeCause,@DisposeOpinion, @expire, @rest, @unit, @byl_rest);
				SELECT SCOPE_IDENTITY();";
            SqlParameter[] parameters = new SqlParameter[]{
                model.bgbh == null ? new SqlParameter("@bgbh", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@bgbh", SqlDbType.VarChar, 50) { Value = model.bgbh },
                model.rwlx == null ? new SqlParameter("@rwlx", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@rwlx", SqlDbType.VarChar, 50) { Value = model.rwlx },
                model.cydh == null ? new SqlParameter("@cydh", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@cydh", SqlDbType.VarChar, 50) { Value = model.cydh },
                model.sjqy_mc == null ? new SqlParameter("@sjqy_mc", SqlDbType.VarChar, 200) { Value = DBNull.Value } : new SqlParameter("@sjqy_mc", SqlDbType.VarChar, 200) { Value = model.sjqy_mc },
                model.ypmc == null ? new SqlParameter("@ypmc", SqlDbType.VarChar, 100) { Value = DBNull.Value } : new SqlParameter("@ypmc", SqlDbType.VarChar, 100) { Value = model.ypmc },
                model.yp_ggxh == null ? new SqlParameter("@yp_ggxh", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@yp_ggxh", SqlDbType.VarChar, 50) { Value = model.yp_ggxh },
                model.yp_sl == null ? new SqlParameter("@yp_sl", SqlDbType.NVarChar, 1000) { Value = DBNull.Value } : new SqlParameter("@yp_sl", SqlDbType.NVarChar, 1000) { Value = model.yp_sl },
                model.yp_ddrq == null ? new SqlParameter("@yp_ddrq", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@yp_ddrq", SqlDbType.DateTime, 8) { Value = model.yp_ddrq },
                model.wtdw == null ? new SqlParameter("@wtdw", SqlDbType.VarChar, 100) { Value = DBNull.Value } : new SqlParameter("@wtdw", SqlDbType.VarChar, 100) { Value = model.wtdw },
                model.scdw == null ? new SqlParameter("@scdw", SqlDbType.VarChar, 200) { Value = DBNull.Value } : new SqlParameter("@scdw", SqlDbType.VarChar, 200) { Value = model.scdw },
                model.yp_bzq == null ? new SqlParameter("@yp_bzq", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@yp_bzq", SqlDbType.NVarChar, 100) { Value = model.yp_bzq },
                model.panding == null ? new SqlParameter("@panding", SqlDbType.VarChar, 20) { Value = DBNull.Value } : new SqlParameter("@panding", SqlDbType.VarChar, 20) { Value = model.panding },
                model.pz_time == null ? new SqlParameter("@pz_time", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@pz_time", SqlDbType.DateTime, 8) { Value = model.pz_time },
                model.pz_bz == null ? new SqlParameter("@pz_bz", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@pz_bz", SqlDbType.Int, -1) { Value = model.pz_bz },
                model.yp_scrq == null ? new SqlParameter("@yp_scrq", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@yp_scrq", SqlDbType.NVarChar, 100) { Value = model.yp_scrq },
                model.ypzt == null ? new SqlParameter("@ypzt", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@ypzt", SqlDbType.VarChar, 50) { Value = model.ypzt },
                model.yp_byl == null ? new SqlParameter("@yp_byl", SqlDbType.NVarChar, 1000) { Value = DBNull.Value } : new SqlParameter("@yp_byl", SqlDbType.NVarChar, 1000) { Value = model.yp_byl },
                model.Address == null ? new SqlParameter("@Address", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@Address", SqlDbType.NVarChar, 100) { Value = model.Address },
                model.DisposeWay == null ? new SqlParameter("@DisposeWay", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@DisposeWay", SqlDbType.NVarChar, 100) { Value = model.DisposeWay },
                model.DisposeResult == null ? new SqlParameter("@DisposeResult", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@DisposeResult", SqlDbType.Int, -1) { Value = model.DisposeResult },
                model.InDate == null ? new SqlParameter("@InDate", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@InDate", SqlDbType.DateTime, 8) { Value = model.InDate },
                model.Remark == null ? new SqlParameter("@Remark", SqlDbType.NVarChar, 200) { Value = DBNull.Value } : new SqlParameter("@Remark", SqlDbType.NVarChar, 200) { Value = model.Remark },
                model.GroupCode == null ? new SqlParameter("@GroupCode", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@GroupCode", SqlDbType.NVarChar, 100) { Value = model.GroupCode },
                model.Type == null ? new SqlParameter("@Type", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@Type", SqlDbType.Int, -1) { Value = model.Type },
                model.ExtendField1 == null ? new SqlParameter("@ExtendField1", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@ExtendField1", SqlDbType.NVarChar, 100) { Value = model.ExtendField1 },
                model.DisposeCause == null ? new SqlParameter("@DisposeCause", SqlDbType.NVarChar, 1000) { Value = DBNull.Value } : new SqlParameter("@DisposeCause", SqlDbType.NVarChar, 1000) { Value = model.DisposeCause },
                model.DisposeOpinion == null ? new SqlParameter("@DisposeOpinion", SqlDbType.NVarChar, 500) { Value = DBNull.Value } : new SqlParameter("@DisposeOpinion", SqlDbType.NVarChar, 500) { Value = model.DisposeOpinion },
                model.ExtendField2 == null ? new SqlParameter("@ExtendField2", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@ExtendField2", SqlDbType.Int, -1) { Value = model.ExtendField2 },
                model.expire == null ? new SqlParameter("@expire", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@expire", SqlDbType.DateTime, 8) { Value = model.expire },
                model.rest == null ? new SqlParameter("@rest", SqlDbType.Float, -1) { Value = DBNull.Value } : new SqlParameter("@rest", SqlDbType.Float, -1) { Value = model.rest },
                model.unit == null ? new SqlParameter("@unit", SqlDbType.VarChar, 10) { Value = DBNull.Value } : new SqlParameter("@unit", SqlDbType.VarChar, 10) { Value = model.unit },
                model.byl_rest == null ? new SqlParameter("@byl_rest", SqlDbType.Float, -1) { Value = DBNull.Value } : new SqlParameter("@byl_rest", SqlDbType.Float, -1) { Value = model.byl_rest }
            };
            int maxID;
            return int.TryParse(dbHelper.ExecuteScalar(sql, parameters), out maxID) ? maxID : -1;
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="model">AssetManage.Data.Model.AmSample实体类</param>
        public int Update(AssetManage.Data.Model.AmSample model)
        {
            string sql = @"UPDATE AmSample SET 
				bgbh=@bgbh,rwlx=@rwlx,cydh=@cydh,sjqy_mc=@sjqy_mc,ypmc=@ypmc,yp_ggxh=@yp_ggxh,yp_sl=@yp_sl,yp_ddrq=@yp_ddrq,wtdw=@wtdw,scdw=@scdw,yp_bzq=@yp_bzq,panding=@panding,pz_time=@pz_time,pz_bz=@pz_bz,yp_scrq=@yp_scrq,ypzt=@ypzt,yp_byl=@yp_byl,Address=@Address,DisposeWay=@DisposeWay,DisposeResult=@DisposeResult,InDate=@InDate,Remark=@Remark,GroupCode=@GroupCode,Type=@Type,ExtendField1=@ExtendField1,ExtendField2=@ExtendField2,DisposeCause=@DisposeCause,DisposeOpinion=@DisposeOpinion,expire=@expire,rest=@rest,unit=@unit,byl_rest=@byl_rest
				WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
                model.bgbh == null ? new SqlParameter("@bgbh", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@bgbh", SqlDbType.VarChar, 50) { Value = model.bgbh },
                model.rwlx == null ? new SqlParameter("@rwlx", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@rwlx", SqlDbType.VarChar, 50) { Value = model.rwlx },
                model.cydh == null ? new SqlParameter("@cydh", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@cydh", SqlDbType.VarChar, 50) { Value = model.cydh },
                model.sjqy_mc == null ? new SqlParameter("@sjqy_mc", SqlDbType.VarChar, 200) { Value = DBNull.Value } : new SqlParameter("@sjqy_mc", SqlDbType.VarChar, 200) { Value = model.sjqy_mc },
                model.ypmc == null ? new SqlParameter("@ypmc", SqlDbType.VarChar, 100) { Value = DBNull.Value } : new SqlParameter("@ypmc", SqlDbType.VarChar, 100) { Value = model.ypmc },
                model.yp_ggxh == null ? new SqlParameter("@yp_ggxh", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@yp_ggxh", SqlDbType.VarChar, 50) { Value = model.yp_ggxh },
                model.yp_sl == null ? new SqlParameter("@yp_sl", SqlDbType.NVarChar, 1000) { Value = DBNull.Value } : new SqlParameter("@yp_sl", SqlDbType.NVarChar, 1000) { Value = model.yp_sl },
                model.yp_ddrq == null ? new SqlParameter("@yp_ddrq", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@yp_ddrq", SqlDbType.DateTime, 8) { Value = model.yp_ddrq },
                model.wtdw == null ? new SqlParameter("@wtdw", SqlDbType.VarChar, 100) { Value = DBNull.Value } : new SqlParameter("@wtdw", SqlDbType.VarChar, 100) { Value = model.wtdw },
                model.scdw == null ? new SqlParameter("@scdw", SqlDbType.VarChar, 200) { Value = DBNull.Value } : new SqlParameter("@scdw", SqlDbType.VarChar, 200) { Value = model.scdw },
                model.yp_bzq == null ? new SqlParameter("@yp_bzq", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@yp_bzq", SqlDbType.NVarChar, 100) { Value = model.yp_bzq },
                model.panding == null ? new SqlParameter("@panding", SqlDbType.VarChar, 20) { Value = DBNull.Value } : new SqlParameter("@panding", SqlDbType.VarChar, 20) { Value = model.panding },
                model.pz_time == null ? new SqlParameter("@pz_time", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@pz_time", SqlDbType.DateTime, 8) { Value = model.pz_time },
                model.pz_bz == null ? new SqlParameter("@pz_bz", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@pz_bz", SqlDbType.Int, -1) { Value = model.pz_bz },
                model.yp_scrq == null ? new SqlParameter("@yp_scrq", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@yp_scrq", SqlDbType.NVarChar, 100) { Value = model.yp_scrq },
                model.ypzt == null ? new SqlParameter("@ypzt", SqlDbType.VarChar, 50) { Value = DBNull.Value } : new SqlParameter("@ypzt", SqlDbType.VarChar, 50) { Value = model.ypzt },
                model.yp_byl == null ? new SqlParameter("@yp_byl", SqlDbType.NVarChar, 1000) { Value = DBNull.Value } : new SqlParameter("@yp_byl", SqlDbType.NVarChar, 1000) { Value = model.yp_byl },
                model.Address == null ? new SqlParameter("@Address", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@Address", SqlDbType.NVarChar, 100) { Value = model.Address },
                model.DisposeWay == null ? new SqlParameter("@DisposeWay", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@DisposeWay", SqlDbType.NVarChar, 100) { Value = model.DisposeWay },
                model.DisposeResult == null ? new SqlParameter("@DisposeResult", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@DisposeResult", SqlDbType.Int, -1) { Value = model.DisposeResult },
                model.InDate == null ? new SqlParameter("@InDate", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@InDate", SqlDbType.DateTime, 8) { Value = model.InDate },
                model.Remark == null ? new SqlParameter("@Remark", SqlDbType.NVarChar, 200) { Value = DBNull.Value } : new SqlParameter("@Remark", SqlDbType.NVarChar, 200) { Value = model.Remark },
                model.GroupCode == null ? new SqlParameter("@GroupCode", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@GroupCode", SqlDbType.NVarChar, 100) { Value = model.GroupCode },
                model.Type == null ? new SqlParameter("@Type", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@Type", SqlDbType.Int, -1) { Value = model.Type },
                model.ExtendField1 == null ? new SqlParameter("@ExtendField1", SqlDbType.NVarChar, 100) { Value = DBNull.Value } : new SqlParameter("@ExtendField1", SqlDbType.NVarChar, 100) { Value = model.ExtendField1 },
                model.ExtendField2 == null ? new SqlParameter("@ExtendField2", SqlDbType.Int, -1) { Value = DBNull.Value } : new SqlParameter("@ExtendField2", SqlDbType.Int, -1) { Value = model.ExtendField2 },
                model.DisposeCause == null ? new SqlParameter("@DisposeCause", SqlDbType.NVarChar, 1000) { Value = DBNull.Value } : new SqlParameter("@DisposeCause", SqlDbType.NVarChar, 1000) { Value = model.DisposeCause },
                model.DisposeOpinion == null ? new SqlParameter("@DisposeOpinion", SqlDbType.NVarChar, 500) { Value = DBNull.Value } : new SqlParameter("@DisposeOpinion", SqlDbType.NVarChar, 500) { Value = model.DisposeOpinion },
                model.expire == null ? new SqlParameter("@expire", SqlDbType.DateTime, 8) { Value = DBNull.Value } : new SqlParameter("@expire", SqlDbType.DateTime, 8) { Value = model.expire },
                model.rest == null ? new SqlParameter("@rest", SqlDbType.Float, -1) { Value = DBNull.Value } : new SqlParameter("@rest", SqlDbType.Float, -1) { Value = model.rest },
                model.unit == null ? new SqlParameter("@unit", SqlDbType.VarChar, 10) { Value = DBNull.Value } : new SqlParameter("@unit", SqlDbType.VarChar, 10) { Value = model.unit },
                model.byl_rest == null ? new SqlParameter("@byl_rest", SqlDbType.Float, -1) { Value = DBNull.Value } : new SqlParameter("@byl_rest", SqlDbType.Float, -1) { Value = model.byl_rest },
                new SqlParameter("@ID", SqlDbType.Int, -1){ Value = model.ID }
            };
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(int id)
        {
            string sql = "DELETE FROM AmSample WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@ID", SqlDbType.Int){ Value = id }
            };
            return dbHelper.Execute(sql, parameters);
        }
        /// <summary>
        /// 将DataRedar转换为List
        /// </summary>
        private List<AssetManage.Data.Model.AmSample> DataReaderToList(SqlDataReader dataReader)
        {
            List<AssetManage.Data.Model.AmSample> List = new List<AssetManage.Data.Model.AmSample>();
            AssetManage.Data.Model.AmSample model = null;
            while (dataReader.Read())
            {
                model = new AssetManage.Data.Model.AmSample();
                model.ID = dataReader.GetInt32(0);
                if (!dataReader.IsDBNull(1))
                    model.bgbh = dataReader.GetString(1);
                if (!dataReader.IsDBNull(2))
                    model.rwlx = dataReader.GetString(2);
                if (!dataReader.IsDBNull(3))
                    model.cydh = dataReader.GetString(3);
                if (!dataReader.IsDBNull(4))
                    model.sjqy_mc = dataReader.GetString(4);
                if (!dataReader.IsDBNull(5))
                    model.ypmc = dataReader.GetString(5);
                if (!dataReader.IsDBNull(6))
                    model.yp_ggxh = dataReader.GetString(6);
                if (!dataReader.IsDBNull(7))
                    model.yp_sl = dataReader.GetString(7);
                if (!dataReader.IsDBNull(8))
                    model.yp_ddrq = dataReader.GetDateTime(8);
                if (!dataReader.IsDBNull(9))
                    model.wtdw = dataReader.GetString(9);
                if (!dataReader.IsDBNull(10))
                    model.scdw = dataReader.GetString(10);
                if (!dataReader.IsDBNull(11))
                    model.yp_bzq = dataReader.GetString(11);
                if (!dataReader.IsDBNull(12))
                    model.panding = dataReader.GetString(12);
                if (!dataReader.IsDBNull(13))
                    model.pz_time = dataReader.GetDateTime(13);
                if (!dataReader.IsDBNull(14))
                    model.pz_bz = dataReader.GetInt32(14);
                if (!dataReader.IsDBNull(15))
                    model.yp_scrq = dataReader.GetString(15);
                if (!dataReader.IsDBNull(16))
                    model.ypzt = dataReader.GetString(16);
                if (!dataReader.IsDBNull(17))
                    model.yp_byl = dataReader.GetString(17);
                if (!dataReader.IsDBNull(18))
                    model.Address = dataReader.GetString(18);
                if (!dataReader.IsDBNull(19))
                    model.DisposeWay = dataReader.GetString(19);
                if (!dataReader.IsDBNull(20))
                    model.DisposeResult = dataReader.GetInt32(20);

                if (!dataReader.IsDBNull(21))
                    model.InDate = dataReader.GetDateTime(21);
                if (!dataReader.IsDBNull(22))
                    model.Remark = dataReader.GetString(22);
                if (!dataReader.IsDBNull(23))
                    model.GroupCode = dataReader.GetString(23);
                if (!dataReader.IsDBNull(24))
                    model.Type = dataReader.GetInt32(24);
                if (!dataReader.IsDBNull(25))
                    model.ExtendField1 = dataReader.GetString(25);
                if (!dataReader.IsDBNull(26))
                    model.ExtendField2 = dataReader.GetInt32(26);
                if (!dataReader.IsDBNull(27))
                    model.DisposeCause = dataReader.GetString(27);
                if (!dataReader.IsDBNull(28))
                    model.DisposeOpinion = dataReader.GetString(28);
                if (!dataReader.IsDBNull(29))
                    model.expire = dataReader.GetDateTime(29);
                if (!dataReader.IsDBNull(30))
                {
                    var r = dataReader.GetDouble(30);
                    model.rest = (float)r;
                }
                    
                if (!dataReader.IsDBNull(31))
                    model.unit = dataReader.GetString(31);
                if (!dataReader.IsDBNull(32))
                    model.byl_rest = (float)dataReader.GetDouble(32);
                List.Add(model);
            }
            return List;
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<AssetManage.Data.Model.AmSample> GetAll()
        {
            string sql = "SELECT * FROM AmSample";
            SqlDataReader dataReader = dbHelper.GetDataReader(sql);
            List<AssetManage.Data.Model.AmSample> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List;
        }
        /// <summary>
        /// 查询记录数
        /// </summary>
        public long GetCount()
        {
            string sql = "SELECT COUNT(*) FROM AmSample";
            long count;
            return long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;
        }
        /// <summary>
        /// 根据主键查询一条记录
        /// </summary>
        public AssetManage.Data.Model.AmSample Get(int id)
        {
            string sql = "SELECT * FROM AmSample WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@ID", SqlDbType.Int){ Value = id }
            };
            SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<AssetManage.Data.Model.AmSample> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }
        /// <summary>
        /// 根据报告编号查询一条记录
        /// </summary>
        public AssetManage.Data.Model.AmSample GetByBgbh(string bgbh,int type)
        {
            string sql = "SELECT * FROM AmSample WHERE bgbh=@bgbh and type=@type";
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@bgbh", SqlDbType.NVarChar){ Value = bgbh },
                 new SqlParameter("@type", SqlDbType.Int){ Value = type }
            };
            SqlDataReader dataReader = dbHelper.GetDataReader(sql, parameters);
            List<AssetManage.Data.Model.AmSample> List = DataReaderToList(dataReader);
            dataReader.Close();
            return List.Count > 0 ? List[0] : null;
        }

        public List<AssetManage.Data.Model.AmSample> GetByBgbhs(string bgbhs)
        {
            string sql = string.Format("select * from AmSample where '{0}' like '%'+bgbh+'%'", bgbhs);
            var dt = dbHelper.GetDataReader(sql);
            return DataReaderToList(dt);
        }

    }
}