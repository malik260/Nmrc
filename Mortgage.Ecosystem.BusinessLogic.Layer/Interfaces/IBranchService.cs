using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IBranchService
    {
        Task<TData<List<BranchEntity>>> GetList(BranchListParam param);
        Task<TData<List<BranchEntity>>> GetPageList(BranchListParam param, Pagination pagination);
        Task<TData<BranchEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(BranchEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
