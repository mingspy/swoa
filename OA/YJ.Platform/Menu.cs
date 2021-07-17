using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

namespace YJ.Platform
{
    public class Menu
    {
        private YJ.Data.Interface.IMenu dataMenu;
        public Menu()
        {
            this.dataMenu = YJ.Data.Factory.Factory.GetMenu();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.Menu model)
        {
            ClearAllDataTableCache();
            return dataMenu.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.Menu model)
        {
            ClearAllDataTableCache();
            return dataMenu.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.Menu> GetAll()
        {
            return dataMenu.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.Menu Get(Guid id)
        {
            return dataMenu.Get(id);
        }
        /// <summary>
        /// 从缓存查询一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataRow GetFromCache(Guid id)
        {
            DataRow[] drs = GetAllDataTable().Select("ID='" + id + "'");
            return drs.Length > 0 ? drs[0] : null;
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            ClearAllDataTableCache();
            return dataMenu.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataMenu.GetCount();
        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        public void ClearAllDataTableCache()
        {
            string cackeKey = Utility.Keys.CacheKeys.MenuTable.ToString();
            Cache.IO.Opation.Remove(cackeKey);
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        public System.Data.DataTable GetAllDataTable(bool cache = true)
        {
            if(!cache)
            {
                return dataMenu.GetAllDataTable();
            }
            string cackeKey = Utility.Keys.CacheKeys.MenuTable.ToString();
            object obj = Cache.IO.Opation.Get(cackeKey);
            if (obj == null || !(obj is DataTable))
            {
                var dt = dataMenu.GetAllDataTable();
                Cache.IO.Opation.Set(cackeKey, dt);
                return dt;
            }
            else
            {
                return (DataTable)obj;
            }
        }

        /// <summary>
        /// 查询下级记录
        /// </summary>
        public List<YJ.Data.Model.Menu> GetChild(Guid id)
        {
            return dataMenu.GetChild(id);
        }

        /// <summary>
        /// 是否有下级记录
        /// </summary>
        public bool HasChild(Guid id)
        {
            return dataMenu.HasChild(id);
        }

        /// <summary>
        /// 更新排序
        /// </summary>
        public int UpdateSort(Guid id, int sort)
        {
            return dataMenu.UpdateSort(id, sort);
        }

        /// <summary>
        /// 得到所有下级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<YJ.Data.Model.Menu> GetAllChild(Guid id)
        {
            List<YJ.Data.Model.Menu> list = new List<YJ.Data.Model.Menu>();
            var childs = GetChild(id);
            foreach (var child in childs)
            {
                list.Add(child);
                addChilds(list, child.ID);
            }
            return list;
        }
        private void addChilds(List<YJ.Data.Model.Menu> list, Guid id)
        {
            var childs = GetChild(id);
            foreach (var child in childs)
            {
                list.Add(child);
                addChilds(list, child.ID);
            }
        }

        /// <summary>
        /// 删除当有应用和所有下级应用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteAndAllChilds(Guid id)
        {
            var childs = GetAllChild(id);
            int i = 0;
            foreach (var child in childs)
            {
                i += Delete(child.ID);
            }
            i += Delete(id);
            ClearAllDataTableCache();
            return i;
        }

        /// <summary>
        /// 得到最大排序值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetMaxSort(Guid id)
        {
            return dataMenu.GetMaxSort(id);
        }
        

        /// <summary>
        /// 得到应用地址
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="params1"></param>
        /// <returns></returns>
        private string getAddress(System.Data.DataRow dr, string paramsMenuUsers = "")
        {
            string address = dr["Address"].ToString().Trim();
            string params1 = dr["Params"].ToString().Trim();
            string params2 = dr["Params1"].ToString().Trim();
            StringBuilder sb = new StringBuilder();
            if (params1.IsNullOrEmpty() && params2.IsNullOrEmpty() && paramsMenuUsers.IsNullOrEmpty())
            {
                return address;
            }

            if (!params2.IsNullOrEmpty())
            {
                sb.Append(params2.TrimStart('?').TrimStart('&').TrimEnd('&').TrimEnd('?'));
            }
            if (!params1.IsNullOrEmpty())
            {
                if (sb.Length > 0)
                {
                    sb.Append('&');
                }
                sb.Append(params1.TrimStart('?').TrimStart('&').TrimEnd('&').TrimEnd('?'));
            }
            if (!paramsMenuUsers.IsNullOrEmpty())
            {
                if (sb.Length > 0)
                {
                    sb.Append('&');
                }
                sb.Append(paramsMenuUsers.TrimStart('?').TrimStart('&').TrimEnd('&').TrimEnd('?'));
            }

            return (address.Contains("?") ? string.Concat(address, "&", sb.ToString()) : string.Concat(address, "?", sb.ToString())).FilterWildcard(YJ.Platform.Users.CurrentUserID.ToString());
        }

        /// <summary>
        /// 得到用户一级菜单
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetUserMenu(Guid userID)
        {
            Menu Menu = new Platform.Menu();
            AppLibrary Applibary = new AppLibrary();
            System.Data.DataTable appDt = Menu.GetAllDataTable();
            if (appDt.Rows.Count == 0)
            {
                return "";
            }
            StringBuilder menuSb = new StringBuilder();
            //添加快捷菜单
            string sourceMember = string.Empty;
            List<YJ.Data.Model.MenuUser> menuusers = new MenuUser().GetAll();
            var shortcuts = new YJ.Platform.UserShortcut().GetAllByUserID(userID, true);
            foreach (var shortcut in shortcuts)
            {
                string params1 = string.Empty;
                var menu = menuusers.FindAll(p => p.MenuID == shortcut.MenuID && p.SubPageID == Guid.Empty && p.Users.Contains(userID.ToString(), StringComparison.CurrentCultureIgnoreCase));
                if (menu.Count > 0)
                {
                    StringBuilder psb = new StringBuilder();
                    foreach (var m in menu.FindAll(p => !p.Params.IsNullOrEmpty()).GroupBy(p => p.Params))
                    {
                        psb.Append(m.Key.Trim1());
                        psb.Append("&");
                    }
                    params1 = psb.ToString().TrimEnd('&');
                }
                if (!HasUse(shortcut.MenuID, userID, menuusers, out sourceMember, out params1))
                {
                    continue;
                }
                var menudts = appDt.Select(string.Format("ID='{0}'", shortcut.MenuID.ToString()));
                if (menudts.Length > 0)
                {
                    DataRow dr = menudts[0];
                    string icoColor = dr["IcoColor"].ToString();
                    if (icoColor.IsNullOrEmpty())
                    {
                        icoColor = dr["IcoColor1"].ToString();
                    }
                    var childs = appDt.Select("ParentID='" + dr["ID"].ToString() + "'");
                    menuSb.Append("<div class=\"menulistdiv\" onclick=\"menuClick(this);\" data-id=\"" + dr["ID"].ToString()
                    + "\" data-title=\"" + dr["Title"].ToString().Trim() + "\" data-model=\"" + dr["OpenMode"].ToString()
                    + "\" data-width=\"" + dr["Width"].ToString() + "\" data-height=\"" + dr["Height"].ToString()
                    + "\" data-childs=\"" + (childs.Length > 0 ? "1" : "0") + "\" data-url=\"" + getAddress(dr, params1) + "\" data-parent=\"" + Guid.Empty.ToString() + "\" style=\""
                        + (icoColor.IsNullOrEmpty() ? "" : "color:" + icoColor.Trim1() + ";") + "\">");
                    menuSb.Append("<div class=\"menulistdiv1\">");
                    string appIco = dr["Ico"].ToString();
                    if (appIco.IsNullOrEmpty())
                    {
                        appIco = dr["AppIco"].ToString();
                    }
                    if (appIco.IsNullOrEmpty())
                    {
                        menuSb.Append("<i class=\"fa fa-list-ul\" style=\"font-size:14px;margin-right:3px;vertical-align:middle\"></i>");
                    }
                    else if (appIco.IsFontIco())
                    {
                        menuSb.Append("<i class=\"fa " + appIco + "\" style=\"font-size:14px;margin-right:3px;vertical-align:middle\"></i>");
                    }
                    else
                    {
                        menuSb.Append("<img src=\"" + Utility.Config.BaseUrl + appIco + "\" style=\"vertical-align:middle\" alt=\"\"/>");
                    }
                    menuSb.Append("<span style=\"vertical-align:middle\">" + dr["Title"].ToString().Trim1() + "</span>");
                    menuSb.Append("</div>");
                    if (childs.Length > 0)
                    {
                        menuSb.Append("<div class=\"menulistdiv2\"><i class=\"fa fa-angle-left\"></i></div>");
                    }
                    menuSb.Append("</div>");
                }
            }
            //加载一级菜单
            var root = appDt.Select(string.Format("ParentID='{0}'", Guid.Empty.ToString()));
            if (root.Length == 0)
            {
                return menuSb.ToString();
            }
            var apps = appDt.Select(string.Format("ParentID='{0}'", root[0]["ID"].ToString()));
            for (int i = 0; i < apps.Length; i++)
            {
                string params1 = string.Empty;
                DataRow dr = apps[i];
                if (!HasUse(dr["ID"].ToString().ToGuid(), userID, menuusers, out sourceMember, out params1))
                {
                    continue;
                }
                var childs = appDt.Select("ParentID='" + dr["ID"].ToString() + "'");
                bool hasChilds = false;
                foreach (var child in childs)
                {
                    if (HasUse(child["ID"].ToString().ToGuid(), userID, menuusers, out sourceMember, out params1))
                    {
                        hasChilds = true;
                        break;
                    }
                }

                string icoColor = dr["IcoColor"].ToString();
                if (icoColor.IsNullOrEmpty())
                {
                    icoColor = dr["IcoColor1"].ToString();
                }

                menuSb.Append("<div class=\"menulistdiv\" onclick=\"menuClick(this);\" data-id=\"" + dr["ID"].ToString()
                    + "\" data-title=\"" + dr["Title"].ToString().Trim() + "\" data-model=\"" + dr["OpenMode"].ToString()
                    + "\" data-width=\"" + dr["Width"].ToString() + "\" data-height=\"" + dr["Height"].ToString()
                    + "\" data-childs=\"" + (hasChilds ? "1" : "0") + "\" data-url=\"" + getAddress(dr, params1) + "\" data-parent=\"" + Guid.Empty.ToString() + "\" style=\""
                        + (icoColor.IsNullOrEmpty() ? "" : "color:" + icoColor.Trim1() + ";") + "\">");
                menuSb.Append("<div class=\"menulistdiv1\">");
                string appIco = dr["Ico"].ToString();
                if (appIco.IsNullOrEmpty())
                {
                    appIco = dr["AppIco"].ToString();
                }
                if (appIco.IsNullOrEmpty())
                {
                    menuSb.Append("<i class=\"fa fa-th-list\" style=\"font-size:14px;margin-right:3px;vertical-align:middle\"></i>");
                }
                else if (appIco.IsFontIco())
                {
                    menuSb.Append("<i class=\"fa " + appIco + "\" style=\"font-size:14px;margin-right:3px;vertical-align:middle\"></i>");
                }
                else
                {
                    menuSb.Append("<img src=\"" + Utility.Config.BaseUrl + appIco + "\" style=\"vertical-align:middle\" alt=\"\"/>");
                }
                menuSb.Append("<span style=\"vertical-align:middle\">" + dr["Title"].ToString().Trim1() + "</span>");
                menuSb.Append("</div>");
                if (hasChilds)
                {
                    menuSb.Append("<div class=\"menulistdiv2\"><i class=\"fa fa-angle-left\"></i></div>");
                }
                menuSb.Append("</div>");

            }
            var s = menuSb.ToString();

            return s;
        }

        /// <summary>
        /// 得到用户下级菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetUserMenuChilds(Guid userID, Guid refreshID, string rootDir = "", string isrefresh1 = "0")
        {
            StringBuilder menuSb = new StringBuilder();
            DataTable appDt1 = GetAllDataTable();
            var dv = appDt1.DefaultView;
            dv.RowFilter = string.Format("ParentID='{0}'", refreshID);
            dv.Sort = "Sort";
            var appDt = dv.ToTable();
            if (appDt.Rows.Count == 0)
            {
                return "[]";
            }
            int count = appDt.Rows.Count;
            System.Text.StringBuilder json = new System.Text.StringBuilder("[", count * 100);
            List<YJ.Data.Model.MenuUser> menuusers = new MenuUser().GetAll();
            string sourceMember = string.Empty;
            
            foreach (DataRow dr in appDt.Rows)
            {
                string params1 = string.Empty;
                if (!HasUse(dr["ID"].ToString().ToGuid(), userID, menuusers, out sourceMember, out params1))
                {
                    continue;
                }
                var childs = appDt1.Select(string.Format("ParentID='{0}'", dr["id"].ToString()));
                bool hasChilds = false;
                foreach (var child in childs)
                {
                    if (HasUse(child["ID"].ToString().ToGuid(), userID, menuusers, out sourceMember, out params1))
                    {
                        hasChilds = true;
                        break;
                    }
                }
                string icoColor = dr["IcoColor"].ToString();
                if (icoColor.IsNullOrEmpty())
                {
                    icoColor = dr["IcoColor1"].ToString();
                }
                menuSb.Append("<div class=\"menulistdiv\" " + ("1" == isrefresh1 ? "data-isrefresh1=\"1\"" : "") + " onclick=\"" + ("1" == isrefresh1 ? "menuClick(this, 1);" : "menuClick(this, 0);") + "\" data-id=\"" + dr["ID"].ToString()
                    + "\" data-title=\"" + dr["Title"].ToString().Trim() + "\" data-model=\"" + dr["OpenMode"].ToString()
                    + "\" data-width=\"" + dr["Width"].ToString() + "\" data-height=\"" + dr["Height"].ToString()
                    + "\" data-childs=\"" + (hasChilds ? "1" : "0") + "\" data-url=\"" + getAddress(dr, params1) + "\" data-parent=\"" + refreshID.ToString() + "\" style=\""
                        + (icoColor.IsNullOrEmpty() ? "" : "color:" + icoColor.Trim1() + ";") + "\">");
                menuSb.Append("<div class=\"menulistdiv1\">");
                string appIco = dr["Ico"].ToString();
                if (appIco.IsNullOrEmpty())
                {
                    appIco = dr["AppIco"].ToString();
                }
                if (appIco.IsNullOrEmpty())
                {
                    menuSb.Append("<i class=\"fa fa-file-text-o\" style=\"font-size:14px;margin-right:3px;vertical-align:middle\"></i>");
                }
                else if (appIco.IsFontIco())
                {
                    menuSb.Append("<i class=\"fa " + appIco + "\" style=\"font-size:14px;margin-right:3px;vertical-align:middle\"></i>");
                }
                else
                {
                    menuSb.Append("<img src=\"" + Utility.Config.BaseUrl + appIco + "\" style=\"vertical-align:middle\" alt=\"\"/>");
                }
                menuSb.Append("<span style=\"vertical-align:middle\">" + dr["Title"].ToString().Trim1() + "</span>");
                menuSb.Append("</div>");
                if (hasChilds)
                {
                    menuSb.Append("<div class=\"menulistdiv2\"><i class=\"fa fa-angle-left\"></i></div>");
                }
                menuSb.Append("</div>");
            }
            return menuSb.ToString();
        }


        /// <summary>
        /// 得到用户一级菜单(缩小显示)
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetUserMenu1(Guid userID)
        {
            Menu Menu = new Platform.Menu();
            AppLibrary Applibary = new AppLibrary();
            System.Data.DataTable appDt = Menu.GetAllDataTable();
            if (appDt.Rows.Count == 0)
            {
                return "";
            }
            StringBuilder menuSb = new StringBuilder();
            //添加快捷菜单
            string sourceMember = string.Empty;
            List<YJ.Data.Model.MenuUser> menuusers = new MenuUser().GetAll();
            var shortcuts = new YJ.Platform.UserShortcut().GetAllByUserID(userID, true);
            foreach (var shortcut in shortcuts)
            {
                string params1 = string.Empty;
                var menu = menuusers.FindAll(p => p.MenuID == shortcut.MenuID && p.SubPageID == Guid.Empty && p.Users.Contains(userID.ToString(), StringComparison.CurrentCultureIgnoreCase));
                if (menu.Count > 0)
                {
                    StringBuilder psb = new StringBuilder();
                    foreach (var m in menu.FindAll(p => !p.Params.IsNullOrEmpty()).GroupBy(p => p.Params))
                    {
                        psb.Append(m.Key.Trim1());
                        psb.Append("&");
                    }
                    params1 = psb.ToString().TrimEnd('&');
                }
                if (!HasUse(shortcut.MenuID, userID, menuusers, out sourceMember, out params1))
                {
                    continue;
                }
                var menudts = appDt.Select(string.Format("ID='{0}'", shortcut.MenuID.ToString()));
                if (menudts.Length > 0)
                {
                    DataRow dr = menudts[0];
                    string icoColor = dr["IcoColor"].ToString();
                    if (icoColor.IsNullOrEmpty())
                    {
                        icoColor = dr["IcoColor1"].ToString();
                    }
                    var childs = appDt.Select("ParentID='" + dr["ID"].ToString() + "'");
                    menuSb.Append("<div class=\"menulistdiv11\" title=\"" + dr["Title"].ToString().Trim1() + "\" onclick=\"menuClick1(this);\" data-id=\"" + dr["ID"].ToString()
                    + "\" data-title=\"" + dr["Title"].ToString().Trim() + "\" data-model=\"" + dr["OpenMode"].ToString()
                    + "\" data-width=\"" + dr["Width"].ToString() + "\" data-height=\"" + dr["Height"].ToString()
                    + "\" data-childs=\"" + (childs.Length > 0 ? "1" : "0") + "\" data-url=\"" + getAddress(dr, params1) + "\" data-parent=\"" + Guid.Empty.ToString() + "\" style=\""
                        + (icoColor.IsNullOrEmpty() ? "" : "color:" + icoColor.Trim1() + ";") + "\">");
                    menuSb.Append("<div class=\"menulistdiv1\">");
                    string appIco = dr["Ico"].ToString();
                    if (appIco.IsNullOrEmpty())
                    {
                        appIco = dr["AppIco"].ToString();
                    }
                    if (appIco.IsNullOrEmpty())
                    {
                        menuSb.Append("<i class=\"fa fa-list-ul\" style=\"margin-right:3px;vertical-align:middle\"></i>");
                    }
                    else if (appIco.IsFontIco())
                    {
                        menuSb.Append("<i class=\"fa " + appIco + "\" style=\"margin-right:3px;vertical-align:middle\"></i>");
                    }
                    else
                    {
                        menuSb.Append("<img src=\"" + Utility.Config.BaseUrl + appIco + "\" style=\"vertical-align:middle\" alt=\"\"/>");
                    }
                    menuSb.Append("</div>");
                    menuSb.Append("</div>");
                }
            }
            //加载一级菜单
            var root = appDt.Select(string.Format("ParentID='{0}'", Guid.Empty.ToString()));
            if (root.Length == 0)
            {
                return menuSb.ToString();
            }
            var apps = appDt.Select(string.Format("ParentID='{0}'", root[0]["ID"].ToString()));
            for (int i = 0; i < apps.Length; i++)
            {
                string params1 = string.Empty;
                DataRow dr = apps[i];
                if (!HasUse(dr["ID"].ToString().ToGuid(), userID, menuusers, out sourceMember, out params1))
                {
                    continue;
                }
                var childs = appDt.Select("ParentID='" + dr["ID"].ToString() + "'");
                bool hasChilds = false;
                foreach (var child in childs)
                {
                    if (HasUse(child["ID"].ToString().ToGuid(), userID, menuusers, out sourceMember, out params1))
                    {
                        hasChilds = true;
                        break;
                    }
                }

                string icoColor = dr["IcoColor"].ToString();
                if (icoColor.IsNullOrEmpty())
                {
                    icoColor = dr["IcoColor1"].ToString();
                }

                menuSb.Append("<div class=\"menulistdiv11\" title=\"" + dr["Title"].ToString().Trim1() + "\" onclick=\"menuClick1(this);\" data-id=\"" + dr["ID"].ToString()
                    + "\" data-title=\"" + dr["Title"].ToString().Trim() + "\" data-model=\"" + dr["OpenMode"].ToString()
                    + "\" data-width=\"" + dr["Width"].ToString() + "\" data-height=\"" + dr["Height"].ToString()
                    + "\" data-childs=\"" + (hasChilds ? "1" : "0") + "\" data-url=\"" + getAddress(dr, params1) + "\" data-parent=\"" + Guid.Empty.ToString() + "\" style=\""
                        + (icoColor.IsNullOrEmpty() ? "" : "color:" + icoColor.Trim1() + ";") + "\">");
                menuSb.Append("<div class=\"menulistdiv1\">");
                string appIco = dr["Ico"].ToString();
                if (appIco.IsNullOrEmpty())
                {
                    appIco = dr["AppIco"].ToString();
                }
                if (appIco.IsNullOrEmpty())
                {
                    menuSb.Append("<i class=\"fa fa-th-list\" style=\"margin-right:3px;vertical-align:middle\"></i>");
                }
                else if (appIco.IsFontIco())
                {
                    menuSb.Append("<i class=\"fa " + appIco + "\" style=\"margin-right:3px;vertical-align:middle\"></i>");
                }
                else
                {
                    menuSb.Append("<img src=\"" + Utility.Config.BaseUrl + appIco + "\" style=\"vertical-align:middle\" alt=\"\"/>");
                }
                menuSb.Append("</div>");
                menuSb.Append("</div>");

            }

            return menuSb.ToString();
        }
        
        /// <summary>
        /// 得到JSON
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="rootDir"></param>
        /// <param name="showSource">是否显示菜单来源(在查看人员菜单设置时用到)</param>
        /// <returns></returns>
        public string GetUserMenuJsonString(Guid userID, string rootDir = "", bool showSource = false)
        {
            Menu Menu = new Platform.Menu();
            AppLibrary Applibary = new AppLibrary();
            System.Data.DataTable appDt = Menu.GetAllDataTable();
            if (appDt.Rows.Count == 0)
            {
                return "[]";
            }

            var root = appDt.Select(string.Format("ParentID='{0}'", Guid.Empty.ToString()));
            if (root.Length == 0)
            {
                return "[]";
            }
            List<YJ.Data.Model.MenuUser> menuusers = new MenuUser().GetAll();
            var apps = appDt.Select(string.Format("ParentID='{0}'", root[0]["ID"].ToString()));
            
            System.Text.StringBuilder json = new System.Text.StringBuilder("", 1000);
            System.Data.DataRow rootDr = root[0];
            string params0 = string.Empty;
            //var menu0 = menuusers.Find(p => p.MenuID == rootDr["ID"].ToString().ToGuid() && p.SubPageID == Guid.Empty && p.Users.Contains(userID.ToString(), StringComparison.CurrentCultureIgnoreCase));
            //if (menu0 != null)
            //{
            //    params0 = menu0.Params;
            //}
            json.Append("{");
            json.AppendFormat("\"id\":\"{0}\",", rootDr["ID"].ToString());
            json.AppendFormat("\"title\":\"{0}\",", rootDr["Title"].ToString().Trim());
            json.AppendFormat("\"ico\":\"{0}\",", rootDr["AppIco"].ToString());
            json.AppendFormat("\"color\":\"{0}\",", rootDr["IcoColor"].ToString());
            json.AppendFormat("\"link\":\"{0}\",", getAddress(rootDr).ToString(), params0);
            json.AppendFormat("\"model\":\"{0}\",", rootDr["OpenMode"].ToString());
            json.AppendFormat("\"width\":\"{0}\",", rootDr["Width"].ToString());
            json.AppendFormat("\"height\":\"{0}\",", rootDr["Height"].ToString());
            json.AppendFormat("\"hasChilds\":\"{0}\",", apps.Length > 0 ? "1" : "0");
            json.AppendFormat("\"childs\":[");
            
            StringBuilder json1 = new StringBuilder(apps.Length * 100);
            string sourceMember = string.Empty;

            #region 加载个人快捷方式
            if (!showSource)
            {
                var shortcuts = new YJ.Platform.UserShortcut().GetAllByUserID(userID, true);
                if (shortcuts.Count > 0)
                {
                    json1.Append("{");
                    json1.AppendFormat("\"id\":\"{0}\",", Guid.NewGuid());
                    json1.AppendFormat("\"title\":\"{0}\",", "快捷菜单");
                    json1.AppendFormat("\"ico\":\"{0}\",", "");
                    json1.AppendFormat("\"color\":\"{0}\",", "");
                    json1.AppendFormat("\"link\":\"{0}\",", "");
                    json1.AppendFormat("\"model\":\"{0}\",", "");
                    json1.AppendFormat("\"width\":\"{0}\",", "");
                    json1.AppendFormat("\"height\":\"{0}\",", "");
                    json1.AppendFormat("\"hasChilds\":\"1\",");
                    json1.AppendFormat("\"childs\":[");
                    StringBuilder jsonShortcut = new StringBuilder();
                    foreach (var shortcut in shortcuts)
                    {
                       
                        string params1 = string.Empty;
                        var menu = menuusers.FindAll(p => p.MenuID == shortcut.MenuID && p.SubPageID == Guid.Empty && p.Users.Contains(userID.ToString(), StringComparison.CurrentCultureIgnoreCase));
                        if (menu.Count>0)
                        {
                            StringBuilder psb = new StringBuilder();
                            foreach (var m in menu.FindAll(p=>!p.Params.IsNullOrEmpty()).GroupBy(p=>p.Params))
                            {
                                psb.Append(m.Key.Trim1());
                                psb.Append("&");
                            }
                            params1 = psb.ToString().TrimEnd('&');
                        }
                        if (!HasUse(shortcut.MenuID, userID, menuusers, out sourceMember, out params1, showSource))
                        {
                            continue;
                        }
                        var menudts = appDt.Select(string.Format("ID='{0}'", shortcut.MenuID.ToString()));
                        if (menudts.Length > 0)
                        {
                            DataRow dr = menudts[0];
                            var childs = appDt.Select("ParentID='" + dr["ID"].ToString() + "'");
                            jsonShortcut.Append("{");
                            jsonShortcut.AppendFormat("\"id\":\"{0}\",", dr["ID"].ToString());
                            jsonShortcut.AppendFormat("\"title\":\"{0}\",", dr["Title"].ToString().Trim1());
                            jsonShortcut.AppendFormat("\"ico\":\"{0}\",", dr["AppIco"].ToString());
                            jsonShortcut.AppendFormat("\"color\":\"{0}\",", dr["IcoColor"].ToString());
                            jsonShortcut.AppendFormat("\"link\":\"{0}\",", getAddress(dr, params1));
                            jsonShortcut.AppendFormat("\"model\":\"{0}\",", dr["OpenMode"].ToString());
                            jsonShortcut.AppendFormat("\"width\":\"{0}\",", dr["Width"].ToString());
                            jsonShortcut.AppendFormat("\"height\":\"{0}\",", dr["Height"].ToString());
                            jsonShortcut.AppendFormat("\"hasChilds\":\"{0}\",", childs.Length > 0 ? "1" : "0");
                            jsonShortcut.AppendFormat("\"childs\":[]");
                            jsonShortcut.Append("},");
                            
                        }
                    }
                    json1.Append(jsonShortcut.Length > 0 ? jsonShortcut.ToString().TrimEnd(',') : "");
                    json1.Append("]");
                    json1.Append("}");
                    if (apps.Length > 0)
                    {
                        json1.Append(",");
                    }
                }
            }
            #endregion
            
            for (int i = 0; i < apps.Length; i++)
            {
                string params1 = string.Empty;
                DataRow dr = apps[i];
                if (!HasUse(dr["ID"].ToString().ToGuid(), userID, menuusers, out sourceMember, out params1, showSource))
                {
                    continue;
                }
                var childs = appDt.Select("ParentID='" + dr["ID"].ToString() + "'");
                bool hasChilds = false;
                foreach (var child in childs)
                {
                    if (HasUse(child["ID"].ToString().ToGuid(), userID, menuusers, out sourceMember, out params1, showSource))
                    {
                        hasChilds = true;
                        break;
                    }
                }
                json1.Append("{");
                json1.AppendFormat("\"id\":\"{0}\",", dr["ID"].ToString());
                json1.AppendFormat("\"title\":\"{0}{1}\",", dr["Title"].ToString().Trim1(), showSource ? "<span style='margin-left:4px;color:#666;'>[" + sourceMember + "]</span>" : "");
                json1.AppendFormat("\"ico\":\"{0}\",", dr["AppIco"].ToString());
                json1.AppendFormat("\"color\":\"{0}\",", dr["IcoColor"].ToString());
                json1.AppendFormat("\"link\":\"{0}\",", getAddress(dr, params1));
                json1.AppendFormat("\"model\":\"{0}\",", dr["OpenMode"].ToString());
                json1.AppendFormat("\"width\":\"{0}\",", dr["Width"].ToString());
                json1.AppendFormat("\"height\":\"{0}\",", dr["Height"].ToString());
                json1.AppendFormat("\"hasChilds\":\"{0}\",", hasChilds ? "1" : "0");
                json1.AppendFormat("\"childs\":[");

                json1.Append("]");
                json1.Append("}");
                json1.Append(",");
            }
            json.Append(json1.ToString().TrimEnd(','));
            json.Append("]");
            json.Append("}");
            return json.ToString();
        }

        /// <summary>
        /// 得到刷新JSON
        /// </summary>
        /// <returns></returns>
        public string GetUserMenuRefreshJsonString(Guid userID, Guid refreshID, string rootDir = "", bool showSource=false)
        {
            DataTable appDt1 = GetAllDataTable();
            var dv = appDt1.DefaultView;
            dv.RowFilter = string.Format("ParentID='{0}'", refreshID);
            dv.Sort = "Sort";
            var appDt = dv.ToTable();
            if (appDt.Rows.Count == 0)
            {
                return "[]";
            }
            int count = appDt.Rows.Count;
            System.Text.StringBuilder json = new System.Text.StringBuilder("[", count * 100);
            List<YJ.Data.Model.MenuUser> menuusers = new MenuUser().GetAll();
            string sourceMember = string.Empty;
            
            foreach (DataRow dr in appDt.Rows)
            {
                string params1 = string.Empty;
                if (!HasUse(dr["ID"].ToString().ToGuid(), userID, menuusers, out sourceMember, out params1, showSource))
                {
                    continue;
                }
                var childs = appDt1.Select(string.Format("ParentID='{0}'", dr["id"].ToString()));
                bool hasChilds = false;
                foreach (var child in childs)
                {
                    if (HasUse(child["ID"].ToString().ToGuid(), userID, menuusers, out sourceMember, out params1, showSource))
                    {
                        hasChilds = true;
                        break;
                    }
                }
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", dr["ID"].ToString());
                json.AppendFormat("\"title\":\"{0}{1}\",", dr["Title"].ToString().Trim1(), showSource ? "<span style='margin-left:4px;color:#666;'>[" + sourceMember + "]</span>" : "");
                json.AppendFormat("\"ico\":\"{0}\",", dr["AppIco"].ToString());
                json.AppendFormat("\"color\":\"{0}\",", dr["IcoColor"].ToString());
                json.AppendFormat("\"link\":\"{0}\",", getAddress(dr, params1));
                json.AppendFormat("\"model\":\"{0}\",", dr["OpenMode"].ToString());
                json.AppendFormat("\"width\":\"{0}\",", dr["Width"].ToString());
                json.AppendFormat("\"height\":\"{0}\",", dr["Height"].ToString());
                json.AppendFormat("\"hasChilds\":\"{0}\",", hasChilds ? "1" : "0");
                json.AppendFormat("\"childs\":[");
                json.Append("]");
                json.Append("}");
                json.Append(",");
            }

            return json.ToString().TrimEnd(',') + "]";
        }

        
        /// <summary>
        /// 判断一个人员是否有权限使用该菜单
        /// </summary>
        /// <param name="menuID"></param>
        /// <param name="userID"></param>
        /// <param name="menuusers"></param>
        /// <param name="source"></param>
        /// <param name="params1">地址参数</param>
        /// <param name="showSource"></param>
        /// <returns></returns>
        public bool HasUse(Guid menuID, Guid userID, List<YJ.Data.Model.MenuUser> menuusers, out string source, out string params1, bool showSource = false)
        {
            source = string.Empty;
            params1 = string.Empty;
            var menus = menuusers.FindAll(p => p.MenuID == menuID && p.SubPageID == Guid.Empty && p.Users.Contains(userID.ToString(), StringComparison.CurrentCultureIgnoreCase));
            if (menus.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var menu in menus)
                {
                    sb.Append(menu.Organizes);
                    sb.Append(",");
                    if (!menu.Params.IsNullOrEmpty())
                    {
                        params1 = menu.Params;
                    }
                }
                Organize borg = new Organize();
                source = borg.GetNames(sb.ToString().TrimEnd(','));
                StringBuilder psb = new StringBuilder();
                foreach (var m in menus.FindAll(p => !p.Params.IsNullOrEmpty()).GroupBy(p => p.Params))
                {
                    psb.Append(m.Key.Trim1());
                    psb.Append("&");
                }
                params1 = psb.ToString().TrimEnd('&');
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetMenuTreeTableHtml(string orgID, Guid? userId = null)
        {
            YJ.Platform.Menu bmenu = new YJ.Platform.Menu();
            DataTable appDt = bmenu.GetAllDataTable();
            var menuUsers = new YJ.Platform.MenuUser().GetAll();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            getMenuTreeTableHtml(sb, appDt, Guid.Empty, menuUsers, orgID, userId);
            return sb.ToString();
        }

        private void getMenuTreeTableHtml(System.Text.StringBuilder sb, DataTable appDt, Guid parentID, List<YJ.Data.Model.MenuUser> menuUsers, string orgID, Guid? userId = null)
        {
            DataRow[] drs = appDt.Select("ParentID='" + parentID.ToString() + "'");
            string orgid = orgID;
            foreach (DataRow dr in drs)
            {
                if (userId != null && userId.HasValue && !userId.Value.IsEmptyGuid())
                { 
                    string source,params1;
                    if (!HasUse(dr["ID"].ToString().ToGuid(), userId.Value, menuUsers, out source, out params1))
                    {
                        continue;
                    }
                }

                var mu = menuUsers.Find(p => p.MenuID == dr["ID"].ToString().ToGuid() && p.SubPageID.IsEmptyGuid()
                    && p.Organizes.Contains(orgid, StringComparison.CurrentCultureIgnoreCase));
                string menuchecked = mu != null ? " checked=\"checked\"" : "";

                sb.Append("<tr id=\"" + dr["ID"] + "\" data-pid=\"" + dr["ParentID"] + "\">");
                sb.Append("<td>" + dr["Title"] + "</td>");
                if (!dr["Ico"].ToString().IsNullOrEmpty())
                {
                    sb.Append("<td><input type=\"hidden\" name=\"apptype\" value=\"0\"/>" + (dr["Ico"].ToString().IsFontIco() ? "<i class=\"fa " + dr["Ico"].ToString() + "\" style=\"font-size:14px;\"></i>" : "<img src=\"" + dr["Ico"] + "\" style=\"vertical-align:middle;\"/>") + "</td>");
                }
                else
                {
                    sb.Append("<td><input type=\"hidden\" name=\"apptype\" value=\"0\"/>" + (dr["AppIco"].ToString().IsNullOrEmpty() ? "" : dr["AppIco"].ToString().IsFontIco() ? "<i class=\"fa " + dr["AppIco"].ToString() + "\" style=\"font-size:14px;\"></i>" : "<img src=\"" + dr["AppIco"] + "\" style=\"vertical-align:middle;\"/>") + "</td>");
                }
                sb.Append("<td style=\"text-align:center\"><input type=\"checkbox\" " + menuchecked + " onclick=\"appboxclick(this);\" name=\"menuid\" value=\"" + dr["ID"] + "\"/></td>");
                sb.Append("<td>");
                bool isAppLibrary = dr["AppLibraryID"].ToString().IsGuid();
                if (isAppLibrary)
                {
                    var buttons = new YJ.Platform.AppLibraryButtons1().GetAllByAppID(dr["AppLibraryID"].ToString().ToGuid());
                    foreach (var button in buttons.OrderBy(p => p.ShowType).ThenBy(p => p.Sort))
                    {
                        menuchecked = mu != null &&
                                mu.Buttons.Contains(button.ID.ToString(), StringComparison.CurrentCultureIgnoreCase) ? " checked=\"checked\"" : "";

                        sb.Append("<input type=\"checkbox\" " + menuchecked + " onclick=\"buttonboxclick(this);\" style=\"vertical-align:middle;\" id=\"button_" + dr["ID"] + "_" + button.ID + "\" name=\"button_" + dr["ID"] + "\" value=\"" + button.ID + "\"/>");
                        sb.Append("<label style=\"vertical-align:middle;\" for=\"button_" + dr["ID"] + "_" + button.ID + "\">" + button.Name + "</label>");
                    }
                }
                sb.Append("</td>");
                if (isAppLibrary)
                {
                    sb.Append("<td><input type=\"text\" class=\"mytext\" style=\"width:95%\" value=\"" + (mu != null ? mu.Params : "") + "\" name=\"params_" + dr["id"] + "\"/></td>");
                }
                else
                {
                    sb.Append("<td>&nbsp;</td>");
                }
                sb.Append("</tr>");
                if (isAppLibrary)
                {
                    var subpages = new YJ.Platform.AppLibrarySubPages().GetAllByAppID(dr["AppLibraryID"].ToString().ToGuid());
                    foreach (var page in subpages.OrderBy(p => p.Sort))
                    {
                        mu = menuUsers.Find(p => p.MenuID == dr["ID"].ToString().ToGuid() && p.SubPageID == page.ID
                            && p.Organizes.Contains(orgid, StringComparison.CurrentCultureIgnoreCase));
                        menuchecked = mu != null ? " checked=\"checked\"" : "";

                        sb.Append("<tr id=\"" + page.ID + "\" data-pid=\"" + dr["ID"] + "\">");
                        sb.Append("<td>" + page.Name + "</td>");
                        sb.Append("<td><input type=\"hidden\" name=\"apptype\" value=\"1\"/></td>");
                        sb.Append("<td style=\"text-align:center\"><input type=\"checkbox\" " + menuchecked + " onclick=\"appboxclick(this);\" name=\"subpage_" + dr["id"] + "\" value=\"" + page.ID + "\"/></td>");
                        sb.Append("<td>");
                        var buttons1 = new YJ.Platform.AppLibraryButtons1().GetAllByAppID(page.ID);
                        foreach (var button in buttons1.OrderBy(p => p.ShowType).ThenBy(p => p.Sort))
                        {
                            menuchecked = mu != null &&
                                mu.Buttons.Contains(button.ID.ToString(), StringComparison.CurrentCultureIgnoreCase) ? " checked=\"checked\"" : "";

                            sb.Append("<input type=\"checkbox\" " + menuchecked + " onclick=\"buttonboxclick(this);\" style=\"vertical-align:middle;\" id=\"button_" + page.ID + "_" + button.ID + "\" name=\"button_" + dr["id"] + "_" + page.ID + "\" value=\"" + button.ID + "\"/>");
                            sb.Append("<label style=\"vertical-align:middle;\" for=\"button_" + page.ID + "_" + button.ID + "\">" + button.Name + "</label>");
                        }
                        sb.Append("</td>");
                        sb.Append("<td>&nbsp;</td>");
                        sb.Append("</tr>");
                    }
                }
                getMenuTreeTableHtml(sb, appDt, dr["ID"].ToString().ToGuid(), menuUsers, orgID, userId);
            }
        }

        /// <summary>
        /// 查询一个应用程序库所有记录
        /// </summary>
        public List<YJ.Data.Model.Menu> GetAllByApplibaryID(Guid applibaryID)
        {
            return dataMenu.GetAllByApplibaryID(applibaryID);
        }
    }
}
