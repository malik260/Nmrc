using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Extensions;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Operator;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using System.Linq.Expressions;

namespace Mortgage.Ecosystem.DataAccess.Layer.Repositories
{
    public class MenuRepository : DataRepository, IMenuRepository
    {
        #region Get data
        public async Task<List<MenuEntity>> GetList(MenuListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList<MenuEntity>(expression);
            return list.OrderBy(p => p.MenuSort).ToList();
        }

        public async Task<List<MenuEntity>> GetEmployerMenuList()
        {
            var expression = ListEmployerMenuFilter();
            var list = await BaseRepository().FindList<MenuEntity>(expression);
            return list.OrderBy(p => p.MenuSort).ToList();
        }

        public async Task<List<MenuEntity>> GetPmbMenuList()
        {
            var expression = ListPmbMenuFilter();
            var list = await BaseRepository().FindList<MenuEntity>(expression);
            return list.OrderBy(p => p.MenuSort).ToList();
        }

        public async Task<List<MenuEntity>> GetSecondaryLenderMenuList()
        {
            var expression = ListSecondaryLenderMenuFilter();
            var list = await BaseRepository().FindList<MenuEntity>(expression);
            return list.OrderBy(p => p.MenuSort).ToList();
        }

        public async Task<List<MenuEntity>> GetEmployeeMenuList()
        {
            var expression = ListEmployeeMenuFilter();
            var list = await BaseRepository().FindList<MenuEntity>(expression);
            return list.OrderBy(p => p.MenuSort).ToList();
        }



        public async Task<MenuEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<MenuEntity>(id);
        }

        public async Task<MenuEntity> GetEntitybyUrl(string url)
        {
            return await BaseRepository().FindEntity<MenuEntity>(x=> x.MenuUrl == url);
        }

        public async Task<int> GetMaxSort(long parent)
        {
            string where = string.Empty;
            if (parent > 0)
            {
                where += " AND Parent = " + parent;
            }
            object result = await BaseRepository().FindObject("SELECT MAX(MenuSort) FROM tbl_Menu where BaseIsDelete = 0 " + where);
            int sort = result.ToInt();
            sort++;
            return sort;
        }

        public bool ExistMenuName(MenuEntity entity)
        {
            var expression = ExtensionLinq.True<MenuEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.MenuName == entity.MenuName && t.MenuType == entity.MenuType);
            }
            else
            {
                expression = expression.And(t => t.MenuName == entity.MenuName && t.MenuType == entity.MenuType && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region Submit data
        public async Task SaveForm(MenuEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            var db = await BaseRepository().BeginTrans();
            try
            {
                long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
                await db.Delete<MenuEntity>(p => idArr.Contains(p.Id) || idArr.Contains(p.Parent));
                await db.Delete<MenuAuthorizeEntity>(p => idArr.Contains(p.MenuId));
                await db.CommitTrans();
            }
            catch
            {
                await db.RollbackTrans();
                throw;
            }
        }
        #endregion

        #region private method
        private Expression<Func<MenuEntity, bool>> ListFilter(MenuListParam param)
        {
            var expression = ExtensionLinq.True<MenuEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.MenuName))
                {
                    expression = expression.And(t => t.MenuName.Contains(param.MenuName));
                }
                if (param.MenuStatus > 0)
                {
                    expression = expression.And(t => t.MenuStatus == param.MenuStatus);
                }
            }
            return expression;
        }

        private Expression<Func<MenuEntity, bool>> ListEmployerMenuFilter()
        {
            var menu = new GlobalConstant();
            var expression = ExtensionLinq.True<MenuEntity>();
            expression = expression.And(t => t.Category == GlobalConstant.EMPLOYER_MENU_CATEGORY ||  t.Category == GlobalConstant.GLOBAL_MENU_CATEGORY || t.Category == GlobalConstant.EMPLOYERANDEMPLOYEE_MENU_CATEGORY || t.Category == GlobalConstant.ISAGENT_MENU_CATEGORY || t.Category == GlobalConstant.EMPLOYERANDPMB_MENU_CATEGORY);

            return expression;
        }

        private Expression<Func<MenuEntity, bool>> ListPmbMenuFilter()
        {
            var menu = new GlobalConstant();
            var expression = ExtensionLinq.True<MenuEntity>();
            expression = expression.And(t => t.Category == GlobalConstant.PMB_MENU_CATEGORY || t.Category == GlobalConstant.GLOBAL_MENU_CATEGORY || t.Category == GlobalConstant.PMBANDEMPLOYEE_MENU_CATEGORY || t.Category == GlobalConstant.ISAGENT_MENU_CATEGORY || t.Category == GlobalConstant.EMPLOYERANDPMB_MENU_CATEGORY);

            return expression;
        }

        private Expression<Func<MenuEntity, bool>> ListEmployeeMenuFilter()
        {
            var menu = new GlobalConstant();
            var expression = ExtensionLinq.True<MenuEntity>();
            expression = expression.And(t => t.Category == GlobalConstant.EMPLOYEE_MENU_CATEGORY || t.Category == GlobalConstant.GLOBAL_MENU_CATEGORY || t.Category == GlobalConstant.PMBANDEMPLOYEE_MENU_CATEGORY || t.Category == GlobalConstant.EMPLOYERANDEMPLOYEE_MENU_CATEGORY);

            return expression;
        }

        private Expression<Func<MenuEntity, bool>> ListSecondaryLenderMenuFilter()
        {
            var menu = new GlobalConstant();
            var expression = ExtensionLinq.True<MenuEntity>();
            expression = expression.And(t => t.Category == GlobalConstant.SECONDARYLENDER_MENU_CATEGORY || t.Category == GlobalConstant.GLOBAL_MENU_CATEGORY || t.Category == GlobalConstant.PMBANDEMPLOYEE_MENU_CATEGORY || t.Category == GlobalConstant.ISAGENT_MENU_CATEGORY || t.Category == GlobalConstant.EMPLOYERANDPMB_MENU_CATEGORY);

            return expression;
        }



        #endregion
    }
}