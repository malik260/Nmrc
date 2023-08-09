using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ICompanyClassService
    {
        Task<TData<List<CompanyClassEntity>>> GetList(CompanyClassListParam param);
        Task<TData<List<CompanyClassEntity>>> GetPageList(CompanyClassListParam param, Pagination pagination);
        Task<TData<CompanyClassEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(CompanyClassEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
