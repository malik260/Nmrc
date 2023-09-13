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
    public class StatementOfAccountService : IStatementOfAccountService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public StatementOfAccountService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<FinanceCounterpartyTransactionEntity>>> GetList(StatementOfAccountListParam param)
        {
            TData<List<FinanceCounterpartyTransactionEntity>> obj = new TData<List<FinanceCounterpartyTransactionEntity>>();
            var loggedUser = await Operator.Instance.Current();
            param.NHFNumber = loggedUser.Employee.ToStr();
            obj.Data = await _iUnitOfWork.StatementOfAccounts.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<FinanceCounterpartyTransactionEntity>>> GetPageList(StatementOfAccountListParam param, Pagination pagination)
        {
            TData<List<FinanceCounterpartyTransactionEntity>> obj = new TData<List<FinanceCounterpartyTransactionEntity>>();
            var loggedUser = await Operator.Instance.Current();
            param.NHFNumber =  loggedUser.Employee.ToStr();
            obj.Data = await _iUnitOfWork.StatementOfAccounts.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        //public async Task<TData<List<ZtreeInfo>>> GetZtreeStatementOfAccountList(StatementOfAccountListParam param)
        //{
        //    var obj = new TData<List<ZtreeInfo>>();
        //    obj.Data = new List<ZtreeInfo>();
        //    List<StatementOfAccountEntity> statementOfAccountList = await _iUnitOfWork.StatementOfAccounts.GetList(param);
        //    foreach (StatementOfAccountEntity statementOfAccount in statementOfAccountList)
        //    {
        //        obj.Data.Add(new ZtreeInfo
        //        {
        //            id = statementOfAccount.Id,
        //            name = statementOfAccount.FirstName
        //        });
        //    }
        //    obj.Tag = 1;
        //    return obj;
        //}

        //public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(StatementOfAccountListParam param)
        //{
        //    var obj = new TData<List<ZtreeInfo>>();
        //    obj.Data = new List<ZtreeInfo>();
        //    List<FinanceCounterpartyTransactionEntity> statementOfAccountList = await _iUnitOfWork.StatementOfAccounts.GetList(param);
        //    List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
        //    foreach (FinanceCounterpartyTransactionEntity statementOfAccount in statementOfAccountList)
        //    {
        //        obj.Data.Add(new ZtreeInfo
        //        {
        //            id = statementOfAccount.Id,
        //            name = statementOfAccount.FirstName
        //        });
        //        List<long> userIdList = userList.Where(t => t.Company == statementOfAccount.Id).Select(t => t.Employee).ToList();
        //        foreach (UserEntity user in userList.Where(t => userIdList.Contains(t.Employee)))
        //        {
        //            obj.Data.Add(new ZtreeInfo
        //            {
        //                id = user.Id,
        //                name = user.RealName
        //            });
        //        }
        //    }
        //    obj.Tag = 1;
        //    return obj;
        //}

        public async Task<TData<StatementOfAccountEntity>> GetEntity(long id)
        {
            TData<StatementOfAccountEntity> obj = new TData<StatementOfAccountEntity>();
            obj.Data = await _iUnitOfWork.StatementOfAccounts.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.StatementOfAccounts.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(StatementOfAccountEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.StatementOfAccounts.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.StatementOfAccounts.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
