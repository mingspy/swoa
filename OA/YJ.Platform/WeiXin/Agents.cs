using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YJ.Platform.WeiXin
{
    /// <summary>
    /// 微信应用设置类
    /// </summary>
    public class Agents
    {
        private string cacheKey = Utility.Keys.CacheKeys.WeiXinAgents.ToString();
        /// <summary>
        /// 得到密钥
        /// </summary>
        /// <returns></returns>
        private string GetAccessToken()
        {
            return Config.GetAccessToken(Config.OrganizeSecret);
        }

        /// <summary>
        /// 获取企业号应用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Data.Model.WeiXinAgent GetAgentInfo(int id)
        {
            #region 数据示例
            //{
            //   "errcode":"0",
            //   "errmsg":"ok" ,
            //   "agentid":"1" ,
            //   "name":"NAME" ,
            //   "square_logo_url":"xxxxxxxx企业应用方形头像" ,
            //   "round_logo_url":"yyyyyyyy企业应用圆形头像" ,
            //   "description":"企业应用详情" ,
            //   "allow_userinfos":{企业应用可见范围（人员），其中包括userid和关注状态state
            //       "user":[
            //             {
            //                 "userid":"id1",
            //                 "status":"1"
            //             },
            //             {
            //                 "userid":"id2",
            //                 "status":"1"
            //             },
            //             {
            //                 "userid":"id3",
            //                 "status":"1"
            //             }
            //              ]
            //    },
            //   "allow_partys":{企业应用可见范围（部门）
            //       "partyid": [1]
            //    },
            //   "allow_tags":{企业应用可见范围（标签）
            //       "tagid": [1,2,3]
            //    }
            //   "close":0 ,企业应用是否被禁用
            //   "redirect_domain":"www.qq.com",企业应用可信域名
            //   "report_location_flag":0,企业应用是否打开地理位置上报 0：不上报；1：进入会话上报；2：持续上报
            //   "isreportuser":0,是否接收用户变更通知。0：不接收；1：接收
            //   "isreportenter":0,是否上报用户进入应用事件。0：不接收；1：接收
            //   "chat_extension_url":"http://www.qq.com",关联会话url
            //   "type":1应用类型。1：消息型；2：主页型
            //}
            #endregion
            string url = "https://qyapi.weixin.qq.com/cgi-bin/agent/get?access_token=" + GetAccessToken() + "&agentid=" + id.ToString();
            string returnString = Utility.HttpHelper.SendGet(url);
            LitJson.JsonData jd = LitJson.JsonMapper.ToObject(returnString);
            if (jd.ContainsKey("errcode") && "0" != jd["errcode"].ToString())
            {
                Platform.Log.Add("微信获取应用详情-失败", returnString, Log.Types.微信企业号);
                return null;
            }
            Data.Model.WeiXinAgent agent = new Data.Model.WeiXinAgent();
            agent.agentid = jd["agentid"].ToString().ToInt();
            agent.name = jd["name"].ToString();
            agent.square_logo_url = jd["square_logo_url"].ToString();
            agent.round_logo_url = jd["round_logo_url"].ToString();
            agent.description = jd["description"].ToString();
            agent.close = jd["close"].ToString().ToInt();
            agent.redirect_domain = jd["redirect_domain"].ToString();
            agent.report_location_flag = jd["report_location_flag"].ToString().ToInt();
            agent.isreportuser = jd["isreportuser"].ToString().ToInt();
            agent.isreportenter = jd["isreportenter"].ToString().ToInt();
            agent.chat_extension_url = jd["chat_extension_url"].ToString();
            agent.type = jd["type"].ToString().ToInt();
            if (jd["allow_tags"].ContainsKey("tagid") && jd["allow_tags"]["tagid"].IsArray)
            {
                StringBuilder tagsb = new StringBuilder();
                foreach (LitJson.JsonData tag in jd["allow_tags"]["tagid"])
                {
                    tagsb.Append(tag.ToString());
                    tagsb.Append(",");
                }
                agent.allow_tags = tagsb.ToString().TrimEnd(',');
            }
            if (jd["allow_partys"].ContainsKey("partyid") && jd["allow_partys"]["partyid"].IsArray)
            {
                StringBuilder partysb = new StringBuilder();
                foreach (LitJson.JsonData tag in jd["allow_partys"]["partyid"])
                {
                    partysb.Append(tag.ToString());
                    partysb.Append(",");
                }
                agent.allow_partys = partysb.ToString().TrimEnd(',');
            }
            if (jd["allow_userinfos"].ContainsKey("user") && jd["allow_userinfos"]["user"].IsArray)
            {
                List<Tuple<string, int>> tuple = new List<Tuple<string, int>>();
                foreach (LitJson.JsonData tag in jd["allow_userinfos"]["user"])
                {
                    tuple.Add(new Tuple<string, int>(tag["userid"].ToString(), tag["status"].ToString().ToInt()));
                }
                agent.allow_userinfos = tuple;
            }
            return agent;
        }

        /// <summary>
        /// 设置企业号应用
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        public bool SetAgentInfo(Data.Model.WeiXinAgent agent)
        {
            #region 数据示例
            //{
            //   "agentid": "5",
            //   "report_location_flag": "0",
            //   "logo_mediaid": "xxxxx",
            //   "name": "NAME",
            //   "description": "DESC",
            //   "redirect_domain": "xxxxxx",
            //   "isreportuser":0,
            //   "isreportenter":0,
            //   "home_url":"http://www.qq.com",
            //   "chat_extension_url":"http://www.qq.com"
            //}
            #endregion
            string url = "https://qyapi.weixin.qq.com/cgi-bin/agent/set?access_token=" + GetAccessToken();
            LitJson.JsonData jd = new LitJson.JsonData();
            jd["agentid"] = agent.agentid;
            jd["report_location_flag"] = agent.report_location_flag;
            jd["logo_mediaid"] = agent.logo_mediaid;
            jd["name"] = agent.name;
            jd["description"] = agent.description;
            jd["redirect_domain"] = agent.redirect_domain;
            jd["isreportuser"] = agent.isreportuser;
            jd["isreportenter"] = agent.isreportenter;
            jd["home_url"] = agent.home_url;
            jd["chat_extension_url"] = agent.chat_extension_url;
            string jsonData = jd.ToJson(false);
            string returnString = Utility.HttpHelper.SendPost(url, jsonData);
            LitJson.JsonData rjd = LitJson.JsonMapper.ToObject(returnString);
            if (rjd.ContainsKey("errcode") && "0" != rjd["errcode"].ToString())
            {
                Platform.Log.Add("微信设置应用-失败", returnString, Log.Types.微信企业号, jsonData);
                return false;
            }
            else
            {
                Platform.Log.Add("微信设置应用-成功", returnString, Log.Types.微信企业号, jsonData);
                return true;
            }
        }

        /// <summary>
        /// 获取应用概况列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Data.Model.WeiXinAgent> GetAgents()
        {
            #region 数据示例
            //{
            //   "errcode": 0,
            //   "errmsg": "ok",
            //   "agentlist": [
            //       {
            //           "agentid": "5",
            //           "name": "企业小助手",
            //           "square_logo_url": "url",
            //           "round_logo_url": "url"
            //       },
            //       {
            //           "agentid": "8",
            //           "name": "HR小助手",
            //           "square_logo_url": "url",
            //           "round_logo_url": "url"
            //       }
            //       ]  
            //}
            #endregion

            List<Data.Model.WeiXinAgent> list = new List<Data.Model.WeiXinAgent>();
            string url = "https://qyapi.weixin.qq.com/cgi-bin/agent/list?access_token=" + GetAccessToken();
            string returnString = Utility.HttpHelper.SendGet(url);
            LitJson.JsonData jd = LitJson.JsonMapper.ToObject(returnString);
            if (jd.ContainsKey("errcode") && "0" != jd["errcode"].ToString())
            {
                Platform.Log.Add("微信获取应用列表-失败", returnString, Log.Types.微信企业号);
                return list;
            }
            if (jd.ContainsKey("agentlist") && jd["agentlist"].IsArray)
            {
                foreach (LitJson.JsonData agent in jd["agentlist"])
                {
                    Data.Model.WeiXinAgent ag = new Data.Model.WeiXinAgent();
                    ag.agentid = agent["agentid"].ToString().ToInt();
                    ag.name = agent["name"].ToString();
                    ag.square_logo_url = agent.ContainsKey("square_logo_url") ? agent["square_logo_url"].ToString() : "";
                    ag.round_logo_url = agent.ContainsKey("round_logo_url") ? agent["round_logo_url"].ToString() : "";
                    list.Add(ag);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据代码得到应用ID
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int GetAgentIDByCode(string code)
        {
            var dict = new Dictionary().GetByCode(code, true);
            return dict == null ? -1 : dict.Value.ToInt(-1);
        }


    }
}
