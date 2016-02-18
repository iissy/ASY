using System;
using System.Runtime.Caching;

namespace ASY.Iissy.Caching.Cors
{
    public class LocalFactory<T> : AbstractFactory<T>
    {
        public override T InternalGetCached(string refID)
        {
            ObjectCache cache = MemoryCache.Default;
            return (T)cache.Get(refID);
        }

        public override bool InternalSetCached(string cacheKey, T cached, int minute)
        {
            ObjectCache cache = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy();
            DateTimeOffset dtOffset = new DateTimeOffset(DateTime.Now.AddTicks(minute * 60L * 10000000L));
            policy.AbsoluteExpiration = dtOffset;
            return cache.Add(cacheKey, cached, policy);
        }
    }
}