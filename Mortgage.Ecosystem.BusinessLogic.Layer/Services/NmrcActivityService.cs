using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using Mortgage.Ecosystem.DataAccess.Layer.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class NmrcActivityService : INmrcActivityService
    {

        private readonly IUnitOfWork _iUnitOfWork;

        public NmrcActivityService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data

        public async Task<TData<List<RefinancingEntity>>> GetList(RefinancingEntity param)
        {
            TData<List<RefinancingEntity>> obj = new TData<List<RefinancingEntity>>();
            obj.Data = await _iUnitOfWork.NmrcActivity.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<List<RefinancingEntity>>> GetListByBatchId(RefinancingEntity param)
        {
            TData<List<RefinancingEntity>> obj = new TData<List<RefinancingEntity>>();
            obj.Data = await _iUnitOfWork.NmrcActivity.GetList(param);
            foreach (var item in obj.Data)
            {
                var obligorinfo = await _iUnitOfWork.Employees.GetEntityByNhfNumber(long.Parse(item.NHFNumber));
                item.CustomerName = obligorinfo.FirstName + " " + obligorinfo.LastName;
                item.ProductName = _iUnitOfWork.CreditTypes.GetEntityByProductCode(item.ProductCode).Result.Name;
            }
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }



        public async Task<TData<List<NmrcRefinancingEntity>>> GetLoanForDisbursment()
        {
            try
            {
                var context = new ApplicationDbContext();
                var user = await Operator.Instance.Current();
                var userinfo = await _iUnitOfWork.Employees.GetEntity(user.Employee);
                var companyinfo = await _iUnitOfWork.Pmbs.GetEntity(user.Company);
                var query = (from t1 in context.NmrcRefinancingEntity
                             join t2 in context.ApprovalSetupEntity on 5987774554544522399 equals t2.MenuId
                             where t1.Checklisted == 1 && t1.Reviewed == 1 && t2.Authority == user.Employee && t1.Status == 0
                             select t1).Distinct();
                TData<List<NmrcRefinancingEntity>> obj = new TData<List<NmrcRefinancingEntity>>();
                //obj.Data = await _iUnitOfWork.Underwritings.GetLoanForBatching();
                obj.Data = query.ToList();
                foreach (var item in obj.Data)
                {
                    item.PmbName = _iUnitOfWork.Pmbs.GetEntity(item.PmbId).Result.Name;

                }

                obj.Total = obj.Data.Count;
                obj.Tag = 1;
                return obj;
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public async Task<TData<string>> DisburseNonNhfLoan(long Id)
        {
            var db = new ApplicationDbContext();
            var user = await Operator.Instance.Current();
            var obj = new TData<string>();
            var LoanList = db.NmrcRefinancingEntity.Where(i => i.Id == Id).ToList();
            foreach (var item in LoanList)
            {
                item.Status = 1;
                item.Disbursed = 1;
                db.SaveChanges();

            }

            obj.Message = "Loan Disbursed Successfully";
            obj.Tag = 1;
            return obj;
        }




        public async Task<TData<string>> RejectLoanReview(long Id)
        {
            var db = new ApplicationDbContext();
            var user = await Operator.Instance.Current();
            var obj = new TData<string>();
            var loanInfo = await _iUnitOfWork.NmrcRefinance.GetEntity(Id);

            var underwriting = db.NmrcRefinancingEntity.Where(i => i.Id == Id).ToList();
            foreach (var item in underwriting)
            {
                item.Reviewed = 1;
                item.Status = 2;
                item.Disbursed = 2;
                db.SaveChanges();
            }


            db.SaveChanges();
            obj.Message = "Disapproved Successfully";
            obj.Tag = 1;
            return obj;
        }




        public async Task<TData<string>> ApproveLoanReview(long Id)
        {
            var db = new ApplicationDbContext();
            var user = await Operator.Instance.Current();
            var obj = new TData<string>();
            var loanInfo = await _iUnitOfWork.NmrcRefinance.GetEntity(Id);

            var underwriting = db.NmrcRefinancingEntity.Where(i => i.Id == Id).ToList();
            foreach (var item in underwriting)
            {
                item.Reviewed = 1;
                db.SaveChanges();
            }


            db.SaveChanges();
            obj.Message = "Approved Successfully";
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<List<SecondaryLenderChecklistProcedureEntity>>> GetPmbChecklist(long id)
        {

            TData<List<SecondaryLenderChecklistProcedureEntity>> obj = new TData<List<SecondaryLenderChecklistProcedureEntity>>();
            var param = new SecondaryLenderChecklistProcedureEntity();
            param.PmbId = id;
            obj.Data = await _iUnitOfWork.SecondaryLenderChecklistsProcedures.GetList(param);
            obj.Tag = 1;
            return obj;
        }






        public async Task<TData<List<NmrcRefinancingEntity>>> GetPageList(NmrcRefinancingEntity param, Pagination pagination)
        {
            var user = await Operator.Instance.Current();
            TData<List<NmrcRefinancingEntity>> obj = new TData<List<NmrcRefinancingEntity>>();
            obj.Data = await _iUnitOfWork.NmrcRefinance.GetPageList(param, pagination);
            obj.Data = obj.Data.Where(x => x.Status == 0 && x.Disbursed == 0).DistinctBy(x => x.RefinanceNumber).ToList();
            foreach (var item in obj.Data)
            {
                item.ProductName = _iUnitOfWork.CreditTypes.GetEntityByProductCode(item.ProductCode).Result.Name;
                item.PmbName = _iUnitOfWork.Pmbs.GetEntity(item.PmbId).Result.Name;
                item.CustomerName = _iUnitOfWork.Employees.GetEntityByNhfNumber(long.Parse(item.NHFNumber)).Result.FirstName;
            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<RefinancingEntity>> GetEntity(long Id)
        {
            TData<RefinancingEntity> obj = new TData<RefinancingEntity>();
            obj.Data = await _iUnitOfWork.NmrcActivity.GetEntity(Id);
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<List<NmrcRefinancingEntity>>> GetLoanForReview()
        {
            var context = new ApplicationDbContext();
            TData<List<NmrcRefinancingEntity>> obj = new TData<List<NmrcRefinancingEntity>>();
            //obj.Data = await _iUnitOfWork.Underwritings.GetLoanForReview();
            var user = await Operator.Instance.Current();
            var userinfo = await _iUnitOfWork.Employees.GetEntity(user.Employee);
            var companyinfo = await _iUnitOfWork.Pmbs.GetEntity(user.Company);

            var query = from t1 in context.NmrcRefinancingEntity
                        join t2 in context.ApprovalSetupEntity on 5987447100236541478 equals t2.MenuId
                        where t1.Checklisted == 1 && t2.Authority == user.Employee && t1.Reviewed == 0
                        && t1.Amount > 0 && t1.Status != 1
                        select t1;
            //obj.Data = await _iUnitOfWork.Underwritings.GetLoanForUnderwriting();
            obj.Data = query.ToList().DistinctBy(i => i.Id).ToList();
            foreach (var item in obj.Data)
            {
                item.PmbName = _iUnitOfWork.Pmbs.GetEntity(item.PmbId).Result.Name;

            }
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }




        public async Task<TData<string>> ApproveUnderwriting(long id)
        {
            var context = new ApplicationDbContext();
            TData<string> obj = new TData<string>();
            var Item = await _iUnitOfWork.NmrcRefinance.GetEntity(id);
            NmrcRefinancingEntity param = new NmrcRefinancingEntity();
            param.RefinanceNumber = Item.RefinanceNumber;
            var LoanItems = await _iUnitOfWork.NmrcRefinance.GetList(param);
            foreach (var item in LoanItems)
            {
                var checklistInfo = await _iUnitOfWork.SecondaryLenderChecklistsProcedures.GetEntity(item.NHFNumber);
                if (checklistInfo == null)
                {
                    obj.Message = "Please complete Checklist Process For all obligors";
                    obj.Tag = 0;
                    return obj;
                }

            }
            var PmbChecklist = await _iUnitOfWork.SecondaryLenderChecklistsProcedures.GetEntityForPmb(Item.LenderID);
            if (PmbChecklist == null)
            {
                obj.Message = "Please complete Checklist Process for PMB";
                obj.Tag = 0;
                return obj;
            }
            var NmrcRef = context.NmrcRefinancingEntity.Where(x => x.RefinanceNumber == Item.RefinanceNumber).ToList();
            foreach (var item in NmrcRef)
            {
                item.Checklisted = 1;
                context.SaveChanges();
            }

            obj.Message = "Send for review Successfully";
            obj.Tag = 1;
            return obj;
        }


        #endregion Retrieve data

        #region Submit data

        public async Task<TData<string>> SaveForm(RefinancingEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.NmrcActivity.SaveForm(entity);
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



    }
}
