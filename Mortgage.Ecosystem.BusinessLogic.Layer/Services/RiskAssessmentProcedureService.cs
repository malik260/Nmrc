using Microsoft.EntityFrameworkCore;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class RiskAssessmentProcedureService : IRiskAssessmentProcedureService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public RiskAssessmentProcedureService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;

        }


        #region Submit data
        public async Task<TData<string>> SaveForm(RiskAssessmentProcedureDTO selectedData)
        {
            var db = new ApplicationDbContext();
            TData<string> obj = new TData<string>();
            var userInfo = await Operator.Instance.Current();
            var interestRate =  _iUnitOfWork.LoanInitiations.GetEntity(selectedData?.NhfNumber).Result;
            var CustomerRating = await _iUnitOfWork.CreditScores.GetScorebyWeight(selectedData.Weight);
            RiskAssessmentProcedureEntity entity = new RiskAssessmentProcedureEntity();
            entity.ApprovedBy = userInfo.UserName;
           // entity.AverageScore = Convert.ToDecimal(CustomerRating.RangeMin + "-" + CustomerRating.RangeMax);
            //entity.AverageScore = Convert.ToString($"{CustomerRating.RangeMin}-{CustomerRating.RangeMax}");
            entity.AverageScore = Convert.ToString(selectedData.Weight);
            entity.BranchCode = "";
            entity.Comment = selectedData.Remark;
            entity.Date = DateTime.Now;
             entity.InterestRate = interestRate.Rate;
            entity.NHFNo = selectedData.NhfNumber;
            entity.ApprovedBy = userInfo.UserName;
            entity.Rating = CustomerRating.Rating;
            entity.RiskOfficer = userInfo.UserName;
            entity.Status = "1";
            entity.Remark = selectedData.Remark;
            entity.Date = DateTime.Now;
            entity.LoanId = selectedData.LoanId;
            entity.Rating = CustomerRating.CreditGrade;
            await _iUnitOfWork.RiskAssessmentProcedure.SaveForm(entity);
            entity.RiskMessage = CustomerRating.CreditGradeDefinition;
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            obj.Message = "Risk Rating successful, Risk Score:" + " " + entity.AverageScore + " - " + entity.RiskMessage.ToUpper();
            return obj;
        }


        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.ChecklistsProcedure.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

       


        #endregion
    }
}
