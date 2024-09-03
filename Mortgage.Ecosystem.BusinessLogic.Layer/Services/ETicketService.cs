using Microsoft.AspNetCore.Identity;
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
    public class ETicketService : IETicketService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IEmployeeService _employeeService;

        public ETicketService(IUnitOfWork iUnitOfWork, IEmployeeService employeeService)
        {
            _iUnitOfWork = iUnitOfWork;
            _employeeService = employeeService;
        }

        #region Retrieve data
        public async Task<TData<List<ETicketEntity>>> GetList(ETicketListParam param)
        {
            TData<List<ETicketEntity>> obj = new TData<List<ETicketEntity>>();
            obj.Data = await _iUnitOfWork.ETickets.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ETicketEntity>>> GetPageList(ETicketListParam param, Pagination pagination)
        {
            TData<List<ETicketEntity>> obj = new TData<List<ETicketEntity>>();
            obj.Data = await _iUnitOfWork.ETickets.GetPageList(param, pagination);
            if (obj.Data.Count > 0)
            {
                List<CompanyEntity> companyList = await _iUnitOfWork.Companies.GetList(new CompanyListParam { Ids = obj.Data.Select(p => p.Company).ToList() });
                foreach (ETicketEntity eTicket in obj.Data)
                {
                    eTicket.CompanyName = companyList.Where(p => p.Id == eTicket.Company).Select(p => p.Name).FirstOrDefault();
                }
            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<List<ETicketEntity>>> GetEmployeePageList(ETicketListParam param, Pagination pagination)
        {
            var DB = new ApplicationDbContext();
            var user = await Operator.Instance.Current();
            var employeeDetails = DB.EmployeeEntity.Where(i => i.Id == user.Employee).FirstOrDefault();
            TData<List<ETicketEntity>> obj = new TData<List<ETicketEntity>>();
            obj.Data = await _iUnitOfWork.ETickets.GetEmployeePageList(param, pagination);
            obj.Data = obj.Data.Where(eticket => eticket.NHFNumber == employeeDetails.NHFNumber).ToList();
            if (obj.Data.Count > 0)
            {
                List<CompanyEntity> companyList = await _iUnitOfWork.Companies.GetList(new CompanyListParam { Ids = obj.Data.Select(p => p.Company).ToList() });
                foreach (ETicketEntity eTicket in obj.Data)
                {
                    eTicket.CompanyName = companyList.Where(p => p.Id == eTicket.Company).Select(p => p.Name).FirstOrDefault();
                }
            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ETicketEntity>>> GetApprovalPageList(ETicketListParam param, Pagination pagination)
        {
            TData<List<ETicketEntity>> obj = new TData<List<ETicketEntity>>();
            obj.Data = await _iUnitOfWork.ETickets.GetApprovalPageList(param, pagination);
            if (obj.Data.Count > 0)
            {
                List<CompanyEntity> companyList = await _iUnitOfWork.Companies.GetList(new CompanyListParam { Ids = obj.Data.Select(p => p.Company).ToList() });
                foreach (ETicketEntity eTicket in obj.Data)
                {
                    eTicket.CompanyName = companyList.Where(p => p.Id == eTicket.Company).Select(p => p.Name).FirstOrDefault();
                }
            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<List<ZtreeInfo>>> GetZtreeETicketList(ETicketListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ETicketEntity> eTicketList = await _iUnitOfWork.ETickets.GetList(param);
            foreach (ETicketEntity eTicket in eTicketList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = eTicket.Id,
                    name = eTicket.RequestNumber
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(ETicketListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ETicketEntity> eTicketList = await _iUnitOfWork.ETickets.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (ETicketEntity eTicket in eTicketList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = eTicket.Id,
                    name = eTicket.RequestNumber
                });
                List<long> userIdList = userList.Where(t => t.Company == eTicket.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<ETicketEntity>> GetEntity(long id)
        {
            TData<ETicketEntity> obj = new TData<ETicketEntity>();
            obj.Data = await _iUnitOfWork.ETickets.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.ETickets.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<CustomerDetailsViewModel>> GetStatus(long id)
        {
            TData<CustomerDetailsViewModel> obj = new TData<CustomerDetailsViewModel>();
            var DB = new ApplicationDbContext();
            //var eticketDetails = DB.EmployeeEntity.Where(i => i.NHFNumber == entity.NHFNumber).FirstOrDefault();
            var eticketDetails = await _iUnitOfWork.ETickets.GetEntityDetails(id);

            var custDetails = new CustomerDetailsViewModel
            {

                Status = eticketDetails.Status,
            };

            obj.Data = custDetails;
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(ETicketEntity entity)
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
                //if (entity.MessageType != null)
                //{
                //    entity.MessageType = Enum.GetName(typeof(MessageTypeEnum), entity.MessageType);
                //}
                entity.Company = user.EmployeeInfo.Company;
                entity.NHFNumber = customerDetails.Data.NHFNumber;
                entity.Branch = Convert.ToInt32(customerDetails.Data.Branch);
                entity.EmailAddress = customerDetails.Data.EmailAddress;
                entity.Status = GlobalConstant.ONE;

                // Generate a random five-digit number
                Random random = new Random();
                int randomNumber = random.Next(10000, 99999);

                // Set the RequestNumber with the generated random number
                entity.RequestNumber = randomNumber.ToString();
                entity.DateSent = DateTime.Now;

                var message = string.Empty;
                var AuthorityInfo = new ApprovalSetupListParam();
                long menu = entity.BaseProcessMenu;
                AuthorityInfo.MenuId = GlobalConstant.ETicket_MENU_ID;

                await _iUnitOfWork.ETickets.SaveForm(entity);
                var approveMenu = await _iUnitOfWork.ApprovalSetups.GetList(AuthorityInfo);
                //employeeListParam.Company = employeeListParam.Id;
                //var newEmployee = _iUnitOfWork.Employees.GetListByCompany(employeeListParam).Result.Where(i => i.EmployerType == 0).FirstOrDefault();
                foreach (var item in approveMenu)
                {
                    var authorityEmail = await _iUnitOfWork.Employees.GetById(item.Authority);

                    MailParameter mailParameter = new()
                    {
                        EticketApprover = authorityEmail.FirstName + " " + authorityEmail.LastName,
                        ApproverEmail = authorityEmail.EmailAddress,
                        RequestNumber = entity.RequestNumber,
                        MessageType = entity.MessageType,
                        UserCompany = companyDetails.Name,
                        Message = entity.Message,
                        Subject = entity.Subject,
                        DateSubmitted = entity.DateSent.ToString(),
                        NhfNumber = entity.NHFNumber.ToStr(),
                        UserName = user.UserName,
                        UserEmail = entity.EmailAddress,
                        UserPhoneNumber = customerDetails.Data.MobileNumber,
                        EmployeeName = customerDetails.Data.FirstName + ' ' + customerDetails.Data.LastName,

                    };

                    EmailHelper.IsETicketSent(mailParameter, out message);


                }

                obj.Data = entity.Id.ToString();
                obj.Tag = 1;
                obj.Message = "Message Sent Successful";
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

        public async Task<TData<string>> SaveForms(ETicketEntity entity)
        {
            TData<string> obj = new TData<string>();

            var entityRecord = await _iUnitOfWork.ETickets.GetEntity(entity.Id);
            var employeeInfo = await _iUnitOfWork.Employees.GetEntityByNhfNumber(entityRecord.NHFNumber);
            //var DB = new ApplicationDbContext();
            //var customerDetails = DB.EmployeeEntity.Where(i => i.NHFNumber == entityRecord.NHFNumber).DefaultIfEmpty().FirstOrDefault();
            //var companyDetails = DB.CompanyEntity.Where(i => i.NHFNumber == entityRecord.NHFNumber).DefaultIfEmpty().FirstOrDefault();

            //if (customerDetails == null)
            //{
            //    obj.Tag = 0;
            //    obj.Message = "Customer details not found.";
            //    return obj;
            //}
            entity.EmploymentType = entityRecord.EmploymentType;
            entity.NHFNumber = entityRecord.NHFNumber;
            entity.Company = entityRecord.Company;
            entity.BaseCreatorId = entityRecord.BaseCreatorId;
            entity.BaseProcessMenu = entityRecord.BaseProcessMenu;
            var message = string.Empty;
            MailParameter mailParameter = new()
            {
                ContactPerson = employeeInfo.FirstName + " " + employeeInfo.LastName,
                ContactPersonEmail = entityRecord.EmailAddress,
                RequestNumber = entityRecord.RequestNumber,
                MessageType = entityRecord.MessageType,
                UserCompany = GlobalConstant.COMPANY_NAME,
                Message = entity.ResponseMessage,
                Subject = entityRecord.Subject,
                DateSubmitted = entityRecord.DateSent.ToString(),
                NhfNumber = entityRecord.NHFNumber.ToString(),
            };

            if (entity.Status == GlobalConstant.TWO)
            {
                EmailHelper.IsETicketPending(mailParameter, out message);
            }
            else if (entity.Status == GlobalConstant.THREE)
            {
                EmailHelper.IsETicketClosed(mailParameter, out message);
            }
            await _iUnitOfWork.ETickets.UpdateForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            obj.Message = "Ticket Status Sent Successful";
            return obj;
        }



        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.ETickets.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> ApproveForm(ETicketEntity entity)
        {
            TData<string> obj = new TData<string>();
            var user = await Operator.Instance.Current();
            var entityRecord = await _iUnitOfWork.ETickets.GetEntity(entity.Id);
            var menuRecord = await _iUnitOfWork.Menus.GetEntity(entityRecord.BaseProcessMenu);
            var approvalLogListParam = new ApprovalLogListParam()
            {
                Company = user.Company,
                MenuId = menuRecord.Id,
                //Authority = user.Employee,
                Record = entity.Id
            };
            var approvalLogRecords = await _iUnitOfWork.ApprovalLogs.GetList(approvalLogListParam);
            menuRecord.ApprovalLogList = approvalLogRecords;
            await _iUnitOfWork.ETickets.ApproveForm(entityRecord, menuRecord, user);
            obj.Data = entity.Id.ParseToString();
            obj.Message = "Approved successfully";
            obj.Tag = 1;
            return obj;
        }

        #endregion
    }
}
