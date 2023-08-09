using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public DepartmentService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Get data
        public async Task<TData<List<DepartmentEntity>>> GetList(DepartmentListParam param)
        {
            TData<List<DepartmentEntity>> obj = new TData<List<DepartmentEntity>>();
            obj.Data = await _iUnitOfWork.Departments.GetList(param);
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            if (operatorInfo.IsSystem != 1)
            {
                List<int> childrenDepartmentIdList = await GetChildrenDepartmentIdList(obj.Data, operatorInfo.EmployeeInfo?.Department);
                obj.Data = obj.Data.Where(p => childrenDepartmentIdList.Contains(p.Id)).ToList();
            }
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(new UserListParam { UserIds = string.Join(",", obj.Data.Select(p => p.Principal).ToArray()) });
            foreach (DepartmentEntity entity in obj.Data)
            {
                if (entity.Principal > 0)
                {
                    entity.PrincipalName = userList.Where(p => p.Id == entity.Principal).Select(p => p.RealName).FirstOrDefault();
                }
                else
                {
                    entity.PrincipalName = string.Empty;
                }
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeDepartmentList(DepartmentListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<DepartmentEntity> departmentList = await _iUnitOfWork.Departments.GetList(param);
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            if (operatorInfo.IsSystem != 1)
            {
                List<int> childrenDepartmentIdList = await GetChildrenDepartmentIdList(departmentList, operatorInfo.EmployeeInfo?.Department);
                departmentList = departmentList.Where(p => childrenDepartmentIdList.Contains(p.Id)).ToList();
            }
            foreach (DepartmentEntity department in departmentList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = department.Id,
                    //pId = department.Parent,
                    name = department.Name
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(DepartmentListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<DepartmentEntity> departmentList = await _iUnitOfWork.Departments.GetList(param);
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            if (operatorInfo.IsSystem != 1)
            {
                List<int> childrenDepartmentIdList = await GetChildrenDepartmentIdList(departmentList, operatorInfo.EmployeeInfo?.Department);
                departmentList = departmentList.Where(p => childrenDepartmentIdList.Contains(p.Id)).ToList();
            }
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (DepartmentEntity department in departmentList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = department.Id,
                    //pId = department.Parent,
                    name = department.Name
                });
                //List<long> userIdList = userList.Where(t => t.Id == department.Id).Select(t => t.Id).ToList();
                //foreach (UserEntity user in userList.Where(t => userIdList.Contains(t.Id)))
                //{
                //    obj.Data.Add(new ZtreeInfo
                //    {
                //        id = user.Id,
                //        pId = department.Id,
                //        name = user.RealName
                //    });
                //}
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<DepartmentEntity>> GetEntity(int id)
        {
            TData<DepartmentEntity> obj = new TData<DepartmentEntity>();
            obj.Data = await _iUnitOfWork.Departments.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.Departments.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(DepartmentEntity entity)
        {
            TData<string> obj = new TData<string>();
            if (!entity.Id.IsNullOrZero() && entity.Id == entity.Branch)
            {
                obj.Message = "Can't choose yourself as a superior department!";
                return obj;
            }
            if (_iUnitOfWork.Departments.ExistDepartmentName(entity))
            {
                obj.Message = "Department name already exists!";
                return obj;
            }
            await _iUnitOfWork.Departments.SaveForm(entity);
            obj.Data = entity.Id.ToStr();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            foreach (long id in TextHelper.SplitToArray<long>(ids, ','))
            {
                if (_iUnitOfWork.Departments.ExistChildrenDepartment(id))
                {
                    obj.Message = "There are sub-departments under this department!";
                    return obj;
                }
            }
            await _iUnitOfWork.Departments.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region public methods
        // Get the current department and all the following departments
        // <param name="departmentList"></param>
        // <param name="departmentId"></param>
        // <returns></returns>
        public async Task<List<int>> GetChildrenDepartmentIdList(List<DepartmentEntity> departmentList, int? departmentId)
        {
            if (departmentList == null)
            {
                departmentList = await _iUnitOfWork.Departments.GetList(null);
            }
            List<int> departmentIdList = new List<int>();
            departmentIdList.Add((int)departmentId);
            GetChildrenDepartmentIdList(departmentList, departmentId, departmentIdList);
            return departmentIdList;
        }
        #endregion

        #region private method
        // Get all sub-departments under the department
        // <param name="departmentList"></param>
        // <param name="departmentId"></param>
        // <param name="departmentIdList"></param>
        private void GetChildrenDepartmentIdList(List<DepartmentEntity> departmentList, int? departmentId, List<int> departmentIdList)
        {
            var children = departmentList.Where(p => p.Id == departmentId).Select(p => p.Id).ToList();
            if (children.Count > 0)
            {
                departmentIdList.AddRange(children);
                foreach (int id in children)
                {
                    GetChildrenDepartmentIdList(departmentList, id, departmentIdList);
                }
            }
        }
        #endregion
    }
}