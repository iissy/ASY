using System.Collections.Generic;
using ASY.Iissy.BLL.IService;
using ASY.Iissy.Model.Entities;
using ASY.Iissy.Caching.CachedItem;

namespace ASY.Iissy.BLL.Service
{
    public class ASYService : IASYService
    {
        public IEnumerable<Article> Fun(int catid)
        {
            return ASYItem.Get(catid);
        }
    }
}