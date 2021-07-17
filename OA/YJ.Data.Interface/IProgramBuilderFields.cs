namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using YJ.Data.Model;

    public interface IProgramBuilderFields
    {
        int Add(ProgramBuilderFields model);
        int Delete(Guid id);
        int DeleteByProgramID(Guid id);
        ProgramBuilderFields Get(Guid id);
        List<ProgramBuilderFields> GetAll();
        List<ProgramBuilderFields> GetAll(Guid programID);
        long GetCount();
        int Update(ProgramBuilderFields model);
    }
}

