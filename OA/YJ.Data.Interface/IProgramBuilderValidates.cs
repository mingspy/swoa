namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using YJ.Data.Model;

    public interface IProgramBuilderValidates
    {
        int Add(ProgramBuilderValidates model);
        int Delete(Guid id);
        int DeleteByProgramID(Guid id);
        ProgramBuilderValidates Get(Guid id);
        List<ProgramBuilderValidates> GetAll();
        List<ProgramBuilderValidates> GetAll(Guid programID);
        long GetCount();
        int Update(ProgramBuilderValidates model);
    }
}

