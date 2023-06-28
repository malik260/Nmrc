using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ILogLoginService
    {
        Task<TData<List<LogLoginEntity>>> GetList(LogLoginListParam param);
        Task<TData<List<LogLoginEntity>>> GetPageList(LogLoginListParam param, Pagination pagination);
        Task<TData<LogLoginEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(LogLoginEntity entity);
        Task<TData> DeleteForm(string ids);
        Task<TData> RemoveAllForm();
    }
}