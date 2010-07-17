using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Store.Repository.DataEntities;
//using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Mytrip.Store.Models
{
    //[MetadataType(typeof(DepartmentModel))]
   public class DepartmentModel
    {
       public IQueryable<mytrip_storedepartment> Department { get; set; }
       public int total { get; set; }
       public int take { get; set; }       
       public bool paging { get; set; }
       public bool paging2 { get; set; }
       public TitleDepartmentModel titleDepartmentModel { get; set; }
       public IQueryable<mytrip_storeproduct> Product { get; set; }
       public IQueryable<mytrip_storeproducer> Producer { get; set; }
       public int takepaging { get; set; }
       public string smallprice { get; set; }
       public string bigprice { get; set; }
       public SelectList SelectDepartment { get; set; }
       public SelectList SelectProducer { get; set; }
       public int DepartmentId { get; set; }
       public int ProducerId { get; set; }
       public string Search { get; set; }
       public bool DepartmentAndProducer { get; set; }
       public bool DepartmentAndProducer2 { get; set; }
       
    }
    public class TitleDepartmentModel
    {
        public bool producer { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public string img { get; set; }
        public string subDepartmentTitle { get; set; }
        public string subDepartmentPath { get; set; }
        public int subDepartmentId { get; set; }
        public int count { get; set; }
        public int subcount { get; set; }
        //////
        public bool _search { get; set; }
        public string search { get; set; }
        public int totalsearch { get; set; }
        ///////////
        public int id { get; set; }
        public string path { get; set; }
        public int ProducerId { get; set; }
        public string ProducerTitle { get; set; }
        public string ProducerBody { get; set; }
        public string ProducerImg { get; set; }
        public string ProducerPath { get; set; }
        public int departmentcount { get; set; }
        public int producercount { get; set; }

    }
    public class ProductModel
    {
        public IQueryable<mytrip_storeproduct> Product { get; set; }
    }
}
