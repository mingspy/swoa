namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using YJ.Data.Model;

    public interface IShortMessage
    {
        int Add(ShortMessage model);
        int Delete(Guid id);
        int Delete(string linkID, int Type);
        ShortMessage Get(Guid id);
        List<ShortMessage> GetAll();
        List<ShortMessage> GetAllNoRead();
        List<ShortMessage> GetAllNoReadByUserID(Guid userID);
        long GetCount();
        List<ShortMessage> GetList(int[] status, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string contents, [Optional, DefaultParameterValue("")] string senderID, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue("")] string receiveID, [Optional, DefaultParameterValue("")] string order);
        List<ShortMessage> GetList(out string pager, int[] status, [Optional, DefaultParameterValue("")] string query, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string contents, [Optional, DefaultParameterValue("")] string senderID, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue("")] string receiveID);
        List<ShortMessage> GetList(out long count, int[] status, int size, int number, [Optional, DefaultParameterValue("")] string title, [Optional, DefaultParameterValue("")] string contents, [Optional, DefaultParameterValue("")] string senderID, [Optional, DefaultParameterValue("")] string date1, [Optional, DefaultParameterValue("")] string date2, [Optional, DefaultParameterValue("")] string receiveID, [Optional, DefaultParameterValue("")] string order);
        ShortMessage GetRead(Guid id);
        int Update(ShortMessage model);
        int UpdateStatus(Guid id);
    }
}

