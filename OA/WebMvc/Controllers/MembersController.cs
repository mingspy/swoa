using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace WebMvc.Controllers
{
    public class MembersController : MyController
    {
        //
        // GET: /Members/

        public ActionResult Index()
        {
            return View();
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult Tree()
        {
            return View();
        }

        [MyAttribute(CheckApp = false, CheckUrl = false, CheckLogin = false)]
        public string Tree1()
        {
            string loginMsg;
            if (!WebMvc.Common.Tools.CheckLogin(out loginMsg) && !YJ.Platform.WeiXin.Organize.CheckLogin())
            {
                return "";
            }

            string rootid = Request.QueryString["rootid"] ?? "";
            string showtype = Request.QueryString["showtype"] ?? "";
            string searchword = Request.QueryString["searchword"] ?? "";
            YJ.Platform.Organize BOrganize = new YJ.Platform.Organize();
            YJ.Platform.WorkGroup BWorkGroup = new YJ.Platform.WorkGroup();
            YJ.Platform.Users busers = new YJ.Platform.Users();
            
            System.Text.StringBuilder json = new System.Text.StringBuilder("[", 1000);

            if (!searchword.IsNullOrEmpty())
            {
                #region 搜索

                if ("1" == showtype)
                {
                    var s_workgroups = BWorkGroup.GetAll().FindAll(p => p.Name.Contains(searchword, StringComparison.CurrentCultureIgnoreCase));
                    json.Append("{");
                    json.AppendFormat("\"id\":\"{0}\",", Guid.Empty);
                    json.AppendFormat("\"parentID\":\"{0}\",", Guid.Empty);
                    json.AppendFormat("\"title\":\"查询“{0}”的结果\",", searchword);
                    json.AppendFormat("\"ico\":\"{0}\",", Url.Content("~/images/ico/search.png"));
                    json.AppendFormat("\"link\":\"{0}\",", "");
                    json.AppendFormat("\"type\":\"{0}\",", 1);
                    json.AppendFormat("\"hasChilds\":\"{0}\",", s_workgroups.Count);
                    json.Append("\"childs\":[");
                    int countwg = s_workgroups.Count;
                    int iwg = 0;
                    foreach (var wg in s_workgroups)
                    {
                        json.Append("{");
                        json.AppendFormat("\"id\":\"{0}\",", wg.ID);
                        json.AppendFormat("\"parentID\":\"{0}\",", Guid.Empty);
                        json.AppendFormat("\"title\":\"{0}\",", wg.Name);
                        json.AppendFormat("\"ico\":\"{0}\",", "");
                        json.AppendFormat("\"link\":\"{0}\",", "");
                        json.AppendFormat("\"type\":\"{0}\",", 5);
                        json.AppendFormat("\"hasChilds\":\"{0}\",", 0);
                        json.Append("\"childs\":[");
                        json.Append("]");
                        json.Append("}");
                        if (iwg++ < countwg - 1)
                        {
                            json.Append(",");
                        }
                    }
                }
                else
                {
                    var s_orgs = BOrganize.GetAll().FindAll(p => p.Name.Contains(searchword, StringComparison.CurrentCultureIgnoreCase));
                    var s_users = busers.GetAll().FindAll(p => p.Name.Contains(searchword, StringComparison.CurrentCultureIgnoreCase));
                    json.Append("{");
                    json.AppendFormat("\"id\":\"{0}\",", Guid.Empty);
                    json.AppendFormat("\"parentID\":\"{0}\",", Guid.Empty);
                    json.AppendFormat("\"title\":\"查询“{0}”的结果\",", searchword);
                    json.AppendFormat("\"ico\":\"{0}\",", Url.Content("~/images/ico/search.png"));
                    json.AppendFormat("\"link\":\"{0}\",", "");
                    json.AppendFormat("\"type\":\"{0}\",", 1);
                    json.AppendFormat("\"hasChilds\":\"{0}\",", s_orgs.Count + s_users.Count);
                    json.Append("\"childs\":[");
                    foreach (var org in s_orgs)
                    {
                        json.Append("{");
                        json.AppendFormat("\"id\":\"{0}\",", org.ID);
                        json.AppendFormat("\"parentID\":\"{0}\",", Guid.Empty);
                        json.AppendFormat("\"title\":\"{0}\",", org.Name);
                        json.AppendFormat("\"ico\":\"{0}\",", "");
                        json.AppendFormat("\"link\":\"{0}\",", "");
                        json.AppendFormat("\"type\":\"{0}\",", org.Type);
                        json.AppendFormat("\"hasChilds\":\"{0}\",", org.ChildsLength);
                        json.Append("\"childs\":[");
                        json.Append("]");
                        json.Append("}");
                        if (org.ID != s_orgs.Last().ID || s_users.Count > 0)
                        {
                            json.Append(",");
                        }
                    }
                    foreach (var user in s_users)
                    {
                        json.Append("{");
                        json.AppendFormat("\"id\":\"{0}\",", user.ID);
                        json.AppendFormat("\"parentID\":\"{0}\",", Guid.Empty);
                        json.AppendFormat("\"title\":\"{0}\",", user.Name);
                        json.AppendFormat("\"ico\":\"{0}\",", Url.Content("~/images/ico/contact_grey.png"));
                        json.AppendFormat("\"link\":\"{0}\",", "");
                        json.AppendFormat("\"type\":\"{0}\",", "4");
                        json.AppendFormat("\"hasChilds\":\"{0}\",", "0");
                        json.Append("\"childs\":[");
                        json.Append("]");
                        json.Append("}");
                        if (user.ID != s_users.Last().ID)
                        {
                            json.Append(",");
                        }
                    }
                }
                json.Append("]}]");
                return json.ToString();
                #endregion
            }

            if ("1" == showtype)
            {
                #region 显示角色组

                var workGroups = BWorkGroup.GetAll();

                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", Guid.Empty);
                json.AppendFormat("\"parentID\":\"{0}\",", Guid.Empty);
                json.AppendFormat("\"title\":\"{0}\",", "角色组");
                json.AppendFormat("\"ico\":\"{0}\",", Url.Content("~/images/ico/group.gif"));
                json.AppendFormat("\"link\":\"{0}\",", "");
                json.AppendFormat("\"type\":\"{0}\",", 5);
                json.AppendFormat("\"hasChilds\":\"{0}\",", workGroups.Count);
                json.Append("\"childs\":[");

                int countwg = workGroups.Count;
                int iwg = 0;
                foreach (var wg in workGroups)
                {
                    var users = BOrganize.GetAllUsers("w_" + wg.ID.ToString());
                    json.Append("{");
                    json.AppendFormat("\"id\":\"{0}\",", wg.ID);
                    json.AppendFormat("\"parentID\":\"{0}\",", Guid.Empty);
                    json.AppendFormat("\"title\":\"{0}\",", wg.Name);
                    json.AppendFormat("\"ico\":\"{0}\",", "");
                    json.AppendFormat("\"link\":\"{0}\",", "");
                    json.AppendFormat("\"type\":\"{0}\",", 5);
                    json.AppendFormat("\"hasChilds\":\"{0}\",", users.Count);
                    json.Append("\"childs\":[");
                    json.Append("]");
                    json.Append("}");
                    if (iwg++ < countwg - 1)
                    {
                        json.Append(",");
                    }
                }

                json.Append("]");
                json.Append("}");
                json.Append("]");
                Response.Write(json.ToString());
                Response.End();
                return "";
                #endregion
            }

            if (rootid.IsNullOrEmpty())
            {
                rootid = BOrganize.GetRoot().ID.ToString();
            }
            string[] rootIDArray = rootid.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            int m = 0;
            foreach (string rootID in rootIDArray)
            {
                List<YJ.Data.Model.Users> users = new List<YJ.Data.Model.Users>();
                Guid rootGuid = Guid.Empty;
                if (rootID.IsGuid(out rootGuid))
                {
                    var root = BOrganize.Get(rootGuid);
                    if (root != null)
                    {
                        users = busers.GetAllByOrganizeID(rootGuid);
                        json.Append("{");
                        json.AppendFormat("\"id\":\"{0}\",", root.ID);
                        json.AppendFormat("\"parentID\":\"{0}\",", root.ParentID);
                        json.AppendFormat("\"title\":\"{0}\",", root.Name);
                        json.AppendFormat("\"ico\":\"{0}\",", rootIDArray.Length == 1 ? Url.Content("~/images/ico/icon_site.gif") : "");
                        json.AppendFormat("\"link\":\"{0}\",", "");
                        json.AppendFormat("\"type\":\"{0}\",", root.Type);
                        json.AppendFormat("\"hasChilds\":\"{0}\",", root.ChildsLength == 0 && users.Count == 0 ? "0" : "1");
                        json.Append("\"childs\":[");
                    }
                }
                else if (rootID.StartsWith(YJ.Platform.Users.PREFIX))
                {
                    var root = busers.Get(busers.RemovePrefix1(rootID).ToGuid());
                    if (root != null)
                    {
                        json.Append("{");
                        json.AppendFormat("\"id\":\"{0}\",", root.ID);
                        json.AppendFormat("\"parentID\":\"{0}\",", Guid.Empty);
                        json.AppendFormat("\"title\":\"{0}\",", root.Name);
                        json.AppendFormat("\"ico\":\"{0}\",", Url.Content("~/images/ico/contact_grey.png"));
                        json.AppendFormat("\"link\":\"{0}\",", "");
                        json.AppendFormat("\"type\":\"{0}\",", "4");
                        json.AppendFormat("\"hasChilds\":\"{0}\",", "0");
                        json.Append("\"childs\":[");
                    }
                }
                else if (rootID.StartsWith(YJ.Platform.WorkGroup.PREFIX))
                {
                    var root = BWorkGroup.Get(BWorkGroup.RemovePrefix1(rootID).ToGuid());
                    if (root != null)
                    {
                        users = BOrganize.GetAllUsers(rootID);
                        json.Append("{");
                        json.AppendFormat("\"id\":\"{0}\",", root.ID);
                        json.AppendFormat("\"parentID\":\"{0}\",", Guid.Empty);
                        json.AppendFormat("\"title\":\"{0}\",", root.Name);
                        json.AppendFormat("\"ico\":\"{0}\",", "");
                        json.AppendFormat("\"link\":\"{0}\",", "");
                        json.AppendFormat("\"type\":\"{0}\",", "5");
                        json.AppendFormat("\"hasChilds\":\"{0}\",", users.Count > 0 ? "1" : "0");
                        json.Append("\"childs\":[");
                    }
                }

                #region 只有一个根时显示二级
                if (rootIDArray.Length == 1)
                {
                    List<YJ.Data.Model.Organize> orgs = rootID.IsGuid() ? BOrganize.GetChilds(rootGuid)
                        : new List<YJ.Data.Model.Organize>();
                    int count = orgs.Count;
                    int i = 0;
                    foreach (var org in orgs)
                    {
                        json.Append("{");
                        json.AppendFormat("\"id\":\"{0}\",", org.ID);
                        json.AppendFormat("\"parentID\":\"{0}\",", org.ParentID);
                        json.AppendFormat("\"title\":\"{0}\",", org.Name);
                        json.AppendFormat("\"ico\":\"{0}\",", "");
                        json.AppendFormat("\"link\":\"{0}\",", "");
                        json.AppendFormat("\"type\":\"{0}\",", org.Type);
                        json.AppendFormat("\"hasChilds\":\"{0}\",", org.ChildsLength);
                        json.Append("\"childs\":[");
                        json.Append("]");
                        json.Append("}");
                        if (i++ < count - 1 || users.Count > 0)
                        {
                            json.Append(",");
                        }
                    }

                    if (users.Count > 0)
                    {
                        var userRelations = new YJ.Platform.UsersRelation().GetAllByOrganizeID(rootGuid);
                        int count1 = users.Count;
                        int j = 0;
                        foreach (var user in users)
                        {
                            var ur = userRelations.Find(p => p.UserID == user.ID);
                            json.Append("{");
                            json.AppendFormat("\"id\":\"{0}\",", user.ID);
                            json.AppendFormat("\"parentID\":\"{0}\",", rootGuid);
                            json.AppendFormat("\"title\":\"{0}{1}\",", user.Name, ur != null && ur.IsMain == 0 ? "<span style='color:#999;'>[兼任]</span>" : "");
                            json.AppendFormat("\"ico\":\"{0}\",", Url.Content("~/images/ico/contact_grey.png"));
                            json.AppendFormat("\"link\":\"{0}\",", "");
                            json.AppendFormat("\"type\":\"{0}\",", "4");
                            json.AppendFormat("\"hasChilds\":\"{0}\",", "0");
                            json.Append("\"childs\":[");
                            json.Append("]");
                            json.Append("}");
                            if (j++ < count1 - 1)
                            {
                                json.Append(",");
                            }
                        }
                    }
                }
                #endregion

                json.Append("]");
                json.Append("}");
                if (m++ < rootIDArray.Length - 1)
                {
                    json.Append(",");
                }
            }
            json.Append("]");

            return json.ToString();
        }

        [MyAttribute(CheckApp = false, CheckUrl = false, CheckLogin = false)]
        public string TreeRefresh()
        {
            string loginMsg;
            if (!WebMvc.Common.Tools.CheckLogin(out loginMsg) && !YJ.Platform.WeiXin.Organize.CheckLogin())
            {
                return "";
            }

            string id = Request.QueryString["refreshid"] ?? "";
            string showtype = Request.QueryString["showtype"] ?? "";
            string type = Request.QueryString["type"] ?? "";
            YJ.Platform.Organize BOrganize = new YJ.Platform.Organize();
            System.Text.StringBuilder json = new System.Text.StringBuilder("[", 1000);

            if ("1" == showtype)
            {
                #region 显示角色组
                var users1 = BOrganize.GetAllUsers("w_" + id);
                foreach (var user in users1)
                {
                    json.Append("{");
                    json.AppendFormat("\"id\":\"{0}\",", user.ID);
                    json.AppendFormat("\"parentID\":\"w_{0}\",", id);
                    json.AppendFormat("\"title\":\"{0}\",", user.Name);
                    json.AppendFormat("\"ico\":\"{0}\",", Url.Content("~/images/ico/contact_grey.png"));
                    json.AppendFormat("\"link\":\"{0}\",", "");
                    json.AppendFormat("\"type\":\"{0}\",", "4");
                    json.AppendFormat("\"hasChilds\":\"{0}\",", "0");
                    json.Append("\"childs\":[");
                    json.Append("]");
                    json.Append("}");
                    if (user.ID != users1.Last().ID)
                    {
                        json.Append(",");
                    }
                }
                json.Append("]");
                Response.Write(json.ToString());
                Response.End();
                return "";
                #endregion
            }

            Guid orgID;
            if (!id.IsGuid(out orgID))
            {
                json.Append("]");
                return json.ToString();
            }

            var childOrgs = BOrganize.GetChilds(orgID);

            int count = childOrgs.Count;
            int i = 0;
            foreach (var org in childOrgs)
            {
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", org.ID);
                json.AppendFormat("\"parentID\":\"{0}\",", id);
                json.AppendFormat("\"title\":\"{0}\",", org.Name);
                json.AppendFormat("\"ico\":\"{0}\",", "");
                json.AppendFormat("\"link\":\"{0}\",", "");
                json.AppendFormat("\"type\":\"{0}\",", org.Type);
                json.AppendFormat("\"hasChilds\":\"{0}\",", org.ChildsLength);
                json.Append("\"childs\":[");
                json.Append("]");
                json.Append("}");
                if (i++ < count - 1)
                {
                    json.Append(",");
                }
            }

            var userRelations = new YJ.Platform.UsersRelation().GetAllByOrganizeID(orgID);
            var users = "5" == type ? BOrganize.GetAllUsers(YJ.Platform.WorkGroup.PREFIX + id)
                : new YJ.Platform.Users().GetAllByOrganizeID(orgID);
            int count1 = users.Count;
            if (count1 > 0 && count > 0)
            {
                json.Append(",");
            }
            int j = 0;
            foreach (var user in users)
            {
                var ur = userRelations.Find(p => p.UserID == user.ID);
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", user.ID);
                json.AppendFormat("\"parentID\":\"{0}\",", id);
                json.AppendFormat("\"title\":\"{0}{1}\",", user.Name, ur != null && ur.IsMain == 0 ? "<span style='color:#999;'>[兼任]</span>" : "");
                json.AppendFormat("\"ico\":\"{0}\",", Url.Content("~/images/ico/contact_grey.png"));
                json.AppendFormat("\"link\":\"{0}\",", "");
                json.AppendFormat("\"type\":\"{0}\",", "4");
                json.AppendFormat("\"hasChilds\":\"{0}\",", "0");
                json.Append("\"childs\":[");
                json.Append("]");
                json.Append("}");
                if (j++ < count1 - 1)
                {
                    json.Append(",");
                }
            }
            json.Append("]");
            return json.ToString();
        }

        public ActionResult Empty()
        {
            return View();
        }

        public ActionResult Body()
        {
            return Body(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Body(FormCollection collection)
        {
            YJ.Data.Model.Organize org = null;
            YJ.Platform.Organize borganize = new YJ.Platform.Organize();
            string id = Request.QueryString["id"];
            if (id.IsGuid())
            {
                org = borganize.Get(id.ToGuid());
            }

            //保存
            if (!Request.Form["Save"].IsNullOrEmpty() && org != null)
            {
                string name = Request.Form["Name"];
                string type = Request.Form["Type"];
                string status = Request.Form["Status"];
                string chargeLeader = Request.Form["ChargeLeader"];
                string leader = Request.Form["Leader"];
                string note = Request.Form["note"];
                string oldXML = org.Serialize();
                org.Name = name.Trim();
                org.Type = type.ToInt(1);
                org.Status = status.ToInt(0);
                org.ChargeLeader = chargeLeader;
                org.Leader = leader;
                org.Note = note.IsNullOrEmpty() ? null : note.Trim();

                borganize.Update(org);
                //同步修改微信企业号
                if (YJ.Platform.WeiXin.Config.IsUse)
                {
                    new YJ.Platform.WeiXin.Organize().EditDeptAsync(org);
                }
                new YJ.Platform.MenuUser().ClearCache();
                YJ.Platform.Log.Add("修改了组织机构", "", YJ.Platform.Log.Types.组织机构, oldXML, org.Serialize());
                string rid = org.ParentID == Guid.Empty ? org.ID.ToString() : org.ParentID.ToString();
                ViewBag.Script = "alert('保存成功!');parent.frames[0].reLoad('" + rid + "');";
            }

            //移动
            if (!Request.Form["Move1"].IsNullOrEmpty() && org != null)
            {
                string toOrgID = Request.Form["deptmove"];
                Guid toID;
                if (toOrgID.IsGuid(out toID) && borganize.Move(org.ID, toID))
                {
                    new YJ.Platform.MenuUser().ClearCache();
                    new YJ.Platform.HomeItems().ClearCache();//清除首页缓存
                    YJ.Platform.Log.Add("移动了组织机构", "将机构：" + org.ID + "移动到了：" + toID, YJ.Platform.Log.Types.组织机构);
                    string refreshID = org.ParentID == Guid.Empty ? org.ID.ToString() : org.ParentID.ToString();
                    ViewBag.Script = "alert('移动成功!');parent.frames[0].reLoad('" + refreshID + "');parent.frames[0].reLoad('" + toOrgID + "')";
                }
                else
                {
                    ViewBag.Script = "alert('移动失败!');";
                }
            }

            //删除
            if (!Request.Form["Delete"].IsNullOrEmpty())
            {
                int i = borganize.DeleteAndAllChilds(org.ID);
                new YJ.Platform.MenuUser().ClearCache();
                new YJ.Platform.HomeItems().ClearCache();//清除首页缓存
                YJ.Platform.Log.Add("删除了组织机构及其所有下级共" + i.ToString() + "项", org.Serialize(), YJ.Platform.Log.Types.组织机构);
                string refreshID = org.ParentID == Guid.Empty ? org.ID.ToString() : org.ParentID.ToString();
                ViewBag.Script = "alert('共删除了" + i.ToString() + "项!');parent.frames[0].reLoad('" + refreshID + "');";
            }

            if (!Request.Form["ToWeiXin"].IsNullOrEmpty())
            {
                YJ.Platform.WeiXin.Organize worg = new YJ.Platform.WeiXin.Organize();
                worg.UpdateAllOrganize();
                worg.UpdateAllUsers();
                ViewBag.script = "alert('同步完成!');window.location=window.location;";
            }

            if (org == null)
            {
                org = new YJ.Data.Model.Organize();
            }
            ViewBag.TypeRadios = borganize.GetTypeRadio("Type", org.Type.ToString(), "validate=\"radio\"");
            ViewBag.StatusRadios = borganize.GetStatusRadio("Status", org.Status.ToString(), "validate=\"radio\"");
           
            return View(org);
        }

        public ActionResult BodyAdd()
        {
            return BodyAdd(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BodyAdd(FormCollection collection)
        {
            YJ.Platform.Organize borganize = new YJ.Platform.Organize();
            YJ.Data.Model.Organize org = null;
            string id = Request.QueryString["id"];
            string name = string.Empty;
            string type = string.Empty;
            string status = string.Empty;
            string note = string.Empty;

            Guid orgID;
            if (id.IsGuid(out orgID))
            {
                org = borganize.Get(orgID);
            }

            if (collection != null && org != null)
            {
                name = Request.Form["Name"];
                type = Request.Form["Type"];
                status = Request.Form["Status"];
                note = Request.Form["note"];

                YJ.Data.Model.Organize org1 = new YJ.Data.Model.Organize();
                Guid org1ID = Guid.NewGuid();
                org1.ID = org1ID;
                org1.Name = name.Trim();
                org1.Note = note.IsNullOrEmpty() ? null : note.Trim();
                org1.Number = org.Number + "," + org1ID.ToString().ToLower();
                org1.ParentID = org.ID;
                org1.Sort = borganize.GetMaxSort(org.ID);
                org1.Status = status.IsInt() ? status.ToInt() : 0;
                org1.Type = type.ToInt();
                org1.Depth = org.Depth + 1;
                org1.IntID = org1ID.ToInt32();

                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    borganize.Add(org1);
                    //更新父级[ChildsLength]字段
                    borganize.UpdateChildsLength(org.ID);
                    scope.Complete();
                }
                //同步修改微信企业号
                if (YJ.Platform.WeiXin.Config.IsUse)
                {
                    new YJ.Platform.WeiXin.Organize().AddDeptAsync(org1);
                }
                new YJ.Platform.MenuUser().ClearCache();
                YJ.Platform.Log.Add("添加了组织机构", org1.Serialize(), YJ.Platform.Log.Types.组织机构);
                ViewBag.Script = "alert('添加成功!');parent.frames[0].reLoad('" + id + "');window.location=window.location;";
            }
            ViewBag.TypeRadios = borganize.GetTypeRadio("Type", type, "validate=\"radio\"");
            ViewBag.StatusRadios = borganize.GetStatusRadio("Status", "0", "validate=\"radio\"");
            return View();
        }

        public ActionResult UserAdd()
        {
            return UserAdd(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserAdd(FormCollection collection)
        {
            YJ.Platform.Organize borganize = new YJ.Platform.Organize();
            YJ.Platform.Users busers = new YJ.Platform.Users();

            string id = Request.QueryString["id"];

            string name = string.Empty;
            string account = string.Empty;
            string status = string.Empty;
            string note = string.Empty;
            string sex = string.Empty;
            Guid parentID;

            if (collection != null && id.IsGuid(out parentID))
            {
                name = Request.Form["Name"];
                account = Request.Form["Account"];
                status = Request.Form["Status"];
                note = Request.Form["Note"];
                string Tel = Request.Form["Tel"];
                string Mobile = Request.Form["Mobile"];
                string WeiXin = Request.Form["WeiXin"];
                string Email = Request.Form["Email"];
                string Fax = Request.Form["Fax"];
                string QQ = Request.Form["QQ"];
                string OtherTel = Request.Form["OtherTel"];
                sex = Request.Form["Sex"];

                Guid userID = Guid.NewGuid();
                string userXML = string.Empty;
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    //添加人员
                    YJ.Data.Model.Users user = new YJ.Data.Model.Users();
                    user.Account = account.Trim();
                    user.Name = name.Trim();
                    user.Note = note.IsNullOrEmpty() ? null : note;
                    user.Password = busers.GetUserEncryptionPassword(userID.ToString(), busers.GetInitPassword());
                    user.Sort = 1;
                    user.Status = status.IsInt() ? status.ToInt() : 0;
                    user.ID = userID;
                    user.Tel = Tel.Trim1();
                    user.Mobile = Mobile.Trim1();
                    user.WeiXin = WeiXin.Trim1();
                    user.WeiXin = WeiXin.Trim1();
                    user.Email = Email.Trim1();
                    user.Fax = Fax.Trim1();
                    user.QQ = QQ.Trim1();
                    user.OtherTel = OtherTel.Trim1();
                    if (sex.IsInt())
                    {
                        user.Sex = sex.ToInt();
                    }
                    busers.Add(user);

                    //添加关系
                    YJ.Data.Model.UsersRelation userRelation = new YJ.Data.Model.UsersRelation();
                    userRelation.IsMain = 1;
                    userRelation.OrganizeID = parentID;
                    userRelation.Sort = new YJ.Platform.UsersRelation().GetMaxSort(parentID);
                    userRelation.UserID = userID;
                    new YJ.Platform.UsersRelation().Add(userRelation);

                    //更新父级[ChildsLength]字段
                    borganize.UpdateChildsLength(parentID);

                    //添加微信
                    if (YJ.Platform.WeiXin.Config.IsUse)
                    {
                        new YJ.Platform.WeiXin.Organize().AddUserAsync(user);
                    }
                    userXML = user.Serialize();
                    scope.Complete();
                }
                new YJ.Platform.MenuUser().ClearCache();
                new YJ.Platform.HomeItems().ClearCache();//清除首页缓存
                new YJ.Platform.DocumentDirectory().ClearAllDirUsersCache();//清除文档中心栏目缓存
                YJ.Platform.Log.Add("添加了人员", userXML, YJ.Platform.Log.Types.组织机构);
                ViewBag.Script = "alert('添加成功!');parent.frames[0].reLoad('" + id + "');window.location=window.location;";
            }
            ViewBag.StatusRadios = borganize.GetStatusRadio("Status", "0", "validate=\"radio\"");
            ViewBag.SexRadios = borganize.GetSexRadio("Sex", "", "validate=\"radio\"");
            return View();
        }

        public ActionResult User()
        {
            return  User(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult User(FormCollection collection)
        {
            YJ.Platform.Organize borganize = new YJ.Platform.Organize();
            YJ.Platform.Users busers = new YJ.Platform.Users();
            YJ.Platform.UsersRelation buserRelation = new YJ.Platform.UsersRelation();
            YJ.Data.Model.Users user = null;
            YJ.Data.Model.Organize organize = null;
            string id = Request.QueryString["id"];
            string parentID = Request.QueryString["parentid"];

            string name = string.Empty;
            string account = string.Empty;
            string status = string.Empty;
            string note = string.Empty;
            string sex = string.Empty;

            string parentString = string.Empty;

            Guid userID, organizeID;
            if (id.IsGuid(out userID))
            {
                user = busers.Get(userID);
                if (user != null)
                {
                    name = user.Name;
                    account = user.Account;
                    status = user.Status.ToString();
                    note = user.Note;
                    sex = user.Sex.ToString();

                    //所在组织字符串
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    var userRelations = buserRelation.GetAllByUserID(user.ID).OrderByDescending(p => p.IsMain);
                    foreach (var userRelation in userRelations)
                    {
                        sb.Append("<div style='margin:3px 0;'>");
                        sb.Append(borganize.GetAllParentNames(userRelation.OrganizeID, true));
                        if (userRelation.IsMain == 0)
                        {
                            sb.Append("<span style='color:#999'> [兼任]</span>");
                        }
                        sb.Append("</div>");

                    }
                    ViewBag.ParentString = sb.ToString();
                    ViewBag.RoleString = new YJ.Platform.WorkGroup().GetAllNamesByUserID(user.ID);
                }
            }
            if (parentID.IsGuid(out organizeID))
            {
                organize = borganize.Get(organizeID);
            }

            if (collection != null)
            {
                //保存
                if (!Request.Form["Save"].IsNullOrEmpty() && user != null)
                {
                    name = Request.Form["Name"];
                    account = Request.Form["Account"];
                    status = Request.Form["Status"];
                    note = Request.Form["Note"];
                    string Tel = Request.Form["Tel"];
                    string Mobile = Request.Form["Mobile"];
                    string WeiXin = Request.Form["WeiXin"];
                    string Email = Request.Form["Email"];
                    string Fax = Request.Form["Fax"];
                    string QQ = Request.Form["QQ"];
                    string OtherTel = Request.Form["OtherTel"];
                    sex = Request.Form["Sex"];

                    string oldXML = user.Serialize();

                    user.Name = name.Trim();
                    user.Account = account.Trim();
                    user.Status = status.ToInt(1);
                    user.Note = note.IsNullOrEmpty() ? null : note.Trim();
                    user.Tel = Tel.Trim1();
                    user.Mobile = Mobile.Trim1();
                    user.WeiXin = WeiXin.Trim1();
                    user.WeiXin = WeiXin.Trim1();
                    user.Email = Email.Trim1();
                    user.Fax = Fax.Trim1();
                    user.QQ = QQ.Trim1();
                    user.OtherTel = OtherTel.Trim1();
                    if (sex.IsInt())
                    {
                        user.Sex = sex.ToInt();
                    }

                    busers.Update(user);
                    //更新微信
                    if (YJ.Platform.WeiXin.Config.IsUse)
                    {
                        new YJ.Platform.WeiXin.Organize().EditUserAsync(user);
                    }
                    new YJ.Platform.MenuUser().ClearCache();
                    YJ.Platform.Log.Add("修改了用户", "", YJ.Platform.Log.Types.组织机构, oldXML, user.Serialize());
                    ViewBag.Script = "alert('保存成功!');parent.frames[0].reLoad('" + parentID + "');";
                }

                //删除用户
                if (!Request.Form["DeleteBut"].IsNullOrEmpty() && user != null && organize != null)
                {
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {

                        var urs = buserRelation.GetAllByUserID(user.ID);
                        busers.Delete(user.ID);
                        buserRelation.DeleteByUserID(user.ID);
                        //更新父级[ChildsLength]字段
                        foreach (var ur in urs)
                        {
                            borganize.UpdateChildsLength(ur.OrganizeID);
                        }
                        //删除微信
                        if (YJ.Platform.WeiXin.Config.IsUse)
                        {
                            new YJ.Platform.WeiXin.Organize().DeleteUserAsync(user.Account);
                        }
                        scope.Complete();
                    }
                    new YJ.Platform.MenuUser().ClearCache();
                    new YJ.Platform.HomeItems().ClearCache();//清除首页缓存
                    string refreshID = parentID;
                    string url = string.Empty;
                    var users = borganize.GetAllUsers(refreshID);
                    if (users.Count > 0)
                    {
                        url = "User?id=" + users.Last().ID + "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&parentid=" + parentID;
                    }
                    else if (organize != null)
                    {
                        refreshID = organize.ParentID == Guid.Empty ? organize.ID.ToString() : organize.ParentID.ToString();
                        url = "Body?id=" + parentID + "&appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["tabid"] + "&parentid=" + organize.ParentID;
                    }
                    YJ.Platform.Log.Add("删除了用户", user.Serialize(), YJ.Platform.Log.Types.组织机构);
                    ViewBag.Script = "alert('删除成功');parent.frames[0].reLoad('" + refreshID + "');window.location='" + url + "'";
                    
                }

                //初始化密码
                if (!Request.Form["InitPass"].IsNullOrEmpty() && user != null)
                {
                    string initpass = busers.GetInitPassword();
                    busers.InitPassword(user.ID);
                    YJ.Platform.Log.Add("初始化了用户密码", user.Serialize(), YJ.Platform.Log.Types.组织机构);
                    ViewBag.Script = "alert('密码已初始化为：" + initpass + "');";
                }

                //调动
                if (!Request.Form["Move1"].IsNullOrEmpty() && user != null)
                {
                    string moveto = Request.Form["movetostation"];
                    string movetostationjz = Request.Form["movetostationjz"];
                    Guid moveToID;
                    if (moveto.IsGuid(out moveToID))
                    {
                        using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                        {
                            var us = buserRelation.GetAllByUserID(user.ID);
                            if ("1" != movetostationjz)
                            {
                                buserRelation.DeleteByUserID(user.ID);
                            }

                            YJ.Data.Model.UsersRelation ur = new YJ.Data.Model.UsersRelation();
                            ur.UserID = user.ID;
                            ur.OrganizeID = moveToID;
                            ur.IsMain = "1" == movetostationjz ? 0 : 1;
                            ur.Sort = buserRelation.GetMaxSort(moveToID);
                            buserRelation.Add(ur);

                            foreach (var u in us)
                            {
                                borganize.UpdateChildsLength(u.OrganizeID);
                            }

                            borganize.UpdateChildsLength(organizeID);
                            borganize.UpdateChildsLength(moveToID);
                            new YJ.Platform.MenuUser().ClearCache();
                            new YJ.Platform.HomeItems().ClearCache();//清除首页缓存
                            new YJ.Platform.DocumentDirectory().ClearAllDirUsersCache();//清除文档中心栏目缓存
                            //更新微信
                            if (YJ.Platform.WeiXin.Config.IsUse)
                            {
                                new YJ.Platform.WeiXin.Organize().EditUserAsync(user);
                            }
                            scope.Complete();
                            ViewBag.Script = "alert('调动成功!');parent.frames[0].reLoad('" + parentID + "');parent.frames[0].reLoad('" + moveto + "')";
                        }

                        YJ.Platform.Log.Add(("1" == movetostationjz ? "兼职" : "全职") + "调动了人员的岗位", "将人员调往岗位(" + moveto + ")", YJ.Platform.Log.Types.组织机构);
                       
                    }
                }
            }
            ViewBag.StatusRadios = borganize.GetStatusRadio("Status", status, "validate=\"radio\"");
            ViewBag.SexRadios = borganize.GetSexRadio("Sex", sex, "validate=\"radio\"");
            return View(user);
        }

        public ActionResult Sort()
        {
            return Sort(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sort(FormCollection collection)
        {
            string parentid = Request.QueryString["parentid"];
            if (collection != null)
            {
                string sort = Request.Form["sort"] ?? "";
                string[] sortArray = sort.Split(',');
                YJ.Platform.Organize borganize = new YJ.Platform.Organize();
                for (int i = 0; i < sortArray.Length; i++)
                {
                    Guid gid;
                    if (!sortArray[i].IsGuid(out gid))
                    {
                        continue;
                    }
                    borganize.UpdateSort(gid, i + 1);
                }
                ViewBag.Script = "parent.frames[0].reLoad('" + parentid + "');";
            }
            var orgs = new YJ.Platform.Organize().GetChilds(parentid.ToGuid());
            return View(orgs);
        }

        public ActionResult SortUsers()
        {
            return SortUsers(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SortUsers(FormCollection collection)
        {
            string parentID = Request.QueryString["parentid"];
            if (collection != null)
            {
                string sort = Request.Form["sort"] ?? "";
                string[] sortArray = sort.Split(',');
                YJ.Platform.Users busers = new YJ.Platform.Users();
                for (int i = 0; i < sortArray.Length; i++)
                {
                    Guid gid;
                    if (!sortArray[i].IsGuid(out gid))
                    {
                        continue;
                    }
                    busers.UpdateSort(gid, i + 1);
                }
                ViewBag.Script = "parent.frames[0].reLoad('" + parentID + "');";
            }
            var users = new YJ.Platform.Organize().GetAllUsers(parentID.ToGuid());
            return View(users);
        }

        [MyAttribute(CheckApp = false)]
        public string GetPy()
        {
            string name = Request.Form["name"];
            string account = name.ToChineseSpell();
            return account.IsNullOrEmpty() ? "" : new YJ.Platform.Users().GetAccount(account.Trim());
        }

        [MyAttribute(CheckApp = false)]
        public string CheckAccount()
        {
            string loginMsg;
            if (!WebMvc.Common.Tools.CheckLogin(out loginMsg) && !YJ.Platform.WeiXin.Organize.CheckLogin())
            {
                return "";
            }
            string account = Request.Form["value"];
            string id = Request["id"];
            return new YJ.Platform.Users().HasAccount(account, id) ? "帐号已经被使用了" : "1";
        }

        [MyAttribute(CheckApp = false)]
        public string GetNames()
        {
            string loginMsg;
            if (!WebMvc.Common.Tools.CheckLogin(out loginMsg) && !YJ.Platform.WeiXin.Organize.CheckLogin())
            {
                return "";
            }
            string values = Request.QueryString["values"];
            return new YJ.Platform.Organize().GetNames(values);
        }

        [MyAttribute(CheckApp = false)]
        public string GetNote()
        {
            string loginMsg;
            if (!WebMvc.Common.Tools.CheckLogin(out loginMsg) && !YJ.Platform.WeiXin.Organize.CheckLogin())
            {
                return "";
            }
            string id = Request.QueryString["id"];
            Guid gid;
            if (id.IsNullOrEmpty())
            {
                return "";
            }
            YJ.Platform.Organize borg = new YJ.Platform.Organize();
            YJ.Platform.Users buser = new YJ.Platform.Users();
            if (id.StartsWith(YJ.Platform.Users.PREFIX))
            {
                Guid uid = buser.RemovePrefix1(id).ToGuid();
                return string.Concat(borg.GetAllParentNames(buser.GetMainStation(uid)), " / ", buser.GetName(uid));
            }
            else if (id.StartsWith(YJ.Platform.WorkGroup.PREFIX))
            {
                return new YJ.Platform.WorkGroup().GetUsersNames(YJ.Platform.WorkGroup.RemovePrefix(id).ToGuid(), '、');
            }
            else if (id.IsGuid(out gid))
            {
                return borg.GetAllParentNames(gid);
            }
            return "";
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult EditPass()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAttribute(CheckApp = false)]
        public ActionResult EditPass(FormCollection collection)
        {
            string oldpass = Request.Form["oldpass"];
            string newpass = Request.Form["newpass"];
            if (!YJ.Utility.Tools.PasswordStrength(newpass)) {
                ViewBag.Script = "alert('密码强度不够，至少要包含8位数字+字母!');new RoadUI.Window().close();";
                return View();
            }

            YJ.Platform.Users busers = new YJ.Platform.Users();
            var user = YJ.Platform.Users.CurrentUser;
            if (user != null)
            {
                if (string.Compare(user.Password, busers.GetUserEncryptionPassword(user.ID.ToString(), oldpass.Trim()), false) != 0)
                {
                    YJ.Platform.Log.Add("修改密码失败", string.Concat("用户：", user.Name, "(", user.ID, ")修改密码失败,旧密码错误!"), YJ.Platform.Log.Types.用户登录);
                    ViewBag.Script = "alert('旧密码错误!');";
                }
                else
                {
                    busers.UpdatePassword(newpass.Trim(), user.ID);
                    YJ.Platform.Log.Add("修改密码成功", string.Concat("用户：", user.Name, "(", user.ID, ")修改密码成功!"), YJ.Platform.Log.Types.用户登录);
                    ViewBag.Script = "alert('密码修改成功!');new RoadUI.Window().close();";
                }
            }
            return View();
        }

        public ActionResult WorkGroup()
        {
            return WorkGroup(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WorkGroup(FormCollection collection)
        {
            string id = Request.QueryString["id"];
            Guid wid;
            YJ.Platform.WorkGroup bwg = new YJ.Platform.WorkGroup();
            YJ.Data.Model.WorkGroup wg = null;
            string name = string.Empty;
            string members = string.Empty;
            string note = string.Empty;
            string users = string.Empty;

            if (!id.IsGuid(out wid) || wid == Guid.Empty)
            {
                Response.End();
                return null;
            }

            wg = bwg.Get(wid);
            string oldmembers = string.Empty;
            if (wg != null)
            {
                oldmembers = wg.Members;
                name = wg.Name;
                members = wg.Members;
                note = wg.Note;
                users = bwg.GetUsersNames(wg.Members, '、');
            }

            if (!Request.Form["Save"].IsNullOrEmpty() && collection != null && wg != null)
            {
                string oldxml = wg.Serialize();
                name = Request.Form["Name"];
                members = Request.Form["Members"];
                note = Request.Form["Note"];
                wg.Name = name.Trim();
                wg.Members = members;
                if (!note.IsNullOrEmpty())
                {
                    wg.Note = note;
                }
                wg.IntID = wg.ID.ToInt32();
                
                bwg.Update(wg);
                //更新微信标签
                if (YJ.Platform.WeiXin.Config.IsUse)
                {
                    YJ.Platform.WeiXin.Organize worg = new YJ.Platform.WeiXin.Organize();
                    worg.EditGroupAsync(wg);
                    if (!oldmembers.Equals(wg.Members, StringComparison.CurrentCultureIgnoreCase))
                    {
                        worg.AddGroupUserAsync(wg);
                    }
                }
                new YJ.Platform.MenuUser().ClearCache();
                new YJ.Platform.HomeItems().ClearCache();//清除首页缓存
                users = bwg.GetUsersNames(wg.Members, '、');
                string query = Request.Url.Query;
                YJ.Platform.Log.Add("修改了工作组", "修改了工作组", YJ.Platform.Log.Types.组织机构, oldxml, wg.Serialize());

                ViewBag.Script = "alert('保存成功!');";
            }

            //删除
            if (!Request.Form["DeleteBut"].IsNullOrEmpty() && collection != null && wg != null)
            {
                string oldxml = wg.Serialize();
                bwg.Delete(wg.ID);
                //同步微信删除
                if (YJ.Platform.WeiXin.Config.IsUse)
                {
                    YJ.Platform.WeiXin.Organize worg = new YJ.Platform.WeiXin.Organize();
                    worg.DeleteGroupAsync(wg);
                }
                new YJ.Platform.MenuUser().ClearCache();
                new YJ.Platform.HomeItems().ClearCache();//清除首页缓存
                string query = Request.Url.Query;
                YJ.Platform.Log.Add("删除了工作组", oldxml, YJ.Platform.Log.Types.组织机构);
                ViewBag.Script = "parent.frames[0].treecng('1');alert('删除成功!');window.location = 'Empty' + '" + query + "';";
            }
            return View(wg);
        }

        public ActionResult WorkGroupAdd()
        {
            return WorkGroupAdd(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WorkGroupAdd(FormCollection collection)
        {
            YJ.Platform.WorkGroup bwg = new YJ.Platform.WorkGroup();
            string name = string.Empty;
            string members = string.Empty;
            string note = string.Empty;
            if (collection != null)
            {
                name = Request.Form["Name"];
                members = Request.Form["Members"];
                note = Request.Form["Note"];

                YJ.Data.Model.WorkGroup wg = new YJ.Data.Model.WorkGroup();
                wg.ID = Guid.NewGuid();
                wg.Name = name.Trim();
                wg.Members = members;
                if (!note.IsNullOrEmpty())
                {
                    wg.Note = note;
                }
                wg.IntID = wg.ID.ToInt32();
                bwg.Add(wg);
                //更新微信标签
                if (YJ.Platform.WeiXin.Config.IsUse)
                {
                    new YJ.Platform.WeiXin.Organize().AddGroupAsync(wg);
                }
                new YJ.Platform.MenuUser().ClearCache();
                string query = Request.Url.Query;
                YJ.Platform.Log.Add("添加了工作组", wg.Serialize(), YJ.Platform.Log.Types.组织机构);
                ViewBag.Script = "parent.frames[0].treecng('1');alert('添加成功!');window.location = 'WorkGroup' + '" + query + "';";

            }
            return View();
        }

        public ActionResult SetMenu()
        {
            return SetMenu(null);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetMenu(FormCollection collection)
        {
            YJ.Platform.Menu bmenu = new YJ.Platform.Menu();
            YJ.Platform.MenuUser bmenuuser = new YJ.Platform.MenuUser();
            string id = Request.QueryString["id"];
            string type = Request.QueryString["type"];
            string userid = ("4" == type ? YJ.Platform.Users.PREFIX : "5" == type ? YJ.Platform.WorkGroup.PREFIX : "") + id;
            if (collection != null)
            {
                string menus = Request.Form["menuid"] ?? "";
                YJ.Platform.Organize borg = new YJ.Platform.Organize();
                bmenuuser.ClearCache(); 
                string useridstring = borg.GetAllUsersIdList(userid).ToArray().Join1(",");
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    bmenuuser.DeleteByOrganizes(userid);
                    foreach (string menu in menus.Split(','))
                    {
                        if (!menu.IsGuid())
                        {
                            continue;
                        }
                        YJ.Data.Model.MenuUser mu = new YJ.Data.Model.MenuUser();
                        mu.Buttons = Request.Form["button_" + menu] ?? "";
                        mu.SubPageID = Guid.Empty;
                        mu.ID = Guid.NewGuid();
                        mu.MenuID = menu.ToGuid();
                        mu.Organizes = userid;
                        mu.Users = useridstring;
                        mu.Params = (Request.Form["params_" + menu] ?? "").Replace("\"", "");
                        bmenuuser.Add(mu);
                        string subpage_ = Request.Form["subpage_" + menu] ?? "";
                        foreach (string subpage in subpage_.Split(','))
                        {
                            if (!subpage.IsGuid())
                            {
                                continue;
                            }
                            YJ.Data.Model.MenuUser mu1 = new YJ.Data.Model.MenuUser();
                            mu1.Buttons = Request.Form["button_" + menu + "_" + subpage] ?? "";
                            mu1.SubPageID = subpage.ToGuid();
                            mu1.ID = Guid.NewGuid();
                            mu1.MenuID = mu.MenuID;
                            mu1.Organizes = userid;
                            mu1.Users = useridstring;
                            bmenuuser.Add(mu1);
                        }
                    }
                    
                    scope.Complete();
                    ViewBag.script = "alert('保存成功!');window.location=window.location;";
                }
                bmenuuser.ClearCache();
            }

            var menuHtml = bmenu.GetMenuTreeTableHtml(id);
            ViewBag.menuhtml = menuHtml;
            return View();
        }

        public ActionResult ShowMenu()
        {
            return View();
        }
    }
}
