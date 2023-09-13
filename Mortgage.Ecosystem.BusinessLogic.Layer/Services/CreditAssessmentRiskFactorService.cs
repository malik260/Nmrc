using Microsoft.EntityFrameworkCore;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using NPOI.Util;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class CreditAssessmentRiskFactorService : ICreditAssessmentRiskFactorService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public CreditAssessmentRiskFactorService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;

        }

        #region Retrieve data
        public async Task<List<CreditAssessmentRiskFactorEntity>> GetList(string productcode)
        {
            List<CreditAssessmentRiskFactorEntity> obj = new List<CreditAssessmentRiskFactorEntity>();
            obj = await _iUnitOfWork.CreditAssessmentRiskFactors.GetList(productcode);
            
            return obj;
        }



        //public async Task<TData<List<CreditAssessmentRiskFactor>>> GetPageList(CreditAssessmentRiskFactorListParam param, Pagination pagination)
        //{
        //    TData<List<CreditAssessmentRiskFactor>> obj = new TData<List<CreditAssessmentRiskFactor>>();
        //    obj.Data = await _iUnitOfWork.CreditAssessmentRiskFactors.GetPageList(param, pagination);
        //    obj.Total = pagination.TotalCount;
        //    obj.Tag = 1;
        //    return obj;
        //}

        //public async Task<TData<List<ZtreeInfo>>> GetZtreeCreditAssesstmentRiskFactorList(CreditAssessmentRiskFactorListParam param)
        //{
        //    var obj = new TData<List<ZtreeInfo>>();
        //    obj.Data = new List<ZtreeInfo>();
        //    List<CreditAssessmentRiskFactor> creditAssesstmentRiskFactorList = await _iUnitOfWork.CreditAssessmentRiskFactors.GetList(param);
        //    foreach (CreditAssessmentRiskFactor creditAssesstmentRiskFactor in creditAssesstmentRiskFactorList)
        //    {
        //        obj.Data.Add(new ZtreeInfo
        //        {
        //            id = creditAssesstmentRiskFactor.Id,
        //        });
        //    }
        //    obj.Tag = 1;
        //    return obj;
        //}

        //public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(CreditAssessmentRiskFactorListParam param)
        //{
        //    var obj = new TData<List<ZtreeInfo>>();
        //    obj.Data = new List<ZtreeInfo>();
        //    List<CreditAssessmentRiskFactor> creditAssesstmentRiskFactorList = await _iUnitOfWork.CreditAssessmentRiskFactors.GetList(param);
        //    List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
        //    foreach (CreditAssessmentRiskFactor creditAssesstmentRiskFactor in creditAssesstmentRiskFactorList)
        //    {
        //        obj.Data.Add(new ZtreeInfo
        //        {
        //            id = creditAssesstmentRiskFactor.Id,
        //        });
        //        List<long> userIdList = userList.Where(t => t.Company == creditAssesstmentRiskFactor.Id).Select(t => t.Employee).ToList();
        //        foreach (UserEntity user in userList.Where(t => userIdList.Contains(t.Employee)))
        //        {
        //            obj.Data.Add(new ZtreeInfo
        //            {
        //                id = user.Id,
        //                name = user.RealName
        //            });
        //        }
        //    }
        //    obj.Tag = 1;
        //    return obj;
        //}

        public async Task<TData<CreditAssessmentRiskFactorEntity>> GetEntity(long id)
        {
            TData<CreditAssessmentRiskFactorEntity> obj = new TData<CreditAssessmentRiskFactorEntity>();
            obj.Data = await _iUnitOfWork.CreditAssessmentRiskFactors.GetEntity(id)
;
            obj.Tag = 1;
            return obj;
        }

        //public async Task<TData<int>> GetMaxSort()
        //{
        //    TData<int> obj = new TData<int>();
        //    obj.Data = await _iUnitOfWork.CreditTypes.GetMaxSort();
        //    obj.Tag = 1;
        //    return obj;
        //}
        #endregion

        #region Submit data
        //public async Task<TData<string>> SaveForm(CreditAssessmentRiskFactor entity)
        //{
        //    try
        //    {
        //        TData<string> obj = new TData<string>();
        //        var autoIncreament = _iUnitOfWork.CreditAssessmentRiskFactors.GetList(entity.Productcode).Result.ToList();
        //        if (autoIncreament != null)
        //        {
        //            var maxRiskId = autoIncreament.Max(x => x.Riskfactorid);
        //            entity.Riskfactorid = ++maxRiskId;
        //        }

        //        await _iUnitOfWork.CreditAssessmentRiskFactors.SaveForm(entity);
        //        obj.Data = entity.Riskfactorid.ParseToString();
        //        //obj.Data = Id.ParseToString();
        //        obj.Tag = 1;
        //        obj.Message = "Product added successfully";
        //        return obj;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //}

        public async Task<TData<string>> SaveForm(CreditAssessmentRiskFactorEntity entity)
        {
            TData<string> obj = new TData<string>();
            try
            {
               
                var autoIncreament = _iUnitOfWork.CreditAssessmentRiskFactors.GetList(entity.ProductCode).Result.ToList();
                
                if (autoIncreament.Count ==0)
                {
                    entity.RiskFactorId = 1;
                    
                }
                else
                {
                    var maxRiskId = autoIncreament.Max(x => x.RiskFactorId);
                    entity.RiskFactorId = ++maxRiskId;
                    
                }

                await _iUnitOfWork.CreditAssessmentRiskFactors.SaveForm(entity);
                obj.Data = entity.Id.ToString();
                obj.Tag = 1;
                obj.Message = "Risk Factor added successfully";
                return obj;
            }
            
            catch (Exception ex)
            {
                // Log the exception here, you can use a logger of your choice.
                // Example: logger.LogError(ex, "An error occurred while saving the form");
                // This will log the error, and the debugger will not break here during debugging.
            }

            // In case of an exception, return the error response
            obj.Tag = -1;
            obj.Message = "An error occurred while saving the form";
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.CreditAssessmentRiskFactors.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> UpdateForm(CreditAssessmentRiskFactorEntity entity)
        {
            TData<string> obj = new TData<string>();           

            await _iUnitOfWork.CreditAssessmentRiskFactors.SaveForm(entity);

            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }



        #endregion
    }
}