namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using YJ.Data.Model;

    public interface IWorkFlowArchives
    {
        int Add(WorkFlowArchives model);
        int Delete(Guid id);
        WorkFlowArchives Get(Guid id);
        List<WorkFlowArchives> GetAll();
        long GetCount();
        DataTable GetPagerData(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string flowIDString);
        DataTable GetPagerData(out long count, int pageSize, int pageNumber, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string flowIDString, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue("")] string order);
        int Update(WorkFlowArchives model);
    }
}

