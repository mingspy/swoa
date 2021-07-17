using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;


namespace WebMvc.PlatformApiControllers
{
    public class ConferenceRoomController : ApiController
    {
        //
        // post: /查询空闲会议库/
        [HttpPost]
        public HttpResponseMessage SelectCR(dynamic data)
        {
            List<object> list = new List<object>();
            YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
            DateTime BeginDay = DateTime.Now;
            DateTime EndDay = DateTime.Now;
            DateTime BeginTime= DateTime.Now;
            DateTime EndTime = DateTime.Now;
            //接收传过来的时间值
            //开始日期
            if (data.BeginDay!="")
            {
                string Time1 = data.BeginDay;
                 BeginDay =Convert.ToDateTime(Time1);
            }
            //结束日期
            if (data.EndDay != "") {
                string Time2 = data.EndDay;
                EndDay = Convert.ToDateTime(Time2);
            }
            //开始时间
            if (data.BeginTime != "")
            {
                string Time3 = data.BeginTime;
                BeginTime = Convert.ToDateTime(Time3);
            }
            //结束时间
            if (data.EndTime != "")
            {
                string Time4 = data.EndTime;
                EndTime = Convert.ToDateTime(Time4);
            }
            //查询所有会议室    
            string sql = "select ID,Name,Address from CRConferenceRoom";
            DataTable dt = new DataTable();
           
            dt = db.GetDataTable(sql);
            //循环查询该会议室所有占用时间
            foreach (DataRow dr in dt.Rows)
            {
                string sql1 = "select BeginTime,EndTime,ConferenceRoom from CRMeetingRequest where ConferenceRoom='" + dr["ID"]+ "'and BeginDay='"+ BeginDay + "'";
                DataTable dt1 = db.GetDataTable(sql1);
                if (dt1.Rows.Count>0)
                {
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        //判断时间不为空
                        if (dr1["BeginTime"].ToString()==""|| dr1["EndTime"].ToString()=="")
                        {
                            break;
                        }
                        //循环对比
                        //时间判断如果新的开始时间小于历史开始时间那么新的结束时间也必须小于历史开始时间
                        if (BeginTime < Convert.ToDateTime(dr1["BeginTime"]) && EndTime < Convert.ToDateTime(dr1["BeginTime"]))
                        {
                            list.Add("{\"ID\":\"" + dr["id"].ToString() + "\",\"Name\":\"" + dr["Name"].ToString() + "\"}");
                            break;
                        }
                        //时间判断如果新的开始时间大于历史开始时间那么新开始时间必须大于历史结束时间
                        if (BeginTime > Convert.ToDateTime(dr1["BeginTime"]) && BeginTime > Convert.ToDateTime(dr1["EndTime"]))
                        {
                            list.Add("{\"ID\":\"" + dr["id"].ToString() + "\",\"Name\":\"" + dr["Name"].ToString() + "\"}");
                            break;
                        }
                    }
                }
                else
                {
                    list.Add("{\"ID\":\"" + dr["id"].ToString() + "\",\"Name\":\"" + dr["Name"].ToString() + "\"}");
                }
               
            }
           
            //拼接单选框html返回
            return new HttpResponseMessage()
            {
                Content = new StringContent("{\"data\":"+list.ToJsonString()+",\"status\":1,\"msg\":\"正常\"}", Encoding.UTF8, "application/json")
            };
        }
        //
        //post查询会议库地址
        [HttpPost]
        public HttpResponseMessage SelectAddress(dynamic data)
        {
            YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
            string ID = data.ID;
            string sql = "select Address from CRConferenceRoom where ID='"+ID+"'";
            string Address = db.ExecuteScalar(sql);
            return new HttpResponseMessage()
            {
                Content = new StringContent(""+ Address + "", Encoding.UTF8, "application/text")
            };
        }

        /// <summary>
        /// 查询当前周次
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage selectToDayViewWeek()
        {

            string sql = "select WeekNumberOfYear from DimDateStartWithMonday where FullDate =  CONVERT(varchar(10),GETDATE(),120)";
            try
            {
                YJ.Data.MSSQL.DBHelper dbHelper = new YJ.Data.MSSQL.DBHelper();
                DataTable dt = dbHelper.GetDataTable(sql);
                return new HttpResponseMessage()
                {
                    Content = WebMvc.AppControllers.Tool.ErrorContent.GetError(WebMvc.AppControllers.Tool.ErrorType.成功, "", dt.ToJsonString())
                };
            }
            catch (Exception e)
            {
                return new HttpResponseMessage()
                {
                    Content = WebMvc.AppControllers.Tool.ErrorContent.GetError(WebMvc.AppControllers.Tool.ErrorType.程序错误, e.Message)
                };
            }
        }
    }
}
