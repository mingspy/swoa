using System;
using System.Collections.Generic;
using System.Text;

namespace YJ.Platform
{
    public class WorkCalendar
    {
        private YJ.Data.Interface.IWorkCalendar dataWorkCalendar;
        public WorkCalendar()
        {
            this.dataWorkCalendar = YJ.Data.Factory.Factory.GetWorkCalendar();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public int Add(YJ.Data.Model.WorkCalendar model)
        {
            return dataWorkCalendar.Add(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public int Update(YJ.Data.Model.WorkCalendar model)
        {
            return dataWorkCalendar.Update(model);
        }
        /// <summary>
        /// 查询所有记录
        /// </summary>
        public List<YJ.Data.Model.WorkCalendar> GetAll()
        {
            return dataWorkCalendar.GetAll();
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        public YJ.Data.Model.WorkCalendar Get(DateTime workdate)
        {
            return dataWorkCalendar.Get(workdate);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public int Delete(DateTime workdate)
        {
            return dataWorkCalendar.Delete(workdate);
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        public long GetCount()
        {
            return dataWorkCalendar.GetCount();
        }

        /// <summary>
        /// 删除一年的记录
        /// </summary>
        public int Delete(int year)
        {
            return dataWorkCalendar.Delete(year);
        }

        /// <summary>
        /// 查询一年所有记录
        /// </summary>
        public List<YJ.Data.Model.WorkCalendar> GetAll(int year, bool cache = true)
        {
            string cacheKey = "WorkCalendar_" + year.ToString();
            if (!cache)
            {
                return dataWorkCalendar.GetAll(year);
            }
            else
            {
                var obj = Cache.IO.Opation.Get(cacheKey);
                if (obj == null)
                {
                    var wc = dataWorkCalendar.GetAll(year);
                    Cache.IO.Opation.Set(cacheKey, wc);
                    return wc;
                }
                else
                {
                    return (List<YJ.Data.Model.WorkCalendar>)obj;
                }
            }
        }

        /// <summary>
        /// 得到几天之后的日期时间(除去休息日)
        /// </summary>
        /// <param name="day"></param>
        /// <param name="dt">在什么时间加上几天</param>
        /// <returns></returns>
        public DateTime GetWorkDate(double day, DateTime dt)
        {
            DateTime dt1 = dt.AddDays(day);
            List<YJ.Data.Model.WorkCalendar> wc = GetAll(dt.Year);
            if (dt.Year != dt1.Year)
            {
                wc.AddRange(GetAll(dt1.Year));
            }
            if (wc == null || wc.Count == 0)
            {
                return dt.AddDays(day);
            }
            DateTime dt2 = dt;
            for (int i = 0; i <= day; i++)
            {
                if (wc.Find(p => p.WorkDate.Year == dt2.Year && p.WorkDate.Month == dt2.Month && p.WorkDate.Day == dt2.Day) == null)
                {
                    day++;
                }
                dt2 = dt2.AddDays(1);
            }
            return dt2.AddDays(-1 + (day - (int)Math.Floor(day)));
        }
    }
}
