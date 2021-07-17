using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace WebMvc.Controllers
{
    public class MenuController : MyController
    {
        //
        // GET: /Menu/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Empty()
        {
            return View();
        }

        public ActionResult Tree()
        {
            return View();
        }

        [MyAttribute(CheckApp=false)]
        public string Tree1()
        {
            YJ.Platform.Menu BMenu = new YJ.Platform.Menu();
            var appDt = BMenu.GetAllDataTable();
            if (appDt.Rows.Count == 0)
            {
                return "[]";
            }

            var root = appDt.Select("ParentID='" + Guid.Empty.ToString() + "'");
            if (root.Length == 0)
            {
                return "[]";
            }

            var apps = appDt.Select("ParentID='" + root[0]["ID"].ToString() + "'");
            StringBuilder json = new StringBuilder("[", 1000);
            System.Data.DataRow rootDr = root[0];
            string ico = rootDr["AppIco"].ToString();
            if (ico.IsNullOrEmpty())
            {
                ico = rootDr["Ico"].ToString();
            }
            json.Append("{");
            json.AppendFormat("\"id\":\"{0}\",", rootDr["ID"]);
            json.AppendFormat("\"title\":\"{0}\",", rootDr["Title"]);
            json.AppendFormat("\"ico\":\"{0}\",", ico);
            json.AppendFormat("\"link\":\"{0}\",", rootDr["Address"]);
            json.AppendFormat("\"type\":\"{0}\",", "0");
            json.AppendFormat("\"model\":\"{0}\",", rootDr["OpenMode"]);
            json.AppendFormat("\"width\":\"{0}\",", rootDr["Width"]);
            json.AppendFormat("\"height\":\"{0}\",", rootDr["Height"]);
            json.AppendFormat("\"hasChilds\":\"{0}\",", apps.Length > 0 ? "1" : "0");
            json.AppendFormat("\"childs\":[");

            for (int i = 0; i < apps.Length; i++)
            {
                System.Data.DataRow dr = apps[i];
                string ico1 = dr["AppIco"].ToString();
                if (ico1.IsNullOrEmpty())
                {
                    ico1 = dr["Ico"].ToString();
                }
                var childs = appDt.Select("ParentID='" + dr["ID"].ToString() + "'");
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", dr["ID"]);
                json.AppendFormat("\"title\":\"{0}\",", dr["Title"]);
                json.AppendFormat("\"ico\":\"{0}\",", ico1);
                json.AppendFormat("\"link\":\"{0}\",", dr["Address"]);
                json.AppendFormat("\"type\":\"{0}\",", "0");
                json.AppendFormat("\"model\":\"{0}\",", dr["OpenMode"]);
                json.AppendFormat("\"width\":\"{0}\",", dr["Width"]);
                json.AppendFormat("\"height\":\"{0}\",", dr["Height"]);
                json.AppendFormat("\"hasChilds\":\"{0}\",", childs.Length > 0 ? "1" : "0");
                json.AppendFormat("\"childs\":[");
                json.Append("]");
                json.Append("}");
                if (i < apps.Length - 1)
                {
                    json.Append(",");
                }
            }
            json.Append("]");
            json.Append("}");
            json.Append("]");

            return json.ToString();
        }

        [MyAttribute(CheckApp = false)]
        public string TreeRefresh()
        {
            string id = Request["refreshid"];
            if (!id.IsGuid())
            {
                return "[]";
            }
            YJ.Platform.Menu BMenu = new YJ.Platform.Menu();
            var childs = BMenu.GetAllDataTable().Select("ParentID='" + id + "'");
            System.Text.StringBuilder json = new System.Text.StringBuilder("[", childs.Length * 50);
            int count = childs.Length;
            int i = 0;
            foreach (var child in childs)
            {
                string ico1 = child["AppIco"].ToString();
                if (ico1.IsNullOrEmpty())
                {
                    ico1 = child["Ico"].ToString();
                }
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", child["ID"]);
                json.AppendFormat("\"title\":\"{0}\",", child["Title"]);
                json.AppendFormat("\"ico\":\"{0}\",", ico1);
                json.AppendFormat("\"link\":\"{0}\",", "");
                json.AppendFormat("\"type\":\"{0}\",", "0");
                json.AppendFormat("\"model\":\"{0}\",", "");
                json.AppendFormat("\"width\":\"{0}\",", "");
                json.AppendFormat("\"height\":\"{0}\",", "");
                json.AppendFormat("\"hasChilds\":\"{0}\",", BMenu.HasChild(child["ID"].ToString().ToGuid()) ? "1" : "0");
                json.AppendFormat("\"childs\":[");
                json.Append("]");
                json.Append("}");
                if (i++ < count - 1)
                {
                    json.Append(",");
                }
            }
            json.Append("]");
            return json.ToString();
        }

        public ActionResult Body()
        {
            return Body(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Body(FormCollection collection)
        {
            YJ.Platform.AppLibrary bappLibrary = new YJ.Platform.AppLibrary();
            YJ.Platform.Menu bMenu = new YJ.Platform.Menu();
            YJ.Data.Model.Menu menu = null;
            string id = Request.QueryString["id"];
            string name = string.Empty;
            string type = string.Empty;
            string appid = string.Empty;
            string params1 = string.Empty;
            string ico = string.Empty;
            string IcoColor = string.Empty;
            Guid appID;
            if (id.IsGuid(out appID))
            {
                menu = bMenu.Get(appID);
            }

            if (!Request.Form["Save"].IsNullOrEmpty())
            {
                name = Request.Form["Name"];
                type = Request.Form["Type"];
                appid = Request.Form["AppID"];
                params1 = Request.Form["Params"];
                ico = Request.Form["Ico"];
                IcoColor = Request.Form["IcoColor"];

                string oldXML = menu.Serialize();
                menu.Title = name.Trim();
                if (appid.IsGuid())
                {
                    menu.AppLibraryID = appid.ToGuid();
                }
                else
                {
                    menu.AppLibraryID = null;
                }
                menu.Params = params1.IsNullOrEmpty() ? null : params1.Trim();
                if (!ico.IsNullOrEmpty())
                {
                    menu.Ico = ico;
                }
                else
                {
                    menu.Ico = null;
                }
                if (!IcoColor.IsNullOrEmpty())
                {
                    menu.IcoColor = IcoColor;
                }
                else
                {
                    menu.IcoColor = null;
                }

                bMenu.Update(menu);

                YJ.Platform.Log.Add("修改了菜单", "", YJ.Platform.Log.Types.菜单权限, oldXML, menu.Serialize());
                string refreshID = menu.ParentID == Guid.Empty ? menu.ID.ToString() : menu.ParentID.ToString();
                ViewBag.Script = "parent.frames[0].reLoad('" + refreshID + "');alert('保存成功!');";
                bMenu.ClearAllDataTableCache();
            }

            if (!Request.Form["Delete"].IsNullOrEmpty())
            {
                int i = bMenu.DeleteAndAllChilds(menu.ID);
                YJ.Platform.Log.Add("删除了菜单及其所有下级共" + i.ToString() + "项", menu.Serialize(), YJ.Platform.Log.Types.菜单权限);
                string refreshID = menu.ParentID == Guid.Empty ? menu.ID.ToString() : menu.ParentID.ToString();
                ViewBag.Script = "parent.frames[0].reLoad('" + refreshID + "');window.location='Body?id=" + refreshID + "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "';";
                bMenu.ClearAllDataTableCache();
            }
            if (menu != null && menu.AppLibraryID.HasValue)
            {
                var app = new YJ.Platform.AppLibrary().Get(menu.AppLibraryID.Value);
                if (app != null)
                {
                    type = app.Type.ToString();
                }
            }
            ViewBag.AppTypesOptions = bappLibrary.GetTypeOptions(type);
            ViewBag.AppID = menu.AppLibraryID.ToString();
            return View(menu);
        }

        [MyAttribute(CheckApp = false)]
        public string GetApps()
        {
            string type = Request.Form["type"];
            string appid = Request.Form["value"];
            return new YJ.Platform.AppLibrary().GetAppsOptions(type.ToGuid(), appid);
        }

        public ActionResult AddApp()
        {
            return AddApp(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddApp(FormCollection collection)
        {
            YJ.Platform.AppLibrary bappLibrary = new YJ.Platform.AppLibrary();
            YJ.Platform.Menu bmenu = new YJ.Platform.Menu();
            YJ.Data.Model.Menu menu = null;

            string id = Request.QueryString["id"];

            if (collection != null)
            {
                menu = bmenu.Get(id.ToGuid());
                if (!Request.Form["Save"].IsNullOrEmpty())
                {
                    string name = Request.Form["Name"];
                    string type = Request.Form["Type"];
                    string appid = Request.Form["AppID"];
                    string params1 = Request.Form["Params"];
                    string ico = Request.Form["Ico"];
                    string IcoColor = Request.Form["IcoColor"];

                    YJ.Data.Model.Menu menu1 = new YJ.Data.Model.Menu();

                    menu1.ID = Guid.NewGuid();
                    menu1.ParentID = id.ToGuid();
                    menu1.Title = name.Trim();
                    menu1.Sort = bmenu.GetMaxSort(menu1.ParentID);
                    
                    if (appid.IsGuid())
                    {
                        menu1.AppLibraryID = appid.ToGuid();
                    }
                    else
                    {
                        menu1.AppLibraryID = null;
                    }
                    menu1.Params = params1.IsNullOrEmpty() ? null : params1.Trim();
                    if (!ico.IsNullOrEmpty())
                    {
                        menu1.Ico = ico;
                    }
                    if (!IcoColor.IsNullOrEmpty())
                    {
                        menu1.IcoColor = IcoColor;
                    }

                    bmenu.Add(menu1);
                    YJ.Platform.Log.Add("添加了菜单", menu1.Serialize(), YJ.Platform.Log.Types.菜单权限);
                    string refreshID = id;
                    ViewBag.Script = "alert('添加成功');parent.frames[0].reLoad('" + refreshID + "');";
                    bmenu.ClearAllDataTableCache();
                }

            }

            ViewBag.AppTypesOptions = bappLibrary.GetTypeOptions();
            return View();
        }

        public ActionResult Sort()
        {
            return Sort(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sort(FormCollection collection)
        {
            YJ.Platform.Menu bMenu = new YJ.Platform.Menu();
            List<YJ.Data.Model.Menu> menuList = new List<YJ.Data.Model.Menu>();
            string id = Request.QueryString["id"];
            var menu = bMenu.Get(id.ToGuid());
            menuList = bMenu.GetChild(menu.ParentID);

            if (collection != null)
            {
                string srots = Request.Form["sortapp"];
                if (srots.IsNullOrEmpty())
                {
                    return View(menuList);
                }
                string[] sortArray = srots.Split(new char[] { ',' });
                for (int i = 0; i < sortArray.Length; i++)
                {
                    Guid guid;
                    if (!sortArray[i].IsGuid(out guid))
                    {
                        continue;
                    }
                    bMenu.UpdateSort(guid, i + 1);
                }

                string rid = menu.ParentID.ToString();
                ViewBag.Script = "parent.frames[0].reLoad('" + rid + "');";
                menuList = bMenu.GetChild(menu.ParentID);
                bMenu.ClearAllDataTableCache();
            }
            return View(menuList);
        }

        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult Buttons()
        {
            string query = "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"];
            ViewBag.Query = query;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string ButtonsQuery()
        {
            string title = Request.Form["Title"];
            string sidx = Request.Form["sidx"];
            string sord = Request.Form["sord"];

            long count;
            int pageSize = YJ.Utility.Tools.GetPageSize();
            int pageNumber = YJ.Utility.Tools.GetPageNumber();
            string order = (sidx.IsNullOrEmpty() ? "Type" : sidx) + " " + (sord.IsNullOrEmpty() ? "asc" : sord);
            var list = new YJ.Platform.AppLibraryButtons().GetPagerData(out count, pageSize, pageNumber, title, order);
            LitJson.JsonData json = new LitJson.JsonData();
            foreach (var but in list)
            {
                LitJson.JsonData j = new LitJson.JsonData();
                j["id"] = but.ID.ToString();
                j["Name"] = but.Name;
                j["Ico"] = but.Ico.IsNullOrEmpty() ? "" : but.Ico.IsFontIco() ? "<i class='fa " + but.Ico + "' style='font-size:14px;vertical-align:middle;margin-right:3px;'></i>" : "<img src=\"" + Url.Content("~" + but.Ico) + "\" style=\"vertical-align:middle;\" />";
                j["Events"] = but.Events;
                j["Note"] = but.Note;
                j["Sort"] = but.Sort;
                j["Opation"] = "<a class=\"editlink\" href=\"javascript:void(0);\" onclick=\"add('" + but.ID + "');return false;\">编辑</a>";

                json.Add(j);
            }
            return "{\"userdata\":{\"total\":" + count + ",\"pagesize\":" + pageSize + ",\"pagenumber\":" + pageNumber + "},\"rows\":" + json.ToJson() + "}";
        }

        public ActionResult ButtionsEdit()
        {
            return ButtionsEdit(null);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ButtionsEdit(FormCollection collection)
        {
            YJ.Data.Model.AppLibraryButtons but = null;
            YJ.Platform.AppLibraryButtons But = new YJ.Platform.AppLibraryButtons();
            string butid = Request.QueryString["butid"];
            if (butid.IsGuid())
            {
                but = But.Get(butid.ToGuid());
            }

            if (collection != null)
            {
                string Name = Request.Form["Name"];
                string Events = Request.Form["Events"];
                string Ico = Request.Form["Ico"];
                string Note = Request.Form["Note"];
                string Sort = Request.Form["Sort"];

                bool isAdd = false;
                string oldxml = string.Empty;
                if (but == null)
                {
                    isAdd = true;
                    but = new YJ.Data.Model.AppLibraryButtons();
                    but.ID = Guid.NewGuid();

                }
                else
                {
                    oldxml = but.Serialize();
                }
                but.Name = Name.Trim1();
                but.Events = Events;
                but.Ico = Ico;
                but.Note = Note;
                but.Sort = Sort.ToInt();

                if (isAdd)
                {
                    But.Add(but);
                    YJ.Platform.Log.Add("添加了按钮", but.Serialize(), YJ.Platform.Log.Types.系统管理);
                }
                else
                {
                    But.Update(but);
                    YJ.Platform.Log.Add("修改了按钮", but.Serialize(), YJ.Platform.Log.Types.系统管理, oldxml);
                }
                ViewBag.Script = "alert('保存成功!');new RoadUI.Window().getOpenerWindow().query();new RoadUI.Window().close();";
            }

            string query = "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&title=";
            ViewBag.Query = query;
            if (but == null)
            {
                but = new YJ.Data.Model.AppLibraryButtons();
                but.Sort = But.GetMaxSort();
            }
            return View(but);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string ButtionsDelete()
        {
            YJ.Platform.AppLibraryButtons But = new YJ.Platform.AppLibraryButtons();
            string[] idarray = (Request.Form["ids"] ?? "").Split(',');
            foreach (var id in idarray)
            {
                var but = But.Get(id.ToGuid());
                if (but != null)
                {
                    But.Delete(but.ID);
                    YJ.Platform.Log.Add("删除了按钮", but.Serialize(), YJ.Platform.Log.Types.系统管理);
                }
            }
            return "删除成功!";
        }

        public ActionResult Refresh()
        {
            return View();
        }

        public string Refresh1()
        {
            new YJ.Platform.MenuUser().RefreshUsers();
            return "更新完成!";
        }
    }
}
