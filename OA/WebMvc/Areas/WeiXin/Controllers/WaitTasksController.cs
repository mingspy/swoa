using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Areas.WeiXin.Controllers
{
    public class WaitTasksController : Controller
    {
        //
        // GET: /WeiXin/WaitTasks/

        public ActionResult Index()
        {
            return Index(null);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection coll)
        {
            YJ.Platform.WeiXin.Organize.CheckLogin();
            Guid UserID = YJ.Platform.WeiXin.Organize.CurrentUserID;
            YJ.Platform.WorkFlowTask BWF = new YJ.Platform.WorkFlowTask();
            List<YJ.Data.Model.WorkFlowTask> TasksList = new List<YJ.Data.Model.WorkFlowTask>();
            string SearchTitle = Request.QueryString["searchkey"];
            if (coll != null)
            {
                SearchTitle = Request.Form["searchkey"];
            }
            long count;
            TasksList = BWF.GetTasks(UserID, out count, 15, 1, title: SearchTitle);
            ViewBag.Count = count;
            ViewBag.SearchTitle = SearchTitle;
            return View(TasksList);
        }

        public ActionResult text()
        {
            return View();
        }

        public string GetTasks()
        {
            string pageNumber = Request.QueryString["pagenumber"];
            string pageSize = Request.QueryString["pagesize"];
            string searchTitle = Request.QueryString["SearchTitle"];
            long count;
            Guid userID = YJ.Platform.WeiXin.Organize.CurrentUserID;
            var tasks = new YJ.Platform.WorkFlowTask().GetTasks(userID, out count, pageSize.ToInt(15), pageNumber.ToInt(2), title: searchTitle);
            LitJson.JsonData jd = new LitJson.JsonData();
            if (tasks.Count == 0)
            {
                return "[]";
            }
            foreach (var task in tasks)
            {
                LitJson.JsonData jd1 = new LitJson.JsonData();
                jd1["id"] = task.ID.ToString();
                jd1["title"] = task.Title;
                jd1["time"] = task.ReceiveTime.ToDateTimeString();
                jd1["sender"] = task.SenderName;
                jd1["flowid"] = task.FlowID.ToString();
                jd1["stepid"] = task.StepID.ToString();
                jd1["instanceid"] = task.InstanceID;
                jd1["groupid"] = task.GroupID.ToString();
                jd.Add(jd1);
            }
            return jd.ToJson();
        }

    }
}
