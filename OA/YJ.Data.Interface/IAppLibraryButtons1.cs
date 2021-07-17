namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using YJ.Data.Model;

    public interface IAppLibraryButtons1
    {
        int Add(AppLibraryButtons1 model);
        int Delete(Guid id);
        int DeleteByAppID(Guid id);
        AppLibraryButtons1 Get(Guid id);
        List<AppLibraryButtons1> GetAll();
        List<AppLibraryButtons1> GetAllByAppID(Guid id);
        long GetCount();
        int Update(AppLibraryButtons1 model);
    }
}

