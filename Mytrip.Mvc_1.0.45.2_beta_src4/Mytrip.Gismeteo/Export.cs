using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using Mytrip.Gismeteo.Repository;
using Mytrip.Mvc;

namespace Mytrip.Gismeteo
{
    public static class Export
    {
        public static string AccordionGismeteo()
        {
            GismeteoSettings artset = new GismeteoSettings();
            if (artset.unlockGismeteo())
            {
                GismeteoRepository ar = new GismeteoRepository();
                StringBuilder _result = new StringBuilder();
                int count = 0;
            string culture=HttpContext.Current.Session["culture"].ToString();
            foreach (var item in ar.GetGismeteoForAccardion(culture))
                {
                    _result.AppendLine("<b>" + item.Title + "</b><div class=\"div_border\" style=\"border-left:0;border-right:0;\">");
                    var gismeteo = ar.GetOnlyGismeteoXml(item.UrlXml);
                    string data = "a";
                    foreach (var _gismeteo in gismeteo)
                    {
                        int day =int.Parse(_gismeteo.Attribute("day").Value);
                        int month = int.Parse(_gismeteo.Attribute("month").Value);                        
                        int year = int.Parse(_gismeteo.Attribute("year").Value);
                        int hour = int.Parse(_gismeteo.Attribute("tod").Value);
                        int phenom = int.Parse(_gismeteo.Element("PHENOMENA").Attribute("cloudiness").Value);
                        int termmax = int.Parse(_gismeteo.Element("TEMPERATURE").Attribute("max").Value);
                        int termmin = int.Parse(_gismeteo.Element("TEMPERATURE").Attribute("min").Value);
                        string _termmax = string.Empty;
                        if (termmax > 0)
                            _termmax = "+";
                        string _termmin = string.Empty;
                        if (termmin > 0)
                            _termmin = "+";
                        if (hour == 0 || hour == 2)
                        {
                            _result.AppendLine("<div style=\"height:22px;\">");
                            string _hour = GismeteoLanguage.night;
                            if (hour == 2)
                                _hour = GismeteoLanguage.day;
                            string _data = month + "/" + day + "/" + year;
                            if (culture.ToLower() == "ru-ru")
                                _data = day + "." + month + "." + year;
                            if (data != _data && data != "a")
                                _result.AppendLine("<div class=\"div_border\" style=\"border-left:0;border-right:0;border-top:0px;\"></div>");
                            if (_data != data)
                            {
                                data = _data;
                                _result.AppendLine("<div style=\"position:relative;float:right;\">" + data + "</div>");
                            }
                            string img = "<img src=\"/Content/Gismeteo/" + hour + "/" + phenom + ".png\" style=\"width:22px;position:relative;float:left;\"/>";
                            _result.AppendLine(img + " " + _hour+" " + _termmin + termmin + "&deg;C..." + _termmax + termmax + "&deg;C" + "</div>");
                        }
                    }
                    count++;
                    _result.AppendLine("</div>");
                }
            string gismeteolink = "<div style=\"height:10px;\"><a href=\"http://gismeteo.ru\" style=\"font-size:10px;float:right;\">gismeteo.ru</a></div>";
            bool cssclacc = false;
            if (count > 0)
                cssclacc = true;
            return GeneralMethods.Accordion2("accgismeteo", cssclacc, artset.NameGismeteoPage(), _result.ToString() + gismeteolink);
            }
            else { return string.Empty; }
        }
        public static string SettingGismeteo()
        {
            TagBuilder li = new TagBuilder("li");
            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", "/Gismeteo/GismeteoSetting");
            a.InnerHtml = GismeteoLanguage.Gismeteosetting;
            li.InnerHtml = a.ToString();
            return li.ToString();

        }
        public static string ManagerGismeteo()
        {
            TagBuilder li_votes = new TagBuilder("li");
            TagBuilder a_archive = new TagBuilder("a");
            a_archive.MergeAttribute("href", "/Gismeteo/Manager");
            a_archive.InnerHtml = GismeteoLanguage.Gismeteo_manager;
            li_votes.InnerHtml = a_archive.ToString();
            return li_votes.ToString();
        }
    }
}
