using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Resources;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class LoanScheduleService : ILoanScheduleService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly HttpClient _client;

        public LoanScheduleService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
            _client = new HttpClient
            {
                BaseAddress = new Uri(ApiResource.baseAddress)
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApiResource.ApplicationJson));


        }

        #region Retrieve data
        public async Task<TData<List<LoanScheduleEntity>>> GetList(LoanScheduleListParam param)
        {
            TData<List<LoanScheduleEntity>> obj = new TData<List<LoanScheduleEntity>>();
            obj.Data = await _iUnitOfWork.LoanSchedules.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<LoanScheduleEntity>>> GetPageList(LoanScheduleListParam param, Pagination pagination)
        {
            TData<List<LoanScheduleEntity>> obj = new TData<List<LoanScheduleEntity>>();
            obj.Data = await _iUnitOfWork.LoanSchedules.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeLoanScheduleList(LoanScheduleListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<LoanScheduleEntity> loanScheduleList = await _iUnitOfWork.LoanSchedules.GetList(param);
            foreach (LoanScheduleEntity loanSchedule in loanScheduleList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = loanSchedule.Id,
                    name = loanSchedule.Customer
                });
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ZtreeInfo>>> GetZtreeUserList(LoanScheduleListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<LoanScheduleEntity> loanScheduleList = await _iUnitOfWork.LoanSchedules.GetList(param);
            List<UserEntity> userList = await _iUnitOfWork.Users.GetList(null);
            foreach (LoanScheduleEntity loanSchedule in loanScheduleList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = loanSchedule.Id,
                    name = loanSchedule.Customer
                });
                List<long> userIdList = userList.Where(t => t.Company == loanSchedule.Id).Select(t => t.Employee).ToList();
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

        public async Task<TData<LoanScheduleEntity>> GetEntity(long id)
        {
            TData<LoanScheduleEntity> obj = new TData<LoanScheduleEntity>();
            obj.Data = await _iUnitOfWork.LoanSchedules.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.LoanSchedules.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        public async Task<TData<List<LoanSchedule>>> LoanSchedule(string applicationRefNo)
        {

            try
            {
                var response = await _client.GetAsync(ApiResource.loanSchedule + applicationRefNo);
                string response1 = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<LoanScheduleResponse>(response1);

                if (result.success == true && result.count > 0)
                {
                    TData<List<LoanSchedule>> obj = new TData<List<LoanSchedule>>();
                    obj.Data = result.result;
                    obj.Total = result.count;
                    obj.Tag = 1;
                    return obj;
                }
                else
                {
                    TData<List<LoanSchedule>> obj = new TData<List<LoanSchedule>>();
                    obj.Data = result.result;
                    obj.Total = result.count;
                    obj.Tag = 0;
                    return obj;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #region Submit data
        public async Task<TData<string>> SaveForm(LoanScheduleEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.LoanSchedules.SaveForm(entity);
            obj.Data = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData<long> obj = new TData<long>();
            await _iUnitOfWork.LoanSchedules.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        #endregion


    }
}
