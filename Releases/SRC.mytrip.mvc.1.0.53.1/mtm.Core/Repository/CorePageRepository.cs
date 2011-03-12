using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using mtm.Core.Repository.DataEntities;
using mtm.Core.Settings;
using mtm.Core.Models;

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
            var x = _GetPagesForMenu(culture);
                return x;
         }
        internal IQueryable<mytrip_corepages> GetPagesForHomePage()
        {
                var x = _GetPagesForHomePage();
            return x;
          }
        internal IQueryable<mytrip_corepages> GetPagesForSearch(string culture, string search)
        {
                var x = _GetPagesForSearch(culture, search);
                return x;
          }
        internal mytrip_corepages GetPages(int id)
        {
                var x = _GetPages(id);
                return x;
        }
        internal mytrip_corepages GetPages(int id, string culture)
        {
                var x=_GetPages(id,culture);
                return x;
        }
        #endregion

        #region no cache
        internal mytrip_corepages CreatePage(EditorPageModel m)
        {
            CreatePagesZero();
            mytrip_corepages x = new mytrip_corepages { 
            PageId=CreatePageId(),
            AddHomePage=m.addHomePage,
            AddMenu=m.addMenu,
            AllCulture=m.allCulture,
            Body=m.body,
            Culture=LocalisationSetting.culture(),
            EmailForm=m.emailForm,
            Path = (m.path!=null&&m.path.Length>0)?m.path:GeneralMethods.DecodingString(m.title),
            SideBar=m.sideBar,
            SubPagesId=m.id,
            Title=m.title,
            ViewOnlyHomePage = m.viewOnlyHomePage,
            SeoKeyword=m.seokeywords,
            SeoDescription=m.seodescription,
            SeoTitle = (m.seotitle != null && m.seotitle.Length > 0) ? m.seotitle : m.title
            };
            entities.mytrip_corepages.AddObject(x);
            entities.SaveChanges();
            return x;
        }
        internal mytrip_corepages EditPage(EditorPageModel m)
        {
            CreatePagesZero();
            mytrip_corepages x = _GetPages(m.id);
            string culture = x.Culture;
            x.AddHomePage = m.addHomePage;
                x.AddMenu = m.addMenu;
                x.AllCulture = m.allCulture;
                x.Body = m.body;
                x.EmailForm = m.emailForm;
                x.Path = (m.path != null && m.path.Length > 0) ? m.path : GeneralMethods.DecodingString(m.title);
                x.SideBar = m.sideBar;
                x.Title = m.title;
                x.ViewOnlyHomePage = m.viewOnlyHomePage;
                x.SeoDescription = m.seodescription;
                x.SeoKeyword = m.seokeywords;
                x.SeoTitle = (m.seotitle != null && m.seotitle.Length > 0) ? m.seotitle : m.title;
             entities.SaveChanges();
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