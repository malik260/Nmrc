using Mortgage.Ecosystem.BusinessLogic.Layer.Cache;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public MenuService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        #region Get data

        public async Task<TData<List<MenuEntity>>> GetList(MenuListParam param)
        {
            var obj = new TData<List<MenuEntity>>();

            List<MenuEntity> list = await new MenuCache(_iUnitOfWork).GetList();
           
            list = ListFilter(param, list);

            obj.Data = list;
            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<List<ZtreeInfo>>> GetZtreeList1(MenuListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();

            List<MenuEntity> list = new MenuCache(_iUnitOfWork).GetList().Result.Where(x => x.Category != 0 && x.MenuStatus == 1).ToList();
            var LoanApproval = list.Where(i => i.Id == 660261644680564736).FirstOrDefault();
            var LoanReview = list.Where(i => i.Id == 660881219264712704).FirstOrDefault();
            var LoanBatching = list.Where(i => i.Id == 664553002530508800).FirstOrDefault();
            var LoanUnderwriting = list.Where(i => i.Id == 563327185478225920).FirstOrDefault();
            list.Remove(LoanBatching);
            list.Remove(LoanReview);
            list.Remove(LoanApproval);
            list = ListFilter(param, list);

            foreach (MenuEntity menu in list)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = menu.Id,
                    pId = menu.Parent,
                    name = menu.MenuName
                });
            }

            obj.Tag = 1;
            return obj;
        }




        public async Task<TData<List<ZtreeInfo>>> GetZtreeList(MenuListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();

            List<MenuEntity> list = new MenuCache(_iUnitOfWork).GetList().Result.Where(i=> i.ApprovalLevel > 0).ToList();
            var LoanApproval = list.Where(i => i.Id == 660261644680564736).FirstOrDefault();
            var LoanReview = list.Where(i => i.Id == 660881219264712704).FirstOrDefault();
            var LoanBatching = list.Where(i => i.Id == 664553002530508800).FirstOrDefault();
            var LoanUnderwriting = list.Where(i => i.Id == 563327185478225920).FirstOrDefault();
            list.Remove(LoanBatching);
            list.Remove(LoanReview);
            list.Remove(LoanApproval);
            list = ListFilter(param, list);

            foreach (MenuEntity menu in list)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = menu.Id,
                    pId = menu.Parent,
                    name = menu.MenuName
                });
            }

            obj.Tag = 1;
            return obj;
        }


        public async Task<TData<List<ZtreeInfo>>> GetZtreeList2(MenuListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();

            List<MenuEntity> list = await new MenuCache(_iUnitOfWork).GetList();
            list = ListFilter(param, list);

            foreach (MenuEntity menu in list)
            {
                if (menu.MenuUrl != null && menu.MenuUrl.Contains("Underwriting"))
                {
                    obj.Data.Add(new ZtreeInfo
                    {
                        id = menu.Id,
                        pId = menu.Parent,
                        name = menu.MenuName
                    });

                }
                
            }

            obj.Tag = 1;
            return obj;
        }



        public async Task<TData<MenuEntity>> GetEntity(long id)
        {
            TData<MenuEntity> obj = new TData<MenuEntity>();
            obj.Data = await _iUnitOfWork.Menus.GetEntity(id);
            if (obj.Data != null)
            {
                long parent = obj.Data.Parent;
                if (parent > 0)
                {
                    MenuEntity parentMenu = await _iUnitOfWork.Menus.GetEntity(parent);
                    if (parentMenu != null)
                    {
                        obj.Data.ParentName = parentMenu.MenuName;
                    }
                }
                else
                {
                    obj.Data.ParentName = "Home Directory";
                }
            }
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort(long parent)
        {
            TData<int> obj = new TData<int>();
            obj.Data = await _iUnitOfWork.Menus.GetMaxSort(parent);
            obj.Tag = 1;
            return obj;
        }

        #endregion Get data

        #region Submit data

        public async Task<TData<string>> SaveForm(MenuEntity entity)
        {
            TData<string> obj = new TData<string>();
            if (!entity.Id.IsNullOrZero() && entity.Id == entity.Parent)
            {
                obj.Message = "Cannot select myself as parent menu!";
                return obj;
            }
            if (_iUnitOfWork.Menus.ExistMenuName(entity))
            {
                obj.Message = "Menu name already exists!";
                return obj;
            }
            await _iUnitOfWork.Menus.SaveForm(entity);

            new MenuCache(_iUnitOfWork).Remove();

            obj.Data = entity.Id.ToStr();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await _iUnitOfWork.Menus.DeleteForm(ids);

            new MenuCache(_iUnitOfWork).Remove();
            obj.Tag = 1;
            return obj;
        }

        #endregion Submit data

        #region private method

        private List<MenuEntity> ListFilter(MenuListParam param, List<MenuEntity> list)
        {
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.MenuName))
                {
                    list = list.Where(p => p.MenuName.Contains(param.MenuName)).ToList();
                }
                if (param.MenuStatus > 0)
                {
                    list = list.Where(p => p.MenuStatus == param.MenuStatus).ToList();
                }
            }
            return list;
        }

        #endregion private method
    }
}