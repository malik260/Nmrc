using Mortgage.Ecosystem.DataAccess.Layer.Enums;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories.Base
{
    // Database factory
    public class DbFactory
    {
        // database type
        public static DatabaseProvider Type
        {
            get
            {
                var dbTypeStr = SystemConfig.Database.DBProvider ?? "sqlserver";
                var dbType = dbTypeStr.ToLower() switch
                {
                    "mssql" => DatabaseProvider.SqlServer,
                    "mysql" => DatabaseProvider.MySql,
                    "oracle" => DatabaseProvider.Oracle,
                    _ => throw new Exception("Database configuration not found"),
                };
                return dbType;
            }
        }

        // Database connection string
        public static string Connect
        {
            get
            {
                var dbConnect = SystemConfig.Database.DBConnectionString;
                return dbConnect;
            }
        }

        // The database connection command timed out
        public static int Timeout
        {
            get
            {
                var dbTimeoutStr = SystemConfig.Database.DBCommandTimeout;
                return dbTimeoutStr <= 0 ? 10 : Convert.ToInt32(dbTimeoutStr * 1000);
            }
        }
    }
}