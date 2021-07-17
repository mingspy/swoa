namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using YJ.Data.Model;

    public interface IWorkFlow
    {
        int Add(WorkFlow model);
        int Delete(Guid id);
        WorkFlow Get(Guid id);
        List<WorkFlow> GetAll();
        Dictionary<Guid, string> GetAllIDAndName();
        List<string> GetAllTypes();
        List<WorkFlow> GetByTypes(string typeString);
        long GetCount();
        List<WorkFlow> GetPagerData(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string userid, [Optional, DefaultParameterValue("")] string typeid, [Optional, DefaultParameterValue("")] string name, [Optional, DefaultParameterValue(15)] int pagesize);
        List<WorkFlow> GetPagerData(out long count, int pageSize, int pageNumber, [Optional, DefaultParameterValue("")] string userid, [Optional, DefaultParameterValue("")] string typeid, [Optional, DefaultParameterValue("")] string name, [Optional, DefaultParameterValue("")] string order);
        int Update(WorkFlow model);
    }
}

