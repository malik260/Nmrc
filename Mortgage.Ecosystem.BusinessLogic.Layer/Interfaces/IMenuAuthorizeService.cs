using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IMenuAuthorizeService
    {
        Task<TData<List<MenuAuthorizeInfo>>> GetAuthorizeList(OperatorInfo user);
    }
}