using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ISecondaryLenderChecklistRepository
    { 
        Task SaveForm(SecondaryLenderChecklistEntity entity);
        Task DeleteForm(string ids);
        Task SaveForms(List<SecondaryLenderChecklistEntity> entity);
        Task<SecondaryLenderChecklistEntity> GetEntity(string Nhf);
    }
}
