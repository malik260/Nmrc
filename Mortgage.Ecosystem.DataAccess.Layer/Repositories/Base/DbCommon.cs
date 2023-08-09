using Microsoft.EntityFrameworkCore;
using Mortgage.Ecosystem.DataAccess.Layer.Common;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Interceptor;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories.Base
{
    // <b>Database connection object</b>
    //
    // <para>General usage: using var dbComm = new DbContext()</para>
    // <para>Injection use: services.AddDbContext&lt;DbContext&gt;()</para>
    // <para>Inheriting this object enables native operations! by zgcwkj</para>
    public class DbCommon : DbContext, IDisposable
    {
        // database type
        private DatabaseProvider dbType { get; }

        // Connection character
        private string dbConnect { get; }

        // Connection timed out
        private int dbTimeout { get; }

        // Database version
        private int dbVersion { get; }

        // <para>Database connection object</para>
        // <para><b>Use the information in the configuration file to connect</b></para>
        public DbCommon()
        {
            this.dbType = DbFactory.Type;
            this.dbConnect = DbFactory.Connect;
            this.dbTimeout = DbFactory.Timeout;
            if (!string.IsNullOrEmpty(Regex.Match(DbFactory.Connect, "version=.+?;").Value))
            {
                this.dbConnect = DbFactory.Connect.Replace(Regex.Match(DbFactory.Connect, "version=.+?;").Value, "");
                this.dbVersion = Convert.ToInt32(Regex.Match(DbFactory.Connect, "(?<=version=).+?(?=;)").Value);
            }
        }

        // <para>Database connection object</para>
        // <para><b>Use custom configuration to connect</b></para>
        // <para>Usage: base(DbType, "SQLConnect")</para>
        // <param name="dbType">Database Type</param>
        // <param name="dbConnect">connection character</param>
        // <param name="dbTimeout">Connection timeout</param>
        public DbCommon(DatabaseProvider dbType, string dbConnect, int dbTimeout = 10)
        {
            this.dbType = dbType;
            this.dbConnect = dbConnect;
            this.dbTimeout = dbTimeout == 10 ? dbTimeout : DbFactory.Timeout;
            if (!string.IsNullOrEmpty(Regex.Match(dbConnect, "version=.+?;").Value))
            {
                this.dbConnect = dbConnect.Replace(Regex.Match(dbConnect, "version=.+?;").Value, "");
                this.dbVersion = Convert.ToInt32(Regex.Match(dbConnect, "(?<=version=).+?(?=;)").Value);
            }
        }

        // <para>Database connection object</para>
        // <para><b>Use custom configuration to connect, same database type</b></para>
        // <para>Usage: base("SQLConnect")</para>
        // <param name="dbConnect">connection character</param>
        // <param name="dbTimeout">Connection timeout</param>
        public DbCommon(string dbConnect, int dbTimeout = 10)
        {
            this.dbType = DbFactory.Type;
            this.dbConnect = dbConnect;
            this.dbTimeout = dbTimeout == 10 ? dbTimeout : DbFactory.Timeout;
            if (!string.IsNullOrEmpty(Regex.Match(dbConnect, "version=.+?;").Value))
            {
                this.dbConnect = dbConnect.Replace(Regex.Match(dbConnect, "version=.+?;").Value, "");
                this.dbVersion = Convert.ToInt32(Regex.Match(dbConnect, "(?<=version=).+?(?=;)").Value);
            }
        }

        // Configure the database to use
        // <param name="optionsBuilder">Context Options Builder</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //SqlServer
            if (dbType == DatabaseProvider.SqlServer)
            {
                optionsBuilder.UseSqlServer(dbConnect, p =>
                {
                    p.CommandTimeout(dbTimeout);
                });
            }
            //MySql
            else if (dbType == DatabaseProvider.MySql)
            {
                optionsBuilder.UseMySql(dbConnect, ServerVersion.AutoDetect(dbConnect), p =>
                {
                    p.CommandTimeout(dbTimeout);
                });
            }
            //Oracle
            else if (dbType == DatabaseProvider.Oracle)
            {
                optionsBuilder.UseOracle(dbConnect, p =>
                {
                    if (dbVersion != 0) p.UseOracleSQLCompatibility($"{dbVersion}");
                    p.CommandTimeout(dbTimeout);
                });
            }
            //database interceptor
            optionsBuilder.AddInterceptors(new DbInterceptor());
            // output log
            EFLogger.Add(optionsBuilder);
            //
            base.OnConfiguring(optionsBuilder);
        }

        // Configure the model discovered by convention
        // <param name="modelBuilder">Model Builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // extract all models
            var filePath = GlobalConstant.GetRunPath;
            var root = new DirectoryInfo(filePath);
            var files = root.GetFiles("*.dll");
            foreach (var file in files)
            {
                try
                {
                    if (file.FullName.Contains("Microsoft")) continue;
                    if (file.FullName.Contains("System")) continue;
                    var fileName = file.Name.Replace(file.Extension, "");
                    var assemblyName = new AssemblyName(fileName);
                    var entityAssembly = Assembly.Load(assemblyName);
                    var typesToRegister = entityAssembly.GetTypes()
                        .Where(p => !string.IsNullOrEmpty(p.Namespace))
                        .Where(p => !string.IsNullOrEmpty(p.GetCustomAttribute<TableAttribute>()?.Name));
                    foreach (var type in typesToRegister)
                    {
                        var createInstance = Activator.CreateInstance(type);
                        modelBuilder.Model.AddEntityType(type);
                    }
                }
                catch { }
            }
            //Set the primary key to ID
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                PrimaryKeyConvention.SetPrimaryKey(modelBuilder, entity.Name);
                var currentTableName = modelBuilder.Entity(entity.Name).Metadata.GetTableName();
                modelBuilder.Entity(entity.Name).ToTable(currentTableName);
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}