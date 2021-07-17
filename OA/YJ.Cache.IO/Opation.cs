namespace YJ.Cache.IO
{
    using System;
    using YJ.Cache.Factory;

    public class Opation
    {
        public static object Get(string key)
        {
            try
            {
                return Cache.CreateInstance().Get(key);
            }
            catch
            {
                return null;
            }
        }

        public static bool Insert(string key, object obj)
        {
            return Cache.CreateInstance().Insert(key, obj);
        }

        public static bool Insert(string key, object obj, DateTime expiry)
        {
            return Cache.CreateInstance().Insert(key, obj, expiry);
        }

        public static bool Remove(string key)
        {
            return Cache.CreateInstance().Remove(key);
        }

        public static void RemoveAll()
        {
            Cache.CreateInstance().RemoveAll();
        }

        public static bool Set(string key, object obj)
        {
            return Insert(key, obj);
        }

        public static bool Set(string key, object obj, DateTime expiry)
        {
            return Insert(key, obj, expiry);
        }
    }
}

