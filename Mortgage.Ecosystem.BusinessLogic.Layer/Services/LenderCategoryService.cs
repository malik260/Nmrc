using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class LenderCategoryService : ILenderCategoryService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public LenderCategoryService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;

        }

        public async Task<TData<List<LenderCategoryEntity>>> GetList(LenderCategoryEntity param)
        {
            TData<List<LenderCategoryEntity>> obj = new TData<List<LenderCategoryEntity>>();
            obj.Data = await _iUnitOfWork.LenderCategory.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

    }
}
