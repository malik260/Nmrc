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
    public class ContributionRefundPostingService : IContributionRefundPostingService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public ContributionRefundPostingService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<ContributionRefundPostingEntity>>> GetList(ContributionRefundPostingListParam param)
        {
            TData<List<ContributionRefundPostingEntity>> obj = new TData<List<ContributionRefundPostingEntity>>();
            obj.Data = await _iUnitOfWork.ContributionRefundPostings.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj; 
        }

        public async Task<TData<List<ContributionRefundPostingEntity>>> GetPageList(ContributionRefundPostingListParam param, Pagination pagination)
        {
            TData<List<ContributionRefundPostingEntity>> obj = new TData<List<ContributionRefundPostingEntity>>();
            obj.Data = await _iUnitOfWork.ContributionRefundPostings.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeContributionRefundPostingList(ContributionRefundPostingListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ContributionRefundPostingEntity> contributionRefundPostingList = await _iUnitOfWork.ContributionRefundPostings.GetList(param);
            foreach (ContributionRefundPostingEntity contributionRefundPosting in contributionRefundPostingList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = contributionRefundPosting.Id,
                    name = contributionRefundPosting.LedgerCr
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(ContributionRefundPostingListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ContributionRefundPostingEntity> contributionRefundPostingList = await _iUnitOfWork.ContributionRefundPostings.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (ContributionRefundPostingEntity contributionRefundPosting in contributionRefundPostingList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = contributionRefundPosting.Id,
                    name = contributionRefundPosting.LedgerCr
                });
                List<long> userIdList = userList.Where(t => t.Company == contributionRefundPosting.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<ContributionRefundPostingEntity>> GetEntity(long id)
        {
            TData<ContributionRefundPostingEntity> obj = new TData<ContributionRefundPostingEntity>();
            obj.Data = await _iUnitOfWork.ContributionRefundPostings.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.ContributionRefundPostings.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(ContributionRefundPostingEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.ContributionRefundPostings.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.ContributionRefundPostings.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
