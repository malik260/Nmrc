using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IResetPasswordTokenRepository
    {
        Task<List<ResetPasswordTokenEntity>> GetList(ResetPasswordTokenListParam param);
        Task<List<ResetPasswordTokenEntity>> GetPageList(ResetPasswordTokenListParam param, Pagination pagination);
        Task<ResetPasswordTokenEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        bool ExistToken(string token);
        Task SaveForm(ResetPasswordTokenEntity entity);
        Task DeleteForm(string ids);
    }
}
