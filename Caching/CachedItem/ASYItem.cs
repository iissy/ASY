using System.Collections.Generic;
using ASY.Iissy.Model.Entities;
using ASY.Iissy.Caching.Cors;
using ASY.Iissy.DAL.Repository;

namespace ASY.Iissy.Caching.CachedItem
{
    public class ASYItem : CachedValueRefID<IEnumerable<Article>, ASYItem>
    {
        protected override IEnumerable<Article> GetFromSource()
        {
            int catid = this.Parse<int>(1);
            return ASYRepository.Fun(catid);
        }
    }
}