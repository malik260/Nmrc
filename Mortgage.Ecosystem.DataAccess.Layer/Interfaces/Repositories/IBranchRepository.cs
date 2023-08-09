using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IBranchRepository
    {
        Task<List<BranchEntity>> GetList(BranchListParam param);
        Task<List<BranchEntity>> GetPageList(BranchListParam param, Pagination pagination);
        Task<BranchEntity> GetEntity(long id);
        Task SaveForm(BranchEntity entity);
        Task DeleteForm(string ids);
    }
}
