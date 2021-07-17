namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using YJ.Data.Model;

    public interface IDocumentDirectory
    {
        int Add(DocumentDirectory model);
        int Delete(Guid id);
        DocumentDirectory Get(Guid id);
        List<DocumentDirectory> GetAll();
        List<DocumentDirectory> GetChilds(Guid id);
        long GetCount();
        int GetMaxSort(Guid dirID);
        int Update(DocumentDirectory model);
    }
}

