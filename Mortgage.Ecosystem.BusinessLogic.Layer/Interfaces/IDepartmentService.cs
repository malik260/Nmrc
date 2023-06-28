using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IDepartmentService
    {
        Task<TData<List<DepartmentEntity>>> GetList(DepartmentListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeDepartmentList(DepartmentListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(DepartmentListParam param);
        Task<TData<DepartmentEntity>> GetEntity(int id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(DepartmentEntity entity);
        Task<TData> DeleteForm(string ids);
        Task<List<int>> GetChildrenDepartmentIdList(List<DepartmentEntity> departmentList, int? departmentId);
    }
}