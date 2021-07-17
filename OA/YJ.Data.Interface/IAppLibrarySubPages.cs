namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using YJ.Data.Model;

    public interface IAppLibrarySubPages
    {
        int Add(AppLibrarySubPages model);
        int Delete(Guid id);
        int DeleteByAppID(Guid id);
        AppLibrarySubPages Get(Guid id);
        List<AppLibrarySubPages> GetAll();
        List<AppLibrarySubPages> GetAllByAppID(Guid id);
        long GetCount();
        int Update(AppLibrarySubPages model);
    }
}

