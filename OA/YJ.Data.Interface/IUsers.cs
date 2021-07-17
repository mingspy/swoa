namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using YJ.Data.Model;

    public interface IUsers
    {
        int Add(Users model);
        int Delete(Guid id);
        Users Get(Guid id);
        List<Users> GetAll();
        List<Users> GetAllByIDString(string idString);
        List<Users> GetAllByOrganizeID(Guid organizeID);
        List<Users> GetAllByOrganizeIDArray(Guid[] organizeIDArray);
        List<Users> GetAllByWorkGroupID(Guid workgroupid);
        Users GetByAccount(string account);
        long GetCount();
        bool HasAccount(string account, [Optional, DefaultParameterValue("")] string userID);
        int Update(Users model);
        bool UpdatePassword(string password, Guid userID);
        int UpdateSort(Guid userID, int sort);
    }
}

