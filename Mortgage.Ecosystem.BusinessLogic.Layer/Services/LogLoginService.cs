using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class LogLoginService : ILogLoginService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public LogLoginService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Get data
        public async Task<TData<List<LogLoginEntity>>> GetList(LogLoginListParam param)
        {
            TData<List<LogLoginEntity>> obj = new TData<List<LogLoginEntity>>();
            obj.Data = await _iUnitOfWork.LogLogins.GetList(param);
            obj.Tag = 1;
            return obj;
        }

        public class Des
        {
            public string userName { get; set; }
            public string password { get; set; }
            public string captchaCode { get; set; }
        }
        public async Task<TData<List<LogLoginEntity>>> GetPageList(LogLoginListParam param, Pagination pagination)
        {
            var context = new ApplicationDbContext();
            TData<List<LogLoginEntity>> obj = new TData<List<LogLoginEntity>>();
            obj.Data = await _iUnitOfWork.LogLogins.GetUsersPageList(param, pagination);
            var Admins = new UserListParam();
            var AdminUsers = _iUnitOfWork.Users.GetList(Admins).Result.Select(i => i.Company).ToList();
            var query = from t1 in obj.Data
                        join t2 in context.UserEntity on t1.BaseCreatorId equals t2.Employee
                        where t2.IsSystem == 0 && t2.UserStatus == 1
                        select t1;
            obj.Data = query.ToList().OrderByDescending(i => i.Id).ToList(); 

            foreach (LogLoginEntity entity in obj.Data)
            {
                var user = await _iUnitOfWork.Employees.GetEntity(entity.BaseCreatorId);
                var companyInfo = await _iUnitOfWork.Companies.GetById(entity.Company);
                entity.UserName = user.FirstName + " " + user.LastName;
                entity.CompanyName = companyInfo.Name;
                entity.EmailAddress = user.EmailAddress;


            }
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<LogLoginEntity>>> GetAdminPageList(LogLoginListParam param, Pagination pagination)
        {
            try
            {
                var context = new ApplicationDbContext();
                var userinfo = await Operator.Instance.Current();
                TData<List<LogLoginEntity>> obj = new TData<List<LogLoginEntity>>();
                param.employee = userinfo.Employee;
                obj.Data = await _iUnitOfWork.LogLogins.GetPageList(param, pagination);
                obj.Data = obj.Data.Where(i => i.Company != 0).ToList();
                var Admins = new UserListParam();
                var AdminUsers = _iUnitOfWork.Users.GetList(Admins).Result.Select(i => i.Company).ToList();
                //var query = from t1 in obj.Data
                //            join t2 in context.UserEntity on t1.BaseCreatorId equals t2.Employee
                //            where t2.IsSystem == 1 && t2.UserStatus == 1
                //            select t1;
                //obj.Data = query.ToList().OrderByDescending(i => i.Id).ToList();
                foreach (LogLoginEntity entity in obj.Data)
                {
                    var user = await _iUnitOfWork.Employees.GetEntity(entity.BaseCreatorId);
                    var companyInfo = await _iUnitOfWork.Companies.GetById(entity.Company);
                    entity.UserName = user?.FirstName + " " + user?.LastName;
                    entity.CompanyName = companyInfo?.Name;
                    entity.EmailAddress = user?.EmailAddress;
                }
                obj.Total = pagination.TotalCount;
                obj.Tag = 1;
                return obj;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<TData<LogLoginEntity>> GetEntity(long id)
        {
            TData<LogLoginEntity> obj = new TData<LogLoginEntity>();
            obj.Data = await _iUnitOfWork.LogLogins.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(LogLoginEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.LogLogins.SaveForm(entity);
            obj.Data = entity.Id.ToStr();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await _iUnitOfWork.LogLogins.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> RemoveAllForm()
        {
            TData obj = new TData();
            await _iUnitOfWork.LogLogins.RemoveAllForm();
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}