using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using LitJson;
using System.IO;

namespace WebMvc.Areas.AssetManage.Controllers
{
    public class AmSampleController : MyController
    {
        //
        // GET: /AssetManage/AmSample/
        [MyAttribute(CheckApp = false, CheckUrl = false, CheckLogin = false)]
        public ActionResult Index()
        {
            string loginMsg;
            if (!WebMvc.Common.Tools.CheckLogin(out loginMsg) && YJ.Platform.WeiXin.Organize.CurrentUserID.IsEmptyGuid())
            {
                if (YJ.Platform.WeiXin.Config.IsUse)
                {
                    System.Web.HttpContext.Current.Response.Cookies.Add(new System.Web.HttpCookie("LastURL", System.Web.HttpContext.Current.Request.Url.PathAndQuery));
                    string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=";
                    url += YJ.Platform.WeiXin.Config.SuitID;
                    url += "&redirect_uri=" + YJ.Platform.WeiXin.Config.GetAccountUrl + "&response_type=code&scope=snsapi_base&state=a#wechat_redirect";
                    //Log.Add("调用了微信获取人员CODE", url, Log.Types.微信企业号, url);
                    return Redirect(url);
                }
            }


            string bgbh = Request.QueryString.Get("bgbh");
            ViewBag.index_bgbh = bgbh != null ? bgbh : "";
            return View();
        }

        public ActionResult GetBatchQrCode()
        {
            return View();
        }

        public ActionResult GetSingleQrCode()
        {
            return View();
        }

        public ActionResult PrintQrCodes()
        {
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
        [MyAttribute(CheckApp = false, CheckUrl = false, CheckLogin = false)]
        public string getSamples(string from_date, string print_neq)
        {
            YJ.Data.MSSQL.DBHelper myDb = new YJ.Data.MSSQL.DBHelper();
            string sql = string.Format("select * from AmSample where yp_ddrq >='{0}' and (has_print is null or has_print !={1})",
                from_date, print_neq);
            DataTable dt = myDb.GetDataTable(sql);

            var r = "{\"data\":" + dt.ToJsonString() + ",\"status\":1,\"msg\":\"正常\"}";
            //System.Diagnostics.Debug.WriteLine(r);
            return r;
        }


        public ActionResult PrintZBQrCodes()
        {
            return View();
        }

        public ActionResult inSam()
        {
            return View();
        }

        public JsonData getSamsByID(string uuid, string bag_code)
        {
            JsonData jdarr = new JsonData();
            if (string.IsNullOrEmpty(uuid) && string.IsNullOrEmpty(bag_code))
            {
                return jdarr;
            }

            var uuids = "";
            if (!string.IsNullOrEmpty(uuid))
            {
                var ss = uuid.Split(' ');
                foreach (var u in ss)
                {
                    var uu = u.Trim();
                    if (uu.Length < 1)
                    {
                        continue;
                    }

                    if (uuids.Length > 0)
                    {
                        uuids += ",";
                    }
                    uuids += string.Format("'{0}'", uu);
                }
            }

            YJ.Data.MSSQL.DBHelper myDb = new YJ.Data.MSSQL.DBHelper();
            string sql = "select * from AmSampleDetail where ";
            string w = "";
            if (!string.IsNullOrEmpty(uuids))
            {
                w = string.Format("ID in ({0})", uuids);
            }

            if (!string.IsNullOrEmpty(bag_code))
            {
                bag_code = bag_code.Trim();
                w = string.Format("bag_code = '{0}' and owner is null;", bag_code);
            }
            sql += w;
            DataTable dt = myDb.GetDataTable(sql);
            Dictionary<string, JsonData> rets = new Dictionary<string, JsonData>();

            HashSet<string> sets = new HashSet<string>();
            foreach (DataRow row in dt.Rows)
            {
                var sid = row["ID"].ToString();
                var bgbh = row["bgbh"].ToString();
                var tp = row["type"].ToString();
                sets.Add(bgbh);
                if (rets.ContainsKey(sid))
                {
                    continue;
                }
                else
                {
                    JsonData jd = new JsonData();
                    jd["type"] = tp;
                    jd["bgbh"] = bgbh;
                    jd["ID"] = sid;
                    jd["bag_code"] = row["bag_code"].ToString();
                    jd["no"] = row["no"].ToString();
                    jd["sl"] = row["sl"].ToString();
                    jdarr.Add((object)jd);
                    rets[sid] = jd;
                }
            }

            if(rets.Count==0 || sets.Count == 0)
            {
                throw new Exception("样品记录不存在");
            }
            var bgbhs = "";
            foreach (var bgbh in sets)
            {
                if (bgbhs.Length > 0)
                {
                    bgbhs += ",";
                }
                bgbhs += "'" + bgbh + "'";
            }


            sql = string.Format("select [ypmc],[bgbh] from [AmSample] where bgbh in ({0});", bgbhs);
            System.Diagnostics.Debug.WriteLine("sql is: " + sql);
            DataTable dt2 = myDb.GetDataTable(sql);
            foreach (DataRow row in dt2.Rows)
            {
                var bgbh = row["bgbh"].ToString();
                var ypmc = row["ypmc"].ToString();
                for (int i = 0; i < jdarr.Count; i++)
                {
                    JsonData jd = (JsonData)jdarr[i];
                    if (bgbh.Equals(jd["bgbh"].ToString()))
                    {
                        jd["ypmc"] = ypmc;
                    }
                }

            }

            return jdarr;
        }

        [MyAttribute(CheckApp = false, CheckUrl = false, CheckLogin = false)]
        public ActionResult outSam(string bag_code)
        {
            ViewBag.bag_code = bag_code;
            return View();
        }

        [MyAttribute(CheckApp = false, CheckUrl = false, CheckLogin = false)]
        public string getSams(string uuid, string bag_code)
        {
            try
            {
                var jdarr = getSamsByID(uuid, bag_code);
                var r = "{\"data\":" + jdarr.ToJson() + ",\"status\":1,\"msg\":\"ok\"}";
                return r;
            }
            catch (Exception e)
            {
                return "{\"status\":0,\"msg\":\"样品信息不存在，原因：" + e.Message + "\"}";
            }

        }

        [HttpPost]
        [MyAttribute(CheckApp = false, CheckUrl = false, CheckLogin = false)]
        public string inSams()
        {
            var sr = new StreamReader(Request.InputStream);
            var stream = sr.ReadToEnd();
            JsonData jsonData2 = JsonMapper.ToObject(stream);
            var bgbhs = new HashSet<string>();
            YJ.Data.MSSQL.DBHelper myDb = new YJ.Data.MSSQL.DBHelper();
            for (int i = 0; i < jsonData2.Count; i++)
            {
                JsonData r = jsonData2[i];
                string bgbh = r["bgbh"].ToString();

                try
                {
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        string sql2 = string.Format("MERGE INTO [AmSampleDetail] as T"
                        + " USING(SELECT '{0}' AS bgbh, '{1}' AS no, '{2}' as type,'{3}'as address,'{4}' as ID, '{5}' as in_time, '{6}' as in_operator, '{7}' as remark) AS S"
                        + " ON T.ID = S.ID "
                        + " WHEN MATCHED THEN"
                        + " UPDATE SET T.address = S.address,T.in_time = S.in_time, T.in_operator = S.in_operator, T.remark = S.remark"
                        + " WHEN NOT MATCHED THEN"
                        + " INSERT([ID],[bgbh], [no], [type], [address],[remark]) VALUES(S.ID, S.bgbh, S.no, S.type, S.address, S.remark); ", r["bgbh"].ToString(), r["no"].ToString(),
                            r["type"].ToString(), r["address"].ToString(), r["ID"].ToString(), DateTime.Now, CurrentUser.ID, r["remark"].ToString());
                        myDb.Execute(sql2);
                        bgbhs.Add(bgbh);
                        myDb.Execute(string.Format("INSERT INTO [AmSampleInOut]([AmsAampleId],[UseUId],[Address1],[Remark])VALUES('{0}','u_{1}','{2}', '样品入库')",
                             r["ID"].ToString(), CurrentUser.ID, r["address"].ToString()));
                        scope.Complete();
                    }
                }
                catch (Exception e)
                {
                    return "{\"status\":0,\"msg\":\"" + e.Message + "\"}";
                }
            }
            return "{\"status\":1,\"msg\":\"ok\"}";
        }

        [HttpPost]
        [MyAttribute(CheckApp = false, CheckUrl = false, CheckLogin = false)]
        public string outSams()
        {
            var sr = new StreamReader(Request.InputStream);
            var stream = sr.ReadToEnd();
            JsonData jsonData2 = JsonMapper.ToObject(stream);
            var bgbhs = new HashSet<string>();
            YJ.Data.MSSQL.DBHelper myDb = new YJ.Data.MSSQL.DBHelper();
            for (int i = 0; i < jsonData2.Count; i++)
            {
                JsonData r = jsonData2[i];
                string bgbh = r["bgbh"].ToString();

                try
                {
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        string sql2 = string.Format("MERGE INTO [AmSampleDetail] as T"
                        + " USING(SELECT '{0}' AS bgbh, '{1}' AS no, '{2}' as type,'{3}'as address,'{4}' as ID, '{5}' as out_time, '{6}' as owner) AS S"
                        + " ON T.ID = S.ID "
                        + " WHEN MATCHED THEN"
                        + " UPDATE SET T.address = S.address,T.out_time = S.out_time, T.owner = S.owner"
                        + " WHEN NOT MATCHED THEN"
                        + " INSERT([ID],[bgbh], [no], [type], [address]) VALUES(S.ID, S.bgbh, S.no, S.type, S.address); ", r["bgbh"].ToString(), r["no"].ToString(),
                            r["type"].ToString(), CurrentUser.ID, r["ID"].ToString(), DateTime.Now, CurrentUser.ID);
                        myDb.Execute(sql2);
                        bgbhs.Add(bgbh);
                        myDb.Execute(string.Format("INSERT INTO [AmSampleInOut]([AmsAampleId],[UseUId],[Address1],[Remark])VALUES('{0}','u_{1}','{2}', '样品出库')",
                             r["ID"].ToString(), CurrentUser.ID, CurrentUser.ID));
                        scope.Complete();
                    }
                }
                catch (Exception e)
                {
                    return "{\"status\":0,\"msg\":\"" + e.Message + "\"}";
                }
            }
            return "{\"status\":1,\"msg\":\"ok\"}";
        }

        [HttpPost]
        [MyAttribute(CheckApp = false, CheckUrl = false, CheckLogin = false)]
        public string bagSams()
        {
            var sr = new StreamReader(Request.InputStream);
            var stream = sr.ReadToEnd();
            JsonData jsonData2 = JsonMapper.ToObject(stream);
            var bgbhs = new HashSet<string>();
            YJ.Data.MSSQL.DBHelper myDb = new YJ.Data.MSSQL.DBHelper();
            for (int i = 0; i < jsonData2.Count; i++)
            {
                JsonData r = jsonData2[i];
                string bgbh = r["bgbh"].ToString();

                try
                {
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        string sql2 = string.Format("MERGE INTO [AmSampleDetail] as T"
                        + " USING(SELECT '{0}' AS bgbh, '{1}' AS no, '{2}' as type,'{3}'as bag_code,'{4}' as ID, '{5}' as bag_time, '{6}' as operator) AS S"
                        + " ON T.ID = S.ID "
                        + " WHEN MATCHED THEN"
                        + " UPDATE SET T.operator = S.operator,T.bag_time = S.bag_time, T.bag_code = S.bag_code;", r["bgbh"].ToString(), r["no"].ToString(),
                            r["type"].ToString(), r["bag_code"].ToString(), r["ID"].ToString(), DateTime.Now, CurrentUser.ID);
                        myDb.Execute(sql2);
                        bgbhs.Add(bgbh);
                        myDb.Execute(string.Format("INSERT INTO [AmSampleInOut]([AmsAampleId],[UseUId],[Address1],[Remark])VALUES('{0}','u_{1}','{2}', '样品打包 {3}')",
                             r["ID"].ToString(), CurrentUser.ID, "", r["bag_code"].ToString()));
                        scope.Complete();
                    }
                }
                catch (Exception e)
                {
                    return "{\"status\":0,\"msg\":\"" + e.Message + "\"}";
                }
            }
            return "{\"status\":1,\"msg\":\"ok\"}";
        }


        [MyAttribute(CheckApp = false, CheckUrl = false, CheckLogin = false)]
        public string getSampleDetail(string from_date, string ly_date)
        {
            YJ.Data.MSSQL.DBHelper myDb = new YJ.Data.MSSQL.DBHelper();
            string sql = "select TOP (100) * from AmSampleDetail where has_zy = 0 ";
            string w = "";
            if (!string.IsNullOrEmpty(from_date))
            {
                w += " and [print_time] >= '" + from_date + "' ";
            }

            if (!string.IsNullOrEmpty(ly_date))
            {
                w += " and [in_time] >= '" + ly_date + "' ";
            }
            sql += w;
            sql += " ORDER BY no asc;";
            DataTable dt = myDb.GetDataTable(sql);
            Dictionary<string, JsonData> rets = new Dictionary<string, JsonData>();
            JsonData jdarr = new JsonData();
            HashSet<string> sets = new HashSet<string>();
            foreach (DataRow row in dt.Rows)
            {
                var bgbh = row["bgbh"].ToString();
                var tp = row["type"].ToString();
                var kk = bgbh + "-" + tp;
                sets.Add(bgbh);
                if (rets.ContainsKey(kk))
                {
                    JsonData jd = (JsonData)rets[kk];
                    jd["ids"].Add((object)row["ID"]);
                    jd["nos"].Add((object)row["no"]);
                }
                else
                {
                    JsonData jd = new JsonData();
                    JsonData jd2 = new JsonData();
                    JsonData jd_ids = new JsonData();
                    jd_ids.Add((object)row["ID"]);
                    jd2.Add((object)row["no"].ToString());
                    jd["nos"] = jd2;
                    jd["bgbh"] = bgbh;
                    jd["ids"] = jd_ids;
                    jd["type"] = row["type"].ToString();
                    jdarr.Add((object)jd);
                    rets[kk] = jd;
                }
            }

            var bgbhs = "";
            foreach (var bgbh in sets)
            {
                if (bgbhs.Length > 0)
                {
                    bgbhs += ",";
                }
                bgbhs += "'" + bgbh + "'";
            }

            try
            {
                sql = string.Format("select [ypmc],[bgbh] from [AmSample] where bgbh in ({0});", bgbhs);
                System.Diagnostics.Debug.WriteLine("sql is: " + sql);
                DataTable dt2 = myDb.GetDataTable(sql);
                foreach (DataRow row in dt2.Rows)
                {
                    var bgbh = row["bgbh"].ToString();
                    var ypmc = row["ypmc"].ToString();
                    for (int i = 0; i < jdarr.Count; i++)
                    {
                        JsonData jd = (JsonData)jdarr[i];
                        if (bgbh.Equals(jd["bgbh"].ToString()))
                        {
                            jd["ypmc"] = ypmc;
                        }
                    }

                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + e.Message);
            }
            var r = "{\"data\":" + jdarr.ToJson() + ",\"status\":1,\"msg\":\"正常\"}";
            System.Diagnostics.Debug.WriteLine(r);
            return r;
        }


        [HttpPost]
        [MyAttribute(CheckApp = false, CheckUrl = false, CheckLogin = false)]
        public string printDetail()
        {
            var sr = new StreamReader(Request.InputStream);
            var stream = sr.ReadToEnd();
            JsonData jsonData2 = JsonMapper.ToObject(stream);
            var bgbhs = new HashSet<string>();
            YJ.Data.MSSQL.DBHelper myDb = new YJ.Data.MSSQL.DBHelper();
            for (int i = 0; i < jsonData2.Count; i++)
            {
                JsonData r = jsonData2[i];
                string bgbh = r["bgbh"].ToString();

                try
                {
                    string sql2 = string.Format("MERGE INTO [AmSampleDetail] as T"
                        + " USING(SELECT '{0}' AS bgbh, '{1}' AS no, '{2}' as type,'{3}'as is_share,'{4}' as ID, '{5}' as sl) AS S"
                        + " ON T.ID = S.ID "
                        + " WHEN MATCHED THEN"
                        + " UPDATE SET T.is_share = S.is_share"
                        + " WHEN NOT MATCHED THEN"
                        + " INSERT([ID],[bgbh], [no], [type], [is_share], [sl]) VALUES(S.ID, S.bgbh, S.no, S.type, S.is_share, S.sl); ", r["bgbh"].ToString(), r["no"].ToString(),
                            r["type"].ToString(), r["is_share"].ToString(), r["ID"].ToString(), r["sl"].ToString());
                    myDb.Execute(sql2);
                    bgbhs.Add(bgbh);
                }
                catch (Exception e)
                {
                    return "{\"status\":0,\"msg\":\"" + e.Message + "\"}";
                }
            }

            foreach (var bg in bgbhs)
            {
                try
                {
                    string sql2 = string.Format("update [AmSample] set has_print = 1 where bgbh = '{0}'", bg);
                    myDb.Execute(sql2);
                }
                catch (Exception e)
                {
                    return "{\"status\":0,\"msg\":\"" + e.Message + "\"}";
                }
            }
            return "{\"status\":1,\"msg\":\"ok\"}";
        }

        [HttpPost]
        [MyAttribute(CheckApp = false, CheckUrl = false, CheckLogin = false)]
        public string printZBDetail()
        {
            var sr = new StreamReader(Request.InputStream);
            var stream = sr.ReadToEnd();
            JsonData jsonData2 = JsonMapper.ToObject(stream);
            var bgbhs = new HashSet<string>();

            var ids = "";
            for (int i = 0; i < jsonData2.Count; i++)
            {
                JsonData r = jsonData2[i];

                for (int j = 0; j < r["used"].Count; j++)
                {
                    if (ids.Length > 0)
                    {
                        ids += ",";
                    }
                    ids += string.Format("'{0}'", r["used"][j].ToString());

                }
            }

            var sql = string.Format("update AmSampleDetail set has_zy = 1 where ID in ({0})", ids);

            var dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]{
                new DataColumn("ID",typeof(string)),
                new DataColumn("bgbh",typeof(string)),
                new DataColumn("no",typeof(int)),new DataColumn("type",typeof(int))});

            for (int i = 0; i < jsonData2.Count; i++)
            {
                JsonData jd = jsonData2[i];
                for (int j = 0; j < jd["new"].Count; j++)
                {
                    JsonData jdc = jd["new"][j];
                    DataRow r = dt.NewRow();
                    r[0] = jdc["ID"].ToString();
                    r[1] = jdc["bgbh"].ToString();
                    r[2] = jdc["no"].ToString().ToInt();
                    r[3] = jdc["type"].ToString().ToInt();
                    dt.Rows.Add(r);
                }
            }

            using (var conn = new SqlConnection(YJ.Utility.Config.PlatformConnectionStringMSSQL))
            {
                try
                {
                    conn.Open();


                    //使用SqlBulkCopy 加载数据到临时表中
                    using (var bulkCopy = new SqlBulkCopy(conn))
                    {
                        foreach (DataColumn dcPrepped in dt.Columns)
                        {
                            bulkCopy.ColumnMappings.Add(dcPrepped.ColumnName, dcPrepped.ColumnName);
                        }

                        bulkCopy.BulkCopyTimeout = 660;
                        bulkCopy.DestinationTableName = "AmSampleDetail";
                        bulkCopy.WriteToServer(dt);
                        bulkCopy.Close();
                    }

                    using (var command = new SqlCommand("", conn))
                    {
                        command.CommandTimeout = 300;
                        command.CommandText = sql;
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    return "{\"status\":0,\"msg\":\"" + e.Message + "\"}";
                }
                finally
                {
                    conn.Close();
                }
            }


            return "{\"status\":1,\"msg\":\"ok\"}";
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
