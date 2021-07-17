namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using YJ.Data.Model;

    public interface IProgramBuilderExport
    {
        int Add(ProgramBuilderExport model);
        int Delete(Guid id);
        ProgramBuilderExport Get(Guid id);
        List<ProgramBuilderExport> GetAll();
        List<ProgramBuilderExport> GetAll(Guid programID);
        long GetCount();
        int Update(ProgramBuilderExport model);
    }
}

