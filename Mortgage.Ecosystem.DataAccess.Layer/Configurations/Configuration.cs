using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Exceptions;
using Microsoft.Data.SqlClient;

namespace Mortgage.Ecosystem.DataAccess.Layer.Configurations
{
    public class Configuration
    {
        private string? _installLocation;
        private string? _connectionString;
        private GoogleConfig? _googleConfig;
        private DatabaseProvider _databaseProvider;

        public static bool CheckConfiguration(bool testConnection = false)
        {
            if (SystemConfig.Instance == null)
            {
                throw new ConfigurationException("The configuration has not been set.");
            }

            if (testConnection)
            {
                try
                {
                    TestConnection(SystemConfig.Instance.ConnectionString);
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        public static void CreateInstance(string? databaseProvider, string? connectionString)
        {
            if (SystemConfig.Instance != null)
            {
                throw new ConfigurationException("A configuration has already been added.");
            }

            var databaseProviderValue = (databaseProvider?.ToLower()) switch
            {
                "mssql" => DatabaseProvider.SqlServer,
                "mysql" => DatabaseProvider.MySql,
                "oracle" => DatabaseProvider.Oracle,
                _ => throw new ArgumentException($"The database provider '{databaseProvider}' is invalid.",
                                        nameof(databaseProvider)),
            };

            //TestConnection(connectionString);

            SystemConfig.Instance = new Configuration();

            SystemConfig.Instance.DatabaseProvider = databaseProviderValue;
            SystemConfig.Instance.ConnectionString = connectionString;
        }

        private static void TestConnection(string? connectionString)
        {
            var connection = new SqlConnection(connectionString);
            SystemConfig.Connection = connection;

            connection.Open();
            connection.Close();
        }

        public string? InstallLocation
        {
            get { return _installLocation; }
            set { _installLocation = value; }
        }

        public DatabaseProvider DatabaseProvider
        {
            get { return _databaseProvider; }
            private set
            {
                _databaseProvider = value;
            }
        }

        public string? ConnectionString
        {
            get { return _connectionString; }
            private set
            {
                //TestConnection(value);
                _connectionString = value;
            }
        }

        public GoogleConfig? GoogleConfig
        {
            get { return _googleConfig; }
            set { _googleConfig = value; }
        }
    }
}
