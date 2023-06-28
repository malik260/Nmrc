using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IGenderService
    {
        Task<TData<List<GenderEntity>>> GetList(GenderListParam param);
        Task<TData<List<GenderEntity>>> GetPageList(GenderListParam param, Pagination pagination);
        Task<TData<GenderEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(GenderEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
