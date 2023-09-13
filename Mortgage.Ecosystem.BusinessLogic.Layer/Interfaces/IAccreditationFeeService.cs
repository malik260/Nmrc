using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IAccreditationFeeService
    {
        Task<TData<List<AccreditationFeeEntity>>> GetList(AccreditationFeeListParam param);
        Task<TData<List<AccreditationFeeEntity>>> GetPageList(AccreditationFeeListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeAccreditationFeeList(AccreditationFeeListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(AccreditationFeeListParam param);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(AccreditationFeeEntity entity);
        Task<TData> DeleteForm(string ids);
       Task<TData<AccreditationFeeEntity>> GetEntity(long id);
    }
}
