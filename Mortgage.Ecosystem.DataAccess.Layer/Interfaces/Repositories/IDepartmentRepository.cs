using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IDepartmentRepository
    {
        Task<List<DepartmentEntity>> GetList(DepartmentListParam param);
        Task<DepartmentEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        bool ExistDepartmentName(DepartmentEntity entity);
        bool ExistChildrenDepartment(long id);
        Task SaveForm(DepartmentEntity entity);
        Task DeleteForm(string ids);
    }
}
