namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using YJ.Data.Model;

    public interface ILog
    {
        int Add(Log model);
        int Delete(Guid id);
        Log Get(Guid id);
        List<Log> GetAll();
        long GetCount();
        DataTable GetPagerData(out long count, [Optional, DefaultParameterValue(15)] int size, [Optional, DefaultParameterValue(1)] int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string type, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue("")] string userID, [Optional, DefaultParameterValue("")] string order);
        DataTable GetPagerData(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue(15)] int size, [Optional, DefaultParameterValue(1)] int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string type, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue("")] string userID);
        int Update(Log model);
    }
}

