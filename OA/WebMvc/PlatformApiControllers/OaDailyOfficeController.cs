using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;


namespace WebMvc.PlatformApiControllers
{
    public class OaDailyOfficeController : ApiController
    {
        /// <summary>
        /// 验证加班天数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetDay(int type,string statedate = "",string enddate="", string statedatehr= "", string enddatehr= "",string UserID="")
        {
            YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
            try
            {
                string ID =  YJ.Platform.Users.CurrentUserID.ToString();
                string userid = UserID;
                double Hours = 0;
                double day = 0;
                //加班小时数
                string count = db.ExecuteScalar("select SUM((Days*8))+(case  when SUM(Hours) is null then 0 else SUM(Hours) end) from OaWorkOverTime where IsFinished=1  and YEAR(StartDate)=YEAR(getdate()) and CONVERT(varchar(100), StartDate, 23)>'2017-09-01' and OwnerID like '%" + userid + "%'");
                //调休小时数
                string count1 = db.ExecuteScalar("select SUM((Days*8))+(case  when SUM(Hours) is null then 0 else SUM(Hours) end) from OaLeave where IsFinished=1 and YEAR(LeaveDate)=YEAR(getdate())  and Type=6 and CONVERT(varchar(100), LeaveDate, 23)>'2017-09-01' and  UserID='" + userid + "'"); 
                if (type==0)
                {
                    if (count.IsNullOrEmpty())
                    {
                        return new HttpResponseMessage()
                        {
                            Content = new StringContent("{\"status\":0,\"msg\":\"您没有可用的调休天数\"}", Encoding.UTF8, "application/json")
                        };
                    }
                    else
                    {
                        if (count1.IsNullOrEmpty() && count.ToDouble()> 0)
                        {
                            return new HttpResponseMessage()
                            {
                                Content = new StringContent("{\"status\":1,\"msg\":\"现有调休" +count.ToDouble()/8+ "天\"}", Encoding.UTF8, "application/json")
                            };
                        }
                        else
                        {
                            if ((count.ToDouble()-count1.ToDouble()) <= 0)
                            {
                                return new HttpResponseMessage()
                                {
                                    Content = new StringContent("{\"status\":0,\"msg\":\"您没有可用的调休天数\"}", Encoding.UTF8, "application/json")
                                };
                            }
                            else
                            {
                                return new HttpResponseMessage()
                                {
                                    Content = new StringContent("{\"status\":1,\"msg\":\"现有调休" + (count.ToDouble() - count1.ToDouble())/8 + "天\"}", Encoding.UTF8, "application/json")
                                };
                            }
                        }

                    }
                }
                if (statedate.IsNullOrEmpty() || enddate.IsNullOrEmpty() || statedatehr.IsNullOrEmpty() || enddatehr.IsNullOrEmpty())
                {
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent("{\"status\":0,\"msg\":\"请选择开始时间或者结束时间\"}", Encoding.UTF8, "application/json")
                    };
                }
                string dt1 = string.Empty;
                string dt2 = string.Empty;
                if (getTimeSpan(0, statedatehr))
                {
                    dt1 =statedatehr;
                }
                else if (getTimeSpan(1, statedatehr))
                {
                    dt1 ="08:00";
                }
                else
                {
                    dt1 ="17:00";
                }
                //结束时间
                if (getTimeSpan(0, enddatehr))
                {
                    dt2 =enddatehr;
                }
                else if (getTimeSpan(2, enddatehr))
                {
                    dt2 ="17:00";
                }
                else
                {
                    dt2 ="08:00";
                }
                TimeSpan d4 = enddate.ToDateTime().Subtract(statedate.ToDateTime());
                TimeSpan d5;
                TimeSpan d6;
                double Restdate = 0;
                if (getTimeSpan(1, dt1, "12:00") && getTimeSpan(2, dt2, "00:00", "13:00"))
                {
                    Restdate = 1;
                }
                if (d4.Days==0)
                {
                    if (dt1.ToDateTime()> dt2.ToDateTime())
                    {
                        return new HttpResponseMessage()
                        {
                            Content = new StringContent("{\"data\":0,\"Hours\":0,\"status\":1,\"msg\":\"结束时间不能早于开始时间\"}", Encoding.UTF8, "application/json")
                        };
                    }
                    if (getTimeSpan(0,dt1,"08:00","08:30")&& getTimeSpan(0, dt2, "17:00", "17:30"))
                    {
                        day=1;
                    }
                    else
                    {
                        d5 = dt2.ToDateTime().Subtract(dt1.ToDateTime());
                        Hours=(d5.Hours + Math.Round(d5.Minutes / 60.ToString().ToDouble(), 2))-Restdate;
                    }               
                }
                else if(d4.Days==1)
                {
                    if (getTimeSpan(0, dt1, "17:00", "17:30") && getTimeSpan(0, dt2, "08:00", "08:30"))
                    {
                        return new HttpResponseMessage()
                        {
                            Content = new StringContent("{\"data\":0,\"Hours\":0,\"status\":1,\"msg\":\"正常\"}", Encoding.UTF8, "application/json")
                        };
                    }
                    else if (getTimeSpan(0, dt1, "08:00", "08:30") && getTimeSpan(0, dt2, "17:00", "17:30"))
                    {
                        day = d4.Days + 1;
                    }
                    else
                    {
                        day = d4.Days;
                        d5 = "17:00".ToDateTime().Subtract(dt1.ToDateTime());
                        if (d5.Hours <= 8)
                        {
                            day = day - 0.5;
                            Hours = Hours + d5.Hours + Math.Round(d5.Minutes / 60.ToString().ToDouble(), 2);
                        }
                        d6 = dt2.ToDateTime().Subtract("08:00".ToDateTime());
                        if (d6.Hours <= 8)
                        {
                            day = day - 0.5;
                            Hours = Hours + d6.Hours + Math.Round(d6.Minutes / 60.ToString().ToDouble(), 2);
                        }
                        if (getTimeSpan(1, dt1, "12:00"))
                        {
                            Hours = Hours - 1;
                        }
                        if (getTimeSpan(1, "17:00", dt2))
                        {
                            Hours = Hours - 1;
                        }
                        day = day + Hours / 8;
                        Hours = Hours % 8;
                    }
                }
                else
                {
                    if (getTimeSpan(0, dt1, "17:00", "17:30") && getTimeSpan(0, dt2, "08:00", "08:30"))
                    {
                        day =d4.Days-1;
                    }
                    else if (getTimeSpan(0, dt1, "08:00", "08:30") && getTimeSpan(0, dt2, "17:00", "17:30"))
                    {
                        day =d4.Days+1;
                    }
                    else
                    {
                        day = d4.Days;
                        d5 = "17:00".ToDateTime().Subtract(dt1.ToDateTime());
                        if (d5.Hours <= 8)
                        {
                            day =day-0.5;
                            Hours = Hours + d5.Hours + Math.Round(d5.Minutes / 60.ToString().ToDouble(), 2);
                        }
                        d6 = dt2.ToDateTime().Subtract("08:00".ToDateTime());
                        if (d6.Hours<=8)
                        {
                            day =day-0.5;
                            Hours = Hours + d6.Hours + Math.Round(d6.Minutes / 60.ToString().ToDouble(), 2);
                        }
                        if (getTimeSpan(1, dt1, "12:00"))
                        {
                            Hours = Hours - 1;
                        }
                        if (getTimeSpan(1, "17:00", dt2))
                        {
                            Hours = Hours - 1;
                        }
                        day = day + Hours/8;
                        Hours = Hours % 8;
                    }
                }
                day = Math.Floor(day);
                if (count1.IsNullOrEmpty()?(day*8+Hours)>count.ToDouble():(day*8 + Hours + count1.ToDouble())>count.ToDouble())
                {
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent("{\"status\":0,\"msg\":\"调休天数已超出加班天数，您的现有调休天数为"+Math.Round((count.ToDouble()-count1.ToDouble())/8,2)+"天，您的请假申请天数为"+ day+ "天"+Hours+"小时\"}", Encoding.UTF8, "application/json")
                    };
                }
                return new HttpResponseMessage()
                {
                    Content = new StringContent("{\"data\":" + day + ",\"Hours\":" + (Math.Round(Hours, 2) >= 0 ? Math.Round(Hours, 2) : 0) + ",\"status\":1,\"msg\":\"正常\"}", Encoding.UTF8, "application/json")
                };
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        /// <summary>
        /// 验证加班天数（批量）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetDay(double day,double hours, string userid)
        {
            YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
            try
            {
                string ID = YJ.Platform.Users.CurrentUserID.ToString();
                //加班小时数
                string count = db.ExecuteScalar("select SUM((Days*8))+(case  when SUM(Hours) is null then 0 else SUM(Hours) end) from OaWorkOverTime where IsFinished=1  and YEAR(StartDate)=YEAR(getdate())  and OwnerID like '%" + userid + "%'");
                //调休小时数
                string count1 = db.ExecuteScalar("select SUM((Days*8))+(case  when SUM(Hours) is null then 0 else SUM(Hours) end) from OaLeave where IsFinished=1 and YEAR(LeaveDate)=YEAR(getdate())  and Type=6  and  UserID='" + userid + "'");
                day = Math.Floor(day);
                if (count1.IsNullOrEmpty() ? (day * 8 + hours) > count.ToDouble() : (day * 8 + hours + count1.ToDouble()) > count.ToDouble())
                {
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent("{\"status\":0,\"msg\":\"此用户调休天数已超出加班天数，您的现有调休天数为" + Math.Round((count.ToDouble() - count1.ToDouble()) / 8, 2) + "天\"}", Encoding.UTF8, "application/json")
                    };
                }
                return new HttpResponseMessage()
                {
                    Content = new StringContent("{\"data\":" + count + ",\"count\":" + count1+ ",\"status\":1,\"msg\":\"正常\"}", Encoding.UTF8, "application/json")
                };
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        /// <summary>
        /// 获取请假天数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetWorkDay(string statedate = "", string enddate = "", string statedatehr = "", string enddatehr = "")
        {
            YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
            string NoonDate = string.Empty;
            string AfternoonDate = string.Empty;
            int month = DateTime.Now.Month;
            if (5<=month&&month<10)
            {
                NoonDate = "13:30";
                AfternoonDate = "17:30";
            }
            else
            {
                NoonDate = "13:00";
                AfternoonDate = "17:00";
            }
            double Hours = 0;
            double day = 0;
            try
            {
                if (statedate.IsNullOrEmpty()||enddate.IsNullOrEmpty()||statedatehr.IsNullOrEmpty()||enddatehr.IsNullOrEmpty())
                {
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent("{\"status\":0,\"msg\":\"请选择开始时间或者结束时间\"}", Encoding.UTF8, "application/json")
                    };
                }
                string dt1 = string.Empty;
                string dt2 = string.Empty;
                if (getTimeSpan(0, statedatehr))
                {
                    dt1 = statedatehr;
                }
                else if (getTimeSpan(1, statedatehr))
                {
                    dt1 = "08:00";
                }
                else
                {
                    dt1 = AfternoonDate;
                }
                //结束时间
                if (getTimeSpan(0, enddatehr))
                {
                    dt2 = enddatehr;
                }
                else if (getTimeSpan(2, enddatehr))
                {
                    dt2 = AfternoonDate;
                }
                else
                {
                    dt2 = "08:00";
                }
                TimeSpan d4 = enddate.ToDateTime().Subtract(statedate.ToDateTime());
                TimeSpan d5;
                TimeSpan d6;
                double Restdate = 0;
                if (getTimeSpan(1, dt1, "12:00") && getTimeSpan(2, dt2, "00:00", NoonDate))
                {
                    Restdate = 1;
                }
                if (d4.Days == 0)
                {
                    if (dt1.ToDateTime() > dt2.ToDateTime())
                    {
                        return new HttpResponseMessage()
                        {
                            Content = new StringContent("{\"data\":0,\"Hours\":0,\"status\":1,\"msg\":\"结束时间不能早于开始时间\"}", Encoding.UTF8, "application/json")
                        };
                    }
                    if (getTimeSpan(0, dt1, "08:00", "08:30") && getTimeSpan(0, dt2, "17:00", "17:30"))
                    {
                        day = 1;
                    }
                    else if(getTimeSpan(0, dt1, "08:00", "08:30")&& getTimeSpan(0, dt2, "12:00", NoonDate))
                    {
                        Hours=4;
                    }
                    else if (getTimeSpan(0, dt1, "12:00", NoonDate) && getTimeSpan(0, dt2, "17:00", "17:30"))
                    {
                        Hours = 4;
                    }
                    else
                    {
                        d5 = dt2.ToDateTime().Subtract(dt1.ToDateTime());
                        Hours = (d5.Hours + Math.Round(d5.Minutes/60.ToString().ToDouble(), 2)) - Restdate;
                    }
                }
                else if (d4.Days == 1)
                {
                    if (getTimeSpan(0, dt1, "17:00", "17:30") && getTimeSpan(0, dt2, "08:00", "08:30"))
                    {
                        return new HttpResponseMessage()
                        {
                            Content = new StringContent("{\"data\":0,\"Hours\":0,\"status\":1,\"msg\":\"正常\"}", Encoding.UTF8, "application/json")
                        };
                    }
                    else if (getTimeSpan(0, dt1, "08:00", "08:30") && getTimeSpan(0, dt2, "17:00", "17:30"))
                    {
                        day = d4.Days + 1;
                    }
                    else if (getTimeSpan(0, dt1, "08:00", "08:30") && getTimeSpan(0, dt2, "12:00", NoonDate))
                    {
                        day = d4.Days;
                        Hours = 4;
                    }
                    else if (getTimeSpan(0, dt1, "12:00", NoonDate) && getTimeSpan(0, dt2, "17:00", "17:30"))
                    {
                        day = d4.Days;
                        Hours = 4;
                    }
                    else
                    {
                        d5 = AfternoonDate.ToDateTime().Subtract(dt1.ToDateTime());
                        if (d5.Hours <= 8)
                        {
                            Hours = Hours + d5.Hours + Math.Round(d5.Minutes / 60.ToString().ToDouble(), 2);
                            if (getTimeSpan(1, dt1, "12:00"))
                            {
                                Hours = Hours - 1;
                            }
                        }
                        else
                        {
                            day = day + 1;
                        }
                        d6 = dt2.ToDateTime().Subtract("08:00".ToDateTime());
                        if (d6.Hours <= 8)
                        {
                            Hours = Hours + d6.Hours + Math.Round(d6.Minutes / 60.ToString().ToDouble(), 2);
                            if (getTimeSpan(1, NoonDate, dt2))
                            {
                                Hours = Hours - 1;
                            }
                        }
                        else
                        {
                            day = day + 1;
                        }
                        day = day + Hours / 8;
                        Hours = Hours % 8;
                    }
                }
                else
                {
                    if (getTimeSpan(0, dt1, "17:00", "17:30") && getTimeSpan(0, dt2, "08:00", "08:30"))
                    {
                        day = d4.Days - 1;
                    }
                    else if (getTimeSpan(0, dt1, "08:00", "08:30") && getTimeSpan(0, dt2, "17:00", "17:30"))
                    {
                        day = d4.Days + 1;
                    }
                    else if (getTimeSpan(0, dt1, "08:00", "08:30") && getTimeSpan(0, dt2, "12:00", NoonDate))
                    {
                        day = d4.Days;
                        Hours = 4;
                    }
                    else if (getTimeSpan(0, dt1, "12:00", NoonDate) && getTimeSpan(0, dt2, "17:00", "17:30"))
                    {
                        day = d4.Days;
                        Hours = 4;
                    }
                    else
                    {
                        day = d4.Days;
                        d5 = "17:00".ToDateTime().Subtract(dt1.ToDateTime());
                        if (d5.Hours <= 8)
                        {
                            day = day - 0.5;
                            Hours = Hours + d5.Hours + Math.Round(d5.Minutes / 60.ToString().ToDouble(), 2);
                        }
                        d6 = dt2.ToDateTime().Subtract("08:00".ToDateTime());
                        if (d6.Hours <= 8)
                        {
                            day = day - 0.5;
                            Hours = Hours + d6.Hours + Math.Round(d6.Minutes / 60.ToString().ToDouble(), 2);
                        }
                        if (getTimeSpan(1, dt1, "12:00"))
                        {
                            Hours = Hours - 1;
                        }
                        if (getTimeSpan(1, "17:00", dt2))
                        {
                            Hours = Hours - 1;
                        }
                        day = day + Hours / 8;
                        Hours = Hours % 8;
                    }
                }
                //string dt1 = string.Empty;
                //string dt2 = string.Empty;
                ////开始时间
                //if (getTimeSpan(0,statedatehr))
                //{
                //    dt1 = statedate + " " + statedatehr;
                //}
                //else if (getTimeSpan(1, statedatehr))
                //{
                //    dt1 = statedate + " 8:00";
                //}
                //else
                //{
                //    dt1 = statedate.ToDateTime().AddDays(1).ToString("d") + " 8:30";
                //}
                ////结束时间
                //if (getTimeSpan(0, enddatehr))
                //{
                //    dt2 = enddate+" " + enddatehr;
                //}
                //else if (getTimeSpan(2, enddatehr))
                //{
                //    dt2 = enddate + " 17:00";
                //}
                //else
                //{
                //    dt2 = enddate.ToDateTime().AddDays(-1).ToString("d") + " 17:00";
                //}
                //if (dt1.ToDateTime() > dt2.ToDateTime())
                //{
                //    return new HttpResponseMessage()
                //    {
                //        Content = new StringContent("{\"data\":0,\"status\":1,\"msg\":\"结束时间不能早于开始时间\"}", Encoding.UTF8, "application/json")
                //    };
                //}
                //TimeSpan d3 = dt2.ToDateTime().Subtract(dt1.ToDateTime());
                //double Restdate = 0;
                //if (getTimeSpan(1, statedatehr, "12:00") && getTimeSpan(2, enddatehr, "00:00", "13:00"))
                //{
                //    Restdate = 1;
                //}
                //double day = Math.Round(Convert.ToDouble(d3.Days + Math.Round((d3.Hours - Restdate) / 8.ToString().ToDouble(), 2)), 2);
                //day = day < 1 ? 0 : Math.Floor(day);
                //double Hours =(d3.Hours-Restdate)%8>1?Math.Round(((d3.Hours - Restdate)%8) + Math.Round(d3.Minutes/60.ToString().ToDouble(), 2), 2):0;
                return new HttpResponseMessage()
                {
                    Content = new StringContent("{\"data\":" +Math.Floor(day) + ",\"Hours\":" +(Math.Round(Hours,2)>=0? Math.Round(Hours, 2):0) + ",\"status\":1,\"msg\":\"正常\"}", Encoding.UTF8, "application/json")
                };
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        /// <summary>
        /// 获取加班天数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetWorkOverDays(string userid,string statedate = "", string enddate = "", string statedatehr = "", string enddatehr = "")
        {
            YJ.Data.MSSQL.DBHelper db = new YJ.Data.MSSQL.DBHelper();
            try
            {

                if (statedate.IsNullOrEmpty() || enddate.IsNullOrEmpty() || statedatehr.IsNullOrEmpty() || enddatehr.IsNullOrEmpty())
                {
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent("{\"status\":0,\"msg\":\"请选择开始时间或者结束时间\"}", Encoding.UTF8, "application/json")
                    };
                }
                StringBuilder sb = new StringBuilder();
                bool isshow = false;
                YJ.Platform.Users busers = new YJ.Platform.Users();
                foreach (var item in userid.Split(','))
                {
                    //加班天数
                    string count = db.ExecuteScalar("select SUM((Days*8))+(case  when SUM(Hours) is null then 0 else SUM(Hours) end) from OaWorkOverTime where IsFinished = 1  and YEAR(StartDate)=YEAR(getdate()) and MONTH(StartDate)=MONTH('" + statedate + "') and  OwnerID = '" + item + "'");
                    if (!count.IsNullOrEmpty())
                    {
                        if (Math.Round(count.ToDouble())> 36)
                        {
                            isshow = true;
                            sb.Append(""+busers.GetName(busers.RemovePrefix1(item).ToGuid())+"在本月的加班天数已超过36小时<br/>");
                        }
                    }
                }
                if (isshow)
                {
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent("{\"status\":2,\"msg\":\""+sb.ToString()+"超过部分不予计算加班时长。\"}", Encoding.UTF8, "application/json")
                    };
                }
                string dt1 = string.Empty;
                string dt2 = string.Empty;
                //开始时间
                    dt1 = statedate + " " + statedatehr;      
                //结束时间
                    dt2 = enddate + " " + enddatehr;
                if (dt1.ToDateTime()> dt2.ToDateTime())
                {
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent("{\"data\":0,\"Hours\":0,\"status\":1,\"msg\":\"结束时间不能早于开始时间\"}", Encoding.UTF8, "application/json")
                    };
                }
                TimeSpan d3 = dt2.ToDateTime().Subtract(dt1.ToDateTime());
                double Restdate = 0;
                if (getTimeSpan(1,statedatehr,"12:00")&&getTimeSpan(2,enddatehr,"00:00","13:00"))
                {
                    Restdate = 1;
                }
                if (8<d3.Hours&&d3.Hours<24)
                {
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent("{\"data\":" + (d3.Days+1)+ ",\"Hours\":\"0\",\"status\":1,\"msg\":\"正常\"}", Encoding.UTF8, "application/json")
                    };
                }
                double day = Math.Round(Convert.ToDouble(d3.Days + Math.Round((d3.Hours-Restdate)/8.ToString().ToDouble(), 2)),2);
                day = day < 1 ? 0 : Math.Floor(day);
                double Hours = Math.Round(((d3.Hours - Restdate)%8)+ Math.Round(d3.Minutes/60.ToString().ToDouble(), 2), 2);
                return new HttpResponseMessage()
                {
                    Content = new StringContent("{\"data\":" + day + ",\"Hours\":" + Hours + ",\"status\":1,\"msg\":\"正常\"}", Encoding.UTF8, "application/json")
                };
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        /// <summary>
        /// 判断时间事都在某个时间段之间
        /// </summary>
        /// <param name="timeStr"></param>
        /// <returns></returns>
        protected bool getTimeSpan(int type,string timeStr,string _strWorkingDayAM="08:00",string _strWorkingDayPM="17:00")
        {
            //判断当前时间是否在工作时间段内
            TimeSpan dspWorkingDayAM = DateTime.Parse(_strWorkingDayAM).TimeOfDay;
            TimeSpan dspWorkingDayPM = DateTime.Parse(_strWorkingDayPM).TimeOfDay;
            DateTime t1 = Convert.ToDateTime(timeStr);

            TimeSpan dspNow = t1.TimeOfDay;
            if (type==0)
            {
                if (dspNow >= dspWorkingDayAM && dspNow <= dspWorkingDayPM)
                {
                    return true;
                }
            } 
            else if (type==1)
            {
                if (dspNow<=dspWorkingDayAM)
                {
                    return true;
                }
            }
            else if(type == 2)
            {
                if (dspNow>=dspWorkingDayPM)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 获取时间差（天数）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetDataDays(string data1 = "", string data2 = "")
        {
            try
            {
                if (data1.IsNullOrEmpty() || data2.IsNullOrEmpty())
                {
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent("{\"status\":0,\"msg\":\"时间不能为空\"}", Encoding.UTF8, "application/json")
                    };
                }
                TimeSpan d3 = data2.ToDateTime().Subtract(data1.ToDateTime());
                double day = d3.Days;
                return new HttpResponseMessage()
                {
                    Content = new StringContent("{\"data\":" + day + ",\"status\":1,\"msg\":\"正常\"}", Encoding.UTF8, "application/json")
                };
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
