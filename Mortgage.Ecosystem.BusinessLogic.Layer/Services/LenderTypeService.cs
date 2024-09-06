using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class LenderTypeService : ILenderTypeService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public LenderTypeService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

   
        public async Task<TData<List<LenderTypeEntity>>> GetList(LenderTypeListParam param)
        {
            TData<List<LenderTypeEntity>> obj = new TData<List<LenderTypeEntity>>();
            obj.Data = await _iUnitOfWork.LenderTypes.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }


    }
}