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
    public class ChargeSetupService : IChargeSetupService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public ChargeSetupService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<ChargeSetupEntity>>> GetList(ChargeSetupListParam param)
        {
            TData<List<ChargeSetupEntity>> obj = new TData<List<ChargeSetupEntity>>();
            obj.Data = await _iUnitOfWork.ChargeSetups.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ChargeSetupEntity>>> GetPageList(ChargeSetupListParam param, Pagination pagination)
        {
            TData<List<ChargeSetupEntity>> obj = new TData<List<ChargeSetupEntity>>();
            obj.Data = await _iUnitOfWork.ChargeSetups.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeChargeSetupList(ChargeSetupListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ChargeSetupEntity> ChargeSetupList = await _iUnitOfWork.ChargeSetups.GetList(param);
            foreach (ChargeSetupEntity ChargeSetup in ChargeSetupList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = ChargeSetup.Id,
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(ChargeSetupListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ChargeSetupEntity> ChargeSetupList = await _iUnitOfWork.ChargeSetups.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (ChargeSetupEntity ChargeSetup in ChargeSetupList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = ChargeSetup.Id,
                });
                List<long> userIdList = userList.Where(t => t.Company == ChargeSetup.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<ChargeSetupEntity>> GetEntity(long id)
        {
            TData<ChargeSetupEntity> obj = new TData<ChargeSetupEntity>();
            obj.Data = await _iUnitOfWork.ChargeSetups.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.ChargeSetups.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(ChargeSetupEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.ChargeSetups.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.ChargeSetups.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
