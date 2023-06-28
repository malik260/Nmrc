using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ISubSectorService
    {
        Task<TData<List<SubSectorEntity>>> GetList(SubSectorListParam param);
        Task<TData<List<SubSectorEntity>>> GetPageList(SubSectorListParam param, Pagination pagination);
        Task<TData<SubSectorEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(SubSectorEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
