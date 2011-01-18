using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Weather.Repository.DataEntities;
using System.Web;
using System.Net;
using System.IO;
using System.Xml.Linq;

namespace mtm.Weather.Repository
{
    public class WeatherRepository
    {
        #region Подключение к Entity Репозиторию
        Entities _entities;
        public Entities entities
        {
            get
            {
                if (_entities == null)
                    _entities = new Entities(ModuleSettings.connectionString());
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

        public IQueryable<mytrip_weather> GetWeatherForAccardion(string culture)
        {
            return entities.mytrip_weather.Where(x => x.VisibleInformer == true)
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .OrderBy(x => x.Title);
        }
        public IQueryable<mytrip_weather> GetWeatherForManager()
        {
            return entities.mytrip_weather.OrderBy(x => x.Title);
        }
        public mytrip_weather GetOnlyWeather(int id)
        {
            return entities.mytrip_weather.FirstOrDefault(x => x.weatherId == id);
        }
        private int CreateWeatherId()
        {
            int catId;
            for (catId = 1; GetOnlyWeather(catId) != null; catId++) ;
            return catId;
        }
        public mytrip_weather CreateWeather(string title, string culture, bool allculture, string urlxml, bool visible)
        {
            int id = CreateWeatherId();
            mytrip_weather x = new mytrip_weather
            {
                weatherId = id,
                Title = title,
                UrlXml=urlxml,
                Culture = culture,
                AllCulture = allculture,
                CreateDate = DateTime.Now,
                UserName = username,
                VisibleInformer=visible
            };
            entities.mytrip_weather.AddObject(x);
            entities.SaveChanges();
            return x;
        }
        public void EditWeather(int id, string title,bool allculture, string urlxml, bool visible)
        {
            mytrip_weather x = GetOnlyWeather(id);
            x.Title = title;
            x.AllCulture = allculture; 
            x.VisibleInformer = visible;
            x.UrlXml = urlxml;
            entities.SaveChanges();
        }
        public void DeleteWeather(int id)
        {
            mytrip_weather x = GetOnlyWeather(id);
            entities.DeleteObject(x);
            entities.SaveChanges();
        }
        public IEnumerable<XElement> GetOnlyWeatherXml(string urlxml)
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
