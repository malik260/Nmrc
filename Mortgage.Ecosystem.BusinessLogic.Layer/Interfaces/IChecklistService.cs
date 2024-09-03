using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IChecklistService
    {
        Task<TData<List<ChecklistEntity>>> GetList(ChecklistListParam param);
        Task<TData<List<ChecklistEntity>>> GetPageList(ChecklistListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeChecklistList(ChecklistListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(ChecklistListParam param);
        Task<TData<ChecklistEntity>> GetEntity(int id);
        //Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(ChecklistEntity entity);
        Task<TData<string>> UpdateForm(ChecklistEntity entity);

        Task<TData> DeleteForm(string ids);
    }
}