using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IResetPasswordTokenService
    {
        Task<TData<List<ResetPasswordTokenEntity>>> GetList(ResetPasswordTokenListParam param);
        Task<TData<List<ResetPasswordTokenEntity>>> GetPageList(ResetPasswordTokenListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeResetPasswordTokenList(ResetPasswordTokenListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(ResetPasswordTokenListParam param);
        Task<TData<ResetPasswordTokenEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> GenerateToken(ResetPasswordTokenEntity entity);
        Task<TData<ResetPasswordTokenEntity>> GetTokenList(ResetPasswordTokenListParam param);
        Task<TData> DeleteForm(string ids);
    }
}
