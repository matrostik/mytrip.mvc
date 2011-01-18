using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using mtm.Core.Settings;
using mtm.Store.Repository.DataEntities;
using mtm.Core.Repository.LingToExcel;
using System.IO;

namespace mtm.Store.Repository
{
   public class ExcelRepository
    {
        #region Подключение к Entity Репозиторию

        Entities _entities;

        /// <summary>Подключение к Entity Репозиторию
        /// </summary>
        public Entities entities
        {
            get
            {
                if (_entities == null)
                    _entities = new Entities(ModuleSetting.connectionString());
                return _entities;
            }
        }
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
        public void SaleExcelToSql(string filename,bool _delete)
        {
            if (filename.Contains(".xls"))
            {
                string directory = "/Content/Store/Catalog/" + filename;
                string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
                ExcelProvider provider = ExcelProvider.Create(absolutDirectory);
                var producer = provider.GetSheet<ExcelSale>().Where(x => x.SaleId !=null);
                foreach (var z in entities.mytrip_storesale
                    .Include("mytrip_storeproduct")
                    .Include("mytrip_storeproducer")
                    .Include("mytrip_storedepartment")
                    .Where(z=>z.SaleId>0).OrderBy(z => z.SaleId))
                {
                    try
                    {
                        bool delete = true;
                        foreach (var x in producer)
                        {
                            if (z.SaleId == (int)x.SaleId)
                            {
                                delete = false;
                                break;
                            }
                        }
                        if (_delete&&delete)
                        {
                            foreach (var y in z.mytrip_storeproduct.ToList())
                            {
                                y.SaleId = 0;
                            }
                            foreach (var y in z.mytrip_storeproducer.ToList())
                            {
                                y.SaleId = 0;
                            }
                            foreach (var y in z.mytrip_storedepartment.ToList())
                            {
                                y.SaleId = 0;
                            }
                        }
                    }
                    catch { }
                }
                entities.SaveChanges();
                foreach (var x in producer)
                {
                    try
                    {
                        store.sale.CreateSaleZero();
                        int id = (int)x.SaleId;
                        double sale = x.Sale;
                        var y = entities.mytrip_storesale.FirstOrDefault(z => z.SaleId == id);
                        if (y != null)
                        {
                            y.Title = x.Title;
                            y.Sale = (int)sale;
                            y.CloseDate = DateTime.Parse(x.CloseDate);
                            y.StartDate = DateTime.Parse(x.StartDate);
                            entities.SaveChanges();
                        }
                        else
                        {
                            store.sale.CreateSale((int)x.SaleId, (int)sale, x.Title, DateTime.Parse(x.StartDate), DateTime.Parse(x.CloseDate));
                        }
                    }
                    catch { }
                }
            }
        }
        public void ProducerExcelToSql(string filename, bool _delete)
        {
            if (filename.Contains(".xls"))
            {
                string directory = "/Content/Store/Catalog/" + filename;
                string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
                ExcelProvider provider = ExcelProvider.Create(absolutDirectory);
                var producer = provider.GetSheet<ExcelProducer>().Where(x => x.ProducerId != null);
                foreach (var z in entities.mytrip_storeproducer.Include("mytrip_storeproduct").OrderBy(z => z.ProducerId))
                {
                    try
                    {
                        bool delete = true;
                        foreach (var x in producer)
                        {
                            if (z.ProducerId == (int)x.ProducerId)
                            {
                                delete = false;
                                break;
                            }

                        }
                        if (_delete&&delete)
                        {
                            foreach (var y in z.mytrip_storeproduct.ToList())
                            {
                                foreach (var q in y.mytrip_storevotes.ToList())
                                {
                                    entities.mytrip_storevotes.DeleteObject(q);
                                }
                                foreach (var q in y.mytrip_storeorderisproduct.ToList())
                                {
                                    entities.mytrip_storeorderisproduct.DeleteObject(q);
                                }
                                entities.mytrip_storeproduct.DeleteObject(y);
                            }
                            entities.mytrip_storeproducer.DeleteObject(z);
                        }
                    }
                    catch { }
                }
                entities.SaveChanges();
                foreach (var x in producer)
                {
                    try
                    {
                        bool allculture = (x.AllCulture.ToUpper() == "TRUE") ? true : false;
                        string culture = (x.Culture != null) ? x.Culture : LocalisationSetting.culture();
                        var sale = entities.mytrip_storesale.FirstOrDefault(z => z.Title == x.SaleId);
                        int saleid = (sale != null) ? sale.SaleId : 0;
                        store.sale.CreateSaleZero();
                        int id = (int)x.ProducerId;
                        string body = (x.Body.ToLower() == "null") ? null : x.Body;
                        var y = entities.mytrip_storeproducer.FirstOrDefault(z => z.ProducerId == id);

                        if (y != null)
                        {
                            y.Title = x.Title;
                            y.Path = GeneralMethods.DecodingString(x.Title);
                            y.AllCulture = allculture;
                            y.Body = body;
                            y.SaleId = saleid;
                            y.Culture = culture;
                            entities.SaveChanges();
                        }
                        else
                        {
                            store.producer.CreateProducer((int)x.ProducerId, x.Title, body, allculture, culture, saleid);
                        }
                    }
                    catch { }
                }
            }
        }
        public void DepartmentExcelToSql(string filename, bool _delete)
        {
            if (filename.Contains(".xls"))
            {
                string directory = "/Content/Store/Catalog/" + filename;
                string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
                ExcelProvider provider = ExcelProvider.Create(absolutDirectory);
                var producer = provider.GetSheet<ExcelDepartment>()
                    .Where(x => x.DepartmentId != null);
                foreach (var z in entities.mytrip_storedepartment
                    .Include("mytrip_storeproduct")
                    .Include("mytrip_storedepartment2")
                    .Include("mytrip_storedepartment1.mytrip_storeproduct")
                    .Include("mytrip_storesale")
                    .Include("mytrip_storedepartment2.mytrip_storesale")
                    .Where(z=>z.DepartmentId>0)
                    .OrderBy(z => z.DepartmentId))
                {
                    try
                    {
                        bool delete = true;
                        foreach (var x in producer)
                        {
                            if (z.DepartmentId == (int)x.DepartmentId)
                            {
                                delete = false;
                                break;
                            }

                        }
                        if (_delete&&delete)
                        {
                            foreach (var y in z.mytrip_storedepartment1.ToList())
                            {
                                foreach (var q in y.mytrip_storeproduct.ToList())
                                {
                                    foreach (var zz in q.mytrip_storevotes.ToList())
                                    { entities.mytrip_storevotes.DeleteObject(zz); }
                                    foreach (var zz in q.mytrip_storeorderisproduct.ToList())
                                    {
                                        entities.mytrip_storeorderisproduct.DeleteObject(zz);
                                    }
                                    entities.mytrip_storeproduct.DeleteObject(q);
                                }
                            }
                            foreach (var y in z.mytrip_storeproduct.ToList())
                            {
                                foreach (var zz in y.mytrip_storevotes.ToList())
                                { entities.mytrip_storevotes.DeleteObject(zz); }
                                foreach (var zz in y.mytrip_storeorderisproduct.ToList())
                                {
                                    entities.mytrip_storeorderisproduct.DeleteObject(zz);
                                }
                                entities.mytrip_storeproduct.DeleteObject(y);
                            }
                            entities.mytrip_storedepartment.DeleteObject(z);
                        }
                    }
                    catch { }
                }
                entities.SaveChanges();
                IDictionary<int, string> department = new Dictionary<int, string>();
                foreach (var x in producer.Where(z => z.SubDepartmentId == null || z.SubDepartmentId == "null"))
                {
                    try
                    {
                        bool allculture = (x.AllCulture.ToUpper() == "TRUE") ? true : false;
                        string culture = (x.Culture != null) ? x.Culture : LocalisationSetting.culture();
                        var sale = entities.mytrip_storesale.FirstOrDefault(z => z.Title == x.SaleId);
                        int saleid = (sale != null) ? sale.SaleId : 0;
                        string body = (x.Body.ToLower() == "null") ? null : x.Body;
                        store.sale.CreateSaleZero();
                        int id = (int)x.DepartmentId;
                        var y = entities.mytrip_storedepartment.FirstOrDefault(z => z.DepartmentId == id);
                        if (y != null)
                        {
                            y.Title = x.Title;
                            y.Path = GeneralMethods.DecodingString(x.Title);
                            y.AllCulture = allculture;
                            y.Body = body;
                            y.SaleId = saleid;
                            y.Culture = culture;
                            y.SubDepartmentId = 0;
                            entities.SaveChanges();
                        }
                        else
                        {
                            store.department.CreateDepartment((int)x.DepartmentId, 0, x.Title, body, allculture, culture, saleid);
                        }
                        department.Add((int)x.DepartmentId, x.Title);
                    }
                    catch { }
                }
                foreach (var x in producer.Where(z => z.SubDepartmentId != null && z.SubDepartmentId != "null"))
                {
                    try
                    {
                        bool allculture = (x.AllCulture.ToUpper() == "TRUE") ? true : false;
                        string culture = (x.Culture != null) ? x.Culture : LocalisationSetting.culture();
                        var sale = entities.mytrip_storesale.FirstOrDefault(z => z.Title == x.SaleId);
                        int saleid = (sale != null) ? sale.SaleId : 0;
                        string body = (x.Body.ToLower() == "null") ? null : x.Body;
                        store.sale.CreateSaleZero();
                        int id = (int)x.DepartmentId;
                        int departmentid = 0;
                        try { departmentid = department.FirstOrDefault(xx => xx.Value == x.SubDepartmentId).Key; }
                        catch { }
                        var y = entities.mytrip_storedepartment.FirstOrDefault(z => z.DepartmentId == id);
                        if (y != null)
                        {
                            y.Title = x.Title;
                            y.Path = GeneralMethods.DecodingString(x.Title);
                            y.AllCulture = allculture;
                            y.Body = body;
                            y.SaleId = saleid;
                            y.Culture = culture;
                            y.SubDepartmentId = departmentid;
                            entities.SaveChanges();
                        }
                        else
                        {
                            store.department.CreateDepartment((int)x.DepartmentId, departmentid, x.Title, body, allculture, culture, saleid);
                        }
                    }
                    catch { }
                }
            }
        }
        public void ProductExcelToSql(string filename, bool _delete)
        {
            if (filename.Contains(".xls"))
            {
                string directory = "/Content/Store/Catalog/" + filename;
                string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
                ExcelProvider provider = ExcelProvider.Create(absolutDirectory);
                var producer = provider.GetSheet<ExcelProduct>().Where(x => x.ProductId != null);
                foreach (var z in entities.mytrip_storeproduct
                    .Include("mytrip_storevotes")
                    .Include("mytrip_storeorderisproduct")
                    .OrderBy(z => z.ProductId))
                {
                    try
                    {
                        bool delete = true;
                        foreach (var x in producer)
                        {
                            if (z.ProductId == (int)x.ProductId)
                            {
                                delete = false;
                                break;
                            }

                        }
                        if (_delete&&delete)
                        {
                                foreach (var q in z.mytrip_storevotes.ToList())
                                {
                                    entities.mytrip_storevotes.DeleteObject(q);
                                }
                                foreach (var q in z.mytrip_storeorderisproduct.ToList())
                                {
                                    entities.mytrip_storeorderisproduct.DeleteObject(q);
                                }
                                entities.mytrip_storeproduct.DeleteObject(z);
                        }
                    }
                    catch { }
                }
                entities.SaveChanges();
                foreach (var x in producer)
                {
                    try
                    {
                        bool allculture = (x.AllCulture.ToUpper() == "TRUE") ? true : false;
                        bool viewcount = (x.ViewCount.ToUpper() == "TRUE") ? true : false;
                        bool viewprice = (x.ViewPrice.ToUpper() == "TRUE") ? true : false;
                        bool viewvotes = (x.ViewVotes.ToUpper() == "TRUE") ? true : false;
                        string body = (x.Body.ToLower() == "null") ? null : x.Body;
                        string details = (x.Details.ToLower() == "null") ? null : x.Details;
                        string packing = (x.Packing.ToLower() == "null") ? null : x.Packing;
                        string nambercatalog = (x.NamberCatalog.ToLower() == "null") ? null : x.NamberCatalog;
                        string culture = (x.Culture != null) ? x.Culture : LocalisationSetting.culture();
                        var sale = entities.mytrip_storesale.FirstOrDefault(z => z.Title == x.SaleId);
                        var _producer = entities.mytrip_storeproducer.FirstOrDefault(z => z.Title == x.ProducerId);
                        var _department = entities.mytrip_storedepartment.FirstOrDefault(z => z.Title == x.DepartmentId);
                        int saleid = (sale != null) ? sale.SaleId : 0;
                        int producerid = (_producer != null) ? _producer.ProducerId : 0;
                        int departmentid = (_department != null) ? _department.DepartmentId : 0;
                        store.sale.CreateSaleZero();
                        int id = (int)x.ProductId;
                        var y = entities.mytrip_storeproduct.FirstOrDefault(z => z.ProductId == id);
                        if (y != null)
                        {
                            y.Title = x.Title;
                            y.Path = GeneralMethods.DecodingString(x.Title);
                            y.AllCulture = allculture;
                            y.Body = body;
                            y.SaleId = saleid;
                            y.Culture = culture;
                            y.ProducerId = producerid;
                            y.DepartmentId = departmentid;
                            y.Details = details;
                            y.MoneyId = x.MoneyId;
                            y.NamberCatalog = nambercatalog;
                            y.Packing = packing;
                            y.Price = (decimal)x.Price;
                            y.TotalCount = (int)x.TotalCount;
                            y.ViewCount = viewcount;
                            y.ViewPrice = viewprice;
                            y.ViewVotes = viewvotes;
                            entities.SaveChanges();
                        }
                        else
                        {
                            store.product.CreateProduct(id,departmentid,producerid, x.Title, body,details,x.Culture, allculture,(decimal)x.Price,(int)x.TotalCount,null,viewcount,viewprice,viewvotes,packing,x.MoneyId,nambercatalog, saleid);
                        }
                    }
                    catch { }
                }
            }
        }

        public void SqlToExcel(string file)
        {

            string _directory = "/Content/Store/Catalog/Empty/mytrip_products."+file;
            string _absolutDirectory = HttpContext.Current.Server.MapPath(_directory);
            string directory = "/Content/Store/Catalog/mytrip_products." + file;
                string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
                File.Copy(_absolutDirectory, absolutDirectory,true);
                ExcelProvider provider = ExcelProvider.Create(absolutDirectory);
                foreach (var z in entities.mytrip_storesale.OrderBy(z => z.SaleId))
                {
                    ExcelSale x = new ExcelSale();
                    x.SaleId = z.SaleId;
                    if (z.Title == null || z.Title.Length < 2)
                        x.Title = "sale " + z.Sale + "% from " + string.Format("{0:dd.MM.yy}", z.StartDate) + " to " + string.Format("{0:dd.MM.yy}", z.CloseDate);
                    else
                    x.Title = z.Title;
                    x.StartDate = z.StartDate.ToString("yyyy-MM-dd");
                    x.CloseDate = z.CloseDate.ToString("yyyy-MM-dd");
                    if (z.Sale > 0)
                        x.Sale = z.Sale;
                    else
                        x.Sale = 0; 
                    provider.GetSheet<ExcelSale>().InsertOnSubmit(x);
                }
                foreach (var z in entities.mytrip_storeproducer
                    .Include("mytrip_storesale")
                    .OrderBy(z => z.ProducerId))
                {
                    string body = (z.Body == null || z.Body.Length < 2) ? "null" : z.Body;
                    ExcelProducer x = new ExcelProducer();
                    x.ProducerId = z.ProducerId;
                    x.Title = z.Title;
                    x.Body = body;
                    x.AllCulture = z.AllCulture.ToString().ToUpper();
                    x.Culture = z.Culture.ToLower();
                    if (z.mytrip_storesale.Title == null || z.mytrip_storesale.Title.Length < 2)
                        x.SaleId = "sale " + z.mytrip_storesale.Sale + "% from " + string.Format("{0:dd.MM.yy}", z.mytrip_storesale.StartDate) + " to " + string.Format("{0:dd.MM.yy}", z.mytrip_storesale.CloseDate);
                    else
                    x.SaleId = z.mytrip_storesale.Title;
                    provider.GetSheet<ExcelProducer>().InsertOnSubmit(x);
                }
                foreach (var z in entities.mytrip_storedepartment
                    .Include("mytrip_storesale")
                    .Include("mytrip_storedepartment2")
                    .Where(z=>z.DepartmentId>0)
                    .OrderBy(z => z.DepartmentId))
                {
                    string body = (z.Body == null || z.Body.Length < 2) ? "null" : z.Body;
                    string subdepartment = (z.SubDepartmentId > 0) ? z.mytrip_storedepartment2.Title : "null";
                    ExcelDepartment x = new ExcelDepartment();
                    x.DepartmentId = z.DepartmentId;
                    x.Title = z.Title;
                    x.Body = body;
                    x.AllCulture = z.AllCulture.ToString().ToUpper();
                    x.SubDepartmentId = subdepartment;
                    x.Culture = z.Culture.ToLower();
                    if (z.mytrip_storesale.Title == null || z.mytrip_storesale.Title.Length < 2)
                        x.SaleId = "sale " + z.mytrip_storesale.Sale + "% from " + string.Format("{0:dd.MM.yy}", z.mytrip_storesale.StartDate) + " to " + string.Format("{0:dd.MM.yy}", z.mytrip_storesale.CloseDate);
                    else
                        x.SaleId = z.mytrip_storesale.Title;
                    provider.GetSheet<ExcelDepartment>().InsertOnSubmit(x);
                }
                foreach (var z in entities.mytrip_storeproduct
                       .Include("mytrip_storesale")
                       .Include("mytrip_storedepartment")
                       .Include("mytrip_storeproducer")
                       .OrderBy(z => z.ProductId))
                {
                    string body = (z.Body == null || z.Body.Length < 2) ? "null" : z.Body;
                    string details = (z.Details == null || z.Details.Length < 2) ? "null" : z.Details;
                    string packing = (z.Packing == null || z.Packing.Length < 2) ? "null" : z.Packing;
                    string nambercatalog = (z.NamberCatalog == null || z.NamberCatalog.Length < 2) ? "null" : z.NamberCatalog;
                    ExcelProduct x = new ExcelProduct();
                    x.ProductId = z.ProductId;
                    x.NamberCatalog = nambercatalog;
                    x.Title = z.Title;
                    x.Body = body;
                    x.Details = details;
                    x.Price = (double)z.Price;
                    x.MoneyId = z.MoneyId;
                    x.Packing = packing;
                    x.TotalCount = z.TotalCount;
                    x.ViewPrice = z.ViewPrice.ToString().ToUpper();
                    x.ViewCount = z.ViewCount.ToString().ToUpper();
                    x.ViewVotes = z.ViewVotes.ToString().ToUpper();

                    x.AllCulture = z.AllCulture.ToString().ToUpper();
                    x.ProducerId = z.mytrip_storeproducer.Title;
                    x.DepartmentId = z.mytrip_storedepartment.Title;
                    x.Culture = z.Culture.ToLower();
                    if (z.mytrip_storesale.Title == null || z.mytrip_storesale.Title.Length < 2)
                        x.SaleId = "sale " + z.mytrip_storesale.Sale + "% from " + string.Format("{0:dd.MM.yy}", z.mytrip_storesale.StartDate) + " to " + string.Format("{0:dd.MM.yy}", z.mytrip_storesale.CloseDate);
                    else
                        x.SaleId = z.mytrip_storesale.Title;
                    provider.GetSheet<ExcelProduct>().InsertOnSubmit(x);
                }
                provider.SubmitChanges();
        }
        public int saleid()
        {
            var sale = entities.mytrip_storesale.OrderByDescending(x => x.SaleId).FirstOrDefault();
            if (sale == null)
                return 1;
            else
                return sale.SaleId + 1;
        }
        public int producerid()
        {
            var sale = entities.mytrip_storeproducer.OrderByDescending(x => x.ProducerId).FirstOrDefault();
            if (sale == null)
                return 1;
            else
                return sale.ProducerId + 1;
        }
        public int departmentid()
        {
            var sale = entities.mytrip_storedepartment.OrderByDescending(x => x.DepartmentId).FirstOrDefault();
            if (sale == null)
                return 1;
            else
                return sale.DepartmentId + 1;
        }
        public int productid()
        {
            var sale = entities.mytrip_storeproduct.OrderByDescending(x => x.ProductId).FirstOrDefault();
            if (sale == null)
                return 1;
            else
                return sale.ProductId + 1;
        }
    }
}
