using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.DataAccess.Layer.Request;
using Newtonsoft.Json;
using static Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos.LoanApplicationDTO;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class RefinancingService : IRefinancingService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public RefinancingService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data

        public async Task<TData<List<RefinancingEntity>>> GetList(RefinancingListParam param)
        {
            TData<List<RefinancingEntity>> obj = new TData<List<RefinancingEntity>>();
            obj.Data = await _iUnitOfWork.Refinancings.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<RefinancingEntity>>> GetPageList(RefinancingListParam param, Pagination pagination)
        {
            TData<List<RefinancingEntity>> obj = new TData<List<RefinancingEntity>>();
            obj.Data = await _iUnitOfWork.Refinancings.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<RefinancingEntity>> GetEntity()
        {
            var user = await Operator.Instance.Current();
            TData<RefinancingEntity> obj = new TData<RefinancingEntity>();
            obj.Data = await _iUnitOfWork.Refinancings.GetEntity(Convert.ToString(user.EmployeeInfo.NHFNumber));
            obj.Tag = 1;
            return obj;
        }

        //public async Task<TData<RefinancingEntity>> GetEntity(int id)
        //{
        //    TData<RefinancingEntity> obj = new TData<RefinancingEntity>();
        //    obj.Data = await _iUnitOfWork.Refinancings.GetEntity(id);
        //    obj.Tag = 1;
        //    return obj;
        //}

        #endregion Retrieve data

        #region Submit data

        public async Task<TData<string>> SaveForm(RefinancingEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.Refinancings.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.Refinancings.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        #endregion Submit data

        public async Task<TData<string>> RefinanceLoans(string lists, long SecondaryLender)
        {
            TData<string> obj = new TData<string>();
            var batchData = new List<LoanDisbursementEntity>();
            var batchNo = RandomHelper.RandomLongGenerator(2000005, 99999999);
            batchData = JsonConvert.DeserializeObject<List<LoanDisbursementEntity>>(JsonConvert.DeserializeObject(lists).ToString());
            var lender = await _iUnitOfWork.Pmbs.GetEntity(SecondaryLender);
            var pmb = await _iUnitOfWork.Pmbs.GetEntity(batchData.FirstOrDefault().PmbId);
            var batchRef = lender.Name.ParseToString() + "-" + batchNo;
            decimal total = batchData.Select(i => i.Amount).ToList().Sum();
            foreach (LoanDisbursementEntity item in batchData)
            {
                var refinance = new RefinancingEntity();
                refinance.Amount = item.Amount;
                refinance.NHFNumber = item.CustomerNhf;
                refinance.PmbId = item.PmbId;
                refinance.Tenor = item.Tenor;
                refinance.Rate = item.Rate;
                refinance.ApplicationDate = DateTime.Now;
                refinance.RefinanceNumber = batchRef;
                refinance.Status = 0;
                refinance.TotalAmount = total;
                refinance.LoanId = item.LoanId;
                refinance.ProductCode = item.ProductCode;
                refinance.LenderID = SecondaryLender;
                await _iUnitOfWork.Refinancings.SaveForm(refinance);

            }

            foreach (LoanDisbursementEntity item in batchData)
            {
                var refinance = new NmrcRefinancingEntity();
                refinance.Amount = item.Amount;
                refinance.NHFNumber = item.CustomerNhf;
                refinance.PmbId = item.PmbId;
                refinance.Tenor = item.Tenor;
                refinance.Rate = item.Rate;
                refinance.ApplicationDate = DateTime.Now;
                refinance.RefinanceNumber = batchRef;
                refinance.Status = 0;
                refinance.TotalAmount = total;
                refinance.LoanId = item.LoanId;
                refinance.ProductCode = item.ProductCode;
                refinance.LenderID = SecondaryLender;
                refinance.Reviewed = 0;
                refinance.Checklisted = 0;
                refinance.Disbursed = 0;
                await _iUnitOfWork.NmrcRefinance.SaveForm(refinance);

            }

            string message;
            MailParameter mailParameter = new()
            {
                SecondaryLenderEmail = lender.EmailAddress,
                SecondaryLender = lender.Name,
                PmbName = pmb.Name,
                
                UserCompany = "Federal Mortgage Bank of Nigeria"


            };

            var sendMail = EmailHelper.IsLoanRefinanceMailSent(mailParameter, out message);

            obj.Message = "Loan(s) Submitted for refinancing sucessfully";
            obj.Tag = 1;
            return obj;
        }




    }
}