using LitJson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Controllers
{
    public class TDevice
    {
        public string did { get; set; }
        public string ip { get; set; }
        public string address { get; set; }
        public int stat { get; set; }
    }
    public class TempController : MyController
    {
        //
        // GET: /Temp/
        [MyAttribute(CheckApp = false, CheckUrl = false, CheckLogin = false)]
        public ActionResult Index()
        {
            return View();
        }

        [MyAttribute(CheckApp = false, CheckUrl = false, CheckLogin = false)]
        public string GetDids()
        {
            string sql = string.Format("select * from [TemperatureDevice] order by stat desc, did asc");
            YJ.Data.MSSQL.DBHelper myDb = new YJ.Data.MSSQL.DBHelper();
            DataTable dt = myDb.GetDataTable(sql);
            List<TDevice> dids = new List<TDevice>();
            foreach (DataRow item in dt.Rows)
            {
                TDevice t = new TDevice();
                t.did = item["did"].ToString();
                t.ip = item["ip"].ToString();
                t.address = item["address"].ToString();
                t.stat = item["stat"].ToString().ToInt();
                dids.Add(t);
            }

            var json = new LitJson.JsonData();
            json["dids"] = JsonMapper.ToJson(dids);
            return json.ToJson();
        }

        [MyAttribute(CheckApp = false, CheckUrl = false, CheckLogin =false)]
        public string GetDetail(string did, int limits=30)
        {
            string sql = string.Format("select top {0} * from TemperatureDetail where did='{1}' order by 'itime' desc", limits, did);
            YJ.Data.MSSQL.DBHelper myDb = new YJ.Data.MSSQL.DBHelper();
            DataTable dt = myDb.GetDataTable(sql);

            List<string> datetimes = new List<string>();
            List<double> temps = new List<double>();
            List<double> humbs = new List<double>();
            foreach (DataRow item in dt.Rows)
            {
                datetimes.Add(item["itime"].ToString());
                temps.Add( item["temp"].ToString().ToDouble());
                humbs.Add(item["humb"].ToString().ToDouble());
            }

            datetimes.Reverse();
            temps.Reverse();
            humbs.Reverse();


            var json = new LitJson.JsonData();

            json["datetimes"] = JsonMapper.ToJson(datetimes);
            json["temps"] = JsonMapper.ToJson(temps); 
            json["humbs"] = JsonMapper.ToJson(humbs);

            
            var s = json.ToJson();
            return s;
        }

    }
}
