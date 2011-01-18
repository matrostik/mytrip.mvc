using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.RssParser.Repository.DataEntities;
using System.Web;
using System.Net;
using System.IO;
using System.Xml.Linq;
using mtm.Core;
using mtm.Core.Settings;

namespace mtm.RssParser.Repository
{
   public class RssparserRepository
    {
        #region Подключение к Entity Репозиторию
        Entities _entities;
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

        #region Другие подключения и переменные
        private string username
        {
            get { return HttpContext.Current.User.Identity.Name; }
        }
        #endregion

        public IQueryable<mytrip_rssparser> GetAllRssparsers()
        {
            return entities.mytrip_rssparser.OrderByDescending(x => x.Title);
        }
        public IQueryable<mytrip_rssparser> GetAllRssparsers(string culture)
        {
            return entities.mytrip_rssparser.Where(x => x.AllCulture == true || x.Culture == culture)
                .OrderByDescending(x => x.Views);
        }
        public IQueryable<mytrip_rssparser> GetAllRssparsers(int pageIndex, int pageSize, string culture, out int total)
        {
            var a= entities.mytrip_rssparser.Where(x => x.AllCulture == true || x.Culture == culture)
                .OrderByDescending(x => x.Views);
            total = a.Count();
            return a.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        public mytrip_rssparser GetRssparser(int id)
        {
            return entities.mytrip_rssparser.FirstOrDefault(x => x.RssparserId == id);
        
        }
        private int CreateRssparserId()
        {
            int id;
            for (id = 1; entities.mytrip_rssparser.Count(x => x.RssparserId == id) != 0; id++) ;
            return id;
        }
        public mytrip_rssparser Create(string title, string culture,bool allculture,string rssurl,string imgurl)
        {
            int id = CreateRssparserId();
            mytrip_rssparser x = new mytrip_rssparser { 
            RssparserId=id,
            Title=title,
            Culture=culture,
            AllCulture=allculture,
            Path = GeneralMethods.DecodingString(title),
            CreateDate = DateTime.Now,
            UserName = username,
            RssUrl=rssurl,
            ImageUrl=imgurl,
            Views=0
            };
            entities.mytrip_rssparser.AddObject(x);
            entities.SaveChanges();
            return x;
        }
        public void Edit(int id,string title, bool allculture, string rssurl, string imgurl)
        {
            mytrip_rssparser x = GetRssparser(id);
            
                x.Title = title;
                x.AllCulture = allculture;
                x.Path = GeneralMethods.DecodingString(title);
                x.RssUrl = rssurl;
                x.ImageUrl = imgurl;
            entities.SaveChanges();
        }
        public void Delete(int id)
        {
            mytrip_rssparser x = GetRssparser(id);
            entities.DeleteObject(x);
            entities.SaveChanges();
        }
        public void ViewRssparser(int id)
        {
            mytrip_rssparser x = GetRssparser(id);
            x.Views++;
            entities.SaveChanges();
        }
        public IEnumerable<XElement> GetRssparsers(int pageIndex, int pageSize, int id, out bool img, out string link, out string title, out int total)
        {
            try
            {
                var b = entities.mytrip_rssparser.FirstOrDefault(x => x.RssparserId == id);
                string url = b.RssUrl;
                title = b.Title;
                string doc = GeneralMethods.HttpGetParsing(url);
                if (doc.IndexOf("<enclosure") != -1 && doc.IndexOf("type=\"image/") != -1)
                    img = true;
                else
                    img = false;
                XDocument _doc = XDocument.Parse(doc);
                link = _doc.Root.Elements("channel").Elements("link").FirstOrDefault().Value;
                var a = _doc.Root.Elements("channel").Elements("item");
                total = a.Count();
                return a.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            catch {
                img = false; link = string.Empty; title = string.Empty; total = 0;
                return null; }
        }
        public IEnumerable<XElement> GetRssparsers(int pageSize, int id,string culture, out bool img, out string link, out string title, out bool _culture,out string rsslink)
        {
            try
            {
                var b = entities.mytrip_rssparser.Where(x=>x.AllCulture==true||x.Culture==culture)
                    .FirstOrDefault(x => x.RssparserId == id);
                if (b == null)
                    _culture = false;
                else
                    _culture = true;
                string url = b.RssUrl;
                title = b.Title;
                rsslink = b.RssUrl;
                string doc = GeneralMethods.HttpGetParsing(url);
                if (doc.IndexOf("<enclosure") != -1 && doc.IndexOf("type=\"image/") != -1)
                    img = true;
                else
                    img = false;
                XDocument _doc = XDocument.Parse(doc);
                link = _doc.Root.Elements("channel").Elements("link").FirstOrDefault().Value;
                var a = _doc.Root.Elements("channel").Elements("item");
                return a.Skip(0).Take(pageSize);
            }
            catch
            {
                _culture = false;
                img = false; link = string.Empty; title = string.Empty; rsslink = string.Empty; 
                return null;
            }
        }
       
       
    }
}
