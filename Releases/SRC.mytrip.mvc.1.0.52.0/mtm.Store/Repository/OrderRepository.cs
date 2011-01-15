using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Store.Repository.DataEntities;
using System.Web;
using mtm.Core.Repository;
using mtm.Store.Helpers;
using System.Net.Mail;
using mtm.Core.Settings;

namespace mtm.Store.Repository
{
    public class OrderRepository
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
        ManagerOrderRepository _manager;
        public ManagerOrderRepository __manager
        {
            get
            {
                if (_manager == null)
                    _manager = new ManagerOrderRepository();
                return _manager;
            }
        }
        #endregion
        public void CreateOrder(string culture, string address, string firstname,
            string lastname, string phone, string useremail, string organization,
            string organizationINN,
            string organizationKPP)
        {
            string cart = (HttpContext.Current.Request.Cookies["mtProductCart"] == null)
                        ? string.Empty
                        : HttpContext.Current.Request.Cookies["mtProductCart"].Value;
            cart = cart.Replace("][", "|").Replace("[", "").Replace("]", "");
            string[] _cart = cart.Split('|');
            string ordermail = "";
            string userName = (HttpContext.Current.User.Identity.IsAuthenticated) ? HttpContext.Current.User.Identity.Name : "x";
            int managerId = __manager.CreateManagerOrder(userName, culture, address, firstname, lastname, phone, useremail,
                organization, organizationINN, organizationKPP);
            bool buy = false;
            foreach (string x in _cart)
            {
                string[] _x = x.Split('_');
                int _id = 0;
                int _count = 0;
                if (_x.Length == 3)
                {
                    int.TryParse(_x[1], out _id);
                    int.TryParse(_x[2], out _count);
                }
                var product = entities.mytrip_storeproduct
                           .Include("mytrip_storedepartment")
                           .Include("mytrip_storedepartment.mytrip_storedepartment2")
                           .Include("mytrip_storeproducer")
                           .Include("mytrip_storevotes")
                           .Include("mytrip_storesale")
                           .Include("mytrip_storedepartment.mytrip_storesale")
                           .Include("mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale")
                           .Include("mytrip_storeproducer.mytrip_storesale")
                    .FirstOrDefault(z => z.ProductId == _id);
                if (product != null)
                {
                    int sale1 = 0;
                    if ((product.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.Sale > 0 &&
                                   product.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.CloseDate > DateTime.Now) ||
                                   (product.mytrip_storedepartment.mytrip_storesale.Sale > 0 &&
                                   product.mytrip_storedepartment.mytrip_storesale.CloseDate > DateTime.Now) ||
                                   (product.mytrip_storeproducer.mytrip_storesale.Sale > 0 &&
                                   product.mytrip_storeproducer.mytrip_storesale.CloseDate > DateTime.Now) ||
                                   (product.mytrip_storesale.Sale > 0 && product.mytrip_storesale.CloseDate > DateTime.Now))
                    {
                        sale1 = (product.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.CloseDate > DateTime.Now) ? product.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.Sale : 0;
                        int sale2 = (product.mytrip_storesale.CloseDate > DateTime.Now) ? product.mytrip_storesale.Sale : 0;
                        int sale3 = (product.mytrip_storedepartment.mytrip_storesale.CloseDate > DateTime.Now) ? product.mytrip_storedepartment.mytrip_storesale.Sale : 0;
                        int sale4 = (product.mytrip_storeproducer.mytrip_storesale.CloseDate > DateTime.Now) ? product.mytrip_storeproducer.mytrip_storesale.Sale : 0;
                        if (sale2 > sale1)
                            sale1 = sale2;
                        if (sale3 > sale1)
                            sale1 = sale3;
                        if (sale4 > sale1)
                            sale1 = sale4;
                    }
                    decimal price = (sale1 > 0) ? ((product.Price / 100) * (100 - sale1)) : product.Price;
                    mytrip_storeorderisproduct y = new mytrip_storeorderisproduct
                        {
                            ProductId = _id,
                            Price = price,
                            Count = _count,
                            OrderId = managerId,
                            MoneyId = product.MoneyId
                        };
                    entities.mytrip_storeorderisproduct.AddObject(y);
                    entities.SaveChanges();
                    ordermail += product.Title + " (" + _count + " * " + MoneyHelpers.ConvertMoney(product.MoneyId, price) + ") " +
                       MoneyHelpers.ConvertMoney(product.MoneyId, (decimal)(price * _count)) + "<br/>";
                    buy = true;
                }

            }
            if (buy)
            {

                string from = firstname + " " + lastname;
                string client = "<br/>" + firstname + " " + lastname + "<br/>" + useremail + " " + phone
                    + "<br/>" + address;
                string domain = CoreSetting.applicationName();
                string domainlink = "<a href='http://"+domain+"'>"+domain+"</a>";
                var manager = MytripUser.GetUsers(ModuleSetting.roleStoreManager());
                foreach (var item in manager)
                {
                    string email = item._userEmail;
                    if (!string.IsNullOrEmpty(email))
                    {
                        MailMessage msg = new MailMessage();
                        msg.To.Add(email);
                        msg.From = new MailAddress(EmailSetting.from_email(), string.Format(CoreSetting.NameTitlePage(), domain));
                        msg.Subject = string.Format(CoreSetting.NameTitlePage(), StoreLanguage.orderTitle);
                        msg.Body = string.Format(StoreLanguage.ordermanageremail, from, client
                                   , DateTime.Now, " " + managerId + "<br/>" + ordermail, domainlink);
                        msg.IsBodyHtml = true;
                        EmailSetting.SendEmail(msg);
                    }
                }
            }
            else
            {
                __manager.DeleteManagerOrder(managerId);
            }
        }
        public IQueryable<mytrip_storeorderisproduct> GetOrdersForUser(int id)
        {
            return entities.mytrip_storeorderisproduct
                .Include("mytrip_storeproduct")
                .Include("mytrip_storeproduct.mytrip_storedepartment")
                           .Include("mytrip_storeproduct.mytrip_storedepartment.mytrip_storedepartment2")
                           .Include("mytrip_storeproduct.mytrip_storeproducer")
                .Where(x => x.OrderId == id);
        }
        public void CountOrders(int orderid, int productid, int count)
        {
            mytrip_storeorderisproduct y = entities.mytrip_storeorderisproduct.Where(x => x.OrderId == orderid)
              .FirstOrDefault(x => x.ProductId == productid);
            if (y != null)
            {
                y.Count = count;
                entities.SaveChanges();
            }

        }
        public void DeleteOrders(int orderid, int productid)
        {
            mytrip_storeorderisproduct y = entities.mytrip_storeorderisproduct.Where(x => x.OrderId == orderid)
              .FirstOrDefault(x => x.ProductId == productid);
            if (y != null)
            {
                entities.mytrip_storeorderisproduct.DeleteObject(y);
                entities.SaveChanges();
            }

        }
        public void AddPosition(int orderid, int prodyktid)
        {
            var a = entities.mytrip_storeorderisproduct
               .Where(x => x.OrderId == orderid).FirstOrDefault(x => x.ProductId == prodyktid);
            if (a == null)
            {
                var product = entities.mytrip_storeproduct
                            .Include("mytrip_storedepartment")
                                   .Include("mytrip_storedepartment.mytrip_storedepartment2")
                                   .Include("mytrip_storeproducer")
                                   .Include("mytrip_storevotes")
                                   .Include("mytrip_storesale")
                                   .Include("mytrip_storedepartment.mytrip_storesale")
                        .Include("mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale")
                        .Include("mytrip_storeproducer.mytrip_storesale")
                            .FirstOrDefault(z => z.ProductId == prodyktid);
                if (product != null)
                {
                    int sale1 = 0;
                    if ((product.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.Sale > 0 &&
                                   product.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.CloseDate > DateTime.Now) ||
                                   (product.mytrip_storedepartment.mytrip_storesale.Sale > 0 &&
                                   product.mytrip_storedepartment.mytrip_storesale.CloseDate > DateTime.Now) ||
                                   (product.mytrip_storeproducer.mytrip_storesale.Sale > 0 &&
                                   product.mytrip_storeproducer.mytrip_storesale.CloseDate > DateTime.Now) ||
                                   (product.mytrip_storesale.Sale > 0 && product.mytrip_storesale.CloseDate > DateTime.Now))
                    {
                        sale1 = (product.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.CloseDate > DateTime.Now) ? product.mytrip_storedepartment.mytrip_storedepartment2.mytrip_storesale.Sale : 0;
                        int sale2 = (product.mytrip_storesale.CloseDate > DateTime.Now) ? product.mytrip_storesale.Sale : 0;
                        int sale3 = (product.mytrip_storedepartment.mytrip_storesale.CloseDate > DateTime.Now) ? product.mytrip_storedepartment.mytrip_storesale.Sale : 0;
                        int sale4 = (product.mytrip_storeproducer.mytrip_storesale.CloseDate > DateTime.Now) ? product.mytrip_storeproducer.mytrip_storesale.Sale : 0;
                        if (sale2 > sale1)
                            sale1 = sale2;
                        if (sale3 > sale1)
                            sale1 = sale3;
                        if (sale4 > sale1)
                            sale1 = sale4;
                    }
                    decimal price = (sale1 > 0) ? ((product.Price / 100) * (100 - sale1)) : product.Price;
                    mytrip_storeorderisproduct y = new mytrip_storeorderisproduct
                        {
                            ProductId = prodyktid,
                            Price = price,
                            Count = 1,
                            OrderId = orderid,
                            MoneyId = product.MoneyId
                        };
                    entities.mytrip_storeorderisproduct.AddObject(y);
                    entities.SaveChanges();
                }
            }
        }

    }

}
