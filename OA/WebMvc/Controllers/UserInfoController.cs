using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace WebMvc.Controllers
{
    public class UserInfoController : MyController
    {
        //
        // GET: /UserInfo/

        public ActionResult Index()
        {
            return View();
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult EditUserInfo()
        {
            return EditUserInfo(null);
        }

        [HttpPost]
        [MyAttribute(CheckApp = false)]
        public ActionResult EditUserInfo(FormCollection collection)
        {
            YJ.Platform.Users BUI = new YJ.Platform.Users();
            YJ.Data.Model.Users ui = null;

            Guid userID = YJ.Platform.Users.CurrentUserID;
            ui = BUI.Get(userID);

            if (collection != null)
            {
                string Tel = Request.Form["Tel"];
                string MobilePhone = Request.Form["MobilePhone"];
                string WeiXin = Request.Form["WeiXin"];
                string Email = Request.Form["Email"];
                string QQ = Request.Form["QQ"];
                string OtherTel = Request.Form["OtherTel"];
                string Note = Request.Form["Note"];

                bool isAdd = false;
                if (ui == null)
                {
                    
                }
                ui.Tel = Tel;
                ui.Mobile = MobilePhone;
                ui.WeiXin = WeiXin;
                ui.Email = Email;
                ui.QQ = QQ;
                ui.OtherTel = OtherTel;
                ui.Note = Note;

                if (isAdd)
                {
                    BUI.Add(ui);
                }
                else
                {
                    BUI.Update(ui);
                }
                ViewBag.script = "alert('保存成功!');window.location=window.location;";
            }
            if (ui == null)
            {
              
            }
            return View(ui);
        }

        [HttpPost]
        [MyAttribute(CheckApp=false)]
        public string SaveUserHead()
        {
            string x = Request.Form["x"];
            string y = Request.Form["y"];
            string x2 = Request.Form["x2"];
            string y2 = Request.Form["y2"];
            string w = Request.Form["w"];
            string h = Request.Form["h"];
            string img = (Request.Form["img"] ?? "").DesDecrypt();
            var userID = YJ.Platform.Users.CurrentUserID;
            if (img.IsNullOrEmpty() || !System.IO.File.Exists(img))
            {
                return "文件不存在!";
            }
            try
            {
                string newfile = YJ.Utility.ImgHelper.CutAvatar(img, Common.Tools.BaseUrl + "/Content/UserHeads/" + userID + ".jpg", x.ToInt(), y.ToInt(), w.ToInt(), h.ToInt());
                if (!newfile.IsNullOrEmpty())
                {
                    YJ.Platform.Users bui = new YJ.Platform.Users();
                    var ui = bui.Get(userID);
                    if (ui != null)
                    {
                        ui.HeadImg = newfile;
                        bui.Update(ui);
                    }
                    return "保存成功!";
                }
                else
                {
                    return "保存失败!";
                }
            }
            catch
            {
                return "保存失败!";
            }
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult ShortcutSet()
        {
            return ShortcutSet(null);
        }
        [HttpPost]
        [MyAttribute(CheckApp = false)]
        public ActionResult ShortcutSet(FormCollection collection)
        {
            YJ.Platform.UserShortcut bus = new YJ.Platform.UserShortcut();
            if (collection != null)
            {
                
                if (!Request.Form["issort"].IsNullOrEmpty())
                {
                    string[] sorts = (Request.Form["sort"] ?? "").Split(',');
                    for (int j = 0; j < sorts.Length; j++)
                    {
                        var us = bus.Get(sorts[j].ToGuid());
                        if (us != null)
                        {
                            us.Sort = j;
                            bus.Update(us);
                        }
                    }
                    ViewBag.script = "alert('排序保存成功!');window.location=window.location;";
                }
                else
                {
                    Guid userID = YJ.Platform.Users.CurrentUserID;
                    string menuids = Request.Form["menuid"] ?? "";
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        bus.DeleteByUserID(userID);
                        int i = 0;
                        foreach (string menuid in menuids.Split(','))
                        {
                            if (menuid.IsGuid())
                            {
                                YJ.Data.Model.UserShortcut us = new YJ.Data.Model.UserShortcut();
                                us.ID = Guid.NewGuid();
                                us.MenuID = menuid.ToGuid();
                                us.Sort = i++;
                                us.UserID = userID;
                                bus.Add(us);
                            }
                        }
                        scope.Complete();
                        ViewBag.script = "alert('保存成功!');window.location=window.location;";
                    }
                }
                bus.ClearCache();
            }
            return View();
        }

        public ActionResult EditUserHeader()
        {
            return View();
        }
    }
}
