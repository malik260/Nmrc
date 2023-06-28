using Mortgage.Ecosystem.DataAccess.Layer.Caching;
using Mortgage.Ecosystem.DataAccess.Layer.Configurations;
using Mortgage.Ecosystem.DataAccess.Layer.Settings;
using Microsoft.Data.SqlClient;

namespace Mortgage.Ecosystem.DataAccess.Layer
{
    public class SystemConfig
    {
        public static Configuration? Instance { get; set; }
        public static SqlConnection? Connection { get; set; }
        public static ApplicationPortal? ApplicationPortal { get; set; }
        public static WebHost? WebHost { get; set; }
        public static Database? Database { get; set; }
        public static FileProvider? FileProvider { get; set; }
        public static GoogleConfig? Google { get; set; }
        public static Jwt? Jwt { get; set; }
        public static CacheConfiguration? CacheConfiguration { get; set; }
        public static AzureAd? AzureAd { get; set; }
        public static string? Origins { get; set; }
        public bool Demo { get; set; } // Whether it is Demo mode        
        public bool Debug { get; set; } // Is it in debug mode
        public bool LoginMultiple { get; set; } // Allows a user to log in on multiple computers at the same time
        public string? LoginProvider { get; set; } // Login provider
        public int SnowFlakeWorkerId { get; set; } // Snowflake ID
        public string? ApiSite { get; set; } // Api address
        public string? VirtualDirectory { get; set; } // Website virtual directory
        public string? CacheProvider { get; set; } // Cache type
        public string? RedisConnectionString { get; set; } // cache string
        public static string? DefaultScheme { get; set; }
        public static SecurityHeaders? SecurityHeaders { get; set; }
        public int DBSlowSqlLogTime { get; set; } // Slow query record Sql (seconds), save to file for analysis
    }
}
