using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ASY.Iissy.Util.SqlHelpers
{
    internal class DBFactory
    {
        private static int TIMEOUTSECONDS = 60;

        public static Database Create(string databaseName)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            return string.IsNullOrWhiteSpace(databaseName) ? factory.CreateDefault() : factory.Create(databaseName);
        }

        public static DbCommand CreateDbCommand(Database db, string storedProcName, params object[] paramValues)
        {
            DbCommand cmd = (paramValues.Length <= 0) ? db.GetStoredProcCommand(storedProcName) : db.GetStoredProcCommand(storedProcName, paramValues);
            cmd.CommandTimeout = TIMEOUTSECONDS;
            return cmd;
        }
    }
}