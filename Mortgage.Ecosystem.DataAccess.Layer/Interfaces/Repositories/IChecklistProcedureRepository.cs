using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IChecklistProcedureRepository
    { 
        Task SaveForm(ChecklistProcedureEntity entity);
        Task DeleteForm(string ids);
        Task SaveForms(List<ChecklistProcedureEntity> entity);
    }
}
