using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Gismeteo.Repository.DataEntities;
using System.Web;
using System.Net;
using System.IO;
using System.Xml.Linq;

namespace Mytrip.Gismeteo.Repository
{
    public class GismeteoRepository
    {
        #region Подключение к Entity Репозиторию
        GismeteoSettings _core;
        public GismeteoSettings core
        {
            get
            {
                if (_core == null)
                    _core = new GismeteoSettings();
                return _core;
            }
        }
        Entities _entities;
        public Entities entities
        {
            get
            {
                if (_entities == null)
                    _entities = new Entities(core.connectionString());
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
        public IQueryable<mytrip_gismeteo> GetGismeteoForAccardion(string culture)
        {
            return entities.mytrip_gismeteo.Where(x => x.VisibleInformer == true)
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .OrderBy(x => x.Title);
        }
        public IQueryable<mytrip_gismeteo> GetGismeteoForManager()
        {
            return entities.mytrip_gismeteo.OrderBy(x => x.Title);
        }
        public mytrip_gismeteo GetOnlyGismeteo(int id)
        {
            return entities.mytrip_gismeteo.FirstOrDefault(x => x.GismeteoId == id);
        }
        private int CreateGismeteoId()
        {
            int catId;
            for (catId = 1; GetOnlyGismeteo(catId) != null; catId++) ;
            return catId;
        }
        public mytrip_gismeteo CreateGismeteo(string title, string culture, bool allculture, string urlxml, bool visible)
        {
            int id = CreateGismeteoId();
            mytrip_gismeteo x = new mytrip_gismeteo
            {
                GismeteoId = id,
                Title = title,
                UrlXml=urlxml,
                Culture = culture,
                AllCulture = allculture,
                CreateDate = DateTime.Now,
                UserName = username,
                VisibleInformer=visible
            };
            entities.mytrip_gismeteo.AddObject(x);
            entities.SaveChanges();
            return x;
        }
        public void EditGismeteo(int id, string title,bool allculture, string urlxml, bool visible)
        {
            mytrip_gismeteo x = GetOnlyGismeteo(id);
            x.Title = title;
            x.AllCulture = allculture; 
            x.VisibleInformer = visible;
            x.UrlXml = urlxml;
            entities.SaveChanges();
        }
        public void DeleteGismeteo(int id)
        {
            mytrip_gismeteo x = GetOnlyGismeteo(id);
            entities.DeleteObject(x);
            entities.SaveChanges();
        }
        public IEnumerable<XElement> GetOnlyGismeteoXml(string urlxml)
        {
            try
            {
                XDocument _doc = XDocument.Parse(XmlParsing(urlxml));
                return _doc.Root.Elements("REPORT").Elements("TOWN").Elements("FORECAST");
            }
            catch
            {
                return null;
            } 
        }
        private string XmlParsing(string url)
        {
            try
            {
                string retVal = string.Empty;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Accept = "*/*";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0)";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(response.CharacterSet)))
                {
                    retVal = streamReader.ReadToEnd();
                }
                response.Close();
                return retVal;
            }
            catch { return string.Empty; }
        }
    }
}
