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
    public class FeedBackFormService : IFeedBackFormService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public FeedBackFormService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<FeedBackFormEntity>>> GetList(FeedBackFormListParam param)
        {
            TData<List<FeedBackFormEntity>> obj = new TData<List<FeedBackFormEntity>>();
            obj.Data = await _iUnitOfWork.FeedBackForms.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<FeedBackFormEntity>>> GetPageList(FeedBackFormListParam param, Pagination pagination)
        {
            TData<List<FeedBackFormEntity>> obj = new TData<List<FeedBackFormEntity>>();
            obj.Data = await _iUnitOfWork.FeedBackForms.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeFeedBackFormList(FeedBackFormListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<FeedBackFormEntity> feedBackFormList = await _iUnitOfWork.FeedBackForms.GetList(param);
            foreach (FeedBackFormEntity feedBackForm in feedBackFormList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = feedBackForm.Id,
                    name = feedBackForm.Name
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(FeedBackFormListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<FeedBackFormEntity> feedBackFormList = await _iUnitOfWork.FeedBackForms.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (FeedBackFormEntity feedBackForm in feedBackFormList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = feedBackForm.Id,
                    name = feedBackForm.Name
                });
                List<long> userIdList = userList.Where(t => t.Company == feedBackForm.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<FeedBackFormEntity>> GetEntity(long id)
        {
            TData<FeedBackFormEntity> obj = new TData<FeedBackFormEntity>();
            obj.Data = await _iUnitOfWork.FeedBackForms.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.FeedBackForms.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(FeedBackFormEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.FeedBackForms.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.FeedBackForms.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
