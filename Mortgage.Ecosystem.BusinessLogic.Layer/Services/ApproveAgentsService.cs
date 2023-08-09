using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Resources;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class ApproveAgentsService : IApproveAgentsService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        

        public ApproveAgentsService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<ApproveAgentsEntity>>> GetList(ApproveAgentsListParam param)
        {
            TData<List<ApproveAgentsEntity>> obj = new TData<List<ApproveAgentsEntity>>();
            obj.Data = await _iUnitOfWork.ApproveAgents.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ApproveAgentsEntity>>> GetPageList(ApproveAgentsListParam param, Pagination pagination)
        {
            TData<List<ApproveAgentsEntity>> obj = new TData<List<ApproveAgentsEntity>>();
            obj.Data = await _iUnitOfWork.ApproveAgents.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeApproveAgentsList(ApproveAgentsListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ApproveAgentsEntity> approveAgentsList = await _iUnitOfWork.ApproveAgents.GetList(param);
            foreach (ApproveAgentsEntity approveAgents in approveAgentsList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = approveAgents.Id,
                    name = approveAgents.Company
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(ApproveAgentsListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ApproveAgentsEntity> approveAgentsList = await _iUnitOfWork.ApproveAgents.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (ApproveAgentsEntity approveAgents in approveAgentsList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = approveAgents.Id,
                    name = approveAgents.Company
                });
                List<long> userIdList = userList.Where(t => t.Company == approveAgents.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<ApproveAgentsEntity>> GetEntity(long id)
        {
            TData<ApproveAgentsEntity> obj = new TData<ApproveAgentsEntity>();
            obj.Data = await _iUnitOfWork.ApproveAgents.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.ApproveAgents.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }

        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(ApproveAgentsEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.ApproveAgents.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.ApproveAgents.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
