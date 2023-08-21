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
    public class CreditScoreService : ICreditScoreService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public CreditScoreService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<CreditScoreEntity>>> GetList(CreditScoreListParam param)
        {
            TData<List<CreditScoreEntity>> obj = new TData<List<CreditScoreEntity>>();
            obj.Data = await _iUnitOfWork.CreditScores.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<CreditScoreEntity>>> GetPageList(CreditScoreListParam param, Pagination pagination)
        {
            TData<List<CreditScoreEntity>> obj = new TData<List<CreditScoreEntity>>();
            obj.Data = await _iUnitOfWork.CreditScores.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeCreditScoreList(CreditScoreListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<CreditScoreEntity> creditScoreList = await _iUnitOfWork.CreditScores.GetList(param);
            foreach (CreditScoreEntity creditScore in creditScoreList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = creditScore.Id,
                    name = creditScore.CreditType
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(CreditScoreListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<CreditScoreEntity> creditScoreList = await _iUnitOfWork.CreditScores.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (CreditScoreEntity creditScore in creditScoreList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = creditScore.Id,
                    name = creditScore.CreditType
                });
                List<long> userIdList = userList.Where(t => t.Company == creditScore.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<CreditScoreEntity>> GetEntity(long id)
        {
            TData<CreditScoreEntity> obj = new TData<CreditScoreEntity>();
            obj.Data = await _iUnitOfWork.CreditScores.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.CreditScores.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(CreditScoreEntity entity)
        {
            TData<string> obj = new TData<string>();
            //entity.CreditType = _iUnitOfWork.CreditType.GetEntity(entity.CreditType.ParseToLong()).Result.Name;
            await _iUnitOfWork.CreditScores.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            obj.Message = "Credit Score Added Successful";
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.CreditScores.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
