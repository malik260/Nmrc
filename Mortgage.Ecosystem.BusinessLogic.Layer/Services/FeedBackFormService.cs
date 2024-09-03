using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.DataAccess.Layer.Request;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class FeedBackFormService : IFeedBackFormService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IEmployeeService _employeeService;

        public FeedBackFormService(IUnitOfWork iUnitOfWork, IEmployeeService employeeService)
        {
            _iUnitOfWork = iUnitOfWork;
            _employeeService = employeeService;
        }

        #region Retrieve data
        public async Task<TData<List<FeedBackFormEntity>>> GetList(FeedBackFormListParam param)
        {
            TData<List<FeedBackFormEntity>> obj = new TData<List<FeedBackFormEntity>>();
            obj.Data = await _iUnitOfWork.FeedBackForms.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<FeedBackFormEntity>>> GetPageList(FeedBackFormListParam param, Pagination pagination)
        {
            TData<List<FeedBackFormEntity>> obj = new TData<List<FeedBackFormEntity>>();
            obj.Data = await _iUnitOfWork.FeedBackForms.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<FeedBackFormEntity>>> GetEmployeePageList(FeedBackFormListParam param, Pagination pagination)
        {
            var DB = new ApplicationDbContext();
            var user = await Operator.Instance.Current();
            var employeeDetails = DB.EmployeeEntity.Where(i => i.Id == user.Employee).FirstOrDefault();
            TData<List<FeedBackFormEntity>> obj = new TData<List<FeedBackFormEntity>>();
            obj.Data = await _iUnitOfWork.FeedBackForms.GetEmployeePageList(param, pagination);
            obj.Data = obj.Data.Where(feedbackform => feedbackform.NHFNumber == employeeDetails.NHFNumber).ToList();
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeFeedBackFormList(FeedBackFormListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<FeedBackFormEntity> feedBackFormList = await _iUnitOfWork.FeedBackForms.GetList(param);
            foreach (FeedBackFormEntity feedBackForm in feedBackFormList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = feedBackForm.Id,
                    name = feedBackForm.Name
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(FeedBackFormListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<FeedBackFormEntity> feedBackFormList = await _iUnitOfWork.FeedBackForms.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (FeedBackFormEntity feedBackForm in feedBackFormList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = feedBackForm.Id,
                    name = feedBackForm.Name
                });
                List<long> userIdList = userList.Where(t => t.Company == feedBackForm.Id).Select(t => t.Employee).ToList();
                foreach (UserEntity user in userList.Where(t => userIdList.Contains(t.Employee)))
                {
                    obj.Data.Add(new ZtreeInfo
                    {
                        id = user.Id,
                        name = user.RealName
                    });
                }
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<FeedBackFormEntity>> GetEntity(long id)
        {
            TData<FeedBackFormEntity> obj = new TData<FeedBackFormEntity>();
            obj.Data = await _iUnitOfWork.FeedBackForms.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.FeedBackForms.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<EmployeeDetailsVM>> GetCustomerDetails()
        {
            TData<EmployeeDetailsVM> obj = new TData<EmployeeDetailsVM>();
            var user = await Operator.Instance.Current();
            var empInfo = await _iUnitOfWork.Employees.GetById(user.Employee);
            var employeedetails = await _iUnitOfWork.Employees.GetEntityByNhfNumber(empInfo.NHFNumber);
            //  var employerdetail = await _iUnitOfWork.Companies.GetEntity(employeedetails.Company);

            var custDetails = new EmployeeDetailsVM
            {
                // Nhfno = employeedetails.NHFNumber.ToString(),
                EmailAddress = employeedetails.EmailAddress.ToString(),
                Name = employeedetails.FirstName + " " + employeedetails.LastName,
                //   EmployerName = employerdetail.Name,

            };

            obj.Data = custDetails;
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data

        public async Task<TData<string>> SaveForms(FeedBackFormEntity entity)
        {
            TData<string> obj = new TData<string>();
            var entityRecord = await _iUnitOfWork.FeedBackForms.GetEntity(entity.Id);
            entity.EmploymentType = entityRecord.EmploymentType;
            entity.NHFNumber = entityRecord.NHFNumber;
            entity.Company = entityRecord.Company;
            entity.BaseCreatorId = entityRecord.BaseCreatorId;
            entity.BaseProcessMenu = entityRecord.BaseProcessMenu;

            await _iUnitOfWork.FeedBackForms.UpdateForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            obj.Message = "Response Message sent successfully";
            return obj;
        }
        public async Task<TData<string>> SaveForm(FeedBackFormEntity entity)
        {
            TData<string> obj = new TData<string>();
            try
            {
                var user = await Operator.Instance.Current();
                var customerDetails = await _employeeService.GetEntityByNhfNo(user.EmployeeInfo.NHFNumber);
                var companyDetails = await _iUnitOfWork.Companies.GetEntity(user.EmployeeInfo.Company);
                if (customerDetails == null)
                {
                    obj.Tag = 0;
                    obj.Message = "Customer details not found.";
                    return obj;
                }
                entity.EmploymentType = EmploymentTypeEnum.Employed.ToInt();
                entity.Company = user.EmployeeInfo.Company;
                entity.NHFNumber = customerDetails.Data.NHFNumber.ToLong();
                entity.Branch = Convert.ToInt32(customerDetails.Data.Branch);
                entity.EmailAddress = customerDetails.Data.EmailAddress;
                entity.Status = GlobalConstant.ONE;
                entity.DateSent = DateTime.Now;
                await _iUnitOfWork.FeedBackForms.SaveForm(entity);
                obj.Data = entity.Id.ToString();
                obj.Tag = 1;
                obj.Message = "Feedback sent successfully";
            }
            catch (Exception ex)
            {

                // Log the exception for debugging purposes
                Console.WriteLine($"Exception: {ex}");
                obj.Tag = -1;
                obj.Message = "An error occurred while sending ticket.";
            }
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.FeedBackForms.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
