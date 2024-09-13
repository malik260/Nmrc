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
    public interface INmrcActivityService
    {
        Task<TData<List<RefinancingEntity>>> GetList(RefinancingEntity param);
        Task<TData<List<RefinancingEntity>>> GetPageList(RefinancingEntity param, Pagination pagination);
        Task<TData<RefinancingEntity>> GetEntity(long Id);
        Task<TData<string>> SaveForm(RefinancingEntity entity);

    }
}
