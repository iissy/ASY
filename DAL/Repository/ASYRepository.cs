using System.Collections.Generic;
using ASY.Iissy.Util.SqlHelpers;
using ASY.Iissy.Model.Entities;

namespace ASY.Iissy.DAL.Repository
{
    public static class ASYRepository
    {
        public static IEnumerable<Article> Fun(int catid)
        {
            return SqlHelpers.ExecuteEntity<Article>(null, "MyProc", catid);
        }
    }
}