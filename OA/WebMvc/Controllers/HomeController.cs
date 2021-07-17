using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Controllers
{
    public class HomeController : MyController
    {
        //
        // GET: /Home/
        [MyAttribute(CheckApp = false, CheckUrl = false)]
        public ActionResult Index()
        {
            var user = CurrentUser;
            ViewBag.UserName = user == null ? "" : user.Name;
            ViewBag.DateTime = CurrentDateTime.ToDateWeekString();

            //得到未读消息
            var noReadMsg = new YJ.Platform.ShortMessage().GetAllNoReadByUserID(user.ID);
            if (noReadMsg.Count > 0)
            {
                LitJson.JsonData jd = new LitJson.JsonData();
                string msgContents = string.Empty;
                var noReadMsg1 = noReadMsg.OrderByDescending(p => p.SendTime).FirstOrDefault();
                if (!noReadMsg1.LinkUrl.IsNullOrEmpty())
                {
                    msgContents = "<a class=\"blue1\" href=\"" + noReadMsg1.LinkUrl + "\">" + noReadMsg1.Contents.RemoveHTML() + "</a>";
                }
                else
                {
                    msgContents = noReadMsg1.Contents.RemoveHTML();
                }
                jd["title"] = noReadMsg1.Title;
                jd["contents"] = msgContents;
                jd["count"] = noReadMsg.Count;
                ViewBag.NoReadMsgJson = jd.ToJson();
            }

            //得到头像
            string HeadImg = Url.Content("~/Content/UserHeads/default.jpg");
            if (!user.HeadImg.IsNullOrEmpty() && System.IO.File.Exists(Server.MapPath(Url.Content("~" + user.HeadImg))))
            {
                HeadImg = Url.Content("~" + user.HeadImg);
            }
            ViewBag.HeadImg = HeadImg;

            return View();
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult Home()
        {
            return View();
        }

        [MyAttribute(CheckApp = false)]
        public string Menu()
        {
            string userid = Request.QueryString["userid"];
            Guid uid = userid.IsGuid() ? userid.ToGuid() : YJ.Platform.Users.CurrentUserID;
            bool showSource = "1" == Request.QueryString["showsource"];
            if (uid.IsEmptyGuid())
            {
                return "[]";
            }
            else
            {
                return new YJ.Platform.Menu().GetUserMenuJsonString(uid, Url.Content("~/").TrimEnd('/'), showSource);
            }
        }

        [MyAttribute(CheckApp = false)]
        public string MenuRefresh()
        {
            string userid = Request.QueryString["userid"];
            string refreshID = Request.QueryString["refreshid"];
            bool showSource = "1" == Request.QueryString["showsource"];
            Guid refreshid;
            Guid uid = YJ.Platform.Users.CurrentUserID; 
            if (!refreshID.IsGuid(out refreshid))
            {
                return "[]";
            }
            if (!refreshid.IsEmptyGuid())
            {
                return new YJ.Platform.Menu().GetUserMenuRefreshJsonString(uid, refreshid, Url.Content("~/").TrimEnd('/'), showSource);
            }
            else
            {
                return "[]";
            }
        }

        [MyAttribute(CheckApp = false)]
        public string MenuRefresh1()
        {
            string refreshID = Request.QueryString["refreshid"];
            string isrefresh1 = Request.QueryString["isrefresh1"];
            Guid refreshid;
            Guid uid = YJ.Platform.Users.CurrentUserID;
            if (!refreshID.IsGuid(out refreshid))
            {
                return "";
            }
            if (!refreshid.IsEmptyGuid())
            {
                return new YJ.Platform.Menu().GetUserMenuChilds(uid, refreshid, Url.Content("~/").TrimEnd('/'), isrefresh1);
            }
            else
            {
                return "";
            }
        }

        public ActionResult SetList()
        {
            YJ.Platform.HomeItems BHI = new YJ.Platform.HomeItems();
            ViewBag.TypeOptions = BHI.getTypeOptions(); 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Query()
        {
            YJ.Platform.HomeItems BHI = new YJ.Platform.HomeItems();
            YJ.Platform.Organize BORG = new YJ.Platform.Organize();
            List<YJ.Data.Model.HomeItems> HIList = new List<YJ.Data.Model.HomeItems>();
            string s_Name1 = Request.Form["Name1"];
            string s_Title1 = Request.Form["Title1"];
            string s_Type = Request.Form["Type"];
            string sidx = Request.Form["sidx"];
            string sord = Request.Form["sord"];

            long count;
            int pageSize = YJ.Utility.Tools.GetPageSize();
            int pageNumber = YJ.Utility.Tools.GetPageNumber();
            string order = (sidx.IsNullOrEmpty() ? "Type" : sidx) + " " + (sord.IsNullOrEmpty() ? "asc" : sord);
            
            HIList = BHI.GetList(out count, pageSize, pageNumber, s_Name1, s_Title1, s_Type, order);

            LitJson.JsonData json = new LitJson.JsonData();
            foreach (var hi in HIList)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                if (!hi.Ico.IsNullOrEmpty())
                {
                    if (hi.Ico.IsFontIco())
                    {
                        sb.Append("<i class='fa " + hi.Ico + "' style='font-size:14px;vertical-align:middle;margin-right:3px;'></i>");
                    }
                    else
                    {
                        sb.Append("<img src='" + Url.Content("~" + hi.Ico) + "' style='vertical-align:middle;margin-right:3px;'/>");
                    }
                }
                sb.Append(hi.Title);
                LitJson.JsonData j = new LitJson.JsonData();
                j["id"] = hi.ID.ToString();
                j["Name"] = hi.Name;
                j["Title"] = sb.ToString();
                j["Type"] = BHI.GetTypeTitle(hi.Type);
                j["DataSourceType"] = BHI.GetDataSourceTitle(hi.DataSourceType);
                j["UseOrganizes"] = BORG.GetNames(hi.UseOrganizes);
                j["Note"] = hi.Note;
                j["Opation"] = "<a class=\"editlink\" href=\"javascript:void(0);\" onclick=\"edit('" + hi.ID + "');return false;\">编辑</a>";
                json.Add(j);
            }

            return "{\"userdata\":{\"total\":" + count + ",\"pagesize\":" + pageSize + ",\"pagenumber\":" + pageNumber + "},\"rows\":" + json.ToJson() + "}";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Delete()
        {
            YJ.Platform.HomeItems BHI = new YJ.Platform.HomeItems();
            string ids = Request.Form["ids"] ?? "";
            foreach (string id in ids.Split(','))
            {
                BHI.Delete(id.ToGuid());
                YJ.Platform.Log.Add("删除了首页模块设置", id, YJ.Platform.Log.Types.其它分类);
            }
            BHI.ClearCache();
            return "删除成功!";
        }

        public ActionResult SetAdd()
        {
            return SetAdd(null);
        }

        [HttpPost]
        public ActionResult SetAdd(FormCollection collection)
        {
            YJ.Platform.HomeItems BHI = new YJ.Platform.HomeItems();
            YJ.Data.Model.HomeItems hi = null;

            string id = Request.QueryString["id"];
            if (id.IsGuid())
            {
                hi = BHI.Get(id.ToGuid());
            }

            if (collection != null)
            {
                string Name1 = Request.Form["Name1"];
                string Title1 = Request.Form["Title1"];
                string Type = Request.Form["Type"];
                string DataSourceType = Request.Form["DataSourceType"];
                string DataSource = Request.Form["DataSource"];
                string Ico = Request.Form["Ico"];
                string BgColor = Request.Form["BgColor"];
                string UseOrganizes = Request.Form["UseOrganizes"];
                string DBConnID = Request.Form["DBConnID"];
                string LinkURL = Request.Form["LinkURL"];
                string Note = Request.Form["Note"];
                string Sort = Request.Form["Sort"];

                bool isAdd = false;
                if (hi == null)
                {
                    hi = new YJ.Data.Model.HomeItems();
                    hi.ID = Guid.NewGuid();
                    isAdd = true;
                }
                hi.Title = Title1;
                hi.Name = Name1;
                hi.Type = Type.ToInt();
                hi.DataSourceType = DataSourceType.ToInt();
                hi.DataSource = DataSource;
                hi.Ico = Ico;
                hi.BgColor = BgColor;
                hi.UseOrganizes = UseOrganizes;
                hi.Sort = Sort.IsInt() ? Sort.ToInt() : BHI.GetMaxSort(hi.Type);
                if (DBConnID.IsGuid())
                {
                    hi.DBConnID = DBConnID.ToGuid();
                }
                else
                {
                    hi.DBConnID = null;
                }
                hi.LinkURL = LinkURL;
                hi.Note = Note;
                if (isAdd)
                {
                    BHI.Add(hi);
                }
                else
                {
                    BHI.Update(hi);
                }
                BHI.ClearCache();
                ViewBag.script = "alert('保存成功!');window.location='SetList" + Request.Url.Query + "';";
            }
            ViewBag.TypeOptions = BHI.getTypeOptions(hi==null?"":hi.Type.ToString());
            ViewBag.DataSourceTypeOptions = BHI.getDataSourceOptions(hi==null?"":hi.DataSourceType.ToString());
            ViewBag.DBConnIDOptions = new YJ.Platform.DBConnection().GetAllOptions(hi == null ? "" : hi.DBConnID.ToString());
            if (hi == null)
            {
                hi = new YJ.Data.Model.HomeItems();
               
            }
            return View(hi);
        }
    }
}
