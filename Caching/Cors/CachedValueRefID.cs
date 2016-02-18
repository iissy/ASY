using System.Configuration;
using System.Xml.Linq;

namespace ASY.Iissy.Caching.Cors
{
    public class CachedValueRefID<V, T> : CachedValue<V> where T : CachedValueRefID<V, T>, new()
    {
        public static V Get(params object[] paramValues)
        {
            CachedValueRefID<V, T> cached = new T();
            cached.RefID = "[-" + string.Join("-][-", paramValues) + "-]";

            XElement optionExpiresNode = FeatureItem.Get("CachedOptionExpires").Element(cached.GetType().Name);
            string cachedKey = ConfigurationManager.AppSettings["CachedOptionKey"];
            if (optionExpiresNode != null && optionExpiresNode.Attribute("CachedOptionKey") != null)
            {
                cachedKey = optionExpiresNode.Attribute("CachedOptionKey").Value ?? cachedKey;
            }
            if (cachedKey == "Redis")
                cached.CachedOption = CachedOption.Redis;
            else if (cachedKey == "Local")
                cached.CachedOption = CachedOption.Local;
            else
                cached.CachedOption = CachedOption.None;

            V item = cached.InternalGetCached();
            if (item == null)
            {
                if (optionExpiresNode != null)
                {
                    int time = 5;
                    int.TryParse(optionExpiresNode.Value, out time);
                    if (time > 60 || time < 1)
                    {
                        time = 5;
                    }

                    cached.Minute = time;
                }

                item = cached.GetFromSource();
                if (item != null)
                {
                    cached.Value = item;
                    cached.InternalSetCached();
                }
            }

            return item;
        }
    }
}