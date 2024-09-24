using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface ILenderCategoryService
    {
        Task<TData<List<LenderCategoryEntity>>> GetList(LenderCategoryEntity param);
    }
}
