namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using YJ.Data.Model;

    public interface IHomeItems
    {
        int Add(HomeItems model);
        int Delete(Guid id);
        HomeItems Get(Guid id);
        List<HomeItems> GetAll();
        long GetCount();
        List<HomeItems> GetList(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string name, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string type);
        List<HomeItems> GetList(out long count, int size, int number, [Optional, DefaultParameterValue("")] string name, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string type, [Optional, DefaultParameterValue("")] string order);
        int GetMaxSort(int type);
        int Update(HomeItems model);
    }
}

