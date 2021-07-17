using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace WebMvc.Controllers
{
    public class WorkCalendarController : MyController
    {
        //
        // GET: /WorkCalendar/

        public ActionResult Index()
        {
            return Index(null);
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            YJ.Data.MSSQL.WorkCalendar BCal = new YJ.Data.MSSQL.WorkCalendar();
            int Year1 = Request.Form["DropDownList1"].IsNullOrEmpty() ? YJ.Utility.DateTimeNew.Now.Year : Request.Form["DropDownList1"].ToInt();

            if (!Request.Form["saveBut"].IsNullOrEmpty())
            {
                string workdate = Request.Form["workdate"] ?? "";
                string year1 = Request.Form["year1"];
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    BCal.Delete(year1.ToInt());
                    foreach (string wk in workdate.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct())
                    {
                        if (wk.IsDateTime())
                        {
                            var cal = new YJ.Data.Model.WorkCalendar();
                            cal.WorkDate = wk.ToDateTime();
                            BCal.Add(cal);
                        }
                    }
                    scope.Complete();
                }
                YJ.Cache.IO.Opation.Remove("WorkCalendar_" + year1);
                YJ.Platform.Log.Add("设置了工作日历", workdate, YJ.Platform.Log.Types.系统管理);
                ViewBag.script = "alert('保存成功!')";
            }
            
            StringBuilder options = new StringBuilder();
            for (int i = 2016; i < 2099; i++)
            {
                options.Append("<option value='" + i + "'" + (i == Year1 ? "selected='selected'" : "") + ">" + i + "</option>");
            }
            var list = BCal.GetAll(Year1);
            ViewBag.options = options;
            ViewBag.year = Year1;
            return View(list);
        }

    }
}
