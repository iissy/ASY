namespace ASY.Iissy.Caching.Cors
{
    public abstract class AbstractFactory<T>
    {
        public static AbstractFactory<T> GetInstance(CachedOption cachedOption)
        {
            AbstractFactory<T> instance;
            switch (cachedOption)
            {
                case CachedOption.Redis:
                    instance = new RedisFactory<T>();
                    break;
                case CachedOption.Local:
                    instance = new LocalFactory<T>();
                    break;
                default:
                    instance = new NoneFactory<T>();
                    break;
            }

            return instance;
        }

        public abstract T InternalGetCached(string cacheKey);

        public abstract bool InternalSetCached(string cacheKey, T cached, int minute);
    }
}