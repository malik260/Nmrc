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
    public class UnderwritingService : IUnderwritingService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public UnderwritingService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<UnderwritingEntity>>> GetList(UnderwritingListParam param)
        {
            TData<List<UnderwritingEntity>> obj = new TData<List<UnderwritingEntity>>();
            obj.Data = await _iUnitOfWork.Underwritings.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<UnderwritingEntity>>> GetPageList(UnderwritingListParam param, Pagination pagination)
        {
            TData<List<UnderwritingEntity>> obj = new TData<List<UnderwritingEntity>>();
            obj.Data = await _iUnitOfWork.Underwritings.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUnderwritingList(UnderwritingListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<UnderwritingEntity> underwritingList = await _iUnitOfWork.Underwritings.GetList(param);
            foreach (UnderwritingEntity underwriting in underwritingList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = underwriting.Id,
                    name = underwriting.Name
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(UnderwritingListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<UnderwritingEntity> underwritingList = await _iUnitOfWork.Underwritings.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (UnderwritingEntity underwriting in underwritingList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = underwriting.Id,
                    name = underwriting.Name
                });
                List<long> userIdList = userList.Where(t => t.Company == underwriting.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<UnderwritingEntity>> GetEntity(long id)
        {
            TData<UnderwritingEntity> obj = new TData<UnderwritingEntity>();
            obj.Data = await _iUnitOfWork.Underwritings.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.PropertySubscriptions.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(UnderwritingEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.Underwritings.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.Underwritings.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
