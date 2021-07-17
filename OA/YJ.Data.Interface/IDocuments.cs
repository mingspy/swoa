namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using YJ.Data.Model;

    public interface IDocuments
    {
        int Add(Documents model);
        int Delete(Guid id);
        int DeleteByDirectoryID(Guid directoryID);
        Documents Get(Guid id);
        List<Documents> GetAll();
        long GetCount();
        DataTable GetList(out string pager, string dirID, string userID, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(false)] bool isNoRead);
        DataTable GetList(out long count, int size, int number, string dirID, string userID, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue(false)] bool isNoRead, [Optional, DefaultParameterValue("")] string order);
        int Update(Documents model);
        void UpdateReadCount(Guid id);
    }
}

