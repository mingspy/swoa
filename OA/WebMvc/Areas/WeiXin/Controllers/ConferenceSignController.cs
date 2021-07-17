using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YJ.Data.Model.Areas;

namespace WebMvc.Areas.WeiXin.Controllers
{
    public class ConferenceSignController : Controller
    {
        //
        // GET: /WeiXin/ConferenceSign/

        public ActionResult Index()
        {
            YJ.Platform.WeiXin.Organize.CheckLogin();
            ViewBag.Uid= YJ.Platform.Users.CurrentUserID;
            ViewBag.Uname = YJ.Platform.Users.CurrentUserName;
            if (YJ.Platform.Users.CurrentUserID.IsEmptyGuid())
            {
                return this.Content("<script>alert('未授权或没有此用户，请联系系统管理员！'); var userAgent = navigator.userAgent; if (userAgent.indexOf(\"Firefox\") != -1 ||userAgent.indexOf(\"Presto\") != -1){ window.location.replace(\"about:blank\"); } else  {  window.opener = null;  window.open(\"\", \"_self\"); window.close(); }</script>");
            }
            string ConferenceNo=Request.QueryString["ConferenceNo"];
            var dbcon = new YJ.Platform.DBConnection();
            string sql = string.Format("select *,CONVERT(varchar(50), EndDay, 23)+'  '+CONVERT(varchar(50), EndTime, 24) 'EndDT',CONVERT(varchar(50),BeginDay,23)+'  '+CONVERT(varchar(50),BeginTime,24) 'BeginDT'     from CRMeetingRequest where id=@id");
            SqlParameter[] _param = new SqlParameter[]
                {new SqlParameter("@id", SqlDbType.UniqueIdentifier, -1) {Value =ConferenceNo}};
            IDataParameter[] param = new IDataParameter[_param.Length];
            _param.CopyTo(param, 0);
            var data=dbcon.GetDataTable(dbcon.GetAll().Find(a=>a.Type.Equals(YJ.Utility.Config.DataBaseType1)).ID,sql,param);
            if (data.Rows.Count<=0)
            {
                return this.Content("<script>alert('未找到此次会议，请咨询会议申请人员！'); var userAgent = navigator.userAgent; if (userAgent.indexOf(\"Firefox\") != -1 ||userAgent.indexOf(\"Presto\") != -1){ window.location.replace(\"about:blank\"); } else  {  window.opener = null;  window.open(\"\", \"_self\"); window.close(); }</script>");
            }
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Sign()
        {
            try
            {
                string ConferenceID = Request.Form["ConferenceID"];
                string Cuid = Request.Form["Cuid"];
                string Cuname = Request.Form["Cuname"];
                YJ.Data.Model.Areas.CRConferenceSign ccsModel=new CRConferenceSign();
                YJ.Platform.Areas.CRConferenceSign ccsBLL=new YJ.Platform.Areas.CRConferenceSign();
                if (ccsBLL.GetAll().Where(a=>a.CRMeetingID==ConferenceID).Where(a=>a.UserID== Cuid).Count()>0)
                {
                    return "{\"status\":1,\"msg\":\"请不要重复签到!\"}";
                }
                ccsModel.SignDate=DateTime.Now;
                ccsModel.CRMeetingID = ConferenceID;
                ccsModel.UserID = Cuid;
                ccsModel.UserName = Cuname;
                if (ccsBLL.Add(ccsModel)<=0)
                {
                    return "{\"status\":2,\"msg\":\"签到失败，请尝试重新签到!\"}";
                }
                return "{\"status\":0,\"msg\":\"签到成功!\"}";
            }
            catch (Exception e)
            {
                return "{\"status\":1,\"msg\":\""+e.Message+"!\"}";
            }
        }
        public ActionResult SignImg()
        {
            ViewBag.ImgUrl=Url.Content(YJ.Utility.Config.WebUrl+ "/weixin/ConferenceSign?ConferenceNo=" + Request.QueryString["ConferenceNo"]);
            return View();
        }

    }
}
