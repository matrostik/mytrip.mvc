using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mtm.Core.Repository.DataEntities;
using System.Text;
using mtm.Core.Settings;
using mtm.Core.Repository;


namespace mtm.Core.Helpers
{
    public static class MapHelper
    {
        public static HtmlString ScriptStatisticBingMap(this HtmlHelper helper, IQueryable<mytrip_corestatistic> body)
        {
            if (GeoSetting.unlockGeo())
            {
                StringBuilder result = new StringBuilder();
                result.AppendLine("<script type=\"text/javascript\">");
                result.AppendLine("function GetMap() {");
                result.AppendLine("var map = new Microsoft.Maps.Map(document.getElementById('mapDiv'),");
                result.AppendLine("{");
                result.AppendLine("credentials: '" + GeoSetting.keyBingMap() + "',");
                result.AppendLine("center: new Microsoft.Maps.Location(50, 5),");
                result.AppendLine("mapTypeId: Microsoft.Maps.MapTypeId.road,");
                result.AppendLine("showLogo: false,");
                result.AppendLine("showCopyright: false,");
                result.AppendLine("showDashboard: false,");
                result.AppendLine("showScalebar: false,");
                result.AppendLine("zoom: 2");
                result.AppendLine("});");
                result.AppendLine("var red = '/Theme/" + ThemeSetting.theme() + "/images/PushPin.png';");
                string stat = "";
                string usrcount = "";
                if (body != null)
                {
                    foreach (var x in body)
                    {
                        if (!x.Day && !x.Month && !x.Year)
                        {
                            if (usrcount == null)
                                usrcount = "[" + x.CityId + "|" + x.UserIP + "]";
                            else if (!usrcount.Contains("[" + x.CityId + "|" + x.UserIP + "]"))
                                usrcount += "[" + x.CityId + "|" + x.UserIP + "]";
                        }
                        else
                        {
                            string[] b = x.UserIP.Replace("[", "").Split(']');
                            foreach (var a in b)
                            {
                                string[] c = a.Trim().Split('|');
                                if (c.Count() == 2)
                                {
                                    if (usrcount == null)
                                        usrcount = "[" + x.CityId + "|" + c[0].Trim() + "]";
                                    else if (!usrcount.Contains("[" + x.CityId + "|" + c[0].Trim() + "]"))
                                        usrcount += "[" + x.CityId + "|" + c[0].Trim() + "]";
                                }
                            }
                        }
                        if (stat == null)
                            stat = "[" + x.CityId + "|" + x.ViewCount + "]";
                        else if (!stat.Contains("[" + x.CityId + "|"))
                            stat += "[" + x.CityId + "|" + x.ViewCount + "]";
                        else
                        {
                            string[] c = stat.Replace("[", "").Split(']');
                            foreach (var a in c)
                            {
                                string[] b = a.Trim().Split('|');
                                if (b.Count() == 2 && b[0].Trim() == x.CityId.ToString())
                                {
                                    int e = int.Parse(b[1].Trim());
                                    int _e = e + x.ViewCount;
                                    stat = stat.Replace("[" + x.CityId + "|" + e + "]",
                                        "[" + x.CityId + "|" + _e + "]");
                                }

                            }
                        }
                    }
                }
                GeoRepository geo = new GeoRepository();
                var city = geo.GetAllCity();
                string[] _c = stat.Replace("[", "").Split(']');
                string[] _u = usrcount.Replace("[", "").Split(']');
                int namber = 1;
                foreach (var a in _c)
                {
                    string[] b = a.Trim().Split('|');
                    if (b.Count() == 2)
                    {
                        int count = 0;
                        foreach (var u in _u)
                        {
                            string[] f = u.Trim().Split('|');
                            if (f.Count() == 2 && f[0].Trim() == b[0].Trim())
                                count++;
                        }
                        int d = int.Parse(b[0].Trim());
                        int g = count;
                        var _city = city.FirstOrDefault(x => x.CityId == d);
                        if (_city != null)
                        {
                            result.AppendLine("var pin" + namber +
                                " = new Microsoft.Maps.Pushpin(new Microsoft.Maps.Location(" +
                                _city.Latitude.ToString().Replace(",", ".") + ", " +
                                _city.Longitude.ToString().Replace(",", ".") + "), { icon: red});");
                            result.AppendLine("var info" + namber + " = Microsoft.Maps.Ext.InfoBox(");
                            result.AppendLine("'<h4>" + _city.Name.Replace("'","") + "</h4>',");
                            result.AppendLine("'" + CoreLanguage.visitors + ": " + g + "<br/>" +
                                CoreLanguage.views + ": " + b[1] + "',");
                            result.AppendLine("map");
                            result.AppendLine(");");
                            result.AppendLine("pin" + namber + ".setInfoBox(info" + namber + ");");
                            result.AppendLine("map.entities.push(pin" + namber + ");");
                            namber++;
                        }
                    }

                }
                result.AppendLine("}");
                result.AppendLine("$(document).ready(function () {");

                result.AppendLine("$('div.infobox').live(");
                result.AppendLine("'hover',");
                result.AppendLine("function (ev) {");
                result.AppendLine("if (ev.type == 'mouseover') {");
                result.AppendLine("$(this).show();");
                result.AppendLine("}");
                result.AppendLine("if (ev.type == 'mouseout') {");
                result.AppendLine("$(this).hide();");
                result.AppendLine("}");
                result.AppendLine("});");
                result.AppendLine("});");
                result.AppendLine("</script>");
                return new HtmlString(result.ToString());
            }
            else return null;
        }
        public static HtmlString ScriptProfileBingMap(this HtmlHelper helper, mytrip_usersprofile x, bool create)
        {
            if (GeoSetting.unlockGeo())
            {
                if (x.Latitude == null)
                    x.Latitude = x.mytrip_geocity.Latitude;
                if (x.Longitude == null)
                    x.Longitude = x.mytrip_geocity.Longitude;
                if (x.Latitude == 0 && x.Longitude == 0)
                {
                    x.Latitude = 40;
                        x.Longitude = -10;
                }
                StringBuilder result = new StringBuilder();
                result.AppendLine("<script type=\"text/javascript\">");
                result.AppendLine("function GetMap() {");
                result.AppendLine("var map = new Microsoft.Maps.Map(document.getElementById('mapDiv'),");
                result.AppendLine("{");
                result.AppendLine("credentials: '" + GeoSetting.keyBingMap() + "',");
                result.AppendLine("center: new Microsoft.Maps.Location(" + x.Latitude.ToString().Replace(",", ".")
                    + ", " + x.Longitude.ToString().Replace(",", ".") + "),");
                result.AppendLine("mapTypeId: Microsoft.Maps.MapTypeId.road,");
                result.AppendLine("showLogo: false,");
                result.AppendLine("showCopyright: false,");
                result.AppendLine("showDashboard: false,");
                result.AppendLine("showScalebar: false,");
                result.AppendLine("zoom: 2");
                result.AppendLine("});");
                result.AppendLine("var red = '/Theme/" + ThemeSetting.theme() + "/images/PushPin.png';");
                result.AppendLine("var pin1" +
                    " = new Microsoft.Maps.Pushpin(new Microsoft.Maps.Location(" +
                    x.Latitude.ToString().Replace(",", ".") + ", " +
                    x.Longitude.ToString().Replace(",", ".") + "), { icon: red"+(create?",draggable: true":"")+"});");
                if (!create)
                {
                    result.AppendLine("var info1= Microsoft.Maps.Ext.InfoBox(");
                    result.AppendLine("'<div class=\"right\" style=\"margin-right:5px;\" >" +
                        AvatarHelper.Avatar(helper, MytripUser.UserEmail(x.mytrip_users.UserName), new { width = 50 }).ToString().Replace("'", "\"")
                        + "</div><h4>" + x.mytrip_users.UserName.Replace("'", "") + "</h4>',");
                    result.AppendLine("'" + x.mytrip_geocity.Name.Replace("'", "") + "<br/>"
                        + x.mytrip_geocity.mytrip_georegion.Name.Replace("'", "") + "<br/>"
                        + x.mytrip_geocity.mytrip_georegion.mytrip_geocountry.Name.Replace("'", "") + "<br/>',");
                    result.AppendLine("map");
                    result.AppendLine(");");
                    result.AppendLine("pin1.setInfoBox(info1);");
                }
                result.AppendLine("map.entities.push(pin1);");
                if (create)
                    result.AppendLine("Microsoft.Maps.Events.addHandler(pin1, 'drag', DragHandler);");
                result.AppendLine("}");
                if (!create)
                {
                    result.AppendLine("$(document).ready(function () {");
                    result.AppendLine("$('div.infobox').live(");
                    result.AppendLine("'hover',");
                    result.AppendLine("function (ev) {");
                    result.AppendLine("if (ev.type == 'mouseover') {");
                    result.AppendLine("$(this).show();");
                    result.AppendLine("}");
                    result.AppendLine("if (ev.type == 'mouseout') {");
                    result.AppendLine("$(this).hide();");
                    result.AppendLine("}");
                    result.AppendLine("});");
                    result.AppendLine("});");
                }
                result.AppendLine("</script>");
                return new HtmlString(result.ToString());
            }
            else return null;
        }
      
    }
}