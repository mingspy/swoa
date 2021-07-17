using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace WebMvc.Controllers
{
    public class LogController : MyController
    {
        //
        // GET: /Log/

        public ActionResult Index()
        {
            YJ.Platform.Log blog = new YJ.Platform.Log();
            ViewBag.TypeOptions = blog.GetTypeOptions();
            string query = string.Format("&appid={0}&tabid={1}",
                Request.QueryString["appid"],
                Request.QueryString["tabid"]
                );
            ViewBag.Query = query;
            return View();
        }
      
        public ActionResult Detail()
        {
            string id = Request.QueryString["id"];
            if (id.IsGuid())
            {
                return View(new YJ.Platform.Log().Get(id.ToGuid()));
            }
            else
            {
                return View(new YJ.Data.Model.Log());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Query()
        {
            string Title = Request.Form["Title"];
            string UserID = Request.Form["UserID"];
            string Type = Request.Form["Type"];
            string Date1 = Request.Form["Date1"];
            string Date2 = Request.Form["Date2"];
            string sidx = Request.Form["sidx"];
            string sord = Request.Form["sord"];
            long count;
            //string users = string.Empty;
            //if (!UserID.IsNullOrEmpty())
            //{
                //users = new YJ.Platform.Organize().GetAllUsersIdList(UserID).ToArray().Join1();
            //}
            int pageSize = YJ.Utility.Tools.GetPageSize();
            int pageNumber = YJ.Utility.Tools.GetPageNumber();
            string order = (sidx.IsNullOrEmpty() ? "WriteTime" : sidx) + " " + (sord.IsNullOrEmpty() ? "asc" : sord);
            DataTable dt = new YJ.Platform.Log().GetPagerData(out count, pageSize, pageNumber, Title, Type, Date1, Date2, UserID, order);
            List<object> list = new List<object>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new
                {
                    ID = dr["ID"].ToString(),
                    Title = dr["Title"].ToString(),
                    Type = dr["Type"].ToString(),
                    WriteTime = dr["WriteTime"].DateFormat("yyyy-MM-dd HH:mm:ss"),
                    UserName = dr["UserName"].ToString(),
                    IPAddress = dr["IPAddress"].ToString(),
                    Opation = "<a class=\"viewlink\" href=\"javascript:void(0);\" onclick=\"detail('" + dr["ID"].ToString() + "');return false;\">查看</a>"
                });
            }
            return "{\"userdata\":{\"total\":" + count + ",\"pagesize\":" + pageSize + ",\"pagenumber\":" + pageNumber + "},\"rows\":" + list.ToJsonString() + "}";
        }
    }
}
