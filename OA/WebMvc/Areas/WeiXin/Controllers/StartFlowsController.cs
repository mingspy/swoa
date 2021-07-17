using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Areas.WeiXin.Controllers
{
    public class StartFlowsController : Controller
    {
        //
        // GET: /WeiXin/StartFlows/

        public ActionResult Index()
        {
            return Index(null);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection coll)
        {
            YJ.Platform.WeiXin.Organize.CheckLogin();
            List<YJ.Data.Model.WorkFlowStart> StartFlows;
            YJ.Platform.Users BUsers = new YJ.Platform.Users();
            string s_searchkey = Request.QueryString["searchkey"];
            if (coll != null)
            {
                s_searchkey = Request.Form["searchkey"];
            }
            StartFlows = new YJ.Platform.WorkFlow().GetUserStartFlows(YJ.Platform.WeiXin.Organize.CurrentUserID);
            if (!s_searchkey.IsNullOrEmpty())
            {
                StartFlows = StartFlows.FindAll(p => p.Name.Contains(s_searchkey.Trim1(), StringComparison.CurrentCultureIgnoreCase));
            }
            return View(StartFlows);
        }

    }
}
