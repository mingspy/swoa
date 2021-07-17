namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using YJ.Data.Model;

    public interface IUsersRelation
    {
        int Add(UsersRelation model);
        int Delete(Guid userid, Guid organizeid);
        int DeleteByOrganizeID(Guid organizeID);
        int DeleteByUserID(Guid userID);
        int DeleteNotIsMainByUserID(Guid userID);
        UsersRelation Get(Guid userid, Guid organizeid);
        List<UsersRelation> GetAll();
        List<UsersRelation> GetAllByOrganizeID(Guid organizeID);
        List<UsersRelation> GetAllByUserID(Guid userID);
        long GetCount();
        UsersRelation GetMainByUserID(Guid userID);
        int GetMaxSort(Guid organizeID);
        int Update(UsersRelation model);
    }
}

