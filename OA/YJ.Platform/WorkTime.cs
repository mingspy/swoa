using System;
using System.Collections.Generic;
using System.Text;

namespace YJ.Platform
{
    public class WorkTime
    {
        private YJ.Data.Interface.IWorkTime dataWorkTime;
        public WorkTime()
        {
            this.dataWorkTime = YJ.Data.Factory.Factory.GetWorkTime();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.WorkTime model)
        {
            return dataWorkTime.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.WorkTime model)
        {
            return dataWorkTime.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.WorkTime> GetAll()
        {
            return dataWorkTime.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.WorkTime Get(Guid id)
        {
            return dataWorkTime.Get(id);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(Guid id)
        {
            return dataWorkTime.Delete(id);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataWorkTime.GetCount();
        }

        /// <summary>
        /// 查询所有年份
        /// </summary>
        public List<int> GetAllYear()
        {
            return dataWorkTime.GetAllYear();
        }

        /// <summary>
        /// 得到所有年份下拉列表选项
        /// </summary>
        /// <param name="defaultYear">默认值</param>
        /// <returns></returns>
        public System.Web.UI.WebControls.ListItem[] GetAllYearOptionItems(int defaultYear = 0)
        {
            if (defaultYear == 0)
            {
                defaultYear = Utility.DateTimeNew.Now.Year;
            }
            var years = GetAllYear();
            List<System.Web.UI.WebControls.ListItem> items = new List<System.Web.UI.WebControls.ListItem>();
            
            foreach (var year in years)
            {
                System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem(year.ToString(), year.ToString());
                item.Selected = items.Find(p => p.Selected) == null && defaultYear == year;
                items.Add(item);
            }
            return items.ToArray();
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.WorkTime> GetAll(int year)
        {
            return dataWorkTime.GetAll(year);
        }

        /// <summary>
        /// 得到数字下拉选项
        /// </summary>
        /// <param name="start">开始数字</param>
        /// <param name="end">结束数字</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public System.Web.UI.WebControls.ListItem[] GetNumberOptionsItems(int start, int end, int defaultValue)
        {
            List<System.Web.UI.WebControls.ListItem> items = new List<System.Web.UI.WebControls.ListItem>();
            for (int i = start; i <= end; i++)
            {
                System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString());
                item.Selected = defaultValue == i;
                items.Add(item);
            }
            return items.ToArray();
        }

        /// <summary>
        /// 从缓存得到一年的工作时间设置
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<YJ.Data.Model.WorkTime> GetYearFromCache(int year)
        {
            string cacheKey = YJ.Utility.Keys.CacheKeys.WorkTime.ToString() + "_" + year.ToString();
            var obj = Cache.IO.Opation.Get(cacheKey);
            if (obj != null && obj is List<YJ.Data.Model.WorkTime>)
            {
                return (List<YJ.Data.Model.WorkTime>)obj;
            }
            else
            {
                var list = GetAll(year);
                Cache.IO.Opation.Set(cacheKey, list);
                return list;
            }
        }

        /// <summary>
        /// 清除一年的工作时间缓存
        /// </summary>
        /// <param name="year"></param>
        public void ClearYearCache(int year)
        {
            string cacheKey = YJ.Utility.Keys.CacheKeys.WorkTime.ToString() + "_" + year.ToString();
            Cache.IO.Opation.Remove(cacheKey);
        }

        /// <summary>
        /// 得到某个工作日的上下班时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public Tuple<DateTime, DateTime, DateTime, DateTime> GetWorkAmPmTime(DateTime date)
        {
            Tuple<DateTime, DateTime, DateTime, DateTime> tupl = null;
            var workTimeList = GetYearFromCache(date.Year);
            Data.Model.WorkTime wt = null;
            foreach (var workTime in workTimeList)
            {
                if (date >= workTime.Date1 && date <= workTime.Date2)
                {
                    wt = workTime;
                    break;
                }
            }

            if (wt == null)
            {
                return tupl;
            }

            DateTime amTime1 = (date.ToDateString() + " " + wt.AmTime1).ToDateTime();
            DateTime amTime2 = (date.ToDateString() + " " + wt.AmTime2).ToDateTime();
            DateTime pmTime1 = (date.ToDateString() + " " + wt.PmTime1).ToDateTime();
            DateTime pmTime2 = (date.ToDateString() + " " + wt.PmTime2).ToDateTime();
            return new Tuple<DateTime, DateTime, DateTime, DateTime>(amTime1, amTime2, pmTime1, pmTime2);
        }

        /// <summary>
        /// 得到一个时间段内的休息时间分钟数
        /// </summary>
        /// <param name="date1">开始时间</param>
        /// <param name="date2">结束时间</param>
        /// <returns></returns>
        public double GetRestMinutes(DateTime date1, DateTime date2)
        {
            double minutes = 0;
            int days = (date2 - date1).Days;
            for (int day = 0; day < days; day++)
            {
                Tuple<DateTime, DateTime, DateTime, DateTime> tuple = GetWorkAmPmTime(date1.AddDays(day));
                if (tuple == null)
                {
                    minutes += 1440;
                    continue;
                }
                DateTime amTime1 = tuple.Item1;
                DateTime amTime2 = tuple.Item2;
                DateTime pmTime1 = tuple.Item3;
                DateTime pmTime2 = tuple.Item4;
                minutes += (amTime1 - amTime1.Date).TotalMinutes;
                minutes += ((amTime1.Date.ToDateString() + " 23:59:59").ToDateTime() - pmTime2).TotalMinutes;
                minutes += (pmTime1 - amTime2).TotalMinutes;
            }

            Tuple<DateTime, DateTime, DateTime, DateTime> tupleStart = GetWorkAmPmTime(date1);
            if (tupleStart != null && date1 < tupleStart.Item1)
            {
                minutes += ( tupleStart.Item1 - date1).TotalMinutes;
            }
            Tuple<DateTime, DateTime, DateTime, DateTime> tupleEnd = GetWorkAmPmTime(date2);
            if (tupleEnd != null && date2 > tupleEnd.Item4)
            {
                minutes += (tupleEnd.Item1.AddDays(1) - tupleEnd.Item4).TotalMinutes;
            }

            return minutes;
        }

        /// <summary>
        /// 得到实际的工作时间
        /// </summary>
        /// <param name="date">工作时间</param>
        /// <returns></returns>
        public DateTime GetWorkDateTime(DateTime date, int hour)
        {
            DateTime workTime = date.AddHours(hour);

            workTime = workTime.AddMinutes(GetRestMinutes(date, workTime));


            return workTime;
        }
    }
}
