﻿using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ICustomerProfileUpdateRepository
    {
        Task<List<CustomerProfileUpdateEntity>> GetList(CustomerProfileUpdateListParam param);
        Task<List<CustomerProfileUpdateEntity>> GetPageList(CustomerProfileUpdateListParam param, Pagination pagination);
        Task<List<CustomerProfileUpdateEntity>> GetApprovalPageList(CustomerProfileUpdateListParam param, Pagination pagination);
        Task<CustomerProfileUpdateEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(CustomerProfileUpdateEntity entity);
        Task DeleteForm(string ids);
        Task<bool> ApproveForm(CustomerProfileUpdateEntity entity, MenuEntity menu, OperatorInfo user, UserEntity loginProfile);
        Task RejectForm(CustomerProfileUpdateEntity entity);
        //Task<CustomerDetailsViewModel?> GetPageList(CustomerProfileUpdateListParam param);
        //Task<CustomerProfileUpdateEntity> GetEntity(long id);



        //Task<CustomerProfileUpdateEntity> GetById(long id);

    }
}
