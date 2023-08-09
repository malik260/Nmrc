using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;

namespace Mortgage.Ecosystem.DataAccess.Layer.Extensions
{
    public static class ExtensionSqlQuery
    {
        public static async Task<IList<T>> SqlQuery<T>(this DbContext db, string sql, params object[] parameters) where T : class
        {
            using (var db2 = new ContextForQueryType<T>(db.Database.GetDbConnection()))
            {
                return await db2.Set<T>().FromSqlRaw(sql, parameters).ToListAsync();
            }
        }

        private class ContextForQueryType<T> : DbContext where T : class
        {
            private readonly DbConnection connection;

            public ContextForQueryType(DbConnection connection)
            {
                this.connection = connection;
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                // switch on the connection type name to enable support multiple providers
                string dbType = SystemConfig.Database?.DBProvider.ToLower();
                switch (dbType)
                {
                    case "mssql":
                        optionsBuilder.UseSqlServer(connection, options => options.EnableRetryOnFailure());
                        break;
                    case "mySql":
                        optionsBuilder.UseMySql(connection, ServerVersion.AutoDetect(connection.ConnectionString), options => options.EnableRetryOnFailure());
                        break;
                    case "oracle":
                        optionsBuilder.UseOracle(connection, options =>
                        {
                            //if (dbVersion != 0) options.UseOracleSQLCompatibility($"{dbVersion}");
                            //options.CommandTimeout(dbTimeout);
                        });
                        break;
                    default: throw new Exception("Database configuration not found");
                }
                base.OnConfiguring(optionsBuilder);
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<T>(p => { p.HasNoKey(); });
                base.OnModelCreating(modelBuilder);
            }
        }
    }
}