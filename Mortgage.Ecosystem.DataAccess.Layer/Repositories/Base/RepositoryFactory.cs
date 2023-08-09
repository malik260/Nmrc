using Microsoft.Data.SqlClient;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Exceptions;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Base;
using Mortgage.Ecosystem.DataAccess.Layer.Models;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories.Base
{
    public class RepositoryFactory
    {
        private static string _dbConnectionString = string.Empty;

        public static DbRepository BaseRepository()
        {
            IDbRepository? dbRepository;
            if (SystemConfig.Connection != null)
            {
                DbHelper.dbProvider = SystemConfig.Instance.DatabaseProvider;
                _dbConnectionString = SystemConfig.Database.DBConnectionString;
                dbRepository = new DbRepository(CreateContext());
            }
            else
            {
                string dbProvider = SystemConfig.Database.DBProvider.ToLower();
                DbHelper.dbProvider = dbProvider switch
                {
                    "mssql" => DatabaseProvider.SqlServer,
                    "mysql" => DatabaseProvider.MySql,
                    "oracle" => DatabaseProvider.Oracle,
                    _ => throw new Exception("Database configuration not found"),
                };
                _dbConnectionString = SystemConfig.Database.DBConnectionString;
                dbRepository = new DbRepository(CreateContext());
            }
            return (DbRepository)dbRepository;
        }

        private static ApplicationDbContext CreateContext()
        {
            //CheckDbConnection();

            //var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            //    .UseSqlServer(dbConnectionString).Options;

            return new ApplicationDbContext();
        }

        private static SqlConnection CreateConnection()
        {
            CheckConnectionString();

            var connection = new SqlConnection(_dbConnectionString);

            return connection;
        }

        internal static void CheckDbConnection()
        {
            var connection = CreateConnection();
            SystemConfig.Connection = connection;

            connection.OpenAsync();
            connection.CloseAsync();
        }

        private static void CheckConnectionString()
        {
            if (string.IsNullOrWhiteSpace(_dbConnectionString))
            {
                throw new ConnectionStringException("The connection string has not been set.");
            }
        }
    }
}