using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Tourism.Repository.DataEntities;
using mtm.Core.Settings;
using System.Web;
using mtm.Tourism.Models;

namespace mtm.Tourism.Repository
{
   public class CategoryRepository
    {
        #region Подключение к Entity Репозиторию
        Entities _entities;

        /// <summary>Подключение к Entity Репозиторию 
        /// </summary>
        public Entities entities
        {
            get
            {
                if (_entities == null)
                    _entities = new Entities(ModuleSetting.connectionString());
                return _entities;
            }
        }
        #endregion
        /// <summary>Все категории с учетом культуры
        /// отсортированные по алфавиту
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <returns>возвращает IQueryable &lt; mytrip_tourscategory &gt;</returns>
        public IQueryable<mytrip_tourscategory> GetAllCategory(string culture)
        {
            return entities.mytrip_tourscategory
                .Include("mytrip_tourscategory1")
                .Where(x => x.SubCategoryId == 0)
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .OrderBy(x => x.Title);
        }
        public IQueryable<mytrip_tourscountry> GetAllCountry(string culture)
        {
            return entities.mytrip_tourscountry
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .Where(x => x.CountryId != 0)
                .OrderBy(x => x.Title);
        }
        public IDictionary<int, string> GetAllCategoryDdl(string culture)
        {
            IDictionary<int, string> mcats = new Dictionary<int, string>();
            var a = GetAllCategory(culture);
            foreach (var x in a)
            {
                mcats.Add(x.CategoryId, x.Title);
                foreach (var y in x.mytrip_tourscategory1.ToList())
                {
                    mcats.Add(y.CategoryId, "--" + y.Title);
                }
            }
            return mcats;
        }
        public IDictionary<int, string> GetAllCountryDdl(string culture)
        {
            IDictionary<int, string> mcats = new Dictionary<int, string>();
            var a = GetAllCountry(culture);
            foreach (var x in a)
            {
                mcats.Add(x.CountryId, x.Title);
            }
            return mcats;
        }
        public IDictionary<int, string> GetAllCategoryDdlsearch(string culture)
        {
            IDictionary<int, string> mcats = new Dictionary<int, string>();
            var a = GetAllCategory(culture);
            mcats.Add(0, ToursLanguage.categorylist);
            foreach (var x in a)
            {
                mcats.Add(x.CategoryId, x.Title);
                foreach (var y in x.mytrip_tourscategory1.ToList())
                {
                    mcats.Add(y.CategoryId, "--" + y.Title);
                }
            }
            return mcats;
        }
        public IDictionary<int, string> GetAllCountryDdlsearch(string culture)
        {
            IDictionary<int, string> mcats = new Dictionary<int, string>();
            var a = GetAllCountry(culture);
            mcats.Add(0, ToursLanguage.countrylist);
            foreach (var x in a)
            {
                mcats.Add(x.CountryId, x.Title);
            }
            return mcats;
        }
        public IQueryable<mytrip_tourscategory> GetSubCategory(int id,string culture)
        {
            return entities.mytrip_tourscategory
                .Include("mytrip_tourscategory1")
                .Where(x => x.SubCategoryId == id)
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .OrderBy(x => x.Title);
        }
        public mytrip_tourscategory GetCategory(int id)
        {
            return entities.mytrip_tourscategory
                .Include("mytrip_tourscategory2")
                .FirstOrDefault(x => x.CategoryId == id);
        }
        public mytrip_tourscountry GetCountry(int id)
        {
            return entities.mytrip_tourscountry.FirstOrDefault(x => x.CountryId == id);
        }
        #region _Создать уникальный CategoryId
        private int CreateCategoryId()
        {
            int catId;
            for (catId = 1; entities.mytrip_tourscategory.Count(x => x.CategoryId == catId) != 0; catId++) ;
            return catId;
        }
        private int CreateCountryId()
        {
            int catId;
            for (catId = 1; entities.mytrip_tourscountry.Count(x => x.CountryId == catId) != 0; catId++) ;
            return catId;
        }
        #endregion

        #region Создать рубрику ZERO
        /// <summary>
        /// Создать рубрику ZERO
        /// </summary>
        public void CreateСategoryZero()
        {
            mytrip_tourscategory zero = entities.mytrip_tourscategory.FirstOrDefault(x => x.CategoryId == 0);
            if (zero == null)
            {
                mytrip_tourscategory x = new mytrip_tourscategory
                {
                    CategoryId = 0,
                    Title = "zero",
                    Path = "zero",
                    Body="zero",
                    CreateDate = DateTime.Now,
                    UserName = "mytripmvc",
                    SubCategoryId = 0,
                    AllCulture = false,
                    Culture = "zero"
                };
                entities.mytrip_tourscategory.AddObject(x);
                entities.SaveChanges();
            }
        }
        public void CreateCountryZero()
        {
            mytrip_tourscountry zero = entities.mytrip_tourscountry.FirstOrDefault(x => x.CountryId == 0);
            if (zero == null)
            {
                mytrip_tourscountry x = new mytrip_tourscountry
                {
                    CountryId = 0,
                    Title = "zero",
                    Path = "zero",
                    Body = "zero",
                    CreateDate = DateTime.Now,
                    UserName = "mytripmvc",
                    AllCulture = false,
                    Culture = "zero"
                };
                entities.mytrip_tourscountry.AddObject(x);
                entities.SaveChanges();
            }
        }
        #endregion
        public mytrip_tourscategory CreateCategory(EditorCategoryModel m)
        {
            CreateСategoryZero();
            mytrip_tourscategory x = new mytrip_tourscategory { 
            CategoryId=CreateCategoryId(),
            Title=m.title,
            AllCulture=m.allculture,
            Body=m.body,
            CreateDate=DateTime.Now,
            Culture=LocalisationSetting.culture(),
            Path = (m.path!=null&&m.path.Length>0)?m.path:GeneralMethods.DecodingString(m.title),
            SubCategoryId=m.id,
            UserName = HttpContext.Current.User.Identity.Name,
            SeoTitle=(m.seoTitle!=null&&m.seoTitle.Length>0)?m.seoTitle:m.title,
            SeoKeyword=m.seokeywords,
            SeoDescription=m.seodescription
            };
            entities.mytrip_tourscategory.AddObject(x);
            entities.SaveChanges();
            return x;
        }
        public mytrip_tourscountry CreateCountry(EditorCategoryModel m)
        {
            mytrip_tourscountry x = new mytrip_tourscountry
            {
                CountryId = CreateCountryId(),
                Title = m.title,
                AllCulture = m.allculture,
                Body = m.body,
                CreateDate = DateTime.Now,
                Culture = LocalisationSetting.culture(),
                Path = (m.path != null && m.path.Length > 0) ? m.path : GeneralMethods.DecodingString(m.title),
                UserName = HttpContext.Current.User.Identity.Name,
                SeoTitle = (m.seoTitle != null && m.seoTitle.Length > 0) ? m.seoTitle : m.title,
                SeoKeyword = m.seokeywords,
                SeoDescription = m.seodescription
            };
            entities.mytrip_tourscountry.AddObject(x);
            entities.SaveChanges();
            return x;
        }
        public mytrip_tourscategory EditCategory(EditorCategoryModel m)
        {
            var x = GetCategory(m.id);
            x.Title = m.title;
            x.AllCulture = m.allculture;
            x.Body = m.body;
            x.Path = (m.path != null && m.path.Length > 0) ? m.path : GeneralMethods.DecodingString(m.title);
            x.SeoTitle = (m.seoTitle != null && m.seoTitle.Length > 0) ? m.seoTitle : m.title;
            x.SeoDescription = m.seodescription;
            x.SeoKeyword = m.seokeywords;
            entities.SaveChanges();
            return x;
        }
        public mytrip_tourscountry EditCountry(EditorCategoryModel m)
        {
            var x = GetCountry(m.id);
            x.Title = m.title;
            x.AllCulture = m.allculture;
            x.Body = m.body;
            x.Path = (m.path != null && m.path.Length > 0) ? m.path : GeneralMethods.DecodingString(m.title);
            x.SeoTitle = (m.seoTitle != null && m.seoTitle.Length > 0) ? m.seoTitle : m.title;
            x.SeoDescription = m.seodescription;
            x.SeoKeyword = m.seokeywords;
            entities.SaveChanges();
            return x;
        }
        public void DeleteCategory(int id)
        {
            var x = entities.mytrip_tourscategory
                .Include("mytrip_tourscategory1.mytrip_tours")
                .Include("mytrip_tours")
                .FirstOrDefault(z => z.CategoryId == id);
            foreach (var z in x.mytrip_tourscategory1.ToList())
            {
                foreach (var y in z.mytrip_tours.ToList())
                {
                    foreach (var a in y.mytrip_toursvariants.ToList())
                    {
                        entities.mytrip_toursvariants.DeleteObject(a);
                    }
                    entities.mytrip_tours.DeleteObject(y);
                }
                entities.mytrip_tourscategory.DeleteObject(z);
            }
            foreach (var z in x.mytrip_tours.ToList())
            {
                foreach (var a in z.mytrip_toursvariants.ToList())
                    {
                        entities.mytrip_toursvariants.DeleteObject(a);
                    }
                entities.mytrip_tours.DeleteObject(z);
            }
            entities.mytrip_tourscategory.DeleteObject(x);
            entities.SaveChanges();
        }
        public void DeleteCountry(int id)
        {
            var x = entities.mytrip_tourscountry
                .Include("mytrip_tours")
                .FirstOrDefault(z => z.CountryId == id);
            foreach (var z in x.mytrip_tours.ToList())
            {
                foreach (var a in z.mytrip_toursvariants.ToList())
                {
                    entities.mytrip_toursvariants.DeleteObject(a);
                }
                entities.mytrip_tours.DeleteObject(z);
            }
            entities.mytrip_tourscountry.DeleteObject(x);
            entities.SaveChanges();
        }
    }
}
