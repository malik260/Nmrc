using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ILenderRepository
    {
        Task<List<LenderSetupEntity>> GetList(LenderListParam param);
        Task<List<LenderSetupEntity>> GetPageList(LenderListParam param, Pagination pagination);
        //Task<CreditTypeEntity> GetEntity(string code);
        //Task<int> GetMaxSort();
        Task<LenderSetupEntity> GetEntity(long id);
        //Task<LenderSetupEntity> GetEntitybyName(string name);

        Task SaveForm(LenderSetupEntity entity);
   
        //bool ExistLenderName(LenderSetupEntity entity);
        Task DeleteForm(string ids);
        Task<LenderSetupEntity> GetEntities(int id);




    }
}