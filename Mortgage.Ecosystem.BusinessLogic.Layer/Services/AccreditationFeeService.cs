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
    public class AccreditationFeeService : IAccreditationFeeService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public AccreditationFeeService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<AccreditationFeeEntity>>> GetList(AccreditationFeeListParam param)
        {
            TData<List<AccreditationFeeEntity>> obj = new TData<List<AccreditationFeeEntity>>();
            obj.Data = await _iUnitOfWork.AccreditationFees.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<AccreditationFeeEntity>>> GetPageList(AccreditationFeeListParam param, Pagination pagination)
        {
            TData<List<AccreditationFeeEntity>> obj = new TData<List<AccreditationFeeEntity>>();
            obj.Data = await _iUnitOfWork.AccreditationFees.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeAccreditationFeeList(AccreditationFeeListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<AccreditationFeeEntity> accredidationFeeList = await _iUnitOfWork.AccreditationFees.GetList(param);
            foreach (AccreditationFeeEntity accredidationFee in accredidationFeeList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = accredidationFee.Id,
                    name = accredidationFee.AgenName
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(AccreditationFeeListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<AccreditationFeeEntity> accredidationFeeList = await _iUnitOfWork.AccreditationFees.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (AccreditationFeeEntity accredidationFee in accredidationFeeList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = accredidationFee.Id,
                    name = accredidationFee.AgenName
                });
                List<long> userIdList = userList.Where(t => t.Company == accredidationFee.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<AccreditationFeeEntity>> GetEntity(long id)
        {
            TData<AccreditationFeeEntity> obj = new TData<AccreditationFeeEntity>();
            obj.Data = await _iUnitOfWork.AccreditationFees.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.AccreditationFees.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(AccreditationFeeEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.AccreditationFees.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.AccreditationFees.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
