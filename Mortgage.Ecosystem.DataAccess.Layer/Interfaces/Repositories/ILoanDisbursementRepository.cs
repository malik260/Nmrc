using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ILoanDisbursementRepository
    {
        Task<List<LoanDisbursementEntity>> GetList(LoanDisbursementDto param);
        Task<List<LoanDisbursementEntity>> GetPageList(LoanDisbursementDto param, Pagination pagination);
        Task<LoanDisbursementEntity> GetEntity(long id);
        Task SaveForm(LoanDisbursementEntity entity);
    }
}
