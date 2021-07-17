using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebMvc.Areas.AssetManage.Data.Business
{
    public class AmSample
    {
        private AssetManage.Data.MSSQL.AmSample dataAmSample;
        public AmSample()
        {
            this.dataAmSample = new AssetManage.Data.MSSQL.AmSample();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(AssetManage.Data.Model.AmSample model)
        {
            return dataAmSample.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(AssetManage.Data.Model.AmSample model)
        {
            return dataAmSample.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<AssetManage.Data.Model.AmSample> GetAll()
        {
            return dataAmSample.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public AssetManage.Data.Model.AmSample Get(int id)
        {
            return dataAmSample.Get(id);
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public AssetManage.Data.Model.AmSample GetByBgbh(string  bgbh,int type)
        {
            return dataAmSample.GetByBgbh(bgbh,type);
        }

        public List<AssetManage.Data.Model.AmSample> GetByBgbhs(string bgbhs)
        {
            return dataAmSample.GetByBgbhs(bgbhs);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(int id)
        {
            return dataAmSample.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataAmSample.GetCount();
        }
        /// <summary>
        /// 样品报批导入
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public int InExcelData(string file,out string msg)
        {
            int count = 0;
            msg = "";
            try
            {
                DataTable dt = YJ.Utility.NPOIHelper.ReadToDataTable(file);
                if (dt.Rows.Count == 0)
                {
                    msg = "未发现要导入的数据";
                    return count;
                }
            
                AssetManage.Data.Model.AmSample sam;
                string group = GetTimeStamp();
                foreach (DataRow item in dt.Rows)
                {
                    sam =Get(item[0].ToString().ToInt32());
                    if (sam != null)
                    {
                        sam.DisposeResult = item["处理结果"].ToString().ToInt32();
                        sam.DisposeCause = item["处理原因"].ToString();
                        sam.DisposeOpinion = item["处理意见"].ToString();
                        sam.Remark = item["备注"].ToString();
                        int wo=Update(sam);
                        count++;
                        continue;
                    }
                    msg += msg+item["报告编号"]+"未导入成功\n";
                    //samList.Add(sam);
                }
                YJ.Platform.Log.Add("样品报批导入操作", string.Format("操作员：{0}\n样品码信息：{1}",YJ.Platform.Users.CurrentUserName,dt.Serialize()), YJ.Platform.Log.Types.信息管理);
                return count;
            }
            catch (Exception err)
            {

                msg = err.Message;
                return count;
            }
        }
        /// <summary>
        /// 固定资产（办公用品）导入
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public int InExcelData1(string file, out string msg)
        {
            int count = 0;
            msg = "";
            try
            {
                DataTable dt = YJ.Utility.NPOIHelper.ReadToDataTable(file);
                if (dt.Rows.Count == 0)
                {
                    msg = "未发现要导入的数据";
                    
                    return count;
                }
                Model.AmAssets amAssets;
                Model.AmAssetInOut amAssetInOut;
                AmAssets bAmAssets = new AmAssets();
                AmAssetInOut bAssetInOut = new AmAssetInOut();
               
                    foreach (DataRow item in dt.Rows)
                    {
                        amAssets = new Model.AmAssets();
                        amAssets.Name = item[0].ToString();
                        amAssets.TypeId = item[1].ToString();
                        amAssets.Specs = item[2].ToString();
                        amAssets.Brand = item[3].ToString();
                        amAssets.MeasureUnit = item[4].ToString();
                        amAssets.Money = item[5].ToString().ToDecimal();
                        amAssets.PurchaseDate = item[6].ToString().ToDateTime();
                        amAssets.IsFixedAsset = item[7].ToString().ToInt32();
                        amAssets.FixedAssetCode = item[8].ToString();
                        amAssets.Status = 0;
                        amAssets.Module = 0;

                        amAssets.CreateDate = DateTime.Now;
                        using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                        {
                            int aaid = bAmAssets.Add(amAssets);
                            amAssetInOut = new Model.AmAssetInOut();
                            amAssetInOut.UseUId=item[9].ToString();
                            amAssetInOut.Type = "入库";
                            amAssetInOut.ExtendField2= item[10].ToString();
                            amAssetInOut.UseDate = item[11].ToString().ToDateTime();
                            amAssetInOut.Address = item[12].ToString();
                            amAssetInOut.Remark = item[13].ToString();
                            amAssetInOut.AmAssetsId = aaid;
                            amAssetInOut.CreateDate = DateTime.Now;
                            bAssetInOut.Add(amAssetInOut);
                            scope.Complete();
                            count++;
                        }
                }
                msg = "录入成功";
                return count;
            }
            catch (Exception err)
            {

                msg = err.Message;
                return count;
            }
        }
        /// <summary>
        /// 固定资产（仪器设备账面）导入
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public int InExcelData2(string file, out string msg)
        {
            int count = 0;
            msg = "";
            try
            {
                DataTable dt = YJ.Utility.NPOIHelper.ReadToDataTable(file);
                if (dt.Rows.Count == 0)
                {
                    msg = "未发现要导入的数据";

                    return count;
                }
                Model.AmAssets amAssets;
                Model.AmAssetInOut amAssetInOut;
                AmAssets bAmAssets = new AmAssets();
                AmAssetInOut bAssetInOut = new AmAssetInOut();

                foreach (DataRow item in dt.Rows)
                {
                    amAssets = new Model.AmAssets();
                    amAssets.Module = 1;
                    amAssets.Name = item[0].ToString();
                    amAssets.TypeId = item[1].ToString();
                    amAssets.Specs = item[2].ToString();
                    amAssets.Brand = item[3].ToString();
                    amAssets.MeasureUnit = item[4].ToString();
                    amAssets.Money = item[5].ToString().ToDecimal();
                    amAssets.PurchaseDate = item[6].ToString().ToDateTime();
                    amAssets.IsFixedAsset = item[7].ToString().ToInt32();
                    amAssets.FixedAssetCode = item[8].ToString();
                    amAssets.AppraisalUnit = item[9].ToString();
                    amAssets.Remark = item[10].ToString();
                    amAssets.Status = 0;
                    amAssets.CreateDate = DateTime.Now;
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        int aaid = bAmAssets.Add(amAssets);
                        amAssetInOut = new Model.AmAssetInOut();
                        amAssetInOut.UseUId = item[11].ToString();
                        amAssetInOut.Type = "入库";
                        amAssetInOut.ExtendField2 = item[12].ToString();
                        amAssetInOut.UseDate = item[13].ToString().ToDateTime();
                        amAssetInOut.Address = item[14].ToString();
                        amAssetInOut.Remark = item[15].ToString();
                        amAssetInOut.AmAssetsId = aaid;
                        amAssetInOut.CreateDate = DateTime.Now;
                        bAssetInOut.Add(amAssetInOut);
                        scope.Complete();
                        count++;
                    }
                }
                msg = "录入成功";
                return count;
            }
            catch (Exception err)
            {

                msg = err.Message;
                return count;
            }
        }
        /// <summary> 
        /// 获取时间戳 
        /// </summary> 
        /// <returns></returns> 
        private  string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
    }
}