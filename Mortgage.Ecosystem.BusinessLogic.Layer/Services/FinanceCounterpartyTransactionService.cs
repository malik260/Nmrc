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
    public class FinanceCounterpartyTransactionService : IFinanceCounterpartyTransactionService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public FinanceCounterpartyTransactionService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<FinanceCounterpartyTransactionEntity>>> GetList(FinanceCounterpartyTransactionListParam param)
        {
            TData<List<FinanceCounterpartyTransactionEntity>> obj = new TData<List<FinanceCounterpartyTransactionEntity>>();
            obj.Data = await _iUnitOfWork.FinanceCounterpartyTransactions.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj; 
        }

        public async Task<TData<List<FinanceCounterpartyTransactionEntity>>> GetPageList(FinanceCounterpartyTransactionListParam param, Pagination pagination)
        {
            TData<List<FinanceCounterpartyTransactionEntity>> obj = new TData<List<FinanceCounterpartyTransactionEntity>>();
            obj.Data = await _iUnitOfWork.FinanceCounterpartyTransactions.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeFinanceCounterpartyTransactionList(FinanceCounterpartyTransactionListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<FinanceCounterpartyTransactionEntity> financeCounterpartyTransactionList = await _iUnitOfWork.FinanceCounterpartyTransactions.GetList(param);
            foreach (FinanceCounterpartyTransactionEntity financeCounterpartyTransaction in financeCounterpartyTransactionList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = financeCounterpartyTransaction.Id,
                    name = financeCounterpartyTransaction.Ref
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(FinanceCounterpartyTransactionListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<FinanceCounterpartyTransactionEntity> financeCounterpartyTransactionList = await _iUnitOfWork.FinanceCounterpartyTransactions.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (FinanceCounterpartyTransactionEntity financeCounterpartyTransaction in financeCounterpartyTransactionList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = financeCounterpartyTransaction.Id,
                    name = financeCounterpartyTransaction.Ref
                });
                List<long> userIdList = userList.Where(t => t.Company == financeCounterpartyTransaction.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<FinanceCounterpartyTransactionEntity>> GetEntity(long id)
        {
            TData<FinanceCounterpartyTransactionEntity> obj = new TData<FinanceCounterpartyTransactionEntity>();
            obj.Data = await _iUnitOfWork.FinanceCounterpartyTransactions.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.FinanceCounterpartyTransactions.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(FinanceCounterpartyTransactionEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.FinanceCounterpartyTransactions.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.FinanceCounterpartyTransactions.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
