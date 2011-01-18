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
        public IQueryable<mytrip_tours> Tours { get; set; }
        public int total { get; set; }
        public string PageTitle { get; set; }
        public mytrip_tourscategory CategoryOnly { get; set; }
    }
    public class TourViewModel
    {
        public string PageTitle { get; set; }
        public mytrip_tours Tours { get; set; }
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
    }
    public class EditorTourModel
    {
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string title { get; set; }
        [AllowHtml]
        public string body { get; set; }
        public int categoryid { get; set; }
        public SelectList category { get; set; }
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
    }
    public class OrderTourModel
    {
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "register_error_email")]
        public string Email { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
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
