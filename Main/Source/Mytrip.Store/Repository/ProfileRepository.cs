using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Store.Repository.DataEntities;
using System.Web;

namespace Mytrip.Store.Repository
{
   public class ProfileRepository
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

        #endregion
        private int ProfileId()
        {
            int catId;
            for (catId = 1; entities.mytrip_storeprofile.Count(x => x.ProfileId == catId) != 0; catId++) ;
            return catId;
        }
        public int CreateProfile(string username, string address,
             string firstname, string lastname, string phone, string useremail,
             string organization,
             string organizationINN,
             string organizationKPP)
        {
            mytrip_storeprofile z = entities.mytrip_storeprofile.Where(y => y.IsAnonym == true)
            .FirstOrDefault(y=>y.UserName == HttpContext.Current.User.Identity.Name);
            if (z == null || !HttpContext.Current.User.Identity.IsAuthenticated)
            {
                mytrip_storeprofile x = new mytrip_storeprofile
                {
                    ProfileId = ProfileId(),
                    UserName = username,
                    IsAnonym = HttpContext.Current.User.Identity.IsAuthenticated,
                    Address = address,
                    FirstName = firstname,
                    LastName = lastname,
                    Phone = phone,
                    UserEmail = useremail,
                    Organization = organization,
                    OrganizationINN = organizationINN,
                    OrganizationKPP = organizationKPP,
                    UserIP = HttpContext.Current.Request.UserHostAddress
                };
                entities.mytrip_storeprofile.AddObject(x);
                entities.SaveChanges();
                z = x;
            }
            else if (z != null &&HttpContext.Current.User.Identity.IsAuthenticated)
            { 
            z.UserName = username;
                    z.IsAnonym = HttpContext.Current.User.Identity.IsAuthenticated;
                    z.Address = address;
                    z.FirstName = firstname;
                    z.LastName = lastname;
                    z.Phone = phone;
                    z.UserEmail = useremail;
                    z.Organization = organization;
                    z.OrganizationINN = organizationINN;
                    z.OrganizationKPP = organizationKPP;
                    z.UserIP = HttpContext.Current.Request.UserHostAddress;
            }
            return z.ProfileId;
        }
        public void UpdateProfile(int id,string address,string firstname,
            string lastname,string organization,string organizationINN,
            string organizationKPP,string phone,string email)
        {
            mytrip_storeprofile z = entities.mytrip_storeprofile
                .FirstOrDefault(y => y.ProfileId == id);
            z.Address = address;
            z.FirstName = firstname;
            z.LastName = lastname;
            z.Organization=organization;
            z.OrganizationINN = organizationINN;
            z.OrganizationKPP = organizationKPP;
            z.Phone = phone;
            z.UserEmail = email;
            entities.SaveChanges();
        }
    }
}
