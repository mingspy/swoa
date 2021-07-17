using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text;

namespace WebMvc.Areas.Controls.Controllers
{
    public class SelectDivController : Controller
    {
        //
        // GET: /Controls/SelectDiv/

        public ActionResult Index()
        {
            return View();
        }

        public string GetTitles()
        {
            string loginMsg;
            if (!WebMvc.Common.Tools.CheckLogin(out loginMsg) && !YJ.Platform.WeiXin.Organize.CheckLogin())
            {
                return "";
            }
            string values = Request.QueryString["values"];
            string titlefield = Request.QueryString["titlefield"];
            string pkfield = Request.QueryString["pkfield"];
            string applibaryid = Request.QueryString["applibaryid"];
            var applibary = new YJ.Platform.AppLibrary().Get(applibaryid.ToGuid());
            if (applibary == null)
            {
                return values;
            }
            var program = new YJ.Platform.ProgramBuilder().Get(applibary.Code.ToGuid());
            if (program == null)
            {
                return values;
            }
            var dbconn = new YJ.Platform.DBConnection().Get(program.DBConnID);
            if (dbconn == null)
            {
                return values;
            }
            string sql = "select " + titlefield + " from (" + program.SQL.ReplaceSelectSql().FilterWildcard(YJ.Platform.Users.CurrentUserID.ToString()) + ") gettitletemptable where " + pkfield + " in(" + YJ.Utility.Tools.GetSqlInString(values) + ")";
            DataTable dt = new YJ.Platform.DBConnection().GetDataTable(dbconn, sql);
            StringBuilder titles = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                titles.Append(dr[0]);
                titles.Append(",");
            }
            return titles.ToString().TrimEnd(',');
        }
        
    }
}
