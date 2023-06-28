using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class ETicketService : IETicketService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public ETicketService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<ETicketEntity>>> GetList(ETicketListParam param)
        {
            TData<List<ETicketEntity>> obj = new TData<List<ETicketEntity>>();
            obj.Data = await _iUnitOfWork.ETickets.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ETicketEntity>>> GetPageList(ETicketListParam param, Pagination pagination)
        {
            TData<List<ETicketEntity>> obj = new TData<List<ETicketEntity>>();
            obj.Data = await _iUnitOfWork.ETickets.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeETicketList(ETicketListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ETicketEntity> eTicketList = await _iUnitOfWork.ETickets.GetList(param);
            foreach (ETicketEntity eTicket in eTicketList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = eTicket.Id,
                    name = eTicket.RequestNumber
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(ETicketListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ETicketEntity> eTicketList = await _iUnitOfWork.ETickets.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (ETicketEntity eTicket in eTicketList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = eTicket.Id,
                    name = eTicket.RequestNumber
                });
                List<long> userIdList = userList.Where(t => t.Company == eTicket.Id).Select(t => t.Employee).ToList();
                foreach (UserEntity user in userList.Where(t => userIdList.Contains(t.Employee)))
                {
                    obj.Data.Add(new ZtreeInfo
                    {
                        id = user.Id,
                        name = user.RealName
                    });
                }
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<ETicketEntity>> GetEntity(long id)
        {
            TData<ETicketEntity> obj = new TData<ETicketEntity>();
            obj.Data = await _iUnitOfWork.ETickets.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.ETickets.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(ETicketEntity entity)
        {
            TData<string> obj = new TData<string>();

            // Generate a random five-digit number
            Random random = new Random();
            int randomNumber = random.Next(10000, 99999);

            // Set the RequestNumber with the generated random number
            entity.RequestNumber = randomNumber.ToString();

            // Determine the ApprovalStatus...
            if (entity.Approved == 1 && entity.Disapproved == 0)
            {
                entity.ApprovalStatus = "Approved";
            }
            else if (entity.Approved == 0 && entity.Disapproved == 1)
            {
                entity.ApprovalStatus = "Disapproved";
            }
            else
            {
                entity.ApprovalStatus = "Pending";
            }

            await _iUnitOfWork.ETickets.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.ETickets.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
