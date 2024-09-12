using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface INmrcActivityRepository
    {
        Task<List<RefinancingEntity>> GetList(RefinancingEntity param);
        Task<List<RefinancingEntity>> GetPageList(RefinancingEntity param, Pagination pagination);
        Task<RefinancingEntity> GetEntity(long lenderid);
    }
}
