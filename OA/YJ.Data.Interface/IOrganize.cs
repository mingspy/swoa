namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using YJ.Data.Model;

    public interface IOrganize
    {
        int Add(Organize model);
        int Delete(Guid id);
        Organize Get(Guid id);
        List<Organize> GetAll();
        List<Organize> GetAllChild(string number);
        List<Organize> GetAllParent(string number);
        List<Organize> GetChilds(Guid ID);
        long GetCount();
        int GetMaxSort(Guid id);
        Organize GetRoot();
        int Update(Organize model);
        int UpdateChildsLength(Guid id, int length);
        int UpdateSort(Guid id, int sort);
    }
}

