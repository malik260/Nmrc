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
    public class SecondaryLenderChecklistProcedureService : ISecondaryLenderChecklistProcedureService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public SecondaryLenderChecklistProcedureService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;

        }


        #region Submit data
        public async Task<TData<SecondaryLenderChecklistProcedureEntity>> SaveForm(List<SecondaryLenderCheckListVM> selectedData)
        {
            var db = new ApplicationDbContext();
            TData<SecondaryLenderChecklistProcedureEntity> obj = new TData<SecondaryLenderChecklistProcedureEntity>();
            var employeeInfo = await Operator.Instance.Current();
            List<SecondaryLenderChecklistProcedureEntity> entities = new List<SecondaryLenderChecklistProcedureEntity>();
            foreach (var check in selectedData)
            {
                SecondaryLenderChecklistProcedureEntity entity = new SecondaryLenderChecklistProcedureEntity();
                entity.PmbId = check.PmbId;
                entity.Item = check.Item;
                entity.Applicable = true;
                entity.EmployeeNhfNumber = check.NhfNumber;
                entity.Description = check.Description;
                entities.Add(entity);

            }
            await _iUnitOfWork.SecondaryLenderChecklistsProcedures.SaveForms(entities);

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
