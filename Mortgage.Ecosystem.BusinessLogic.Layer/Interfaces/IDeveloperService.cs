using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IDeveloperService
    {
        Task<TData<List<DeveloperEntity>>> GetApprovalPageList(DeveloperListParam param, Pagination pagination);
        Task<TData<List<DeveloperEntity>>> GetList(DeveloperListParam param);
        Task<TData<List<DeveloperEntity>>> GetPageList(DeveloperListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreePmbList(DeveloperListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(DeveloperListParam param);
        Task<TData<DeveloperEntity>> GetEntity(long id);
        Task<TData<string>> SaveForms(DeveloperEntity entity);
        Task<TData> DeleteForm(string ids);
        Task<TData> ApproveForm(DeveloperEntity entity);



    }
}
