using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IErrorLogService
    {
        //Task<TData<List<ErrorLogEntity>>> GetPageList(ErrorLogEntity param);
        Task<TData<List<ErrorLogEntity>>> GetPageList(ErrorLogEntity param, Pagination pagination);
    }
}
