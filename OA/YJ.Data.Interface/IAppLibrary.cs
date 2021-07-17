namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using YJ.Data.Model;

    public interface IAppLibrary
    {
        int Add(AppLibrary model);
        int Delete(string[] idArray);
        int Delete(Guid id);
        AppLibrary Get(Guid id);
        List<AppLibrary> GetAll();
        List<AppLibrary> GetAllByType(string type);
        AppLibrary GetByCode(string code);
        long GetCount();
        List<AppLibrary> GetPagerData(out long count, [Optional, DefaultParameterValue(15)] int size, [Optional, DefaultParameterValue(1)] int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string type, [Optional, DefaultParameterValue("")] string address, [Optional, DefaultParameterValue("")] string order);
        List<AppLibrary> GetPagerData(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("Type,Title")] string order, [Optional, DefaultParameterValue(15)] int size, [Optional, DefaultParameterValue(1)] int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string type, [Optional, DefaultParameterValue("")] string address);
        int Update(AppLibrary model);
    }
}

