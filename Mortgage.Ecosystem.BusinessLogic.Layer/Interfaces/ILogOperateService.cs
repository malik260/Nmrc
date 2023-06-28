using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ILogOperateService
    {
        Task<TData<List<LogOperateEntity>>> GetList(LogOperateListParam param);
        Task<TData<List<LogOperateEntity>>> GetPageList(LogOperateListParam param, Pagination pagination);
        Task<TData<LogOperateEntity>> GetEntity(int id);
        Task<TData<string>> SaveForm(LogOperateEntity entity);
        Task<TData<string>> SaveForm(string remark);
        Task<TData> DeleteForm(string ids);
        Task<TData> RemoveAllForm();
    }
}