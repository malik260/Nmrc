﻿namespace Mortgage.Ecosystem.BusinessLogic.Layer.Helpers
{
    internal class DataConnectionFactory
    {
        //private static Func<CacheTechniques, ICacheService> cacheService;

        //private static ApplicationDbContext CreateContext()
        //{
        //    CheckConnectionString();

        //    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //        .UseSqlServer(Configuration.Instance.ConnectionString).Options;

        //    return new ApplicationDbContext(options);
        //}

        //private static SqlConnection CreateConnection()
        //{
        //    CheckConnectionString();

        //    var connection = new SqlConnection(Configuration.Instance.ConnectionString);

        //    return connection;
        //}

        //internal static async Task CheckDbConnection()
        //{
        //    var connection = CreateConnection();

        //    await connection.OpenAsync();
        //    await connection.CloseAsync();
        //}

        //internal static async Task<IUnitOfWork> CreateUnitOfWork()
        //{
        //    var context = CreateContext();

        //    return await UnitOfWork.Create(context, cacheService);
        //}

        //private static void CheckConnectionString()
        //{
        //    if (string.IsNullOrWhiteSpace(Configuration.Instance.ConnectionString))
        //    {
        //        throw new ConnectionStringException("The connection string has not been set.");
        //    }
        //}
    }
}
