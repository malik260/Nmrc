using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IChangeEmployerService
    {
        Task<TData<List<ChangeEmployerEntity>>> GetList(ChangeEmployerListParam param);
        Task<TData<List<ChangeEmployerEntity>>> GetPageList(ChangeEmployerListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeChangeEmployerList(ChangeEmployerListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(ChangeEmployerListParam param);
        Task<TData<ChangeEmployerEntity>> GetEntity(long id);
        Task<TData<CustomerDetailsViewModel>> GetCompanyName();
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(ChangeEmployerEntity entity);

        Task<TData<string>> UpdateForm(ChangeEmployerEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}