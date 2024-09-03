using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IBrokerService
    {
        Task<TData<List<BrokerEntity>>> GetApprovalPageList(BrokerListParam param, Pagination pagination);
        Task<TData<List<BrokerEntity>>> GetPageList(BrokerListParam param, Pagination pagination);
        Task<TData> ApproveForm(BrokerEntity entity);
    }
}
