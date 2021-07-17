namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using YJ.Data.Model;

    public interface IHastenLog
    {
        int Add(HastenLog model);
        int Delete(Guid id);
        HastenLog Get(Guid id);
        List<HastenLog> GetAll();
        long GetCount();
        int Update(HastenLog model);
    }
}

