using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace WebMvc.Areas.Controls.Controllers
{
    public class UploadFilesController : Controller
    {
        //
        // GET: /Controls/UploadFiles/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index_App()
        {
            return View();
        }

        public string Upload()
        {
            Response.ContentType = "text/plain";
            string filetype = Request.Form["filetype"];

            if (!Common.Tools.CheckLogin(false))
            {
                return "您不能上传文件";
            }

            //接收上传后的文件
            HttpPostedFileBase file = Request.Files["Filedata"];

            if (filetype.IsNullOrEmpty())
            {
                if (!YJ.Utility.Config.UploadFileType.Contains(Path.GetExtension(file.FileName).TrimStart('.'), StringComparison.CurrentCultureIgnoreCase))
                {
                    return "您上传的文件类型不被允许";
                }
            }
            else
            {
                if (!filetype.Contains(Path.GetExtension(file.FileName).TrimStart('.'), StringComparison.CurrentCultureIgnoreCase))
                {
                    return "您上传的文件类型不被允许";
                }
            }

            //获取文件的保存路径
            string uploadPath;
            string uploadFullPath = Server.MapPath(getFilePath(out uploadPath));

            //判断上传的文件是否为空
            if (file != null)
            {
                if (!Directory.Exists(uploadFullPath))
                {
                    Directory.CreateDirectory(uploadFullPath);
                }
                //保存文件
                string newFileName = getFileName(uploadFullPath, file.FileName);
                string newFileFullPath = uploadFullPath + newFileName;
                try
                {
                    int fileLength = file.ContentLength;
                    file.SaveAs(newFileFullPath);
                    return "1|" + (uploadPath + newFileName) + "|" + (fileLength/1000).ToString("###,###") + "|" + newFileName;
                }
                catch
                {
                    return "上传文件发生了错误";
                }
            }
            else
            {
                return "上传文件为空";
            }
        }

        /// <summary>
        /// 得到上传文件名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string getFileName(string filePath, string fileName)
        {
            while (System.IO.File.Exists(filePath + fileName))
            {
                fileName = Path.GetFileNameWithoutExtension(fileName) + "_" + YJ.Utility.Tools.GetRandomString() + Path.GetExtension(fileName);
            }
            return fileName;
        }

        /// <summary>
        /// 得到文件保存路径
        /// </summary>
        /// <returns></returns>
        private string getFilePath(out string path1)
        {
            DateTime date = YJ.Utility.DateTimeNew.Now;
            path1 = Url.Content("~/Content/UploadFiles/" + date.ToString("yyyyMM") + "/" + date.ToString("dd") + "/");
            return path1;
        }

        public string Delete()
        {
            string str1 = Request.QueryString["str1"];
            string str2 = Request.QueryString["str2"];
            var obj = YJ.Cache.IO.Opation.Get(str1 ?? "");
            if (!Common.Tools.CheckLogin(false))
            {
                return "var json = {\"success\":0,\"message\":\"您不能删除文件\"}";
            }
            string file = Request.QueryString["file"];
            if (!file.IsNullOrEmpty())
            {
                try
                {
                    System.IO.File.Delete(Server.MapPath(Path.Combine(Url.Content("~/Content/Controls/UploadFiles/"), file)));
                    return "var json = {\"success\":1,\"message\":\"\"}";
                }
                catch (Exception e)
                {
                    return "var json = {\"success\":0,\"message\":\"" + e.Message + "\"}";
                }
            }
            return "";
        }

    }
}
