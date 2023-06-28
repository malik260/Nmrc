using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ITitleRepository
    {
        Task<List<TitleEntity>> GetList(TitleListParam param);
        Task<List<TitleEntity>> GetPageList(TitleListParam param, Pagination pagination);
        Task<TitleEntity> GetEntity(long id);
        Task SaveForm(TitleEntity entity);
        Task DeleteForm(string ids);
    }
}
