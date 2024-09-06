using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ISchemeLenderRepository
    {
        Task<List<SchemeLenderEntity>> GetList(SchemeLenderListParam param);
        Task<List<SchemeLenderEntity>> GetPageList(SchemeLenderListParam param, Pagination pagination);

        //Task<int> GetMaxSort();
        Task<SchemeLenderEntity> GetEntity(int id);
        Task<SchemeLenderEntity> GetEntities(int id);
        Task<SchemeLenderEntity> GetEntitybiId(int id);
        Task SaveForm(SchemeLenderEntity entity);
        Task DeleteForm(string ids);
       // bool ExistSchemeName(SchemeSetupEntity entity);
       // Task<SchemeSetupEntity> GetEntitybyName(string name)


    }
}