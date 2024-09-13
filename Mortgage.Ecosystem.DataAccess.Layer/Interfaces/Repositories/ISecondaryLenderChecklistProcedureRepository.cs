﻿using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface ISecondaryLenderChecklistProcedureRepository
    { 
        Task SaveForm(SecondaryLenderChecklistProcedureEntity entity);
        Task DeleteForm(string ids);
        Task SaveForms(List<SecondaryLenderChecklistProcedureEntity> entity);
        Task<SecondaryLenderChecklistProcedureEntity> GetEntity(string Nhf);
    }
}
