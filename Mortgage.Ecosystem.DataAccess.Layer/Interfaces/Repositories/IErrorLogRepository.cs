using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IErrorLogRepository
    {
        Task<List<ErrorLogEntity>> GetList(ErrorLogEntity param);
        Task<List<ErrorLogEntity>> GetPageList(ErrorLogEntity param, Pagination pagination);
    }
}
