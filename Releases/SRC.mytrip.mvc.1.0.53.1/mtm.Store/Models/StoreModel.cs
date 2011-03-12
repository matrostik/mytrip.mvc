using System.Linq;
using mtm.Store.Repository.DataEntities;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using mtm.Core.Repository;
using System.Web.Routing;
using System.Web;
using mtm.Core;

namespace mtm.Store.Models
{
    #region Модель для страницы /Views/Store/Index.cshtml
    // **********************************************
    // Модель для страницы /Views/Store/Index.cshtml
    // **********************************************

    /// <summary>Модель для страницы /Views/Store/Index.cshtml 
    /// </summary>
    public class DepartmentModel
    {
        /// <summary>
        /// 
        /// </summary>
        public IQueryable<mytrip_storedepartment> Department { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int take { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool paging { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool paging2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TitleDepartmentModel titleDepartmentModel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IQueryable<mytrip_storeproduct> Product { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IQueryable<mytrip_storeproducer> Producer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int takepaging { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string smallprice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bigprice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SelectList SelectDepartment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SelectList SelectProducer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProducerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool DepartmentAndProducer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool DepartmentAndProducer2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string[] Bread { get; set; }
        public string seotitle { get; set; }
        public string seokeywords { get; set; }
        public string seodescription { get; set; }


    }

    //****************** E N D **********************
    #endregion

    #region Модель Титла для страницы /Views/Store/Index.cshtml
    // **********************************************
    // Модель Титла для страницы /Views/Store/Index.cshtml
    // **********************************************

    /// <summary>Модель Титла для страницы /Views/Store/Index.cshtml
    /// </summary>
   public class TitleDepartmentModel
    {
        /// <summary>
        /// 
        /// </summary>
        public bool producer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string body { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string img { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string subDepartmentTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string subDepartmentPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int subDepartmentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int subcount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool _search { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string search { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int totalsearch { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProducerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProducerTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProducerBody { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProducerImg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProducerPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int departmentcount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int producercount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int createdepartment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SubUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int produceridforeditor { get; set; }

    }

    //****************** E N D **********************
    #endregion

    #region Модель для страницы /Views/Store/View.cshtml
    // **********************************************
    // Модель для страницы /Views/Store/View.cshtml
    // **********************************************

    /// <summary>Модель для страницы /Views/Store/View.cshtml
    /// </summary>
    public class ProductModel
    {
        /// <summary>
        /// 
        /// </summary>
        public IQueryable<mytrip_storeproduct> Product { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public mytrip_storeproduct ViewProduct { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool comparison { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool comparison2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string review { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public HtmlString review2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reviewTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string[] Bread { get; set; }
        public string seotitle { get; set; }
        public string seokeywords { get; set; }
        public string seodescription { get; set; }
    }

    //****************** E N D **********************
    #endregion

    #region Модель для страницы /Views/Store/Cart.cshtml
    // **********************************************
    // Модель для страницы /Views/Store/Cart.cshtml
    // **********************************************

    /// <summary>Модель для страницы /Views/Store/View.cshtml
    /// </summary>
    public class CartModel
    {
        /// <summary>
        /// 
        /// </summary>
        public HtmlString cart { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// 
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string firstname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lastname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_email")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_max_length_email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "register_error_email")]
        [DataType(DataType.EmailAddress)]
        public string useremail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string valid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string organization{ get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string organizationINN{ get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string organizationKPP { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string viewOrganization { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string viewOrganizationRu { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool organizationOrPrivte { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string[] Bread { get; set; }
    }

    //****************** E N D **********************
    #endregion

    #region Модель для страницы /Views/Store/EditorCategory.cshtml
    // **********************************************
    // Модель для страницы /Views/Store/EditorCategory.cshtml
    // **********************************************

    /// <summary>Модель для страницы /Views/Store/EditorCategory.cshtml
    /// </summary>
    public class EditorCategoryModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string PageTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AllowHtml]
        public string Body { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public HtmlString Image { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool AllCulture { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LabelForTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LabelForImage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LabelForBody { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Submit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SelectList SelectSale { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Sale { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Param { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string[] Bread { get; set; }
        public string seotitle { get; set; }
        public string seokeywords { get; set; }
        public string seodescription { get; set; }
        public string path { get; set; }
    }

    //****************** E N D **********************
    #endregion

    #region Модель для страницы /Views/Store/EditorProduct.cshtml
    // **********************************************
    // Модель для страницы /Views/Store/EditorProduct.cshtml
    // **********************************************

    /// <summary>Модель для страницы /Views/Store/EditorProduct.cshtml
    /// </summary>
    public class EditorProductModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string PageTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Param { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Submit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public int DepartmentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public int ProducerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        [AllowHtml]
        public string Abstracts { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [AllowHtml]
        public string Body { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public HtmlString Image { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public HtmlString ImageOption { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool AllCulture { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string Price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public int TotalCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UrlFile { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ViewCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ViewPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ViewVotes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SelectList SelectDepartment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SelectList SelectProducer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NameMoney { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Packing { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SelectList SelectCultureMoney { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CultureMoney { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NumberCatalog { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SelectList SelectSale { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Sale { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string[] Bread { get; set; }
        public string seotitle { get; set; }
        public string seokeywords { get; set; }
        public string seodescription { get; set; }
        public string path { get; set; }
    }

    //****************** E N D **********************
    #endregion

    #region Модель для страницы /Views/Store/CreateSale.cshtml
    // **********************************************
    // Модель для страницы /Views/Store/CreateSale.cshtml
    // **********************************************

    /// <summary>Модель для страницы /Views/Store/CreateSale.cshtml 
    /// </summary>
    public class CreateSaleModel
    {
        public int sale { get; set; } 
        public string datestart { get; set; }
        public string dateclose { get; set; }
        public string[] Bread { get; set; }
    }
    public class CreateProductXmlModel
    {
        public bool delete { get; set; }
        public int sale { get; set; }
        public int department { get; set; }
        public int producer { get; set; }
        public int product { get; set; }
        public string[] Bread { get; set; }
    }
    public class SettingStorelModel
    {
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string roleStoreManager { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string roleChiefStoreManager { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public int styleProduct { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public int widthImgProduct { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public int columnProduct { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public int styleDepartment { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public int widthImgDepartment { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public int columnDepartment { get; set; }
        public bool onlineBuy { get; set; }
        public bool unlockStore { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public int MoneyProcent { get; set; }
        public bool organizationBuy { get; set; }
        public bool viewProduktTable { get; set; }
        
        public string[] Bread { get; set; }

        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string partialAccordion { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string partialNoAccordion { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string partialMenuLogon { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string partialMenuLogonWrap { get; set; }


        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string NameSearchPage_ru_ru { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string NameSearchPage_en_us { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameProducer_ru_ru { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameProducer_en_us { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameStore_ru_ru { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameStore_en_us { get; set; }

        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameProducerTitle_ru_ru { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameProducerTitle_en_us { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameStoreTitle_ru_ru { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameStoreTitle_en_us { get; set; }

        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameProducerKeywords_ru_ru { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameProducerKeywords_en_us { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameStoreKeywords_ru_ru { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameStoreKeywords_en_us { get; set; }

        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameProducerDescription_ru_ru { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameProducerDescription_en_us { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameStoreDescription_ru_ru { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameStoreDescription_en_us { get; set; }

        public string view_ru_ru { get; set; }
        public string view_en_us { get; set; }
    }
    //****************** E N D **********************
    #endregion

    #region Модель для страницы /Views/Store/OrderDetails.cshtml
    // **********************************************
    // Модель для страницы /Views/Store/OrderDetails.cshtml
    // **********************************************

    /// <summary>Модель для страницы /Views/Store/OrderDetails.cshtml
    /// </summary>
    public class OrderDetailsModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public HtmlString orderisproduct { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string firstname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lastname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_email")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_max_length_email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "register_error_email")]
        [DataType(DataType.EmailAddress)]
        public string useremail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string organization { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string organizationINN { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string organizationKPP { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string viewOrganization { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string viewOrganizationRu { get; set; }
        public string delivery { get; set; }
        public string moneyId { get; set; }
        public SelectList SelectCultureMoney { get; set; }
        public HtmlString total { get; set; }
        public string priceInWords { get; set; }
        public int? namberaccount { get; set; }
        //SELLER
        public string selleraccountant { get; set; }
        public string selleraddress { get; set; }
        public string sellerbank { get; set; }
        public string sellerbankaccount { get; set; }
        public string sellerbankaccountBIK { get; set; }
        public string sellerbankaccountSeller { get; set; }
        public string sellerdirector { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_null_email")]
        [StringLength(50, ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "account_max_length_email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "register_error_email")]
        [DataType(DataType.EmailAddress)]
        public string selleremail { get; set; }
        public bool sellerliteNDS { get; set; }
        public string sellerorganization { get; set; }
        public string sellerorganizationINN { get; set; }
        public string sellerorganizationKPP { get; set; }
        public string sellerphone { get; set; }
        public string[] Bread { get; set; }
    }

    //****************** E N D **********************
    #endregion

    #region RoleStoreAttribute
    // **********************************************
    // RoleStoreAttribute
    // **********************************************

    /// <summary>
    /// 
    /// </summary>
    public class RoleStoreAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (MytripUser.UserInRole(ModuleSetting.roleChiefStoreManager()) || MytripUser.UserInRole(ModuleSetting.roleStoreManager()))
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
