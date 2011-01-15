using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mtm.Core.Repository.DataEntities;
using System.Text;
using mtm.Core.Settings;
using mtm.Core.Repository;
using System.Collections;

namespace mtm.Core.Helpers
{
    public static class StatisticHelper
    {
        public static HtmlString ScriptStatistic(this HtmlHelper html)
        {
            if (!CoreSetting.Development()&&StatisticSetting.unlockStatistic())
            {
                string referrer = "(direct)";
                try
                {
                    referrer = HttpContext.Current.Request.UrlReferrer.ToString();
                }
                catch { }
                string path = HttpContext.Current.Request.Path.Replace("/", "(y)").Replace("&", "(z)");
                referrer = referrer.Replace("/", "(y)").Replace("&", "(z)");
                StringBuilder result = new StringBuilder();
                result.AppendLine("<script type=\"text/javascript\">");
                result.AppendLine("$(document).ready(function () {");
                result.AppendLine("$.ajax({ type: 'POST', url: '/mtm/Statistic', data: 'path=" + path + "&refer=" + referrer + "' });");
                result.AppendLine("});");
                result.AppendLine("</script>");
                HtmlString _result = new HtmlString(result.ToString());
                return _result;
            }
            else return null;
        }
        public static HtmlString Statistic(this HtmlHelper hthl, IQueryable<mytrip_corestatistic> body)
        {
            StringBuilder result = new StringBuilder();
            string ipusers = "";
            string browser = "";
            int usercount = 0;
            int userview = 0;
            string page = "";
            string os = "";
            string referrer = "";
            if (body != null)
            {
                foreach (var x in body)
                {
                    userview += x.ViewCount;
                    ipusers = FormationLocation(ipusers, x.UserIP);
                    if (!x.Day && !x.Month && !x.Year)
                    {
                        int time = (x.Time == null) ? 0 : (int)x.Time;
                        page = FormationParam(page, x.Page, time);
                    }
                    else if (x.Day || x.Month || x.Year)
                    {
                        page = FormationParam(page, x.Page);
                    }
                    browser = FormationParam(browser, x.Browser);
                    os = FormationParam(os, x.OS);
                    referrer = FormationParam(referrer, x.Referrer);
                }
            }
            usercount = ipusers.Split(']').Count() - 1;
            #region page
            string _a1 = "<h4>"
                + CoreLanguage.viewPages + ":</h4>";
            string[] brow = page.Replace("[", "").Split(']');
            IDictionary<string, int>  dic = new Dictionary<string, int>();
            int countbrow = 0;
            foreach (var _brow in brow)
            {
                string[] __brow = _brow.Split('|');
                if (__brow.Count() == 3)
                {
                    int br = int.Parse(__brow[1].Trim());
                    dic.Add(__brow[0] + "|" + __brow[2].Trim(), br);
                    countbrow += br;
                }
            }
            _a1 += "<div class='scroll'><table class='noborders'>";
            foreach (var _dic in dic.OrderByDescending(x => x.Value))
            {
                decimal br = (_dic.Value * 100) / countbrow;
                string[] _page = _dic.Key.Trim().Split('|');
                int time = 0;
                if (_page.Count() == 2)
                {
                    time = int.Parse(_page[1]);
                }
                string _time = "N/A";
                if (time > 0)
                {
                    int second = (time > 60) ? time % 60 : time;
                    int minute = (time > 60) ? time / 60 : 0;
                    minute = (minute > 60) ? minute % 60 : minute;
                    int hour = (minute > 60) ? minute / 60 : 0;
                    _time = hour + ":" + minute.ToString("00") + ":" + second.ToString("00");
                }
                _a1 += "<tr><td><a href='"
                    + _page[0] +
                    "'>" + _page[0] + "</a></td><td>" + _time + "</td><td>" + _dic.Value + "</td><td>"
                    + br.ToString("0.00") + "%</td></tr>";
            }
            _a1 += "</table></div>";
            _a1 += "<h4 style='margin-top:5px;'>" 
                + CoreLanguage.referrerPages + ":</h4>";
            brow = referrer.Replace("[", "").Split(']');
            dic = new Dictionary<string, int>();
            countbrow = 0;
            foreach (var osss in brow)
            {
                string[] __os = osss.Split('|');
                if (__os.Count() == 2 && !__os[0].Contains("http://" + CoreSetting.applicationName())
                    && !__os[0].Contains("http://www." + CoreSetting.applicationName()))
                {
                    int br = int.Parse(__os[1].Trim());
                    dic.Add(__os[0], br);
                    countbrow += br;
                }
            } _a1 += "<div class='scroll'><table class='noborders'>";
            foreach (var _dic in dic.OrderByDescending(x => x.Value))
            {
                decimal br = (_dic.Value * 100) / countbrow;
                _a1 += "<tr><td>"
                    + _dic.Key +
                    "</td><td>" + _dic.Value + "</td><td>"
                    + br.ToString("0.00") + "%</td></tr>";
            }
            _a1 += "</table></div>";
            
            #endregion
            #region users
            string _a = "<table class='noborders'><tr><td><h4>" +
                CoreLanguage.visitors + ": " + usercount + "</h4>" +
                "<h4>" + CoreLanguage.views + ": " + userview +
                "</h4>";
            _a += "</td><td><h4>" + CoreLanguage.browsers + ":</h4>";
            brow=browser.Replace("[","").Split(']');
            dic = new Dictionary<string, int>();
            countbrow = 0;
            foreach (var _brow in brow)
            {
                string[] __brow = _brow.Split('|');
                if (__brow.Count() == 2)
                {
                    int br=int.Parse(__brow[1].Trim());
                    dic.Add(__brow[0], br);
                    countbrow += br;
                }
            }
            _a += "<table class='noborders'>";
            foreach (var _dic in dic.OrderByDescending(x => x.Value))
            {
                decimal br = (_dic.Value * 100 )/ countbrow;
                _a +="<tr><td>"
                    + _dic.Key
                    .Replace("IE_999,1", "Internet Explorer 9 beta")
                    .Replace("IE_", "Internet Explorer ")
                    .Replace("Firefox_", "Mozilla Firefox ")
                    .Replace("Chrome_", "Google Chrome ") + 
                    "</td><td>" 
                    + br.ToString("0.00") + "%</td></tr>";
            }
            _a += "</table>";
            _a += "</td><td><h4>" + CoreLanguage.os + ":</h4>";
            string[] oss = os.Replace("[", "").Split(']');
            dic = new Dictionary<string, int>();
            countbrow = 0;
            foreach (var osss in oss)
            {
                string[] __os = osss.Split('|');
                if (__os.Count() == 2)
                {
                    int br = int.Parse(__os[1].Trim());
                    dic.Add(__os[0], br);
                    countbrow += br;
                }
            } _a += "<table class='noborders'>";
            foreach (var _dic in dic.OrderByDescending(x => x.Value))
            {
                decimal br = (_dic.Value * 100) / countbrow;
                _a += "<tr><td>"
                    + _dic.Key +
                    "</td><td>"
                    + br.ToString("0.00") + "%</td></tr>";
            }
            _a += "</table>";
            _a += "</td></tr></table>";
            result.AppendLine(GeneralMethods.Accordion(CoreLanguage.infoUsers, _a));
            #endregion

            result.AppendLine(GeneralMethods.Accordion(CoreLanguage.infoPages, _a1));
            return new HtmlString(result.ToString());
        }
        public static HtmlString StatisticMap(this HtmlHelper html)
        {
            if (GeoSetting.unlockGeo())
            {
                string a = "<div id='mapDiv' style='position:relative; height:500px;'></div>";
                return new HtmlString(GeneralMethods.Accordion(CoreLanguage.statisticMap, a));
            }
            else return null;
        }
        public static HtmlString StatisticLink(this HtmlHelper html)
        {
            if (!CoreSetting.Development() && StatisticSetting.unlockStatistic())
            {
                string a = "<a href='/Home/Statistic' title='" +
                        string.Format(CoreLanguage.statisticTitle, DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd")
                        + " - " + DateTime.Now.ToString("yyyy-MM-dd"))
                        + "' ><img src='/Theme/" + ThemeSetting.theme() + "/images/statistic.png' class='footerlogo' alt='" +
                        string.Format(CoreLanguage.statisticTitle, DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd")
                        + " - " + DateTime.Now.ToString("yyyy-MM-dd"))
                        + "'/></a>";
                StringBuilder result = new StringBuilder();
                if (StatisticSetting.statisticAnonym() || StatisticSetting.statisticUser())
                    result.Append(a);
                else if (MytripUser.UserInRole(UsersSetting.roleAdmin()) || MytripUser.UserInRole(UsersSetting.roleChiefEditor()))
                    result.Append(a);
                return new HtmlString(result.ToString());
            }
            else return null;
        }
        public static string FormationLocation(string a, string b)
        {
            if (b != null&&a == null && !b.Contains("|"))
                    a = "[" + b + "]";
            else if (b != null && a != null && !b.Contains("|") && !a.Contains("[" + b + "]"))
                    a += "[" + b + "]";
            else if (b != null && b.Contains("|"))
                {
                    string[] _b = b.Replace("[", "").Split(']');
                    foreach (var _a in _b)
                    {
                        string[] c = _a.Trim().Split('|');
                        if (c.Count() == 2)
                        {
                            if (a == null)
                                a = "[" + c[0].Trim() + "]";
                            else if (!a.Contains("[" + c[0].Trim() + "]"))
                                a += "[" + c[0].Trim() + "]";
                        }
                    }
                }
           
            return a;
        }
        public static string FormationParam(string a, string b)
        {
            if ((a == null || !a.Contains("[")) && b != null && !b.Contains("]"))
                a = "[" + b + "|" + 1 + "]";
            else if ((a == null || !a.Contains("[")) && b != null && b.Contains("]"))
                a = b;
            else if (a != null&&a.Contains("[")&&b != null && !b.Contains("]"))
            {
                if (!a.Contains("[" + b + "|"))
                    a += "[" + b + "|" + 1 + "]";
                else
                {
                    string[] c = a.Replace("[", "").Split(']');
                    foreach (var z in c)
                    {
                        if (z.Contains(b + "|"))
                        {
                            int _a = int.Parse(z.Replace(b + "|", "").Trim());
                            int _b = _a + 1;
                            a = a.Replace("[" + b + "|" + _a + "]", "[" + b + "|" + _b + "]");
                        }
                    }
                }
            }
            else if (a != null &&a.Contains("[")&& b != null && b.Contains("]"))
            {
                string[] c = a.Replace("[", "").Split(']');
                string[] d = b.Replace("[", "").Split(']');
                string param = "|";
                foreach (var z in c)
                {
                    int count = 0;
                    string[] f = z.Trim().Split('|');
                    if (f.Count() == 2)
                    {
                        foreach (var e in d)
                        {  
                            string[] g = e.Trim().Split('|');
                            if (g[0] == f[0])
                            {
                                param += count + "|";
                                int _a = int.Parse(f[1]);
                                int _b = int.Parse(g[1]);
                                _b += _a;
                                a = a.Replace("[" + g[0] + "|" + _a + "]", "[" + g[0] + "|" + _b + "]");
                            }
                            count++;
                        }
                    }
                    else if (f.Count() == 3)
                    {
                        foreach (var e in d)
                        {
                            string[] g = e.Trim().Split('|');
                            if (g.Count() == 3 && g[0].Trim() == f[0].Trim())
                            {
                                param += count + "|";
                                int _a = int.Parse(f[1]);
                                int _b = int.Parse(g[1]);
                                _b += _a;
                                int w = int.Parse(f[2]);
                                int q = int.Parse(g[2]);
                                if (w > 0 && q > 0)
                                    q = (q + w) / 2;
                                else
                                    q += w;
                                a = a.Replace("[" + g[0] + "|" + _a + "|" + w + "]", "[" + g[0] + "|" + _b + "|" + q + "]");
                            }
                                count++;
                        }
                    }
                }
                int _count = 0;
                foreach (var x in d)
                {
                    if (!param.Contains("|" + _count + "|"))
                        a += "[" + x + "]";
                    _count++;
                }
            }
            return a;
        }
        public static string FormationParam(string a, string b, int c)
        {
            if (a == null && b != null)
                a = "[" + b + "|" + 1 + "|" + c + "]";
            else if (b != null)
            {
                if (!a.Contains("[" + b + "|"))
                    a += "[" + b + "|" + 1 + "|" + c + "]";
                else
                {
                    string[] _c = a.Replace("[", "").Split(']');

                    foreach (var z in _c)
                    {
                        string[] _z = z.Trim().Split('|');
                        if (_z.Count() == 3 && _z[0].Trim() == b.Trim())
                        {
                            int _a = int.Parse(_z[1].Trim());
                            int _b = _a + 1;
                            int _d = int.Parse(_z[2].Trim());
                            if (_d > 0 && c > 0)
                                c = (c + _d) / 2;
                            else
                                c += _d;
                            a = a.Replace("[" + b + "|" + _a + "|" + _d + "]",
                                "[" + b + "|" + _b + "|" + c + "]");

                        }
                    }
                }
            }
            return a;
        }
    }
}