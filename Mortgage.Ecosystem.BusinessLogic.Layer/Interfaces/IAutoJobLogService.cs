using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IAutoJobLogService
    {
        Task<TData<List<AutoJobLogEntity>>> GetList(AutoJobLogListParam param);
        Task<TData<List<AutoJobLogEntity>>> GetPageList(AutoJobLogListParam param, Pagination pagination);
        Task<TData<AutoJobLogEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(AutoJobLogEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}