using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Controllers
{
    public class UserDefineController : Controller
    {
        //用户自定义方法
        // GET: /UserDefine/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 通过车辆ID获取上一次结束里程作为本次起始里程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [MyAttribute(CheckApp = false, CheckUrl = false)]
        public ActionResult GetStartOdometer()
        {
            string ID = Request.QueryString["CarID"];
            if (null == ID) {
                ID = "";
            }
            string sql = string.Empty;
            sql = "SELECT TOP(1) " +
                  "Odometer " +
                  "FROM[WeiXinTest20181217].[dbo].[OaVehicleApplication] " +
                   "where CarNumber = @ID and Status = 1 " +
                   " order by seque DESC";
            System.Data.SqlClient.SqlParameter[] par = new System.Data.SqlClient.SqlParameter[] {
                    new System.Data.SqlClient.SqlParameter("@ID", ID)
            };
            var data = new YJ.Data.MSSQL.DBHelper().Execute(sql, par, true);
            return View();
        }

    }
}
