namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using YJ.Data.Model;

    public interface IWorkFlowButtons
    {
        int Add(WorkFlowButtons model);
        int Delete(Guid id);
        WorkFlowButtons Get(Guid id);
        List<WorkFlowButtons> GetAll();
        long GetCount();
        int GetMaxSort();
        int Update(WorkFlowButtons model);
    }
}

