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
    public class PropertySubscriptionService : IPropertySubscriptionService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public PropertySubscriptionService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<PropertySubscriptionEntity>>> GetList(PropertySubscriptionListParam param)
        {
            TData<List<PropertySubscriptionEntity>> obj = new TData<List<PropertySubscriptionEntity>>();
            obj.Data = await _iUnitOfWork.PropertySubscriptions.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<PropertySubscriptionEntity>>> GetPageList(PropertySubscriptionListParam param, Pagination pagination)
        {
            TData<List<PropertySubscriptionEntity>> obj = new TData<List<PropertySubscriptionEntity>>();
            obj.Data = await _iUnitOfWork.PropertySubscriptions.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreePropertySubscriptionList(PropertySubscriptionListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<PropertySubscriptionEntity> propertySubscriptionList = await _iUnitOfWork.PropertySubscriptions.GetList(param);
            foreach (PropertySubscriptionEntity propertySubscription in propertySubscriptionList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = propertySubscription.Id,
                    name = propertySubscription.PropertyType
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(PropertySubscriptionListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<PropertySubscriptionEntity> propertySubscriptionList = await _iUnitOfWork.PropertySubscriptions.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (PropertySubscriptionEntity propertySubscription in propertySubscriptionList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = propertySubscription.Id,
                    name = propertySubscription.PropertyType
                });
                List<long> userIdList = userList.Where(t => t.Company == propertySubscription.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<PropertySubscriptionEntity>> GetEntity(long id)
        {
            TData<PropertySubscriptionEntity> obj = new TData<PropertySubscriptionEntity>();
            obj.Data = await _iUnitOfWork.PropertySubscriptions.GetEntity(id);
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
        public async Task<TData<string>> SaveForm(PropertySubscriptionEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.PropertySubscriptions.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> Subscribe(long id)
        {

            TData<string> obj = new TData<string>();
            var user = await Operator.Instance.Current();
            var userInfo = await _iUnitOfWork.Employees.GetEntity(user.Employee);
            var propertydetails = await _iUnitOfWork.PropertyRegistrations.GetEntity(id);
            PropertySubscriptionEntity entity = new PropertySubscriptionEntity();
            entity.Developer = Convert.ToString(propertydetails.ComapnyNumber);
            entity.PropertyDescription = propertydetails.PropertyDescription;
            entity.PropertyLocation = propertydetails.PropertyLocation;
            entity.PropertyType = propertydetails.PropertyType;
            entity.PhoneNumber = user.EmployeeInfo.MobileNumber;
            entity.Subscriber = Convert.ToString(userInfo.NHFNumber);
            var employerExist = await _iUnitOfWork.Pmbs.GetEntitybyNhf(entity.Developer);
            if(employerExist == null)
            {
                obj.Tag = 0;
                obj.Message = "PMB associated to selected properties has been Deactivated";
                return obj;
            }
            entity.Email = user.EmployeeInfo.EmailAddress;
            await _iUnitOfWork.PropertySubscriptions.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            obj.Message = "Property Subscribed successfully";
            return obj;
        }



        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.PropertySubscriptions.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
