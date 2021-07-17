namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using YJ.Data.Model;

    public interface IUserShortcut
    {
        int Add(UserShortcut model);
        int Delete(Guid id);
        int DeleteByMenuID(Guid menuID);
        int DeleteByUserID(Guid userID);
        UserShortcut Get(Guid id);
        List<UserShortcut> GetAll();
        List<UserShortcut> GetAllByUserID(Guid userID);
        long GetCount();
        DataTable GetDataTableByUserID(Guid userID);
        int Update(UserShortcut model);
    }
}

