using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Controllers
{
    public class WorkFlowRunController : MyController
    {
        //
        // GET: /WorkFlowRun/
        [MyAttribute(CheckApp = false, CheckUrl = false)]
        public ActionResult Index()
        {
            return View();
        }

        [MyAttribute(CheckApp = false, CheckUrl = false, CheckLogin = false)]
        public ActionResult Index_App()
        {
            return View();
        }

        [MyAttribute(CheckApp = false, CheckUrl = false)]
        public ActionResult ShowComment()
        {
            return View();
        }

        [MyAttribute(CheckApp = false, CheckLogin = false, CheckUrl = false)]
        public ActionResult Print()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAttribute(CheckApp = false, CheckLogin = false)]
        public ActionResult Execute()
        {
            return View();
        }

        [MyAttribute(CheckApp = false, CheckLogin = false)]
        public ActionResult FlowBack()
        {
            return View();
        }

        [MyAttribute(CheckApp = false, CheckLogin = false)]
        public ActionResult FlowRedirect()
        {
            return View();
        }

        [MyAttribute(CheckApp = false, CheckLogin = false)]
        public ActionResult FlowSend()
        {
            return View();
        }

        [MyAttribute(CheckApp = false, CheckUrl = false, CheckLogin = false)]
        public ActionResult Process()
        {
            return View();
        }

        [MyAttribute(CheckApp = false, CheckLogin = false, CheckUrl = false)]
        public ActionResult Sign()
        {
            return View();
        }

        [MyAttribute(CheckApp = false, CheckLogin = false)]
        public ActionResult SaveData()
        {
            return View();
        }

        /// <summary>
        /// 查看流程图
        /// </summary>
        /// <returns></returns>
        [MyAttribute(CheckApp = false, CheckLogin = false)]
        public ActionResult ShowDesign()
        {
            return View();
        }

        [MyAttribute(CheckApp = false, CheckLogin = false)]
        public ActionResult SubTableEdit()
        {
            return View();
        }

        [MyAttribute(CheckApp = false, CheckLogin = false)]
        public string SubTableDelete()
        {
            string secondtableconnid = Request.QueryString["secondtableconnid"];
            string secondtable = Request.QueryString["secondtable"];
            string secondtableprimarykey = Request.QueryString["secondtableprimarykey"];
            string secondtablepkvalue = Request.QueryString["secondtablepkvalue"];
            YJ.Platform.DBConnection bdbconn = new YJ.Platform.DBConnection();

            int i = bdbconn.DeleteData(secondtableconnid.ToGuid(), secondtable, secondtableprimarykey, secondtablepkvalue);
            if (i > 0)
            {
                return "删除成功!";
            }
            else
            {
                return "删除失败!";
            }
        }

        [MyAttribute(CheckApp = false, CheckLogin = false)]
        public ActionResult FlowFreedomSend()
        {
            return View();
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult TaskEnd()
        {
            string taskid = Request.QueryString["taskid"];
            string msg = taskid.IsGuid() ? new YJ.Platform.WorkFlowTask().EndTask(taskid.ToGuid()) : "参数错误";
            YJ.Platform.Log.Add("终止的流程任务", taskid, YJ.Platform.Log.Types.流程相关);
            if ("1" != msg)
            {
                ViewBag.script = "alert('" + msg + "')";
            }
            else
            {
                ViewBag.script = "alert('终止成功!'); try{top.mainDialog.close();}catch(e){} try{top.mainTab.closeTab();}catch(e){parent.close();}";
            }
            return View();
        }

        [MyAttribute(CheckApp = false, CheckLogin = false, CheckUrl = false)]
        public string GetLinkageOptions()
        {
            string linkagesource = Request["linkagesource"];
            string linkagesourcetext = Request["linkagesourcetext"];
            string linkagesourceconn = Request["linkagesourceconn"];
            string value = Request["value"];

            if ("sql" == linkagesource)
            {
                Guid connID;
                if (!linkagesourceconn.IsGuid(out connID))
                {
                    return "";
                }
                YJ.Platform.DBConnection DBConn = new YJ.Platform.DBConnection();
                return DBConn.GetOptionsFromSql(connID, linkagesourcetext);
            }
            else if ("dict" == linkagesource)
            {
                return new YJ.Platform.Dictionary().GetOptionsByID(value.ToGuid(), YJ.Platform.Dictionary.OptionValueField.ID, "", false);
            }

            return "";
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult FlowCopyFor()
        {
            return FlowCopyFor(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAttribute(CheckApp = false)]
        public ActionResult FlowCopyFor(FormCollection collection)
        {
            YJ.Platform.WorkFlow bworkFlow = new YJ.Platform.WorkFlow();
            YJ.Platform.WorkFlowTask btask = new YJ.Platform.WorkFlowTask();
            YJ.Data.Model.WorkFlowInstalled wfInstalled = null;
            YJ.Data.Model.WorkFlowTask currentTask = null;
            string flowid = Request.QueryString["flowid"];
            string stepid = Request.QueryString["stepid"];
            string groupid = Request.QueryString["groupid"];
            string instanceid = Request.QueryString["instanceid"];
            wfInstalled = bworkFlow.GetWorkFlowRunModel(flowid);
            if (wfInstalled == null)
            {
                Response.Write("未找到流程运行实体");
                Response.End();
                return null;
            }

            var steps = wfInstalled.Steps.Where(p => p.ID == stepid.ToGuid());
            if (steps.Count() == 0)
            {
                Response.Write("未找到当前步骤");
                Response.End();
                return null;
            }

            currentTask = btask.Get(Request.QueryString["taskid"].ToGuid());
            if (currentTask == null)
            {
                Response.Write("当前任务为空,请先保存再抄送!");
                Response.End();
                return null;
            }
            if (collection != null)
            {
                var tasks = btask.GetTaskList(currentTask.ID);
                var users = new YJ.Platform.Organize().GetAllUsers(Request.Form["user"] ?? "");
                System.Text.StringBuilder names = new System.Text.StringBuilder();
                foreach (var user in users)
                {
                    if (tasks.Find(p => p.ReceiveID == user.ID) != null)
                    {
                        continue;
                    }
                    var nextStep = wfInstalled.Steps.Where(p => p.ID == Request.QueryString["stepid"].ToGuid()).First();
                    YJ.Data.Model.WorkFlowTask task = new YJ.Data.Model.WorkFlowTask();
                    if (nextStep.WorkTime > 0)
                    {
                        task.CompletedTime = YJ.Utility.DateTimeNew.Now.AddHours((double)nextStep.WorkTime);
                    }
                    task.FlowID = currentTask.FlowID;
                    task.GroupID = currentTask.GroupID;
                    task.ID = Guid.NewGuid();
                    task.Type = 5;
                    task.InstanceID = currentTask.InstanceID;
                    task.Note = "抄送任务";
                    task.PrevID = currentTask.PrevID;
                    task.PrevStepID = currentTask.PrevStepID;
                    task.ReceiveID = user.ID;
                    task.ReceiveName = user.Name;
                    task.ReceiveTime = YJ.Utility.DateTimeNew.Now;
                    task.SenderID = currentTask.ReceiveID;
                    task.SenderName = currentTask.ReceiveName;
                    task.SenderTime = task.ReceiveTime;
                    task.Status = 0;
                    task.StepID = currentTask.StepID;
                    task.StepName = currentTask.StepName;
                    task.Sort = currentTask.Sort;
                    task.Title = currentTask.Title;
                    btask.Add(task);
                    names.Append(task.ReceiveName);
                    names.Append(",");
                }
                ViewBag.script = "alert('成功抄送给：" + names.ToString().TrimEnd(',') + "');new RoadUI.Window().close();";
            }
            return View();
        }
        
        /// <summary>
        /// 显示主流程表单
        /// </summary>
        /// <returns></returns>
        [MyAttribute(CheckApp = false)]
        public ActionResult ShowForm()
        {
            string taskID = Request.QueryString["taskid"];
            if (taskID.IsGuid())
            {
                YJ.Platform.WorkFlowTask workFlowTask = new YJ.Platform.WorkFlowTask();
                var task = workFlowTask.Get(taskID.ToGuid());
                if (task != null)
                {
                    var mainTasks = workFlowTask.GetBySubFlowGroupID(task.GroupID);
                    if (mainTasks.Count > 0)
                    {
                        var mainTask = mainTasks.OrderByDescending(p => p.Sort).FirstOrDefault();
                        string url = ("1" == Request.QueryString["ismobile"] ? "Index_App" : "Index") + "?flowid=" + mainTask.FlowID + "&stepid=" + mainTask.StepID +
                            "&instanceid=" + mainTask.InstanceID + "&taskid=" + mainTask.ID + "&groupid=" + mainTask.GroupID +
                            "&appid=" + Request.QueryString["appid"] + "&display=1&tabid=" + Request.QueryString["tabid"];
                        return Redirect(url);
                    }
                }
            }
            return View();
        }

        /// <summary>
        /// 加签
        /// </summary>
        /// <returns></returns>
        [MyAttribute(CheckApp = false)]
        public ActionResult AddWrite()
        {
            return AddWrite(null);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAttribute(CheckApp = false)]
        public ActionResult AddWrite(FormCollection collection)
        {
            if (collection != null)
            {
                string addtype = Request.Form["addtype"];
                string writetype = Request.Form["writetype"];
                string writeuser = Request.Form["writeuser"];
                string note = Request.Form["note"];
                string taskid = Request.QueryString["taskid"];

                string msg;
                bool isSuccess = new YJ.Platform.WorkFlowTask().AddWrite(taskid.ToGuid(), addtype.ToInt(), writetype.ToInt(), writeuser, note, out msg);
                string script = "alert('" + (isSuccess ? "加签成功!" : msg) + "');";
                if (addtype.ToInt() == 1)
                {
                    script += "try{if(top.refreshPage){top.refreshPage();}if(top.mainTab){top.mainTab.closeTab();}else{top.close();}}catch(e){}";
                }
                script += "try{new RoadUI.Window().close();}catch(e){}";
                ViewBag.script = script;
            }
            return View();
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult StartFlow()
        {
            return StartFlow(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAttribute(CheckApp = false)]
        public ActionResult StartFlow(FormCollection collection)
        {
            List<YJ.Data.Model.WorkFlowStart> StartFlows;
            string s_FlowName = Request.QueryString["FlowName"];
            if (collection != null)
            {
                s_FlowName = Request.Form["FlowName"];
            }
            StartFlows = new YJ.Platform.WorkFlow().GetUserStartFlows(YJ.Platform.Users.CurrentUserID);
            if (!s_FlowName.IsNullOrEmpty())
            {
                StartFlows = StartFlows.FindAll(p => p.Name.Contains(s_FlowName.Trim1(), StringComparison.CurrentCultureIgnoreCase));
            }
            ViewBag.FlowName = s_FlowName;
            return View(StartFlows);
        }

        [MyAttribute(CheckApp = false, CheckLogin = false, CheckUrl = false)]
        public ActionResult AutoSubmit()
        {
            return View();
        }
    }
}
