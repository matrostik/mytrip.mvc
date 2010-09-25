using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Store.Repository.DataEntities;
using System.Web;
using Mytrip.Mvc.Settings;

namespace Mytrip.Store.Repository
{
    /// <summary>Репозиторий производителей
    /// </summary>
    public class ProducerRepository
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

        /// <summary>Все производители с учетом текущей культуры
        /// отсортированные по алфавиту 
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <returns>возвращает IQueryable &lt; mytrip_storeproducer &gt;</returns>
        public IQueryable<mytrip_storeproducer> GetAllProducer(string culture)
        {
            return entities.mytrip_storeproducer
                .Include("mytrip_storeproduct")
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .OrderBy(x => x.Title);
        }

        /// <summary>Все производители постранично с учетом текущей культуры
        /// отсортированные по алфавиту
        /// </summary>
        /// <param name="pageIndex">номер страницы</param>
        /// <param name="pageSize">количество на странице</param>
        /// <param name="culture">текущая культура</param>
        /// <param name="total">(out int) общее количество производителей</param>
        /// <returns>возвращает IQueryable &lt; mytrip_storeproducer &gt;</returns>
        public IQueryable<mytrip_storeproducer> GetAllProducer(int pageIndex, int pageSize, string culture, out int total)
        {
            var a = entities.mytrip_storeproducer
                .Include("mytrip_storeproduct")
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .OrderBy(x => x.Title);
            total = a.Count();
            return a.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        /// <summary>производитель по индентификатору 
        /// </summary>
        /// <param name="id">индентификатор производителя</param>
        /// <returns>возвращает mytrip_storeproducer</returns>
        public mytrip_storeproducer GetProducer(int id)
        {
            return entities.mytrip_storeproducer
                .Include("mytrip_storeproduct")
                .FirstOrDefault(x => x.ProducerId == id);
        }

        /// <summary>Все производители для дропдауна
        /// отсортированные по алфавиту
        /// </summary>
        /// <param name="culture">текущая культура</param>
        /// <returns>возвращает IDictionary &lt; int,string &gt;</returns>
        public IDictionary<int, string> GetProducerForDdl(string culture,bool search)
        {
            IDictionary<int, string> mcats = new Dictionary<int, string>();
            var a = entities.mytrip_storeproducer
           .Where(x => x.Culture == culture || x.AllCulture == true)
           .OrderBy(x => x.Title);
            if(search)
            mcats.Add(0, StoreLanguage.AllProducer);
            foreach (mytrip_storeproducer cat in a)
            {
                mcats.Add(cat.ProducerId, cat.Title);
            }
            return mcats;
        }

        /// <summary>Создание производителя
        /// </summary>
        /// <param name="title">название производителя</param>
        /// <param name="body">описание производителя (возможен null)</param>
        /// <param name="image">изображение производителя (возможен null)</param>
        /// <param name="allculture">показывать для всех культур (true для всех)</param>
        /// <param name="culture">текущая культура</param>
        /// <returns>возвращает mytrip_storeproducer</returns>
        public mytrip_storeproducer CreateProducer(string title, string body, string image, bool allculture, string culture)
        {
            mytrip_storeproducer x = new mytrip_storeproducer
            {
                ProducerId=CreateProducerId(),
                Title = title,
                Path = GeneralMethods.DecodingString(title),
                CreationDate = DateTime.Now,
                UserName = HttpContext.Current.User.Identity.Name,
                Body = body,
                Image = image,
                AllCulture = allculture,
                Culture = culture
            };
            entities.mytrip_storeproducer.AddObject(x);
            entities.SaveChanges();
            return x;
        }

        /// <summary>Редактирование производителя
        /// </summary>
        /// <param name="id">индентификатор производителя</param>
        /// <param name="title">название производителя</param>
        /// <param name="body">описание производителя (возможен null)</param>
        /// <param name="image">изображение производителя (возможен null)</param>
        /// <param name="allculture">показывать для всех культур (true для всех)</param>
        /// <returns>возвращает mytrip_storeproducer</returns>
        public mytrip_storeproducer EditProducer(int id, string title, string body, string image, bool allculture)
        {
            mytrip_storeproducer x = GetProducer(id);
            x.Title = title;
            x.Image = image;
            x.Body = body;
            x.AllCulture = allculture;
            entities.SaveChanges();
            return x;
        }


        /// <summary>Создать уникальный ProducerId
        /// </summary>
        /// <returns>возвращает int</returns>
        private int CreateProducerId()
        {
            int catId;
            for (catId = 1; entities.mytrip_storeproducer.Count(x => x.ProducerId == catId) != 0; catId++) ;
            return catId;
        }
    }
}
