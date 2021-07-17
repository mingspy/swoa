namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using YJ.Data.Model;

    public interface ILoginBlacklist
    {
        int Add(LoginBlacklist model);
        int Delete(Guid id);
        LoginBlacklist Get(Guid id);
        List<LoginBlacklist> GetAll();
        LoginBlacklist GetByIPaddress(string IPaddress);
        long GetCount();
        DataTable GetPagerData(out long count, [Optional, DefaultParameterValue(15)] int size, [Optional, DefaultParameterValue(1)] int number, [Optional, DefaultParameterValue("")] string ID, [Optional, DefaultParameterValue("")] string IPaddress, [Optional, DefaultParameterValue("")] string account, [Optional, DefaultParameterValue("")] string blocktime, [Optional, DefaultParameterValue("")] string order);
        DataTable GetPagerData(out string pager, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue(15)] int size, [Optional, DefaultParameterValue(1)] int number, [Optional, DefaultParameterValue("")] string ID, [Optional, DefaultParameterValue("")] string IPaddress, [Optional, DefaultParameterValue("")] string account, [Optional, DefaultParameterValue("")] string blocktime);
        int Update(LoginBlacklist model);

    }
}

