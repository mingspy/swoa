using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Controllers
{
    public class OnlineUsersController : MyController
    {
        //
        // GET: /OnlineUsers/
        [MyAttribute(CheckApp = false)]
        public ActionResult Index()
        {
            return query(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAttribute(CheckApp=false)]
        public ActionResult Index(FormCollection collection)
        {
            YJ.Platform.OnlineUsers bou = new YJ.Platform.OnlineUsers();
            if (!Request.Form["ClearAll"].IsNullOrEmpty())
            {
                bou.RemoveAll();
            }

            if (!Request.Form["ClearSelect"].IsNullOrEmpty())
            {
                string userids = Request.Form["checkbox_app"];
                if (!userids.IsNullOrEmpty())
                {
                    foreach (string userid in userids.Split(','))
                    {
                        Guid uid;
                        if (userid.IsGuid(out uid))
                        {
                            bou.Remove(uid);
                        }
                    }
                }
            }

            return query(collection);
        }

        [MyAttribute(CheckApp = false)]
        private ActionResult query(FormCollection collection)
        {
            YJ.Platform.OnlineUsers bou = new YJ.Platform.OnlineUsers();
            string name = string.Empty;
            string orgname = string.Empty;
            if (collection != null)
            {
                name = Request.Form["Name"];
                orgname = Request.Form["OrgName"];
            }
            else
            {
                name = Request.QueryString["Name"];
                orgname = Request.QueryString["OrgName"];
            }
            ViewBag.Name = name;
            ViewBag.OrgName = orgname;
            var userList = bou.GetAll();
            if (!name.IsNullOrEmpty())
            {
                userList = userList.Where(p => p.UserName.IndexOf(name) >= 0).ToList();
                
            }
            else if (!orgname.IsNullOrEmpty())
            {
                userList = userList.Where(p => p.OrgName.IndexOf(orgname) >= 0).ToList();
            }
            return View(userList);

        }

    }
}
