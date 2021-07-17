using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Controllers
{
    public class DocumentsController : MyController
    {
        //
        // GET: /Documents/

        public ActionResult Index()
        {
            return View();
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult Tree()
        {
            return View();
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult ListNoRead()
        {
            string query = "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&dirid=" + Request.QueryString["dirid"];
            ViewBag.Query = query;
            return View(); 
        }

        public ActionResult List()
        {
            string Query = "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&dirid=" + Request.QueryString["dirid"];
            ViewBag.Query = Query;
            return View();
        }

        public ActionResult DocAdd()
        {
            return DocAdd(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DocAdd(FormCollection collection)
        {
            string editID = Request.QueryString["docid"];
            YJ.Platform.Documents Doc = new YJ.Platform.Documents();
            YJ.Platform.DocumentDirectory DocDir = new YJ.Platform.DocumentDirectory();
            YJ.Platform.DocumentsReadUsers DocReadUsers = new YJ.Platform.DocumentsReadUsers();
            YJ.Data.Model.Documents doc = null;
            if (editID.IsGuid())
            {
                doc = Doc.Get(editID.ToGuid());
            }
            if (collection != null)
            {
                string DirectoryID = Request.Form["DirectoryID"];
                string Title1 = Request.Form["Title1"];
                string ReadUsers = Request.Form["ReadUsers"];
                string Source = Request.Form["Source"];
                string Contents = Request.Form["Contents"];
                string Files = Request.Form["Files"];
                string oldXML = string.Empty;
                bool isAdd = false;
                if (doc == null)
                {
                    isAdd = true;
                    doc = new YJ.Data.Model.Documents();
                    doc.ID = Guid.NewGuid();
                    doc.ReadCount = 0;
                    doc.WriteTime = YJ.Utility.DateTimeNew.Now;
                    doc.WriteUserID = YJ.Platform.Users.CurrentUserID;
                    doc.WriteUserName = YJ.Platform.Users.CurrentUserName;
                }
                else
                {
                    oldXML = doc.Serialize();
                }
                doc.Contents = Contents;
                doc.DirectoryID = DirectoryID.ToGuid();
                doc.DirectoryName = DocDir.GetName(doc.DirectoryID);
                doc.EditTime = YJ.Utility.DateTimeNew.Now;
                doc.EditUserID = YJ.Platform.Users.CurrentUserID;
                doc.EditUserName = YJ.Platform.Users.CurrentUserName;
                doc.Files = Files;
                doc.ReadUsers = ReadUsers;
                doc.Source = Source.IsNullOrEmpty() ? " " : Source;
                doc.Title = Title1.Trim1();

                if (isAdd)
                {
                    Doc.Add(doc);
                    YJ.Platform.Log.Add("添加了文档", doc.Serialize(), YJ.Platform.Log.Types.文档中心);
                }
                else
                {
                    Doc.Update(doc);
                    YJ.Platform.Log.Add("修改了文档", doc.Serialize(), YJ.Platform.Log.Types.文档中心, oldXML, doc.Serialize());
                }

                //更新阅读人员
                var users = doc.ReadUsers.IsNullOrEmpty() ? DocDir.GetReadUsers(doc.DirectoryID) : new YJ.Platform.Organize().GetAllUsers(doc.ReadUsers);
                DocReadUsers.Delete(doc.ID);

                bool isWeiXin = YJ.Platform.WeiXin.Config.IsUse;
                YJ.Platform.WeiXin.Message weiXinMsg = new YJ.Platform.WeiXin.Message();
                System.Text.StringBuilder wxUsers = new System.Text.StringBuilder();
                foreach (var user in users)
                {
                    YJ.Data.Model.DocumentsReadUsers docReadUsers = new YJ.Data.Model.DocumentsReadUsers();
                    docReadUsers.DocumentID = doc.ID;
                    docReadUsers.IsRead = 0;
                    docReadUsers.UserID = user.ID;
                    DocReadUsers.Add(docReadUsers);
                    if (isWeiXin)
                    {
                        wxUsers.Append(user.Account);
                        wxUsers.Append('|');
                    }
                }
                string url = string.Empty;
                if (isAdd)
                {
                    url = "'List" + Request.Url.Query + "'";
                }
                else
                {
                    url = "'DocRead" + Request.Url.Query + "'";
                }
                if (isWeiXin)
                {
                    weiXinMsg.SendText(doc.Title, wxUsers.ToString().TrimEnd('|'), agentid: new YJ.Platform.WeiXin.Agents().GetAgentIDByCode("weixinagents_documents"), async: true);
                }

                ViewBag.script = "alert('保存成功!');window.location=" + url + ";";
            }
            if (doc == null) doc = new YJ.Data.Model.Documents();

            return View(doc);
        }

        public ActionResult DirAdd()
        {
            return DirAdd(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DirAdd(FormCollection collection)
        {
            string DirID = Request.QueryString["DirID"];
            string currentDirID = Request.QueryString["currentDirID"];
            YJ.Platform.DocumentDirectory DocDir = new YJ.Platform.DocumentDirectory();
            YJ.Data.Model.DocumentDirectory docDir = null;
            if (currentDirID.IsGuid())
            {
                docDir = DocDir.Get(currentDirID.ToGuid());
            }
            if (collection != null)
            {
                string Name = Request.Form["Name"];
                string ReadUsers = Request.Form["ReadUsers"];
                string PublishUsers = Request.Form["PublishUsers"];
                string ManageUsers = Request.Form["ManageUsers"];
                string Sort = Request.Form["Sort"];
                bool isAdd = false;
                string oldxml = string.Empty;
                if (docDir == null)
                {
                    isAdd = true;
                    docDir = new YJ.Data.Model.DocumentDirectory();
                    docDir.ID = Guid.NewGuid();
                    docDir.ParentID = DirID.ToGuid();
                }
                else
                {
                    oldxml = docDir.Serialize();
                }
                docDir.ManageUsers = ManageUsers;
                docDir.Name = Name.Trim1();
                docDir.PublishUsers = PublishUsers;
                docDir.ReadUsers = ReadUsers;
                docDir.Sort = Sort.ToInt();

                if (isAdd)
                {
                    DocDir.Add(docDir);
                    YJ.Platform.Log.Add("添加了栏目", docDir.Serialize(), YJ.Platform.Log.Types.文档中心);
                }
                else
                {
                    DocDir.Update(docDir);
                    YJ.Platform.Log.Add("修改了栏目", docDir.Serialize(), YJ.Platform.Log.Types.文档中心, oldxml, docDir.Serialize());
                }
                DocDir.ClearDirUsersCache(docDir.ID);
                DocDir.ClearCache();
                ViewBag.script = "parent.frames[0].reLoad('" + docDir.ParentID + "');alert('保存成功!');window.location='List" + Request.Url.Query + "';";
            }
            if (docDir == null)
            {
                docDir = new YJ.Data.Model.DocumentDirectory();
                docDir.Sort = DocDir.GetMaxSort(DirID.ToGuid());
                docDir.ParentID = DirID.ToGuid();
            }
            return View(docDir);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DirDelete(FormCollection collection)
        {
            string DirID = Request.QueryString["DirID"];
            string currentDirID = Request.QueryString["currentDirID"];
            YJ.Platform.DocumentDirectory DocDir = new YJ.Platform.DocumentDirectory();
            YJ.Data.Model.DocumentDirectory docDir = null;
            if (currentDirID.IsGuid())
            {
                docDir = DocDir.Get(currentDirID.ToGuid());
            }
            if (docDir == null)
            {
                ViewBag.script = "alert('栏目为空!');window.location='List" + Request.Url.Query + "';";
                return View();
            }
            if (docDir.ParentID == Guid.Empty)
            {
                ViewBag.script = "alert('根栏目不能删除根栏目!');window.location=window.location;";
                return View();
            }
            var childIdString = DocDir.GetAllChildIdString(docDir.ID);
            YJ.Platform.Documents Doc = new YJ.Platform.Documents();
            foreach (string id in childIdString.Split(','))
            {
                DocDir.Delete(id.ToGuid());
                Doc.DeleteByDirectoryID(id.ToGuid());
                DocDir.ClearDirUsersCache(id.ToGuid());
            }
            DocDir.ClearCache();
            YJ.Platform.Log.Add("删除的文档栏目及其所有下级栏目", childIdString, YJ.Platform.Log.Types.文档中心);
            ViewBag.script = "parent.frames[0].reLoad('" + docDir.ParentID + "');alert('删除成功!');window.location='List" + Request.Url.Query + "';";
            return View();
        }

        [MyAttribute(CheckApp = false, CheckUrl = false)]
        public ActionResult DocRead()
        {
            YJ.Data.Model.Documents doc = null;
            YJ.Platform.Documents Doc = new YJ.Platform.Documents();
            YJ.Platform.DocumentsReadUsers DocReadUsers = new YJ.Platform.DocumentsReadUsers();
            bool IsEdit = false;//是否可以编辑文档
            string DocID = Request.QueryString["docid"];
            var CurrentUserID = YJ.Platform.Users.CurrentUserID.IsEmptyGuid() ? YJ.Platform.WeiXin.Organize.CurrentUserID : YJ.Platform.Users.CurrentUserID;
            if (DocID.IsGuid())
            {
                doc = Doc.Get(DocID.ToGuid());
                if (doc != null)
                {
                    var readusers = DocReadUsers.Get(doc.ID, CurrentUserID);
                    if (readusers == null)
                    {
                        Response.Write("您无权查看该文档!");
                        Response.End();
                        return null;
                    }
                    IsEdit = new YJ.Platform.DocumentDirectory().HasPublish(doc.DirectoryID, CurrentUserID) ||
                        new YJ.Platform.DocumentDirectory().HasManage(doc.DirectoryID, CurrentUserID);
                    Doc.UpdateReadCount(doc.ID);
                    DocReadUsers.UpdateRead(doc.ID, CurrentUserID);
                }
            }
            ViewBag.IsEdit = IsEdit;
           
            if (doc == null) doc = new YJ.Data.Model.Documents();
            return View(doc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string DocDelete()
        {
            YJ.Data.Model.Documents doc = null;
            YJ.Platform.Documents Doc = new YJ.Platform.Documents();
            string DocID = Request.QueryString["docid"];
            if (DocID.IsGuid())
            {
                doc = Doc.Get(DocID.ToGuid());
            }
            if (doc != null)
            {
                Doc.Delete(doc.ID);
                new YJ.Platform.DocumentsReadUsers().Delete(doc.ID);
                YJ.Platform.Log.Add("删除了文档", doc.Serialize(), YJ.Platform.Log.Types.文档中心);
                return "1";
            }
            else
            {
                return "未找到文档";
            }
        }

        [MyAttribute(CheckApp = false)]
        public string Tree1()
        {
            return new YJ.Platform.DocumentDirectory().GetTreeJsonString();
        }

        [MyAttribute(CheckApp = false)]
        public string TreeRefresh()
        {
            string refreshid = Request["refreshid"];
            if (refreshid.IsGuid())
            {
                return new YJ.Platform.DocumentDirectory().GetTreeRefreshJsonString(refreshid.ToGuid());
            }
            else
            {
                return "[]";
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAttribute(CheckApp=false)]
        public string QueryNoRead()
        {
            string DirID = Request.Form["DirID"];
            string Title1 = Request.Form["Title1"];
            string Date1 = Request.Form["Date1"];
            string Date2 = Request.Form["Date2"];
            string sidx = Request.Form["sidx"];
            string sord = Request.Form["sord"];
            
            YJ.Platform.Documents Doc = new YJ.Platform.Documents();
            YJ.Platform.DocumentDirectory DocDir = new YJ.Platform.DocumentDirectory();
            string dirIdString = "";
            if (DirID.IsGuid())
            {
                dirIdString = new YJ.Platform.DocumentDirectory().GetAllChildIdString(DirID.ToGuid(), YJ.Platform.Users.CurrentUserID);
            }
            long count;
            int pageSize = YJ.Utility.Tools.GetPageSize();
            int pageNumber = YJ.Utility.Tools.GetPageNumber();
            string order = (sidx.IsNullOrEmpty() ? "WriteTime" : sidx) + " " + (sord.IsNullOrEmpty() ? "asc" : sord);
            var DocDt = Doc.GetList(out count, pageSize, pageNumber, dirIdString, YJ.Platform.Users.CurrentUserID.ToString(), Title1.Trim1(), Date1, Date2, true, order);
            LitJson.JsonData json = new LitJson.JsonData();
            foreach(System.Data.DataRow dr in DocDt.Rows)
            {
                LitJson.JsonData j = new LitJson.JsonData();
                j["Title"] = "<a class=\"blue\" href=\"javascript:;\" onclick='showDoc(\"" + dr["ID"].ToString() + "\",\"{{window.encodeURIComponent(value.Title)}}\");return false;'>" + dr["Title"].ToString() + "</a><span style=\"margin-left:5px;\"><img src=\"../Images/loading/new.png\" style=\"border:0 none; vertical-align:middle;\" /></span>";
                j["DirectoryName"] = dr["DirectoryName"].ToString();
                j["WriteUserName"] = dr["WriteUserName"].ToString();
                j["WriteTime"] = dr["WriteTime"].ToString().ToDateTimeString();
                j["ReadCount"] = dr["ReadCount"].ToString();
                json.Add(j);
            }

            return "{\"userdata\":{\"total\":" + count + ",\"pagesize\":" + pageSize + ",\"pagenumber\":" + pageNumber + "},\"rows\":" + json.ToJson() + "}";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAttribute(CheckApp = false)]
        public string Query()
        {
            string DirID = Request.Form["DirID"];
            string Title1 = Request.Form["Title1"];
            string Date1 = Request.Form["Date1"];
            string Date2 = Request.Form["Date2"];
            string sidx = Request.Form["sidx"];
            string sord = Request.Form["sord"];
            YJ.Platform.Documents Doc = new YJ.Platform.Documents();
            YJ.Platform.DocumentDirectory DocDir = new YJ.Platform.DocumentDirectory();
            string dirIdString = "";
            if (DirID.IsGuid())
            {
                dirIdString = new YJ.Platform.DocumentDirectory().GetAllChildIdString(DirID.ToGuid(), YJ.Platform.Users.CurrentUserID);
            }
            long count;
            int pageSize = YJ.Utility.Tools.GetPageSize();
            int pageNumber = YJ.Utility.Tools.GetPageNumber();
            string order = (sidx.IsNullOrEmpty() ? "WriteTime" : sidx) + " " + (sord.IsNullOrEmpty() ? "asc" : sord);
            var DocDt = Doc.GetList(out count, pageSize, pageNumber, dirIdString, YJ.Platform.Users.CurrentUserID.ToString(), Title1.Trim1(), Date1, Date2, false, order);
            LitJson.JsonData json = new LitJson.JsonData();
            foreach (System.Data.DataRow dr in DocDt.Rows)
            {
                LitJson.JsonData j = new LitJson.JsonData();
                j["Title"] = "<a class=\"blue\" href=\"javascript:;\" onclick='showDoc(\"" + dr["ID"].ToString() + "\",\"{{window.encodeURIComponent(value.Title)}}\");return false;'>" + dr["Title"].ToString() + "</a><span style=\"margin-left:5px;\">"
                    + ("0" == dr["IsRead"].ToString() ? "<img src=\"../Images/loading/new.png\" style=\"border:0 none; vertical-align:middle;\" />" : "")
                    + "</span>";
                j["DirectoryName"] = dr["DirectoryName"].ToString();
                j["WriteUserName"] = dr["WriteUserName"].ToString();
                j["WriteTime"] = dr["WriteTime"].ToString().ToDateTimeString();
                j["ReadCount"] = dr["ReadCount"].ToString();
                json.Add(j);
            }

            return "{\"userdata\":{\"total\":" + count + ",\"pagesize\":" + pageSize + ",\"pagenumber\":" + pageNumber + "},\"rows\":" + json.ToJson() + "}";
        }
    }
}
