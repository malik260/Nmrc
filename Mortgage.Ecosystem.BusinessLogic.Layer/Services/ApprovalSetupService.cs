using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class ApprovalSetupService : IApprovalSetupService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public ApprovalSetupService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<ApprovalSetupEntity>>> GetList(ApprovalSetupListParam param)
        {
            TData<List<ApprovalSetupEntity>> obj = new TData<List<ApprovalSetupEntity>>();
            obj.Data = await _iUnitOfWork.ApprovalSetups.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ApprovalSetupEntity>>> GetPageList(ApprovalSetupListParam param, Pagination pagination)
        {
            TData<List<ApprovalSetupEntity>> obj = new TData<List<ApprovalSetupEntity>>();
            obj.Data = await _iUnitOfWork.ApprovalSetups.GetPageList(param, pagination);
            if (obj.Data != null)
            {
                List<CompanyEntity> companyList = await _iUnitOfWork.Companies.GetList(new CompanyListParam { Ids = obj.Data.Select(p => p.Company).ToList() });
                List<BranchEntity> branchList = await _iUnitOfWork.Branches.GetList(new BranchListParam { Ids = obj.Data.Select(p => p.Branch).ToList() });
                List<MenuEntity> menuList = await _iUnitOfWork.Menus.GetList(new MenuListParam { Ids = obj.Data.Select(p => p.MenuId).ToList() });
                List<EmployeeEntity> employeeList = await _iUnitOfWork.Employees.GetList(new EmployeeListParam { Ids = obj.Data.Select(p => p.Authority).ToList() });
                foreach (ApprovalSetupEntity approval in obj.Data)
                {
                    approval.CompanyName = companyList.Where(p => p.Id == approval.Company).Select(p => p.Name).FirstOrDefault();
                    approval.BranchName = branchList.Where(p => p.Id == approval.Branch).Select(p => p.Name).FirstOrDefault();
                    approval.MenuName = menuList.Where(p => p.Id == approval.MenuId).Select(p => p.MenuName).FirstOrDefault();
                    approval.AuthorityName = employeeList.Where(p => p.Id == approval.Authority).Select(p => $"{p.LastName} {p.FirstName}").FirstOrDefault();
                }
            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<ApprovalSetupEntity>> GetEntity(long id)
        {
            TData<ApprovalSetupEntity> obj = new TData<ApprovalSetupEntity>();
            obj.Data = await _iUnitOfWork.ApprovalSetups.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(ApprovalSetupEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.ApprovalSetups.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.ApprovalSetups.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
