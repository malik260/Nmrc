using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Enums;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class LogOperateSevice : ILogOperateService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public LogOperateSevice(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region get data
        public async Task<TData<List<LogOperateEntity>>> GetList(LogOperateListParam param)
        {
            TData<List<LogOperateEntity>> obj = new TData<List<LogOperateEntity>>();
            obj.Data = await _iUnitOfWork.LogOperates.GetList(param);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<LogOperateEntity>>> GetPageList(LogOperateListParam param, Pagination pagination)
        {
            TData<List<LogOperateEntity>> obj = new TData<List<LogOperateEntity>>();
            obj.Data = await _iUnitOfWork.LogOperates.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<LogOperateEntity>> GetEntity(int id)
        {
            TData<LogOperateEntity> obj = new TData<LogOperateEntity>();
            obj.Data = await _iUnitOfWork.LogOperates.GetEntity(id);
            if (obj.Data != null)
            {
                UserEntity userEntity = await _iUnitOfWork.Users.GetEntity(obj.Data.Company, obj.Data.BaseCreatorId);
                if (userEntity != null)
                {
                    obj.Data.UserName = userEntity.UserName;
                    DepartmentEntity departmentEntitty = await _iUnitOfWork.Departments.GetEntity(userEntity.Dept);
                    if (departmentEntitty != null)
                    {
                        obj.Data.DeptName = departmentEntitty.Name;
                    }
                }
            }
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region Submit data
        public async Task<TData<string>> SaveForm(LogOperateEntity entity)
        {
            TData<string> obj = new TData<string>();
            await _iUnitOfWork.LogOperates.SaveForm(entity);
            obj.Data = entity.Id.ToStr();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> SaveForm(string remark)
        {
            TData<string> obj = new TData<string>();
            LogOperateEntity entity = new LogOperateEntity();
            await _iUnitOfWork.LogOperates.SaveForm(entity);
            entity.LogStatus = OperateStatusEnum.Success.ToInt();
            entity.ExecuteUrl = remark;
            obj.Data = entity.Id.ToStr();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await _iUnitOfWork.LogOperates.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> RemoveAllForm()
        {
            TData obj = new TData();
            await _iUnitOfWork.LogOperates.RemoveAllForm();
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}