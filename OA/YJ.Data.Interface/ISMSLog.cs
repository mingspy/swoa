namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using YJ.Data.Model;

    public interface ISMSLog
    {
        int Add(SMSLog model);
        int Delete(Guid id);
        SMSLog Get(Guid id);
        List<SMSLog> GetAll();
        long GetCount();
        int Update(SMSLog model);
    }
}

