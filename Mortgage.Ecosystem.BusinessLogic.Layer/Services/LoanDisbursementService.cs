using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Resources;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class LoanDisbursementService : ILoanDisbursementService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        public LoanDisbursementService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
            

        }

        public async Task<TData<List<LoanDisbursementEntity>>> GetList(LoanDisbursementDto param)
        {
            TData<List<LoanDisbursementEntity>> obj = new TData<List<LoanDisbursementEntity>>();
            obj.Data = await _iUnitOfWork.LoanDisbursement.GetList(param);           
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<LoanDisbursementEntity>>> GetPageList(LoanDisbursementDto param, Pagination pagination)
        {
            var user = await Operator.Instance.Current();
            param.PmbId = user.Company;
            TData<List<LoanDisbursementEntity>> obj = new TData<List<LoanDisbursementEntity>>();
            obj.Data = await _iUnitOfWork.LoanDisbursement.GetPageList(param, pagination);
            foreach (var item in obj.Data)
            {
                var customerInfo = await _iUnitOfWork.Employees.GetEntityByNhfNumber(long.Parse(item.CustomerNhf));
                item.ProductName = _iUnitOfWork.CreditTypes.GetEntityByProductCode(item.ProductCode).Result.Name;
                item.CustomerName = customerInfo.FirstName + " " + customerInfo.LastName;                       
            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }



    }
}
