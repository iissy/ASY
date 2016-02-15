using System;
using ASY.Iissy.Util.SqlHelpers;

namespace ASY.Iissy.DAL.Repository
{
    public static class ASYRepository
    {
        public static object Fun()
        {
            return SqlHelpers.ExecuteScalar(null, "");
        }
    }
}