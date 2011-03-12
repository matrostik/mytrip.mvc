using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Core.Repository;
using System.Web.Mvc;
using mtm.Core.Settings;
using System.Web;
using mtm.Tourism.Repository.DataEntities;

namespace mtm.Tourism.Helpers
{
    public static class EditorHelper
    {
        public static string AddCategoryAndTour(int id, bool subcat, bool tour)
        {
            if (MytripUser.UserInRole(ModuleSetting.roleTourManager()) || MytripUser.UserInRole(ModuleSetting.roleChiefTourManager()))
            {
                TagBuilder img = new TagBuilder("img");
                img.MergeAttribute("src", "/Theme/" + ThemeSetting.theme() + "/images/add.png");
                img.MergeAttribute("style", "height:14px;");
                string a = string.Empty;
                if (subcat)
                {
                    a = "<br/><a href=\"/Tours/EditorCategory/" + id + "/CreateCategory \">" + ToursLanguage.createsubcategory1 + " " + img + "</a>";
                }
                if (tour)
                {
                    a += "<br/><a href=\"/Tours/EditorTour/" + id + "/CreateTour\">" + ToursLanguage.createtour + " " + img + "</a>";
                }
                string country = "<a href=\"/Tours/EditorCategory/" + id + "/CreateCountry \">" + ToursLanguage.createcountry + " " + img + "</a><br/>";
                return "<div class=\"right\"><div class=\"edit\">" + country + "<a href=\"/Tours/EditorCategory/0/CreateCategory\">" + ToursLanguage.createcategory + " " + img + "</a>" + a + "</div></div>";
            }
            return null;
        }
        public static string EditAndDeleteCategory(int id, string user, string subuser)
        {
            if (MytripUser.UserInRole(ModuleSetting.roleChiefTourManager()) || (MytripUser.UserInRole(ModuleSetting.roleTourManager()) && (HttpContext.Current.User.Identity.Name == user || HttpContext.Current.User.Identity.Name == subuser)))
            {
                string edit = "/EditCategory";
                string delete = "/DeleteCategory";
                return GeneralMethods.ImgInput("/images/edite.png", "/Tours/EditorCategory/" + id + edit, "rename", 14) +
                              " " + GeneralMethods.ImgInput("/images/delete.png", "/Tours/EditorCategory/" + id + delete, "delete", 14);
            }
            else return null;
        }
        public static string EditAndDeleteCountry(int id, string user, string subuser)
        {
            if (MytripUser.UserInRole(ModuleSetting.roleChiefTourManager()) || (MytripUser.UserInRole(ModuleSetting.roleTourManager()) && (HttpContext.Current.User.Identity.Name == user || HttpContext.Current.User.Identity.Name == subuser)))
            {
                string edit = "/EditCountry";
                string delete = "/DeleteCountry";
                return GeneralMethods.ImgInput("/images/edite.png", "/Tours/EditorCategory/" + id + edit, "rename", 14) +
                              " " + GeneralMethods.ImgInput("/images/delete.png", "/Tours/EditorCategory/" + id + delete, "delete", 14);
            }
            else return null;
        }
        public static HtmlString EditAndDeleteTour(this HtmlHelper html, mytrip_tours x)
        {
            if (MytripUser.UserInRole(ModuleSetting.roleChiefTourManager()) 
                || (MytripUser.UserInRole(ModuleSetting.roleTourManager()) 
                && (HttpContext.Current.User.Identity.Name == x.UserName 
                || HttpContext.Current.User.Identity.Name == x.mytrip_tourscategory.UserName
                || HttpContext.Current.User.Identity.Name == x.mytrip_tourscategory.mytrip_tourscategory2.UserName)))
            {
                string edit = "/EditTour";
                string delete = "/DeleteTour";
                return new HtmlString(GeneralMethods.ImgInput("/images/edite.png", "/Tours/EditorTour/" + x.TourId + edit, "rename", 14) +
                              " " + GeneralMethods.ImgInput("/images/delete.png", "/Tours/EditorTour/" + x.TourId + delete, "delete", 14));
            }
            else return null;
        }
    }
}
