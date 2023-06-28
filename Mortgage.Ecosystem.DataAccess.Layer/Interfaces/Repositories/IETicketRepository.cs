﻿using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IETicketRepository
    {
        Task<List<ETicketEntity>> GetList(ETicketListParam param);
        Task<List<ETicketEntity>> GetPageList(ETicketListParam param, Pagination pagination);
        Task<ETicketEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(ETicketEntity entity);
        Task DeleteForm(string ids);
    }
}
