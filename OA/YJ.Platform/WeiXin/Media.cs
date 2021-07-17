using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace YJ.Platform.WeiXin
{
    /// <summary>
    /// 素材文件类
    /// </summary>
    public class Media
    {
        private string secret = string.Empty;

        public Media()
        {
            this.secret = Config.OrganizeSecret;
        }

        /// <summary>
        /// 带相关应用的ID-企业微信使用
        /// </summary>
        /// <param name="agentId"></param>
        public Media(int agentId)
        {
            this.secret = Config.GetSecret(agentId);
        }

        /// <summary>
        /// 带相关应用的代码-企业微信使用
        /// </summary>
        /// <param name="agentId"></param>
        public Media(string secret)
        {
            this.secret = secret;
        }

        /// <summary>
        /// 得到密钥(默认为管理人员管理应用的secret，旧版本企业号使用)
        /// </summary>
        /// <returns></returns>
        private string GetAccessToken()
        {
            return Config.GetAccessToken(secret);
        }

        /// <summary>
        /// 上传临时素材
        /// </summary>
        /// <param name="file">文件绝对路径</param>
        /// <param name="type">媒体文件类型，分别有图片（image）、语音（voice）、视频（video），普通文件(file)</param>
        /// <returns>media_id</returns>
        public string UploadTemp(string file, string type)
        {
            string url = "https://qyapi.weixin.qq.com/cgi-bin/media/upload?access_token=" + GetAccessToken() + "&type=" + type;
            FileStream fs = new FileStream(file, FileMode.Open);
            long size = fs.Length;
            byte[] array = new byte[size];
            fs.Read(array, 0, array.Length);
            fs.Close();
            string returnString = Utility.HttpHelper.SendFile(url, file, array);
            LitJson.JsonData jd = LitJson.JsonMapper.ToObject(returnString);
            if (jd.ContainsKey("media_id"))
            {
                string media_id = jd["media_id"].ToString();
                if (media_id.IsNullOrEmpty())
                {
                    Platform.Log.Add("调用了微信上传临时素材错误-" + file, "返回：" + returnString, Log.Types.微信企业号, url);
                }
                return media_id;
            }
            else
            {
                Platform.Log.Add("调用了微信上传临时素材错误-" + file, "返回：" + returnString, Log.Types.微信企业号, url);
                return "";
            }
        }

        /// <summary>
        /// 上传临时素材
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="type">媒体文件类型，分别有图片（image）、语音（voice）、视频（video），普通文件(file)</param>
        /// <returns>media_id</returns>
        public string UploadTemp(HttpPostedFile postedFile, string type)
        {
            string url = "https://qyapi.weixin.qq.com/cgi-bin/media/upload?access_token=" + GetAccessToken() + "&type=" + type;
            string returnString = Utility.HttpHelper.SendFile(url, postedFile);
            LitJson.JsonData jd = LitJson.JsonMapper.ToObject(returnString);
            if (jd.ContainsKey("media_id"))
            {
                string media_id = jd["media_id"].ToString();
                if (media_id.IsNullOrEmpty())
                {
                    Platform.Log.Add("调用了微信上传临时素材错误-" + postedFile.FileName, "返回：" + returnString, Log.Types.微信企业号, url);
                }
                return media_id;
            }
            else
            {
                Platform.Log.Add("调用了微信上传临时素材错误-" + postedFile.FileName, "返回：" + returnString, Log.Types.微信企业号, url);
                return "";
            }
        }

        /// <summary>
        /// 获取临时素材
        /// </summary>
        /// <param name="media_id">素材ID</param>
        /// <param name="savePath">保存路径</param>
        /// <returns></returns>
        public string DownladTemp(string media_id, string savePath)
        {
            string url = "https://qyapi.weixin.qq.com/cgi-bin/media/get?access_token=" + GetAccessToken() + "&media_id=" + media_id;
            return "";
        }

        /// <summary>
        /// 上传永久素材(图片，文件等)
        /// </summary>
        /// <param name="file">文件绝对路径</param>
        /// <param name="type">媒体文件类型，分别有图片（image）、语音（voice）、视频（video），普通文件(file)</param>
        /// <returns>media_id</returns>
        public string UploadFormal(string file, string type)
        {
            string url = "https://qyapi.weixin.qq.com/cgi-bin/material/add_material?access_token=" + GetAccessToken() + "&type=" + type;
            FileStream fs = new FileStream(file, FileMode.Open);
            long size = fs.Length;
            byte[] array = new byte[size];
            fs.Read(array, 0, array.Length);
            fs.Close();
            string returnString = Utility.HttpHelper.SendFile(url, file, array);
            LitJson.JsonData jd = LitJson.JsonMapper.ToObject(returnString);
            if (jd.ContainsKey("errcode") && "0" == jd["errcode"].ToString() && jd.ContainsKey("media_id"))
            {
                string media_id = jd["media_id"].ToString();
                if (media_id.IsNullOrEmpty())
                {
                    Platform.Log.Add("调用了微信上传永久素材错误-" + file, "返回：" + returnString, Log.Types.微信企业号, url);
                }
                return media_id;
            }
            else
            {
                Platform.Log.Add("调用了微信上传永久素材错误-" + file, "返回：" + returnString, Log.Types.微信企业号, url);
                return "";
            }
        }
        /// <summary>
        /// 上传永久图文素材
        /// </summary>
        /// <param name="title">图文消息的标题</param>
        /// <param name="thumb_media_id">图文消息缩略图的media_id, 可以直接传图片绝对路径(方法内上传获得media_id)</param>
        /// <param name="content">图文消息的内容，支持html标签</param>
        /// <param name="author">图文消息的作者</param>
        /// <param name="content_source_url">图文消息点击“阅读原文”之后的页面链接</param>
        /// <param name="digest">图文消息的描述</param>
        /// <param name="show_cover_pic">是否显示封面，1为显示，0为不显示。默认为0</param>
        /// <param name="edit_media_id">为空是添加，不为空是修改</param>
        /// <returns>media_id</returns>
        public string UploadMPNews(string title, string thumb_media_id, string content, string author = "", string content_source_url = "", string digest = "", int show_cover_pic = 0, string edit_media_id = "")
        {
            #region 请求包结构体为
            //{
            //    "mpnews":{
            //           "articles":[
            //            {
            //               "title": "Title01",
            //               "thumb_media_id": "2-G6nrLmr5EC3MMb_-zK1dDdzmd0p7cNliYu9V5w7o8K0",
            //               "author": "zs",
            //               "content_source_url": "",
            //               "content": "Content001",
            //               "digest": "airticle01",
            //               "show_cover_pic": "0"
            //              }
            //            ]
            //    }
            //}
            #endregion
            string media_id = string.Empty;
            if (File.Exists(thumb_media_id))
            {
                thumb_media_id = UploadFormal(thumb_media_id, "image");
                if (thumb_media_id.IsNullOrEmpty())
                {
                    return media_id;
                }
            }
            bool isEdit = !edit_media_id.IsNullOrEmpty();//是否为修改
            string url = isEdit ? "https://qyapi.weixin.qq.com/cgi-bin/material/update_mpnews?access_token=" + GetAccessToken() 
                : "https://qyapi.weixin.qq.com/cgi-bin/material/add_mpnews?access_token=" + GetAccessToken();
            LitJson.JsonData jd = new LitJson.JsonData();
            LitJson.JsonData mpnews = new LitJson.JsonData();
            LitJson.JsonData articles = new LitJson.JsonData();
            LitJson.JsonData article = new LitJson.JsonData();
            if (isEdit)
            {
                jd["media_id"] = edit_media_id;
            }
            article["title"] = title;
            article["thumb_media_id"] = thumb_media_id;
            article["content_source_url"] = content_source_url;
            article["content"] = content;
            article["digest"] = digest;
            article["show_cover_pic"] = show_cover_pic;
            articles.Add(article);
            mpnews["articles"] = articles;
            jd["mpnews"] = mpnews;

            string json = jd.ToJson();
            string rstr = Utility.HttpHelper.SendPost(url, json);
            LitJson.JsonData rjson = LitJson.JsonMapper.ToObject(rstr);
            if (rjson.ContainsKey("errcode") && "0" == rjson["errcode"].ToString() && rjson.ContainsKey("media_id"))
            {
                media_id = rjson["media_id"].ToString();
                Platform.Log.Add("调用了微信上传永久图文素材-" + title, "返回：" + rstr, Log.Types.微信企业号, json);
            }
            else
            {
                Platform.Log.Add("调用了微信上传永久图文素材错误-" + title, "返回：" + rstr, Log.Types.微信企业号, json);
            }
            return media_id;
        }
        /// <summary>
        /// 删除永久图文素材
        /// </summary>
        /// <param name="media_id"></param>
        /// <returns></returns>
        public bool DeleteMPNews(string media_id)
        {
            string url = "https://qyapi.weixin.qq.com/cgi-bin/material/del?access_token=" + GetAccessToken() + "&media_id=" + media_id;
            string rstr = Utility.HttpHelper.SendGet(url);
            LitJson.JsonData json = LitJson.JsonMapper.ToObject(rstr);
            Platform.Log.Add("调用了微信删除永久图文素材-" + media_id, "返回：" + rstr, Log.Types.微信企业号);
            if (json.ContainsKey("errcode") && "0" == json["errcode"].ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 上传图文消息内的图片
        /// </summary>
        /// <param name="file">图片地址，绝对路径</param>
        /// <returns>返回图片url</returns>
        public string UploadImg(string file)
        {
            string url = "https://qyapi.weixin.qq.com/cgi-bin/media/uploadimg?access_token=" + GetAccessToken();
            FileStream fs = new FileStream(file, FileMode.Open);
            long size = fs.Length;
            byte[] array = new byte[size];
            fs.Read(array, 0, array.Length);
            fs.Close();

            string returnString = Utility.HttpHelper.SendFile(url, file, array);
            LitJson.JsonData jd = LitJson.JsonMapper.ToObject(returnString);
            if (jd.ContainsKey("url"))
            {
                return jd["url"].ToString();
            }
            else
            {
                Platform.Log.Add("调用了微信上传图文消息内的图片错误-" + file, "返回：" + returnString, Log.Types.微信企业号, url);
                return "";
            }
        }
        /// <summary>
        /// 获取永久素材
        /// </summary>
        /// <param name="media_id"></param>
        /// <returns></returns>
        public string Download(string media_id)
        {
            string url = "https://qyapi.weixin.qq.com/cgi-bin/material/get?access_token=" + GetAccessToken() + "&media_id=" + media_id;
            string rstr = Utility.HttpHelper.DownloadFile(url,"D:\\sdfsdf.mp4");
            Platform.Log.Add("调用了微信下载素材", "返回：" + rstr, Log.Types.微信企业号, url);
            return rstr;
        }

    }
}
