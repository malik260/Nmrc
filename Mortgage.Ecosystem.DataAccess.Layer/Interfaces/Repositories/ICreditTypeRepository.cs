using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ICreditTypeRepository
    {
        Task<List<CreditTypeEntity>> GetList(CreditTypeListParam param);
        Task<List<CreditTypeEntity>> GetPageList(CreditTypeListParam param, Pagination pagination);
        Task<CreditTypeEntity> GetEntity(string code);
        //Task<int> GetMaxSort();
        Task SaveForm(CreditTypeEntity entity);
        Task DeleteForm(string ids);
    }
}