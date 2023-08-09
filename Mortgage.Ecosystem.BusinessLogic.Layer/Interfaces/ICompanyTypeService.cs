using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ICompanyTypeService
    {
        Task<TData<List<CompanyTypeEntity>>> GetList(CompanyTypeListParam param);
        Task<TData<List<CompanyTypeEntity>>> GetPageList(CompanyTypeListParam param, Pagination pagination);
        Task<TData<CompanyTypeEntity>> GetEntity(long id);
        Task<TData<string>> SaveForm(CompanyTypeEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
