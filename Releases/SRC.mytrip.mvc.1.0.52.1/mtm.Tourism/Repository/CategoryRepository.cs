using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Tourism.Repository.DataEntities;
using mtm.Core.Settings;
using System.Web;

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
        #region _Создать уникальный CategoryId
        private int CreateCategoryId()
        {
            int catId;
            for (catId = 1; entities.mytrip_tourscategory.Count(x => x.CategoryId == catId) != 0; catId++) ;
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
        #endregion
        public mytrip_tourscategory CreateCategory(string title,bool allculture,string body,string culture,
            int subcategory)
        {
            CreateСategoryZero();
            mytrip_tourscategory x = new mytrip_tourscategory { 
            CategoryId=CreateCategoryId(),
            Title=title,
            AllCulture=allculture,
            Body=body,
            CreateDate=DateTime.Now,
            Culture=culture,
            Path = GeneralMethods.DecodingString(title),
            SubCategoryId=subcategory,
            UserName = HttpContext.Current.User.Identity.Name
            };
            entities.mytrip_tourscategory.AddObject(x);
            entities.SaveChanges();
            return x;
        }
        public mytrip_tourscategory EditCategory(int id,string title, bool allculture, string body)
        {
            var x = GetCategory(id);
            x.Title = title;
            x.AllCulture = allculture;
            x.Body = body;
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
    }
}
