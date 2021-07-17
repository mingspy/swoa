namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using YJ.Data.Model;

    public interface IAppLibraryButtons
    {
        int Add(AppLibraryButtons model);
        int Delete(Guid id);
        AppLibraryButtons Get(Guid id);
        List<AppLibraryButtons> GetAll();
        long GetCount();
        int GetMaxSort();
        List<AppLibraryButtons> GetPagerData(out long count, int size, int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string order);
        List<AppLibraryButtons> GetPagerData(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue(15)] int size, [Optional, DefaultParameterValue(1)] int number, [Optional, DefaultParameterValue("")] string title);
        int Update(AppLibraryButtons model);
    }
}

