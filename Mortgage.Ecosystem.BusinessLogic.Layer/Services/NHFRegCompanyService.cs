using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class NHFRegCompanyService : INHFRegCompanyService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public NHFRegCompanyService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Retrieve data
        public async Task<TData<List<NHFRegCompanyEntity>>> GetList(NHFRegCompanyListParam param)
        {
            TData<List<NHFRegCompanyEntity>> obj = new TData<List<NHFRegCompanyEntity>>();
            obj.Data = await _iUnitOfWork.NHFRegCompanies.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<NHFRegCompanyEntity>>> GetPageList(NHFRegCompanyListParam param, Pagination pagination)
        {
            TData<List<NHFRegCompanyEntity>> obj = new TData<List<NHFRegCompanyEntity>>();
            obj.Data = await _iUnitOfWork.NHFRegCompanies.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeNHFRegCompanyList(NHFRegCompanyListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<NHFRegCompanyEntity> nhfRegCompanyList = await _iUnitOfWork.NHFRegCompanies.GetList(param);
            foreach (NHFRegCompanyEntity nhfRegCompany in nhfRegCompanyList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = nhfRegCompany.Id,
                    name = nhfRegCompany.Name
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(NHFRegCompanyListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<NHFRegCompanyEntity> nhfRegCompanyList = await _iUnitOfWork.NHFRegCompanies.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (NHFRegCompanyEntity nhfRegCompany in nhfRegCompanyList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = nhfRegCompany.Id,
                    name = nhfRegCompany.Name
                });
                List<long> userIdList = userList.Where(t => t.Company == nhfRegCompany.Id).Select(t => t.Employee).ToList();
                foreach (UserEntity user in userList.Where(t => userIdList.Contains(t.Employee)))
                {
                    obj.Data.Add(new ZtreeInfo
                    {
                        id = user.Id,
                        name = user.RealName
                    });
                }
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<NHFRegCompanyEntity>> GetEntity(long id)
        {
            TData<NHFRegCompanyEntity> obj = new TData<NHFRegCompanyEntity>();
            obj.Data = await _iUnitOfWork.NHFRegCompanies.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.NHFRegCompanies.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(NHFRegCompanyEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.NHFRegCompanies.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.NHFRegCompanies.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
