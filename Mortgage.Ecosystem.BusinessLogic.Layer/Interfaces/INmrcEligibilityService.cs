using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface INmrcEligibilityService
    {
        Task<TData<List<NmrcEligibilityEntity>>> GetList(NmrcEligibilityListParam param);
        Task<TData<List<NmrcEligibilityEntity>>> GetCategory(string categoryId);
        Task<TData<List<NmrcEligibilityEntity>>> GetPageList(NmrcEligibilityListParam param, Pagination pagination);
        Task<TData<NmrcEligibilityEntity>> GetEntity(int id);
        Task<TData<NmrcEligibilityEntity>> GetEntities(int id);

        Task<TData<string>> SaveForm(NmrcEligibilityEntity entity);
        Task<TData> DeleteForm(string ids);
        Task<TData<string>> UpdateForm(NmrcEligibilityEntity entity);

    }
}