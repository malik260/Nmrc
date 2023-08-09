using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IChangePasswordService
    {
        Task<TData<List<ChangePasswordEntity>>> GetList(ChangePasswordListParam param);
        Task<TData<List<ChangePasswordEntity>>> GetPageList(ChangePasswordListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeChangePasswordList(ChangePasswordListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(ChangePasswordListParam param);
        Task<TData<ChangePasswordEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(ChangePasswordEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
