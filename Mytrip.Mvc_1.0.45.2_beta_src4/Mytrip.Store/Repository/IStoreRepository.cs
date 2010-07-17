using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mytrip.Store.Repository
{
   public class IStoreRepository
   {
       private DepartmentRepository _DepartmentRepository;
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
