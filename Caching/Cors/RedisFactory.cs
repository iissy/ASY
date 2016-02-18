namespace ASY.Iissy.Caching.Cors
{
    public class RedisFactory<T> : AbstractFactory<T>
    {
        public override T InternalGetCached(string refID)
        {
            return default(T);
            // #TODO
            //return RedisHelpers.KGet<T>(refID);
        }

        public override bool InternalSetCached(string cacheKey, T cached, int minute)
        {
            return true;
            // #TODO
            //return RedisHelpers.KSet<T>(cacheKey, cached, minute);
        }
    }
}