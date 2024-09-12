using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ISecondaryLenderChecklistService
    {

        Task<TData<SecondaryLenderChecklistEntity>> SaveForm(List<SecondaryLenderChecklisVM> selectedData);
     
        Task<TData> DeleteForm(string ids);
    }
}
