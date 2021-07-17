using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Areas.WeiXin.Controllers
{
    public class DocumentsController : Controller
    {
        //
        // GET: /WeiXin/Documents/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            return Search(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(FormCollection coll)
        {
            string searchText = string.Empty;
            if (coll != null)
            {
                searchText = Request.Form["searchkey"];
            }
            ViewBag.searchText = searchText;
            return View();
        }

        public ActionResult List()
        {
            return List(null);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult List(FormCollection coll)
        {
            string searchText = string.Empty;
            if (coll != null)
            {
                searchText = Request.Form["searchkey"];
            }
            ViewBag.searchText = searchText;
            return View();
        }

        public string GetDocs()
        {
            string pageNumber = Request.QueryString["pagenumber"];
            string pageSize = Request.QueryString["pagesize"];
            string searchTitle = Request.QueryString["SearchTitle"];
            string dirID = Request.QueryString["dirid"];
            long count;
            Guid userID = YJ.Platform.WeiXin.Organize.CurrentUserID;

            var docs = new YJ.Platform.Documents().GetList(out count, pageSize.ToInt(), pageNumber.ToInt(), dirID, userID.ToString(), searchTitle);
            LitJson.JsonData jd = new LitJson.JsonData();
            if (docs.Rows.Count == 0)
            {
                return "[]";
            }
            foreach (System.Data.DataRow dr in docs.Rows)
            {
                LitJson.JsonData jd1 = new LitJson.JsonData();
                jd1["id"] = dr["ID"].ToString();
                jd1["title"] = dr["Title"].ToString();
                jd1["writetime"] = dr["WriteTime"].ToString().ToDateTime().ToDateTimeString();
                jd1["writeuser"] = dr["WriteUserName"].ToString();
                jd.Add(jd1);
            }
            return jd.ToJson();
        }
    }
}
