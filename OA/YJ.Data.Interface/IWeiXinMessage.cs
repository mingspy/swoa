namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using YJ.Data.Model;

    public interface IWeiXinMessage
    {
        int Add(WeiXinMessage model);
        int Delete(Guid id);
        WeiXinMessage Get(Guid id);
        List<WeiXinMessage> GetAll();
        long GetCount();
        int Update(WeiXinMessage model);
    }
}

