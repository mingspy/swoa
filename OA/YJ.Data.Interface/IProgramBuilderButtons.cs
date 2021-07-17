namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using YJ.Data.Model;

    public interface IProgramBuilderButtons
    {
        int Add(ProgramBuilderButtons model);
        int Delete(Guid id);
        ProgramBuilderButtons Get(Guid id);
        List<ProgramBuilderButtons> GetAll();
        List<ProgramBuilderButtons> GetAll(Guid id);
        long GetCount();
        int Update(ProgramBuilderButtons model);
    }
}

