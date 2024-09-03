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
    public class ChecklistProcedureService : IChecklistProcedureService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public ChecklistProcedureService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;

        }


        #region Submit data
        public async Task<TData<ChecklistProcedureEntity>> SaveForm(List<CheckListVM> selectedData)
        {
            var db = new ApplicationDbContext();
            TData<ChecklistProcedureEntity> obj = new TData<ChecklistProcedureEntity>();
            var employeeInfo = await Operator.Instance.Current();
            List<ChecklistProcedureEntity> entities = new List<ChecklistProcedureEntity>();
            foreach (var check in selectedData)
            {
                ChecklistProcedureEntity entity = new ChecklistProcedureEntity();
                entity.Checklist = check.Checklist;
                entity.Remark = check.Remark;
                entity.Applicable = "1";
                entity.NonApplicable = "0";
                entity.NHFNo = check.NhfNumber;
                entity.ProductCode = check.ProductCode;
                entity.BranchCode = Convert.ToString(employeeInfo.EmployeeInfo.Branch);
                entities.Add(entity);

            }
            await _iUnitOfWork.ChecklistsProcedure.SaveForms(entities);

            //var underwriting = db.UnderwritingEntity.Where(i => i.NHFNumber == selectedData.FirstOrDefault().NhfNumber).DefaultIfEmpty().FirstOrDefault();
            //underwriting.CheckList = "1";
            //db.SaveChanges();
            //obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            obj.Message = "Checklist added successfully";
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.Checklists.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }




        #endregion
    }
}
