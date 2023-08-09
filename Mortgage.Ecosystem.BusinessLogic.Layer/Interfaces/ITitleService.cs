using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ITitleService
    {
        Task<TData<List<TitleEntity>>> GetList(TitleListParam param);
        Task<TData<List<TitleEntity>>> GetPageList(TitleListParam param, Pagination pagination);
        Task<TData<TitleEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(TitleEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
