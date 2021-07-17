namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using YJ.Data.Model;

    public interface IWorkCalendar
    {
        int Add(WorkCalendar model);
        int Delete(DateTime workdate);
        int Delete(int year);
        WorkCalendar Get(DateTime workdate);
        List<WorkCalendar> GetAll();
        List<WorkCalendar> GetAll(int year);
        long GetCount();
        int Update(WorkCalendar model);
    }
}

