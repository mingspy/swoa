using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
namespace WebMvc.Areas.AssetManage.Controllers
{
    public class AmSampleController : MyController
    {
        //
        // GET: /AssetManage/AmSample/
        [MyAttribute(CheckApp = false, CheckUrl = false, CheckLogin = false)]
        public ActionResult Index()
        {
            string bgbh = Request.QueryString.Get("bgbh");
            ViewBag.index_bgbh = bgbh != null? bgbh : "";
            return View();
        }

        public ActionResult GetBatchQrCode()
        {
            ;
            return View();
        }

        [MyAttribute(CheckApp = false, CheckUrl = false, CheckLogin = false)]
        public ActionResult ScanQr()
        {
            string AssetsID = Request.QueryString["bgbh"];
            ViewBag.index_bgbh = AssetsID;
            return View();

        }

        [HttpPost]
        [MyAttribute(CheckApp = false, CheckUrl = false)]
        public string AddSample(string address, string bgbhs, int type, string group, string capacity, string remark)
        {
            YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper(System.Configuration.ConfigurationManager.ConnectionStrings["SampleConnection"].ConnectionString);
            YJ.Data.MSSQL.DBHelper myDb = new YJ.Data.MSSQL.DBHelper();
            try
            {
                string cloneBgbhs = bgbhs;
                int count = 0;
                string sql = string.Format("select * from OA_SampleView where '{0}' like '%'+bgbh+'%'", bgbhs);
                DataTable dt = db.GetDataTable(sql);
                AssetManage.Data.Model.AmSample sam;
                foreach (DataRow item in dt.Rows)
                {
                    var bgbh = item["bgbh"].ToString();
                    sam = new Data.Business.AmSample().GetByBgbh(bgbh, type);
                    if (sam != null)
                    {
                        throw new DuplicateNameException(string.Format("禁止样品重复入库,{0}", bgbh));
                    }
                    sam = new Data.Model.AmSample();
                    sam.Address = address;
                    if (remark != null)
                    {
                        sam.Remark = remark;
                    }

                    sam.bgbh = item["bgbh"].ToString();
                    sam.Type = type;
                    sam.GroupCode = group;
                    sam.rwlx = item["rwlx"].ToString();
                    sam.cydh = item["cydh"].ToString();
                    sam.sjqy_mc = item["sjqy_mc"].ToString();
                    sam.ypmc = item["ypmc"].ToString();
                    sam.yp_ggxh = item["yp_ggxh"].ToString();
                    sam.yp_sl = item["yp_sl"].ToString();
                    sam.yp_ddrq = item["yp_ddrq"].ToString().ToDateTime();
                    sam.wtdw = item["wtdw"].ToString();
                    sam.scdw = item["scdw"].ToString();
                    sam.yp_bzq = item["yp_bzq"].ToString();
                    sam.panding = item["panding"].ToString();
                    sam.pz_time = item["pz_time"].ToString().IsNullOrEmpty() ? DateTime.Now : item["pz_time"].ToString().ToDateTime();
                    sam.pz_bz = item["pz_bz"].ToString().ToInt32();
                    sam.yp_scrq = item["yp_scrq"].ToString();
                    sam.ypzt = item["ypzt"].ToString();
                    sam.yp_byl = item["yp_byl"].ToString();
                    sam.InDate = DateTime.Now;
                    if (sam.yp_sl != null)
                    {
                        var m = sam.yp_sl.ToNumUnit();
                        sam.rest = m.Num;
                        sam.unit = m.Unit;
                    }

                    if (sam.yp_byl != null)
                    {
                        var m = sam.yp_byl.ToNumUnit();
                        sam.byl_rest = m.Num;
                    }

                    DateTime expire = DateTime.Now.AddDays(90);
                    if (sam.yp_bzq != null)
                    {
                        DateTime stime = sam.yp_ddrq != null ? (DateTime)sam.yp_ddrq : (DateTime)sam.InDate;
                        if (sam.yp_scrq != null)
                        {
                            var s = sam.yp_scrq.ToDateTime();
                            if (s != DateTime.MinValue)
                            {
                                stime = s;
                            }
                        }
                        expire = sam.yp_bzq.ToExpireDate(stime);
                    }
                    sam.expire = expire;

                    int ret = 0;
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        myDb.Execute(string.Format("INSERT INTO [AmSampleInOut]([AmsAampleId],[UseUId],[Address1],[Remark])VALUES('{0}','u_{1}','{2}', '样品入库')",
                             item["bgbh"].ToString(), CurrentUser.ID, address));
                        ret = new Data.Business.AmSample().Add(sam);

                        scope.Complete();
                    }
                    if (ret > 0)
                    {
                        count++;
                        bool isContains = bgbhs.IndexOf(sam.bgbh.Trim() + ",", StringComparison.OrdinalIgnoreCase) >= 0;
                        if (isContains)
                        {
                            bgbhs = bgbhs.Replace(sam.bgbh.Trim() + ",", "");
                        }
                        else
                        {
                            bgbhs = bgbhs.Replace(sam.bgbh.Trim(), "");
                        }
                    }


                    //samList.Add(sam);

                }
                if (count > 0)
                {
                    string sql2 = string.Format("MERGE INTO [AmAddress] as T"
                        + " USING(SELECT '{0}' AS Address, '{1}' AS Capacity) AS S"
                        + " ON T.Address = S.Address "
                        + " WHEN MATCHED THEN"
                        + " UPDATE SET T.Capacity = S.Capacity"
                        + " WHEN NOT MATCHED THEN"
                        + " INSERT([Address], [Capacity]) VALUES(S.Address, S.Capacity); ", address, capacity);
                    myDb.Execute(sql2);
                }
                YJ.Platform.Log.Add("样品扫码入库", string.Format("操作员：{0}\n原始样品码:{1}\n成功添加条数：{2}\n添加失败样品码信息：{3}", CurrentUserName, cloneBgbhs, count, bgbhs), YJ.Platform.Log.Types.信息管理);
                return "{\"data\":" + count + ",\"status\":1,\"msg\":\"" + bgbhs + "\"}";
            }
            catch (Exception e)
            {

                return "{\"status\":0,\"msg\":\"" + e.Message + "\"}";
            }
        }

        [HttpPost]
        [MyAttribute(CheckApp = false, CheckUrl = false)]
        public string ExSample(string address, string bgbhs, string group, string capacity, string remark, string address2, string capacity2)
        {
            YJ.Data.MSSQL.DBHelper myDb = new YJ.Data.MSSQL.DBHelper();
            try
            {
                string cloneBgbhs = bgbhs;
                var samFactor = new Data.Business.AmSample();
                int count = 0;
                List<AssetManage.Data.Model.AmSample> sams = samFactor.GetByBgbhs(bgbhs);
                foreach (var sam in sams)
                {
                    int ret = 0;
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        myDb.Execute(string.Format("INSERT INTO [AmSampleInOut]([AmsAampleId],[UseUId],[Address],[Address1],[Remark])VALUES('{0}','u_{1}','{2}','{3}', '样品转移')",
                            sam.bgbh, CurrentUser.ID, sam.Address, address2));
                        string orgAddres = sam.Address;
                        string sql2 = string.Format("MERGE INTO [AmAddress] as T"
                            + " USING(SELECT '{0}' AS Address, '{1}' AS Capacity) AS S"
                            + " ON T.Address = S.Address "
                            + " WHEN MATCHED THEN"
                            + " UPDATE SET T.Capacity = S.Capacity"
                            + " WHEN NOT MATCHED THEN"
                            + " INSERT([Address], [Capacity]) VALUES(S.Address, S.Capacity); ", orgAddres, capacity);
                        myDb.Execute(sql2);

                        sam.Address = address2;
                        if (remark != null)
                        {
                            sam.Remark = remark;
                        }
                        ret = samFactor.Update(sam);
                        scope.Complete();
                    }

                    if (ret > 0)
                    {
                        count++;
                        bool isContains = bgbhs.IndexOf(sam.bgbh.Trim() + ",", StringComparison.OrdinalIgnoreCase) >= 0;
                        if (isContains)
                        {
                            bgbhs = bgbhs.Replace(sam.bgbh.Trim() + ",", "");
                        }
                        else
                        {
                            bgbhs = bgbhs.Replace(sam.bgbh.Trim(), "");
                        }
                    }

                }
                if (count > 0)
                {
                    string sql2 = string.Format("MERGE INTO [AmAddress] as T"
                        + " USING(SELECT '{0}' AS Address, '{1}' AS Capacity) AS S"
                        + " ON T.Address = S.Address "
                        + " WHEN MATCHED THEN"
                        + " UPDATE SET T.Capacity = S.Capacity"
                        + " WHEN NOT MATCHED THEN"
                        + " INSERT([Address], [Capacity]) VALUES(S.Address, S.Capacity); ", address2, capacity2);
                    myDb.Execute(sql2);
                }
                YJ.Platform.Log.Add("样品转移", string.Format("操作员：{0}\n原始样品码:{1}\n成功转移条数：{2}\n转移失败样品码信息：{3}", CurrentUserName, cloneBgbhs, count, bgbhs), YJ.Platform.Log.Types.信息管理);
                return "{\"data\":" + count + ",\"status\":1,\"msg\":\"" + bgbhs + "\"}";
            }
            catch (Exception e)
            {

                return "{\"status\":0,\"msg\":\"" + e.Message + "\"}";
            }
        }


        [HttpPost]
        [MyAttribute(CheckApp = false, CheckUrl = false)]
        public string DrawSample(string address, string bgbhs, string group, string capacity, string remark, string sample_num)
        {
            YJ.Data.MSSQL.DBHelper myDb = new YJ.Data.MSSQL.DBHelper();
            try
            {
                string cloneBgbhs = bgbhs;
                var samFactor = new Data.Business.AmSample();
                int count = 0;
                List<AssetManage.Data.Model.AmSample> sams = samFactor.GetByBgbhs(bgbhs);
                foreach (var sam in sams)
                {
                    int ret = 0;
                    //sam.Address = address;
                    if (remark != null)
                    {
                        sam.Remark = remark;
                    }

                    float num = 0;

                    if (sample_num != null)
                    {
                        decimal t;
                        if (decimal.TryParse(sample_num, out t))
                        {
                            num = (float)t;
                        }
                    }

                    string info;
                    if (sam.rest != null)
                    {
                        float org = (float)sam.rest;
                        info = string.Format("样品领用，数量{0}，剩余{1}", num, org - num);
                        sam.rest -= num;
                    }
                    else
                    {
                        info = string.Format("样品领用，数量{0}，剩余0", num);
                    }


                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        myDb.Execute(string.Format("INSERT INTO [AmSampleInOut]([AmsAampleId],[UseUId],[Address],[Address1],[Remark])VALUES('{0}','u_{1}','{2}', '{3}', '{4}')",
                            sam.bgbh, CurrentUser.ID, sam.Address, address, info));
                        ret = samFactor.Update(sam);
                        scope.Complete();
                    }


                    if (ret > 0)
                    {
                        count++;
                        bool isContains = bgbhs.IndexOf(sam.bgbh.Trim() + ",", StringComparison.OrdinalIgnoreCase) >= 0;
                        if (isContains)
                        {
                            bgbhs = bgbhs.Replace(sam.bgbh.Trim() + ",", "");
                        }
                        else
                        {
                            bgbhs = bgbhs.Replace(sam.bgbh.Trim(), "");
                        }
                    }

                }
                if (count > 0)
                {
                    string sql2 = string.Format("MERGE INTO [AmAddress] as T"
                        + " USING(SELECT '{0}' AS Address, '{1}' AS Capacity) AS S"
                        + " ON T.Address = S.Address "
                        + " WHEN MATCHED THEN"
                        + " UPDATE SET T.Capacity = S.Capacity"
                        + " WHEN NOT MATCHED THEN"
                        + " INSERT([Address], [Capacity]) VALUES(S.Address, S.Capacity); ", address, capacity);
                    myDb.Execute(sql2);
                }
                YJ.Platform.Log.Add("样品领用", string.Format("操作员：{0}\n原始样品码:{1}\n成功转移条数：{2}\n转移失败样品码信息：{3}", CurrentUserName, cloneBgbhs, count, bgbhs), YJ.Platform.Log.Types.信息管理);
                return "{\"data\":" + count + ",\"status\":1,\"msg\":\"" + bgbhs + "\"}";
            }
            catch (Exception e)
            {

                return "{\"status\":0,\"msg\":\"" + e.Message + "\"}";
            }
        }

        [HttpPost]
        [MyAttribute(CheckApp = false, CheckUrl = false)]
        public string SyncResult(string bgbh, string sid)
        {
            YJ.Data.MSSQL.DBHelper myDb = new YJ.Data.MSSQL.DBHelper();
            try
            {
                string cloneBgbhs = bgbh;
                var samFactor = new Data.Business.AmSample();
                var sql = string.Format("select * from AmSampleSync where id = {0}", sid);
                int count = 0;
                DataTable dt = myDb.GetDataTable(sql);
                foreach (DataRow item in dt.Rows)
                {
                    string panding = item["panding"].ToString();
                    var pz_time = item["pz_time"].ToString().IsNullOrEmpty() ? DateTime.Now : item["pz_time"].ToString().ToDateTime();
                    var pz_bz = item["pz_bz"].ToString().ToInt32();
                    var ypzt = item["ypzt"].ToString();
                    string info = string.Format("同步样品结果：{0}，判定时间{1}, 判定标志{2}, 样品状态{3}", panding, pz_time, pz_bz, ypzt);
                    List<AssetManage.Data.Model.AmSample> sams = samFactor.GetByBgbhs(bgbh);
                    foreach (var sam in sams)
                    {
                        sam.panding = panding;
                        sam.pz_time = pz_time;
                        sam.pz_bz = pz_bz;
                        sam.ypzt = ypzt;

                        using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                        {
                            myDb.Execute(string.Format("INSERT INTO [AmSampleInOut]([AmsAampleId],[UseUId],[Remark])VALUES('{0}','u_{1}','{2}')",
                                sam.bgbh, CurrentUser.ID, info));
                            if (samFactor.Update(sam) > 0)
                            {
                                count++;
                            }
                            scope.Complete();
                        }
                    }
                }
                if (count > 0)
                {
                    string sql2 = string.Format("update AmSampleSync set status=1 where id={0}", sid);
                    myDb.Execute(sql2);
                }
                YJ.Platform.Log.Add("样品状态更新", string.Format("操作员：{0}\n原始样品码:{1}\n成功更新条数：{2}\n", CurrentUserName, cloneBgbhs, count), YJ.Platform.Log.Types.信息管理);
                return "{\"data\":" + count + ",\"status\":1,\"msg\":\"" + bgbh + "\"}";
            }
            catch (Exception e)
            {

                return "{\"status\":0,\"msg\":\"" + e.Message + "\"}";
            }
        }

        //[HttpPost]
        [MyAttribute(CheckApp = false, CheckUrl = false)]
        public ActionResult SendResult(string bgbh, string type)
        {
            //YJ.Data.MSSQL.DBHelper myDb = new YJ.Data.MSSQL.DBHelper();

            var samFactor = new Data.Business.AmSample();
            List<AssetManage.Data.Model.AmSample> sams = samFactor.GetByBgbhs(bgbh);
            ViewBag.Sample = sams[0];
            ViewBag.Type = type;
            return View();
        }
    }
}
