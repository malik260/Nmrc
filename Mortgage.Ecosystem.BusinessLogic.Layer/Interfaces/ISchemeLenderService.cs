using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ISchemeLenderService
    {
        Task<TData<List<SchemeLenderEntity>>> GetList(SchemeLenderListParam param);
        Task<TData<List<SchemeLenderEntity>>> GetPageList(SchemeLenderListParam param, Pagination pagination);
        //Task<TData<List<ZtreeInfo>>> GetZtreeCreditTypeList(CreditTypeListParam param);
        //Task<TData<List<ZtreeInfo>>> GetZtreeUserList(CreditTypeListParam param);
        Task<TData<SchemeLenderEntity>> GetEntity(int id);
        //Task<TData<int>> GetMaxSort();
        //Task<TData<SchemeLenderEntity>> GetEntities(int id);

        Task<TData<string>> SaveForm(SchemeLenderEntity entity);
        Task<TData> DeleteForm(string ids);

        //Task<TData<string>> UpdateForm(SchemeSetupEntity entity);
    }
}
