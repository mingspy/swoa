namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using YJ.Data.Model;

    public interface IDBConnection
    {
        int Add(DBConnection model);
        int Delete(Guid id);
        DBConnection Get(Guid id);
        List<DBConnection> GetAll();
        long GetCount();
        int Update(DBConnection model);
    }
}

