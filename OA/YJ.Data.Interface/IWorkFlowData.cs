namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using YJ.Data.Model;

    public interface IWorkFlowData
    {
        int Add(WorkFlowData model);
        int Delete(Guid id);
        WorkFlowData Get(Guid id);
        List<WorkFlowData> GetAll();
        List<WorkFlowData> GetAll(Guid instanceID);
        long GetCount();
        int Update(WorkFlowData model);
    }
}

