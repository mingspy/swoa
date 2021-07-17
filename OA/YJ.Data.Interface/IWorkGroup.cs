namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using YJ.Data.Model;

    public interface IWorkGroup
    {
        int Add(WorkGroup model);
        int Delete(Guid id);
        WorkGroup Get(Guid id);
        List<WorkGroup> GetAll();
        long GetCount();
        int Update(WorkGroup model);
    }
}

