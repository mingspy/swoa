using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Controllers
{
    public class WorkFlowButtonsController : MyController
    {
        //
        // GET: /WorkFlowButtons/
        public ActionResult Index()
        {
            string name = string.Empty;
            ViewBag.Query1 = string.Format("&appid={0}&tabid={1}", Request.QueryString["appid"], Request.QueryString["tabid"]);
            YJ.Platform.WorkFlowButtons bworkFlowButtons = new YJ.Platform.WorkFlowButtons();
            IEnumerable<YJ.Data.Model.WorkFlowButtons> workFlowButtonsList= bworkFlowButtons.GetAll();

            LitJson.JsonData json = new LitJson.JsonData();
            foreach (var but in workFlowButtonsList.OrderBy(p => p.Sort).ThenBy(p => p.Title))
            {
                LitJson.JsonData j = new LitJson.JsonData();
                j["id"] = but.ID.ToString();
                j["Title"] = but.Title;
                if (!but.Ico.IsNullOrEmpty())
                {
                    if (but.Ico.IsFontIco())
                    {
                        j["Ico"] = "<i class=\"fa " + but.Ico + "\" style=\"font-size:14px;\"></i>";
                    }
                    else
                    {
                        j["Ico"] = "<img src=\"" + Url.Content("~" + but.Ico) + "\" alt=\"\" />";
                    }
                }
                else
                {
                    j["Ico"] = "";
                }
                j["Note"] = but.Note;
                j["Sort"] = but.Sort.ToString();
                j["Opation"] = "<a class=\"editlink\" href=\"javascript:edit('" + but.ID.ToString() + "');\">编辑</a>";
                json.Add(j);
            }
            ViewBag.list = json.ToJson();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Delete()
        {
            YJ.Platform.WorkFlowButtons bworkFlowButtons = new YJ.Platform.WorkFlowButtons();
            string ids = Request.Form["ids"];
            foreach (string id in ids.Split(','))
            {
                Guid bid;
                if (!id.IsGuid(out bid))
                {
                    continue;
                }
                var but = bworkFlowButtons.Get(bid);
                if (but != null)
                {
                    bworkFlowButtons.Delete(bid);
                    YJ.Platform.Log.Add("删除了流程按钮", but.Serialize(), YJ.Platform.Log.Types.流程相关);
                }
            }
            bworkFlowButtons.ClearCache();
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
            YJ.Platform.WorkFlowButtons bworkFlowButtons = new YJ.Platform.WorkFlowButtons();
            YJ.Data.Model.WorkFlowButtons workFlowButton = null;
            string id = Request.QueryString["id"];

            string title = string.Empty;
            string ico = string.Empty;
            string script = string.Empty;
            string note = string.Empty;
            string sort = string.Empty;

            Guid buttionID;
            if (id.IsGuid(out buttionID))
            {
                workFlowButton = bworkFlowButtons.Get(buttionID);
            }
            string oldXML = workFlowButton.Serialize();
            if (collection != null)
            {
                title = Request.Form["Title"];
                ico = Request.Form["Ico"];
                script = Request.Form["Script"];
                note = Request.Form["Note"];
                sort = Request.Form["Sort"];

                bool isAdd = !id.IsGuid();
                if (workFlowButton == null)
                {
                    workFlowButton = new YJ.Data.Model.WorkFlowButtons();
                    workFlowButton.ID = Guid.NewGuid();
                    workFlowButton.Sort = bworkFlowButtons.GetMaxSort();
                }

                workFlowButton.Ico = ico.IsNullOrEmpty() ? null : ico.Trim();
                workFlowButton.Note = note.IsNullOrEmpty() ? null : note.Trim();
                workFlowButton.Script = script.IsNullOrEmpty() ? null : script;
                workFlowButton.Title = title.Trim();
                if (sort.IsInt())
                {
                    workFlowButton.Sort = sort.ToInt();
                }
                else
                {
                    workFlowButton.Sort = bworkFlowButtons.GetMaxSort();
                }

                if (isAdd)
                {
                    bworkFlowButtons.Add(workFlowButton);
                    YJ.Platform.Log.Add("添加了流程按钮", workFlowButton.Serialize(), YJ.Platform.Log.Types.流程相关);
                }
                else
                {
                    bworkFlowButtons.Update(workFlowButton);
                    YJ.Platform.Log.Add("修改了流程按钮", "", YJ.Platform.Log.Types.流程相关, oldXML, workFlowButton.Serialize());
                }
                bworkFlowButtons.ClearCache();
                ViewBag.Script = "new RoadUI.Window().reloadOpener();alert('保存成功!');new RoadUI.Window().close();";
            }
            if (workFlowButton == null)
            { 
                workFlowButton = new YJ.Data.Model.WorkFlowButtons();
                workFlowButton.Sort = bworkFlowButtons.GetMaxSort();
            }
            return View(workFlowButton);
        }

    }
}
