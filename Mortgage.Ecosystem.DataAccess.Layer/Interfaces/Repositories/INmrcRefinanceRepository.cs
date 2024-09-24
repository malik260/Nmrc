using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface INmrcRefinanceRepository
    {
        Task<List<NmrcRefinancingEntity>> GetListByRefinanceNumber(NmrcRefinancingEntity param);
        Task SaveForm(NmrcRefinancingEntity entity);
        Task<List<NmrcRefinancingEntity>> GetList(NmrcRefinancingEntity param);
        Task<List<NmrcRefinancingEntity>> GetPageList(NmrcRefinancingEntity param, Pagination pagination);
        Task<List<NmrcRefinancingEntity>> GetLoanBatches(string id, Pagination pagination);
        Task<NmrcRefinancingEntity> GetEntity(long id);
        Task<NmrcRefinancingEntity> GetEntitybyNHF(string NHF);
        Task<NmrcRefinancingEntity> GetEntitybyLoanId(string id);
    }
}
