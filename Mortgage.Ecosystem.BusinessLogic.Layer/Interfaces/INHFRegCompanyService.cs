﻿using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface INHFRegCompanyService
    {
        Task<TData<List<NHFRegCompanyEntity>>> GetList(NHFRegCompanyListParam param);
        Task<TData<List<NHFRegCompanyEntity>>> GetPageList(NHFRegCompanyListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeNHFRegCompanyList(NHFRegCompanyListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(NHFRegCompanyListParam param);
        Task<TData<NHFRegCompanyEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(NHFRegCompanyEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
