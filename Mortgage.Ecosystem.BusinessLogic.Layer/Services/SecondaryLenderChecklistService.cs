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
    public class SecondaryLenderChecklistService : ISecondaryLenderChecklistService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public SecondaryLenderChecklistService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;

        }


        #region Submit data
        public async Task<TData<SecondaryLenderChecklistEntity>> SaveForm(List<SecondaryLenderChecklisVM> selectedData)
        {
            var db = new ApplicationDbContext();
            TData<SecondaryLenderChecklistEntity> obj = new TData<SecondaryLenderChecklistEntity>();
            var employeeInfo = await Operator.Instance.Current();
            List<SecondaryLenderChecklistEntity> entities = new List<SecondaryLenderChecklistEntity>();
            foreach (var check in selectedData)
            {
                SecondaryLenderChecklistEntity entity = new SecondaryLenderChecklistEntity();
                entity.PmbId  = check.PmbId;
                entity.Item = check.Item;
                entity.Description = "";
                entity.Applicable = true;
                
                entity.EmployeeNhfNumber = check.EmployeeNhfNumber;
  
                entities.Add(entity);

            }
            await _iUnitOfWork.SecondaryLenderChecklist.SaveForms(entities);


            obj.Tag = 1;
            obj.Message = "Secondary Lender Checklist added successfully";
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.SecondaryLenderChecklist.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }




        #endregion
    }
}
