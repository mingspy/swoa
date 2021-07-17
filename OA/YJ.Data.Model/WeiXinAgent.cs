using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YJ.Data.Model
{
    public class WeiXinAgent
    {
        /// <summary>
        /// 企业应用的id
        /// </summary>
        public int agentid { get; set; }
        /// <summary>
        /// 企业应用是否打开地理位置上报 0：不上报；1：进入会话上报；2：持续上报
        /// </summary>
        public int report_location_flag { get; set; }
        /// <summary>
        /// 企业应用头像的mediaid，通过多媒体接口上传图片获得mediaid，上传后会自动裁剪成方形和圆形两个头像
        /// </summary>
        public string logo_mediaid { get; set; }
        /// <summary>
        /// 企业应用名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 企业应用详情
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 企业应用可信域名
        /// </summary>
        public string redirect_domain { get; set; }
        /// <summary>
        /// 是否接收用户变更通知。0：不接收；1：接收。
        /// </summary>
        public int isreportuser { get; set; }
        /// <summary>
        /// 是否上报用户进入应用事件。0：不接收；1：接收。
        /// </summary>
        public int isreportenter { get; set; }
        /// <summary>
        /// 主页型应用url。url必须以http或者https开头。消息型应用无需该参数
        /// </summary>
        public string home_url { get; set; }
        /// <summary>
        /// 关联会话url。设置该字段后，企业会话"+"号将出现该应用，点击应用可直接跳转到此url，支持jsapi向当前会话发送消息。
        /// </summary>
        public string chat_extension_url { get; set; }
        /// <summary>
        /// 方形头像url
        /// </summary>
        public string square_logo_url { get; set; }
        /// <summary>
        /// 圆形头像url
        /// </summary>
        public string round_logo_url { get; set; }
        /// <summary>
        /// 应用类型。1：消息型；2：主页型
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 企业应用是否被禁用
        /// </summary>
        public int close { get; set; }
        /// <summary>
        /// 企业应用可见范围（人员），其中包括userid和关注状态state
        /// </summary>
        public List<Tuple<string, int>> allow_userinfos { get; set; }
        /// <summary>
        /// 企业应用可见范围（部门）
        /// </summary>
        public string allow_partys { get; set; }
        /// <summary>
        /// 企业应用可见范围（标签）
        /// </summary>
        public string allow_tags { get; set; }
    }
}
