using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ISectorService
    {
        Task<TData<List<SectorEntity>>> GetList(SectorListParam param);
        Task<TData<List<SectorEntity>>> GetPageList(SectorListParam param, Pagination pagination);
        Task<TData<SectorEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(SectorEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
