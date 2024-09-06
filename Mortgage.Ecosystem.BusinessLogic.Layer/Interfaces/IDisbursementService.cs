using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IDisbursementService
    {
        Task<TData<List<DisbursementEntity>>> GetList(DisbursementListParam param);
        Task<TData<List<DisbursementEntity>>> GetPageList(DisbursementListParam param, Pagination pagination);
        Task<TData<DisbursementEntity>> GetEntity();
        Task<TData<string>> SaveForm(DisbursementEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
