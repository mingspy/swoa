using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Areas.Controls.Controllers
{
    public class SelectMemberController : Controller
    {
        //
        // GET: /Controls/SelectMember/

        public ActionResult Index()
        {
            return View();
        }

        public string GetNames()
        {
            string values = Request.Form["values"];
            return new YJ.Platform.Organize().GetNames(values);
        }

        public string GetNote()
        {
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

        public ActionResult Index_App()
        {
            return View();
        }
    }
}
