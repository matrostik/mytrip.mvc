using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using mtm.Weather.Repository;
using mtm.Core;
using mtm.Core.Settings;

namespace mtm.Weather
{
    public static class Export
    {
        public static HtmlString AccordionWeather()
        {
            if (ModuleSettings.unlockWeather())
            {
                WeatherRepository ar = new WeatherRepository();
                StringBuilder _result = new StringBuilder();
                int count = 0;
                _result.AppendLine("<table class='noborders'>");
            string culture=LocalisationSetting.culture();
            foreach (var item in ar.GetWeatherForAccardion(culture))
            {
                _result.AppendLine("<tr><td>");
                    _result.AppendLine("<b>" + item.Title + "</b><div class=\"div_border\" style=\"border-left:0;border-right:0;\">");
                    _result.AppendLine("</td></tr>"); 
                var Weather = ar.GetOnlyWeatherXml(item.UrlXml);
                    string data = "a";
                    if (Weather != null)
                    {
                        foreach (var _Weather in Weather)
                        {
                            int day = int.Parse(_Weather.Attribute("day").Value);
                            int month = int.Parse(_Weather.Attribute("month").Value);
                            int year = int.Parse(_Weather.Attribute("year").Value);
                            int hour = int.Parse(_Weather.Attribute("tod").Value);
                            int phenom = int.Parse(_Weather.Element("PHENOMENA").Attribute("cloudiness").Value);
                            int termmax = int.Parse(_Weather.Element("TEMPERATURE").Attribute("max").Value);
                            int termmin = int.Parse(_Weather.Element("TEMPERATURE").Attribute("min").Value);
                            string _termmax = string.Empty;
                            if (termmax > 0)
                                _termmax = "+";
                            string _termmin = string.Empty;
                            if (termmin > 0)
                                _termmin = "+";
                            if (hour == 0 || hour == 2)
                            {
                                _result.AppendLine("<tr><td>");
                                string _hour = WeatherLanguage.night;
                                if (hour == 2)
                                    _hour = WeatherLanguage.day;
                                string _data = month + "/" + day + "/" + year;
                                if (culture.ToLower() == "ru-ru")
                                    _data = day + "." + month + "." + year;
                                if (data != _data && data != "a")
                                {
                                    
                                    _result.AppendLine("<div class=\"div_border\" style=\"border-left:0;border-right:0;border-top:0px;\"></div>");
                                }
                                if (_data != data)
                                {
                                    data = _data;
                                    _result.AppendLine("<div style=\"text-align:right;\">" + data + "</div>");
                                }
                                
                                string img = "<img src=\"/Weather/Image/_" + hour + "_" + phenom + "\" style=\"width:22px;position:relative;float:left;\"/>";
                                _result.AppendLine(img + " " + _hour + " " + _termmin + termmin + "&deg;C..." + _termmax + termmax + "&deg;C" + "</div>");
                                _result.AppendLine("</td></tr>");
                            }
                        }
                    }
                    count++;

            } _result.AppendLine("</table>");
            string Weatherlink = "<div style=\"height:10px;\"><a href=\"http://gismeteo.ru\" style=\"font-size:10px;float:right;\">gismeteo.ru</a></div>";
            bool cssclacc = false;
            if (count > 0)
                cssclacc = true;
            return new HtmlString(GeneralMethods.Accordion(cssclacc, ModuleSettings.NameWeatherPage(), _result.ToString() + Weatherlink));
            }
            else { return null; }
        }
        public static HtmlString Setting()
        {
            TagBuilder li = new TagBuilder("li");
            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", "/Weather/Setting");
            a.InnerHtml = WeatherLanguage.Weathersetting;
            li.InnerHtml = a.ToString();
            HtmlString htmlresult = new HtmlString(li.ToString());
            return htmlresult;

        }
        public static HtmlString Manager()
        {
            TagBuilder li_votes = new TagBuilder("li");
            TagBuilder a_archive = new TagBuilder("a");
            a_archive.MergeAttribute("href", "/Weather/Manager");
            a_archive.InnerHtml = WeatherLanguage.Weather_manager;
            li_votes.InnerHtml = a_archive.ToString();
            HtmlString htmlresult = new HtmlString(li_votes.ToString());
            return htmlresult;
        }
    }
}
