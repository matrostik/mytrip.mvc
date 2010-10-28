using System.Linq;
using Mytrip.Store.Repository.DataEntities;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Mytrip.Mvc.Repository;
using System.Web.Routing;
using System.Web;
using Mytrip.Mvc;

namespace Mytrip.Store.Models
{
    #region Модель для страницы /Views/Store/Index.cshtml
    // **********************************************
    // Модель для страницы /Views/Store/Index.cshtml
    // **********************************************

    /// <summary>Модель для страницы /Views/Store/Index.cshtml 
    /// </summary>
    [MetadataType(typeof(DepartmentModel))]
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

    }

    //****************** E N D **********************
    #endregion

    #region Модель Титла для страницы /Views/Store/Index.cshtml
    // **********************************************
    // Модель Титла для страницы /Views/Store/Index.cshtml
    // **********************************************

    /// <summary>Модель Титла для страницы /Views/Store/Index.cshtml
    /// </summary>
    [MetadataType(typeof(TitleDepartmentModel))]
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
    [MetadataType(typeof(ProductModel))]
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
        
    }

    //****************** E N D **********************
    #endregion

    #region Модель для страницы /Views/Store/Cart.cshtml
    // **********************************************
    // Модель для страницы /Views/Store/Cart.cshtml
    // **********************************************

    /// <summary>Модель для страницы /Views/Store/View.cshtml
    /// </summary>
    [MetadataType(typeof(CartModel))]
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

    }

    //****************** E N D **********************
    #endregion

    #region Модель для страницы /Views/Store/EditorCategory.cshtml
    // **********************************************
    // Модель для страницы /Views/Store/EditorCategory.cshtml
    // **********************************************

    /// <summary>Модель для страницы /Views/Store/EditorCategory.cshtml
    /// </summary>
    [MetadataType(typeof(EditorCategoryModel))]
    public class EditorCategoryModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string pagetitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string body { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public HtmlString image { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool allculture { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string labelfortitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string labelforimage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string labelforbody { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string submit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SelectList SelectSale { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Sale { get; set; }
    }

    //****************** E N D **********************
    #endregion

    #region Модель для страницы /Views/Store/EditorProduct.cshtml
    // **********************************************
    // Модель для страницы /Views/Store/EditorProduct.cshtml
    // **********************************************

    /// <summary>Модель для страницы /Views/Store/EditorProduct.cshtml
    /// </summary>
    [MetadataType(typeof(EditorProductModel))]
    public class EditorProductModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string pagetitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string submit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public int departmentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public int producerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string abstracts { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string body { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public HtmlString image { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public HtmlString imageOption { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool allculture { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public int totalcount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string urlfile { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool viewcount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool viewprice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool viewvotes { get; set; }

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
        public string nameMoney { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string packing { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SelectList SelectCultureMoney { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cultureMoney { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string namberCatalog { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SelectList SelectSale { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Sale { get; set; }
    }

    //****************** E N D **********************
    #endregion

    #region Модель для страницы /Views/Store/CreateSale.cshtml
    // **********************************************
    // Модель для страницы /Views/Store/CreateSale.cshtml
    // **********************************************

    /// <summary>Модель для страницы /Views/Store/CreateSale.cshtml 
    /// </summary>
    [MetadataType(typeof(CreateSaleModel))]
    public class CreateSaleModel
    {
        public int sale { get; set; } 
        public string datestart { get; set; }
        public string dateclose { get; set; }
    }
    [MetadataType(typeof(CreateProductXmlModel))]
    public class CreateProductXmlModel
    {
        public int departmentid { get; set; }
        public int producerid { get; set; }
        public SelectList SelectDepartment { get; set; }
        public SelectList SelectProducer { get; set; }
    }
    [MetadataType(typeof(SettingStorelModel))]
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
        public string nameProducer { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string nameStore { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public int MoneyProcent { get; set; }
        public bool organizationBuy { get; set; }
        public bool viewProduktTable { get; set; }
        [Required(ErrorMessageResourceType = typeof(CoreLanguage), ErrorMessageResourceName = "core_null")]
        public string NameSearchPage { get; set; }
    }
    //****************** E N D **********************
    #endregion

    #region Модель для страницы /Views/Store/OrderDetails.cshtml
    // **********************************************
    // Модель для страницы /Views/Store/OrderDetails.cshtml
    // **********************************************

    /// <summary>Модель для страницы /Views/Store/OrderDetails.cshtml
    /// </summary>
    [MetadataType(typeof(OrderDetailsModel))]
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
