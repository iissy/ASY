namespace ASY.Iissy.Caching.Cors
{
    public class NoneFactory<T> : AbstractFactory<T>
    {
        public override T InternalGetCached(string cacheKey)
        {
            object obj = null;
            return (T)obj;
        }

        public override bool InternalSetCached(string cacheKey, T cached, int minute)
        {
            return true;
        }
    }
}