using System;
using ASY.Iissy.Util.SqlHelpers;

namespace ASY.Iissy.DAL.Repository
{
    public static class ASYRepository
    {
        public static string Fun()
        {
            return "Hello World!";//SqlHelpers.ExecuteScalar(null, "");
        }
    }
}