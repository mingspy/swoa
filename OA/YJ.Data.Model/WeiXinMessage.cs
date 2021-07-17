using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YJ.Data.Model
{
	[Serializable]
	public class WeiXinMessage
	{
		/// <summary>
		/// ID
		/// </summary>
		[DisplayName("ID")]
		public Guid ID { get; set; }

		/// <summary>
		/// 企业号CorpID
		/// </summary>
		[DisplayName("企业号CorpID")]
		public string ToUserName { get; set; }

		/// <summary>
		/// 发送人员账号
		/// </summary>
		[DisplayName("发送人员账号")]
		public string  FromUserName { get; set; }

		/// <summary>
		/// 消息创建时间
		/// </summary>
		[DisplayName("消息创建时间")]
		public int CreateTime { get; set; }

		/// <summary>
		/// 消息创建时间
		/// </summary>
		[DisplayName("消息创建时间")]
		public DateTime CreateTime1 { get; set; }

		/// <summary>
		/// 消息类型
		/// </summary>
		[DisplayName("消息类型")]
		public string MsgType { get; set; }

		/// <summary>
		/// 消息id，64位整型
		/// </summary>
		[DisplayName("消息id，64位整型")]
		public long MsgId { get; set; }

		/// <summary>
		/// 企业应用的id
		/// </summary>
		[DisplayName("企业应用的id")]
		public int AgentID { get; set; }

		/// <summary>
		/// 文本消息内容
		/// </summary>
		[DisplayName("文本消息内容")]
		public string Contents { get; set; }

		/// <summary>
		/// 图片链接
		/// </summary>
		[DisplayName("图片链接")]
		public string PicUrl { get; set; }

		/// <summary>
		/// 图片媒体文件id
		/// </summary>
		[DisplayName("图片媒体文件id")]
		public string MediaId { get; set; }

		/// <summary>
		/// 语音格式，如amr，speex等
		/// </summary>
		[DisplayName("语音格式，如amr，speex等")]
		public string Format { get; set; }

		/// <summary>
		/// 视频消息缩略图的媒体id
		/// </summary>
		[DisplayName("视频消息缩略图的媒体id")]
		public string ThumbMediaId { get; set; }

		/// <summary>
		/// 地理位置纬度
		/// </summary>
		[DisplayName("地理位置纬度")]
		public string Location_X { get; set; }

		/// <summary>
		/// 地理位置经度
		/// </summary>
		[DisplayName("地理位置经度")]
		public string Location_Y { get; set; }

		/// <summary>
		/// 地图缩放大小
		/// </summary>
		[DisplayName("地图缩放大小")]
		public string Scale { get; set; }

		/// <summary>
		/// 地理位置信息
		/// </summary>
		[DisplayName("地理位置信息")]
		public string Label { get; set; }

		/// <summary>
		/// 标题(link消息)
		/// </summary>
		[DisplayName("标题(link消息)")]
		public string Title { get; set; }

		/// <summary>
		/// 描述(link消息)
		/// </summary>
		[DisplayName("描述(link消息)")]
		public string Description { get; set; }

		/// <summary>
		/// 添加时间
		/// </summary>
		[DisplayName("添加时间")]
		public DateTime AddTime { get; set; }

	}
}
