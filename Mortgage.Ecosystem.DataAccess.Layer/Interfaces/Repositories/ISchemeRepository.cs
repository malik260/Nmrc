using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ISchemeRepository
    {
        Task<List<SchemeSetupEntity>> GetList(SchemeListParam param);
        Task<List<SchemeSetupEntity>> GetPageList(SchemeListParam param, Pagination pagination);
        Task<SchemeSetupEntity> GetEntitybyName(string name);
        //Task<int> GetMaxSort();
        Task<SchemeSetupEntity> GetEntity(string name);
        Task<SchemeSetupEntity> GetEntities(int id);
        Task<SchemeSetupEntity> GetEntitybiId(int id);
        Task SaveForm(SchemeSetupEntity entity);
        Task DeleteForm(string ids);
        bool ExistSchemeName(SchemeSetupEntity entity);
       // Task<SchemeSetupEntity> GetEntitybyName(string name)


    }
}