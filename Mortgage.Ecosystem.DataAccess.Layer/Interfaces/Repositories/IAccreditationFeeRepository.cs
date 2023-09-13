using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IAccreditationFeeRepository
    {
        Task<List<AccreditationFeeEntity>> GetList(AccreditationFeeListParam param);
        Task<List<AccreditationFeeEntity>> GetPageList(AccreditationFeeListParam param, Pagination pagination);
        Task<AccreditationFeeEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(AccreditationFeeEntity entity);
        Task DeleteForm(string ids);
        //Task<AccredidationFeeEntity> GetEntity(long id);



        //Task<AccredidationFeeEntity> GetById(long id);

    }
}
