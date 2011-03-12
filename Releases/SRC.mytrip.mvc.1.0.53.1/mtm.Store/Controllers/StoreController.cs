using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using mtm.Core.Models;
using mtm.Store.Repository;
using mtm.Store.Models;
using mtm.Core;
using mtm.Core.Settings;
using System.Web;
using mtm.Store.Helpers;
using mtm.Core.Repository;
using System.Xml.Linq;
using System.Drawing;

namespace mtm.Store.Controllers
{
    /// <summary>Контроллер магазина
    /// </summary>
    public class StoreController : Controller
    {
        #region Properties
        private IStoreRepository _IStoreRepository;

        /// <summary>Инициализация репозитория магазина
        /// </summary>
        public IStoreRepository store
        {
            get
            {
                if (_IStoreRepository == null)
                    _IStoreRepository = new IStoreRepository();
                return _IStoreRepository;
            }
        }
        #endregion
       
        #region Index,Search,View
        /// <summary>GET: /Store/Index
        /// Представление для отделов, производителей, товаров из отделов
        /// и сортировка по отделу производителю и цене для представления товаров
        /// </summary>
        /// <param name="id">номер страницы</param>
        /// <param name="id2">количество на странице</param>
        /// <param name="id3">номер отдела</param>
        /// <param name="id4">номер производителя</param>
        /// <param name="id5">номер сортировки</param>
        /// <param name="id6">паз или поисковая фраза</param>
        /// <param name="id7">минимальная цена</param>
        /// <param name="id8">максимальная цена</param>
        /// <returns>ActionResult</returns>
        public ActionResult Index(int id, int id2, int id3, int id4, int id5, string id6, int? id7, int? id8)
        {
            #region vars
            int total = 0;
            int _id2 = id2;
            DepartmentModel model = new DepartmentModel();
            string body = string.Empty;
            string img = string.Empty;
            string subDepartmentPath = string.Empty;
            string subDepartmentTitle = string.Empty;
            model.seotitle = ModuleSetting.nameStoreTitle();
            model.seokeywords = ModuleSetting.nameStoreKeyWords();
            model.seodescription = ModuleSetting.nameStoreDesc();
            int subDepartmentId = 0;
            model.paging = false;
            model.paging2 = false;
            int count = -1;
            int subcount = 0;
            string title = string.Empty;
            model.Product = null;
            model.Producer = null;
            bool producer = false;
            model.bigprice = string.Empty;
            model.smallprice = string.Empty;
            model.DepartmentId = id3;
            model.ProducerId = id4;
            model.Search = string.Empty;
            model.DepartmentAndProducer = false;
            model.DepartmentAndProducer2 = false;
            string s_search = string.Empty;
            int s_totalsearch = 0;
            int s_id = 0;
            string s_path = string.Empty;
            int s_ProducerId = 0;
            string s_ProducerTitle = string.Empty;
            string s_ProducerBody = string.Empty;
            string s_ProducerImg = string.Empty;
            string s_ProducerPath = string.Empty;
            int s_departmentcount = 0;
            int s_producercount = 0;
            bool _search = false;
            string user = string.Empty;
            string subuser = string.Empty;
            List<string> bread = new List<string>(new string[] { "<a href='/Store/Index/1/10/0/0/1/Department'>" + ModuleSetting.nameStore() + "</a>" });
            #endregion
            #region Store
            if (id3 == 0 && id4 == 0 && id6 != "Producer" && id8 == null)
            {
                _id2 = id2 * ModuleSetting.columnProduct();
                model.Department = store.department.GetAllDepartment(1, int.MaxValue, LocalisationSetting.culture(), out total);
                model.Product = store.product.GetProductForStore(id, _id2, id5, LocalisationSetting.culture(), out total);
                model.total = total;
                model.take = _id2;
                model.paging2 = true;
                model.takepaging = (int)Math.Ceiling((double)total / ModuleSetting.columnProduct());
                title = ModuleSetting.nameStore();
                bread = new List<string>(new string[] { ModuleSetting.nameStore() });
            }
            #endregion
            #region Department & SubDepartment
            else if (id3 > 0 && id4 == 0 && id8 == null)
            {
                count = 0;
                model.Department = store.department.GetSubDepartment(id3, LocalisationSetting.culture());
                model.take = model.Department.Count();
                if (model.take > 0)
                {
                    foreach (var x in model.Department)
                    {
                        count += x.mytrip_storeproduct.Where(q => q.Culture == LocalisationSetting.culture() || q.AllCulture == true).Count();
                    }
                }
                var department = store.department.GetDepartment(id3);
                model.seotitle = department.SeoTitle;
                model.seokeywords = department.SeoKeyword;
                model.seodescription = department.SeoDescription;
                user = department.UserName;
                subuser = department.mytrip_storedepartment2.UserName;
                count += department.mytrip_storeproduct.Where(q => q.Culture == LocalisationSetting.culture() || q.AllCulture == true).Count();
                title = department.Title;
                body = department.Body;
                int _id = department.mytrip_storedepartment2.DepartmentId;
                if (_id > 0)
                {
                    var subdepartment = store.department.GetDepartment(_id);
                    subDepartmentPath = subdepartment.Path;
                    subDepartmentTitle = subdepartment.Title;
                    subDepartmentId = _id;
                    subcount = subdepartment.mytrip_storeproduct.Count();
                    foreach (var x in subdepartment.mytrip_storedepartment1)
                    {
                        subcount += x.mytrip_storeproduct.Count();
                    }
                    bread.Add("<a href='/Store/Index/1/10/" + subdepartment.DepartmentId + "/0/1/" + subdepartment.Path + "'>" + subdepartment.Title + "</a>");
                    bread.Add(department.Title);
                }
                else
                    bread.Add(department.Title);

                _id2 = _id2 * ModuleSetting.columnProduct();
                model.Product = store.product.GetProductForDepartment(id3, id, _id2, id5, LocalisationSetting.culture(), out total);
                model.total = total;
                model.take = _id2;
                model.paging2 = true;
                model.takepaging = (int)Math.Ceiling((double)total / ModuleSetting.columnProduct());
            }
            #endregion
            #region Producers
            else if (id3 == 0 && id4 == 0 && id6 == "Producer" && id8 == null)
            {
                id2 = id2 * ModuleSetting.columnDepartment();
                model.Producer = store.producer.GetAllProducer(id, id2, LocalisationSetting.culture(), out total);
                model.total = total;
                model.take = id2;
                if (id2 < total)
                    model.paging = true;
                producer = true;
                model.takepaging = (int)Math.Ceiling((double)total / ModuleSetting.columnDepartment());
                title = ModuleSetting.nameProducer();
                bread.Add(ModuleSetting.nameProducer());
                model.seotitle = ModuleSetting.nameProducerTitle();
                model.seokeywords = ModuleSetting.nameProducerKeyWords();
                model.seodescription = ModuleSetting.nameProducerDesc();
            }
            #endregion
            #region Producer
            else if (id3 == 0 && id4 > 0 && id8 == null)
            {
                count = 0;
                var department = store.producer.GetProducer(id4);
                model.seotitle = department.SeoTitle;
                model.seokeywords = department.SeoKeyword;
                model.seodescription = department.SeoDescription;
                user = department.UserName;
                count += department.mytrip_storeproduct.Where(q => q.Culture == LocalisationSetting.culture() || q.AllCulture == true).Count();
                title = department.Title;
                body = department.Body;
                _id2 = _id2 * ModuleSetting.columnProduct();
                model.Product = store.product.GetProductForProducer(id4, id, _id2, id5, LocalisationSetting.culture(), out total);
                model.total = total;
                model.take = _id2;
                producer = true;
                model.paging2 = true;
                model.takepaging = (int)Math.Ceiling((double)total / ModuleSetting.columnProduct());
                bread.Add("<a href='/Store/Index/1/10/0/0/1/Producer'>" + ModuleSetting.nameProducer() + "</a>");
                bread.Add(department.Title);

            }
            #endregion
            #region Search
            else if (id8 >= 0)
            {
                _search = true;
                if (id7 > 0)
                    model.smallprice = id7.ToString();
                if (id8 > id7)
                    model.bigprice = id8.ToString();
                if (id6 != "x")
                {
                    id6 = GeneralMethods.UndecodingSearch(id6);
                    model.Search = id6;
                    s_search = id6;
                }
                _id2 = _id2 * ModuleSetting.columnProduct();
                #region Producer
                if (id3 == 0 && id4 > 0 && id7 >= 0 && id8 >= 0)
                {
                    producer = true;
                    count = 0;
                    var department = store.producer.GetProducer(id4);
                    count += department.mytrip_storeproduct.Where(q => q.Culture == LocalisationSetting.culture() || q.AllCulture == true).Count();
                    title = department.Title;
                    body = department.Body;
                    var searchproduct = store.product.GetProductForProducer(id4, id, _id2, id5, (int)id7, (int)id8, LocalisationSetting.culture(), id6, out total);
                    if (id6 != "x")
                    {
                        foreach (var art in searchproduct)
                        {
                            art.NamberCatalog = GeneralMethods.ReplaceString(art.NamberCatalog, id6);
                            art.Title = GeneralMethods.ReplaceString(art.Title, id6);
                            art.Body = GeneralMethods.ReplaceString(art.Body, id6);
                        }
                    }
                    model.Product = searchproduct;
                }
                #endregion
                #region Department
                else if (id3 > 0 && id4 == 0 && id7 >= 0 && id8 >= 0)
                {
                    count = 0;
                    model.Department = store.department.GetSubDepartment(id3, LocalisationSetting.culture());
                    model.take = model.Department.Count();
                    if (model.take > 0)
                    {
                        foreach (var x in model.Department)
                        {
                            count += x.mytrip_storeproduct.Where(q => q.Culture == LocalisationSetting.culture() || q.AllCulture == true).Count();
                        }
                    }
                    var department = store.department.GetDepartment(id3);
                    user = department.UserName;
                    subuser = department.mytrip_storedepartment2.UserName;
                    count += department.mytrip_storeproduct.Where(q => q.Culture == LocalisationSetting.culture() || q.AllCulture == true).Count();
                    title = department.Title;
                    body = department.Body;
                    int _id = department.mytrip_storedepartment2.DepartmentId;
                    if (_id > 0)
                    {
                        var subdepartment = store.department.GetDepartment(_id);
                        subDepartmentPath = subdepartment.Path;
                        subDepartmentTitle = subdepartment.Title;
                        subDepartmentId = _id;
                        subcount = subdepartment.mytrip_storeproduct.Count();
                        foreach (var x in subdepartment.mytrip_storedepartment1)
                        {
                            subcount += x.mytrip_storeproduct.Count();
                        }
                    }
                    var searchproduct = store.product.GetProductForDepartment(id3, id, _id2, id5, (int)id7, (int)id8, LocalisationSetting.culture(), id6, out total);
                    if (id6 != "x")
                    {
                        foreach (var art in searchproduct)
                        {
                            art.NamberCatalog = GeneralMethods.ReplaceString(art.NamberCatalog, id6);
                            art.Title = GeneralMethods.ReplaceString(art.Title, id6);
                            art.Body = GeneralMethods.ReplaceString(art.Body, id6);
                        }
                    }
                    model.Product = searchproduct;
                }
                #endregion
                #region Department & producer
                else if (id3 > 0 && id4 > 0 && id7 >= 0 && id8 >= 0)
                {
                    model.DepartmentAndProducer = true;
                    #region producer
                    var searchproducer = store.producer.GetProducer(id4);
                    s_producercount = searchproducer.mytrip_storeproduct.Count();
                    s_ProducerId = id4;
                    s_ProducerPath = searchproducer.Path;
                    s_ProducerTitle = searchproducer.Title;
                    s_ProducerBody = searchproducer.Body;
                    #endregion
                    model.Department = store.department.GetSubDepartment(id3, LocalisationSetting.culture());
                    model.take = model.Department.Count();
                    if (model.take > 0)
                    {
                        foreach (var x in model.Department)
                        {
                            s_departmentcount += x.mytrip_storeproduct.Count();
                        }
                    }
                    var department = store.department.GetDepartment(id3);
                    user = department.UserName;
                    subuser = department.mytrip_storedepartment2.UserName;
                    s_departmentcount += department.mytrip_storeproduct.Count();
                    s_id = id3;
                    s_path = department.Path;
                    title = department.Title;
                    body = department.Body;
                    int _id = department.mytrip_storedepartment2.DepartmentId;
                    if (_id > 0)
                    {
                        var subdepartment = store.department.GetDepartment(_id);
                        subDepartmentPath = subdepartment.Path;
                        subDepartmentTitle = subdepartment.Title;
                        subDepartmentId = _id;
                        subcount = subdepartment.mytrip_storeproduct.Count();
                        foreach (var x in subdepartment.mytrip_storedepartment1)
                        {
                            subcount += x.mytrip_storeproduct.Count();
                        }
                    }
                    var searchproduct = store.product.GetProductForDepartmentAndProducer(id3, id4, id, _id2, id5, (int)id7, (int)id8, LocalisationSetting.culture(), id6, out total);
                    if (id6 != "x")
                    {
                        foreach (var art in searchproduct)
                        {
                            art.NamberCatalog = GeneralMethods.ReplaceString(art.NamberCatalog, id6);
                            art.Title = GeneralMethods.ReplaceString(art.Title, id6);
                            art.Body = GeneralMethods.ReplaceString(art.Body, id6);
                        }
                    }
                    model.Product = searchproduct;
                }
                #endregion
                #region Department & producer
                else if (id3 == 0 && id4 == 0 && id7 >= 0 && id8 >= 0)
                {
                    model.DepartmentAndProducer2 = true;
                    title = StoreLanguage.Search;
                    var searchproduct = store.product.GetProductForDepartmentAndProducer(id, _id2, id5, (int)id7, (int)id8, LocalisationSetting.culture(), id6, out total);
                    if (id6 != "x")
                    {
                        foreach (var art in searchproduct)
                        {
                            art.NamberCatalog = GeneralMethods.ReplaceString(art.NamberCatalog, id6);
                            art.Title = GeneralMethods.ReplaceString(art.Title, id6);
                            art.Body = GeneralMethods.ReplaceString(art.Body, id6);
                        }
                    }
                    model.Product = searchproduct;
                }
                #endregion
                model.total = total;
                s_totalsearch = total;
                model.take = _id2;
                model.paging2 = true;
                model.takepaging = (int)Math.Ceiling((double)total / ModuleSetting.columnProduct());
                bread.Add(title);
            }
            #endregion
            model.SelectDepartment = new SelectList(store.department.GetDepartmentForDdl(LocalisationSetting.culture(), true), "Key", "Value", model.DepartmentId);
            model.SelectProducer = new SelectList(store.producer.GetProducerForDdl(LocalisationSetting.culture(), true), "Key", "Value", model.ProducerId);
            model.titleDepartmentModel = new TitleDepartmentModel
            {
                producer = producer,
                body = body,
                img = img,
                subDepartmentPath = subDepartmentPath,
                subDepartmentTitle = subDepartmentTitle,
                subDepartmentId = subDepartmentId,
                count = count,
                subcount = subcount,
                title = title,
                search = s_search,
                totalsearch = s_totalsearch,
                id = s_id,
                path = s_path,
                ProducerId = s_ProducerId,
                ProducerTitle = s_ProducerTitle,
                ProducerBody = s_ProducerBody,
                ProducerImg = s_ProducerImg,
                ProducerPath = s_ProducerPath,
                _search = _search,
                departmentcount = s_departmentcount,
                producercount = s_producercount,
                createdepartment = id3,
                User = user,
                SubUser = subuser,
                produceridforeditor = id4
            };
            model.Bread = bread.ToArray();
            return View(model);
        }
        /// <summary>POST: /Store/Index
        /// Поиск с учетом выбранной сортировки
        /// </summary>
        /// <param name="model">DepartmentModel</param>
        /// <param name="id5">номер сортировки</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Index(DepartmentModel model, int id5)
        {
            string Search = string.Empty;
            if (model.Search != null)
                Search = GeneralMethods.DecodingSearch(model.Search);
            else
                Search = "x";
            int _Department = model.DepartmentId;
            int _Producer = model.ProducerId;
            int _SmallPrice = 0;
            int _BigPraice = 0;
            int.TryParse(model.smallprice, out _SmallPrice);
            int.TryParse(model.bigprice, out _BigPraice);
            return RedirectToAction("Index", "Store",
                new
                {
                    id = 1,
                    id2 = 10,
                    id3 = _Department,
                    id4 = _Producer,
                    id5 = id5,
                    id6 = Search.Trim(),
                    id7 = _SmallPrice,
                    id8 = _BigPraice
                });
        }
        [HttpPost]
        public ActionResult Search(string search, string url)
        {
            if (search != string.Empty)
            {
                search = GeneralMethods.DecodingSearch(search);
                return RedirectToAction("Index", "Store",
                new
                {
                    id = 1,
                    id2 = 10,
                    id3 = 0,
                    id4 = 0,
                    id5 = 1,
                    id6 = search.Trim(),
                    id7 = 0,
                    id8 = 0
                });
            }
            else
                return Redirect(url);
        }
        /// <summary>GET: /Store/View
        /// Отображение продукта или продуктов выбранных для сравнения
        /// </summary>
        /// <param name="id">индентификатор продукта</param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public ActionResult View(int id, string id2)
        {
            ProductModel model = new ProductModel();
            if (id2 != null && id2 == "review")
                model.review2 = new HtmlString("<div id='review2'></div>");
            model.comparison = true;
            model.comparison2 = false;
            model.ViewProduct = null;
            model.Product = null;
            model.seotitle = ModuleSetting.nameStoreTitle();
            model.seokeywords = ModuleSetting.nameStoreKeyWords();
            model.seodescription = ModuleSetting.nameStoreDesc();
            if (id == 0 && (string.IsNullOrEmpty(StoreCookies.Comparison) || !StoreCookies.Comparison.Contains("][")))
            {
                model.comparison = false;
                model.Bread = new string[] { "<a href='/Store/Index/1/10/0/0/1/Department'>" + ModuleSetting.nameStore() + "</a>", StoreLanguage.compare_products };
            }
            if (id == 0 && model.comparison)
            {
                string _comparison = StoreCookies.Comparison.Replace("][", "|").Replace("[", "").Replace("]", "");
                string[] _id = _comparison.Split('|');
                model.Product = store.product.GetProductForViews(_id);
                model.comparison2 = true;
                model.Bread = new string[] { "<a href='/Store/Index/1/10/0/0/1/Department'>" + ModuleSetting.nameStore() + "</a>", StoreLanguage.compare_products };
            }
            if (id > 0)
            {
                model.ViewProduct = store.product.GetProduct(id);
                model.seotitle = model.ViewProduct.SeoTitle;
                model.seokeywords = model.ViewProduct.SeoKeyword;
                model.seodescription = model.ViewProduct.SeoDescription;
                if (store.product.StatusReview(id) != null)
                {
                    model.review = store.product.StatusReview(id);
                    model.reviewTitle = StoreLanguage.review_body_edit;
                }
                else
                    model.reviewTitle = StoreLanguage.review_body;
                if (model.ViewProduct.mytrip_storedepartment.SubDepartmentId == 0)
                    model.Bread = new string[] { "<a href='/Store/Index/1/10/0/0/1/Department'>" + ModuleSetting.nameStore() + "</a>"
                ,"<a href='/Store/Index/1/10/"+model.ViewProduct.DepartmentId+"/0/1/Department'>" + model.ViewProduct.mytrip_storedepartment.Title + "</a>"
                , model.ViewProduct.Title };
                else
                {
                    model.Bread = new string[] { "<a href='/Store/Index/1/10/0/0/1/Department'>" + ModuleSetting.nameStore() + "</a>"
                 ,"<a href='/Store/Index/1/10/"+model.ViewProduct.mytrip_storedepartment.SubDepartmentId+"/0/1/Department'>" + model.ViewProduct.mytrip_storedepartment.mytrip_storedepartment2.Title + "</a>"
                ,"<a href='/Store/Index/1/10/"+model.ViewProduct.DepartmentId+"/0/1/Department'>" + model.ViewProduct.mytrip_storedepartment.Title + "</a>", model.ViewProduct.Title };
                }
            }
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult View(int id, ProductModel model)
        {
            if (ModelState.IsValid)
            {
                store.product.CreateReview(id, model.review);
                return RedirectToAction("View", "Store", new { id, id2 = "review" });
            }
            model.comparison = true;
            model.comparison2 = false;
            model.ViewProduct = null;
            model.Product = null;
            model.seotitle = ModuleSetting.nameStoreTitle();
            model.seokeywords = ModuleSetting.nameStoreKeyWords();
            model.seodescription = ModuleSetting.nameStoreDesc();
            model.review2 = new HtmlString("<div id='errorReview'></div>");
            if (id == 0 && (string.IsNullOrEmpty(StoreCookies.Comparison) || !StoreCookies.Comparison.Contains("][")))
            {
                model.comparison = false;
                model.Bread = new string[] { "<a href='/Store/Index/1/10/0/0/1/Department'>" + ModuleSetting.nameStore() + "</a>", StoreLanguage.compare_products };
            }
            if (id == 0 && model.comparison)
            {
                string _comparison = StoreCookies.Comparison.Replace("][", "|").Replace("[", "").Replace("]", "");
                string[] _id = _comparison.Split('|');
                model.Product = store.product.GetProductForViews(_id);
                model.comparison2 = true;
                model.Bread = new string[] { "<a href='/Store/Index/1/10/0/0/1/Department'>" + ModuleSetting.nameStore() + "</a>", StoreLanguage.compare_products };
            }
            if (id > 0)
            {
                model.ViewProduct = store.product.GetProduct(id);
                model.seotitle = model.ViewProduct.SeoTitle;
                model.seokeywords = model.ViewProduct.SeoKeyword;
                model.seodescription = model.ViewProduct.SeoDescription;
                if (store.product.StatusReview(id) != null)
                {
                    model.reviewTitle = StoreLanguage.review_body_edit;
                }
                else
                    model.reviewTitle = StoreLanguage.review_body;
                if (model.ViewProduct.mytrip_storedepartment.SubDepartmentId == 0)
                    model.Bread = new string[] { "<a href='/Store/Index/1/10/0/0/1/Department'>" + ModuleSetting.nameStore() + "</a>"
                ,"<a href='/Store/Index/1/10/"+model.ViewProduct.DepartmentId+"/0/1/Department'>" + model.ViewProduct.mytrip_storedepartment.Title + "</a>"
                , model.ViewProduct.Title };
                else
                {
                    model.Bread = new string[] { "<a href='/Store/Index/1/10/0/0/1/Department'>" + ModuleSetting.nameStore() + "</a>"
                 ,"<a href='/Store/Index/1/10/"+model.ViewProduct.mytrip_storedepartment.SubDepartmentId+"/0/1/Department'>" + model.ViewProduct.mytrip_storedepartment.mytrip_storedepartment2.Title + "</a>"
                ,"<a href='/Store/Index/1/10/"+model.ViewProduct.DepartmentId+"/0/1/Department'>" + model.ViewProduct.mytrip_storedepartment.Title + "</a>", model.ViewProduct.Title };
                }
            }
            return View(model);
        }
        /// <summary>
        /// Add product for comparison
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        public void ProductForComparison(string id)
        {
            if (Request.IsAjaxRequest())
            {
                string _id = StoreCookies.Comparison.Contains(string.Format("[{0}]", id))
                    ? StoreCookies.Comparison.Replace(string.Format("[{0}]", id), "")
                    : string.Format("{0}[{1}]", StoreCookies.Comparison, id);
                StoreCookies.Comparison = _id;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vote"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public string Rate(int id, int vote, int count)
        {
            double total = (double)store.product.CreateVote(id, vote);
            int newcount = store.product.GetVotesCount(id);
            StringBuilder result = new StringBuilder();
            result.AppendLine(GeneralMethods.CoreRating(true, false, total, newcount));
            if (count == newcount)
                result.AppendLine("<br/>" + StoreLanguage.you_have_a_voted);
            else
                result.AppendLine("<br/>" + StoreLanguage.thanks_for_vote);
            return result.ToString();
        }
        #endregion

        #region Create/Edit/Delete Producer/Product/Category
        /// <summary>GET: /Store/CreateDepartment
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [RoleStore]
        public ActionResult EditorCategory(int id, string id2)
        {
            EditorCategoryModel model = new EditorCategoryModel();
            model.Param = id + "_" + id2;
            model.LabelForBody = StoreLanguage.body;
            model.Sale = 0;
            List<string> bread = new List<string>(new string[] { "<a href='/Store/Index/1/10/0/0/1/Department'>" + ModuleSetting.nameStore() + "</a>" });

            if (id2 == "CreateDepartment")
            {

                model.Image = store.file.GetFile(id, id2);
                model.LabelForTitle = StoreLanguage.titleDepartment;
                model.LabelForImage = StoreLanguage.imageDepartment;
                model.Submit = StoreLanguage.create;
                
                model.seokeywords = ModuleSetting.nameStoreKeyWords();
                model.seodescription = ModuleSetting.nameStoreDesc();
                model.PageTitle = (id == 0)
                    ? StoreLanguage.createDepartment
                    : string.Format(StoreLanguage.createSubDepartment, store.department.GetDepartment(id).Title);
            }
            else if (id2 == "CreateProducer")
            {
                model.Image = store.file.GetFile(id, id2);
                model.LabelForTitle = StoreLanguage.titleProducer;
                model.LabelForImage = StoreLanguage.imageProducer;
                model.Submit = StoreLanguage.create;
                model.PageTitle = StoreLanguage.createProducer;
                
                model.seokeywords = ModuleSetting.nameProducerKeyWords();
                model.seodescription = ModuleSetting.nameProducerDesc();
            }
            else if (id2 == "EditDepartment")
            {
                model.Image = store.file.GetFile(id, id2);
                var department = store.department.GetDepartment(id);
                model.LabelForTitle = StoreLanguage.titleDepartment;
                model.LabelForImage = StoreLanguage.imageDepartment;
                model.Submit = StoreLanguage.edit;
                model.Title = department.Title;
                model.Body = department.Body;
                model.AllCulture = department.AllCulture;
                model.Sale = department.SaleId;
                model.seotitle = department.SeoTitle;
                model.seokeywords = department.SeoKeyword;
                model.seodescription = department.SeoDescription;
                model.path = department.Path;
                model.PageTitle = (department.SubDepartmentId == 0)
                    ? string.Format(StoreLanguage.editDepartment, model.Title)
                    : string.Format(StoreLanguage.editSubdepartment, model.Title, department.mytrip_storedepartment2.Title);

            }
            else if (id2 == "EditProducer")
            {
                model.Image = store.file.GetFile(id, id2);
                var department = store.producer.GetProducer(id);
                model.LabelForTitle = StoreLanguage.titleProducer;
                model.LabelForImage = StoreLanguage.imageProducer;
                model.Submit = StoreLanguage.edit;
                model.Title = department.Title;
                model.Body = department.Body;
                model.AllCulture = department.AllCulture;
                model.Sale = department.SaleId;
                model.seotitle = department.SeoTitle;
                model.seokeywords = department.SeoKeyword;
                model.seodescription = department.SeoDescription;
                model.path = department.Path;
                model.PageTitle = string.Format(StoreLanguage.editProducer, model.Title);

            }
            else if (id2 == "DeleteDepartment")
            {
                store.file.DeleteFile(id, "/Content/Store/Department");
                store.department.DeleteDepartment(id);
                return RedirectToAction("Index", "Store",
                       new
                       {
                           id = 1,
                           id2 = 10,
                           id3 = 0,
                           id4 = 0,
                           id5 = 1,
                           id6 = "Store"
                       });
            }
            else if (id2 == "DeleteProducer")
            {
                store.file.DeleteFile(id, "/Content/Store/Producer");
                store.producer.DeleteProducer(id);
                return RedirectToAction("Index", "Store",
                       new
                       {
                           id = 1,
                           id2 = 10,
                           id3 = 0,
                           id4 = 0,
                           id5 = 1,
                           id6 = "Producer"
                       });
            }
            //model.Bread = bread.ToArray();
            model.Bread = new string[] { "<a href='/Store/Index/1/10/0/0/1/Department'>" + ModuleSetting.nameStore() + "</a>", model.PageTitle };
            model.SelectSale = new SelectList(store.sale.SaleDictionary(), "Key", "Value", model.Sale);
            return View(model);
        }
        /// <summary>POST: /Store/CreateDepartment
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="id2"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [RoleStore]
        public ActionResult EditorCategory(int id, string id2, EditorCategoryModel model)
        {
            if (ModelState.IsValid && id2 == "CreateDepartment")
            {
                var x = store.department.CreateDepartment(id, model.Title, model.Body, model.AllCulture,
                    LocalisationSetting.culture(), model.Sale,model.path,model.seokeywords,model.seodescription,model.seotitle);
                store.file.RenameFile(x.DepartmentId, "/Content/Store/Department");
                return RedirectToAction("Index", "Store",
                   new
                   {
                       id = 1,
                       id2 = 10,
                       id3 = x.DepartmentId,
                       id4 = 0,
                       id5 = 1,
                       id6 = x.Path
                   });
            }
            else if (ModelState.IsValid && id2 == "EditDepartment")
            {
                var x = store.department.EditDepartment(id, model.Title, model.Body, model.AllCulture,
                    model.Sale,model.path,model.seokeywords,model.seodescription,model.seotitle);
                store.file.RenameFile(x.DepartmentId, "/Content/Store/Department");
                return RedirectToAction("Index", "Store",
                   new
                   {
                       id = 1,
                       id2 = 10,
                       id3 = x.DepartmentId,
                       id4 = 0,
                       id5 = 1,
                       id6 = x.Path
                   });
            }
            else if (ModelState.IsValid && id2 == "CreateProducer")
            {
                var x = store.producer.CreateProducer(model.Title, model.Body, model.AllCulture, LocalisationSetting.culture(), model.Sale,
                    model.path,model.seokeywords,model.seodescription,model.seotitle);
                store.file.RenameFile(x.ProducerId, "/Content/Store/Producer");
                return RedirectToAction("Index", "Store",
                   new
                   {
                       id = 1,
                       id2 = 10,
                       id3 = 0,
                       id4 = x.ProducerId,
                       id5 = 1,
                       id6 = x.Path
                   });
            }
            else if (ModelState.IsValid && id2 == "EditProducer")
            {
                var x = store.producer.EditProducer(id, model.Title, model.Body, model.AllCulture, model.Sale,
                    model.path, model.seokeywords, model.seodescription, model.seotitle);
                store.file.RenameFile(x.ProducerId, "/Content/Store/Producer");
                return RedirectToAction("Index", "Store",
                   new
                   {
                       id = 1,
                       id2 = 10,
                       id3 = 0,
                       id4 = x.ProducerId,
                       id5 = 1,
                       id6 = x.Path
                   });
            }
            model.Bread = new string[] { "<a href='/Store/Index/1/10/0/0/1/Department'>" + ModuleSetting.nameStore() + "</a>", model.PageTitle };
            return View(model);
        }
        /// <summary>GET: /Store/EditorProduct
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        /// <returns></returns>
        [RoleStore]
        public ActionResult EditorProduct(int id, int id2, string id3)
        {
            EditorProductModel model = new EditorProductModel();
            model.Param = id + "_" + id3;
            model.NameMoney = string.Format(" ({0})", ModuleSetting.nameMoney());
            string[] a = store.file.GetFiles(id + "_" + id3);
            StringBuilder result = new StringBuilder();
            foreach (var x in a)
            {
                if (x.Contains("/"))
                    result.Append("<span class='forimg'><img src='" + x + "' class='catImg' style='width:" + ModuleSetting.widthImgDepartment() + "px;'/>"
                      + GeneralMethods.ImgInput("/images/delete.png", "/Store/DeleteFile2/" + x.Replace("/", "()") + "/" + model.Param, "deleteImg2", 14) + "</span>");
            }
            model.ImageOption = new HtmlString(result.ToString());
            model.Image = store.file.GetFile(id, id3);
            model.Sale = 0;
            if (id3 == "CreateProduct")
            {
                model.seokeywords = ModuleSetting.nameStoreKeyWords();
                model.seodescription = ModuleSetting.nameStoreDesc();
                model.PageTitle = StoreLanguage.createProduct;
                model.Submit = StoreLanguage.add;
                model.ProducerId = id2;
                model.DepartmentId = id;
                model.CultureMoney = LocalisationSetting.culture().ToLower();

            }
            else if (id3 == "EditProduct")
            {
                var product = store.product.GetProduct(id);
                model.seotitle = product.SeoTitle;
                model.seokeywords = product.SeoKeyword;
                model.seodescription = product.SeoDescription;
                model.path = product.Path;
                model.Title = product.Title;
                model.PageTitle = string.Format(StoreLanguage.editProduct, model.Title);
                model.Submit = StoreLanguage.edit;
                model.Abstracts = product.Body;
                model.AllCulture = product.AllCulture;
                model.Body = product.Details;
                model.DepartmentId = product.DepartmentId;
                model.Price = product.Price.ToString();
                model.ProducerId = product.ProducerId;
                model.TotalCount = product.TotalCount;
                model.UrlFile = product.UrlFile;
                model.ViewCount = product.ViewCount;
                model.ViewPrice = product.ViewPrice;
                model.ViewVotes = product.ViewVotes;
                model.CultureMoney = product.MoneyId;
                model.NumberCatalog = product.NamberCatalog;
                model.Sale = product.SaleId;
            }
            else if (id3 == "DeleteProduct")
            {
                string path = "";
                model.DepartmentId = store.product.DeleteProduct(id, out path);
                store.file.DeleteFolder(id);
                return RedirectToAction("Index", "Store",
                       new
                       {
                           id = 1,
                           id2 = 10,
                           id3 = model.DepartmentId,
                           id4 = 0,
                           id5 = 1,
                           id6 = path
                       });
            }
            model.SelectSale = new SelectList(store.sale.SaleDictionary(), "Key", "Value", model.Sale);
            model.SelectCultureMoney = new SelectList(MoneyHelpers.CultureMoney(), "Key", "Value", model.CultureMoney);
            if (id > 0)
                model.SelectDepartment = new SelectList(store.department.GetDepartmentForDdl(LocalisationSetting.culture(), false), "Key", "Value", model.DepartmentId);
            else
                model.SelectDepartment = new SelectList(store.department.GetDepartmentForDdl(LocalisationSetting.culture(), false), "Key", "Value");
            if (id2 > 0)
                model.SelectProducer = new SelectList(store.producer.GetProducerForDdl(LocalisationSetting.culture(), false), "Key", "Value", model.ProducerId);
            else
                model.SelectProducer = new SelectList(store.producer.GetProducerForDdl(LocalisationSetting.culture(), false), "Key", "Value");
            model.Bread = new string[] { "<a href='/Store/Index/1/10/0/0/1/Department'>" + ModuleSetting.nameStore() + "</a>", model.PageTitle };
            return View(model);
        }
        /// <summary>POST: /Store/EditorProduct
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [RoleStore]
        public ActionResult EditorProduct(int id, int id2, string id3, EditorProductModel model)
        {
            if (ModelState.IsValid && id3 == "CreateProduct")
            {
                decimal pr = 0;
                if (LocalisationSetting.culture().ToLower() == "ru-ru")
                    decimal.TryParse(model.Price.Replace(".", ","), out pr);
                if (LocalisationSetting.culture().ToLower() == "en-us")
                    decimal.TryParse(model.Price.Replace(",", "."), out pr);
                var x = store.product.CreateProduct(model.DepartmentId, model.ProducerId, model.Title,
                model.Abstracts, model.Body, LocalisationSetting.culture(), model.AllCulture, pr, model.TotalCount,
                model.UrlFile, model.ViewCount, model.ViewPrice, model.ViewVotes, model.Packing, model.CultureMoney,
                model.NumberCatalog, model.Sale, model.path,model.seokeywords,model.seodescription,model.seotitle);
                store.file.RenameFolder(x.ProductId);
                return RedirectToAction("View", "Store", new { id = x.ProductId });
            }
            else if (ModelState.IsValid && id3 == "EditProduct")
            {
                decimal pr = 0;
                if (LocalisationSetting.culture().ToLower() == "ru-ru")
                    decimal.TryParse(model.Price.Replace(".", ","), out pr);
                if (LocalisationSetting.culture().ToLower() == "en-us")
                    decimal.TryParse(model.Price.Replace(",", "."), out pr);
                var x = store.product.EditProduct(id, model.DepartmentId, model.ProducerId, model.Title,
                model.Abstracts, model.Body, model.AllCulture, pr, model.TotalCount,
                model.UrlFile, model.ViewCount, model.ViewPrice, model.ViewVotes, model.Packing, model.CultureMoney,
                model.NumberCatalog, model.Sale, model.path, model.seokeywords, model.seodescription, model.seotitle);
                return RedirectToAction("View", "Store", new { id = x.ProductId });
            }
            string[] a = store.file.GetFiles(id + "_" + id3);
            StringBuilder result = new StringBuilder();
            foreach (var x in a)
            {
                result.Append("<span class='forimg'><img src='" + x + "' class='catImg' style='width:" + ModuleSetting.widthImgDepartment() + "px;'/>"
                  + GeneralMethods.ImgInput("/images/delete.png", "/Store/DeleteFile2/" + x.Replace("/", "()") + "/" + id + "_" + id3, "deleteImg2", 14) + "</span>");
            }
            model.SelectCultureMoney = new SelectList(MoneyHelpers.CultureMoney(), "Key", "Value", model.CultureMoney);
            model.ImageOption = new HtmlString(result.ToString());
            model.Image = store.file.GetFile(id, id3);
            model.SelectDepartment = new SelectList(store.department.GetDepartmentForDdl(LocalisationSetting.culture(), false), "Key", "Value", model.DepartmentId);
            model.SelectProducer = new SelectList(store.producer.GetProducerForDdl(LocalisationSetting.culture(), false), "Key", "Value", model.ProducerId);
            model.Bread = new string[] { "<a href='/Store/Index/1/10/0/0/1/Department'>" + ModuleSetting.nameStore() + "</a>", model.PageTitle };
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase id, string param)
        {
            if (id != null)
            {
                string result = "";
                if (!id.ContentType.Contains("image"))
                {
                    string[] _param = param.Split('_');
                    result = "<div class='field-validation-error'> * " + StoreLanguage.file_not_allowed_img + "</div>" + store.file.GetFile(int.Parse(_param[0]), _param[1]);
                }
                else
                {
                    string link = store.file.UploadFile(param, id);
                    result = "<span class='forimg'><img src='" + link + "?" + id.ContentLength + "' class='catImg' style='width:" + ModuleSetting.widthImgDepartment() + "px;'/>"
                        + GeneralMethods.ImgInput("/images/delete.png", "/Store/DeleteFile/" + link.Replace("/", "()") + "/" + param, "deleteImg", 14) + "</span>";
                }
                return Content(result);
            }
            else
            {
                string[] _param = param.Split('_');
                return Content("<span class='field-validation-error'> * " + CoreLanguage.select_file_upload + "</span>" + store.file.GetFile(int.Parse(_param[0]), _param[1]));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id2"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadFileProductOption(HttpPostedFileBase id2, string param)
        {
            string result = "";
            string[] a;
            if (id2 != null)
            {
                if (!id2.ContentType.Contains("image"))
                {
                    string[] _param = param.Split('_');
                    result = "<div class='field-validation-error'> * " + StoreLanguage.file_not_allowed_img + "</div>";
                    a = store.file.GetFiles(param);
                }
                else
                    a = store.file.UploadFiles(param, id2);
            }
            else
            {
                a = store.file.GetFiles(param);
                result = "<span class='field-validation-error'> * " + CoreLanguage.select_file_upload + "</span>";
            }
            foreach (var x in a)
            {
                result += "<span class='forimg'><img src='" + x + "' class='catImg' style='width:" + ModuleSetting.widthImgDepartment() + "px;'/>"
                  + GeneralMethods.ImgInput("/images/delete.png", "/Store/DeleteFile2/" + x.Replace("/", "()") + "/" + param, "deleteImg2", 14) + "</span>";
            }
            return Content(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteFile(string id)
        {
            id = id.Replace("()", "/");
            store.file.DeleteFile(id);
            return Content("");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteFile2(string id, string id2)
        {
            id = id.Replace("()", "/");
            store.file.DeleteFile(id);
            string[] a = store.file.GetFiles(id2);
            StringBuilder result = new StringBuilder();
            foreach (var x in a)
            {
                if (!string.IsNullOrEmpty(x))
                    result.Append("<span class='forimg'><img src='" + x + "' class='catImg' style='width:" + ModuleSetting.widthImgDepartment() + "px;'/>"
                  + GeneralMethods.ImgInput("/images/delete.png", "/Store/DeleteFile2/" + x.Replace("/", "()") + "/" + id2, "deleteImg2", 14) + "</span>");
            }
            return Content(result.ToString());
        }
        #endregion

        #region Cart
        /// <summary>
        /// Get Cart content
        /// </summary>
        [HttpPost]
        public ActionResult ProductCart()
        {
            if (Request.IsAjaxRequest())
            {
                return Content(CartHelper.MyCart());
            }
            else return null;
        }
        /// <summary>
        /// Add product to Cart
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        public ActionResult ProductForCart(string id)
        {
            if (Request.IsAjaxRequest())
            {
                string _id = StoreCookies.Cart.Contains(string.Format("[{0}]", id))
                    ?StoreCookies.Cart.Replace(string.Format("[{0}]", id), "")
                    : string.Format("{0}[{1}]", StoreCookies.Cart, id);
                StoreCookies.Cart = _id;
                return Content(CartHelper.MyCart());
            }
            else return null;
        }
        /// <summary>
        /// Cart
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Cart(int? id, string message)
        {
            CartModel model = new CartModel();
            model.title = StoreLanguage.mycarttitle;
            model.cart = new HtmlString(CartHelper.ViewMyCart(id));
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                model.firstname = store.managerorder.FirstName(HttpContext.User.Identity.Name).First(x => x.Key == 1).Value;
                model.lastname = store.managerorder.FirstName(HttpContext.User.Identity.Name).First(x => x.Key == 2).Value;
                model.useremail = store.managerorder.FirstName(HttpContext.User.Identity.Name).First(x => x.Key == 3).Value;
                model.phone = store.managerorder.FirstName(HttpContext.User.Identity.Name).First(x => x.Key == 4).Value;
                model.address = store.managerorder.FirstName(HttpContext.User.Identity.Name).First(x => x.Key == 5).Value;
                model.organization = store.managerorder.FirstName(HttpContext.User.Identity.Name).First(x => x.Key == 6).Value;
                model.organizationINN = store.managerorder.FirstName(HttpContext.User.Identity.Name).First(x => x.Key == 7).Value;
                model.organizationKPP = store.managerorder.FirstName(HttpContext.User.Identity.Name).First(x => x.Key == 8).Value;
            }
            if (!ModuleSetting.organizationBuy())
            {
                model.viewOrganization = "none";
            }
            else model.viewOrganization = "show";
            if (LocalisationSetting.culture().ToLower() != "ru-ru")
            {
                model.viewOrganizationRu = "none";
            }
            else model.viewOrganizationRu = "show";
            if (message != null && message.Length > 0)
                model.valid = message;
            else
                model.valid = "no";
            model.Bread = new string[] { "<a href='/Store/Index/1/10/0/0/1/Department'>" + ModuleSetting.nameStore() + "</a>", model.title };
            return View(model);
        }
        [HttpPost]
        public ActionResult Cart(int? id, CartModel model)
        {
            if (ModelState.IsValid)
            {
                if (ModuleSetting.onlineBuy() && !model.organizationOrPrivte)
                {
                    string guidforuser = Guid.NewGuid().ToString();
                    store.order.CreateOrder(LocalisationSetting.culture(), model.address, model.firstname,
                       model.lastname, model.phone, model.useremail, "privatePerson", guidforuser,
                       model.organizationKPP);
                    model.valid = "onlinebuy";
                    StoreCookies.OnlineBuy = guidforuser;
                }
                else if (!ModuleSetting.onlineBuy() || model.organizationOrPrivte)
                {
                    store.order.CreateOrder(LocalisationSetting.culture(), model.address, model.firstname,
                       model.lastname, model.phone, model.useremail, model.organization, model.organizationINN,
                       model.organizationKPP);
                    StoreCookies.Cart = "";
                    model.valid = "message";
                }
                model.cart = new HtmlString(StoreLanguage.buy_ok);
                model.Bread = new string[] { "<a href='/Store/Index/1/10/0/0/1/Department'>" + ModuleSetting.nameStore() + "</a>", model.title };
                return View(model);
            }
            else
            {
                if (!ModuleSetting.organizationBuy())
                {
                    model.viewOrganization = "none";
                }
                else model.viewOrganization = "show";
                if (LocalisationSetting.culture().ToLower() != "ru-ru")
                {
                    model.viewOrganizationRu = "none";
                }
                else model.viewOrganizationRu = "show";
                model.valid = "yes";
                model.cart = new HtmlString(CartHelper.ViewMyCart(id));
                //if (HttpContext.User.Identity.IsAuthenticated)
                //{
                //    model.firstname = store.managerorder.FirstName(HttpContext.User.Identity.Name).First(x=>x.Key==1).Value;
                //    model.lastname = store.managerorder.FirstName(HttpContext.User.Identity.Name).First(x => x.Key == 2).Value;
                //    model.useremail = store.managerorder.FirstName(HttpContext.User.Identity.Name).First(x => x.Key == 3).Value;
                //    model.phone = store.managerorder.FirstName(HttpContext.User.Identity.Name).First(x => x.Key == 4).Value;
                //    model.address = store.managerorder.FirstName(HttpContext.User.Identity.Name).First(x => x.Key == 5).Value;
                //    model.organisation = store.managerorder.FirstName(HttpContext.User.Identity.Name).First(x => x.Key == 6).Value;
                //    model.organisationINN = store.managerorder.FirstName(HttpContext.User.Identity.Name).First(x => x.Key == 7).Value;
                //    model.organisationKPP = store.managerorder.FirstName(HttpContext.User.Identity.Name).First(x => x.Key == 8).Value;
                //}
                model.Bread = new string[] { "<a href='/Store/Index/1/10/0/0/1/Department'>" + ModuleSetting.nameStore() + "</a>", model.title };
                return View(model);
            }
        }
        /// <summary>
        /// Delete product from Cart
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteProductCart(int id)
        {
            string cart = StoreCookies.Cart;
            if (id > 0 && cart.Contains(string.Format("_{0}_", id)))
            {
                cart = cart.Replace("][", "|").Replace("[", "").Replace("]", "");
                string[] _cart = cart.Split('|');
                string __cart = "";
                foreach (string x in _cart)
                {
                    if (!x.Contains(string.Format("_{0}_", id)))
                        __cart += string.Format("[{0}]", x);
                }
                StoreCookies.Cart = __cart;
            }
            return RedirectToAction("Cart");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public ActionResult CountProduct(string id, string id2)
        {
            string cart = StoreCookies.Cart;
            int _id = 0;
            int _id2 = 0;
            int.TryParse(id, out _id);
            int.TryParse(id2, out _id2);
            if (_id > 0 && _id2 > 0 && cart.Contains(string.Format("_{0}_", _id2)))
            {
                cart = cart.Replace("][", "|").Replace("[", "").Replace("]", "");
                string[] _cart = cart.Split('|');
                string __cart = "";
                foreach (string x in _cart)
                {
                    if (x.Contains(string.Format("_{0}_", _id2)))
                        __cart += string.Format("[_{0}_{1}]", _id2, _id);
                    else
                        __cart += string.Format("[{0}]", x);
                }
                StoreCookies.Cart = __cart;
            }
            return RedirectToAction("Cart");
        }
        public ActionResult OnlineBuy()
        {
            string cart = string.IsNullOrEmpty(StoreCookies.OnlineBuy)
                     ? "0" : StoreCookies.OnlineBuy;
            StoreCookies.Cart = "";
            store.managerorder.SetOrdersOnlineStatus2("privatePerson", cart);
            return RedirectToAction("Cart", new { message = "message" });
        }
        [HttpPost]
        public ActionResult TabCart(string ids)
        {
            string cart = string.IsNullOrEmpty(StoreCookies.TabCart)
                        ? "orders0" : StoreCookies.TabCart;
            if (ids.Contains("orders") && cart != ids)
            {
                StoreCookies.TabCart = ids;
                return RedirectToAction(ids);
            }
            else { return RedirectToAction(cart); }
        }
       
        #endregion

        #region Orders
        /// <summary>
        /// ManageOrders
        /// </summary>
        /// <returns></returns>
        [RoleStore]
        public ActionResult ManagerOrders()
        {
            ViewBag.Bread = new string[] { "<a href='/Store/Index/1/10/0/0/1/Department'>" + ModuleSetting.nameStore() + "</a>", StoreLanguage.managerorders };
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult DeleteOrder(int id, string id2)
        {
            store.managerorder.DeleteManagerOrder(id, HttpContext.User.Identity.Name);
            return RedirectToAction("Cart", new { id = 0, id2 });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult ApprovedOrder(int id, string id2)
        {
            store.managerorder.ApprovedOrder(id, HttpContext.User.Identity.Name);
            return RedirectToAction("Cart", new { id = 0, id2 });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [RoleStore]
        public ActionResult MoveOrders(int id)
        {
            store.managerorder.MoveToArhivManagerOrder(id);
            return RedirectToAction("ManagerOrders");
        }
        [RoleStore]
        public ActionResult OrderDetails(int id)
        {
            OrderDetailsModel model = new OrderDetailsModel();
            var x = store.managerorder.GetOrderForManager(id);
            decimal totalprice = 0;
            model.orderisproduct = ManagerOrdersHelpers.ViewOrdersForOrderDetails(store.order.GetOrdersForUser(id), out totalprice);
            model.id = id;
            model.address = x.mytrip_storeprofile.Address;
            model.firstname = x.mytrip_storeprofile.FirstName;
            model.lastname = x.mytrip_storeprofile.LastName;
            model.viewOrganization = "none";
            model.viewOrganizationRu = "none";
            model.total = ManagerOrdersHelpers.Total(totalprice, x.Delivery, x.MoneyId);
            model.priceInWords = x.PriceInWords;
            model.namberaccount = x.NamberAccount;
            if (ModuleSetting.organizationBuy())
            {
                model.viewOrganization = "show";
                model.organization = x.mytrip_storeprofile.Organization;
                if (LocalisationSetting.culture().ToLower() == "ru-ru")
                {
                    model.viewOrganizationRu = "show";
                    model.organizationINN = x.mytrip_storeprofile.OrganizationINN;
                    model.organizationKPP = x.mytrip_storeprofile.OrganizationKPP;
                }
            }
            model.phone = x.mytrip_storeprofile.Phone;
            model.useremail = x.mytrip_storeprofile.UserEmail;
            model.delivery = x.Delivery.ToString("0.00");
            model.moneyId = x.MoneyId;
            model.SelectCultureMoney = new SelectList(MoneyHelpers.CultureMoney(), "Key", "Value", model.moneyId);
            //seller
            var s = store.seller.GetSeller();
            model.selleraccountant = s.Accountant;
            model.selleraddress = s.Address;
            model.sellerbank = s.Bank;
            model.sellerbankaccount = s.BankAccount;
            model.sellerbankaccountBIK = s.BankAccountBIK;
            model.sellerbankaccountSeller = s.BankAccountSeller;
            model.sellerdirector = s.Director;
            model.selleremail = s.Email;
            model.sellerliteNDS = s.LiteNDS;
            model.sellerorganization = s.Organization;
            model.sellerorganizationINN = s.OrganizationINN;
            model.sellerorganizationKPP = s.OrganizationKPP;
            model.sellerphone = s.Phone;
            model.Bread = new string[] { "<a href='/Store/Index/1/10/0/0/1/Department'>" + ModuleSetting.nameStore() + "</a>",
               "<a href='/Store/Index/1/10/0/0/1/Department'>" + StoreLanguage.managerorders 
               + "</a>", string.Format(StoreLanguage.orderdetailstitle,id) };
            return View(model);
        }
        [RoleStore]
        [HttpPost]
        public ActionResult OrderDetails(int id, OrderDetailsModel model)
        {
            if (ModelState.IsValid)
            {
                decimal pr = 0;
                if (LocalisationSetting.culture().ToLower() == "ru-ru")
                    decimal.TryParse(model.delivery.Replace(".", ","), out pr);
                if (LocalisationSetting.culture().ToLower() == "en-us")
                    decimal.TryParse(model.delivery.Replace(",", "."), out pr);
                int profileid = store.managerorder.UpdateOrder(id, model.priceInWords,
                    pr, model.moneyId, model.namberaccount);
                store.seller.UpdateSeller(model.selleraccountant, model.selleraddress,
                    model.sellerbank, model.sellerbankaccount, model.sellerbankaccountBIK,
                    model.sellerbankaccountSeller, model.sellerdirector, model.selleremail,
                    model.sellerliteNDS, model.sellerorganization, model.sellerorganizationINN,
                    model.sellerorganizationKPP, model.sellerphone);
                store.profile.UpdateProfile(profileid, model.address, model.firstname, model.lastname,
                    model.organization, model.organizationINN, model.organizationKPP, model.phone,
                    model.useremail);
                return RedirectToAction("OrderDetails", new { id });
            }
            model.Bread = new string[] { "<a href='/Store/Index/1/10/0/0/1/Department'>" + ModuleSetting.nameStore() + "</a>",
               "<a href='/Store/Index/1/10/0/0/1/Department'>" + StoreLanguage.managerorders + "</a>", string.Format(StoreLanguage.orderdetailstitle,id) };
            return View(model);
        }
        public ActionResult DeleteProductOrder(int id, int id2)
        {
            store.order.DeleteOrders(id2, id);
            return RedirectToAction("OrderDetails", new { id = id2 });
        }
        public ActionResult BillingOrder(int id)
        {
            store.managerorder.SetOrdersStatus2(id);
            return RedirectToAction("ManagerOrders");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [RoleStore]
        public ActionResult CountProductManager(int id, int id2, int id3)
        {
            store.order.CountOrders(id3, id2, id);
            return RedirectToAction("OrderDetails", new { id = id3 });
        }
        [RoleStore]
        public string CreateAccount(int id)
        {
            return ManagerOrdersHelpers.PrintPreview(id);
        }
        public string Account(int id)
        {
            var x = store.managerorder.GetOrderForManager(id);
            return x.AccountPage;
        }
        [RoleStore]
        public ActionResult SetAccount(int id)
        {
            store.managerorder.SetOrdersStatus1(id, ManagerOrdersHelpers.PrintPreview(id));
            return RedirectToAction("ManagerOrders");
        }
        [RoleStore]
        public ActionResult AddAccount(int id)
        {
            StoreCookies.AddAcount = id.ToString();
            return RedirectToAction("Index", new { id = 1, id2 = 10, id3 = 0, id4 = 0, id5 = 1, id6 = "Department" });
        }
        [RoleStore]
        public ActionResult AddPosition(int id, int id2)
        {
            store.order.AddPosition(id, id2);
            StoreCookies.AddAcount = "0";
            return RedirectToAction("OrderDetails", new { id });
        }
        #endregion

        #region ControlPanel, Setting, Manage
        [RoleAdminAndEditor]
        public ActionResult Setting()
        {
            SettingStorelModel model = new SettingStorelModel();
            model.columnDepartment = ModuleSetting.columnDepartment();
            model.columnProduct = ModuleSetting.columnProduct();
            model.MoneyProcent = ModuleSetting.MoneyProcent();

            model.nameProducer_ru_ru = ModuleSetting.nameProducer("ru-ru");
            model.NameSearchPage_ru_ru = ModuleSetting.NameSearchPage("ru-ru");
            model.nameStore_ru_ru = ModuleSetting.nameStore("ru-ru");
            model.nameProducerDescription_en_us = ModuleSetting.nameProducerDesc("en-us");
            model.nameProducerDescription_ru_ru = ModuleSetting.nameProducerDesc("ru-ru");
            model.nameProducerKeywords_en_us = ModuleSetting.nameProducerKeyWords("en-us");
            model.nameProducerKeywords_ru_ru = ModuleSetting.nameProducerKeyWords("ru-ru");
            model.nameProducerTitle_en_us = ModuleSetting.nameProducerTitle("en-us");
            model.nameProducerTitle_ru_ru = ModuleSetting.nameProducerTitle("ru-ru");
            model.nameStoreDescription_en_us = ModuleSetting.nameStoreDesc("en-us");
            model.nameStoreDescription_ru_ru = ModuleSetting.nameStoreDesc("ru-ru");
            model.nameStoreKeywords_en_us = ModuleSetting.nameStoreKeyWords("en-us");
            model.nameStoreKeywords_ru_ru = ModuleSetting.nameStoreKeyWords("ru-ru");
            model.nameStoreTitle_en_us = ModuleSetting.nameStoreTitle("en-us");
            model.nameStoreTitle_ru_ru = ModuleSetting.nameStoreTitle("ru-ru");
            model.nameProducer_en_us = ModuleSetting.nameProducer("en-us");
            model.NameSearchPage_en_us = ModuleSetting.NameSearchPage("en-us");
            model.nameStore_en_us = ModuleSetting.nameStore("en-us");

            model.onlineBuy = ModuleSetting.onlineBuy();
            model.organizationBuy = ModuleSetting.organizationBuy();
            model.roleChiefStoreManager = ModuleSetting.roleChiefStoreManager();
            model.roleStoreManager = ModuleSetting.roleStoreManager();
            model.styleDepartment = ModuleSetting.styleDepartment();
            model.styleProduct = ModuleSetting.styleProduct();
            model.unlockStore = ModuleSetting.unlockStore();
            model.viewProduktTable = ModuleSetting.viewProduktTable();
            model.widthImgDepartment = ModuleSetting.widthImgDepartment();
            model.widthImgProduct = ModuleSetting.widthImgProduct();

            model.partialAccordion = ModuleSetting.partialAccordion();
            model.partialMenuLogon = ModuleSetting.partialMenuLogon();
            model.partialMenuLogonWrap = ModuleSetting.partialMenuLogonWrap();
            model.partialNoAccordion = ModuleSetting.partialNoAccordion();
            if (!LocalisationSetting.unlockAllCulture() && LocalisationSetting.defaultCulture() == "ru-ru")
                model.view_en_us = "none";
            if (!LocalisationSetting.unlockAllCulture() && LocalisationSetting.defaultCulture() == "en-us")
                model.view_ru_ru = "none";
            model.Bread = new string[] { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", StoreLanguage.storeSetting };
            return View(model);
        }
        [RoleAdminAndEditor]
        [HttpPost]
        public ActionResult Setting(SettingStorelModel model)
        {
            if (ModelState.IsValid)
            {
                MytripUser.RenameRole(ModuleSetting.roleChiefStoreManager(), model.roleChiefStoreManager);
                MytripUser.RenameRole(ModuleSetting.roleStoreManager(), model.roleStoreManager);
                #region Сохранение данных в mtm.Config.xml
                string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
                XDocument _doc = XDocument.Load(_absolutDirectory);
                var a = _doc.Root.Elements("mtm.Store").Elements("add");
                a.FirstOrDefault(x => x.Attribute("name").Value == "unlockStore")
                    .SetAttributeValue("value", model.unlockStore.ToString());
                a.FirstOrDefault(x => x.Attribute("name").Value == "columnDepartment")
                    .SetAttributeValue("value", model.columnDepartment.ToString());
                a.FirstOrDefault(x => x.Attribute("name").Value == "widthImgDepartment")
                    .SetAttributeValue("value", model.widthImgDepartment.ToString());
                a.FirstOrDefault(x => x.Attribute("name").Value == "styleDepartment")
                    .SetAttributeValue("value", model.styleDepartment.ToString());
                a.FirstOrDefault(x => x.Attribute("name").Value == "columnProduct")
                    .SetAttributeValue("value", model.columnProduct.ToString());
                a.FirstOrDefault(x => x.Attribute("name").Value == "widthImgProduct")
                    .SetAttributeValue("value", model.widthImgProduct.ToString());
                a.FirstOrDefault(x => x.Attribute("name").Value == "styleProduct")
                    .SetAttributeValue("value", model.styleProduct.ToString());
                a.FirstOrDefault(x => x.Attribute("name").Value == "roleChiefStoreManager")
                    .SetAttributeValue("value", model.roleChiefStoreManager.ToString());
                a.FirstOrDefault(x => x.Attribute("name").Value == "roleStoreManager")
                    .SetAttributeValue("value", model.roleStoreManager.ToString());
                a.FirstOrDefault(x => x.Attribute("name").Value == "onlineBuy")
                    .SetAttributeValue("value", model.onlineBuy.ToString());
                a.FirstOrDefault(x => x.Attribute("name").Value == "MoneyProcent")
                   .SetAttributeValue("value", model.MoneyProcent.ToString());
                a.FirstOrDefault(x => x.Attribute("name").Value == "organizationBuy")
                   .SetAttributeValue("value", model.organizationBuy.ToString());
                a.FirstOrDefault(x => x.Attribute("name").Value == "viewProduktTable")
                   .SetAttributeValue("value", model.viewProduktTable.ToString());

                a.FirstOrDefault(x => x.Attribute("name").Value == "partialAccordion")
                   .SetAttributeValue("value", model.partialAccordion);
                a.FirstOrDefault(x => x.Attribute("name").Value == "partialNoAccordion")
                   .SetAttributeValue("value", model.partialNoAccordion);
                a.FirstOrDefault(x => x.Attribute("name").Value == "partialMenuLogon")
                   .SetAttributeValue("value", model.partialMenuLogon);
                a.FirstOrDefault(x => x.Attribute("name").Value == "partialMenuLogonWrap")
                   .SetAttributeValue("value", model.partialMenuLogonWrap);

                var nameStore = a.FirstOrDefault(x => x.Attribute("name").Value == "nameStore").Elements("add");
                nameStore.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetAttributeValue("name", model.nameStore_ru_ru);

                nameStore.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetElementValue("title", model.nameStoreTitle_ru_ru);
                nameStore.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetElementValue("keywords", model.nameStoreKeywords_ru_ru);
                nameStore.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetElementValue("description", model.nameStoreDescription_ru_ru);


                nameStore.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetAttributeValue("name", model.nameStore_en_us);

                nameStore.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetElementValue("title", model.nameStoreTitle_en_us);
                nameStore.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetElementValue("keywords", model.nameStoreKeywords_en_us);
                nameStore.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetElementValue("description", model.nameStoreDescription_en_us);

                var nameProducer = a.FirstOrDefault(x => x.Attribute("name").Value == "nameProducer").Elements("add");
                nameProducer.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetAttributeValue("name", model.nameProducer_ru_ru);

                nameProducer.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetElementValue("title", model.nameProducerTitle_ru_ru);
                nameProducer.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetElementValue("keywords", model.nameProducerKeywords_ru_ru);
                nameProducer.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetElementValue("description", model.nameProducerDescription_ru_ru);


                nameProducer.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetAttributeValue("name", model.nameProducer_en_us);

                nameProducer.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetElementValue("title", model.nameProducerTitle_en_us);
                nameProducer.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetElementValue("keywords", model.nameProducerKeywords_en_us);
                nameProducer.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetElementValue("description", model.nameProducerDescription_en_us);


                var nameSearch = a.FirstOrDefault(x => x.Attribute("name").Value == "nameSearch").Elements("add");
                nameSearch.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetAttributeValue("name", model.NameSearchPage_ru_ru);
                nameSearch.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetAttributeValue("name", model.NameSearchPage_en_us);
                _doc.Save(_absolutDirectory);
                #endregion

                #region Очистка кеша
                GeneralMethods.MytripCacheRemove("ss_viewprodukttable");
                GeneralMethods.MytripCacheRemove("ss_organizationbuy");
                GeneralMethods.MytripCacheRemove("ss_moneyprocent");
                GeneralMethods.MytripCacheRemove("ss_unlockstore");
                GeneralMethods.MytripCacheRemove("ss_onlinebuy");
                GeneralMethods.MytripCacheRemove("ss_columndepartment");
                GeneralMethods.MytripCacheRemove("ss_widthimgdepartment");
                GeneralMethods.MytripCacheRemove("ss_styledepartment");
                GeneralMethods.MytripCacheRemove("ss_columnproduct");
                GeneralMethods.MytripCacheRemove("ss_widthimgproduct");
                GeneralMethods.MytripCacheRemove("ss_styleproduct");
                GeneralMethods.MytripCacheRemove("ss_rolechiefstoremanager");
                GeneralMethods.MytripCacheRemove("ss_rolestoremanager");

                GeneralMethods.MytripCacheRemove("ss_partialaccordion");
                GeneralMethods.MytripCacheRemove("ss_partialnoaccordion");
                GeneralMethods.MytripCacheRemove("ss_partialmenulogon");
                GeneralMethods.MytripCacheRemove("ss_partialmenulogonwrap");

                GeneralMethods.MytripCacheRemove("ss_nameproducer", "ru-ru");
                GeneralMethods.MytripCacheRemove("ss_namestore", "ru-ru");
                GeneralMethods.MytripCacheRemove("ss_namesearch", "ru-ru");

                GeneralMethods.MytripCacheRemove("ss_nameproducerdescription", "ru-ru");
                GeneralMethods.MytripCacheRemove("ss_nameproducertitle", "ru-ru");
                GeneralMethods.MytripCacheRemove("ss_nameproducerkeywords", "ru-ru");

                GeneralMethods.MytripCacheRemove("ss_namestoredescription", "ru-ru");
                GeneralMethods.MytripCacheRemove("ss_namestoretitle", "ru-ru");
                GeneralMethods.MytripCacheRemove("ss_namestorekeywords", "ru-ru");


                GeneralMethods.MytripCacheRemove("ss_nameproducer", "en-us");
                GeneralMethods.MytripCacheRemove("ss_namestore", "en-us");
                GeneralMethods.MytripCacheRemove("ss_namesearch", "en-us");

                GeneralMethods.MytripCacheRemove("ss_nameproducerdescription", "en-us");
                GeneralMethods.MytripCacheRemove("ss_nameproducertitle", "en-us");
                GeneralMethods.MytripCacheRemove("ss_nameproducerkeywords", "en-us");

                GeneralMethods.MytripCacheRemove("ss_namestoredescription", "en-us");
                GeneralMethods.MytripCacheRemove("ss_namestoretitle", "en-us");
                GeneralMethods.MytripCacheRemove("ss_namestorekeywords", "en-us");
                #endregion

                return RedirectToAction("Index", "Home");
            }
            if (!LocalisationSetting.unlockAllCulture() && LocalisationSetting.defaultCulture() == "ru-ru")
                model.view_en_us = "none";
            if (!LocalisationSetting.unlockAllCulture() && LocalisationSetting.defaultCulture() == "en-us")
                model.view_ru_ru = "none";
            model.Bread = new string[] { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", StoreLanguage.storeSetting };
            return View(model);
        }
        /// <summary>
        /// StoreManager
        /// </summary>
        /// <returns></returns>
        [RoleStore]
        public ActionResult Manager()
        {
            ViewBag.Bread = new string[] { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>", StoreLanguage.storeSetting };
            return View();
        }
        /// <summary>
        /// CreateSale
        /// </summary>
        /// <returns></returns>
        [RoleStore]
        public ActionResult CreateSale()
        {
            CreateSaleModel model = new CreateSaleModel();
            model.datestart = DateTime.Now.ToString("yyyy-MM-dd");
            model.Bread = new string[] { "<a href='/Store/Index/1/10/0/0/1/Department'>" + ModuleSetting.nameStore() + "</a>", StoreLanguage.createsale };
            return View(model);
        }
        /// <summary>
        /// CreateSale
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [RoleStore]
        [HttpPost]
        public ActionResult CreateSale(CreateSaleModel model)
        {
            if (ModelState.IsValid)
            {
                store.sale.CreateSale(model.sale, DateTime.Parse(model.datestart), DateTime.Parse(model.dateclose));
                return RedirectToAction("Manager");
            }
            model.Bread = new string[] { "<a href='/Store/Index/1/10/0/0/1/Department'>" + ModuleSetting.nameStore() + "</a>", StoreLanguage.createsale };
            return View(model);
        }
        /// <summary>
        /// CreateProductXml
        /// </summary>
        /// <returns></returns>
        [RoleStore]
        public ActionResult CreateProductXml()
        {
            ExcelRepository vv = new ExcelRepository();
            CreateProductXmlModel model = new CreateProductXmlModel();
            model.sale = vv.saleid();
            model.producer = vv.producerid();
            model.department = vv.departmentid();
            model.product = vv.productid();
            model.Bread = new string[] { "<a href='/Store/Index/1/10/0/0/1/Department'>" + ModuleSetting.nameStore() + "</a>", StoreLanguage.uploadproductxml };
            return View(model);
        }
        /// <summary>
        /// CreateProductXml
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [RoleStore]
        [HttpPost]
        public ActionResult CreateProductXml(CreateProductXmlModel model, HttpPostedFileBase id)
        {
            ExcelRepository vv = new ExcelRepository();
            if (ModelState.IsValid && id != null)
            {
                string name = store.product.CreateProductXml(id);
                vv.SaleExcelToSql(name, model.delete);
                vv.ProducerExcelToSql(name, model.delete);
                vv.DepartmentExcelToSql(name, model.delete);
                vv.ProductExcelToSql(name, model.delete);
                return RedirectToAction("Manager");
            }
            model.sale = vv.saleid();
            model.producer = vv.producerid();
            model.department = vv.departmentid();
            model.product = vv.productid();
            model.Bread = new string[] { "<a href='/Store/Index/1/10/0/0/1/Department'>" + ModuleSetting.nameStore() + "</a>", StoreLanguage.uploadproductxml };
            return View(model);
        }
        public FilePathResult DowloadFile(string id)
        {
            ExcelRepository dd = new ExcelRepository();
            dd.SqlToExcel(id);
            string directory = "/Content/Store/Catalog/mytrip_products." + id;
            string absolutDirectory = HttpContext.Server.MapPath(directory);
            return File(absolutDirectory, "text/plain", "mytrip_products." + id);
        }
        #endregion
       
        #region /*---------ОТЛОЖИЛ------------------*/
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        //public ActionResult Order()
        //{
        //    if (!ModuleSetting.onlineBuy())
        //    {

        //    }
        //    //оформление заявки либо
        //    //редирект на оплату эл.деньгами
        //    //если доставка то снятие адреса у пользователя
        //    //и контактных данных
        //    //резервирование товара на складе
        //    return View();
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public ActionResult PayPal()
        //{

        //    MerchantProfile merchant = new MerchantProfile(
        //    "blabla_1234567890_biz[at]email.com",
        //     "1234567890",
        //     "odifhp9p83948rlwkcmnwli430948f3ojldkjflskdjlsdkjsf0o98209",
        //     "sandbox");
        //    BuyerProfile buyer = new BuyerProfile(
        //    "John", "Doe",
        //    "1 Main St", "", "San Jose", "CA", "95131",
        //    "Visa", "4197058882575379", "926",
        //    10, 2010
        //    );
        //    PayPalResponse PayPalResponse = PayPalHelper.DoDirectPayment("13.45", merchant, buyer);
        //    if (PayPalResponse.Ack == AckType.Success)
        //    {
        //        //ok
        //    }
        //    else
        //    {
        //        var error = PayPalResponse.Errors.First();
        //    }
        //    return View();
        //}
        #endregion
    }
}
