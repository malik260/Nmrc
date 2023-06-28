using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace Mortgage.Ecosystem.DataAccess.Layer.Helpers
{
    public class DbHelper
    {
        // Database Provider
        public static DatabaseProvider dbProvider { get; set; }

        #region Constructor
        // Construction method
        public DbHelper(DbConnection _dbConnection)
        {
            dbConnection = _dbConnection;
            dbCommand = dbConnection.CreateCommand();
        }

        public DbHelper(DbContext _dbContext, DbConnection _dbConnection)
        {
            dbContext = _dbContext;
            dbConnection = _dbConnection;
            dbCommand = dbConnection.CreateCommand();
        }
        #endregion

        #region Attributes
        private DbContext dbContext { get; set; }

        // Database connection object
        private DbConnection dbConnection { get; set; }

        // Execute command object
        private DbCommand dbCommand { get; set; }

        // Close database connection
        public void Close()
        {
            if (dbConnection != null)
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }
            if (dbCommand != null)
            {
                dbCommand.Dispose();
            }
        }
        #endregion

        // Execute SQL to return DataReader
        // <param name="cmdType">Type of command</param>
        // <param name="strSql">SQL Statement</param>
        // <param name="dbParameter">Sql Parameter</param>
        // <returns></returns>
        public async Task<IDataReader> ExecuteReadeAsync(CommandType cmdType, string strSql, params DbParameter[] dbParameter)
        {
            try
            {
                if (dbContext == null)
                {
                    PrepareCommand(dbConnection, dbCommand, null, cmdType, strSql, dbParameter);
                    var reader = await dbCommand.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                    return reader;
                }
                else
                {
                    // Compatible with EF Core's DbCommandInterceptor
                    var dependencies = ((IDatabaseFacadeDependenciesAccessor)dbContext.Database).Dependencies;
                    var relationalDatabaseFacade = (IRelationalDatabaseFacadeDependencies)dependencies;
                    var connection = relationalDatabaseFacade.RelationalConnection;
                    var logger = relationalDatabaseFacade.CommandLogger;
                    var commandId = Guid.NewGuid();

                    PrepareCommand(dbConnection, dbCommand, null, cmdType, strSql, dbParameter);

                    var startTime = DateTimeOffset.UtcNow;
                    var stopwatch = Stopwatch.StartNew();

                    var interceptionResult = logger == null
                       ? default
                       : await logger.CommandReaderExecutingAsync(
                           connection,
                           dbCommand,
                           dbContext,
                           Guid.NewGuid(),
                           connection.ConnectionId,
                           startTime,
                           Microsoft.EntityFrameworkCore.Diagnostics.CommandSource.Unknown);

                    var reader = interceptionResult.HasResult
                        ? interceptionResult.Result
                        : await dbCommand.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                    if (logger != null)
                    {
                        reader = await logger.CommandReaderExecutedAsync(
                            connection,
                            dbCommand,
                            dbContext,
                            commandId,
                            connection.ConnectionId,
                            reader,
                            startTime,
                            stopwatch.Elapsed,
                            Microsoft.EntityFrameworkCore.Diagnostics.CommandSource.Unknown);
                    }
                    return reader;
                }
            }
            catch (Exception)
            {
                Close();
                throw;
            }
        }

        // Execute the query and return the result set returned by the query
        // <param name="cmdType">Type of command</param>
        // <param name="strSql">SQL Statement</param>
        // <param name="dbParameter">SqlParameter</param>
        // <returns></returns>
        public async Task<object> ExecuteScalarAsync(CommandType cmdType, string strSql, params DbParameter[] dbParameter)
        {
            try
            {
                if (dbContext == null)
                {
                    PrepareCommand(dbConnection, dbCommand, null, cmdType, strSql, dbParameter);
                    var obj = await dbCommand.ExecuteScalarAsync();
                    dbCommand.Parameters.Clear();
                    return obj;
                }
                else
                {
                    // Compatible with EF Core's DbCommandInterceptor
                    var dependencies = ((IDatabaseFacadeDependenciesAccessor)dbContext.Database).Dependencies;
                    var relationalDatabaseFacade = (IRelationalDatabaseFacadeDependencies)dependencies;
                    var connection = relationalDatabaseFacade.RelationalConnection;
                    var logger = relationalDatabaseFacade.CommandLogger;
                    var commandId = Guid.NewGuid();

                    PrepareCommand(dbConnection, dbCommand, null, cmdType, strSql, dbParameter);

                    var startTime = DateTimeOffset.UtcNow;
                    var stopwatch = Stopwatch.StartNew();

                    var interceptionResult = logger == null
                       ? default
                       : await logger.CommandScalarExecutingAsync(
                           connection,
                           dbCommand,
                           dbContext,
                           Guid.NewGuid(),
                           connection.ConnectionId,
                           startTime,
                           Microsoft.EntityFrameworkCore.Diagnostics.CommandSource.Unknown);

                    var obj = interceptionResult.HasResult
                        ? interceptionResult.Result
                        : await dbCommand.ExecuteScalarAsync();

                    if (logger != null)
                    {
                        obj = await logger.CommandScalarExecutedAsync(
                            connection,
                            dbCommand,
                            dbContext,
                            commandId,
                            connection.ConnectionId,
                            obj,
                            startTime,
                            stopwatch.Elapsed,
                            Microsoft.EntityFrameworkCore.Diagnostics.CommandSource.Unknown);
                    }
                    return obj;
                }
            }
            catch (Exception)
            {
                Close();
                throw;
            }
        }

        // Prepare a command for the upcoming execution
        // <param name="conn">SQLConnection object</param>
        // <param name="cmd">SqlCommand object</param>
        // <param name="isOpenTrans">DbTransaction object</param>
        // <param name="cmdType">Execute Type of command (stored procedure or T-SQL, etc.)</param>
        // <param name="strSql">Stored procedure name or T-SQL command line, e.g. Select * from Products</param>
        // <param name="dbParameter">The sql statement required to execute the command corresponds to the Parameter</param>
        private void PrepareCommand(DbConnection conn, IDbCommand cmd, DbTransaction isOpenTrans, CommandType cmdType, string strSql, params DbParameter[] dbParameter)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = strSql;
            cmd.CommandTimeout = SystemConfig.Database.DBCommandTimeout;
            if (isOpenTrans != null)
            {
                cmd.Transaction = isOpenTrans;
            }
            cmd.CommandType = cmdType;
            if (dbParameter != null)
            {
                cmd.Parameters.Clear();
                dbParameter = DbParameterExtension.ToDbParameter(dbParameter);
                foreach (var parameter in dbParameter)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
        }
    }
}
