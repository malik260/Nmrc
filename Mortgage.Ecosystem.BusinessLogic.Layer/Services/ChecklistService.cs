﻿using Microsoft.EntityFrameworkCore;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer;
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
    public class ChecklistService : IChecklistService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public ChecklistService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;

        }

        #region Retrieve data
        public async Task<TData<List<ChecklistEntity>>> GetList(ChecklistListParam param)
        {
            TData<List<ChecklistEntity>> obj = new TData<List<ChecklistEntity>>();
            obj.Data = await _iUnitOfWork.Checklists.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ChecklistEntity>>> GetPageList(ChecklistListParam param, Pagination pagination)
        {
            TData<List<ChecklistEntity>> obj = new TData<List<ChecklistEntity>>();
            obj.Data = await _iUnitOfWork.Checklists.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeChecklistList(ChecklistListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ChecklistEntity> changeEmployerList = await _iUnitOfWork.Checklists.GetList(param);
            foreach (ChecklistEntity changeEmployer in changeEmployerList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = changeEmployer.Id,
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(ChecklistListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ChecklistEntity> changeEmployerList = await _iUnitOfWork.Checklists.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (ChecklistEntity changeEmployer in changeEmployerList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = changeEmployer.Id,
                });
                List<long> userIdList = userList.Where(t => t.Company == changeEmployer.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<ChecklistEntity>> GetEntity(long id)
        {
            TData<ChecklistEntity> obj = new TData<ChecklistEntity>();
            obj.Data = await _iUnitOfWork.Checklists.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        //public async Task<TData<int>> GetMaxSort()
        //{
        //    TData<int> obj = new TData<int>();
        //    obj.Data = await _iUnitOfWork.Checklists.GetMaxSort();
        //    obj.Tag = 1;
        //    return obj;
        //}
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(ChecklistEntity entity)
        {
            TData<string> obj = new TData<string>();
            var PdName = await _iUnitOfWork.CreditTypes.GetEntity(entity.ProductName);
            entity.ProductName = PdName.Name;
            await _iUnitOfWork.Checklists.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            obj.Message = "Checklist added successfully";
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.Checklists.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> UpdateForm(ChecklistEntity entity)
        {
            TData<string> obj = new TData<string>();

            // Check if the entity exists in the database
            EmployeeEntity existingEntity = await _iUnitOfWork.Employees.GetEntity(entity.Id);
            if (existingEntity == null)
            {
                obj.Message = "Entity not found";
                obj.Tag = -1;
                return obj;
            }

            // Update the existing entity with the new data
            //existingEntity.EmployerNo = entity.Name;
            //existingEntity.Employer = entity.Name;


            // Save the changes to the database
            await _iUnitOfWork.Employees.SaveForm(existingEntity);
            await _iUnitOfWork.Checklists.SaveForm(entity);

            obj.Data = existingEntity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }



        #endregion
    }
}
