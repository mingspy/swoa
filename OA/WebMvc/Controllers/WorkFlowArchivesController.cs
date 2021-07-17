using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Controllers
{
    public class WorkFlowArchivesController : MyController
    {
        //
        // GET: /WorkFlowArchives/

        public ActionResult Index()
        {
            YJ.Platform.WorkFlow bworkFlow = new YJ.Platform.WorkFlow();
            ViewBag.flowOptions = bworkFlow.GetOptions();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Query()
        {
            string sidx = Request.Form["sidx"];
            string sord = Request.Form["sord"];
            string title = Request.Form["Title"];
            string FlowID = Request.Form["FlowID"];
            string Date1 = Request.Form["Date1"];
            string Date2 = Request.Form["Date2"];

            YJ.Platform.WorkFlowArchives BWFA = new YJ.Platform.WorkFlowArchives();
            YJ.Platform.WorkFlow BWF = new YJ.Platform.WorkFlow();

            long count;
            int pageSize = YJ.Utility.Tools.GetPageSize();
            int pageNumber = YJ.Utility.Tools.GetPageNumber();
            string order = (sidx.IsNullOrEmpty() ? "WriteTime" : sidx) + " " + (sord.IsNullOrEmpty() ? "asc" : sord);
            System.Data.DataTable Dt = BWFA.GetPagerData(out count, pageSize, pageNumber, title, FlowID, Date1, Date2 , order);

            LitJson.JsonData json = new LitJson.JsonData();
            foreach (System.Data.DataRow dr in Dt.Rows)
            {
                LitJson.JsonData j = new LitJson.JsonData();
                j["id"] = dr["ID"].ToString();
                j["Title"] = "<a href=\"javascript:show('" + dr["ID"].ToString() + "');\" class=\"blue\">" + dr["Title"].ToString() + "</a>";
                j["FlowName"] = dr["FlowName"].ToString();
                j["StepName"] = dr["StepName"].ToString();
                j["WriteTime"] = dr["WriteTime"].ToString().ToDateTimeString();
                json.Add(j);
            }

            return "{\"userdata\":{\"total\":" + count + ",\"pagesize\":" + pageSize + ",\"pagenumber\":" + pageNumber + "},\"rows\":" + json.ToJson() + "}";
        }

        public ActionResult Show()
        {
            string id = Request.QueryString["id"];
            if (!id.IsGuid())
            {
                return View();
            }
            var archives = new YJ.Platform.WorkFlowArchives().Get(id.ToGuid());
            if (archives == null)
            {
                return View();
            }
            return View(archives);
        }
    }
}
