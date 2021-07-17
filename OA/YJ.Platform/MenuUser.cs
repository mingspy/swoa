using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace YJ.Platform
{
    public class MenuUser
    {
        private static readonly string cachekey = Utility.Keys.CacheKeys.MenuTable.ToString() + "_MenuUsers";
        private YJ.Data.Interface.IMenuUser dataMenuUser;
        public MenuUser()
        {
            this.dataMenuUser = YJ.Data.Factory.Factory.GetMenuUser();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.MenuUser model)
        {
            return dataMenuUser.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.MenuUser model)
        {
            return dataMenuUser.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.MenuUser> GetAll(bool cache = true)
        {
            if (!cache)
            {
                Organize borg = new Organize();
                var list = dataMenuUser.GetAll();
                foreach (var li in list)
                {
                    li.Users = borg.GetAllUsersIdString(li.Organizes);
                }
                return list;
            }
            else
            {
                var obj = Cache.IO.Opation.Get(cachekey);
                if (obj == null)
                {
                    Organize borg = new Organize();
                    var list = dataMenuUser.GetAll();
                    foreach (var li in list)
                    {
                        li.Users = borg.GetAllUsersIdString(li.Organizes);
                    }
                    Cache.IO.Opation.Set(cachekey, list);
                    return list;
                }
                else
                {
                    return (List<Data.Model.MenuUser>)obj;
                }
            }
        }

        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.MenuUser Get(Guid id)
        {
            return dataMenuUser.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            return dataMenuUser.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataMenuUser.GetCount();
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        public int DeleteByOrganizes(string organizes)
        {
            return dataMenuUser.DeleteByOrganizes(organizes);
        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        public void ClearCache()
        {
            Cache.IO.Opation.Remove(cachekey);
        }

        /// <summary>
        /// 更新所有菜单权限
        /// </summary>
        public void RefreshUsers()
        { 
            YJ.Platform.MenuUser bmenuusers = new YJ.Platform.MenuUser();
            YJ.Platform.Organize borg=new YJ.Platform.Organize();
            var menuusers = bmenuusers.GetAll(false);
            foreach (var menuuser in menuusers)
            {
                menuuser.Users = borg.GetAllUsersIdList(menuuser.Organizes).ToArray().Join1(",");
                bmenuusers.Update(menuuser);
            }
            ClearCache();
        }
       
        /// <summary>
        /// 得到当前应用对于当前用户的显示按钮
        /// </summary>
        /// <param name="showType"></param>
        /// <param name="menuID"></param>
        /// <param name="subpageID"></param>
        /// <returns></returns>
        public static Dictionary<int, string> getButtonsHtml(string menuID="", string subpageID="", string programID="")
        {
            Guid menuID1;
            if (menuID.IsNullOrEmpty() || !menuID.IsGuid(out menuID1))
            {
                menuID1 = System.Web.HttpContext.Current.Request.QueryString["appid"].ToGuid();
            }
            Guid subpageID1;
            if (!subpageID.IsGuid(out subpageID1))
            {
                subpageID1 = System.Web.HttpContext.Current.Request.QueryString["subpageid"].ToGuid();
            }
            Dictionary<int, string> dicts = new Dictionary<int, string>();
            dicts.Add(0, "");
            dicts.Add(1, "");
            dicts.Add(2, "");
            string applibaryid = System.Web.HttpContext.Current.Request.QueryString["applibaryid"];
            List<Guid> buttons = new List<Guid>();
            if (applibaryid.IsGuid())
            {
                var abuttons = new AppLibraryButtons1().GetAllByAppID(applibaryid.ToGuid()).FindAll(p => p.IsValidateShow == 1);
                foreach (var abut in abuttons)
                {
                    buttons.Add(abut.ID);
                }
            }
            else
            {
                var menuusers = new MenuUser().GetAll().FindAll(p => p.MenuID == menuID1 && p.SubPageID == subpageID1
                    && p.Users.Contains(YJ.Platform.Users.CurrentUserID.ToString(), StringComparison.CurrentCultureIgnoreCase));
                foreach (var menuuser in menuusers)
                {
                    foreach (string buttonid in menuuser.Buttons.Split(','))
                    {
                        Guid bid;
                        if (buttonid.IsGuid(out bid))
                        {
                            if (!buttons.Contains(bid))
                            {
                                buttons.Add(bid);
                            }
                        }
                    }
                }
            }
            List<Data.Model.AppLibraryButtons1> buttonList = new List<Data.Model.AppLibraryButtons1>();
            //加上不需要验证权限的按钮
            var app = new AppLibrary().GetByCode(programID, true);
            if (app != null)
            {
                buttonList.AddRange(new AppLibraryButtons1().GetAllByAppID(app.ID).FindAll(p => p.IsValidateShow == 0));
            }
            //===
            if (buttons.Count == 0 && buttonList.Count == 0)
            {
                return dicts;
            }
            StringBuilder sb0 = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            YJ.Platform.AppLibraryButtons1 bbut = new AppLibraryButtons1();
            
            foreach (Guid buttonid in buttons)
            {
                var button = bbut.Get(buttonid, true);
                if (button == null)
                {
                    continue;
                }
                else if (button.IsValidateShow == 0)//去掉不需要授权的按钮，避免显示重复
                {
                    continue;
                }
                buttonList.Add(button);
            }
            foreach (var button in buttonList.OrderBy(p=>p.Sort))
            {
                string userid = Users.CurrentUserID.ToString();
                if (button.ShowType == 0)
                {
                    string funName = "fun_" + Guid.NewGuid().ToString("N");
                    sb0.Append("<a href=\"javascript:void(0);\" onclick=\"" + funName + "();return false;\"><span style=\"" + (!button.Ico.IsNullOrEmpty() && !button.Ico.IsFontIco() ? "background-image:url(" + Utility.Config.BaseUrl + button.Ico + ");" : "padding-left:0px;") + "\">" + (!button.Ico.IsNullOrEmpty() && button.Ico.IsFontIco() ? "<i class='fa " + button.Ico + "' style='font-size:14px;vertical-align:middle;margin-right:3px;'></i>" : "") + button.Name + "</span></a>");
                    sb0.Append("<script type=\"text/javascript\">function " + funName + "(){" + button.Events.FilterWildcard(userid) + "}</script>");
                }
                else if (button.ShowType == 1)
                {
                    string funName = "fun_" + Guid.NewGuid().ToString("N");
                    sb1.Append("<button type=\"button\" " + (button.Ico.IsNullOrEmpty() ? "style=\"margin-left:6px;\"" : "style=\"margin-left:6px;" + (!button.Ico.IsNullOrEmpty() && !button.Ico.IsFontIco() ? "background-image:url(" + Utility.Config.BaseUrl + button.Ico + ");background-repeat:no-repeat;background-position-y:center;background-position-x:8px;padding-left:28px;" : "") + "\"") + " onclick=\"" + funName + "();return false;\" class=\"mybutton\">");
                    if (!button.Ico.IsNullOrEmpty() && button.Ico.IsFontIco())
                    {
                        sb1.Append("<i class=\"fa " + button.Ico + "\" style=\"font-size:14px;vertical-align:middle;margin-right:3px;\"></i>");
                    }
                    sb1.Append("<span style=\"vertical-align:middle;\">" + button.Name + "</span>");
                    sb1.Append("</button>");
                    sb1.Append("<script type=\"text/javascript\">function " + funName + "(){" + button.Events.FilterWildcard(userid) + "}</script>");
                }
                else if (button.ShowType == 2)
                {
                    string funName = "fun_" + Guid.NewGuid().ToString("N");
                    sb2.Append("<a href=\"javascript:void(0);\" onclick=\"" + button.Events + ";return false;\" " +
                        (button.Ico.IsNullOrEmpty() ? "style=\"margin-left:0px;\"" : "style=\"margin-left:0px;" + (!button.Ico.IsFontIco() ? "padding-left:26px;background-image:url(" + Utility.Config.BaseUrl + button.Ico + ");background-repeat:no-repeat;background-position-y:center;background-position-x:8px;" : "") + "\"") + ">"
                        + (!button.Ico.IsNullOrEmpty() && button.Ico.IsFontIco() ? "<i class='fa " + button.Ico + "' style='font-size:14px;vertical-align:middle;margin-right:3px;padding-left:10px;'></i>" : "") + button.Name + "</a>");
                    //sb2.Append("<script type=\"text/javascript\">function " + funName + "(){" + button.Events + "}</script>");
                }
                
            }
            dicts[0] = sb0.Length > 0 ? "<div class=\"toolbar\" style=\"margin-top:0; border-top:none 0; position:fixed; top:0; left:0; right:0; margin-left:auto; z-index:999; width:100%; margin-right:auto;\">" +
                sb0.ToString() + "</div>" : "";
            dicts[1] = sb1.ToString();
            dicts[2] = sb2.ToString();
            return dicts;
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        public int DeleteByMenuID(Guid menuID)
        {
            return dataMenuUser.DeleteByMenuID(menuID);
        }
    }
}
