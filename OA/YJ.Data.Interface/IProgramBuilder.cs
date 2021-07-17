namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using YJ.Data.Model;

    public interface IProgramBuilder
    {
        int Add(ProgramBuilder model);
        int Delete(Guid id);
        ProgramBuilder Get(Guid id);
        List<ProgramBuilder> GetAll();
        long GetCount();
        List<ProgramBuilder> GetList(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string name, [Optional, DefaultParameterValue("")] string typeid);
        int Update(ProgramBuilder model);
    }
}

