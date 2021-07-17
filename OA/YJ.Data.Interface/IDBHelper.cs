namespace YJ.Data.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface IDBHelper
    {
        int Execute(List<string> sqlList);
        int Execute(string sql);
        string ExecuteScalar(string sql);
        DataSet GetDataSet(string sql);
        DataTable GetDataTable(string sql);
        string GetFieldValue(string sql);
    }
}

