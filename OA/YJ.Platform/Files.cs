using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace YJ.Platform
{
    public class Files
    {
        /// <summary>
        /// 文件目录
        /// </summary>
        public static string FilePath = Utility.Config.FilePath;
        /// <summary>
        /// 得到一个用户的文件目录JSON
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="path">刷新路径</param>
        /// <param name="isFirst">是否第一次加载，第一次加载要加载根目录</param>
        /// <returns></returns>
        public string GetUserDirectoryJson(Guid userID, string path = "", bool isFirst = false)
        {
            path = path.IsNullOrEmpty() ? GetUserRootPath(userID) : path;
            var dirs = Directory.GetDirectories(path);
            LitJson.JsonData jd = new LitJson.JsonData();
            
            if (isFirst)
            {
                jd["id"] = path.DesEncrypt();
                jd["parentID"] = "0";
                jd["title"] = "我的文件";
                jd["type"] = 0;
                jd["ico"] = "";
                jd["hasChilds"] = dirs.Length > 0 ? "1" : "0";
                if (dirs.Length > 0)
                {
                    jd["childs"] = new LitJson.JsonData();
                }
            }
            foreach (var dir in dirs)
            {
                bool hasChilds = Directory.GetDirectories(dir).Length > 0;
                string dirName = Path.GetFileName(dir); 
                LitJson.JsonData jd1 = new LitJson.JsonData();
                jd1["id"] = dir.DesEncrypt();
                jd1["parentID"] = path.DesEncrypt();
                jd1["title"] = dirName;
                jd1["type"] = 1;
                jd1["hasChilds"] = hasChilds ? "1" : "0";
                jd1["ico"] = hasChilds ? "" : "fa-folder";
                //jd1["childs"] = new LitJson.JsonData();
                if (isFirst)
                {
                    jd["childs"].Add(jd1);
                }
                else
                { 
                    jd.Add(jd1);
                }
            }
            return jd.ToJson();
        }

        /// <summary>
        /// 得到一个目录下的子目录和文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<Data.Model.Files> GetList(string path)
        {
            List<Data.Model.Files> list = new List<Data.Model.Files>();
            if (!Directory.Exists(path))
            {
                return list;
            }
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (var dir in di.GetDirectories())
            {
                
                Data.Model.Files fi = new Data.Model.Files();
                fi.CreateTime = dir.CreationTime;
                fi.FileLength = dir.GetFiles().Length;
                fi.FullName = dir.FullName;
                fi.ModifyTime = dir.LastWriteTime;
                fi.Name = dir.Name;
                fi.Type = 0;
                list.Add(fi);
            }
            foreach (var file in di.GetFiles())
            {
               
                Data.Model.Files fi = new Data.Model.Files();
                fi.CreateTime = file.CreationTime;
                fi.Length = file.Length;
                fi.ModifyTime = file.LastWriteTime;
                fi.Name = file.Name;
                fi.FullName = file.FullName;
                fi.Type = 1;
                list.Add(fi);
            }
            
            return list;
        }

        /// <summary>
        /// 得到文件根目录
        /// </summary>
        /// <returns></returns>
        public string GetRootPath()
        {
            return FilePath;
        }

        /// <summary>
        /// 得到用户根目录
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetUserRootPath(Guid userID)
        {
            string path = FilePath + "UserFiles\\" + userID.ToString();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        /// <summary>
        /// 得到上传的文件的目录
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetUploadPath()
        { 
            DateTime dt = Utility.DateTimeNew.Now;
            string path = FilePath + "UploadFiles\\" + dt.Year + "\\" + dt.Month + "\\" + dt.Day;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dirName"></param>
        /// <returns></returns>
        public bool CreateDirectory(string path, string dirName)
        {
            if (path.IsNullOrEmpty() || dirName.IsNullOrEmpty())
            {
                return false;
            }
            string path1 = Path.Combine(path, dirName);
            if (!Directory.Exists(path1))
            {
                var dir = Directory.CreateDirectory(path1);
                return dir != null;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 删除目录或文件
        /// </summary>
        /// <param name="pathOrFile"></param>
        /// <returns></returns>
        public string Delete(string pathOrFile)
        {
            StringBuilder sb = new StringBuilder();
            FileInfo fi = new FileInfo(pathOrFile);
            if (fi.Exists)
            {
                try
                {
                    fi.Delete();
                }
                catch(IOException err)
                {
                    sb.Append(fi.Name + "删除失败;");
                    Log.Add(err);
                }
            }
            DirectoryInfo di = new DirectoryInfo(pathOrFile);
            if (di.Exists)
            {
                try
                {
                    di.Delete(true);
                }
                catch (IOException err)
                {
                    sb.Append(di.Name + "删除失败;");
                    Log.Add(err);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 得到显示文件的ContentType
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        public string GetFileContentType(string fileExtension)
        {
            if (fileExtension.IsNullOrEmpty())
            {
                return "";
            }
            string cacheKey = Utility.Keys.CacheKeys.FileContentType.ToString();
            var obj = Cache.IO.Opation.Get(cacheKey);
            List<Tuple<string, string>> list = new List<Tuple<string, string>>();
            if (obj != null)
            {
                list = (List<Tuple<string, string>>)obj;
            }
            var li = list.Find(p => p.Item1.Equals(fileExtension, StringComparison.CurrentCultureIgnoreCase));
            if (li != null)
            {
                return li.Item2;
            }
            try
            {
                string tmpContentType = Microsoft.Win32.Registry.GetValue(@"HKEY_CLASSES_ROOT\" + fileExtension, "Content Type", string.Empty).ToString();
                if (!tmpContentType.IsNullOrEmpty())
                {
                    list.Add(new Tuple<string, string>(fileExtension, tmpContentType));
                    Cache.IO.Opation.Set(cacheKey, list);
                    return tmpContentType;
                }
            }
            catch { }
            return "";
        }

        /// <summary>
        /// 得到文件显示字符串
        /// </summary>
        /// <param name="files"></param>
        /// <param name="showUrl">显示地址 WebForm为 /Platform/Files/Show.ashx MVC为 /Files/Show</param>
        /// <param name="showImg">是否将图片直接显示</param>
        /// <param name="newRow">是否换行</param>
        /// <returns></returns>
        public static string GetFilesShowString(string files, string showUrl = "", bool showImg = false, bool newRow = true)
        {
            if (files.IsNullOrEmpty())
            {
                return "";
            }
            if (showUrl.IsNullOrEmpty())
            {
                if (System.Web.HttpContext.Current.Request.Url != null
                            && (System.Web.HttpContext.Current.Request.Url.AbsolutePath.EndsWith(".aspx", StringComparison.CurrentCultureIgnoreCase)
                             || (System.Web.HttpContext.Current.Request.Url.AbsolutePath.EndsWith(".ashx", StringComparison.CurrentCultureIgnoreCase))))
                {
                    showUrl = Utility.Config.BaseUrl + "/Platform/Files/Show.ashx";
                }
                else
                {
                    showUrl = Utility.Config.BaseUrl + "/Content/Show.ashx";
                }
            }
            if (showUrl.IsNullOrEmpty())
            {
                return "";
            }
            string[] filesArray = files.Split('|');
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (string file in filesArray)
            {
                string path = FilePath + file.DesDecrypt();
                if (!File.Exists(path))
                {
                    continue;
                }
                string style = " style=\"vertical-align:middle;";
                //style += (i == 0 || i == filesArray.Length - 1) ? "" : "margin:8px 0;";
                if (!newRow)
                {
                    style += "float:left;margin:8px 8px 0 0;";
                }
                else
                {
                    style += "margin:8px 0;";
                }
                style += "\"";
                if (showImg && IsImage(path))
                {
                    sb.AppendFormat("<div{0}><img src=\"{1}\" style=\"border:none 0;\"/></div>",
                        style, showUrl + "?id=" + path.DesEncrypt()
                        );
                }
                else
                {
                    sb.AppendFormat("<div{0}><a target=\"_blank\" href=\"{1}\" class=\"blue1\">{2}</a></div>",
                        style, showUrl + "?id=" + path.DesEncrypt(), (++i).ToString() + "、" + System.IO.Path.GetFileName(path)
                        );
                }
            }
            sb.Append("<div style=\"clear:both;\"></div>");
            return sb.ToString();
        }

        /// <summary>
        /// 判断文件是否为图片文件
        /// </summary>
        /// <param name="fileExtName"></param>
        public static bool IsImage(string file)
        {
            string extName=Path.GetExtension(file);
            if(extName.IsNullOrEmpty())
            {
                return false;
            }
            return extName.ToLower()==".jpg" ||extName.ToLower()==".jpeg"||extName.ToLower()==".png"||extName.ToLower()==".gif"||extName.ToLower()==".bmp";
        }
    }
}
