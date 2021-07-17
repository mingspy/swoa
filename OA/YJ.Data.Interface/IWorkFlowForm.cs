namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using YJ.Data.Model;

    public interface IWorkFlowForm
    {
        int Add(WorkFlowForm model);
        int Delete(Guid id);
        WorkFlowForm Get(Guid id);
        List<WorkFlowForm> GetAll();
        List<WorkFlowForm> GetAllByType(string types);
        long GetCount();
        List<WorkFlowForm> GetPagerData(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string userid, [Optional, DefaultParameterValue("")] string typeid, [Optional, DefaultParameterValue("")] string name, [Optional, DefaultParameterValue(15)] int pagesize);
        List<WorkFlowForm> GetPagerData(out long count, int pageSize, int pageNumber, [Optional, DefaultParameterValue("")] string userid, [Optional, DefaultParameterValue("")] string typeid, [Optional, DefaultParameterValue("")] string name, [Optional, DefaultParameterValue("")] string order);
        int Update(WorkFlowForm model);
    }
}

