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
    public class PropertyRegistrationService : IPropertyRegistrationService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public PropertyRegistrationService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<PropertyRegistrationEntity>>> GetList(PropertyRegistrationListParam param)
        {
            TData<List<PropertyRegistrationEntity>> obj = new TData<List<PropertyRegistrationEntity>>();
            obj.Data = await _iUnitOfWork.PropertyRegistrations.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<PropertyRegistrationEntity>>> GetPageList(PropertyRegistrationListParam param, Pagination pagination)
        {
            TData<List<PropertyRegistrationEntity>> obj = new TData<List<PropertyRegistrationEntity>>();
            obj.Data = await _iUnitOfWork.PropertyRegistrations.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreePropertyRegistrationList(PropertyRegistrationListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<PropertyRegistrationEntity> propertyRegistrationList = await _iUnitOfWork.PropertyRegistrations.GetList(param);
            foreach (PropertyRegistrationEntity propertyRegistration in propertyRegistrationList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = propertyRegistration.Id,
                    name = propertyRegistration.PropertyType
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(PropertyRegistrationListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<PropertyRegistrationEntity> propertyRegistrationList = await _iUnitOfWork.PropertyRegistrations.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (PropertyRegistrationEntity propertyRegistration in propertyRegistrationList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = propertyRegistration.Id,
                    name = propertyRegistration.PropertyType
                });
                List<long> userIdList = userList.Where(t => t.Company == propertyRegistration.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<PropertyRegistrationEntity>> GetEntity(long id)
        {
            TData<PropertyRegistrationEntity> obj = new TData<PropertyRegistrationEntity>();
            obj.Data = await _iUnitOfWork.PropertyRegistrations.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.PropertyRegistrations.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(PropertyRegistrationEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.PropertyRegistrations.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.PropertyRegistrations.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
