namespace YJ.Cache.InProc
{
    using System;
    using System.Threading;
    using System.Web;
    using System.Web.Caching;
    using YJ.Cache.Interface;

    public class Cache : ICache
    {
        private static System.Web.Caching.Cache _cache;
        private static readonly object lockobj = new object();

        public object Get(string key)
        {
            if (cache == null)
            {
                return null;
            }
            return cache.Get(key);
        }

        public bool Insert(string key, object obj)
        {
            if ((obj == null) || (cache == null))
            {
                return false;
            }
            object lockobj = YJ.Cache.InProc.Cache.lockobj;
            bool flag3 = false;
            try
            {
                Monitor.Enter(lockobj, ref flag3);
                cache.Insert(key, obj, null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration);
            }
            finally
            {
                if (flag3)
                {
                    Monitor.Exit(lockobj);
                }
            }
            return true;
        }

        public bool Insert(string key, object obj, DateTime expiry)
        {
            if ((obj == null) || (cache == null))
            {
                return false;
            }
            object lockobj = YJ.Cache.InProc.Cache.lockobj;
            bool flag3 = false;
            try
            {
                Monitor.Enter(lockobj, ref flag3);
                cache.Insert(key, obj, null, expiry, System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            finally
            {
                if (flag3)
                {
                    Monitor.Exit(lockobj);
                }
            }
            return true;
        }

        public bool Remove(string key)
        {
            if (cache == null)
            {
                return false;
            }
            object lockobj = YJ.Cache.InProc.Cache.lockobj;
            bool flag3 = false;
            try
            {
                Monitor.Enter(lockobj, ref flag3);
                cache.Remove(key);
            }
            finally
            {
                if (flag3)
                {
                    Monitor.Exit(lockobj);
                }
            }
            return true;
        }

        public void RemoveAll()
        {
            for (int i = 0; i < cache.Count; i++)
            {
            }
        }

        private static System.Web.Caching.Cache cache
        {
            get
            {
                if (_cache != null)
                {
                    return _cache;
                }
                if (HttpContext.Current != null)
                {
                    _cache = HttpContext.Current.Cache;
                    return _cache;
                }
                return null;
            }
        }
    }
}

