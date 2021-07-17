namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using YJ.Data.Model;

    public interface IWorkFlowComment
    {
        int Add(WorkFlowComment model);
        int Delete(Guid id);
        WorkFlowComment Get(Guid id);
        List<WorkFlowComment> GetAll();
        long GetCount();
        List<WorkFlowComment> GetManagerAll();
        int GetManagerMaxSort();
        int GetUserMaxSort(Guid userID);
        int Update(WorkFlowComment model);
    }
}

