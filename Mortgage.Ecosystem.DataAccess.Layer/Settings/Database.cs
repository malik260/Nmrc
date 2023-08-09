namespace Mortgage.Ecosystem.DataAccess.Layer.Settings
{
    public class Database
    {
        public string? DBProvider { get; set; }
        public string? DBConnectionString { get; set; }
        public string? DBHangfireServer { get; set; }
        public int DBCommandTimeout { get; set; }
        public string? DBBackup { get; set; }
    }
}
