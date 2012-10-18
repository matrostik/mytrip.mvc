using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using mtm.Core.Settings;
using System.Net;
using System.IO.Compression;
using System.Xml.Linq;
using System.Threading;
using System.Text;
using mtm.Core.Repository.Geo;
using mtm.Core.Repository.DataEntities;
using mtm.Core.Settings;

namespace mtm.Core.Repository
{
    public class GeoRepository
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
        public int CreateCityProfile(decimal latitude, decimal longitude)
        {
            int cityId = 0;
            RenameLocations();
                string name = null;
                string region = "null";
                string country = null;
                string countrycode = "null";
                try
                {
                    string url = "http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20geo.places%20where%20woeid%20in%20(select%20place.woeid%20from%20flickr.places%20where%20lat%3D" +
                        latitude.ToString().Replace(",", ".") +
                        "%20and%20%20lon%3D" +
                        longitude.ToString().Replace(",", ".") +
                        ")&format=xml&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";
                    string doc = GeneralMethods.HttpGetParsing(url);
                    doc = doc.Replace("xmlns:", "")
                        .Replace("yahoo:", "")
                        .Replace("xml:", "")
                        .Replace("xmlns", "ab");
                    if (doc != null && doc.Contains(""))
                    {
                        XDocument _doc = XDocument.Parse(doc);
                        var _result = _doc.Root.Elements("results").FirstOrDefault();//.Element("place");
                        var result = _result.Elements("place").FirstOrDefault();
                        name = result.Element("name").Value;
                        country = result.Elements("country").FirstOrDefault(q => q.Attribute("type").Value == "Country").Value;
                        try
                        {
                            countrycode = result.Elements("country").FirstOrDefault(q => q.Attribute("type").Value == "Country").Attribute("code").Value;
                        }
                        catch { }
                        try
                        {
                            region = result.Elements("admin1").FirstOrDefault(q => q.Attribute("type").Value == "Province").Value;
                        }
                        catch { }
                        try
                        {
                            var latt = result.Elements("centroid").FirstOrDefault();
                            if (LocalisationSetting.culture().ToLower() == "ru-ru")
                            {
                                decimal.TryParse(latt.Element("latitude").Value.Replace(".", ","), out latitude);
                                decimal.TryParse(latt.Element("longitude").Value.Replace(".", ","), out longitude);
                            }
                            if (LocalisationSetting.culture().ToLower() == "en-us")
                            {
                                decimal.TryParse(latt.Element("latitude").Value.Replace(",", "."), out latitude);
                                decimal.TryParse(latt.Element("longitude").Value.Replace(",", "."), out longitude);
                            }
                        }
                        catch { }
                    }
                }
                catch { }
                if (name != null && country != null) {
                    if(region=="null")
                        region=name;
                    var x = entities.mytrip_geocity
                        .Include("mytrip_georegion")
                        .Include("mytrip_georegion.mytrip_geocountry")
                        .Where(z => z.Name == name)
                        .Where(z => z.mytrip_georegion.Name == region)
                        .FirstOrDefault(z => z.mytrip_georegion.mytrip_geocountry.Name == country);
                    if (x != null)
                        cityId = x.CityId;
                    else {
                        var xx = entities.mytrip_georegion
                            .Include("mytrip_geocountry")
                            .Where(z => z.Name == region)
                            .FirstOrDefault(z => z.mytrip_geocountry.Name == country);
                        if (xx != null)
                        cityId = CreateCity(xx.RegionId, false, true, latitude, longitude, name);
                        else {
                            var xxx = entities.mytrip_geocountry
                                .FirstOrDefault(z => z.Name == country);
                            if (xxx != null)
                            {
                                int regionid = CreateRegion(xxx.CountryId, false, true, latitude, longitude, region, "N/A");
                                cityId = CreateCity(regionid, false, true, latitude, longitude, name);
                            }
                            else {
                                int countryid = CreateCountry(false, true, latitude, longitude, country, countrycode);
                                int regionid = CreateRegion(countryid, false, true, latitude, longitude, region, "N/A");
                                cityId = CreateCity(regionid, false, true, latitude, longitude, name);
                            }

                        }
                    }
                }
                return cityId;
        }
        public void RenameLocations()
        {
            var x = entities.mytrip_geocity
                .Where(z => (z.Longitude+z.Latitude) != 0)
                .Where(z => z.EditAdmin == false&&z.EditUser==false);
            List<City> listcity = new List<City>();
            #region create city list
            foreach (var _x in x)
            {
                string name = null;
                string region = "null";
                string country = null;
                string countrycode = "null";
                try
                {
                    string url = "http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20geo.places%20where%20woeid%20in%20(select%20place.woeid%20from%20flickr.places%20where%20lat%3D" +
                        _x.Latitude.ToString().Replace(",", ".") +
                        "%20and%20%20lon%3D" +
                        _x.Longitude.ToString().Replace(",", ".") +
                        ")&format=xml&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";
                    string doc = GeneralMethods.HttpGetParsing(url);
                    doc = doc.Replace("xmlns:", "")
                        .Replace("yahoo:", "")
                        .Replace("xml:", "")
                        .Replace("xmlns", "ab");
                    if (doc != null && doc.Contains(""))
                    {
                        XDocument _doc = XDocument.Parse(doc);
                        var _result = _doc.Root.Elements("results").FirstOrDefault();//.Element("place");
                        var result = _result.Elements("place").FirstOrDefault();
                        name = result.Element("name").Value;
                        country = result.Elements("country").FirstOrDefault(q => q.Attribute("type").Value == "Country").Value;
                        try
                        {
                            countrycode = result.Elements("country").FirstOrDefault(q => q.Attribute("type").Value == "Country").Attribute("code").Value;
                        }
                        catch { }
                        try
                        {
                            region = result.Elements("admin1").FirstOrDefault(q => q.Attribute("type").Value == "Province").Value;
                        }
                        catch { }
                    }
                }
                catch { }
                if (name != null && country != null)
                {
                    City a = new City(_x.CityId, name, region, country, countrycode, (double)_x.Latitude, (double)_x.Longitude);
                    listcity.Add(a);
                }
            }
            #endregion
            foreach (var q in listcity)
            {
                var a = entities.mytrip_geocity
                    .Include("mytrip_georegion")
                    .Include("mytrip_georegion.mytrip_geocountry")
                    .FirstOrDefault(z => z.CityId == q._cityid);
                a.Name = q._name;
                a.EditAdmin = true;
                if (a.mytrip_georegion.EditAdmin == false && q._region != "null")
                {
                    a.mytrip_georegion.Name = q._region;
                    a.mytrip_georegion.EditAdmin = true;
                }
                if (a.mytrip_georegion.mytrip_geocountry.EditAdmin == false)
                {
                    a.mytrip_georegion.mytrip_geocountry.Name = q._country;
                    a.mytrip_georegion.mytrip_geocountry.EditAdmin = true;
                }
            }
            entities.SaveChanges();
        }


        public IQueryable<mytrip_geocity> GetAllCity()
        {
            return entities.mytrip_geocity
                .Where(x => (x.Longitude+x.Latitude) != 0)
                .OrderBy(x => x.CityId);
        }
        private int CountryId()
        {
            int[] _x = entities.mytrip_geocountry.Select(x => x.CountryId).ToArray();
            int catId;
            for (catId = 1; _x.Count(x => x == catId) != 0; catId++) ;
            return catId;
        }
        private int RegionId()
        {
            int[] _x = entities.mytrip_georegion.Select(x => x.RegionId).ToArray();
            int catId;
            for (catId = 1; _x.Count(x => x == catId) != 0; catId++) ;
            return catId;
        }
        private int CityId()
        {
            int[] _x = entities.mytrip_geocity.Select(x => x.CityId).ToArray();
            int catId;
            for (catId = 1; _x.Count(x => x == catId) != 0; catId++) ;
            return catId;
        }
        private void CreateCountryZero()
        {
            var a = entities.mytrip_geocountry.FirstOrDefault(x => x.CountryId == 0);
            if (a == null)
            {
                mytrip_geocountry x = new mytrip_geocountry
                {
                    CountryId = 0,
                    Code = "--",
                    EditAdmin = true,
                    EditUser = false,
                    Latitude = 0,
                    Longitude = 0,
                    Name = "N/A"
                };
                entities.mytrip_geocountry.AddObject(x);
            }
        }
        private void CreateRegionZero()
        {
            CreateCountryZero();
            var a = entities.mytrip_georegion.FirstOrDefault(x => x.RegionId == 0);
            if (a == null)
            {
                mytrip_georegion x = new mytrip_georegion
                {
                    RegionId = 0,
                    CountryId = 0,
                    Code = "--",
                    EditAdmin = true,
                    EditUser = false,
                    Latitude = 0,
                    Longitude = 0,
                    Name = "N/A"
                };
                entities.mytrip_georegion.AddObject(x);
            }
        }
        public void CreateCityZero()
        {
            if (!StatisticSetting.statisticDefaultCity()||CoreSetting.Development())
            {
                if (CoreSetting.Development()) {
                    HttpCookie cookie = new HttpCookie("mtCity", "0");
                    cookie.Expires = DateTime.Now.AddYears(1);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }
                CreateRegionZero();
                var a = entities.mytrip_geocity.FirstOrDefault(x => x.CityId == 0);
                if (a == null)
                {
                    mytrip_geocity x = new mytrip_geocity
                    {
                        CityId = 0,
                        RegionId = 0,
                        EditAdmin = true,
                        EditUser = false,
                        Latitude = 0,
                        Longitude = 0,
                        Name = "N/A"
                    };
                    entities.mytrip_geocity.AddObject(x);
                    entities.SaveChanges();
                }
                string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
                XDocument _doc = XDocument.Load(_absolutDirectory);
                var connection = _doc.Root.Elements("statistic").Elements("add");
                connection.FirstOrDefault(x => x.Attribute("name").Value == "defaultCity")
                    .SetAttributeValue("value", true.ToString());
                _doc.Save(_absolutDirectory);
                GeneralMethods.MytripCacheRemove("mtm_cs_defaultcity");
            }
        }
        public int CreateCity(int regionid, bool editadmin, bool edituser,
            decimal latitude, decimal longitude, string name)
        {
            int id = CityId();
            mytrip_geocity x = new mytrip_geocity
            {
                CityId = id,
                RegionId = regionid,
                EditAdmin = editadmin,
                EditUser = edituser,
                Latitude = latitude,
                Longitude = longitude,
                Name = name
            };
            entities.mytrip_geocity.AddObject(x);
            entities.SaveChanges();
            return id;
        }
        public int CreateRegion(int countryid, bool editadmin, bool edituser,
            decimal latitude, decimal longitude, string name, string code)
        {
            int id = RegionId();
            mytrip_georegion x = new mytrip_georegion
                {
                    RegionId = id,
                    CountryId = countryid,
                    Code = code,
                    EditAdmin = editadmin,
                    EditUser = edituser,
                    Latitude = latitude,
                    Longitude = longitude,
                    Name = name
                };
            entities.mytrip_georegion.AddObject(x);
            entities.SaveChanges();
            return id;
        }
        public int CreateCountry(bool editadmin, bool edituser,
            decimal latitude, decimal longitude, string name, string code)
        {
            int id = CountryId();
            mytrip_geocountry x = new mytrip_geocountry
            {
                CountryId = id,
                Code = code,
                EditAdmin = editadmin,
                EditUser = edituser,
                Latitude = latitude,
                Longitude = longitude,
                Name = name
            };
            entities.mytrip_geocountry.AddObject(x);
            entities.SaveChanges();
            return id;
        }
        public int CityId(string ip)
        {
            if (HttpContext.Current.Request.Cookies["mtCity"] != null)
            {
                string messs = HttpContext.Current.Request.Cookies["mtCity"].Value;

                try
                {
                    return int.Parse(messs);
                }
                catch { }
            }
            int mess = 0;
            string _base = GeoBaseName();
            try{
            if (_base != "null")
            {
                string dir = GeneralMethods.GetPath("App_Data", _base);
                LookupService ls = new LookupService(dir, LookupService.GEOIP_STANDARD);
                Location l = ls.getLocation(ip);
                if (l != null)
                {
                    if ((l.longitude +l.latitude) != 0)
                    {
                        int? a = entities.mytrip_geocity
                            .Where(x => x.Latitude == (decimal)l.latitude
                                &&x.Longitude == (decimal)l.longitude)
                                .Select(x=>x.CityId).FirstOrDefault();
                        if (a != null)
                        {
                            mess = (int)a;
                        }
                        else
                        {
                            string country = (l.countryCode == null) ? "--" : l.countryCode;
                            string region = (l.region == null) ? "--" : l.region;
                            var aa = entities.mytrip_georegion
                                .Include("mytrip_geocountry")
                                .Include("mytrip_geocity")
                                .Where(x => x.Code == region)
                                .FirstOrDefault(x => x.mytrip_geocountry.Code == country);
                            if (aa != null)
                            #region region no null
                            {
                                mess = CreateCity(aa.RegionId, false, false, (decimal)l.latitude, (decimal)l.longitude, l.city);
                                if (!aa.EditAdmin && !aa.EditUser)
                                {
                                    int count = aa.mytrip_geocity.Count(x => (x.Longitude+x.Latitude) != 0);
                                    decimal? lat = 0;
                                    decimal? longi = 0;
                                    if (count == 1)
                                    {
                                        lat = (decimal)l.latitude;
                                        longi = (decimal)l.longitude;
                                    }
                                    else
                                    {
                                        foreach (var s in aa.mytrip_geocity.Where(x =>( x.Longitude+x.Latitude) != 0).ToList())
                                        {
                                            lat += s.Latitude;
                                            longi += s.Longitude;
                                        }
                                        lat = lat / count;
                                        longi = longi / count;
                                    }
                                    bool create = false;
                                    if (aa.Latitude != lat)
                                    { aa.Latitude = lat; create = true; }
                                    if (aa.Longitude != longi)
                                    { aa.Longitude = longi; create = true; }
                                    if (create)
                                        entities.SaveChanges();
                                }
                            }
                            #endregion
                            else
                            #region region null
                            {
                                var aaa = entities.mytrip_geocountry
                                   .Include("mytrip_georegion")
                                   .FirstOrDefault(x => x.Code == country);
                                if (aaa != null)
                                #region country no null
                                {
                                    int regid = CreateRegion(aaa.CountryId, false, false, (decimal)l.latitude, (decimal)l.longitude, l.regionName, l.region);
                                    mess = CreateCity(regid, false, false, (decimal)l.latitude, (decimal)l.longitude, l.city);
                                    if (!aaa.EditAdmin && !aaa.EditUser)
                                    {
                                        int count = aaa.mytrip_georegion.Count(x => (x.Longitude+x.Latitude) != 0);
                                        decimal? lat = 0;
                                        decimal? longi = 0;
                                        if (count == 1)
                                        {
                                            lat = (decimal)l.latitude;
                                            longi = (decimal)l.longitude;
                                        }
                                        else
                                        {
                                            foreach (var s in aaa.mytrip_georegion.Where(x => (x.Longitude+x.Latitude) != 0).ToList())
                                            {
                                                lat += s.Latitude;
                                                longi += s.Longitude;
                                            }
                                            lat = lat / count;
                                            longi = longi / count;
                                        }
                                        bool create = false;
                                        if (aaa.Latitude != lat)
                                        { aaa.Latitude = lat; create = true; }
                                        if (aaa.Longitude != longi)
                                        { aaa.Longitude = longi; create = true; }
                                        if (create)
                                            entities.SaveChanges();
                                    }
                                }
                                #endregion
                                else
                                #region country null
                                {
                                    int countid = CreateCountry(false, false, (decimal)l.latitude, (decimal)l.longitude, l.countryName, l.countryCode);
                                    int regid = CreateRegion(countid, false, false, (decimal)l.latitude, (decimal)l.longitude, l.regionName, l.region);
                                    mess = CreateCity(regid, false, false, (decimal)l.latitude, (decimal)l.longitude, l.city);
                                }
                                #endregion
                            }
                            #endregion
                        }
                    }
                    else
                        CreateCityZero();
                }
                else
                    CreateCityZero();
            }
            else
                CreateCityZero();
        }catch{}
            HttpCookie cookie = new HttpCookie("mtCity", mess.ToString());
            cookie.Expires = DateTime.Now.AddYears(1);
            HttpContext.Current.Response.Cookies.Add(cookie);
            return mess;
        }
        public static string GeoBaseName()
        {

            if (GeoSetting.unlockGeo())
            {
                string _absolutDirectory = GeneralMethods.GetPath("App_Data");
                DirectoryInfo __absolutDirectory = new DirectoryInfo(_absolutDirectory);
                if (!__absolutDirectory.Exists)
                    __absolutDirectory.Create();
                FileInfo[] result = __absolutDirectory.GetFiles();
                bool download = true;
                bool namebool = false;
                int day = DateTime.Now.Day;
                string __name = (day > 1) ? "Geo_" + DateTime.Now.ToString("MM_yyyy") + ".dat" :
                    "Geo_" + DateTime.Now.AddMonths(-1).ToString("MM_yyyy") + ".dat";
                IDictionary<string, DateTime> aa = new Dictionary<string, DateTime>();
                foreach (FileInfo x in result)
                {
                    if (x.Extension == ".dat" && x.Name == __name)
                    {
                        aa.Add(x.Name, x.CreationTime);
                        download = false;
                        namebool = true;
                    }
                    else if (x.Extension == ".dat")
                    {
                        aa.Add(x.Name, x.CreationTime);
                        namebool = true;
                    }
                }
                if (!GeoSetting.DownloadGeo() && download)
                {
                    string dir = GeneralMethods.GetHostingPath();
                    DowloadBaseThread ethread = new DowloadBaseThread(dir);
                    Thread t = new Thread(new ThreadStart(ethread._DownloadBase));
                    t.IsBackground = true;
                    t.Start();
                }
                DirectoryInfo[] _result = __absolutDirectory.GetDirectories();
                foreach (DirectoryInfo x in _result)
                {
                    if (x.Name.Contains("GeoLiteCity"))
                        x.Delete(true);
                }

                return (namebool) ? aa.OrderByDescending(x => x.Value).FirstOrDefault().Key : "null";
            }
            else return "null";
        }
    }
    public class City
    {
        private int cityid;
        private string name;
        private string region;
        private string country;
        private string countrycode;
        private double latitude;
        private double longitude;
        public City(int CityId, string Name, string Region, string Country, string CountryCode, double Latitude, double Longitude)
        {
            _cityid = CityId;
            _name = Name;
            _region = Region;
            _country = Country;
            _countrycode = CountryCode;
            _latitude = Latitude;
            _longitude = Longitude;
        }
        public int _cityid
        {
            get { return cityid; }
            set { cityid = value; }
        }
        public string _name
        {
            get { return name; }
            set { name = value; }
        }
        public string _region
        {
            get { return region; }
            set { region = value; }
        }
        public string _country
        {
            get { return country; }
            set { country = value; }
        }
        public string _countrycode
        {
            get { return countrycode; }
            set { countrycode = value; }
        }
        public double _latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }
        public double _longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }
    }
    public class DowloadBaseThread
    {
        private static string dir;
        public DowloadBaseThread(string Dir)
        {
            dir = Dir;
        }
        private static string MytripConfigurationValue(string element, string attribute)
        {
            string _absolutDirectory = MytripConfigurationDirectory();
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements(element).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == attribute);
            return core.Attribute("value").Value;
        }
        private static string UrlGeo()
        {
            return MytripConfigurationValue("geo", "Url");
        }
        private static string MytripConfigurationDirectory()
        {
            return Path.Combine(dir, "App_Data\\mtm.Config.xml");
        }
        public void _DownloadBase()
        {
            string _name = "Geo_" + DateTime.Now.ToString("MM_yyyy") + ".dat";
            string _absolutDirectory = Path.Combine(dir, "App_Data");
            DirectoryInfo __absolutDirectory = new DirectoryInfo(_absolutDirectory);
            if (!__absolutDirectory.Exists)
                __absolutDirectory.Create();
            string ____absolutDirectory = Path.Combine(dir, "App_Data\\GeoLiteCity");
            DirectoryInfo ___absolutDirectory = new DirectoryInfo(____absolutDirectory);
            if (!___absolutDirectory.Exists)
                ___absolutDirectory.Create();
            FileInfo[] result = __absolutDirectory.GetFiles();
            DirectoryInfo[] _result = __absolutDirectory.GetDirectories();
            string XmlDirectory = MytripConfigurationDirectory();
            XDocument _doc = XDocument.Load(XmlDirectory);
            var core = _doc.Root.Elements("geo").Elements("add");
            core.FirstOrDefault(x => x.Attribute("name").Value == "DownloadGeo")
                .SetAttributeValue("value", true.ToString());
            _doc.Save(XmlDirectory);
            try
            {
                string absolutDirectory = Path.Combine(_absolutDirectory, "GeoLiteCity\\GeoLiteCity.dat.gz");
                //web
                WebClient myWebClient = new WebClient();
                myWebClient.DownloadFile(UrlGeo(), absolutDirectory);
                //testlocal
                //File.Copy("C:\\GeoLiteCity.dat.gz", absolutDirectory,true);                    
                FileStream fInStream = new FileStream(absolutDirectory, FileMode.Open, FileAccess.Read);
                GZipStream zipStream = new GZipStream(fInStream, CompressionMode.Decompress);
                string name = absolutDirectory.Replace("GeoLiteCity\\GeoLiteCity.dat.gz", _name);
                FileStream fOutStream = new FileStream(name, FileMode.Create, FileAccess.Write);
                byte[] tempBytes = new byte[60000000];
                int i;
                while ((i = zipStream.Read(tempBytes, 0, tempBytes.Length)) != 0)
                {
                    fOutStream.Write(tempBytes, 0, i);
                }
                fOutStream.Close();
                zipStream.Close();
                fInStream.Close();
                foreach (FileInfo x in result)
                {
                    if (x.Extension == ".dat" && x.Name != _name)
                        x.Delete();
                }
                foreach (DirectoryInfo x in _result)
                {
                    if (x.Name.Contains("GeoLiteCity"))
                        x.Delete(true);
                }
            }
            catch { }
            core.FirstOrDefault(x => x.Attribute("name").Value == "DownloadGeo")
                .SetAttributeValue("value", false.ToString());
            _doc.Save(XmlDirectory);

        }
    }
}