namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using YJ.Data.Model;

    public interface IMenu
    {
        int Add(Menu model);
        int Delete(Guid id);
        Menu Get(Guid id);
        List<Menu> GetAll();
        List<Menu> GetAllByApplibaryID(Guid applibaryID);
        DataTable GetAllDataTable();
        List<Menu> GetChild(Guid id);
        long GetCount();
        int GetMaxSort(Guid id);
        bool HasChild(Guid id);
        int Update(Menu model);
        int UpdateSort(Guid id, int sort);
    }
}

