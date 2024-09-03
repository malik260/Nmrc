using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
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
        public async Task<TData<List<PropertyRegistrationEntity>>> GetList(PropertyRegistrationListParam param, Pagination pagination)
        {
            TData<List<PropertyRegistrationEntity>> obj = new TData<List<PropertyRegistrationEntity>>();
            obj.Data = await _iUnitOfWork.PropertyRegistrations.GetList(param, pagination);
            foreach (var item in obj.Data)
            {
                item.PropertyType = ConversionEnum.GetDescriptionbyNumber<PropertyTypeEnum>(Convert.ToInt32(item.PropertyType));
                long location = long.Parse(item.PropertyLocation);
                item.PropertyLocation = _iUnitOfWork.States.GetEntity(location).Result.Name.ToString();

            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<PropertyRegistrationEntity>> GetEntities(long id)
        {
            TData<PropertyRegistrationEntity> obj = new TData<PropertyRegistrationEntity>();

            try
            {
                PropertyRegistrationEntity entity;

                if (id == 0)
                {
                    entity = new PropertyRegistrationEntity();
                }
                else
                {

                    entity = await _iUnitOfWork.PropertyRegistrations.GetEntities(id);

                    if (entity == null)
                    {
                        obj.Message = "Entity not found.";
                        obj.Tag = -1;
                        return obj;
                    }
                }

                obj.Data = entity;
                obj.Tag = 1;
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
                obj.Tag = -1;
            }

            return obj;
        }


        public async Task<TData<List<PropertyRegistrationEntity>>> GetPageList(PropertyRegistrationListParam param, Pagination pagination)
        {
            var user = await Operator.Instance.Current();
            var pmbnhf = await _iUnitOfWork.Pmbs.GetEntity(user.Company);
            param.companyNumber = long.Parse(pmbnhf.NHFNumber);
            TData<List<PropertyRegistrationEntity>> obj = new TData<List<PropertyRegistrationEntity>>();
            obj.Data = await _iUnitOfWork.PropertyRegistrations.GetPageList(param, pagination);
            foreach (var item in obj.Data)
            {
                item.PropertyType = ConversionEnum.GetDescriptionbyNumber<PropertyTypeEnum>(Convert.ToInt32(item.PropertyType));
                long location = long.Parse(item.PropertyLocation);
                item.PropertyLocation = _iUnitOfWork.States.GetEntity(location).Result.Name.ToString();

            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        //public async Task<TData<List<ZtreeInfo>>> GetZtreePropertyRegistrationList(PropertyRegistrationListParam param)
        //{
        //    var obj = new TData<List<ZtreeInfo>>();
        //    obj.Data = new List<ZtreeInfo>();
        //    List<PropertyRegistrationEntity> propertyRegistrationList = await _iUnitOfWork.PropertyRegistrations.GetList(param);
        //    foreach (PropertyRegistrationEntity propertyRegistration in propertyRegistrationList)
        //    {
        //        obj.Data.Add(new ZtreeInfo
        //        {
        //            id = propertyRegistration.Id,
        //            name = propertyRegistration.PropertyLocation
        //        });
        //    }
        //    obj.Tag = 1;
        //    return obj;
        //}

        //public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(PropertyRegistrationListParam param)
        //{
        //    var obj = new TData<List<ZtreeInfo>>();
        //    obj.Data = new List<ZtreeInfo>();
        //    List<PropertyRegistrationEntity> propertyRegistrationList = await _iUnitOfWork.PropertyRegistrations.GetList(param);
        //    List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
        //    foreach (PropertyRegistrationEntity propertyRegistration in propertyRegistrationList)
        //    {
        //        obj.Data.Add(new ZtreeInfo
        //        {
        //            id = propertyRegistration.Id,
        //            name = propertyRegistration.PropertyLocation
        //        });
        //        List<long> userIdList = userList.Where(t => t.Company == propertyRegistration.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<List<PropertyRegistrationListParam>>> GetEntity(long id)
        {
            TData<List<PropertyRegistrationListParam>> obj = new TData<List<PropertyRegistrationListParam>>();
            var Data = new PropertyRegistrationListParam();
            var Datas = new List<PropertyRegistrationListParam>();
            var result = await _iUnitOfWork.PropertyRegistrations.GetEntity(id);
            Data.longitude = result.Longitude;
            Data.latitude = result.Latitude;
            Data.propertyDescription = result.PropertyDescription;
            Data.phoneNumber = result.PhoneNumber;
            Data.companyName = result.ComapnyName;
            List<byte[]>? photos = new List<byte[]>();
            var images = _iUnitOfWork.PropertyUploads.GetList(id).Result;
            foreach (var item in images)
            {
                photos.Add(item.filedata);

            }
            Data.files = photos;
            Datas.Add(Data);
            obj.Data = Datas;
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

        public async Task<TData<CustomerDetailsViewModel>> GetPmbCompanyName()
        {
            TData<CustomerDetailsViewModel> obj = new TData<CustomerDetailsViewModel>();
            var user = await Operator.Instance.Current();
            var customerDetails = await _iUnitOfWork.Pmbs.GetEntity(user.Company);

            var custDetails = new CustomerDetailsViewModel
            {

                PmbName = customerDetails.Name,
                PmbNo = customerDetails.NHFNumber.ToString(),
                MobileNo = customerDetails.MobileNumber,
                Email = customerDetails.EmailAddress
            };

            obj.Data = custDetails;
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(PropertyRegistrationEntity entity)
        {
            TData<string> obj = new TData<string>();
            try
            {
                var user = await Operator.Instance.Current();
                //var pmb = await _iUnitOfWork.Pmbs.GetEntitybyEmail(user.);
                await _iUnitOfWork.PropertyRegistrations.SaveForm(entity);
                obj.Data = entity.Id.ParseToString();
                obj.Tag = 1;
                obj.Message = "Property Registered Successfully";
            }
            catch (Exception ex)
            {
                obj.Tag = -1;
                obj.Message = "An error occurred while registering the property.";
            }
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
