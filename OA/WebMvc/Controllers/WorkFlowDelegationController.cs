using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Controllers
{
    public class WorkFlowDelegationController : MyController
    {
        //
        // GET: /WorkFlowDelegation/
        public ActionResult Index()
        {
            YJ.Platform.WorkFlowDelegation bworkFlowDelegation = new YJ.Platform.WorkFlowDelegation();
            string query1 = string.Format("&appid={0}&tabid={1}&isoneself={2}", Request.QueryString["appid"], Request.QueryString["tabid"], Request.QueryString["isoneself"]);
            ViewBag.Query = "&isoneself=" + Request.QueryString["isoneself"] + "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Delete()
        {
            YJ.Platform.WorkFlowDelegation bworkFlowDelegation = new YJ.Platform.WorkFlowDelegation();
            string ids = Request.Form["ids"];
            foreach (string id in ids.Split(','))
            {
                Guid bid;
                if (!id.IsGuid(out bid))
                {
                    continue;
                }
                var comment = bworkFlowDelegation.Get(bid);
                if (comment != null)
                {
                    bworkFlowDelegation.Delete(bid);
                    YJ.Platform.Log.Add("删除了流程意见", comment.Serialize(), YJ.Platform.Log.Types.流程相关);
                }
            }
            bworkFlowDelegation.RefreshCache();
            return "删除成功!";
        }

        [ValidateAntiForgeryToken]
        public string Query()
        {
            YJ.Platform.WorkFlowDelegation bworkFlowDelegation = new YJ.Platform.WorkFlowDelegation();
            YJ.Platform.Organize borganize = new YJ.Platform.Organize();
            YJ.Platform.Users busers = new YJ.Platform.Users();
            YJ.Platform.WorkFlow bworkFlow = new YJ.Platform.WorkFlow();
            IEnumerable<YJ.Data.Model.WorkFlowDelegation> workFlowDelegationList;

            string startTime = Request.Form["S_StartTime"];
            string endTime = Request.Form["S_EndTime"];
            string suserid = Request.Form["S_UserID"];
            string sidx = Request.Form["sidx"];
            string sord = Request.Form["sord"];
            string typeid = Request.Form["typeid"];

            long count;
            int pageSize = YJ.Utility.Tools.GetPageSize();
            int pageNumber = YJ.Utility.Tools.GetPageNumber();
            string order = (sidx.IsNullOrEmpty() ? "SenderTime" : sidx) + " " + (sord.IsNullOrEmpty() ? "asc" : sord);
            bool isOneSelf = "1" == Request.QueryString["isoneself"];
            if (isOneSelf)
            {
                workFlowDelegationList = bworkFlowDelegation.GetPagerData(out count, pageSize, pageNumber, CurrentUserID.ToString(), startTime, endTime, order);
            }
            else
            {
                workFlowDelegationList = bworkFlowDelegation.GetPagerData(out count, pageSize, pageNumber, YJ.Platform.Users.RemovePrefix(suserid), startTime, endTime, order);
            }
            LitJson.JsonData json = new LitJson.JsonData();
            foreach (var delegation in workFlowDelegationList)
            {
                string status = "委托中";
                if (delegation.StartTime > YJ.Utility.DateTimeNew.Now)
                {
                    status = "未开始";
                }
                else if (delegation.EndTime < YJ.Utility.DateTimeNew.Now)
                {
                    status = "已失效";
                }

                LitJson.JsonData j = new LitJson.JsonData();
                j["id"] = delegation.ID.ToString();
                j["UserID"] = busers.GetName(delegation.UserID);
                j["ToUserID"] = busers.GetName(delegation.ToUserID);
                j["FlowID"] = delegation.FlowID.HasValue ? bworkFlow.GetFlowName(delegation.FlowID.Value) : "";
                j["StartTime"] = delegation.StartTime.ToDateTimeString();
                j["EndTime"] = delegation.EndTime.ToDateTimeString();
                j["Note"] = delegation.Note;
                j["Status"] = status;
                j["Edit"] = "<a class=\"editlink\" href=\"javascript:edit('" + delegation.ID.ToString() + "');\">编辑</a>";
                json.Add(j);
            }
            return "{\"userdata\":{\"total\":" + count + ",\"pagesize\":" + pageSize + ",\"pagenumber\":" + pageNumber + "},\"rows\":" + json.ToJson() + "}";
        }

        public ActionResult Edit()
        {
            return Edit(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection collection)
        {
            YJ.Platform.WorkFlowDelegation bworkFlowDelegation = new YJ.Platform.WorkFlowDelegation();
            YJ.Data.Model.WorkFlowDelegation workFlowDelegation = null;
            string id = Request.QueryString["id"];

            string UserID = string.Empty;
            string ToUserID = string.Empty;
            string StartTime = string.Empty;
            string EndTime = string.Empty;
            string FlowID = string.Empty;
            string Note = string.Empty;

            bool isOneSelf = "1" == Request.QueryString["isoneself"];

            Guid delegationID;
            if (id.IsGuid(out delegationID))
            {
                workFlowDelegation = bworkFlowDelegation.Get(delegationID);
                if (workFlowDelegation != null)
                {
                    FlowID = workFlowDelegation.FlowID.ToString();
                }
            }
            string oldXML = workFlowDelegation.Serialize();

            if (collection != null)
            {
                UserID = Request.Form["UserID"];
                ToUserID = Request.Form["ToUserID"];
                StartTime = Request.Form["StartTime"];
                EndTime = Request.Form["EndTime"];
                FlowID = Request.Form["FlowID"];
                Note = Request.Form["Note"];

                bool isAdd = !id.IsGuid();
                if (workFlowDelegation == null)
                {
                    workFlowDelegation = new YJ.Data.Model.WorkFlowDelegation();
                    workFlowDelegation.ID = Guid.NewGuid();
                }
                workFlowDelegation.UserID = isOneSelf ? YJ.Platform.Users.CurrentUserID : YJ.Platform.Users.RemovePrefix(UserID).ToGuid();
                workFlowDelegation.EndTime = EndTime.ToDateTime();
                if (FlowID.IsGuid())
                {
                    workFlowDelegation.FlowID = FlowID.ToGuid();
                }
                else
                {
                    workFlowDelegation.FlowID = null;
                }
                workFlowDelegation.Note = Note.IsNullOrEmpty() ? null : Note;
                workFlowDelegation.StartTime = StartTime.ToDateTime();
                workFlowDelegation.ToUserID = YJ.Platform.Users.RemovePrefix(ToUserID).ToGuid();
                workFlowDelegation.WriteTime = YJ.Utility.DateTimeNew.Now;



                if (isAdd)
                {
                    bworkFlowDelegation.Add(workFlowDelegation);
                    YJ.Platform.Log.Add("添加了工作委托", workFlowDelegation.Serialize(), YJ.Platform.Log.Types.流程相关);
                }
                else
                {
                    bworkFlowDelegation.Update(workFlowDelegation);
                    YJ.Platform.Log.Add("修改了工作委托", "", YJ.Platform.Log.Types.流程相关, oldXML, workFlowDelegation.Serialize());
                }
                bworkFlowDelegation.RefreshCache();
                ViewBag.Script = "alert('保存成功!');new RoadUI.Window().getOpenerWindow().query();new RoadUI.Window().close();";
            }
            ViewBag.FlowOptions = new YJ.Platform.WorkFlow().GetOptions(FlowID);
            return View(workFlowDelegation == null ? new YJ.Data.Model.WorkFlowDelegation() { UserID = YJ.Platform.Users.CurrentUserID } : workFlowDelegation);
        }

    }
}
