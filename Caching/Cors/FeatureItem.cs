using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Caching;
using System.Xml.Linq;

namespace ASY.Iissy.Caching.Cors
{
    public static class FeatureItem
    {
        public static XElement Get(string nodeName)
        {
            string cacheKey = "Global.FeatureItem.Config";
            ObjectCache cache = MemoryCache.Default;
            XElement feature = (XElement)cache.Get(cacheKey);
            if (feature == null)
            {
                string configDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");
                string path = Path.Combine(configDir, "Features.config");
                feature = XElement.Load(path);

                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddHours(24);

                IList<string> filePaths = new List<string>();
                filePaths.Add(path);
                policy.ChangeMonitors.Add(new HostFileChangeMonitor(filePaths));

                cache.Add(cacheKey, feature, policy);
            }

            return feature.Element(nodeName);
        }
    }
}