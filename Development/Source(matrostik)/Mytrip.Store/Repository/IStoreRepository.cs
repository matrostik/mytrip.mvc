using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mytrip.Store.Repository
{
    /// <summary>
    /// 
    /// </summary>
   public class IStoreRepository
   {
       private DepartmentRepository _DepartmentRepository;

       /// <summary>
       /// 
       /// </summary>
       public DepartmentRepository department
       {
           get
           {
               if (_DepartmentRepository == null)
                   _DepartmentRepository = new DepartmentRepository();
               return _DepartmentRepository;
           }
       }
       private ProductRepository _ProductRepository;

       /// <summary>
       /// 
       /// </summary>
       public ProductRepository product
       {
           get
           {
               if (_ProductRepository == null)
                   _ProductRepository = new ProductRepository();
               return _ProductRepository;
           }
       }
       private ProducerRepository _ProducerRepository;

       /// <summary>
       /// 
       /// </summary>
       public ProducerRepository producer
       {
           get
           {
               if (_ProducerRepository == null)
                   _ProducerRepository = new ProducerRepository();
               return _ProducerRepository;
           }
       }
    }
}
