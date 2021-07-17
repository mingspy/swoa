namespace YJ.Cache.Interface
{
    using System;

    public interface ICache
    {
        object Get(string key);
        bool Insert(string key, object obj);
        bool Insert(string key, object obj, DateTime expiry);
        bool Remove(string key);
        void RemoveAll();
    }
}

