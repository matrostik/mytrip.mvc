using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Store.Repository.DataEntities;
using System.Web;

namespace Mytrip.Store.Repository
{
   public class DepartmentRepository
    {
        #region Подключение к Entity Репозиторию
        StoreSettings _core;
        public StoreSettings core
        {
            get
            {
                if (_core == null)
                    _core = new StoreSettings();
                return _core;
            }
        }
        Entities _entities;
        public Entities entities
        {
            get
            {
                if (_entities == null)
                    _entities = new Entities(core.connectionString());
                return _entities;
            }
        }
        #endregion
        #region Другие подключения и переменные
        private string username
        {
            get { return HttpContext.Current.User.Identity.Name; }
        }
        #endregion
        public IQueryable<mytrip_storedepartment> GetAllDepartment(string culture)
        {
            return entities.mytrip_storedepartment
                .Include("mytrip_storedepartment1")
                .Where(x=>x.SubDepartmentId==0)
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .OrderBy(x => x.Title);
        }
        public IQueryable<mytrip_storedepartment> GetAllDepartment(int pageIndex, int pageSize, string culture, out int total)
        {
            var a= entities.mytrip_storedepartment
                .Include("mytrip_storeproduct")
                .Include("mytrip_storedepartment1.mytrip_storeproduct")
                .Where(x => x.SubDepartmentId == 0)
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .OrderBy(x => x.Title);
            total = a.Count();
            return a.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        public IQueryable<mytrip_storedepartment> GetSubDepartment(int id, string culture)
        {
            return entities.mytrip_storedepartment
                .Include("mytrip_storeproduct")
                .Include("mytrip_storedepartment1.mytrip_storeproduct")
                .Where(x => x.SubDepartmentId == id)
                .Where(x => x.Culture == culture || x.AllCulture == true)
                .OrderBy(x => x.Title);
        }
        public mytrip_storedepartment GetDepartment(int id)
        {
            return entities.mytrip_storedepartment
                .Include("mytrip_storeproduct")
                .Include("mytrip_storedepartment2")
                .Include("mytrip_storedepartment1.mytrip_storeproduct")
                .FirstOrDefault(x => x.DepartmentId == id);
        }
        public IDictionary<int,string> GetDepartmentForDdl(string culture)
        {
            IDictionary<int, string> mcats = new Dictionary<int, string>();
            var a = entities.mytrip_storedepartment.Include("mytrip_storedepartment1")
           .Where(x => x.DepartmentId != 0 && x.SubDepartmentId == 0)
           .Where(x => x.Culture == culture || x.AllCulture == true)
           .OrderBy(x => x.Title);
            mcats.Add(0, StoreLanguage.AllDepartment);
            foreach (mytrip_storedepartment cat in a)
            {
                mcats.Add(cat.DepartmentId,cat.Title);
                foreach (mytrip_storedepartment subcat in cat.mytrip_storedepartment1)
                {
                    string Title = "--" + subcat.Title;
                    mcats.Add(subcat.DepartmentId,Title);
                }
            }
            return mcats;
        }
    }
}
