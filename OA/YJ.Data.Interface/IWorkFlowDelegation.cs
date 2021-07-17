namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using YJ.Data.Model;

    public interface IWorkFlowDelegation
    {
        int Add(WorkFlowDelegation model);
        int Delete(Guid id);
        WorkFlowDelegation Get(Guid id);
        List<WorkFlowDelegation> GetAll();
        List<WorkFlowDelegation> GetByUserID(Guid userID);
        long GetCount();
        List<WorkFlowDelegation> GetNoExpiredList();
        List<WorkFlowDelegation> GetPagerData(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string userID, [Optional, DefaultParameterValue("")] string startTime, [Optional, DefaultParameterValue("")] string endTime);
        List<WorkFlowDelegation> GetPagerData(out long count, int pageSize, int pageNumber, [Optional, DefaultParameterValue("")] string userID, [Optional, DefaultParameterValue("")] string startTime, [Optional, DefaultParameterValue("")] string endTime, [Optional, DefaultParameterValue("")] string order);
        int Update(WorkFlowDelegation model);
    }
}

