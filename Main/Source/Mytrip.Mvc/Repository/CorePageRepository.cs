using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mytrip.Mvc.Repository.DataEntities;
using Mytrip.Mvc.Settings;

namespace Mytrip.Mvc.Repository
{
    internal class CorePageRepository
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
        internal mytrip_corepages GetPages(int id)
        {
            return entities.mytrip_corepages
                .Include("mytrip_corepages1")
                .Include("mytrip_corepages2")
                .Include("mytrip_corepages2.mytrip_corepages1")
                .FirstOrDefault(x => x.PageId == id);
        }
        internal IEnumerable<mytrip_corepages> GetPagesForMenu(string culture)
        {
            return entities.mytrip_corepages
                .Include("mytrip_corepages1")
                .Where(x => x.AddMenu == true)
                .Where(x => x.SubPagesId == 0)
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .OrderBy(x=>x.Title);
        }
        internal mytrip_corepages GetPagesForHome(int id)
        {string culture=LocalisationSetting.culture();
            return entities.mytrip_corepages
                .Where(x=>x.AddHomePage==true||x.ViewOnlyHomePage==true)
                .Where(x=>x.Culture==culture||x.AllCulture==true)
                .OrderByDescending(x=>x.PageId)
                .Skip(id)
                .FirstOrDefault();
        }
        private int CreatePageId()
        {
            int catId;
            for (catId = 1; entities.mytrip_corepages.Count(x => x.PageId == catId) != 0; catId++);
            return catId;
        }
        private void CreatePagesZero()
        {
            mytrip_corepages e = entities.mytrip_corepages.FirstOrDefault(x => x.PageId == 0);
            if (e == null)
            {
                e = new mytrip_corepages { 
                PageId=0,
                AddHomePage=false,
                AllCulture=false,
                AddMenu=false,
                Culture="zero",
                EmailForm=false,
                SideBar=false,
                SubPagesId=0,
                ViewOnlyHomePage=false
                };
                entities.mytrip_corepages.AddObject(e);
                entities.SaveChanges();
            }
        }
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
            return x;
        }
        internal mytrip_corepages EditPage(string title, string body, 
            int id, bool sideBar, bool emailForm, bool allCulture, bool addMenu,
            bool addHomePage, bool viewOnlyHomePage)
        {
            CreatePagesZero();
            mytrip_corepages x = GetPages(id);
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
            return x;
        }
        internal void DeletePage(int id)
        {
            mytrip_corepages x = GetPages(id);
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
    }
}