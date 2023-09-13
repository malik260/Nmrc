using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IETicketService
    {
        Task<TData<List<ETicketEntity>>> GetList(ETicketListParam param);
        Task<TData<List<ETicketEntity>>> GetPageList(ETicketListParam param, Pagination pagination);
        Task<TData<List<ETicketEntity>>> GetApprovalPageList(ETicketListParam param, Pagination pagination);

        Task<TData<List<ZtreeInfo>>> GetZtreeETicketList(ETicketListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(ETicketListParam param);
        Task<TData<ETicketEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(ETicketEntity entity);
        Task<TData> DeleteForm(string ids);
        Task<TData<string>> ApproveForm(ETicketEntity entity);

    }
}
