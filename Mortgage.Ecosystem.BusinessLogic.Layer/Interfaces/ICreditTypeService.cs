using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ICreditTypeService
    {
        Task<TData<List<CreditTypeEntity>>> GetNonNhfList(CreditTypeListParam param);
        Task<TData<List<CreditTypeEntity>>> GetList(CreditTypeListParam param);
        Task<TData<List<CreditTypeEntity>>> GetPageList(CreditTypeListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeCreditTypeList(CreditTypeListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(CreditTypeListParam param);
        Task<TData<CreditTypeEntity>> GetEntity(string code);
        //Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(CreditTypeEntity entity);
        Task<TData<string>> UpdateForm(CreditTypeEntity entity);

        Task<TData> DeleteForm(string ids);
    }
}
