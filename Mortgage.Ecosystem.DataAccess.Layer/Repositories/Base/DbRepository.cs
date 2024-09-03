﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Base;
using Mortgage.Ecosystem.DataAccess.Layer.Models;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Text;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories.Base
{
    public class DbRepository : IDbRepository
    {
        private readonly ApplicationDbContext _context;

        // Transaction object
        public IDbContextTransaction? _dbContextTransaction { get; set; }

        #region Constructor

        // Construction method
        public DbRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Transaction commit

        // Business begins
        public async Task<IDbRepository> BeginTrans()
        {
            if (SystemConfig.Connection?.State == ConnectionState.Closed)
            {
                await SystemConfig.Connection.OpenAsync();
            }
            _dbContextTransaction = await _context.Database.BeginTransactionAsync();
            return this;
        }

        // Submit the result of the current operation
        public async Task<int> CommitTrans()
        {
            try
            {
                DbContextExtension.SetEntityDefaultValue(_context);

                int returnValue = await _context.SaveChangesAsync();
                if (_dbContextTransaction != null)
                {
                    await _dbContextTransaction.CommitAsync();
                    await this.Close();
                }
                else
                {
                    await this.Close();
                }
                return returnValue;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (_dbContextTransaction == null)
                {
                    await this.Close();
                }
            }
        }

        // Roll back the current operation to an uncommitted state
        public async Task RollbackTrans()
        {
            await _dbContextTransaction.RollbackAsync();
            await _dbContextTransaction.DisposeAsync();
            await Close();
        }

        // Close connection memory reclamation
        public async Task Close()
        {
            await _context.DisposeAsync();
        }

        #endregion Transaction commit

        #region Execute SQL statement

        public async Task<int> ExecuteBySql(string strSql)
        {
            if (_dbContextTransaction == null)
            {
                return await _context.Database.ExecuteSqlRawAsync(strSql);
            }
            else
            {
                await _context.Database.ExecuteSqlRawAsync(strSql);
                return _dbContextTransaction == null ? await this.CommitTrans() : 0;
            }
        }

        public async Task<int> ExecuteBySql(string strSql, params DbParameter[] dbParameter)
        {
            if (_dbContextTransaction == null)
            {
                return await _context.Database.ExecuteSqlRawAsync(strSql, dbParameter);
            }
            else
            {
                await _context.Database.ExecuteSqlRawAsync(strSql, dbParameter);
                return _dbContextTransaction == null ? await this.CommitTrans() : 0;
            }
        }

        public async Task<int> ExecuteByProc(string procName)
        {
            if (_dbContextTransaction == null)
            {
                return await _context.Database.ExecuteSqlRawAsync(DbContextExtension.BuilderProc(procName));
            }
            else
            {
                await _context.Database.ExecuteSqlRawAsync(DbContextExtension.BuilderProc(procName));
                return _dbContextTransaction == null ? await this.CommitTrans() : 0;
            }
        }

        public async Task<int> ExecuteByProc(string procName, params DbParameter[] dbParameter)
        {
            if (_dbContextTransaction == null)
            {
                return await _context.Database.ExecuteSqlRawAsync(DbContextExtension.BuilderProc(procName, dbParameter), dbParameter);
            }
            else
            {
                await _context.Database.ExecuteSqlRawAsync(DbContextExtension.BuilderProc(procName, dbParameter), dbParameter);
                return _dbContextTransaction == null ? await this.CommitTrans() : 0;
            }
        }

        #endregion Execute SQL statement

        #region Object entity Add, modify, delete

        public async Task<int> Insert<T>(T entity) where T : class
        {
            _context.Entry<T>(entity).State = EntityState.Added;
            return _dbContextTransaction == null ? await this.CommitTrans() : 0;
        }

        public async Task<int> Insert<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                _context.Entry<T>(entity).State = EntityState.Added;
            }
            return _dbContextTransaction == null ? await this.CommitTrans() : 0;
        }

        public async Task<int> Delete<T>() where T : class
        {
            IEntityType entityType = DbContextExtension.GetEntityType<T>(_context);
            if (entityType != null)
            {
                string tableName = entityType.GetTableName();
                return await this.ExecuteBySql(DbContextExtension.DeleteSql(tableName));
            }
            return -1;
        }

        public async Task<int> Delete<T>(T entity) where T : class
        {
            _context.Set<T>().Attach(entity);
            _context.Set<T>().Remove(entity);
            return _dbContextTransaction == null ? await this.CommitTrans() : 0;
        }

        public async Task<int> Delete<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                _context.Set<T>().Attach(entity);
                _context.Set<T>().Remove(entity);
            }
            return _dbContextTransaction == null ? await this.CommitTrans() : 0;
        }

        public async Task<int> Delete<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            IEnumerable<T> entities = await _context.Set<T>().Where(condition).ToListAsync();
            return entities.Count() > 0 ? await Delete(entities) : 0;
        }

        public async Task<int> Delete<T>(long keyValue) where T : class
        {
            IEntityType entityType = DbContextExtension.GetEntityType<T>(_context);
            if (entityType != null)
            {
                string tableName = entityType.GetTableName();
                string keyField = "Id";
                return await this.ExecuteBySql(DbContextExtension.DeleteSql(tableName, keyField, keyValue));
            }
            return -1;
        }

        public async Task<int> Delete<T>(long[] keyValue) where T : class
        {
            IEntityType entityType = DbContextExtension.GetEntityType<T>(_context);
            if (entityType != null)
            {
                string tableName = entityType.GetTableName();
                string keyField = "Id";
                return await this.ExecuteBySql(DbContextExtension.DeleteSql(tableName, keyField, keyValue));
            }
            return -1;
        }

        public async Task<int> Delete<T>(string propertyName, long propertyValue) where T : class
        {
            IEntityType entityType = DbContextExtension.GetEntityType<T>(_context);
            if (entityType != null)
            {
                string tableName = entityType.GetTableName();
                return await this.ExecuteBySql(DbContextExtension.DeleteSql(tableName, propertyName, propertyValue));
            }
            return -1;
        }

        public async Task<int> Update<T>(T entity) where T : class
        {
            _context.Set<T>().Attach(entity);
            Hashtable props = DatabasesExtension.GetPropertyInfo<T>(entity);
            foreach (string item in props.Keys)
            {
                if (item == "Id")
                {
                    continue;
                }
                object value = _context.Entry(entity).Property(item).CurrentValue;
                if (value != null)
                {
                    _context.Entry(entity).Property(item).IsModified = true;
                }
            }
            return _dbContextTransaction == null ? await this.CommitTrans() : 0;
        }

        public async Task<int> Update<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                _context.Entry<T>(entity).State = EntityState.Modified;
            }
            return _dbContextTransaction == null ? await this.CommitTrans() : 0;
        }

        public async Task<int> UpdateAllField<T>(T entity) where T : class
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return _dbContextTransaction == null ? await this.CommitTrans() : 0;
        }

        public async Task<int> Update<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            IEnumerable<T> entities = await _context.Set<T>().Where(condition).ToListAsync();
            return entities.Count() > 0 ? await Update(entities) : 0;
        }

        public IQueryable<T> IQueryable<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return _context.Set<T>().Where(condition);
        }

        #endregion Object entity Add, modify, delete

        #region Object entity query

        public async Task<T> FindEntity<T>(object keyValue) where T : class
        {
            try
            {
                await _context.Set<T>().FindAsync(keyValue);
            }
            catch (Exception w)
            {
                var ccc = w.Message;
            }
            return await _context.Set<T>().FindAsync(keyValue);
        }

        public async Task<T> FindEntity<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return await _context.Set<T>().Where(condition).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> FindList<T>() where T : class, new()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindList<T>(Func<T, object> orderby) where T : class, new()
        {
            var list = await _context.Set<T>().ToListAsync();
            list = list.OrderBy(orderby).ToList();
            return list;
        }

        public async Task<IEnumerable<T>> FindList<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return await _context.Set<T>().Where(condition).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindList<T>(string strSql) where T : class
        {
            return await FindList<T>(strSql, dbParameter: null);
        }

        public async Task<IEnumerable<T>> FindList<T>(string strSql, DbParameter[] dbParameter) where T : class
        {
            using (var dbConnection = _context.Database.GetDbConnection())
            {
                var reader = await new DbHelper(_context, dbConnection).ExecuteReadeAsync(CommandType.Text, strSql, dbParameter);
                return DatabasesExtension.IDataReaderToList<T>(reader);
            }
        }

        public async Task<(int total, IEnumerable<T> list)> FindList<T>(string sort, bool isAsc, int pageSize, int pageIndex) where T : class, new()
        {
            var tempData = _context.Set<T>().AsQueryable();
            return await FindList<T>(tempData, sort, isAsc, pageSize, pageIndex);
        }

        public async Task<(int total, IEnumerable<T> list)> FindList<T>(Expression<Func<T, bool>> condition, string sort, bool isAsc, int pageSize, int pageIndex) where T : class, new()
        {
            var tempData = _context.Set<T>().Where(condition);
            return await FindList<T>(tempData, sort, isAsc, pageSize, pageIndex);
        }

        public async Task<(int total, IEnumerable<T>)> FindList<T>(string strSql, string sort, bool isAsc, int pageSize, int pageIndex) where T : class
        {
            return await FindList<T>(strSql, null, sort, isAsc, pageSize, pageIndex);
        }

        public async Task<(int total, IEnumerable<T>)> FindList<T>(string strSql, DbParameter[] dbParameter, string sort, bool isAsc, int pageSize, int pageIndex) where T : class
        {
            using (var dbConnection = _context.Database.GetDbConnection())
            {
                DbHelper dbHelper = new DbHelper(_context, dbConnection);
                StringBuilder sb = new StringBuilder();
                sb.Append(DbPageExtension.SqlServerPageSql(strSql, dbParameter, sort, isAsc, pageSize, pageIndex));
                object tempTotal = await dbHelper.ExecuteScalarAsync(CommandType.Text, "SELECT COUNT(1) FROM (" + strSql + ") T", dbParameter);
                int total = tempTotal.ParseToInt();
                if (total > 0)
                {
                    var reader = await dbHelper.ExecuteReadeAsync(CommandType.Text, sb.ToString(), dbParameter);
                    return (total, DatabasesExtension.IDataReaderToList<T>(reader));
                }
                else
                {
                    return (total, new List<T>());
                }
            }
        }

        private async Task<(int total, IEnumerable<T> list)> FindList<T>(IQueryable<T> tempData, string sort, bool isAsc, int pageSize, int pageIndex)
        {
            tempData = DatabasesExtension.AppendSort<T>(tempData, sort, isAsc);
            var total = tempData.Count();
            if (total > 0)
            {
                tempData = tempData.Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize).AsQueryable();
                var list = await tempData.ToListAsync();
                return (total, list);
            }
            else
            {
                return (total, new List<T>());
            }
        }

        public async Task<(int total, IEnumerable<T> list)> FindList<T>(Pagination pagination) where T : class, new()
        {
            var total = pagination.TotalCount;
            var data = await FindList<T>(pagination.Sort, pagination.SortType.ToLower() == "asc" ? true : false, pagination.PageSize, pagination.PageIndex);
            pagination.TotalCount = total;
            return data;
        }

        public async Task<IEnumerable<T>> FindList<T>(Expression<Func<T, bool>> condition, Pagination pagination) where T : class, new()
        {
            //try
            //{
            //    await FindList<T>(condition, pagination.Sort, pagination.SortType.ToLower() == "asc" ? true : false, pagination.PageSize, pagination.PageIndex);
            //}
            //catch (Exception e)
            //{
            //    var xx = e.Message;
            //    var xx1 = xx;
            //}
            var data = await FindList<T>(condition, pagination.Sort, pagination.SortType.ToLower() == "asc" ? true : false, pagination.PageSize, pagination.PageIndex);
            pagination.TotalCount = data.total;
            return data.list;
        }

        public async Task<(int total, IEnumerable<T> list)> FindList<T>(string strSql, Pagination pagination) where T : class
        {
            var total = pagination.TotalCount;
            var data = await FindList<T>(strSql, pagination.Sort, pagination.SortType.ToLower() == "asc" ? true : false, pagination.PageSize, pagination.PageIndex);
            pagination.TotalCount = total;
            return data;
        }

        public async Task<IEnumerable<T>> FindList<T>(string strSql, DbParameter[] dbParameter, Pagination pagination) where T : class
        {
            var data = await FindList<T>(strSql, dbParameter, pagination.Sort, pagination.SortType.ToLower() == "asc" ? true : false, pagination.PageSize, pagination.PageIndex);
            pagination.TotalCount = data.total;
            return data.Item2;
        }

        #endregion Object entity query

        #region Data source query

        public async Task<DataTable> FindTable(string strSql)
        {
            return await FindTable(strSql, null);
        }

        public async Task<DataTable> FindTable(string strSql, DbParameter[] dbParameter)
        {
            using (var dbConnection = _context.Database.GetDbConnection())
            {
                var reader = await new DbHelper(_context, dbConnection).ExecuteReadeAsync(CommandType.Text, strSql, dbParameter);
                return DatabasesExtension.IDataReaderToDataTable(reader);
            }
        }

        public async Task<(int total, DataTable)> FindTable(string strSql, string sort, bool isAsc, int pageSize, int pageIndex)
        {
            return await FindTable(strSql, null, sort, isAsc, pageSize, pageIndex);
        }

        public async Task<(int total, DataTable)> FindTable(string strSql, DbParameter[] dbParameter, string sort, bool isAsc, int pageSize, int pageIndex)
        {
            using (var dbConnection = _context.Database.GetDbConnection())
            {
                DbHelper dbHelper = new DbHelper(_context, dbConnection);
                StringBuilder sb = new StringBuilder();
                sb.Append(DbPageExtension.SqlServerPageSql(strSql, dbParameter, sort, isAsc, pageSize, pageIndex));
                object tempTotal = await dbHelper.ExecuteScalarAsync(CommandType.Text, "SELECT COUNT(1) FROM (" + strSql + ") T", dbParameter);
                int total = tempTotal.ParseToInt();
                if (total > 0)
                {
                    var reader = await dbHelper.ExecuteReadeAsync(CommandType.Text, sb.ToString(), dbParameter);
                    DataTable resultTable = DatabasesExtension.IDataReaderToDataTable(reader);
                    return (total, resultTable);
                }
                else
                {
                    return (total, new DataTable());
                }
            }
        }

        public async Task<object> FindObject(string strSql)
        {
            return await FindObject(strSql, null);
        }

        public async Task<object> FindObject(string strSql, DbParameter[] dbParameter)
        {
            using (var dbConnection = _context.Database.GetDbConnection())
            {
                return await new DbHelper(_context, dbConnection).ExecuteScalarAsync(CommandType.Text, strSql, dbParameter);
            }
        }

        public async Task<T> FindObject<T>(string strSql) where T : class
        {
            try
            {
                var list = await _context.SqlQuery<T>(strSql);
                return list.FirstOrDefault();
            }
            catch (Exception e)
            {

                throw;
            }
           
        }

        #endregion Data source query
    }
}