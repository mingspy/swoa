namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using YJ.Data.Model;

    public interface IDocumentsReadUsers
    {
        int Add(DocumentsReadUsers model);
        int Delete(Guid documentid);
        int Delete(Guid documentid, Guid userid);
        DocumentsReadUsers Get(Guid documentid, Guid userid);
        List<DocumentsReadUsers> GetAll();
        long GetCount();
        int Update(DocumentsReadUsers model);
        void UpdateRead(Guid docID, Guid userID);
    }
}

