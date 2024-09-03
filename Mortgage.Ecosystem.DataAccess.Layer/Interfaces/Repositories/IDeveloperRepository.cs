using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IDeveloperRepository
    {
        Task<List<DeveloperEntity>> GetApprovalPageList(DeveloperListParam param, Pagination pagination);
        Task<List<DeveloperEntity>> GetList(DeveloperListParam param);
        Task<List<DeveloperEntity>> GetPageList(DeveloperListParam param, Pagination pagination);
        Task<DeveloperEntity> GetEntity(long id);
        bool ExistDeveloper(DeveloperEntity entity);
        bool ExistRCNumber(DeveloperEntity entity);
        Task SaveForm(DeveloperEntity entity);
        Task SaveForms(DeveloperEntity entity);
        Task DeleteForm(string ids);
        Task ApproveForm(DeveloperEntity entity, MenuEntity menu, OperatorInfo user);

    }
}
