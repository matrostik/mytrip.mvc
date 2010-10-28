using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Store.Repository.DataEntities;
using Mytrip.Mvc.Settings;
using Mytrip.Mvc.Repository;

namespace Mytrip.Store.Repository
{
    public class SellerRepository
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
        private int CreateSellerId()
        {
            int catId;
            for (catId = 1; entities.mytrip_storeseller.Count(x => x.SellerId == catId) != 0; catId++) ;
            return catId;
        }
        public void UpdateSeller(string accountant,string address,string bank,
            string bankaccount,string bankaccountBIK,string bankaccountSeller,
            string director,string email,bool liteNDS,
            string organization, string organizationINN, string organizationKPP,
            string phone)
        {  
            mytrip_storeseller y = entities.mytrip_storeseller.FirstOrDefault(); 
           y.Accountant = accountant;
           y.Address=address;
           y.Bank=bank;
           y.BankAccount=bankaccount;
           y.BankAccountBIK=bankaccountBIK;
           y.BankAccountSeller=bankaccountSeller;
           y.Director=director;
           y.Email=email;
           y.LiteNDS=liteNDS;
           y.Organization=organization;
           y.OrganizationINN = organizationINN;
           y.OrganizationKPP = organizationKPP;
           y.Phone = phone;
        entities.SaveChanges();           
            
        }
        public mytrip_storeseller CreateSellerDefault()
        {
            mytrip_storeseller y = new mytrip_storeseller
            {
                SellerId = CreateSellerId(),
                AllCulture = false,
                Culture = LocalisationSetting.culture().ToLower(),
                LiteNDS = true,
                Email=MytripUser.UserEmail()
            };
            entities.mytrip_storeseller.AddObject(y);
            entities.SaveChanges();
            return y;
        }
        public int GetSellerId()
        { 
            return entities.mytrip_storeseller.FirstOrDefault().SellerId; 
        }
        public mytrip_storeseller GetSeller()
        { 
            var x= entities.mytrip_storeseller.FirstOrDefault();
            if (x == null)
                x = CreateSellerDefault();
            return x;
        }
        
    }
}
