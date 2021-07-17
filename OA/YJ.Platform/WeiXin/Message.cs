using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace YJ.Platform.WeiXin
{
    /// <summary>
    /// 微信消息类
    /// </summary>
    public class Message
    {
        delegate string delegate_send(string data);
        private string secret = string.Empty;
        private int agentId = -1;
        public Message()
        {
            this.secret = Config.OrganizeSecret;
        }

        /// <summary>
        /// 带相关应用的ID-企业微信使用
        /// </summary>
        /// <param name="agentId"></param>
        public Message(int agentId)
        {
            this.secret = Config.GetSecret(agentId);
        }

        /// <summary>
        /// 带相关应用的代码-企业微信使用
        /// </summary>
        /// <param name="agentId"></param>
        public Message(string agentCode)
        {
            this.secret = Config.GetSecret(agentCode);
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
        /// 发送消息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string send(string data)
        {
            if (this.agentId != -1 && this.secret.IsNullOrEmpty())
            {
                this.secret = Config.GetSecret(agentId);
            }
            string url = "https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token=" + GetAccessToken();
            string rstr = Utility.HttpHelper.SendPost(url, data);
            LitJson.JsonData json = LitJson.JsonMapper.ToObject(rstr);
            if (json.ContainsKey("errcode") && "0" == json["errcode"].ToString())
            {
                Platform.Log.Add("调用了微信发送消息", "返回：" + rstr, Log.Types.微信企业号, url, data);
                return "";
            }
            else
            {
                Platform.Log.Add("调用了微信发送消息错误", "返回：" + rstr, Log.Types.微信企业号, url, data);
                return rstr;
            }
        }

        /// <summary>
        /// 发送消息（异步方式）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private void sendAsync(string data)
        {
            delegate_send dsend = new delegate_send(send);
            dsend.BeginInvoke(data, null, null);
        }

        /// <summary>
        /// 发送text消息
        /// </summary>
        /// <param name="contents">消息内容</param>
        /// <param name="userids">消息接收人员账号,多个有|线分开</param>
        /// <param name="depts">消息接收部门id,多个有|线分开</param>
        /// <param name="groups">消息接收组id,多个有|线分开</param>
        /// <param name="saf">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="agentid">应用ID</param>
        /// <param name="async">是否异步方式</param>
        /// <returns></returns>
        public string SendText(string contents, string userids = "", string depts = "", string groups = "", int safe = 0, int agentid = 0, bool async = false)
        {
            #region 数据格式
            //{
            //   "touser": "UserID1|UserID2|UserID3",
            //   "toparty": " PartyID1 | PartyID2 ",
            //   "totag": " TagID1 | TagID2 ",
            //   "msgtype": "text",
            //   "agentid": 1, 企业应用的id，整型。可在应用的设置页面查看
            //   "text": {
            //       "content": "Holiday Request For Pony(http://xxxxx)"
            //   },
            //   "safe":0
            //}
            #endregion
            LitJson.JsonData json = new LitJson.JsonData();
            json["touser"] = userids;
            json["toparty"] = depts;
            json["totag"] = groups;
            json["msgtype"] = "text";
            json["agentid"] = agentid;
            LitJson.JsonData text = new LitJson.JsonData();
            text["content"] = contents;
            json["text"] = text;
            json["safe"] = safe;
            this.agentId = agentid;
            string jsonstr = json.ToJson(false);
            if (async)
            {
                sendAsync(jsonstr);
                return "";
            }
            else
            {
                return send(jsonstr);
            }
        }
        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="media_idOrImgPath">图片素材ID或者图片绝对路径</param>
        /// <param name="userids">接收人员账号，多个用|分开</param>
        /// <param name="depts">接收部门</param>
        /// <param name="groups">接收标签</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="agentid">应用ID</param>
        /// <param name="async">是否异步方式</param>
        /// <returns></returns>
        public string SendImg(string media_idOrImgPath, string userids = "", string depts = "", string groups = "", int safe = 0, int agentid = 0, bool async = false)
        {
            #region 数据格式
            //{
            //   "touser": "UserID1|UserID2|UserID3",
            //   "toparty": " PartyID1 | PartyID2 ",
            //   "totag": " TagID1 | TagID2 ",
            //   "msgtype": "image",
            //   "agentid": 1,
            //   "image": {
            //       "media_id": "MEDIA_ID"
            //   },
            //   "safe":0
            //}
            #endregion
            string media_id = string.Empty;
            if (File.Exists(media_idOrImgPath))
            {
                media_id = new Media(agentid).UploadFormal(media_idOrImgPath, "image");
            }
            if (media_id.IsNullOrEmpty())
            {
                return "上传图片错误";
            }
            LitJson.JsonData json = new LitJson.JsonData();
            json["touser"] = userids;
            json["toparty"] = depts;
            json["totag"] = groups;
            json["msgtype"] = "image";
            json["agentid"] = agentid;
            LitJson.JsonData image = new LitJson.JsonData();
            image["media_id"] = media_id;
            json["image"] = image;
            json["safe"] = safe;
            this.agentId = agentid;
            string jsonstr = json.ToJson(false);
            if (async)
            {
                sendAsync(jsonstr);
                return "";
            }
            else
            {
                return send(jsonstr);
            }
        }

        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="media_idOrVoicePath">语音素材ID或者语音文件绝对路径</param>
        /// <param name="userids">接收人员账号，多个用|分开</param>
        /// <param name="depts">接收部门</param>
        /// <param name="groups">接收标签</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="agentid">应用ID</param>
        /// <param name="async">是否异步方式</param>
        /// <returns></returns>
        public string SendVoice(string media_idOrVoicePath, string userids = "", string depts = "", string groups = "", int safe = 0, int agentid = 0, bool async = false)
        {
            string media_id = string.Empty;
            if (File.Exists(media_idOrVoicePath))
            {
                media_id = new Media(agentid).UploadFormal(media_idOrVoicePath, "voice");
            }
            if (media_id.IsNullOrEmpty())
            {
                return "上传语音错误";
            }
            LitJson.JsonData json = new LitJson.JsonData();
            json["touser"] = userids;
            json["toparty"] = depts;
            json["totag"] = groups;
            json["msgtype"] = "voice";
            json["agentid"] = agentid;
            LitJson.JsonData voice = new LitJson.JsonData();
            voice["media_id"] = media_id;
            json["voice"] = voice;
            json["safe"] = safe;
            this.agentId = agentid;
            string jsonstr = json.ToJson(false);
            if (async)
            {
                sendAsync(jsonstr);
                return "";
            }
            else
            {
                return send(jsonstr);
            }
        }

        /// <summary>
        /// 发送视频消息
        /// </summary>
        /// <param name="media_idOrVideoPath">视频素材ID或者视频文件绝对路径</param>
        /// <param name="userids">接收人员账号，多个用|分开</param>
        /// <param name="depts">接收部门</param>
        /// <param name="groups">接收标签</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="agentid">应用ID</param>
        /// <param name="title">视频标题</param>
        /// <param name="description">视频描述</param>
        /// <param name="async">是否异步方式</param>
        /// <returns></returns>
        public string SendVoice(string media_idOrVideoPath, string userids = "", string depts = "", string groups = "", int safe = 0, int agentid = 0, string title = "", string description = "", bool async = false)
        {
            string media_id = string.Empty;
            if (File.Exists(media_idOrVideoPath))
            {
                media_id = new Media(agentid).UploadFormal(media_idOrVideoPath, "video");
            }
            if (media_id.IsNullOrEmpty())
            {
                return "上传视频错误";
            }
            LitJson.JsonData json = new LitJson.JsonData();
            json["touser"] = userids;
            json["toparty"] = depts;
            json["totag"] = groups;
            json["msgtype"] = "video";
            json["agentid"] = agentid;
            LitJson.JsonData video = new LitJson.JsonData();
            video["media_id"] = media_id;
            video["title"] = title;
            video["description"] = description;
            json["video"] = video;
            json["safe"] = safe;
            this.agentId = agentid;
            string jsonstr = json.ToJson(false);
            if (async)
            {
                sendAsync(jsonstr);
                return "";
            }
            else
            {
                return send(jsonstr);
            }
        }

        /// <summary>
        /// 发送文件消息
        /// </summary>
        /// <param name="media_idOrFilePath">文件素材ID或者文件绝对路径</param>
        /// <param name="userids">接收人员账号，多个用|分开</param>
        /// <param name="depts">接收部门</param>
        /// <param name="groups">接收标签</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="agentid">应用ID</param>
        /// <param name="async">是否异步方式</param>
        /// <returns></returns>
        public string SendFile(string media_idOrFilePath, string userids = "", string depts = "", string groups = "", int safe = 0, int agentid = 0, bool async = false)
        {
            string media_id = string.Empty;
            if (File.Exists(media_idOrFilePath))
            {
                media_id = new Media(agentid).UploadFormal(media_idOrFilePath, "file");
            }
            if (media_id.IsNullOrEmpty())
            {
                return "上传文件错误";
            }
            LitJson.JsonData json = new LitJson.JsonData();
            json["touser"] = userids;
            json["toparty"] = depts;
            json["totag"] = groups;
            json["msgtype"] = "file";
            json["agentid"] = agentid;
            LitJson.JsonData file = new LitJson.JsonData();
            file["media_id"] = media_id;
            json["file"] = file;
            json["safe"] = safe;
            this.agentId = agentid;
            string jsonstr = json.ToJson(false);
            if (async)
            {
                sendAsync(jsonstr);
                return "";
            }
            else
            {
                return send(jsonstr);
            }
        }

        /// <summary>
        /// 发送News消息
        /// </summary>
        /// <param name="articleList">消息列表Tuple title,description,url,picurl</param>
        /// <param name="userids">接收人员账号，多个用|分开</param>
        /// <param name="depts">接收部门</param>
        /// <param name="groups">接收标签</param>
        /// <param name="agentid">应用ID</param>
        /// <param name="async">是否异步方式</param>
        /// <returns></returns>
        public string SendNews(List<Tuple<string, string, string, string>> articleList, string userids = "", string depts = "", string groups = "", int agentid = 0, bool async = false)
        {
            #region 数据格式
            //{
            //   "touser": "UserID1|UserID2|UserID3",
            //   "toparty": " PartyID1 | PartyID2 ",
            //   "totag": " TagID1 | TagID2 ",
            //   "msgtype": "news",
            //   "agentid": 1,
            //   "news": {
            //       "articles":[
            //           {
            //               "title": "Title",
            //               "description": "Description",
            //               "url": "URL",
            //               "picurl": "PIC_URL"
            //           },
            //           {
            //               "title": "Title",
            //               "description": "Description",
            //               "url": "URL",
            //               "picurl": "PIC_URL"
            //           }    
            //       ]
            //   }
            //}
            #endregion
            
            LitJson.JsonData json = new LitJson.JsonData();
            json["touser"] = userids;
            json["toparty"] = depts;
            json["totag"] = groups;
            json["msgtype"] = "news";
            json["agentid"] = agentid;
            LitJson.JsonData news = new LitJson.JsonData();
            LitJson.JsonData articles = new LitJson.JsonData();
            foreach (var article in articleList)
            {
                LitJson.JsonData articleJSON = new LitJson.JsonData();
                articleJSON["title"] = article.Item1;
                articleJSON["description"] = article.Item2;
                articleJSON["url"] = article.Item3;
                string picurl = article.Item4;
                //if (File.Exists(article.Item4))
                //{
                //    picurl = new Media(agentid).UploadImg(article.Item4);
                //}
                if (picurl.IsNullOrEmpty())
                {
                    continue;
                }
                articleJSON["picurl"] = picurl;
                articles.Add(articleJSON);
            }
            news["articles"] = articles;
            json["news"] = news;
            this.agentId = agentid;
            string jsonstr = json.ToJson(false);
            if (async)
            {
                sendAsync(jsonstr);
                return "";
            }
            else
            {
                return send(jsonstr);
            }
        }

        /// <summary>
        /// 发送mpnews消息
        /// </summary>
        /// <param name="articleList">消息列表Tuple 图文消息的标题,图文消息缩略图绝对路径,作者,点击“阅读原文”之后的页面链接,消息的内容,图文消息的描述,是否显示封面，1为显示，0为不显示</param>
        /// <param name="userids">接收人员账号，多个用|分开</param>
        /// <param name="depts">接收部门</param>
        /// <param name="groups">接收标签</param>
        /// <param name="agentid">应用ID</param>
        /// <param name="async">是否异步方式</param>
        /// <returns></returns>
        public string SendMPNews(List<Tuple<string, string, string, string, string, string, string>> articleList, string userids = "", string depts = "", string groups = "", int agentid = 0, bool async = false)
        {
            #region 数据格式
            //{
            //   "touser": "UserID1|UserID2|UserID3",
            //   "toparty": " PartyID1 | PartyID2 ",
            //   "totag": " TagID1 | TagID2 ",
            //   "msgtype": "mpnews",
            //   "agentid": 1,
            //   "mpnews": {
            //       "articles":[
            //           {
            //               "title": "Title",
            //               "thumb_media_id": "id",
            //               "author": "Author",
            //               "content_source_url": "URL",
            //               "content": "Content",
            //               "digest": "Digest description",
            //               "show_cover_pic": "0"
            //           },
            //           {
            //               "title": "Title",
            //               "thumb_media_id": "id",
            //               "author": "Author",
            //               "content_source_url": "URL",
            //               "content": "Content",
            //               "digest": "Digest description",
            //               "show_cover_pic": "0"
            //           }
            //       ]
            //   },
            //   "safe":0
            //}
            #endregion
            LitJson.JsonData json = new LitJson.JsonData();
            json["touser"] = userids;
            json["toparty"] = depts;
            json["totag"] = groups;
            json["msgtype"] = "mpnews";
            json["agentid"] = agentid;
            LitJson.JsonData mpnews = new LitJson.JsonData();
            LitJson.JsonData articles = new LitJson.JsonData();
            foreach (var article in articleList)
            {
                LitJson.JsonData articleJSON = new LitJson.JsonData();
                articleJSON["title"] = article.Item1;
                string media_id = article.Item2;
                if (File.Exists(article.Item2))
                {
                    media_id = new Media(agentid).UploadFormal(article.Item2, "image");
                }
                articleJSON["thumb_media_id"] = media_id;
                articleJSON["author"] = article.Item3;
                articleJSON["content_source_url"] = article.Item4;
                articleJSON["content"] = article.Item5;
                articleJSON["digest"] = article.Item6;
                articleJSON["show_cover_pic"] = article.Item7;
                articles.Add(articleJSON);
            }
            mpnews["articles"] = articles;
            json["mpnews"] = mpnews;
            this.agentId = agentid;
            string jsonstr = json.ToJson(false);
            if (async)
            {
                sendAsync(jsonstr);
                return "";
            }
            else
            {
                return send(jsonstr);
            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public void Receive(string xml)
        {
            //Log.Add("收到消息", xml, Log.Types.微信企业号);

            XDocument xdoc = XDocument.Parse(xml);
            XElement root = xdoc.Root;
            string ToUserName = root.Element("ToUserName").Value;
            string FromUserName = root.Element("FromUserName").Value;
            string CreateTime = root.Element("CreateTime").Value;
            string MsgType = root.Element("MsgType").Value;
            string MsgId = root.Element("MsgId").Value;
            string AgentID = root.Element("AgentID").Value;

            WeiXinMessage bwxm = new WeiXinMessage();
            Data.Model.WeiXinMessage wxm = new Data.Model.WeiXinMessage();
            wxm.ID = Guid.NewGuid();
            wxm.AddTime = Utility.DateTimeNew.Now;
            wxm.AgentID = AgentID.ToInt();
            wxm.CreateTime = CreateTime.ToInt();
            wxm.CreateTime1 = Utility.Tools.JavaLongToDateTime(wxm.CreateTime);
            wxm.MsgType = MsgType;
            wxm.MsgId = MsgId.ToLong();
            wxm.ToUserName = ToUserName;
            wxm.FromUserName = FromUserName;
            switch (MsgType)
            { 
                case "text":
                    wxm.Contents = root.Element("Content").Value;
                    break;
                case "image":
                    wxm.PicUrl = root.Element("PicUrl").Value;
                    wxm.MediaId = root.Element("MediaId").Value;
                    break;
                case "voice":
                    wxm.Format = root.Element("Format").Value;
                    wxm.MediaId = root.Element("MediaId").Value;
                    break;
                case "video":
                    wxm.ThumbMediaId = root.Element("ThumbMediaId").Value;
                    wxm.MediaId = root.Element("MediaId").Value;
                    break;
                case "shortvideo":
                    wxm.ThumbMediaId = root.Element("ThumbMediaId").Value;
                    wxm.MediaId = root.Element("MediaId").Value;
                    break;
                case "location":
                    wxm.Location_X = root.Element("Location_X").Value;
                    wxm.Location_Y = root.Element("Location_Y").Value;
                    wxm.Scale = root.Element("Scale").Value;
                    wxm.Label = root.Element("Label").Value;
                    break;
                case "link":
                    wxm.Title = root.Element("Title").Value;
                    wxm.Description = root.Element("Description").Value;
                    wxm.PicUrl = root.Element("PicUrl").Value;
                    break;
            }

            bwxm.Add(wxm);
        }


    }
}
