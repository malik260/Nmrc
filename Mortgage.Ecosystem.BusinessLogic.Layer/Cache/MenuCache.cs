using Mortgage.Ecosystem.DataAccess.Layer.Caching;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using System.Collections.Generic;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Cache
{
    public class MenuCache : BaseBusinessCache<MenuEntity>
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public MenuCache(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        public override string CacheKey => this.GetType().Name;

        public override async Task<List<MenuEntity>> GetList()
        {
            try
            {
                var cacheList = CacheFactory.Cache.GetCache<List<MenuEntity>>(CacheKey);
                if (cacheList == null)
                {

                    var list = await _iUnitOfWork.Menus.GetList(null);
                    CacheFactory.Cache.SetCache(CacheKey, list);
                    return list;


                }
                else
                {
                    var authMenuList = new ApplicationDbContext();
                    var lists = authMenuList.MenuEntity.Where(i => i.MenuName != null).ToList();
                    //var lists = await _iUnitOfWork.Menus.GetList(null);
                    if (lists.Count > cacheList.Count)
                    {
                        CacheFactory.Cache.SetCache(CacheKey, lists);
                        return lists;

                    }
                    else
                    {
                        return cacheList;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //   if (cacheList == null)
        //    {
        //        var list = await _iUnitOfWork.Menus.GetList(null);
        //        CacheFactory.Cache.SetCache(CacheKey, list);
        //        return list;
        //    }
        //    else
        //    {
        //        return cacheList;

        //    }
        //}
    }
}
