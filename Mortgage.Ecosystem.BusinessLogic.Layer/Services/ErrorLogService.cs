using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class ErrorLogService : IErrorLogService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        public ErrorLogService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        public async Task<TData<List<ErrorLogEntity>>> GetPageList(ErrorLogEntity param, Pagination pagination )
        {
            try
            {
                var context = new ApplicationDbContext();
                TData<List<ErrorLogEntity>> obj = new TData<List<ErrorLogEntity>>();
                obj.Data = _iUnitOfWork.ErrorLog.GetPageList(param, pagination)?.Result.ToList();
                //obj.Data = context.ErrorLogEntity.DefaultIfEmpty().ToList().OrderByDescending(i=> i.Id).ToList();
                obj.Tag = 1;
                obj.Total = pagination.TotalCount;
                return obj;
            }catch(Exception ex)
            {
                throw;
            }
        }


    }
}
