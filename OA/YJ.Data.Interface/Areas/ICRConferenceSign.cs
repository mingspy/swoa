namespace YJ.Data.Interface.Areas
{
    using System;
    using System.Collections.Generic;
    using YJ.Data.Model.Areas;

    public interface ICRConferenceSign
    {
        int Add(CRConferenceSign model);
        int Delete(int id);
        CRConferenceSign Get(int id);
        List<CRConferenceSign> GetAll();
        long GetCount();
        int Update(CRConferenceSign model);
    }
}

