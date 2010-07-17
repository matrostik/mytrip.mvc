using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mytrip.Mvc.Models;
using Mytrip.Store.Repository;
using Mytrip.Store.Models;
using Mytrip.Mvc;

namespace Mytrip.Store.Controllers
{
    [HandleError]
    [Localization]
   public class StoreController:Controller
    {
        private IStoreRepository _IStoreRepository;
        public IStoreRepository store
        {
            get
            {
                if (_IStoreRepository == null)
                    _IStoreRepository = new IStoreRepository();
                return _IStoreRepository;
            }
        }
        private StoreSettings _StoreSettings;
        public StoreSettings storeset
        {
            get
            {
                if (_StoreSettings == null)
                    _StoreSettings = new StoreSettings();
                return _StoreSettings;
            }
        }
        public string culture
        {
            get
            { return Session["culture"].ToString(); }
            set
            { Session["culture"] = value; }
        }
        public ActionResult Index(int id,int id2,int id3,int id4,int id5,string id6,int? id7,int? id8)
        {
            
            int total=0;
            int _id2 = id2;
            DepartmentModel model = new DepartmentModel();
            string body = string.Empty; 
            string img = string.Empty; 
            string subDepartmentPath = string.Empty; 
            string subDepartmentTitle = string.Empty; 
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
            //Search Depatment&Producer
            string s_search= string.Empty;
            int s_totalsearch=0;
            int s_id =0;
            string s_path= string.Empty;
            int s_ProducerId =0;
            string s_ProducerTitle= string.Empty;
            string s_ProducerBody= string.Empty;
            string s_ProducerImg= string.Empty;
            string s_ProducerPath = string.Empty;
            int s_departmentcount =0;
            int s_producercount = 0;
            bool _search = false;
            #region Store
            if (id3 == 0 && id4==0&&id6!="Producer"&&id8==null)
            {
                id2 = id2 * storeset.columnDepartment();
                model.Department = store.department.GetAllDepartment(id, id2, culture, out total);
                model.total = total;
                model.take = id2;
                if (id2 < total)
                    model.paging = true;
                model.takepaging = (int)Math.Ceiling((double)total / storeset.columnDepartment());    
                title = storeset.nameStore();
            }
            #endregion                
            #region Department & SubDepartment
            else if (id3 > 0 && id4 == 0 && id8 == null)
            {
                count = 0;
                model.Department = store.department.GetSubDepartment(id3, culture);
                model.take = model.Department.Count();
                if (model.take > 0)
                {
                    foreach (var x in model.Department)
                    {
                        count += x.mytrip_storeproduct.Count();
                    }
                }
                var department = store.department.GetDepartment(id3);
                
                count += department.mytrip_storeproduct.Count();
                title = department.Title;
                body = department.Body;
                img = department.Image;
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
                } _id2 = _id2 * storeset.columnProduct();
                model.Product = store.product.GetProductForDepartment(id3, id, _id2,id5, culture, out total);
                //total product!!!!
                model.total = total;
                model.take = _id2;
                model.paging2 = true;
                model.takepaging = (int)Math.Ceiling((double)total / storeset.columnProduct());  
            }
            #endregion
            #region Producers
            else if (id3 == 0 && id4 == 0 && id6 == "Producer" && id8 == null)
            {
                id2 = id2 * storeset.columnDepartment();
                model.Producer = store.producer.GetAllProducer(id, id2, culture, out total);
                model.total = total;
                model.take = id2;
                if (id2 < total)
                    model.paging = true;
                producer = true;
                model.takepaging = (int)Math.Ceiling((double)total / storeset.columnDepartment());  
                title = StoreLanguage.Producers;
            }
            #endregion
            #region Producer
            else if (id3 == 0 && id4 > 0 && id8 == null)
            {
                count = 0;
                var department = store.producer.GetProducer(id4);
                count += department.mytrip_storeproduct.Count();
                title = department.Title;
                body = department.Body;
                img = department.Image;
                 _id2 = _id2 * storeset.columnProduct();
                model.Product = store.product.GetProductForProducer(id4, id, _id2,id5, culture, out total);
                //total product!!!!
                model.total = total;
                model.take = _id2;
                producer = true;
                model.paging2 = true;
                model.takepaging = (int)Math.Ceiling((double)total / storeset.columnProduct()); 
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
                    id6 = GeneralMethods.DecodingSearch2(id6);
                    model.Search = id6;
                    s_search = id6;
                }
                _id2 = _id2 * storeset.columnProduct();
                #region Producer
                if (id3 == 0 && id4 > 0 && id7 >= 0 && id8 >= 0)
                {
                    producer = true;
                    count = 0;
                    var department = store.producer.GetProducer(id4);
                    count += department.mytrip_storeproduct.Count();
                    title = department.Title;
                    body = department.Body;
                    img = department.Image;
                    var searchproduct = store.product.GetProductForProducer(id4, id, _id2, id5, (int)id7, (int)id8, culture, id6, out total);
                    if (id6 != "x")
                    {
                        foreach (var art in searchproduct)
                        {
                            art.Title = GeneralMethods.ReplaceString(art.Title, id6);
                            art.Abstract = GeneralMethods.ReplaceString(art.Abstract, id6);
                        }
                    }
                    model.Product = searchproduct;
                }
                #endregion
                #region Department
                else if (id3 > 0 && id4 == 0 && id7 >= 0 && id8 >= 0)
                {
                    count = 0;
                    model.Department = store.department.GetSubDepartment(id3, culture);
                    model.take = model.Department.Count();
                    if (model.take > 0)
                    {
                        foreach (var x in model.Department)
                        {
                            count += x.mytrip_storeproduct.Count();
                        }
                    }
                    var department = store.department.GetDepartment(id3);
                    count += department.mytrip_storeproduct.Count();
                    title = department.Title;
                    body = department.Body;
                    img = department.Image;
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
                    var searchproduct = store.product.GetProductForDepartment(id3, id, _id2, id5, (int)id7, (int)id8, culture, id6, out total);
                    if (id6 != "x")
                    {
                        foreach (var art in searchproduct)
                        {
                            art.Title = GeneralMethods.ReplaceString(art.Title, id6);
                            art.Abstract = GeneralMethods.ReplaceString(art.Abstract, id6);
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
                    s_ProducerImg = searchproducer.Image;
                    #endregion
                    model.Department = store.department.GetSubDepartment(id3, culture);
                    model.take = model.Department.Count();
                    if (model.take > 0)
                    {
                        foreach (var x in model.Department)
                        {
                            s_departmentcount += x.mytrip_storeproduct.Count();
                        }
                    }
                    var department = store.department.GetDepartment(id3);
                    s_departmentcount += department.mytrip_storeproduct.Count();
                    s_id = id3;
                    s_path = department.Path;
                    title = department.Title;
                    body = department.Body;
                    img = department.Image;
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
                    var searchproduct = store.product.GetProductForDepartmentAndProducer(id3,id4, id, _id2, id5, (int)id7, (int)id8, culture, id6, out total);
                    if (id6 != "x")
                    {
                        foreach (var art in searchproduct)
                        {
                            art.Title = GeneralMethods.ReplaceString(art.Title, id6);
                            art.Abstract = GeneralMethods.ReplaceString(art.Abstract, id6);
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
                    var searchproduct = store.product.GetProductForDepartmentAndProducer(id, _id2, id5, (int)id7, (int)id8, culture, id6, out total);
                    if (id6 != "x")
                    {
                        foreach (var art in searchproduct)
                        {
                            art.Title = GeneralMethods.ReplaceString(art.Title, id6);
                            art.Abstract = GeneralMethods.ReplaceString(art.Abstract, id6);
                        }
                    }
                    model.Product = searchproduct;
                }
                #endregion
                //total product!!!!
                model.total = total;
                s_totalsearch = total;
                model.take = _id2;
                model.paging2 = true;
                model.takepaging = (int)Math.Ceiling((double)total / storeset.columnProduct());
            }
            #endregion
            model.SelectDepartment = new SelectList(store.department.GetDepartmentForDdl(culture), "Key", "Value", model.DepartmentId);
            model.SelectProducer = new SelectList(store.producer.GetProducerForDdl(culture), "Key", "Value", model.ProducerId);
            model.titleDepartmentModel = new TitleDepartmentModel
            {
                producer=producer,
                body = body,
                img = img,
                subDepartmentPath = subDepartmentPath,
                subDepartmentTitle = subDepartmentTitle,
                subDepartmentId = subDepartmentId,
                count = count,
                subcount = subcount,
                title = title,
                search=s_search,
                totalsearch=s_totalsearch,
                id=s_id,
                path=s_path,
                ProducerId=s_ProducerId,
                ProducerTitle=s_ProducerTitle,
                ProducerBody=s_ProducerBody,
                ProducerImg=s_ProducerImg,
                ProducerPath=s_ProducerPath,
                _search=_search,
                departmentcount=s_departmentcount,
                producercount=s_producercount
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(DepartmentModel model,int id5) {
            string Search = string.Empty;
            if (model.Search !=null)
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
            new {id=1,
            id2=10,
            id3=_Department,
            id4=_Producer,
            id5=id5,
            id6=Search.Trim(),
            id7=_SmallPrice,
            id8=_BigPraice });
        }
        public ActionResult View(int id, string id2)
        {
            string[] _id={};
            ProductModel model = new ProductModel();
            if(id>0)
            model.Product=store.product.GetProductForViews(id);
            return View(model);
        
        }
    }
}
