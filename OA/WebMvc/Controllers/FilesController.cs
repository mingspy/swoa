using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace WebMvc.Controllers
{
    public class FilesController : MyController
    {
        //
        // GET: /Files/
        [MyAttribute(CheckApp=false)]
        public ActionResult Index()
        {
            return View();
        }

        [MyAttribute(CheckApp=false)]
        public ActionResult Tree()
        {
            return View();
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult List()
        {
            return List(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAttribute(CheckApp = false)]
        public ActionResult List(FormCollection collection)
        {
            string dir = (Request.QueryString["id"] ?? "").DesDecrypt();
            string ParentDir = string.Empty;
            string RootDir = string.Empty;
            YJ.Platform.Files BFiles = new YJ.Platform.Files();
            string Query = string.Empty;
            if (dir.IsNullOrEmpty() || !Directory.Exists(dir))
            {
                Response.Write("目录为空或不存在!");
                Response.End();
                return null;
            }
            RootDir = BFiles.GetUserRootPath(YJ.Platform.Users.CurrentUserID);
            if (!dir.Equals(RootDir, StringComparison.CurrentCultureIgnoreCase))
            {
                var parent = Directory.GetParent(dir);
                ParentDir = parent.FullName;
            }
            Query = "&appid=" + Request.QueryString["appid"] + "&isselect=" + Request.QueryString["isselect"] + "&eid=" + Request.QueryString["eid"] +
                "&files=" + Request.QueryString["files"] + "&filetype=" + Request.QueryString["filetype"] + "&iframeid=" + Request.QueryString["iframeid"];

            if (collection != null)
            {
                string operation = Request.Form["operation"];
                if ("0" == operation)
                {
                    string refreshDir = ParentDir.IsNullOrEmpty() ? RootDir : ParentDir;
                    string msg = BFiles.Delete(dir);
                    ViewBag.script = "alert('" + (msg.IsNullOrEmpty() ? "删除成功!" : msg) + "');window.location='List?id=" + refreshDir.DesEncrypt() + Query + "';";
                }
                else if ("1" == operation)
                {
                    string files = Request.Form["file"] ?? "";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (var file in files.Split(','))
                    {
                        sb.Append(BFiles.Delete(file.DesDecrypt()));
                    }
                    string refreshDir = ParentDir.IsNullOrEmpty() ? RootDir : ParentDir;
                    ViewBag.script = "alert('" + (sb.Length == 0 ? "删除成功!" : sb.ToString()) + "');window.location='List?id=" + refreshDir.DesEncrypt() + Query + "';";
                }
            }
            ViewBag.ParentDir = ParentDir;
            ViewBag.dir = dir;
            ViewBag.RootDir = RootDir;
            ViewBag.Query = Query;
            List<YJ.Data.Model.Files> FilesList = BFiles.GetList(dir);
            return View(FilesList);
        }

        [MyAttribute(CheckApp = false)]
        public string Tree1()
        {
            return "[" + new YJ.Platform.Files().GetUserDirectoryJson(YJ.Platform.Users.CurrentUserID, "", true) + "]";
        }

        [MyAttribute(CheckApp = false)]
        public string TreeRefresh()
        {
            string dir = Request.QueryString["refreshid"];
            if (dir.IsNullOrEmpty())
            {
                return "[]";
            }

            return new YJ.Platform.Files().GetUserDirectoryJson(YJ.Platform.Users.CurrentUserID, dir.DesDecrypt());
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult Dir_Add()
        {
            return Dir_Add(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAttribute(CheckApp = false)]
        public ActionResult Dir_Add(FormCollection collection)
        {
            string dir = (Request.QueryString["id"] ?? "").DesDecrypt();
            if (dir.IsNullOrEmpty() || !Directory.Exists(dir))
            {
                Response.Write("目录为空或不存在!");
                Response.End();
                return null;
            }

            if (collection != null)
            {
                string dirName = Request.Form["DirName"];
                bool isAdd = new YJ.Platform.Files().CreateDirectory(dir, dirName);
                if (isAdd)
                {
                    ViewBag.script = "alert('添加成功!');parent.frames[0].reLoad('" + Request.QueryString["id"] + "');window.location='List" + Request.Url.Query + "';";
                }
                else
                {
                    ViewBag.script = "alert('添加失败!')";
                }
            }

            return View();
        }

        [MyAttribute(CheckApp = false)]
        public ActionResult File_Add()
        {
            return View();
        }

        [MyAttribute(CheckApp = false, CheckUrl = false, CheckLogin = false)]
        public string FileUpload()
        {
            string loginMsg;
            if (!WebMvc.Common.Tools.CheckLogin(out loginMsg) && !YJ.Platform.WeiXin.Organize.CheckLogin())
            {
                return "";
            }
            YJ.Platform.Files bFiles = new YJ.Platform.Files();
            string uploadDir = string.Empty;
            string allowFiles = string.Empty; //允许上传的文件扩展名
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            uploadDir = Request.Form["dir"].DesDecrypt();
            Guid userID = YJ.Platform.Users.CurrentUserID;
            if (userID.IsEmptyGuid())
            {
                return "";
            }
            if (uploadDir.IsNullOrEmpty())
            {
                return "";
            }
            if (Request["REQUEST_METHOD"] == "OPTIONS")
            {
                return "";
            }
            allowFiles = YJ.Utility.Config.UploadFileType;
            string basePath = uploadDir.EndsWith("\\") ? uploadDir : uploadDir + "\\";
            string name;
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            if (!System.IO.Directory.Exists(basePath))
            {
                System.IO.Directory.CreateDirectory(basePath);
            }
            var suffix = files[0].ContentType.Split('/');
            //获取文件格式  
            //var _suffix = suffix[1].Equals("jpeg", StringComparison.CurrentCultureIgnoreCase) ? "" : suffix[1];  
            var _suffix = suffix[1];
            var _temp = System.Web.HttpContext.Current.Request["name"];
            //如果不修改文件名，则创建随机文件名  
            if (!string.IsNullOrEmpty(_temp))
            {
                name = _temp;
            }
            else
            {
                Random rand = new Random(24 * (int)DateTime.Now.Ticks);
                name = rand.Next() + "." + _suffix;
            }
            //文件保存  
            var full = basePath + name;
            //如果文件存在则要重命名
            if (System.IO.File.Exists(full))
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(full);
                string fileExtName = System.IO.Path.GetExtension(full);
                full = basePath + fileName + "(" + YJ.Utility.Tools.GetRandomString() + ")" + fileExtName;
            }
            string extName = System.IO.Path.GetExtension(full).Replace(".", "");
            if (!("," + allowFiles + ",").Contains("," + extName + ",", StringComparison.CurrentCultureIgnoreCase))
            {
                return "{\"jsonrpc\":\"2.0\",\"error\":\"不允许的文件\",\"id\":\"" + name + "\"}";
            }
            files[0].SaveAs(full);
            string id1 = full.Replace1(new YJ.Platform.Files().GetRootPath(), "").DesEncrypt();
            decimal size = decimal.Round((files[0].ContentLength / 1024), 0);
            string fileSize = size == 0 ? files[0].ContentLength > 0 ? "1" : "0" : size.ToFormatString();
            return "{\"jsonrpc\" : \"2.0\", \"result\" : null, \"id\" : \"" + full.DesEncrypt() + "\", \"id1\":\"" + id1 + "\",\"size\":\"" + fileSize + "KB\"}";
        }

        /// <summary>
        /// 查看文件
        /// </summary>
        [MyAttribute(CheckApp = false)]
        public void Show()
        {
            string loginMsg;
            if (!WebMvc.Common.Tools.CheckLogin(out loginMsg) && !YJ.Platform.WeiXin.Organize.CheckLogin())
            {
                Response.End();
                return;
            }

            string file = Request.QueryString["id"].DesDecrypt();
            FileInfo tmpFile = new FileInfo(file);

            if (!tmpFile.Exists)
            {
                Response.Write("未找到要查看的文件!");
                return;
            }

            if (!("," + YJ.Utility.Config.UploadFileType + ",").Contains("," + tmpFile.Extension.Replace(".", "") + ",", StringComparison.CurrentCultureIgnoreCase))
            {
                Response.Write("该文件类型不允许查看!");
                return;
            }

            string fileName = tmpFile.Name;
            if (Request != null && Request.Browser != null && (Request.Browser.Type.StartsWith("IE", StringComparison.CurrentCultureIgnoreCase)
                || Request.Browser.Type.StartsWith("InternetExplorer", StringComparison.CurrentCultureIgnoreCase)))
            {
                fileName = fileName.UrlEncode();
            }
            
            Response.AppendHeader("Server-FileName", fileName);
            string tmpContentType = ",.zip,.rar,.7z,".Contains("," + tmpFile.Extension + ",", StringComparison.CurrentCultureIgnoreCase) ? "" : new YJ.Platform.Files().GetFileContentType(tmpFile.Extension);


            if (string.IsNullOrEmpty(tmpContentType))
            {
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            }
            else
            {
                Response.ContentType = tmpContentType;
                Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
            }

            Response.AddHeader("Content-Length", tmpFile.Length.ToString());
            

            using (var tmpRead = tmpFile.OpenRead())
            {
                var tmpByte = new byte[2048];
                var i = tmpRead.Read(tmpByte, 0, tmpByte.Length);
                while (i > 0)
                {
                    if (Response.IsClientConnected)
                    {
                        Response.OutputStream.Write(tmpByte, 0, i);
                        Response.Flush();
                    }
                    else
                    {
                        break;
                    }
                    i = tmpRead.Read(tmpByte, 0, tmpByte.Length);
                }
            }
            Response.Flush();
            Response.OutputStream.Close();
            Response.Output.Close();
        }

        [MyAttribute(CheckApp = false, CheckUrl = false, CheckLogin = false)]
        public string GetShowString()
        {
            string loginMsg;
            if (!WebMvc.Common.Tools.CheckLogin(out loginMsg) && !YJ.Platform.WeiXin.Organize.CheckLogin())
            {
                return "";
            }
            return YJ.Platform.Files.GetFilesShowString(Request["files"], WebMvc.Common.Tools.BaseUrl + "/Content/Show.ashx");
        }
    }
}
