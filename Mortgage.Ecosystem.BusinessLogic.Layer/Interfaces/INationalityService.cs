using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface INationalityService
    {
        Task<TData<List<NationalityEntity>>> GetList(NationalityListParam param);
        Task<TData<List<NationalityEntity>>> GetPageList(NationalityListParam param, Pagination pagination);
        Task<TData<NationalityEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(NationalityEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
