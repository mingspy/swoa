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

    public class AssetsQrCodeController : Controller
    {
        //
        // GET: /WeiXin/AssetsQrCode/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAssetsInfo() {

            //扫码时获取固定资产所有的信息。

            /*扫码时获取固定资产全部信息
            YJ.Platform.WeiXin.Organize.CheckLogin();
            ViewBag.Uid = YJ.Platform.Users.CurrentUserID;
            ViewBag.Uname = YJ.Platform.Users.CurrentUserName;
            if (YJ.Platform.Users.CurrentUserID.IsEmptyGuid())
            {
                return this.Content("<script>alert('未授权或没有此用户，请联系系统管理员！'); var userAgent = navigator.userAgent; if (userAgent.indexOf(\"Firefox\") != -1 ||userAgent.indexOf(\"Presto\") != -1){ window.location.replace(\"about:blank\"); } else  {  window.opener = null;  window.open(\"\", \"_self\"); window.close(); }</script>");
            }
            */
            string AssetsID = Request.QueryString["AssetsID"];
            var dbcon = new YJ.Platform.DBConnection();
            string sql = string.Format("SELECT * FROM 一般固定资产信息 WHERE 资产编号=@资产编号");
            SqlParameter[] _param = new SqlParameter[]
                {new SqlParameter("@资产编号", SqlDbType.NVarChar, -1) {Value =AssetsID}};
            IDataParameter[] param = new IDataParameter[_param.Length];
            _param.CopyTo(param, 0);
            var data = dbcon.GetDataTable(dbcon.GetAll().Find(a => a.Type.Equals(YJ.Utility.Config.DataBaseType1)).ID, sql, param);
            if (data.Rows.Count <= 0)
            {
                return this.Content("<script>alert('未找到该固定资产信息！'); var userAgent = navigator.userAgent; if (userAgent.indexOf(\"Firefox\") != -1 ||userAgent.indexOf(\"Presto\") != -1){ window.location.replace(\"about:blank\"); } else  {  window.opener = null;  window.open(\"\", \"_self\"); window.close(); }</script>");
            }
            return View(data);//将数据返回至页面，页面@model将数据绑定到控件上。


        }


        public ActionResult GetQrCode() {
            ViewBag.ImgUrl = Url.Content(YJ.Utility.Config.WebUrl + "/weixin/AssetsQrCode/GetAssetsInfo?AssetsID=" + Request.QueryString["AssetsID"]);//二维码对应的链接，即扫码时触发的访问~/WeiXin/AssetsQrCode/GetAssetsInfo，该控制方法返回内容到页面。
            return View();
        }


        public ActionResult BatchRemove(){
            //支持批量删除操作，应用程序设计3个要点，1是数据要设计成html控件形式，2是页面脚本支持参数传递、3是在控制方法中进行处理。
            string AssetsIDs = Request.QueryString["AssetsIDs"];
            //业务代码
            return this.Content("<script>alert('批量删除成功！'); var userAgent = navigator.userAgent; if (userAgent.indexOf(\"Firefox\") != -1 ||userAgent.indexOf(\"Presto\") != -1){ window.location.replace(\"about:blank\"); } else  {  window.opener = null;  window.open(\"\", \"_self\"); window.close(); }</script>");

        }

    }
}
