using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Store.Repository.DataEntities;
using System.Web;
using Mytrip.Mvc.Repository;
using System.Net.Mail;
using Mytrip.Mvc.Settings;

namespace Mytrip.Store.Repository
{
    public class ManagerOrderRepository
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
        ProfileRepository _ProfileRepository;
        public ProfileRepository profile
        {
            get
            {
                if (_ProfileRepository == null)
                    _ProfileRepository = new ProfileRepository();
                return _ProfileRepository;
            }
        }
        #endregion
        private int ManageOrderId()
        {
            int catId;
            for (catId = 1; entities.mytrip_storeorder.Count(x => x.OrderId == catId) != 0; catId++) ;
            return catId;
        }
        public int CreateManagerOrder(string username,string culture,string address,
            string firstname,string lastname,string phone,string useremail,
            string organization,
            string organizationINN,
            string organizationKPP)
        {int orderid=ManageOrderId();
            int profileId = profile.CreateProfile(username, address,
             firstname, lastname, phone, useremail,
             organization,
             organizationINN,
             organizationKPP);
            mytrip_storeorder x = new mytrip_storeorder {
                OrderId = orderid,
            Status=0,
            Culture = culture,
            CreationDate = DateTime.Now,
            Delivery=0,
            MoneyId = ModuleSetting.keyMoney().ToUpper(),
            ProfileId = profileId,
                NamberAccount = orderid+1000
            };
            entities.mytrip_storeorder.AddObject(x);
            entities.SaveChanges();
            return x.OrderId;
        }
        public void DeleteManagerOrder(int id)
        {
            mytrip_storeorder z = entities.mytrip_storeorder
                .Include("mytrip_storeorderisproduct")
                .FirstOrDefault(x => x.OrderId == id);
            if (z != null)
            {
                foreach (var y in z.mytrip_storeorderisproduct.ToList())
                {
                    entities.mytrip_storeorderisproduct.DeleteObject(y);
                }
                entities.mytrip_storeorder.DeleteObject(z);
                entities.SaveChanges();
            }
        }
        public void MoveToArhivManagerOrder(int id)
        {
            mytrip_storeorder z = entities.mytrip_storeorder
                .Include("mytrip_storeorderisproduct")
                .FirstOrDefault(x => x.OrderId == id);
            if (z != null && z.Status == 0)
            {
                foreach (var y in z.mytrip_storeorderisproduct.ToList())
                {
                    entities.mytrip_storeorderisproduct.DeleteObject(y);
                }
                entities.mytrip_storeorder.DeleteObject(z);
                entities.SaveChanges();
            }
            else if (z != null && z.Status > 0)
            {
                z.Status = z.Status * 10;
                entities.SaveChanges();

            }
        }
        public void DeleteManagerOrder(int id, string userName)
        {
            mytrip_storeorder z = entities.mytrip_storeorder
                .Include("mytrip_storeorderisproduct")
                .Include("mytrip_storeprofile")
                .Where(x => x.mytrip_storeprofile.UserName == userName)
                .FirstOrDefault(x => x.OrderId == id);
            int status = 0;
            if (z != null && z.Status == 0)
            {
                foreach (var y in z.mytrip_storeorderisproduct.ToList())
                {
                    entities.mytrip_storeorderisproduct.DeleteObject(y);
                }
                entities.mytrip_storeorder.DeleteObject(z);
                entities.SaveChanges();
            }
            else if (z != null && z.Status > 0)
            {
                status = z.Status;
                z.Status = status * 10;
                entities.SaveChanges();

            }
            string domain = HttpContext.Current.Request.Url.Host;
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
                    msg.Body = string.Format(StoreLanguage.orderdelete, userName, id) + " (status " + status + ")";
                    msg.IsBodyHtml = true;
                    EmailSetting.SendEmail(msg);
                }
            }
        }
        public int UpdateOrder(int id, string priceInWords,decimal delivery,
            string moneyid, int? namberaccount)
        {
            mytrip_storeorder z = entities.mytrip_storeorder
                      .FirstOrDefault(x => x.OrderId == id);
            z.PriceInWords = priceInWords;
            z.DateAccount = DateTime.Now;
            z.Delivery = delivery;
            z.ManagerName = HttpContext.Current.User.Identity.Name;
            z.MoneyId = moneyid;
            z.NamberAccount = namberaccount;
            entities.SaveChanges();
            return z.ProfileId;
        }
        public void SetOrdersStatus1(int id, string orders)
        {
            mytrip_storeorder z = entities.mytrip_storeorder
                .Include("mytrip_storeprofile")
                      .FirstOrDefault(x => x.OrderId == id);
            z.Status = 1;
            z.DateAccount = DateTime.Now;
            z.AccountPage = orders;
            entities.SaveChanges();
            string email = z.mytrip_storeprofile.UserEmail;
            if (!string.IsNullOrEmpty(email))
            {

                string domain = UsersSetting.applicationName();
                string domainlink = "<a href='http://" + domain + "'>" + domain + "</a>";
                string accountlink = "<a href='http://" + domain + "/Store/Account/" + id + "'>" + StoreLanguage.orderTitle+" "+z.OrderId + "</a>";
                MailMessage msg = new MailMessage();
                msg.To.Add(email);
                msg.From = new MailAddress(EmailSetting.from_email(), string.Format(CoreSetting.NameTitlePage(), domain));
                msg.Subject = string.Format(CoreSetting.NameTitlePage(), StoreLanguage.orderTitle);
                msg.Body = string.Format(StoreLanguage.accountcustomeremail, accountlink, domainlink);
                msg.IsBodyHtml = true;
                EmailSetting.SendEmail(msg);
            }

        }
        public void SetOrdersStatus2(int id)
        {
            mytrip_storeorder z = entities.mytrip_storeorder
                .Include("mytrip_storeprofile")
                      .FirstOrDefault(x => x.OrderId == id);
            z.Status = 2;
            entities.SaveChanges();
            
        }
        public void ApprovedOrder(int id, string username)
        {
            int profileid = 0;
            mytrip_storeprofile y = entities.mytrip_storeprofile
                .Where(x => x.IsAnonym == true)
                .FirstOrDefault(x => x.UserName == username);
            if (y != null)
            {
                profileid = y.ProfileId;
            }

            mytrip_storeorder z = entities.mytrip_storeorder
                  .FirstOrDefault(x => x.OrderId == id);
            if (z != null && profileid > 0)
            {
                int oldprofileid = z.ProfileId;
                z.ProfileId = profileid;
                entities.SaveChanges();

                mytrip_storeprofile q = entities.mytrip_storeprofile
               .Where(x => x.IsAnonym == false)
               .FirstOrDefault(x => x.ProfileId == oldprofileid);
                entities.mytrip_storeprofile.DeleteObject(q);
                entities.SaveChanges();

            }
            else if (z != null && profileid == 0)
            {
                profileid = z.ProfileId;
                mytrip_storeprofile q = entities.mytrip_storeprofile
               .Where(x => x.IsAnonym == false)
               .FirstOrDefault(x => x.ProfileId == profileid);
                q.IsAnonym = true;
                entities.SaveChanges();
            }
        }
        public mytrip_storeorder GetOrderForManager(int id)
        {
            return entities.mytrip_storeorder
                .Include("mytrip_storeorderisproduct")
                .Include("mytrip_storeprofile")
                .FirstOrDefault(x => x.OrderId == id);
        }
        public IQueryable<mytrip_storeorder> GetOrdersForUser(string username)
        {
            return entities.mytrip_storeorder
                .Include("mytrip_storeorderisproduct")
                .Include("mytrip_storeprofile")
                .Where(x => x.mytrip_storeprofile.IsAnonym == true)
                .Where(x => x.mytrip_storeprofile.UserName == username).OrderByDescending(x=>x.CreationDate);
        }
        public IQueryable<mytrip_storeorder> GetOrdersForUser(string username,int status)
        {
            return entities.mytrip_storeorder
                .Include("mytrip_storeorderisproduct")
                .Include("mytrip_storeprofile")
                .Where(x => x.mytrip_storeprofile.IsAnonym == true)
                .Where(x => x.Status == status)
                .Where(x => x.mytrip_storeprofile.UserName == username).OrderByDescending(x => x.CreationDate);
        }
        public IQueryable<mytrip_storeorder> GetOrdersForUserApproved(string username)
        {
            string email = MytripUser.UserEmail(username);
            return entities.mytrip_storeorder
                .Include("mytrip_storeorderisproduct")
                .Include("mytrip_storeprofile")
                .Where(x => x.mytrip_storeprofile.IsAnonym == false)
                .Where(x => x.mytrip_storeprofile.UserEmail == email)
                .OrderByDescending(x => x.CreationDate);
        }
        public IQueryable<mytrip_storeorder> GetOrdersForManager(int id)
        {
            return entities.mytrip_storeorder
                    .Include("mytrip_storeorderisproduct")
                    .Include("mytrip_storeprofile")
                    .Where(x => x.Status == id)
                    .OrderByDescending(x => x.CreationDate);
        }
        public IDictionary<int,string> FirstName(string userName)
        {
            var x = entities.mytrip_storeorder
                .Include("mytrip_storeprofile")
                .Where(y=>y.mytrip_storeprofile.IsAnonym==true)
                .FirstOrDefault(y => y.mytrip_storeprofile.UserName == userName);
            IDictionary<int,string> result=new Dictionary<int,string>();
            if (x != null)
            {
                result.Add(1, x.mytrip_storeprofile.FirstName);
                result.Add(2, x.mytrip_storeprofile.LastName);
                result.Add(3, x.mytrip_storeprofile.UserEmail);
                result.Add(4, x.mytrip_storeprofile.Phone);
                result.Add(5, x.mytrip_storeprofile.Address);
                result.Add(6, x.mytrip_storeprofile.Organization);
                result.Add(7, x.mytrip_storeprofile.OrganizationINN);
                result.Add(8, x.mytrip_storeprofile.OrganizationKPP);
            }
            else
            {
                result.Add(1, userName);
                result.Add(2, "");
                result.Add(3, MytripUser.UserEmail(userName));
                result.Add(4, "");
                result.Add(5, "");
                result.Add(6, "");
                result.Add(7, "");
                result.Add(8, "");
            }
            return result;
        }
    }
}
