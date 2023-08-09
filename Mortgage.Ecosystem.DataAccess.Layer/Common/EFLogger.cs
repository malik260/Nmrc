using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Mortgage.Ecosystem.DataAccess.Layer.Common
{
    // Executed script log
    public class EFLogger
    {
        // Output to Debug
        public static readonly ILoggerFactory LoggerFactoryDeBug = LoggerFactory.Create(builder => { builder.AddDebug(); });

        // Output to Console
        public static readonly ILoggerFactory loggerFactoryConsole = LoggerFactory.Create(builder => { builder.AddConsole(); });

        // Output Data
        public static void Add(DbContextOptionsBuilder optionsBuilder)
        {
            //Development mode
            if (GlobalConstant.IsDevelopment)
            {
                //controller
                optionsBuilder.UseLoggerFactory(loggerFactoryConsole);
                ////DeBug
                //optionsBuilder.UseLoggerFactory(LoggerFactoryDeBug);
            }
        }
    }
}