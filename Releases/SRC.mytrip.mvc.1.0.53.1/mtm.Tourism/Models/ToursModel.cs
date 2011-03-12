using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Tourism.Repository.DataEntities;
using System.Web.Mvc;
using mtm.Core.Repository;
using System.Web.Routing;
using System.Web;
using System.ComponentModel.DataAnnotations;
using mtm.Core;

namespace mtm.Tourism.Models
{
    public class ToursIndexModel
    {
        public IQueryable<mytrip_tourscategory> Category { get; set; }
        public IQueryable<mytrip_tourscountry> Country { get; set; }
        public IQueryable<mytrip_tours> Tours { get; set; }
        public int total { get; set; }
        public string PageTitle { get; set; }
        public mytrip_tourscategory CategoryOnly { get; set; }
        public mytrip_tourscountry CountryOnly { get; set; }
        public string seoTitle { get; set; }
        public string seokeywords { get; set; }
        public string seodescription { get; set; }
        public string[] bread { get; set; }
        public bool countryview { get; set; }
        public bool _country { get; set; }
        public string __search { get; set; }
        public string __startdate { get; set; }
        public string __stopdate { get; set; }
        public int __category { get; set; }
        public int __country { get; set; }
        public SelectList __categorylist { get; set; }
        public SelectList __countrylist { get; set; }
    }
    public class TourViewModel
    {
        public string PageTitle { get; set; }
        public mytrip_tours Tours { get; set; }
        public string[] bread { get; set; }
        public string seoTitle { get; set; }
        public string seokeywords { get; set; }
        public string seodescription { get; set; }
    }
    public class EditorCategoryModel
    {
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string title { get; set; }
        public bool allculture { get; set; }
        [AllowHtml]
        public string body { get; set; }
        public int categoryId { get; set; }
        public string submit { get; set; }
        public string TitlePage { get; set; }
        public string[] bread { get; set; }
        public string path { get; set; }
        public string seoTitle { get; set; }
        public string seokeywords { get; set; }
        public string seodescription { get; set; }
        public int id { get; set; }
        public string id2 { get; set; }
    }
    public class EditorTourModel
    {
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string title { get; set; }
        [AllowHtml]
        public string body { get; set; }
        public int categoryid { get; set; }
        public int countryid { get; set; }
        public SelectList category { get; set; }
        public SelectList country { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string stopdate { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string startdate { get; set; }
        [AllowHtml]
        public string image { get; set; }
        public bool allculture { get; set; }
        public string submit { get; set; }
        public string TitlePage { get; set; }
        //Варианты отелей и цен
        public IEnumerable<mytrip_toursvariants> varianty { get; set; }
        public int tourid { get; set; }
        [AllowHtml]
        public string hotel { get; set; }
        [AllowHtml]
        public string services { get; set; }
        public string price { get; set; }
        public string momeyid { get; set; }
        public SelectList money { get; set; }
        public string[] bread { get; set; }
        public string path { get; set; }
        public string seoTitle { get; set; }
        public string seokeywords { get; set; }
        public string seodescription { get; set; }
        public int id { get; set; }
        public string id2 { get; set; }
    }
    public class OrderTourModel
    {
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "register_error_email")]
        public string Email { get; set; }
        //[Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string Phone { get; set; }
        public string Country { get; set; }
        public string Resort { get; set; }
        public string FromCity { get; set; }
        public string StartDate { get; set; }
        public string StopDate { get; set; }
        public string CountPeople { get; set; }
        public string Hotel { get; set; }
        [AllowHtml]
        public string Body { get; set; }
        public string[] bread { get; set; }
        public bool mess { get; set; }
        public string PageTitle { get; set; }
        public string seoTitle { get; set; }
        public string seokeywords { get; set; }
        public string seodescription { get; set; }
    }
    public class TourSetting {

public bool unlockTours{ get; set; }
    public bool viewDescription{ get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public int columnTours { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public int styleTours { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public int widthImgTours { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public int MoneyProcent { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public int closeTour { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string roleChiefTourManager { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string roleTourManager { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string partialAccordion { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string partialNoAccordion { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string partialMenuLogon { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string partialMenuLogonWrap { get; set; }
    public string[] bread { get; set; }
    public string view_ru_ru { get; set; }
    public string view_en_us { get; set; }

    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string nameTours_ru_ru { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string nameTours_en_us { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string nameTours_title_ru_ru { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string nameTours_title_en_us { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string nameTours_keywords_ru_ru { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string nameTours_keywords_en_us { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string nameTours_description_ru_ru { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string nameTours_description_en_us { get; set; }

    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string nameCountry_ru_ru { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string nameCountry_en_us { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string nameCountry_title_ru_ru { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string nameCountry_title_en_us { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string nameCountry_keywords_ru_ru { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string nameCountry_keywords_en_us { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string nameCountry_description_ru_ru { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string nameCountry_description_en_us { get; set; }

    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string nameOrderTours_ru_ru { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string nameOrderTours_en_us { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string nameOrderTours_title_ru_ru { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string nameOrderTours_title_en_us { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string nameOrderTours_keywords_ru_ru { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string nameOrderTours_keywords_en_us { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string nameOrderTours_description_ru_ru { get; set; }
    [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
    public string nameOrderTours_description_en_us { get; set; }
        
    }
    #region RoleTourAttribute
    // **********************************************
    // RoleStoreAttribute
    // **********************************************

    /// <summary>
    /// 
    /// </summary>
    public class RoleTourAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (MytripUser.UserInRole(ModuleSetting.roleChiefTourManager()) || MytripUser.UserInRole(ModuleSetting.roleTourManager()))
            {

            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new { controller = "Account", action = "LogOn", returnUrl = HttpContext.Current.Request.Path.ToString() }));
            }
        }
    }
    //****************** E N D **********************
    #endregion
}
