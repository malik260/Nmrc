using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IChecklistRepository
    {
        Task<List<ChecklistEntity>> GetList(ChecklistListParam param);
        Task<List<ChecklistEntity>> GetPageList(ChecklistListParam param, Pagination pagination);
        Task<ChecklistEntity> GetEntity(int id);
        //Task<int> GetMaxSort();
        Task SaveForm(ChecklistEntity entity);
        Task DeleteForm(string ids);
    }
}
