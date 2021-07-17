using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Controllers
{
    public class WorkFlowCommentsController : MyController
    {
        //
        // GET: /WorkFlowComments/

        public ActionResult Index()
        {
            return Index(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection collection)
        {
            YJ.Platform.WorkFlowComment bworkFlowComment = new YJ.Platform.WorkFlowComment();
            YJ.Platform.Organize borganize = new YJ.Platform.Organize();
            IEnumerable<YJ.Data.Model.WorkFlowComment> workFlowCommentList;
            workFlowCommentList = bworkFlowComment.GetAll();
            bool isOneSelf = "1" == Request.QueryString["isoneself"];
            if (isOneSelf)
            {
                workFlowCommentList = workFlowCommentList.Where(p => p.MemberID == YJ.Platform.Users.PREFIX + YJ.Platform.Users.CurrentUserID.ToString());
            }

            LitJson.JsonData json = new LitJson.JsonData();
            foreach (var comment in workFlowCommentList.OrderBy(p => p.Type).ThenBy(p => p.Sort))
            {
                LitJson.JsonData j = new LitJson.JsonData();
                j["id"] = comment.ID.ToString();
                j["Comment"] = comment.Comment;
                j["MemberID"] = comment.MemberID.IsNullOrEmpty() ? "所有人员" : borganize.GetNames(comment.MemberID);
                j["Type"] = comment.Type == 0 ? "管理员" : "个人";
                j["Sort"] = comment.Sort;
                j["Opation"] = "<a class=\"editlink\" href=\"javascript:edit('" + comment.ID.ToString() + "');\">编辑</a>";
                json.Add(j);
            }
            ViewBag.list = json.ToJson();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Delete()
        {
            YJ.Platform.WorkFlowComment bworkFlowComment = new YJ.Platform.WorkFlowComment();
            string ids = Request.Form["ids"];
            foreach (string id in ids.Split(','))
            {
                Guid bid;
                if (!id.IsGuid(out bid))
                {
                    continue;
                }
                var comment = bworkFlowComment.Get(bid);
                if (comment != null)
                {
                    bworkFlowComment.Delete(bid);
                    YJ.Platform.Log.Add("删除了流程意见", comment.Serialize(), YJ.Platform.Log.Types.流程相关);
                }
            }
            bworkFlowComment.RefreshCache();
            return "删除成功!";
        }

        public ActionResult Edit()
        {
            return Edit(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection collection)
        {
            YJ.Platform.WorkFlowComment bworkFlowComment = new YJ.Platform.WorkFlowComment();
            YJ.Data.Model.WorkFlowComment workFlowComment = null;
            string id = Request.QueryString["id"];

            string member = string.Empty;
            string comment = string.Empty;
            string sort = string.Empty;

            bool isOneSelf = "1" == Request.QueryString["isoneself"];

            Guid commentID;
            if (id.IsGuid(out commentID))
            {
                workFlowComment = bworkFlowComment.Get(commentID);
                member = workFlowComment.MemberID;
                comment = workFlowComment.Comment;
                sort = workFlowComment.Sort.ToString();
            }
            string oldXML = workFlowComment.Serialize();
            if (collection != null)
            {
                member = isOneSelf ? YJ.Platform.Users.PREFIX + YJ.Platform.Users.CurrentUserID.ToString() : Request.Form["Member"];
                comment = Request.Form["Comment"];
                sort = Request.Form["Sort"];

                bool isAdd = !id.IsGuid();
                if (workFlowComment == null)
                {
                    workFlowComment = new YJ.Data.Model.WorkFlowComment();
                    workFlowComment.ID = Guid.NewGuid();
                    workFlowComment.Type = isOneSelf ? 1 : 0;
                }

                workFlowComment.MemberID = member.IsNullOrEmpty() ? "" : member.Trim();
                workFlowComment.Comment = comment.IsNullOrEmpty() ? "" : comment.Trim();
                workFlowComment.Sort = sort.IsInt() ? sort.ToInt() : bworkFlowComment.GetManagerMaxSort();


                if (isAdd)
                {
                    bworkFlowComment.Add(workFlowComment);
                    YJ.Platform.Log.Add("添加了流程意见", workFlowComment.Serialize(), YJ.Platform.Log.Types.流程相关);
                }
                else
                {
                    bworkFlowComment.Update(workFlowComment);
                    YJ.Platform.Log.Add("修改了流程意见", "", YJ.Platform.Log.Types.流程相关, oldXML, workFlowComment.Serialize());
                }
                bworkFlowComment.RefreshCache();
                ViewBag.Script = "new RoadUI.Window().reloadOpener();alert('保存成功!');";
            }
            if (workFlowComment == null)
            {
                workFlowComment = new YJ.Data.Model.WorkFlowComment();
                workFlowComment.Sort = bworkFlowComment.GetManagerMaxSort() + 5;
            }
            return View(workFlowComment);
        }

    }
}
