using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using mtm.Core.Repository.DataEntities;
using mtm.Core.Settings;

namespace mtm.Core.Repository
{
    public class CorePageRepository
    {
        #region Entities

        Entities _entities;
        /// <summary>
        /// 
        /// </summary>
        public Entities entities
        {
            get
            {
                if (_entities == null)
                    _entities = new Entities(CoreSetting.connectionString());
                return _entities;
            }
        }
        #endregion

        #region cache
        internal IQueryable<mytrip_corepages> GetPagesForMenu(string culture)
        {
            if (HttpContext.Current.Cache["mtm_getpagesformenu" + culture] == null)
            {
                TimeSpan _spanSek = TimeSpan.FromSeconds(36000);
                DateTime _absolutSek = DateTime.MaxValue;
                var x = _GetPagesForMenu(culture);
                if (x == null)
                    return null;
                HttpContext.Current.Cache.Insert("mtm_getpagesformenu" + culture, x, null, _absolutSek, _spanSek, CacheItemPriority.High, null);
            }
            return HttpContext.Current.Cache["mtm_getpagesformenu" + culture] as IQueryable<mytrip_corepages>;
        }
        internal IQueryable<mytrip_corepages> GetPagesForHomePage()
        {
            if (HttpContext.Current.Cache["mtm_getpagesforhomepage"] == null)
            {
                TimeSpan _spanSek = TimeSpan.FromSeconds(36000);
                DateTime _absolutSek = DateTime.MaxValue;
                var x = _GetPagesForHomePage();
                if (x == null)
                    return null;
                HttpContext.Current.Cache.Insert("mtm_getpagesforhomepage", x, null, _absolutSek, _spanSek, CacheItemPriority.High, null);
            }
            return HttpContext.Current.Cache["mtm_getpagesforhomepage"] as IQueryable<mytrip_corepages>;
        }
        internal IQueryable<mytrip_corepages> GetPagesForSearch(string culture, string search)
        {
            if (HttpContext.Current.Cache["mtm_getpagesforsearch" + search + culture] == null)
            {
                TimeSpan _spanSek = TimeSpan.FromSeconds(1800);
                DateTime _absolutSek = DateTime.MaxValue;
                var x = _GetPagesForSearch(culture, search);
                if (x == null)
                    return null;
                HttpContext.Current.Cache.Insert("mtm_getpagesforsearch" + search + culture, x, null, _absolutSek, _spanSek, CacheItemPriority.High, null);
            }
            return HttpContext.Current.Cache["mtm_getpagesforsearch" + search + culture] as IQueryable<mytrip_corepages>;
        }
        internal mytrip_corepages GetPages(int id)
        {
            if (HttpContext.Current.Cache["mtm_getpages" + id] == null)
            {
                TimeSpan _spanSek = TimeSpan.FromSeconds(36000);
                DateTime _absolutSek = DateTime.MaxValue;
                var x = _GetPages(id);
                if (x == null)
                    return null;
                HttpContext.Current.Cache.Insert("mtm_getpages" + id, x, null, _absolutSek, _spanSek, CacheItemPriority.High, null);
            }
            return HttpContext.Current.Cache["mtm_getpages" + id] as mytrip_corepages;
        }
        internal mytrip_corepages GetPages(int id, string culture)
        {
            if (HttpContext.Current.Cache["mtm_getpages" + id + culture] == null)
            {
                TimeSpan _spanSek = TimeSpan.FromSeconds(36000);
                DateTime _absolutSek = DateTime.MaxValue;
                var x=_GetPages(id,culture);
                if(x==null)
                    return null;
                HttpContext.Current.Cache.Insert("mtm_getpages" + id + culture, x, null, _absolutSek, _spanSek, CacheItemPriority.High,null);
            }
            return HttpContext.Current.Cache["mtm_getpages" + id + culture] as mytrip_corepages;
        }
        #endregion

        #region no cache
        internal mytrip_corepages CreatePage(string title,string body,string culture,
            int subid,bool sideBar,bool emailForm,bool allCulture,bool addMenu,
            bool addHomePage, bool viewOnlyHomePage)
        {
            CreatePagesZero();
            mytrip_corepages x = new mytrip_corepages { 
            PageId=CreatePageId(),
            AddHomePage=addHomePage,
            AddMenu=addMenu,
            AllCulture=allCulture,
            Body=body,
            Culture=culture,
            EmailForm=emailForm,
            Path = GeneralMethods.DecodingString(title),
            SideBar=sideBar,
            SubPagesId=subid,
            Title=title,
            ViewOnlyHomePage = viewOnlyHomePage
            };
            entities.mytrip_corepages.AddObject(x);
            entities.SaveChanges();
            GeneralMethods.MytripCacheRemove("mtm_getpagesformenu",true,true);
            GeneralMethods.MytripCacheRemove("mtm_getpagesforhomepage");
            GeneralMethods.MytripCacheRemoveContains("mtm_getpagesforsearch");
            return x;
        }
        internal mytrip_corepages EditPage(string title, string body, 
            int id, bool sideBar, bool emailForm, bool allCulture, bool addMenu,
            bool addHomePage, bool viewOnlyHomePage)
        {
            CreatePagesZero();
            mytrip_corepages x = _GetPages(id);
            string culture = x.Culture;
            x.AddHomePage = addHomePage;
                x.AddMenu = addMenu;
                x.AllCulture = allCulture;
                x.Body = body;
                x.EmailForm = emailForm;
                x.Path = GeneralMethods.DecodingString(title);
                x.SideBar = sideBar;
                x.Title = title;
                x.ViewOnlyHomePage = viewOnlyHomePage;
             entities.SaveChanges();
             GeneralMethods.MytripCacheRemove("mtm_getpagesformenu", true,true);
             GeneralMethods.MytripCacheRemove("mtm_getpagesforhomepage");
             GeneralMethods.MytripCacheRemoveContains("mtm_getpagesforsearch");
             GeneralMethods.MytripCacheRemove("mtm_getpages"+id);
             GeneralMethods.MytripCacheRemove("mtm_getpages"+id, true);
            return x;
        }
        internal void DeletePage(int id)
        {
            mytrip_corepages x = _GetPages(id);
            if (x.SubPagesId == 0)
            {
                foreach (var z in x.mytrip_corepages1.ToList())
                {
                    entities.DeleteObject(z);
                }
            }            
            entities.DeleteObject(x);
            entities.SaveChanges();
            GeneralMethods.MytripCacheRemove("mtm_getpagesformenu", true, true);
            GeneralMethods.MytripCacheRemove("mtm_getpagesforhomepage");
            GeneralMethods.MytripCacheRemoveContains("mtm_getpagesforsearch");
            GeneralMethods.MytripCacheRemove("mtm_getpages"+id);
            GeneralMethods.MytripCacheRemove("mtm_getpages"+id, true);
        }
        #endregion

        #region private
        private mytrip_corepages _GetPages(int id)
        {
            return entities.mytrip_corepages
                .Include("mytrip_corepages1")
                .Include("mytrip_corepages2")
                .Include("mytrip_corepages2.mytrip_corepages1")
                .FirstOrDefault(x => x.PageId == id);
        }
        private mytrip_corepages _GetPages(int id, string culture)
        {
            return entities.mytrip_corepages
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .FirstOrDefault(x => x.PageId == id);
        } 
        private IQueryable<mytrip_corepages> _GetPagesForMenu(string culture)
        {
            return entities.mytrip_corepages
                .Include("mytrip_corepages1")
                .Where(x => x.AddMenu == true)
                .Where(x => x.SubPagesId == 0)
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .OrderBy(x => x.Title);
        }
        private IQueryable<mytrip_corepages> _GetPagesForHomePage()
        {
            return entities.mytrip_corepages
                .Where(x => x.PageId != 0)
                .Where(x => x.AddHomePage == true || x.ViewOnlyHomePage == true)
                .Where(x => x.SubPagesId == 0)
                .OrderBy(x => x.Title);
        }
        private IQueryable<mytrip_corepages> _GetPagesForSearch(string culture, string search)
        {
            return entities.mytrip_corepages
                  .Include("mytrip_corepages1")
                  .Where(x => x.ViewOnlyHomePage == false)
                  .Where(x => x.SubPagesId == 0)
                  .Where(x => x.Culture == culture || x.AllCulture == true)
                  .Where(x => x.Title.Contains(search) == true || x.Body.Contains(search) == true)
                  .OrderBy(x => x.Title);
        }
        private int CreatePageId()
        {
            int[] _x = entities.mytrip_corepages.Select(x => x.PageId).ToArray();
            int catId;
            for (catId = 1; _x.Count(x=>x==catId)!=0; catId++);
            return catId;
        }
        private void CreatePagesZero()
        {
            mytrip_corepages e = entities.mytrip_corepages.FirstOrDefault(x => x.PageId == 0);
            if (e == null)
            {
                e = new mytrip_corepages
                {
                    PageId = 0,
                    AddHomePage = false,
                    AllCulture = false,
                    AddMenu = false,
                    Culture = "zero",
                    EmailForm = false,
                    SideBar = false,
                    SubPagesId = 0,
                    ViewOnlyHomePage = false
                };
                entities.mytrip_corepages.AddObject(e);
                entities.SaveChanges();
            }
        }
        #endregion
    }
}