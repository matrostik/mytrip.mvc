using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mtm.Core.Repository.DataEntities;
using mtm.Core.Settings;
using mtm.Core.Helpers;
using System.Threading;

namespace mtm.Core.Repository
{
    public class StatisticRepository
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
        GeoRepository _geo;
        public GeoRepository geo
        {
            get
            {
                if (_geo == null)
                    _geo = new GeoRepository();
                return _geo;
            }
        }
        #endregion
        public IQueryable<mytrip_corestatistic> GetTodayStatistic()
        {
            string _start = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime _day = DateTime.Parse(_start);
            return entities.mytrip_corestatistic
                .Where(x => x.StatisticId != 0)
                .Where(x => x.Date >= _day)
                .OrderBy(x => x.StatisticId);
        }
        public IQueryable<mytrip_corestatistic> GetStatistic(DateTime start, DateTime end)
        {
           // try
            //{
                string _start = start.ToString("yyyy-MM-dd");
                DateTime _day = DateTime.Parse(_start);
                string _end = end.ToString("yyyy-MM-dd");
                DateTime _end2 = DateTime.Parse(_end);
                _end2 = _end2.AddDays(1);
                ////
                DateTime min = entities.mytrip_corestatistic
                    .OrderBy(x => x.Date).FirstOrDefault().Date;
                ////
                geo.RenameLocations();
                CreateDayStatistic(min, DateTime.Now);
                CreateMonthStatistic(min, DateTime.Now);
                CreateYearStatistic(min, DateTime.Now);
                ////
                string _min = min.ToString("yyyy-MM-dd");
                min = DateTime.Parse(_min);
                if (_day < min)
                    _day = min;
                if (_end2 < min)
                    _end2 = min.AddDays(1);
                return entities.mytrip_corestatistic
                    .Where(x => x.StatisticId != 0)
                    .Where(x => x.Date >= _day && x.Date < _end2)
                    .OrderBy(x => x.StatisticId);
            //}
            //catch { return null; }
        }
        private int StatisticId()
        {
            int catid = 0;
            mytrip_corestatistic a = entities.mytrip_corestatistic
                .FirstOrDefault(x => x.StatisticId == 0);
            if (a == null)
            {
                a = new mytrip_corestatistic
                {
                    CityId = 0,
                    Date = DateTime.Now,
                    Day = false,
                    Month = false,
                    StatisticId = 0,
                    UserCount = 0,
                    ViewCount = 0,
                    Year = false,
                    Page="0|"
                };
                entities.mytrip_corestatistic.AddObject(a);
            }
            string[] _a = a.Page.Split('|');
            if (_a.Count() < 3)
                catid = entities.mytrip_corestatistic
                                .OrderByDescending(x => x.StatisticId)
                                .Select(x => x.StatisticId).FirstOrDefault() + 1;
            else
            {                
                catid = int.Parse(_a[1]);
                a.Page = a.Page.Replace("|"+_a[1] + "|", "|").Trim();
            }
            return catid;
        }
        public void CreateStatistic(string page, string referrer)
        {
            geo.CreateCityZero();
            string browser = HttpContext.Current.Request.Browser.Browser;
            string version = HttpContext.Current.Request.Browser.Version;
            string userip = HttpContext.Current.Request.UserHostAddress;
            #region
            //userip = "178.49.68.199";
            //userip = "46.116.76.134";
            //userip = "87.253.82.6";
            //userip = "124.124.48.35";
            //userip = "61.155.28.42";
            //userip = "178.49.131.252";
            //userip = "91.208.153.1";
            //userip = "199.191.74.20";
            //userip = "178.120.64.41";
            //userip = "203.58.3.94";
            //userip = "118.70.129.177";
            //userip = "59.92.120.208";
            //userip = "82.200.122.179";
            //userip = "88.62.64.132";
            //userip = "203.91.201.56";
            //userip = "24.210.243.166";
            //userip = "120.138.105.165";
            #endregion
            string os = getFullOS(HttpContext.Current.Request.UserAgent);
            string _referrer = (!referrer.Contains("http://" + CoreSetting.applicationName())) ?
            referrer : null;
            try
            {
                int cityid = geo.CityId(userip);
                int statistic = StatisticId();
                mytrip_corestatistic x = new mytrip_corestatistic
                {
                    StatisticId = statistic,
                    Browser = string.Concat(browser, "_", version),
                    UserIP = userip,
                    CityId = cityid,
                    Date = DateTime.Now,
                    OS = os,
                    Page = page,
                    Referrer = _referrer,
                    UserCount = 1,
                    ViewCount = 1,
                    Day = false,
                    Month = false,
                    Year = false
                };
                entities.mytrip_corestatistic.AddObject(x);
                if (HttpContext.Current.Request.Cookies["mtStatistic"] != null)
                {
                    string[] a = HttpContext.Current.Request.Cookies["mtStatistic"].Value.Split('|');
                    if (a.Count() == 2)
                    {
                        try
                        {
                            int statid = int.Parse(a[0].Trim());
                            int c = int.Parse(a[1].Trim());
                            var b = entities.mytrip_corestatistic.Where(z => z.Day == false &&
                                z.Month == false && z.Year == false).FirstOrDefault(z => z.StatisticId == statid);
                            referrer = referrer.Replace("http://" + CoreSetting.applicationName(), "").Trim();
                            if (referrer.Contains("?"))
                                referrer = referrer.Substring(0, referrer.IndexOf("?"));
                            if (referrer == b.Page.Trim())
                            {
                                int time = (((DateTime.Now.Hour * 60) * 60) +
                (DateTime.Now.Minute * 60) +
                DateTime.Now.Second);
                                if (time > c)
                                    b.Time = time - c;
                            }

                        }
                        catch { }

                    }
                }
                entities.SaveChanges();
                int _time = (((DateTime.Now.Hour * 60) * 60) +
                    (DateTime.Now.Minute * 60) +
                    DateTime.Now.Second);
                string _stat = statistic + "|" + _time;
                HttpCookie cookie = new HttpCookie("mtStatistic", _stat);
                cookie.Expires = DateTime.Now.AddDays(1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch { }
        }
        private void CreateDayStatistic(DateTime start, DateTime end)
        {
            string _start = start.ToString("yyyy-MM-dd");
            DateTime _day = DateTime.Parse(_start);
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime _today = DateTime.Parse(_start);
            int todayday = _today.Day;
            int _todayday = DateTime.Now.Day;
            todayday = _todayday - todayday;
            _today = _today.AddDays(todayday);
            string _end = end.ToString("yyyy-MM-dd");
            DateTime _end2 = DateTime.Parse(_end);
            if (_end2 >= _today)
                _end2 = _today.AddDays(-1);
            if (_day >= _today)
                _day = _today.AddDays(-1);
            while (_day <= _end2)
            {
                DateTime _day2 = _day.AddDays(1);
                ArhivStatistic(_day, _day2, false, false, false);
                _day = _day.AddDays(1);
            }

        }
        private void CreateMonthStatistic(DateTime start, DateTime end)
        {
            string _start = start.ToString("yyyy-MM");
            string _end = end.ToString("yyyy-MM");
            DateTime _day = DateTime.Parse(_start);
            DateTime _end2 = DateTime.Parse(_end);
            if (_day == _end2)
                _end2 = _end2.AddMonths(1);
            string today = DateTime.Now.ToString("yyyy-MM");
            DateTime _today = DateTime.Parse(today);
            if (_end2 >= _today)
                _end2 = _today.AddMonths(-1);
            if (_day >= _today)
                _day = _today.AddMonths(-1);
            while (_day <= _end2)
            {
                DateTime _day2 = _day.AddMonths(1);
                ArhivStatistic(_day, _day2, true, false, false);
                _day = _day.AddMonths(1);
            }

        }
        private void CreateYearStatistic(DateTime start, DateTime end)
        {
            string _start = start.ToString("yyyy-MM");
            string[] __start = _start.Split('-');
            int ___start = int.Parse(__start[1]) - 1;
            string _end = end.ToString("yyyy-MM");
            string[] __end = _end.Split('-');
            int ___end = int.Parse(__end[1]) - 1;
            DateTime _day = DateTime.Parse(_start).AddMonths(-___start);
            DateTime _end2 = DateTime.Parse(_end).AddMonths(-___end);
            string today = DateTime.Now.ToString("yyyy-MM");
            string[] __today = today.Split('-');
            int ___today = int.Parse(__today[1]) - 1;
            DateTime _today = DateTime.Parse(today).AddMonths(-___today);
            if (_day == _end2)
                _end2 = _end2.AddYears(1);
            if (_end2 >= _today)
                _end2 = _today.AddYears(-1);
            if (_day >= _today)
                _day = _today.AddYears(-1);
            while (_day < _end2)
            {
                DateTime _day2 = _day.AddYears(1);
                ArhivStatistic(_day, _day2, false, true, false);
                _day = _day.AddYears(1);
            }

        }
        private void ArhivStatistic(DateTime _day, DateTime _day2, bool day, bool month, bool year)
        {
            var statistic = entities.mytrip_corestatistic
                        .Where(x => x.Day == day && x.Month == month && x.Year == year)
                        .Where(x => x.Date >= _day && x.Date < _day2)
                        .OrderBy(x => x.StatisticId);
            if (statistic != null)
            {
                string location = null;
                foreach (var x in statistic)
                {
                    location = StatisticHelper.FormationLocation(location, x.CityId.ToString());
                }
                if (location != null && location.Contains("]"))
                {
                    string[] _location = location.Replace("[", "").Split(']');
                    string deleteid = null;
                    foreach (var loc in _location)
                    {
                        if (loc != null && loc.Length > 0)
                        {
                            string browser = null;
                            string userip = null;
                            int usercount = 0;
                            int userview = 0;
                            string os = null;
                            string page = null;
                            string referrer = null;
                            int cityid = int.Parse(loc);
                            
                            foreach (var x in statistic
                                .Where(x => x.CityId == cityid))
                            {
                                userview++;
                                browser = StatisticHelper.FormationParam(browser, x.Browser);
                                userip = StatisticHelper.FormationParam(userip, x.UserIP);
                                os = StatisticHelper.FormationParam(os, x.OS);
                                referrer = StatisticHelper.FormationParam(referrer, x.Referrer);
                                if (!day && !month && !year)
                                {
                                    int time = (x.Time == null) ? 0 : (int)x.Time;
                                    page = StatisticHelper.FormationParam(page, x.Page, time);
                                }
                                else
                                    page = StatisticHelper.FormationParam(page, x.Page);
                                deleteid += x.StatisticId + "|";
                                entities.mytrip_corestatistic.DeleteObject(x);
                            }
                            
                            bool __day = day; bool __month = month; bool __year = year;
                            if (!day && !month && !year)
                                __day = true;
                            else if (day && !month && !year)
                            { __day = false; __month = true; }
                            else if (!day && month && !year)
                            { __month = false; __year = true; }
                            mytrip_corestatistic xx = new mytrip_corestatistic
                            {
                                StatisticId = StatisticId(),
                                Browser = browser,
                                CityId = cityid,
                                Date = _day,
                                Day = __day,
                                Month = __month,
                                OS = os,
                                Page = page,
                                Referrer = referrer,
                                UserCount = usercount,
                                UserIP = userip,
                                ViewCount = userview,
                                Year = __year
                            };
                            entities.mytrip_corestatistic.AddObject(xx);
                            entities.SaveChanges();
                        }
                    }
                    mytrip_corestatistic a = entities.mytrip_corestatistic
                            .FirstOrDefault(x => x.StatisticId == 0);
                    a.Page += deleteid.Trim();
                    entities.SaveChanges();
                }
            }
        }
        private string getFullOS(string userAgent)
        {
            string getFullOSRes = "Unknown";
            if (userAgent.Contains("Win"))
                getFullOSRes = "Windows";
            else if (userAgent.Contains("Mac"))
                getFullOSRes = "Macintosh";
            else if (userAgent.Contains("Linux"))
                getFullOSRes = "Linux";
            else if (userAgent.Contains("Unix"))
                getFullOSRes = "Unix";
            else if (userAgent.Contains("SunOS"))
                getFullOSRes = "Sun Solaris";
            else if (userAgent.Contains("FreeBSD"))
                getFullOSRes = "FreeBSD";
            return getFullOSRes;
        }
    }
}