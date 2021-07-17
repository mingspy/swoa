namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using YJ.Data.Model;

    public interface IMenuUser
    {
        int Add(MenuUser model);
        int Delete(Guid id);
        int DeleteByMenuID(Guid menuID);
        int DeleteByOrganizes(string organizes);
        MenuUser Get(Guid id);
        List<MenuUser> GetAll();
        long GetCount();
        int Update(MenuUser model);
    }
}

