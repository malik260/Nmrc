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
    public class ContributionHistoryService : IContributionHistoryService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public ContributionHistoryService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<ContributionHistoryEntity>>> GetList(ContributionHistoryListParam param)
        {
            TData<List<ContributionHistoryEntity>> obj = new TData<List<ContributionHistoryEntity>>();
            obj.Data = await _iUnitOfWork.ContributionHistories.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ContributionHistoryEntity>>> GetPageList(ContributionHistoryListParam param, Pagination pagination)
        {
            TData<List<ContributionHistoryEntity>> obj = new TData<List<ContributionHistoryEntity>>();
            obj.Data = await _iUnitOfWork.ContributionHistories.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeContributionHistoryList(ContributionHistoryListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ContributionHistoryEntity> contributionHistoryList = await _iUnitOfWork.ContributionHistories.GetList(param);
            foreach (ContributionHistoryEntity contributionHistory in contributionHistoryList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = contributionHistory.Id,
                    name = contributionHistory.Name
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(ContributionHistoryListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ContributionHistoryEntity> contributionHistoryList = await _iUnitOfWork.ContributionHistories.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (ContributionHistoryEntity contributionHistory in contributionHistoryList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = contributionHistory.Id,
                    name = contributionHistory.Name
                });
                List<long> userIdList = userList.Where(t => t.Company == contributionHistory.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<ContributionHistoryEntity>> GetEntity(long id)
        {
            TData<ContributionHistoryEntity> obj = new TData<ContributionHistoryEntity>();
            obj.Data = await _iUnitOfWork.ContributionHistories.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.ContributionHistories.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(ContributionHistoryEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.ContributionHistories.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.ContributionHistories.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
