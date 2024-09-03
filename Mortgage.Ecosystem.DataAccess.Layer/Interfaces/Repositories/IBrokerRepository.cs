using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IBrokerRepository
    {
        Task<List<BrokerEntity>> GetApprovalPageList(BrokerListParam param, Pagination pagination);
        Task<List<BrokerEntity>> GetPageList(BrokerListParam param, Pagination pagination);
        Task ApproveForm(BrokerEntity entity, MenuEntity menu, OperatorInfo user);
        Task<BrokerEntity> GetEntity(long id);
    }
}
