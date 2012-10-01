//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.EntityClient;

namespace mtm.Store.Repository.DataEntities
{
    public partial class Entities : ObjectContext
    {
        public const string ConnectionString = "name=Entities";
        public const string ContainerName = "Entities";
    
        #region Constructors
    
        public Entities()
            : base(ConnectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = false;
        }
    
        public Entities(string connectionString)
            : base(connectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = false;
        }
    
        public Entities(EntityConnection connection)
            : base(connection, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = false;
        }
    
        #endregion
    
        #region ObjectSet Properties
    
        public ObjectSet<mytrip_storedepartment> mytrip_storedepartment
        {
            get { return _mytrip_storedepartment  ?? (_mytrip_storedepartment = CreateObjectSet<mytrip_storedepartment>("mytrip_storedepartment")); }
        }
        private ObjectSet<mytrip_storedepartment> _mytrip_storedepartment;
    
        public ObjectSet<mytrip_storeorderisproduct> mytrip_storeorderisproduct
        {
            get { return _mytrip_storeorderisproduct  ?? (_mytrip_storeorderisproduct = CreateObjectSet<mytrip_storeorderisproduct>("mytrip_storeorderisproduct")); }
        }
        private ObjectSet<mytrip_storeorderisproduct> _mytrip_storeorderisproduct;
    
        public ObjectSet<mytrip_storeproducer> mytrip_storeproducer
        {
            get { return _mytrip_storeproducer  ?? (_mytrip_storeproducer = CreateObjectSet<mytrip_storeproducer>("mytrip_storeproducer")); }
        }
        private ObjectSet<mytrip_storeproducer> _mytrip_storeproducer;
    
        public ObjectSet<mytrip_storeproduct> mytrip_storeproduct
        {
            get { return _mytrip_storeproduct  ?? (_mytrip_storeproduct = CreateObjectSet<mytrip_storeproduct>("mytrip_storeproduct")); }
        }
        private ObjectSet<mytrip_storeproduct> _mytrip_storeproduct;
    
        public ObjectSet<mytrip_storeprofile> mytrip_storeprofile
        {
            get { return _mytrip_storeprofile  ?? (_mytrip_storeprofile = CreateObjectSet<mytrip_storeprofile>("mytrip_storeprofile")); }
        }
        private ObjectSet<mytrip_storeprofile> _mytrip_storeprofile;
    
        public ObjectSet<mytrip_storeseller> mytrip_storeseller
        {
            get { return _mytrip_storeseller  ?? (_mytrip_storeseller = CreateObjectSet<mytrip_storeseller>("mytrip_storeseller")); }
        }
        private ObjectSet<mytrip_storeseller> _mytrip_storeseller;
    
        public ObjectSet<mytrip_storevotes> mytrip_storevotes
        {
            get { return _mytrip_storevotes  ?? (_mytrip_storevotes = CreateObjectSet<mytrip_storevotes>("mytrip_storevotes")); }
        }
        private ObjectSet<mytrip_storevotes> _mytrip_storevotes;
    
        public ObjectSet<mytrip_storeorder> mytrip_storeorder
        {
            get { return _mytrip_storeorder  ?? (_mytrip_storeorder = CreateObjectSet<mytrip_storeorder>("mytrip_storeorder")); }
        }
        private ObjectSet<mytrip_storeorder> _mytrip_storeorder;
    
        public ObjectSet<mytrip_storesale> mytrip_storesale
        {
            get { return _mytrip_storesale  ?? (_mytrip_storesale = CreateObjectSet<mytrip_storesale>("mytrip_storesale")); }
        }
        private ObjectSet<mytrip_storesale> _mytrip_storesale;

        #endregion
    }
}