using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace WebMvc.Controllers
{
    public class WorkFlowTasksController : MyController
    {
        //
        // GET: /WorkFlowTasks/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Instance()
        {
            return View();
        }

        public ActionResult InstanceTree()
        {
            return View();
        }
       
        public ActionResult InstanceList()
        {
            YJ.Platform.WorkFlowTask bworkFlowTask = new YJ.Platform.WorkFlowTask();
            YJ.Platform.WorkFlow bworkFlow = new YJ.Platform.WorkFlow();
            string typeid = Request.QueryString["typeid"];
            //可管理的流程ID数组
            var flows = bworkFlow.GetInstanceManageFlowIDList(YJ.Platform.Users.CurrentUserID, typeid);
            string flowOptions = bworkFlow.GetOptions(flows, typeid, "");
            string query = string.Format("&appid={0}&tabid={1}&typeid={2}",
               Request.QueryString["appid"], Request.QueryString["tabid"], typeid);
            
            List<SelectListItem> statusItems = new List<SelectListItem>();
            statusItems.Add(new SelectListItem() { Text = "==全部==", Value = "0"});
            statusItems.Add(new SelectListItem() { Text = "未完成", Value = "1"});
            statusItems.Add(new SelectListItem() { Text = "已完成", Value = "2"});

            ViewBag.Query = query;
            ViewBag.StatusItems = statusItems;
            ViewBag.FlowOptions = flowOptions;
            return View();
        }

        public ActionResult InstanceManage()
        {
            YJ.Platform.WorkFlowTask bworkFlowTask = new YJ.Platform.WorkFlowTask();
            YJ.Platform.WorkFlow bworkFlow = new YJ.Platform.WorkFlow();
            string flowid = Request.QueryString["flowid1"];
            string groupid = Request.QueryString["groupid"];
            var wfInstall = bworkFlow.GetWorkFlowRunModel(flowid);
            var tasks = bworkFlowTask.GetTaskList(flowid.ToGuid(), groupid.ToGuid()).OrderBy(p => p.Sort);

            LitJson.JsonData json = new LitJson.JsonData();
            foreach (var task in tasks)
            {
                System.Text.StringBuilder sb=new System.Text.StringBuilder();
                sb.Append("<a style=\"background:url("+Url.Content("~/Images/ico/permission.gif")+") no-repeat left center; padding-left:18px;\" href=\"javascript:void(0);\" onclick=\"cngStatus('"+task.ID+"');\">状态</a>");
                if (task.Status.In(0,1))
                { 
                    sb.Append("<a style=\"background:url("+Url.Content("~/Images/ico/arrow_medium_lower_left.png")+") no-repeat left center; padding-left:16px;\" href=\"javascript:void(0);\" onclick=\"designate('"+task.ID+"');\">指派</a>");
                    sb.Append("<a style=\"background:url("+Url.Content("~/Images/ico/arrow_medium_lower_right.png")+") no-repeat left center; padding-left:16px;\" href=\"javascript:void(0);\" onclick=\"goTo('"+task.ID+"');\">跳转</a>");
                }
                LitJson.JsonData j = new LitJson.JsonData();
                j["id"] = task.ID.ToString();
                j["StepID"] = bworkFlow.GetStepName(task.StepID, wfInstall);
                j["SenderName"] = task.SenderName;
                j["ReceiveTime"] = task.ReceiveTime.ToDateTimeStringS();
                j["ReceiveName"] = task.ReceiveName;
                j["CompletedTime1"] = task.CompletedTime1.HasValue ? task.CompletedTime1.Value.ToDateTimeStringS() : "";
                j["Status"] = bworkFlowTask.GetStatusTitle(task.Status);
                j["Comment"] = task.Comment;
                j["Opation"] = sb.ToString();
                json.Add(j);
            }
            ViewBag.list = json.ToJson();
            return View();
        }

        [MyAttribute(CheckApp = false, CheckLogin = false)]
        public ActionResult Designate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAttribute(CheckApp = false, CheckLogin = false)]
        public ActionResult Designate(FormCollection collection)
        {
            string taskid = Request.QueryString["taskid"];
            Guid taskID;
            if (taskid.IsGuid(out taskID))
            {
                string user = Request.Form["user"];
                string openerid = Request.QueryString["openerid"];

                YJ.Platform.WorkFlowTask btask = new YJ.Platform.WorkFlowTask();
                var users = new YJ.Platform.Organize().GetAllUsers(user);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (var user1 in users)
                {
                    btask.DesignateTask(taskID, user1);
                    YJ.Platform.Log.Add("管理员指派了流程任务", "将任务" + taskID + "指派给了：" + user1.Name + user1.ID, YJ.Platform.Log.Types.流程相关);

                    sb.Append(user1.Name);
                    sb.Append(",");
                }
                string userNames = sb.ToString().TrimEnd(',');
                ViewBag.Script = "alert('已成功指派给：" + userNames + "!');new RoadUI.Window().reloadOpener();new RoadUI.Window().close();";
            }
            return View();
        }

        public string Delete()
        {
            string flowid = Request.QueryString["flowid1"];
            string groupid = Request.QueryString["groupid"];

            Guid fid, gid;
            if (flowid.IsGuid(out fid) && groupid.IsGuid(out gid))
            {
                System.Text.StringBuilder delxml = new System.Text.StringBuilder();
                var tasks = new YJ.Platform.WorkFlowTask().GetTaskList(fid, gid);
                foreach (var task in tasks)
                {
                    delxml.Append(task.Serialize());
                }
                new YJ.Platform.WorkFlowTask().DeleteInstance(fid, gid);
                YJ.Platform.Log.Add("管理员删除了流程实例", delxml.ToString(), YJ.Platform.Log.Types.流程相关);
                return "删除成功!";
            }
            else
            {
                return "参数错误!";
            }
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult WaitList()
        {
            YJ.Platform.WorkFlow bworkFlow = new YJ.Platform.WorkFlow();
            ViewBag.flowOptions = bworkFlow.GetOptions();
            string query = string.Format("&appid={0}&tabid={1}", Request.QueryString["appid"], Request.QueryString["tabid"]);
            ViewBag.query = query;
            return View();
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult CompletedList()
        {
            YJ.Platform.WorkFlowTask bworkFlowTask = new YJ.Platform.WorkFlowTask();
            YJ.Platform.WorkFlow bworkFlow = new YJ.Platform.WorkFlow();

            string query = string.Format("&appid={0}&tabid={1}", Request.QueryString["appid"], Request.QueryString["tabid"]);
            ViewBag.flowOptions = bworkFlow.GetOptions();
            ViewBag.query = query;
            return View();
        }

        [MyAttribute(CheckApp = false, CheckLogin = false)]
        public ActionResult Detail()
        {
            YJ.Platform.WorkFlowTask bworkFlowTask = new YJ.Platform.WorkFlowTask();
            YJ.Platform.WorkFlow bworkFlow = new YJ.Platform.WorkFlow();

            string flowid = Request.QueryString["flowid1"] ?? Request.QueryString["flowid"];
            string groupid = Request.QueryString["groupid"];
            string displayModel = Request.QueryString["displaymodel"];

            var wfInstall = bworkFlow.GetWorkFlowRunModel(flowid);
            var tasks = bworkFlowTask.GetTaskList(flowid.ToGuid(), groupid.ToGuid()).Where(p => p.Status != -1).OrderBy(p => p.Sort).ThenBy(p=>p.StepSort);
            string query = string.Format("&flowid1={0}&groupid={1}&appid={2}&tabid={3}&iframeid={4}&openerid={5}",
                flowid, groupid,
                Request.QueryString["appid"],
                Request.QueryString["tabid"],
                Request.QueryString["iframeid"],
                Request.QueryString["openerid"]
                );
            string query1 = string.Format("&groupid={0}&appid={1}&tabid={2}&ismobile={3}",
                groupid,
                Request.QueryString["appid"],
                Request.QueryString["tabid"],
                Request.QueryString["ismobile"]
                );
            ViewBag.flowid = flowid;
            ViewBag.groupid = groupid;
            ViewBag.displayModel = displayModel;
            ViewBag.wfInstall = wfInstall;
            ViewBag.query = query;
            ViewBag.query1 = query1;

            LitJson.JsonData json = new LitJson.JsonData();
            foreach (var task in tasks)
            {
                LitJson.JsonData j = new LitJson.JsonData();
                j["StepName"] = task.StepName;
                j["SenderName"] = task.SenderName;
                j["SenderTime"] = task.SenderTime.ToDateTimeStringS();
                j["ReceiveName"] = task.ReceiveName;
                j["CompletedTime1"] = task.CompletedTime1.HasValue ? task.CompletedTime1.Value.ToDateTimeStringS() : "";
                j["StatusTitle"] = bworkFlowTask.GetStatusTitle(task.Status);
                j["Comment"] = task.Comment;
                j["Note"] = task.Note;
                json.Add(j);
            }
            ViewBag.list = json.IsArray ? json.ToJson() : "{}";
            return View(tasks);
        }

        [MyAttribute(CheckApp = false, CheckLogin = false)]
        public ActionResult DetailSubFlow()
        {
            YJ.Platform.WorkFlowTask bworkFlowTask = new YJ.Platform.WorkFlowTask();
            YJ.Platform.WorkFlow bworkFlow = new YJ.Platform.WorkFlow();

            string query = string.Format("&flowid1={0}&groupid={1}&appid={2}&tabid={3}&title={4}&flowid={5}&sender={6}&date1={7}&date2={8}&iframeid={9}&openerid={10}&taskid={11}",
                Request.QueryString["flowid"],
                Request.QueryString["groupid"],
                Request.QueryString["appid"],
                Request.QueryString["tabid"],
                Request.QueryString["title"].UrlEncode(),
                Request.QueryString["flowid"],
                Request.QueryString["sender"],
                Request.QueryString["date1"],
                Request.QueryString["date2"],
                Request.QueryString["iframeid"],
                Request.QueryString["openerid"],
                Request.QueryString["taskid"]
                );
            ViewBag.flowid = Request.QueryString["flowid"];
            ViewBag.groupid = Request.QueryString["groupid"];
            ViewBag.displayModel = Request.QueryString["displaymodel"];
            ViewBag.wfInstall = null;
            ViewBag.query = query;

            string taskid = Request.QueryString["taskid"];
            string displayModel = Request.QueryString["displaymodel"];
            if (!taskid.IsGuid())
            {
                return View(new List<YJ.Data.Model.WorkFlowTask>());
            }
            var task = bworkFlowTask.Get(taskid.ToGuid());
            
            if (task == null || task.SubFlowGroupID.IsNullOrEmpty())
            {
                return View(new List<YJ.Data.Model.WorkFlowTask>());
            }
            List<YJ.Data.Model.WorkFlowTask> tasks = new List<YJ.Data.Model.WorkFlowTask>();
            foreach (string groupID in task.SubFlowGroupID.Split(','))
            {
                tasks.AddRange(bworkFlowTask.GetTaskList(Guid.Empty, groupID.ToGuid()));
            }

            if (tasks.Count == 0)
            {
                Response.Write("未找到任务");
                Response.End();
                return null;
            }

            var wfInstall = bworkFlow.GetWorkFlowRunModel(tasks.FirstOrDefault().FlowID);
            ViewBag.wfInstall = wfInstall;
            ViewBag.flowid = tasks.FirstOrDefault().FlowID.ToString();
            return View(tasks);
        }

        [MyAttribute(CheckApp = false)]
        public string Withdraw()
        {
            string loginMsg;
            if (!WebMvc.Common.Tools.CheckLogin(out loginMsg) && !YJ.Platform.WeiXin.Organize.CheckLogin())
            {
                return "";
            }
            string taskid = Request.QueryString["taskid"];
            Guid tid;
            if (!taskid.IsGuid(out tid))
            {
                return "参数错误!";
            }
            else if (new YJ.Platform.WorkFlowTask().HasWithdraw(tid))
            {
                bool success = new YJ.Platform.WorkFlowTask().WithdrawTask(tid);
                if (success)
                {
                    YJ.Platform.Log.Add("收回了任务", "任务ID：" + taskid, YJ.Platform.Log.Types.流程相关);
                    return "收回成功!";
                }
                else
                {
                    return "收回失败!";
                }
            }
            else
            {
                return "该任务不能收回!";
            }
        }

        [MyAttribute(CheckApp=false, CheckLogin=false)]
        public ActionResult ChangeStatus()
        {
            YJ.Platform.WorkFlowTask BTask = new YJ.Platform.WorkFlowTask();
            YJ.Data.Model.WorkFlowTask task = null;
            string taskid = string.Empty;
            taskid = Request.QueryString["taskid"];
            if (taskid.IsGuid())
            {
                task = BTask.Get(taskid.ToGuid());
            }
            string Status = "";
            if (task != null)
            {
                Status = task.Status.ToString();
            }
            ViewBag.Status = Status;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAttribute(CheckApp = false, CheckLogin = false)]
        public ActionResult ChangeStatus(FormCollection collection)
        {
            YJ.Platform.WorkFlowTask BTask = new YJ.Platform.WorkFlowTask();
            YJ.Data.Model.WorkFlowTask task = null;
            string Status = string.Empty;
            string taskid = string.Empty;
            taskid = Request.QueryString["taskid"];
            if (taskid.IsGuid())
            {
                task = BTask.Get(taskid.ToGuid());
            }
            if (task != null)
            {
                Status = Request.Form["Status"];
                if (Status.IsInt())
                {
                    string oldxml = task.Serialize();
                    task.Status = Status.ToInt();
                    BTask.Update(task);
                    YJ.Platform.Log.Add("改变了流程任务状态", "改变了流程任务状态", YJ.Platform.Log.Types.流程相关, oldxml, task.Serialize());
                    ViewBag.Script = "alert('设置成功!');new RoadUI.Window().reloadOpener();new RoadUI.Window().close();";
                }
            }

            ViewBag.Status = Status;
            return View();
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult Hasten()
        {
            return Hasten(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAttribute(CheckApp = false)]
        public ActionResult Hasten(FormCollection collection)
        {
            if (collection != null)
            {
                string users = Request.Form["HastenUsers"];
                string types = Request.Form["HastenType"];
                string contents = Request.Form["Contents"];

                YJ.Platform.WorkFlowTask WFT = new YJ.Platform.WorkFlowTask();
                YJ.Data.Model.WorkFlowTask task = WFT.Get(Request.QueryString["taskid"].ToGuid());
                YJ.Platform.HastenLog.Hasten(types, users, contents, task);

                ViewBag.script = "alert('催办成功!');new RoadUI.Window().close()";
            }
            return View();
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult GoTo()
        {
            return GoTo(null);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAttribute(CheckApp = false)]
        public ActionResult GoTo(FormCollection collection)
        {
            if (collection != null)
            {
                string[] stepids = (Request.Form["step"] ?? "").Split(',');
                Dictionary<Guid, string> steps = new Dictionary<Guid, string>();
                foreach (string step in stepids)
                {
                    if (!step.IsGuid()) continue;
                    string member = Request.Form["member_" + step];
                    if (member.IsNullOrEmpty()) continue;
                    steps.Add(step.ToGuid(), member);
                }
                YJ.Data.Model.WorkFlowTask Task = null;
                YJ.Platform.WorkFlowTask BTask = new YJ.Platform.WorkFlowTask();

                string taskid = Request.QueryString["taskid"];
                Task = BTask.Get(taskid.ToGuid());
                bool isgoto = BTask.GoToTask(Task, steps);
                ViewBag.script = "alert('跳转" + (isgoto ? "成功" : "失败") + "');new RoadUI.Window().reloadOpener();new RoadUI.Window().close();";
            }
            return View();
        }

        /// <summary>
        /// 删除待办
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [MyAttribute(CheckApp = false)]
        public string deleteTask()
        {
            string flowID = Request.QueryString["flowid"];
            string groupID = Request.QueryString["groupid"];
            string taskID = Request.QueryString["taskid"];
            var task = new YJ.Platform.WorkFlowTask().Get(taskID.ToGuid());
            if(task == null)
            {
                return "未找到当前任务!";
            }
            new YJ.Platform.WorkFlowTask().DeleteInstance(task.FlowID, task.GroupID);
            YJ.Platform.Log.Add("作废了流程实例-" + task.Title, task.Serialize(), YJ.Platform.Log.Types.流程相关);
            return "作废成功!";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAttribute(CheckApp=false)]
        public string QueryWaitList()
        {
            YJ.Platform.WorkFlowTask bworkFlowTask = new YJ.Platform.WorkFlowTask();
            YJ.Platform.WorkFlow bworkFlow = new YJ.Platform.WorkFlow();
            YJ.Platform.AppLibrary bapplibary = new YJ.Platform.AppLibrary();
            string title = Request.Form["title"];
            string flowid = Request.Form["flowid"];
            string sender = Request.Form["sender"];
            string date1 = Request.Form["date1"];
            string date2 = Request.Form["date2"];
            string sidx = Request.Form["sidx"];
            string sord = Request.Form["sord"];
            string appid = "";

            long count;
            int pageSize = YJ.Utility.Tools.GetPageSize();
            int pageNumber = YJ.Utility.Tools.GetPageNumber();
            string order = (sidx.IsNullOrEmpty() ? "ReceiveTime" : sidx) + " " + (sord.IsNullOrEmpty() ? "asc" : sord);
            var taskList = bworkFlowTask.GetTasks(CurrentUserID, out count, pageSize, pageNumber, title.Trim1(), flowid, sender, date1, date2, 0, order);
            LitJson.JsonData json = new LitJson.JsonData();
            Guid userID = CurrentUserID;
            foreach (var task in taskList)
            {
                var applibary = bapplibary.GetByCode(task.FlowID.ToString());
                int openModel = 0;
                int width = 1000;
                int height = 500;
                if (applibary != null)
                {
                    openModel = applibary.OpenMode;
                    width = applibary.Width.HasValue ? applibary.Width.Value : 1000;
                    height = applibary.Height.HasValue ? applibary.Height.Value : 500;
                }
                var flowRunModel = bworkFlow.GetWorkFlowRunModel(task.FlowID);
                LitJson.JsonData j = new LitJson.JsonData();
                j["id"] = task.ID.ToString();
                j["FlowName"] = bworkFlow.GetFlowName(task.FlowID);
                j["StepName"] = task.StepName;
                j["Note"] = task.Note;
                j["ReceiveTime"] = task.ReceiveTime.ToDateTimeString();
                j["SenderName"] = task.SenderName;
                if (task.CompletedTime.HasValue)
                {
                    if (task.CompletedTime.Value < YJ.Utility.DateTimeNew.Now)
                    {
                        j["StatusTitle"] = "<i title=\"已过期\" class=\"fa fa-bell\" style=\"color:red;font-weight:bold;\"><span title=\"要求完成时间：" + task.CompletedTime.Value.ToDateTimeString() + "\">已过期</span></i>";
                    }
                    else if ((task.CompletedTime.Value - YJ.Utility.DateTimeNew.Now).Days <= 3)
                    {
                        j["StatusTitle"] = "<i title=\"即将过期\" class=\"fa fa-bell\" style=\"color:#fd8a02;font-weight:bold;\"><span title=\"要求完成时间：" + task.CompletedTime.Value.ToDateTimeString() + "\">即将到期</span></i>";
                    }
                    else
                    {
                        j["StatusTitle"] = "<i title=\"正常\" class=\"fa fa-bell\" style=\"color:#666;font-weight:bold;\"></i><span title=\"要求完成时间：" + task.CompletedTime.Value.ToDateTimeString() + "\">正常</span></i>";
                    }
                }
                else
                {
                    j["StatusTitle"] = "<i title=\"正常\" class=\"fa fa-bell\" style=\"color:#666;font-weight:bold;\"></i><span title=\"要求完成时间：无时间要求\">正常</span></i>";
                }
                
                j["Title"] = "<a href=\"javascript:void(0);\" class=\"blue\" onclick=\"openTask('/WorkFlowRun/Index?" + string.Format("flowid={0}&stepid={1}&instanceid={2}&taskid={3}&groupid={4}&appid={5}",
                        task.FlowID, task.StepID, task.InstanceID, task.ID, task.GroupID, appid
                        ) + "','" + task.Title.RemoveHTML().UrlEncode() + "','" + task.ID + "'," + openModel + "," + width + "," + height + ");return false;\">" + task.Title.HtmlEncode() + "</a>";
                string opation = "<a href=\"javascript:void(0);\" class=\"editlink\" onclick=\"openTask('/WorkFlowRun/Index?" + string.Format("flowid={0}&stepid={1}&instanceid={2}&taskid={3}&groupid={4}&appid={5}",
                        task.FlowID, task.StepID, task.InstanceID, task.ID, task.GroupID, appid
                        ) + "','" + task.Title.RemoveHTML().UrlEncode() + "','" + task.ID + "'," + openModel + "," + width + "," + height + ");return false;\">处理</a>"
                        + "&nbsp;&nbsp;<a class=\"viewlink\" href=\"javascript:void(0);\" onclick=\"detail('" + task.FlowID + "','" + task.GroupID + "','" + task.ID + "');return false;\">查看</a>";
                if (flowRunModel != null && flowRunModel.FirstStepID == task.StepID && task.SenderID == userID)//第一步发起者可以删除
                {
                    opation += "&nbsp;&nbsp;<a class=\"deletelink\" href=\"javascript:void(0);\" onclick=\"delTask('" + task.FlowID + "','" + task.GroupID + "','" + task.ID + "');return false;\">作废</a>";
                }
                j["Opation"] = opation;
                json.Add(j);
            }
            return "{\"userdata\":{\"total\":" + count + ",\"pagesize\":" + pageSize + ",\"pagenumber\":" + pageNumber + "},\"rows\":" + json.ToJson() + "}";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAttribute(CheckApp = false)]
        public string QueryCompletedList()
        {
            YJ.Platform.WorkFlowTask bworkFlowTask = new YJ.Platform.WorkFlowTask();
            YJ.Platform.WorkFlow bworkFlow = new YJ.Platform.WorkFlow();
            YJ.Platform.AppLibrary bapplibary = new YJ.Platform.AppLibrary();
            string title = Request.Form["title"];
            string flowid = Request.Form["flowid"];
            string sender = Request.Form["sender"];
            string date1 = Request.Form["date1"];
            string date2 = Request.Form["date2"];
            string sidx = Request.Form["sidx"];
            string sord = Request.Form["sord"];
            string appid = "";

            long count;
            int pageSize = YJ.Utility.Tools.GetPageSize();
            int pageNumber = YJ.Utility.Tools.GetPageNumber();
            string order = (sidx.IsNullOrEmpty() ? "CompletedTime1" : sidx) + " " + (sord.IsNullOrEmpty() ? "asc" : sord);
            var taskList = bworkFlowTask.GetTasks(CurrentUserID,
               out count, pageSize, pageNumber, title.Trim1(), flowid, sender, date1, date2, 1, order);
            LitJson.JsonData json = new LitJson.JsonData();
            foreach (var task in taskList)
            {
                bool isHasten = false;
                var applibary = bapplibary.GetByCode(task.FlowID.ToString());
                int openModel = 0;
                int width = 1000;
                int height = 500;
                if (applibary != null)
                {
                    openModel = applibary.OpenMode;
                    width = applibary.Width.HasValue ? applibary.Width.Value : 1000;
                    height = applibary.Height.HasValue ? applibary.Height.Value : 500;
                }
                System.Text.StringBuilder opation = new System.Text.StringBuilder();
                opation.Append("<a class=\"viewlink\" href=\"javascript:void(0);\" onclick=\"detail('" + task.FlowID + "','" + task.GroupID + "','" + task.ID + "');return false;\">查看</a>");
                if (task.Status == 2 && bworkFlowTask.HasWithdraw(task.ID, out isHasten))
                {
                    opation.Append("<a style=\"background:url(" + Url.Content("~/Images/ico/back.gif") + ") no-repeat left center; padding-left:18px;margin-left:5px;\" href=\"javascript:void(0);\" onclick=\"withdraw('" + task.ID + "');\">收回</a>");
                }
                if (isHasten)
                {
                    opation.Append("<a style=\"background:url(" + Url.Content("~/Images/ico/comment_reply.png") + ") no-repeat left center; padding-left:18px;margin-left:5px;\" href=\"javascript:void(0);\" onclick=\"hasten('" + task.ID + "');\">催办</a>");
                }
                LitJson.JsonData j = new LitJson.JsonData();
                j["id"] = task.ID.ToString();
                j["FlowName"] = bworkFlow.GetFlowName(task.FlowID);
                j["Note"] = task.Note;
                j["ReceiveTime"] = task.ReceiveTime.ToDateTimeString();
                j["CompletedTime"] = task.CompletedTime1.HasValue ? task.CompletedTime1.Value.ToDateTimeString() : "";
                j["SenderName"] = task.SenderName;
                //j["StatusTitle"] = bworkFlowTask.GetStatusTitle(task.Status);
                j["StepName"] = task.StepName;
                j["Title"] = "<a href=\"javascript:void(0);\" onclick=\"openTask('/WorkFlowRun/Index?" + string.Format("flowid={0}&stepid={1}&instanceid={2}&taskid={3}&groupid={4}&appid={5}&display=1",
                       task.FlowID, task.StepID, task.InstanceID, task.ID, task.GroupID, appid
                       ) + "','" + task.Title.RemoveHTML().UrlEncode() + "','" + task.ID + "'," + openModel + "," + width + "," + height + ");return false;\">" + task.Title.HtmlEncode() + "</a>";
                j["Opation"] = opation.ToString();
                json.Add(j);
            }
            return "{\"userdata\":{\"total\":" + count + ",\"pagesize\":" + pageSize + ",\"pagenumber\":" + pageNumber + "},\"rows\":" + json.ToJson() + "}";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string QueryInstanceList()
        {
            YJ.Platform.WorkFlowTask bworkFlowTask = new YJ.Platform.WorkFlowTask();
            YJ.Platform.WorkFlow bworkFlow = new YJ.Platform.WorkFlow();
            string Title = Request.Form["Title"];
            string FlowID = Request.Form["FlowID"];
            string SenderID = Request.Form["SenderID"];
            string Date1 = Request.Form["Date1"];
            string Date2 = Request.Form["Date2"];
            string Status = Request.Form["Status"];
            string sidx = Request.Form["sidx"];
            string sord = Request.Form["sord"];
            string typeid = Request.Form["typeid"];
            string appid = Request.Form["appid"];

            //可管理的流程ID数组
            var flows = bworkFlow.GetInstanceManageFlowIDList(YJ.Platform.Users.CurrentUserID, typeid);
            List<Guid> flowids = new List<Guid>();
            foreach (var flow in flows.OrderBy(p => p.Value))
            {
                flowids.Add(flow.Key);
            }
            Guid[] manageFlows = flowids.ToArray();
            long count;
            int pageSize = YJ.Utility.Tools.GetPageSize();
            int pageNumber = YJ.Utility.Tools.GetPageNumber();
            string order = (sidx.IsNullOrEmpty() ? "SenderTime" : sidx) + " " + (sord.IsNullOrEmpty() ? "asc" : sord);
            var taskList = bworkFlowTask.GetInstances1(manageFlows, new Guid[] { },
                SenderID.IsNullOrEmpty() ? new Guid[] { } : new Guid[] { SenderID.Replace(YJ.Platform.Users.PREFIX, "").ToGuid() },
                out count, pageSize, pageNumber, Title.Trim1(), FlowID, Date1, Date2, Status.ToInt(), order);
            LitJson.JsonData json = new LitJson.JsonData();
            foreach (DataRow dr in taskList.Rows)
            {
                var task = bworkFlowTask.GetLastTask(dr["FlowID"].ToString().ToGuid(), dr["GroupID"].ToString().ToGuid());
                if (task == null)
                {
                    continue;
                }
                string flowName;
                string stepName = bworkFlow.GetStepName(task.StepID, task.FlowID, out flowName);
                string query = string.Format("flowid={0}&stepid={1}&instanceid={2}&taskid={3}&groupid={4}&appid={5}&display=1",
                     task.FlowID, task.StepID, task.InstanceID, task.ID, task.GroupID, appid
                     );
                System.Text.StringBuilder opation = new System.Text.StringBuilder();
                opation.Append("<a style=\"margin-right:5px; background:url(" + Url.Content("~/Images/ico/mouse.png") + ") no-repeat left center; padding-left:18px;\" href=\"javascript:void(0);\" onclick=\"manage('" + task.FlowID.ToString() + "','" + task.GroupID.ToString() + "');\">管理</a>");
                if (task.Status.In(-1, 0, 1))
                {
                    opation.Append("<a style=\"background:url(" + Url.Content("~/Images/ico/trash.gif") + ") no-repeat left center; padding-left:18px;\" href=\"javascript:void(0);\" onclick=\"delete1('" + task.FlowID.ToString() + "','" + task.GroupID.ToString() + "');\">删除</a>");
                }
                LitJson.JsonData j = new LitJson.JsonData();
                j["id"] = task.ID.ToString();
                j["Title"] = "<a href=\"javascript:void(0);\" onclick=\"openTask('/WorkFlowRun/Index?" + query + "','" + task.Title.RemoveHTML().UrlEncode() + "','" + task.ID + "');return false;\" class=\"blue\">" + task.Title.HtmlEncode() + "</a>";
                j["FlowName"] = flowName;
                j["StepName"] = stepName;
                j["ReceiveName"] = task.ReceiveName;
                j["ReceiveTime"] = task.ReceiveTime.ToDateTimeStringS();
                j["StatusTitle"] = task.Status;
                j["Opation"] = opation.ToString();
                json.Add(j);
            }
            return "{\"userdata\":{\"total\":" + count + ",\"pagesize\":" + pageSize + ",\"pagenumber\":" + pageNumber + "},\"rows\":" + json.ToJson() + "}";
        }
    }
}
