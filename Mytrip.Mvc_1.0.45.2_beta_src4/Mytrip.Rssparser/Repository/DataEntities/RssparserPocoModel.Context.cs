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

namespace Mytrip.Rssparser.Repository.DataEntities
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
    
        public ObjectSet<mytrip_rssparser> mytrip_rssparser
        {
            get { return _mytrip_rssparser  ?? (_mytrip_rssparser = CreateObjectSet<mytrip_rssparser>("mytrip_rssparser")); }
        }
        private ObjectSet<mytrip_rssparser> _mytrip_rssparser;

        #endregion
    }
}
