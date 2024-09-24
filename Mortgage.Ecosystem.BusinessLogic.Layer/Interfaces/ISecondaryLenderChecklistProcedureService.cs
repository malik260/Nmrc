using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ISecondaryLenderChecklistProcedureService
    {

        Task<TData<SecondaryLenderChecklistProcedureEntity>> SaveForm(List<SecondaryLenderCheckListVM> selectedData);
       // Task SaveForms(List<ContributionEntity> entity);
        Task<TData> DeleteForm(string ids);
    }
}
