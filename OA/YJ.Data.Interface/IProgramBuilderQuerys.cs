namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using YJ.Data.Model;

    public interface IProgramBuilderQuerys
    {
        int Add(ProgramBuilderQuerys model);
        int Delete(Guid id);
        int DeleteByProgramID(Guid id);
        ProgramBuilderQuerys Get(Guid id);
        List<ProgramBuilderQuerys> GetAll();
        List<ProgramBuilderQuerys> GetAll(Guid programID);
        long GetCount();
        int Update(ProgramBuilderQuerys model);
    }
}

