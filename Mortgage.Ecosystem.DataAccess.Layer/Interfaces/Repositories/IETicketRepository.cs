using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IETicketRepository
    {
        Task<List<ETicketEntity>> GetList(ETicketListParam param);
        Task<List<ETicketEntity>> GetPageList(ETicketListParam param, Pagination pagination);
        Task<List<ETicketEntity>> GetEmployeePageList(ETicketListParam param, Pagination pagination);
        Task<List<ETicketEntity>> GetApprovalPageList(ETicketListParam param, Pagination pagination);
        Task<ETicketEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task<ETicketEntity> GetEntityDetails(long id);
        Task SaveForm(ETicketEntity entity);
        Task UpdateForm(ETicketEntity entity);
        Task DeleteForm(string ids);
        Task ApproveForm(ETicketEntity entity, MenuEntity menu, OperatorInfo user);

    }
}
