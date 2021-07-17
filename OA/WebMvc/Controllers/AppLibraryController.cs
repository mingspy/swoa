using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Controllers
{
    public class AppLibraryController : MyController
    {
        //
        // GET: /AppLibrary/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tree()
        {
            return View();
        }

        public ActionResult List()
        {
            string appid = Request.QueryString["appid"];
            string tabid = Request.QueryString["tabid"];
            string typeid = Request.QueryString["typeid"];
            string query = string.Format("&appid={0}&tabid={1}&typeid={2}", Request.QueryString["appid"], Request.QueryString["tabid"], Request.QueryString["typeid"]);
            ViewBag.Query1 = query;
            ViewBag.TypeID = typeid;
            ViewBag.AppID = appid;
            ViewBag.TabID = tabid;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Delete()
        {
            YJ.Platform.AppLibrary bappLibrary = new YJ.Platform.AppLibrary();
            string deleteID = Request.Form["ids"];
            System.Text.StringBuilder delxml = new System.Text.StringBuilder();
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                foreach (string id in deleteID.Split(','))
                {
                    Guid gid;
                    if (id.IsGuid(out gid))
                    {
                        var app = bappLibrary.Get(gid);
                        if (app != null)
                        {
                            delxml.Append(app.Serialize());
                            bappLibrary.Delete(gid);
                            new YJ.Platform.AppLibraryButtons1().DeleteByAppID(gid);
                            new YJ.Platform.AppLibrarySubPages().DeleteByAppID(gid);
                        }
                    }
                }
                new YJ.Platform.Menu().ClearAllDataTableCache();
                new YJ.Platform.AppLibraryButtons1().ClearCache();
                new YJ.Platform.AppLibrarySubPages().ClearCache();
                YJ.Platform.Log.Add("删除了一批应用程序库", delxml.ToString(), YJ.Platform.Log.Types.菜单权限);
                scope.Complete();
            }
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
            string editID = Request.QueryString["id"];
            string type = Request.QueryString["typeid"];

            YJ.Platform.AppLibrary bappLibrary = new YJ.Platform.AppLibrary();
            YJ.Data.Model.AppLibrary appLibrary = null;
            if (editID.IsGuid())
            {
                appLibrary = bappLibrary.Get(editID.ToGuid());
            }
            bool isAdd = !editID.IsGuid();
            string oldXML = string.Empty;
            if (appLibrary == null)
            {
                appLibrary = new YJ.Data.Model.AppLibrary();
                appLibrary.ID = Guid.NewGuid();
                ViewBag.TypeOptions = new YJ.Platform.AppLibrary().GetTypeOptions(type);
                ViewBag.OpenOptions = new YJ.Platform.Dictionary().GetOptionsByCode("appopenmodel", value: "");
            }
            else
            {
                oldXML = appLibrary.Serialize();
                ViewBag.TypeOptions = new YJ.Platform.AppLibrary().GetTypeOptions(appLibrary.Type.ToString());
                ViewBag.OpenOptions = new YJ.Platform.Dictionary().GetOptionsByCode("appopenmodel", value: appLibrary.OpenMode.ToString());
            }

            if (collection != null)
            {
                string title = collection["title"];
                string address = collection["address"];
                string openModel = collection["openModel"];
                string width = collection["width"];
                string height = collection["height"];
                string params1 = collection["Params"];
                string note = collection["Note"];
                string Ico = collection["Ico"];
                string IcoColor = collection["IcoColor"];
                type = collection["type"];


                appLibrary.Address = address.Trim();
                appLibrary.Height = height.ToIntOrNull();
                appLibrary.Note = note;
                appLibrary.OpenMode = openModel.ToInt();
                appLibrary.Params = params1;
                appLibrary.Title = title;
                appLibrary.Type = type.ToGuid();
                appLibrary.Width = width.ToIntOrNull();

                if (!Ico.IsNullOrEmpty())
                {
                    appLibrary.Ico = Ico;
                }
                else
                {
                    appLibrary.Ico = null;
                }
                if (!IcoColor.IsNullOrEmpty())
                {
                    appLibrary.Color = IcoColor.Trim();
                }
                else
                {
                    appLibrary.Color = null;
                }
                string pagesize = Request.QueryString["pagesize"];
                string pagenumber = Request.QueryString["pagenumber"];
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    if (isAdd)
                    {
                        bappLibrary.Add(appLibrary);
                        YJ.Platform.Log.Add("添加了应用程序库", appLibrary.Serialize(), YJ.Platform.Log.Types.菜单权限);
                        ViewBag.Script = "alert('添加成功!');new RoadUI.Window().reloadOpener(undefined,undefined,\"query('" + pagesize + "','" + pagenumber + "')\");new RoadUI.Window().close();";
                    }
                    else
                    {
                        bappLibrary.Update(appLibrary);
                        YJ.Platform.Log.Add("修改了应用程序库", "", YJ.Platform.Log.Types.菜单权限, oldXML, appLibrary.Serialize());
                        ViewBag.Script = "alert('修改成功!');new RoadUI.Window().reloadOpener(undefined,undefined,\"query('" + pagesize + "','" + pagenumber + "')\");new RoadUI.Window().close();";
                    }
                    
                    YJ.Platform.AppLibraryButtons1 Sub1 = new YJ.Platform.AppLibraryButtons1();
                    string buttonindex = Request.Form["buttonindex"] ?? "";
                    var buttons = Sub1.GetAllByAppID(appLibrary.ID);
                    List<YJ.Data.Model.AppLibraryButtons1> buttons1 = new List<YJ.Data.Model.AppLibraryButtons1>();
                    foreach (var index in buttonindex.Split(','))
                    {
                        string button_ = Request.Form["button_" + index];
                        string buttonname_ = Request.Form["buttonname_" + index];
                        string buttonevents_ = Request.Form["buttonevents_" + index];
                        string buttonico_ = Request.Form["buttonico_" + index];
                        string showtype_ = Request.Form["showtype_" + index];
                        string buttonsort_ = Request.Form["buttonsort_" + index];
                        if (buttonname_.IsNullOrEmpty() || buttonevents_.IsNullOrEmpty())
                        {
                            continue;
                        }
                        YJ.Data.Model.AppLibraryButtons1 sub1 = buttons.Find(p => p.ID == index.ToGuid());
                        bool isAdd1 = false;
                        if (sub1 == null)
                        {
                            isAdd1 = true;
                            sub1 = new YJ.Data.Model.AppLibraryButtons1();
                            sub1.ID = Guid.NewGuid();
                        }
                        else
                        {
                            buttons1.Add(sub1);
                        }
                        sub1.AppLibraryID = appLibrary.ID;
                        if (button_.IsGuid())
                        {
                            sub1.ButtonID = button_.ToGuid();
                        }
                        sub1.Events = buttonevents_;
                        sub1.Ico = buttonico_;
                        sub1.Name = buttonname_.Trim1();
                        sub1.Sort = buttonsort_.ToInt(0);
                        sub1.ShowType = showtype_.ToInt(0);
                        sub1.Type = 0;
                        if (isAdd1)
                        {
                            Sub1.Add(sub1);
                        }
                        else
                        {
                            Sub1.Update(sub1);
                        }
                    }
                    foreach (var sub in buttons)
                    {
                        if (buttons1.Find(p => p.ID == sub.ID) == null)
                        {
                            Sub1.Delete(sub.ID);
                        }
                    }
                    scope.Complete();
                    Sub1.ClearCache();
                }
                new YJ.Platform.Menu().ClearAllDataTableCache();
                new YJ.Platform.WorkFlow().ClearStartFlowsCache();
                bappLibrary.ClearCache();
                
            }
            return View(appLibrary);
        }

        public ActionResult SubPages()
        {
            return View();
        }

        public ActionResult SubPageEdit()
        {
            return SubPageEdit(null);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubPageEdit(FormCollection collection)
        {
            YJ.Platform.AppLibrarySubPages Sub = new YJ.Platform.AppLibrarySubPages();
            YJ.Data.Model.AppLibrarySubPages sub = null;
            string subid = Request.QueryString["subid"];
            if (subid.IsGuid())
            {
                sub = Sub.Get(subid.ToGuid());
            }

            if (collection != null)
            {
                string Title = Request.Form["Title"];
                string Address = Request.Form["Address"];

                bool isAdd = false;
                if (sub == null)
                {
                    sub = new YJ.Data.Model.AppLibrarySubPages();
                    isAdd = true;
                    sub.ID = Guid.NewGuid();
                    sub.AppLibraryID = Request.QueryString["id"].ToGuid();
                }
                sub.Name = Title.Trim1();
                sub.Address = Address.Trim1();
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    if (isAdd)
                    {
                        Sub.Add(sub);
                        YJ.Platform.Log.Add("添加了子页面", sub.Serialize(), YJ.Platform.Log.Types.菜单权限);
                        ViewBag.Script = "alert('添加成功!');window.location='SubPages" + Request.Url.Query + "';";
                    }
                    else
                    {
                        Sub.Update(sub);
                        YJ.Platform.Log.Add("修改了子页面", sub.Serialize(), YJ.Platform.Log.Types.菜单权限);
                        ViewBag.Script = "alert('保存成功!');window.location='SubPages" + Request.Url.Query + "';";
                    }

                    YJ.Platform.AppLibraryButtons1 Sub1 = new YJ.Platform.AppLibraryButtons1();
                    string buttonindex = Request.Form["buttonindex"] ?? "";
                    var buttons = Sub1.GetAllByAppID(sub.ID);
                    List<YJ.Data.Model.AppLibraryButtons1> buttons1 = new List<YJ.Data.Model.AppLibraryButtons1>();
                    foreach (var index in buttonindex.Split(','))
                    {
                        string button_ = Request.Form["button_" + index];
                        string buttonname_ = Request.Form["buttonname_" + index];
                        string buttonevents_ = Request.Form["buttonevents_" + index];
                        string buttonico_ = Request.Form["buttonico_" + index];
                        string showtype_ = Request.Form["showtype_" + index];
                        string buttonsort_ = Request.Form["buttonsort_" + index];
                        if (buttonname_.IsNullOrEmpty() || buttonevents_.IsNullOrEmpty())
                        {
                            continue;
                        }
                        YJ.Data.Model.AppLibraryButtons1 sub1 = buttons.Find(p => p.ID == index.ToGuid());
                        bool isAdd1 = false;
                        if (sub1 == null)
                        {
                            isAdd1 = true;
                            sub1 = new YJ.Data.Model.AppLibraryButtons1();
                            sub1.ID = Guid.NewGuid();
                        }
                        else
                        {
                            buttons1.Add(sub1);
                        }
                        sub1.AppLibraryID = sub.ID;
                        if (button_.IsGuid())
                        {
                            sub1.ButtonID = button_.ToGuid();
                        }
                        sub1.Events = buttonevents_;
                        sub1.Ico = buttonico_;
                        sub1.Name = buttonname_.Trim1();
                        sub1.Sort = buttonsort_.ToInt(0);
                        sub1.ShowType = showtype_.ToInt(0);
                        sub1.Type = 0;
                        if (isAdd1)
                        {
                            Sub1.Add(sub1);
                        }
                        else
                        {
                            Sub1.Update(sub1);
                        }
                    }
                    foreach (var sub1 in buttons)
                    {
                        if (buttons1.Find(p => p.ID == sub1.ID) == null)
                        {
                            Sub1.Delete(sub1.ID);
                        }
                    }
                    scope.Complete();
                    Sub1.ClearCache();
                    Sub.ClearCache();
                }

            }

            if (sub == null)
            {
                sub = new YJ.Data.Model.AppLibrarySubPages();
                sub.ID = Guid.Empty;
                sub.AppLibraryID = Request.QueryString["id"].ToGuid();
            }
            return View(sub);
        }

        public RedirectResult SubPageDelete()
        {
            string subpagesbox = Request.Form["subpagesbox"] ?? "";
            YJ.Platform.AppLibrarySubPages Sub = new YJ.Platform.AppLibrarySubPages();
            YJ.Platform.AppLibraryButtons1 But = new YJ.Platform.AppLibraryButtons1();
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                foreach (var subindex in subpagesbox.Split(','))
                {
                    if (subindex.IsGuid())
                    {
                        Sub.Delete(subindex.ToGuid());
                        But.DeleteByAppID(subindex.ToGuid());
                    }
                }
                YJ.Platform.Log.Add("删除了子页面", subpagesbox, YJ.Platform.Log.Types.菜单权限);
                scope.Complete();
            }
            Sub.ClearCache();
            But.ClearCache();
            return Redirect("SubPages" + Request.Url.Query);
        }

        [MyAttribute(CheckApp = false, CheckLogin=false, CheckUrl=false)]
        public string GetApps()
        {
            string type = Request.Form["type"];
            string appid = Request.Form["value"];
            return new YJ.Platform.AppLibrary().GetAppsOptions(type.ToGuid(), appid);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAttribute(CheckApp=false)]
        public string Query()
        {
            string Title = Request.Form["Title"];
            string Address = Request.Form["Address"];
            string typeid = Request.Form["typeid"];
            string sidx = Request.Form["sidx"];
            string sord = Request.Form["sord"];


            YJ.Platform.Dictionary bdict = new YJ.Platform.Dictionary();
            YJ.Platform.AppLibrary bapp = new YJ.Platform.AppLibrary();
            string typeidstring = typeid.IsGuid() ? bapp.GetAllChildsIDString(typeid.ToGuid()) : "";
            long count;
            int pageSize = YJ.Utility.Tools.GetPageSize();
            int pageNumber = YJ.Utility.Tools.GetPageNumber();
            string order = (sidx.IsNullOrEmpty() ? "Title" : sidx) + " " + (sord.IsNullOrEmpty() ? "asc" : sord);
            List<YJ.Data.Model.AppLibrary> appList = bapp.GetPagerData(out count, pageSize, pageNumber, Title.Trim1(), typeidstring, Address.Trim1(), order);
            LitJson.JsonData json = new LitJson.JsonData();
            foreach (var app in appList)
            {
                string ico = string.Empty;
                if(app.Ico.IsFontIco())
                {
                    ico = "<i class=\"fa " + app.Ico.Trim1() + "\" style=\"font-size:14px;vertical-align:middle;" + (app.Color.IsNullOrEmpty() ? "" : "color:" + app.Color + ";") + "\"></i>";
                }
                else
                {
                    ico = "<img src=\"" + app.Ico.Trim1() + "\" style=\"vertical-align:middle;\" />";
                }
                LitJson.JsonData j = new LitJson.JsonData();
                j["id"] = app.ID.ToString();
                j["Title"] = ico + "<span style=\"vertical-align:middle;margin-left:4px;\">" + app.Title + "</span>";
                j["Address"] = app.Address;
                j["TypeTitle"] = bdict.GetTitle(app.Type);
                j["Opation"] = "<a class=\"editlink\" href=\"javascript:void(0);\" onclick=\"edit('" + app.ID.ToString() + "');return false;\" style=\"margin-right:6px;\">编辑</a><a class=\"editlink\" href=\"javascript:void(0);\" onclick=\"editsubpage('" + app.ID.ToString() + "');return false;\">子页面</a>";
                json.Add(j);
            }

            return "{\"userdata\":{\"total\":" + count + ",\"pagesize\":" + pageSize + ",\"pagenumber\":" + pageNumber + "},\"rows\":" + json.ToJson() + "}";
        }
    }
}
