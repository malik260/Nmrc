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
    public class InternetBankingUsersService : IInternetBankingUsersService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public InternetBankingUsersService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<InternetBankingUsersEntity>>> GetList(InternetBankingUsersListParam param)
        {
            TData<List<InternetBankingUsersEntity>> obj = new TData<List<InternetBankingUsersEntity>>();
            obj.Data = await _iUnitOfWork.InternetBankingUsers.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj; 
        }

        public async Task<TData<List<InternetBankingUsersEntity>>> GetPageList(InternetBankingUsersListParam param, Pagination pagination)
        {
            TData<List<InternetBankingUsersEntity>> obj = new TData<List<InternetBankingUsersEntity>>();
            obj.Data = await _iUnitOfWork.InternetBankingUsers.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeInternetBankingUsersList(InternetBankingUsersListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<InternetBankingUsersEntity> eTicketList = await _iUnitOfWork.InternetBankingUsers.GetList(param);
            foreach (InternetBankingUsersEntity eTicket in eTicketList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = eTicket.Id,
                    name = eTicket.AccountNo
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(InternetBankingUsersListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<InternetBankingUsersEntity> internetBankingUsersList = await _iUnitOfWork.InternetBankingUsers.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (InternetBankingUsersEntity internetBankingUsers in internetBankingUsersList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = internetBankingUsers.Id,
                    name = internetBankingUsers.AccountNo
                });
                List<long> userIdList = userList.Where(t => t.Company == internetBankingUsers.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<InternetBankingUsersEntity>> GetEntity(long id)
        {
            TData<InternetBankingUsersEntity> obj = new TData<InternetBankingUsersEntity>();
            obj.Data = await _iUnitOfWork.InternetBankingUsers.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.InternetBankingUsers.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(InternetBankingUsersEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.InternetBankingUsers.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
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
