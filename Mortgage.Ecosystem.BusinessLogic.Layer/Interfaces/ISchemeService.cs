using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ISchemeService
    {
        Task<TData<List<SchemeSetupEntity>>> GetList(SchemeListParam param);
        Task<TData<List<SchemeSetupEntity>>> GetPageList(SchemeListParam param, Pagination pagination);
        //Task<TData<List<ZtreeInfo>>> GetZtreeCreditTypeList(CreditTypeListParam param);
        //Task<TData<List<ZtreeInfo>>> GetZtreeUserList(CreditTypeListParam param);
        Task<TData<SchemeSetupEntity>> GetEntity(string name);
        //Task<TData<int>> GetMaxSort();
        Task<TData<SchemeSetupEntity>> GetEntities(int id);

        Task<TData<string>> SaveForm(SchemeSetupEntity entity);
        Task<TData> DeleteForm(string ids);

        Task<TData<string>> UpdateForm(SchemeSetupEntity entity);
    }
}
