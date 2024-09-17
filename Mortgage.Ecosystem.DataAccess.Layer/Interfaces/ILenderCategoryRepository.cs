using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces
{
    public interface ILenderCategoryRepository
    {
        Task<List<LenderCategoryEntity>> GetList(LenderCategoryEntity param);
    }
}
