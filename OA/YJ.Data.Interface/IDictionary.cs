namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using YJ.Data.Model;

    public interface IDictionary
    {
        int Add(Dictionary model);
        int Delete(Guid id);
        Dictionary Get(Guid id);
        List<Dictionary> GetAll();
        Dictionary GetByCode(string code);
        List<Dictionary> GetChilds(Guid id);
        List<Dictionary> GetChilds(string code);
        long GetCount();
        int GetMaxSort(Guid id);
        Dictionary GetParent(Guid id);
        Dictionary GetRoot();
        bool HasChilds(Guid id);
        int Update(Dictionary model);
        int UpdateSort(Guid id, int sort);
    }
}

