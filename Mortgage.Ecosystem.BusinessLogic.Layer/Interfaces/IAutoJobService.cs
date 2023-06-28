using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IAutoJobService
    {
        Task<TData<List<AutoJobEntity>>> GetList(AutoJobListParam param);
        Task<TData<List<AutoJobEntity>>> GetPageList(AutoJobListParam param, Pagination pagination);
        Task<TData<AutoJobEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(AutoJobEntity entity);
        Task<TData> DeleteForm(string ids);
        Task<TData> ChangeJobStatus(AutoJobEntity entity);
    }
}