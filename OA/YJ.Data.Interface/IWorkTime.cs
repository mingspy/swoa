namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using YJ.Data.Model;

    public interface IWorkTime
    {
        int Add(WorkTime model);
        int Delete(Guid id);
        WorkTime Get(Guid id);
        List<WorkTime> GetAll();
        List<WorkTime> GetAll(int year);
        List<int> GetAllYear();
        long GetCount();
        int Update(WorkTime model);
    }
}

