using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ILenderService
    {
        Task<TData<List<LenderSetupEntity>>> GetList(LenderListParam param);
        Task<TData<List<LenderSetupEntity>>> GetPageList(LenderListParam param, Pagination pagination);
        //Task<TData<List<ZtreeInfo>>> GetZtreeCreditTypeList(CreditTypeListParam param);
        //Task<TData<List<ZtreeInfo>>> GetZtreeUserList(CreditTypeListParam param);
        Task<TData<LenderSetupEntity>> GetEntity(long id
            );
        Task<TData<LenderSetupEntity>> GetEntities(int id);
        //Task<TData<int>> GetMaxSort();

        Task<TData<string>> SaveForm(LenderSetupEntity entity);

        Task<TData<string>> UpdateForm(LenderSetupEntity entity);

        Task<TData> DeleteForm(string ids);
    }
}
