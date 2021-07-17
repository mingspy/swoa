namespace YJ.Cache.Factory
{
    using System;
    using YJ.Cache.InProc;
    using YJ.Cache.Interface;

    public class Cache
    {
        public static ICache CreateInstance()
        {
            return new YJ.Cache.InProc.Cache();
        }
    }
}

